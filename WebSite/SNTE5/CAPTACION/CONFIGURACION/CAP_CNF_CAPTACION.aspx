<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_CNF_CAPTACION.aspx.vb" Inherits="SNTE5.CAP_CNF_CAPTACION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Datos</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_ISRVISTA" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Tasa ISR(%):</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_ISRVISTA" runat="server"
                            ControlToValidate="txt_ISRVISTA" CssClass="textogris"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cnfcap">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ISRVISTA" runat="server"
                            TargetControlID="txt_ISRVISTA" Enabled="True" FilterType="Numbers, Custom" ValidChars="." />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_DIASEXP" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Días expiración tasa especial inversión:</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DIASEXP" runat="server"
                            ControlToValidate="txt_DIASEXP" CssClass="textogris"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cnfcap">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DIASEXP" runat="server"
                            TargetControlID="txt_DIASEXP" FilterType="Numbers" Enabled="True" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_TASAINFL" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Tasa de inflación:</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TASAINFL" runat="server"
                            ControlToValidate="txt_TASAINFL" CssClass="textogris"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cnfcap">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TASAINFL" runat="server"
                            TargetControlID="txt_TASAINFL" Enabled="True" FilterType="numbers, custom" ValidChars="." />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_TASAINFL" runat="server"
                            class="textogris" ControlToValidate="txt_TASAINFL" ErrorMessage=" Tasa incorrecta!"
                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="module_subsec flex_center">
                <asp:Label runat="server" CssClass="alerta flex_1 module_subsec low_m flex_center" ID="lbl_Alerta"></asp:Label>
            </div>
            <div class="module_subsec flex_end align_items_flex_center low_m">
                <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_cnfcap" />
            </div>

        </div>
    </section>

</asp:Content>

