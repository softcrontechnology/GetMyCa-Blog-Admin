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
    public partial class add_link : System.Web.UI.Page
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
                    getAllLinkList();

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

        #region
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMediaLinks.SelectedValue == "" || ddlMediaLinks.SelectedValue == null || ddlMediaLinks.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtLinkUrl.Text == "" || txtLinkUrl.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else
                {
                    addLink();

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrLinkList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string commandname = e.CommandName;
                string id = e.CommandArgument.ToString();

                switch (commandname)
                {
                    case "edit":
                        try
                        {
                           
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "oopsToast", true);
                        }
                        break;
                    case "active":
                        try
                        {

                            HdnFLinkMasterId.Value = id;
                            Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                            if (lblIsActive.Text != null)
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "oopsToast();", true);
                        }
                        break;
                    case "delete":
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "oopsToast();", true);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrLinkList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "oopsToast()", true);
            }
        }

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFLinkMasterId.Value != null && hdnfStatusTestimonial.Value != null)
                {
                    checkAndActive_DeactiveSocailMediaLink(Convert.ToInt32(HdnFLinkMasterId.Value), Convert.ToInt32(hdnfStatusTestimonial.Value));
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMediaLinks.SelectedValue == "" || ddlMediaLinks.SelectedValue == null || ddlMediaLinks.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtLinkUrl.Text == "" || txtLinkUrl.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else
                {
                   

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlMediaLinks.SelectedIndex = 0;
            txtLinkUrl.Text = string.Empty;
        }

        #endregion


        /// <summary>
        /// This Method is Used to Save data
        /// </summary>
        private void addLink()
        {
            try
            {
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {

                    using (SqlCommand _sqlCommand = new SqlCommand())
                    {
                        _sqlConnection.Open();

                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = @"insert into SocialMediaLinkMaster(link_name,link_url,created_by,modified_by,is_active) values (@link_name,@link_url,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@link_name", ddlMediaLinks.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@link_url", txtLinkUrl.Text);
                        _sqlCommand.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@is_active", 1);

                        int _outputCount = _sqlCommand.ExecuteNonQuery();
                        if (_outputCount > 0)
                        {

                            try
                            {
                               
                                ddlMediaLinks.SelectedIndex = 0;
                                txtLinkUrl.Text = string.Empty;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);
                                getAllLinkList();
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('noe record added','warning');", true);
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
        /// This Method is Used to Get All Testimonial data
        /// </summary>
        private void getAllLinkList()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from SocialMediaLinkMaster order by social_media_link_master_id desc";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            rptrLinkList.DataSource = _datatable;
                            rptrLinkList.DataBind();
                        }
                        else
                        {
                            rptrLinkList.DataSource = null;
                            rptrLinkList.DataBind();
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
        /// This Method is Used to Check Socail Media Link valid or Not and Set Socail-Media-Link Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveSocailMediaLink(int _link_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from SocialMediaLinkMaster where social_media_link_master_id=" + _link_master_id + "";
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
                                string _queryUpdate = "update SocialMediaLinkMaster set is_active=" + _isStatus + " where social_media_link_master_id=" + _link_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            txtLinkUrl.Text = string.Empty;
                                            getAllLinkList();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate();", true);

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