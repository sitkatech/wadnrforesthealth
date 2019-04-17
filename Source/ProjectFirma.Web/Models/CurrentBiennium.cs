using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class CurrentBiennium
    {
        public static int GetCurrentBienniumFiscalYear()
        {
            return HttpRequestStorage.DatabaseEntities.CurrentBiennia.ToList().Single().CurrentBienniumFiscalYear;
        }
    }
}