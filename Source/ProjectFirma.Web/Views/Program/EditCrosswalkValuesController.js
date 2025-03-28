﻿/*-----------------------------------------------------------------------
<copyright file="EditCrosswalkValuesController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("EditCrosswalkValuesController", function($scope,
    angularModelAndViewData) {


    $scope.$watch(function() {
        //jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });



    $scope.addProjectStage = function (selectedProjectStageText, projectStageSourceValue) {

        $scope.AngularModel.ProjectStageSimples.push({
            GisCrosswalkDefaultID: -1,
            FieldDefinitionID: $scope.AngularViewData.ProjectStageFieldDefinitionID,
            GisCrosswalkSourceValue: projectStageSourceValue,
            GisCrosswalkMappedValue: selectedProjectStageText
        });
        //$scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);

    };

    $scope.removeProjectStageSimple = function (projectStageSimpleToRemove) {
        _.remove($scope.AngularModel.ProjectStageSimples,
            function (pos) {
                return pos.FieldDefinitionID === projectStageSimpleToRemove.FieldDefinitionID
                    && pos.GisCrosswalkSourceValue === projectStageSimpleToRemove.GisCrosswalkSourceValue
                    && pos.GisCrosswalkMappedValue === projectStageSimpleToRemove.GisCrosswalkMappedValue;
            });
    };



    $scope.addLeadImplementer = function (selectedLeadImplementerText, leadImplementerSourceValue) {

        $scope.AngularModel.LeadImplementerSimples.push({
            GisCrosswalkDefaultID: -1,
            FieldDefinitionID: $scope.AngularViewData.LeadImplementerFieldDefinitionID,
            GisCrosswalkSourceValue: leadImplementerSourceValue,
            GisCrosswalkMappedValue: selectedLeadImplementerText
        });
        //$scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);

    };

    $scope.removeLeadImplementerSimple = function (leadImplementerSimpleToRemove) {
        _.remove($scope.AngularModel.LeadImplementerSimples,
            function (pos) {
                return pos.FieldDefinitionID === leadImplementerSimpleToRemove.FieldDefinitionID
                    && pos.GisCrosswalkSourceValue === leadImplementerSimpleToRemove.GisCrosswalkSourceValue
                    && pos.GisCrosswalkMappedValue === leadImplementerSimpleToRemove.GisCrosswalkMappedValue;
            });
    };




    $scope.addTreatmentType = function (selectedTreatmentTypeText, treatmentTypeSourceValue) {

        $scope.AngularModel.TreatmentTypeSimples.push({
            GisCrosswalkDefaultID: -1,
            FieldDefinitionID: $scope.AngularViewData.TreatmentTypeFieldDefinitionID,
            GisCrosswalkSourceValue: treatmentTypeSourceValue,
            GisCrosswalkMappedValue: selectedTreatmentTypeText
        });
        //$scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);

    };

    $scope.removeTreatmentTypeSimple = function (treatmentTypeSimpleToRemove) {
        _.remove($scope.AngularModel.TreatmentTypeSimples,
            function (pos) {
                return pos.FieldDefinitionID === treatmentTypeSimpleToRemove.FieldDefinitionID
                    && pos.GisCrosswalkSourceValue === treatmentTypeSimpleToRemove.GisCrosswalkSourceValue
                    && pos.GisCrosswalkMappedValue === treatmentTypeSimpleToRemove.GisCrosswalkMappedValue;
            });
    };


    $scope.addTreatmentDetailedActivityType = function (selectedTreatmentDetailedActivityTypeText, treatmentDetailedActivityTypeSourceValue) {

        $scope.AngularModel.TreatmentDetailedActivityTypeSimples.push({
            GisCrosswalkDefaultID: -1,
            FieldDefinitionID: $scope.AngularViewData.TreatmentDetailedActivityTypeFieldDefinitionID,
            GisCrosswalkSourceValue: treatmentDetailedActivityTypeSourceValue,
            GisCrosswalkMappedValue: selectedTreatmentDetailedActivityTypeText
        });
        //$scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);

    };

    $scope.removeTreatmentDetailedActivityTypeSimple = function (treatmentDetailedActivityTypeSimpleToRemove) {
        _.remove($scope.AngularModel.TreatmentDetailedActivityTypeSimples,
            function (pos) {
                return pos.FieldDefinitionID === treatmentDetailedActivityTypeSimpleToRemove.FieldDefinitionID
                    && pos.GisCrosswalkSourceValue === treatmentDetailedActivityTypeSimpleToRemove.GisCrosswalkSourceValue
                    && pos.GisCrosswalkMappedValue === treatmentDetailedActivityTypeSimpleToRemove.GisCrosswalkMappedValue;
            });
    };



    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;

    $scope.selectedProjectStageText = $scope.AngularViewData.ProjectStageSelectListItems[0].Text;
    $scope.selectedLeadImplementerText = $scope.AngularViewData.LeadImplementerSelectListItems[0].Text;
    $scope.selectedTreatmentTypeText = $scope.AngularViewData.TreatmentTypeSelectListItems[0].Text;
    $scope.selectedTreatmentDetailedActivityTypeText = $scope.AngularViewData.TreatmentDetailedActivityTypeSelectListItems[0].Text;
});
