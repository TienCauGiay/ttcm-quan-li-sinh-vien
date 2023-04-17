﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ttcm_quan_li_sinh_vien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "admin",
                url: "quan-tri-vien",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add faculty",
                url: "them-khoa",
                defaults: new { controller = "Faculty", action = "AddFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage faculty",
                url: "quan-li-khoa",
                defaults: new { controller = "Faculty", action = "ManageFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin update faculty",
                url: "quan-li-khoa-sua-{id}",
                defaults: new { controller = "Faculty", action = "UpdateFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin delete faculty",
                url: "quan-li-khoa-xoa-{id}",
                defaults: new { controller = "Faculty", action = "DeleteFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add teacher",
                url: "them-giang-vien",
                defaults: new { controller = "Admin", action = "AddTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage teacher",
                url: "quan-li-giang-vien",
                defaults: new { controller = "Admin", action = "ManageTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add student",
                url: "them-sinh-vien",
                defaults: new { controller = "Admin", action = "AddStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage student",
                url: "quan-li-sinh-vien",
                defaults: new { controller = "Admin", action = "ManageStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add user",
                url: "them-tai-khoan",
                defaults: new { controller = "Admin", action = "AddUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage user",
                url: "quan-li-tai-khoan",
                defaults: new { controller = "Admin", action = "ManageUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Teacher",
                url: "giang-vien",
                defaults: new { controller = "Teacher", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Student",
                url: "sinh-vien",
                defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
