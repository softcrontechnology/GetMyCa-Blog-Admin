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

namespace Healing2Peace.Modules.Dashboard
{
    public partial class dashboard : System.Web.UI.Page
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
                    string _AppUserRole = Session[Constants.UserType].ToString();
                    if (_AppUserRole.ToLower() =="admin")
                    {
                        menutags.Visible = true;
                        //memusubscribe.Visible = true;
                    }
                    else
                    {
                        menutags.Visible = false;
                        //memusubscribe.Visible = false;
                    }

                    //lblBlogCategory.Text = lblBlog.Text = lblTotalTag.Text = lblTotalSubscribe.Text = "0";
                    lblBlogCategory.Text = lblBlog.Text = lblTotalTag.Text  = "0";
                    getDashboardData();
                }
               

            }
            else
            {
            }
        }


        private void getDashboardData()
        {
            try
            {

                using (_sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                {

                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("sp_DashboardDetail", _sqlConnection);
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    //_sqlCommand.Parameters.AddWithValue("@SaleHeaderId", SaleHeaderIds);
                    _sqlCommand.CommandTimeout = 600;
                    _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                    _datatable = new DataTable();
                    _sqlDataAdapter.Fill(_datatable);

                    if (_datatable.Rows.Count > 0)
                    {
                        for (int index = 0; index < _datatable.Rows.Count; index++)
                        {

                            if (_datatable.Rows[index]["Name"].ToString() == "blogcategory")
                            {
                                lblBlogCategory.Text = _datatable.Rows[index]["Total"].ToString();
                            }
                            else if (_datatable.Rows[index]["Name"].ToString() == "blog")
                            {
                                lblBlog.Text = _datatable.Rows[index]["Total"].ToString();
                            }
                            else if (_datatable.Rows[index]["Name"].ToString() == "tag")
                            {
                                lblTotalTag.Text = _datatable.Rows[index]["Total"].ToString();
                            }
                            //else if (_datatable.Rows[index]["Name"].ToString() == "subscribe")
                            //{
                            //    lblTotalSubscribe.Text = _datatable.Rows[index]["Total"].ToString();
                            //}
                            else
                            {

                            }
                        }

                    }
                    else
                    {

                    }
                    _sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _sqlConnection.Close();
            }
        }

    }
}