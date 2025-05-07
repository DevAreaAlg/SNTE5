<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_SISTEMA.aspx.vb" Inherits="SNTE5.CORE_CNF_SISTEMA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel" id="panel_datos">
        <header class="panel-heading">
            <span>Datos</span>
        </header>
        <div class="panel-body">

            <asp:UpdatePanel ID="UpdatePanelDatos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_correo_servidor" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Servidor de correo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_correo_servidor" runat="server" ControlToValidate="txt_correo_servidor"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_correo_servidor" runat="server" Enabled="True"
                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,@,.,-,/,:" TargetControlID="txt_correo_servidor">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_usuario_correo" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Usuario de correo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_usuario_correo" runat="server" ControlToValidate="txt_usuario_correo"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_usuario_correo" runat="server" Enabled="True"
                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,@,.,-,/,:" TargetControlID="txt_usuario_correo">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_psw_correo" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Contraseña de correo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_psw_correo"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,@,.,-,/,:,#$" TargetControlID="txt_psw_correo">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_puerto" CssClass="text_input_nice_input" runat="server" MaxLength="9"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Puerto de correo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_usuario" runat="server" ControlToValidate="txt_puerto"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_puerto">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_from_correo" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Cuenta de correo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_from_correo"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,@,.,-,/,:" TargetControlID="txt_from_correo">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_ssl" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Correo SSL:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="req_politica" CssClass="alertaValidator"
                                        ControlToValidate="cmb_ssl" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_sistema" InitialValue="-1" />
                                </div>
                            </div>                           
                        </div>                       

                    </div>



                    <div class="module_subsec low_m columned three_columns">

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_ruta_app" class="text_input_nice_input" runat="server" MaxLength="300"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Ruta de aplicación:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ruta_app"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,@,.,-,/,:,\" TargetControlID="txt_ruta_app">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>


                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tam_dig" class="text_input_nice_input" runat="server" MaxLength="9"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Tamaño de digitalización (Kb):</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_tam_dig"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_sistema">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_tam_dig">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                         <div class="module_subsec_elements">
                          <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_envio" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Enviar Correo:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" CssClass="alertaValidator"
                                        ControlToValidate="cmb_envio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_sistema" InitialValue="-1" />
                                </div>
                            </div>
                          </div>
                    </div>
                    
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_guardado" runat="server" class="alerta flex_1 module_subsec low_m flex_center"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end low_m align_items_flex_center">

                        <asp:Button ID="btn_guardar1" class="btn btn-primary" runat="server" OnClick="btn_guardar1_Click" ValidationGroup="val_sistema" Text="Guardar" />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar1" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </section>

</asp:Content>
