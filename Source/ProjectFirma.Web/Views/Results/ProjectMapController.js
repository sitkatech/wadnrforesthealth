
angular.module("ProjectFirmaApp")
    .controller("ProjectMapController",
    function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
            $scope.selectedLocationProjectID = null;

            $scope.IconStyleSelected = new L.MakiMarkers.icon({ icon: "marker", color: "#fff200", size: "s" });


        $scope.isSelectedProjectMapLocation = function (projectLocation) {
            if ($scope.selectedLocationProjectID) {
                return $scope.selectedLocationProjectID == projectLocation.ProjectMapProjectID;
            }
            return false;
        };


        $scope.isShownLocation = function (projectLocation) {
            if (projectLocation.IsHidden == true) {
                return false;
            }
            return true;
        };

        var setLayerIconColors = function () {
            projectFirmaMap.projectLocationsLayer.eachLayer(function (layer) {
                var currentLocationProjectID = layer.feature.properties.ProjectID;
                if ($scope.selectedLocationProjectID == currentLocationProjectID) {
                    if (layer._icon) {
                        layer.setIcon($scope.IconStyleSelected);
                    } else {
                        layer.setStyle({
                            color: '#fff200',
                            fillColor: '#fff200',
                            weight: 3,
                            opacity: 0.6
                        });
                    }
                } else {
                    if (layer._icon) {
                        var iconColor = layer.feature.properties.ProjectStageColor;
                        var icon = new L.MakiMarkers.icon({ icon: "marker", color: iconColor, size: "s" });
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

        $scope.toggleProjectMapLocationDetails = function (locationProjectID) {
            $scope.selectedLocationProjectID = locationProjectID;
            //console.log('toggleProjectLocationDetails passed in leafletID :' + locationLeafletID);
            projectFirmaMap.projectLocationsLayer.eachLayer(function (layer) {
                var currentLocationProjectID = layer.feature.properties.ProjectID;
                if ($scope.selectedLocationProjectID == currentLocationProjectID) {
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


        var bindProjectLocationSelectClickEvent = function(feature, layer) {
            var projectID = layer.feature.properties.ProjectID;
            $scope.AngularViewData.ProjectMapLocationJsons.forEach(function(item) {
                if (item.ProjectMapProjectID == projectID) {
                    item.IsHidden = false;
                }
                //console.log("item(" + projectID +")" + JSON.stringify(item));
            });

            //2023-11-06 RMO: Removing this behavior for WADNR
            //layer.on('click', function (f) {
            //    if (layer.editing.enabled()) {
            //        return;
            //    }

            //    $scope.$apply(function () {
            //        $scope.selectedLocationProjectID = projectID;
            //    });
            //    setLayerIconColors();
            //});
            
        };


        var initializeMap = function () {
            //console.log('initializeMap');

            var x = 0;
            jQuery.each(projectFirmaMap.projectLocationsLayer._layers, function(index, layer) {
                if (!layer.getLayers) {
                    bindProjectLocationSelectClickEvent(layer.feature, layer);
                    x++;
                }
            });

            projectFirmaMap.map.on('overlayremove', function (event) {
                //console.log("made it into overlayremove");

                if ( event.name != null && event.name.includes("Mapped Projects")) {
                    $scope.selectedLocationProjectID = null;
                    jQuery(".mapGridContainer").find("tr.selectedRow").removeClass("selectedRow");
                    projectFirmaMap.map.closePopup();
                }
                
            });


            projectFirmaMap.map.on('layeradd', function (event) {
               //console.log("made it into layeradd event");

                if (event.layer.feature != null && event.layer.feature.properties.ProjectID != null) {
                    bindProjectLocationSelectClickEvent(event.layer.feature, event.layer);
                }
                $scope.$apply();
            });


            projectFirmaMap.map.on('layerremove', function (event) {
                //console.log("made it into layerremove event");

                if (event.layer.feature != null && event.layer.feature.properties.ProjectID != null) {
                    var projectID = event.layer.feature.properties.ProjectID;
                    $scope.AngularViewData.ProjectMapLocationJsons.forEach(function (item) {
                        if (item.ProjectMapProjectID == projectID) {
                            item.IsHidden = true;
                        }
                        //console.log("item in layerremove(" + projectID + ")" + JSON.stringify(item));
                    });
                    projectFirmaMap.map.closePopup();
                }
                $scope.$apply();
            });


        };


        jQuery(function() {
            initializeMap();
        });

        jQuery(document).on('change', ':checkbox.leaflet-control-layers-selector',
            function () {
                // This hardcoding is not great, but TK and SLG both can't see a better way.
                // This check at least will ensure that the control we looking for actually exists.
                if (jQuery(this).parents("div.leaflet-control-layers-overlays").find("span:contains('Mapped Projects')").length > 0) {
                    if (jQuery(this).siblings("span:contains(' Mapped Projects')").length > 0) {
                        var checkbox = jQuery(this);
                        if (checkbox.is(':checked')) {
                            jQuery('.mapGridContainer').show();
                            jQuery('.mapCustomizations').show();
                            jQuery('.clusterLocationsContainer').show();
                            jQuery('.mapLegendContainer').show();
                        } else {
                            jQuery('.mapGridContainer').hide();
                            jQuery('.mapCustomizations').hide();
                            jQuery('.clusterLocationsContainer').hide();
                            jQuery('.mapLegendContainer').hide();
                        }
                    }
                } else {
                    alert("No 'Mapped Projects' layer found. Please contact an admin.");
                }
      
            });

    });



