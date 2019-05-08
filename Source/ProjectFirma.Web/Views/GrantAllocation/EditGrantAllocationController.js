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

    $scope.addProgramIndexProjectCodePair = function (programIndexID, programIndexName, projectCodeID, projectCodeName) {

        console.log("programIndexID: " + programIndexID);
        console.log("programIndexName: " + programIndexName);
        console.log("projectCodeID: " + projectCodeID);
        console.log("projectCodeName: " + projectCodeName);

        console.log("programIndexID-Selected: " + $scope.selectedProgramIndexID);
        console.log("programIndexName-Selected: " + $scope.selectedProgramIndexName);
        console.log("projectCodeID-Selected: " + $scope.selectedProjectCodeID);
        console.log("projectCodeName-Selected: " + $scope.selectedProjectCodeName);

        $scope.AngularModel.ProgramIndexProjectCodeJsons.push({
            ProgramIndexID: Number(programIndexID),
            ProgramIndexName: programIndexName,
            ProjectCodeID: projectCodeID,
            ProjectCodeName: projectCodeName
        });
        $scope.resetSelectedIDsAndNames();

        console.log("after selected fields have been wiped:")
        console.log("programIndexID-Selected: " + $scope.selectedProgramIndexID);
        console.log("programIndexName-Selected: " + $scope.selectedProgramIndexName);
        console.log("projectCodeID-Selected: " + $scope.selectedProjectCodeID);
        console.log("projectCodeName-Selected: " + $scope.selectedProjectCodeName);

        console.log("programIndexProjectCodeJsons: " + JSON.stringify($scope.AngularModel.ProgramIndexProjectCodeJsons));

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
        //set ProjectCodeID value and display text in input
        //jQuery("#@ProjectCodeSearchResults.ProjectCodeSearchInputTextboxID").val(ui.item.label);
        //jQuery("#SelectedProjectCodeID").val(ui.item.actualValue);
        $scope.selectedProjectCodeName = name;
        $scope.selectedProjectCodeID = id;
    };

    $scope.postBackProgramIndexHandler = function(id, name) {
        //set ProgramIndexID value and display text in input 
        //jQuery("#@ProgramIndexSearchResults.ProgramIndexSearchInputTextboxID").val(ui.item.label);
        //jQuery("#SelectedProgramIndexID").val(ui.item.actualValue);
        $scope.selectedProgramIndexName = name;
        $scope.selectedProgramIndexID = id;      
    };

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    console.log('EditGrantAllocationController -- Angular view model:');
    console.log($scope.AngularViewModel);

    $scope.selectedProgramIndexID = null;
    $scope.selectedProgramIndexName = null;
    $scope.selectedProjectCodeID = null;
    $scope.selectedProjectCodeName = null;




});
