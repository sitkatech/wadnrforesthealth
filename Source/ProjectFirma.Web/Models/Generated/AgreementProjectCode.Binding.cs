//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AgreementProjectCode]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[AgreementProjectCode] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[AgreementProjectCode]")]
    public partial class AgreementProjectCode : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected AgreementProjectCode()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public AgreementProjectCode(int agreementProjectCodeID, int agreementID, int projectCodeID) : this()
        {
            this.AgreementProjectCodeID = agreementProjectCodeID;
            this.AgreementID = agreementID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public AgreementProjectCode(int agreementID, int projectCodeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.AgreementID = agreementID;
            this.ProjectCodeID = projectCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public AgreementProjectCode(Agreement agreement, ProjectCode projectCode) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.AgreementProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.AgreementID = agreement.AgreementID;
            this.Agreement = agreement;
            agreement.AgreementProjectCodes.Add(this);
            this.ProjectCodeID = projectCode.ProjectCodeID;
            this.ProjectCode = projectCode;
            projectCode.AgreementProjectCodes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static AgreementProjectCode CreateNewBlank(Agreement agreement, ProjectCode projectCode)
        {
            return new AgreementProjectCode(agreement, projectCode);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(AgreementProjectCode).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.AgreementProjectCodes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int AgreementProjectCodeID { get; set; }
        public int AgreementID { get; set; }
        public int ProjectCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return AgreementProjectCodeID; } set { AgreementProjectCodeID = value; } }

        public virtual Agreement Agreement { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }

        public static class FieldLengths
        {

        }
    }
}