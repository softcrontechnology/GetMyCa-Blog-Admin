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

namespace Healing2Peace.Modules.Items
{
    public partial class manage_item : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
        string imageUrl = ConfigurationManager.AppSettings["webapibaseimgurl"].ToString();
        string blogBaseURL = ConfigurationManager.AppSettings["BlogBaseURL"].ToString();

        string _getAllBlogCategories = apiconstant.GET_ALL_CATEGORIES_FOR_ADMIN;
        string _getAllitemList = apiconstant.GET_ALL_ITEM_LIST_FOR_ADMIN;

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
                    // getAllItemList();
                    getAllItemListListByADO();
                    getAllItemCategoriesListByADO();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlItemCategory.SelectedValue == "" || ddlItemCategory.SelectedValue == null || ddlItemCategory.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemName.Text == "" || txtItemName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemPrice.Text == "" || txtItemPrice.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemTitle.Text == "" || txtItemTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else if (ddlItemDiscount.SelectedItem.Text == "Yes" && txtDiscountInPercentage.Text == null && txtDiscountinAmount.Text == null)
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
                            string fileName = "item_" + _randomNumber.Next() + fileUploadFile;
                            if (fileName != null)
                            {
                                addItem(fileName);
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "notification('Please upload items','warning');", true);

                    }


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
                if (ddlItemCategory.SelectedValue == "" || ddlItemCategory.SelectedValue == null || ddlItemCategory.SelectedValue == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemName.Text == "" || txtItemName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemPrice.Text == "" || txtItemPrice.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }
                else if (txtItemTitle.Text == "" || txtItemTitle.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else if (ddlItemDiscount.SelectedItem.Text == "Yes" && txtDiscountInPercentage.Text == null && txtDiscountinAmount.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError()", true);
                }

                else
                {
                    updateItem(Convert.ToInt32(HdnFItemMasterId.Value));

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
                txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text = txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                ddlItemCategory.SelectedIndex = 0;
                ddlItemDiscount.SelectedValue = "0";
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        #region Get All Item Details 
        /// <summary>
        /// Get All ItemList Method is Used to Get All Item Information or List for Bind Repeater or many more 
        /// </summary>
        private void getAllItemList()
        {
            string strUrl = string.Format(requestUrl + _getAllitemList); //api call name 
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

                        List<ItemMasterModel> _lstItemMasterModel = new List<ItemMasterModel>();

                        listBlogMaster = responseOutput.responseData.data;

                        string jsonString = JsonConvert.SerializeObject(listBlogMaster);

                        _lstItemMasterModel = JsonConvert.DeserializeObject<List<ItemMasterModel>>(jsonString);

                        string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                        if (_loginUserType.ToLower() == "admin")
                        {
                            Session[SessionNames.AllItemList] = _lstItemMasterModel;

                            rptrItemList.DataSource = _lstItemMasterModel;
                            rptrItemList.DataBind();
                        }
                        else
                        {
                            int _APPUSERMASTERID = Convert.ToInt32(Session[Constants.Id]);
                            Session[SessionNames.AllItemList] = _lstItemMasterModel.Where(x => x.created_by == _APPUSERMASTERID).ToList();

                            rptrItemList.DataSource = _lstItemMasterModel.Where(x => x.created_by == _APPUSERMASTERID).ToList(); ;
                            rptrItemList.DataBind();
                        }

                        if (_lstItemMasterModel.Count > 0)
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
        /// This Method is Used to Get All Item List data By using ADO
        /// </summary>
        private void getAllItemListListByADO()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from ItemMaster order by item_master_id desc";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            List<ItemMasterModel> _lstItemMasterModel = new List<ItemMasterModel>();
                            try
                            {
                                _lstItemMasterModel = (from DataRow dr in _datatable.Rows
                                                            select new ItemMasterModel()
                                                            {
                                                                item_master_id = Convert.ToInt32(dr["item_master_id"]),
                                                                guid = (Guid)dr["guid"],
                                                                item_category_id = (int)dr["item_category_id"],
                                                                category_name = dr["category_name"].ToString(),
                                                                item_name = dr["item_name"].ToString(),
                                                                item_title = dr["item_title"].ToString(),
                                                                item_description = dr["item_description"].ToString(),
                                                                item_image = dr["item_image"].ToString(),
                                                                item_old_price = decimal.Parse(dr["item_old_price"].ToString()),
                                                                item_new_price = decimal.Parse(dr["item_new_price"].ToString()),
                                                                is_discount = (Boolean)dr["is_discount"],
                                                                item_discount_in_percentage = decimal.Parse(dr["item_discount_in_percentage"].ToString()),
                                                                item_discount_in_amount = decimal.Parse(dr["item_discount_in_amount"].ToString()),
                                                                item_stock = (int)dr["item_stock"],
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

                           
                            string _loginUserType = Convert.ToString(Session[Constants.UserType]);
                            if (_loginUserType.ToLower() == "admin")
                            {
                                Session[SessionNames.AllItemList] = _lstItemMasterModel.OrderByDescending(x => x.item_master_id).ToList();

                                rptrItemList.DataSource = _lstItemMasterModel.OrderByDescending(x => x.item_master_id).ToList();
                                rptrItemList.DataBind();
                            }
                            else
                            {
                                int _APPUSERMASTERID = Convert.ToInt32(Session[Constants.Id]);
                                Session[SessionNames.AllItemList] = _lstItemMasterModel.Where(x => x.created_by == _APPUSERMASTERID).OrderByDescending(x => x.item_master_id).ToList();

                                rptrItemList.DataSource = _lstItemMasterModel.Where(x => x.created_by == _APPUSERMASTERID).OrderByDescending(x => x.item_master_id).ToList();
                                rptrItemList.DataBind();
                            }

                        }
                        else
                        {
                            rptrItemList.DataSource = null;
                            rptrItemList.DataBind();
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

        #region Repeater
        protected void rptrItemList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string commandname = e.CommandName;
            string id = e.CommandArgument.ToString();
            HdnFItemMasterId.Value = id;
            switch (commandname)
            {
                case "edit":
                    try
                    {
                        Int32 ItemMasterId = Convert.ToInt32(id);
                        HdnFItemMasterId.Value = id;
                        List<ItemMasterModel> _lstItemMasterModel = Session[SessionNames.AllItemList] as List<ItemMasterModel>;
                        ItemMasterModel _ObjItemMasterModel = new ItemMasterModel();

                        _ObjItemMasterModel = _lstItemMasterModel.Where(x => x.item_master_id == ItemMasterId).FirstOrDefault();

                        if (_ObjItemMasterModel.item_master_id >0)
                        {
                            ddlItemCategory.SelectedValue = _ObjItemMasterModel.item_category_id.ToString();
                            txtItemName.Text = _ObjItemMasterModel.item_name;
                            txtItemPrice.Text = _ObjItemMasterModel.item_new_price.ToString();
                            txtItemTitle.Text = _ObjItemMasterModel.item_title;
                            txtItemDescription.Text = _ObjItemMasterModel.item_description;
                            txtDiscountInPercentage.Text = _ObjItemMasterModel.item_discount_in_percentage.ToString();
                            txtDiscountinAmount.Text = _ObjItemMasterModel.item_discount_in_amount.ToString();
                            ddlItemDiscount.SelectedValue = _ObjItemMasterModel.is_discount == true ? "1" : "0";
                            txtItemStock.Text = _ObjItemMasterModel.item_stock.ToString();

                            if (string.IsNullOrEmpty(_ObjItemMasterModel.item_image))
                            {
                                imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                            }
                            else
                            {
                                imagePreview.ImageUrl = "/assets/items/" + _ObjItemMasterModel.item_image;
                            }

                            btnUpdate.Visible = true;
                            btnSubmit.Visible = false;
                        }
                        else
                        {

                        }
                       

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
                        HdnFItemMasterId.Value = id;
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
                            hdnfStatusItem.Value = _UserStatus.ToString();
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
                
                
                default:
                    break;
            }
        }

        protected void lnkbtnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnFItemMasterId.Value != null && hdnfStatusItem.Value != null)
                {
                    checkAndActive_DeactiveItem(Convert.ToInt32(HdnFItemMasterId.Value), Convert.ToInt32(hdnfStatusItem.Value));
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

        protected void rptrItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {   

                    Label lblpic = (Label)e.Item.FindControl("lblpic");
                   
                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                    Image lblimageurl = (Image)e.Item.FindControl("lblimageurl");
                    LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");
                    
                    string url;
                    if (string.IsNullOrEmpty(lblpic.Text))
                    {
                        url = "/Content/img/placeholders/avatars/avatar2.jpg";
                    }
                    else
                    {
                        url = "/assets/items/" + lblpic.Text;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }

        #endregion


        #region Get All Item categories Details and This Method also used for Get All Item List and bind Dropdown Tag By API and ADO
        /// <summary>
        /// Get All Item categories Method is Used to Get All Item categories Information or List for Bind Repeater or many more and This Method also used for Get All Item List and bind Dropdown Item
        /// </summary>
        private void getAllItemCategoriesList()
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

                       
                        List<BlogCategoryModel> _lstItemCategory = new List<BlogCategoryModel>();

                         
                        _lstItemCategory = _lstBlogCategoryModel.Where(x => x.is_active == true && x.category_name != "All" && x.category_type == "ic").ToList(); 

                        Session["itemcategory"] = _lstItemCategory;

                        try
                        {
                            ddlItemCategory.DataSource = _lstItemCategory;
                            ddlItemCategory.DataTextField = "category_name";
                            ddlItemCategory.DataValueField = "blog_category_id";
                            ddlItemCategory.DataBind();
                            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
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


        /// <summary>
        /// This Method is Used to Get All Blog-Category List data By using ADO
        /// </summary>
        private void getAllItemCategoriesListByADO()
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
 

                            Session["itemcategory"] = _lstBlogCategoryModel.Where(x => x.is_active == true && x.category_name != "All" && x.category_type == "ic").ToList();


                            try
                            {
                                ddlItemCategory.DataSource = _lstBlogCategoryModel.Where(x => x.is_active == true && x.category_name != "All" && x.category_type == "ic").ToList();
                                ddlItemCategory.DataTextField = "category_name";
                                ddlItemCategory.DataValueField = "blog_category_id";
                                ddlItemCategory.DataBind();
                                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            
                        }
                        else
                        {
                            ddlItemCategory.DataSource = null;
                            ddlItemCategory.DataBind();
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

        /// <summary>
        /// This Method is Used to Save data
        /// </summary>
        private void addItem(string _itemfileName)
        {
            try
            {
                string _ItemFileName = string.Empty;
                _ItemFileName = _itemfileName;
                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {

                    using (SqlCommand _sqlCommand = new SqlCommand())
                    {
                        _sqlConnection.Open();

                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = @"insert into ItemMaster(item_category_id,category_name,item_name,item_title,item_description,item_image,item_old_price,item_new_price,is_discount,item_discount_in_percentage,item_discount_in_amount,item_stock,created_by,modified_by,is_active) values (@item_category_id,@category_name,@item_name,@item_title,@item_description,@item_image,@item_old_price,@item_new_price,@is_discount,@item_discount_in_percentage,@item_discount_in_amount,@item_stock,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@item_category_id", ddlItemCategory.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@category_name", ddlItemCategory.SelectedItem.Text.ToString());
                        _sqlCommand.Parameters.AddWithValue("@item_name", txtItemName.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_title", txtItemTitle.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_description", txtItemDescription.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_image", _ItemFileName);
                        _sqlCommand.Parameters.AddWithValue("@item_old_price", txtItemPrice.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_new_price", txtItemPrice.Text);
                        _sqlCommand.Parameters.AddWithValue("@is_discount", ddlItemDiscount.SelectedValue.ToString());
                        _sqlCommand.Parameters.AddWithValue("@item_discount_in_percentage", txtDiscountInPercentage.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_discount_in_amount", txtDiscountinAmount.Text);
                        _sqlCommand.Parameters.AddWithValue("@item_stock", txtItemStock.Text);
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
                                    string ITEMNAME = _ItemFileName;

                                    string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                    string filelUpload = fuUploadedFile.FileName.ToString();
                                    if (fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg")
                                    {

                                        if (!Directory.Exists(Server.MapPath("~/assets/items/")))
                                        {
                                            Directory.CreateDirectory(Server.MapPath("~/assets/items/"));
                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/items/") + ITEMNAME);
                                            txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text =  txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                                            ddlItemCategory.SelectedIndex = 0;
                                            ddlItemDiscount.SelectedValue = "0";
                                            getAllItemListListByADO();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showSuccess()", true);

                                        }
                                        else
                                        {

                                            if (File.Exists(Server.MapPath("~/assets/items/") + ITEMNAME))
                                            {
                                                File.Delete(Server.MapPath("~/assets/items/") + ITEMNAME);
                                            }

                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/items/") + ITEMNAME);
                                            txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text = txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                                            ddlItemCategory.SelectedIndex = 0;
                                            ddlItemDiscount.SelectedValue = "0";
                                            getAllItemListListByADO();
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
        /// This Method is Used to Update Item data
        /// </summary>
        private void updateItem(int _Item_master_id)
        {
            try
            {
                string _ItemFileName = string.Empty;

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from ItemMaster where item_master_id=" + _Item_master_id + "";
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


                            _ItemFileName = _datatable.Rows[0]["item_image"].ToString();
                            using (SqlConnection _sqlConnection2 = new SqlConnection(_connectionstring))
                            {

                                string _queryUpdate = "update ItemMaster set item_category_id='" + ddlItemCategory.SelectedValue.ToString() + "',category_name='" + ddlItemCategory.SelectedItem.Text + "',item_name='" + txtItemName.Text + "',item_title='"+ txtItemTitle.Text + "',item_description='"+ txtItemDescription.Text + "',item_image='" + _ItemFileName + "',item_new_price='"+ txtItemPrice.Text + "',is_discount='"+ ddlItemDiscount.SelectedValue.ToString() + "',item_discount_in_percentage='"+ txtDiscountInPercentage.Text + "',item_discount_in_amount='"+ txtDiscountinAmount.Text + "',item_stock='"+ txtItemStock.Text + "',modified_by="+ Convert.ToString(Session[Constants.Id]) + "  where item_master_id=" + _Item_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                       
                                            try
                                            {
                                                if (fuUploadedFile.HasFile)
                                                {
                                                    string ITEMNAME = _ItemFileName;

                                                    string fleUpload = Path.GetExtension(fuUploadedFile.FileName.ToString());
                                                    string filelUpload = fuUploadedFile.FileName.ToString();
                                                    if (fleUpload.ToLower() == ".png" || fleUpload.ToLower() == ".jpeg" || fleUpload.ToLower() == ".jpg")
                                                    {

                                                        if (!Directory.Exists(Server.MapPath("~/assets/items/")))
                                                        {
                                                            Directory.CreateDirectory(Server.MapPath("~/assets/items/"));
                                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/items/") + ITEMNAME);
                                                            txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text = txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                                                            ddlItemCategory.SelectedIndex = 0;
                                                            ddlItemDiscount.SelectedValue = "0";
                                                            btnUpdate.Visible = false;
                                                            btnSubmit.Visible = true;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                            getAllItemListListByADO();
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);

                                                    }
                                                        else
                                                        {

                                                            if (File.Exists(Server.MapPath("~/assets/items/") + ITEMNAME))
                                                            {
                                                                File.Delete(Server.MapPath("~/assets/items/") + ITEMNAME);
                                                            }

                                                            fuUploadedFile.SaveAs(Server.MapPath("~/assets/items/") + ITEMNAME);
                                                            txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text = txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                                                            ddlItemCategory.SelectedIndex = 0;
                                                            ddlItemDiscount.SelectedValue = "0";
                                                            btnUpdate.Visible = false;
                                                            btnSubmit.Visible = true;
                                                            imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                        getAllItemListListByADO();
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
                                                    
                                                    btnUpdate.Visible = false;
                                                    btnSubmit.Visible = true;
                                                    imagePreview.ImageUrl = "/Content/img/placeholders/avatars/avatar2.jpg";
                                                    txtItemName.Text = txtItemPrice.Text = txtItemStock.Text = txtItemTitle.Text = txtItemDescription.Text = txtDiscountInPercentage.Text = txtDiscountinAmount.Text = string.Empty;
                                                    ddlItemCategory.SelectedIndex = 0;
                                                    ddlItemDiscount.SelectedValue = "0";
                                                getAllItemListListByADO();
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);

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
        /// This Method is Used to Check Item valid or Not and Set Item Active/Deactive
        /// </summary>
        private void checkAndActive_DeactiveItem(int _item_master_id, int _isStatus)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from ItemMaster where item_master_id=" + _item_master_id + "";
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
                                string _queryUpdate = "update ItemMaster set is_active=" + _isStatus + ",modified_by='"+ Convert.ToString(Session[Constants.Id]) + "' where item_master_id=" + _item_master_id + "";
                                using (SqlCommand _sqlCommand2 = new SqlCommand(_queryUpdate, _sqlConnection2))
                                {
                                    _sqlConnection2.Open();

                                    _sqlCommand2.CommandTimeout = 600;

                                    int _outputCount = _sqlCommand2.ExecuteNonQuery();
                                    if (_outputCount > 0)
                                    {
                                        try
                                        {
                                            getAllItemListListByADO();
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