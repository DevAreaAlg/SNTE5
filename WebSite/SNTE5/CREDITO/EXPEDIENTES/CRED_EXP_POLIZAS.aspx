<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_POLIZAS.aspx.vb" Inherits="SNTE5.CRED_EXP_POLIZAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Pólizas Contables</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec low_m columned four_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_tipo_poliza" runat="server" CssClass="btn btn-primary2 dropdown_label"
                                        AutoPostBack="true" ValidationGroup="val_poliza">
                                        <asp:ListItem Value="D">Diaria</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Tipo de Póliza: " />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" ControlToValidate="ddl_tipo_poliza"
                                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_poliza" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_entidades" runat="server" CssClass="btn btn-primary2 dropdown_label"
                                        AutoPostBack="true" ValidationGroup="val_poliza" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Entidad: " />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" ControlToValidate="ddl_entidades"
                                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_poliza" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fecha_mov" runat="server" CssClass="text_input_nice_input"
                                        MaxLength="10" ValidationGroup="val_poliza" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Fecha de Movimiento: " />
                                        <ajaxToolkit:MaskedEditExtender ID="mee_fecha_mov" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha_mov" />
                                        <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txt_fecha_mov" />
                                        <ajaxToolkit:MaskedEditValidator ID="mev_fecha_mov" runat="server" ControlExtender="mee_fecha_mov"
                                            ControlToValidate="txt_fecha_mov" CssClass="alertaValidator" ErrorMessage="mev_fecha_mov"
                                            Display="Dynamic" InvalidValueMessage="Fecha Inválida!" ValidationGroup="val_poliza" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_fecha_mov"
                                            CssClass="alertaValidator" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_poliza" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_no_cheque" runat="server" CssClass="text_input_nice_input"
                                        MaxLength="5" ValidationGroup="val_poliza" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*No. Cheque: " />
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_no_cheque" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_no_cheque"
                                            CssClass="alertaValidator" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_poliza" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_poliza" runat="server" CssClass="btn btn-primary" Text="Descargar" ToolTip="Descargar"
                                ValidationGroup="val_poliza" />
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_poliza" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>

