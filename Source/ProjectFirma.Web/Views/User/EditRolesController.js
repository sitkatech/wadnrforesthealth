/*-----------------------------------------------------------------------
<copyright file="EditRolesController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("EditRolesController", function($scope,
    angularModelAndViewData) {

    //console.log('start of EditRolesController');

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.getAvailableRolesForPerson = function() {
        var allRoles = $scope.AngularViewData.AllRoles;
        var usedRoles = $scope.AngularModel.RoleSimples;
        var usedRoleIds = _.map(usedRoles,
            function(f) {
                return f.RoleID;
            });

        var filteredRoleSelectList = _.filter(allRoles,
            function(f) {
                return !_.includes(usedRoleIds, f.RoleID)
            });

        return filteredRoleSelectList;
    };

    $scope.addRole = function (roleID) {
        var allRoles = _.filter($scope.AngularViewData.AllRoles,
            function(f) {
                return f.RoleID === Number(roleID);
            });
        var role = allRoles[0];

        if (role !== undefined)
        {
            $scope.AngularModel.RoleSimples.push({
                RoleID: Number(role.RoleID),
                RoleDisplayName: role.RoleDisplayName
            });
        }
        $scope.resetSelectedRoleID();
    };

    $scope.removeSelectedRole = function(roleID) {
        _.remove($scope.AngularModel.RoleSimples,
            function(pos) {
                return pos.RoleID == roleID;
            });
    };

    $scope.resetSelectedRoleID = function() {
        $scope.selectedRoleID = "";
    };

    $scope.isOptionSelected = function (role)
    {
        return false;
    };

    $scope.dropdownDefaultOption = function () {
        return "Add a Role";
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    //console.log('EditRolesController -- Angular view data:');
    //console.log($scope.AngularViewData);

    $scope.selectedRoleID = null;
});
