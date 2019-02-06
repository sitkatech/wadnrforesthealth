
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

        var createProjectLocationJson = function (wellKnownText, projectLocationId, featureType, locationTypeId, locationName, locationTypeName, locationNotes, leafletID) {
            return {
                ProjectLocationGeometryWellKnownText: wellKnownText,
                ProjectLocationID: projectLocationId,
                ProjectLocationFeatureType: featureType,
                ProjectLocationTypeID: locationTypeId,
                ProjectLocationName: locationName,
                ProjectLocationTypeName: locationTypeName,
                ProjectLocationNotes: locationNotes,
                ProjectLocationLeafletID: leafletID
            };
        };

        var addFeatureToAngularModel = function (newestGeoJson, newestLeafletID) {
            console.log('addFeatureToAngularModel');
            //debugger;
            //Terraformer.WKT.convert(geoJson.features[i].geometry)
            //var wkt = Terraformer.WKT.convert(feature.geometry);
            var locationJson = createProjectLocationJson(newestGeoJson, -1, 'Polygon', -1, 'locationnamehere', 'locationTypeHere', 'NotesShouldBeBlankUltimately', newestLeafletID);
            $scope.AngularModel.ProjectLocationJsons.push(locationJson);
            $scope.$apply();
        };

        $scope.deleteProjectLocationRowAndRefreshMap = function (projectLocation) {
            //debugger;
            var layer = projectFirmaMap.editableFeatureGroup.getLayer(projectLocation.ProjectLocationLeafletID);
            projectFirmaMap.editableFeatureGroup.removeLayer(layer);//remove from map

            _.remove($scope.AngularModel.ProjectLocationJsons, function(n) {
                return n.ProjectLocationLeafletID === projectLocation.ProjectLocationLeafletID;
            }); 
        };


        /*
         * ToDo for JJV / whomever
         *
         * - Delete is only partly working. It works in the all-new case we think, but not the existing case. We have yet to figure out how to get
         * the LeafletID associcated with the ProjectLocationJson for existing features. See below in the initalizeMap function.
         *
         * - addFeatureToAngularModel does not need dummy values like "locationtypehere" long term, but they have been very helpful in the short for testing. Removed
         * when they get in your way.
         *
         * - Have yet to touch upload GDB yet at all. It might be working already, we just haven't tested at all. From the card "Uploading a GDB
         * should append features, not delete and replace the features"
         *
         * - Highlighting selected grid row and associated feature with same color needs to happen
         *
         * - Need to load the feature name ("line", "point", "poly") for existing features. Should work properly for new.
         *
         * -- TK & SLG -- 2/5/2019 - 5:20 PM
         * 
         *
         */



        var initializeMap = function () {
                console.log('initializeMap');
                var mapInitJson = $scope.AngularViewData.MapInitJson;
                var editableFeatureJsonObject = $scope.AngularViewData.EditableLayerGeoJson;
                projectFirmaMap = new ProjectFirmaMaps.Map(mapInitJson);

                projectFirmaMap.editableFeatureGroup = new L.FeatureGroup();

                var layerGroup = L.geoJson(editableFeatureJsonObject.GeoJsonFeatureCollection, {
                    onEachFeature: function (feature, layer) {
                        if (layer.getLayers) {
                            layer.getLayers().forEach(function (l) { projectFirmaMap.editableFeatureGroup.addLayer(l); });
                        }
                        else {
                            projectFirmaMap.editableFeatureGroup.addLayer(layer);
                        }
                       
                    }
                });

                var drawOptions = getDrawOptions(projectFirmaMap.editableFeatureGroup);
                var drawControl = new L.Control.Draw(drawOptions);
                projectFirmaMap.map.addControl(drawControl);
                projectFirmaMap.map.addLayer(projectFirmaMap.editableFeatureGroup);

                projectFirmaMap.map.on('draw:created', function (e) {
                    console.log('draw:created called');
                    var layer = e.layer;
                    projectFirmaMap.editableFeatureGroup.addLayer(layer);
                    var leafletId = layer._leaflet_id;
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.properties = new Object();
                    projectFirmaMap.editableFeatureGroup._layers[leafletId].feature.type = "Feature";

                    var allGeoJson = projectFirmaMap.editableFeatureGroup.toGeoJSON();
                    var newestGeoJson = Terraformer.WKT.convert(allGeoJson.features[allGeoJson.features.length - 1].geometry);

                    //var feature = projectFirmaMap.editableFeatureGroup._layers[leafletId].feature;
                    //update grid with new drawing
                    addFeatureToAngularModel(newestGeoJson, leafletId);
                    
                    
                });

                projectFirmaMap.map.on('draw:edited', function (e) {
                    
                });

                projectFirmaMap.map.on('draw:deleted', function (e) {
                    
                });

                



            };

            initializeMap();

    });



