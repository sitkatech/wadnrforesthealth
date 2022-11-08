/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationRequestController.js" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
angular.module("ProjectFirmaApp").controller("ProjectGrantAllocationRequestController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetGrantAllocationIDToAdd = function () { $scope.GrantAllocationIDToAdd = ($scope.FromGrantAllocation) ? $scope.getGrantAllocation(angularModelAndViewData.AngularViewData.GrantAllocationID).GrantAllocationID : null; };

    $scope.resetProjectIDToAdd = function () { $scope.ProjectIDToAdd = ($scope.FromProject) ? $scope.getProject(angularModelAndViewData.AngularViewData.ProjectID).ProjectID : null; };

    $scope.getAllUsedGrantAllocationIds = function () {
        return _.map($scope.AngularModel.ProjectGrantAllocationRequests, function (p) { return p.GrantAllocationID; });
    };

    $scope.filteredGrantAllocations = function () {
        var usedGrantAllocationIDs = $scope.getAllUsedGrantAllocationIds();
        return _($scope.AngularViewData.AllGrantAllocations).filter(function (f) {
            return f.IsActive && !_.contains(usedGrantAllocationIDs, f.GrantAllocationID);
        }).sortBy(function (fs) {
            return [fs.GrantAllocationName.toLowerCase()];
        }).value();
    };

    $scope.getAllUsedProjectIDs = function () {
        return _.map($scope.AngularModel.ProjectGrantAllocationRequests, function (p) { return p.ProjectID; });
    };

    $scope.filteredProjects = function () {
        var usedProjectIDs = $scope.getAllUsedProjectIDs();
        return _($scope.AngularViewData.AllProjects).filter(function (f) { return !_.includes(usedProjectIDs, f.ProjectID); })
            .sortBy(["DisplayName"]).value();
    };

    $scope.getProjectName = function (projectGrantAllocationRequest)
    {
        var projectToFind = $scope.getProject(projectGrantAllocationRequest.ProjectID);
        return projectToFind.DisplayName;
    };

    $scope.getProject = function (projectID) {
        return _.find($scope.AngularViewData.AllProjects, function (f) { return projectID == f.ProjectID; });
    };

    $scope.getGrantAllocationName = function (projectGrantAllocationRequest) {
        var grantAllocationToFind = $scope.getGrantAllocation(projectGrantAllocationRequest.GrantAllocationID);
        return grantAllocationToFind.DisplayNameWithAllocationAmount;
    };

    $scope.getGrantAllocation = function (grantAllocationID) {
        return _.find($scope.AngularViewData.AllGrantAllocations, function (f) { return grantAllocationID == f.GrantAllocationID; });
    };

    $scope.getColumnTotal = function () {
        return Number(_.reduce($scope.AngularModel.ProjectGrantAllocationRequests, function (m, x) { return Number(m) + Number(x.TotalAmount); }, 0));
    };

    $scope.getTotal = function() {
        return Number($scope.getColumnTotal());
    };


    $scope.findProjectGrantAllocationRequestRow = function(projectID, grantAllocationID) {
        return _.find($scope.AngularModel.ProjectGrantAllocationRequests,
            function(pfse) { return pfse.ProjectID == projectID && pfse.GrantAllocationID == grantAllocationID; });
    };

    $scope.addRow = function()
    {
        if (($scope.GrantAllocationIDToAdd == null) || ($scope.ProjectIDToAdd == null)) {
            return;
        }
        var newProjectGrantAllocationRequest = $scope.createNewRow($scope.ProjectIDToAdd, $scope.GrantAllocationIDToAdd);
        $scope.AngularModel.ProjectGrantAllocationRequests.push(newProjectGrantAllocationRequest);
        $scope.resetGrantAllocationIDToAdd();
        $scope.resetProjectIDToAdd();
    };

    $scope.createNewRow = function (projectID, grantAllocationID)
    {
        var project = $scope.getProject(projectID);
        var grantAllocation = $scope.getGrantAllocation(grantAllocationID);
        var newProjectGrantAllocationRequest = {
            ProjectID: project.ProjectID,
            GrantAllocationID: grantAllocation.GrantAllocationID,
            TotalAmount: null
    };
        return newProjectGrantAllocationRequest;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectGrantAllocationRequests, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.FromGrantAllocation = angularModelAndViewData.AngularViewData.FromGrantAllocation;
    $scope.FromProject = !$scope.FromGrantAllocation;
    $scope.resetGrantAllocationIDToAdd();
    $scope.resetProjectIDToAdd();
});

