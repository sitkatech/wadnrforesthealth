/*-----------------------------------------------------------------------
<copyright file="EditProgramPeopleController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("EditProgramPeopleController", function ($scope, angularModelAndViewData)
{

    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.resetSelectedPersonID = function () { $scope.selectedPersonID = null; };

    $scope.filteredPeople = function ()
    {
        var usedPersonIDs = $scope.AngularModel.PersonIDList;
        return _($scope.AngularViewData.AllPeople).filter(function (f) { return !_.contains(usedPersonIDs, f.PersonID); }).sortBy(["LastName"]).value();
    };

    $scope.getPersonName = function (personId)
    {
        var personToFind = $scope.getPerson(personId);
        return personToFind.FullName;
    };

    $scope.getPerson = function (personId) {
        return _.find($scope.AngularViewData.AllPeople, function (f) { return personId == f.PersonID; });
    };

    $scope.addRow = function(personId) {
        var personAdding = $scope.getPerson(personId);
        if (personAdding !== undefined)
        {
            $scope.AngularModel.PersonIDList.push(personAdding.PersonID);
            $scope.resetSelectedPersonID();
        }
    };

    $scope.deleteRow = function (rowToDelete)
    {
        $scope.AngularModel.PersonIDList = _.without($scope.AngularModel.PersonIDList, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetSelectedPersonID();
});
