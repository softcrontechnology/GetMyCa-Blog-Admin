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

namespace Healing2Peace
{
    public partial class Login : System.Web.UI.Page
    {

        #region Global Variables
        SqlConnection _sqlConnection = new SqlConnection();
        SqlCommand _sqlCommand, _sqlCommand2, _sqlCommand3 = new SqlCommand();
        SqlDataAdapter _sqlDataAdapter, _sqlDataAdapter2 = null; 
        DataTable _datatable_lstAppUserMaster, _datatable_lstUserRole = null;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Scriptcall();
            
        }


        #region Page load methods

        private void Scriptcall()
        {
            if (Session["Password"] != null)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "PasswordChanged();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('password changed succesfully!')", true);
                Session["Password"] = null;
            }
            if (Session["WrongOTP"] != null)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "OTPExpired();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP not matched!')", true);
                Session["WrongOTP"] = null;
            }
            if (Session["Passworderror"] != null)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "OTPExpired();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong...Please try again!')", true);
                Session["Passworderror"] = null;
            }
        }

        #endregion

        protected void val_terms_CheckedChanged(object sender, EventArgs e)
        {
            if (!val_terms.Checked)
            {
                btnLogin.Enabled = false;
            }
            else
            {
                btnLogin.Enabled = true;
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {  
                    if (Context.Request.Form["user"].ToString() == "" || Context.Request.Form["user"].ToString() == null)
                    {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                       
                    }
                    else if (Context.Request.Form["password"].ToString() == "" || Context.Request.Form["password"].ToString() == null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                    }
                    else
                    {
                        getUserRoleItemById(Context.Request.Form["user"].ToString(), Context.Request.Form["password"].ToString());
                    }

            }
            catch (Exception ex)
            {
                
                string _ErrorMessage = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _ErrorMessage + "','danger');", true);
            }
        }

        /// <summary>
        /// This Method is Used to Get User Detail by Id and Password
        /// </summary>
        private void getUserRoleItemById(string _user_login_email, string _user_login_password)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from AppUserMaster where  is_active=1 and user_login_elm='" + _user_login_email + "' and user_pass_elm="+ _user_login_password+"";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable_lstAppUserMaster = new DataTable();
                        _sqlDataAdapter.Fill(_datatable_lstAppUserMaster);
                        if (_datatable_lstAppUserMaster.Rows.Count > 0)
                        {

                            Session[Constants.SessionUserList] = _datatable_lstAppUserMaster;
                            Session[Constants.UserName] = _datatable_lstAppUserMaster.Rows[0]["display_name"].ToString();
                            Session[Constants.LoginID] = _datatable_lstAppUserMaster.Rows[0]["user_login_elm"].ToString();
                            Session[Constants.RoleId] = _datatable_lstAppUserMaster.Rows[0]["user_role_master_id"].ToString();
                            Session[Constants.Id] = _datatable_lstAppUserMaster.Rows[0]["app_user_master_id"].ToString();

                            _sqlConnection.Close();

                            if (_datatable_lstAppUserMaster.Rows[0]["user_role_master_id"].ToString()=="")
                            {
                                
                            }
                            else
                            {  
                                string _query2 = "select * from UserRole where is_active=1 and user_role_master_id=" + _datatable_lstAppUserMaster.Rows[0]["user_role_master_id"].ToString() + "";
                                _sqlConnection.Open();
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_query2, _sqlConnection))
                                {
                                    _sqlCommand2.CommandTimeout = 600;
                                    _sqlDataAdapter2 = new SqlDataAdapter(_sqlCommand2);
                                    _datatable_lstUserRole = new DataTable();
                                    _sqlDataAdapter2.Fill(_datatable_lstUserRole);

                                    if (_datatable_lstUserRole.Rows.Count > 0)
                                    {
                                        Session[Constants.UserType] = _datatable_lstUserRole.Rows[0]["user_role_name"].ToString();
                                        Response.Redirect("/dashboard", false);
                                    }
                                    else
                                    {

                                    }
                                }

                                 _sqlConnection.Close();

                            }
                            
                        }
                        else
                        {
                            string _ErrorMessage = "Please Fill Valid Details";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _ErrorMessage + "','danger');", true);
                            
                        }

                        _sqlConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + error + "','danger');", true);
            }
        }

    }
}