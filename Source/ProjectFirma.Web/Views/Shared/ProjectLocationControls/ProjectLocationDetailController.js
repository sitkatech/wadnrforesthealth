
angular.module("ProjectFirmaApp")
    .controller("ProjectLocationDetailController",
    function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
            $scope.hasGeospatialAreaTypeLayers = $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.length > 0;
            $scope.GeospatialAreaMapServiceLayerNamesCommaSeparated = $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.join(",");
            $scope.selectedLocationLeafletID = null;
        
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
            $scope.toggleProjectLocationDetails(newestLeafletID);
            $scope.$apply();
        };

        $scope.deleteProjectLocationRowAndRefreshMap = function (projectLocationLeafletID) {
            //console.log("inside deleteProjectLocationRowAndRefreshMap: " + projectLocationLeafletID);
            var layer = projectFirmaMap.editableFeatureGroup.getLayer(projectLocationLeafletID);
            projectFirmaMap.editableFeatureGroup.removeLayer(layer);//remove from map

            _.remove($scope.AngularModel.ProjectLocationJsons, function(n) {
                return n.ProjectLocationLeafletID == projectLocationLeafletID;
            });
            $scope.$apply();//added because the grid would not update after delete on map was used.
        };

        $scope.isSelectedProjectLocation = function(projectLocation) {
            return $scope.selectedLocationLeafletID == projectLocation.ProjectLocationLeafletID;
        };

        $scope.toggleProjectLocationDetails = function (locationLeafletID) {
            $scope.selectedLocationLeafletID = locationLeafletID;
            //console.log('toggleProjectLocationDetails passed in leafletID :' + locationLeafletID);
            projectFirmaMap.editableFeatureGroup.eachLayer(function (layer) {
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
                    
                } else {
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
                }
            });
        };

        var getUserFriendlyGeometryType = function(geometry) {
            return geometry.type.replace("LineString", "Line");
        };

        var bindProjectLocationSelectClickEvent = function (feature, layer) {
            var leafletID = layer._leaflet_id;
            layer.on('click', function (f) {
                if (layer.editing.enabled()) {
                    return;
                }

                $scope.$apply(function () {
                    $scope.selectedLocationLeafletID = leafletID;
                });
                $scope.toggleProjectLocationDetails(leafletID);
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
                projectFirmaMap = new ProjectFirmaMaps.Map(mapInitJson);

                projectFirmaMap.editableFeatureGroup = new L.FeatureGroup();

                var x = 0;
                var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection, {
                    onEachFeature: function (feature, layer) {
                        if (layer.getLayers) {
                            layer.getLayers().forEach(function (l) { projectFirmaMap.editableFeatureGroup.addLayer(l); });//might not be hit
                        }
                        else {
                            projectFirmaMap.editableFeatureGroup.addLayer(layer);
                            $scope.AngularModel.ProjectLocationJsons[x].ProjectLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
                            bindProjectLocationSelectClickEvent(feature, layer);
                            x++;
                        }
                       
                    }
                });

                var drawOptions = getDrawOptions(projectFirmaMap.editableFeatureGroup);
                var drawControl = new L.Control.Draw(drawOptions);
                projectFirmaMap.map.addControl(drawControl);
                projectFirmaMap.map.addLayer(projectFirmaMap.editableFeatureGroup);

                projectFirmaMap.map.on('draw:created', function (e) {
                    
                    var layer = e.layer;
                    projectFirmaMap.editableFeatureGroup.addLayer(layer);
                    var leafletId = layer._leaflet_id;
                    console.log('draw:created called: ' + leafletId);

                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";

                    var allGeoJson = projectFirmaMap.editableFeatureGroup.toGeoJSON();
                    var tempFeature = allGeoJson.features[allGeoJson.features.length - 1];
                    var newestGeoJson = Terraformer.WKT.convert(tempFeature.geometry);

                    //update grid with new drawing
                    addFeatureToAngularModel(newestGeoJson, leafletId, getUserFriendlyGeometryType(tempFeature.geometry));
                    bindProjectLocationSelectClickEvent(tempFeature, layer);

                });

                projectFirmaMap.map.on('draw:edited', function (e) {
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

                projectFirmaMap.map.on('draw:deleted',
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



