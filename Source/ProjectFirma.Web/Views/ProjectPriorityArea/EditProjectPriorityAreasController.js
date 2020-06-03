//#sourceUrl=/Views/ProjectPriorityArea/EditProjectPriorityAreasController.js
angular.module("ProjectFirmaApp")
    .controller("EditProjectPriorityAreasController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;

            $scope.getPriorityAreaName = function (priorityAreaId) {
                return $scope.AngularViewData.PriorityAreaNameByID[priorityAreaId];
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
                        display: "PriorityAreaName",
                        limit: Number.MAX_VALUE
                    });

                finder.bind("typeahead:select",
                    function (event, suggestion) {
                        $scope.togglePriorityArea(suggestion.PriorityAreaID, suggestion.PriorityAreaName, function() {
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
                var priorityAreaMapServiceLayerName = $scope.AngularViewData.PriorityAreaMapServiceLayerName,
                    mapServiceUrl = $scope.AngularViewData.MapServiceUrl;

                if (!priorityAreaMapServiceLayerName || !mapServiceUrl)
                    return;

                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();
                var parameters = L.Util.extend($scope.firmaMap.wfsParams,
                    {
                        typeName: priorityAreaMapServiceLayerName,
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

                        $scope.togglePriorityArea(mergedProperties.PriorityAreaID, mergedProperties.PriorityAreaName, function() {
                            $scope.$apply();
                        });

                    },
                    function() {
                        console.error("There was an error selecting the priority area from list");
                    });
            }

            $scope.togglePriorityArea = function(priorityAreaId, priorityAreaName, callback) {
                if (priorityAreaName && typeof $scope.AngularViewData.PriorityAreaNameByID[priorityAreaId] === "undefined") {
                    $scope.AngularViewData.PriorityAreaNameByID[priorityAreaId] = priorityAreaName;
                }

                if (_.includes($scope.AngularModel.PriorityAreaIDs, priorityAreaId)) {
                    _.pull($scope.AngularModel.PriorityAreaIDs, priorityAreaId);
                } else {
                    $scope.AngularModel.PriorityAreaIDs.push(priorityAreaId);
                }

                updateSelectedPriorityAreaLayer();

                if (typeof callback === "function") {
                    callback.call();
                }
            }

            function updateSelectedPriorityAreaLayer() {
                if ($scope.AngularModel.PriorityAreaIDs == null) {
                    $scope.AngularModel.PriorityAreaIDs = [];
                }

                if ($scope.firmaMap.selectedPriorityAreaLayer) {
                    $scope.firmaMap.layerControl.removeLayer($scope.firmaMap.selectedPriorityAreaLayer);
                    $scope.firmaMap.map.removeLayer($scope.firmaMap.selectedPriorityAreaLayer);
                }
                
                var wmsParameters = L.Util.extend(
                    {
                        layers: $scope.AngularViewData.PriorityAreaMapServiceLayerName,
                        cql_filter: "PriorityAreaID in (" + $scope.AngularModel.PriorityAreaIDs.join(",") + ")",
                        styles: "priorityArea_yellow"
                    },
                    $scope.firmaMap.wmsParams);

                $scope.firmaMap.selectedPriorityAreaLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, wmsParameters);
                $scope.firmaMap.layerControl.addOverlay($scope.firmaMap.selectedPriorityAreaLayer, "Selected Priority Areas");
                $scope.firmaMap.map.addLayer($scope.firmaMap.selectedPriorityAreaLayer);

                // Update map extent to selected priorityAreas
                if (_.any($scope.AngularModel.PriorityAreaIDs)) {
                    var wfsParameters = L.Util.extend($scope.firmaMap.wfsParams,
                        {
                            typeName: $scope.AngularViewData.PriorityAreaMapServiceLayerName,
                            cql_filter: "PriorityAreaID in (" + $scope.AngularModel.PriorityAreaIDs.join(",") + ")"
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
                            console.error("There was an error setting map extent to the selected Priority Areas");
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
                    $scope.AngularViewData.FindPriorityAreaByNameUrl);
                
                updateSelectedPriorityAreaLayer();
            };

            initializeMap();
        });
