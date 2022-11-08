/*-----------------------------------------------------------------------
<copyright file="ProjectMapPopupViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventMapPopupViewData : FirmaUserControlViewData
    {
        public Models.InteractionEvent InteractionEvent { get; }
        public HtmlString InteractionEventContactsWithLinksAsCommaDelimitedHtmlString { get; }
        public HtmlString InteractionEventProjectsWithLinksAsCommaDelimitedHtmlString { get; }

        

        public InteractionEventMapPopupViewData(Models.InteractionEvent interactionEvent)
        {
            InteractionEvent = interactionEvent;
            InteractionEventContactsWithLinksAsCommaDelimitedHtmlString = BuildHtmlStringOfCommaDelimitedObjects(
                                                                        interactionEvent.InteractionEventContacts.Select(iec =>
                                                                        iec.Person.GetFullNameFirstLastAndOrgShortNameAsUrl()));

            InteractionEventProjectsWithLinksAsCommaDelimitedHtmlString =  BuildHtmlStringOfCommaDelimitedObjects(interactionEvent.InteractionEventProjects.Select(iep => iep.Project.DisplayNameAsUrl));
        }


        private HtmlString BuildHtmlStringOfCommaDelimitedObjects(IEnumerable<HtmlString> htmlStringObjects)
        {

            var stringBuilder = new StringBuilder();
            var counter = 0;
            foreach (var htmlString in htmlStringObjects)
            {
                if (counter == 0)
                {
                    //first item added to our stringBuilder
                    stringBuilder.Append(htmlString);
                }
                else
                {
                    stringBuilder.Append(", " + htmlString);
                }

                counter++;
            }

            return new HtmlString(stringBuilder.ToString());
        }



    }
}
