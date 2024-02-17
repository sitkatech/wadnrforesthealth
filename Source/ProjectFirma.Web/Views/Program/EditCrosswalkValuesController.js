/*-----------------------------------------------------------------------
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


    $scope.addProjectType = function (selectedProjectTypeText, projectTypeSourceValue) {

        $scope.AngularModel.ProjectTypeSimples.push({
            GisCrosswalkDefaultID: -1,
            FieldDefinitionID: $scope.AngularViewData.ProjectTypeFieldDefinitionID,
            GisCrosswalkSourceValue: projectTypeSourceValue,
            GisCrosswalkMappedValue: selectedProjectTypeText
        });
        //$scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);
        console.log("projectTypeSimples:" + $scope.AngularModel.ProjectTypeSimples);
    };

    //removeProjectTypeSimple(projectTypeSimple)
    $scope.removeProjectTypeSimple = function (projectTypeSimpleToRemove) {
        _.remove($scope.AngularModel.ProjectTypeSimples,
            function (pos) {
                return pos.FieldDefinitionID === projectTypeSimpleToRemove.FieldDefinitionID
                    && pos.GisCrosswalkSourceValue === projectTypeSimpleToRemove.GisCrosswalkSourceValue
                    && pos.GisCrosswalkMappedValue === projectTypeSimpleToRemove.GisCrosswalkMappedValue;
            });
    };


    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    //console.log($scope.AngularViewData);
    $scope.selectedProjectTypeID = null;
});
