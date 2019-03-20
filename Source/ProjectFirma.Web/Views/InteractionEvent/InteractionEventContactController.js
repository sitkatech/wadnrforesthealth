/*-----------------------------------------------------------------------
<copyright file="IAteractionEventContactController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("InteractionEventContactController", function ($scope, angularModelAndViewData) {
    $scope.PersonToAdd = null;

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });


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

    $scope.chosenContactsForInteractionEvent = function() {
        var chosenContacts = $scope.AngularModel.InteractionEventContacts;

        var contacts = _.map(chosenContacts,
            function(s) {
                var contact =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllContacts,
                        "PersonID",
                        s.PersonID);
                return contact;
            });
        return contacts;
    };

    $scope.addInteractionEventContact = function(personID) {
        $scope.AngularModel.InteractionEventContacts.push({
            PersonID: Number(personID)
        });
        $scope.resetSelectedContactID();
    };

    $scope.removeInteractionEventContact = function(personID) {
        _.remove($scope.AngularModel.InteractionEventContacts,
            function(pos) {
                return pos.PersonID == personID;
            });
    };

    $scope.resetSelectedContactID = function () {
        $scope.selectedContactID = "";
    };


    $scope.isOptionSelected = function(contact) {

        return _.any($scope.AngularModel.InteractionEventContacts,
            function(pos) {
                return pos.PersonID == contact.PersonID;
            });
    };


    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    console.log($scope.AngularViewData);
    $scope.selectedContactID = "";
});
