/*-----------------------------------------------------------------------
<copyright file="ProjectPersonController.js" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
angular.module("ProjectFirmaApp").controller("ProjectPersonController", function($scope,
    angularModelAndViewData) {
    $scope.PersonToAdd = null;

    $scope.$watch(function() {
        jQuery(".selectpicker").selectpicker("refresh");

        // so that unsavedChanges.js knows to check if the form has changed.
        jQuery("form").trigger("input");
    });

    $scope.getAvailablePeopleForProjectPersonRelationshipType = function(projectPersonProjectPersonRelationshipType) {
        var peopleForProjectPersonRelationshipType = $scope.AngularViewData.AllPeople;
        if (projectPersonProjectPersonRelationshipType.IsRequired || projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID === $scope.AngularViewData.PrimaryContactProjectPersonRelationshipType.ProjectPersonRelationshipTypeID) {
            return peopleForProjectPersonRelationshipType;
        } else {
            var usedPeople = _.filter($scope.AngularModel.ProjectPersonSimples,
                function(f) {
                    return f.ProjectPersonRelationshipTypeID == projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID;
                });
            var usedPersonIDs = _.map(usedPeople,
                function (f) {
                    return f.PersonID;
                });

            var filteredList = _.filter(peopleForProjectPersonRelationshipType,
                function (f) {
                    return !_.includes(usedPersonIDs, f.PersonID);
                });

            return filteredList;
        }
    };

    $scope.personIsValidForProjectPersonRelationshipType = function(person, projectPersonProjectPersonRelationshipType) {
        var validProjectPersonRelationshipTypeIDs = _.map($scope.validProjectPersonRelationshipTypes(person.PersonID),
            function(rt) {
                return rt.ProjectPersonRelationshipTypeID;
            });
        return _.includes(validProjectPersonRelationshipTypeIDs, projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID);
    };

    $scope.chosenPeopleForProjectPersonRelationshipType = function(projectPersonProjectPersonRelationshipTypeID) {
        var chosenPersonSimples = _.filter($scope.AngularModel.ProjectPersonSimples,
            function(s) {
                return s.ProjectPersonRelationshipTypeID === projectPersonProjectPersonRelationshipTypeID;
            });

        //console.log('chosen people');
        //console.log(chosenPersonSimples);

        var people = _.map(chosenPersonSimples,
            function(s) {
                var person =
                    Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                        "PersonID",
                        s.PersonID);
                return person;
            });

        //console.log('people to return');
        //console.log(people);

        return people;
    };

    $scope.checkForFieldImport = function (relationshipTypeID) {

        if ($scope.AngularViewData.IsPrivateLandownerImported &&
            $scope.AngularViewData.PrivateLandownerRelationshipID === relationshipTypeID) {
            return true;
        }

        return false;
    }

    $scope.addProjectPersonSimple = function(personID, projectPersonProjectPersonRelationshipTypeID) {
        $scope.AngularModel.ProjectPersonSimples.push({
            PersonID: Number(personID),
            ProjectPersonRelationshipTypeID: projectPersonProjectPersonRelationshipTypeID
        });
        $scope.resetSelectedPersonID(projectPersonProjectPersonRelationshipTypeID);
    };

    $scope.removeProjectPersonSimple = function(personID, projectPersonProjectPersonRelationshipTypeID) {
        _.remove($scope.AngularModel.ProjectPersonSimples,
            function(pos) {
                return pos.PersonID == personID && pos.ProjectPersonRelationshipTypeID === projectPersonProjectPersonRelationshipTypeID;
            });
    };

    $scope.selectionChanged = function(personID, projectPersonProjectPersonRelationshipType) {
        // changing the dropdown selection for a one-and-only-one(IsRequired) relationship type should update the model and of the primary contact relationship type
        if (projectPersonProjectPersonRelationshipType.IsRequired || projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID === $scope.AngularViewData.PrimaryContactProjectPersonRelationshipType.ProjectPersonRelationshipTypeID) {
            // if there's already a projectPersonSimple for this relationship type, just change the PersonID
            var projectPersonSimple =
                Sitka.Methods.findElementInJsonArray($scope.AngularModel.ProjectPersonSimples,
                    "ProjectPersonRelationshipTypeID",
                    projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID);

            if (projectPersonSimple != null) {
                projectPersonSimple.PersonID = Number(personID);
            } else {
                $scope.AngularModel.ProjectPersonSimples.push({
                    PersonID: Number(personID),
                    ProjectPersonRelationshipTypeID: projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID
                });
            }
        } // but nothing should happen if it's a many-or-none relationship type
    };

    $scope.resetSelectedPersonID = function(projectPersonProjectPersonRelationshipTypeID) {
        $scope.selectedPersonID[projectPersonProjectPersonRelationshipTypeID] = "";
    };

    $scope.isOptionSelected = function(person, projectPersonProjectPersonRelationshipType) {
        if (projectPersonProjectPersonRelationshipType.IsRequired || projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID === $scope.AngularViewData.PrimaryContactProjectPersonRelationshipType.ProjectPersonRelationshipTypeID) {
            return _.any($scope.AngularModel.ProjectPersonSimples,
                function (pos) {
                    return pos.PersonID == person.PersonID &&
                        pos.ProjectPersonRelationshipTypeID == projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID;
                });
            
        }
        return false;
    };

    $scope.dropdownDefaultOption = function(projectPersonProjectPersonRelationshipType) {
        if (projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeCanOnlyBeRelatedOnceToAProject) {
            return "Select the " + projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName;
        } else {
            return "Add a " + projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeDisplayName;
        }
    };

    $scope.validProjectPersonRelationshipTypes = function(personID) {
        var person =
            Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                "PersonID",
                personID);

        var valid = person == null ? [] : person.ValidProjectPersonRelationshipTypeSimples;
        return valid;
    };

    $scope.getSelectedPrimaryContactPerson = function (projectPersonProjectPersonRelationshipType) {

        var selectedPrimaryContactPersonID =
            Sitka.Methods.findElementInJsonArray($scope.AngularModel.ProjectPersonSimples,
                "ProjectPersonRelationshipTypeID",
                projectPersonProjectPersonRelationshipType.ProjectPersonRelationshipTypeID).PersonID;

        var selectedPrimaryContactPerson =
            Sitka.Methods.findElementInJsonArray($scope.AngularViewData.AllPeople,
                "PersonID",
                selectedPrimaryContactPersonID);

        return selectedPrimaryContactPerson;
    }

    $scope.primaryContactPersonHasNoPrimaryContact = function(projectPersonProjectPersonRelationshipType) {
        return $scope.getSelectedPrimaryContactPerson(projectPersonProjectPersonRelationshipType).PrimaryContactPersonID == null;
    }

    $scope.primaryContactPerson = function (projectPersonProjectPersonRelationshipType) {
        return $scope.getSelectedPrimaryContactPerson(projectPersonProjectPersonRelationshipType);
    }

    $scope.primaryContactPersonPersonDisplayName = function (projectPersonProjectPersonRelationshipType) {
        if (projectPersonProjectPersonRelationshipType != null) {
            return $scope.getSelectedPrimaryContactPerson(projectPersonProjectPersonRelationshipType).PrimaryContactPersonDisplayName;
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
    //console.log($scope.AngularViewData);
    $scope.selectedPersonID = {};
});
