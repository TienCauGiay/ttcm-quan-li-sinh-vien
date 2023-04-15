using System.Web;
using System.Web.Mvc;

namespace ttcm_quan_li_sinh_vien
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
