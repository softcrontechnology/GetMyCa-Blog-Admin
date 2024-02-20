<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="category-master.aspx.cs" Inherits="Healing2Peace.Modules.Blog.category_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

        function showStatusBlogModal() {
            $("#modal-status-blog").modal('show');

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
                <%--<li runat="server" id="memusubscribe">
                    <a href="/manage-subscribe"><i class="fa fa-user-circle"></i>Subscriber</a>
                </li>--%>
                
                <li>
                    <a href="#"><i class="fa fa-cogs"></i>Settings</a>
                </li>
            </ul>
        </div>
        <!-- END Dashboard 2 Header -->
        
         <ul class="breadcrumb breadcrumb-top">
            <li>Blog-Category Master</li>
            <li><a href="#">Add Blog Category</a></li>
        </ul>

                          <div id="modal-content">
             <div id="modal-status-blog" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <!-- Modal Header -->
                         <div class="modal-header text-center">
                             <h2 class="modal-title"><i class="fa fa-th-list"></i>Blog-Category Master</h2>
                         </div>
                         <!-- END Modal Header -->
                         <asp:HiddenField ID="hdnfStatusBlogCategory" runat="server" />
                         <!-- Modal Body -->


                         <div class="modal-body">
                             <div onsubmit="return false;">

                                 <div class="row">
                                     <div class="col-md-12">

                                         <!-- Normal Form Block -->
                                         <div class="block">
                                             <!-- Normal Form Title -->
                                             <div class="block-title">
                                                 <div class="block-options pull-right">
                                                 </div>
                                                 <h2><strong>Active/Deactive</strong> Blog-Category </h2>
                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group">
                                                         <h5>
                                                             <label for="example-nf-email" class="text-danger ">Are you sure want to Set Active / Deactive Blog-Category ?</label>
                                                         </h5>
                                                     </div>
                                                 </div>

                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group form-actions text-right">
                                                         <%--<button type="submit" class="btn btn-sm btn-primary"> Yes </button>--%>
                                                         <asp:LinkButton ID="lnkbtnStatus" runat="server" OnClick="lnkbtnStatus_Click" CssClass="btn btn-primary form-actions"> Yes </asp:LinkButton>
                                                         <button type="button" class="btn btn-default" data-dismiss="modal">No </button>


                                                     </div>
                                                 </div>

                                             </div>

                                         </div>
                                     </div>

                                 </div>


                             </div>
                         </div>
                         <!-- END Modal Body -->
                     </div>
                 </div>
             </div>

         </div>

          <div class="row">

            <div class="col-md-12">

                <!-- Normal Form Block -->
                <div class="block">
                    <!-- Normal Form Title -->
                    <div class="block-title">
                        <div class="block-options pull-right">
                           <%-- <a href="javascript:void(0)" class="btn btn-alt btn-sm btn-default toggle-bordered enable-tooltip" data-toggle="button" title="Toggles .form-bordered class">All Party </a>--%>
                        </div>
                        <h2><strong>Blog Category</strong> Master </h2>
                    </div>
                    <!-- END Normal Form Title -->

                    <!-- Normal Form Content -->
                    <asp:HiddenField ID="hdnId" runat="server" />
                    <div class="row">
                            <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <label for="txtDisplayName">Heading Name <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" placeholder="Enter categories name here.."></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                               <label for="txtDisplayName">Heading Type <span class="text-danger">*</span></label>
                                 <asp:DropDownList ID="ddlCategoryType" runat="server" class="select-chosen" data-placeholder="Choose Category Type.." Style="width: 250px;">
                                     <asp:ListItem  Selected="True" Value="category" Text="Category"></asp:ListItem>
                                        <asp:ListItem  Value="tag" Text="Tag"></asp:ListItem>
                                        <asp:ListItem  Value="ic" Text="Item"></asp:ListItem>
                                       
                                  </asp:DropDownList>
                            </div>
                        </div>
                         
                        </div>
                     
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group form-actions" style="float: right">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-sm btn-warning"  OnClick="btnReset_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Category" class="btn btn-sm btn-success" Visible="false"  OnClick="btnUpdate_Click"  />
                                <asp:Button ID="btnSubmit" runat="server" Text="Add Category" class="btn btn-sm btn-primary" OnClick="btnSubmit_Click"  />
                            </div>
                        </div>
                    </div>

                    </div>
                </div>
              </div>
        
       
      
         <!-- End Normal Form Content -->

        <!-- Responsive Full Block -->
        <div class="block full">
            <!-- All Deals Title -->
            <div class="block-title clearfix">
                <div class="block-options pull-right">
                    <a href="#" class="btn btn-alt btn-sm btn-default" data-toggle="tooltip" data-placement="bottom" title="Refresh Data"><i class="fa fa-refresh"></i></a>
                </div>
                <h2><i class="fa fa-list"></i>&nbsp;Manage <strong>Blog Categories</strong></h2>
            </div>
            <!-- END All Deals Title -->

            <!-- All Deals Content -->
            <div class="table-responsive">
               
                <asp:Repeater ID="rptrBlogCategoriesList" runat="server" OnItemCommand="rptrBlogCategoriesList_ItemCommand" OnItemDataBound="rptrBlogCategoriesList_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example-datatable" class="table table-vcenter table-condensed table-bordered">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 40px;">Sr.No</th>
                                    <%--<th class="text-center" style="width: 40px;">ID</th>--%>
                                    <th style="width: 200px;">Heading Name</th>
                                    <th class="text-center" style="width: 100px;">Heading Type</th>
                                    <th class="text-center" style="width: 80px;">Date </th>
                                    <th  class="text-center" style="width: 60px;">Status </th>
                                    <th class="text-center" style="width: 60px;">Action </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %>    </td>

                            <%--<td  class="text-center">
                                <asp:Label ID="lblCategoryId" Text='<%#Eval("blog_category_id")%>' runat="server"></asp:Label>
                            </td>--%>
                             <td>
                                <asp:Label ID="lblBlogCategory" Text='<%#Eval("category_name")%>' CssClass="text text-primary" runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblCategoryType" Text='<%#Eval("category_type")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblCreatedOn" Text='<%#Eval("created_on","{0: dd MMM yyyy}")%>' runat="server"></asp:Label>
                            </td>
                            
                            <td class="text-center">
                                <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                                <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                                <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="label label-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                            </td>
                            
                            <td class="text-center">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("blog_category_id")%>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" data-placement="bottom" title="Click to edit" data-original-title="Basic tooltp"><i class="fa fa-pencil"></i></asp:LinkButton>
                                <%--<a href="#" data-toggle="tooltip" title="Delete" class="btn btn-xs btn-danger"><i class="fa fa-times"></i></a>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="HdnFBlogCategoriesMasterId" runat="server" />
            </div>
            <!-- END All Deals Content -->
        </div>
        <!-- END Responsive Full Block -->
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headbottom" runat="server">

    <script src="../../Content/js/helpers/ckeditor/ckeditor.js"></script>
    <script src="../../Content/js/pages/formsValidation.js"></script>
    <script>$(function () { FormsValidation.init(); });</script>
</asp:Content>
