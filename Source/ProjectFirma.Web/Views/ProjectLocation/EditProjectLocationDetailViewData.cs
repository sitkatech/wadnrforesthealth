using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectLocation
{
    public class EditProjectLocationDetailViewData : FirmaViewData
    {
        public EditProjectLocationDetailViewData(Person currentPerson, string postUrl, string uploadGisUrl,
            string basicsPageUrl, string editProjectLocationDetailGridDataUrl, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PostUrl = postUrl;
            UploadGisUrl = uploadGisUrl;
            BasicsPageUrl = basicsPageUrl;
            EditProjectLocationDetailGridDataUrl = editProjectLocationDetailGridDataUrl;
            

        }

        public AngularViewDataForEditProjectLocationDetail AngularViewData { get; set; }
        public string PostUrl { get; set; }
        public string UploadGisUrl { get; set; }
        public string BasicsPageUrl { get; set; }
        private string EditProjectLocationDetailGridDataUrl { get; set; }
    }


    public class AngularViewDataForEditProjectLocationDetail
    {
        public MapInitJson EditProjectLocationDetailMapInitJson { get; set; }
        //public List<ProjectLocationTypeJson> ProjectLocationTypeJsons { get; set; }
        public int ProjectID { get; set; }
        public LayerGeoJson EditProjectLocationDetailLayerGeoJson { get; set; }



        public AngularViewDataForEditProjectLocationDetail(MapInitJson editProjectLocationDetailMapInitJson,
            /*List<ProjectLocationTypeJson> projectLocationTypeJsons,*/ Models.Project project,
            LayerGeoJson editProjectLocationDetailLayerGeoJson)
        {
            EditProjectLocationDetailMapInitJson = editProjectLocationDetailMapInitJson;
            //ProjectLocationTypeJsons = projectLocationTypeJsons;

            ProjectID = project.ProjectID;
            EditProjectLocationDetailLayerGeoJson = editProjectLocationDetailLayerGeoJson;

        }
    }




}