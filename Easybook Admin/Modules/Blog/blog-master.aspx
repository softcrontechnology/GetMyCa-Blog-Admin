<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeBehind="blog-master.aspx.cs" Inherits="Healing2Peace.Modules.Blog.blog_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pagecontent_imagePreview').prop('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function ShowThumbnailImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pagecontent_ThumbnailimagePreview').prop('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function ShowInnerImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#pagecontent_InnerimagePreview').prop('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

     <script>
         function showPublishBlogModal() {
             $("#modal-publish-blog").modal('show');

         }

         function showfeaturedBlogModal() {
             $("#modal-featured-blog").modal('show');

         }

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
            <li>Blog Master</li>
            <li><a href="#">Add Blog</a></li>
        </ul>


         <div id="modal-content">

             <!-- User Settings, modal which opens from Settings link (found in top right user menu) and the Cog link (found in sidebar user info) -->
             <div id="modal-publish-blog" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <!-- Modal Header -->
                         <div class="modal-header text-center">
                             <h2 class="modal-title"><i class="fa fa-th-list"></i>Blog Master</h2>
                         </div>
                         <!-- END Modal Header -->

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
                                                 <h2><strong>Published</strong> Blog </h2>
                                             </div>


                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group">

                                                         <h5>
                                                             <label for="example-nf-email" class="text-danger">Are you sure want to Published / Unpublished Blog ?</label>
                                                         </h5>
                                                     </div>
                                                 </div>

                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group form-actions text-right">
                                                         <%--<button type="submit" class="btn btn-sm btn-primary"> Yes </button>--%>
                                                         <asp:LinkButton ID="btnPublishedBlog" runat="server" OnClick="btnPublishedBlog_Click" CssClass="btn btn-primary form-actions"> Yes </asp:LinkButton>
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
             <!-- END User Settings -->

             <div id="modal-featured-blog" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <!-- Modal Header -->
                         <div class="modal-header text-center">
                             <h2 class="modal-title"><i class="fa fa-th-list"></i>Blog Master</h2>
                         </div>
                         <!-- END Modal Header -->

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
                                                 <h2><strong>Featured</strong> Blog </h2>
                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group">
                                                         <h5>
                                                             <label for="example-nf-email" class="text-danger center-block">Are you sure want to Set Featured / Unfeatured Blog ?</label>
                                                         </h5>
                                                     </div>
                                                 </div>

                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group form-actions text-right">
                                                         <%--<button type="submit" class="btn btn-sm btn-primary"> Yes </button>--%>
                                                         <asp:LinkButton ID="lnkbtnFeatured" runat="server" OnClick="lnkbtnFeatured_Click" CssClass="btn btn-primary form-actions"> Yes </asp:LinkButton>
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

             <div id="modal-status-blog" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <!-- Modal Header -->
                         <div class="modal-header text-center">
                             <h2 class="modal-title"><i class="fa fa-th-list"></i>Blog Master</h2>
                         </div>
                         <!-- END Modal Header -->
                          <asp:HiddenField ID="hdnfStatusBlog" runat="server" />
                          <asp:HiddenField ID="HdnFBlogCategoriesMasterId" runat="server" />
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
                                                 <h2><strong>Active/Deactive</strong> Blog </h2>
                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group">
                                                         <h5>
                                                             <label for="example-nf-email" class="text-danger center-block">Are you sure want to Set Active / Deactive Blog ?</label>
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
                        <h2><strong>Blog</strong> Master </h2>
                    </div>
                    <!-- END Normal Form Title -->

                    <!-- Normal Form Content -->
                    <asp:HiddenField ID="hdnId" runat="server" />
                    <div class="row">
                        <div class="col-md-9">
                        <div class="col-md-4 col-sm-12">
                            <div class="form-group">
                                <label for="txtDisplayName">Select Category <span class="text-danger">*</span></label>
                                 <asp:DropDownList ID="ddlBlogCategoryType" runat="server" class="select-chosen" data-placeholder="Choose Category.." Style="width: 250px;">
                                     
                                  </asp:DropDownList>
                            </div>
                        </div>

                            <div class="col-md-4 col-sm-12">
                            <div class="form-group">
                                <label for="txtDisplayName">Author Name <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control" placeholder="Enter author name here.." MaxLength="16" Minlength="3"></asp:TextBox>
                            </div>
                        </div>

                            <div class="col-md-2 col-sm-12">
                            <div class="form-group">
                                <label for="txtDisplayName">Blog Views</label>
                                 <asp:TextBox ID="txtBlogViews" runat="server" CssClass="form-control" onkeypress="return Number(event)" placeholder="Enter blog views "></asp:TextBox>
                            </div>
                        </div>

                            <div class="col-md-2 col-sm-12">
                            <div class="form-group">
                                 <label for="example-nf-email">Set Featured</label>
                                        <asp:DropDownList ID="ddlSetFeatured" runat="server" class="select-chosen" data-placeholder="Select Feature..." Style="width: 250px;">
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        <asp:ListItem  Selected="True" Value="0" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                        </div>
                        
                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-title">Page Title <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtPageTitle" runat="server" CssClass="form-control" placeholder="Enter page title " MinLength="10" MaxLength="1000" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-title"> Meta Key <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtMetaKey" runat="server" CssClass="form-control" placeholder="Enter meta key " TextMode="MultiLine" Rows="4" MinLength="10" MaxLength="2000" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-title"> Meta Description <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtMetaDescription" runat="server" CssClass="form-control" placeholder="Enter meta description " TextMode="MultiLine" Rows="4" MinLength="10" MaxLength="2500" ></asp:TextBox>
                                </div>
                            </div>

                        <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-title">Blog Title <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtBlogTitle" runat="server" CssClass="form-control" placeholder="Enter blog title " MinLength="10" MaxLength="100" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-title">Blog Friendly URL </label>
                                       <asp:TextBox ID="txtfriendlyUrl" runat="server" CssClass="form-control" placeholder="Enter blog friendly url " MinLength="10" MaxLength="100" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="blog-desc">Blog Description <span class="text-danger">*</span></label>
                                       <asp:TextBox ID="txtBlogDescription" runat="server" CssClass="ckeditor" TextMode="Multiline" ></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-12 col-sm-12">
                                <div class="form-group">
                                    <label for="ddlItemName">Select Tag</label>
                                <asp:ListBox ID="ddlTagList" runat="server" CssClass="select-chosen select2 " placeholder="Select Tag " SelectionMode="Multiple">
                                      <asp:ListItem></asp:ListItem>
                                 </asp:ListBox>
                                </div>
                            </div>
                            

                            </div>
                        <div class="col-md-3">
                                <div class="col-md-12 col-sm-12">
                                <div class="block full ">
                                    <!-- Upload Picture -->
                                    <div class="block-title">
                                        <h2><i class="fa fa-image"></i> Blog Outer Image  <strong></strong></h2>
                                    </div>
                                    <div class="dz-default dz-message">
                                        <asp:Image ID="imagePreview" runat="server" ImageUrl="/Content/img/placeholders/avatars/avatar2.jpg" Width="100%" Height="210px" alt="Category Picture" />
                                    </div>

                                    <asp:FileUpload ID="fuUploadedFile" runat="server" Style="border: 1px solid #dbe1e8; width: 100%; border-radius: 3px; padding: 3px; margin-top: 10px" CssClass="form-control-file profile-picture" onchange="ShowImagePreview(this);" />

                                    <!-- END Upload Picture -->
                                </div>
                            </div>
                             <div class="col-md-12 col-sm-12">
                                <div class="block full ">
                                    <!-- Upload Picture -->
                                    <div class="block-title">
                                        <h2><i class="fa fa-image"></i> Blog Thumbnail Image  <strong></strong></h2>
                                    </div>
                                    <div class="dz-default dz-message text-center">
                                        <asp:Image ID="ThumbnailimagePreview" runat="server" ImageUrl="/Content/img/placeholders/avatars/avatar2.jpg" Width="100%" Height="210px" alt="Category Picture" />
                                    </div>

                                    <asp:FileUpload ID="fuImageThumbail" runat="server" Style="border: 1px solid #dbe1e8; width: 100%; border-radius: 3px; padding: 3px; margin-top: 10px" CssClass="form-control-file profile-picture" onchange="ShowThumbnailImagePreview(this);" />

                                    <!-- END Upload Picture -->
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12">
                                <div class="block full ">
                                    <!-- Upload Picture -->
                                    <div class="block-title">
                                        <h2><i class="fa fa-image"></i> Blog Inner Image <strong></strong></h2>
                                    </div>
                                    <div class="dz-default dz-message">
                                        <asp:Image ID="InnerimagePreview" runat="server" ImageUrl="/Content/img/placeholders/avatars/avatar2.jpg" Width="100%" Height="210px" alt="Category Picture" />
                                    </div>

                                    <asp:FileUpload ID="fuInner" runat="server" Style="border: 1px solid #dbe1e8; width: 100%; border-radius: 3px; padding: 3px; margin-top: 10px" CssClass="form-control-file profile-picture" onchange="ShowInnerImagePreview(this);" />

                                    <!-- END Upload Picture -->
                                </div>
                            </div>

                            </div>

                        

                        </div>

                     
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group form-actions" style="float: right">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-sm btn-warning"  OnClick="btnReset_Click"  />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Blog" class="btn btn-sm btn-success" Visible="false"  OnClick="btnUpdate_Click"  />
                                <asp:Button ID="btnSubmit" runat="server" Text="Add Blog" class="btn btn-sm btn-primary"  OnClick="btnSubmit_Click"  />
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
                <h2><i class="fa fa-list"></i>&nbsp;Manage <strong>Blog List</strong></h2>
            </div>
            <!-- END All Deals Title -->

            <!-- All Deals Content -->
            <div class="table-responsive">
               
                <asp:Repeater ID="rptrBlogList" runat="server" OnItemCommand="rptrBlogList_ItemCommand" OnItemDataBound="rptrBlogList_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example-datatable" class="table table-vcenter table-condensed table-bordered">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 20px;">ID</th>
                                    <th class="text-center" style="width: 100px;">Category Name</th>
                                    <th style="width: 400px;">Blog Title </th>
                                    <th class="text-center" style="width: 100px;">Author Name</th>
                                    <th class="text-center" style="width: 100px;">Blog Image</th>
                                    <th class="text-center" style="width: 100px;">Blog Views</th>
                                    <th  class="text-center" style="width: 20px;">Status </th>
                                    <th  class="text-center" style="width: 30px;">Featured</th>
                                    <th class="text-center" style="width: 30px;">Published</th>
                                    <th class="text-center" style="width: 30px;">Action </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                        <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %>    </td> 

                             <td class="text-center">
                                <asp:Label ID="lblBlogCategory" Text='<%#Eval("category_name")%>' runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBlogTitle" Text='<%#Eval("blog_title")%>' CssClass="text text-primary" runat="server"></asp:Label>
                            </td>
                            <td class="text-center">
                                <asp:Label ID="lblAuthorName" Text='<%#Eval("author_name")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center" style="width: 50px; height: 50px">
                                <asp:Label ID="lblpic" Text='<%#Eval("blog_outer_image")%>' Visible="false" runat="server"></asp:Label>

                                <asp:Image ID="lblimageurl" runat="server" ImageUrl='<%#Eval("blog_outer_image")%>' Style="height: 50px; width: 50px;" />
                            </td>

                             <td class="text-center">
                                <asp:Label ID="lblBlogViews" Text='<%#Eval("blog_view")%>' runat="server"></asp:Label>
                            </td>

                            <td class="text-center">
                                <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                                <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                                <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("blog_master_id")%>' CssClass="label label-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                            </td>
                             <td class="text-center">
                                <asp:Label ID="lblIsFeatured" Text='<%#Eval("is_featured")%>' Visible="false" runat="server"></asp:Label>
                                 <asp:LinkButton ID="btnFeatured" runat="server" CommandName="isfeatured" CommandArgument='<%# Eval("blog_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Set Featured" data-original-title="Basic tooltp"></asp:LinkButton>
                               
                            </td>
                            <td class="text-center">
                                <asp:Label ID="lblIsPublished" Text='<%#Eval("is_published")%>' Visible="false" runat="server"></asp:Label>
                                <asp:LinkButton ID="btnPublished" runat="server" CommandName="ispublished" CommandArgument='<%# Eval("blog_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Published" data-original-title="Basic tooltp"></asp:LinkButton>
                               
                            </td>

                            <td class="text-center">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("blog_master_id")%>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" data-placement="bottom" title="Click to edit" data-original-title="Basic tooltp"><i class="fa fa-pencil"></i></asp:LinkButton>
                                <%--<a href="#" data-toggle="tooltip" title="Delete" class="btn btn-xs btn-danger"><i class="fa fa-times"></i></a>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="HdnFBlogMasterId" runat="server" />
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

