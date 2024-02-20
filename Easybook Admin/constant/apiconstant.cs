using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.constant
{
    public class apiconstant
    {

        //Blog Categories

        public static string GET_ALL_CATEGORIES = "blog/categories";
        public static string GET_ALL_CATEGORIES_FOR_ADMIN = "blog/admincategories";

        //Item Master

        public static string GET_ALL_ITEM_LIST = "item/all";
        public static string GET_ALL_ITEM_LIST_FOR_ADMIN = "item/adminall";

        //Contact

        public static string ADD_NEW_CONTACT = "contact/add";
        public static string GET_ALL_CONTACT = "contact/all";


        //Subscribe

        public static string ADD_NEW_SUBSCRIBE = "subscribe/add";
        public static string GET_ALL_SUBSCRIBE = "subscribe/all";

        ////Blog
        public static string ADD_NEW_BLOGS = "blog/add";
        public static string UPDATE_BLOGS = "blog/update";
        public static string GET_ALL_BLOGS = "blog/all";
        public static string GET_ALL_BLOGS_FOR_ADMIN = "blog/adminall";
        public static string PUBLISH_BLOGS = "blog/publish";
        public static string SET_FEATURED_BLOGS = "blog/setfeatured";
        public static string GET_ALL_SET_FEATURED_BLOGS = "blog/featured";
        public static string SET_STATUS_ACTIVE_DEACTIVE_BLOGS = "blog/togglestatus";


       
    }
}