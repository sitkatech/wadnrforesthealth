/*-----------------------------------------------------------------------
<copyright file="Project.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Models;
using LtInfo.Common.Views;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public partial class Project : IAuditableEntity, IProject
    {
        public const int MaxLengthForProjectDescription = 700;

        public int EntityID => ProjectID;

        public string AuditDescriptionString => ProjectName;

        public string DisplayName => ProjectName;

        public HtmlString DisplayNameAsUrl => UrlTemplate.MakeHrefString(this.GetDetailUrl(), DisplayName);
        public HtmlString DisplayNameAsUrlBlankTarget => UrlTemplate.MakeHrefStringBlankTarget(this.GetDetailUrl(), DisplayName);

        public string PercentageMatchFormatted => PercentageMatch.HasValue ? $"{PercentageMatch}%" : String.Empty;

        public static bool IsProjectNameUnique(IEnumerable<Project> projects, string projectName, int? currentProjectID)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                return false;
            }

            var project = projects.SingleOrDefault(x =>
                x.ProjectID != (currentProjectID ?? 0) && string.Equals(x.ProjectName.Trim(), projectName.Trim(),
                    StringComparison.InvariantCultureIgnoreCase));
            return project == null;
        }

        public const string ImportedFieldWarningMessage = "This field is imported for the program. To edit data, visit the system of record.";

        public bool IsProjectNameImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.ProjectName);
        }

        public bool IsProjectIdentifierImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.ProjectIdentifier);
        }

        public bool IsCompletionDateImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.CompletionDate);
        }

        public bool IsLeadImplementerOrganizationImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.LeadImplementerOrganization);
        }

        public bool IsPrivateLandownerImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.Landowner);
        }

        /// <summary>
        /// Field is Planned Date in the DB. but we refer to it as the Project Initiation Date throughout the site.
        /// The import mappings are setup to use the PlannedDate field definition
        /// </summary>
        /// <returns></returns>
        public bool IsProjectInitiationDateImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.PlannedDate);
        }

        public bool IsProjectStageImported()
        {
            var programsToCheck = ProjectPrograms.Select(x => x.Program);
            return CheckIfFieldIsImported(programsToCheck, FieldDefinition.ProjectStage);
        }

        public static bool CheckIfFieldIsImported(IEnumerable<Program> programsToCheck, FieldDefinition fieldDefinition)
        {
            foreach (var program in programsToCheck)
            {
                if (program.GisUploadSourceOrganization.GisDefaultMappings.Any(x => x.FieldDefinitionID == fieldDefinition.FieldDefinitionID && !string.IsNullOrEmpty(x.GisDefaultMappingColumnName)))
                {
                    return true;
                }
            }

            return false;
        }

        public decimal TotalPlannedFootprintAcres
        {
            get
            {
                return Math.Round(GrantAllocationAwardLandownerCostShareLineItems.Where(x => x.LandownerCostShareLineItemStatus == LandownerCostShareLineItemStatus.Planned)
                    .Select(x => x.Treatment?.TreatmentFootprintAcres ?? 0).Sum(), 2);
            }
        }

        public decimal TotalCompletedFootprintAcres
        {
            get
            {
                return Math.Round(GrantAllocationAwardLandownerCostShareLineItems.Where(x => x.LandownerCostShareLineItemStatus == LandownerCostShareLineItemStatus.Completed)
                    .Select(x => x.Treatment?.TreatmentFootprintAcres ?? 0).Sum(), 2);
            }
        }

        public decimal TotalTreatedAcres
        {
            get
            {
                return Math.Round(Treatments.Select(x => x.TreatmentTreatedAcres ?? 0).Sum(), 2);
            }
        }

        public decimal TotalFootprintAcres
        {
            get
            {
                return Math.Round(Treatments.Select(x => x.TreatmentFootprintAcres).Sum(), 2);
            }
        }

        public Organization GetPrimaryContactOrganization()
        {
            return ProjectOrganizations.SingleOrDefault(x => x.RelationshipType.IsPrimaryContact)?.Organization;
        }

        public FileResource GetPrimaryContactOrganizationLogo()
        {
            return GetPrimaryContactOrganization()?.LogoFileResource;
        }

        public Organization GetCanStewardProjectsOrganization()
        {
            return ProjectOrganizations.SingleOrDefault(x => x.RelationshipType.CanStewardProjects)?.Organization;
        }

        public TaxonomyBranch GetCanStewardProjectsTaxonomyBranch()
        {
            return ProjectType.TaxonomyBranch;
        }

        public List<DNRUplandRegion> GetCanStewardProjectsRegions()
        {
            return ProjectRegions.Select(x => x.DNRUplandRegion).ToList();
        }

        public IEnumerable<Organization> GetOrganizationsToReportInAccomplishments()
        {
            if (MultiTenantHelpers.GetRelationshipTypeToReportInAccomplishmentsDashboard() == null)
            {
                // Default is Funding Organizations
                var organizations = ProjectGrantAllocationExpenditures.Select(x => x.GrantAllocation.BottommostOrganization)
                    .Union(ProjectGrantAllocationRequests
                        .Select(x => x.GrantAllocation.BottommostOrganization))
                    .Distinct(new HavePrimaryKeyComparer<Organization>());
                return organizations;
            }
            else
            {
                return ProjectOrganizations.Where(x => x.RelationshipType.ReportInAccomplishmentsDashboard)
                    .Select(x => x.Organization).ToList();
            }
        }

        public Person GetPrimaryContact()
        {
            var primaryContact = this.ProjectPeople.SingleOrDefault(pp => pp.ProjectPersonRelationshipTypeID == ProjectPersonRelationshipType.PrimaryContact.ProjectPersonRelationshipTypeID);
            if (primaryContact != null)
            {
                return primaryContact.Person;
            }

            return GetPrimaryContactOrganization()?.PrimaryContactPerson;
        }

        public List<Person> GetPrivateLandowners()
        {
            var privateLandowners = this.ProjectPeople.Where(pp => pp.ProjectPersonRelationshipTypeID == ProjectPersonRelationshipType.PrivateLandowner.ProjectPersonRelationshipTypeID).Select(x => x.Person);
            return privateLandowners.ToList();
        }

        //public decimal? UnfundedNeed()
        //{
        //    return EstimatedTotalCost - GetSecuredFunding();
        //}

        public decimal? GetTotalFunding()
        {
            return ProjectGrantAllocationRequests.Any()
                ? (decimal?) ProjectGrantAllocationRequests.Sum(x => x.TotalAmount.GetValueOrDefault())
                : null;
        }

        public decimal GetTotalPaymentAmount()
        {
            var invoices = this.InvoicePaymentRequests.SelectMany(x => x.Invoices);
            return invoices.Sum(x => x.PaymentAmount ?? 0);
        }

        public decimal GetTotalMatchAmount()
        {
            var invoices = this.InvoicePaymentRequests.SelectMany(x => x.Invoices);
            return invoices.Sum(x => x.MatchAmount ?? 0);
        }

        //public decimal? GetUnsecuredFunding()
        //{
        //    return ProjectGrantAllocationRequests.Any()
        //        ? (decimal?) ProjectGrantAllocationRequests.Sum(x => x.TotalAmount.GetValueOrDefault())
        //        : null;
        //}

        public decimal? GetNoGrantAllocationIdentifiedAmount()
        {
            decimal? totalFunding = GetTotalFunding() == null ? null : GetTotalFunding();

            var noGrantAllocationIdentifiedAmount = (EstimatedTotalCost ?? 0) - (totalFunding ?? 0);
            if (noGrantAllocationIdentifiedAmount >= 0)
            {
                return noGrantAllocationIdentifiedAmount;
            }

            return null;
        }


        public decimal? TotalExpenditures
        {
            get
            {
                return ProjectGrantAllocationExpenditures.Any()
                    ? ProjectGrantAllocationExpenditures.Sum(x => x.ExpenditureAmount)
                    : (decimal?) null;
            }
        }

        public bool HasProjectLocationPoint => ProjectLocationPoint != null;
        public bool HasProjectLocationDetail => AllDetailedLocationsToGeoJsonFeatureCollection().Features.Any();

        private bool _hasCheckedProjectUpdateHistories;
        private List<ProjectUpdateHistory> _projectUpdateHistories;

        public List<ProjectUpdateHistory> ProjectUpdateHistories
        {
            get
            {
                if (_hasCheckedProjectUpdateHistories)
                {
                    return _projectUpdateHistories;
                }

                ProjectUpdateHistories = ProjectUpdateBatches.SelectMany(x => x.ProjectUpdateHistories).ToList();
                return _projectUpdateHistories;
            }
            set
            {
                _projectUpdateHistories = value;
                _hasCheckedProjectUpdateHistories = true;
            }
        }

        public ProjectUpdateBatch GetLatestNotApprovedUpdateBatch()
        {
            return ProjectUpdateBatches.SingleOrDefault(x => x.ProjectUpdateState != ProjectUpdateState.Approved);
        }

        public ProjectUpdateBatch GetLatestApprovedUpdateBatch()
        {
            var projectUpdateBatches = ProjectUpdateBatches
                .Where(x => x.ProjectUpdateState == ProjectUpdateState.Approved).ToList();

            return projectUpdateBatches.Any() ? projectUpdateBatches.MaxBy(x => x.LastUpdateDate) : null;
        }

        public ProjectUpdateBatch GetLatestUpdateBatch()
        {
            var projectUpdateBatches = ProjectUpdateBatches.ToList();

            return projectUpdateBatches.Any() ? projectUpdateBatches.MaxBy(x => x.LastUpdateDate) : null;
        }

        public bool IsUpdateMandatory
        {
            get
            {
                if (IsPendingProject())
                {
                    return false;
                }

                if (!IsUpdatableViaProjectUpdateProcess)
                {
                    return false;
                }

                var latestUpdateBatch = GetLatestUpdateBatch();

                if (latestUpdateBatch == null)
                {
                    return true;
                }

                if (!latestUpdateBatch.IsApproved)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsUpdatableViaProjectUpdateProcess => !IsPendingProject();

        public ProjectUpdateState GetLatestUpdateState()
        {
            if (!ProjectUpdateBatches.Any())
                return null;

            if (ProjectUpdateBatches.Count(x => x.ProjectUpdateState != ProjectUpdateState.Approved) > 1)
                throw new Exception(FirmaValidationMessages.MoreThanOneProjectUpdateInProgress);

            return ProjectUpdateBatches.MaxBy(x => x.LastUpdateDate).ProjectUpdateState;
        }

        public DateTime? GetLatestUpdateSubmittalDate()
        {
            var notNullSubmittalDates =
                ProjectUpdateBatches.Select(x => x.LatestSubmittalDate).Where(x => x.HasValue).ToList();
            return notNullSubmittalDates.Any() ? notNullSubmittalDates.Max() : null;
        }

        private string _projectLocationStateProvince;
        private bool _hasSetProjectLocationStateProvince;

        public string ProjectLocationStateProvince
        {
            get
            {
                if (_hasSetProjectLocationStateProvince)
                {
                    return _projectLocationStateProvince;
                }

                SetProjectLocationStateProvince(HttpRequestStorage.DatabaseEntities.StateProvinces.ToList());
                return _projectLocationStateProvince;
            }
            set
            {
                _projectLocationStateProvince = value;
                _hasSetProjectLocationStateProvince = true;
            }
        }

        public void SetProjectLocationStateProvince(IEnumerable<StateProvince> stateProvinces)
        {
            if (HasProjectLocationPoint)
            {
                var stateProvince = stateProvinces.FirstOrDefault(x =>
                    x.StateProvinceFeature != null && x.StateProvinceFeature.Intersects(ProjectLocationPoint));
                ProjectLocationStateProvince = stateProvince != null
                    ? stateProvince.StateProvinceAbbreviation
                    : ViewUtilities.NaString;
            }
            else
            {
                ProjectLocationStateProvince = ViewUtilities.NaString;
            }
        }

        public bool IsProjectRegionValid()
        {
            return ProjectRegions.Any() || !string.IsNullOrWhiteSpace(NoRegionsExplanation);
        }

        public bool IsProjectCountiesValid()
        {
            return ProjectCounties.Any() || !string.IsNullOrWhiteSpace(NoCountiesExplanation);
        }

        public bool IsProjectPriorityLandscapeValid()
        {
            return ProjectPriorityLandscapes.Any() || !string.IsNullOrWhiteSpace(NoPriorityLandscapesExplanation);
        }

        public bool IsMyProject(Person person)
        {
            return !person.IsAnonymousUser && (IsPersonThePrimaryContact(person) ||
                                               person.Organization.IsMyProject(this) ||
                                               person.PersonStewardOrganizations.Any(x =>
                                                   x.Organization.IsMyProject(this)));
        }

        public bool IsPersonThePrimaryContact(Person person)
        {
            if (person == null)
            {
                return false;
            }

            var primaryContactPerson = GetPrimaryContact();
            return person.PersonID == primaryContactPerson?.PersonID;
        }

        public bool IsEditableToThisPerson(Person person)
        {
            return IsMyProject(person) || new ProjectApproveFeature().HasPermission(person, this).HasPermission;
        }

        public List<PerformanceMeasureReportedValue> GetReportedPerformanceMeasures()
        {
            var reportedPerformanceMeasures = GetNonVirtualPerformanceMeasureReportedValues();
            return reportedPerformanceMeasures.OrderByDescending(pma => pma.CalendarYear)
                .ThenBy(pma => pma.PerformanceMeasureID).ToList();
        }

        public List<PerformanceMeasureReportedValue> GetNonVirtualPerformanceMeasureReportedValues()
        {
            var performanceMeasureReportedValues = PerformanceMeasureActuals.Select(x => x.PerformanceMeasure)
                .Distinct(new HavePrimaryKeyComparer<PerformanceMeasure>())
                .SelectMany(x => x.GetReportedPerformanceMeasureValues(this)).ToList();
            return performanceMeasureReportedValues.OrderByDescending(pma => pma.CalendarYear)
                .ThenBy(pma => pma.PerformanceMeasureID).ToList();
        }

        public FeatureCollection SimpleLocationToGeoJsonFeatureCollection(bool addProjectProperties)
        {
            var featureCollection = new FeatureCollection();
            var allProjectGrantAllocationExpenditures =
                HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
            var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

            if (ProjectLocationSimpleType == ProjectLocationSimpleType.PointOnMap && HasProjectLocationPoint)
            {
                featureCollection.Features.Add(
                    MakePointFeatureWithRelevantProperties(ProjectLocationPoint, addProjectProperties, true, FieldDefinition.Organization.GetFieldDefinitionLabel(), FieldDefinition.Organization.GetFieldDefinitionLabelPluralized(), projectGrantAllocationExpenditureDict));
            }

            return featureCollection;
        }

        public static string CreateNewFhtProjectNumber()
        {
            var currentCounter = 1;
            var lastProjectCreatedThisYear = HttpRequestStorage.DatabaseEntities.Projects.Where(p => p.FhtProjectNumber.Contains(DateTime.Now.Year.ToString())).OrderByDescending(p => p.FhtProjectNumber).ToList().FirstOrDefault(p => p.FhtProjectNumber.StartsWith($"FHT-{DateTime.Now.Year}"));
            if (lastProjectCreatedThisYear != null)
            {
                var splitFhtProjectNumber = lastProjectCreatedThisYear.FhtProjectNumber.Split('-');
                Int32.TryParse(splitFhtProjectNumber[2], out currentCounter);
                currentCounter++;

            }

            return $"FHT-{DateTime.Now.Year}-{currentCounter:00000}";

        }

        public IEnumerable<IProjectLocation> GetProjectLocationDetails()
        {
            return ProjectLocations.ToList();
        }

        public DbGeometry GetDefaultBoundingBox()
        {
            return DefaultBoundingBox;
        }

        public IEnumerable<DNRUplandRegion> GetProjectRegions()
        {
            return ProjectRegions.Select(x => x.DNRUplandRegion);
        }
        public IEnumerable<PriorityLandscape> GetProjectPriorityLandscapes()
        {
            return ProjectPriorityLandscapes.Select(x => x.PriorityLandscape);
        }

        public IEnumerable<County> GetProjectCounties()
        {
            return ProjectCounties.Select(x => x.County);
        }

        public void AutoAssignProjectPriorityLandscapesAndDnrUplandRegionsAndCounties()
        {
            var detailedProjectLocations = ProjectLocations.Select(x => x.ProjectLocationGeometry).ToList();
            var projectHasDetailedLocations = HasProjectLocationDetail;
            var detailedProjectLocationsAggregated = HasProjectLocationDetail ? detailedProjectLocations.Aggregate((x, y) => x.Union(y)) : null;
            var detailedProjectLocationsAggregatedMadeValid = HasProjectLocationDetail ? (detailedProjectLocationsAggregated.IsValid ? detailedProjectLocationsAggregated : detailedProjectLocationsAggregated.ToSqlGeometry().MakeValid().ToDbGeometryWithCoordinateSystem()) : null;

            var projectLocationPoint = this.ProjectLocationPoint;
            var projectLocationPointExists = this.HasProjectLocationPoint;

            var updatedProjectPriorityLandscapes = HttpRequestStorage.DatabaseEntities.PriorityLandscapes
                .Where(x => (projectHasDetailedLocations && x.PriorityLandscapeLocation.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.PriorityLandscapeLocation.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectPriorityLandscape(ProjectID, x.PriorityLandscapeID))
                .ToList();

            if (!updatedProjectPriorityLandscapes.Any())
            {
                NoPriorityLandscapesExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any Priority Landscape.";
            }
            else
            {
                NoPriorityLandscapesExplanation = null;
            }


            ProjectPriorityLandscapes.Merge(updatedProjectPriorityLandscapes, HttpRequestStorage.DatabaseEntities.ProjectPriorityLandscapes.Local, (x, y) => x.ProjectID == y.ProjectID && x.PriorityLandscapeID == y.PriorityLandscapeID);

            var updatedProjectRegions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions
                .Where(x => (projectHasDetailedLocations && x.DNRUplandRegionLocation.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.DNRUplandRegionLocation.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectRegion(ProjectID, x.DNRUplandRegionID))
                .ToList();

            if (!updatedProjectRegions.Any())
            {
                NoRegionsExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any DNR Upland Region.";
            }
            else
            {
                NoRegionsExplanation = null;
            }

            ProjectRegions.Merge(updatedProjectRegions, HttpRequestStorage.DatabaseEntities.ProjectRegions.Local, (x, y) => x.ProjectID == y.ProjectID && x.DNRUplandRegionID == y.DNRUplandRegionID);

            var updatedProjectCounties= HttpRequestStorage.DatabaseEntities.Counties
                .Where(x => (projectHasDetailedLocations && x.CountyFeature.Intersects(detailedProjectLocationsAggregatedMadeValid)) || (projectLocationPointExists && x.CountyFeature.Intersects(projectLocationPoint)))
                .ToList()
                .Select(x => new ProjectCounty(ProjectID, x.CountyID))
                .ToList();

            if (!updatedProjectCounties.Any())
            {
                NoCountiesExplanation =
                    "Neither the simple location nor the detailed location on this project intersects with any County.";
            }
            else
            {
                NoCountiesExplanation = null;
            }

            ProjectCounties.Merge(updatedProjectCounties, HttpRequestStorage.DatabaseEntities.ProjectCounties.Local, (x, y) => x.ProjectID == y.ProjectID && x.CountyID == y.CountyID);
        }

        public FeatureCollection AllDetailedLocationsToGeoJsonFeatureCollection()
        {
            return ProjectLocations.ToGeoJsonFeatureCollection();
        }
        public FeatureCollection DetailedLocationsByTypeToGeoJsonFeatureCollection(
            ProjectLocationType projectLocationType)
        {
            return ProjectLocations.Where(x => x.ProjectLocationTypeID == projectLocationType.ProjectLocationTypeID)
                .ToGeoJsonFeatureCollection();
        }

        public static FeatureCollection MappedPointsToGeoJsonFeatureCollection(List<Project> projects,
            bool addProjectProperties, bool useDetailedCustomPopup)
        {
            var featureCollection = new FeatureCollection();
            var filteredProjectList = projects.Where(x1 => x1.HasProjectLocationPoint)
                .Where(x => x.ProjectStage.ShouldShowOnMap()).ToList();
            var organizationFieldDefinitionLabelSingle = FieldDefinition.Organization.GetFieldDefinitionLabel();
            var organizationFieldDefinitionLabelPluralized = FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();

            var allProjectGrantAllocationExpenditures =
                HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
            var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

            featureCollection.Features.AddRange(filteredProjectList.Select(project =>
            {
                
                return project.MakePointFeatureWithRelevantProperties(project.ProjectLocationPoint,
                    addProjectProperties,
                    useDetailedCustomPopup, organizationFieldDefinitionLabelSingle,
                    organizationFieldDefinitionLabelPluralized,
                    projectGrantAllocationExpenditureDict);
            }).ToList());
            return featureCollection;
        }

        public static LayerGeoJson ProjectDetailedLocationsToGeoJsonFeatureCollection(List<Project> projects)
        {

            var projectIDs = projects.Select(x => x.ProjectID).ToList();

            var projectLocations = HttpRequestStorage.DatabaseEntities.ProjectLocations
                .Where(x => projectIDs.Contains(x.ProjectID)).ToList();


            var detailedLocationsByTypeGeoJsonFeatureCollection = projectLocations.ToGeoJsonFeatureCollectionWithPopupUrl();

            var layerGeoJson =
                new LayerGeoJson($"{Models.FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()} - Detail",
                    detailedLocationsByTypeGeoJsonFeatureCollection, "#ed5e0b", (decimal) 0.3,
                    LayerInitialVisibility.Hide);
            layerGeoJson.LayerIconImageLocation = "/Content/leaflet/images/washington_location_detailed.png";


            return layerGeoJson;
        }

        public static LayerGeoJson ProjectTreatmentAreasToGeoJsonFeatureCollection(List<Project> projects)
        {
            Check.Require(projects.Any(), "We need to have projects to create a GeoJsonFeatureCollection");

            var projectIDs = projects.Select(x => x.ProjectID).ToList();


            var projectTreatments = HttpRequestStorage.DatabaseEntities.Treatments
                .Where(x => projectIDs.Contains(x.ProjectID))
                .Include(x => x.ProjectLocation)
                .ToList();

            if (!projectTreatments.Any())
            {
                return null;
            }


            var detailedLocationsByTypeGeoJsonFeatureCollection = projectTreatments.ToGeoJsonFeatureCollectionWithPopupUrl();
            var layerName = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Treatment Areas";
            var layerGeoJson =
                new LayerGeoJson(layerName,
                    detailedLocationsByTypeGeoJsonFeatureCollection, "#355e3b", (decimal)0.3,
                    LayerInitialVisibility.Hide);
            layerGeoJson.LayerIconImageLocation = "/Content/leaflet/images/washington_project_treatment.png";


            return layerGeoJson;
        }


    public Feature MakePointFeatureWithRelevantProperties(DbGeometry projectLocationPoint
            , bool addProjectProperties
            , bool useDetailedCustomPopup
            , string organizationFieldDefinitionLabelSingle
            , string organizationFieldDefinitionLabelPluralized
            , Dictionary<int, List<ProjectGrantAllocationExpenditure>> projectGrantAllocationExpenditureDict)
        {
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(projectLocationPoint);
            feature.Properties.Add("TaxonomyTrunkID",
                ProjectType.TaxonomyBranch.TaxonomyTrunkID.ToString(CultureInfo.InvariantCulture));
            feature.Properties.Add("ProjectStageID", ProjectStageID.ToString(CultureInfo.InvariantCulture));
            if (ProjectStage != null)
            {
                feature.Properties.Add("ProjectStageColor", ProjectStage.ProjectStageColor);
            }

            feature.Properties.Add("Info", DisplayName);
            if (addProjectProperties)
            {
                feature.Properties.Add("ProjectID", ProjectID.ToString(CultureInfo.InvariantCulture));
                feature.Properties.Add("TaxonomyBranchID",
                    ProjectType.TaxonomyBranchID.ToString(CultureInfo.InvariantCulture));
                feature.Properties.Add("ProjectTypeID", ProjectTypeID.ToString(CultureInfo.InvariantCulture));
                feature.Properties.Add("ClassificationID",
                    string.Join(",", ProjectClassifications.Select(x => x.ClassificationID)));
                var associatedOrganizations = this.GetAssociatedOrganizations(organizationFieldDefinitionLabelSingle, organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict);
                foreach (var relationshipTypeGroup in associatedOrganizations.GroupBy(x => x.RelationshipType.RelationshipTypeName))
                {
                    feature.Properties.Add($"{relationshipTypeGroup.First().RelationshipType.RelationshipTypeName}ID",
                        relationshipTypeGroup.Select(z => z.Organization.OrganizationID).ToList());
                }

                if (useDetailedCustomPopup)
                {
                    feature.Properties.Add("PopupUrl", this.GetProjectMapPopupUrl());
                }
                else
                {
                    feature.Properties.Add("PopupUrl", this.GetProjectSimpleMapPopupUrl());
                }
                feature.Properties.Add("ProgramID", string.Join(",", ProjectPrograms.Select(x => x.ProgramID)));
                feature.Properties.Add("LeadImplementerID", this.ProjectOrganizations.SingleOrDefault(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID)?.OrganizationID.ToString(CultureInfo.InvariantCulture) ?? (-1).ToString(CultureInfo.InvariantCulture));

            }

            return feature;
        }

        public string Duration
        {
            get
            {
                if (GetImplementationStartYear() == GetCompletionYear() && GetImplementationStartYear().HasValue)
                {
                    return GetImplementationStartYear().Value.ToString(CultureInfo.InvariantCulture);
                }

                return
                    $"{GetImplementationStartYear()?.ToString(CultureInfo.InvariantCulture) ?? "?"} - {GetCompletionYear()?.ToString(CultureInfo.InvariantCulture) ?? "?"}";
            }
        }

        /// <summary>
        /// Returns a commma-separated list of organizations that doesn't include the lead implementer or the funders and only includes the relationships that are configured to show on the fact sheet
        /// </summary>
        public string ProjectOrganizationNamesForFactSheet
        {
            get
            {

                var allProjectGrantAllocationExpenditures =
                    HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
                var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

                // get the list of funders so we can exclude any that have other project associations
                var organizationFieldDefinitionLabelSingle = FieldDefinition.Organization.GetFieldDefinitionLabel();
                var organizationFieldDefinitionLabelPluralized = FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();
                var fundingOrganizations = this.GetFundingOrganizations(organizationFieldDefinitionLabelSingle, organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict).Select(x => x.Organization.OrganizationID);
                // Don't use GetAssociatedOrganizations because we don't care about funders for this list.
                var associatedOrganizations = ProjectOrganizations.Where(x =>
                    x.RelationshipType.ShowOnFactSheet && !fundingOrganizations.Contains(x.OrganizationID)).ToList();
                associatedOrganizations.RemoveAll(x =>
                    x.OrganizationID == GetPrimaryContactOrganization()?.OrganizationID);
                var organizationNames = associatedOrganizations
                    .OrderByDescending(x => x.RelationshipType.IsPrimaryContact)
                    .ThenByDescending(x => x.RelationshipType.CanStewardProjects)
                    .ThenBy(x => x.Organization.OrganizationName).Select(x => x.Organization.OrganizationName)
                    .Distinct().ToList();
                return organizationNames.Any() ? string.Join(", ", organizationNames) : string.Empty;
            }
        }

        public string FundingOrganizationNamesForFactSheet
        {
            get
            {
                var allProjectGrantAllocationExpenditures =
                    HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
                var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

                var organizationFieldDefinitionLabelPluralized = FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();
                var organizationFieldDefinitionLabelSingle = FieldDefinition.Organization.GetFieldDefinitionLabel();
                return string.Join(", ",
                    this.GetFundingOrganizations(organizationFieldDefinitionLabelSingle, organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict).OrderBy(x => x.Organization.OrganizationName)
                        .Select(x => x.Organization.OrganizationName));
            }
        }

        public string AssociatedOrganizationNames(Organization organization, string organizationFieldDefinitionLabelSingle, string organizationFieldDefinitionLabelPluralized, Dictionary<int, List<ProjectGrantAllocationExpenditure>> projectGrantAllocationExpenditureDict)
        {

            
            var projectOrganizationAssociationNames = new List<string>();
            this.GetAssociatedOrganizations(organizationFieldDefinitionLabelSingle, organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict).Where(x => x.Organization == organization).ForEach(x =>
                projectOrganizationAssociationNames.Add(x.RelationshipType.RelationshipTypeName));
            return string.Join(", ", projectOrganizationAssociationNames);
        }

        public ProjectImage KeyPhoto
        {
            get { return ProjectImages.SingleOrDefault(x => x.IsKeyPhoto); }
        }

        private DateTime _lastUpdateDate;
        private bool _hasCheckedLastUpdateDate;

        public DateTime LastUpdateDate
        {
            get
            {
                if (!_hasCheckedLastUpdateDate)
                {
                    var auditLogEntriesForProject = HttpRequestStorage.DatabaseEntities.AuditLogs.GetAuditLogEntriesForProject(this);
                    LastUpdateDate = auditLogEntriesForProject.Any() ? auditLogEntriesForProject.Max(x => x.AuditLogDate) : _lastUpdateDate;
                }

                return _lastUpdateDate;
            }
            set
            {
                _lastUpdateDate = value;
                _hasCheckedLastUpdateDate = true;
            }
        }

        public double? ProjectLocationPointLatitude =>
            HasProjectLocationPoint ? ProjectLocationPoint.YCoordinate : null;

        public double? ProjectLocationPointLongitude =>
            HasProjectLocationPoint ? ProjectLocationPoint.XCoordinate : null;

        public FancyTreeNode ToFancyTreeNode()
        {
            var fancyTreeNode = new FancyTreeNode(
                $"{UrlTemplate.MakeHrefString(this.GetFactSheetUrl(), ProjectName, ProjectName)}",
                FancyTreeNodeKey.ToString(), false)
            {
                ThemeColor = ProjectType.TaxonomyBranch.TaxonomyTrunk.ThemeColor,
                MapUrl = null
            };
            return fancyTreeNode;
        }

        /// <summary>
        /// Note this will do a deep delete of this project image, meaning it will remove it from a ProjectImageUpdate if it is tied to that
        /// </summary>
        /// <param name="projectImages"></param>
        public static void DeleteProjectImages(ICollection<ProjectImage> projectImages)
        {
            var projectImageFileResourceIDsToDelete = projectImages.Select(x => x.FileResourceID).ToList();
            var projectImageIDsToDelete = projectImages.Select(x => x.ProjectImageID).ToList();
            foreach (var projectImageUpdate in HttpRequestStorage.DatabaseEntities.ProjectImageUpdates.Where(x => x.ProjectImageID.HasValue && projectImageIDsToDelete.Contains(x.ProjectImageID.Value)).ToList())
            {
                projectImageUpdate.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            foreach (var projectImage in projectImages)
            {
                projectImage.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(projectImageFileResourceIDsToDelete);
        }

        public IEnumerable<Person> GetProjectStewards()
        {
            return GetCanStewardProjectsOrganization()?.People
                       .Where(y => y.HasRole(Role.ProjectSteward))
                       .ToList() ?? new List<Person>();
        }

        public ICollection<IEntityClassification> ProjectClassificationsForMap =>
            new List<IEntityClassification>(ProjectClassifications);

        public ICollection<IEntityProgram> ProjectProgramsForMap =>
            new List<IEntityProgram>(ProjectPrograms);

        public int FancyTreeNodeKey => ProjectID;

        IEnumerable<IProjectCustomAttribute> IProject.ProjectCustomAttributes
        {
            get => ProjectCustomAttributes;
            set => ProjectCustomAttributes = (ICollection<ProjectCustomAttribute>) value;
        }

        public List<GooglePieChartSlice> GetExpenditureGooglePieChartSlices()
        {
            var sortOrder = 0;
            var googlePieChartSlices = new List<GooglePieChartSlice>();
            var expendituresDictionary = ProjectGrantAllocationExpenditures.Where(x => x.ExpenditureAmount > 0)
                .GroupBy(x => x.GrantAllocation, new HavePrimaryKeyComparer<GrantAllocation>())
                .ToDictionary(x => x.Key, x => x.Sum(y => y.ExpenditureAmount));

            var groupedGrantAllocations = expendituresDictionary.Keys.GroupBy(x => x.BottommostOrganization.OrganizationType,
                new HavePrimaryKeyComparer<OrganizationType>());
            foreach (var groupedGrantAllocation in groupedGrantAllocations)
            {
                var sectorColor = ColorTranslator.FromHtml(groupedGrantAllocation.Key.LegendColor);
                var sectorColorHsl = new HslColor(sectorColor.R, sectorColor.G, sectorColor.B);

                groupedGrantAllocation.OrderBy(x => x.GrantAllocationName)
                    .ForEach((grantAllocation, index) =>
                    {
                        var luminosity = 100.0 * (groupedGrantAllocation.Count() - index - 1) /
                                         groupedGrantAllocation.Count() + 120;
                        var color = ColorTranslator.ToHtml(new HslColor(sectorColorHsl.Hue, sectorColorHsl.Saturation,
                            luminosity));

                        googlePieChartSlices.Add(new GooglePieChartSlice(grantAllocation.FixedLengthDisplayName,
                            Convert.ToDouble(expendituresDictionary[grantAllocation]), sortOrder++, color));
                    });
            }

            return googlePieChartSlices;
        }

        public List<GooglePieChartSlice> GetGrantAllocationRequestGooglePieChartSlices()
        {
            var sortOrder = 0;
            var googlePieChartSlices = new List<GooglePieChartSlice>();

            var totalAmountsDictionary = ProjectGrantAllocationRequests.Where(x => x.TotalAmount > 0)
                .GroupBy(x => x.GrantAllocation, new HavePrimaryKeyComparer<GrantAllocation>())
                .ToDictionary(x => x.Key, x => x.Sum(y => y.TotalAmount));

            var securedColorHsl = new {hue = 96.0, sat = 60.0};
            var unsecuredColorHsl = new {hue = 33.3, sat = 240.0};

            totalAmountsDictionary.OrderBy(x => x.Key.GrantAllocationName).ForEach(
                (grantAllocationDictionaryItem, index) =>
                {
                    var grantAllocation = grantAllocationDictionaryItem.Key;
                    var fundingAmount = grantAllocationDictionaryItem.Value;

                    var luminosity = 100.0 * (totalAmountsDictionary.Count - index - 1) /
                                     totalAmountsDictionary.Count + 120;
                    var color = ColorTranslator.ToHtml(new HslColor(securedColorHsl.hue, securedColorHsl.sat,
                        luminosity));

                    googlePieChartSlices.Add(new GooglePieChartSlice(
                        "Secured Funding: " + grantAllocation.FixedLengthDisplayName, Convert.ToDouble(fundingAmount),
                        sortOrder++, color));
                });

            return googlePieChartSlices;
        }

        public List<GooglePieChartSlice> GetRequestAmountGooglePieChartSlices()
        {
            var requestAmountsDictionary = GetGrantAllocationRequestGooglePieChartSlices();
            var noGrantAllocationIdentifiedAmount =
                Convert.ToDouble(EstimatedTotalCost ?? 0) - requestAmountsDictionary.Sum(x => x.Value);
            if (noGrantAllocationIdentifiedAmount > 0)
            {
                var sortOrder = requestAmountsDictionary.Any() ? requestAmountsDictionary.Max(x => x.SortOrder) + 1 : 0;
                requestAmountsDictionary.Add(new GooglePieChartSlice("No Grant Allocation Identified",
                    noGrantAllocationIdentifiedAmount, sortOrder, "#dbdbdb"));
            }

            return requestAmountsDictionary;
        }

        public bool IsActiveProject()
        {
            return !IsProposal() && ProjectApprovalStatus == ProjectApprovalStatus.Approved;
        }

        public bool IsProposal()
        {
            return ProjectStage == ProjectStage.Proposed;
        }

        public bool IsActiveProposal()
        {
            return IsProposal() && ProjectApprovalStatus == ProjectApprovalStatus.PendingApproval;
        }

        public bool IsPendingProject()
        {
            return !IsProposal() && ProjectApprovalStatus != ProjectApprovalStatus.Approved;
        }

        public bool IsRejected()
        {
            return ProjectApprovalStatus == ProjectApprovalStatus.Rejected;
        }

        public bool IsForwardLookingFactSheetRelevant()
        {
            return ProjectStage.ForwardLookingFactSheetProjectStages.Contains(ProjectStage);
        }

        public bool IsBackwardLookingFactSheetRelevant()
        {
            return !IsForwardLookingFactSheetRelevant();
        }

        public bool IsExpectedFundingRelevant()
        {
            // todo: Always relevant for pending projects, otherwise relevant for every stage except terminated/completed
            return true;
        }

        public bool AreReportedPerformanceMeasuresRelevant()
        {
            return ProjectStage != ProjectStage.Proposed && ProjectStage != ProjectStage.Planned;
        }

        public bool AreReportedExpendituresRelevant()
        {
            return ProjectStage != ProjectStage.Proposed;
        }

        public bool IsInLandownerAssistanceProgram => ProjectPrograms.Any(x => x.ProgramID == Program.LandownerAssistanceProgramID);

        public static List<ProjectSectionSimple> GetApplicableProposalWizardSections(Project project, bool ignoreStatus)
        {
            return ProjectWorkflowSectionGrouping.All.SelectMany(x => x.GetProjectCreateSections(project, ignoreStatus))
                .OrderBy(x => x.ProjectWorkflowSectionGrouping.SortOrder).ThenBy(x => x.SortOrder).ToList();
        }

        public List<ProjectCustomAttributeType> GetProjectCustomAttributeTypesForThisProject()
        {
            return ProjectType.GetProjectCustomAttributeTypesForThisProjectType();
        }

        public string GetPlannedDate()
        {
            return PlannedDate?.ToShortDateString();
        }

        public string GetApplicationDate()
        {
            return SubmissionDate?.ToShortDateString();
        }

        public string GetDecisionDate()
        {
            return ApprovalDate?.ToShortDateString();
        }

        public string GetCompletionDateFormatted()
        {
            return CompletionDate?.ToShortDateString();
        }

        public string GetExpirationDateFormatted()
        {
            return ExpirationDate?.ToShortDateString();
        }

        public int? GetImplementationStartYear()
        {
            return PlannedDate?.Year;
        }

        public int? GetCompletionYear()
        {
            return CompletionDate?.Year;
        }

        // read-only Helper accessors
        public List<ProgramIndex> ProgramIndices => this.ProjectGrantAllocationRequests.SelectMany(aga => aga.GrantAllocation.GrantAllocationProgramIndexProjectCodes).Select(pi => pi.ProgramIndex).Where(pi => pi != null).ToList();
        public List<ProjectCode> ProjectCodes => this.ProjectGrantAllocationRequests.SelectMany(aga => aga.GrantAllocation.GrantAllocationProgramIndexProjectCodes).Select(pc => pc.ProjectCode).Where(pc => pc != null).ToList();


 

        public HtmlString ProgramListDisplayHelper(Dictionary<int, List<Models.Program>> programsByProject, bool showDefaultsAsWell)
        {
            var programs = new List<Models.Program>();
            if (programsByProject.ContainsKey(ProjectID))
            {
                programs = programsByProject[ProjectID];
            }
            var listOfStrings = new List<string>();
            foreach (var program in programs)
            {
                if (!program.IsDefaultProgramForImportOnly || showDefaultsAsWell)
                {
                    var stringReturn = UrlTemplate.MakeHrefString(
                        program.GetDetailUrl(),
                        program.DisplayName).ToString();
                    listOfStrings.Add(stringReturn);
                }
            }

            var returnList = string.Join(", ", listOfStrings);
            if (listOfStrings.Any())
            {
                return new HtmlString(returnList);
            }

            return new HtmlString(string.Empty);
        }

    }
}