using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectImportBlockList
{
    public class EditProjectImportBlockListViewData : FirmaViewData
    {
        public bool LockIdentifierFields { get; set; }

        public EditProjectImportBlockListViewData(Person currentPerson, Models.Project project) : base(currentPerson)
        {
            LockIdentifierFields = (project != null);
        }
    }
}