/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocationController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("EditFundSourceAllocationController", function($scope,
    angularModelAndViewData) {

    $scope.files = [];
    //console.log('start of EditFundSourceAllocationController');

    $scope.addProgramIndexProjectCodePair = function (programIndexID, programIndexName, projectCodeID, projectCodeName) {

        //console.log("programIndexID: " + programIndexID);
        //console.log("programIndexName: " + programIndexName);
        //console.log("projectCodeID: " + projectCodeID);
        //console.log("projectCodeName: " + projectCodeName);

        //console.log("programIndexID-Selected: " + $scope.selectedProgramIndexID);
        //console.log("programIndexName-Selected: " + $scope.selectedProgramIndexName);
        //console.log("projectCodeID-Selected: " + $scope.selectedProjectCodeID);
        //console.log("projectCodeName-Selected: " + $scope.selectedProjectCodeName);

        $scope.AngularModel.ProgramIndexProjectCodeJsons.push({
            ProgramIndexID: Number(programIndexID),
            ProgramIndexName: programIndexName,
            ProjectCodeID: projectCodeID,
            ProjectCodeName: projectCodeName
        });
        $scope.resetSelectedIDsAndNames();

        //console.log("programIndexProjectCodeJsons: " + JSON.stringify($scope.AngularModel.ProgramIndexProjectCodeJsons));

    };

    $scope.removeSelectedProgramIndexProjectCodePair = function(programIndexID, projectCodeID) {
        _.remove($scope.AngularModel.ProgramIndexProjectCodeJsons,
            function(pos) {
                return pos.ProgramIndexID == programIndexID && pos.ProjectCodeID == projectCodeID;
            });
    };

    $scope.resetSelectedIDsAndNames = function() {
        $scope.selectedProgramIndexID = "";
        $scope.selectedProgramIndexName = "";
        $scope.selectedProjectCodeID = "";
        $scope.selectedProjectCodeName = "";

    };

    $scope.postBackProjectCodeHandler = function(id, name) {
        $scope.selectedProjectCodeName = name;
        $scope.selectedProjectCodeID = id;
    };

    $scope.postBackProgramIndexHandler = function(id, name) {
        $scope.selectedProgramIndexName = name;
        $scope.selectedProgramIndexID = id;      
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    //console.log('EditFundSourceAllocationController -- Angular view model:');
    //console.log($scope.AngularViewModel);

    $scope.selectedProgramIndexID = null;
    $scope.selectedProgramIndexName = null;
    $scope.selectedProjectCodeID = null;
    $scope.selectedProjectCodeName = null;




});
