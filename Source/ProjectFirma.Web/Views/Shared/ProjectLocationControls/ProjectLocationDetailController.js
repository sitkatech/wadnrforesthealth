// main idea is that we have a json model representing the ProjectLocations, in AngularModel.projectLocationJsons
// it happens to have a map component via leaflet.  Any changes made via the latlng grid editor or on the location details
// is automatically written to the projectLocationJsons model and will redraw the points on the leaflet map if necessary
// when making edits via leaflet, leaflet will update the projectLocationJsons model and then if necessary redraw
// When saving, we simply post the projectLocationJsons model
var app = angular.module("ProjectLocationDetailApp", ["ngTextTruncate"]);

var controller = app.controller("ProjectLocationDetailController", function ($scope, $http, angularModelAndViewData) {
    var squareMetersToAcresMultiplier = 0.0002471053821147119;
    var feetToMetersMultiplier = 0.3048;

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.projectLocationJsons == null) {
        $scope.AngularModel.projectLocationJsons = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.selectedProjectLocationID = null;
    var currentProjectLocationIDToAdd = -1;

    var getProjectLocationFromProjectLocationJsonsByProjectLocationID = function (projectLocationID) {
        return _.find($scope.AngularModel.ProjectLocationJsons, function (dl) {
            return dl.projectLocationID == projectLocationID;
        });
    };

    var createGeoJSONFeature = function (geometry) {
        return {
            geometry: geometry,
            properties: {},
            type: "Feature"
        };
    };

    var reprojectGeometryAndAddProjectLocationIDToFeature = function (feature, projectLocationID) {
        feature.geometry = kevinMap.reprojectGeoJSONFeatureFrom32100to4326(feature.geometry).geometry;
        feature.properties = { projectLocationID: projectLocationID };
    };

    var convertProjectLocationJsonsToFeatureCollection = function (projectLocationJsons) {
        return {
            features: _.map(projectLocationJsons, function (projectLocationJson) {
                var feature = createGeoJSONFeature(JSON.parse(projectLocationJson.projectLocationGeometry));
                reprojectGeometryAndAddProjectLocationIDToFeature(feature, projectLocationJson.projectLocationID);
                return feature;
            })
        };
    };

    var bindProjectLocationSelectClickEvent = function (feature, layer) {
        var leafletID = layer._leaflet_id;
        layer.on('click', function (f) {
            if (layer.editing.enabled()) {
                return;
            }
            var selectedProjectLocationID = feature.properties.projectLocationID;
            $scope.$apply(function () {
                $scope.selectedProjectLocationID = selectedProjectLocationID;
            });
            $scope.toggleProjectLocations(selectedProjectLocationID);
        });
    };

    var addFeatureCollectionToEditableFeatureGroup = function (projectLocationJsonsAsFeatureCollection) {
        L.geoJson(projectLocationJsonsAsFeatureCollection, {
            onEachFeature: function (feature, layer) {
                if (layer.getLayers) {
                    layer.getLayers().forEach(function (l) {
                        kevinMap.editableFeatureGroup.addLayer(l);
                    });
                } else {
                    kevinMap.editableFeatureGroup.addLayer(layer);
                }
                bindProjectLocationSelectClickEvent(feature, layer);
            }
        });
    };

    var loadExistingProjectLocationsToEditableFeatureGroup = function () {
        var projectLocationJsonsAsFeatureCollection = convertProjectLocationJsonsToFeatureCollection($scope.AngularModel.projectLocationJsons);
        addFeatureCollectionToEditableFeatureGroup(projectLocationJsonsAsFeatureCollection);
    };

    var deleteProjectLocationRow = function (projectLocationID) {
        _.remove($scope.AngularModel.projectLocationJsons, function (dl) {
            return dl.projectLocationID == projectLocationID;
        });
    };

    var displayUserFriendlyGeometryType = function (projectLocationGeometry) {
        return projectLocationGeometry.type == "LineString" ? "Line" : "Polygon";
    };

    var initializeLeafletMap = function () {
        kevinMap.editableFeatureGroup = new L.FeatureGroup();

        // initialize and draw the projectLocations on the map
        loadExistingProjectLocationsToEditableFeatureGroup();
        if (!$scope.AngularViewData.isInCompletedReview) {
            L.drawLocal.draw.toolbar.buttons.polyline = "Draw a projectLocation as a line – good for roads, fences, pipelines, etc.";
            L.drawLocal.draw.toolbar.buttons.polygon = "Draw a projectLocation as a polygon – good for buildings, crops, yards, areas, etc.";
            L.drawLocal.edit.toolbar.buttons.edit = "Edit projectLocation geometries";
            L.drawLocal.edit.toolbar.buttons.editDisabled = "No projectLocation geometries to edit";
            L.drawLocal.edit.toolbar.buttons.remove = "Remove projectLocation geometries";
            L.drawLocal.edit.toolbar.buttons.editRemove = "No projectLocation geometries to remove";

            var polylineOptions = {
                shapeOptions: {
                    color: '#02ffff'
                }
            };
            var polygonOptions = {
                allowIntersection: false, // Restricts shapes to simple polygons
                drawError: {
                    color: '#e1e100', // Color the shape will turn when intersects
                    message: 'Self-intersecting polygons are not allowed.' // Message that will show when intersect
                },
                shapeOptions: {
                    color: '#02ffff'
                }
            };

            var drawOptions = {
                position: 'topleft',
                draw: {
                    polyline: _.some(angularModelAndViewData.AngularViewData.projectLocationTypeJsons, function (f) {
                        return f.geometryTypeName === "Line"
                    }) ? polylineOptions : false,
                    polygon: _.some(angularModelAndViewData.AngularViewData.projectLocationTypeJsons, function (f) {
                        return f.geometryTypeName === "Polygon"
                    }) ? polygonOptions : false,
                    circle: false, // Turns off this drawing tool
                    rectangle: false,
                    marker: false
                },
                edit: {
                    featureGroup: kevinMap.editableFeatureGroup,
                    edit: {
                        maintainColor: true,
                        opacity: 0.3
                    },
                    remove: true
                }
            };
            var drawControl = new L.Control.Draw(drawOptions);
            kevinMap.map.addControl(drawControl);

            // when user draws a point via leaflet, we need to add to our json model and redraw
            kevinMap.map.on('draw:created', function (e) {
                var newLayer = e.layer;
                kevinMap.editableFeatureGroup.addLayer(newLayer);
                var newProjectLocationJson = $scope.addProjectLocationRow(newLayer.toGeoJSON().geometry);
                var leafletID = newLayer._leaflet_id;
                var layer = kevinMap.editableFeatureGroup._layers[leafletID];
                layer.feature = createGeoJSONFeature(null);
                var feature = layer.feature;
                feature.properties.projectLocationID = newProjectLocationJson.projectLocationID;
                newProjectLocationJson.area = $scope.getAreaAndLengthAndGeospatialAttributes(newProjectLocationJson);
                $scope.toggleProjectLocations(newProjectLocationJson.projectLocationID);
                bindProjectLocationSelectClickEvent(feature, layer);
                $scope.$apply();
            });

            // when user edits a point via leaflet, we need to ensure that the new latlng of the point is in the acceptable bounds (i.e. Columbia River Basin)
            // and update the lat lng in the json model
            kevinMap.map.on('draw:edited', function (e) {
                var layers = e.layers;
                layers.eachLayer(function (layer) {
                    var projectLocationID = layer.feature.properties.projectLocationID;
                    var projectLocationJson = getProjectLocationFromProjectLocationJsonsByProjectLocationID(projectLocationID);
                    var projectLocationGeometry = layer.toGeoJSON().geometry;
                    projectLocationJson.projectLocationGeometry = JSON.stringify(projectLocationGeometry);
                    projectLocationJson.projectLocationGeometryType = displayUserFriendlyGeometryType(projectLocationGeometry);
                    projectLocationJson.area = $scope.getAreaAndLengthAndGeospatialAttributes(projectLocationJson);
                    $scope.toggleProjectLocations(projectLocationID);
                });
                $scope.$apply();
            });

            // when user deletes a point via leaflet, we need to remove from the json model
            // if the point deleted is the selected one, we need to change the selected point to be the first point in the grid
            kevinMap.map.on('draw:deleted', function (e) {
                var layers = e.layers;
                var projectLocationIDsDeleted = [];
                layers.eachLayer(function (layer) {
                    var projectLocationID = layer.feature.properties.projectLocationID;
                    projectLocationIDsDeleted.push(projectLocationID);
                    deleteProjectLocationRow(projectLocationID);
                });
                if (_.indexOf(projectLocationIDsDeleted, $scope.selectedProjectLocationID) >= 0) {
                    $scope.toggleProjectLocations($scope.AngularModel.projectLocationJsons.length > 0 ? $scope.AngularModel.projectLocationJsons[0].projectLocationID : null);
                }
                $scope.$apply();
            });
        }
        kevinMap.map.addLayer(kevinMap.editableFeatureGroup);

        var selectedProjectLocationID = $scope.AngularModel.projectLocationJsons.length > 0 ? $scope.AngularModel.projectLocationJsons[0].projectLocationID : null;
        $scope.$apply(function () {
            $scope.selectedProjectLocationID = selectedProjectLocationID;
        });

        $scope.toggleProjectLocations(selectedProjectLocationID);
    };

    $scope.toggleProjectLocations = function (projectLocationID) {
        $scope.selectedProjectLocationID = projectLocationID;
        kevinMap.editableFeatureGroup.eachLayer(function (layer) {
            var currentProjectLocationID = layer.feature.properties.projectLocationID;
            if ($scope.selectedProjectLocationID == currentProjectLocationID) {
                layer.setStyle({
                    color: '#fff200',
                    fillColor: '#fff200',
                    weight: 6,
                    opacity: 0.6
                })
            } else {
                if (currentProjectLocationID < 0) {
                    layer.setStyle({
                        color: '#02ffff',
                        fillColor: '#02ffff',
                        weight: 3,
                        opacity: 0.6
                    });
                } else {
                    layer.setStyle({
                        color: '#02ffff',
                        fillColor: '#02ffff',
                        weight: 3,
                        opacity: 0.6
                    });
                }
            }
        });
    };

    $scope.addProjectLocationRow = function (projectLocationGeometry) {
        var newProjectLocationJson = {
            projectLocationID: currentProjectLocationIDToAdd,
            projectLocationName: null,
            projectLocationGeometry: JSON.stringify(projectLocationGeometry),
            projectLocationGeometryType: displayUserFriendlyGeometryType(projectLocationGeometry),
            projectLocationTypeID: null,
            notes: null
        };
        currentProjectLocationIDToAdd--;

        $scope.AngularModel.projectLocationJsons.push(newProjectLocationJson);
        $scope.selectedProjectLocationID = newProjectLocationJson.projectLocationID;
        return newProjectLocationJson;
    };

    $scope.deleteProjectLocationRowAndRefreshMap = function (projectLocation) {
        if (!$scope.AngularViewData.isInCompletedReview) {
            var projectLocationID = projectLocation.projectLocationID;
            deleteProjectLocationRow(projectLocationID);
            kevinMap.editableFeatureGroup.eachLayer(function (layer) {
                if (projectLocationID == layer.feature.properties.projectLocationID) {
                    kevinMap.editableFeatureGroup.removeLayer(layer);
                }
            });

            if (projectLocationID == $scope.selectedProjectLocationID) {
                $scope.selectedProjectLocationID = $scope.AngularModel.projectLocationJsons.length > 0 ? $scope.AngularModel.projectLocationJsons[0].projectLocationID : null;
            }
            $scope.toggleProjectLocations($scope.selectedProjectLocationID);
        }
    };

    $scope.isSelectedProjectLocation = function (projectLocation) {
        return $scope.selectedProjectLocationID == projectLocation.projectLocationID;
    };

    $scope.isProjectLocationALine = function (projectLocation) {
        return projectLocation.projectLocationGeometryType == "Line";
    };

    $scope.isFormInvalid = function () {
        return $scope.AngularModel.projectLocationJsons.length > 0 && _.some($scope.AngularModel.projectLocationJsons, function (dl) {
            return Sitka.Methods.isUndefinedNullOrEmpty(dl.projectLocationTypeID);
        });
    };

    $scope.getSelectableProjectLocationTypes = function (projectLocation) {
        return $scope.AngularViewData.projectLocationTypeJsons;
        // return _.filter($scope.AngularViewData.projectLocationTypeJsons, function (f) {
        //     return f.geometryTypeName == projectLocation.projectLocationGeometryType;
        // });
    };

    $scope.getProjectLocationTypeName = function (projectLocationTypeID) {
        var projectLocationType = _.find($scope.AngularViewData.projectLocationTypeJsons, function (f) {
            return parseInt(f.projectLocationTypeID) == parseInt(projectLocationTypeID);
        });
        if (projectLocationType != null) {
            return projectLocationType.projectLocationTypeName;
        }
        return "Unknown ProjectLocation Type";
    };

    $scope.isProjectLocationTypeValidForGeometryType = function (projectLocationTypeID, geometryType) {
        return _.find($scope.AngularViewData.projectLocationTypeJsons, function (f) {
            return f.geometryTypeName == geometryType && parseInt(f.projectLocationTypeID) == parseInt(projectLocationTypeID);
        });
    };

    $scope.getProjectLocationsWithProjectLocationTypesNotAlignedWithGeometryType = function () {
        return _.filter($scope.AngularModel.projectLocationJsons, function (f) {
            return !Sitka.Methods.isUndefinedNullOrEmpty(f.projectLocationTypeID) && $scope.isProjectLocationTypeValidForGeometryType(f.projectLocationTypeID, f.projectLocationGeometryType) == null;
        });
    };

    $scope.getNumberOfLocationsWithoutProjectLocationTypeIDMessage = function () {
        var numberOfProjectLocationsWithoutProjectLocationTypeID = _.filter($scope.AngularModel.projectLocationJsons, function (f) {
            return Sitka.Methods.isUndefinedNullOrEmpty(f.projectLocationTypeID);
        }).length;
        if (numberOfProjectLocationsWithoutProjectLocationTypeID > 0) {
            return numberOfProjectLocationsWithoutProjectLocationTypeID + " missing a ProjectLocation Type!";
        }
        return "";
    };

    $scope.calculateArea = function (projectLocation) {
        var area = projectLocation.area;
        if (isNaN(area)) {
            return "";
        }
        return area.toLocaleString('en-US', { maximumFractionDigits: 3 });
    };

    $scope.displayLength = function (projectLocation) {
        if (projectLocation.projectLocationGeometryType == "Line" && !Sitka.Methods.isUndefinedNullOrEmpty(projectLocation.length)) {
            return projectLocation.length.toLocaleString('en-US', { maximumFractionDigits: 1 });
        }
        return "";
    };

    $scope.getAreaAndLengthAndGeospatialAttributes = function (projectLocation) {
        var projectLocationGeometry = $scope.getReprojectedGeoJSON(projectLocation);
        var postData = { "projectLocationGeometry": JSON.stringify(projectLocationGeometry), "width": projectLocation.width };
        var postUrl = '/ProposedProject/getGeospatialAttributesForProjectLocation';
        var successHandler = function (result) {
            projectLocation.area = result.data.area;
            projectLocation.length = result.data.length;
            projectLocation.habitatClassifications = result.data.habitatClassifications;
            projectLocation.counties = result.data.counties;
            projectLocation.townshipRangeSections = result.data.townshipRangeSections;
            projectLocation.landOwnershipOwners = result.data.landOwnershipOwners;
        };
        var errorHandler = function (f) {
            console.log("failed to calculate geospatial attributes!");
        };
        $http.post(postUrl, postData).then(successHandler, errorHandler);
    };

    $scope.getAreaAndLength = function (projectLocation) {
        var projectLocationGeometry = $scope.getReprojectedGeoJSON(projectLocation);
        var postData = { "projectLocationGeometry": JSON.stringify(projectLocationGeometry), "width": projectLocation.width };
        var postUrl = '/ProposedProject/getAreaForProjectLocation';
        var successHandler = function (result) {
            projectLocation.area = result.data.area;
        };
        var errorHandler = function (f) {
            console.log("failed to calculate area!");
        };
        $http.post(postUrl, postData).then(successHandler, errorHandler);
    };

    $scope.getReprojectedGeoJSON = function (projectLocation) {
        var geoJSONReprojected = null;
        kevinMap.editableFeatureGroup.eachLayer(function (layer) {
            if (projectLocation.projectLocationID == layer.feature.properties.projectLocationID) {
                geoJSONReprojected = kevinMap.reprojectGeoJSONFeatureFrom4326to32100(layer.toGeoJSON()).geometry;
            }
        });
        return geoJSONReprojected;
    };

    $scope.reprojectGeometries = function () {
        _.each($scope.AngularModel.projectLocationJsons, function (projectLocation) {
            projectLocation.projectLocationGeometry = JSON.stringify($scope.getReprojectedGeoJSON(projectLocation));
        });
    };

    var LayerForProjectLocationPage = function (layerName, geoJsonFeature, featureStyle, layerInitialVisibility, displayOrder, popupLabel) {
        this.LayerInitialVisibility = layerInitialVisibility;
        this.LayerName = layerName;
        this.FeatureStyle = featureStyle;
        this.GeoJsonFeatureCollection = geoJsonFeature;
        this.HasCustomPopups = false;
        this.DisplayOrder = displayOrder;
        this.PopupLabel = popupLabel;
    };

    // this is the call that initializes the map and worksites
    var kevinMap = new KevinMaps.Map($scope.AngularViewData.projectLocationMapInitJson);
    var previewMap = new KevinMaps.Map($scope.AngularViewData.previewMapInitJson);
    var previewMapDiv = jQuery("#" + $scope.AngularViewData.previewMapInitJson.MapDivID);
    kevinMap.loadMapLayers(function () {
        previewMap.orderedLayers = kevinMap.orderedLayers;
        previewMap.loadMapLayersImpl();

        var previewMapLayerGroups = [];
        _.forEach([
            $scope.AngularViewData.projectLocationLayerGeoJson,
            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson
            //, $scope.AngularViewData.distanceToNearestLekLayerGeoJson
        ], function (layer) {
            previewMapLayerGroups.push(previewMap.addSingleVectorLayer(layer));
        });
        initializeLeafletMap();
        if ($scope.AngularViewData.hidePreviewMapOnLoad) {
            previewMapDiv.hide();
        } else {
            $scope.resetBoundsToExtentOfLayerGroups(previewMap.map, previewMapLayerGroups);
        }
        $scope.resetBoundsToExtentOfLayerGroups(kevinMap.map, [kevinMap.editableFeatureGroup]);
    });

    $scope.resetBoundsToExtentOfLayerGroups = function (map, layerGroups) {
        if (layerGroups.length > 0) {
            var totalBounds = _(layerGroups).filter(function (f) { return f.getLayers().length > 0; }).reduce(function (carry, n) {
                return carry ? carry.extend(n.getBounds()) : n.getBounds();
            }, null);
            if (totalBounds != null) {
                map.fitBounds(totalBounds);
            }
        }
    };

    $scope.fetchDdctPreview = function () {
        jQuery(".previewDdctAlert").alert("close");
        $scope.previewTriggered = true;

        jQuery(".ddctResultContent").hide();
        jQuery(".ddctResultLoading").show();
        previewMapDiv.css("display", "");
        previewMapDiv.addClass("hiddenWhileLoading");

        var postData = { "projectID": $scope.AngularViewData.projectID };
        for (var i = 0; i < $scope.AngularModel.projectLocationJsons.length; i++) {
            var projectLocationJson = $scope.AngularModel.projectLocationJsons[i];
            postData['previewProjectLocations[' + i + '].projectLocationGeometry'] = JSON.stringify($scope.getReprojectedGeoJSON(projectLocationJson));
            postData['previewProjectLocations[' + i + '].width'] = projectLocationJson.width;
        }

        jQuery.ajax($scope.AngularViewData.previewDdctUrl, {
            "method": "POST",
            "data": postData,
            "success": function (data) {
                if (data.errors) {
                    handlePreviewDdctError(data.errors);
                    return;
                }

                jQuery(".ddctResultsContainer").html(jQuery(data.ddctTableHtml));
                var previewMapLayerGroups = [];
                var projectLocationsLayerGeoJson = previewMap.addSingleVectorLayer(
                    // Recreate layer using incoming geometry data
                    new LayerForProjectLocationPage(
                        $scope.AngularViewData.projectLocationLayerGeoJson.LayerName,
                        data.projectLocationsGeoJSON,
                        $scope.AngularViewData.projectLocationLayerGeoJson.FeatureStyle,
                        $scope.AngularViewData.projectLocationLayerGeoJson.LayerInitialVisibility,
                        $scope.AngularViewData.projectLocationLayerGeoJson.DisplayOrder,
                        $scope.AngularViewData.projectLocationLayerGeoJson.PopupLabel));
                previewMapLayerGroups.push(projectLocationsLayerGeoJson);
                if (data.ddctAnalysisAreaGeoJson.features && data.ddctAnalysisAreaGeoJson.features.length) {
                    var ddctAnalysisAreaLayerGeoJson = previewMap.addSingleVectorLayer(
                        // Recreate layer using incoming geometry data
                        new LayerForProjectLocationPage(
                            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.LayerName,
                            data.ddctAnalysisAreaGeoJson,
                            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.FeatureStyle,
                            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.LayerInitialVisibility,
                            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.DisplayOrder,
                            $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.PopupLabel));
                    previewMapLayerGroups.push(ddctAnalysisAreaLayerGeoJson);
                }
                else {
                    previewMap.removeSingleVectorLayer(new LayerForProjectLocationPage(
                        $scope.AngularViewData.ddctAnalysisAreaLayerGeoJson.LayerName,
                        null,
                        null,
                        null,
                        null,
                        null));
                }
                // if (data.distanceToNearestLekLayerGeoJson.features && data.distanceToNearestLekLayerGeoJson.features.length) {
                //     var distanceToNearestLekLayerAsLayerGeoJson = previewMap.addSingleVectorLayer(
                //         // Recreate layer using incoming geometry data
                //         new LayerForProjectLocationPage(
                //             $scope.AngularViewData.distanceToNearestLekLayerGeoJson.LayerName,
                //             data.distanceToNearestLekLayerGeoJson,
                //             $scope.AngularViewData.distanceToNearestLekLayerGeoJson.FeatureStyle,
                //             $scope.AngularViewData.distanceToNearestLekLayerGeoJson.LayerInitialVisibility,
                //             $scope.AngularViewData.distanceToNearestLekLayerGeoJson.DisplayOrder,
                //             $scope.AngularViewData.distanceToNearestLekLayerGeoJson.PopupLabel));
                //     previewMapLayerGroups.push(distanceToNearestLekLayerAsLayerGeoJson);
                // }
                // else {
                //     previewMap.removeSingleVectorLayer(new LayerForProjectLocationPage(
                //         $scope.AngularViewData.distanceToNearestLekLayerGeoJson.LayerName,
                //         null,
                //         null,
                //         null,
                //         null,
                //         null));
                // }
                $scope.resetBoundsToExtentOfLayerGroups(previewMap.map, previewMapLayerGroups);
            },
            "error": function () {
                handlePreviewDdctError(["There was a problem fetching Preview of DDCT Results."]);
                jQuery(".ddctResultContent").show();
            },
            "complete": function () {
                $scope.previewTriggered = false;
                $scope.$apply();
                previewMapDiv.removeClass("hiddenWhileLoading");
                jQuery(".ddctResultLoading").hide();
            }
        });
    };

    var handlePreviewDdctError = function (errors) {
        var alertElement = jQuery(
            "<div class=\"alert alert-danger alert-dismissable previewDdctAlert\">" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
            "<span aria-hidden=\"true\">&times;</span>" +
            "</button>" +
            _.join(errors, "<br>") +
            "</div>");
        jQuery(".previewDdctSubHeader").append(alertElement);
    };

    var genericUploadGisErrorMessage = "";
    $scope.uploadGisFile = function () {
        var uploadGisModal = jQuery("#uploadGisModal");
        uploadGisModal.find(".progress").show();

        var form = document.getElementById("uploadGisForm"),
            request = new XMLHttpRequest();
        request.open("POST", form.action);
        request.onload = function () {
            if (request.status === 200) {
                var response;
                if (typeof request.response === "object") {
                    response = request.response;
                } else if (typeof request.response === "string") {
                    try {
                        response = JSON.parse(request.response);
                    } catch (e) {
                        console.error(e);
                        handleUploadGisFileError(genericUploadGisErrorMessage);
                        return;
                    }
                } else {
                    // Invalid response type
                    handleUploadGisFileError(genericUploadGisErrorMessage);
                    return;
                }

                if (response.errors) {
                    handleUploadGisFileError(_.join(response.errors, '<br>'));
                } else if (!(response.uploadedFeatureCollection && response.uploadedFeatureCollection.features && response.uploadedFeatureCollection.features.length)) {
                    handleUploadGisFileError("No features were found in the uploaded file.");
                } else if (response.uploadedFeatureCollection.features.length + $scope.AngularModel.projectLocationJsons.length > $scope.AngularViewData.maxNumberOfProjectLocations) {
                    handleUploadGisFileError(
                        "<p>You may only have up to " + $scope.AngularViewData.maxNumberOfProjectLocations + " projectLocations on a given project. " +
                        "Please simplify your project’s projectLocations and try again, or contact <a href=\"mailto:sagegrouse@mt.gov\">support</a> for help.</p>");
                } else {
                    var featuresReprojectedTo4326 = [];
                    _.forEach(response.uploadedFeatureCollection.features, function (feature) {
                        var newProjectLocationJson = $scope.addProjectLocationRow(feature.geometry);
                        var newFeature = createGeoJSONFeature(feature.geometry);
                        reprojectGeometryAndAddProjectLocationIDToFeature(newFeature, newProjectLocationJson.projectLocationID);
                        featuresReprojectedTo4326.push(newFeature);
                    });
                    addFeatureCollectionToEditableFeatureGroup(featuresReprojectedTo4326);
                    $scope.resetBoundsToExtentOfLayerGroups(kevinMap.map, [kevinMap.editableFeatureGroup]);
                    $scope.$apply();
                    jQuery("#uploadGisInputFile").val("");
                    var uploadGisInputType = jQuery("#uploadGisInputType");
                    uploadGisInputType.val("");
                    uploadGisInputType.trigger("change");

                    jQuery(".uploadGisAlert").alert("close");
                    jQuery("#uploadGisModal").modal("hide");
                }
            } else {
                handleUploadGisFileError(genericUploadGisErrorMessage);
            }
            uploadGisModal.find(".progress").hide();
        };
        request.send(new FormData(form));
    };

    var handleUploadGisFileError = function (err) {
        jQuery(".uploadGisAlert").alert("close");
        var alertElement = jQuery(
            "<div class=\"alert alert-danger alert-dismissable uploadGisAlert\">" +
            "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
            "<span aria-hidden=\"true\">&times;</span>" +
            "</button>" +
            err +
            "</div>");
        jQuery("#uploadGisModal").find(".modal-body")
            .prepend(alertElement);
    };

    $scope.getProjectLocationError = function (rowIndex, fieldName) {
        return _.find($scope.AngularModel.fieldErrors, function (f) { return f.field === "projectLocationJsons[" + rowIndex + "]." + fieldName; });
    };

    $scope.getProjectLocationErrorMessage = function (rowIndex, fieldName) {
        var fieldError = $scope.getProjectLocationError(rowIndex, fieldName);
        return fieldError != null ? fieldError.defaultMessage : "";
    };

    $scope.hasProjectLocationError = function (rowIndex, fieldName) {
        var fieldError = $scope.getProjectLocationError(rowIndex, fieldName);
        return fieldError != null;
    };
});