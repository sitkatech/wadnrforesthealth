/*-----------------------------------------------------------------------
<copyright file="EditAgreementFundSourceAllocationsController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("EditAgreementFundSourceAllocationsController", function($scope,
    angularModelAndViewData) {

    console.log('start of EditAgreementFundSourceAllocationsController');

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.getAvailableFundSourceAllocationsForAgreement = function() {
        var allPossibleFundSourceAllocationJsons = $scope.AngularViewData.AllPossibleFundSourceAllocationJsons;
        var usedFundSourceAllocations = $scope.AngularModel.FundSourceAllocationJsons;
        var usedFundSourceAllocationIds = _.map(usedFundSourceAllocations,
            function(f) {
                return f.FundSourceAllocationID;
            });

        var filteredFundSourceAllocationSelectList = _.filter(allPossibleFundSourceAllocationJsons,
            function(f) {
                return !_.includes(usedFundSourceAllocationIds, f.FundSourceAllocationID)
            });

        return filteredFundSourceAllocationSelectList;
    };

    $scope.addFundSourceAllocation = function (fundSourceAllocationId) {
        var allRelevantFundSourceAllocationJsons = _.filter($scope.AngularViewData.AllPossibleFundSourceAllocationJsons,
            function(f) {
                return f.FundSourceAllocationID === Number(fundSourceAllocationId);
            });
        var fundSourceAllocationJson = allRelevantFundSourceAllocationJsons[0];

        if (fundSourceAllocationJson !== undefined)
        {
            $scope.AngularModel.FundSourceAllocationJsons.push({
                FundSourceAllocationID: Number(fundSourceAllocationJson.FundSourceAllocationID),
                FundSourceNumber: fundSourceAllocationJson.FundSourceNumber,
                FundSourceAllocationName: fundSourceAllocationJson.FundSourceAllocationName
            });
        }
        $scope.resetSelectedFundSourceAllocationID();
    };

    $scope.removeSelectedFundSourceAllocation = function(fundSourceAllocationID) {
        _.remove($scope.AngularModel.FundSourceAllocationJsons,
            function(pos) {
                return pos.FundSourceAllocationID == fundSourceAllocationID;
            });
    };

    $scope.resetSelectedFundSourceAllocationID = function() {
        $scope.selectedFundSourceAllocationID = "";
    };

    $scope.isOptionSelected = function (fundSourceAllocation)
    {
        return false;
    };

    $scope.dropdownDefaultOption = function ()
    {
        // TODO; can we use type here?
        return "Add a " + 'FundSource Allocation';
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    console.log('EditAgreementFundSourceAllocationsController -- Angular view data:');
    console.log($scope.AngularViewData);

    $scope.selectedFundSourceAllocationID = null;
});
