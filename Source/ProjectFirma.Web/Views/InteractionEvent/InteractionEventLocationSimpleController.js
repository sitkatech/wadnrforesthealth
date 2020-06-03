angular.module("ProjectFirmaApp")
    .controller("InteractionEventLocationSimpleController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;
            $scope.hasGeospatialAreaTypeLayers = $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.length > 0;
            $scope.GeospatialAreaMapServiceLayerNamesCommaSeparated =
                $scope.AngularViewData.GeospatialAreaMapServiceLayerNames.join(",");


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

            $scope.toggleMap = function() {
                document.getElementById($scope.AngularViewData.MapInitJson.MapDivID).style.cursor = "crosshair";
                $scope.interactionEventLocationMap.unblockMap();
                $scope.interactionEventLocationMap.map.scrollWheelZoom.enable();

                if ($scope.interactionEventLocationMap.currentSelectedPoint) {
                    $scope.interactionEventLocationMap.map.addLayer($scope.interactionEventLocationMap.currentSelectedPoint);
                }

                if ($scope.interactionEventLocationMap.currentSelectedGeometry) {
                    $scope.interactionEventLocationMap.map.removeLayer($scope.interactionEventLocationMap.currentSelectedGeometry);
                }
            };

            function onMapClick(event) {
                onMapClickSetPointOnMap(event,
                    function () {
                        $scope.$apply();
                    });
            }

            function onMapClickSetPointOnMap(event, callback) {
                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();

                var propertiesForDisplay = {
                    Latitude: L.Util.formatNum(latlngWrapped.lat, 4),
                    Longitude: L.Util.formatNum(latlngWrapped.lng, 4)
                };

                if (!$scope.hasGeospatialAreaTypeLayers) {
                    setPointOnMap(latlng);
                    $scope.propertiesForPointOnMap = propertiesForDisplay;
                    if (callback) {
                        callback.call();
                    }
                }
                else {
                    var parameters = L.Util.extend($scope.interactionEventLocationMap.wfsParams,
                        {
                            typeName: $scope.GeospatialAreaMapServiceLayerNamesCommaSeparated,
                            cql_filter: "intersects(Ogr_Geometry, POINT(" +
                                latlngWrapped.lat +
                                " " +
                                latlngWrapped.lng +
                                "))"
                        });
                    SitkaAjax.ajax({
                        url: $scope.AngularViewData.MapServiceUrl + L.Util.getParamString(parameters),
                            dataType: "json",
                            jsonpCallback: "getJson"
                        },
                        function(response) {
                            setPointOnMap(latlng);
                            if (response.features.length > 0) {
                                var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));
                                propertiesForDisplay[$scope.AngularViewData.GeospatialAreaFieldDefinitionLabel] =
                                    mergedProperties.GeospatialAreaName;
                            }

                            $scope.propertiesForPointOnMap = propertiesForDisplay;

                            if (callback) {
                                callback.call();
                            }
                        },
                        function() {
                            console.error(
                                "There was an error selecting the " +
                                $scope.AngularViewData.InteractionEventLocationFieldDefinitionLabel +
                                " area from list.");
                        });
                }
            }

            function setPointOnMap(latlng) {
                $scope.AngularModel.InteractionEventLocationPointX = L.Util.formatNum(latlng.lng);
                $scope.AngularModel.InteractionEventLocationPointY = L.Util.formatNum(latlng.lat);

                if ($scope.interactionEventLocationMap.currentSelectedPoint) {
                    $scope.interactionEventLocationMap.map.removeLayer(
                        $scope.interactionEventLocationMap.currentSelectedPoint);
                }

                $scope.interactionEventLocationMap.currentSelectedPoint = L.marker(latlng,
                    {
                        icon: L.MakiMarkers.icon({
                            icon: "marker",
                            color: $scope.selectedStyle.color,
                            size: "m"
                        })
                    });

                $scope.interactionEventLocationMap.map.addLayer($scope.interactionEventLocationMap.currentSelectedPoint);
                $scope.interactionEventLocationMap.map.panTo(latlng);
            }

            $scope.getInteractionEventLocationProperties = function() {
                if ($scope.propertiesForPointOnMap === undefined) {
                    return $scope.propertiesForPointOnMap;
                }
                Object.keys($scope.propertiesForPointOnMap).forEach(key => {
                    if ($scope.propertiesForPointOnMap[key] === undefined) {
                        delete $scope.propertiesForPointOnMap[key];
                    }
                });
                return $scope.propertiesForPointOnMap;
            };

            ProjectFirmaMaps.Map.prototype.handleWmsPopupClickEventWithCurrentLayer  = function() {
                // Override parent to do nothing
                return function () { };
            };

            var initializeMap = function () {
                $scope.interactionEventLocationMap = new ProjectFirmaMaps.Map($scope.AngularViewData.MapInitJson);
                $scope.interactionEventLocationMap.map.on("click", onMapClick);

                if ($scope.AngularViewData.CurrentFeature) {
                    // ReSharper disable once InconsistentNaming
                    $scope.interactionEventLocationMap.currentSelectedGeometry = new L.geoJSON(
                        $scope.AngularViewData.CurrentFeature,
                        {
                            style: function() {
                                return $scope.selectedStyle;
                            }
                        });

                    $scope.propertiesForNamedArea = {};
                    $scope.propertiesForNamedArea[$scope.AngularViewData.GeospatialAreaFieldDefinitionLabel] = $scope.AngularViewData.InitialGeospatialAreaName;
                }

                if ($scope.AngularModel.InteractionEventLocationPointX && $scope.AngularModel.InteractionEventLocationPointY) {
                    var latlng = new L.LatLng($scope.AngularModel.InteractionEventLocationPointY, $scope.AngularModel.InteractionEventLocationPointX);
                    var latlngWrapped = latlng.wrap();
                    $scope.interactionEventLocationMap.currentSelectedPoint = L.marker(latlng, { icon: L.MakiMarkers.icon({ icon: "marker", color: $scope.selectedStyle.color, size: "m" }) });

                    $scope.propertiesForPointOnMap = {
                        Latitude: L.Util.formatNum(latlngWrapped.lat, 4),
                        Longitude: L.Util.formatNum(latlngWrapped.lng, 4)
                    };

                    // Get the initial Location Information from the WMS service
                    if ($scope.hasGeospatialAreaTypeLayers) {
                        SitkaAjax.ajax({
                                url: $scope.AngularViewData.MapServiceUrl +
                                    L.Util.getParamString(L.Util.extend($scope.interactionEventLocationMap.wfsParams,
                                        {
                                            typeName: $scope.GeospatialAreaMapServiceLayerNamesCommaSeparated,
                                            cql_filter: "intersects(Ogr_Geometry, POINT(" + latlngWrapped.lat + " " + latlngWrapped.lng + "))"
                                        })),
                                dataType: "json",
                                jsonpCallback: "getJson"
                            },
                            function(response) {
                                if (response.features.length === 0)
                                    return;

                                var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));
                                $scope.propertiesForPointOnMap[$scope.AngularViewData.GeospatialAreaFieldDefinitionLabel] = mergedProperties.GeospatialAreaName;
                                $scope.$apply();
                            },
                            function() {
                                console.error("There was an error getting the initial " + $scope.AngularViewData.GeospatialAreaFieldDefinitionLabel + " name to display.");
                            });
                    }
                }
            };

            initializeMap();
            $scope.toggleMap();
        });
