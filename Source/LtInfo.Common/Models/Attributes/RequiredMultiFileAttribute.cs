using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LtInfo.Common.Models.Attributes
{
    public class RequiredMultiFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var files = value as List<HttpPostedFileBase>;

            if (files[0] == null)
            {
                return false;
            }

            return true;
        }
    }
}