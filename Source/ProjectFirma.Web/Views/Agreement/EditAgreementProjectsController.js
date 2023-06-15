/*-----------------------------------------------------------------------
<copyright file="EditAgreementProjectsController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
angular.module("ProjectFirmaApp").controller("EditAgreementProjectsController", function($scope,
    angularModelAndViewData) {

    console.log('start of EditAgreementProjectsController');

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.getAvailableProjectsForAgreement = function() {
        var allPossibleProjectJsons = $scope.AngularViewData.AllPossibleProjectJsons;
        var usedProjects = $scope.AngularModel.ProjectJsons;
        var usedProjectIds = _.map(usedProjects,
            function(f) {
                return f.ProjectID;
            });

        var filteredProjectSelectList = _.filter(allPossibleProjectJsons,
            function(f) {
                return !_.includes(usedProjectIds, f.ProjectID)
            });

        return filteredProjectSelectList;
    };

    $scope.addProject = function (ProjectId) {
        var allRelevantProjectJsons = _.filter($scope.AngularViewData.AllPossibleProjectJsons,
            function(f) {
                return f.ProjectID === Number(ProjectId);
            });
        var ProjectJson = allRelevantProjectJsons[0];

        if (ProjectJson !== undefined)
        {
            $scope.AngularModel.ProjectJsons.push({
                ProjectID: Number(ProjectJson.ProjectID),
                ProjectNumber: ProjectJson.ProjectNumber,
                ProjectName: ProjectJson.ProjectName
            });
        }
        $scope.resetSelectedProjectID();
    };

    $scope.removeSelectedProject = function(ProjectID) {
        _.remove($scope.AngularModel.ProjectJsons,
            function(pos) {
                return pos.ProjectID == ProjectID;
            });
    };

    $scope.resetSelectedProjectID = function() {
        $scope.selectedProjectID = "";
    };

    $scope.isOptionSelected = function (Project)
    {
        return false;
    };

    $scope.dropdownDefaultOption = function ()
    {
        // TODO; can we use type here?
        return "Add a " + 'Project';
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    //console.log('EditAgreementProjectsController -- Angular view data:');
    //console.log($scope.AngularViewData);

    $scope.selectedProjectID = null;
});
