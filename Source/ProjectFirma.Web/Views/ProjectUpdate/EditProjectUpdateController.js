/*-----------------------------------------------------------------------
<copyright file="EditProjectUpdateController.js" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
angular.module("ProjectFirmaApp").controller("EditProjectUpdateController", function ($scope, angularModelAndViewData) {
    
    $scope.$watch(function () {
        jQuery(".selectpicker").selectpicker("refresh");
    });

  
    $scope.resetProgramIDToAdd = function () { $scope.ProgramIDToAdd = -1 };

    $scope.resetProjectUpdateIDToAdd = function () { $scope.ProjectUpdateIDToAdd = $scope.AngularViewData.ProjectUpdateID };


    $scope.getAllUsedProgramIDs = function () {
        return _.map($scope.AngularModel.ProjectUpdateProgramSimples, function (p) { return p.ProgramID; });
    };

    $scope.filteredPrograms = function () {
        var usedProgramIDs = $scope.getAllUsedProgramIDs();
        console.log($scope.AngularViewData);
        var returnArray = _($scope.AngularViewData.AllPrograms).filter(function (f) { return !_.includes(usedProgramIDs, f.ProgramID); })
            .sortBy(["DisplayString"]).value();
        console.log(returnArray);
        return returnArray;
    };

    $scope.getProgramName = function (projectUpdateProgram)
    {
        var programToFind = $scope.getProgram(projectUpdateProgram.ProgramID);
        var returnString = programToFind.DisplayString;
        return returnString;
    };

    $scope.getProgram = function (programID) {
        return _.find($scope.AngularViewData.AllPrograms, function (f) { return programID == f.ProgramID; });
    };




    $scope.findPojectProgramRow = function(ProjectUpdateID, programID) {
        return _.find($scope.AngularModel.ProjectUpdateProgramSimples,
            function(pfse) { return pfse.ProjectUpdateID == ProjectUpdateID && pfse.ProgramID == programID; });
    };


    $scope.selectionChanged = function (programID) {
        console.log(programID);
    };

    $scope.addRow = function() {
        console.log($scope.ProjectUpdateIDToAdd);
        if (($scope.ProgramIDToAdd == null) || $scope.ProgramIDToAdd == -1 || ($scope.ProjectUpdateIDToAdd == null)) {
            return;
        }
        var newProjectUpdateProgram = $scope.createNewRow($scope.ProjectUpdateIDToAdd, $scope.ProgramIDToAdd);
        $scope.AngularModel.ProjectUpdateProgramSimples.push(newProjectUpdateProgram);
        $scope.resetProgramIDToAdd();
        $scope.resetProjectUpdateIDToAdd();
    };

    $scope.createNewRow = function (ProjectUpdateID, programID) {
        console.log("Program ID ",programID);
        var programIDAsInt = parseInt(programID.toString());
        var newProjectUpdateProgram = {
            ProjectUpdateID: ProjectUpdateID,
            ProgramID: programIDAsInt,
            ProjectUpdateProgramID: -1
    };
        var displayName = $scope.getProgramName(newProjectUpdateProgram);
        newProjectUpdateProgram.DisplayString = displayName;
        return newProjectUpdateProgram;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectUpdateProgramSimples, rowToDelete);
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetProgramIDToAdd();
    $scope.resetProjectUpdateIDToAdd();
});

