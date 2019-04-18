using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public static class GrantModificationStatusModelExtensions
    {
        public static string GetDisplayName(this GrantModificationStatus grantModificationStatus)
        {
            return grantModificationStatus != null ? grantModificationStatus.GrantModificationStatusName : string.Empty;
        }

    }
}


    