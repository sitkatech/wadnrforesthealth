/*-----------------------------------------------------------------------
<copyright file="EditCrosswalkValuesViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.Program
{
    public class EditCrosswalkValuesViewModel : FormViewModel, IValidatableObject
    {

        public int GisUploadSourceOrganizationID { get; set; }


        public List<CrosswalkMappingSimple> ProjectTypeSimples { get; set; }





        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditCrosswalkValuesViewModel()
        {
        }

        public EditCrosswalkValuesViewModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization)
        {
            GisUploadSourceOrganizationID = gisUploadSourceOrganization.GisUploadSourceOrganizationID;
            ProjectTypeSimples = gisUploadSourceOrganization.GisCrossWalkDefaults
                                .Where(x => x.FieldDefinitionID == Models.FieldDefinition.ProjectType.FieldDefinitionID)
                                .Select(y => new CrosswalkMappingSimple(y)).ToList();


        }

        public void UpdateModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization, Person currentPerson)
        {
            var allGisCrossWalkDefaults = HttpRequestStorage.DatabaseEntities.GisCrossWalkDefaults.Local;

            //var projectPeopleUpdated = ProjectPersonSimples.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x =>
            //    new Models.ProjectPerson(project.ProjectID, x.PersonID, x.ProjectPersonRelationshipTypeID)).ToList();

            //project.ProjectPeople.Merge(projectPeopleUpdated,
            //    allProjectPeople,
            //    (x, y) => x.ProjectID == y.ProjectID && x.PersonID == y.PersonID && x.ProjectPersonRelationshipTypeID == y.ProjectPersonRelationshipTypeID);

            var projectTypeCrosswalksUpdated = ProjectTypeSimples.Select(x =>
                new Models.GisCrossWalkDefault(gisUploadSourceOrganization.GisUploadSourceOrganizationID, Models.FieldDefinition.ProjectType.FieldDefinitionID, x.GisCrosswalkSourceValue, x.GisCrosswalkMappedValue)).ToList();

            gisUploadSourceOrganization.GisCrossWalkDefaults.Where(x => x.FieldDefinitionID == Models.FieldDefinition.ProjectType.FieldDefinitionID).ToList().Merge(projectTypeCrosswalksUpdated,
                allGisCrossWalkDefaults,
                (x, y) => x.GisUploadSourceOrganizationID == y.GisUploadSourceOrganizationID && 
                          x.FieldDefinitionID == y.FieldDefinitionID &&
                          x.GisCrossWalkSourceValue == y.GisCrossWalkSourceValue &&
                          x.GisCrossWalkMappedValue == y.GisCrossWalkMappedValue);


        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            return new List<ValidationResult>();

        }
    }


    public class CrosswalkMappingSimple
    {
        public int GisCrosswalkDefaultID { get; set; }
        public int FieldDefinitionID { get; set; }
        public string GisCrosswalkSourceValue { get; set; }
        public string GisCrosswalkMappedValue { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public CrosswalkMappingSimple()
        {
        }

        public CrosswalkMappingSimple(GisCrossWalkDefault gisCrossWalkDefault)
        {
            GisCrosswalkDefaultID = gisCrossWalkDefault.GisCrossWalkDefaultID;
            FieldDefinitionID = gisCrossWalkDefault.FieldDefinitionID;
            GisCrosswalkSourceValue = gisCrossWalkDefault.GisCrossWalkSourceValue;
            GisCrosswalkMappedValue = gisCrossWalkDefault.GisCrossWalkMappedValue;
        }
    }
}
