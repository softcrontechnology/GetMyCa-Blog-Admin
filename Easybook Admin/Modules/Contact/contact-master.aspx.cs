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

namespace Healing2Peace.Modules.Contact
{
    public partial class contact_master : System.Web.UI.Page
    {
        #region
        string requestUrl = ConfigurationManager.AppSettings["webapibaseurl"].ToString();
        string Custom_key = ConfigurationManager.AppSettings["authkey"].ToString();
        
        string _getAllContact = apiconstant.GET_ALL_CONTACT;
        
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
                        menutags.Visible = true;
                        memusubscribe.Visible = true;
                        // getAllConatct();
                        getAllConatctListByADO();
                    }
                    else
                    {
                        menutags.Visible = false;
                        memusubscribe.Visible = false;
                        Response.Redirect("/dashboard", false);
                    }
                    
                    
                }
                
               

            }
            else
            {
            }
        }

        protected void rptrContactList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptrContactList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        #region Get All Contact Details
        /// <summary>
        /// Get All Contact Method is Used to Get All Contact Information or List for Bind Repeater or many more
        /// </summary>
        private void getAllConatct()
        {
            string strUrl = string.Format(requestUrl + _getAllContact); //api call name 
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
                        dynamic listContactMasterModel = new ExpandoObject();

                        List<ContactMasterModel> _lstContactMasterModel = new List<ContactMasterModel>();

                        listContactMasterModel = responseOutput.responseData.data;

                        string jsonString = JsonConvert.SerializeObject(listContactMasterModel);

                        _lstContactMasterModel = JsonConvert.DeserializeObject<List<ContactMasterModel>>(jsonString);

                        Session["allContact"] = _lstContactMasterModel;

                        rptrContactList.DataSource = _lstContactMasterModel;
                        rptrContactList.DataBind();
                        if (_lstContactMasterModel.Count > 0)
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
        /// This Method is Used to Get All Contact List data By using ADO
        /// </summary>
        private void getAllConatctListByADO() 
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from ContactMaster where is_active=1";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable = new DataTable();
                        _sqlDataAdapter.Fill(_datatable);
                        if (_datatable.Rows.Count > 0)
                        {
                            List<ContactMasterModel> _lstContactMasterModel = new List<ContactMasterModel>();
                            try
                            {
                                _lstContactMasterModel = (from DataRow dr in _datatable.Rows
                                                            select new ContactMasterModel()
                                                            {
                                                                contact_master_id = Convert.ToInt32(dr["contact_master_id"]),
                                                                guid = (Guid)dr["guid"],
                                                                full_name = dr["full_name"].ToString(),
                                                                email = dr["email"].ToString(),
                                                                phone_number = dr["phone_number"].ToString(),
                                                                subject = dr["subject"].ToString(),
                                                                message = dr["message"].ToString(),
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


                            Session["allContact"] = _lstContactMasterModel.OrderByDescending(x => x.contact_master_id).ToList();

                            rptrContactList.DataSource = _lstContactMasterModel.OrderByDescending(x => x.contact_master_id).ToList();
                            rptrContactList.DataBind();

                        }
                        else
                        {
                            rptrContactList.DataSource = null;
                            rptrContactList.DataBind();
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

    }
}