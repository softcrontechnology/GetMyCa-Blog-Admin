using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.constant
{
    public class Constants
    {
        public const string Id = "Id";
        public const string UserName = "UserName";
        public const string UserType = "UserType";
        public const string LoginID = "LoginID";
        public const string RoleId = "RoleId";
        public const string SessionUserList = "SessionUserList";
        public const string SessionReviewerList = "SessionReviewerList";
        public const string LoginPage = "~/Login.aspx";
    }

    public class SessionNames
    {
        
        public const string AllUserRoleList = "UserRoleNameList";
        public const string AllMenuList = "MenuList";
        public const string AllAppUserList = "AppUserList";
        public const string AllSiteContentPageList = "SiteContentPageList"; 

        /// <summary>
        /// This Session is used for Store Item List from Api
        /// </summary>
        public const string AllItemList = "ItemList"; 
        
    }


    public class QueryConstants
    {
        #region User Master Query
        public const string GET_ALL_APP_USER_LIST_FOR_ADMIN = "select * from AppUserMaster order by app_user_master_id desc";
        public const string GET_ALL_APP_USER_LIST_FOR_USER = "select * from AppUserMaster where created_by=";
        public const string CHECK_APP_USER_ALREADY_IN_DB_OR_NOT = "select * from AppUserMaster where user_phone_elm='' and user_email_elm=''";

        #endregion

        #region User Role Master Query
        public const string GET_ALL_USER_ROLL_LIST_FOR_ADMIN = "select * from UserRole";
        public const string GET_ALL_USER_ROLL_LIST_FOR_USER = "select * from UserRole where user_role_name !='admin' and user_role_name !='Admin' and user_role_name !='ADMIN'";
        #endregion



    }

}