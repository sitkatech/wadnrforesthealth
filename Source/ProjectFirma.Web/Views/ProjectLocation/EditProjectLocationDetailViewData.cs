using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectLocation
{
    public class EditProjectLocationDetailViewData : FirmaViewData
    {
        public EditProjectLocationDetailViewData(Person currentPerson, Models.FirmaPage firmaPage, string editProjectLocationDetailGridDataUrl, int projectId, MapInitJson mapInitJson, LayerGeoJson editProjectLocationLayerGeoJson) : base(currentPerson, firmaPage)
        {
            PostUrl = SitkaRoute<ProjectLocationController>.BuildUrlFromExpression(x => x.EditProjectLocationDetail(projectId));//todo actually hit the httpPost action
            UploadGisUrl = SitkaRoute<ProjectLocationController>.BuildUrlFromExpression(x => x.UploadGisFile(projectId));
            EditProjectLocationDetailGridDataUrl = editProjectLocationDetailGridDataUrl;
            AngularViewData = new AngularViewDataForEditProjectLocationDetail(mapInitJson, projectId, editProjectLocationLayerGeoJson);

        }

        public AngularViewDataForEditProjectLocationDetail AngularViewData { get; set; }
        public string PostUrl { get; set; }
        public string UploadGisUrl { get; set; }
        private string EditProjectLocationDetailGridDataUrl { get; set; }
    }


    public class AngularViewDataForEditProjectLocationDetail
    {
        public MapInitJson EditProjectLocationDetailMapInitJson { get; set; }
        //public List<ProjectLocationTypeJson> ProjectLocationTypeJsons { get; set; }
        public int ProjectID { get; set; }
        public LayerGeoJson EditProjectLocationDetailLayerGeoJson { get; set; }



        public AngularViewDataForEditProjectLocationDetail(MapInitJson editProjectLocationDetailMapInitJson,
            /*List<ProjectLocationTypeJson> projectLocationTypeJsons,*/ int projectId,
            LayerGeoJson editProjectLocationDetailLayerGeoJson)
        {
            EditProjectLocationDetailMapInitJson = editProjectLocationDetailMapInitJson;
            //ProjectLocationTypeJsons = projectLocationTypeJsons;

            ProjectID = projectId;
            EditProjectLocationDetailLayerGeoJson = editProjectLocationDetailLayerGeoJson;

        }
    }




}