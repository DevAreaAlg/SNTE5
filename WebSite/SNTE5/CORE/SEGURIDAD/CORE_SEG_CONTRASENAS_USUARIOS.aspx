<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_SEG_CONTRASENAS_USUARIOS.aspx.vb" Inherits="SNTE5.CORE_SEG_CONTRASENAS_USUARIOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel" id="panel_cambiocontra">
        <header class="panel-heading">
            <span>Cambio de contraseña</span>

        </header>
        <div class="panel-body">

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_usuarios2" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Seleccione usuario:</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec low_m columned two_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">

                                <%--<h4><span class="text_input_nice_label">La nueva contraseña debe cumplir las siguientes características:</span></h4>--%>
                                <ul class="texto">
                                    <li><span class="text_input_nice_label">- Debe contener al menos un número.</span></li>
                                    <li><span class="text_input_nice_label">- Debe mezclar mayúsculas y minúsculas</span></li>
                                    <li><span class="text_input_nice_label">- Al menos 8 caracteres </span></li>
                                </ul>
                                <h5 class="module_subsec_elements  no_tbm">La nueva contraseña debe cumplir las siguientes características:</h5>

                            </div>
                        </div>
                    </div>


                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_pwdn1" runat="server" class="text_input_nice_input" MaxLength="15" TextMode="Password"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Nueva contraseña: </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_pwdn1" runat="server" ControlToValidate="txt_pwdn1"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_contrasena"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:PasswordStrength ID="txt_pwdn1_PasswordStrength" runat="server" Enabled="True"
                                        TargetControlID="txt_pwdn1" PrefixText="Fortaleza: " TextCssClass="TextIndicator_TextBox1"
                                        MinimumUpperCaseCharacters="1" TextStrengthDescriptions="No cumple;Muy bajo;Bajo;Promedio;Buena;Muy buena;Excelente"
                                        PreferredPasswordLength="8" MinimumNumericCharacters="1" RequiresUpperAndLowerCaseCharacters="True" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_pwdn2" runat="server" class="text_input_nice_input" MaxLength="15" TextMode="Password"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Repita nueva contraseña: </span>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator_pwdn2" runat="server" ControlToValidate="txt_pwdn2"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_contrasena"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_Results" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary" Text="Cambiar" Enabled="false" ToolTip="Presione si realmente desea cambiar la contraseña" ValidationGroup="val_contrasena" />&nbsp;&nbsp;&nbsp;
                         <asp:LinkButton ID="lnk_generar" runat="server" class="btn btn-primary" Enabled="False">Generar Aleatoria</asp:LinkButton>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Desbloqueo de cuentas</span>

        </header>
        <div class="panel-body">
            <%-- <asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>

            <div class="module_subsec no_m">
                <div class="module_subsec columned low_m no_rm no_column" style="flex: 1;">
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div">
                            <asp:DropDownList ID="cmb_usuariobloq" runat="server" CssClass="btn btn-primary2 dropdown_label">
                            </asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="texto" Text="Seleccione usuario:"></asp:Label>
                        </div>
                    </div>
                    <div class=" module_subsec_elements">
                        <asp:Button ID="btn_desbloquear" runat="server" Text="Desbloquear" class="btn btn-primary" ToolTip="Presione si realmente desea cambiar la contraseña" />
                    </div>
                </div>
            </div>
            <div align="center">
                <asp:Label ID="lbl_statbloq" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <%--                            </ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Desbloqueo de sesión</span>
                    
        </header>
        <div class="panel-body">

                        <div class="module_subsec no_m">
                            <div class="module_subsec columned low_m no_rm no_column" style="flex: 1;">
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div">
                                        <asp:DropDownList ID="cmb_usuarioSes" runat="server" CssClass="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <asp:Label ID="Lbl_usu" runat="server" CssClass="texto" Text="Seleccione usuario:"></asp:Label>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <asp:Button ID="BtnDesBloqueSesion" runat="server" Text="Desbloquear" class="btn btn-primary" ToolTip="Presione si realmente desea cambiar la contraseña" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="Lbl_staSesion" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    
        </div>
    </section>


</asp:Content>
