<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_INVERSIONES.aspx.vb" Inherits="SNTE5.CRED_EXP_INVERSIONES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <asp:Label runat="server" Text="Inversiones" />
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="module_subsec columned low_m four_columns">
                        <div class="module_subsec_elements text_input_nice_div">

                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ciclo:" />
                               <asp:DropDownList ID="cmb_ciclo" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack ="true">
                                 </asp:DropDownList>
                            </div>
                        </div>
                        </div>
                    <div class="module_subsec columned low_m four_columns">
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="tbx_monto_inversion" CssClass="text_input_nice_input" MaxLength="20" />
                            <asp:TextBox runat="server" ID="tbx_id_inversion" CssClass="text_input_nice_input"
                                Enabled="false" Visible="false" Text="0" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Monto Inversión:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="tbx_monto_inversion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_inversion" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_monto_inversion"
                                    FilterType="Custom, Numbers" ValidChars="." />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="tbx_monto_inversion"
                                    CssClass="textogris" ErrorMessage=" Monto Incorrecto!" Display="Dynamic"
                                    ValidationExpression="^[0-9]{1,17}(\.[0-9]{1}[0-9]?)?$" />
                            </div>
                        </div>
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="tbx_tasa_inversion" CssClass="text_input_nice_input" MaxLength="11" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Tasa Inversión (Anual):" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="tbx_tasa_inversion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_inversion" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_tasa_inversion"
                                    FilterType="Custom, Numbers" ValidChars="." />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="tbx_tasa_inversion"
                                    CssClass="textogris" ErrorMessage=" Tasa Incorrecto!" Display="Dynamic"
                                    ValidationExpression="^[0-9]{1,3}(\.[0-9]{1}[0-9]?)?$" />
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" ID="tbx_fecha_ini_inv" CssClass="text_input_nice_input"
                                    MaxLength="10" ValidationGroup="val_inversion" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="mee_fecha_ini_inv" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="tbx_fecha_ini_inv" />
                                <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="tbx_fecha_ini_inv" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Fecha Inicio Inversión:" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbx_fecha_ini_inv"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_inversion" />
                                    <ajaxToolkit:MaskedEditValidator runat="server" ID="mev_fecha_ini_inv"
                                        ControlExtender="mee_fecha_ini_inv" ControlToValidate="tbx_fecha_ini_inv"
                                        CssClass="textogris" ErrorMessage="mev_fecha_ini_inv" InvalidValueMessage="Fecha Invalida" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" ID="tbx_fecha_fin_inv" CssClass="text_input_nice_input"
                                    MaxLength="10" ValidationGroup="val_inversion" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="mee_fecha_fin_inv" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="tbx_fecha_fin_inv" />
                                <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="tbx_fecha_fin_inv" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Fecha Término Inversión:" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbx_fecha_fin_inv"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_inversion" />
                                    <ajaxToolkit:MaskedEditValidator runat="server" ID="mev_fecha_fin_inv"
                                        ControlExtender="mee_fecha_fin_inv" ControlToValidate="tbx_fecha_fin_inv"
                                        CssClass="textogris" ErrorMessage="mev_fecha_fin_inv" InvalidValueMessage="Fecha Invalida" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lbl_estatus_inversion" CssClass="module_subsec flex_center alerta" />
                    <div class="module_subsec flex_end">
                        <asp:Button runat="server" ID="btn_guardar" CssClass="btn btn-primary" ValidationGroup="val_inversion"
                            Text="Guardar" />
                    </div>
                    <div class="module_subsec columned low_m four_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" ID="tbx_devengados" CssClass="text_input_nice_input" Enabled="false" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="Intereses Devengados Totales:" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" ID="tbx_devengados_75" CssClass="text_input_nice_input" Enabled="false" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="Intereses Devengados Totales 75%:" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" ID="tbx_devengados_25" CssClass="text_input_nice_input" Enabled="false" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="Intereses Devengados Totales 25%:" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec overflow_x shadow">
                        <div class="flex_1">
                            <asp:DataGrid runat="server" ID="dgd_inversiones" AutoGenerateColumns="False"
                                CssClass="table table-striped" GridLines="None">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="ID_INVERSION" HeaderText="Id Inversión" Visible="false" />
                                    <asp:BoundColumn DataField="ID_ESTATUS" HeaderText="Id Estatus" Visible="false" />
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto Invertido" />
                                    <asp:BoundColumn DataField="TASA" HeaderText="Tasa Inversión (%)" />
                                    <asp:BoundColumn DataField="FECHA_INICIO" HeaderText="Fecha Inicio Inversión" />
                                    <asp:BoundColumn DataField="FECHA_TERMINO" HeaderText="Fecha Término Inversión" />
                                    <asp:BoundColumn DataField="PLAZO" HeaderText="Días Inversión" />
                                    <asp:BoundColumn DataField="BRUTO" HeaderText="Monto Bruto" />
                                    <asp:BoundColumn DataField="DEVENGADOS" HeaderText="Intereses Devengados" />
                                    <asp:BoundColumn DataField="DEVENGADOS_75" HeaderText="Intereses Devengados 75%" />
                                    <asp:BoundColumn DataField="DEVENGADOS_25" HeaderText="Intereses Devengados 25%" />
                                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus Inversión" />
                                    <asp:ButtonColumn CommandName="EDITAR" Text="Editar" />
                                    <asp:ButtonColumn CommandName="DETALLE" Text="Detalle" />
                                    <asp:ButtonColumn CommandName="DISPERSAR" Text="Dispersar" />
                                    <asp:ButtonColumn CommandName="REPORTE" Text="Reporte" />
                                    <asp:ButtonColumn CommandName="CONFIRMAR" Text="Confirmar" Visible="false" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="module_subsec overflow_x shadow">
                        <div class="flex_1">
                            <asp:DataGrid runat="server" ID="dgd_inversion_detalle" AutoGenerateColumns="False"
                                CssClass="table table-striped" GridLines="None">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="ID_INVERSION" HeaderText="Id Inversión" Visible="false" />
                                    <asp:BoundColumn DataField="ID_DETALLE" HeaderText="Id Detalle" Visible="false" />
                                    <asp:BoundColumn DataField="QNA" HeaderText="Quincena" />
                                    <asp:BoundColumn DataField="ANIO" HeaderText="Año" />
                                    <asp:BoundColumn DataField="MONTO_BRUTO" HeaderText="Monto Bruto" />
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto Invertido" />
                                    <asp:BoundColumn DataField="INTERESES" HeaderText="Intereses Devengados" />
                                    <asp:BoundColumn DataField="DIAS" HeaderText="Días de Inversión" />
                                    <asp:BoundColumn DataField="RENDIMIENTOS_75" HeaderText="Rendimientos del 75%" />
                                    <asp:BoundColumn DataField="RENDIMIENTOS_25" HeaderText="Rendimientos del 25%" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnl_modal_confirmar" Style="display: none;" Align="Center">
                        <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                            <section class="panel no_bm " style="display: inline-block">
                                <header runat="server" class="panel-heading ">
                                    <asp:Label runat="server" Text="Confirmación de Inversión" />
                                </header>
                                <div class="panel-body align_items_flex_center">
                                    <asp:Label runat="server" ID="lbl_id_estatus" CssClass="resalte_azul module_subsec" Visible="false" />
                                    <asp:Label runat="server" ID="lbl_id_inversion" CssClass="resalte_azul module_subsec" Visible="false" />
                                    <asp:Label runat="server" ID="lbl_inversion" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_intereses" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_dispersion" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_fondo" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_pregunta" CssClass="resalte_azul module_subsec" />
                                    <asp:Button runat="server" ID="btn_confirmar" CssClass="btn btn-primary" Text="Aceptar" />
                                    <asp:Button runat="server" ID="btn_canelar" CssClass="btn btn-primary" Text="Cancelar" />
                                </div>
                            </section>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="modal_confirmar" runat="server"
                        Enabled="True" PopupControlID="pnl_modal_confirmar"
                        PopupDragHandleControlID="pnl_modal_confirmar" TargetControlID="hdn_alertas">
                    </ajaxToolkit:ModalPopupExtender>
                    <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />
                </div>
            </div>
        </section>
    </div>

</asp:Content>
