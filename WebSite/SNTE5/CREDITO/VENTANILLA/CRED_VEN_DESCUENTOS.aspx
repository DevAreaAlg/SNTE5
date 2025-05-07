<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_VEN_DESCUENTOS.aspx.vb" Inherits="SNTE5.CRED_VEN_DESCUENTOS" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Pólizas contables</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec low_m columned four_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="tbx_numPoliza" runat="server" CssClass="text_input_nice_input" MaxLength="2" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Número de Póliza:" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                            ControlToValidate="tbx_numPoliza" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_Poliza"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_numPoliza"
                                            FilterType="Numbers" Enabled="True" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Quincenas" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" Enabled="true" ValidationGroup="val_cliente"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Fecha de Movimiento:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="alertaValidator"
                                            ControlToValidate="cmb_Quincenas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:Button ID="btn_GetPolizaDescuentos" runat="server" class="btn btn-primary" Text="Descargar" ToolTip="Descargar póliza contable" Visible="false" ValidationGroup="val_Poliza" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                    </ContentTemplate>

                    <Triggers>
                        <asp:PostBackTrigger ControlID="cmb_Quincenas" />
                        <asp:PostBackTrigger ControlID="btn_GetPolizaDescuentos" />
                    </Triggers>

                </asp:UpdatePanel>
            </div>
        </div>
    </section>

</asp:Content>
