<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_CREDITO.aspx.vb" Inherits="SNTE5.CRED_CNF_CREDITO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Datos</span>
        </header>
        <div class="panel-body">

            <div class="module_sec low_m">
                <div class="module_subsec no_column">
                    <span class="text_input_nice_label title_tag">Activa EPRC:</span>
                    <asp:CheckBox ID="checkbox_activo" CssClass="mod_check" runat="server" />
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_LIMINFBURO" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite inferior buro:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LIMINFBURO" runat="server"
                            TargetControlID="txt_LIMINFBURO" FilterType="Numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_LIMINFBURO" CssClass="textogris"
                            ControlToValidate="txt_LIMINFBURO" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_credito" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_LIMSUPBURO" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite superior buro:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LIMSUPBURO" runat="server"
                            TargetControlID="txt_LIMSUPBURO" FilterType="Numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_LIMSUPBURO" CssClass="textogris"
                            ControlToValidate="txt_LIMSUPBURO" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_credito" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_SCOREMINBURO" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>

                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Score mínimo buro:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_SCOREMINBURO" runat="server"
                            TargetControlID="txt_SCOREMINBURO" FilterType="Numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="Req_SCOREMINBURO" CssClass="textogris" ControlToValidate="txt_SCOREMINBURO"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_credito" />
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_TASAMAX" class="text_input_nice_input" runat="server" MaxLength="3" Enabled="false"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Tasa máxima (CAT %):</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TASAMAX" runat="server"
                            TargetControlID="txt_TASAMAX" FilterType="numbers, custom" ValidChars="." />
                        <asp:RequiredFieldValidator runat="server" ID="Req_TASAMAX" CssClass="textogris" ControlToValidate="txt_TASAMAX"
                            Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_credito" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_TASAMAX" runat="server"
                            class="textogris" ControlToValidate="txt_TASAMAX" ErrorMessage=" Tasa incorrecta!"
                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_RANPROMPAG" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Rango promesas de pago:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RANPROMPAG" runat="server"
                            TargetControlID="txt_RANPROMPAG" FilterType="numbers" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_RANPROMPAG" CssClass="textogris"
                            ControlToValidate="txt_RANPROMPAG" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_credito" />
                    </div>
                </div>
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_COGAIN" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Cobertura garantía inversión:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_COGAIN" runat="server"
                            TargetControlID="txt_COGAIN" FilterType="numbers, custom" ValidChars="." />
                        <asp:RequiredFieldValidator runat="server" ID="Req_COGAIN" CssClass="textogris"
                            ControlToValidate="txt_COGAIN" Display="Dynamic" ErrorMessage="Falta Dato!"  ValidationGroup="val_credito"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_COGAIN" runat="server"
                            class="textogris" ControlToValidate="txt_COGAIN" ErrorMessage=" Valor incorrecto!"
                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_Alerta" runat="server" CssClass="alerta module_subsec low_m flex_1 flex_center"></asp:Label>

            </div>
            <div class="module_subsec flex_end">

                <asp:Button ID="btn_Guardar" class="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_credito"/>
            </div>
        </div>
    </section>

</asp:Content>
