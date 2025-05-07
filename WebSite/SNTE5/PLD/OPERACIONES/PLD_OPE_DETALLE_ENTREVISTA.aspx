<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_DETALLE_ENTREVISTA.aspx.vb" Inherits="SNTE5.PLD_OPE_DETALLE_ENTREVISTA" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Detalle Entrevista</title>
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
            var myContentToPrint = document.getElementById('Resumen_Indicador');
            var myWindowToPrint = window.open('', '', 'width=800,height=1000,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');

            myWindowToPrint.document.write(myContentToPrint.innerHTML);
            myWindowToPrint.document.close();
            myWindowToPrint.focus();
            
            var css = myWindowToPrint.document.createElement("link");
            css.setAttribute("href", "/css/style.css");
            css.setAttribute("rel", "stylesheet");
            css.setAttribute("type", "text/css");
            myWindowToPrint.document.head.appendChild(css);
                        
            myWindowToPrint.print();
            myWindowToPrint.close();
        }
   </script>
  </head>
 <body onload="history.forward(1);Minimize();Maximize();">
    <form id="form1" runat="server">
    <!-----------------------------
                RESUMEN
    --------------------------------->
    <section class="panel" runat="server"   id="Section1">
       

        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="btn_imprimir">
                            Imprimir
                            <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:10px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div>

                <div id="Resumen_Indicador">
                     <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Detalle entrevista</td>
                            </tr>
                         
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_IDAlerta" runat="server" Text="Id alerta: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_IDAlerta1" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="3">
                                <asp:Label ID="lbl_Persona" runat="server" Text="Afiliado: " class="module_subsec_elements module_subsec_small-elements"></asp:Label> &nbsp;
                                <asp:Label ID="lbl_Persona1" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                     
                         <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_Folio" runat="server" Text="Expediente: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_Folio1" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="3">
                              <asp:Label ID="lbl_Usuario" runat="server" Text="Usuario: " class="module_subsec_elements module_subsec_small-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_Usuario1" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                      
                           <tr>
                            <td colspan="3">
                                  <asp:Label ID="lbl_FechaAlerta" runat="server" Text="Fecha de alerta: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                     &nbsp;
                                <asp:Label ID="lbl_FechaAlerta1" runat="server" class="texto"></asp:Label>
                             </td>
                             <td colspan="3">
                                  <asp:Label ID="lbl_Sucursal" runat="server" Text="Sucursal: " class="module_subsec_elements module_subsec_small-elements"></asp:Label>

                                     &nbsp;
                                <asp:Label ID="lbl_Sucursal1" runat="server" class="texto"></asp:Label>
                             </td>
                        </tr>
                           <tr>
                          
                        </tr>
                           <tr>
                            <td colspan="1">
                                   <asp:Label ID="lbl_RealizoEnt" runat="server" Text="¿Realizó entrevista?: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>

                                <asp:Label ID="lbl_RealizoEnt1" runat="server" class="texto"></asp:Label>
                             </td>
                               
                                <td colspan="1">
                                    <label id="lbl_RazonNoEnt1" class="texto" visible="false" >Razón por no realizar la entrevisita:</label>
                                     <asp:Label ID="lbl_RazonnNoEnt1" runat="server" class="texto" visible="false" ></asp:Label>
                                </td>
                             
                                
               
                     
                        </tr>
                         <tr>
                              <td>
                                    <label id="lbl_ObservacionesNoEnt" class="texto">Observaciones:</label>
                                   <asp:Label ID="lbl_ObservacionesNoEnt1" runat="server" class="texto"></asp:Label>
                                </td>
                              
                             
                               <td>
                                    <asp:LinkButton ID="lnk_EntrevistaPLDDigit" runat="server" CssClass="module_subsec_elements module_subsec_bigger-elements" Text="Ver Entrevista Digitalizada"></asp:LinkButton>

                               </td>

                         </tr>
                          
                                  
                      
                        <tr>
                            <td colspan="6">
                                 <asp:Label ID="lbl_OrigenDep" runat="server" Text="Origen del depósito: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_OrigenDep1" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_PerDeposito" runat="server" Text="Periodicidad de depósito: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_PerDeposito1" runat="server" class="texto"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lbl_PuestoPolitico" runat="server" Text="Puesto político: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_PuestoPolitico1" runat="server" class="texto"></asp:Label>
                                 &nbsp;
                                  <asp:Label ID="lbl_AntPuestoPolitico1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Label>

                            </td>
                        </tr>
                        
                      
                        <tr class="table_header">
                            <td colspan="6">Extranjero</td>
                          
                            
                        </tr>
                        <tr>
                             <td colspan="6">
                               
                                <asp:Label ID="lbl_TiempoEnMex" runat="server" Text="Tiempo viviendo en México: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_TiempoEnMex1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>

                         <tr>
                             <td colspan="6">
                               
                               <asp:Label ID="lbl_RazonEnMex" runat="server" Text="Motivo de vivir en México: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_RazonEnMex1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                             <td colspan="6">
                               
                                 <asp:Label ID="lbl_TiempoMasMex" runat="server" Text="Cuánto tiempo más estará en México: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>                                 &nbsp;
                                <asp:Label ID="lbl_TiempoMasMex1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                          
                        </tr>
                        
                         <tr>
                            <td colspan="6">
                               
                                 <asp:Label ID="lbl_CatPasaporte" runat="server" Text="Categoría de pasaporte: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_CatPasaporte1" runat="server" class="texto"></asp:Label>
                            </td>
                            </tr>
                        
                       
                       <tr class="table_header">
                       <td colspan="6">Moral</td>    
                       </tr>
                            <tr>
                              <td colspan="6">
                               
                                 <asp:Label ID="lbl_MoralNombre" runat="server" Text="Nombre de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_MoralNombre1" runat="server" class="texto"></asp:Label>
                            </td>
                           </tr>
                          <tr>
                             <td colspan="6">
                               
                               <asp:Label ID="lbl_MoralRelacion" runat="server" Text="Relación con la empresa: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_MoralRelacion1" runat="server" class="texto"></asp:Label>
                            </td>
                           </tr>
                          <tr>
                              <td colspan="6">
                             <asp:Label ID="lbl_MoralDireccion" runat="server" Text="Dirección de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_MoralDireccion1" runat="server" class="texto"></asp:Label>
                            </td>
                              </tr>
                          <tr>
                           <td colspan="6">
                               
                                 <asp:Label ID="lbl_MoralNacionalidad" runat="server" Text="Nacionalidad de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_MoralNacionalidad1" runat="server" class="texto"></asp:Label>
                            </td>
                            </tr>
                          <tr>
                              <td colspan="6">
                               
                                <asp:Label ID="Label1" runat="server" Text="Nacionalidad de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_MoralTelefono1" runat="server" class="texto"></asp:Label>
                            </td>
                              </tr>
                         <tr>
                             <td colspan="6">
                               
                                <asp:Label ID="lbl_MoralCelular" runat="server" Text="Núm. celular de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_MoralCelular1" runat="server" class="texto"></asp:Label>
                            </td>
                        

                         </tr>
                       
                        
                        
                         <tr class="table_header">
                            <td colspan="6">Tercero</td>
                           
                        </tr>
                        <tr>
                             <td colspan="6">
                               
                                <asp:Label ID="lbl_TerceroNombre" runat="server" Text="Nombre de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_TerceroNombre1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                             <td colspan="6">
                               
                               <asp:Label ID="lbl_TerceroRelacion" runat="server" Text="Relación con el afiliado: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_TerceroRelacion1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                           <td colspan="6">
                               
                                <asp:Label ID="lbl_TerceroDireccion" runat="server" Text="Dirección de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_TerceroDireccion1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                            <td colspan="6">
                               
                                <asp:Label ID="lbl_TerceroNacionalidad" runat="server" Text="Nacionalidad de quien deposita: " class="module_subsec_elements module_subsec_big-elements"></asp:Label>
                                 &nbsp;
                                <asp:Label ID="lbl_TerceroNacionalidad1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                           <tr>
                            <td colspan="6">
                               
                                 <label id="lbl_ObservacionesSiEnt" class="module_subsec_elements module_subsec_big-elements">Observaciones:</label>
                                 &nbsp;
                                 <asp:Label ID="lbl_ObservacionesSiEnt1" runat="server" class="texto"></asp:Label>
                            </td>
                            
                        </tr>
                                           
                    </table>

                </div>                 
            </div>
            <%--<asp:Panel runat="server" ID="pnl_NoRealizoEnt">
            <table cellspacing="0" cellpadding="0" width="700px" align="center" bgcolor="#ffffff" border="0">
                <tr>
                    <td width="200px">
                        <label id="lbl_RazonNoEnt" class="texto" >Razon por no realizar la entrevisita:</label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_RazonNoEnt1" runat="server" class="texto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lbl_ObservacionesNoEnt" class="texto">Observaciones:</label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_ObservacionesNoEnt1" runat="server" class="texto"></asp:Label>
                    </td>
                </tr>
            </table>  
        </asp:Panel>--%>

        
        </div>
    </section>
  
 </form>
</body>
</html>



