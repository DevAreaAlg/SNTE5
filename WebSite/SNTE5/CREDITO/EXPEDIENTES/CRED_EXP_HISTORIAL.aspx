<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_HISTORIAL.aspx.vb" Inherits="SNTE5.CRED_EXP_HISTORIAL" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Cliente:</h5>
                <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>
                        
    <section class="panel" runat="server" id="panel_1"> 
        <header  runat="server" id="head_panel_1" class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Expedientes de afiliado</span>
            <span class=" panel_folder_toogle down" runat="server" id="toggle_panel_1">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_panel_1">
                <asp:Panel ID="pnl_Historial" runat="server" Width="100%" >
                
                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="DAG_expedientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None"  Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="cveexp" HeaderText="Clave">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="tipoexp" HeaderText="Tipo expediente">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="monto" HeaderText="Monto">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechault" HeaderText="Último pago">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="saldoact" HeaderText="Saldo actual">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="plazo" HeaderText="Plazo">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="numpagoatras" HeaderText="Pagos atrasados">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="diasmaxmora" Visible="False" HeaderText="Máximo mora">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="sucursal" HeaderText="Sucursal">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="DETALLE" HeaderText="" Text="Detalle">
                                    <ItemStyle Width="5%" />
                                </asp:ButtonColumn>
                            </Columns>                                        
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_mensaje" runat="server" CssClass="subtitulos"></asp:Label>
                    </div>
                   
                </asp:Panel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_2" visible="false"> 
        <header  runat="server" id="head_panel_2" class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Detalle de expediente de préstamo</span>
            <span class=" panel_folder_toogle down" runat="server" id="toggle_panel_2">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_panel_2">
                <asp:Panel ID="pnl_detcred" runat="server">
                    <h5 class="module_subsec resalte_azul">Datos generales del expediente</h5>
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center " >
                                <asp:Label ID="lbl_FolioDetalleA" runat="server" Text="Folio del expediente: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_FolioDetalleB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_ProductoDetalleA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_ProductoDetalleB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_MontoA" runat="server" Text="Monto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_MontoB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_FechaLiberaA" runat="server" Text="Fecha de liberación: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_FechaLiberaB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>

                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_PlazoA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_PlazoB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_TasaNormalA" runat="server" Text="Tasa ordinaria anual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_TasaNormalB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_TasaMoraA" runat="server" Text="Tasa moratoria mensual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_TasaMoraB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_PeriodicidadA" runat="server" Text="Periodicidad: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_PeriodicidadB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>
                    </div>                     
                    <h5 class="module_subsec resalte_azul">Datos de préstamo actual</h5> 
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">                
                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_EstatusA" runat="server" Text="Estatus: " class="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                <asp:Textbox ID="lbl_EstatusB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div> 

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_SaldoA" runat="server" Text="Saldo actual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_SaldoB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_FechaUltA" runat="server" Text="Fecha de último pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_FechaUltB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>

                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_MaxDiasMoraA" runat="server" Text="Máximo de días en mora: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_MaxDiasMoraB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_center" >
                                <asp:Label ID="lbl_NumPagoAtrA" runat="server" Text="Número pagos atrasados: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_NumPagoAtrB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div> 
                        </div>
                    </div>                                               
                </asp:Panel>                  
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_3" Visible="False"> 
        <header runat="server" id="head_panel_3" class="panel_header_folder panel-heading">
            <span>Detalle de expediente de captación</span>
            <span class="panel_folder_toogle down" runat="server" id="toggle_panel_3">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_panel_3">
                <asp:Panel ID="pnl_detcap" runat="server">
                    <h5 class="module_subsec resalte_azul">Datos generales del expediente</h5>
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1"> 
                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_FolioCaptA" runat="server" Text="Folio el expediente: " class="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                <asp:Textbox ID="lbl_FolioCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_ProductoCaptA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                <asp:Textbox ID="lbl_ProductoCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>

                        <div class="module_subsec_elements vertical flex_1"> 
                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_PlazoCaptA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                <asp:Textbox ID="lbl_PlazoCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_TasaCaptA" runat="server" Text="Tasa anual: " class="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                <asp:Textbox ID="lbl_TasaCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>
                    </div>

                    <h5 class="module_subsec resalte_azul">Datos de expediente actual</h5>
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_EstausCaptA" runat="server" Text="Estatus: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_EstausCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center">
                                <asp:Label ID="lbl_SaldoCaptA" runat="server" Text="Saldo actual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_SaldoCaptB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>

                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned low_m align_items_flex_center">
                                <asp:Label ID="lbl_FechaUltRetiroA" runat="server" Text="Fecha de último retiro: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_FechaUltRetiroB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center">
                                <asp:Label ID="lbl_FechaUltDepositoA" runat="server" Text="Fecha de último depósito: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                <asp:Textbox ID="lbl_FechaUltDepositoB" runat="server" Enabled="False" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>        
            </div>
        </div>
    </section>
</asp:Content>

