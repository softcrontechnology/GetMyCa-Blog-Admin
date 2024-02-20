using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Healing2Peace.constant;
using Healing2Peace.Model;
using System.Text;

namespace Healing2Peace.Layout
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {

        #region Global Variables
        SqlConnection _sqlConnection = new SqlConnection();
        SqlCommand _sqlCommand = new SqlCommand();
        SqlDataAdapter _sqlDataAdapter = null;
        DataTable _datatable, _datatable_lstMenuMaster, _datatable_lstSubMenuList, _datatable_lstMenuMasterRonly = null;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           

            try
            {
                string id = Convert.ToString(Session[Constants.Id]);
                if (id == "")
                {
                    Response.Redirect(Constants.LoginPage);
                }
                else
                {
                    string RoleId = Convert.ToString(Session[Constants.RoleId]);
                    string UserId = Convert.ToString(Session[Constants.Id]);
                    string _appUserName = Convert.ToString(Session[Constants.UserName]);

                    lblLoginAppUserName.Text = _appUserName;
                    // get All Staff Before bind Userrole item 

                    getUserRoleItemById(RoleId);



                }

            }
            catch(Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }


        /// <summary>
        /// This Method is Used to Get All Query data
        /// </summary>
        private void getAllStaff()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from ContactMaster where is_active=1";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                           
                        }
                        else
                        {
                           
                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
            }
        }


        /// <summary>
        /// This Method is Used to Get User Role Item By Id
        /// </summary>
        private void getUserRoleItemById(string _role_id)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from UserRole where  is_active=1 and user_role_master_id="+ _role_id +"";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            //if (_staff != null)
                            //{

                            //}
                            //else
                            //{

                            //}
                            string assinged_menu_id = _datatable.Rows[0]["assigned_menu_ids"].ToString();

                            if (Session[Constants.SessionUserList] !=null)
                            {
                                CreateMenu(assinged_menu_id);
                            }
                            else
                            {

                            }
                            

                        }
                        else
                        {

                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                Response.Redirect("/login", false);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                Response.Redirect("/login", false);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        /// <summary>
        /// This Method is Used to Get All menu List and Create Menu
        /// </summary>
        private void CreateMenu(string _menu_id)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from MenuMaster where is_active=1 and menu_master_id in (" + _menu_id + ")";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable_lstMenuMaster = new DataTable();
                        _sqlDataAdapter.Fill(_datatable_lstMenuMaster);
                        if (_datatable_lstMenuMaster.Rows.Count > 0)
                        {
                            var _stringBuilder = new StringBuilder();
                           
                            List<MenuMasterModel> listMenu = new List<MenuMasterModel>();

                            List<MenuMasterModel> listMenuRonly = new List<MenuMasterModel>();

                           // listMenu = DataTableToList(_datatable_lstMenuMaster).ToList();

                            listMenu = (from DataRow dr in _datatable_lstMenuMaster.Rows
                                           select new MenuMasterModel()
                                           {
                                               menu_master_id = Convert.ToInt32(dr["menu_master_id"]),
                                               guid = (Guid) dr["guid"],
                                               display_name = dr["display_name"].ToString(),
                                               page_url = dr["page_url"].ToString(),
                                               parent_id =(int) dr["parent_id"],
                                               parent_order = (int) dr["parent_order"],
                                               child_order = (int) dr["child_order"],
                                               cssclass =  dr["cssclass"].ToString()

                                           }).ToList();
                            listMenuRonly = listMenu.Where(x => x.parent_id == 0).OrderBy(x => x.parent_order).ToList();
                            Session["allmenu"] = listMenu;
                            string unorderedList = GenerateUL(listMenuRonly, listMenu, _stringBuilder);
                            ltrlRecMenu.Text = unorderedList;
                        }
                        else
                        {

                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
            }
        }


        public List<MenuMasterModel> DataTableToList(DataTable _dataTableList)
        {
            List<MenuMasterModel> _lstMenuMaster = new List<MenuMasterModel>();
           
            MenuMasterModel _objMenuMasterModel = new MenuMasterModel();
            for (int _indexlist = 0; _indexlist < _dataTableList.Rows.Count; _indexlist++)
            {
                _objMenuMasterModel.menu_master_id = (int)_dataTableList.Rows[_indexlist]["menu_master_id"];
                _objMenuMasterModel.display_name = _dataTableList.Rows[_indexlist]["display_name"].ToString();
                _objMenuMasterModel.page_url = _dataTableList.Rows[_indexlist]["page_url"].ToString();
                _objMenuMasterModel.parent_id = (int)_dataTableList.Rows[_indexlist]["parent_id"];
                _objMenuMasterModel.child_order = (int)_dataTableList.Rows[_indexlist]["child_order"];
                _objMenuMasterModel.cssclass = _dataTableList.Rows[_indexlist]["cssclass"].ToString();


                _lstMenuMaster.Add(_objMenuMasterModel);
            }


            return _lstMenuMaster.ToList();
        }
        private string GenerateUL(List< MenuMasterModel> menuListParentsOnly, List<MenuMasterModel> menuListAll, StringBuilder sb)
        {

            if (menuListParentsOnly.Count > 0)
            {
                foreach (MenuMasterModel menu in menuListParentsOnly)
                {
                    string pageUrl = menu.page_url;
                    string menuDisplayName = menu.display_name;
                    string CssClass = menu.cssclass;
                    string line = string.Empty;

                    string pid = Convert.ToString(menu.menu_master_id);
                    string parentId = menu.parent_id.ToString();

                    if (parentId == "0")
                    {
                        line = String.Format(@"<li> <a href=""{0}"" class=""sidebar-nav-menu""><i class=""fa fa-angle-left sidebar-nav-indicator sidebar-nav-mini-hide""></i><i class=""{2}""></i><span class=""sidebar-nav-mini-hide"">{1} </span></a>", pageUrl, menuDisplayName, CssClass + " sidebar-nav-icon");
                        sb.Append(line);
                    }

                    else
                    {
                        line = String.Format(@" <li><a href=""{0}"">{1}</a></li>", pageUrl, menuDisplayName);
                        sb.Append(line);
                    }

                    List<MenuMasterModel> subMenu = menuListAll.Where(x => x.parent_id == Convert.ToInt32(pid)).OrderBy(x => x.child_order).ToList();

                    if (subMenu.Count > 0 && !pid.Equals(parentId))
                    {
                        var subMenuBuilder = new StringBuilder();

                        string lineUlSub = String.Format(@"<ul class=""{0}"">", "treeview-menu");
                        subMenuBuilder.AppendLine(lineUlSub);
                        sb.Append(GenerateUL(subMenu, menuListAll, subMenuBuilder));
                    }
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            return sb.ToString();

        }
    }
}