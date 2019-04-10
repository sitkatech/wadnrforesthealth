
angular.module("ProjectFirmaApp")
    .controller("ProjectMapController",
    function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
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

        //var addFeatureToAngularModel = function (newestGeoJson, newestLeafletID, featureTypeName) {
        //    console.log('addFeatureToAngularModel');

        //    var locationJson = createProjectLocationJson(newestGeoJson, -1, featureTypeName, '', '', newestLeafletID);
        //    $scope.AngularModel.ProjectLocationJsons.push(locationJson);
        //    $scope.toggleProjectLocationDetails(newestLeafletID);
        //    $scope.$apply();
        //};

        $scope.isSelectedProjectLocation = function(projectLocation) {
            return $scope.selectedLocationLeafletID == projectLocation.ProjectLocationLeafletID;
        };

        $scope.toggleProjectLocationDetails = function (locationLeafletID) {
            $scope.selectedLocationLeafletID = locationLeafletID;
            console.log('toggleProjectLocationDetails passed in leafletID :' + locationLeafletID);
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
            return _.find($scope.AngularViewData.ProjectLocationJsons, function (pl) {
                return pl.ProjectLocationLeafletID == leafletID;
            });
        };



        var initializeMap = function () {
                console.log('initializeMap');
                var mapInitJson = $scope.AngularViewData.MapInitJson;
                var layerGeoJsonObject = $scope.AngularViewData.LayerGeoJson;

                debugger;

                
                projectFirmaMap.editableFeatureGroup = new L.FeatureGroup();

                var x = 0;
                

            //var layerGroup = L.geoJson(layerGeoJsonObject.GeoJsonFeatureCollection, {
            //    onEachFeature: function (feature, layer) {
            //        if (layer.getLayers) {
            //            //layer.getLayers().forEach(function (l) { projectFirmaMap.editableFeatureGroup.addLayer(l); });//might not be hit
            //        }
            //        else {
            //            projectFirmaMap.editableFeatureGroup.addLayer(layer);
            //            //debugger;
            //            $scope.AngularViewData.ProjectLocationJsons[x].ProjectLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
            //            bindProjectLocationSelectClickEvent(feature, layer);
            //            x++;
            //        }

            //    }
            //});

            jQuery.each(projectFirmaMap.projectLocationsLayer._layers, function(index, layer) {
                if (layer.getLayers) {
                    //layer.getLayers().forEach(function (l) { projectFirmaMap.editableFeatureGroup.addLayer(l); });//might not be hit
                }
                else {
                    //projectFirmaMap.editableFeatureGroup.addLayer(layer);
                    debugger;
                    $scope.AngularViewData.ProjectLocationJsons[x].ProjectLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
                    bindProjectLocationSelectClickEvent(layer.feature, layer);
                    x++;
                }
            });







                //var drawOptions = getDrawOptions(projectFirmaMap.editableFeatureGroup);
                //var drawControl = new L.Control.Draw(drawOptions);
                //projectFirmaMap.map.addControl(drawControl);
            projectFirmaMap.map.addLayer(projectFirmaMap.editableFeatureGroup);
            debugger;

        };


        jQuery(document).ready(function() {
            initializeMap();
        });

    });



