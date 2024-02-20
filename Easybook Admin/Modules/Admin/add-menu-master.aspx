<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="add-menu-master.aspx.cs" Inherits="Healing2Peace.Modules.Admin.add_menu_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        function scrollToLocation(Location) {
            $('html, body').animate({
                scrollTop: ($("" + Location + "").offset().top - 80)
            }, 1000);
        }
     </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagecontent" runat="server">

    <div id="page-content">
         <!-- Dashboard 2 Header -->
        <div class="content-header">
            <ul class="nav-horizontal text-center">
                <li class="active">
                    <a href="/dashboard"><i class="fa fa-home"></i>Home</a>
                </li>
                <li>
                    <a href="/manage-blog-category"><i class="fa fa-th"></i>Blog Category</a>
                </li>
                <li>
                    <a href="/manage-blog"><i class="fa fa-th-large"></i>Blog</a>
                </li>
                <li runat="server" id="menutags">
                    <a href="/manage-blog-category"><i class="fa fa-tags"></i>Tags</a>
                </li>
                <li runat="server" id="memusubscribe">
                    <a href="/manage-subscribe"><i class="fa fa-user-circle"></i>Subscriber</a>
                </li>
                
                <li>
                    <a href="#"><i class="fa fa-cogs"></i>Settings</a>
                </li>
            </ul>
        </div>
        <!-- END Dashboard 2 Header -->
        <div class="content-header">
            <div class="header-section">
                <h1>
                    <i class="fa fa-asterisk fa-spin fa-3x"></i>Add Menu Master<br />
                    <small>Add/Manage Menu alongwith the required details !</small>
                </h1>
            </div>
        </div>
         <ul class="breadcrumb breadcrumb-top">
            <li>Menu Master</li>
            <li><a href="#">Add Menu</a></li>
        </ul>

          <div class="row">

            <div class="col-md-12">

                <!-- Normal Form Block -->
                <div class="block">
                    <!-- Normal Form Title -->
                    <div class="block-title">
                        <div class="block-options pull-right">
                           <%-- <a href="javascript:void(0)" class="btn btn-alt btn-sm btn-default toggle-bordered enable-tooltip" data-toggle="button" title="Toggles .form-bordered class">All Party </a>--%>
                        </div>
                        <h2><strong>Menu</strong> Master </h2>
                    </div>
                    <!-- END Normal Form Title -->

                    <!-- Normal Form Content -->
                    <asp:HiddenField ID="hdnId" runat="server" />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtDisplayName">Display Name <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" placeholder="Display name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="lbParentPage">Parent Page <span class="text-danger">*</span></label>
                                  <asp:ListBox ID="lbParentPage" runat="server" SelectionMode="Single" CssClass="select-chosen select2" data-placeholder="Parent Page.."></asp:ListBox>    
                            </div>
                        </div>
                        <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtParentOrder">Parent Order <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtParentOrder" runat="server" CssClass="form-control" onkeypress="return Number(event)" placeholder="Parent Order" MaxLength="3" onkeydown="return (!((event.keyCode >= 65) && event.keyCode != 32) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105));"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                     <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtPageURL">Page URL <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txtPageURL" runat="server" CssClass="form-control" placeholder="Page URL"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtCssIcon">Icon CSS <span class="text-danger">*</span></label>
                                  <asp:TextBox ID="txtCssIcon" runat="server" CssClass="form-control" placeholder="Icon CSS"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                                <div class="form-group">
                                    <label for="txtParentOrder">Child Order <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtChildOrder" runat="server" CssClass="form-control" placeholder="Child Order" MaxLength="3" onkeydown="return (!((event.keyCode >= 65) && event.keyCode != 32) || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105));"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group form-actions" style="float: right">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-sm btn-warning"  OnClick="btnReset_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Menu" class="btn btn-sm btn-success" Visible="false"  OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Add Menu" class="btn btn-sm btn-primary"  OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>

                    </div>
                </div>
              </div>
        
       
      
         <!-- End Normal Form Content -->

        <!-- Responsive Full Block -->
        <div class="block">
            <!-- Responsive Full Title -->
            <div class="block-title">
                
                <div class="block-options pull-right" style="padding-right: 1.3rem;">
                    <asp:LinkButton runat="server" ID="btnExportToExcel" CssClass="btn btn-default" title="Export Excel"><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                </div>
                 <i class="fa fa-pencil-square-o sidebar-nav-icon" style="padding-left: 0px;color: white;"></i>
                        <h2><strong>Menu</strong> List</h2>               
            </div>
            <!-- END Responsive Full Title -->

            <!-- Responsive Full Content -->
            <div class="table-responsive">
             <asp:Repeater ID="rptrMenuDetails" runat="server" OnItemCommand="rptrMenuDetails_ItemCommand" OnItemDataBound="rptrMenuDetails_ItemDataBound">
                <HeaderTemplate>
                    <table id="example-datatable" class="table table-bordered table-striped table-vcenter">
                        <thead>
                            <tr>
                                <th class="text-left" style="width: 50px;">Sr.No.</th>
                                <th class="text-left ">Display Name</th>
                                <th class="text-left ">Page URL</th>
                                <th class="text-left" style="width: 50px;">Parent Page</th>
                                <th class="text-left" style="width: 50px;">Parent Order</th>
                                <th class="text-left"  style="width: 50px;">Child Order</th>
                                <th class="text-left ">Css Class</th>
                                <th class="text-left">Status</th>
                                <th class="text-left">Action</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="text-center">
                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                        </td>
                        <td class="text-left">
                            <asp:Label ID="lblDisplayName" Text='<%# Eval("display_name")%>' CssClass="text text-primary" runat="server"></asp:Label>
                        </td>
                        <td class="text-left">
                            <asp:Label ID="lblPageURL" Text='<%# Eval("page_url")%>' runat="server"></asp:Label>

                        </td>
                        <td class="text-center">
                            <asp:Label ID="lblParentID" Text='<%# Eval("parent_id")%>' runat="server"></asp:Label>

                        </td>
                        <td class="text-center">
                            <asp:Label ID="lblParentOrder" Text='<%# Eval("parent_order")%>' runat="server"></asp:Label>
                        </td>
                        <td class="text-center">
                            <asp:Label ID="lblChildOrder" Text='<%# Eval("child_order")%>' runat="server"></asp:Label>
                        </td>
                        <td class="text-left">
                          
                            <asp:Label ID="lblCssClass" Text='<%# Eval("cssclass")%>' runat="server"></asp:Label>
                        </td>
                       
                        <td class="text-center">
                            <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                            <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("menu_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                            <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("menu_master_id")%>' CssClass="label label-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                        </td>
                       
                        <td class=" text-left btn-group btn-group-xs ">
                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("menu_master_id")%>' CssClass="btn btn-default text-center" data-toggle="tooltip" data-placement="bottom" title="Click to edit" data-original-title="Basic tooltp"><i class="fa fa-pencil"></i></asp:LinkButton>
                           
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>         
                </FooterTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdnMenuMasterID" runat="server" />
          </div>
            <!-- END Responsive Full Content -->
        </div>
        <!-- END Responsive Full Block -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headbottom" runat="server">


</asp:Content>
