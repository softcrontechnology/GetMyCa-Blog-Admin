<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="subscribe-master.aspx.cs" Inherits="Healing2Peace.Modules.Subscriber.subscribe_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        
         <ul class="breadcrumb breadcrumb-top">
            <li>Subscribe Master</li>
            <li><a href="#">Subscriber List</a></li>
        </ul>

       
      
         <!-- End Normal Form Content -->

             <!-- All Orders Block -->
                        <div class="block full">
                            <!-- All Deals Title -->
            <div class="block-title clearfix">
                <div class="block-options pull-right">
                   <%-- <a href="#" class="btn btn-alt btn-sm btn-default" data-toggle="tooltip" data-placement="bottom" title="Refresh Data"><i class="fa fa-refresh"></i></a>--%>

                    <asp:LinkButton ID="btnDownloadExcel" runat="server" CssClass="btn btn-alt btn-sm btn-default toggle-bordered enable-tooltip" data-toggle="tooltip" data-placement="bottom" title="Download Excel" data-original-title="Basic tooltp" Text="Export"  OnClick="btnDownloadExcel_Click"></asp:LinkButton>

                </div>
                <h2><i class="fa fa-list"></i>&nbsp;Manage <strong>Subscriber List </strong></h2>
            </div>
            <!-- END All Deals Title -->

                           <!-- All Deals Content -->
            <div class="table-responsive">

            <asp:Repeater ID="rptrSubscribe" runat="server" OnItemCommand="rptrSubscribe_ItemCommand" OnItemDataBound="rptrSubscribe_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example-datatable" class="table table-vcenter table-condensed table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 50px;">Sr.No</th>
                                    <%--<th class="text-center">Id</th>--%>
                                    <th class="text-center">Email Address</th>
                                    <th class="text-center">Date</th>
                                    <%--<th class="text-center">Status</th>
                                    <th class="text-center">Action</th>--%>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %>    </td>

                            <%--<td class="text-center">
                                <asp:Label ID="lblSubscribeId" Text='<%#Eval("subscribe_master_id")%>' runat="server"></asp:Label>
                            </td>--%>
                            <td class="text-center">
                                <asp:Label ID="lblSubscribeEmail" Text='<%#Eval("subscribe_email")%>' CssClass="text text-primary" runat="server"></asp:Label>
                            </td>
                           
                            <td class="text-center">
                                <asp:Label ID="lblCreatedOn" Text='<%#Eval("created_on","{0: dd MMM yyyy}")%>' runat="server"></asp:Label>
                            </td>
                             <%--<td class="text-center">
                                <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                                <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("subscribe_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                                <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("subscribe_master_id")%>' CssClass="label label-primary" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                            </td>
                              <td class="text-center">
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("subscribe_master_id")%>' CssClass="btn btn-xs btn-danger" data-toggle="tooltip" data-placement="bottom" title="Click to delete" data-original-title="Basic tooltp"><i class="fa fa-times"></i></asp:LinkButton>
                            </td>--%>
                           
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>         
                    </FooterTemplate>
                </asp:Repeater>

            </div>
            <!-- END All Deals Content -->
                        </div>
                        <!-- END All Orders Block -->


    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headbottom" runat="server">

    <script src="../../Content/js/helpers/ckeditor/ckeditor.js"></script>
    <script src="../../Content/js/pages/formsValidation.js"></script>
    <script>$(function () { FormsValidation.init(); });</script>

</asp:Content>
