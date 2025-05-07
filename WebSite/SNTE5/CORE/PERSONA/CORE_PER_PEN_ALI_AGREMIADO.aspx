<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PEN_ALI_AGREMIADO.aspx.vb" Inherits="SNTE5.CORE_PER_PEN_ALI_AGREMIADO" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            }
            else {
                var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            }
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
        function ClickBotonBusqueda(ControlTextbox, ControlButton) {
            var CTextbox = document.getElementById(ControlTextbox);
            var CButton = document.getElementById(ControlButton);
            if (CTextbox != null && CButton != null) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    if (CTextbox.value != "") {
                        CButton.click();
                        CButton.disabled = true;
                    }
                    else {
                        CTextbox.focus()
                        return false
                    }
                    return true
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="panel">
                <header class="panel-heading" runat="server" id="head_panel_lbl_titulo_b1">
                    <asp:Label runat="server" Text="Datos del demandado" />
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <section runat="server" id="stn_buscar_agremiado">
                            <div class="module_subsec columned low_m three_columns">
                                <div class="module_subsec_elements text_input_nice_div">
                                    <asp:TextBox runat="server" ID="tbx_rfc" CssClass="text_input_nice_input" MaxLength="13" />
                                    <asp:TextBox runat="server" ID="tbx_id_persona" CssClass="text_input_nice_input" MaxLength="9"
                                        Visible="false" Enabled="false" Text="0" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" TargetControlID="tbx_rfc"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" />
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                                </div>
                                <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                                    <asp:Button runat="server" ID="btn_buscar" Text="Buscar RFC"
                                        CssClass="btn btn-primary module_subsec_elements no_tbm " />
                                    <asp:Button runat="server" ID="btn_buscar_agremiado" Text="Buscar Agremiado"
                                        CssClass="btn btn-primary module_subsec_elements no_tbm" />
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lbl_estatus_rfc" CssClass="module_subsec flex_center alerta" />
                        </section>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_rfc_agremiado" CssClass="text_input_nice_input" MaxLength="13"
                                        Enabled="false" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="RFC:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_nombre_agremiado" CssClass="text_input_nice_input"
                                        Enabled="false" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Agremiado:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_id_registro" CssClass="text_input_nice_input"
                                        Enabled="false" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_region" CssClass="text_input_nice_input" Enabled="false" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Región:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_delegacion" CssClass="text_input_nice_input" Enabled="false" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Delegación:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_ct" CssClass="text_input_nice_input" Enabled="false" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="CT:" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section class="panel">
                <header class="panel-heading panel_header_folder" runat="server" id="head_panel_beneficiario">
                    <asp:Label runat="server" Text="Datos del beneficiario" />
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_beneficiario" CssClass="text_input_nice_input"
                                        MaxLength="200" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" TargetControlID="tbx_beneficiario"
                                        FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑÜáéíóúñü" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Nombre beneficiario:" />
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage=" Falta Dato!" ValidationGroup="VAL_BENEFICIARIO"
                                            Display="Dynamic" ControlToValidate="tbx_beneficiario" CssClass="alertaValidator bold" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_rfc_beneficiario" MaxLength="13" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbx_rfc_beneficiario" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="RFC del beneficiario:" />
                                        <asp:Label runat="server" ID="lbl_error_rfc" Visible="false" Style="color: red" CssClass="bold" Text=" Formato de RFC Incorrecto!" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="textogris"
                                            ControlToValidate="tbx_rfc_beneficiario" ErrorMessage=" Formato Incorrecto!"
                                            ValidationExpression="^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_telefono" CssClass="text_input_nice_input" MaxLength="20" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="tbx_telefono" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Teléfono:" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_correo" CssClass="text_input_nice_input" MaxLength="50" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Correo:" />
                                        <asp:Label runat="server" ID="lbl_error_correo" Visible="false" CssClass="bold" Style="color: red"
                                            Text="Formato de correo incorrecto!" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="textogris"
                                            ControlToValidate="tbx_correo" ErrorMessage=" Formato Incorrecto!"
                                            ValidationExpression="^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox runat="server" ID="tbx_cp" CssClass="text_input_nice_input" MaxLength="5" />
                                        <div class="text_input_nice_labels">
                                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Código postal:" />
                                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers" TargetControlID="tbx_cp" />
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div>
                                        &nbsp;
                                <asp:ImageButton runat="server" ID="btn_buscar_cp" ImageUrl="~/img/glass.png" Style="height: 16px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_estado" CssClass="btn btn-primary2 dropdown_label" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Estado:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_municipio" CssClass="btn btn-primary2 dropdown_label" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Municipio:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_colonia" CssClass="btn btn-primary2 dropdown_label" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Colonia:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_calle" MaxLength="100" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑÜáéíóúñü"
                                        TargetControlID="tbx_calle" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Calle:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_numero_ext" MaxLength="10" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=" "
                                        TargetControlID="tbx_numero_ext" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Numero exterior:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_numero_int" MaxLength="10" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=" "
                                        TargetControlID="tbx_numero_int" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Numero interior:" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_banco" CssClass="btn btn-primary2 dropdown_label" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Banco:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_clabe" MaxLength="18" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="tbx_clabe" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="CLABE:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_clabe_confirma" MaxLength="18" CssClass="text_input_nice_input" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="tbx_clabe_confirma" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Confirmar CLABE:" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_porcentaje_oficio" CssClass="text_input_nice_input" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*% declarado en el oficio:" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                            ControlToValidate="tbx_porcentaje_oficio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="."
                                            TargetControlID="tbx_porcentaje_oficio" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="textogris"
                                            ControlToValidate="tbx_porcentaje_oficio" ErrorMessage=" Formato Incorrecto!"
                                            ValidationExpression="^(100(\.0{1,2})?|[0-9]?[0-9]|([0-9]?[0-9](\.[0-9]{1,2})))$"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_porcentaje" CssClass="text_input_nice_input" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" ID="lbl_periodo_porc_bene" CssClass="text_input_nice_label" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                            ControlToValidate="tbx_porcentaje" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="."
                                            TargetControlID="tbx_porcentaje" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="textogris" Display="Dynamic"
                                            ControlToValidate="tbx_porcentaje" ErrorMessage=" Formato Incorrecto!"
                                            ValidationExpression="^(100(\.0{1,2})?|[0-9]?[0-9]|([0-9]?[0-9](\.[0-9]{1,2})))$"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_porcentaje_fondo" CssClass="text_input_nice_input" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" ID="lbl_periodo_porc_fondo" CssClass="text_input_nice_label" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                            ControlToValidate="tbx_porcentaje_fondo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="."
                                            TargetControlID="tbx_porcentaje_fondo" />
                                        <asp:RegularExpressionValidator runat="server" CssClass="textogris" Display="Dynamic"
                                            ControlToValidate="tbx_porcentaje_fondo" ErrorMessage=" Formato Incorrecto!"
                                            ValidationExpression="^(100(\.0{1,2})?|[0-9]?[0-9]|([0-9]?[0-9](\.[0-9]{1,2})))$"
                                            ValidationGroup="VAL_BENEFICIARIO" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_fecha_oficio" CssClass="text_input_nice_input"
                                        MaxLength="10" />
                                    <ajaxToolkit:MaskedEditExtender runat="server" ID="mee_fecha_oficio" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="tbx_fecha_oficio" />
                                    <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="tbx_fecha_oficio" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Fecha oficio:" />
                                        <ajaxToolkit:MaskedEditValidator runat="server" ControlExtender="mee_fecha_oficio"
                                            ControlToValidate="tbx_fecha_oficio" CssClass="textogris" ErrorMessage="mev_fecha_vencimiento"
                                            InvalidValueMessage=" Fecha Invalida!" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="tbx_fecha_vencimiento" CssClass="text_input_nice_input"
                                        MaxLength="10" />
                                    <ajaxToolkit:MaskedEditExtender runat="server" ID="mee_fecha_vencimiento" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="tbx_fecha_vencimiento" />
                                    <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="tbx_fecha_vencimiento" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Fecha vencimiento:" />
                                        <ajaxToolkit:MaskedEditValidator runat="server" ControlExtender="mee_fecha_vencimiento"
                                            ControlToValidate="tbx_fecha_vencimiento" CssClass="textogris" ErrorMessage="mev_fecha_vencimiento"
                                            InvalidValueMessage=" Fecha Invalida!" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button runat="server" ID="btn_guardar" CssClass="btn btn-primary" ValidationGroup="VAL_BENEFICIARIO" Text="Guardar" Enabled="false" />
                            &nbsp; &nbsp;
                            <asp:Button runat="server" ID="btn_digitalizar" CssClass="btn btn-primary" Text="Digitalizar Expediente" />
                        </div>
                    </div>
                </div>
            </section>
            <section class="panel" runat="server" id="panel_contacto">
                <header class="panel-heading panel_header_folder" runat="server" id="Header1">
                    <asp:Label runat="server" Text="Seguimientos" />
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <div class="module_subsec columned low_m three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_seguimiento" CssClass="btn btn-primary2 dropdown_label" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Tipo seguimiento:" />
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage=" Falta Dato!" ValidationGroup="val_segumiento"
                                            Display="Dynamic" ControlToValidate="ddl_seguimiento" CssClass="alertaValidator bold" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec no_column">
                            <div class="text_input_nice_div w_100">
                                <asp:TextBox runat="server" ID="tbx_seguimiento" CssClass="text_input_nice_input"
                                    TextMode="MultiLine" MaxLength="2000" Rows="5" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Texto:" />
                                    <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                        ControlToValidate="tbx_seguimiento" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_segumiento" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_seguimiento"
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                        ValidChars=" ,ÁÉÍÓÚÑáéíóúñ./-" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus_segumiento" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button runat="server" ID="btn_guardar_segumiento" CssClass="btn btn-primary" ValidationGroup="val_segumiento" Text="Guardar" Enabled="false" />
                        </div>
                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView runat="server" ID="gvw_segumientos" AutoGenerateColumns="false" CssClass="table table-striped" GridLines="None"
                                    AllowSorting="true" AllowPaging="true" HeaderStyle-HorizontalAlign="Center">
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundField DataField="PERIODO" HeaderText="Periodo" ItemStyle-Width="10%" ItemStyle-Wrap="true" />
                                        <asp:BoundField DataField="FECHA_SISTEMA" HeaderText="Fecha Sistema" ItemStyle-Width="10%" ItemStyle-Wrap="true" />
                                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" ItemStyle-Width="10%" ItemStyle-Wrap="true" />
                                        <asp:BoundField DataField="CREADOX" HeaderText="Capturista" ItemStyle-Width="10%" ItemStyle-Wrap="true" />
                                        <asp:BoundField DataField="TEXTO" HeaderText="Descripción" ItemStyle-Width="60%" ItemStyle-Wrap="true" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        
                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_buscar" />
            <asp:AsyncPostBackTrigger ControlID="btn_buscar_agremiado" />
            <asp:AsyncPostBackTrigger ControlID="btn_buscar_cp" />
            <asp:AsyncPostBackTrigger ControlID="btn_guardar" />
            <asp:AsyncPostBackTrigger ControlID="btn_digitalizar" />
            <asp:AsyncPostBackTrigger ControlID="btn_guardar_segumiento" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
