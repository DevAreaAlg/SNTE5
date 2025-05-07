<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_PROVEEDORRECURSO.aspx.vb" Inherits="SNTE5.PLD_OPE_PROVEEDORRECURSO" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PENSIONES Servicios en Línea</title>
    <link href="css/estilosmascore.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">                                                                                  document.onkeydown = function () { if (122 == event.keyCode) { event.keyCode = 0; return false; } }</script>

    <script type="text/javascript">
        //If you want the FillForm function to fire when the user closes the window, you can do this
        //window.onunload = function() { doParentActivatePostBack(); }
        //if you want it to do the fillform on a button click
        //<input onclick ="doParentActivatePostBack()"/>
        function doParentActivatePostBack() {
            if (window.opener.ActivatePostBack) {
                window.opener.ActivatePostBack();
            }
        }
    </script>
    <meta charset="utf-8"/>
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins"/>

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Sección Oficinas" />
    <meta name="author" content="GeeksLabs" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="img/favicon.png" />

    <!-- Bootstrap CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap theme -->
    <link href="/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />
    <link href="/css/style-responsive.css" rel="stylesheet" />

    <!-- bootstrap -->
    <script src="/js/bootstrap.min.js"></script>

    <!-- nice scroll -->
    <script src="/js/jquery.scrollTo.min.js"></script>
    <script src="/js/jquery.nicescroll.js" type="text/javascript"></script>

    <!-- custom select -->
    <script src="/js/jquery.customSelect.min.js"></script>
    <script src="/assets/chart-master/Chart.js"></script>

    <!--custome script for all page-->
    <script src="js/scripts.js"></script>

    <script language="JavaScript" type="text/javascript">

        function Minimize() {
            window.innerWidth = 100;
            window.innerHeight = 100;
            window.screenX = screen.width;
            window.screenY = screen.height;
            alwaysLowered = true;
        }

        function Maximize() {
            window.innerWidth = screen.width;
            window.innerHeight = screen.height;
            window.screenX = 50;
            window.screenY = 50;
            alwaysLowered = false;
        }
        function postea() {
            //window.opener.location.reload();
        }
    </script>
</head>
<body>
    <div class="panel-body">

        <%--//<ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />--%>

         <div class="module_subsec flex_center">
             <span class="text_input_nice_label">Proveedor de Recursos:</span>
         </div>

        <asp:DataGrid ID="dag_Proveedor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
            GridLines="None">
            <HeaderStyle CssClass="table_header"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="IDPROVEEDOR" HeaderText="Número de Afiliado">
                    <ItemStyle Width="100px"/></asp:BoundColumn>
                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                    <ItemStyle Width="300px"/></asp:BoundColumn>
                <asp:BoundColumn DataField="RELACION" HeaderText="Relación">
                    <ItemStyle Width="150px" /></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>

        <div class="module_subsec flex_center">
            <span class="text_input_nice_label">Propietarios Reales:</span>
        </div>
         
        <asp:DataGrid ID="dag_Propietario" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
            GridLines="None">
            <HeaderStyle CssClass="table_header"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="IDPROPIETARIO" HeaderText="Número de Afiliado">
                    <ItemStyle Width="100px"/>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                    <ItemStyle Width="300px"/>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RELACION" HeaderText="Relación">
                    <ItemStyle Width="150px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PORCENTAJE" HeaderText="Porcentaje">
                    <ItemStyle Width="100px" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>

        <asp:Label ID="lbl_Info" runat="server" CssClass="alerta"></asp:Label>
        
    </div>

</body>
</html>

