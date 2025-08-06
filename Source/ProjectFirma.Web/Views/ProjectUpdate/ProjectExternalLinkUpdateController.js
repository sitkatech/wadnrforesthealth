/*-----------------------------------------------------------------------
<copyright file="ProjectExternalLinkUpdateController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("ProjectExternalLinkUpdateController", function ($scope, angularModelAndViewData)
{
    $scope.resetExternalLinkToAdd = function()
    {
        $scope.ExternalLinkLabelToAdd = null;
        $scope.ExternalLinkUrlToAdd = "http://";
    };

    $scope.getExternalLink = function (externalLinkId) {
        return _.find($scope.AngularViewData.AllExternalLinks, function (f) { return externalLinkId == f.ExternalLinkID; });
    };

    $scope.isAddButtonDisabled = function () { return Sitka.Methods.isUndefinedNullOrEmpty($scope.ExternalLinkLabelToAdd) || Sitka.Methods.isUndefinedNullOrEmpty($scope.ExternalLinkUrlToAdd); };

    $scope.addRow = function()
    {
        if (!$scope.isAddButtonDisabled())
        {
            var newProjectExternalLink = $scope.createNewRow($scope.AngularViewData.ProjectID, $scope.ExternalLinkLabelToAdd, $scope.ExternalLinkUrlToAdd);
            $scope.AngularModel.ProjectExternalLinks.push(newProjectExternalLink);
            $scope.resetExternalLinkToAdd();
        }
        $scope.canSubmitProject();
    };

    $scope.createNewRow = function (projectId, externalLinkLabel, externalLinkUrl) {
        var newProjectExternalLink = {
            ProjectID: projectId,
            ExternalLinkLabel: externalLinkLabel,
            ExternalLinkUrl: externalLinkUrl
        };
        $scope.canSubmitProject();
        return newProjectExternalLink;
    };

    $scope.deleteRow = function (rowToDelete) {
        Sitka.Methods.removeFromJsonArray($scope.AngularModel.ProjectExternalLinks, rowToDelete);
        $scope.canSubmitProject();
    };


    $scope.canSubmitProject = function () {

        if (JSON.stringify($scope.AngularModel.ProjectExternalLinks) === $scope.DefaultFormState) {
            jQuery($scope.submitSelector).attr("disabled", false).attr("onclick", $submitButton.data("cachedOnClick"));
        }else {
            jQuery($scope.submitSelector).attr("disabled", true).attr('onclick', 'event.preventDefault()');;

        }
    }


    
    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.resetExternalLinkToAdd();

    $scope.DefaultFormState = JSON.stringify($scope.AngularModel.ProjectExternalLinks);
    $scope.submitSelector = ".submitProject";
    var $submitButton = jQuery($scope.submitSelector);
    $submitButton.data("cachedOnClick", $submitButton.attr("onclick"));

    $scope.canSubmitProject();
});

