<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_VEN_PERSONAS_AUTORIZADAS.aspx.vb" Inherits="SNTE5.CAP_VEN_PERSONAS_AUTORIZADAS" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>FIRMAS AUTORIZADAS</title>


        <meta charset="utf-8">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins">
 
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content="Sección Oficinas"/>
    <meta name="author" content="GeeksLabs"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>





    <link href="css/style.css" id="cssArchivo" rel="stylesheet" type="text/css"/>
    <link rel="shortcut icon" href="img/favicon.png"/>
    
    <!-- Bootstrap CSS -->    
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <!-- bootstrap theme -->
    <link href="css/bootstrap-theme.css" rel="stylesheet"/>
     <link href="css/style.css" id="Link1" rel="stylesheet" type="text/css"/>
    <link href="css/style-responsive.css" rel="stylesheet" />
     <script src="js/jquery.js"></script>
	<script src="js/jquery-ui-1.10.4.min.js"></script>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src= "Scripts/jquery-1.3.2.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
    
    <!-- bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- nice scroll -->
    <script src="js/jquery.scrollTo.min.js"></script>
    <script src="js/jquery.nicescroll.js" type="text/javascript"></script>
    
    <!-- custom select -->
    <script src="js/jquery.customSelect.min.js" ></script>
	<script src="assets/chart-master/Chart.js"></script>
    
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
            window.screenX = 0;
            window.screenY = 0;
            alwaysLowered = false;
        }
        function postea() {
            //window.opener.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="margin:20px">       
        <section class="panel" id="panel_personaaut">
            <header class="panel-heading">
                <span>PERSONAS AUTORIZADAS</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
                   
                    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />
                                                    
                    <table >
                                       
                        <tr align="right">  
                            <asp:DataGrid ID="DAG_PerAcep" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                    <Columns> 
                                        <asp:BoundColumn DataField="idfirma" HeaderText="Id Persona">
                                            <ItemStyle Width="100px" HorizontalAlign="Center"   />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                            <ItemStyle HorizontalAlign="Center" Width="300px" />
                                        </asp:BoundColumn>
                                    </Columns> 
                            </asp:DataGrid>
                        </tr>  
                                        
                    </table>
                                    
                    <br />
                    <asp:Label ID="lbl_Info" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                
                </div>
            </div>
        </section>
    </form>
</body>
</html>

