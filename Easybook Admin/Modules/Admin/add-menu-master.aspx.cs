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
using Healing2Peace.Model;
using System.Text;

namespace Healing2Peace.Modules.Admin
{
    public partial class add_menu_master : System.Web.UI.Page
    {
        #region Global Variables
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
                    }
                    else
                    {
                        menutags.Visible = false;
                        memusubscribe.Visible = false;
                    }

                    GetParentPage();
                }
                
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDisplayName.Text == "" || txtDisplayName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }

                else if (txtParentOrder.Text == "" || txtParentOrder.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else if (txtPageURL.Text == "" || txtPageURL.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }

                else if (txtChildOrder.Text == "" || txtChildOrder.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else
                {
                    UpdateMenuMaster( Convert.ToInt32(hdnMenuMasterID.Value));
                }
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
                if (txtDisplayName.Text == "" || txtDisplayName.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
               
                else if (txtParentOrder.Text == "" || txtParentOrder.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else if (txtPageURL.Text == "" || txtPageURL.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                
                else if (txtChildOrder.Text == "" || txtChildOrder.Text == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showError();", true);
                }
                else
                {
                    addMenuMaster();
                    btnUpdate.Visible = false;
                    btnSubmit.Visible = true;
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
                txtDisplayName.Text = txtPageURL.Text = txtParentOrder.Text = txtChildOrder.Text = txtCssIcon.Text = string.Empty;
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
                GetParentPage();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

       

        /// <summary>
        /// This Method is Used to Get All menu List for Get Parent page
        /// </summary>
        private void GetParentPage()
        {
            try
            {

                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from MenuMaster where is_active=1";
                    _sqlConnection.Open();
                    using (SqlCommand _sqlCommand = new SqlCommand(_query, _sqlConnection))
                    {

                        _sqlCommand.CommandTimeout = 600;
                        _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                        _datatable_lstMenuMaster = new DataTable();
                        _sqlDataAdapter.Fill(_datatable_lstMenuMaster);
                        if (_datatable_lstMenuMaster.Rows.Count > 0)
                        {
                            var _stringBuilder = new StringBuilder();

                            List<MenuMasterModel> listMenu = new List<MenuMasterModel>();

                            List<MenuMasterModel> _lstParentMenu = new List<MenuMasterModel>();

                            rptrMenuDetails.DataSource = _datatable_lstMenuMaster;
                            rptrMenuDetails.DataBind();

                            try
                            {
                                listMenu = (from DataRow dr in _datatable_lstMenuMaster.Rows
                                            select new MenuMasterModel()
                                            {
                                                menu_master_id = Convert.ToInt32(dr["menu_master_id"]),
                                                guid = (Guid)dr["guid"],
                                                display_name = dr["display_name"].ToString(),
                                                page_url = dr["page_url"].ToString(),
                                                parent_id = (int)dr["parent_id"],
                                                child_order = (int)dr["child_order"],
                                                cssclass = dr["cssclass"].ToString()
                                            }).ToList();
                            }
                            catch(Exception ex)
                            {
                                ex.Message.ToString();
                            }

                            Session["allmenu"] = listMenu;

                            _lstParentMenu = listMenu.Where(x => x.parent_id == 0).OrderBy(x => x.display_name).ToList();
                            if (_lstParentMenu != null)
                            {
                                lbParentPage.DataSource = _lstParentMenu;
                                lbParentPage.DataTextField = "display_name";
                                lbParentPage.DataValueField = "menu_master_id";
                                lbParentPage.DataBind();
                                lbParentPage.Items.Insert(0, new ListItem("Select", "0"));
                            }
                            else
                            {

                            }
                            
                        }
                        else
                        {

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


        private void addMenuMaster()
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
                        _sqlCommand.CommandText = @"insert into MenuMaster(display_name,page_url,parent_id,parent_order,child_order,cssclass,created_by,modified_by,is_active) values (@display_name, @page_url, @parent_id,@parent_order,@child_order,@cssclass,@created_by,@modified_by,@is_active)";

                        _sqlCommand.Parameters.AddWithValue("@display_name", txtDisplayName.Text);
                        _sqlCommand.Parameters.AddWithValue("@page_url", txtPageURL.Text);
                        _sqlCommand.Parameters.AddWithValue("@parent_id", lbParentPage.SelectedValue == "" ? 0 : Convert.ToInt32(lbParentPage.SelectedValue));
                        _sqlCommand.Parameters.AddWithValue("@parent_order", Convert.ToInt32(txtParentOrder.Text.Trim()));
                        _sqlCommand.Parameters.AddWithValue("@child_order", Convert.ToInt32(txtChildOrder.Text.Trim()));
                        _sqlCommand.Parameters.AddWithValue("@cssclass", txtCssIcon.Text.Trim());
                        _sqlCommand.Parameters.AddWithValue("@created_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@modified_by", Convert.ToString(Session[Constants.Id]));
                        _sqlCommand.Parameters.AddWithValue("@is_active", 1);

                        int _outputCount = _sqlCommand.ExecuteNonQuery();
                        if (_outputCount > 0)
                        {
                            txtDisplayName.Text = txtPageURL.Text = txtParentOrder.Text = txtChildOrder.Text = txtCssIcon.Text = string.Empty;
                            try
                            {
                                GetParentPage();
                            }
                            catch(Exception ex)
                            {
                                ex.Message.ToString();
                            }
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showAddMenuSuccess();", true);
                            

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showNoRecordFound();", true);
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

        private void UpdateMenuMaster(int _menu_masterId)
        {
            try
            {


                string _connectionstring = (ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionstring))
                {
                    string _query = "select * from MenuMaster where menu_master_id=" + _menu_masterId + "";
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

                                string _queryUpdate = "update MenuMaster set display_name='" + txtDisplayName.Text + "',page_url='" + txtPageURL.Text + "',parent_id='"+ lbParentPage.SelectedValue + "',  parent_order='" + txtParentOrder.Text + "',child_order='" + txtChildOrder.Text + "',cssclass='"+ txtCssIcon.Text + "' where menu_master_id=" + _menu_masterId + "";
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
                                                
                                                txtDisplayName.Text = txtPageURL.Text = txtParentOrder.Text = txtChildOrder.Text = txtCssIcon.Text = string.Empty;
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showUpdate()", true);
                                                GetParentPage();

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
            }
        }

        protected void rptrMenuDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                    LinkButton btnstatusactive = (LinkButton)e.Item.FindControl("btnstatusactive");
                    LinkButton btnstatusdeactive = (LinkButton)e.Item.FindControl("btnstatusdeactive");
                    //CheckBox chkActive=(CheckBox)e.Item.FindControl("chkActive");
                    string isactive = lblIsActive.Text;
                    if (isactive == "True")
                    {
                        btnstatusactive.Visible = true;
                        btnstatusdeactive.Visible = false;
                        //chkActive.Checked=true;
                    }
                    else
                    {
                        btnstatusactive.Visible = false;
                        btnstatusdeactive.Visible = true;
                        //chkActive.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
            }
        }

        protected void rptrMenuDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string commandname = e.CommandName;
            string id = e.CommandArgument.ToString();
            hdnMenuMasterID.Value = id;

            Int32 ID = Convert.ToInt32(id);
            switch (commandname)
            {
                case "active":
                    try
                    {
                       // UpdateStatus(id);
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
                        Int32 menuMasterId = Convert.ToInt32(id);
                        hdnMenuMasterID.Value = id;
                        PopulateData(menuMasterId);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "showException();", true);
                    }
                    break;

            }
        }

        private void PopulateData(int _menu_master_id)
        {
           
            List<MenuMasterModel> _lstMenuMaster = Session["allmenu"] as List<MenuMasterModel>;
            MenuMasterModel _ObjMenuMaster = new MenuMasterModel();
            _ObjMenuMaster = _lstMenuMaster.Where(x => x.menu_master_id == _menu_master_id).FirstOrDefault();
            if (_ObjMenuMaster !=null)
            {
                if(_ObjMenuMaster.menu_master_id > 0)
                {
                    lbParentPage.SelectedValue = _ObjMenuMaster.parent_id.ToString();
                    txtDisplayName.Text = _ObjMenuMaster.display_name;
                    txtPageURL.Text = _ObjMenuMaster.page_url;
                    txtCssIcon.Text = _ObjMenuMaster.cssclass;
                    txtParentOrder.Text = Convert.ToString(_ObjMenuMaster.parent_order);
                    txtChildOrder.Text = Convert.ToString(_ObjMenuMaster.child_order);
                    btnUpdate.Visible = true;
                    btnSubmit.Visible = false;
                }
                else
                {

                }
            }
            
        }
    }
}