
angular.module("ProjectFirmaApp")
    .controller("ProjectLocationDetailController",
    function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
            $scope.hasGeospatialAreaTypeLayers = $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.length > 0;
            $scope.GeospatialAreaMapServiceLayerNamesCommaSeparated = $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.join(",");
            $scope.selectedLocationLeafletID = null;
            $scope.projectFirmaMap = null;

            //console.log(JSON.stringify($scope.AngularModel));
        
            $scope.IconStyleDefault = new L.icon({
                iconUrl: 'https://api.tiles.mapbox.com/v3/marker/pin-m-marker+0000ff.png'
            });
            $scope.IconStyleSelected = new L.icon({
                iconUrl: 'https://api.tiles.mapbox.com/v3/marker/pin-m-marker+fff200.png'
            });


            $scope.selectedStyle = {
                fillColor: "#FFFF00",
                fill: true,
                fillOpacity: 0.2,
                color: "#FFFF00",
                weight: 5,
                stroke: true
            };

            $scope.unselectedStyle = {
                fillColor: "#405d74",
                fill: true,
                fillOpacity: 0.2,
                color: "#405d74",
                weight: 1,
                stroke: true
        };

        var createProjectLocationJson = function (wellKnownText, projectLocationId, featureType, locationTypeId, locationName, leafletID) {
            return {
                ProjectLocationGeometryWellKnownText: wellKnownText,
                ProjectLocationID: projectLocationId,
                ProjectLocationFeatureType: featureType,
                ProjectLocationTypeID: locationTypeId,
                ProjectLocationName: locationName,
                ProjectLocationLeafletID: leafletID
            };
        };

        var addFeatureToAngularModel = function (newestGeoJson, newestLeafletID, featureTypeName) {
            //console.log('addFeatureToAngularModel');

            var locationJson = createProjectLocationJson(newestGeoJson, -1, featureTypeName, '', '', newestLeafletID);
            $scope.AngularModel.ProjectLocationJsons.push(locationJson);
            $scope.toggleProjectLocationDetails($scope.projectFirmaMap.editableFeatureGroup, newestLeafletID);
            $scope.$apply();
        };

        $scope.deleteProjectLocationRowAndRefreshMap = function (projectLocationLeafletID) {
            //console.log("inside deleteProjectLocationRowAndRefreshMap: " + projectLocationLeafletID);
            var layer = $scope.projectFirmaMap.editableFeatureGroup.getLayer(projectLocationLeafletID);
            $scope.projectFirmaMap.editableFeatureGroup.removeLayer(layer);//remove from map

            _.remove($scope.AngularModel.ProjectLocationJsons, function(n) {
                return n.ProjectLocationLeafletID == projectLocationLeafletID;
            });
            $scope.$apply();//added because the grid would not update after delete on map was used.
        };

        $scope.isSelectedProjectLocation = function(projectLocation) {
            return $scope.selectedLocationLeafletID == projectLocation.ProjectLocationLeafletID;
        };

        $scope.toggleProjectLocationDetails = function (featureGroup, locationLeafletID) {
            $scope.selectedLocationLeafletID = locationLeafletID;
            //console.log('toggleProjectLocationDetails passed in leafletID :' + locationLeafletID);

            // turn all highlighting off
            $scope.projectFirmaMap.editableFeatureGroup.eachLayer(function (layer) {
                if (layer._icon) {
                    layer.setIcon($scope.IconStyleDefault);
                } else {
                    layer.setStyle({
                        color: '#02ffff',
                        fillColor: '#02ffff',
                        weight: 3,
                        opacity: 0.6
                    });
                }
            });
            $scope.projectFirmaMap.arcFeatureGroup.eachLayer(function (layer) {
                if (layer._icon) {
                    layer.setIcon($scope.IconStyleDefault);
                } else {
                    layer.setStyle({
                        color: '#02ffff',
                        fillColor: '#02ffff',
                        weight: 3,
                        opacity: 0.6
                    });
                }
            });

            // turn the selected one on
            featureGroup.eachLayer(function (layer) {
                var currentLocationLeafletID = layer._leaflet_id;
                if ($scope.selectedLocationLeafletID == currentLocationLeafletID) {
                    if (layer._icon) {
                        layer.setIcon($scope.IconStyleSelected);
                    } else {
                        layer.setStyle({
                            color: '#fff200',
                            fillColor: '#fff200',
                            weight: 6,
                            opacity: 0.6
                        });
                    }
                    
                }
            });
        };


        $scope.checkLocationDupeFunction = function (locationName)
        {
            //console.log('checkLocationDupeFunction[locationName]: ' + locationName);
            if ($scope.AngularModel.ProjectLocationJsons.length < 2) {
                return true;//1 or 0 items in list so no need to check for duplicates
            }
            
            var duplicates = _.filter($scope.AngularModel.ProjectLocationJsons, function (plj) { return plj.ProjectLocationName == locationName; });
            var hasDuplicates = duplicates.length > 1;

            // disable/enable save buttons on parent control / page / etc.
            enableOrDisableSaveButtonsForLocationDetailControl(hasDuplicates);

            return hasDuplicates;
        };

        var getUserFriendlyGeometryType = function(geometry) {
            return geometry.type.replace("LineString", "Line");
        };

        var bindProjectLocationSelectClickEvent = function (featureGroup, layer)
        {
            var leafletID = layer._leaflet_id;
            layer.on('click', function (f) {
                if (layer.editing.enabled()) {
                    return;
                }

                $scope.$apply(function () {
                    $scope.selectedLocationLeafletID = leafletID;
                });
                $scope.toggleProjectLocationDetails(featureGroup, leafletID);
            });
        };

        var getProjectLocationFromProjectLocationJsonsByLeafletID = function (leafletID) {
            return _.find($scope.AngularModel.ProjectLocationJsons, function (pl) {
                return pl.ProjectLocationLeafletID == leafletID;
            });
        };



        var initializeMap = function () {
                //console.log('initializeMap');
                var mapInitJson = $scope.AngularViewData.MapInitJson;
                var editableFeatureJsonObject = $scope.AngularViewData.EditableLayerGeoJson;
                var arcGisFeatureJsonObject = $scope.AngularViewData.ArcGisLayerGeoJson;
                $scope.projectFirmaMap = new ProjectFirmaMaps.Map(mapInitJson);

                $scope.projectFirmaMap.editableFeatureGroup = new L.FeatureGroup();
                var x = 0;
                var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection, {
                    onEachFeature: function (feature, layer) {
                        if (layer.getLayers) {
                            layer.getLayers().forEach(function (l) { $scope.projectFirmaMap.editableFeatureGroup.addLayer(l); });//might not be hit
                        }
                        else {
                            $scope.projectFirmaMap.editableFeatureGroup.addLayer(layer);
                            $scope.AngularModel.ProjectLocationJsons[x].ProjectLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
                            bindProjectLocationSelectClickEvent($scope.projectFirmaMap.editableFeatureGroup, layer);
                            x++;
                        }
                       
                    }
                });

                var drawOptions = getDrawOptions($scope.projectFirmaMap.editableFeatureGroup);
                var drawControl = new L.Control.Draw(drawOptions);
                $scope.projectFirmaMap.map.addControl(drawControl);
                $scope.projectFirmaMap.map.addLayer($scope.projectFirmaMap.editableFeatureGroup);


                $scope.projectFirmaMap.arcFeatureGroup = new L.FeatureGroup();
                var arcCounter = 0;
                var arcLayerGroup = L.geoJson(arcGisFeatureJsonObject.GeoJsonFeatureCollection, {
                    onEachFeature: function (feature, layer) {
                        if (layer.getLayers) {
                            layer.getLayers().forEach(function (l) { $scope.projectFirmaMap.arcFeatureGroup.addLayer(l); });//might not be hit
                        }
                        else {
                            $scope.projectFirmaMap.arcFeatureGroup.addLayer(layer);
                            $scope.AngularModel.ArcGisProjectLocationJsons[arcCounter].ProjectLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
                            bindProjectLocationSelectClickEvent($scope.projectFirmaMap.arcFeatureGroup, layer);
                            arcCounter++;
                        }

                    }
                });
                $scope.projectFirmaMap.map.addLayer($scope.projectFirmaMap.arcFeatureGroup);

                

                $scope.projectFirmaMap.map.on('draw:created', function (e) {
                    
                    var layer = e.layer;
                    $scope.projectFirmaMap.editableFeatureGroup.addLayer(layer);
                    var leafletId = layer._leaflet_id;
                    //console.log('draw:created called: ' + leafletId);

                    $scope.projectFirmaMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                    $scope.projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                    $scope.projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";

                    var allGeoJson = $scope.projectFirmaMap.editableFeatureGroup.toGeoJSON();
                    var tempFeature = allGeoJson.features[allGeoJson.features.length - 1];
                    var newestGeoJson = Terraformer.WKT.convert(tempFeature.geometry);

                    //update grid with new drawing
                    addFeatureToAngularModel(newestGeoJson, leafletId, getUserFriendlyGeometryType(tempFeature.geometry));
                    bindProjectLocationSelectClickEvent(tempFeature, layer);

                });

                $scope.projectFirmaMap.map.on('draw:edited', function (e) {
                    //console.log('draw:edited called');
                    var layers = e.layers;
                    layers.eachLayer(function (layer) {
                        var currentLeafletID = layer._leaflet_id;
                        var projectLocationObject = getProjectLocationFromProjectLocationJsonsByLeafletID(currentLeafletID);
                        var newGeometry = layer.toGeoJSON().geometry;
                        projectLocationObject.ProjectLocationGeometryWellKnownText = Terraformer.WKT.convert(newGeometry);
                        projectLocationObject.ProjectLocationFeatureType = getUserFriendlyGeometryType(newGeometry);
                    });
                    $scope.$apply();
                });

                $scope.projectFirmaMap.map.on('draw:deleted',
                    function(e) {
                        //console.log('draw:deleted called');
                        for (var layer in e.layers._layers) {
                            if (e.layers._layers.hasOwnProperty(layer)) {
                                $scope.deleteProjectLocationRowAndRefreshMap(layer);
                            }
                        }
                });

            };

            initializeMap();

    });



