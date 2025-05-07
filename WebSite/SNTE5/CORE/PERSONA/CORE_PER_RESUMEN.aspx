<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CORE_PER_RESUMEN.aspx.vb" Inherits="SNTE5.CORE_PER_RESUMEN" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
    <title>Resumen Persona</title>
   
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Sección Oficinas" />
    <meta name="author" content="GeeksLabs" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="img/favicon.png" />

    <!-- Bootstrap CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/font-awesome.min.css" rel="stylesheet" />
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

        <section class="panel" runat="server" id="Section1">
       

        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="btn_imprimir">
                            Imprimir
                         <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div>
                    <div id="P_Resumen">

                     <table id="t_resumen" border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                           <td colspan="6">Personales</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                   <asp:Label ID="lbl_claverese" runat="server" CssClass="texto" Text="Número control:"></asp:Label> &nbsp;
                                 <asp:Label ID="lbl_claveres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_nombresrese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Nombre:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombreres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            <asp:Label ID="lbl_curprese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="CURP:"></asp:Label> &nbsp;
                            <asp:Label ID="lbl_curpres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_rfcrese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="RFC:"></asp:Label> &nbsp;
                                <asp:Label ID="lbl_rfcres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_fechanacrese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Fecha nacimiento:"></asp:Label> &nbsp;
                                <asp:Label ID="lbl_fechanacres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_sexorese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Sexo:"></asp:Label> &nbsp;
                                <asp:Label ID="lbl_sexores" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                           
                        <tr class="table_header">
                            <td colspan="6">Domicilio</td>
                        </tr> 
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_calleynorese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Calle y número.:"></asp:Label>
                                <asp:Label ID="lbl_calleynores" runat="server" CssClass="module_subsec_elements module_subsec_bigger-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_localidadrese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Asentamiento:"></asp:Label>
                            <asp:Label ID="lbl_localidadres" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>                       
                            <td colspan="3">
                               <asp:Label ID="lbl_municipiorese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Municipio:"></asp:Label>
                               <asp:Label ID="lbl_municipiores" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">                             
                                <asp:Label ID="lbl_estadorese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Estado:"></asp:Label>
                                <asp:Label ID="lbl_estadores" runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                           
                        <tr class="table_header">
                            <td colspan="6">Contacto</td>
                        </tr> 
                         <tr>
                            <td colspan="3">
                                <asp:Label  runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Teléfono casa:"></asp:Label>
                                <asp:Label ID="lbl_telCasa" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                            </td>
                              <td colspan="3">
                                <asp:Label  runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Teléfono celular:"></asp:Label>
                                <asp:Label ID="lbl_telCel" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_corre" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Correo electrónico:"></asp:Label>
                                <asp:Label ID="lbl_corres" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                         <tr class="table_header">
                            <td colspan="6">Laboral</td>
                         </tr>
                       
                         <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_antiemprese" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Antigüedad:"></asp:Label>
                                <asp:Label ID="lbl_antiempres" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                            </td> 
                        </tr>
                        <tr>
                             <td colspan="3">
                                    <asp:Label ID="lbl_sueldorese" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Sueldo:"></asp:Label>
                                    <asp:Label ID="lbl_sueldores" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                             </td>
                             <td colspan="3">    
                                <asp:Label ID="lbl_periodosueldorese" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Periodicidad:"></asp:Label>
                                <asp:Label ID="lbl_periodosueldores" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=""></asp:Label>
                            </td>
                         </tr>
                         <tr>
                             <td colspan="6">
                             </td>
                         </tr>
                         <tr>
                             <td colspan="6">
                                 <div id="cuentas" class="overflow_x shadow module_subsec">
                                     <asp:DataGrid ID="dag_Cuentas" runat="server"  CssClass="table table-striped" GridLines="None" Width="100%" >
                                         <Columns>
                                         </Columns>
                                     <HeaderStyle CssClass="table_header"></HeaderStyle>
                                     </asp:DataGrid>
                                 </div>
                             </td>
                         </tr>
                       </table>
                     </div>
                   </div>
                </div>
            </section>

    </form>
</body>
</html>

