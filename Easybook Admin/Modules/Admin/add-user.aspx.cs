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
    public partial class add_user : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();

        AppUserMasterModel _objAppUserMasterModel = new AppUserMasterModel();
        List<AppUserMasterModel> _lstAppUserMasterModel = new List<AppUserMasterModel>();
       
        DataTable dtFilter;

        SqlConnection _sqlConnection, _sqlConnection2 = new SqlConnection();
        SqlCommand _sqlCommand, _sqlCommand2, _sqlCommand3 = new SqlCommand();
        SqlDataAdapter _sqlDataAdapter, _sqlDataAdapter2 = null;

        DataTable _datatable, _datatable_lstUserRole = null;

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
                    GetAllUserRoleList();
                    GetAllAppUserList();

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

        #region All Action Button (Add/Update,Reset)

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserFirstName.Text == "" || txtUserFirstName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtUserPhoneNumber.Text == "" || txtUserPhoneNumber.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtEmailid.Text == "" || txtEmailid.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtPassword.Text == "" || txtPassword.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlUserRole.SelectedValue == "Select" || ddlUserRole.SelectedValue == "" || ddlUserRole.SelectedValue == null || ddlUserRole.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {

                    checkAndAddAppUser(txtEmailid.Text, txtUserPhoneNumber.Text);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserFirstName.Text == "" || txtUserFirstName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtUserPhoneNumber.Text == "" || txtUserPhoneNumber.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtEmailid.Text == "" || txtEmailid.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtPassword.Text == "" || txtPassword.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlUserRole.SelectedValue == "Select" || ddlUserRole.SelectedValue == "" || ddlUserRole.SelectedValue == null || ddlUserRole.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    checkAndUpdateAppUser( Convert.ToInt32(HdnFAppUserMasterId.Value.ToString()));

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserFirstName.Text = txtUserLastName.Text = txtUserPhoneNumber.Text = txtEmailid.Text = txtPassword.Text = string.Empty;
                ddlUserRole.SelectedIndex = 0;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        #endregion

        protected void rptrUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string commandname = e.CommandName;
                string id = e.CommandArgument.ToString();
                HdnFAppUserMasterId.Value = id;

                Int32 ID = Convert.ToInt32(id);
                switch (commandname)
                {
                    case "edit":
                        try
                        {
                            Int32 _appUserMasterid = Convert.ToInt32(id);

                            DataTable _dataTableAppUserList = Session[SessionNames.AllAppUserList] as DataTable;
                            for (int _index = 0; _index < _dataTableAppUserList.Rows.Count; _index++)
                            {
                                if (_dataTableAppUserList.Rows[_index]["app_user_master_id"].ToString() == _appUserMasterid.ToString())
                                {
                                    txtUserFirstName.Text = _dataTableAppUserList.Rows[_index]["user_first_name_elm"].ToString();
                                    txtUserLastName.Text = _dataTableAppUserList.Rows[_index]["user_last_name"].ToString();
                                    txtUserPhoneNumber.Text = _dataTableAppUserList.Rows[_index]["user_phone_elm"].ToString();
                                    txtEmailid.Text = _dataTableAppUserList.Rows[_index]["user_email_elm"].ToString();
                                    txtPassword.Text = _dataTableAppUserList.Rows[_index]["user_pass_elm"].ToString();
                                    ddlUserRole.SelectedValue = _dataTableAppUserList.Rows[_index]["user_role_master_id"].ToString();

                                    btnUpdate.Visible = true;
                                    btnSubmit.Visible = false;

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
                            HdnFAppUserMasterId.Value = id;
                            Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                            if (lblIsActive.Text!= null)
                            {
                                int _UserStatus = 0;
                                if (lblIsActive.Text == "Activated")
                                {
                                    _UserStatus = 0;
                                }
                                else
                                {
                                    _UserStatus = 1;
                                }
                                hdnfStatusTestimonial.Value = _UserStatus.ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showStatusBlogModal();", true);
                                
                            }
                            else
                            {

                            }
                            

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

        protected void rptrUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFAppUserMasterId.Value != null && hdnfStatusTestimonial.Value != null)
                {
                    checkAndActive_DeactiveAppUser(Convert.ToInt32(HdnFAppUserMasterId.Value), Convert.ToInt32(hdnfStatusTestimonial.Value));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        /// <summary>
        /// This Method is Used to Get All UserRole List 
        /// </summary>
        private void GetAllUserRoleList()
        {
            try
            {
                string _query = string.Empty;
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                    if (_loginUserType.ToLower() == "admin")
                    {
                        _query = QueryConstants.GET_ALL_USER_ROLL_LIST_FOR_ADMIN;
                    }
                    else
                    {
                        // In Future Role Show by User login , like (Admin or user) data show According to User Role Then yu can change Query constant according to Requirement
                        _query = QueryConstants.GET_ALL_USER_ROLL_LIST_FOR_USER;
                    }
                    
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

                            try
                            {
                                ddlUserRole.DataSource = _datatable_lstUserRole;
                                ddlUserRole.DataTextField = "user_role_name";
                                ddlUserRole.DataValueField = "user_role_master_id";
                                ddlUserRole.DataBind();
                                ddlUserRole.Items.Insert(0, new ListItem("Select", "0"));
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
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


        /// <summary>
        /// This Method is Used to Get All AppUser List 
        /// </summary>
        private void GetAllAppUserList()
        {
            try
            {
                string _query = string.Empty;
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                    if (_loginUserType !=null)
                    {
                        if (_loginUserType.ToLower() == "admin")
                        {
                            _query = QueryConstants.GET_ALL_APP_USER_LIST_FOR_ADMIN;
                        }
                        else
                        {
                            _query = QueryConstants.GET_ALL_APP_USER_LIST_FOR_USER + Session[Constants.Id].ToString();
                        }

                        _sqlConnection.Open();
                        using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                        {

                            _sqlCommand.CommandTimeout = 600;
                            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                            _datatable_lstUserRole = new DataTable();
                            _sqlDataAdapter.Fill(_datatable_lstUserRole);
                            if (_datatable_lstUserRole.Rows.Count > 0)
                            {
                                Session[SessionNames.AllAppUserList] = _datatable_lstUserRole;
                                rptrUserList.DataSource = _datatable_lstUserRole;
                                rptrUserList.DataBind();

                            }
                            else
                            {
                                rptrUserList.DataSource = null;
                                rptrUserList.DataBind();
                            }

                            _sqlConnection.Close();
                        }
                    }
                    else
                    {
                        Response.Redirect("/login", false);
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
        /// This Method is Used to Check App user Already in Db or not (Duplicate) and Save App User
        /// </summary>
        private void checkAndAddAppUser(string _emailId, string _phoneNumber) 
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from AppUserMaster where user_phone_elm='"+ _phoneNumber + "' and user_email_elm='"+ _emailId + "'";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showAlreadyUser();", true);
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

                                    _sqlCommand2.Connection = _sqlConnection2;
                                    _sqlCommand2.CommandType = CommandType.Text;
                                    _sqlCommand2.CommandText = @"insert into AppUserMaster(user_role_master_id,user_first_name_elm,user_last_name,user_email_elm,user_phone_elm,user_login_elm,user_pass_elm,display_name,profile_pic, created_by,modified_by,is_active) values (@user_role_master_id,@user_first_name_elm,@user_last_name,@user_email_elm,@user_phone_elm,@user_login_elm,@user_pass_elm,@display_name,@profile_pic,@created_by,@modified_by,@is_active)";

                                    _sqlCommand2.Parameters.AddWithValue("@user_role_master_id", ddlUserRole.SelectedValue.ToString());
                                    _sqlCommand2.Parameters.AddWithValue("@user_first_name_elm", txtUserFirstName.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@user_last_name", txtUserLastName.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@user_email_elm", txtEmailid.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@user_phone_elm", txtUserPhoneNumber.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@user_login_elm", txtEmailid.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@user_pass_elm", txtPassword.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@display_name", txtUserFirstName.Text +" " + txtUserLastName.Text);
                                    _sqlCommand2.Parameters.AddWithValue("@profile_pic", "");
                                    _sqlCommand2.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                                    _sqlCommand2.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                                    _sqlCommand2.Parameters.AddWithValue("@is_active", 1);

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        txtUserFirstName.Text = txtUserLastName.Text = txtUserPhoneNumber.Text = txtEmailid.Text = txtPassword.Text = string.Empty;
                                        ddlUserRole.SelectedIndex = 0;
                                        btnSubmit.Visible = true;
                                        btnUpdate.Visible = false;
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess();", true);

                                        try
                                        {
                                            GetAllAppUserList();
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
        /// This Method is Used to Check User  Valid or not and Update User
        /// </summary>
        private void checkAndUpdateAppUser(int _appUserMasterId)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from AppUserMaster where app_user_master_id=" + _appUserMasterId + "";
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
                                string _displayName = txtUserFirstName.Text + " " + txtUserLastName.Text;
                                string _queryUpdate = "update AppUserMaster set user_role_master_id='"+ ddlUserRole.SelectedValue.ToString() + "',user_first_name_elm='"+ txtUserFirstName.Text + "',user_last_name='"+ txtUserLastName.Text + "',user_email_elm='"+ txtEmailid.Text + "',user_phone_elm='"+ txtUserPhoneNumber.Text + "',user_login_elm='"+ txtEmailid.Text + "',user_pass_elm='"+ txtPassword.Text + "',display_name='"+ _displayName + "' where app_user_master_id=" + _appUserMasterId + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            txtUserFirstName.Text = txtUserLastName.Text = txtUserPhoneNumber.Text = txtEmailid.Text = txtPassword.Text = string.Empty;
                                            ddlUserRole.SelectedIndex = 0;
                                            btnSubmit.Visible = true;
                                            btnUpdate.Visible = false;
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate();", true);

                                            GetAllAppUserList();
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
        /// This Method is Used to Check User valid or Not and Set App User Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveAppUser(int _appUserMasterId, int _isStatus)
        {
            try
            {
                

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from AppUserMaster where app_user_master_id=" + _appUserMasterId + "";
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
                                string _queryUpdate = "update AppUserMaster set is_active="+ _isStatus + " where app_user_master_id=" + _appUserMasterId + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate();", true);

                                            GetAllAppUserList();
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

    }
}