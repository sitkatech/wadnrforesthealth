/*-----------------------------------------------------------------------
<copyright file="InteractionEventProjectController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("InteractionEventController", function ($scope, angularModelAndViewData) {
    $scope.ProjectToAdd = null;
    $scope.PersonToAdd = null;

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });


    $scope.getAvailableProjectsForInteractionEvents = function () {
        var projectsForInteractionEvents = $scope.AngularViewData.AllProjects;

        var usedProjects = $scope.AngularModel.InteractionEventProjects;
        var usedProjectIDs = _.map(usedProjects,
            function (f) {
                return f.ProjectID;
            });

        var filteredList = _.filter(projectsForInteractionEvents,
            function (f) {
                return !_.includes(usedProjectIDs, f.ProjectID);
            });

        return filteredList;
    };

    $scope.chosenProjectsForInteractionEvent = function() {
        var chosenProjects = $scope.AngularModel.InteractionEventProjects;

        var projects = _.map(chosenProjects,
            function(s) {
                var project =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllProjects,
                        "ProjectID",
                        s.ProjectID);
                return project;
            });
        return projects;
    };

    $scope.addInteractionEventProject = function(projectID) {
        $scope.AngularModel.InteractionEventProjects.push({
            ProjectID: Number(projectID)
        });
        $scope.resetSelectedProjectID();
    };

    $scope.removeInteractionEventProject = function(projectID) {
        _.remove($scope.AngularModel.InteractionEventProjects,
            function(pos) {
                return pos.ProjectID == projectID;
            });
    };

    $scope.resetSelectedProjectID = function () {
        $scope.selectedProjectID = "";
    };


    $scope.isOptionSelectedProject = function(project) {

        return _.any($scope.AngularModel.InteractionEventProjects,
            function(pos) {
                return pos.ProjectID == project.ProjectID;
            });
    };

    $scope.getAvailableContactsForInteractionEvents = function () {
        var contactsForInteractionEvents = $scope.AngularViewData.AllContacts;

        var usedPeople = $scope.AngularModel.InteractionEventContacts;
        var usedPersonIDs = _.map(usedPeople,
            function (f) {
                return f.PersonID;
            });

        var filteredList = _.filter(contactsForInteractionEvents,
            function (f) {
                return !_.includes(usedPersonIDs, f.PersonID);
            });

        return filteredList;
    };

    $scope.chosenContactsForInteractionEvent = function () {
        var chosenContacts = $scope.AngularModel.InteractionEventContacts;

        var contacts = _.map(chosenContacts,
            function (s) {
                var contact =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllContacts,
                        "PersonID",
                        s.PersonID);
                return contact;
            });
        return contacts;
    };

    $scope.addInteractionEventContact = function (personID) {
        $scope.AngularModel.InteractionEventContacts.push({
            PersonID: Number(personID)
        });
        $scope.resetSelectedContactID();
    };

    $scope.removeInteractionEventContact = function (personID) {
        _.remove($scope.AngularModel.InteractionEventContacts,
            function (pos) {
                return pos.PersonID == personID;
            });
    };

    $scope.resetSelectedContactID = function () {
        $scope.selectedContactID = "";
    };


    $scope.isOptionSelectedContact = function (contact) {

        return _.any($scope.AngularModel.InteractionEventContacts,
            function (pos) {
                return pos.PersonID == contact.PersonID;
            });
    };



    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    console.log($scope.AngularViewData);
    $scope.selectedProjectID = "";
});
