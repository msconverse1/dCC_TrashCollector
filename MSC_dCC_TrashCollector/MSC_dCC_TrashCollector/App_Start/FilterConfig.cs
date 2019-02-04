using System.Web;
using System.Web.Mvc;

namespace MSC_dCC_TrashCollector
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
