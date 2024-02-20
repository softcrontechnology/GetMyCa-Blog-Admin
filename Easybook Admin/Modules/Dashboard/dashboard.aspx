<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Healing2Peace.Modules.Dashboard.dashboard" %>
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
                <%--<li runat="server" id="memusubscribe">
                    <a href="/manage-subscribe"><i class="fa fa-user-circle"></i>Subscriber</a>
                </li>--%>
                
                <li>
                    <a href="#"><i class="fa fa-cogs"></i>Settings</a>
                </li>
            </ul>
        </div>
        <!-- END Dashboard 2 Header -->
        
          <%--<div class="content-header">
            <div class="header-section">
                <h1>Welcome <strong>Admin</strong><br>
                            <small>You Look Awesome!</small></h1>
            </div>
        </div>--%>

         <!-- Quick Stats -->
        <div class="row text-center">
            <div class="col-sm-6 col-lg-3">
                <a href="/manage-blog-category" class="widget widget-hover-effect2">
                    <div class="widget-extra themed-background">
                        <h4 class="widget-content-light"><strong>Total</strong> Blog Categories</h4>
                    </div>
                    <div class="widget-extra-full"><span class="h2 themed-color-dark  animation-expandOpen">
                        <asp:Label ID="lblBlogCategory" runat="server" Text="0"></asp:Label>
                   </span></div>
                </a>
            </div>
            <div class="col-sm-6 col-lg-3">
                <a href="/manage-blog" class="widget widget-hover-effect2">
                    <div class="widget-extra themed-background">
                        <h4 class="widget-content-light"><strong>Total</strong> Blog</h4>
                    </div>
                    <div class="widget-extra-full"><span class="h2 themed-color-dark animation-expandOpen">
                        
                        <asp:Label ID="lblBlog" runat="server" Text="0"></asp:Label>
                                                   </span></div>
                </a>
            </div>
            <div class="col-sm-6 col-lg-3">
                <a href="/manage-blog-category" class="widget widget-hover-effect2">
                    <div class="widget-extra themed-background">
                        <h4 class="widget-content-light"><strong>Total</strong> Tags</h4>
                    </div>
                    <div class="widget-extra-full"><span class="h2 themed-color-dark animation-expandOpen">
                         <asp:Label ID="lblTotalTag" runat="server" Text="0"></asp:Label>
                           </span></div>
                </a>
            </div>
           <%-- <div class="col-sm-6 col-lg-3">
                <a href="/manage-subscribe" class="widget widget-hover-effect2">
                    <div class="widget-extra themed-background">
                        <h4 class="widget-content-light"><strong>Total</strong> Subscriber</h4>
                    </div>
                    <div class="widget-extra-full"><span class="h2 themed-color-dark animation-expandOpen">
                         <asp:Label ID="lblTotalSubscribe" runat="server" Text="0"></asp:Label>
                           </span></div>
                </a>
            </div>--%>
        </div>
        <!-- END Quick Stats -->
        
       
      
         <!-- End Normal Form Content -->

      
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headbottom" runat="server">
</asp:Content>
