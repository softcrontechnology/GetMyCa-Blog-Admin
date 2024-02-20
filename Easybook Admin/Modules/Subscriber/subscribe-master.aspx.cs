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

namespace Healing2Peace.Modules.Subscriber
{
    public partial class subscribe_master : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();

        string _getAllSubscriber = apiconstant.GET_ALL_SUBSCRIBE;

        ContactMasterModel _objContactMasterModel = new ContactMasterModel();
        DataTable dtFilter;

        SqlConnection _sqlConnection = new SqlConnection();
        SqlCommand _sqlCommand, _sqlCommand2, _sqlCommand3 = new SqlCommand();
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
                        //getAllSubscriber();
                        getAllSubscriberListByADO();
                    }
                    else
                    {
                        Response.Redirect("/dashboard", false);
                    }

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

        protected void rptrSubscribe_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptrSubscribe_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {


                    //Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");

                    //LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    //LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");


                    //string isactive = lblIsActive.Text;
                    //if (isactive == "True")
                    //{
                    //    lblIsActive.Text = "Activated";
                    //    lblIsActive.CssClass = "btn btn-success";
                    //    btnstatusactive.Visible = true;
                    //    btnstatusdeactive.Visible = false;
                    //}
                    //else
                    //{
                    //    lblIsActive.Text = "Deactivated";
                    //    lblIsActive.CssClass = "btn btn-danger";
                    //    btnstatusactive.Visible = false;
                    //    btnstatusdeactive.Visible = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException()", true);
            }
        }


        #region Get All Subscriber Details BY API and ADO
        /// <summary>
        /// Get All Subscriber Method is Used to Get All Subscriber Information or List for Bind Repeater or many more
        /// </summary>
        private void getAllSubscriber()
        {
            string strUrl = string.Format(requestUrl + _getAllSubscriber); //api call name 
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
                        dynamic listSubscribeMasterModel = new ExpandoObject();

                        List<SubscribeMasterModel> _lstSubscribeMasterModel = new List<SubscribeMasterModel>();

                        listSubscribeMasterModel = responseOutput.responseData.data;

                        string jsonString = JsonConvert.SerializeObject(listSubscribeMasterModel);

                        _lstSubscribeMasterModel = JsonConvert.DeserializeObject<List<SubscribeMasterModel>>(jsonString);

                        Session["allsubscribe"] = _lstSubscribeMasterModel;

                        rptrSubscribe.DataSource = _lstSubscribeMasterModel;
                        rptrSubscribe.DataBind();
                        if (_lstSubscribeMasterModel.Count > 0)
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
        /// This Method is Used to Get All Subscriber List data By using ADO
        /// </summary>
        private void getAllSubscriberListByADO()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from SubscribeMaster where is_active=1";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            List<SubscribeMasterModel> _lstSubscribeMasterModel = new List<SubscribeMasterModel>();
                            try
                            {
                                _lstSubscribeMasterModel = (from DataRow dr in _datatable.Rows
                                                         select new SubscribeMasterModel()
                                                         {
                                                             subscribe_master_id = Convert.ToInt32(dr["subscribe_master_id"]),
                                                             guid = (Guid)dr["guid"],
                                                             subscribe_email = dr["subscribe_email"].ToString(),
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

                            Session["allcategories"] = _lstSubscribeMasterModel.OrderByDescending( x => x.subscribe_master_id).ToList();

                            rptrSubscribe.DataSource = _lstSubscribeMasterModel.OrderByDescending(x => x.subscribe_master_id).ToList();
                            rptrSubscribe.DataBind();

                        }
                        else
                        {
                            rptrSubscribe.DataSource = null;
                            rptrSubscribe.DataBind();
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


        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ItemMaster.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    foreach (RepeaterItem item in rptrSubscribe.Items)
                    {
                        List<Control> controls = new List<Control>();
                        foreach (Control control in item.Controls)
                        {
                            if (control.GetType() == typeof(LinkButton))
                            {
                                controls.Add(control);
                            }
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "LinkButton":
                                    //item.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                            }
                            item.Controls.Remove(control);
                        }
                    }
                    rptrSubscribe.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}