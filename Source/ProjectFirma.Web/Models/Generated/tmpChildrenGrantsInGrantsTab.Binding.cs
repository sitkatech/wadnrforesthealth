//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpChildrenGrantsInGrantsTab]
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
    // Table [dbo].[tmpChildrenGrantsInGrantsTab] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[tmpChildrenGrantsInGrantsTab]")]
    public partial class tmpChildrenGrantsInGrantsTab : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected tmpChildrenGrantsInGrantsTab()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public tmpChildrenGrantsInGrantsTab(int childGrantID, string parent Child (SITKA), string grant #, string cFDA #, string title, string program Manager, string prog Index, string project Codes, string funding increase, string federal Fund Code, string funds Awarded, string matching Funds, string grant Total, string dNR Match Amount, string non-DNR Other Match, string landowner Match, string match PI & Alpha Code, string expend Method, string start Date, string end Date, string notes) : this()
        {
            this.ChildGrantID = childGrantID;
            this.Parent Child (SITKA) = parent Child (SITKA);
            this.Grant # = grant #;
            this.CFDA # = cFDA #;
            this.Title = title;
            this.Program Manager = program Manager;
            this.Prog Index = prog Index;
            this.Project Codes = project Codes;
            this.Funding increase = funding increase;
            this.Federal Fund Code = federal Fund Code;
            this.Funds Awarded = funds Awarded;
            this.Matching Funds = matching Funds;
            this.Grant Total = grant Total;
            this.DNR Match Amount = dNR Match Amount;
            this.Non-DNR Other Match = non-DNR Other Match;
            this.Landowner Match = landowner Match;
            this.Match PI & Alpha Code = match PI & Alpha Code;
            this.Expend Method = expend Method;
            this.Start Date = start Date;
            this.End Date = end Date;
            this.Notes = notes;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static tmpChildrenGrantsInGrantsTab CreateNewBlank()
        {
            return new tmpChildrenGrantsInGrantsTab();
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(tmpChildrenGrantsInGrantsTab).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.tmpChildrenGrantsInGrantsTabs.Remove(this);
        }

        [Key]
        public int ChildGrantID { get; set; }
        public string Parent Child (SITKA) { get; set; }
        public string Grant # { get; set; }
        public string CFDA # { get; set; }
        public string Title { get; set; }
        public string Program Manager { get; set; }
        public string Prog Index { get; set; }
        public string Project Codes { get; set; }
        public string Funding increase { get; set; }
        public string Federal Fund Code { get; set; }
        public string Funds Awarded { get; set; }
        public string Matching Funds { get; set; }
        public string Grant Total { get; set; }
        public string DNR Match Amount { get; set; }
        public string Non-DNR Other Match { get; set; }
        public string Landowner Match { get; set; }
        public string Match PI & Alpha Code { get; set; }
        public string Expend Method { get; set; }
        public string Start Date { get; set; }
        public string End Date { get; set; }
        public string Notes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ChildGrantID; } set { ChildGrantID = value; } }



        public static class FieldLengths
        {
            public const int Parent Child (SITKA) = 50;
            public const int Grant # = 50;
            public const int CFDA # = 50;
            public const int Title = 50;
            public const int Program Manager = 50;
            public const int Prog Index = 50;
            public const int Project Codes = 50;
            public const int Funding increase = 50;
            public const int Federal Fund Code = 50;
            public const int Funds Awarded = 50;
            public const int Matching Funds = 50;
            public const int Grant Total = 50;
            public const int DNR Match Amount = 50;
            public const int Non-DNR Other Match = 50;
            public const int Landowner Match = 50;
            public const int Match PI & Alpha Code = 50;
            public const int Expend Method = 50;
            public const int Start Date = 50;
            public const int End Date = 50;
            public const int Notes = 50;
        }
    }
}