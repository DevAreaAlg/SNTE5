<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_SEG_CONTRASENA.aspx.vb" Inherits="SNTE5.CORE_SEG_CONTRASENA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
            <section class="panel">
                <header class="panel-heading">
                    Cambio contraseña, pregunta y respuesta secreta
                </header>

                <div class="panel-body">

                        <asp:Panel ID="pnl_pswd" runat="server" Visible="true" Width="100%">
                            
                            <div class="module_subsec columned align_items_flex_center vertical">

                                <div class="module_subsec_elements module_subsec_big-elements vertical">
                                    <asp:Label ID="lbl_subtitulo" runat="server" class=" text-info">Su contraseña debe cumplir con los siguientes requerimientos:</asp:Label>
                                    <ul class="texto">
                                        <li>Debe contener números. </li>
                                        <li>Debe contener letras en mayúsculas y minúsculas. </li>
                                        <li>Longitud mínima de 8 caracteres. </li>
                                    </ul>
                                </div>

                                <div class="module_subsec_elements module_subsec_big-elements text_input_nice_div">
                                    <asp:TextBox ID="txt_antpwd" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label flex_start">*Contraseña actual: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_contraActual" runat="server"
                                            ControlToValidate="txt_antpwd" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_cambioPass"></asp:RequiredFieldValidator>
                                    </div>
                                </div>


                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:TextBox ID="txt_pwdn1" runat="server" MaxLength="15" class="text_input_nice_input" TextMode="Password"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Nueva contraseña: </span>
                                        <ajaxToolkit:PasswordStrength ID="txt_pwdn1_PasswordStrength" runat="server" Enabled="True"
                                            TargetControlID="txt_pwdn1" StrengthIndicatorType="Text" DisplayPosition="BelowRight"
                                            PrefixText="Fortaleza: " TextCssClass="TextIndicator_TextBox1" MinimumUpperCaseCharacters="1"
                                            TextStrengthDescriptions="No cumple;Muy bajo;Bajo;Promedio;Buena;Muy buena;Excelente"
                                            PreferredPasswordLength="8" MinimumNumericCharacters="1" RequiresUpperAndLowerCaseCharacters="true" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator_contra1" runat="server"
                                            ControlToValidate="txt_pwdn1" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_cambioPass"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:TextBox ID="txt_pwdn2" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input"></asp:TextBox>

                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Repita contraseña: </span>

                                        <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" Enabled="True"
                                            TargetControlID="txt_pwdn2" StrengthIndicatorType="Text"
                                            PrefixText="Fortaleza: " TextCssClass="TextIndicator_TextBox1" MinimumUpperCaseCharacters="1"
                                            TextStrengthDescriptions="No cumple;Muy bajo;Bajo;Promedio;Buena;Muy buena;Excelente"
                                            PreferredPasswordLength="8" MinimumNumericCharacters="1" RequiresUpperAndLowerCaseCharacters="true" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator_contra2" runat="server"
                                            ControlToValidate="txt_pwdn2" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_cambioPass"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                                    <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary module_subsec_elements" Text="Guardar" ValidationGroup="val_cambioPass" />
                                    <asp:LinkButton runat="server" Text="Cambiar Respuesta Secreta" ID="lnk_respuesta" class="textogris"></asp:LinkButton>
                                </div>

                                <div class="module_subsec columned align_items_flex_center">
                                    <asp:Label ID="lbl_Results" runat="server" CssClass="alerta"></asp:Label>
                                </div>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnl_secreta" runat="server" Visible="False" Width="100%">
                          
                            <asp:Label ID="Label1" runat="server" class="subtitulos"></asp:Label>
                            <div class="module_subsec columned align_items_flex_center vertical">
                                <asp:LinkButton ID="lnk_regresar_respuesta" runat="server" CssClass="textogris" Text="Regresar a Cambio Contraseña"></asp:LinkButton>

                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:Label ID="lbl_descripcion" runat="server" CssClass="texto">Nota: Recuerde que el uso de pregunta y respuesta secreta es para ayudarle a recuperar su contraseña del sistema en caso de olvido. </asp:Label>
                                </div>





                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:DropDownList ID="cmb_pregunta" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Pregunta secreta: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_pregunta" runat="server" ControlToValidate="cmb_pregunta"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                            ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                    </div>
                                </div>



                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:TextBox ID="txt_respuesta" runat="server" class="text_input_nice_input" MaxLength="30"
                                        ValidationGroup="val_respuesta" TextMode="Password" Width="100%"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Respuesta secreta: </span>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_respuesta" runat="server"
                                            ControlToValidate="txt_respuesta" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_respuesta" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers"
                                            TargetControlID="txt_respuesta">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>



                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:TextBox ID="txt_r2" runat="server" class="text_input_nice_input w_100" MaxLength="30"
                                        TextMode="Password"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Repita respuesta secreta:</span>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_r2" runat="server" ControlToValidate="txt_r2"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_r2" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers"
                                            TargetControlID="txt_r2">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>





                                <div class="text_input_nice_div module_subsec_elements module_subsec_big-elements">
                                    <asp:TextBox ID="txt_antpwd0" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input w_100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Contraseña actual:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_antpwd0" runat="server" ControlToValidate="txt_antpwd0"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                                    <asp:Button ID="BTN_guardar_secreta" runat="server" ValidationGroup="val_respuesta" class="btn btn-primary module_subsec_elements" Text="Guardar" Enabled="true" />


                                    <asp:Button ID="BTN_cancelar" runat="server" class="btn btn-primary module_subsec_elements" Enabled="true" Text="Cancelar" />
                                </div>
                                <div class="text_input_nice_div module_subsec align_items_flex_center">
                                    <asp:Label ID="lbl_Resultado" runat="server" CssClass="alerta"></asp:Label>
                                    <%--  --%>
                                </div>

                            </div>
                            
                        </asp:Panel>              
                    
                </div>
            </section>        
   
</asp:Content>

