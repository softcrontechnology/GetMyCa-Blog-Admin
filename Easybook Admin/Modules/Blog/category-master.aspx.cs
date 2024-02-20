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

namespace Healing2Peace.Modules.Blog
{
    public partial class category_master : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
       
        string _getAllBlogCategories = apiconstant.GET_ALL_CATEGORIES_FOR_ADMIN;

        BlogMasterModel _objNewsArticle = new BlogMasterModel();
        DataTable dtFilter;

        SqlConnection _sqlConnection, _sqlConnection2, _sqlConnection3, _sqlConnection4 = new SqlConnection();
        SqlCommand _sqlCommand, _sqlCommand2, _sqlCommand3, _sqlCommand4 = new SqlCommand();
        SqlDataAdapter _sqlDataAdapter, _sqlDataAdapter2 = null;


        DataTable _datatable, _datatable_lstMenuMaster, _datatable_lstSubMenuList, _datatable_lstMenuMasterRonly = null;

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
                    string _AppUserRole = Session[Constants.UserType].ToString();
                   
                    
                    if (_AppUserRole.ToLower() == "admin")
                    {
                        //getAllBlogCategoriesList();
                        getAllBlogCategoryListByADO();
                    }
                    else
                    {
                        Response.Redirect("/dashboard", false);
                    }

                    if (_AppUserRole.ToLower() == "admin")
                    {
                        menutags.Visible = true;
                        //memusubscribe.Visible = true;
                    }
                    else
                    {
                        menutags.Visible = false;
                        //memusubscribe.Visible = false;
                    }
                }

            }
            else
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text == "" || txtCategory.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else if (ddlCategoryType.SelectedValue == "" || ddlCategoryType.SelectedValue == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else
                {
                    addBlogCateroires();
                   
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
                if (HdnFBlogCategoriesMasterId.Value != null && hdnfStatusBlogCategory.Value != null)
                {
                    checkAndActive_DeactiveBlogCategoryData(Convert.ToInt32(HdnFBlogCategoriesMasterId.Value), Convert.ToInt32(hdnfStatusBlogCategory.Value));
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
                if (txtCategory.Text == "" || txtCategory.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else if (ddlCategoryType.SelectedValue == "" || ddlCategoryType.SelectedValue == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else
                {
                    updateBlogCategory(Convert.ToInt32(HdnFBlogCategoriesMasterId.Value));
                    
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCategory.Text = string.Empty;
            ddlCategoryType.SelectedIndex = 0;
            btnUpdate.Visible = false;
            btnSubmit.Visible = true;
        }

        #region Get All Blog categories Details BY API and ADO
        /// <summary>
        /// Get All Blog categories Method is Used to Get All Blog categories Information or List for Bind Repeater or many more
        /// </summary>
        private void getAllBlogCategoriesList()
        {
            string strUrl = string.Format(requestUrl + _getAllBlogCategories); //api call name 
            try
            {

                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(strUrl);
                    string postData = "";
                    //string postData = JsonConvert.SerializeObject(appUserMixedModel);
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.ContentLength = data.Length; 
                    request.Method = "GET";
                    request.Headers.Add("Custom-key", Custom_key);
                    //string Token = "Bearer " + Session["Token"].ToString();
                    //  request.Headers.Add("Authorization", Token);
                    request.UserAgent = Request.UserAgent.ToString();

                    request.MediaType = "application/json";
                    request.Accept = "*";
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                    //byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                    request.Timeout = 500000;             //Increase timeout   for testing

                    request.ContentType = "application/x-www-form-urlencoded";

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var responseOutput = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (responseOutput.responseMsg.isError != true)
                    {
                        dynamic listBlogCategoryModel = new ExpandoObject();

                        List<BlogCategoryModel> _lstBlogCategoryModel = new List<BlogCategoryModel>();

                        listBlogCategoryModel = responseOutput.responseData.data;

                        string jsonString = JsonConvert.SerializeObject(listBlogCategoryModel);

                        _lstBlogCategoryModel = JsonConvert.DeserializeObject<List<BlogCategoryModel>>(jsonString);

                        Session["allcategories"] = _lstBlogCategoryModel.Where(x => x.category_name != "All").ToList(); ;

                        rptrBlogCategoriesList.DataSource = _lstBlogCategoryModel.Where(x => x.category_name != "All").ToList();  /* All is Static Category send by api code its blog_category_id=0 */
                        rptrBlogCategoriesList.DataBind();
                        if (_lstBlogCategoryModel.Count > 0)
                        {
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('No Record Found','success');", true);
                        }
                    }
                    else
                    {
                        string _warningMessage = responseOutput.responseMsg.errorMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _warningMessage + "','danger');", true);

                    }
                }
                catch (WebException ex)
                {
                    string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);

            }

        }


        /// <summary>
        /// This Method is Used to Get All Blog-Category List data By using ADO
        /// </summary>
        private void getAllBlogCategoryListByADO()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogCategory order by blog_category_id desc";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            List<BlogCategoryModel> _lstBlogCategoryModel = new List<BlogCategoryModel>();
                            try
                            {
                                _lstBlogCategoryModel = (from DataRow dr in _datatable.Rows
                                                         select new BlogCategoryModel()
                                                         {
                                                             blog_category_id = Convert.ToInt32(dr["blog_category_id"]),
                                                             guid = (Guid)dr["guid"],
                                                             category_name = dr["category_name"].ToString(),
                                                             category_type = dr["category_type"].ToString(),
                                                             created_on = Convert.ToDateTime(dr["created_on"]),
                                                             created_by = (int)dr["created_by"],
                                                             modified_on = Convert.ToDateTime(dr["modified_on"]),
                                                             modified_by = (int)dr["created_by"],
                                                             is_active = (Boolean)dr["is_active"]

                                                         }).ToList();


                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            Session["allcategories"] = _lstBlogCategoryModel.OrderByDescending(x => x.blog_category_id).ToList();

                            rptrBlogCategoriesList.DataSource = _lstBlogCategoryModel.OrderByDescending(x => x.blog_category_id).ToList(); 
                            rptrBlogCategoriesList.DataBind();

                        }
                        else
                        {
                            rptrBlogCategoriesList.DataSource = null;
                            rptrBlogCategoriesList.DataBind();
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

        #endregion


        private void addBlogCateroires()
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
                        _sqlCommand.CommandText = @"insert into BlogCategory(category_name,category_type,created_by,modified_by,is_active) values (@category_name,@category_type,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@category_name", txtCategory.Text);
                        _sqlCommand.Parameters.AddWithValue("@category_type", ddlCategoryType.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@is_active", 1);

                        int _outputCount = _sqlCommand.ExecuteNonQuery();
                        if (_outputCount > 0)
                        {
                             txtCategory.Text = string.Empty;
                            btnUpdate.Visible = false;
                            btnSubmit.Visible = true;
                            try
                            {
                                // getAllBlogCategoriesList();
                                getAllBlogCategoryListByADO();
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess();", true);


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
            }
        }

        /// <summary>
        /// This Method is Used to Update Category
        /// </summary>
        private void updateBlogCategory(int _blogCategory_id)
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogCategory where blog_category_id=" + _blogCategory_id + "";
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

                                string _queryUpdate = "update BlogCategory set category_name='" + txtCategory.Text + "',category_type='" + ddlCategoryType.SelectedValue.ToString() + "' where blog_category_id=" + _blogCategory_id + "";
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
                                                txtCategory.Text =  string.Empty;
                                                ddlCategoryType.SelectedIndex = 0;
                                                btnUpdate.Visible = false;
                                                btnSubmit.Visible = true;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                // getAllBlogCategoriesList();
                                                getAllBlogCategoryListByADO();
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

        protected void rptrBlogCategoriesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                    Image lblimageurl = (Image)e.Item.FindControl("lblimageurl");
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

        protected void rptrBlogCategoriesList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                string commandname = e.CommandName;
                string id = e.CommandArgument.ToString();
                HdnFBlogCategoriesMasterId.Value = id;

                Int32 ID = Convert.ToInt32(id);
                switch (commandname)
                {
                    case "active":
                        try
                        {
                            Int32 menuMasterId = Convert.ToInt32(id);
                            HdnFBlogCategoriesMasterId.Value = id;
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
                                hdnfStatusBlogCategory.Value = _UserStatus.ToString();
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
                    case "edit":
                        try
                        {
                            Int32 BlogCategoryMasterId = Convert.ToInt32(id);
                            HdnFBlogCategoriesMasterId.Value = id;
                          
                            List<BlogCategoryModel> _lstBlogCategoryModel = Session["allcategories"] as List<BlogCategoryModel>;
                            BlogCategoryModel _ObjBlogCategoryModel = new BlogCategoryModel();

                            _ObjBlogCategoryModel = _lstBlogCategoryModel.Where(x => x.blog_category_id == BlogCategoryMasterId).FirstOrDefault();
                            if (_ObjBlogCategoryModel.blog_category_id >0)
                            {
                                ddlCategoryType.SelectedValue = _ObjBlogCategoryModel.category_type.ToString();
                                txtCategory.Text = _ObjBlogCategoryModel.category_name;

                                btnUpdate.Visible = true;
                                btnSubmit.Visible = false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
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
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
           


        }


        /// <summary>
        /// This Method is Used to Check BlogCategory Data valid or Not and Set BlogCategory Data Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveBlogCategoryData(int _blogCatgeory_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogCategory where blog_category_id=" + _blogCatgeory_master_id + "";
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
                                string _queryUpdate = "update BlogCategory set is_active=" + _isStatus + " where blog_category_id=" + _blogCatgeory_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            checkAndActive_DeactiveBlogByBlogCategoryId(Convert.ToInt32(HdnFBlogCategoriesMasterId.Value), Convert.ToInt32(hdnfStatusBlogCategory.Value));

                                            // getAllBlogCategoriesList();
                                            getAllBlogCategoryListByADO();
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


        /// <summary>
        /// This Method is Used to Check Blog Data valid or Not and Set Blog Data Active/Deactive By BlogCategoryId
        /// </summary>
        private void checkAndActive_DeactiveBlogByBlogCategoryId(int _blogCatgeory_master_id, int _isStatus) 
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection3 = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster where blog_category_id=" + _blogCatgeory_master_id + "";
                    _sqlConnection3.Open();
                    using (SqlCommand _sqlCommand3 = new SqlCommand(_query, _sqlConnection3))
                    {

                        _sqlCommand3.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand3);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            _sqlConnection3.Close();


                            using (SqlConnection _sqlConnection4 = new SqlConnection(_connectionstring))
                            {
                                string _queryUpdate = "update BlogMaster set is_active=" + _isStatus + " where blog_category_id=" + _blogCatgeory_master_id + "";
                                using (SqlCommand _sqlCommand4 = new SqlCommand(_queryUpdate, _sqlConnection4))
                                {
                                    _sqlConnection4.Open();

                                    _sqlCommand4.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand4.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {   

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
                                    _sqlConnection4.Close();
                                }
                            }
                        }
                        else
                        {
                            

                        }

                        _sqlConnection3.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection3.Close();
                _sqlConnection4.Close();

            }
        }


    }
}