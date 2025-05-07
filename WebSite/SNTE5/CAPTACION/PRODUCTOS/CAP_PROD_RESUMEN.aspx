<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_RESUMEN.aspx.vb" Inherits="SNTE5.CAP_PROD_RESUMEN" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <script type="text/javascript">

     function imprimir() {
         var myContentToPrint = document.getElementById('resumen');
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
     <section class="panel" runat="server"   id="Section1">
        <header class="panel_header_folder panel-heading">
            <span>Resumen del Producto de Captación</span>
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


                 <asp:Panel ID="pnl_res" runat="server">
             
                    
                     <div id="resumen">
                     <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos Generales</td>
                            </tr>              
                      <tr>
                      <td class="style9">
                       <asp:Label runat="server" Text="Id Producto:" CssClass="texto" 
                        ID="lbl_idprod"></asp:Label>
                       &nbsp;
                      <asp:Label runat="server" CssClass="texto" ID="lbl_idprodres"></asp:Label>
                      
                      </td>
                      
                      </tr>

                 
                  
                    <tr>
                    <td>
                        <asp:Label runat="server" Text="Nombre del Producto:" CssClass="texto"
                        ID="lbl_nombre"></asp:Label>
                     &nbsp;
                     <asp:label runat="server" CssClass="texto" ID="lbl_nombreres" ></asp:label>
                    
                    </td>
                    
                    
                    </tr>
                    <tr>
                    
                    <td class="style9">
                    
                     <asp:Label ID="lbl_descripcion" runat="server" Text="Descripción:" CssClass="texto"
                      ></asp:Label>
                     &nbsp;
                        <asp:Label ID="lbl_descripcionres" runat="server" CssClass="texto"></asp:Label>
                    </td>
                    
                    </tr>
                    
                                   
                    <tr>
                    <td>
                       <asp:Label ID="lbl_tipopersona" runat="server" Text="Tipo de Persona:" CssClass="texto"
                         ></asp:Label>
                    &nbsp;
                       <asp:Label ID="lbl_tipopersonares" runat="server" CssClass="texto"
                        ></asp:Label>
                    </td>
                    
                    </tr>
                    <tr>
                    <td>
                     <asp:Label ID="lbl_tipoprod" runat="server" Text="Tipo de Producto:" CssClass="texto"
                       ></asp:Label>
                     &nbsp;
                        <asp:Label ID="lbl_tipoprodres" runat="server" CssClass="texto" 
                           ></asp:Label>
                    </td>
                    </tr>
                                       
                        </table>
                                     
                     <table border="1" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Parámetros Generales</td>
                  </tr>           
                  
                  <tr>
                  <td>
                   <asp:Label ID="lbl_requisitos" runat="server" CssClass="texto" Font-Bold="True" 
                        Text="Configuración requerida" Width="180px"></asp:Label>   
                  </td>
                  </tr>
                 <tr>
                  <td>
                    <asp:Label ID="lbl_saldomin" runat="server" Text="Saldo minímo:" CssClass="texto"
                        ></asp:Label>                 
                  
                  &nbsp;
                  <asp:Label ID="lbl_saldominimo" runat="server" CssClass="texto" 
                       ></asp:Label>
                  </td>
                  </tr>
                  
                 
                  <tr>
                  <td>
                   <asp:Label ID="lbl_garantia" runat="server" Text="Tipo de Captación:" 
                          CssClass="texto"
                        ></asp:Label>
                   &nbsp;
                   <asp:Label ID="lbl_tipocap" runat="server" CssClass="texto" 
                        ></asp:Label>
                  
                  </td>
                  </tr>
                   <tr>
                  <td>
                   <asp:Label ID="lbl_mancomunado" runat="server" Text="Producto mancomunado:" 
                          CssClass="texto"
                       ></asp:Label>
                   &nbsp;
                   <asp:Label ID="lbl_mancomunadoproducto" runat="server" CssClass="texto" 
                        ></asp:Label>
                  
                  </td>
                  </tr>
                   <tr>
                  <td>
                   <asp:Label ID="LBL_PPE" runat="server" Text="Investigación PPE:" 
                          CssClass="texto" 
                      ></asp:Label>
                   &nbsp;
                   <asp:Label ID="lbl_ppe_resp" runat="server" CssClass="texto" 
                       
					   ></asp:Label>
                  
                  </td>
                  </tr>
                   <tr>
                  <td>
                   <asp:Label ID="Label4" runat="server" Text="Relación Funcionarios:" 
                          CssClass="texto" 
                        ></asp:Label>
                   &nbsp;
                   <asp:Label ID="lbl_reqfun" runat="server" CssClass="texto"
                        ></asp:Label>
                  
                  </td>
                  </tr>
                  
                  
                  </table>

                     <table border="1" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Beneficiarios y Referencias</td>
                  </tr>   
                  <tr>
                 
                      <td >
                  
                   </td>
                   <td class="style4">
                   <asp:Label ID="lbl_confreqmin" runat="server" CssClass="texto" 
                           Width="54px">Mínimo:</asp:Label>
                   </td>
                   <td>
                       &nbsp;<asp:Label ID="lbl_conreqmax" runat="server" CssClass="texto" 
                           Width="52px">Máximo:</asp:Label>
                   
                   
                   </td>
                   </tr>
                      <tr>
                   
                   <td class="style3">
                   
                     <asp:Label ID="lbl_ben" runat="server" CssClass="texto" Text="Número de Beneficiarios:"
                        Width="191px"></asp:Label>   </td>
                        <td class="style4">
                        
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_benresmin" runat="server" CssClass="texto" 
                                Width="46px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lbl_benresmax" runat="server" CssClass="texto" 
                            Width="72px" Height="16px"></asp:Label>
                    </td>
                
                   
                     </tr>
                   <tr>
                   <td class="style3" >
                     <asp:Label ID="lbl_ref" runat="server" Text="Número de Referecias:" CssClass="texto"
                       Width="155px"></asp:Label>
                   </td>
                   <td class="style4" >
                       &nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lbl_refresmin" runat="server" CssClass="texto"
                           Width="32px" Height="16px"></asp:Label>
                   </td>
                   <td >
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lbl_refresmax" runat="server" CssClass="texto" 
                            Width="80px"></asp:Label>
                   </td>
                 
                   </tr>
                
                   </table>
                
                     <table border="1" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Parámetros Contables</td>
                  </tr>   
                    <tr>
             
                   <td class="style5">
                   
                       <asp:Label ID="lbl_cuenta" runat="server" CssClass="texto" Text="Cuenta de capital vigente:"
                        Width="177px"></asp:Label>
                   </td>
                   <td>
                   <asp:Label ID="lbl_cuentares" runat="server" CssClass="texto"></asp:Label>
                   </td>
                  
                   </tr>
                   
                   
                   </table>
                   
                     <table border="0" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Documentación Requerida</td>
                  </tr>   
                             
                   <tr>
                   <td>
                   
                    <asp:ListView ID="ListView2" runat="server" DataSourceID="sqldocumentacion">
                        <ItemTemplate>
                            <tr >
                                <td >
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" CssClass="texto"  Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CANTIDADLabel" runat="server" CssClass="texto"   Text='<%# Eval("CANTIDAD") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="FASELabel" runat="server" CssClass="texto" Text='<%# Eval("FASE") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td >
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" CssClass="texto"  Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CANTIDADLabel" runat="server"  CssClass="texto"  Text='<%# Eval("CANTIDAD") %>' />
                                </td>
                                <td ">
                                    <asp:Label ID="FASELabel" runat="server"   CssClass="texto"  Text='<%# Eval("FASE") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table3" runat="server" style="">
                                <tr>
                                    <td>
                                       <asp:Label ID="lbl_d4" runat="server" CssClass="texto"  Text='No hay datos disponibles' />
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr>
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td >
                                    <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" CssClass="texto"  Text='<%# Bind("DESCRIPCION") %>' />
                                </td>
                                <td >
                                    <asp:TextBox ID="CANTIDADTextBox" runat="server" CssClass="texto" Text='<%# Bind("CANTIDAD") %>' />
                                </td>
                                <td >
                                    <asp:TextBox ID="FASETextBox" runat="server" CssClass="texto"  Text='<%# Bind("FASE") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <LayoutTemplate>
                            <table border="0" style = "width:100%; text-align:left;">
                                <tr id="Tr4" runat="server" cssclass="texto">
                                    <td id="Td3" runat="server">
                                        <table id="itemPlaceholderContainer"  border="1" style = "width:100%; text-align:left;">
                                            <tr id="Tr5" runat="server" cssclass="texto">
                                                <td>
                                                 <asp:Label ID="lbl_descripcion" runat="server" CssClass="texto" Font-Bold=false  Text='Descripción:' />
                                                </td>
                                                <td>
                                            <asp:Label ID="lbl_cantidad" runat="server"  CssClass="texto"  Font-Bold=false Text='Cantidad:' />
                                                </td>
                                                <td>
                                                     <asp:Label ID="lbl_fase" runat="server"  CssClass="texto" Font-Bold=false Text='Fase:' />
                                                </td>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server">
                                    <td id="Td4" runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <EditItemTemplate>
                            <tr>
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" CssClass="texto"  Text='<%# Bind("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CANTIDADTextBox" runat="server"  CssClass="texto" Text='<%# Bind("CANTIDAD") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="FASETextBox" runat="server"  CssClass="texto" Text='<%# Bind("FASE") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" CssClass="texto"   Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CANTIDADLabel" runat="server"  CssClass="texto" Text='<%# Eval("CANTIDAD") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="FASELabel" runat="server"  CssClass="texto" Text='<%# Eval("FASE") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="sqldocumentacion" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>"
                        SelectCommand="SEL_RESUMEN_CAPTACION_DOCUMENTACION" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" Type="Int32" />
                            </SelectParameters>
                    </asp:SqlDataSource>
                    </td>
              </tr>
                       </table>
                   
                     <table border="1" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Divisas asignadas</td>
                  </tr>   
                
                 <tr>
                   <td>
                 
                    <asp:ListView ID="ListView5" runat="server" DataSourceID="sqldivisas">
                        <ItemTemplate>
                            <tr>
                                <td >
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" cssclass="texto"   Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td >
                                    <asp:Label ID="CLAVELabel" runat="server" cssclass="texto"   Text='<%# Eval("CLAVE") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr >
                                <td>
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" cssclass="texto"   Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CLAVELabel" runat="server"  cssclass="texto"   Text='<%# Eval("CLAVE") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table9" runat="server" style=""  cssclass="texto">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_d3" runat="server"   CssClass="texto"  Text='No hay datos disponibles' />
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr>
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" cssclass="texto"   Text='<%# Bind("DESCRIPCION") %>' />
                                </td>
                                <td> 
                                    <asp:TextBox ID="CLAVETextBox" runat="server" cssclass="texto"  Text='<%# Bind("CLAVE") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <LayoutTemplate>
                            <table id="Table10" runat="server">
                                <tr id="Tr13" runat="server">
                                    <td id="Td9" runat="server">
                                        <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td id="Th10" runat="server" style=" font-weight: normal">
                                                
                                                   <asp:Label ID="lbl_descripcion" runat="server"  CssClass="texto"  Text='Descripción:' />
                                                 
                                                </td>
                                                <td id="Th11" runat="server" style=" font-weight: normal">
                                                   <asp:Label ID="lbl_clave" runat="server" CssClass="texto"  Text='Clave:' />
                                                 
                                                </td>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="Tr15" runat="server">
                                    <td id="Td10" runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <EditItemTemplate>
                            <tr>
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" CssClass="texto"  Text='<%# Bind("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CLAVETextBox" runat="server" CssClass="texto"    Text='<%# Bind("CLAVE") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="DESCRIPCIONLabel" runat="server" CssClass="texto"   Text='<%# Eval("DESCRIPCION") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CLAVELabel" runat="server" CssClass="texto" Text='<%# Eval("CLAVE") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="sqldivisas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>"
                        SelectCommand="SEL_RESUMEN_CAPTACION_DIVISAS" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" Type="Int32" />
                           </SelectParameters>
                    </asp:SqlDataSource>
                      </td>
                    </tr>
                    </table>
                        
                 <table  border="0" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Tasas de Interés</td>
                  </tr>   
            
                 <tr>
                 <td>               
                
                    <asp:ListView ID="ListView6" runat="server" DataSourceID="sqltasas" >
                        <ItemTemplate>
                            <tr >
                                <td>
                                    <asp:Label ID="MINIMALabel" runat="server"  cssclass="texto"  Text='<%# Eval("MINIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MAXIMALabel" runat="server"  cssclass="texto" Text='<%# Eval("MAXIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TIPOLabel" runat="server"  cssclass="texto"  Text='<%# Eval("TIPO") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CLASIFICACIONLabel" runat="server" cssclass="texto"  Text='<%# Eval("CLASIFICACION") %>' />
                                </td>
                                
                                <td>
                                    <asp:Label ID="INDICELabel" runat="server"  cssclass="texto" Text='<%# Eval("INDICE") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ESTATUSLabel" runat="server" cssclass="texto"   Text='<%# Eval("ESTATUS") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="MINIMALabel" runat="server" cssclass="texto"  Text='<%# Eval("MINIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MAXIMALabel" runat="server"  cssclass="texto"   Text='<%# Eval("MAXIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TIPOLabel" runat="server" cssclass="texto" Text='<%# Eval("TIPO") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CLASIFICACIONLabel" runat="server" cssclass="texto"  Text='<%# Eval("CLASIFICACION") %>' />
                                </td>
                                
                                <td>
                                    <asp:Label ID="INDICELabel" runat="server" cssclass="texto"   Text='<%# Eval("INDICE") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ESTATUSLabel" runat="server" cssclass="texto" Text='<%# Eval("ESTATUS") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" style = "width:100%; text-align:left;">
                                <tr>
                                    <td>
                                      <asp:Label ID="lbl_d2" runat="server"  CssClass="texto"  Text='No hay datos disponibles' />
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="MINIMATextBox" runat="server" cssclass="texto"    Text='<%# Bind("MINIMA") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MAXIMATextBox" runat="server" cssclass="texto" Text='<%# Bind("MAXIMA") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TIPOTextBox" runat="server"  cssclass="texto"  Text='<%# Bind("TIPO") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CLASIFICACIONTextBox" runat="server" cssclass="texto" Text='<%# Bind("CLASIFICACION") %>' />
                                </td>
                              
                                <td>
                                    <asp:TextBox ID="INDICETextBox" runat="server"  cssclass="texto"  Text='<%# Bind("INDICE") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ESTATUSTextBox" runat="server" cssclass="texto"  Text='<%# Bind("ESTATUS") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <LayoutTemplate>
                            <table border="0" style = "width:100%; text-align:left;">
                                <tr id="Tr16" runat="server">
                                    <td id="Td11" runat="server">
                                        <table id="itemPlaceholderContainer"  border="1" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                     <asp:Label ID="lbl_minima" runat="server"  CssClass="texto" Text='Miníma:' />
                                                  
                                                </td>
                                                <td>
                                                     <asp:Label ID="lbl_maxima" runat="server"  CssClass="texto" Text='Máxima:' />
                                                </td>
                                                <td >
                                                    <asp:Label ID="Label1" runat="server"  CssClass="texto" Text='Tipo:' />
                                                </td>
                                                <td >
                                                      <asp:Label ID="lbl_clasificacion" runat="server"   CssClass="texto"  Text='Clasificación:' />
                                                </td>
                                               <td>
                                                      <asp:Label ID="lbl_indice" runat="server"  CssClass="texto"   Text='Indice:' />
                                                </td>
                                                <td >
                                                      <asp:Label ID="lbl_Estatus" runat="server"   CssClass="texto" Text='Estatus:' />
                                                </td>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <EditItemTemplate>
                            <tr>
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="MINIMATextBox" runat="server"  cssclass="texto" Text='<%# Bind("MINIMA") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="MAXIMATextBox" runat="server" cssclass="texto"  Text='<%# Bind("MAXIMA") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="TIPOTextBox" runat="server"  cssclass="texto" Text='<%# Bind("TIPO") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="CLASIFICACIONTextBox" runat="server" cssclass="texto" Text='<%# Bind("CLASIFICACION") %>' />
                                </td>
                               
                                <td>
                                    <asp:TextBox ID="INDICETextBox" runat="server" cssclass="texto" Text='<%# Bind("INDICE") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="ESTATUSTextBox" runat="server" cssclass="texto" Text='<%# Bind("ESTATUS") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="MINIMALabel" runat="server" cssclass="texto"  Text='<%# Eval("MINIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="MAXIMALabel" runat="server" cssclass="texto" Text='<%# Eval("MAXIMA") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="TIPOLabel" runat="server" cssclass="texto" Text='<%# Eval("TIPO") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="CLASIFICACIONLabel" runat="server" cssclass="texto"  Text='<%# Eval("CLASIFICACION") %>' />
                                </td>
                               
                                <td>
                                    <asp:Label ID="INDICELabel" runat="server" cssclass="texto" Text='<%# Eval("INDICE") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="ESTATUSLabel" runat="server" cssclass="texto" Text='<%# Eval("ESTATUS") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="sqltasas" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>"
                        SelectCommand="SEL_RESUMEN_CAPTACION_TASAS" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" Type="Int32" />
                            
                        </SelectParameters>
                    </asp:SqlDataSource>
                
                     </td>
                    </tr>
                    
                </table>
                     
                     <table border="1" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Comisiones</td>
                  </tr>            
                      
                    <tr>
                  
                   <td>
                   
                    
                    <asp:ListView ID="ListView7" runat="server" DataSourceID="sqlcomisiones">
                        <ItemTemplate>
                            <tr  >
                                <td>
                                    <asp:Label ID="NOMBRELabel" runat="server"  cssclass="texto"  Text='<%# Eval("NOMBRE") %>' />
                                </td>
                             </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr >
                                <td>
                                    <asp:Label ID="NOMBRELabel" runat="server"  cssclass="texto" Text='<%# Eval("NOMBRE") %>' />
                                </td>
                            
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                             <table border="0" style = "width:100%; text-align:left;">
                               
                                <tr>
                                    <td>
                                     <asp:Label ID="lbl_d0" runat="server"  CssClass="texto"  Text='No hay datos disponibles' />
                                      
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="NOMBRETextBox" runat="server"  cssclass="texto" Text='<%# Bind("NOMBRE") %>' />
                                </td>
                                
                            </tr>
                        </InsertItemTemplate>
                        <LayoutTemplate>
                            <table border="0" style = "width:100%; text-align:left;">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer"  border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <th runat="server" style=" font-weight: normal">
                                                   
                                                </th>
                                             
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="NOMBRETextBox" runat="server"  cssclass="texto" Text='<%# Bind("NOMBRE") %>' />
                                </td>
                            
                            </tr>
                        </EditItemTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="NOMBRELabel" runat="server"  cssclass="texto"  Text='<%# Eval("NOMBRE") %>' />
                                </td>
                            
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="sqlcomisiones" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>"
                        SelectCommand="SEL_RESUMEN_CAPTACION_COMISIONES" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" Type="Int32" />
                            
                        </SelectParameters>
                    </asp:SqlDataSource>
                  
                    </td>
                      </tr>
                      </table>
                   
                     <table border="0" style = "width:100%; text-align:left;">
                   <tr class="table_header">
                            <td colspan="6">Sucursales</td>
                  </tr>   
                         
                </table>
                  
                     <table border="0" style = "width:100%; text-align:left;">

                    <asp:ListView ID="ListView11" runat="server" DataSourceID="sqlsucursales">
                        <ItemTemplate>
                            <tr >
                                <td>
                                    <asp:Label ID="SUCURSALLabel" runat="server" CssClass="texto" Text='<%# Eval("SUCURSAL") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr >
                                <td>
                                    <asp:Label ID="SUCURSALLabel" runat="server" CssClass="texto" Text='<%# Eval("SUCURSAL") %>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_d1" runat="server" CssClass="texto"  Text='No hay datos disponibles' />
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <InsertItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                                </td>
                                <td>
                                    <asp:TextBox ID="SUCURSALTextBox" runat="server" CssClass="texto" Text='<%# Bind("SUCURSAL") %>' />
                                </td>
                            </tr>
                        </InsertItemTemplate>
                        <LayoutTemplate>
                            <table border="0" style = "width:100%; text-align:left;"">
                                <tr runat="server">
                                    <td runat="server">
                                        <table id="itemPlaceholderContainer"  border="1" style = "width:100%; text-align:left;">
                                            <tr>
                                               
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server">
                                            
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <EditItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <td>
                                    <asp:TextBox ID="SUCURSALTextBox" runat="server" CssClass="texto" Text='<%# Bind("SUCURSAL") %>' />
                                </td>
                            </tr>
                        </EditItemTemplate>
                        <SelectedItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label ID="SUCURSALLabel" runat="server"  CssClass="texto" Text='<%# Eval("SUCURSAL") %>' />
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="sqlsucursales" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>"
                        SelectCommand="SEL_RESUMEN_CAPTACION_SUCURSALES" SelectCommandType="StoredProcedure">
                           <SelectParameters>
                            <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" Type="Int32" />
                            
                        </SelectParameters>
                       
                    </asp:SqlDataSource>
               </table> 
                    
             </asp:Panel>
            
    

    </div>
  </div>
  </section>
</asp:Content>
