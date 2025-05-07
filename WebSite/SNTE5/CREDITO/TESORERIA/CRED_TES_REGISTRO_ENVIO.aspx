<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_TES_REGISTRO_ENVIO.aspx.vb" Inherits="SNTE5.CRED_TES_REGISTRO_ENVIO" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>ENVIOS PENDIENTES</title>

        <meta charset="utf-8"/>
        <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins"/>
 
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
         <link href="~/css/style.css" id="Link1" rel="stylesheet" type="text/css"/>
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

    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="module_subsec">
            <asp:Label runat="server" ID="lbl_status" CssClass="text_input_nice_label"></asp:Label>
        </div>        
        
    <div class="module_subsec overflow_x shadow" >  
        <asp:DataGrid ID="grd_operaciones" runat="server" AutoGenerateColumns="False"  class="table table-striped"
                   GridLines="None" >
            <HeaderStyle CssClass="table_header"/>
            <Columns>
                <asp:BoundColumn DataField="ID_OPERACION" HeaderText="Id. Operación">
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ORIGEN" HeaderText="Origen">
                    <ItemStyle Width="15%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                    <ItemStyle Width="15%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ENVIA" HeaderText="Envía">
                    <ItemStyle Width="15%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TRANSPORTA" HeaderText="Transporta">
                    <ItemStyle Width="15%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RECIBE" HeaderText="Recibe">
                    <ItemStyle Width="15%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                    <ItemStyle Width="8%" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="CONSTANCIA" Text="Constancia">
                    <ItemStyle Width="10%" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    </div>
    </form>
</body>
</html>

