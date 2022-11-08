/*-----------------------------------------------------------------------
<copyright file="EditProjectController.js" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
angular.module("ProjectFirmaApp").controller("EditProjectController", function ($scope, angularModelAndViewData)
{
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

  
    $scope.resetProgramIDToAdd = function () { $scope.ProgramIDToAdd = -1 };

    $scope.resetProjectIDToAdd = function () { $scope.ProjectIDToAdd = $scope.AngularViewData.ProjectID };


    $scope.getAllUsedProgramIDs = function () {
        return _.map($scope.AngularModel.ProjectProgramSimples, function (p) { return p.ProgramID; });
    };

    $scope.filteredPrograms = function () {
        var usedProgramIDs = $scope.getAllUsedProgramIDs();
        //console.log(usedProgramIDs);
        var returnArray = _($scope.AngularViewData.AllPrograms).filter(function (f) { return !_.includes(usedProgramIDs, f.ProgramID); })
            .sortBy(["DisplayString"]).value();
        //console.log(returnArray);
        return returnArray;
    };

    $scope.getProgramName = function (projectProgram)
    {
        var programToFind = $scope.getProgram(projectProgram.ProgramID);
        var returnString = programToFind.DisplayString;
        return returnString;
    };

    $scope.getProgram = function (programID) {
        return _.find($scope.AngularViewData.AllPrograms, function (f) { return programID == f.ProgramID; });
    };




    $scope.findPojectProgramRow = function(projectID, programID) {
        return _.find($scope.AngularModel.ProjectProgramSimples,
            function(pfse) { return pfse.ProjectID == projectID && pfse.ProgramID == programID; });
    };


    $scope.addRow = function() {
        //console.log($scope.ProjectIDToAdd);
        if (($scope.ProgramIDToAdd == null) || $scope.ProgramIDToAdd == -1 || ($scope.ProjectIDToAdd == null)) {
            return;
        }
        var newProjectProgram = $scope.createNewRow($scope.ProjectIDToAdd, $scope.ProgramIDToAdd);
        $scope.AngularModel.ProjectProgramSimples.push(newProjectProgram);
        $scope.resetProgramIDToAdd();
        $scope.resetProjectIDToAdd();
    };

    $scope.createNewRow = function (projectID, programID) {
        var programIDAsInt = parseInt(programID.toString());
        var newProjectProgram = {
            ProjectID: projectID,
            ProgramID: programIDAsInt,
            ProjectProgramID: -1
    };
        var displayName = $scope.getProgramName(newProjectProgram);
        newProjectProgram.DisplayString = displayName;
        return newProjectProgram;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectProgramSimples, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetProgramIDToAdd();
    $scope.resetProjectIDToAdd();
});

