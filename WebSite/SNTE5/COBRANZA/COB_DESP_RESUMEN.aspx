<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DESP_RESUMEN.aspx.vb" Inherits="SNTE5.COB_DESP_RESUMEN" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <!-----------------------------
                RESUMEN
    --------------------------------->
    <section class="panel" runat="server"   id="Section1">
        <header class="panel_header_folder panel-heading">
            <span>Resumen</span>
           </header>

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

                <div id="Resumen_Indicador">
                     <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos del abogado</td>
                            </tr>
                          <tr class="table_header">
                           <td colspan="6">Personales</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <label id="Label5" runat="server" class="texto">Id abogado:</label> &nbsp;
                                <asp:Label ID="lbl_id_abogado" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <label id="Label2" runat="server" class="texto">Nombre:</label> &nbsp;
                                <asp:Label ID="lbl_nombre_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6">
                                <label id="Label6" runat="server" class="texto">CURP:</label> &nbsp;
                                <asp:Label ID="lbl_curp_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6">
                                <label id="Label20" runat="server" class="texto">RFC:</label> &nbsp;
                                <asp:Label ID="lbl_rfc_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                           <tr>
                            <td colspan="6">
                                <label id="Label22" runat="server" class="texto">Notas:</label> &nbsp;
                                <asp:Label ID="lbl_nota_res" runat="server" class="texto"></asp:Label>
                             </td>
                        </tr>
                          
                                  
                                 
                                   
                                 
                        <tr class="table_header">
                            <td colspan="6">Domicilio</td>
                        </tr> 
                        <tr>
                            <td colspan="6">
                                <label id="Label1" runat="server" class="texto">Calle y número:</label> &nbsp;
                                <asp:Label ID="lbl_callenum_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6">
                                <label id="Label4" runat="server" class="texto">Asentamiento:</label> &nbsp;
                                <asp:Label ID="lbl_asentamiento_res" runat="server" class="texto"></asp:Label>
                            </td>
                           
                        </tr>
                         <tr>
                            <td colspan="6">
                                <label id="Label33" runat="server" class="texto">Municipio:</label> &nbsp;
                                <asp:Label ID="lbl_municipio_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <label id="Label27" runat="server" class="texto">Estado:</label> &nbsp;
                                <asp:Label ID="lbl_estado_res" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                      
                        <tr class="table_header">
                            <td colspan="3">Contacto</td>
                            <td colspan="3">Primer contacto</td>
                        </tr>
                        <tr>
                            <td>
                               
                                <label id="Label8" runat="server" class="texto">Nombre:</label> &nbsp;
                                <asp:Label ID="lbl_c1_nombre" runat="server" class="texto"></asp:Label>
                            </td>
                            <td colspan="2">
                                <label id="Label9" runat="server" class="texto">Celular:</label> &nbsp;
                                <asp:Label ID="lbl_c1_tel" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="2">
                                <label id="Label14" runat="server" class="texto">Núm trabajo:</label> &nbsp;
                                <asp:Label ID="lbl_c1_tel_ofi" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="2">
                                <label id="Label16" runat="server" class="texto">Correo:</label> &nbsp;
                                <asp:Label ID="lbl_c1_email" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         
                        <tr class="table_header">
                            <td colspan="3">Contacto</td>
                            <td colspan="3">Segundo contacto</td>
                        </tr>
                        <tr>
                            <td>
                               
                                <label id="Label10" runat="server" class="texto">Nombre:</label> &nbsp;
                                <asp:Label ID="lbl_c2_nombre" runat="server" class="texto"></asp:Label>
                            </td>
                            <td colspan="2">
                                <label id="Label12" runat="server" class="texto">Celular:</label> &nbsp;
                                <asp:Label ID="lbl_c2_tel" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="2">
                                <label id="Label19" runat="server" class="texto">Núm trabajo:</label> &nbsp;
                                <asp:Label ID="lbl_c2_tel_ofi" runat="server" class="texto"></asp:Label>
                            </td>
                             <td colspan="2">
                                <label id="Label25" runat="server" class="texto">Correo:</label> &nbsp;
                                <asp:Label ID="lbl_c2_email" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr class="table_header">
                            <td colspan="6">Asignación de usuarios</td>
                        </tr>
                      <%--  <tr>
                            <td colspan="2">
                                <label id="Label20" runat="server" class="texto">Variable:</label> &nbsp;
                                <asp:Label ID="lbl_variable" runat="server" class="texto"></asp:Label>
                            </td>
                          <td colspan="2">
                                <label id="Label22" runat="server" class="texto">Etiqueta:</label> &nbsp;
                                    <asp:Label ID="lbl_etiqueta" runat="server" class="texto"></asp:Label>
                            </td>
                          <td colspan="3">
                                <label id="Label24" runat="server" class="texto">Frecuencia de medición:</label> &nbsp;
                                <asp:Label ID="lbl_periodov1" runat="server" class="texto"></asp:Label>
                            </td>--%>
                          
                       <%-- </tr>--%>

                     
                    </table>

                        <asp:GridView ID="dag_users" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                        <Columns>
                          
                            <asp:BoundField  DataField="USUARIO" HeaderText="Usuario">
                                <ItemStyle Width="85%"></ItemStyle>
                            </asp:BoundField>                             
                          
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
              <%--       <section class="panel" >                          
                        <div class="overflow_x shadow" >    --%>
                            
                       <asp:DataGrid ID="dag_val" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" 
                            GridLines="None" Width="100%" border="1" text-align="left" >
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn ItemStyle-Width="10%" DataField="IDVARIABLE" HeaderText="" Visible="false">
                                </asp:BoundColumn>
                                
                                <asp:BoundColumn ItemStyle-Width="10%" DataField="VARIABLE" HeaderText="Variable">
                                </asp:BoundColumn>

                                <asp:BoundColumn ItemStyle-Width="40%" DataField="ETIQUETA" HeaderText="Etiqueta">
                                </asp:BoundColumn>   

                                
                                <asp:BoundColumn ItemStyle-Width="40%" DataField="PERIODO" HeaderText="FRECUENCIA DE MEDICIÓN">
                                </asp:BoundColumn>   
                                                                          
                            </Columns>                        
                        </asp:DataGrid>
                       <%--</div>      
                    </section>--%>
                </div>                 
            </div>
        </div>
    </section>
  

</asp:Content>


