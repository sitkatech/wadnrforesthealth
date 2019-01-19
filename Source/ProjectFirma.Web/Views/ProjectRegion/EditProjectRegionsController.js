//#sourceUrl=/Views/ProjectRegion/EditProjectRegionsController.js
angular.module("ProjectFirmaApp")
    .controller("EditProjectRegionsController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;

            $scope.getRegionName = function (regionId) {
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

                finder.bind("typeahead:select",
                    function (event, suggestion) {
                        $scope.toggleRegion(suggestion.RegionID, suggestion.RegionName, function() {
                            $scope.$apply();
                        });
                    });

                jQuery(typeaheadSelectorButton).click(function () { $scope.selectFirstSuggestionFunction(finder); });

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
                var regionMapSericeLayerName = $scope.AngularViewData.RegionMapServiceLayerName,
                    mapServiceUrl = $scope.AngularViewData.MapServiceUrl;

                if (!regionMapSericeLayerName || !mapServiceUrl)
                    return;

                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();
                var parameters = L.Util.extend($scope.firmaMap.wfsParams,
                    {
                        typeName: regionMapSericeLayerName,
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

                        $scope.toggleRegion(mergedProperties.RegionID, mergedProperties.RegionName, function() {
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

                if (_.includes($scope.AngularModel.RegionIDs, regionId)) {
                    _.pull($scope.AngularModel.RegionIDs, regionId);
                } else {
                    $scope.AngularModel.RegionIDs.push(regionId);
                }

                updateSelectedRegionLayer();

                if (typeof callback === "function") {
                    callback.call();
                }
            }

            function updateSelectedRegionLayer() {
                if ($scope.AngularModel.RegionIDs == null) {
                    $scope.AngularModel.RegionIDs = [];
                }

                if ($scope.firmaMap.selectedRegionLayer) {
                    $scope.firmaMap.layerControl.removeLayer($scope.firmaMap.selectedRegionLayer);
                    $scope.firmaMap.map.removeLayer($scope.firmaMap.selectedRegionLayer);
                }
                
                var wmsParameters = L.Util.extend(
                    {
                        layers: $scope.AngularViewData.RegionMapSericeLayerName,
                        cql_filter: "RegionID in (" + $scope.AngularModel.RegionIDs.join(",") + ")",
                        styles: "region_yellow"
                    },
                    $scope.firmaMap.wmsParams);

                $scope.firmaMap.selectedRegionLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, wmsParameters);
                $scope.firmaMap.layerControl.addOverlay($scope.firmaMap.selectedRegionLayer, "Selected Regions");
                $scope.firmaMap.map.addLayer($scope.firmaMap.selectedRegionLayer);

                // Update map extent to selected regions
                if (_.any($scope.AngularModel.RegionIDs)) {
                    var wfsParameters = L.Util.extend($scope.firmaMap.wfsParams,
                        {
                            typeName: $scope.AngularViewData.RegionMapServiceLayerName,
                            cql_filter: "RegionID in (" + $scope.AngularModel.RegionIDs.join(",") + ")"
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
                            console.error("There was an error setting map extent to the selected Regions");
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
