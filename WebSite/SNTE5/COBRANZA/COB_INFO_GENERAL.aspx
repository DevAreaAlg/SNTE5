<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_INFO_GENERAL.aspx.vb" Inherits="SNTE5.COB_INFO_GENERAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px");
            if (wbusf != null) {

                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>Afiliado</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">                        
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_numcliente">Número de afiliado: </asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_cliente">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Enabled="true"
                                CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                        </div>
                    </div>
                    <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                        Style="height: 18px; width: 18px;"></asp:ImageButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="Label2">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Label ID="lbl_Cliente" runat="server" ></asp:Label>
                    </div>
                </div>                
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_Info" runat="server" class="alerta"></asp:Label>                  
            </div>
        </div>
    </section>
                       
    <div class="module_subsec flex_center">
        <asp:Label ID="Label4" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
    </div>

    <%-- INFORMACION DE LA CUENTA --%>
    <section class="panel" runat="server" id="pnl_info" Visible="False"> 
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Información general</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_info" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec flex_end">
                            <asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" 
                                Text="Ver Plan de Pagos"></asp:LinkButton>
                        </div>                       
                
                        <%--Información General --%>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_actualizacion" runat="server" Text="Actualización: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_actualizacion" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_titu_estatus_cob" runat="server" Text="Estatus cobranza: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_estatus_Cob" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_num_cliente" runat="server" Text="Núm. cliente: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_num_cliente" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_folio" runat="server" Text="Folio: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_folio" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_producto" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_producto" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_plazo" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_plazo" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
               
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_unidad_plazo" runat="server" Text="Unidad de plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_unidad_plazo" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_monto" runat="server" Text="Monto préstamo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_monto" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_sucursal" runat="server" Text="Sucursal: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_sucursal" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tasa_ord" runat="server" Text="Tasa ordinaria: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tasa_ord" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tasa_mor" runat="server" Text="Tasa moratoria: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tasa_mor" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_periodicidad" runat="server" Text="Periodicidad: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_periodicidad" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_modalidad" runat="server" Text="Modalidad pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_modalidad" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>

                        <%--Cobranza --%>
                        <asp:UpdatePanel ID="upd_pnl_cobranza" runat="server">
                              <ContentTemplate>
                                  <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_cobranza" runat="server" TargetControlID="pnl_ccobranza" 
                                        ExpandControlID="pnl_tit_cobranza" CollapseControlID="pnl_tit_cobranza" ImageControlID="ToggleImage_cobranza" 
                                        ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg" ExpandedText="Contraer" CollapsedText="Expandir"
                                        SuppressPostBack="True" Collapsed="true" />
                                    <asp:Panel ID="pnl_tit_cobranza" runat="server" Style="cursor: pointer;">
                                        <div class="texto">
                                            <asp:ImageButton ID="ToggleImage_cobranza" runat="server" />
                                            <asp:Label ID="lbl_tit_cobranza_pnl" runat="server" CssClass="textogris" Text="Cobranza"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_ccobranza" runat="server" Style="overflow: hidden;">
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_saldo_insoluto" runat="server" Text="Saldo insoluto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_saldo_insoluto" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_int_ord_gravado" runat="server" Text="Interés ord. gravado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_int_ord_gravado" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_int_mor_gravado" runat="server" Text="Interés mor. gravado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_int_mor_gravado" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_int_ord_exento" runat="server" Text="Interés ordinario exento: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_int_ord_exento" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_int_mor_exento" runat="server" Text="Actualización: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_int_mor_exento" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_total_int_ord" runat="server" Text="Total interés ordinario: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_total_int_ord" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_total_int_mor" runat="server" Text="Total interés moratorio: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_total_int_mor" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_iva_int_ord" runat="server" Text="IVA interés ordinario: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_iva_int_ord" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_iva_int_mor" runat="server" Text="IVA interés moratorio:  " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_iva_int_mor" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_comision" runat="server" Text="Comisión: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_comision" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_iva_comision" runat="server" Text="IVA comisión: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_iva_comision" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_int_ccartera" runat="server" Text="Interés cesión cartera: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_int_ccartera" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_iva_int_ccartera" runat="server" Text="IVA int. cesión cartera: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_iva_int_ccartera" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                    
                                            </div>
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_fecha_prox_pago" runat="server" Text="Fecha próximo pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fecha_prox_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_monto_prox_pago" runat="server" Text="Monto próximo pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_monto_prox_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_fecha_ultimo_pago" runat="server" Text="Fecha último pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fecha_ultimo_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_monto_ultimo_pago" runat="server" Text="Monto último pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_monto_ultimo_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_dias_vencer" runat="server" Text="Días por vencer: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_dias_vencer" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_fecha_ult_mov_cta_eje" runat="server" Text="Fecha últ. mov. cta eje: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fecha_ult_mov_cta_eje" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_saldo_ult_mov_cta_eje" runat="server" Text="Saldo últ. mov. cta eje: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_saldo_ult_mov_cta_eje" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_folio_cta_eje" runat="server" Text="Folio cta eje: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_folio_cta_eje" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_saldo_cta_eje" runat="server" Text="Saldo cta eje: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_saldo_cta_eje" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_monto_liquidar" runat="server" Text="Monto liquidar: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_monto_liquidar" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_fecha_vencimiento" runat="server" Text="Fecha de vencimiento: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fecha_vencimiento" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tit_fecha_liberacion" runat="server" Text="Fecha liberación: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fecha_liberacion" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                              </ContentTemplate>
                        </asp:UpdatePanel>
                                                
                        <%--Vencido --%>
                        <asp:UpdatePanel ID="upd_pnl_Vencido" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_vencido" runat="server"
                                    TargetControlID="pnl_vencido" ExpandControlID="pnl_tit_vencido" CollapseControlID="pnl_tit_vencido"
                                    ImageControlID="ToggleImage_vencido" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg" 
                                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" Collapsed="true" />
                                <asp:Panel ID="pnl_tit_vencido" runat="server" Style="cursor: pointer;">
                                    <div class="texto">
                                        <asp:ImageButton ID="ToggleImage_vencido" runat="server" />
                                        <asp:Label ID="lbl_Tit_Vencido" runat="server" CssClass="textogris" Text="Vencido"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnl_vencido" runat="server" Style="overflow: hidden;">
                                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                        <div class="module_subsec_elements vertical flex_1">
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_dias_mora" runat="server" Text="Días de mora: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_dias_mora" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_abonos_vencidos" runat="server" Text="Abonos vencidos: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_abonos_vencidos" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_capital_vencido" runat="server" Text="Capital vencido: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_capital_vencido" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_monto_regularizacion" runat="server" Text="Monto Regularización: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_monto_regularizacion" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements vertical flex_1">
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_num_reestructura" runat="server" Text="Núm. reestructura: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_num_reestructura" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_tit_folio_reestructura" runat="server" Text="Folio reestructura: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_folio_reestructura" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <%-- DOMICILIO --%>
    <section class="panel" runat="server" id="pnl_dom" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Domicilio</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_domicilio" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_domicilio" runat="server" Text="Domicilio: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_domicilio" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_suc" runat="server" Text="Id sucursal: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_suc" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_destino" runat="server" Text="Destino: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_destino" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tipo_persona" runat="server" Text="Tipo de persona: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tipo_persona" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>                               
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa" runat="server" Text="Teléfono casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_casa" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_cel" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_cel" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>                               
                            </div>
                        </div>
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <%-- AVALES/GARANTIA --%>
    <section class="panel" runat="server" id="pnl_aval" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Avales / Garantías</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
               <asp:UpdatePanel ID="upd_pnl_avga" runat="server">
                   <ContentTemplate>
                       <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <asp:Label ID="lbl_av1" runat="server" CssClass="textogris" Text="Aval 1:"></asp:Label>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_persona_aval1" runat="server" Text="Núm. Afiliado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_persona_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre_aval1" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_direccion_aval1" runat="server" Text="Dirección: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_direccion_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa_aval1" runat="server" Text="Teléfono casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_Casa_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_movil_aval1" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_movil_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi_aval1" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_ext_tel_ofi_aval1" runat="server" Text="Extensión oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_ext_tel_ofi_aval1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                <asp:Label ID="Label5" runat="server" CssClass="textogris" Text="Aval 2:"></asp:Label>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_persona_aval2" runat="server" Text="Núm. Afiliado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_persona_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre_aval2" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_direccion_aval2" runat="server" Text="Dirección: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_direccion_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa_aval2" runat="server" Text="Teléfono casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_casa_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_movil_aval2" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_movil_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi_aval2" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_ext_tel_ofi_aval2" runat="server" Text="Extensión oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_ext_tel_ofi_aval2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="Label6" runat="server" CssClass="module_subsec h4" Text="Garantías:"></asp:Label>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">                               
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tipo_garantias" runat="server" Text="Tipo Garantías: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tipo_garantias" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_valor_gtias" runat="server" Text="Valor garantías: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_valor_gtias" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>                                
                            </div>
                        </div>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1" style="margin-top:-15px!important;">
                               <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_descripción_gtias" runat="server" Text="Descripción garantías: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_descripcion_gtias" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
               </asp:UpdatePanel>        
            </div>
        </div>
    </section>

    <%--REFERENCIAS --%>
    <section class="panel" runat="server" id="pnl_ref" Visible="False"> 
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Referencias</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <asp:Label ID="Label3" runat="server" CssClass="textogris" Text="Referencia 1:"></asp:Label>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_persona_ref1" runat="server" Text="Núm. Afiliado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_persona_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre_ref1" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_direccion_ref1" runat="server" Text="Dirección: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_direccion_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa_ref1" runat="server" Text="Teléfono Casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_casa_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_cel_ref1" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_cel_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi_ref1" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_ext_ofi_ref1" runat="server" Text="Extensión Oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_ext_ofi_ref1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                <asp:Label ID="Label9" runat="server" CssClass="textogris" Text="Referencia 2:"></asp:Label>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_persona_ref2" runat="server" Text="Núm. Afiliado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_persona_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre_ref2" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_direccion_ref2" runat="server" Text="Dirección: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_direccion_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa_ref2" runat="server" Text="Teléfono Casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_casa_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_cel_ref2" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_cel_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi_ref2" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_ext_ofi_ref2" runat="server" Text="Extensión Oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_ext_ofi_ref2" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical flex_1">
                                <asp:Label ID="Label1" runat="server" CssClass="textogris" Text="Referencia 3:"></asp:Label>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_id_persona_ref3" runat="server" Text="Núm. Afiliado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_id_persona_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_nombre_ref3" runat="server" Text="Nombre: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_nombre_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_direccion_ref3" runat="server" Text="Dirección: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_direccion_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_casa_ref3" runat="server" Text="Teléfono Casa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_Casa_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_cel_ref3" runat="server" Text="Teléfono móvil: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_cel_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_tel_ofi_ref3" runat="server" Text="Teléfono oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_tel_ofi_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_ext_ofi_ref3" runat="server" Text="Extensión Oficina: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_ext_ofi_ref3" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>        
            </div>
        </div>
    </section>

    <%--ABOGADOS Y DESPACHOS --%>
    <section class="panel" runat="server" id="pnl_abog" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Abogados/Despachos</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_abogados" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_fecha_diligencia" runat="server" Text="Ingreso de demanda: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_fecha_diligencia" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_estatus_juicio" runat="server" Text="Estatus juicio: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_estatus_juicio" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_Juzgado" runat="server" Text="Juzgado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_juzgado" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_f_emp_tit" runat="server" Text="Emplazamiento titular: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_f_emp_tit" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_f_emp_aval" runat="server" Text="Emplazamiento aval: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_f_emp_aval" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical flex_1">                                
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_exhorto" runat="server" Text="Exhorto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_exhorto" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_juzgado_exhorto" runat="server" Text="Juzgado exhortado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_juzgado_exhortado" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_gestor" runat="server" Text="Gestor: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_gestor" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_cita" runat="server" Text="Cita: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_cita" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_prom_pago" runat="server" Text="Promesa de pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_fecha_prom_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_monto_prom_pago" runat="server" Text="Monto promesa pago: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" ID="lbl_monto_prom_pago" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1" style="margin-top:-15px!important;">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label ID="lbl_tit_detalle" runat="server" Text="Detalle: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                    <asp:Textbox Enabled="false" TextMode="MultiLine" ID="txt_ddetalle" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                    TargetControlID="pnl_cobranza_Asig" ExpandControlID="pnl_tit_cobranza_Asig" CollapseControlID="pnl_tit_cobranza_Asig"
                    ImageControlID="ToggleImage_asignacion" ExpandedImage="~/img/collapse.jpg"
                    CollapsedImage="~/img/expand.jpg" ExpandedText="Contraer" CollapsedText="Expandir"
                    SuppressPostBack="True" Collapsed="True" Enabled="True" />

                <asp:Panel ID="pnl_tit_cobranza_Asig" runat="server" Style="cursor: pointer;">
                    <div class="texto"><asp:ImageButton ID="ToggleImage_asignacion" runat="server" />
                        <asp:Label ID="Label15" runat="server" CssClass="textogris" Text="Asignación y/o Modificación de Abogado/Despacho"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_cobranza_Asig" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec overflow_x shadow">
                        <asp:Panel ID="pnl_expedientes" runat="server">
                            <asp:DataGrid ID="dag_expedientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                        <ItemStyle Width="5%"/></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDPERSONA" HeaderText="Núm. afiliado">
                                        <ItemStyle Width="8%"/></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="20%"/></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                                        <ItemStyle Width="15%" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHAULTPAGO" HeaderText="Última amortización">
                                        <ItemStyle Width="12%" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MORACAPITAL" HeaderText="Días mora cap">
                                        <ItemStyle Width="10%" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MORAINTERES" HeaderText="Días mora int">
                                        <ItemStyle Width="10%" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPOCARTERA" HeaderText="Tipo cartera">
                                        <ItemStyle Width="10%" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDDESPACHO">
                                        <ItemStyle Width="5px" /></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESPACHO" HeaderText="Abogado/Despacho">
                                        <ItemStyle Width="15%" /></asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="MODIFICAR" Text="Modificar">
                                        <ItemStyle Width="10%"/></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_asignacion" runat="server" CssClass="modalPopup" Width="677px" Style='display: none; background:#D4DDE3;'>
                    <asp:Panel ID="pnl_tit_asignacion" runat="server" CssClass="titulosmodal" Align="Center" Height="20px">
                        <div class="module_subsec flex_center">
                            <label id="lbl_tit" class="subtitulosmodal" style='color: White;'>Abogado/Despacho </label>
                        </div>
                    </asp:Panel>

                    <div class="module_subsec low_m  columned two_columns">
                        <div class="module_subsec_elements align_items_flex_end">
                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                <asp:DropDownList ID="cmb_fase" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_fase_asignar" runat="server" CssClass="text_input_nice_label" Text="*Fase: "></asp:Label>                    
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_fase" CssClass="textogris"
                                        ControlToValidate="cmb_fase" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_asignacion" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements align_items_flex_end">
                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                <asp:DropDownList ID="cmb_despacho_asignar" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_tit_Despacho" runat="server" CssClass="text_input_nice_label" Text="*Abogado/Despacho: "></asp:Label>                    
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_despacho_asignar"
                                        CssClass="textogris" ControlToValidate="cmb_despacho_asignar" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_asignacion" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m  columned two_columns">
                        <div class="module_subsec_elements align_items_flex_end">
                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                <asp:TextBox ID="txt_Valor" runat="server" class="text_input_nice_input" MaxLength="9" ValidationGroup="val_asignacion"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="Label14" runat="server" CssClass="text_input_nice_label" Text="*Porcentaje de comisión: "></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Valor" runat="server" ControlToValidate="txt_Valor"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_asignacion"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Valor" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_Valor"></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Valor" runat="server" Display="Dynamic"
                                        ControlToValidate="txt_Valor" CssClass="textogris" ErrorMessage=" Error:Porcentaje incorrecto"
                                         ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <div class="module_subsec low_m">
                        <div class="module_subsec_elements w_100">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_Objetivo" runat="server" class="text_input_nice_textarea" MaxLength="3000"
                                    ValidationGroup="val_conf" TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_Objetivo" runat="server" CssClass="text_input_nice_label" Text="Motivo:"></asp:Label>                        
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_objetivo" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_Objetivo" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,."></ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div><br />
                    
                    <div class="module_subsec flex_center">
                        <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Guardar" 
                            ValidationGroup="val_asignacion" />&nbsp;&nbsp;
                        <asp:Button ID="btn_cerrar" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                    
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_modal" runat="server" CssClass="alerta"></asp:Label>
                    </div>
                    
                </asp:Panel>

                <div align="center">
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_asignacion" runat="server"
                        BackgroundCssClass="modalBackground" DropShadow="True" DynamicServicePath=""
                        Enabled="True" PopupControlID="pnl_asignacion" PopupDragHandleControlID="pnl_tit_asignacion"
                        TargetControlID="hdn_asignacion"></ajaxToolkit:ModalPopupExtender>
                </div>
            </div>
        </div>
    </section>

    <%--BITACORA --%>
    <section class="panel" runat="server" id="pnl_bit" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Bitácora</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_bitacora" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_cobranza" runat="server" Visible="false">
                            <asp:Label ID="lbl_encabezado" runat="server" Class="module_subsec textogris"> Elija un evento a registrar.</asp:Label>
                            
                            <div class="module_subsec no_m  columned three_columns">
                                <div class="module_subsec_elements align_items_flex_end">
                                    <div class="text_input_nice_div module_subsec_elements_content"> 
                                        <asp:DropDownList ID="cmb_evento" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_evento" runat="server" CssClass="text_input_nice_label">*Evento:</asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_evento" runat="server" ControlToValidate="cmb_evento"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                                        
                            <asp:Panel ID="pnl_llamada" runat="server" Visible="false">
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:Textbox ID="lbl_realizo" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_responsable" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_destinatario_llamada" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_destinatario_llamada" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" Enabled="true" CssClass="textogris" InitialValue="0" 
                                                    ControlToValidate="cmb_destinatario_llamada" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_nombre_destinatario" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_nombre_dest_llamada" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_nombre_destinatario" Display="Dynamic"
                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_fechaejecucion" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_fechaejecucion" runat="server" CssClass="text_input_nice_label" Text="*Fecha: "></asp:Label>                                
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" 
                                                    TargetControlID="txt_fechaejecucion" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txt_fechaejecucion" InvalidValueMessage="Fecha inválida" ValidationGroup="val_bitacora"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage="MaskedEditExtender2" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fechaejecucion"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                                <asp:RequiredFieldValidator runat="server" ID="req_feceje" CssClass="textogris" ControlToValidate="txt_fechaejecucion"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:DropDownList ID="cmb_tipo_tel" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tipotel" runat="server" CssClass="text_input_nice_label" Text="*Tipo de télefono:"></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmb_tipo_tel"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>                               
                                
                                <asp:Label ID="lbl_tit_num" runat="server" CssClass="texto" Text="Número"></asp:Label>
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_lada" runat="server" class="text_input_nice_input" MaxLength="6"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_lada" runat="server" CssClass="texto" Text="Lada"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_lada"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_tel" runat="server" class="text_input_nice_input" MaxLength="15"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_telmov" runat="server" CssClass="text_input_nice_label" Text="*Teléfono:"></asp:Label>                                                   
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_telmov" runat="server" ControlToValidate="txt_tel"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tel" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txt_tel"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_ext" runat="server" class="text_input_nice_input" MaxLength="3" Visible="false" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_extofi" runat="server" CssClass="text_input_nice_label" Text="Extensión:" Visible="false" />                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ext" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_ext"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_tiempo" runat="server" CssClass="text_input_nice_input" MaxLength="4" ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tiempo" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    TargetControlID="txt_tiempo" FilterType="Numbers" Enabled="True" />
                                                <asp:RequiredFieldValidator ID="req_tiempo" runat="server" ControlToValidate="txt_tiempo"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                    ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_resultado" runat="server" CssClass="btn btn-primary2 dropsown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_resacc" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="req_resacc" runat="server" ControlToValidate="cmb_resultado"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnl_personas" runat="server" Visible="false">
                                    <div class="module_subsec no_m  columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:DropDownList ID="cmb_tiporel" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tiporel" runat="server" CssClass="text_input_nice_label" Text="*Tipo de Relación:"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporel" runat="server" ControlToValidate="cmb_tiporel"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:TextBox ID="txt_nombres" runat="server" class="text_input_nice_input" MaxLength="300" ValidationGroup="val_referencias"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_nombres" runat="server" CssClass="text_input_nice_label" Text="Primer nombre:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombres" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_nombres"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_nombre2" runat="server" class="text_input_nice_input" MaxLength="300" ValidationGroup="val_personales" />
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_nombre2" class="text_input_nice_label">Segundo(s) nombre(s):</label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre2" runat="server"
                                                        TargetControlID="txt_nombre2" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:TextBox ID="txt_paterno" runat="server" class="text_input_nice_input" MaxLength="100" ValidationGroup="val_Referencias"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_paterno" runat="server" CssClass="text_input_nice_label" Text="Apellido paterno:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_paterno"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_materno" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_materno" runat="server" CssClass="texto" Text="Apellido Materno:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_materno"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_prom_pago_llamada" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_prom_pago_llamada" runat="server" CssClass="texto" Text="Promesa Pago : "></asp:Label>                                
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_prom_pago_llamada" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_prom_pago_llamada" InvalidValueMessage="Fecha inválida"
                                                    ValidationGroup="val_bitacora" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txt_prom_pago_llamada"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_prom_pago_llamada" runat="server" CssClass="text_input_nice_input" MaxLength="10" ></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_monto_prom_pago_llamada" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label>                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_llamada"
                                                    ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_monto_prom_pago_llamada"
                                                    CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>   

                                <div class="module_subsec low_m">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_detacc" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"
                                                 onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_detacc" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>
                                                <asp:RequiredFieldValidator runat="server" ID="req_detacc" CssClass="textogris" ControlToValidate="txt_detacc"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                    </div>
                                </div><br />
                                        
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="lnk_guardar" runat="server" CssClass="btn btn-primary" ValidationGroup="val_bitacora" Text="Agregar"/> &nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_llamda" runat="server" Text="Cancelar" CssClass="btn btn-primary"/>
                                </div>                                
                            </asp:Panel>

                            <asp:Panel ID="pnl_evento_cita" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:DropDownList ID="cmb_evento_cita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <label id="Label8" class="text_input_nice_label">*Acción:</label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_evento_cita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_evento" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seg" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                 <asp:DropDownList ID="cmb_cita_seguimiento" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="Label11" runat="server" CssClass="text_input_nice_label" Text="*Cita: "></asp:Label>                                   
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_cita_seguimiento" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                    
                                </asp:Panel>

                                <asp:Panel ID="pnl_agendar" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_destinatario" runat="server" class="btn btn-primary2 dropdow_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_destinatario" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_destinatario" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_persona_cita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="LBL_nombre_destinatario" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_persona_cita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_c_usuario" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_usuario" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_Bitacoras" class="text_input_nice_label">*Sucursal:</label>                                   
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_sucursal" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_cita_fecha" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_fecha" runat="server" CssClass="text_input_nice_label" Text="*Fecha de cita: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_cita_fecha" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_cita_fecha" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_cita_fecha"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="textogris"
                                                        ControlToValidate="txt_cita_fecha" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_hora" runat="server" CssClass="text_input_nice_label" Text="*Hora cita (Formato 24 Hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txt_hora_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_hora"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cita" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_hora" runat="server" Display="Dynamic"
                                                        ControlToValidate="txt_hora" CssClass="textogris" ErrorMessage=" Error:Formato de hora incorrecto"
                                                         ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec low_m">
                                        <div class="module_subsec_elements w_100">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_cita_notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_cita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                                                
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" CssClass="textogris"
                                                        ControlToValidate="txt_cita_notas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_cita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_cita" Text="Agregar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_cita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_usuario_atendio" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_usuario_atendio" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_usuario_atendio" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_atendio" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="Label10" class="text_input_nice_label">*Sucursal registro:</label>                                     
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_atendio" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_atencion" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_resultado_atencion" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmb_resultado_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_cita_a"></asp:RequiredFieldValidator>
                                                </div>                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_atencion" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_atendio" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                   
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_atencion" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_atencion" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_fecha_atencion"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_atencion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita_a" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_atencion" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_hora_atendio" runat="server" CssClass="text_input_nice_label" Text="*Hora de registro (Formato 24 Hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_atencion"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_hora_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_hora_atencion"
                                                        CssClass="textogris" ErrorMessage=" Error:Formato incorrecto" Display="Dynamic"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_duracion_atencion" runat="server" CssClass="text_input_nice_input" MaxLength="4" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_duracion_atencion" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        TargetControlID="txt_duracion_atencion" FilterType="Numbers" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_duracion_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                        ValidationGroup="val_cita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_cita" runat="server" CssClass="texto" Text="Promesa pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999" TargetControlID="txt_prom_pago_cita" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" MaskType="Date"
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_cita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txt_prom_pago_cita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>  
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prom_pago_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_cita" runat="server" CssClass="texto" Text="Monto promesa pago: "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_cita"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_monto_prom_pago_cita"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div><br />

                                    <div class="module_subsec">
                                        <div class="module_subsec_elements w_100">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_motivo_atencion" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_motivo_cita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" CssClass="textogris"
                                                        ControlToValidate="txt_motivo_atencion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita_a" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_seguimiento" runat="server" CssClass="btn btn-primary" ValidationGroup="val_cita_a" Text="Agregar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_cita_seg" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>                                    
                                </asp:Panel>

                            <asp:Panel ID="pnl_aviso" runat="server" Visible="false">                                    
                                <asp:Label ID="lbl_avisos_gen" runat="server" CssClass="h4" Text="Avisos Generados "></asp:Label>

                                <div class="module_subsec overflow_x shadow">
                                    <asp:DataGrid ID="dag_aviso" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_AVISO">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PLANTILLA">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PERSONA">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                                                <ItemStyle Width="20px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle Width="300px"/></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="SEGUIMIENTO" Text="Seguimiento">
                                                <ItemStyle Width="20"/></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </asp:Panel>
                            
                                <asp:Panel ID="pnl_seguimiento_aviso" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_destinatario_aviso" runat="server" CssClass="text_input_nice_input" Enabled="false" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_nombre_Destinatario_Aviso" runat="server" CssClass="text_input_nice_labels"
                                                        Text="*Nombre destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_aviso" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_Resultado_aviso" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="cmb_resultado_aviso"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_aviso"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_aviso"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_resultado" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_aviso" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_aviso" InvalidValueMessage="Fecha inválida" ValidationGroup="val_aviso"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_fecha_aviso"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_aviso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_aviso" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec low_m">
                                        <div class="module_subsec_elements w_100">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_notas_aviso" runat="server" CssClass="textocajas" MaxLength="3000"
                                                    Width="400px" Height="100px" TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_aviso" runat="server" CssClass="texto" Text="*Notas:" Height="100px"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" CssClass="textogris"
                                                        ControlToValidate="txt_notas_aviso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_aviso" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_aviso"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_aviso" runat="server" CssClass="texto" Text="Promesa Pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_aviso" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_aviso" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_aviso" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txt_prom_pago_aviso"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prom_pago_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_aviso" runat="server" CssClass="texto" Text="Monto promesa pago: "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_aviso"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_monto_prom_pago_aviso"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Label ID="lbl_nombre_usuario_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_usuario_aviso" runat="server" CssClass="texto" Text="*Atendió: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Label ID="lbl_destino_aviso" runat="server" CssClass="texto" Text="*Destinatario: "></asp:Label>
                                    <asp:Label ID="lbl_nombre_destino_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_aviso" runat="server" CssClass="btn btn-primary" ValidationGroup="val_aviso" Text="Agregar"/>&nbsp;
                                        <asp:Button ID="lnk_guardar_cancelar" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                    
                                    </asp:Panel>
                            
                                <asp:Panel ID="pnl_digitalizar_aviso" runat="server" Visible="false">
                                    <table border="0" align="center"><tr align="center"><td colspan="2"><asp:Label ID="lbl_tit_aviso_dig" runat="server" CssClass="textogris" Text="¿Desea digitalizar aviso?"></asp:Label></td></tr><tr align="center">
                                        <asp:LinkButton ID="lnl_si_aviso" runat="server" CssClass="textogris" Text="Si"></asp:LinkButton></td><td><asp:LinkButton ID="lnk_no_aviso" runat="server" CssClass="textogris" Text="No"></asp:LinkButton></td></tr></table>
                                </asp:Panel>

                            <asp:Panel ID="pnl_citatorio" runat="server" Visible="false">
                                <asp:Label ID="lbl_citatorios_sub" runat="server" CssClass="module_subsec subtitulos" Text="Citatorios generados "></asp:Label>

                                <asp:DataGrid ID="dag_citatorios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" Width="100%">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_CITATORIO">
                                        <ItemStyle Width="5px"/></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_PLANTILLA">
                                            <ItemStyle Width="5px"/></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_PERSONA">
                                        <ItemStyle Width="5px"/></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                                            <ItemStyle Width="20px" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle Width="300px" /></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="SEGUIMIENTO" Text="Seguimiento">
                                            <ItemStyle Width="20"></ItemStyle></asp:ButtonColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento_citatorio" runat="server" Visible="false">
                                    <div class="module_subsec low_m  columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Label ID="lbl_nombre_destino_cit" runat="server" CssClass="texto" Text=""></asp:Label>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_destinatario_tit" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Label ID="lbl_nombre_destinatario_cit" runat="server" CssClass="texto" Text=""></asp:Label>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="bl_nom_destinatario_tit" runat="server" CssClass="texto" Text="*Nombre destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_citatorio" runat="server" CssClass="textocajas"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_resultado_cit" runat="server" CssClass="texto" Text="*Resultado obtenido: "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmb_resultado_citatorio"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_citatorio"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_citatorio" runat="server" CssClass="textocajas" MaxLength="10"
                                                    ValidationGroup="val_citatorio"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_Fecha_citatorio" runat="server" CssClass="texto" Text="*Fecha de registro: "></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_citatorio" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_citatorio" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_citatorio" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txt_fecha_citatorio"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator22" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_citatorio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_citatorio" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_citatorio" runat="server" CssClass="textocajas" MaxLength="10"
                                                    ValidationGroup="val_citatorio"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_citatorio" runat="server" CssClass="texto" Text="Promesa de pago : "></asp:Label>                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_citatorio" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_citatorio" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_citatorio" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txt_prom_pago_citatorio"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_pago_citatorio" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_citatorio" runat="server" CssClass="texto" Text="Monto promesa pago: "></asp:Label>                                        
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_pago_citatorio"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_monto_pago_citatorio"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec low_m">
                                        <div class="module_subsec_elements w_100">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_notas_citatorio" runat="server" CssClass="textocajas" MaxLength="3000"
                                                    Width="400px" Height="100px" TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_citatorio" runat="server" CssClass="texto" Text="*Notas:"></asp:Label>    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator24" CssClass="textogris"
                                                        ControlToValidate="txt_notas_citatorio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_citatorio" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>          
                                        
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_Citatorio" runat="server" CssClass="btn btn-primary" ValidationGroup="val_citatorio" Text="Agregar"/>&#160;
                                        <asp:Button ID="lnk_cancelar_citatorio" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                    <asp:Label ID="lbl_usr_cit" runat="server" CssClass="texto" Text="*Atendió: "></asp:Label>
                                    <asp:Label ID="lbl_usuario_citatorio" runat="server" CssClass="texto" Text=""></asp:Label>
                                </asp:Panel></asp:Panel>
                                <asp:Panel ID="pnl_digitalizar_citatorio" runat="server" Visible="false">
                                    <table border="0" align="center">
                                        <tr align="center"><td colspan="2"><asp:Label ID="lbl_tit_digitalizar" runat="server" CssClass="textogris" Text="¿Desea digitalizar citatorio?"></asp:Label>
                                            <td><asp:LinkButton ID="lnk_si" runat="server" CssClass="textogris" Text="Si"></asp:LinkButton></td><td><asp:LinkButton ID="lnk_no" runat="server" CssClass="textogris" Text="No"></asp:LinkButton></td></tr></table>
                                </asp:Panel>

                            <asp:Panel ID="pnl_reg_juicio" runat="server" Visible="false"> 
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:Textbox ID="lbl_user_despacho" runat="server" Enabled="false" CssClass="text_input_nice_input" Text=""></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_realizo" runat="server" CssClass="text_input-nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_ingreso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_fecha_diligencia" runat="server" CssClass="text_input_nice_label" Text="*Fecha ingreso de demanda: "></asp:Label></td><td>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_ingreso" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender7"
                                                    ControlToValidate="txt_fecha_ingreso" InvalidValueMessage="Fecha inválida" Display="Dynamic"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender7" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txt_fecha_ingreso"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator27" CssClass="textogris" Display="Dynamic"
                                                    ControlToValidate="txt_fecha_ingreso" ErrorMessage=" Falta Dato!" ValidationGroup="val_juicio" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estatus_juicio" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_estatus" runat="server" CssClass="text_input_nice_label" Text="*Estatus juicio: "></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="cmb_estatus_juicio"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_juicio"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_juzgado" runat="server" CssClass="text_input_nice_input" MaxLength="3000"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_juzgado" runat="server" CssClass="text_input_nice_label" Text="*Juzgado: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_juzgado" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator25" CssClass="textogris"
                                                    ControlToValidate="txt_juzgado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_juicio" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_emp_tit" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                             ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_fecha_emp_tit" runat="server" CssClass="text_input_nice_label" Text="Fecha emplazamiento titular: "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender13" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_emp_tit" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator13" runat="server" ControlExtender="MaskedEditExtender13"
                                                    ControlToValidate="txt_fecha_emp_tit" InvalidValueMessage="Fecha inválida"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender13" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txt_fecha_emp_tit"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_emp_Aval" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_fecha_emp_aval" runat="server" CssClass="text_input_nice_label" Text="Fecha emplazamiento aval: "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender14" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_emp_Aval" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator14" runat="server" ControlExtender="MaskedEditExtender14"
                                                    ControlToValidate="txt_fecha_emp_Aval" InvalidValueMessage="Fecha inválida" Display="Dynamic"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender14" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txt_fecha_emp_Aval"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_exhorto" runat="server" CssClass="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_exhorto" runat="server" CssClass="text_input_nice_labels" Text="Exhorto: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_exhorto" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_juzgado_exhorto" runat="server" CssClass="text_input_nice_input" MaxLength="3000"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_juzgado_exhorto" runat="server" CssClass="text_input_nice_label" Text="Juzgado Exhortado: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_exhorto" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_gestor" runat="server" CssClass="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_Gestor" runat="server" CssClass="text_input_nice_label" Text="*Gestor: "></asp:Label>                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_gestor" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_gestor"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                    ValidationGroup="val_juicio" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                           
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_cita_juicio" runat="server" CssClass="texto" Text="Cita : "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_cita" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator12" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_cita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_juicio"
                                                    CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txt_cita"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_prom_pago" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_prom_pago" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_prom_pago" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_prom_pago" InvalidValueMessage="Fecha inválida" ValidationGroup="val_juicio"
                                                    CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt_prom_pago"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_prom_pago" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <asp:Label ID="Label16" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago"
                                                    ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" runat="server"
                                                    ControlToValidate="txt_monto_prom_pago" CssClass="textogris" ErrorMessage=" Error:Monto incorrecto"
                                                    lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        
                                <div class= "module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex:1;">
                                        <div class="module_subsec_elements_content vertical">
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_detalle" runat="server" CssClass="text_input_nice_label" Text="*Detalle: "></asp:Label>                               
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" CssClass="textogris"
                                                    ControlToValidate="txt_detalle" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_juicio" SetFocusOnError="True" />
                                            </div>
                                             <asp:TextBox ID="txt_detalle" runat="server" CssClass="text_input_nice_textarea" MaxLength="8000"
                                             TextMode="MultiLine" onKeyUp="javascript:Check(this, 8000);" onChange="javascript:Check(this, 8000);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div><br />
                                
                                <div class="modules_subsec flex_end">
                                    <asp:Button ID="lnk_agregar_reg_juicio" runat="server" CssClass="btn btn-primary" ValidationGroup="val_juicio" Text="Agregar" />&nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_reg_juicio" runat="server" CssClass="btn btn-primary" Text="Cancelar" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnl_estatus" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:Textbox ID="lbl_user_estatus" runat="server" CssClass="text_input_nice_input" Enabled="False" Text=""></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_user_estatus" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estatus_cob" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_estatus_cob" runat="server" CssClass="text_input_nice_label" Text="*Estatus cobranza: "></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="cmb_estatus_cob"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_estatus"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="lnk_agregar_estatus" runat="server" CssClass="btn btn-primary" ValidationGroup="val_estatus" Text="Agregar"/>&nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_estatus" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                </div> 
                            </asp:Panel>

                            <asp:Panel ID="pnl_evento_visita" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_evento_visita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <label id="lbl_tit_accion_visita" class="text_input_nice_label">*Acción:</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_evento_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_visita_evento" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seg_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_visita_seguimiento" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="Label17" runat="server" CssClass="text_input_nice_label" Text="*Visita: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator30" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_visita_seguimiento" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_agendar_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_usuario_visita" runat="server" CssClass="text_input_nice_input" Enabled="false"></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_realizo_visita" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_destinatario_visita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_destinatario_visita" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator33" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_destinatario_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_persona_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_nombre_visita" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator34" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_persona_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_tit_sucursal_visita" class="text_input_nice_label">*Sucursal:</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator35" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_visita"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_fecha_visita" runat="server" CssClass="text_input_nice_label" Text="*Fecha de visita: "></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender15" runat="server" Mask="99/99/9999" CultureTimePlaceholder="" Enabled="True"
                                                        MaskType="Date" TargetControlID="txt_fecha_visita" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""/>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator15" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_visita"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txt_fecha_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator36" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_visita" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_hora_visita" runat="server" CssClass="text_input_nice_label" Text="*Hora visita (Formato 24 Hrs (HH:MM)):"></asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_visita"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_hora_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_visita" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                                        ControlToValidate="txt_hora_visita" CssClass="textogris" ErrorMessage=" Error:Formato incorrecto"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_usuario_seg_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_user_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator45" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_usuario_seg_visita" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class= "module_subsec no_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_notas_visita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator38" CssClass="textogris"
                                                        ControlToValidate="txt_noras_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" SetFocusOnError="True" />
                                                </div>
                                                <asp:TextBox ID="txt_noras_visita" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000"
                                                TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_visita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_visita" Text="Agregar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_visita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_visita" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_resultado_visita" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="cmb_resultado_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_seg_visita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_seg_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_tit_seg_suc" class="text_input_nice_label">*Sucursal registro:</label>                                     
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_seg_visita" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                       
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_seg_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_seg_visita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender16" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_atencion" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator16" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_seg_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_seg_visita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txt_fecha_seg_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator41" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_seg_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_seg_visita_a" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_seg_visita" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_hora_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Hora de registro (Formato 24 Hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_seg_visita"></ajaxToolkit:FilteredTextBoxExtender>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_hora_seg_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_hora_seg_visita"
                                                        CssClass="textogris" ErrorMessage=" Error:Formato incorrecto" Display="Dynamic"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_duracion_visita" runat="server" CssClass="text_input_nice_input" MaxLength="4"
                                                    ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_duracion_visita" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                        TargetControlID="txt_duracion_visita" FilterType="Numbers" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_duracion_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                        ValidationGroup="val_seg_visita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_seg_visita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_seg_prompago" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender17" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_visita" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator17" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_seg_visita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="txt_prom_pago_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prompago_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prompago_visita" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label></td><td>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prompago_visita"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_monto_prompago_visita"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class= "module_subsec no_m columned four_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_seg_notas" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator44" CssClass="textogris"
                                                        ControlToValidate="txt_seg_notas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_seg_visita_a" SetFocusOnError="True" />
                                                </div>
                                                <asp:TextBox ID="txt_seg_notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_seg_visita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_seg_visita_a" Text="Agregar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_seg_visita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>                                                
                                </asp:Panel>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                            </div>
                </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </section>

    <%--CALCULADORA DE PRÉSTAMO--%>
    <section class="panel" runat="server" id="pnl_calc" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Calculadora de préstamo</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_calculadora_credito" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="Label13" runat="server" Class="module_subsec textogris"> Elija una fecha de corte para calcular.</asp:Label><br />
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Monto del préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_montocredito"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_montocreditotxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Saldo del préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_saldocredito"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_saldocreditotxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Fecha disposición préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechaliberacion"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechaliberaciontxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Fecha último pago:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechaultimopago"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechaultimopagotxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Tasa interés ordinaria:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_intnor"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_intnortxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Tasa interés moratoria:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_intmor"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_intmortxt"></asp:TextBox>

                                </div>
                            </div>

                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Días desde el último pago:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_diasultimopago"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_diasultimopagotxt"></asp:TextBox>

                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Abonos vencidos:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_vencidos"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_vencidostxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Fecha vencimiento:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechavenc"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechavenctxt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Fecha de corte:" CssClass="module_subsec_elements module_subsec_bigmedium-elements  " ID="lbl_fechacorte"></asp:Label>
                                    <asp:TextBox ID="txt_fechacorte" runat="server" CssClass="module_subsec_elements flex_1 text_input_nice_input" AutoPostBack="True"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechacorte" runat="server"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechacorte" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechacorte" runat="server"
                                        ControlExtender="MaskedEditExtender_fechacorte" ControlToValidate="txt_fechacorte"
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_Personales" CssClass="textogris"
                                        ErrorMessage="MaskedEditValidator_fechacorte" Display="Dynamic" />
                                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="txt_fechacorte"
                                        Format="dd/MM/yyyy" />
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Monto para liquidar préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements"
                                        ID="lbl_monto_liq"></asp:Label>
                                    <asp:TextBox runat="server" ToolTip="Saldo insoluto - Cuenta eje" Enabled="false" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_monto_liq_txt"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Saldo cuenta eje:" CssClass="module_subsec_elements module_subsec_bigmedium-elements"
                                        ID="Label12"></asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_saldo_cta_eje_txt"></asp:TextBox>
                                </div>
                            </div>
                        </div>                           
                        
                        <div id="upd_datospago">
                            <asp:UpdatePanel ID="updpnl_datospago" runat="server">
                                <ContentTemplate>
                                    <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_info_fecha_corte" runat="server" CssClass="alerta"
                                            Text="Error: no puede elegir una fecha anterior a la fecha de sistema. Verifique." Visible="false" />
                                    </div>
                                    <div class="module_subsec ">
                                    <div class="shadow"  style="flex: 1;">
                                        <asp:GridView ID="grd_pago" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                            GridLines="None" Width="100%">
                                            <HeaderStyle CssClass="table_header" />
                                            <Columns>
                                                <asp:BoundField DataField="CUENTA" HeaderText="Cuenta" SortExpression="CUENTA" />
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                                                <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO"/>
                                                <asp:BoundField DataField="ABONO" HeaderText="Abono" SortExpression="ABONO"/>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txt_fechacorte" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <ajaxToolkit:UpdatePanelAnimationExtender ID="personales" runat="server" BehaviorID="animation_datospago"
                            TargetControlID="updpnl_datospago">
                            <Animations>
                                <OnUpdating>
                                    <Sequence>
                                        <ScriptAction Script="var b = $find('animation_datospago'); b._originalHeight = b._element.offsetHeight;" />
                                        <StyleAction Attribute="overflow" Value="hidden" />
                                        <Parallel duration=".25" Fps="30">
                                            <FadeOut AnimationTarget="upd_datospago" minimumOpacity=".2" />
                                        </Parallel>
                                    </Sequence>
                                </OnUpdating>
                                <OnUpdated>
                                <Sequence>
                                    <Parallel duration=".25" Fps="30">
                                        <FadeIn AnimationTarget="upd_datospago" minimumOpacity=".2" />
                                    </Parallel>
                                </Sequence>
                            </OnUpdated>
                        </Animations>
                        </ajaxToolkit:UpdatePanelAnimationExtender><br />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </section>

    <%--REPORTE AVALES--%>
    <section class="panel" runat="server" id="pnl_rep" Visible="False">
        <header class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Reporte avales</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">                
                <asp:Label ID="lbl_titulo_fecha" runat="server" Class="module_subsec textogris"> Para generar el reporte de Avales, de clic en el botón Generar. </asp:Label>

                <div class="module_subsec flex_center">
                    <asp:Button ID="btn_ver_Aviso" runat="server" class="btn btn-primary" Text="Generar" />
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label runat="server" class="alerta" ID="lbl_documento"></asp:Label>           
                </div>
            </div>
        </div>
    </section>
    
    <input type="hidden" name="hdn_asignacion" id="hdn_asignacion" runat="server" />
</asp:Content>