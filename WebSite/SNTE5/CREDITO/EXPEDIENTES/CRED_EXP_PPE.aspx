<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_PPE.aspx.vb" Inherits="SNTE5.CRED_EXP_PPE" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Persona Políticamente Expuesta</title>
    <meta charset="utf-8">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins">

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
    <script type="text/javascript">

        function imprimir() {
            var myContentToPrint = document.getElementById('form1');
            var myWindowToPrint = window.open('', '', 'toolbar=0,scrollbars=yes,status=0,resizable=0,location=0,directories=0');
            myWindowToPrint.document.write(myContentToPrint.innerHTML);
            myWindowToPrint.document.close();
            myWindowToPrint.focus();
            myWindowToPrint.print();
            myWindowToPrint.close();
        }
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
          </script>
  </head>
 <body onload="history.forward(1);Minimize();Maximize();">
    <form id="form1" runat="server">

     <asp:Panel ID="pnl_DatosOpe" runat="server">
            <section class="panel" runat="server" id="Section1">
                <header class="panel-heading">
                    <span><asp:Label ID="lbl_Folio" runat="server" Enabled="false" ></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                            <asp:Textbox ID="lbl_Cliente1" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                    </div>
                </div>
            </section>
         </asp:Panel>
      <section class="panel">
        <div class="overflow_x shadow">
            <!-- Tabla de Personas con coincidencias encontradas-->
              <asp:DataGrid ID="dag_PPE" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="CLAVEDOC" Visible="false">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CATEGORIA" HeaderText="Categoría">
                                    <ItemStyle Width="210px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PUESTO" HeaderText="Puesto">
                                    <ItemStyle Width="210px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NIVEL" HeaderText="Nivel">
                                    <ItemStyle Width="50px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CALIFICACION" HeaderText="Calificación">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="VER" HeaderText="" Text="Reporte">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonColumn>
                                </Columns>
                        </asp:DataGrid>
        </div>

            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Label ID="lbl_Status" runat="server" Cssclass="alerta"></asp:Label> 
            </div>

    </section>
   </form>
</body>
</html>




