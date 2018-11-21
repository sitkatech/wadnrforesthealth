/*-----------------------------------------------------------------------
<copyright file="StaffTimeActivityController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("StaffTimeActivityController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
        jQuery(".sitkaDatePicker").datepicker();
    });

    $scope.resetFundingSourceIDToAdd = function() {
        $scope.FundingSourceIDToAdd = null;
    };
    
    $scope.getAllUsedFundingSourceIds = function () {
        return _.map($scope.AngularModel.StaffTimeActivities, function (p) { return p.FundingSourceID; });
    };

    $scope.filteredFundingSources = function () {
        var usedFundingSourceIDs = $scope.getAllUsedFundingSourceIds();
        return _($scope.AngularViewData.AllFundingSources).filter(function(f) {
               return f.IsActive && !_.contains(usedFundingSourceIDs, f.FundingSourceID);
            }).sortBy(function (fs) {
                return [fs.FundingSourceName.toLowerCase()];
            }).value();
    };

    $scope.getFundingSourceName = function (staffTimeActivity) {
        var fundingSourceToFind = $scope.getFundingSource(staffTimeActivity.FundingSourceID);
        return fundingSourceToFind.DisplayName;
    };

    $scope.getFundingSource = function (fundingSourceId) {
        return _.find($scope.AngularViewData.AllFundingSources, function (f) { return fundingSourceId == f.FundingSourceID; });
    };

    $scope.findStaffTimeActivityRow = function(projectId, fundingSourceId) { return _.find($scope.AngularModel.StaffTimeActivities, function(pfse) { return pfse.ProjectID == projectId && pfse.FundingSourceID == fundingSourceId; }); }
    
    $scope.addRow = function () {
        var newStaffTimeActivity = $scope.createNewRow($scope.AngularViewData.ProjectID, $scope.FundingSourceIDToAdd);
        $scope.AngularModel.StaffTimeActivities.push(newStaffTimeActivity);
        $scope.resetFundingSourceIDToAdd();
        $scope.resetProjectIDToAdd();
    };

    $scope.createNewRow = function (projectID, fundingSourceID)
    {
        var fundingSource = $scope.getFundingSource(fundingSourceID);
        var newStaffTimeActivity = {
            ProjectID: projectID,
            FundingSourceID: fundingSource.FundingSourceID,
        };
        return newStaffTimeActivity;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.StaffTimeActivities, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.StaffTimeActivities == null) {
        $scope.AngularModel.StaffTimeActivities = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetFundingSourceIDToAdd();
});

