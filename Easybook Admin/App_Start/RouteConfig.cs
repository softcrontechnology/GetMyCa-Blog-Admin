using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Healing2Peace
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;

            #region  Login
            routes.MapPageRoute("Login", "login", "~/Login.aspx", false);
            #endregion

            #region  Admin
            routes.MapPageRoute("Addgallery", "manage-gallery", "~/Modules/Admin/add-gallery.aspx", false);
            routes.MapPageRoute("AddLink", "manage-social-links", "~/Modules/Admin/add-link.aspx", false);
            routes.MapPageRoute("AddMenuMaster", "add-menu-master", "~/Modules/Admin/add-menu-master.aspx", false);
            routes.MapPageRoute("AddMenus", "add-menus", "~/Modules/Admin/add-menus.aspx", false);
            routes.MapPageRoute("AddRole", "manage-role", "~/Modules/Admin/add-role.aspx", false);
            routes.MapPageRoute("AddUser", "manage-user", "~/Modules/Admin/add-user.aspx", false);
            routes.MapPageRoute("AddHomeContant", "site-content-master", "~/Modules/Admin/content-master.aspx", false);
            routes.MapPageRoute("AddTestimonial", "manage-testimonial", "~/Modules/Admin/testimonial-master.aspx", false);
            #endregion

            #region  Blog
            routes.MapPageRoute("AddBlog", "manage-blog", "~/Modules/Blog/blog-master.aspx", false);
            routes.MapPageRoute("AddBlogCategory", "manage-blog-category", "~/Modules/Blog/category-master.aspx", false);
            #endregion

            #region  Contact
            routes.MapPageRoute("ContactMaster", "manage-contact", "~/Modules/Contact/contact-master.aspx", false);
            #endregion

            #region  Dashboard
            routes.MapPageRoute("Dashboard", "dashboard", "~/Modules/Dashboard/dashboard.aspx", false);
            #endregion

            #region  Items
            routes.MapPageRoute("ItemMaster", "manage-item", "~/Modules/Items/manage-item.aspx", false);
            #endregion

            #region  Subscribe
            routes.MapPageRoute("SubscribeMaster", "manage-subscribe", "~/Modules/Subscriber/subscribe-master.aspx", false);
            #endregion

            routes.EnableFriendlyUrls();
        }
    }
}
