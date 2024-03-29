using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Program
{
    public abstract class ProgramViewData : FirmaViewData
    {
        public Models.Program Program { get; }
        public Models.GisUploadSourceOrganization GisUploadSourceOrganization { get; }
        public string EditProgramUrl { get; set; }
        public string DeleteDocumentUrl { get; set; }
        public string DeleteExampleGdbDocumentUrl { get; set; }
        public bool UserHasProgramManagePermissions { get; set; }
        public bool UserHasProgramEditMappingsPermissions { get; set; }

        public string BackToProgramsText { get; set; }

        public string ProgramsListUrl { get; set; }

        public bool ShowDownload { get; set; }
        public bool ShowExampleGdbDownload { get; set; }

        protected ProgramViewData(Person currentPerson, Models.Program program) : base(currentPerson, null)
        {
            Program = program;
            HtmlPageTitle = program.ProgramName;
            EntityName = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabel()}";
            EditProgramUrl = program.GetEditUrl();
            DeleteDocumentUrl = program.GetDeleteDocumentUrl();
            DeleteExampleGdbDocumentUrl = program.GetDeleteExampleDocumentUrl();
            UserHasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);
            UserHasProgramEditMappingsPermissions = new ProgramEditMappingsFeature().HasPermission(currentPerson, program.GisUploadSourceOrganization).HasPermission;
            BackToProgramsText = $"Back to all {Models.FieldDefinition.Program.GetFieldDefinitionLabelPluralized()}";
            ProgramsListUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(c => c.Index());
            GisUploadSourceOrganization = program.GisUploadSourceOrganization;
            ShowDownload = program.ProgramFileResource != null;
            ShowExampleGdbDownload = program.ProgramExampleGeospatialUploadFileResource != null;
        }

        public string ProjectTypeDefaultName()
        {
            return GisUploadSourceOrganization?.ProjectTypeDefaultName;
        }

        public string TreatmentTypeDefaultName()
        {
            return GisUploadSourceOrganization?.TreatmentTypeDefaultName;
        }

        public string ImportIsFlattenedString()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ImportIsFlattened.ToYesNo(null);
            }
            return null;
        }

        public bool ImportIsFlattened()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ImportIsFlattened ?? false;
            }
            return false;
        }

        public string AdjustProjectTypeBasedOnTreatmentTypes()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.AdjustProjectTypeBasedOnTreatmentTypes.ToYesNo();
            }
            return null;
        }

        public string DataDeriveProjectStage()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.DataDeriveProjectStage.ToYesNo();
            }
            return null;
        }

        public string ImportAsDetailedLocationInsteadOfTreatments()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments.ToYesNo();
            }
            return null;
        }

        public string ApplyCompletedDateToProject()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ApplyCompletedDateToProject.ToYesNo();
            }
            return null;
        }

        public string ApplyStartDateToProject()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ApplyStartDateToProject.ToYesNo();
            }
            return null;
        }
        public string ApplyStartDateToTreatments()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ApplyStartDateToTreatments.ToYesNo();
            }
            return null;
        }

        public string ApplyEndDateToTreatments()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ApplyEndDateToTreatments.ToYesNo();
            }
            return null;
        }

        public string ImportAsDetailedLocationInAdditionToTreatments()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ImportAsDetailedLocationInAdditionToTreatments.ToYesNo();
            }
            return null;
        }


        public string ProjectDescriptionDefaultText()
        {
            if (GisUploadSourceOrganization != null)
            {
                return GisUploadSourceOrganization.ProjectDescriptionDefaultText;
            }
            return null;
        }

        public string ProjectStageDefault()
        {
            if (GisUploadSourceOrganization != null)
            {
                
                var projectStageDefaultID =  GisUploadSourceOrganization.ProjectStageDefaultID;
                var projectStageDisplayName = ProjectStage.AllLookupDictionary.ContainsKey(projectStageDefaultID)
                    ? ProjectStage.AllLookupDictionary[projectStageDefaultID].ProjectStageDisplayName
                    : null;
                return projectStageDisplayName;
            }
            return null;
        }

        public string DefaultLeadImplementerOrganization()
        {
            if (GisUploadSourceOrganization != null)
            {

                var defaultLeadImplementerOrganizationID = GisUploadSourceOrganization.DefaultLeadImplementerOrganizationID;
                var organization =
                    HttpRequestStorage.DatabaseEntities.Organizations.SingleOrDefault(x =>
                        x.OrganizationID == defaultLeadImplementerOrganizationID);
                return organization != null ? organization.DisplayName : null;
            }
            return null;
        }

        public string RelationshipTypeForDefaultOrganization()
        {
            if (GisUploadSourceOrganization != null)
            {

                var relationshipTypeForDefaultOrganizationID = GisUploadSourceOrganization.RelationshipTypeForDefaultOrganizationID;
                var relationshipType =
                    HttpRequestStorage.DatabaseEntities.RelationshipTypes.SingleOrDefault(x =>
                        x.RelationshipTypeID == relationshipTypeForDefaultOrganizationID);
                return relationshipType != null ? relationshipType.RelationshipTypeName : null;
            }
            return null;
        }
    }
}