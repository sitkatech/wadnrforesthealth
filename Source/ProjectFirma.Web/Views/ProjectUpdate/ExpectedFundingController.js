/*-----------------------------------------------------------------------
<copyright file="ExpectedFundingController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("ExpectedFundingController", function($scope, angularModelAndViewData)
{
    //console.log("inside angular controller: ExpectedFundingController");
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

    $scope.resetGrantAllocationToAdd = function () { $scope.GrantAllocationToAdd = null; };

    $scope.getAllUsedGrantAllocationIds = function () { return _.map($scope.AngularModel.ProjectGrantAllocationRequests, function (p) { return p.GrantAllocationID; }); };

    $scope.filteredGrantAllocationSimples = function () {
        var usedGrantAllocationIDs = $scope.getAllUsedGrantAllocationIds();
        //console.log("usedGrantAllocationIDs:" + usedGrantAllocationIDs);
        //console.log("allGrantAllocationSimples:" + $scope.AngularViewData.AllGrantAllocationSimples);
        return _($scope.AngularViewData.AllGrantAllocationSimples).filter(function (f) { return f.IsActive && !_.includes(usedGrantAllocationIDs, f.GrantAllocationID); })
            .sortBy(function (fs) {
                return [fs.GrantAllocationName.toLowerCase(), fs.OrganizationName.toLowerCase()];
            }).value();
    };


    $scope.getGrantAllocationName = function(projectGrantAllocationRequest)
    {
        var grantAllocationToFind = $scope.getGrantAllocation(projectGrantAllocationRequest.GrantAllocationID);
        return grantAllocationToFind.DisplayName;
    };

    $scope.getGrantAllocation = function (grantAllocationID) { return _.find($scope.AngularViewData.AllGrantAllocationSimples, function (f) { return grantAllocationID == f.GrantAllocationID; }); };

    $scope.getColumnTotal = function () {
        return Number(_.reduce($scope.AngularModel.ProjectGrantAllocationRequests,
            function(m, x) { return Number(m) + Number(x.TotalAmount); },
            0));
    };

    $scope.getTotal = function() {
        return Number($scope.getColumnTotal());
    }

    $scope.getRowTotal = function (projectGrantAllocationRequest) {
        return Number(projectGrantAllocationRequest.TotalAmount);
    }
    
    $scope.findProjectGrantAllocationRequestRow = function(projectID, grantAllocationID) { return _.find($scope.AngularModel.ProjectGrantAllocationRequests, function(pfse) { return pfse.ProjectID == projectID && pfse.GrantAllocationID == grantAllocationID; }); }

    $scope.addRow = function()
    {
        if ($scope.GrantAllocationToAdd == null)
        {
            return;
        }
        var newProjectGrantAllocationRequest = $scope.createNewRow($scope.ProjectIDToAdd, $scope.GrantAllocationToAdd.GrantAllocationID);
        $scope.AngularModel.ProjectGrantAllocationRequests.push(newProjectGrantAllocationRequest);
        $scope.resetGrantAllocationToAdd();
    };

    $scope.createNewRow = function(projectID, grantAllocationID)
    {
        var newProjectGrantAllocationRequest = {
            ProjectID: projectID,
            GrantAllocationID: grantAllocationID,
            TotalAmount: null
        };
        return newProjectGrantAllocationRequest;
    };

    $scope.deleteRow = function(rowToDelete) { Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectGrantAllocationRequests, rowToDelete); };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetGrantAllocationToAdd();
    $scope.ProjectIDToAdd = $scope.AngularViewData.ProjectID;
});
