<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_RESUMEN.aspx.vb" Inherits="SNTE5.CRED_PROD_RESUMEN" MaintainScrollPositionOnPostback ="true" %>

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

    <section class="panel">
        <header class="panel-heading">
            <span>Resumen del producto</span>
        </header>
        <div class="panel-body">

            <div class="panel-body_content init_show" id="pnl_res">
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="btn_imprimir">
                            Imprimir
                            <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div> 
                     
                <asp:Panel ID="pnl_resumen" runat="server">
                <div id="Resumen_Indicador">
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos Generales</td>
                        </tr>              
                               
						<tr>                                            
                            <td colspan="6">
                               
                                 <asp:Label ID="Label1" runat="server" Text="Identificador del producto:" class="texto"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_idprodres" runat="server" class="texto" ></asp:Label>
                            </td>
                        </tr>                 
					   <tr>
                            <td >
                                <asp:Label ID="lbl_claveProd" runat="server" Text="Clave del producto:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_claveProdRes" runat="server" class="texto"></asp:Label>
                            </td>                       
                           
                        </tr>
                        <tr>
                             <td >
                                <asp:Label ID="lbl_clasif_prod" runat="server" Text="Clasificación de Producto:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_clasifRes" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>                          
                            <td colspan="6">
                                <asp:Label runat="server" Text="Nombre del producto:" class="texto"
                                   ID="lbl_nombre"></asp:Label>&nbsp;                   
                                <asp:Label ID="lbl_nombreres" runat="server" class="texto" ></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_descripcion" runat="server" Text="Descripción:" class="texto" ></asp:Label>&nbsp;
                                <asp:Label ID="lbl_descripcionres" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_cveDscto" runat="server" Text="Clave Descuento:" class="texto" ></asp:Label>&nbsp;
                                <asp:Label ID="lbl_cveDsctoRes" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_destino" runat="server" Text="Destino:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_destinores" runat="server" class="texto"></asp:Label>
                            </td>                         
                        </tr>

                        <tr>
                            <td >
                                <asp:Label ID="lbl_tipopersona" runat="server" Text="Tipo de persona:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_tipopersonares" runat="server" class="texto"></asp:Label>
                            </td>                       
                           
                        </tr>
                        <tr>
                             <td >
                                <asp:Label ID="lbl_tipoprod" runat="server" Text="Tipo de Producto:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_tipoprodres" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                    </table>
                       
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Plazos</td>
                        </tr>
                            
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_plazosliminf" runat="server" Text="Límite Inferior:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_plazosliminfres" runat="server" class="texto"></asp:Label>
                            </td>					    
					        <td colspan="2">
                                <asp:Label ID="lbl_plazoslimsup" runat="server" Text="Límite superior:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_plazoslimsupres" runat="server" class="texto"></asp:Label>
					        </td>                        
                            <td colspan="2">
                                <asp:Label ID="lbl_plazosunidad" runat="server" Text="Unidad:" class="texto"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_plazosunidadres" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                    </table>
                
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Montos</td>
                        </tr>              
                               
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_montoinf" runat="server" class="texto" Text="Límite Inferior:"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_montoinfres" runat="server" class="texto"></asp:Label>
                            </td>                       
                            <td colspan="3">
                                <asp:Label ID="lbl_montosup" runat="server" class="texto" Text="Límite Superior:"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lbl_montosupres" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>                        
                    </table>

                     
                 
                       
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Requisitos Generales</td>
                            </tr>              
                    
                        <tr>
                            <td>
                                <asp:Label ID="lbl_confreq" runat="server" class="texto" 
                                   Text="Configuración Requerida:" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_confreqmin" runat="server" class="texto" 
                                    Width="135px">Mínimo</asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_conreqmax" runat="server" class="texto" 
                                    Width="135px">Máximo</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_ref" runat="server" class="texto"
                                    Text="Número de Referencias:" Width="155px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_refresmin" runat="server" class="texto" 
                                    Width="135px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_refresmax" runat="server" class="texto" 
                                    Width="135px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_avales" runat="server" class="texto"
                                    Text="Número de Avales:" Width="155px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_avalesresmin" runat="server" class="texto" 
                                   Width="135px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_avalesresmax" runat="server" class="texto" 
                                  Width="135px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_cod" runat="server" class="texto" 
                                    Text="Número de Codeudores:" Width="155px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_codresmin" runat="server" class="texto" 
                                    Width="135px"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbl_codresmax" runat="server" class="texto" 
                                    Width="135px"></asp:Label>
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
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="DESCRIPCIONLabel" runat="server" class="texto" 
                                                   Text='<%# Eval("DESCRIPCION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CANTIDADLabel" runat="server" class="texto"
                                                    Text='<%# Eval("CANTIDAD") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="FASELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FASE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="DESCRIPCIONLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("DESCRIPCION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CANTIDADLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("CANTIDAD") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="FASELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FASE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_d2" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td >
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Clear" />
                                            </td>
                                       
                                           <td align="center">
                                                <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("DESCRIPCION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="CANTIDADTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("CANTIDAD") %>' />
                                            </td>

                                            <td align="center">
                                                <asp:TextBox ID="FASETextBox" runat="server" class="texto"
                                                    Text='<%# Bind("FASE") %>' />
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table ID="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_descripcion" runat="server" class="texto" 
                                                                  Text="Descripción:" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_cantidad" runat="server" class="texto" 
                                                                    Text="Cantidad:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="lbl_fase" runat="server" class="texto"
                                                                    Text="Fase:" />
                                                            </td>
                                                        </tr>
                                                        <tr ID="itemPlaceholder" runat="server">
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Cancel" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="DESCRIPCIONTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("DESCRIPCION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="CANTIDADTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("CANTIDAD") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="FASETextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("FASE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td align="center">
                                                <asp:Label ID="DESCRIPCIONLabel" runat="server" class="texto" 
                                                   Text='<%# Eval("DESCRIPCION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CANTIDADLabel" runat="server" class="texto"
                                                    Text='<%# Eval("CANTIDAD") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="FASELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FASE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqldocumentacion" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_CNFPCR_CLONA_REQUISITOS" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPRODFUENTE" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDUSER" SessionField="USERID" 
                                            Type="Int32" />
                                        <asp:SessionParameter Name="IDSESION" SessionField="Sesion" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                  
                    <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Fuentes de Fondeo</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView5" runat="server" DataSourceID="sqlfondeo">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    class="texto" Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlfondeo" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_PROD_FONDEO_RESUMEN" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>


                    </table> 
                                        
                
                    
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Parámetros Contables</td>
                            </tr>              
                               
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_ctacap" runat="server" class="texto" Font-Bold="True" 
                                Text="Cuenta de Capital Vigente" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_cuenta" runat="server" class="texto" 
                                    Text="Cuenta:" ></asp:Label>
                           &nbsp;
                                <asp:Label ID="lbl_cuentares" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                        </tr>
                    </table>
             
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Parámetros de Intereses Moratorios y Cartera Vencida</td>
                            </tr>              
                           <tr>    
                            <td>
                                <asp:Label ID="lbl_diasvencida" runat="server" class="texto" 
                                   Text="Días para caer en cartera vencida:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_diasvencidares" runat="server" class="texto" 
                                     ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_pagossos" runat="server" class="texto" 
                                    Text="Número de pagos sostenidos:" ></asp:Label>
                           &nbsp;
                                <asp:Label ID="lbl_pagossosres" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <asp:Label ID="lbl_diasintnor" runat="server" class="texto"
                                    Text="Días de gracia de interés ordinario:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_diasintnorres" runat="server" class="texto" 
                                  ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_diasintmor" runat="server" class="texto" 
                                    Text="Días de gracia de interés moratorio:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_diasintmorres" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_pergracia" runat="server" class="texto" 
                                    Text="Períodos de gracia:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_pergraciares" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_cobromor" runat="server" class="texto" 
                                    Text="Cobro de interés moratorio sobre:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_cobromorres" runat="server" class="texto" 
                                ></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Parámetros de Evaluacion de Préstamo</td>
                        </tr>              
                                
                        <tr>
                            <td>
                                <asp:Label ID="lbl_montoseguro" runat="server" class="texto" 
                                    Text="Monto minímo seguro de vida:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_montosegurores" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_montodirector" runat="server" class="texto" 
                                    Text="Monto minímo para evaluación de director de préstamo:" 
                                    ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_montodirectorres" runat="server" class="texto" 
                                    ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_razoningreso" runat="server" class="texto" 
                                   Text="Razón Ingreso-Abono:" ></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_razoningresores" runat="server" class="texto" 
                                   Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_montomin" runat="server" class="texto" 
                                    Text="Monto mínimo para evaluación de comité:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_montominres" runat="server" class="texto" 
                                     ></asp:Label>
                            </td>
                        </tr>


                        </table>
                                    
                    <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Tasa de Interés</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView6" runat="server" DataSourceID="sqltasas">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="MINIMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MINIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="MAXIMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MAXIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="TIPOLabel" runat="server" class="texto"
                                                    Text='<%# Eval("TIPO") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CLASIFICACIONLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("CLASIFICACION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="VARIABLELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("VARIABLE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="INDICELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("INDICE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="ESTATUSLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("ESTATUS") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="MINIMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MINIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="MAXIMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MAXIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="TIPOLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("TIPO") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CLASIFICACIONLabel" runat="server" class="texto" 
                                                 Text='<%# Eval("CLASIFICACION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="VARIABLELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("VARIABLE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="INDICELabel" runat="server" class="texto"
                                                    Text='<%# Eval("INDICE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="ESTATUSLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("ESTATUS") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_d2" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Clear" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="MINIMATextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("MINIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="MAXIMATextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("MAXIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="TIPOTextBox" runat="server" class="texto"
                                                    Text='<%# Bind("TIPO") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="CLASIFICACIONTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("CLASIFICACION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="VARIABLETextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("VARIABLE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="INDICETextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("INDICE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="ESTATUSTextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("ESTATUS") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table ID="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_d2" runat="server" class="texto" 
                                                                    Text="Mínima:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label6" runat="server" class="texto"
                                                                    Text="Máxima:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label7" runat="server" class="texto"  
                                                                    Text="Tipo:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label8" runat="server" class="texto" 
                                                                    Text="Clasificación:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label9" runat="server" class="texto" 
                                                                    Text="Variable:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label10" runat="server" class="texto" 
                                                                    Text="Índice:" />
                                                            </td>
                                                            <td runat="server">
                                                                <asp:Label ID="Label11" runat="server" class="texto" 
                                                                    Text="Estatus:" />
                                                            </td>
                                                        </tr>
                                                        <tr ID="itemPlaceholder" runat="server">
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Cancel" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="MINIMATextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("MINIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="MAXIMATextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("MAXIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="TIPOTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("TIPO") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="CLASIFICACIONTextBox" runat="server" class="texto" 
                                                 Text='<%# Bind("CLASIFICACION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="VARIABLETextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("VARIABLE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="INDICETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("INDICE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="ESTATUSTextBox" runat="server" class="texto" 
                                                  Text='<%# Bind("ESTATUS") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td align="center">
                                                <asp:Label ID="MINIMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MINIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="MAXIMALabel" runat="server" class="texto"  
                                                    Text='<%# Eval("MAXIMA") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="TIPOLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("TIPO") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="CLASIFICACIONLabel" runat="server" class="texto" 
                                                  Text='<%# Eval("CLASIFICACION") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="VARIABLELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("VARIABLE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="INDICELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("INDICE") %>' />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="ESTATUSLabel" runat="server" class="texto"  
                                                    Text='<%# Eval("ESTATUS") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqltasas" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_CNFPCR_CLONA_DATOS_TASAS" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPRODFUENTE" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:SessionParameter Name="IDUSER" SessionField="USERID" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDSESION" SessionField="Sesion" 
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Tasa de IVA</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:Label ID="lbl_iva" runat="server" class="texto" 
                                    Text="IVA:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_ivares" runat="server" class="texto" 
                                   ></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lbl_cobroIva" runat="server" class="texto" 
                                    Text="Cobro de IVA:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_cobroIVARes" runat="server" class="texto" 
                                   ></asp:Label>
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
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MONTOTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("MONTO") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PORCENTAJETextBox" runat="server" 
                                                    Text='<%# Bind("PORCENTAJE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="1" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table ID="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_d2" runat="server" class="texto"
                                                                    Text="Nombre:" />
                                                            </td>
                                                        </tr>
                                                        <tr ID="itemPlaceholder" runat="server">
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="MONTOTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("MONTO") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PORCENTAJETextBox" runat="server" class="texto" 
                                                     Text='<%# Bind("PORCENTAJE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"  
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="MONTOLabel" runat="server" class="texto" 
                                                    Text='<%# Eval("MONTO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="PORCENTAJELabel" runat="server" class="texto" 
                                                  Text='<%# Eval("PORCENTAJE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlcomisiones" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_CNFPCR_CLONA_COMISIONES_ASIGNADAS" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPRODFUENTE" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDUSER" SessionField="USERID" 
                                            Type="Int32" />
                                        <asp:SessionParameter Name="IDSESION" SessionField="Sesion" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                                            
                    <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Pagarés</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView8" runat="server" DataSourceID="sqlpagares">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    class="texto" Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlpagares" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_PROD_PAGARES_RESUMEN" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>


                    </table>  
                    
                      <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Garantías</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView1" runat="server" DataSourceID="sqlgtia">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    class="texto" Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlgtia" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_PROD_GARANTIAS_RESUMEN" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>


                    </table> 

                     <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Contratos</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView3" runat="server" DataSourceID="sqlcontratos">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    class="texto" Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="SqlContratos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_PROD_CONTRATOS_RESUMEN" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>


                    </table>   
                    
                      <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Solicitudes</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView4" runat="server" DataSourceID="SqlSolicitudes">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto" 
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_70" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto"  Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table id="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    class="texto" Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="NOMBRETextBox" runat="server" class="texto" 
                                                   Text='<%# Bind("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NOMBRELabel" runat="server" class="texto"
                                                    Text='<%# Eval("NOMBRE") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="SqlSolicitudes" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_PROD_SOLICITUDES_RESUMEN" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPROD" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>


                    </table>   
                    
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Medios de Pago y Periodicidad</td>
                        </tr>              
                         
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" class="texto" 
                                    Text="Tipos de Cobro:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_tipoCobroRes" runat="server" class="texto" 
                                   ></asp:Label>
                            </td>
                        </tr>

                         <tr>
                            <td>
                                <asp:Label ID="lbl_saldo_ope" runat="server" class="texto" 
                                    Text="Procurar tener saldo en cuenta eje:"></asp:Label>
                            &nbsp;
                                <asp:Label ID="lbl_saldoopeRes" runat="server" class="texto" 
                                   ></asp:Label>
                            </td>
                        </tr>

                 
                       
                    </table>                    
             
                    <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Medios de Cobro</td>
                        </tr>              
                               
                        <tr>
                            <td>
                                <asp:ListView ID="ListView10" runat="server" DataSourceID="sqlformaspago">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="FORMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FORMA") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="FORMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FORMA") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_d2" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    class="texto" Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    class="texto" Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="FORMATextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("FORMA") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table ID="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                        <tr>
                                                        </tr>
                                                        <tr ID="itemPlaceholder" runat="server">
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="FORMATextBox" runat="server" class="texto"
                                                    Text='<%# Bind("FORMA") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="FORMALabel" runat="server" class="texto" 
                                                    Text='<%# Eval("FORMA") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlformaspago" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_CNFPCR_CLONA_FORMAS_PAGO" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPRODFUENTE" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:SessionParameter Name="IDUSER" SessionField="USERID" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDSESION" SessionField="Sesion" 
                                            Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                              
                 
                    <table border="0" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Sucursales</td>
                        </tr> 

                        <tr>
                            <td>
                                <asp:ListView ID="ListView11" runat="server" DataSourceID="sqlsucursales">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="SUCURSALLabel" runat="server" class="texto"  Text='<%# Eval("SUCURSAL") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="SUCURSALLabel" runat="server" class="texto"   Text='<%# Eval("SUCURSAL") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_d2" runat="server" class="texto" 
                                                        Text="No hay datos disponibles" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <InsertItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                                                    Text="Insert" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Clear" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="SUCURSALTextBox" runat="server" class="texto" 
                                                    Text='<%# Bind("SUCURSAL") %>' />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <LayoutTemplate>
                                        <table border="0" style = "width:100%; text-align:left;">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table ID="itemPlaceholderContainer" border="1" style = "width:100%; text-align:left;">
                                                       
                                                        <tr ID="itemPlaceholder" runat="server">
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
                                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                                                    Text="Update" />
                                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                                                    Text="Cancel" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="SUCURSALTextBox" runat="server" class="texto"
                                                    Text='<%# Bind("SUCURSAL") %>' />
                                            </td>
                                        </tr>
                                    </EditItemTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="SUCURSALLabel" runat="server" class="texto"  Text='<%# Eval("SUCURSAL") %>' />
                                            </td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                                <asp:SqlDataSource ID="sqlsucursales" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ConnectionStringDotNet %>" 
                                    SelectCommand="SEL_CNFPCR_CLONA_SUCURSALES_PROD" 
                                    SelectCommandType="StoredProcedure" 
                                    UpdateCommand="SEL_CNFPCR_CLONA_SUCURSALES_PROD" 
                                    UpdateCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="IDPRODFUENTE" SessionField="IDPRODRES" 
                                            Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDUSER" SessionField="USERID" 
                                            Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="IDSESION" SessionField="Sesion" 
                                            Type="String" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="IDPRODFUENTE" Type="Int32" />
                                        <asp:Parameter Name="IDPRODDESTINO" Type="Int32" />
                                        <asp:Parameter Name="IDUSER" Type="Int32" />
                                        <asp:Parameter Name="IDSESION" Type="String" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    
                </div>
                </asp:Panel>              
            </div>
		</div>           
    </section>

</asp:Content>