﻿/*-----------------------------------------------------------------------
<copyright file="IWebServices.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.ServiceModel;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Service.ServiceModels;
using LtInfo.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Service
{
    [ServiceContract]
    [WebServicesErrorHandler]
    public interface IWebServices
    {
        [OperationContract]
        [WebServiceDocumentationAttribute("Provides detailed information for the specified {0}.", FieldDefinitionEnum.Project)]
        List<WebServiceProject> GetProject(string returnType, string webServiceToken, int projectID);

        [OperationContract]
        [WebServiceDocumentationAttribute("Provides the list of all {0}s regardless of stage or year completed.", FieldDefinitionEnum.Project)]
        List<WebServiceProject> GetProjects(string returnType, string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("Provides the list of {0}s for the specified {1}.", FieldDefinitionEnum.Project, FieldDefinitionEnum.Organization)]
        List<WebServiceProject> GetProjectsByOrganization(string returnType, string webServiceToken, int organizationID);

        [OperationContract]
        [WebServiceDocumentationAttribute("Provides the description of the specified {0}.", FieldDefinitionEnum.Project)]
        List<WebServiceProjectDescription> GetProjectDescription(string returnType, string webServiceToken, int projectID);

        [OperationContract]
        [WebServiceDocumentationAttribute("Provides the URL to the key photo for the specified {0}.", FieldDefinitionEnum.Project)]
        List<WebServiceProjectKeyPhoto> GetProjectKeyPhoto(string returnType, string webServiceToken, int projectID);


        [OperationContract]
        [WebServiceDocumentationAttribute("Provides the list of all {0}s within the platform. Most {0}s in this list are local jurisdictions, agencies, associations, or private firms, however this list will also include {0}s of any person who as requested an account.", FieldDefinitionEnum.Organization)]
        List<WebServiceOrganization> GetOrganizations(string returnType, string webServiceToken);
    }
}
