<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_REEST_PRES.aspx.vb" Inherits="SNTE5.CRED_VEN_REEST_PRES" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
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

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Reestructurar Préstamo</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
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
                        <asp:Button runat="server" ID="btn_buscar_agremiado" Text="Buscar Agremiado"
                            CssClass="btn btn-primary module_subsec_elements no_tbm" />
                        <asp:Button runat="server" ID="btn_buscar_prestamo" Text="Buscar Préstamo" ValidationGroup="val_rfc"
                            CssClass="btn btn-primary module_subsec_elements no_tbm " />
                    </div>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
                </div>
                <div class="module_subsec columned low_m three_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_agremiado" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Agremiado:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_folio" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <asp:TextBox runat="server" ID="tbx_id_folio" CssClass="text_input_nice_input"
                                Enabled="false" Visible="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Folio:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_fecha_pago" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Fecha Pago:" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec columned low_m three_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_monto" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Monto Préstamo:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_intereses" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Intereses Préstamo:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_total" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Total Préstamo:" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec columned low_m three_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_monto_insoluto" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Monto Restante:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_intereses_insoluto" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Intereses Restantes:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_total_insoluto" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Total Restante:" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec columned low_m three_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_tasa" CssClass="text_input_nice_input" MaxLength="10"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Tasa Préstamo:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="tbx_tasa" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_prestamo" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                    FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbx_tasa" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="tbx_tasa"
                                    CssClass="textogris" ErrorMessage=" Tasa Incorrecta!" Display="Dynamic"
                                    ValidationExpression="^[0-9]{1,3}(\.[0-9]{1}[0-9]?)?$" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_plazo" CssClass="text_input_nice_input" MaxLength="2"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="lbl_plazo" CssClass="text_input_nice_label" Text="Plazo Préstamo:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="tbx_plazo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_prestamo" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="tbx_plazo" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="tbx_descuento" CssClass="text_input_nice_input"
                                Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Descuento:" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button runat="server" ID="btn_reestructurar" CssClass="btn btn-primary" Text="Reestructurar Préstamo"
                        ValidationGroup="val_prestamo" Enabled="false" />
                </div>
                <div class="module_subsec overflow_x shadow">
                    <div class="flex_1">
                        <asp:DataGrid runat="server" ID="dgd_descuentos" AutoGenerateColumns="False"
                            CssClass="table table-striped" GridLines="None">
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="ANIO" HeaderText="Año" />
                                <asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena" />
                                <asp:BoundColumn DataField="CAPITAL" HeaderText="Capital" />
                                <asp:BoundColumn DataField="INTERES" HeaderText="Interés" />
                                <asp:BoundColumn DataField="DESCUENTO" HeaderText="Descuento" />
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
