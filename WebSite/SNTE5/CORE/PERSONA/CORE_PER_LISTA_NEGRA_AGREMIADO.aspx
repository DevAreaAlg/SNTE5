<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_LISTA_NEGRA_AGREMIADO.aspx.vb" Inherits="SNTE5.CORE_PER_LISTA_NEGRA_AGREMIADO" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <section class="panel">
        <header class="panel-heading">
            <asp:Label runat="server" ID="lbl_titulo_bl" />
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <section runat="server" id="stn_buscar_agremiado">
                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="tbx_rfc" CssClass="text_input_nice_input" MaxLength="13" />
                            <asp:TextBox runat="server" ID="tbx_id_persona" CssClass="text_input_nice_input" MaxLength="9"
                                Visible="false" Enabled="false" Text="0" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_rfc" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="tbx_rfc" />
                            </div>
                        </div>
                        <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                            <asp:Button runat="server" ID="btn_buscar" Text="Buscar RFC" ValidationGroup="val_rfc"
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
                <div class="module_subsec no_column">
                    <div class="text_input_nice_div w_100">
                        <asp:TextBox runat="server" ID="tbx_notas" CssClass="text_input_nice_input"
                            TextMode="MultiLine" MaxLength="2000" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Notas:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_notas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_agremiado" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_notas"
                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,ÁÉÍÓÚÑáéíóúñ./" />
                        </div>
                    </div>
                </div>
                <asp:Label runat="server" ID="lbl_estatus_agremiado" CssClass="module_subsec flex_center alerta" />
                <div class="module_subsec flex_end">
                    <asp:Button runat="server" ID="btn_guardar" CssClass="btn btn-primary" ValidationGroup="val_agremiado" Text="Guardar" />
                     &nbsp; &nbsp; &nbsp;
                    <asp:Button runat="server" ID="btn_desbloquear" CssClass="btn btn-primary" Enabled="false" Text="Desbloquear" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>
