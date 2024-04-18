//#sourceUrl=/Views/ProjectRegion/EditProjectRegionsController.js
angular.module("ProjectFirmaApp")
    .controller("EditProjectRegionsController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;

            $scope.getDNRUplandRegionName = function (regionId) {
                return $scope.AngularViewData.RegionNameByID[regionId];
            };

            var typeaheadSearch = function (typeaheadSelector, typeaheadSelectorButton, summaryUrl) {
                var finder = jQuery(typeaheadSelector);
                finder.typeahead({
                        highlight: true,
                        minLength: 1
                    },
                    {
                        source: new Bloodhound({
                            datumTokenizer: Bloodhound.tokenizers.whitespace,
                            queryTokenizer: Bloodhound.tokenizers.whitespace,
                            remote: {
                                url: summaryUrl +
                                    "?term=%QUERY",
                                wildcard: "%QUERY"
                            }
                        }),
                        display: "RegionName",
                        limit: Number.MAX_VALUE
                    });

                finder.on("typeahead:select",
                    function (event, suggestion) {
                        $scope.toggleRegion(suggestion.RegionID, suggestion.RegionName, function() {
                            $scope.$apply();
                        });
                    });

                jQuery(typeaheadSelectorButton).on("click", function () { $scope.selectFirstSuggestionFunction(finder); });

                finder.keypress(function (e) {
                    if (e.which === 13) {
                        e.preventDefault();
                        $scope.selectFirstSuggestionFunction(this);
                        $scope.$apply();
                    }
                });
            };

            $scope.selectFirstSuggestionFunction = function () {
                var selectables = jQuery($scope.typeaheadSelectorButton).siblings(".tt-menu").find(".tt-selectable");
                if (selectables.length > 0) {
                    jQuery(selectables[0]).trigger("click");
                }
            };

            function onMapClick(event) {
                var regionMapServiceLayerName = $scope.AngularViewData.RegionMapServiceLayerName,
                    mapServiceUrl = $scope.AngularViewData.MapServiceUrl;

                if (!regionMapServiceLayerName || !mapServiceUrl)
                    return;

                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();
                var parameters = L.Util.extend($scope.firmaMap.wfsParams,
                    {
                        typeName: regionMapServiceLayerName,
                        cql_filter: "intersects(Ogr_Geometry, POINT(" + latlngWrapped.lat + " " + latlngWrapped.lng + "))"
                    });
                SitkaAjax.ajax({
                        url: mapServiceUrl + L.Util.getParamString(parameters),
                        dataType: "json",
                        jsonpCallback: "getJson"
                    },
                    function(response) {
                        if (response.features.length === 0)
                            return;

                        var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

                        $scope.toggleRegion(mergedProperties.DNRUplandRegionID, mergedProperties.DNRUplandRegionName, function() {
                            $scope.$apply();
                        });

                    },
                    function() {
                        console.error("There was an error selecting the region from list");
                    });
            }

            $scope.toggleRegion = function(regionId, regionName, callback) {
                if (regionName && typeof $scope.AngularViewData.RegionNameByID[regionId] === "undefined") {
                    $scope.AngularViewData.RegionNameByID[regionId] = regionName;
                }

                if (_.includes($scope.AngularModel.DNRUplandRegionIDs, regionId)) {
                    _.pull($scope.AngularModel.DNRUplandRegionIDs, regionId);
                } else {
                    $scope.AngularModel.DNRUplandRegionIDs.push(regionId);
                }

                updateSelectedRegionLayer();

                if (typeof callback === "function") {
                    callback.call();
                }
            }

            function updateSelectedRegionLayer() {
                if ($scope.AngularModel.DNRUplandRegionIDs == null) {
                    $scope.AngularModel.DNRUplandRegionIDs = [];
                }

                if ($scope.firmaMap.selectedRegionLayer) {
                    $scope.firmaMap.layerControl.removeLayer($scope.firmaMap.selectedRegionLayer);
                    $scope.firmaMap.map.removeLayer($scope.firmaMap.selectedRegionLayer);
                }
                


                var selectedMapParameters = null;
                if ($scope.AngularModel.DNRUplandRegionIDs.length > 0) {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.RegionMapServiceLayerName,
                            cql_filter: "DNRUplandRegionID in (" + $scope.AngularModel.DNRUplandRegionIDs.join(",") + ")",
                            styles: "region_yellow"
                        });
                } else {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.RegionMapServiceLayerName,
                            cql_filter: ""
                        });
                }
                
                $scope.firmaMap.selectedRegionLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, selectedMapParameters);
                $scope.firmaMap.layerControl.addOverlay($scope.firmaMap.selectedRegionLayer, "Selected DNR Upland Regions");
                $scope.firmaMap.map.addLayer($scope.firmaMap.selectedRegionLayer);

                // Update map extent to selected regions
                if (_.any($scope.AngularModel.DNRUplandRegionIDs)) {
                    var wfsParameters = L.Util.extend($scope.firmaMap.wfsParams,
                        {
                            typeName: $scope.AngularViewData.RegionMapServiceLayerName,
                            cql_filter: "DNRUplandRegionID in (" + $scope.AngularModel.DNRUplandRegionIDs.join(",") + ")"
                        });
                    SitkaAjax.ajax({
                            url: $scope.AngularViewData.MapServiceUrl + L.Util.getParamString(wfsParameters),
                            dataType: "json",
                            jsonpCallback: "getJson"
                        },
                        function (response) {
                            if (response.features.length === 0)
                                return;

                            $scope.firmaMap.map.fitBounds(new L.geoJSON(response).getBounds());
                        },
                        function () {
                            console.error("There was an error setting map extent to the selected DNR Upland Regions");
                        });
                }
            };

            ProjectFirmaMaps.Map.prototype.handleWmsPopupClickEventWithCurrentLayer = function() {
                // Override parent to do nothing
                return function() {};
            };

            function initializeMap() {
                $scope.firmaMap = new ProjectFirmaMaps.Map($scope.AngularViewData.MapInitJson);
                $scope.firmaMap.map.on("click", onMapClick);
                $scope.firmaMap.map.scrollWheelZoom.enable();

                typeaheadSearch("#" + $scope.AngularViewData.TypeAheadInputId,
                    "#" + $scope.AngularViewData.TypeAheadInputId + "Button",
                    $scope.AngularViewData.FindRegionByNameUrl);
                
                updateSelectedRegionLayer();
            };

            initializeMap();
        });
