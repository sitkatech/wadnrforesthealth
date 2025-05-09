﻿//#sourceUrl=/Views/ProjectPriorityLandscape/EditProjectPriorityLandscapesController.js
angular.module("ProjectFirmaApp")
    .controller("EditProjectPriorityLandscapesController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;

            $scope.getPriorityLandscapeName = function (priorityLandscapeId) {
                return $scope.AngularViewData.PriorityLandscapeNameByID[priorityLandscapeId];
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
                        display: "PriorityLandscapeName",
                        limit: Number.MAX_VALUE
                    });

                finder.on("typeahead:select",
                    function (event, suggestion) {
                        $scope.togglePriorityLandscape(suggestion.PriorityLandscapeID, suggestion.PriorityLandscapeName, function() {
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
                var priorityLandscapeMapServiceLayerName = $scope.AngularViewData.PriorityLandscapeMapServiceLayerName,
                    mapServiceUrl = $scope.AngularViewData.MapServiceUrl;

                if (!priorityLandscapeMapServiceLayerName || !mapServiceUrl)
                    return;

                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();
                var parameters = L.Util.extend($scope.firmaMap.wfsParams,
                    {
                        typeName: priorityLandscapeMapServiceLayerName,
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

                        $scope.togglePriorityLandscape(mergedProperties.PriorityLandscapeID, mergedProperties.PriorityLandscapeName, function() {
                            $scope.$apply();
                        });

                    },
                    function() {
                        console.error("There was an error selecting the priority landscape from list");
                    });
            }

            $scope.togglePriorityLandscape = function(priorityLandscapeId, priorityLandscapeName, callback) {
                if (priorityLandscapeName && typeof $scope.AngularViewData.PriorityLandscapeNameByID[priorityLandscapeId] === "undefined") {
                    $scope.AngularViewData.PriorityLandscapeNameByID[priorityLandscapeId] = priorityLandscapeName;
                }

                if (_.includes($scope.AngularModel.PriorityLandscapeIDs, priorityLandscapeId)) {
                    _.pull($scope.AngularModel.PriorityLandscapeIDs, priorityLandscapeId);
                } else {
                    $scope.AngularModel.PriorityLandscapeIDs.push(priorityLandscapeId);
                }

                updateSelectedPriorityLandscapeLayer();

                if (typeof callback === "function") {
                    callback.call();
                }
            }

            function updateSelectedPriorityLandscapeLayer() {
                if ($scope.AngularModel.PriorityLandscapeIDs == null) {
                    $scope.AngularModel.PriorityLandscapeIDs = [];
                }

                if ($scope.firmaMap.selectedPriorityLandscapeLayer) {
                    $scope.firmaMap.layerControl.removeLayer($scope.firmaMap.selectedPriorityLandscapeLayer);
                    $scope.firmaMap.map.removeLayer($scope.firmaMap.selectedPriorityLandscapeLayer);
                }

                var selectedMapParameters = null;
                if ($scope.AngularModel.PriorityLandscapeIDs.length > 0) {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.PriorityLandscapeMapServiceLayerName,
                            cql_filter: "PriorityLandscapeID in (" + $scope.AngularModel.PriorityLandscapeIDs.join(",") + ")",
                            styles: "priorityLandscape_yellow"
                        });
                } else {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.PriorityLandscapeMapServiceLayerName,
                            cql_filter: ""
                        });
                }

                
                $scope.firmaMap.selectedPriorityLandscapeLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, selectedMapParameters);
                $scope.firmaMap.layerControl.addOverlay($scope.firmaMap.selectedPriorityLandscapeLayer, "Selected Priority Landscapes");
                $scope.firmaMap.map.addLayer($scope.firmaMap.selectedPriorityLandscapeLayer);

                // Update map extent to selected priorityLandscapes
                if (_.any($scope.AngularModel.PriorityLandscapeIDs)) {
                    var wfsParameters = L.Util.extend($scope.firmaMap.wfsParams,
                        {
                            typeName: $scope.AngularViewData.PriorityLandscapeMapServiceLayerName,
                            cql_filter: "PriorityLandscapeID in (" + $scope.AngularModel.PriorityLandscapeIDs.join(",") + ")"
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
                            console.error("There was an error setting map extent to the selected Priority Landscapes");
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
                    $scope.AngularViewData.FindPriorityLandscapeByNameUrl);
                
                updateSelectedPriorityLandscapeLayer();
            };

            initializeMap();
        });
