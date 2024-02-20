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
    public partial class testimonial_master : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
        string imageUrl = ConfigurationManager.AppSettings["webapibaseimgurl"].ToString();
        string blogBaseURL = ConfigurationManager.AppSettings["BlogBaseURL"].ToString();

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
                    getAllTestimonial();

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

        #region Action Buttons (Add,Update,Reset)
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtTestimonialTitle.Text == "" || txtTestimonialTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
               
                else if (txtTestimonialDescription.Text == "" || txtTestimonialDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    updateTestimonial(Convert.ToInt32(HdnFTestimonialMasterId.Value));

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
                txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text  = string.Empty;
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtTestimonialTitle.Text == "" || txtTestimonialTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                
                else if (txtTestimonialDescription.Text == "" || txtTestimonialDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    if (fuUploadedFile.HasFile)
                    {
                        string filename = Path.GetFileName(fuUploadedFile.PostedFile.FileName);
                        string contentType = fuUploadedFile.PostedFile.ContentType;
                        string fileUploadFile = Path.GetExtension(fuUploadedFile.FileName.ToString());
                        if (fileUploadFile.ToLower() == ".png" || fileUploadFile.ToLower() == ".jpeg" || fileUploadFile.ToLower() == ".jpg")
                        {
                            Random _randomNumber = new Random();
                            string fileName = "testimonial_" + _randomNumber.Next() + fileUploadFile;
                            if (fileName != null)
                            {
                                addTestimonial(fileName);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('Please upload Testimonial','warning');", true);

                    }


                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        #endregion

        #region Repeater
        protected void rptrTestimonialList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblpic = (Label)e.Item.FindControl("lblpic");
                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                    LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");
                    Image lblimageurl = (Image)e.Item.FindControl("lblimageurl");
                    string url;
                    if (string.IsNullOrEmpty(lblpic.Text))
                    {
                        //url = HttpContext.Current.Server.MapPath("~/assets/fraud/") + "default-image.jpg";
                        url = "/Content/img/placeholders/avatars/avatar2.jpg";
                    }
                    else
                    {
                        string fullpath = "/assets/testimonial/" + lblpic.Text;
                        url = fullpath;
                    }
                    lblimageurl.ImageUrl = url;

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

        protected void rptrTestimonialList_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                            HdnFTestimonialMasterId.Value = id;
                            bindValueonTextField(id);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException", true);
                        }
                        break;
                    case "active":
                        try
                        {
                            Int32 menuMasterId = Convert.ToInt32(id);
                            HdnFTestimonialMasterId.Value = id;
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                        }
                        break;
                    case "delete":
                        try
                        {
                           
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFTestimonialMasterId.Value != null && hdnfStatusTestimonial.Value !=null)
                {
                    checkAndActive_DeactiveTestimonial(Convert.ToInt32(HdnFTestimonialMasterId.Value), Convert.ToInt32(hdnfStatusTestimonial.Value));
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

        #endregion

        /// <summary>
        /// This Method is Used to Get All Testimonial data
        /// </summary>
        private void getAllTestimonial()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from TestimonialMaster order by testimonial_master_id desc";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            rptrTestimonialList.DataSource = _datatable;
                            rptrTestimonialList.DataBind();
                        }
                        else
                        {
                            rptrTestimonialList.DataSource = null;
                            rptrTestimonialList.DataBind();
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
        /// This Method is Used to Bind or Fill Values on Text Field
        /// </summary>
        private void bindValueonTextField(string _testimonialMasterId)
        {
            try
            {
                try
                {

                    string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                    using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                    {
                        string _query = "select * from TestimonialMaster where testimonial_master_id=" + _testimonialMasterId;
                        _sqlConnection.Open();
                        using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                        {
                            txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                            _sqlCommand.CommandTimeout = 600;
                            _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                            _datatable = new DataTable();
                            _sqlDataAdapter.Fill(_datatable);
                            if (_datatable.Rows.Count > 0)
                            {
                                txtName.Text = _datatable.Rows[0]["name"].ToString();
                                txtTestimonialTitle.Text = _datatable.Rows[0]["testimonial_title"].ToString();
                                txtTestimonialDescription.Text = _datatable.Rows[0]["testimonial_description"].ToString();
                                imagePreview.ImageUrl = "/assets/testimonial/" + _datatable.Rows[0]["image"].ToString();

                                btnUpdate.Visible = true;
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
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


        /// <summary>
        /// This Method is Used to Save data
        /// </summary>
        private void addTestimonial(string _testimonialfileName)
        {
            try
            {
                string _TestimonialFileName = string.Empty;
                _TestimonialFileName = _testimonialfileName;
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {

                    using (SqlCommand _sqlCommand = new SqlCommand())
                    {
                        _sqlConnection.Open();

                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = @"insert into TestimonialMaster(name,testimonial_title,testimonial_description,image,created_by,modified_by,is_active) values (@name, @testimonial_title,@testimonial_description,@image,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                        _sqlCommand.Parameters.AddWithValue("@testimonial_title", txtTestimonialTitle.Text);
                        _sqlCommand.Parameters.AddWithValue("@testimonial_description", txtTestimonialDescription.Text);
                        _sqlCommand.Parameters.AddWithValue("@image", _TestimonialFileName);
                        _sqlCommand.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@is_active", 1);

                        int _outputCount = _sqlCommand.ExecuteNonQuery();
                        if (_outputCount > 0)
                        {

                            try
                            {
                                if (fuUploadedFile.HasFile)
                                {
                                    string TESTIMONIALNAME = _TestimonialFileName;

                                    string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                    string filelUpload = fuUploadedFile.FileName.ToString();
                                    if (fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg")
                                    {

                                        if (!Directory.Exists(Server.MapPath("~/assets/testimonial/")))
                                        {
                                            Directory.CreateDirectory(Server.MapPath("~/assets/testimonial/"));
                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                            txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                                            getAllTestimonial();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);

                                        }
                                        else
                                        {

                                            if (File.Exists(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME))
                                            {
                                                File.Delete(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                            }

                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                            txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                                            getAllTestimonial();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image not selected','warning');", true);
                                }
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
        private void updateTestimonial(int _testimonial_master_id)
        {
            try
            {
                string _TestimonialFileName = string.Empty;

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from TestimonialMaster where testimonial_master_id=" + _testimonial_master_id + "";
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


                            _TestimonialFileName = _datatable.Rows[0]["image"].ToString();
                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {

                                string _queryUpdate = "update TestimonialMaster set name='" + txtName.Text + "',testimonial_title='" + txtTestimonialTitle.Text + "',testimonial_description='" + txtTestimonialDescription.Text + "',image='" + _TestimonialFileName + "' where testimonial_master_id=" + _testimonial_master_id + "";
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
                                                if (fuUploadedFile.HasFile)
                                                {
                                                    string TESTIMONIALNAME = _TestimonialFileName;

                                                    string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                                    string filelUpload = fuUploadedFile.FileName.ToString();
                                                    if (fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg")
                                                    {

                                                        if (!Directory.Exists(Server.MapPath("~/assets/testimonial/")))
                                                        {
                                                            Directory.CreateDirectory(Server.MapPath("~/assets/testimonial/"));
                                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                                            txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                                                            btnUpdate.Visible = false;
                                                            btnSubmit.Visible = true;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            getAllTestimonial();
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);

                                                        }
                                                        else
                                                        {

                                                            if (File.Exists(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME))
                                                            {
                                                                File.Delete(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                                            }

                                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/testimonial/") + TESTIMONIALNAME);
                                                            txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                                                            btnUpdate.Visible = false;
                                                            btnSubmit.Visible = true;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            getAllTestimonial();
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                                                    }

                                                }
                                                else
                                                {
                                                    txtName.Text = txtTestimonialTitle.Text = txtTestimonialDescription.Text = string.Empty;
                                                    btnUpdate.Visible = false;
                                                    btnSubmit.Visible = true;
                                                    imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                    getAllTestimonial();
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                }
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
        /// This Method is Used to Check User valid or Not and Set App User Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveTestimonial(int _testimonial_master_id, int _isStatus) 
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from TestimonialMaster where testimonial_master_id=" + _testimonial_master_id + "";
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
                                string _queryUpdate = "update TestimonialMaster set is_active=" + _isStatus + " where testimonial_master_id=" + _testimonial_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            getAllTestimonial();
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