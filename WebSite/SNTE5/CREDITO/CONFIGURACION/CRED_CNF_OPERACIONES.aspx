<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_OPERACIONES.aspx.vb" Inherits="SNTE5.CRED_CNF_OPERACIONES" MaintainScrollPositionOnPostback ="true" %>

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
                    <asp:TextBox ID="txt_MINPAGSUC" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Mínimo pagaré sucursal:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MINPAGSUC" runat="server"
                            TargetControlID="txt_MINPAGSUC" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_MINPAGSUC" CssClass="textogris" ControlToValidate="txt_MINPAGSUC"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_DIASEXP" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Días expiración de expediente:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DIASEXP" runat="server"
                            TargetControlID="txt_DIASEXP" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_DIASEXP" CssClass="textogris"
                            ControlToValidate="txt_DIASEXP" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:DropDownList ID="cmb_AlertaPLD" CssClass="btn btn-primary2 dropdown_label" runat="server">
                         <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                        <asp:ListItem Value="DIARIO">DIARIO</asp:ListItem>
                        <asp:ListItem Value="SEMANAL">SEMANAL</asp:ListItem>
                    </asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Periodicidad alertas PLD:</span>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ALERTAPLD" CssClass="alertaValidator"
                                    ControlToValidate="cmb_AlertaPLD" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_operaciones" InitialValue="-1" />
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:DropDownList ID="cmb_inhabil" CssClass="btn btn-primary2 dropdown_label" runat="server">
                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                    </asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Días inhabiles por semana:</span>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_INHABIL" CssClass="alertaValidator"
                                    ControlToValidate="cmb_inhabil" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_operaciones" InitialValue="-1" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_DIASASUESUC" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Días asueto consecutivos:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DIASASUESUC" runat="server"
                            TargetControlID="txt_DIASASUESUC" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_DIASASUESUC" CssClass="textogris" ControlToValidate="txt_DIASASUESUC"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_CERTI" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Certificado:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_CERTI" runat="server"
                            TargetControlID="txt_CERTI" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_CERTI" CssClass="textogris" ControlToValidate="txt_CERTI"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_KEY" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Clave:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_KEY" runat="server"
                            TargetControlID="txt_KEY" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_KEY" CssClass="textogris" ControlToValidate="txt_KEY"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_RUTATIM" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Ruta de timbrado:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RUTATIM" runat="server"
                            TargetControlID="txt_RUTATIM" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_RUTATIM" CssClass="textogris" ControlToValidate="txt_RUTATIM"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_operaciones" />
                    </div>
                </div>

            </div>
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_Alerta" runat="server" CssClass="alerta module_subsec flex_1 low_m flex_center"></asp:Label>
            </div>
            <div class="module_subsec flex_end low_m align_items_flex_center">
                <asp:Button ID="btn_Guardar" class="btn btn-primary" runat="server" ValidationGroup="val_operaciones" Text="Guardar" />
            </div>

        </div>
    </section>

</asp:Content>
