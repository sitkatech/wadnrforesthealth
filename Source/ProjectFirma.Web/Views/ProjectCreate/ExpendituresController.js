/*-----------------------------------------------------------------------
<copyright file="ExpendituresController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("ExpendituresController", function($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetGrantAllocationIDToAdd = function() { $scope.GrantAllocationIDToAdd = null; };

    $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray = function() { return _($scope.AngularModel.ProjectGrantAllocationExpenditures).pluck("CalendarYearExpenditures").flatten(); }

    $scope.getAllUsedCalendarYears = function() { return $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray().pluck("CalendarYear").flatten().union().sortBy().value(); };

    $scope.getCalendarYearRange = function() {
        return _.sortBy(_.union($scope.getAllUsedCalendarYears(), $scope.AngularViewData.CalendarYearRange)).reverse();
    };

    $scope.getAllUsedGrantAllocationIds = function() { return _.map($scope.AngularModel.ProjectGrantAllocationExpenditures, function(p) { return p.GrantAllocationID; }); };

    $scope.filteredGrantAllocations = function()
    {
        var usedGrantAllocationIDs = $scope.getAllUsedGrantAllocationIds();
        var projectFundingOrganizationGrantAllocationIDs = _.map($scope.AngularViewData.AllGrantAllocations, function(p) { return p.GrantAllocationID; });
        if ($scope.ShowOnlyProjectFunders)
        {
            projectFundingOrganizationGrantAllocationIDs = $scope.AngularViewData.ProjectFundingOrganizationGrantAllocationIDs;
        }
        return _($scope.AngularViewData.AllGrantAllocations).filter(function(f) { return f.IsActive && _.includes(projectFundingOrganizationGrantAllocationIDs, f.GrantAllocationID) && !_.includes(usedGrantAllocationIDs, f.GrantAllocationID); }).sortByAll(["GrantAllocationName"]).value();
    };

    $scope.getGrantAllocationName = function(projectGrantAllocationExpenditure)
    {
        var grantAllocationToFind = $scope.getGrantAllocation(projectGrantAllocationExpenditure.GrantAllocationID);
        return grantAllocationToFind.DisplayName;
    };

    $scope.getGrantAllocation = function(grantAllocationId) { return _.find($scope.AngularViewData.AllGrantAllocations, function(f) { return grantAllocationId == f.GrantAllocationID; }); };

    $scope.getExpenditureTotalForCalendarYear = function(calendarYear)
    {
        var calendarYearExpendituresAsFlattenedArray = $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray().filter(function(pfse) { return Sitka.Methods.isUndefinedNullOrEmpty(calendarYear) || pfse.CalendarYear == calendarYear; }).value();
        return $scope.calculateExpenditureTotal(calendarYearExpendituresAsFlattenedArray);
    };

    $scope.getExpenditureTotalForRow = function(projectGrantAllocationExpenditure)
    {
        var calendarYearExpendituresAsFlattenedArray = _($scope.AngularModel.ProjectGrantAllocationExpenditures).filter(function(pfse) { return pfse.ProjectID == projectGrantAllocationExpenditure.ProjectID && pfse.GrantAllocationID == projectGrantAllocationExpenditure.GrantAllocationID; }).pluck("CalendarYearExpenditures").flatten().value();
        return $scope.calculateExpenditureTotal(calendarYearExpendituresAsFlattenedArray);
    };

    $scope.calculateExpenditureTotal = function(expenditures) {
        return _.reduce(expenditures, function(m, x) { return Number(m) + Number(x.MonetaryAmount); }, 0);
    };

    $scope.addCalendarYear = function(calendarYear)
    {
        if (Sitka.Methods.isUndefinedNullOrEmpty(calendarYear))
        {
            return;
        }
        _.each($scope.getAllUsedGrantAllocationIds(), function(grantAllocationId) { $scope.addCalendarYearExpenditureRow($scope.ProjectIDToAdd, grantAllocationId, calendarYear); });
    };

    $scope.formatCalendarYear = function (calendarYear) { return $scope.AngularViewData.UseFiscalYears ? "FY" + calendarYear : calendarYear; };

    $scope.findProjectGrantAllocationExpenditureRow = function(projectId, grantAllocationId) { return _.find($scope.AngularModel.ProjectGrantAllocationExpenditures, function(pfse) { return pfse.ProjectID == projectId && pfse.GrantAllocationID == grantAllocationId; }); }

    $scope.addRow = function()
    {
        if (($scope.GrantAllocationIDToAdd == null) || ($scope.ProjectIDToAdd == null))
        {
            return;
        }
        var newProjectGrantAllocationExpenditure = $scope.createNewRow($scope.ProjectIDToAdd, $scope.GrantAllocationIDToAdd, $scope.getCalendarYearRange());
        $scope.AngularModel.ProjectGrantAllocationExpenditures.push(newProjectGrantAllocationExpenditure);
        $scope.resetGrantAllocationIDToAdd();
    };

    $scope.createNewRow = function(projectId, grantAllocationId, calendarYearsToAdd)
    {
        var grantAllocation = $scope.getGrantAllocation(grantAllocationId);
        var newProjectGrantAllocationExpenditure = {
            ProjectID: projectId,
            GrantAllocationID: grantAllocation.GrantAllocationID,
            CalendarYearExpenditures: _.map(calendarYearsToAdd, $scope.createNewCalendarYearExpenditureRow)
        };
        return newProjectGrantAllocationExpenditure;
    };

    $scope.addCalendarYearExpenditureRow = function(projectId, grantAllocationId, calendarYear)
    {
        var projectGrantAllocationExpenditure = $scope.findProjectGrantAllocationExpenditureRow(projectId, grantAllocationId);
        if (!Sitka.Methods.isUndefinedNullOrEmpty(projectGrantAllocationExpenditure))
        {
            projectGrantAllocationExpenditure.CalendarYearExpenditures.push($scope.createNewCalendarYearExpenditureRow(calendarYear));
        }
    };

    $scope.createNewCalendarYearExpenditureRow = function(calendarYear)
    {
        return {
            CalendarYear: calendarYear,
            MonetaryAmount: null
        };
    };

    $scope.selectAllYears = function (isChecked) {
        _.each($scope.AngularModel.ProjectExemptReportingYears,
            function(f) {
                f.IsExempt = isChecked;
            });
    };

    $scope.deleteRow = function(rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectGrantAllocationExpenditures, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.ProjectGrantAllocationExpenditures == null) {
        $scope.AngularModel.ProjectGrantAllocationExpenditures = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.ShowOnlyProjectFunders = false;
    $scope.resetGrantAllocationIDToAdd();
    $scope.ProjectIDToAdd = $scope.AngularViewData.ProjectID;
});
