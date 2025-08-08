/*-----------------------------------------------------------------------
<copyright file="ProjectFundSourceAllocationRequestController.js" company="Tahoe Regional Planning Agency">
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
angular.module("ProjectFirmaApp").controller("ProjectFundSourceAllocationRequestController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetFundSourceAllocationIDToAdd = function () { $scope.FundSourceAllocationIDToAdd = ($scope.FromFundSourceAllocation) ? $scope.getFundSourceAllocation(angularModelAndViewData.AngularViewData.FundSourceAllocationID).FundSourceAllocationID : null; };

    $scope.resetProjectIDToAdd = function () { $scope.ProjectIDToAdd = ($scope.FromProject) ? $scope.getProject(angularModelAndViewData.AngularViewData.ProjectID).ProjectID : null; };

    $scope.getAllUsedFundSourceAllocationIds = function () {
        return _.map($scope.AngularModel.ProjectFundSourceAllocationRequests, function (p) { return p.FundSourceAllocationID; });
    };

    $scope.filteredFundSourceAllocations = function () {
        var usedFundSourceAllocationIDs = $scope.getAllUsedFundSourceAllocationIds();
        return _($scope.AngularViewData.AllFundSourceAllocations).filter(function (f) {
            return f.IsActive && !_.contains(usedFundSourceAllocationIDs, f.FundSourceAllocationID);
        }).sortBy(function (fs) {
            return [fs.FundSourceAllocationName.toLowerCase()];
        }).value();
    };

    $scope.getAllUsedProjectIDs = function () {
        return _.map($scope.AngularModel.ProjectFundSourceAllocationRequests, function (p) { return p.ProjectID; });
    };

    $scope.filteredProjects = function () {
        var usedProjectIDs = $scope.getAllUsedProjectIDs();
        return _($scope.AngularViewData.AllProjects).filter(function (f) { return !_.includes(usedProjectIDs, f.ProjectID); })
            .sortBy(["DisplayName"]).value();
    };

    $scope.getProjectName = function (projectFundSourceAllocationRequest)
    {
        var projectToFind = $scope.getProject(projectFundSourceAllocationRequest.ProjectID);
        return projectToFind.DisplayName;
    };

    $scope.getProject = function (projectID) {
        return _.find($scope.AngularViewData.AllProjects, function (f) { return projectID == f.ProjectID; });
    };

    $scope.getFundSourceAllocationName = function (projectFundSourceAllocationRequest) {
        var fundSourceAllocationToFind = $scope.getFundSourceAllocation(projectFundSourceAllocationRequest.FundSourceAllocationID);
        return fundSourceAllocationToFind.DisplayNameWithAllocationAmount;
    };

    $scope.getFundSourceAllocation = function (fundSourceAllocationID) {
        return _.find($scope.AngularViewData.AllFundSourceAllocations, function (f) { return fundSourceAllocationID == f.FundSourceAllocationID; });
    };

    $scope.getColumnTotal = function () {
        return Number(_.reduce($scope.AngularModel.ProjectFundSourceAllocationRequests, function (m, x) { return Number(m) + Number(x.TotalAmount); }, 0));
    };

    $scope.getTotal = function() {
        return Number($scope.getColumnTotal());
    };


    $scope.findProjectFundSourceAllocationRequestRow = function(projectID, fundSourceAllocationID) {
        return _.find($scope.AngularModel.ProjectFundSourceAllocationRequests,
            function(pfse) { return pfse.ProjectID == projectID && pfse.FundSourceAllocationID == fundSourceAllocationID; });
    };

    $scope.addRow = function()
    {
        if (($scope.FundSourceAllocationIDToAdd == null) || ($scope.ProjectIDToAdd == null)) {
            return;
        }
        var newProjectFundSourceAllocationRequest = $scope.createNewRow($scope.ProjectIDToAdd, $scope.FundSourceAllocationIDToAdd);
        $scope.AngularModel.ProjectFundSourceAllocationRequests.push(newProjectFundSourceAllocationRequest);
        $scope.resetFundSourceAllocationIDToAdd();
        $scope.resetProjectIDToAdd();
    };

    $scope.createNewRow = function (projectID, fundSourceAllocationID)
    {
        var project = $scope.getProject(projectID);
        var fundSourceAllocation = $scope.getFundSourceAllocation(fundSourceAllocationID);
        var newProjectFundSourceAllocationRequest = {
            ProjectID: project.ProjectID,
            FundSourceAllocationID: fundSourceAllocation.FundSourceAllocationID,
            TotalAmount: null
    };
        return newProjectFundSourceAllocationRequest;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectFundSourceAllocationRequests, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.FromFundSourceAllocation = angularModelAndViewData.AngularViewData.FromFundSourceAllocation;
    $scope.FromProject = !$scope.FromFundSourceAllocation;
    $scope.resetFundSourceAllocationIDToAdd();
    $scope.resetProjectIDToAdd();
});

