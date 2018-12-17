/*-----------------------------------------------------------------------
<copyright file="ProjectPersonController.js" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
angular.module("ProjectFirmaApp").controller("ProjectPersonController", function($scope,
    angularModelAndViewData) {
    $scope.PersonToAdd = null;

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.getAvailablePeopleForRelationshipType = function(relationshipType) {
        var peopleForRelationshipType = _.filter($scope.AngularViewData.AllPeople,
            function(person) {
                return $scope.personIsValidForRelationshipType(person, relationshipType);
            });
        if (relationshipType.RelationshipTypeCanOnlyBeRelatedOnceToAProject) {
            return peopleForRelationshipType;
        } else {
            var usedPeople = _.filter($scope.AngularModel.ProjectPersonSimples,
                function(f) {
                    return f.RelationshipTypeID == relationshipType.RelationshipTypeID;
                });
            var usedPersonIDs = _.map(usedPeople,
                function (f) {
                    return f.PersonID;
                });

            var filteredList = _.filter(peopleForRelationshipType,
                function (f) {
                    return !_.includes(usedPersonIDs, f.PersonID);
                });

            return filteredList;
        }
    };

    $scope.personIsValidForRelationshipType = function(person, relationshipType) {
        var validRelationshipTypeIDs = _.map($scope.validRelationshipTypes(person.PersonID),
            function(rt) {
                return rt.RelationshipTypeID;
            });
        return _.includes(validRelationshipTypeIDs, relationshipType.RelationshipTypeID);
    };

    $scope.chosenPeopleForRelationshipType = function(relationshipTypeID) {
        var chosenPersonSimples = _.filter($scope.AngularModel.ProjectPersonSimples,
            function(s) {
                return s.RelationshipTypeID == relationshipTypeID;
            });

        var people = _.map(chosenPersonSimples,
            function(s) {
                var person =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                        "PersonID",
                        s.PersonID);
                return person;
            });
        return people;
    };

    $scope.addProjectPersonSimple = function(personID, relationshipTypeID) {
        $scope.AngularModel.ProjectPersonSimples.push({
            PersonID: Number(personID),
            RelationshipTypeID: relationshipTypeID
        });
        $scope.resetSelectedPersonID(relationshipTypeID);
    };

    $scope.removeProjectPersonSimple = function(personID, relationshipTypeID) {
        _.remove($scope.AngularModel.ProjectPersonSimples,
            function(pos) {
                return pos.PersonID == personID && pos.RelationshipTypeID == relationshipTypeID;
            });
    };

    $scope.selectionChanged = function(personID, relationshipType) {
        // changing the dropdown selection for a one-and-only-one relationship type should update the model
        if (relationshipType.RelationshipTypeCanOnlyBeRelatedOnceToAProject) {
            // if there's already a projectPersonSimple for this relationship type, just change the PersonID
            var projectPersonSimple =
                Sitka.Methods.findElementInJsonArray($scope.AngularModel.ProjectPersonSimples,
                    "RelationshipTypeID",
                    relationshipType.RelationshipTypeID);

            if (projectPersonSimple != null) {
                projectPersonSimple.PersonID = Number(personID);
            } else {
                $scope.AngularModel.ProjectPersonSimples.push({
                    PersonID: Number(personID),
                    RelationshipTypeID: relationshipType.RelationshipTypeID
                });
            }
        } // but nothing should happen if it's a many-or-none relationship type
    };

    $scope.resetSelectedPersonID = function(relationshipTypeID) {
        $scope.selectedPersonID[relationshipTypeID] = "";
    };

    $scope.isOptionSelected = function(person, relationshipType) {
        if (!relationshipType.RelationshipTypeCanOnlyBeRelatedOnceToAProject) {
            return false;
        }
        return _.any($scope.AngularModel.ProjectPersonSimples,
            function(pos) {
                return pos.PersonID == person.PersonID &&
                    pos.RelationshipTypeID == relationshipType.RelationshipTypeID;
            });
    };

    $scope.dropdownDefaultOption = function(relationshipType) {
        if (relationshipType.RelationshipTypeCanOnlyBeRelatedOnceToAProject) {
            return "Select the " + relationshipType.RelationshipTypeName;
        } else {
            return "Add a " + relationshipType.RelationshipTypeName;
        }
    };

    $scope.validRelationshipTypes = function(personID) {
        var person =
            Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                "PersonID",
                personID);

        var valid = person == null ? [] : person.ValidRelationshipTypeSimples;
        return valid;
    };

    $scope.getSelectedPrimaryContactPerson = function (relationshipType) {

        var selectedPrimaryContactPersonID =
            Sitka.Methods.findElementInJsonArray($scope.AngularModel.ProjectPersonSimples,
                "RelationshipTypeID",
                relationshipType.RelationshipTypeID).PersonID;

        var selectedPrimaryContactPerson =
            Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                "PersonID",
                selectedPrimaryContactPersonID);

        return selectedPrimaryContactPerson;
    }

    $scope.primaryContactPersonHasNoPrimaryContact = function(relationshipType) {
        return $scope.getSelectedPrimaryContactPerson(relationshipType).PrimaryContactPersonID == null;
    }

    $scope.primaryContactPerson = function (relationshipType) {
        return $scope.getSelectedPrimaryContactPerson(relationshipType);
    }

    $scope.primaryContactPersonPersonDisplayName = function (relationshipType) {
        if (relationshipType != null) {
            return $scope.getSelectedPrimaryContactPerson(relationshipType).PrimaryContactPersonDisplayName;
        }

        return "nobody";
    }

    $scope.isPersonSelected = function (personID) {
        var primaryContactPersonId = $scope.AngularModel.PrimaryContactPersonID;

        return primaryContactPersonId === personID;
    };

    $scope.primaryContactPersonChange = function (personID) {
        $scope.AngularModel.PrimaryContactPersonID = personID === "null" ? null : parseInt(personID);
    }

    $scope.AngularModel = angularModelAndViewData.AngularModel;
    $scope.AngularViewData = angularModelAndViewData.AngularViewData;
    $scope.selectedPersonID = {};
});
