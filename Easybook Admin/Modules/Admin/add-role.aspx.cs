using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Healing2Peace.constant;
using Healing2Peace.Model;
using System.Net;
using System.Text;
using System.Dynamic;

namespace Healing2Peace.Modules.Admin
{
    public partial class add_role : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
        
        MenuMasterModel _objMenuMasterModel = new MenuMasterModel();
        List<MenuMasterModel> _lstMenuMasterModel = new List<MenuMasterModel>();
        DataTable dtFilter;

        SqlConnection _sqlConnection, _sqlConnection2 = new SqlConnection();
        SqlCommand _sqlCommand, _sqlCommand2, _sqlCommand3 = new SqlCommand();
        SqlDataAdapter _sqlDataAdapter, _sqlDataAdapter2 = null;

        DataTable _datatable, _datatable_lstUserRole= null;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string id = Convert.ToString(Session[Constants.Id]);
                if (id == "")
                {
                    Response.Redirect(Constants.LoginPage);
                }
                else
                {
                    getMenusList();
                    GetAllUserRoleList();

                    string _AppUserRole = Session[Constants.UserType].ToString();
                    if (_AppUserRole.ToLower() == "admin")
                    {
                        menutags.Visible = true;
                        memusubscribe.Visible = true;
                    }
                    else
                    {
                        menutags.Visible = false;
                        memusubscribe.Visible = false;
                    }

                   
                }
                
            }
            else
            {
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleName.Text == "" || txtRoleName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlMenuList.SelectedValue == "Select" || ddlMenuList.SelectedValue == "" || ddlMenuList.SelectedValue == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    checkAndUpdateRole(Convert.ToInt32(HdnFUserRoleMasterId.Value));

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleName.Text == "" || txtRoleName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlMenuList.SelectedValue == "Select" || ddlMenuList.SelectedValue == "" || ddlMenuList.SelectedValue == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
               
                else
                {
                    checkAndAddRole(txtRoleName.Text);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrRoleList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string commandname = e.CommandName;
                string id = e.CommandArgument.ToString();
                HdnFUserRoleMasterId.Value = id;

                Int32 ID = Convert.ToInt32(id);
                switch (commandname)
                {
                    case "edit":
                        try
                        {
                            Int32 _userRoleMasterid = Convert.ToInt32(id);

                            DataTable _dataTableRoleList = Session[SessionNames.AllUserRoleList] as DataTable;
                            for (int _index = 0; _index < _dataTableRoleList.Rows.Count; _index++)
                            {
                                if (_dataTableRoleList.Rows[_index]["user_role_master_id"].ToString() == _userRoleMasterid.ToString())
                                {
                                    
                                    txtRoleName.Text = _dataTableRoleList.Rows[_index]["user_role_name"].ToString();
                                    txtRoleName.ReadOnly = true;
                                    ddlMenuList.Items.Clear();
                                    getMenusList();
                                    List<MenuMasterModel> _lstMenuMasterModel = Session["allmenu"] as List<MenuMasterModel>;
                                    MenuMasterModel _objMenuMasterModel = new MenuMasterModel();

                                    try
                                    {
                                        string _assingedMenuList = _dataTableRoleList.Rows[_index]["assigned_menu_ids"].ToString();

                                        if (!string.IsNullOrEmpty(_assingedMenuList))
                                        {
                                           
                                           
                                            string lasttm = _assingedMenuList.TrimEnd(',');
                                            string[] arrOfSelections = lasttm.Split(',');
                                            foreach (string value in arrOfSelections)
                                            {

                                                _objMenuMasterModel = _lstMenuMasterModel.Where(x => x.menu_master_id == Convert.ToInt32(value)).FirstOrDefault();

                                                if (_objMenuMasterModel == null)
                                                {

                                                }
                                                else
                                                {
                                                    ddlMenuList.Items.Add(new ListItem() { Text = _objMenuMasterModel.display_name, Value = value, Selected = true });
                                                   

                                                }
                                            }

                                            btnSubmit.Visible = false;
                                            btnUpdate.Visible = true;
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        ex.Message.ToString();
                                    }


                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                        }
                        break;
                    case "active":
                        try
                        {
                            Int32 menuMasterId = Convert.ToInt32(id);
                            HdnFUserRoleMasterId.Value = id;
                            checkAndActive_DeactiveUserRole( Convert.ToInt32(HdnFUserRoleMasterId.Value));

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrRoleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                    
                    LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");
                    

                    string isactive = lblIsActive.Text;
                    if (isactive == "True")
                    {
                        lblIsActive.Text = "Activated";
                        lblIsActive.CssClass = "btn btn-success";
                        btnstatusactive.Visible = true;
                        btnstatusdeactive.Visible = false;
                    }
                    else
                    {
                        lblIsActive.Text = "Deactivated";
                        lblIsActive.CssClass = "btn btn-danger";
                        btnstatusactive.Visible = false;
                        btnstatusdeactive.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {   
            try
            {
                txtRoleName.Text = string.Empty;
                txtRoleName.ReadOnly = false;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                getMenusList();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            
        }


        private void getMenusList()
        {
            List<MenuMasterModel> _lstMenuMasterModel = Session["allmenu"] as List<MenuMasterModel>;
            try
            {
                ddlMenuList.DataSource = _lstMenuMasterModel;
                ddlMenuList.DataTextField = "display_name";
                ddlMenuList.DataValueField = "menu_master_id";
                ddlMenuList.DataBind();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        /// <summary>
        /// This Method is Used to Check user Role Already in Db or not (Duplicate) and Save Role
        /// </summary>
        private void checkAndAddRole(string _roleName)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from UserRole where user_role_name='" + _roleName + "'";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                        }
                        else
                        {   
                            _sqlConnection.Close();

                           
                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {

                                using (SqlCommand _sqlCommand2 = new SqlCommand())
                                {
                                    _sqlConnection2.Open();

                                    string _selectedmenuList = string.Empty;

                                    try
                                    {
                                        if (ddlMenuList.Items.Count > 0)
                                        {
                                            string totalitemTagId = null;

                                            for (int i = 0; i < ddlMenuList.Items.Count; i++)
                                            {
                                                if (ddlMenuList.Items[i].Selected)
                                                {
                                                    string selectedItemId = ddlMenuList.Items[i].Value + ",";
                                                    //insert command
                                                    totalitemTagId = totalitemTagId + selectedItemId;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(totalitemTagId))
                                            {
                                                string lasttmId = totalitemTagId.TrimEnd(',');
                                                _selectedmenuList = lasttmId;
                                            }

                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        ex.Message.ToString();
                                    }
                                    


                                    _sqlCommand2.Connection = _sqlConnection2;
                                    _sqlCommand2.CommandType = CommandType.Text;
                                    _sqlCommand2.CommandText = @"insert into UserRole(user_role_name,assigned_menu_ids,assigned_feature_ids,created_by,modified_by,is_active) values (@user_role_name,@assigned_menu_ids,@assigned_feature_ids,@created_by,@modified_by,@is_active)";

                                    _sqlCommand2.Parameters.AddWithValue("@user_role_name", txtRoleName.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@assigned_menu_ids", _selectedmenuList);
                                    _sqlCommand2.Parameters.AddWithValue("@assigned_feature_ids", "");
                                    _sqlCommand2.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                                    _sqlCommand2.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                                    _sqlCommand2.Parameters.AddWithValue("@is_active", 1);

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        txtRoleName.Text = string.Empty;
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess();", true);

                                        btnSubmit.Visible = true;
                                        btnUpdate.Visible = false;

                                        try
                                        {
                                            getMenusList();
                                            GetAllUserRoleList();
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.Message.ToString();
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                                    }
                                    _sqlConnection2.Close();
                                }
                            }


                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
                _sqlConnection2.Close();
               
            }
        }


        /// <summary>
        /// This Method is Used to Check User Role Valid or not and Update Role
        /// </summary>
        private void checkAndUpdateRole( int _userRoleMasterId)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from UserRole where user_role_master_id=" + _userRoleMasterId + "";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            _sqlConnection.Close();

                            string _selectedmenuList = string.Empty;

                            try
                            {
                                if (ddlMenuList.Items.Count > 0)
                                {
                                    string totalitemTagId = null;

                                    for (int i = 0; i < ddlMenuList.Items.Count; i++)
                                    {
                                        if (ddlMenuList.Items[i].Selected)
                                        {
                                            string selectedItemId = ddlMenuList.Items[i].Value + ",";
                                            //insert command
                                            totalitemTagId = totalitemTagId + selectedItemId;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(totalitemTagId))
                                    {
                                        string lasttmId = totalitemTagId.TrimEnd(',');
                                        _selectedmenuList = lasttmId;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {
                                string _queryUpdate = "update UserRole set assigned_menu_ids='" + _selectedmenuList + "' where user_role_master_id=" + _userRoleMasterId + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        txtRoleName.Text = string.Empty;
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess();", true);

                                        btnSubmit.Visible = true;
                                        btnUpdate.Visible = false;

                                        try
                                        {
                                            getMenusList();
                                            GetAllUserRoleList();
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.Message.ToString();
                                        }

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                                    }
                                    _sqlConnection2.Close();
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                            
                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
                _sqlConnection2.Close();

            }
        }


        /// <summary>
        /// This Method is Used to Check User valid or Not and Set User Role Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveUserRole(int _userRoleMasterId)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from UserRole where user_role_master_id=" + _userRoleMasterId + "";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            _sqlConnection.Close();


                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {
                                string _queryUpdate = "update UserRole set is_active=0 where user_role_master_id=" + _userRoleMasterId + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        txtRoleName.Text = string.Empty;
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess();", true);

                                        btnSubmit.Visible = true;
                                        btnUpdate.Visible = false;

                                        try
                                        {
                                            getMenusList();
                                            GetAllUserRoleList();
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.Message.ToString();
                                        }

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                                    }
                                    _sqlConnection2.Close();
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);

                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
                _sqlConnection2.Close();

            }
        }


        /// <summary>
        /// This Method is Used to Get All UserRole List 
        /// </summary>
        private void GetAllUserRoleList()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from UserRole order by user_role_master_id desc";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable_lstUserRole = new DataTable();
                        _sqlDataAdapter.Fill(_datatable_lstUserRole);
                        if (_datatable_lstUserRole.Rows.Count > 0)
                        {
                            Session[SessionNames.AllUserRoleList] = _datatable_lstUserRole;
                            rptrRoleList.DataSource = _datatable_lstUserRole;
                            rptrRoleList.DataBind();

                        }
                        else
                        {
                            rptrRoleList.DataSource = null;
                            rptrRoleList.DataBind();
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

    }
}