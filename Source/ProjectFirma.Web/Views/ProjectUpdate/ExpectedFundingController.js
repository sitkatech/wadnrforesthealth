/*-----------------------------------------------------------------------
<copyright file="ExpectedFundingController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("ExpectedFundingController", function($scope, angularModelAndViewData)
{
    //console.log("inside angular controller: ExpectedFundingController");
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetFundSourceAllocationToAdd = function () { $scope.FundSourceAllocationToAdd = null; };

    $scope.getAllUsedFundSourceAllocationIds = function () { return _.map($scope.AngularModel.ProjectFundSourceAllocationRequests, function (p) { return p.FundSourceAllocationID; }); };

    $scope.filteredFundSourceAllocationSimples = function () {
        var usedFundSourceAllocationIDs = $scope.getAllUsedFundSourceAllocationIds();
        //console.log("usedFundSourceAllocationIDs:" + usedFundSourceAllocationIDs);
        //console.log("allFundSourceAllocationSimples:" + $scope.AngularViewData.AllFundSourceAllocationSimples);
        return _($scope.AngularViewData.AllFundSourceAllocationSimples).filter(function (f) { return f.IsActive && !_.includes(usedFundSourceAllocationIDs, f.FundSourceAllocationID); })
            .sortBy(function (fs) {
                return [fs.FundSourceAllocationName.toLowerCase(), fs.OrganizationName.toLowerCase()];
            }).value();
    };


    $scope.getFundSourceAllocationName = function(projectFundSourceAllocationRequest)
    {
        var fundSourceAllocationToFind = $scope.getFundSourceAllocation(projectFundSourceAllocationRequest.FundSourceAllocationID);
        return fundSourceAllocationToFind.DisplayName;
    };

    $scope.getFundSourceAllocation = function (fundSourceAllocationID) { return _.find($scope.AngularViewData.AllFundSourceAllocationSimples, function (f) { return fundSourceAllocationID == f.FundSourceAllocationID; }); };

    $scope.getColumnTotal = function () {
        return Number(_.reduce($scope.AngularModel.ProjectFundSourceAllocationRequests,
            function(m, x) { return Number(m) + Number(x.TotalAmount); },
            0));
    };

    $scope.getTotal = function() {
        return Number($scope.getColumnTotal());
    }

    $scope.getRowTotal = function (projectFundSourceAllocationRequest) {
        return Number(projectFundSourceAllocationRequest.TotalAmount);
    }
    
    $scope.findProjectFundSourceAllocationRequestRow = function(projectID, fundSourceAllocationID) { return _.find($scope.AngularModel.ProjectFundSourceAllocationRequests, function(pfse) { return pfse.ProjectID == projectID && pfse.FundSourceAllocationID == fundSourceAllocationID; }); }

    $scope.addRow = function()
    {
        if ($scope.FundSourceAllocationToAdd == null)
        {
            return;
        }
        var newProjectFundSourceAllocationRequest = $scope.createNewRow($scope.ProjectIDToAdd, $scope.FundSourceAllocationToAdd.FundSourceAllocationID);
        $scope.AngularModel.ProjectFundSourceAllocationRequests.push(newProjectFundSourceAllocationRequest);
        $scope.resetFundSourceAllocationToAdd();
    };

    $scope.createNewRow = function(projectID, fundSourceAllocationID)
    {
        var newProjectFundSourceAllocationRequest = {
            ProjectID: projectID,
            FundSourceAllocationID: fundSourceAllocationID,
            TotalAmount: null
        };
        return newProjectFundSourceAllocationRequest;
    };

    $scope.deleteRow = function(rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectFundSourceAllocationRequests, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetFundSourceAllocationToAdd();
    $scope.ProjectIDToAdd = $scope.AngularViewData.ProjectID;
});
