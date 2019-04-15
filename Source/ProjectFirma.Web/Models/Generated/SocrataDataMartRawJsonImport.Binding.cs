//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SocrataDataMartRawJsonImport]
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
    // Table [dbo].[SocrataDataMartRawJsonImport] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[SocrataDataMartRawJsonImport]")]
    public partial class SocrataDataMartRawJsonImport : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SocrataDataMartRawJsonImport()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SocrataDataMartRawJsonImport(int socrataDataMartRawJsonImportID, DateTime createDate, int socrataDataMartRawJsonImportTableTypeID, string rawJsonString) : this()
        {
            this.SocrataDataMartRawJsonImportID = socrataDataMartRawJsonImportID;
            this.CreateDate = createDate;
            this.SocrataDataMartRawJsonImportTableTypeID = socrataDataMartRawJsonImportTableTypeID;
            this.RawJsonString = rawJsonString;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SocrataDataMartRawJsonImport(DateTime createDate, int socrataDataMartRawJsonImportTableTypeID, string rawJsonString) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SocrataDataMartRawJsonImportID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CreateDate = createDate;
            this.SocrataDataMartRawJsonImportTableTypeID = socrataDataMartRawJsonImportTableTypeID;
            this.RawJsonString = rawJsonString;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public SocrataDataMartRawJsonImport(DateTime createDate, SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType, string rawJsonString) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SocrataDataMartRawJsonImportID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.CreateDate = createDate;
            this.SocrataDataMartRawJsonImportTableTypeID = socrataDataMartRawJsonImportTableType.SocrataDataMartRawJsonImportTableTypeID;
            this.RawJsonString = rawJsonString;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SocrataDataMartRawJsonImport CreateNewBlank(SocrataDataMartRawJsonImportTableType socrataDataMartRawJsonImportTableType)
        {
            return new SocrataDataMartRawJsonImport(default(DateTime), socrataDataMartRawJsonImportTableType, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SocrataDataMartRawJsonImport).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.SocrataDataMartRawJsonImports.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int SocrataDataMartRawJsonImportID { get; set; }
        public DateTime CreateDate { get; set; }
        public int SocrataDataMartRawJsonImportTableTypeID { get; set; }
        public string RawJsonString { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SocrataDataMartRawJsonImportID; } set { SocrataDataMartRawJsonImportID = value; } }

        public SocrataDataMartRawJsonImportTableType SocrataDataMartRawJsonImportTableType { get { return SocrataDataMartRawJsonImportTableType.AllLookupDictionary[SocrataDataMartRawJsonImportTableTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}