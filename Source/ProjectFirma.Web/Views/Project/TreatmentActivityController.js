/*-----------------------------------------------------------------------
<copyright file="TreatmentActivityController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("TreatmentActivityController", function ($scope, angularModelAndViewData)
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
        return _.map($scope.AngularModel.TreatmentActivities, function (p) { return p.FundingSourceID; });
    };
    
    $scope.getTreatmentTypeName = function (treatmentActivity) {
        var treatmentTypeToFind = $scope.getTreatmentType(treatmentActivity.TreatmentActivityTypeID);
        return treatmentTypeToFind.DisplayName;
    };

    $scope.getTreatmentType = function (fundingSourceId) {
        return _.find($scope.AngularViewData.AllFundingSources, function (f) { return fundingSourceId == f.FundingSourceID; });
    };

    $scope.nonBlankActivities = function() {
        var filtered = _.filter($scope.AngularModel.TreatmentActivities,
            function(f) {
                return !$scope.rowIsEmpty(f);
            });
        return filtered;
    };

    $scope.treatmentActivitiesForForm = function() {
        var filtered = $scope.AngularModel.TreatmentActivities;
        var a = [];
        for (var i = 0; i < filtered.length - 1; i++) {
            if ($scope.rowIsEmpty(filtered[i])) {
                a.push(i);
            }
        }
        for (var i = 0; i < a.length; i++) {
            Sitka.Methods.removeFromJsonArray(filtered, filtered[a[i]]);
        }

        return filtered;
    };

    $scope.blankActivities = function () {
        var filtered = _.filter($scope.AngularModel.TreatmentActivities,
            function (f) {
                return $scope.rowIsEmpty(f);
            });
        return filtered;
    };

    $scope.createNewRow = function () {
        var newTreatmentActivity = {
            ProjectID: $scope.AngularViewData.ProjectID
    };
        return newTreatmentActivity;
    };

    $scope.deleteActivity = function(activityToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.TreatmentActivities, activityToDelete);
    }

    $scope.rowIsEmpty = function(treatmentActivity) {
        return !(treatmentActivity.TreatmentActivityTypeID ||
            treatmentActivity.TreatmentActivityStartDate ||
            treatmentActivity.TreatmentActivityEndDate ||
            treatmentActivity.TreatmentActivityAcresTreated ||
            (treatmentActivity.TreatmentActivityNotes && treatmentActivity.TreatmentActivityNotes.replace(/\s+/, "")));
    };

    $scope.addBlankRowsIfAppropriate = function () {
        var treatmentActivities = $scope.AngularModel.TreatmentActivities;
        if (treatmentActivities.length === 0 || !$scope.rowIsEmpty(treatmentActivities[treatmentActivities.length - 1])) {
            $scope.AngularModel.TreatmentActivities.push($scope.createNewRow());
        }
    };

    $scope.getOverallTotalAcreage = function () {
        return Number(_.reduce($scope.nonBlankActivities(),
            function (m, x) { return Number(m) + Number(x.TreatmentActivityAcresTreated); },
            0));
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.TreatmentActivities == null) {
        $scope.AngularModel.TreatmentActivities = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetFundingSourceIDToAdd();
});
