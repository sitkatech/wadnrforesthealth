/*-----------------------------------------------------------------------
<copyright file="ProjectGrantAllocationExpenditureController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("ProjectGrantAllocationExpenditureController", function ($scope, angularModelAndViewData) {
   
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetGrantAllocationIDToAdd = function () { $scope.GrantAllocationIDToAdd = ($scope.FromGrantAllocation) ? $scope.getGrantAllocation(angularModelAndViewData.AngularViewData.GrantAllocationID).GrantAllocationID : null; };
    
    $scope.resetProjectIDToAdd = function () { $scope.ProjectIDToAdd = ($scope.FromProject) ? $scope.getProject(angularModelAndViewData.AngularViewData.ProjectID).ProjectID : null; };

    $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray = function () { return _($scope.AngularModel.ProjectGrantAllocationExpenditures).pluck("CalendarYearExpenditures").flatten(); }

    $scope.getAllUsedCalendarYears = function () {
        return $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray().pluck("CalendarYear").flatten().union().sortBy().value();
    };

    $scope.getCalendarYearRange = function () {
        return _.sortBy(_.union($scope.getAllUsedCalendarYears(), $scope.AngularViewData.CalendarYearRange)).reverse();
    };

    $scope.getAllUsedGrantAllocationIds = function () {
        return _.map($scope.AngularModel.ProjectGrantAllocationExpenditures, function (p) { return p.GrantAllocationID; });
    };

    $scope.filteredGrantAllocations = function () {
        var usedGrantAllocationIDs = $scope.getAllUsedGrantAllocationIds();
        //console.log("usedGrantAllocationIDs:" + usedGrantAllocationIDs);
        //console.log("$scope.AngularViewData.AllGrantAllocationSimples:" + $scope.AngularViewData.AllGrantAllocationSimples);
        return _($scope.AngularViewData.AllGrantAllocationSimples).filter(function (ga) {
                
               return ga.IsActive && !_.contains(usedGrantAllocationIDs, ga.GrantAllocationID);
            }).sortBy(function (gas) {
                return [gas.GrantAllocationName.toLowerCase()];
            }).value();
    };

    $scope.getAllUsedProjectIds = function () {
        return _.map($scope.AngularModel.ProjectGrantAllocationExpenditures, function (p) { return p.ProjectID; });
    };

    $scope.filteredProjects = function () {
        var usedProjectIDs = $scope.getAllUsedProjectIds();
        return _($scope.AngularViewData.AllProjects)
            .filter(function(f) { return !_.contains(usedProjectIDs, f.ProjectID); }).sortBy(["DisplayName"]).value();
    };

    $scope.getProjectName = function (projectGrantAllocationExpenditure)
    {
        var projectToFind = $scope.getProject(projectGrantAllocationExpenditure.ProjectID);
        return projectToFind.DisplayName;
    };

    $scope.getProject = function (projectId) {
        return _.find($scope.AngularViewData.AllProjects, function (f) { return projectId == f.ProjectID; });
    };

    $scope.getGrantAllocationName = function (projectGrantAllocationExpenditure) {
        var grantAllocationToFind = $scope.getGrantAllocation(projectGrantAllocationExpenditure.GrantAllocationID);
        return grantAllocationToFind.DisplayName;
    };

    $scope.getGrantAllocation = function (grantAllocationId) {
        return _.find($scope.AngularViewData.AllGrantAllocationSimples, function (f) { return grantAllocationId == f.GrantAllocationID; });
    };

    $scope.getExpenditureTotalForCalendarYear = function (calendarYear)
    {
        var calendarYearExpendituresAsFlattenedArray = $scope.getAllCalendarYearExpendituresAsFlattenedLoDashArray().filter(function (pfse) { return Sitka.Methods.isUndefinedNullOrEmpty(calendarYear) || pfse.CalendarYear == calendarYear; }).value();
        return $scope.calculateExpenditureTotal(calendarYearExpendituresAsFlattenedArray);
    };

    $scope.getExpenditureTotalForRow = function (projectGrantAllocationExpenditure)
    {
        var calendarYearExpendituresAsFlattenedArray = _($scope.AngularModel.ProjectGrantAllocationExpenditures).filter(function (pfse) { return pfse.ProjectID == projectGrantAllocationExpenditure.ProjectID && pfse.GrantAllocationID == projectGrantAllocationExpenditure.GrantAllocationID; }).pluck("CalendarYearExpenditures").flatten().value();
        return $scope.calculateExpenditureTotal(calendarYearExpendituresAsFlattenedArray);
    };

    $scope.calculateExpenditureTotal = function (expenditures) {
        var fart = _.reduce(expenditures, function(m, x) { return Number(m) + Number(x.MonetaryAmount); }, 0);
        return fart;
    };

    $scope.addCalendarYear = function (calendarYear) {
        if (Sitka.Methods.isUndefinedNullOrEmpty(calendarYear)) {
            return;
        }
        if ($scope.FromProject)
        {
            _.each($scope.getAllUsedGrantAllocationIds(), function (grantAllocationId) {
                $scope.addCalendarYearExpenditureRow($scope.ProjectIDToAdd, grantAllocationId, calendarYear);
            });
        }
        else if ($scope.FromGrantAllocation) {
            _.each($scope.getAllUsedProjectIds(), function (projectId) {
                $scope.addCalendarYearExpenditureRow(projectId, $scope.GrantAllocationIDToAdd, calendarYear);
            });
        }
    };

    $scope.formatCalendarYear = function (calendarYear) { return $scope.AngularViewData.UseFiscalYears ? "FY" + calendarYear : calendarYear; };

    $scope.findProjectGrantAllocationExpenditureRow = function (projectId, grantAllocationId) { return _.find($scope.AngularModel.ProjectGrantAllocationExpenditures, function(pfse) { return pfse.ProjectID == projectId && pfse.GrantAllocationID == grantAllocationId; }); }
    
    $scope.addRow = function () {
        if (($scope.GrantAllocationIDToAdd == null) || ($scope.ProjectIDToAdd == null)) {
            return;
        }
        var newProjectGrantAllocationExpenditure = $scope.createNewRow($scope.ProjectIDToAdd, $scope.GrantAllocationIDToAdd, $scope.getCalendarYearRange());
        $scope.AngularModel.ProjectGrantAllocationExpenditures.push(newProjectGrantAllocationExpenditure);
        $scope.resetGrantAllocationIDToAdd();
        $scope.resetProjectIDToAdd();
    };

    $scope.createNewRow = function (projectId, grantAllocationId, calendarYearsToAdd)
    {
        var project = $scope.getProject(projectId);
        var grantAllocation = $scope.getGrantAllocation(grantAllocationId);
        var newProjectGrantAllocationExpenditure = {
            ProjectID: project.ProjectID,
            GrantAllocationID: grantAllocation.GrantAllocationID,
            CalendarYearExpenditures: _.map(calendarYearsToAdd, $scope.createNewCalendarYearExpenditureRow)
        };
        return newProjectGrantAllocationExpenditure;
    };

    $scope.addCalendarYearExpenditureRow = function (projectId, grantAllocationId, calendarYear) {
        var projectGrantAllocationExpenditure = $scope.findProjectGrantAllocationExpenditureRow(projectId, grantAllocationId);
        if (!Sitka.Methods.isUndefinedNullOrEmpty(projectGrantAllocationExpenditure)) {
            projectGrantAllocationExpenditure.CalendarYearExpenditures.push($scope.createNewCalendarYearExpenditureRow(calendarYear));
        }
    };

    $scope.createNewCalendarYearExpenditureRow = function (calendarYear) {
        return {
            CalendarYear: calendarYear,
            MonetaryAmount: null
        };
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectGrantAllocationExpenditures, rowToDelete);
    };

    $scope.selectAllYears = function (isChecked) {
        _.each($scope.AngularModel.ProjectExemptReportingYears,
            function (f) {
                f.IsExempt = isChecked;
            });
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    if ($scope.AngularModel.ProjectGrantAllocationExpenditures == null) {
        $scope.AngularModel.ProjectGrantAllocationExpenditures = [];
    }
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.FromGrantAllocation = angularModelAndViewData.AngularViewData.FromGrantAllocation;
    $scope.FromProject = !$scope.FromGrantAllocation;
    $scope.resetGrantAllocationIDToAdd();
    $scope.resetProjectIDToAdd();
});

