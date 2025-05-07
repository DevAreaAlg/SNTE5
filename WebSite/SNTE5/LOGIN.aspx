<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LOGIN.aspx.vb" Inherits="SNTE5.LOGIN" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template">
    <meta name="author" content="GeeksLabs">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal">
    <link rel="shortcut icon" href="img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>SNTE SECCION 5</title>
    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- bootstrap theme -->
    <link href="css/bootstrap-theme.css" rel="stylesheet">
    <!--external css-->
    <!-- font icon -->
    <link href="css/elegant-icons-style.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="css/style.css" rel="stylesheet">
    <link href="css/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
     <script id="clientEventHandlersJS" type="text/javascript">
         function GMA() {

             if (document.getElementById('hdn_counter').value == 1) {
                 var locator = new ActiveXObject("WbemScripting.SWbemLocator");
                 var service = locator.ConnectServer(".");
                 var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                 var e = new Enumerator(properties);
                 var c = new String;
                 var p;
                 for (; !e.atEnd(); e.moveNext()) {
                     p = e.item();
                     if (p.MACAddress != null) {
                         c += p.MACAddress + ",";
                     }
                 }
                 document.getElementById('hdn_mcs').value = c;
                 document.getElementById('hdn_counter').value = 2;
                 __doPostBack('', '');
             }
         }

     </script>
</head>

<body class="login-img3-body" onload="GMA();">

    <div class="container">

       <%-- <form class="login-form" runat="server">
          
            <div class="login-wrap" style="align-content: center">
                <p class="login-img"><i class="icon_lock_alt"></i></p>--%>
         <form class="login-form" runat="server">
               
        <div style="display:flex; justify-content:center;"><div class="logo-img"></div> </div >
        <div class="login-wrap" style="align-content:center">
            <%--<div class="input-group text_input_nice_div">
                <div>
                <asp:RadioButton runat="server" GroupName="LogIn" Text="Usuario Local" ID="rad_LogInLocal" CssClass="text_input_nice_label" Checked="True"></asp:RadioButton>
                <asp:RadioButton runat="server" GroupName="LogIn" Text="Usuario Windows" CssClass="text_input_nice_label" ID="rad_LogInAD"></asp:RadioButton>
                </div>
                <span class="text_input_nice_label">Tipo de autentificación:</span>
            </div>--%>
            <div class="input-group">
                <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true"
            EnableScriptLocalization="true" /> 
                <span class="input-group-addon"><i class="icon_profile"></i></span>
                <asp:TextBox ID="txt_Usuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom"
                                            ValidChars="Ññ" TargetControlID="txt_Usuario" />
            </div>
            <div class="input-group">
                <span class="input-group-addon"><i class="icon_key_alt"></i></span>
                <asp:TextBox ID="txt_Password" MaxLength="15" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña"></asp:TextBox>
            </div>
            <span class="pull-right" style="color:black">*Nota: Revise que su usuario no contenga espacios y aparezca tal como le ha sido proporcionado</span>
            <label class="checkbox">
                <span class="pull-right"><a href="CORE\SEGURIDAD\CORE_SEG_CONTRASENA_RECUPERAR.aspx">Recuperar Contraseña</a></span>
            </label>
            <asp:Label runat="server" ID="lbl_ErrorLogIn" Style="color: red;"></asp:Label>
            <asp:Button ID="btn_LogIn" runat="server" Text="Entrar" CssClass="btn btn-primary btn-lg btn-block" />
            
        </div>

        <input type="hidden" name="hdn_mcs" id="hdn_mcs" runat="server" value="" />
        <input type="hidden" name="hdn_counter" id="hdn_counter" runat="server" value="1" />
    </form>
    <div class="text-right">
    </div>
</div>

</body>


</html>

