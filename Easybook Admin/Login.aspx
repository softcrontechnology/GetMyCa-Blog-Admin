<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Healing2Peace.Login" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <meta charset="utf-8" />


    <title>GetMyCA - Admin Portal</title>

    <meta name="description" content="GetMyCA Admin Portal created by Softcron." />
    <meta name="author" content="Softcron" />
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />

    <!-- Icons -->
    <!-- The following icons can be replaced with your own, they are used by desktop and mobile browsers -->
    <link rel="shortcut icon" href="Content/img/favicon.png" />
    
    <!-- Stylesheets -->
    <!-- Bootstrap is included in its original form, unaltered -->
    <link rel="stylesheet" href="Content/css/bootstrap.min.css" />
    
    <!-- Related styles of various icon packs and plugins -->
    <link rel="stylesheet" href="Content/css/plugins.css" />

    <!-- The main stylesheet of this template. All Bootstrap overwrites are defined in here -->
    <link rel="stylesheet" href="Content/css/main.css" />

    <!-- Include a specific file here from css/themes/ folder to alter the default theme of the template -->

    <!-- The themes stylesheet of this template (for using specific theme color in individual elements - must included last) -->
    <link rel="stylesheet" href="Content/css/themes.css" />
    <!-- END Stylesheets -->

    <!-- jQuery, Bootstrap.js, jQuery plugins and Custom JS code -->
    <script src="Content/js/vendor/jquery.min.js"></script>
    <script src="Content/js/vendor/bootstrap.min.js"></script>
    <script src="Content/js/plugins.js"></script>
    <script src="Content/js/app.js"></script>

     <script src="Custom/toast.js"></script>
    <script src="Custom/Validate.js"></script>
    <!-- Modernizr (browser feature detection library) -->
    <script src="Content/js/vendor/modernizr.min.js"></script>
    
        <style type="text/css">
            .color-overlay {
               position: absolute;
               top: 0;
               left: 0;
               width: 100%;
               height: 100%;
               background-color: lightblue;
               opacity: 0.9;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <!-- Login Full Background -->
            <!-- For best results use an image with a resolution of 1280x1280 pixels (prefer a blurred image for smaller file size) -->
            <img src="Content/img/placeholders/backgrounds/login_full_bg.jpg" alt="Login Full Background" class="full-bg animation-pulseSlow" />
            <!-- END Login Full Background -->

            <!-- Login Container -->
            <div id="login-container" class="animation-fadeIn">
                <!-- Login Title -->
                <div class="login-title text-center">
                    <h1><i class="gi gi-flash"></i> <strong>GetMyCA Blog Admin</strong><br /> 
                  <%-- <img src="Content/img/white-logo.png" width="180x180"/>--%>
                      <small>Please <strong>Login</strong></small></h1>
                </div>
                <!-- END Login Title -->

                <!-- Login Block -->
                <div class="block push-bit">
                    <!-- Login Form -->
                    <div id="form-login" class="form-horizontal form-bordered form-control-borderless">
                        <asp:ScriptManager ID="manager" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="up1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                                            <input name="user" type="text" id="email"  class="form-control input-lg" placeholder="Login Id" tabindex="1" />
                                            <%--<label for="email">Email</label>--%>
                                           <%--  onkeydown="enter()"--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="gi gi-asterisk"></i></span>
                                            <input type="password" id="password" name="password" maxlength="20" class="form-control input-lg" placeholder="Password" aria-pressed="false" tabindex="2" />
                                            <%--<label for="password">Password</label>--%>
                                            <asp:Label ID="lblError" runat="server" Style="color: #fff" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="val_terms" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="form-group form-actions">
                            <div class="col-xs-4">
                                <label>
                                    
                                    <asp:CheckBox ID="val_terms" runat="server"   Visible="false"  />
                                    <span></span>
                                </label>
                            </div>
                            <div class="col-xs-8 text-right">
                                <%--<button type="submit" class="btn btn-sm btn-primary"><i class="fa fa-angle-right"></i> Login to Dashboard</button>--%>
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-sm btn-primary" Text=" Login to Dashboard" OnClick="btnLogin_Click" />
                                <%--  <asp:LinkButton ID="" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnLogin_Click" TabIndex="3"> Login to Dashboard</asp:LinkButton>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 text-center">
                                <%--<a href="/login" id="link-register-login"><small>Term & Conditions</small></a> -
                                <a href="/login" id="link-reminder-login"><small>Forgot password?</small></a>--%>
                                <%--/recover-password--%>
                            </div>
                        </div>
                    </div>
                    <!-- END Login Form -->
                    <!-- Register Form -->
                   
                    <!-- END Register Form -->
                </div>
                <!-- END Login Block -->
            </div>
            <!-- END Login Container -->
            <script src="/Custom/toast.js"></script>
            <!-- jQuery, Bootstrap.js, jQuery plugins and Custom JS code -->
            <script src="Content/js/vendor/jquery.min.js"></script>
            <script src="Content/js/vendor/bootstrap.min.js"></script>
            <script src="Content/js/plugins.js"></script>
            <script src="Content/js/app.js"></script>
            <script src="Custom/Validate.js"></script>
            <!-- Load and execute javascript code used only in this page -->
            <%--<script src="js/pages/login.js"></script>--%>
            <script src="Content/js/pages/login.js"></script>
            <script>$(function () { Login.init(); });</script>
            <script type="text/javascript">
                $(function () {
                    $('#password').keypress(function (event) {
                        if (event.keyCode == 13) {
                            $('#btnLogin').click();
                        }
                    })
                })
            </script>


        </div>
    </form>
</body>
</html>
