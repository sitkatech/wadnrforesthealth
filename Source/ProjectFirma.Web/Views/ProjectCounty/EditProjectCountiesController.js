//#sourceUrl=/Views/ProjectCounty/EditProjectCountiesController.js
angular.module("ProjectFirmaApp")
    .controller("EditProjectCountiesController",
        function ($scope, angularModelAndViewData) {
            $scope.AngularModel = angularModelAndViewData.AngularModel;
            $scope.AngularViewData = angularModelAndViewData.AngularViewData;

            $scope.getCountyName = function (countyId) {
                return $scope.AngularViewData.CountyNameByID[countyId];
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
                        display: "CountyName",
                        limit: Number.MAX_VALUE
                    });

                finder.bind("typeahead:select",
                    function (event, suggestion) {
                        $scope.toggleCounty(suggestion.CountyID, suggestion.CountyName, function() {
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
                debugger;

                var countyMapServiceLayerName = $scope.AngularViewData.CountyMapServiceLayerName,
                    mapServiceUrl = $scope.AngularViewData.MapServiceUrl;
                

                if (!countyMapServiceLayerName || !mapServiceUrl)
                    return;

                var latlng = event.latlng;
                var latlngWrapped = latlng.wrap();
                var parameters = L.Util.extend($scope.firmaMap.wfsParams,
                    {
                        typeName: countyMapServiceLayerName,
                        cql_filter: "intersects(Ogr_Geometry, POINT(" + latlngWrapped.lat + " " + latlngWrapped.lng + "))"
                    });
                SitkaAjax.ajax({
                        url: mapServiceUrl + L.Util.getParamString(parameters),
                        dataType: "json",
                        jsonpCallback: "getJson"
                    },
                    function (response) {

                        debugger;

                        if (response.features.length === 0)
                            return;

                        var mergedProperties = _.merge.apply(_, _.map(response.features, "properties"));

                        $scope.toggleCounty(mergedProperties.CountyID, mergedProperties.CountyName, function() {
                            $scope.$apply();
                        });

                    },
                    function() {
                        console.error("There was an error selecting the county from list");
                    });
            }

            $scope.toggleCounty = function(countyId, countyName, callback) {
                if (countyName && typeof $scope.AngularViewData.CountyNameByID[countyId] === "undefined") {
                    $scope.AngularViewData.CountyNameByID[countyId] = countyName;
                }

                if (_.includes($scope.AngularModel.CountyIDs, countyId)) {
                    _.pull($scope.AngularModel.CountyIDs, countyId);
                } else {
                    $scope.AngularModel.CountyIDs.push(countyId);
                }

                updateSelectedCountyLayer();

                if (typeof callback === "function") {
                    callback.call();
                }
            }

            function updateSelectedCountyLayer() {
                if ($scope.AngularModel.CountyIDs == null) {
                    $scope.AngularModel.CountyIDs = [];
                }

                if ($scope.firmaMap.selectedCountyLayer) {
                    $scope.firmaMap.layerControl.removeLayer($scope.firmaMap.selectedCountyLayer);
                    $scope.firmaMap.map.removeLayer($scope.firmaMap.selectedCountyLayer);
                }

                var selectedMapParameters = null;
                if ($scope.AngularModel.CountyIDs.length > 0) {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.CountyMapServiceLayerName,
                            cql_filter: "CountyID in (" + $scope.AngularModel.CountyIDs.join(",") + ")",
                            styles: "county_yellow"
                        }
                    );
                }
                else {
                    selectedMapParameters = L.Util.extend($scope.firmaMap.wmsParams,
                        {
                            layers: $scope.AngularViewData.CountyMapServiceLayerName,
                            cql_filter: ""
                        });
                }

                $scope.firmaMap.selectedCountyLayer = L.tileLayer.wms($scope.AngularViewData.MapServiceUrl, selectedMapParameters);
                $scope.firmaMap.layerControl.addOverlay($scope.firmaMap.selectedCountyLayer, "Selected Counties");
                $scope.firmaMap.map.addLayer($scope.firmaMap.selectedCountyLayer);

                // Update map extent to selected countys
                if (_.any($scope.AngularModel.CountyIDs)) {
                    var wfsParameters = L.Util.extend($scope.firmaMap.wfsParams,
                        {
                            typeName: $scope.AngularViewData.CountyMapServiceLayerName,
                            cql_filter: "CountyID in (" + $scope.AngularModel.CountyIDs.join(",") + ")"
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
                            console.error("There was an error setting map extent to the selected Counties");
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
                    $scope.AngularViewData.FindCountyByNameUrl);
                
                updateSelectedCountyLayer();
            };

            initializeMap();
        });
