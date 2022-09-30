using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.ReportTemplates
{
    [TestFixture]
    public class ReportTemplateTests
    {
        [Test]
        public void AllExistingProjectModelReportTemplatesAreValid()
        {
            List<ReportTemplateTestFailedReportTemplate> failedReportTemplates = new List<ReportTemplateTestFailedReportTemplate>();
            //var reportTemplates = HttpRequestStorageForTest.DatabaseEntities.AllReportTemplates.ToList();
            var reportTemplates = HttpRequestStorageForTest.DatabaseEntities.ReportTemplates.ToList();


            var sampleProjectIDs = HttpRequestStorageForTest.DatabaseEntities.Projects.OrderBy(x => x.ProjectName).Select(p => p.ProjectID).Take(30).ToList();

            foreach (var reportTemplate in reportTemplates)
            {
                ReportTemplateGenerator.ValidateReportTemplateForSelectedModelIDs(reportTemplate, sampleProjectIDs, out var reportIsValid, out var errorMessage, out var sourceCode);
                if (!reportIsValid)
                {
                    failedReportTemplates.Add(new ReportTemplateTestFailedReportTemplate(reportTemplate, errorMessage, sourceCode));
                }
            }
            

            Assert.That(failedReportTemplates, Is.Empty, $"The following report templates (count: {failedReportTemplates.Count}) have failed to compile: {string.Join(", ", failedReportTemplates.Select(x => x.ErrorMessageForTest()))}. Was there any changes recently to the Docx ReportTemplating or associated models?");
        }

        public class ReportTemplateTestFailedReportTemplate
        {
            public ReportTemplate ReportTemplate { get; }
            public string ErrorMessage { get; }
            public string SourceCode { get; }
           

            public ReportTemplateTestFailedReportTemplate(ReportTemplate reportTemplate, string errorMessage, string sourceCode)
            {
                ReportTemplate = reportTemplate;
                ErrorMessage = errorMessage;
                SourceCode = sourceCode;
            }

            public string ErrorMessageForTest()
            {
                return $"(ReportTemplateID: {ReportTemplate.ReportTemplateID}, ReportTemplateModel: {ReportTemplate.ReportTemplateModel.ReportTemplateModelDisplayName}, ErrorMessage: \"{ErrorMessage.Trim()}\")";
            }
        }
    }

    


}