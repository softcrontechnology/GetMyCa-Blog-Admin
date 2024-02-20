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
    public partial class blog_master : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
        string imageUrl = ConfigurationManager.AppSettings["webapibaseimgurl"].ToString();
        string blogBaseURL = ConfigurationManager.AppSettings["BlogBaseURL"].ToString();

        string _addBlog = apiconstant.ADD_NEW_BLOGS;
        string _updateBlog = apiconstant.UPDATE_BLOGS;
        string _getAllBlog = apiconstant.GET_ALL_BLOGS_FOR_ADMIN;
        string _setFeaturedBlog = apiconstant.SET_FEATURED_BLOGS;
        string _setPublishBlog = apiconstant.PUBLISH_BLOGS;
        string _getAllBlogCategories = apiconstant.GET_ALL_CATEGORIES_FOR_ADMIN;
        string _setBlogActiveDeactive = apiconstant.SET_STATUS_ACTIVE_DEACTIVE_BLOGS;

        BlogMasterModel _objBlogsArticle = new BlogMasterModel();
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
                    //getAllBlogList();
                   // getAllBlogCategoriesList();
                    getAllBlog();
                    getAllBlogCategoryList();

                    string _AppUserRole = Session[Constants.UserType].ToString();
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
                if (txtBlogTitle.Text == "" || txtBlogTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                //else if (txtfriendlyUrl.Text == "" || txtfriendlyUrl.Text == null)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                //}
                else if (ddlBlogCategoryType.SelectedValue == "Select" || ddlBlogCategoryType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtAuthorName.Text == "" || txtAuthorName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtBlogDescription.Text == "" || txtBlogDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else
                {
                    if (fuUploadedFile.HasFile && fuImageThumbail.HasFile && fuInner.HasFile)
                    {
                        //SaveBlog(getBlogFiledForAdd(true), true);
                        //btnSubmit.Visible = true;
                        //btnUpdate.Visible = false;

                        string filename = Path.GetFileName(fuUploadedFile.PostedFile.FileName);
                        string contentType = fuUploadedFile.PostedFile.ContentType;

                        string fileUploadFile = Path.GetExtension(fuUploadedFile.FileName.ToString());
                        string thumbnail_fileuploadfile = Path.GetExtension(fuImageThumbail.FileName.ToString());
                        string inner_fileuploadfile = Path.GetExtension(fuInner.FileName.ToString());

                        if ((fileUploadFile.ToLower() == ".png" || fileUploadFile.ToLower() == ".jpeg" || fileUploadFile.ToLower() == ".jpg") && (thumbnail_fileuploadfile.ToLower() == ".png" || thumbnail_fileuploadfile.ToLower() == ".jpeg" || thumbnail_fileuploadfile.ToLower() == ".jpg") && (inner_fileuploadfile.ToLower() == ".png" || inner_fileuploadfile.ToLower() == ".jpeg" || inner_fileuploadfile.ToLower() == ".jpg"))
                        {
                            Random _randomNumber = new Random();
                            string _blog_outer_FileName = "blogouter_" + _randomNumber.Next() + fileUploadFile;
                            string _blog_thumbnail_fileName = "blogthumbnail_" + _randomNumber.Next() + thumbnail_fileuploadfile;
                            string _blog_iiner_fileName = "bloginner_" + _randomNumber.Next() + inner_fileuploadfile;
                            if (_blog_outer_FileName != null && _blog_thumbnail_fileName !=null && _blog_iiner_fileName !=null)
                            {
                                addBlog(_blog_outer_FileName, _blog_thumbnail_fileName, _blog_iiner_fileName);
                                btnSubmit.Visible = true;
                                btnUpdate.Visible = false;
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('Please Select Category Image','danger');", true);
                    }


                }
            }
            catch (Exception ex)
            {
                string _ExceptionMessage =  ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _ExceptionMessage + "','danger');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBlogTitle.Text == "" || txtBlogTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtfriendlyUrl.Text == "" || txtfriendlyUrl.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (ddlBlogCategoryType.SelectedValue == "Select" || ddlBlogCategoryType.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtAuthorName.Text == "" || txtAuthorName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtBlogDescription.Text == "" || txtBlogDescription.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else
                {
                    if (fuUploadedFile.HasFile || fuImageThumbail.HasFile || fuInner.HasFile)
                    {
                        string filename = Path.GetFileName(fuUploadedFile.PostedFile.FileName);
                        string contentType = fuUploadedFile.PostedFile.ContentType;

                        string fileUploadFile = Path.GetExtension(fuUploadedFile.FileName.ToString());
                        string thumbnail_fileuploadfile = Path.GetExtension(fuImageThumbail.FileName.ToString());
                        string inner_fileuploadfile = Path.GetExtension(fuInner.FileName.ToString());

                        if ((fileUploadFile.ToLower() == ".png" || fileUploadFile.ToLower() == ".jpeg" || fileUploadFile.ToLower() == ".jpg") || (thumbnail_fileuploadfile.ToLower() == ".png" || thumbnail_fileuploadfile.ToLower() == ".jpeg" || thumbnail_fileuploadfile.ToLower() == ".jpg") || (inner_fileuploadfile.ToLower() == ".png" || inner_fileuploadfile.ToLower() == ".jpeg" || inner_fileuploadfile.ToLower() == ".jpg"))
                        {

                            updateBlogMaster(Convert.ToInt32(HdnFBlogMasterId.Value));
                            btnSubmit.Visible = true;
                            btnUpdate.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                        }
                    }
                    else
                    {
                        updateBlogMaster(Convert.ToInt32(HdnFBlogMasterId.Value));
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }
                    //SaveBlog(getBlogFiledForAdd(false), false);

                    


                }
            }
            catch (Exception ex)
            {
                string _ExceptionMessage = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _ExceptionMessage + "','danger');", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
            ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
            InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
            List<BlogCategoryModel> _lstAllTag = Session["alltag"] as List<BlogCategoryModel>;
            try
            {
                ddlTagList.DataSource = _lstAllTag;
                ddlTagList.DataTextField = "category_name";
                ddlTagList.DataValueField = "blog_category_id";
                ddlTagList.DataBind();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void rptrBlogList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string commandname = e.CommandName;
            string id = e.CommandArgument.ToString();
            HdnFBlogMasterId.Value = id;
            switch (commandname)
            {
                case "edit":
                    try
                    {
                        Int32 BlogMasterId = Convert.ToInt32(id);
                        HdnFBlogMasterId.Value = id;
                        List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;
                        BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

                        _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == BlogMasterId).FirstOrDefault();

                        ddlBlogCategoryType.SelectedValue = _ObjBlogMasterModel.blog_category_id.ToString();
                        txtBlogTitle.Text = _ObjBlogMasterModel.blog_title;
                        txtfriendlyUrl.Text = _ObjBlogMasterModel.blog_friendly_url;
                        txtBlogDescription.Text = _ObjBlogMasterModel.blog_description;
                        txtAuthorName.Text = _ObjBlogMasterModel.author_name;
                        txtBlogViews.Text = _ObjBlogMasterModel.blog_view.ToString();
                        ddlSetFeatured.SelectedValue = _ObjBlogMasterModel.is_featured == true ? "1" : "0";
                        txtPageTitle.Text = _ObjBlogMasterModel.page_title.ToString();
                        txtMetaKey.Text = _ObjBlogMasterModel.meta_Key.ToString();
                        txtMetaDescription.Text = _ObjBlogMasterModel.meta_description.ToString();

                        try
                        {
                            if (string.IsNullOrEmpty(_ObjBlogMasterModel.blog_outer_image))
                            {
                                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                            }
                            else
                            {
                                imagePreview.ImageUrl = blogBaseURL + _ObjBlogMasterModel.blog_outer_image;
                            }
                        }
                        catch(Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        try
                        {
                            if (string.IsNullOrEmpty(_ObjBlogMasterModel.blog_thumbnail))
                            {
                                ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                            }
                            else
                            {
                                ThumbnailimagePreview.ImageUrl =  blogBaseURL + _ObjBlogMasterModel.blog_thumbnail;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        try
                        {
                            if (string.IsNullOrEmpty(_ObjBlogMasterModel.blog_inner_banner_img))
                            {
                                InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                            }
                            else
                            {
                                InnerimagePreview.ImageUrl = blogBaseURL + _ObjBlogMasterModel.blog_inner_banner_img;
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }




                        try
                        {
                            List<BlogCategoryModel> _lstTagBlogCategoryModel = Session["alltag"] as List<BlogCategoryModel>;
                            try
                            {
                                if (_lstTagBlogCategoryModel != null)
                                {
                                    ddlTagList.DataSource = _lstTagBlogCategoryModel;
                                    ddlTagList.DataTextField = "category_name";
                                    ddlTagList.DataValueField = "blog_category_id";
                                    ddlTagList.DataBind();

                                    BlogCategoryModel _objTagBlogCategoryModel = new BlogCategoryModel();

                                    if (!string.IsNullOrEmpty(_ObjBlogMasterModel.blog_tag_id))
                                    {
                                        string lasttm = _ObjBlogMasterModel.blog_tag_id.TrimEnd(',');
                                        string[] arrOfSelections = lasttm.Split(',');
                                        foreach (string value in arrOfSelections)
                                        {

                                            _objTagBlogCategoryModel = _lstTagBlogCategoryModel.Where(x => x.blog_category_id == Convert.ToInt32(value)).FirstOrDefault();

                                            if (_objTagBlogCategoryModel == null)
                                            {

                                            }
                                            else
                                            {
                                                ddlTagList.Items.Add(new ListItem() { Text = _objTagBlogCategoryModel.category_name, Value = value, Selected = true });
                                            }


                                        }
                                    }
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
                        catch(Exception ex)
                        {
                            ex.Message.ToString();
                        }


                        btnUpdate.Visible = true;
                        btnSubmit.Visible = false;

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
                        Int32 BlogMasterId = Convert.ToInt32(id);
                        HdnFBlogMasterId.Value = id;
                        Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                        List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;
                        BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

                        _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == BlogMasterId).FirstOrDefault();

                        if (_ObjBlogMasterModel != null)
                        {
                            if (lblIsActive.Text != null)
                            {
                                int _BlogToggleStatus = 0;
                                if (lblIsActive.Text == "Activated")
                                {
                                    _BlogToggleStatus = 0;

                                }
                                else
                                {
                                    _BlogToggleStatus = 1;

                                }
                                hdnfStatusBlog.Value = _BlogToggleStatus.ToString();
                                HdnFBlogCategoriesMasterId.Value = _ObjBlogMasterModel.blog_category_id.ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showStatusBlogModal();", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                            }
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
                case "isfeatured":
                    try
                    {

                        Int32 BlogMasterId = Convert.ToInt32(id);
                        HdnFBlogMasterId.Value = id;
                        Label lblIsFeatured = (Label)e.Item.FindControl("lblIsFeatured");
                        if (lblIsFeatured.Text != null)
                        {
                            int _featuredStatus = 0;
                            if (lblIsFeatured.Text == "True")
                            {
                                _featuredStatus = 0;

                            }
                            else
                            {
                                _featuredStatus = 1;

                            }
                            hdnfStatusBlog.Value = _featuredStatus.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showfeaturedBlogModal();", true);

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
                case "ispublished":
                    try
                    {
                        Int32 BlogMasterId = Convert.ToInt32(id);
                        HdnFBlogMasterId.Value = id;
                        Label lblIsPublished = (Label)e.Item.FindControl("lblIsPublished");
                        if (lblIsPublished.Text != null)
                        {
                            int _publishStatus = 0;
                            if (lblIsPublished.Text == "True")
                            {
                                _publishStatus = 0;

                            }
                            else
                            {
                                _publishStatus = 1;

                            }
                            hdnfStatusBlog.Value = _publishStatus.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showPublishBlogModal();", true);

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
                default:
                    break;
            }
        }

        protected void rptrBlogList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblIsFeatured = (Label)e.Item.FindControl("lblIsFeatured");
                    LinkButton btnFeatured = (LinkButton)e.Item.FindControl("btnFeatured");


                    Label lblpic = (Label)e.Item.FindControl("lblpic");
                    Label lblIsPublished = (Label)e.Item.FindControl("lblIsPublished");
                    LinkButton btnPublished = (LinkButton)e.Item.FindControl("btnPublished");

                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                     Image lblimageurl = (Image)e.Item.FindControl("lblimageurl");
                    LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");
                    string _isfeature = lblIsFeatured.Text;
                    string _isPublished = lblIsPublished.Text;
                    string url;
                    if (string.IsNullOrEmpty(lblpic.Text))
                    {
                        url = "/Content/img/placeholders/avatars/avatar2.jpg";
                    }
                    else
                    {
                        url = blogBaseURL + lblpic.Text;
                    }
                    lblimageurl.ImageUrl = url;

                    if (_isfeature == "True")
                    {
                        btnFeatured.Text = "Yes";
                        btnFeatured.CssClass = "label label-success";
                    }
                    else
                    {
                        btnFeatured.Text = "No";
                        btnFeatured.CssClass = "label label-danger";
                    }

                    if (_isPublished == "True")
                    {
                        btnPublished.Text = "Yes";
                        btnPublished.CssClass = "label label-success";
                    }
                    else
                    {
                        btnPublished.Text = "No";
                        btnPublished.CssClass = "label label-danger";
                    }

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

        #region Get All Blog Details BY API
        /// <summary>
        /// Get All Blog Method is Used to Get All Blog Information or List for Bind Repeater or many more By Using API
        /// </summary>
        private void getAllBlogList()
        {
            string strUrl = string.Format(requestUrl + _getAllBlog); //api call name 
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
                        dynamic listBlogMaster = new ExpandoObject();

                        List<BlogMasterModel> _lstBlogMasterModel = new List<BlogMasterModel>();

                        listBlogMaster = responseOutput.responseData.data;

                        string jsonString = JsonConvert.SerializeObject(listBlogMaster);

                        _lstBlogMasterModel = JsonConvert.DeserializeObject<List<BlogMasterModel>>(jsonString);

                        string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                        if (_loginUserType.ToLower() == "admin")
                        {
                            Session["allblog"] = _lstBlogMasterModel;

                            rptrBlogList.DataSource = _lstBlogMasterModel;
                            rptrBlogList.DataBind();
                        }
                        else
                        {
                            int _APPUSERMASTERID = Convert.ToInt32(Session[Constants.Id]);
                            Session["allblog"] = _lstBlogMasterModel.Where(x => x.created_by == _APPUSERMASTERID).ToList();

                            rptrBlogList.DataSource = _lstBlogMasterModel.Where(x => x.created_by == _APPUSERMASTERID).ToList(); ;
                            rptrBlogList.DataBind();
                        }

                        if (_lstBlogMasterModel.Count > 0)
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

        #endregion

        protected void btnPublishedBlog_Click(object sender, EventArgs e)
        {
            try
            {
                #region Publish Blog By using API
                //if (HdnFBlogMasterId.Value != null)
                //{
                //    List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;
                //    BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

                //    _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == Convert.ToInt32(HdnFBlogMasterId.Value)).FirstOrDefault();

                //    if (_ObjBlogMasterModel != null)
                //    {
                //        if (_ObjBlogMasterModel.is_published == true)
                //        {
                //            _ObjBlogMasterModel.is_published = false;
                //        }
                //        else
                //        {
                //            _ObjBlogMasterModel.is_published = true;
                //        }
                //        SetPublishBlog(_ObjBlogMasterModel);

                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
                //}
                #endregion

                try
                {
                    if (HdnFBlogMasterId.Value != null && hdnfStatusBlog.Value != null)
                    {
                        checkAndPublishYesNOBlog(Convert.ToInt32(HdnFBlogMasterId.Value), Convert.ToInt32(hdnfStatusBlog.Value));
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
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        protected void lnkbtnFeatured_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFBlogMasterId.Value != null && hdnfStatusBlog.Value != null)
                {
                    checkAndIsFeturedYesNOBlog(Convert.ToInt32(HdnFBlogMasterId.Value), Convert.ToInt32(hdnfStatusBlog.Value));
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


            #region Featured By Model and API
            //try
            //{
            //    if (HdnFBlogMasterId.Value != null)
            //    {
            //        List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;
            //        BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

            //        _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == Convert.ToInt32(HdnFBlogMasterId.Value)).FirstOrDefault();

            //        if (_ObjBlogMasterModel != null)
            //        {
            //            if (_ObjBlogMasterModel.is_featured == true)
            //            {
            //                _ObjBlogMasterModel.is_featured = false;
            //            }
            //            else
            //            {
            //                _ObjBlogMasterModel.is_featured = true;
            //            }
            //            SetFeaturedBlogs(_ObjBlogMasterModel);
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            //}

            #endregion
        }

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                #region Toggle Blog STatus By Using API
                //if (HdnFBlogMasterId.Value != null)
                //{
                //    List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;
                //    BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

                //    _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == Convert.ToInt32(HdnFBlogMasterId.Value)).FirstOrDefault();

                //    if (_ObjBlogMasterModel != null)
                //    {
                //        int _UserStatus = 0;
                //        if (_ObjBlogMasterModel.is_active == true)
                //        {
                //            _ObjBlogMasterModel.is_active = false;
                //            _UserStatus = 0;
                //        }
                //        else
                //        {
                //            _ObjBlogMasterModel.is_active = true;
                //            _UserStatus = 1;
                //        }
                //        hdnfStatusBlog.Value = _UserStatus.ToString();
                //        HdnFBlogCategoriesMasterId.Value = _ObjBlogMasterModel.blog_category_id.ToString();
                //        SetBlogToggleStatus(_ObjBlogMasterModel);

                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
                //}

                #endregion

                try
                {
                    if (HdnFBlogMasterId.Value != null && hdnfStatusBlog.Value != null)
                    {
                        checkAndActive_DeactiveBlog(Convert.ToInt32(HdnFBlogMasterId.Value), Convert.ToInt32(hdnfStatusBlog.Value));
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
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }



        #region Get All Blog categories Details and This Method also used for Get All tag List and bind Dropdown Tag
        /// <summary>
        /// Get All Blog categories Method is Used to Get All Blog categories Information or List for Bind Repeater or many more and This Method also used for Get All tag List and bind Dropdown Tag
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

                        List<BlogCategoryModel> _lstBlogCategory = new List<BlogCategoryModel>();
                        List<BlogCategoryModel> _lstBlogTag = new List<BlogCategoryModel>();

                        _lstBlogCategory = _lstBlogCategoryModel.Where(x => x.is_active == true && x.category_name != "All" && x.category_type == "category").ToList(); /* All is Static Category send by api code its blog_category_id=0 */
                        _lstBlogTag = _lstBlogCategoryModel.Where(x => x.is_active == true && x.category_name != "All" && x.category_type == "tag").ToList(); ;
                       
                        Session["alltag"] = _lstBlogTag;

                        try
                        {
                            ddlBlogCategoryType.DataSource = _lstBlogCategory;
                            ddlBlogCategoryType.DataTextField = "category_name";
                            ddlBlogCategoryType.DataValueField = "blog_category_id";
                            ddlBlogCategoryType.DataBind();
                            ddlBlogCategoryType.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }
                        
                        try
                        {
                            ddlTagList.DataSource = _lstBlogTag;
                            ddlTagList.DataTextField = "category_name";
                            ddlTagList.DataValueField = "blog_category_id";
                            ddlTagList.DataBind();
                           
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        


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


        #endregion


        #region  Add Blog Code
        /// <summary>
        /// Add Blog Method is User to Save or Add Blog Details in Data Base Using Api
        /// </summary>
        /// <param name="_objBlogMaster"></param>
        private void SaveBlog(BlogMasterModel _objBlogMaster, bool isSave)
        {
            string strUrl = string.Empty;

            if (isSave)
            {
                strUrl = string.Format(requestUrl + _addBlog); //api call name 
            }
            else
            {
                strUrl = string.Format(requestUrl + _updateBlog); //api call name 
            }
            try
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

                    string postData = JsonConvert.SerializeObject(_objBlogMaster);
                    //var data = Encoding.ASCII.GetBytes(postData);
                    var data = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = data.Length;
                    request.Method = "POST";
                    request.Headers.Add("Custom-key", Custom_key);
                    //string Token = "Bearer " + Session["Token"].ToString();
                    //  request.Headers.Add("Authorization", Token);
                    request.UserAgent = Request.UserAgent.ToString();

                    request.MediaType = "application/json";
                    request.Accept = "*/*";
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                    //byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                    request.Timeout = 500000;             //Increase timeout for testing

                    request.ContentType = "application/x-www-form-urlencoded";
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var resultSet = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (resultSet.responseMsg.isError == true)
                    {
                        string _warningMessage = resultSet.responseMsg.errorMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _warningMessage + "','danger');", true);
                    }
                    else
                    {
                        string _successMessage = resultSet.responseMsg.successMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _successMessage + "','success');", true);

                        txtAuthorName.Text = txtBlogViews.Text  = txtBlogTitle.Text = txtBlogDescription.Text = txtfriendlyUrl.Text = string.Empty;
                        imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                        getAllBlogList();
                        getAllBlogCategoriesList();
                        //List<BlogMasterModel> _lstAllTag = Session["alltag"] as List<BlogMasterModel>;
                        //try
                        //{
                        //    ddlTagList.DataSource = _lstAllTag;
                        //    ddlTagList.DataTextField = "category_name";
                        //    ddlTagList.DataValueField = "blog_category_id";
                        //    ddlTagList.DataBind();

                        //}
                        //catch (Exception ex)
                        //{
                        //    ex.Message.ToString();
                        //}

                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }

                }
                catch (WebException ex)
                {
                    //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    string message = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + message + "','danger');", true);
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        #endregion


        #region
        /// <summary>
        /// This Method is Used to Add Blog and Get Text Filed Value
        /// </summary>
        private BlogMasterModel getBlogFiledForAdd(bool isSave)
        {
            BlogMasterModel _ObjBlogMasterModel = new BlogMasterModel();

            try
            {
                if (isSave)
                {
                    _ObjBlogMasterModel.blog_master_id = 0;

                    _ObjBlogMasterModel.is_published = false;
                    _ObjBlogMasterModel.created_by = Convert.ToInt32(Session[Constants.Id].ToString());
                    _ObjBlogMasterModel.modified_by = Convert.ToInt32(Session[Constants.Id].ToString());
                }
                else
                {
                    Int32 BlogMasterId = Convert.ToInt32(HdnFBlogMasterId.Value.ToString());
                    List<BlogMasterModel> _lstBlogMasterModel = Session["allblog"] as List<BlogMasterModel>;

                    _ObjBlogMasterModel = _lstBlogMasterModel.Where(x => x.blog_master_id == BlogMasterId).FirstOrDefault();
                    _ObjBlogMasterModel.blog_master_id = BlogMasterId;

                   
                    _ObjBlogMasterModel.modified_by = Convert.ToInt32(Session[Constants.Id].ToString());
                }

                _ObjBlogMasterModel.blog_category_id = Convert.ToInt32(ddlBlogCategoryType.SelectedItem.Value.ToString());
                _ObjBlogMasterModel.category_name = ddlBlogCategoryType.SelectedItem.Text;
                _ObjBlogMasterModel.blog_title = txtBlogTitle.Text;
                _ObjBlogMasterModel.blog_friendly_url = txtfriendlyUrl.Text;
                _ObjBlogMasterModel.blog_description = txtBlogDescription.Text;
                _ObjBlogMasterModel.author_name = txtAuthorName.Text;
                _ObjBlogMasterModel.blog_view = Convert.ToInt32(txtBlogViews.Text);
                _ObjBlogMasterModel.is_featured = Convert.ToBoolean(ddlSetFeatured.SelectedValue == "1" ? true : false);
                
                

                if (ddlTagList.Items.Count > 0)
                {
                    string totalitemTagId = null;
                    string totalitemTagname = null;
                    for (int i = 0; i < ddlTagList.Items.Count; i++)
                    {
                        if (ddlTagList.Items[i].Selected)
                        {
                            string selectedItemId = ddlTagList.Items[i].Value + ",";
                            string selectedItemName = ddlTagList.Items[i].Text + ",";
                            //insert command
                            totalitemTagId = totalitemTagId + selectedItemId;
                            totalitemTagname = totalitemTagname + selectedItemName;
                        }
                    }

                    if (!string.IsNullOrEmpty(totalitemTagId))
                    {
                        string lasttmId = totalitemTagId.TrimEnd(',');
                        _ObjBlogMasterModel.blog_tag_id = lasttmId;
                    }

                    if (!string.IsNullOrEmpty(totalitemTagname))
                    {
                        string lasttmName = totalitemTagname.TrimEnd(',');
                        _ObjBlogMasterModel.tag_name = lasttmName;
                    }

                    
                }

                if (!string.IsNullOrEmpty(txtfriendlyUrl.Text))
                {
                    string _friendlyUrl = null;

                    string lasttm = txtfriendlyUrl.Text.TrimEnd(' ');
                    string[] arrOfSelections = lasttm.Split(' ');
                    for (int i = 0; i < arrOfSelections.Length; i++)
                    {
                        string _subStringFriendlyUrl = arrOfSelections[i]+ "-";
                        //insert command
                        _friendlyUrl = _friendlyUrl + _subStringFriendlyUrl;
                    }

                    if (!string.IsNullOrEmpty(_friendlyUrl))
                    {
                        string _final_friendly_url = _friendlyUrl.TrimEnd('-');
                        _ObjBlogMasterModel.blog_friendly_url = _final_friendly_url;
                    }

                }
                else
                {
                    string _friendlyUrl = null;

                    string lasttm = txtBlogTitle.Text.TrimEnd(' ');
                    string[] arrOfSelections = lasttm.Split(' ');
                    for (int i = 0; i < arrOfSelections.Length; i++)
                    {
                        string _subStringFriendlyUrl = arrOfSelections[i] + "-";
                        //insert command
                        _friendlyUrl = _friendlyUrl + _subStringFriendlyUrl;
                    }

                    if (!string.IsNullOrEmpty(_friendlyUrl))
                    {
                        string _final_friendly_url = _friendlyUrl.TrimEnd('-');
                        _ObjBlogMasterModel.blog_friendly_url = _final_friendly_url;
                    }

                }


                if (fuUploadedFile.HasFile)
                {
                    string base64ImageRepresentation = string.Empty;
                    string filename = Path.GetFileName(fuUploadedFile.PostedFile.FileName);
                    string contentType = fuUploadedFile.PostedFile.ContentType;
                    string fileUploadFile = Path.GetExtension(fuUploadedFile.FileName.ToString());
                    if (fileUploadFile.ToLower() == ".png" || fileUploadFile.ToLower() == ".jpeg" || fileUploadFile.ToLower() == ".jpg")
                    {

                        using (Stream fs = fuUploadedFile.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                if (bytes.Length > 1048576)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image Size should be 1 MB','warning');", true);
                                    base64ImageRepresentation = "";
                                }
                                else
                                {
                                    base64ImageRepresentation = Convert.ToBase64String(bytes);
                                    _ObjBlogMasterModel.blog_outer_image = base64ImageRepresentation;

                                    //SaveCategory(_ObjBlogMasterModel,true);
                                    //getAllCategory();
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                        base64ImageRepresentation = "";

                    }
                }
                else
                {
                    _ObjBlogMasterModel.blog_outer_image = _ObjBlogMasterModel.blog_outer_image;
                    //SaveCategory(_ObjBlogMasterModel,true);
                    //getAllCategory();
                }




            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }

            return _ObjBlogMasterModel;
        }
        #endregion


        #region  Add Blogs Featured Code
        /// <summary>
        /// Add Blogs Featured Method is User to Save or Add Blogs Feature Details in Data Base Using Api
        /// </summary>
        /// <param name="_objBlogMasterModel"></param>
        private void SetFeaturedBlogs(BlogMasterModel _objBlogMasterModel)
        {

            string strUrl = string.Format(requestUrl + _setFeaturedBlog); //api call name 

            try
            {
                try
                {

                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

                    string postData = JsonConvert.SerializeObject(_objBlogMasterModel);
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.ContentLength = data.Length;
                    request.Method = "POST";
                    request.Headers.Add("Custom-key", Custom_key);
                    //string Token = "Bearer " + Session["Token"].ToString();
                    //  request.Headers.Add("Authorization", Token);
                    request.UserAgent = Request.UserAgent.ToString();

                    request.MediaType = "application/json";
                    request.Accept = "*";
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                    //byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                    request.Timeout = 500000;             //Increase timeout for testing

                    request.ContentType = "application/x-www-form-urlencoded";
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var resultSet = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (resultSet.responseMsg.isError == true)
                    {
                        string _warningMessage = resultSet.responseMsg.errorMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _warningMessage + "','danger');", true);
                    }
                    else
                    {
                        string _successMessage = resultSet.responseMsg.successMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _successMessage + "','success');", true);

                        getAllBlogList();
                    }

                }
                catch (WebException ex)
                {
                    //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    string message = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + message + "','danger');", true);
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        #endregion

        #region  Add Blog Publish Code
        /// <summary>
        /// Add Blog Publish Method is User to Save or Add Blog Publish Details in Data Base Using Api
        /// </summary>
        /// <param name="_objBlogMasterModel"></param>
        private void SetPublishBlog(BlogMasterModel _objBlogMasterModel)
        {

            string strUrl = string.Format(requestUrl + _setPublishBlog); //api call name 

            try
            {
                try
                {

                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

                    string postData = JsonConvert.SerializeObject(_objBlogMasterModel);
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.ContentLength = data.Length;
                    request.Method = "POST";
                    request.Headers.Add("Custom-key", Custom_key);
                    //string Token = "Bearer " + Session["Token"].ToString();
                    //  request.Headers.Add("Authorization", Token);
                    request.UserAgent = Request.UserAgent.ToString();

                    request.MediaType = "application/json";
                    request.Accept = "*";
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                    //byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                    request.Timeout = 500000;             //Increase timeout for testing

                    request.ContentType = "application/x-www-form-urlencoded";
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var resultSet = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (resultSet.responseMsg.isError == true)
                    {
                        string _warningMessage = resultSet.responseMsg.errorMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _warningMessage + "','danger');", true);
                    }
                    else
                    {
                        string _successMessage = resultSet.responseMsg.successMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _successMessage + "','success');", true);
                        HdnFBlogMasterId.Value = "0";
                        getAllBlogList();
                    }

                }
                catch (WebException ex)
                {
                    //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    string message = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + message + "','danger');", true);
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        #endregion


        #region  Add Blog Status Toggle Active/Deactive Code
        /// <summary>
        /// Add Blog Status Toggle Method is used to Status Toggle Active/Deactive in Data Base Using Api
        /// </summary>
        /// <param name="_objBlogMasterModel"></param>
        private void SetBlogToggleStatus(BlogMasterModel _objBlogMasterModel)
        {

            string strUrl = string.Format(requestUrl + _setBlogActiveDeactive); //api call name 

            try
            {
                try
                {

                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

                    string postData = JsonConvert.SerializeObject(_objBlogMasterModel);
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.ContentLength = data.Length;
                    request.Method = "POST";
                    request.Headers.Add("Custom-key", Custom_key);
                    //string Token = "Bearer " + Session["Token"].ToString();
                    //  request.Headers.Add("Authorization", Token);
                    request.UserAgent = Request.UserAgent.ToString();

                    request.MediaType = "application/json";
                    request.Accept = "*";
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                    //byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                    request.Timeout = 500000;             //Increase timeout for testing

                    request.ContentType = "application/x-www-form-urlencoded";
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var resultSet = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (resultSet.responseMsg.isError == true)
                    {
                        string _warningMessage = resultSet.responseMsg.errorMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _warningMessage + "','danger');", true);
                    }
                    else
                    {
                        
                        string _successMessage = resultSet.responseMsg.successMessage;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + _successMessage + "','success');", true);

                        getAllBlogList();
                       
                    }

                }
                catch (WebException ex)
                {
                    //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    string message = ex.Message.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('" + message + "','danger');", true);
                }

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        #endregion

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
                                            _sqlConnection2.Close();
                                            try
                                            {
                                                getAllBlogCategoryList();
                                            }
                                            catch (Exception ex)
                                            {

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
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);

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
        /// This Method is Used to Save data
        /// </summary>
        private void addBlog(string _blogfileName, string _blogthumbnail_image, string __blog_innerImage) 
        {
            try
            {
                string _BlogImageFileName = string.Empty;
                string _BlogThumbnailImageFileName = string.Empty;
                string _BlogInnerImageFileName = string.Empty;

                _BlogImageFileName = _blogfileName;
                _BlogThumbnailImageFileName = _blogthumbnail_image;
                _BlogInnerImageFileName = __blog_innerImage;

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {

                    using (SqlCommand _sqlCommand = new SqlCommand())
                    {
                        _sqlConnection.Open();

                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = @"insert into BlogMaster(blog_category_id,category_name,blog_title,blog_friendly_url,blog_description,author_name,blog_view,is_published,is_featured, blog_tag_id,tag_name,page_title,meta_Key,meta_description,created_by,modified_by,is_active,blog_outer_image,blog_thumbnail,blog_inner_banner_img) values (@blog_category_id,@category_name,@blog_title,@blog_friendly_url,@blog_description,@author_name,@blog_view,@is_published,@is_featured,@blog_tag_id,@tag_name,@page_title,@meta_Key,@meta_description,@created_by,@modified_by,@is_active,@blog_outer_image,@blog_thumbnail,@blog_inner_banner_img)";
                        BlogMasterModel _blogMasterModelforSave = new BlogMasterModel();
                        _blogMasterModelforSave = getTagandFrieldlyUrlDetailsForAdd();
                        _sqlCommand.Parameters.AddWithValue("@blog_category_id", ddlBlogCategoryType.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@category_name", ddlBlogCategoryType.SelectedItem.Text);
                        _sqlCommand.Parameters.AddWithValue("@blog_title", txtBlogTitle.Text);
                        _sqlCommand.Parameters.AddWithValue("@blog_friendly_url", _blogMasterModelforSave.blog_friendly_url);
                        _sqlCommand.Parameters.AddWithValue("@blog_description", txtBlogDescription.Text);
                        _sqlCommand.Parameters.AddWithValue("@blog_outer_image", _BlogImageFileName);
                        _sqlCommand.Parameters.AddWithValue("@blog_thumbnail", _BlogThumbnailImageFileName);
                        _sqlCommand.Parameters.AddWithValue("@blog_inner_banner_img", _BlogInnerImageFileName);
                        _sqlCommand.Parameters.AddWithValue("@author_name", txtAuthorName.Text);
                        _sqlCommand.Parameters.AddWithValue("@blog_view", txtBlogViews.Text);
                        _sqlCommand.Parameters.AddWithValue("@is_published", 0);
                        _sqlCommand.Parameters.AddWithValue("@is_featured", ddlSetFeatured.SelectedValue.ToString());
                        
                        _sqlCommand.Parameters.AddWithValue("@blog_tag_id", _blogMasterModelforSave.blog_tag_id);
                        _sqlCommand.Parameters.AddWithValue("@tag_name", _blogMasterModelforSave.tag_name);
                        _sqlCommand.Parameters.AddWithValue("@page_title", txtPageTitle.Text);
                        _sqlCommand.Parameters.AddWithValue("@meta_Key", txtMetaKey.Text);
                        _sqlCommand.Parameters.AddWithValue("@meta_description", txtMetaDescription.Text);
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
                                    string BLOG_OUTER_IMAGE_NAME = _BlogImageFileName;
                                    string BLOG_THUMBNAIL_IMAGE_NAME = _BlogThumbnailImageFileName;
                                    string BLOG_INNER_IMAGE_NAME = _BlogInnerImageFileName;

                                    string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                    string filelUpload = fuUploadedFile.FileName.ToString();
                                    string thumbnail_fileuploadfile = Path.GetExtension(fuImageThumbail.FileName.ToString());
                                    string inner_fileuploadfile = Path.GetExtension(fuInner.FileName.ToString());

                                    if ((fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg") && (thumbnail_fileuploadfile.ToLower() == ".png" || thumbnail_fileuploadfile.ToLower() == ".jpeg" || thumbnail_fileuploadfile.ToLower() == ".jpg") && (inner_fileuploadfile.ToLower() == ".png" || inner_fileuploadfile.ToLower() == ".jpeg" || inner_fileuploadfile.ToLower() == ".jpg"))
                                    {

                                        if (!Directory.Exists(Server.MapPath("~/assets/blogs/")))
                                        {
                                            Directory.CreateDirectory(Server.MapPath("~/assets/blogs/"));
                                            try
                                            {
                                                fuUploadedFile.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                            }
                                            catch(Exception ex) {  ex.Message.ToString(); }
                                            try
                                            {
                                                fuImageThumbail.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                            }
                                            catch (Exception ex) {  ex.Message.ToString(); }
                                            try
                                            {
                                                fuInner.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                            }
                                            catch (Exception ex) {  ex.Message.ToString(); }


                                            try
                                            {
                                                txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
                                                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                            }
                                            catch (Exception ex)
                                            {
                                                ex.Message.ToString();
                                            }
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);

                                            getAllBlog();
                                            getAllBlogCategoryList();

                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME))
                                                {
                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                                }
                                                fuUploadedFile.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                            }
                                            catch (Exception ex){ ex.Message.ToString();}

                                            try
                                            {
                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME))
                                                {
                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                                }
                                                fuImageThumbail.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                            }
                                            catch (Exception ex) { ex.Message.ToString(); }

                                            try
                                            {
                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME))
                                                {
                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                                }
                                                fuInner.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                            }
                                            catch (Exception ex) { ex.Message.ToString(); }

                                            try
                                            {
                                                txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
                                                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                            }
                                            catch(Exception ex)
                                            {
                                                ex.Message.ToString();
                                            }
                                            
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);
                                            getAllBlog();
                                            getAllBlogCategoryList();
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
        /// This Method is Used to Get All Blog data By Using ADO
        /// </summary>
        private void getAllBlog()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            List<BlogMasterModel> _lstBlogMasterModel = new List<BlogMasterModel>();
                            try
                            {
                                _lstBlogMasterModel = (from DataRow dr in _datatable.Rows
                                            select new BlogMasterModel()
                                            {
                                                blog_master_id = Convert.ToInt32(dr["blog_master_id"]),
                                                guid = (Guid)dr["guid"],
                                                blog_category_id = (int)dr["blog_category_id"],
                                                category_name = dr["category_name"].ToString(),
                                                blog_title = dr["blog_title"].ToString(),
                                                blog_friendly_url = dr["blog_friendly_url"].ToString(),
                                                blog_description = dr["blog_description"].ToString(),
                                                blog_outer_image = dr["blog_outer_image"].ToString(),
                                                blog_thumbnail = dr["blog_thumbnail"].ToString(),
                                                blog_inner_banner_img = dr["blog_inner_banner_img"].ToString(),
                                                author_name = dr["author_name"].ToString(),
                                                blog_view = (int)dr["blog_view"],
                                                is_published = (Boolean)dr["is_published"],
                                                published_on = dr["published_on"] == DBNull.Value ? System.DateTime.UtcNow : Convert.ToDateTime(dr["published_on"]),
                                                is_featured = (Boolean)dr["is_featured"],
                                                blog_tag_id = dr["blog_tag_id"].ToString(),
                                                tag_name = dr["tag_name"].ToString(),
                                                page_title = dr["page_title"].ToString(),
                                                meta_Key = dr["meta_Key"].ToString(),
                                                meta_description = dr["meta_description"].ToString(),
                                                created_on = Convert.ToDateTime(dr["created_on"]),
                                                created_by = (int)dr["created_by"],
                                                //modified_on = (DateTime)dr["modified_on"],
                                                modified_by = (int)dr["created_by"],
                                                is_active = (Boolean)dr["is_active"]
                                               
                                            }).ToList();

                                //Session["allblog"] = _lstBlogMasterModel;

                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                            if (_loginUserType.ToLower() == "admin")
                            {
                                Session["allblog"] = _lstBlogMasterModel;

                                rptrBlogList.DataSource = _lstBlogMasterModel.OrderByDescending( x => x.blog_master_id).ToList();
                                rptrBlogList.DataBind();
                                
                            }
                            else
                            {
                                int _APPUSERMASTERID = Convert.ToInt32(Session[Constants.Id]);
                                Session["allblog"] = _lstBlogMasterModel.Where(x => x.created_by == _APPUSERMASTERID).OrderByDescending(x => x.blog_master_id).ToList();

                                rptrBlogList.DataSource = _lstBlogMasterModel.Where(x => x.created_by == _APPUSERMASTERID).OrderByDescending(x => x.blog_master_id).ToList();
                                rptrBlogList.DataBind();
                            }
                        }
                        else
                        {
                            rptrBlogList.DataSource = null;
                            rptrBlogList.DataBind();
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
        /// This Method is Used to Get All Blog-Category List data By using ADO
        /// </summary>
        private void getAllBlogCategoryList()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogCategory";
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

                            List<BlogCategoryModel> _lstBlogCategory = new List<BlogCategoryModel>();
                            List<BlogCategoryModel> _lstBlogTag = new List<BlogCategoryModel>();

                            _lstBlogCategory = _lstBlogCategoryModel.Where(x =>  x.category_name != "All" && x.category_type == "category").ToList(); /* All is Static Category send by api code its blog_category_id=0 */
                            _lstBlogTag = _lstBlogCategoryModel.Where(x => x.category_name != "All" && x.category_type == "tag").ToList(); ;

                            Session["alltag"] = _lstBlogTag;
                            

                            try
                            {
                                ddlBlogCategoryType.DataSource = _lstBlogCategory;
                                ddlBlogCategoryType.DataTextField = "category_name";
                                ddlBlogCategoryType.DataValueField = "blog_category_id";
                                ddlBlogCategoryType.DataBind();
                                ddlBlogCategoryType.Items.Insert(0, new ListItem("Select", "0"));
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            try
                            {
                                ddlTagList.DataSource = _lstBlogTag;
                                ddlTagList.DataTextField = "category_name";
                                ddlTagList.DataValueField = "blog_category_id";
                                ddlTagList.DataBind();

                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                        }
                        else
                        {
                            ddlTagList.DataSource = null;
                            ddlTagList.DataBind();
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


        private BlogMasterModel getTagandFrieldlyUrlDetailsForAdd()
        {

            BlogMasterModel _ObjBlogMasterModelForTag = new BlogMasterModel();
            try
            {
                if (ddlTagList.Items.Count > 0)
                {
                    string totalitemTagId = null;
                    string totalitemTagname = null;
                    for (int i = 0; i < ddlTagList.Items.Count; i++)
                    {
                        if (ddlTagList.Items[i].Selected)
                        {
                            string selectedItemId = ddlTagList.Items[i].Value + ",";
                            string selectedItemName = ddlTagList.Items[i].Text + ",";
                            //insert command
                            totalitemTagId = totalitemTagId + selectedItemId;
                            totalitemTagname = totalitemTagname + selectedItemName;
                        }
                    }

                    if (!string.IsNullOrEmpty(totalitemTagId))
                    {
                        string lasttmId = totalitemTagId.TrimEnd(',');
                        _ObjBlogMasterModelForTag.blog_tag_id = lasttmId;
                    }

                    if (!string.IsNullOrEmpty(totalitemTagname))
                    {
                        string lasttmName = totalitemTagname.TrimEnd(',');
                        _ObjBlogMasterModelForTag.tag_name = lasttmName;
                    }


                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            try
            {
                if (!string.IsNullOrEmpty(txtfriendlyUrl.Text))
                {
                    string _friendlyUrl = null;

                    string lasttm = txtfriendlyUrl.Text.TrimEnd(' ');
                    string[] arrOfSelections = lasttm.Split(' ');
                    for (int i = 0; i < arrOfSelections.Length; i++)
                    {
                        string _subStringFriendlyUrl = arrOfSelections[i] + "-";
                        //insert command
                        _friendlyUrl = _friendlyUrl + _subStringFriendlyUrl;
                    }

                    if (!string.IsNullOrEmpty(_friendlyUrl))
                    {
                        string _final_friendly_url = _friendlyUrl.TrimEnd('-');
                        _ObjBlogMasterModelForTag.blog_friendly_url = _final_friendly_url;
                    }

                }
                else
                {
                    string _friendlyUrl = null;

                    string lasttm = txtBlogTitle.Text.TrimEnd(' ');
                    string[] arrOfSelections = lasttm.Split(' ');
                    for (int i = 0; i < arrOfSelections.Length; i++)
                    {
                        string _subStringFriendlyUrl = arrOfSelections[i] + "-";
                        //insert command
                        _friendlyUrl = _friendlyUrl + _subStringFriendlyUrl;
                    }

                    if (!string.IsNullOrEmpty(_friendlyUrl))
                    {
                        string _final_friendly_url = _friendlyUrl.TrimEnd('-');
                        _ObjBlogMasterModelForTag.blog_friendly_url = _final_friendly_url;
                    }

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            return _ObjBlogMasterModelForTag;
        }


        /// <summary>
        /// This Method is Used to Update Blog data
        /// </summary>
        private void updateBlogMaster(int _blog_master_id)
        {
            try
            {
                string _BlogImageFileName = string.Empty;
                string _BlogThumbnailImageFileName = string.Empty;
                string _BlogInnerImageFileName = string.Empty;

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster where blog_master_id=" + _blog_master_id + "";
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


                            _BlogImageFileName = _datatable.Rows[0]["blog_outer_image"].ToString();
                            _BlogThumbnailImageFileName = _datatable.Rows[0]["blog_thumbnail"].ToString();
                            _BlogInnerImageFileName = _datatable.Rows[0]["blog_inner_banner_img"].ToString();

                            Random _randomNumber = new Random();
                            if (_BlogImageFileName == null || _BlogImageFileName == "")
                            {
                                string fleuploadfile = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                _BlogImageFileName = "blogouter_" + _randomNumber.Next() + fleuploadfile;
                               
                            }
                            else { }

                            if (_BlogThumbnailImageFileName == null || _BlogThumbnailImageFileName == "")
                            {
                                string thumbnailuploadfile = Path.GetExtension(fuImageThumbail.FileName.ToString());

                                _BlogThumbnailImageFileName = "blogthumbnail_" + _randomNumber.Next() + thumbnailuploadfile;
                                
                            }
                            else  { }

                            if (_BlogInnerImageFileName == null || _BlogInnerImageFileName == "")
                            {
                                string innerBloguploadfile = Path.GetExtension(fuInner.FileName.ToString());

                                _BlogInnerImageFileName = "bloginner_" + _randomNumber.Next() + innerBloguploadfile;

                            }
                            else { }


                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {
                                BlogMasterModel _blogMasterModelforSave = new BlogMasterModel();
                                _blogMasterModelforSave = getTagandFrieldlyUrlDetailsForAdd();
                                
                                                                string _queryUpdate = "update BlogMaster set blog_category_id="+ ddlBlogCategoryType.SelectedValue.ToString() + ", category_name='" + ddlBlogCategoryType.SelectedItem.Text + "',blog_title='" + txtBlogTitle.Text + "',blog_friendly_url='" + _blogMasterModelforSave.blog_friendly_url.ToString() + "',blog_description='"+ txtBlogDescription.Text + "',blog_outer_image='"+ _BlogImageFileName + "',blog_thumbnail='"+ _BlogThumbnailImageFileName + "',blog_inner_banner_img='"+ _BlogInnerImageFileName + "',author_name='"+ txtAuthorName.Text + "',blog_view='"+ txtBlogViews.Text + "',is_published=0,is_featured='"+ ddlSetFeatured.SelectedValue.ToString() + "',blog_tag_id='"+ _blogMasterModelforSave.blog_tag_id + "',tag_name='"+ _blogMasterModelforSave.tag_name + "',page_title='"+ txtPageTitle.Text + "',meta_Key='"+ txtMetaKey.Text + "',meta_description='"+ txtMetaDescription.Text + "',created_by='"+ Convert.ToString(Session[Constants.Id]) + "',modified_by='"+ Convert.ToString(Session[Constants.Id]) + "',is_active="+ 1 +" where blog_master_id = " + _blog_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            if (fuUploadedFile.HasFile || fuImageThumbail.HasFile || fuInner.HasFile)
                                            {
                                                string BLOG_OUTER_IMAGE_NAME = _BlogImageFileName;
                                                string BLOG_THUMBNAIL_IMAGE_NAME = _BlogThumbnailImageFileName;
                                                string BLOG_INNER_IMAGE_NAME = _BlogInnerImageFileName;

                                                string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                                string filelUpload = fuUploadedFile.FileName.ToString();
                                                string thumbnail_fileuploadfile = Path.GetExtension(fuImageThumbail.FileName.ToString());
                                                string inner_fileuploadfile = Path.GetExtension(fuInner.FileName.ToString());
                                                if ((fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg") || (thumbnail_fileuploadfile.ToLower() == ".png" || thumbnail_fileuploadfile.ToLower() == ".jpeg" || thumbnail_fileuploadfile.ToLower() == ".jpg") || (inner_fileuploadfile.ToLower() == ".png" || inner_fileuploadfile.ToLower() == ".jpeg" || inner_fileuploadfile.ToLower() == ".jpg"))
                                                {

                                                    if (!Directory.Exists(Server.MapPath("~/assets/blogs/")))
                                                    {
                                                        Directory.CreateDirectory(Server.MapPath("~/assets/blogs/"));
                                                        try
                                                        {
                                                            if (fuUploadedFile.HasFile == true)
                                                            {
                                                                fuUploadedFile.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                                            }
                                                            else
                                                            {
                                                            }
                                                                
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }
                                                        try
                                                        {
                                                            if (fuImageThumbail.HasFile == true)
                                                            {
                                                                fuImageThumbail.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                                            }
                                                            else
                                                            {

                                                            }
                                                                
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }
                                                        try
                                                        {
                                                            if (fuInner.HasFile == true)
                                                            {
                                                                fuInner.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                                            }
                                                            else
                                                            {

                                                            }
                                                            
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }


                                                        try
                                                        {
                                                            txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ex.Message.ToString();
                                                        }

                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                        getAllBlog();
                                                        getAllBlogCategoryList();

                                                    }
                                                    else
                                                    {
                                                        try
                                                        { 
                                                            if(fuUploadedFile.HasFile == true)
                                                            {
                                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME))
                                                                {
                                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                                                }
                                                                fuUploadedFile.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_OUTER_IMAGE_NAME);
                                                            }
                                                            else
                                                            {

                                                            }
                                                            
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }

                                                        try
                                                        {
                                                            if (fuImageThumbail.HasFile == true)
                                                            {
                                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME))
                                                                {
                                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                                                }
                                                                fuImageThumbail.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_THUMBNAIL_IMAGE_NAME);
                                                            }
                                                            else
                                                            {

                                                            }
                                                            
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }

                                                        try
                                                        {
                                                            if (fuInner.HasFile == true)
                                                            {
                                                                if (File.Exists(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME))
                                                                {
                                                                    File.Delete(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                                                }
                                                                fuInner.SaveAs(Server.MapPath("~/assets/blogs/") + BLOG_INNER_IMAGE_NAME);
                                                            }
                                                            else
                                                            {

                                                            }
                                                            
                                                        }
                                                        catch (Exception ex) { ex.Message.ToString(); }

                                                        try
                                                        {
                                                            txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ex.Message.ToString();
                                                        }

                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                        getAllBlog();
                                                        getAllBlogCategoryList();
                                                    }

                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('image should be png or jpg','warning');", true);
                                                }

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    txtAuthorName.Text = txtBlogDescription.Text = txtBlogTitle.Text = txtBlogViews.Text = txtfriendlyUrl.Text = txtPageTitle.Text = txtMetaKey.Text = txtMetaDescription.Text = string.Empty;
                                                    imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                    ThumbnailimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                    InnerimagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                }
                                                catch (Exception ex)
                                                {
                                                    ex.Message.ToString();
                                                }

                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                getAllBlog();
                                                getAllBlogCategoryList();
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
        private void checkAndActive_DeactiveBlog(int _blog_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster where blog_master_id=" + _blog_master_id + "";
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
                                string _queryUpdate = "update BlogMaster set is_active=" + _isStatus + " where blog_master_id=" + _blog_master_id + "";
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
                                                if (Convert.ToInt32(hdnfStatusBlog.Value) == 1)
                                                {
                                                    checkAndActive_DeactiveBlogCategoryData(Convert.ToInt32(HdnFBlogCategoriesMasterId.Value), Convert.ToInt32(hdnfStatusBlog.Value));
                                                }
                                                else
                                                {

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ex.Message.ToString();
                                            }
                                            getAllBlog();
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
        /// This Method is Used to Check Blog valid or Not and Set Blog Publish Yes/No By Using ADO
        /// </summary>
        private void checkAndPublishYesNOBlog(int _blog_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster where blog_master_id=" + _blog_master_id + "";
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
                                string _queryUpdate = "update BlogMaster set is_published=" + _isStatus + ",published_on='"+ System.DateTime.UtcNow + "' where blog_master_id=" + _blog_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            getAllBlog();
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
        /// This Method is Used to Check Blog valid or Not and Set Blog Featured Yes/No By Using ADO
        /// </summary>
        private void checkAndIsFeturedYesNOBlog(int _blog_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from BlogMaster where blog_master_id=" + _blog_master_id + "";
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
                                string _queryUpdate = "update BlogMaster set is_featured=" + _isStatus + " where blog_master_id=" + _blog_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            getAllBlog();
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