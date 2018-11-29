/*-----------------------------------------------------------------------
<copyright file="ContractorTimeActivityController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("ContractorTimeActivityController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
        jQuery(".sitkaDatePicker").datepicker();
        $scope.addBlankRowsIfAppropriate();
    });

    $scope.resetFundingSourceIDToAdd = function() {
        $scope.FundingSourceIDToAdd = null;
    };
    
    $scope.getAllUsedFundingSourceIds = function () {
        return _.map($scope.AngularModel.ContractorTimeActivities, function (p) { return p.FundingSourceID; });
    };

    $scope.filteredFundingSources = function () {
        var usedFundingSourceIDs = $scope.getAllUsedFundingSourceIds();
        return _($scope.AngularViewData.AllFundingSources).filter(function(f) {
               return f.IsActive && !_.contains(usedFundingSourceIDs, f.FundingSourceID);
            }).sortBy(function (fs) {
                return [fs.FundingSourceName.toLowerCase()];
            }).value();
    };

    $scope.getFundingSourceName = function (contractorTimeActivity) {
        var fundingSourceToFind = $scope.getFundingSource(contractorTimeActivity.FundingSourceID);
        return fundingSourceToFind.DisplayName;
    };

    $scope.getFundingSource = function (fundingSourceId) {
        return _.find($scope.AngularViewData.AllFundingSources, function (f) { return fundingSourceId == f.FundingSourceID; });
    };

    $scope.contractorTimeActivitiesForFundingSource = function(fundingSource, ignoreBlanks) {
        var filtered = _.filter($scope.AngularModel.ContractorTimeActivities,
            function (a) { return a.FundingSourceID == fundingSource.FundingSourceID; });
        if (!ignoreBlanks) {
            var a = [];
            for (var i = 0; i < filtered.length - 1; i++) {
                if ($scope.rowIsEmpty(filtered[i])) {
                    a.push(i);
                }
            }
            for (var i = 0; i < a.length; i++) {
                Sitka.Methods.removeFromJsonArray(filtered, filtered[a[i]]);
            }
        }
        return filtered;
    };

    $scope.fundingSourcesWithActivities = function() {
        return _.filter($scope.AngularViewData.AllFundingSources,
            function(f) {
                return $scope.contractorTimeActivitiesForFundingSource(f).length > 0;
            });
    };

    $scope.addFundingSource = function () {
        var newContractorTimeActivity = $scope.createNewRow($scope.AngularViewData.ProjectID, $scope.FundingSourceIDToAdd);
        $scope.AngularModel.ContractorTimeActivities.push(newContractorTimeActivity);
        $scope.resetFundingSourceIDToAdd();
    };

    $scope.deleteFundingSource = function (fundingSourceToDelete) {
        var contractorTimeActivitiesWeCareAbout = $scope.contractorTimeActivitiesForFundingSource(fundingSourceToDelete, true);
        for (var i = 0; i < contractorTimeActivitiesWeCareAbout.length; i++) {
            $scope.deleteActivity(contractorTimeActivitiesWeCareAbout[i]);
        }
    };

    $scope.nonBlankActivities = function() {
        var filtered = _.filter($scope.AngularModel.ContractorTimeActivities,
            function(f) {
                return !$scope.rowIsEmpty(f);
            });
        return filtered;
    };

    $scope.blankActivities = function () {
        var filtered = _.filter($scope.AngularModel.ContractorTimeActivities,
            function (f) {
                return $scope.rowIsEmpty(f);
            });
        return filtered;
    };

    $scope.createNewRow = function (projectID, fundingSourceID)
    {
        var fundingSource = $scope.getFundingSource(fundingSourceID);
        var newContractorTimeActivity = {
            ProjectID: projectID,
            FundingSourceID: fundingSource.FundingSourceID,
        };
        return newContractorTimeActivity;
    };

    $scope.deleteActivity = function(activityToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ContractorTimeActivities, activityToDelete);
    }

    $scope.rowIsEmpty = function(contractorTimeActivity) {
        return !(contractorTimeActivity.ContractorTimeActivityHours ||
            contractorTimeActivity.ContractorTimeActivityRate ||
            contractorTimeActivity.ContractorTimeActivityHours ||
            contractorTimeActivity.ContractorTimeActivityTotalAmount ||
            contractorTimeActivity.ContractorTimeActivityStartDate ||
            contractorTimeActivity.ContractorTimeActivityEndDate ||
            (contractorTimeActivity.ContractorTimeActivityNotes && contractorTimeActivity.ContractorTimeActivityNotes.replace(/\s+/, "")));
    };

    $scope.addBlankRowsIfAppropriate = function() {
        var fundingSourcesWeCareAbout = $scope.fundingSourcesWithActivities();
        for (var i = 0; i < fundingSourcesWeCareAbout.length; i++) {
            var contractorTimeActivitiesWeCareAbout =
                $scope.contractorTimeActivitiesForFundingSource(fundingSourcesWeCareAbout[i]);
            if (!$scope.rowIsEmpty(contractorTimeActivitiesWeCareAbout[contractorTimeActivitiesWeCareAbout.length - 1])) {
                $scope.AngularModel.ContractorTimeActivities.push($scope.createNewRow($scope.AngularViewData.ProjectID, fundingSourcesWeCareAbout[i].FundingSourceID));
            }
        }
    };

    $scope.getTotalAmountForActivity = function(contractorTimeActivity) {
        return contractorTimeActivity.ContractorTimeActivityHours * contractorTimeActivity.ContractorTimeActivityRate;
    };
    
    $scope.getOverallTotalHours = function () {
        return Number(_.reduce($scope.nonBlankActivities(),
            function (m, x) { return Number(m) + Number(x.ContractorTimeActivityHours); },
            0));
    };

    $scope.getOverallTotalAmount = function () {
        return Number(_.reduce($scope.nonBlankActivities(),
            function (m, x) { return Number(m) + Number($scope.getTotalAmountForActivity(x)); },
            0));
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.ContractorTimeActivities == null) {
        $scope.AngularModel.ContractorTimeActivities = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetFundingSourceIDToAdd();
});
