<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="contact-master.aspx.cs" Inherits="Healing2Peace.Modules.Contact.contact_master" %>
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
            <li>Contact Master</li>
            <li><a href="#">Contact List</a></li>
        </ul>

       
      
         <!-- End Normal Form Content -->

             <!-- All Orders Block -->
                        <div class="block full">
                            <!-- All Deals Title -->
            <div class="block-title clearfix">
                <div class="block-options pull-right">
                    <a href="#" class="btn btn-alt btn-sm btn-default" data-toggle="tooltip" data-placement="bottom" title="Refresh Data"><i class="fa fa-refresh"></i></a>
                </div>
                <h2><i class="fa fa-list"></i>&nbsp;Manage <strong>Contact </strong></h2>
            </div>
            <!-- END All Deals Title -->

                           <!-- All Deals Content -->
            <div class="table-responsive">
               
                <asp:Repeater ID="rptrContactList" runat="server" OnItemCommand="rptrContactList_ItemCommand" OnItemDataBound="rptrContactList_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example-datatable" class="table table-vcenter table-condensed table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 30px;">ID</th>
                                    <th class="text-center" style="width: 100px;"> Name</th>
                                    <th class="text-center" style="width: 100px;">Email</th>
                                    <th class="text-center" style="width: 100px;">PhoneNo</th>
                                    <th class="text-center" style="width: 40px;">Date </th>
                                    <th class="text-center" style="width: 200px;">Subject</th>
                                    <th class="text-center" style="width: 300px;">Message</th>
                                    
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %>    </td>

                            <td  class="text-center">
                                <asp:Label ID="lblFullName" Text='<%#Eval("full_name")%>' CssClass="text text-primary" runat="server"></asp:Label>
                            </td>
                             <td  class="text-center">
                                <asp:Label ID="lblEmail" Text='<%#Eval("email")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblPhoneNumber" Text='<%#Eval("phone_number")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblCreatedOn" Text='<%#Eval("created_on","{0: dd MMM yyyy}")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblSubject" Text='<%#Eval("subject")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblMessage" Text='<%#Eval("message")%>' runat="server"></asp:Label>
                            </td>

                            
                            
                            <%--<td class="text-center">
                                <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                                <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                                <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="label label-primary" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                            </td>
                            
                            <td class="text-center">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" data-placement="bottom" title="Click to edit" data-original-title="Basic tooltp"><i class="fa fa-pencil"></i></asp:LinkButton>
                                <a href="#" data-toggle="tooltip" title="Delete" class="btn btn-xs btn-danger"><i class="fa fa-times"></i></a>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="HdnFContactMasterId" runat="server" />
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
