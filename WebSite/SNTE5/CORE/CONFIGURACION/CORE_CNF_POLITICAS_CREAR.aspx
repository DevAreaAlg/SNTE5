<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_POLITICAS_CREAR.aspx.vb" Inherits="SNTE5.CORE_CNF_POLITICAS_CREAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            DATOS
        </header>

        <div class="panel-body">

            <div class="module_subsec no_column align_items_flex_center">
                <span class="text_input_nice_label">Número de política:</span>
                <div class="module_subsec_elements" style="flex: 1;">
                    <asp:TextBox ID="txt_id_Politica" runat="server" class="text_input_nice_input" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span>Datos</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <div class="module_sec low_m">
                    <div class="module_subsec no_column">
                        <span class="text_input_nice_label title_tag">Activo:</span>
                        <asp:CheckBox ID="ckb_Activo" CssClass="mod_check" runat="server" />
                    </div>
                </div>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_nombre_politica" runat="server" class="text_input_nice_input" MaxLength="30"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Nombre:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre_politica"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters, Custom" TargetControlID="txt_nombre_politica" ValidChars=" ,ñ,Ñ">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_tiempo_inactividad" runat="server" class="text_input_nice_input" MaxLength="2"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Tiempo de inactividad (Minutos):</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_inactividad" runat="server" ControlToValidate="txt_tiempo_inactividad"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_inactividad" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_tiempo_inactividad">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_intentos_fallidos" runat="server" class="text_input_nice_input" MaxLength="2"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Intentos fallidos de contraseña:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fallidos" runat="server" ControlToValidate="txt_intentos_fallidos"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_fallidos" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_intentos_fallidos">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec flex_start">
                    <span class="text_input_nice_label">Formato de hora hh:mm ó hh:mmAM/PM 24 horas, ejemplo: 07:00 ó 07:00PM.</span>
                </div>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="timepicker1" runat="server" class="text_input_nice_input " name="timepicker1"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Hora inicial de acceso:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechaini" runat="server" ControlToValidate="timepicker1"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_fechaini" runat="server" Enabled="True"
                                    FilterType="Custom, Numbers " ValidChars=":,P,M,A,' '" TargetControlID="timepicker1">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="timepicker2" runat="server" class="text_input_nice_input " name="timepicker2"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Hora final de acceso:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafin" runat="server" ControlToValidate="timepicker2"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_fechafin" runat="server" Enabled="True"
                                    FilterType="Custom, Numbers" ValidChars=":,P,M,A" TargetControlID="timepicker2">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_dias_expiracion" runat="server" class="text_input_nice_input" MaxLength="2"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Días de expiración de contraseña:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_expira" runat="server" ControlToValidate="txt_dias_expiracion"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_expira" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_dias_expiracion">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_mem_contrasena" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Memoria de contraseña:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_memcontra" runat="server" ControlToValidate="txt_mem_contrasena"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_memcontra" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_mem_contrasena">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_tpo_bloqueo" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Tiempo bloqueo:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_tpo_bloqueo" runat="server" ControlToValidate="txt_tpo_bloqueo"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tpo_bloqueo" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_tpo_bloqueo">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <%--<asp:CompareValidator ID="CompareValidator_Min_Bloqueo" runat="server" Display="Dynamic" ControlToCompare="txt_tiempo_inactividad" ControlToValidate="txt_tpo_bloqueo"
                                    ErrorMessage="Min. Bloqueo menor o igual a Tiempo Inactividad" Type="Integer" Operator="GreaterThan" CssClass="alertaValidator text_input_nice_error" />--%>
                            </div>
                        </div>
                    </div>
                </div>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statupol" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                    
                        <button runat="server" class="btn btn-primary" type="button" id="btn_guardar_politica" onserverclick="click_btn_guardar_datos" validationgroup="val_Politica">Guardar</button>
                    
                </div>

                
            </div>
        </div>
    </section>

    <script>        $('#<% = timepicker1.ClientID%>').timepicki();</script>
    <script>        $('#<% = timepicker2.ClientID%>').timepicki();</script>
</asp:Content>

