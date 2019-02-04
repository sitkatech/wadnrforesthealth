
angular.module("ProjectFirmaApp")
    .controller("ProjectLocationDetailController",
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


            var initializeMap = function () {
                var mapInitJson = $scope.AngularViewData.MapInitJson;
                var editableFeatureJsonObject = $scope.AngularViewData.EditableLayerGeoJson;
                projectFirmaMap = new ProjectFirmaMaps.Map(mapInitJson);

                projectFirmaMap.editableFeatureGroup = new L.FeatureGroup();

                var bindAnnotationPopup = function (layer, feature) {
                    var leafletId = layer._leaflet_id;
                    var popupOptions = {
                        minWidth: 200
                    };
                    layer.bindPopup("", popupOptions);
                    layer.on('click', function (f) {
                        if (layer.editing.enabled()) {
                            f.target.closePopup();
                            return;
                        }
                        var popup = f.target._popup;
                        var annotation = Sitka.Methods.isUndefinedNullOrEmpty(feature) ? "" : Sitka.Methods.isUndefinedNullOrEmpty(feature.properties.Info) ? "" : feature.properties.Info;
                        var annotationMaxLength = $scope.AngularViewData.AnnotationMaxLength;
                        var charsRemaining = annotationMaxLength - annotation.length;
                        var textareaId = projectFirmaMap.getTextAreaId(leafletId);
                        var charRemainingForTextareaId = "CharactersRemaining_" + textareaId;
                        var popupContent = "<b>Notes:</b> <br />" +
                            "<textarea id=\"" + textareaId + "\" " +
                            "onkeyup=\"Sitka.Methods.keepTextAreaWithinMaxLength(this, " + annotationMaxLength + ", 20, '" + charRemainingForTextareaId + "', 'Characters Remaining: ');\" " +
                            "onkeydown=\"Sitka.Methods.keepTextAreaWithinMaxLength(this, " + annotationMaxLength + ", 20, '" + charRemainingForTextareaId + "', 'Characters Remaining: ');\">" +
                            annotation + "</textarea>" +
                            "<div id=\"" + charRemainingForTextareaId + "\" style=\"text-align:right;color:#666666;\">Characters Remaining: " + charsRemaining + "</div><br />" +
                            "<button id=\"buttonFor" + leafletId + "\" class=\"button btn-xs btn-info\" onclick=\"savePopupAnnotationEditor(" + leafletId + ")\">Save</button>";
                        popup.setContent(popupContent);
                        popup.update();

                        jQuery("#textboxFor" + leafletId).focus();
                    });
                };

                var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection, {
                    onEachFeature: function (feature, layer) {
                        if (layer.getLayers) {
                            layer.getLayers().forEach(function (l) { projectFirmaMap.editableFeatureGroup.addLayer(l); });
                        }
                        else {
                            projectFirmaMap.editableFeatureGroup.addLayer(layer);
                        }
                        bindAnnotationPopup(layer, feature);
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
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";
                    var feature = projectFirmaMap.editableFeatureGroup._layers[leafletId].feature;
                    bindAnnotationPopup(layer, feature);
                    updateFeatureCollectionJson();
                });
                projectFirmaMap.map.on('draw:edited', function (e) {
                    updateFeatureCollectionJson();
                });

                projectFirmaMap.map.on('draw:deleted', function (e) {
                    updateFeatureCollectionJson();
                });

                updateFeatureCollectionJson();

                var saveButton = jQuery("#" + $scope.AngularViewData.SaveButtonID);
                if (!Sitka.Methods.isUndefinedNullOrEmpty(saveButton)) {
                    saveButton.text("Save");
                }

                var modalTitle = jQuery(".ui-dialog-title");
                if (!Sitka.Methods.isUndefinedNullOrEmpty(modalTitle)) {
                    modalTitle.html($scope.AngularViewData.ModalTitle);
                }



            };
            alert("hello world!");
            initializeMap();

    });



