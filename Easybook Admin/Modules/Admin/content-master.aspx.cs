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
    public partial class content_master : System.Web.UI.Page
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
                    getAllContentMasterList();

                    string _AppUserRole = Session[Constants.UserType].ToString();
                    if (_AppUserRole.ToLower() == "admin")
                    {
                        menutags.Visible = true;
                        memusubscribe.Visible = true;
                        lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                        lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
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
                if (ddlPage.SelectedValue == "" || ddlPage.SelectedValue == null || ddlPage.SelectedValue =="Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlPageContentType.SelectedValue == "" || ddlPageContentType.SelectedValue == null || ddlPageContentType.SelectedValue =="Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else if (txtContentDescription.Text == "" || txtContentDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    addContentMaster();


                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrContentMasterList_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                            HdnFHomeContentMasterId.Value = id;
                            bindValueonTextField(id);
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
                            
                            HdnFHomeContentMasterId.Value = id;
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

        protected void rptrContentMasterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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


        /// <summary>
        /// This Method is Used to Bind or Fill Values on Text Field
        /// </summary>
        private void bindValueonTextField(string _HomeContentMasterId)
        {
            try
            {
                try
                {

                    string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                    using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                    {
                        string _query = "select * from HomeContentMaster where home_content_master_id=" + _HomeContentMasterId;
                        _sqlConnection.Open();
                        using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                        {
                            txtTitle.Text = txtContentDescription.Text = string.Empty;
                            ddlPage.SelectedIndex = 0;
                            ddlPageContentType.SelectedIndex = 0;
                            _sqlCommand.CommandTimeout = 600;
                            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                            _datatable = new DataTable();
                            _sqlDataAdapter.Fill(_datatable);
                            if (_datatable.Rows.Count > 0)
                            {
                                ddlPage.SelectedValue = _datatable.Rows[0]["content_page"].ToString();
                                ddlPageContentType.SelectedValue = _datatable.Rows[0]["content_type"].ToString();

                                txtTitle.Text = _datatable.Rows[0]["content_title"].ToString();
                                if(txtTitle.Text != null)
                                {
                                    divtitle.Visible = true;
                                }
                                else
                                {
                                    divtitle.Visible = false;
                                }
                                txtContentDescription.Text = _datatable.Rows[0]["content_description"].ToString();
                               
                                btnUpdate.Visible = true;
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                txtTitle.Text = txtContentDescription.Text = string.Empty;
                                ddlPage.SelectedIndex = 0;
                                ddlPageContentType.SelectedIndex = 0;
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
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPage.SelectedValue == "" || ddlPage.SelectedValue == null || ddlPage.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlPageContentType.SelectedValue == "" || ddlPageContentType.SelectedValue == null || ddlPageContentType.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else if (txtContentDescription.Text == "" || txtContentDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    updateContentMaster(Convert.ToInt32(HdnFHomeContentMasterId.Value));


                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFHomeContentMasterId.Value != null && hdnfStatusTestimonial.Value != null)
                {
                    checkAndActive_DeactiveContentMaster(Convert.ToInt32(HdnFHomeContentMasterId.Value), Convert.ToInt32(hdnfStatusTestimonial.Value));
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

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTitle.Text = txtContentDescription.Text = string.Empty;
            if (ddlPage.SelectedValue == "Select")
            {
                lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
            }
            else if (ddlPage.SelectedValue == "home")
            {
                divtitle.Visible = false;
                lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
            }
            else
            {
                divtitle.Visible = true;
                if (ddlPage.SelectedValue == "meetme")
                {
                    lblTitle.Text = "Meet Me Heading <span class="+ "text-danger" + ">*</span>";
                    lblDescription.Text = "Meet Me Description <span class=" + "text-danger" + ">*</span>";
                }
                else if (ddlPage.SelectedValue == "ourvision")
                {
                    lblTitle.Text = "OurVision Heading <span class=" + "text-danger" + ">*</span>";
                    lblDescription.Text = "OurVision Description <span class=" + "text-danger" + ">*</span>";
                }
                else
                {
                    HdnFHomeContentMasterId.Value = "0";
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                }
               
            }
        }

        protected void ddlPageContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTitle.Text = txtContentDescription.Text = string.Empty;
            if (ddlPage.SelectedValue == "Select")
            {
                lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
            }
            else if (ddlPageContentType.SelectedValue == "Select")
            {
                lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
            }
            else if (ddlPage.SelectedValue == "home")
            {
                divtitle.Visible = false;
                lblTitle.Text = "Heading <span class=" + "text-danger" + ">*</span>";
                lblDescription.Text = "Description <span class=" + "text-danger" + ">*</span>";
                if (ddlPage.SelectedValue == "home" && ddlPageContentType.SelectedValue == "meetme")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }
                    lblDescription.Text = "Meet Me Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (ddlPage.SelectedValue == "home" && ddlPageContentType.SelectedValue == "ourvision")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }

                    lblDescription.Text = "OurVision Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (ddlPage.SelectedValue == "home" && ddlPageContentType.SelectedValue == "footeraboutus")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }

                    lblDescription.Text = "About Us Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (ddlPage.SelectedValue == "home" && ddlPageContentType.SelectedValue == "footersubscribe")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }

                    lblDescription.Text = "Subscribe Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }

                else
                {

                }
            }
            else
            {
                divtitle.Visible = true;
                if (ddlPage.SelectedValue == "meetme" && ddlPageContentType.SelectedValue == "meetme")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtTitle.Text = _dataTableSiteContentList.Rows[_index]["content_title"].ToString();
                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }
                    lblTitle.Text = "Meet Me Heading <span class=" + "text-danger" + ">*</span>";
                    lblDescription.Text = "Meet Me Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else if (ddlPage.SelectedValue == "ourvision" && ddlPageContentType.SelectedValue == "ourvision")
                {
                    DataTable _dataTableSiteContentList = Session[SessionNames.AllSiteContentPageList] as DataTable;
                    for (int _index = 0; _index < _dataTableSiteContentList.Rows.Count; _index++)
                    {
                        if (_dataTableSiteContentList.Rows[_index]["content_page"].ToString() == ddlPage.SelectedValue.ToString() && _dataTableSiteContentList.Rows[_index]["content_type"].ToString() == ddlPageContentType.SelectedValue.ToString())
                        {

                            txtTitle.Text = _dataTableSiteContentList.Rows[_index]["content_title"].ToString();
                            txtContentDescription.Text = _dataTableSiteContentList.Rows[_index]["content_description"].ToString();
                            HdnFHomeContentMasterId.Value = _dataTableSiteContentList.Rows[_index]["home_content_master_id"].ToString();
                        }
                    }
                    lblTitle.Text = "OurVision Heading <span class=" + "text-danger" + ">*</span>";
                    lblDescription.Text = "OurVision Description <span class=" + "text-danger" + ">*</span>";
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                }
                else
                {
                    txtTitle.Text =  txtContentDescription.Text = string.Empty;
                    HdnFHomeContentMasterId.Value = "0";
                    btnSubmit.Visible = true;
                    btnUpdate.Visible = false;
                }

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text = txtContentDescription.Text = string.Empty;
                ddlPage.SelectedIndex = 0;
                ddlPageContentType.SelectedIndex = 0;
                HdnFHomeContentMasterId.Value = "0";
              
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        #endregion


        /// <summary>
        /// This Method is Used to Save data
        /// </summary>
        private void addContentMaster()
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
                        _sqlCommand.CommandText = @"insert into HomeContentMaster(content_page,content_type,content_title,content_description,created_by,modified_by,is_active) values (@content_page, @content_type,@content_title,@content_description,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@content_page", ddlPage.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@content_type", ddlPageContentType.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@content_title", txtTitle.Text);
                        _sqlCommand.Parameters.AddWithValue("@content_description", txtContentDescription.Text);
                        _sqlCommand.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@is_active", 1);

                        int _outputCount = _sqlCommand.ExecuteNonQuery();
                        if (_outputCount > 0)
                        {

                            try
                            {
                                txtTitle.Text = txtContentDescription.Text = string.Empty;
                                ddlPage.SelectedIndex = 0;
                                ddlPageContentType.SelectedIndex = 0;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);
                                getAllContentMasterList();
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
        /// This Method is Used to Save data
        /// </summary>
        private void updateContentMaster(int _homeContent_master_id)
        {
            try
            {
                
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from HomeContentMaster where home_content_master_id=" + _homeContent_master_id + "";
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

                                string _queryUpdate = "update HomeContentMaster set content_page='" + ddlPage.SelectedValue.ToString() + "',content_type='" + ddlPageContentType.SelectedValue.ToString() + "',content_title='" + txtTitle.Text + "',content_description='" + txtContentDescription.Text + "' where home_content_master_id=" + _homeContent_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            try
                                            {
                                                txtTitle.Text = txtContentDescription.Text = string.Empty;
                                                ddlPage.SelectedIndex = 0;
                                                ddlPageContentType.SelectedIndex = 0;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                getAllContentMasterList();
                                            }
                                            catch (Exception ex)
                                            {
                                                ex.Message.ToString();
                                            }


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
        /// This Method is Used to Get All Testimonial data
        /// </summary>
        private void getAllContentMasterList()
        { 
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from HomeContentMaster";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            Session[SessionNames.AllSiteContentPageList] = _datatable; 
                            rptrContentMasterList.DataSource = _datatable;
                            rptrContentMasterList.DataBind();
                        }
                        else
                        {
                            rptrContentMasterList.DataSource = null;
                            rptrContentMasterList.DataBind();
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
        /// This Method is Used to Check Content Master valid or Not and Set Content Master Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveContentMaster(int _testimonial_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from HomeContentMaster where home_content_master_id=" + _testimonial_master_id + "";
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
                                string _queryUpdate = "update HomeContentMaster set is_active=" + _isStatus + " where home_content_master_id=" + _testimonial_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            getAllContentMasterList();
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