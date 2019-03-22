/*-----------------------------------------------------------------------
<copyright file="InteractionEventProjectController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
angular.module("ProjectFirmaApp").controller("InteractionEventProjectController", function ($scope, angularModelAndViewData) {
    $scope.ProjectToAdd = null;

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });


    $scope.getAvailableProjectsForInteractionEvents = function () {
        var projectsForInteractionEvents = $scope.AngularViewData.AllProjects;

        var usedProjects = $scope.AngularModel.InteractionEventProjects;
        var usedProjectIDs = _.map(usedProjects,
            function (f) {
                return f.ProjectID;
            });

        var filteredList = _.filter(projectsForInteractionEvents,
            function (f) {
                return !_.includes(usedProjectIDs, f.ProjectID);
            });

        return filteredList;
    };

    $scope.chosenProjectsForInteractionEvent = function() {
        var chosenProjects = $scope.AngularModel.InteractionEventProjects;

        var projects = _.map(chosenProjects,
            function(s) {
                var project =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllProjects,
                        "ProjectID",
                        s.ProjectID);
                return project;
            });
        return projects;
    };

    $scope.addInteractionEventProject = function(projectID) {
        $scope.AngularModel.InteractionEventProjects.push({
            ProjectID: Number(projectID)
        });
        $scope.resetSelectedProjectID();
    };

    $scope.removeInteractionEventProject = function(projectID) {
        _.remove($scope.AngularModel.InteractionEventProjects,
            function(pos) {
                return pos.ProjectID == projectID;
            });
    };

    $scope.resetSelectedProjectID = function () {
        $scope.selectedProjectID = "";
    };


    $scope.isOptionSelected = function(project) {

        return _.any($scope.AngularModel.InteractionEventProjects,
            function(pos) {
                return pos.ProjectID == project.ProjectID;
            });
    };


    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    console.log($scope.AngularViewData);
    $scope.selectedProjectID = "";
});
