using System.Web;
using System.Web.Mvc;

namespace gov.cityofboston.hubhacks2.web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
