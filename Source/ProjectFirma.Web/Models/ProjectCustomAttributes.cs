using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public class ProjectCustomAttributes : PartialViewModel, IValidatableObject
    {
        public IList<ProjectCustomAttributeSimple> Attributes { get; set; }
        public int ProjectTypeIDForCustomAttributes { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ProjectCustomAttributes()
        {
        }

        public ProjectCustomAttributes(IProject project)
        {
            Attributes = project.ProjectCustomAttributes
                .Select(x => new ProjectCustomAttributeSimple(x))
                .ToList();
            ProjectTypeIDForCustomAttributes = project.ProjectTypeID;
        }

        public void UpdateModel(Project project, Person currentPerson)
        {
            var allProjectCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.ToList();
            var existingProjectCustomAttributes = project.ProjectCustomAttributes;
            var customAttributesToUpdate = Attributes.Where(x =>
                    x.ProjectCustomAttributeValues != null &&
                    x.ProjectCustomAttributeValues.Any(y => !string.IsNullOrWhiteSpace(y)))
                .Select(x => new ProjectCustomAttribute(project.ProjectID, x.ProjectCustomAttributeTypeID))
                .ToList();
            var existingProjectCustomAttributeValues = existingProjectCustomAttributes.SelectMany(x => x.ProjectCustomAttributeValues).ToList();
            var customAttributeValuesToUpdate = customAttributesToUpdate.Join(Attributes,
                    x => x.ProjectCustomAttributeTypeID,
                    x => x.ProjectCustomAttributeTypeID,
                    (a, b) =>
                    {
                        // Use existing attribute ID if you can, otherwise use dummy entity ID
                        var projectCustomAttributeID =
                            existingProjectCustomAttributes
                                .SingleOrDefault(x => x.ProjectCustomAttributeTypeID == a.ProjectCustomAttributeTypeID)
                                ?.ProjectCustomAttributeID ?? a.ProjectCustomAttributeID;
                        var projectCustomAttributeType = allProjectCustomAttributeTypes.Single(x =>
                            x.ProjectCustomAttributeTypeID == a.ProjectCustomAttributeTypeID);
                        return b.ProjectCustomAttributeValues
                            .Select(x =>
                            {
                                var attributeValue = projectCustomAttributeType.ProjectCustomAttributeDataType
                                    .ValueParsedForDataType(x);
                                return new ProjectCustomAttributeValue(projectCustomAttributeID, attributeValue);
                            })
                            .ToList();
                    })
                .SelectMany(x => x)
                .ToList();

            UpdateProjectCustomAttributesImpl(existingProjectCustomAttributes,
                customAttributesToUpdate,
                HttpRequestStorage.DatabaseEntities.ProjectCustomAttributes.Local);
            UpdateProjectCustomAttributeValuesImpl(
                existingProjectCustomAttributeValues,
                customAttributeValuesToUpdate,
                HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeValues.Local);
        }

        public void UpdateModel(ProjectUpdate projectUpdate, Person currentPerson)
        {
            var allProjectCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.ToList();
            var existingProjectCustomAttributes = projectUpdate.ProjectUpdateBatch.ProjectCustomAttributeUpdates;
            var customAttributesToUpdate = Attributes.Where(x =>
                    x.ProjectCustomAttributeValues != null &&
                    x.ProjectCustomAttributeValues.Any(y => !string.IsNullOrWhiteSpace(y)))
                .Select(x => new ProjectCustomAttributeUpdate(projectUpdate.ProjectUpdateBatchID, x.ProjectCustomAttributeTypeID))
                .ToList();
            var existingProjectCustomAttributeValues = existingProjectCustomAttributes.SelectMany(x => x.ProjectCustomAttributeUpdateValues).ToList();
            var customAttributeValuesToUpdate = customAttributesToUpdate.Join(Attributes,
                    x => x.ProjectCustomAttributeTypeID,
                    x => x.ProjectCustomAttributeTypeID,
                    (a, b) =>
                    {
                        // Use existing attribute ID if you can, otherwise use dummy entity ID
                        var projectCustomAttributeUpdateID =
                            existingProjectCustomAttributes
                                .SingleOrDefault(x => x.ProjectCustomAttributeTypeID == a.ProjectCustomAttributeTypeID)
                                ?.ProjectCustomAttributeUpdateID ?? a.ProjectCustomAttributeUpdateID;
                        var projectCustomAttributeType = allProjectCustomAttributeTypes.Single(x =>
                            x.ProjectCustomAttributeTypeID == a.ProjectCustomAttributeTypeID);
                        return b.ProjectCustomAttributeValues
                            .Select(x =>
                            {
                                var attributeValue = projectCustomAttributeType.ProjectCustomAttributeDataType
                                    .ValueParsedForDataType(x);
                                return new ProjectCustomAttributeUpdateValue(projectCustomAttributeUpdateID, attributeValue);
                            })
                            .ToList();
                    })
                .SelectMany(x => x)
                .ToList();

            UpdateProjectCustomAttributesImpl(existingProjectCustomAttributes,
                customAttributesToUpdate,
                HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeUpdates.Local);
            UpdateProjectCustomAttributeValuesImpl(existingProjectCustomAttributeValues,
                customAttributeValuesToUpdate,
                HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeUpdateValues.Local);
        }

        private static void UpdateProjectCustomAttributesImpl<TProjectCustomAttribute>(
            ICollection<TProjectCustomAttribute> existingProjectCustomAttributes,
            ICollection<TProjectCustomAttribute> projectCustomAttributesToUpdate,
            ObservableCollection<TProjectCustomAttribute> projectCustomAttributesInDatabase)
            where TProjectCustomAttribute : IProjectCustomAttribute
        {
            existingProjectCustomAttributes.Merge(projectCustomAttributesToUpdate,
                projectCustomAttributesInDatabase,
                (x, y) => x.ProjectCustomAttributeTypeID == y.ProjectCustomAttributeTypeID);
        }

        private void UpdateProjectCustomAttributeValuesImpl<TProjectCustomAttributeValue>(
            ICollection<TProjectCustomAttributeValue> existingProjectCustomAttributeValues,
            ICollection<TProjectCustomAttributeValue> projectCustomAttributeValuesToUpdate,
            ObservableCollection<TProjectCustomAttributeValue> projectCustomAttributeValuesInDatabase)
            where TProjectCustomAttributeValue : IProjectCustomAttributeValue
        {
            existingProjectCustomAttributeValues.Merge(projectCustomAttributeValuesToUpdate,
                projectCustomAttributeValuesInDatabase,
                (x, y) => x.IProjectCustomAttributeID == y.IProjectCustomAttributeID &&
                          x.AttributeValue == y.AttributeValue);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var warningList = new List<string>();
            return GetValidationResults(out warningList);
        }

        public IEnumerable<ValidationResult> GetValidationResults(out List<string> errorMessages)
        {
            var outList = new List<ValidationResult>();
            errorMessages = new List<string>();
            var projectCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes.ToList();
            var projectCustomAttributeTypesForThisProject = projectCustomAttributeTypes
                .Where(x => x.ApplyToAllProjectTypes || x.ProjectTypeProjectCustomAttributeTypes
                                .Select(y => y.ProjectTypeID).Contains(ProjectTypeIDForCustomAttributes)).ToList();

            var customAttributeTypeIDs = Attributes.Select(x => x.ProjectCustomAttributeTypeID).ToList();
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.ProjectCustomAttributeTypes
                .Where(x => customAttributeTypeIDs.Contains(x.ProjectCustomAttributeTypeID))
                .ToList();

            var projectCustomAttributeSimples = Attributes
                .Where(x => x.ProjectCustomAttributeValues != null && x.ProjectCustomAttributeValues.Any()).ToList();

            foreach (var attributeSimple in projectCustomAttributeSimples)
            {
                var type = customAttributeTypes.Single(x => x.ProjectCustomAttributeTypeID == attributeSimple.ProjectCustomAttributeTypeID);
                foreach (var value in attributeSimple.ProjectCustomAttributeValues)
                {
                    if (!string.IsNullOrWhiteSpace(value) && !type.ProjectCustomAttributeDataType.ValueIsCorrectDataType(value))
                    {
                        var errorMessage = $"Entered value for {type.ProjectCustomAttributeTypeName} does not match expected type " +
                                           $"({type.ProjectCustomAttributeDataType.ProjectCustomAttributeDataTypeDisplayName}).";
                        errorMessages.Add(errorMessage);
                        outList.Add(new ValidationResult(errorMessage));
                    }
                }

                if (type.IsRequired && attributeSimple.ProjectCustomAttributeValues.All(string.IsNullOrWhiteSpace))
                {
                    var warningMessage = $"Value is required for {type.ProjectCustomAttributeTypeName}.";
                    errorMessages.Add(warningMessage);
                    outList.Add(new ValidationResult(warningMessage));
                }
            }

            foreach (var projectCustomAttributeType in projectCustomAttributeTypesForThisProject)
            {
                if (projectCustomAttributeType.IsRequired && projectCustomAttributeSimples.All(x => x.ProjectCustomAttributeTypeID != projectCustomAttributeType.ProjectCustomAttributeTypeID))
                {
                    var warningMessage = $"Value is required for {projectCustomAttributeType.ProjectCustomAttributeTypeName}.";
                    errorMessages.Add(warningMessage);
                    outList.Add(new ValidationResult(warningMessage));
                }
            }

            return outList;
        }
    }
}
