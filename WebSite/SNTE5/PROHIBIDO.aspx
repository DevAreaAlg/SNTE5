<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PROHIBIDO.aspx.vb" Inherits="SNTE5.PROHIBIDO" %>

<!DOCTYPE html>
<script runat="server">

    Protected Sub btn_Volver_Click(sender As Object, e As EventArgs)

    End Sub
</script>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template">
    <meta name="author" content="GeeksLabs">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal">
    <link rel="shortcut icon" href="img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>

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
</head>

  <body class="login-img3-body">

    <div class="container">

      <form class="login-form" runat="server">
        <h1 align="center">Advertencia:</h1>      
        <div class="login-wrap">
           
       <h3> Lo sentimos, no tiene el permiso para el módulo al que desea ingresar</h3>
          
            <asp:Button ID="btn_Volver" runat="server" Text="Volver" CssClass="btn btn-primary btn-lg btn-block" OnClick="btn_Volver_Click"/>
        </div>
      </form>
    </div>

  </body>
</html>
