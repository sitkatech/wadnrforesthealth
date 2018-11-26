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
        $scope.pruneBlankRowsIfAppropriate();
        $scope.addBlankRowsIfAppropriate();
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

    $scope.staffTimeActivitiesForFundingSource = function(fundingSource) {
        return _.filter($scope.AngularModel.StaffTimeActivities,
            function(a) { return a.FundingSourceID == fundingSource.FundingSourceID; });
    };

    $scope.fundingSourcesWithActivities = function() {
        return _.filter($scope.AngularViewData.AllFundingSources,
            function(f) {
                return $scope.staffTimeActivitiesForFundingSource(f).length > 0;
            });
    };

    $scope.nonBlankActivities = function() {
        var filtered = _.filter($scope.AngularModel.StaffTimeActivities,
            function(f) {
                return !$scope.rowIsEmpty(f);
            });
        return filtered;
    };

    $scope.blankActivities = function () {
        var filtered = _.filter($scope.AngularModel.StaffTimeActivities,
            function (f) {
                return $scope.rowIsEmpty(f);
            });
        return filtered;
    };
    
    $scope.addFundingSource = function () {
        var newStaffTimeActivity = $scope.createNewRow($scope.AngularViewData.ProjectID, $scope.FundingSourceIDToAdd);
        $scope.AngularModel.StaffTimeActivities.push(newStaffTimeActivity);
        $scope.resetFundingSourceIDToAdd();
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

    $scope.deleteFundingSource = function (fundingSourceToDelete) {
        var staffTimeActivitiesWeCareAbout = $scope.staffTimeActivitiesForFundingSource(fundingSourceToDelete);
        for (var i = 0; i < staffTimeActivitiesWeCareAbout.length; i++) {
            $scope.deleteActivity(staffTimeActivitiesWeCareAbout[i]);
        }
    };

    $scope.deleteActivity = function(activityToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.StaffTimeActivities, activityToDelete);
    }

    $scope.rowIsEmpty = function(staffTimeActivity) {
        return !(staffTimeActivity.StaffTimeActivityHours ||
            staffTimeActivity.StaffTimeActivityRate ||
            staffTimeActivity.StaffTimeActivityTotalAmount ||
            staffTimeActivity.StaffTimeActivityStartDate ||
            staffTimeActivity.StaffTimeActivityEndDate ||
            (staffTimeActivity.StaffTimeActivityNotes && staffTimeActivity.StaffTimeActivityNotes.replace(/\s+/, "")));
    };

    $scope.addBlankRowsIfAppropriate = function() {
        var fundingSourcesWeCareAbout = $scope.fundingSourcesWithActivities();
        for (var i = 0; i < fundingSourcesWeCareAbout.length; i++) {
            var staffTimeActivitiesWeCareAbout =
                $scope.staffTimeActivitiesForFundingSource(fundingSourcesWeCareAbout[i]);
            if (!$scope.rowIsEmpty(staffTimeActivitiesWeCareAbout[staffTimeActivitiesWeCareAbout.length - 1])) {
                $scope.AngularModel.StaffTimeActivities.push($scope.createNewRow($scope.AngularViewData.ProjectID, fundingSourcesWeCareAbout[i].FundingSourceID));
            }
        }
    };

    $scope.pruneBlankRowsIfAppropriate = function() {
        var blankActivities = $scope.blankActivities();
        for (var i = 0; i < blankActivities.length; i++) {
            $scope.deleteActivity(blankActivities[i]);
        }
    };

    $scope.getTotalAmountForActivity = function(staffTimeActivity) {
        return staffTimeActivity.StaffTimeActivityHours * staffTimeActivity.StaffTimeActivityRate;
    };
    
    $scope.getOverallTotalHours = function () {
        return Number(_.reduce($scope.nonBlankActivities(),
            function (m, x) { return Number(m) + Number(x.StaffTimeActivityHours); },
            0));
    };

    $scope.getOverallTotalAmount = function () {
        return Number(_.reduce($scope.nonBlankActivities(),
            function (m, x) { return Number(m) + Number($scope.getTotalAmountForActivity(x)); },
            0));
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.StaffTimeActivities == null) {
        $scope.AngularModel.StaffTimeActivities = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetFundingSourceIDToAdd();
});

