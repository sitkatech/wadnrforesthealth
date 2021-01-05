using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class RelationshipType : IAuditableEntity
    {
        /// <summary>
        /// 1/5/21 TK - Creating this constant to grab the lead implementer instances when only that type is needed. If this starts getting more use we should look into other options.
        /// </summary>
        public static readonly int LeadImplementerID = 33;

        public bool CanDelete()
        {
            return !ProjectOrganizations.Any();
        }
        public string AuditDescriptionString => RelationshipTypeName;

        public static bool IsRelationshipTypeNameUnique(List<RelationshipType> existingRelationshipTypes, string relationshipTypeName, int currentRelationshipTypeID)
        {
            var relationshipType = existingRelationshipTypes.SingleOrDefault(x => x.RelationshipTypeID != currentRelationshipTypeID && String.Equals(x.RelationshipTypeName, relationshipTypeName, StringComparison.InvariantCultureIgnoreCase));
            return relationshipType == null;
        }

        public string DeleteUrl
        {
            get { return SitkaRoute<OrganizationAndRelationshipTypeController>.BuildUrlFromExpression(c => c.DeleteRelationshipType(RelationshipTypeID)); }
        }

        public bool IsAssociatedWithOrganiztionType(OrganizationType organizationType)
        {
            return OrganizationTypeRelationshipTypes.Select(x => x.OrganizationType).Contains(organizationType);
        }
    }
}