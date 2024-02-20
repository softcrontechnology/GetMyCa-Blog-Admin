<%@ Page Title="" Language="C#" MasterPageFile="~/Layout/AdminMaster.Master" AutoEventWireup="true" CodeBehind="add-gallery.aspx.cs" Inherits="Healing2Peace.Modules.Admin.add_gallery" %>
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
    </script>

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
            <li>Gallery Master</li>
            <li><a href="#">Add Gallery</a></li>
        </ul>


        <div id="modal-content">
             <div id="modal-status-blog" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <!-- Modal Header -->
                         <div class="modal-header text-center">
                             <h2 class="modal-title"><i class="fa fa-th-list"></i>Gallery Master</h2>
                         </div>
                         <!-- END Modal Header -->
                         <asp:HiddenField ID="hdnfStatusTestimonial" runat="server" />
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
                                                 <h2><strong>Active/Deactive</strong> Gallery </h2>
                                             </div>

                                             <div class="row">
                                                 <div class="col-md-12">
                                                     <div class="form-group">
                                                         <h5>
                                                             <label for="example-nf-email" class="text-danger ">Are you sure want to Set Active / Deactive Gallery ?</label>
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
                        <h2><strong>Gallery</strong> Master </h2>
                    </div>
                    <!-- END Normal Form Title -->

                    <!-- Normal Form Content -->
                    <asp:HiddenField ID="hdnId" runat="server" />
                    <div class="row">
                        <div class="col-md-9">
                       
                            <div class="col-md-12 col-sm-12">
                            <div class="form-group">
                                <label for="txtDisplayName">Image Name <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter name here.." MaxLength="40" Minlength="3"></asp:TextBox>
                            </div>
                        </div>
                            

                            </div>
                        <div class="col-md-3">
                                <div class="col-md-12 col-sm-12">
                                <div class="block full ">
                                    <!-- Upload Picture -->
                                    <div class="block-title">
                                        <h2><i class="fa fa-image"></i> Image  <strong></strong></h2>
                                    </div>
                                    <div class="dz-default dz-message">
                                        <asp:Image ID="imagePreview" runat="server" ImageUrl="/Content/img/placeholders/avatars/avatar2.jpg" Width="100%" Height="210px" alt="testimonal Person Picture" />
                                    </div>

                                    <asp:FileUpload ID="fuUploadedFile" runat="server" Style="border: 1px solid #dbe1e8; width: 100%; border-radius: 3px; padding: 3px; margin-top: 10px" CssClass="form-control-file profile-picture" onchange="ShowImagePreview(this);" />

                                    <!-- END Upload Picture -->
                                </div>
                            </div>
                            </div>


                        </div>

                     
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group form-actions" style="float: right">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-sm btn-warning" OnClick="btnReset_Click" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Gallery" class="btn btn-sm btn-success" Visible="false" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Add Gallery" class="btn btn-sm btn-primary" OnClick="btnSubmit_Click" /> 
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
                <h2><i class="fa fa-list"></i>&nbsp;Manage <strong>Gallery List</strong></h2>
            </div>
            <!-- END All Deals Title -->

            <!-- All Deals Content -->
            <div class="table-responsive">
               
                <asp:Repeater ID="rptrGalleryList" runat="server" OnItemCommand="rptrGalleryList_ItemCommand" OnItemDataBound="rptrGalleryList_ItemDataBound">
                    <HeaderTemplate>
                        <table id="example-datatable" class="table table-vcenter table-condensed table-bordered">
                            <thead>
                                <tr>
                                    <th class="text-center" style="width: 20px;">ID</th>
                                    <th class="text-center" style="width: 100px;"> Name</th>
                                    <th class="text-center" style="width: 80px;">Image</th>
                                    <th class="text-center" style="width: 50px;">Date</th>
                                    <th  class="text-center" style="width: 20px;">Status </th>
                                    <th class="text-center" style="width: 20px;">Action </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="text-center"><%# Container.ItemIndex + 1 %>    </td>

                             <td class="text-center">
                                <asp:Label ID="lblName" Text='<%#Eval("name")%>' CssClass="text text-primary" runat="server"></asp:Label>
                            </td>
                            

                            <td class="text-center" style="width: 50px; height: 50px">
                                <asp:Label ID="lblpic" Text='<%#Eval("image")%>' Visible="false" runat="server"></asp:Label>

                                <asp:Image ID="lblimageurl" runat="server" ImageUrl='<%#Eval("image")%>' Style="height: 80px; width: 80px;" />
                            </td>
                            
                            <td class="text-center">
                                            <asp:Label ID="lblCreatedOn" Text='<%#Eval("created_on","{0: dd MMM yyyy}")%>' runat="server"></asp:Label>
                                        </td> 

                            <td class="text-center">
                                <asp:Label ID="lblIsActive" Text='<%#Eval("is_active")%>' runat="server" Visible="false" />
                                <asp:LinkButton ID="btnstatusactive" runat="server" CommandName="active" CommandArgument='<%# Eval("gallery_master_id")%>' CssClass="label label-success" data-toggle="tooltip" data-placement="bottom" title="Click to Deactivate" data-original-title="Basic tooltp">Active</asp:LinkButton>
                                <asp:LinkButton ID="btnstatusdeactive" runat="server" CommandName="active" CommandArgument='<%# Eval("gallery_master_id")%>' CssClass="label label-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Activate" data-original-title="Basic tooltp">Deactive</asp:LinkButton>

                            </td>
                             
                            <td class="text-center">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("gallery_master_id")%>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" data-placement="bottom" title="Click to edit" data-original-title="Basic tooltp"><i class="fa fa-pencil"></i></asp:LinkButton>
                              
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>         
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="HdnFGalleryMasterId" runat="server" />
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
