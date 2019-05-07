/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("EditGrantAllocationController", function($scope,
    angularModelAndViewData) {

    console.log('start of EditGrantAllocationController');

    //$scope.$watch(function() {
    //    jQuery(".selectpicker").selectpicker("refresh");

    //    // so that unsavedChanges.js knows to check if the form has changed.
    //    jQuery("form").trigger("input");
    //});

    //$scope.getAvailableGrantAllocationsForAgreement = function() {
    //    var allPossibleGrantAllocationJsons = $scope.AngularViewData.AllPossibleGrantAllocationJsons;
    //    var usedGrantAllocations = $scope.AngularModel.GrantAllocationJsons;
    //    var usedGrantAllocationIds = _.map(usedGrantAllocations,
    //        function(f) {
    //            return f.GrantAllocationID;
    //        });

    //    var filteredGrantAllocationSelectList = _.filter(allPossibleGrantAllocationJsons,
    //        function(f) {
    //            return !_.includes(usedGrantAllocationIds, f.GrantAllocationID)
    //        });

    //    return filteredGrantAllocationSelectList;
    //};

    $scope.addProgramIndexProjectCode = function (programIndexId, programIndexName, projectCodeId, projectCodeName) {

        var grantAllocationJson = allRelevantGrantAllocationJsons[0];

        if (grantAllocationJson !== undefined)
        {
            $scope.AngularModel.GrantAllocationJsons.push({
                GrantAllocationID: Number(grantAllocationJson.GrantAllocationID),
                GrantNumber: grantAllocationJson.GrantNumber,
                GrantAllocationName: grantAllocationJson.GrantAllocationName
            });
        }
        $scope.resetSelectedGrantAllocationID();
    };

    $scope.removeSelectedGrantAllocation = function(grantAllocationID) {
        _.remove($scope.AngularModel.GrantAllocationJsons,
            function(pos) {
                return pos.GrantAllocationID == grantAllocationID;
            });
    };

    $scope.resetSelectedGrantAllocationID = function() {
        $scope.selectedGrantAllocationID = "";
    };

    $scope.isOptionSelected = function (grantAllocation)
    {
        return false;
    };

    $scope.dropdownDefaultOption = function ()
    {
        // TODO; can we use type here?
        return "Add a " + 'Grant Allocation';
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    console.log('EditAgreementGrantAllocationsController -- Angular view data:');
    console.log($scope.AngularViewData);

    $scope.selectedGrantAllocationID = null;
});
