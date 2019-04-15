
angular.module("ProjectFirmaApp")
    .controller("ProjectMapController",
    function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
            $scope.selectedLocationLeafletID = null;
        
            $scope.IconStyleSelected = new L.icon({
                iconUrl: 'https://api.tiles.mapbox.com/v3/marker/pin-s-marker+fff200.png'
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

        $scope.isSelectedProjectMapLocation = function (projectLocation) {
            if ($scope.selectedLocationLeafletID) {
                return $scope.selectedLocationLeafletID == projectLocation.ProjectMapLocationLeafletID;
            }
            return false;
        };

        var setLayerIconColors = function () {
            projectFirmaMap.projectLocationsLayer.eachLayer(function (layer) {
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
                        var iconColor = layer.feature.properties.ProjectStageColor.replace('#', '');
                        var iconUrlString = 'https://api.tiles.mapbox.com/v3/marker/pin-s-marker+' + iconColor + '.png';
                        var icon = new L.icon({
                            iconUrl: iconUrlString
                        });
                        layer.setIcon(icon);
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

        $scope.toggleProjectMapLocationDetails = function (locationLeafletID) {
            $scope.selectedLocationLeafletID = locationLeafletID;
            //console.log('toggleProjectLocationDetails passed in leafletID :' + locationLeafletID);
            projectFirmaMap.projectLocationsLayer.eachLayer(function (layer) {
                var currentLocationLeafletID = layer._leaflet_id;
                if ($scope.selectedLocationLeafletID == currentLocationLeafletID) {
                    if (!Sitka.Methods.isUndefinedNullOrEmpty(layer.feature.properties.PopupUrl)) {
                        jQuery.get(layer.feature.properties.PopupUrl).done(function (data) {
                            layer._map.setView(layer._latlng);
                            layer._map.openPopup(L.popup({ maxWidth: 200 }).setLatLng(layer._latlng).setContent(data).openOn(layer._map));
                        });
                    }
                }
            });
            setLayerIconColors();
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
                setLayerIconColors();
            });
        };


        var initializeMap = function () {
            //console.log('initializeMap');

            var x = 0;
            jQuery.each(projectFirmaMap.projectLocationsLayer._layers, function(index, layer) {
                if (!layer.getLayers) {
                    $scope.AngularViewData.ProjectMapLocationJsons[x].ProjectMapLocationLeafletID = layer._leaflet_id;//hacky way to get leaflet_ids tied to locations on grid
                    bindProjectLocationSelectClickEvent(layer.feature, layer);
                    x++;
                }
            });

        };


        jQuery(document).ready(function() {
            initializeMap();
        });

    });



