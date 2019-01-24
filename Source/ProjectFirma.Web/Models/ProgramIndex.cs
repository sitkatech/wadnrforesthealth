using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Microsoft.SqlServer.Types;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class ProgramIndex : IAuditableEntity
    {

        public string AuditDescriptionString => ProgramIndexAbbrev;

    }
}