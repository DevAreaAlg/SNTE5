<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PERSONAEXT.aspx.vb" Inherits="SNTE5.CORE_PER_PERSONAEXT" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>


    <asp:UpdatePanel ID="up_semaforos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="semaforo_barra">
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Generales" ID="lnk_to_generales">

                    <span id="Semaforo1_r" class="semaforo_img alto" runat="server" visible="true" />
                    <span id="Semaforo1_v" class="semaforo_img prosiga" runat="server" visible="false" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Adicionales" ID="lnk_to_adicionales">
                    <span id="Semaforo2_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo2_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Dependientes" ID="lnk_to_dependientes">
                    <span id="Semaforo3_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo3_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Domicilio" ID="lnk_to_domicilio">
                    <span id="Semaforo4_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo4_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Contacto" ID="lnk_to_contacto">
                    <span id="Semaforo5_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo5_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Laborales" ID="lnk_to_laborales">
                    <span id="Semaforo6_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo6_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <section class="panel">
        <header class="panel-heading">
            Id Persona
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:UpdatePanel ID="upd_id" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_id_persona" runat="server" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <!-----------------------------
                    PANEL GENERALES
            --------------------------------->

    <asp:UpdatePanel runat="server" ID="upnl_generales" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="panel" runat="server" id="panel_generales">
                <header runat="server" id="head_panel_generales" class="panel_header_folder panel-heading up_folder">
                    <span class="panel_folder_toogle_header">Generales</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_generales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_generales">


                        <div class="module_subsec low_m columned three_columns">
                            
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList  visible="false" ID="cmb_estatus" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False" ToolTip="*Estatus del afiliado">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <%--<span class="text_input_nice_label" visible="false">*Estatus del afiliado</span>--%>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--NOMBRES-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre1" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Primer nombre</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre1" runat="server" ControlToValidate="txt_nombre1"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre1" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre1">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre2" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Segundo(s) nombre(s)</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre2" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre2">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_paterno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Apellido paterno</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_paterno" runat="server" ControlToValidate="txt_paterno"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_paterno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--APELLIDOS-->
                        <div class="module_subsec low_m columned three_columns">

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_materno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Apellido materno</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_materno" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_materno">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <!--CURP Y RFC-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_curp" class="text_input_nice_input" runat="server" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*CURP</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_curp" runat="server" ControlToValidate="txt_curp"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_curp" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_curp">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_curp" runat="server" class="textogris"
                                            ControlToValidate="txt_curp" ErrorMessage="Error: formato CURP  incorrecto." Display="Dynamic"
                                            ValidationExpression="^[a-zA-Z]{3,4}(\d{6})[hmHM]{1}[a-zA-Z]{4,5}[0-9A-Z]{1}[0-9A-Z]{1}" ValidationGroup="val_Persona" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_rfc" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*RFC</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_rfc" runat="server" ControlToValidate="txt_rfc"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfc" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_rfc">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_id_oficial" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Identificación oficial</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_id_oficial"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_id_oficial" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="-" TargetControlID="txt_id_oficial">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--PAIS DE NACIMIENTO-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_pais_nac" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*País de nacimiento</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_paisnac" CssClass="alertaValidator"
                                            ControlToValidate="cmb_pais_nac" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_nacionalidad" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Nacionalidad</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_nac" CssClass="alertaValidator"
                                            ControlToValidate="cmb_nacionalidad" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--FECHA DE NACIMIENTO Y SEXO-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fecha" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Fecha de nacimiento (DD/MM/AAAA):</span>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txt_fecha" CssClass="textogris"
                                            ErrorMessage="MaskedEditExtender2" Display="Dynamic" InvalidValueMessage="Fecha inválida" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fecha" />
                                        <asp:RequiredFieldValidator runat="server" ID="req_fechanac" CssClass="alertaValidator bold"
                                            ControlToValidate="txt_fecha" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Persona" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="rad_sexcon1" runat="server" Checked="True" class="texto" GroupName="sexocon"
                                            Text="Hombre"></asp:RadioButton>
                                    </div>
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="rad_sexcon2" runat="server" class="texto" GroupName="sexocon"
                                            Text="Mujer"></asp:RadioButton>
                                    </div>
                                    <span class="text_input_nice_label">Sexo:</span>
                                </div>
                            </div>
                        </div>
                        
                        <!--NOTAS-->
                        <div class="module_subsec low_m">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                    <span class="text_input_nice_label">Notas:</span>
                                </div>
                            </div>
                        </div><br />

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_g" class="btn btn-primary" runat="server" OnClick="btn_guardar_g_Click" ValidationGroup="val_Persona" Text="Guardar" AutoPostBack="False" />
                        </div>

                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL ADICIONALES
            --------------------------------->
    <asp:UpdatePanel ID="upnl_adicionales" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="panel  " runat="server" id="panel_adicionales">
                <header runat="server" id="head_panel_adicionales" class="panel_header_folder panel-heading up_folder">
                    <span class="panel_folder_toogle_header">Adicionales</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_adicionales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" id="content_panel_adicionales" runat="server">

                        <!------------ECONOMICOS---------->

                        <h5 style="font-weight: normal" class="resalte_azul module_subsec">Económicos</h5>

                        <!--ACT ECONOMICA-->
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_sector" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Sector:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_sector" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_sector" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_actividad" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Actividad:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_actividad" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_actividad" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <!--GrADO ACADEMICO-->
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_grado" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                                <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="NIN">NINGUNO</asp:ListItem>
                                                <asp:ListItem Value="PRI">PRIMARIA</asp:ListItem>
                                                <asp:ListItem Value="SEC">SECUNDARIA</asp:ListItem>
                                                <asp:ListItem Value="BAC">BACHILLERATO</asp:ListItem>
                                                <asp:ListItem Value="TEC">TECNICA/COMERCIAL</asp:ListItem>
                                                <asp:ListItem Value="LIC">LICENCIATURA</asp:ListItem>
                                                <asp:ListItem Value="ESP">ESPECIALIDAD</asp:ListItem>
                                                <asp:ListItem Value="MAE">MAESTRIA</asp:ListItem>
                                                <asp:ListItem Value="DOC">DOCTORADO</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Grado académico:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_grado" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_grado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <!--NSS Y FIEL-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nss" class="text_input_nice_input" runat="server" MaxLength="11"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Clave única de seguridad social:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="req_nss" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_nss">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fiel" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                    <span class="text_input_nice_label">Fiel:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="req_fiel" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="." TargetControlID="txt_fiel">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fecha_sat" class="text_input_nice_input" runat="server" MaxLength="10" Text=""></asp:TextBox>
                                    <span class="text_input_nice_label">Fecha alta SAT. (DD/MM/AAAA):</span>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha_sat" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txt_fecha_sat" CssClass="textogris"
                                        ErrorMessage="MaskedEditExtender1" InvalidValueMessage="Fecha inválida" Display="Dynamic" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fecha_sat" />
                                </div>
                            </div>
                        </div>

                        <!-------------CIVILES------------>

                        <h5 style="font-weight: normal" class="resalte_azul module_subsec">Civiles</h5>


                        <!--ESTADO CIVIL-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_edo_civil" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="SOL">SOLTERO(A)</asp:ListItem>
                                        <asp:ListItem Value="CAS">CASADO(A)</asp:ListItem>
                                        <asp:ListItem Value="UNL">UNION LIBRE</asp:ListItem>
                                        <asp:ListItem Value="DIV">DIVORCIADO(A)</asp:ListItem>
                                        <asp:ListItem Value="VIU">VIUDO(A)</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Estado civil:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_edo_civil" CssClass="alertaValidator"
                                            ControlToValidate="cmb_edo_civil" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_regimen" CssClass="btn btn-primary2 dropdown_label" runat="server" Visible="False">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="MAN">BIENES MANCOMUNADOS</asp:ListItem>
                                        <asp:ListItem Value="SEP">BIENES SEPARADOS</asp:ListItem>
                                        <asp:ListItem Value="OTR">OTRO</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_regimen" runat="server" CssClass="text_input_nice_label" Text="*Régimen:" Visible="False" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_regimen" CssClass="alertaValidator"
                                            ControlToValidate="cmb_regimen" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--ID DEL CONYUGE-->
                        <div class="module_subsec low_m">
                            <asp:Label ID="lbl_odatoscompletos" runat="server" CssClass="text_input_nice_label" Text="Capture la información del cónyuge "
                                Visible="False" />
                            <asp:LinkButton ID="lnk_conyuge" runat="server" CssClass="textogris" Text="aquí" Visible="False" />

                        <div class="module_subsec low_m">
                            <asp:Label ID="lbl_buscarpersonas1" runat="server" CssClass="text_input_nice_label" Text=" o realice una "
                                    Visible="False" />
                            <asp:LinkButton ID="lnk_busqueda_coyuge" runat="server" CssClass="textogris" Text="búsqueda"
                                Visible="False" CausesValidation="False" />
                          </div>

                            <!--PANEL CAPTURA CONYUGE-->
                            <asp:Panel ID="pnl_alta_mod_conyuge" runat="server" Visible="False" Style='display: none;'>
                                <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                                    <asp:Panel ID="pnl_tit_alta_mod_conyuge" runat="server" CssClass="panel">
                                        <header class="panel-heading">
                                            <asp:Label ID="lbl_tit_alta_mod_conyuge" runat="server" CssClass="module_subsec no_m flex_center" Text="Datos cónyuge" />
                                        </header>
                                        <div class="panel-body">

                                            <!--CONYUGE PRIMER NOMBRE-->
                                            <div class="module_subsec low_m columned two_columns">
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_nombrecon1" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>

                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_nombrecon1" runat="server" CssClass="text_input_nice_label" Text="*Primer nombre:"></asp:Label>

                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombrecon1" runat="server"
                                                                TargetControlID="txt_nombrecon1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True">
                                                            </ajaxToolkit:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator runat="server" ID="req_nombrecon1" CssClass="textogris"
                                                                ControlToValidate="txt_nombrecon1" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                ValidationGroup="val_conyuge" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <!--CONYUGE SEGUNDO NOMBRE-->
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_nombrecon2" runat="server" class="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                                        <asp:Label ID="lbl_nombrecon2" runat="server" CssClass="text_input_nice_label" Text="Segundo nombre:"></asp:Label>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombrecon2" runat="server"
                                                            TargetControlID="txt_nombrecon2" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True">
                                                        </ajaxToolkit:FilteredTextBoxExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <!--CONYUGE PATERNO-->
                                            <div class="module_subsec low_m columned two_columns">
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_paternocon" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>

                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_paternocon" runat="server" CssClass="text_input_nice_label" Text="*Apellido paterno:"></asp:Label>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paternocon" runat="server"
                                                                TargetControlID="txt_paternocon" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True">
                                                            </ajaxToolkit:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator runat="server" ID="req_paternocon" CssClass="textogris"
                                                                ControlToValidate="txt_paternocon" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                ValidationGroup="val_conyuge" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <!--CONYUGE MATERNO-->
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_maternocon" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                                        <asp:Label ID="lbl_maternocon" runat="server" CssClass="text_input_nice_label" Text="Apellido materno:"></asp:Label>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maternocon" runat="server"
                                                            TargetControlID="txt_maternocon" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True">
                                                        </ajaxToolkit:FilteredTextBoxExtender>

                                                    </div>
                                                </div>
                                            </div>

                                            <!--CONYUGE LUGAR DE NACIMIENTO-->
                                            <div class="module_subsec low_m columned two_columns">
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:DropDownList ID="cmb_lugarnaccon" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>

                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_lugarnaccon" runat="server" CssClass="text_input_nice_label" Text="*País de nacimiento:"></asp:Label>
                                                            <asp:RequiredFieldValidator runat="server" ID="req_lugarnaccon" CssClass="textogris"
                                                                ControlToValidate="cmb_lugarnaccon" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                ValidationGroup="val_conyuge" InitialValue="-1" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--CONYUGE FECHA DE NACIMIENTO-->
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_fechanaccon" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>

                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_fechanaccon" runat="server" CssClass="text_input_nice_label" Text="*Fecha nacimiento(DD/MM/AAAA):"></asp:Label>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechanaccon" runat="server"
                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechanaccon" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True">
                                                            </ajaxToolkit:MaskedEditExtender>

                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechanaccon" runat="server" Display="Dynamic"
                                                                ControlExtender="MaskedEditExtender_fechanaccon" ControlToValidate="txt_fechanaccon"
                                                                InvalidValueMessage="Fecha inválida" ValidationGroup="val_conyuge" CssClass="textogris"
                                                                ErrorMessage="MaskedEditValidator_fechanaccon"></ajaxToolkit:MaskedEditValidator>

                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechanaccon" runat="server" TargetControlID="txt_fechanaccon"
                                                                Format="dd/MM/yyyy" OnClientShown="onCalendarShown" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>

                                                            <asp:RequiredFieldValidator runat="server" ID="req_fechanaccon" CssClass="textogris"
                                                                ControlToValidate="txt_fechanaccon" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                ValidationGroup="val_conyuge" />

                                                            <asp:Label ID="lbl_fechacerr" runat="server" CssClass="alerta"></asp:Label>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <!--CONYUGE SEXO-->
                                            <div class="module_subsec low_m columned two_columns">
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <div class="module_subsec_elements">
                                                            <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" class="texto" GroupName="sexocon"
                                                                 Text="Hombre"></asp:RadioButton>
                                                        <asp:RadioButton ID="RadioButton2" runat="server" class="texto" GroupName="sexocon"
                                                             Text="Mujer"></asp:RadioButton>

                                                        </div>
                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_sexocon" runat="server" CssClass="text_input_nice_label" Text="*Sexo:" Width="160px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--CONYUGE CURP-->
                                            <div class="module_subsec low_m columned two_columns">
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_curpcon" runat="server" class="text_input_nice_input" MaxLength="18" ></asp:TextBox>

                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_curpcon" runat="server" CssClass="text_input_nice_label" Text="CURP:"></asp:Label>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_curpcon" runat="server"
                                                                FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_curpcon" Enabled="True">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
<%--                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_curcon" CssClass="textogris" Enabled ="False"
                                                                ControlToValidate="txt_curpcon" Display="Dynamic" ErrorMessage=" Falta Dato!"  ValidationGroup="val_conyuge" />--%>
<%--                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" class="textogris"
                                                                ControlToValidate="txt_curpcon" ErrorMessage="Error: formato CURP  incorrecto." 
                                                                ValidationExpression="^[a-zA-Z]{3,4}(\d{6})[hmHM]{1}[a-zA-Z]{4,5}[0-9A-Z]{1}[0-9A-Z]{1}" ValidationGroup="val_conyuge" />--%>
                                                            <asp:Label ID="lbl_curpwrong2" runat="server" CssClass="alerta" Text="Error: formato CURP incorrecto." Visible="False"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--CONYUGE RFC-->
                                                <div class="module_subsec_elements">
                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_rfccon" runat="server" class="text_input_nice_input" MaxLength="13"></asp:TextBox>
                                                        <div class="text_input_nice_labels">
                                                            <asp:Label ID="lbl_rfccon" runat="server" CssClass="text_input_nice_label" Text="RFC:"></asp:Label>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfccon" runat="server"
                                                                FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_rfccon"
                                                                Enabled="True">
                                                            </ajaxToolkit:FilteredTextBoxExtender>
<%--                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="textogris"
                                                                ControlToValidate="txt_rfccon" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                ValidationGroup="val_conyuge"/>--%>
                                                         <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" class="textogris" ControlToValidate="txt_rfccon" 
                                                                ErrorMessage="Error: formato RFC  incorrecto." ValidationExpression="^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$" ValidationGroup="val_conyuge" />--%>
                                                                <asp:Label ID="lbl_rfcwrong2" runat="server" CssClass="alerta" Text="Error: formato RFC incorrecto." Visible="False"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="module_subsec columned two_columns low_m flex_center">
                                                <!--CONYUGE BOTON GUARDAR-->
                                                <div class="module_subsec_elements module_subsec no_m flex_end">
                                                    <asp:Button ID="btn_guardacon" runat="server" CssClass="btn btn-primary " Text="Guardar" ValidationGroup="val_conyuge" />
                                                </div>
                                                <div class="module_subsec_elements module_subsec no_m">
                                                    <asp:Button ID="btn_cancelarcon" runat="server" CssClass="btn btn-primary"   Text="Cancelar" />
                                                </div>

                                            </div><br>
                                            <asp:Label ID="lbl_status_con" runat="server" CssClass="alerta module_subsec low_m flex_center"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>


                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_alta_mod_conyuge" runat="server" Enabled="False"
                                PopupControlID="pnl_alta_mod_conyuge" PopupDragHandleControlID="pnl_tit_alta_mod_conyuge"
                                TargetControlID="hdn_conyuge_b">
                            </ajaxToolkit:ModalPopupExtender>
                            <input type="hidden" name="hdn_conyuge_b" id="hdn_conyuge_b" runat="server" />
                        </div>

                        <br><div class="module_subsec low_m columned three_columns">
                          <div class="module_subsec low_m">
                            <asp:Label ID="lbl_datosconyugebuscar1" runat="server" CssClass="texto" Text="Nombre del conyuge:"
                                Visible="False" />
                            &nbsp;&nbsp;<asp:Label ID="lbl_datosconyugebuscar2" runat="server" CssClass="texto" Text=""
                                Visible="False" />
                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnk_editar_conyuge" runat="server" CssClass="textogris" Text=" Editar" Visible="False" />
                            </div>
                        </div>
                        <!--DEPENDIENTES-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_dependientes" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Dependientes:</span>
                                        <asp:RequiredFieldValidator ID="req_dependientes" runat="server" ControlToValidate="txt_dependientes"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaAdi">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_dependientes" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_dependientes">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--DATAGRID-->
                        <div class="overflow_x  module_subsec shadow">
                            <asp:DataGrid ID="dag_conyuge" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CON" HeaderText="ID">
                                        <ItemStyle Width="15%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOM_CON" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUM_DEP" HeaderText="Número de Dependientes">
                                        <ItemStyle Width="20%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="REG_CON" HeaderText="Régimen Conyugal">
                                        <ItemStyle Width="30%"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_adi" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">                            
                            <asp:Button ID="btn_guardar_a" class="btn btn-primary" runat="server" OnClick="btn_guardar_a_Click" ValidationGroup="val_PersonaAdi" Text="Guardar" AutoPostBack="False" />
                        </div>

                    </div>
                </div>
            </section>


        </ContentTemplate>

    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL DEPENDIENTES
            --------------------------------->
    <asp:UpdatePanel ID="upnl_dependientes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_dependientes">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_dependientes">
                    <span class="panel_folder_toogle_header">Dependientes</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_dependientes">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_dependientes">

                        <!--Depend-->
                        <div class="module_subsec">
                            <asp:Label runat="server" id="lbl_dependencia_info" class="texto">Si es dependiente económico de una o más personas ya registradas en el sistema, realice una búsqueda</asp:Label>&nbsp; &nbsp;
                            <asp:LinkButton ID="lnk_busca_dependencia" runat="server" CssClass="textogris" Text="aquí" />
                        </div>

                        <div class="module_subsec no_column">
                            <asp:Label ID="lbl_nombre_de_quien_depende" runat="server" CssClass="module_subsec_elements" />

                            <asp:Label ID="lbl_dependencia_parentesco" runat="server" CssClass="module_subsec_elements" Text="Parentesco: "
                                Visible="False" />
                            <asp:DropDownList ID="cmb_dependencia_parentesco" runat="server" class="btn btn-primary2 module_subsec_elements"
                                Visible="False" ValidationGroup="val_dependencia">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="req_dependencia_parentesco" CssClass="module_subsec_elements"
                                ControlToValidate="cmb_dependencia_parentesco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_dependencia" InitialValue="-1" Enabled="False" />

                            <asp:LinkButton ID="lnk_agregar_dependencia" runat="server" CssClass="module_subsec_elements"
                                Text="Agregar" Visible="False" ValidationGroup="val_dependencia" />
                        </div>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_dependencia" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped"
                                GridLines="None">
                                <Columns>
                                    <asp:BoundColumn DataField="IDDEPEN" HeaderText="ID">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PARENTESCO" HeaderText="Parentesco">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHACREADO" HeaderText="Fecha de alta">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                        <ItemStyle Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header" />
                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus_dependencia" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec">
                            <asp:Label runat="server" id="lbl_dependientes_info_b" class="texto">Si uno o más de sus dependientes económicos son personas ya registradas en el sistema, realice una búsqueda</asp:Label>&nbsp; &nbsp;
                            <asp:LinkButton ID="lnk_busca_dependientes" runat="server" CssClass="textogris" Text="aquí" />
                        </div>  

                        <asp:Label ID="lbl_nombre_dependiente" runat="server" CssClass="texto" />
                        &nbsp;&nbsp;
                        <asp:Label ID="lbl_dependiente_parentesco" runat="server" CssClass="texto" Text="Parentesco: "
                            Visible="False" />
                        <asp:DropDownList ID="cmb_dependiente_parentesco" runat="server" class="btn btn-primary2 dropdown_label"
                            Visible="False">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="req_dependiente_parentesco" CssClass="textogris"
                            ControlToValidate="cmb_dependiente_parentesco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_dependiente" InitialValue="-1" Enabled="False" />
                        &nbsp;
                        <asp:LinkButton ID="lnk_agregar_dependiente" runat="server" CssClass="textogris"
                            Text="Agregar" Visible="False" />

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_dependientes" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped"
                                GridLines="None">
                                <HeaderStyle CssClass="table_header" />

                                <Columns>
                                    <asp:BoundColumn DataField="IDDEPEN" HeaderText="ID">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="240" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PARENTESCO" HeaderText="Parentesco">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHACREADO" HeaderText="Fecha de alta">
                                        <ItemStyle Width="100" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                        <ItemStyle ForeColor="#054B66" Width="50" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus_dependiente" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end top_m">
                            <asp:Button ID="btn_guardar_d" class="btn btn-primary" runat="server" OnClick="btn_guardar_d_Click" Text="Guardar" AutoPostBack="False" />

                        </div>
                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL DOMICILIO
            --------------------------------->
    <asp:UpdatePanel ID="upnl_domicilio" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_domicilio">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_domicilio">
                    <span class="panel_folder_toogle_header">Domicilio</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_domicilio">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_domicilio">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                
                                <!--TIPO VIALIDAD, CALLE-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_vialidad" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tipo de vialidad:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_vialidad" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_calle" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Calle:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_calle"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars="Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ " TargetControlID="txt_calle">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <!--CP-->
                                <div class="module_subsec columned three_columns ">
                                     <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_exterior" class="text_input_nice_input" runat="server" MaxLength="4"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número exterior:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_exterior"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_exterior">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_interior" class="text_input_nice_input" runat="server" MaxLength="4"></asp:TextBox>
                                            <span class="text_input_nice_label">Número interior:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_interior">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" OnTextChanged="txt_cp_TextChanged"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Código postal:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_cp" runat="server" Enabled="True"
                                                    TargetControlID="txt_cp" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" AutoPostBack="False" />
                                    </div>
                                </div>

                                <!--LUGAR DE NACIMIENTO PAIS, ESTADO, ASENTAMIENTO-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estado" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Estado:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_estado" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_estado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Municipio:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_municipio" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_municipio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_asentamiento" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Asentamiento:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_asentamiento" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_asentamiento" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                                           
                                <!--REFERENCIAS-->
                                <div class="text_input_nice_div module_sec">
                                    <asp:TextBox ID="txt_referencias" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <span class="text_input_nice_label">Referencias:</span>
                                </div>

                                <!--TIPO VIVIENDA-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_tipoviv" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="PRO">PROPIA</asp:ListItem>
                                                <asp:ListItem Value="FAM">FAMILIAR</asp:ListItem>
                                                <asp:ListItem Value="REN">RENTADA</asp:ListItem>
                                                <asp:ListItem Value="PRE">PRESTADA</asp:ListItem>
                                                <asp:ListItem Value="HIP">HIPOTECADA</asp:ListItem>
                                                <asp:ListItem Value="PAG">PAGANDOLA</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tipo de vivienda:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_tipoviv" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_tiempo" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Años habitando:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_tiempo"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="txt_tiempo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_residentes" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                            <span class="text_input_nice_label">Total de residentes:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                FilterType="Numbers" ValidChars=" " TargetControlID="txt_residentes">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_status_dom" runat="server" CssClass="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar_domicilio" class="btn btn-primary" runat="server" ValidationGroup="val_PersonaDir" Text="Guardar" AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_domicilio" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL CONTACTO
            --------------------------------->
    <asp:UpdatePanel ID="upnl_contacto" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_contacto">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_contacto">
                    <span class="panel_folder_toogle_header">Contacto</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_contacto">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_contacto">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <!--TELEFONO-->

                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Teléfono</h5>

                                <div class="module_subsec low_m columned align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Casa:</span>
                                    <asp:TextBox ID="txt_ladacasa" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_ladacasa">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_telcasa" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_telcasa">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>

                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Celular:</span>
                                    <asp:TextBox ID="txt_ladamov" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_ladamov">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_telmov" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_telmov">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>

                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Recados: &nbsp;</span>
                                    <asp:TextBox ID="txt_ladarec" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_ladarec">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_telrec" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_telrec">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>

                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Trabajo: &nbsp; &nbsp;</span>
                                    <asp:TextBox ID="txt_ladaofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_ladaofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_telofi" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_telofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                                    <asp:TextBox ID="txt_extofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_extofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>

                                <!--CORREO-->

                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Correo</h5>

                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Personal:</span>
                                    <asp:TextBox ID="txt_correoper" class="module_subsec_elements module_subsec_bigger-elements text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" class="alertaValidator bold"
                                        ControlToValidate="txt_correoper" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                        ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                                    </asp:RegularExpressionValidator>
                                    &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Trabajo:</span>
                                    <asp:TextBox ID="txt_correoofi" class="module_subsec_elements module_subsec_bigger-elements text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" class="alertaValidator bold"
                                        ControlToValidate="txt_correoofi" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                        ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                                    </asp:RegularExpressionValidator>
                                </div>

                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_statustel" runat="server" CssClass="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar_c" class="btn btn-primary" runat="server" ValidationGroup="val_PersonaCon" Text="Guardar" AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_c" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL LABORALES
                --------------------------------->
     <asp:UpdatePanel runat="server" ID="upnl_laborales" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="panel" runat="server" id="panel_laborales">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_laborales">
                    <span class="panel_folder_toogle_header">Laborales</span>
                    <span class="panel_folder_toogle up" href="#" runat="server" id="toggle_panel_laborales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_laborales">                
                         
                        <div class="module_subsec low_m columned two_columns">
                            <div class=" module_subsec_elements">
                                <label id="lbl_titulo2" class="module_subsec">Si no es empleado de alguna empresa de click</label>
                                <asp:CheckBox ID="chk_desempleo" runat="server" AutoPostBack="True"/>
                            </div>
                        </div>

                       <!-----------------------------
                                   EMPRESA
                       ------------------------------->
                       <%--<asp:UpdatePanel runat="server" ID="pnl_empresa" UpdateMode="Conditional">
                         <ContentTemplate>--%>
                          <section class="panel" runat="server" id="panel_empresa">
                              
                            <header class="panel_header_folder panel-heading" runat="server" id="head_panel_empresa">
                                <span class="panel_folder_toogle_header">Empresa</span>
                                <span class="panel_folder_toogle up"  href="#"  runat="server" id="toggle_panel_empresa">&rsaquo;</span>
                            </header>
                            <div class="panel-body">
                             <div class="panel-body_content" runat="server" id="content_panel_empresa">
                                 <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                         <ContentTemplate>
                                  <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_empresa" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>                                          
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Empresa:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_empresa" runat="server" ControlToValidate="txt_empresa"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_empresa" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_empresa">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_cp_empresa" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Código postal:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp_empresa" runat="server" ControlToValidate="txt_cp_empresa"
                                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp_empresa" runat="server" Enabled="True"
                                                    TargetControlID="txt_cp_empresa" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <asp:ImageButton ID="btn_buscadat_empresa" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px; width: 14px;" autopostback="False"/>
                                    </div>
                                  </div>
                                  <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estado_empresa" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Estado:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_estado_empresa" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_estado_empresa" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaLab" InitialValue="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio_empresa" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Municipio:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_muni_empresa" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_municipio_empresa" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaLab" InitialValue="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_asentamiento_empresa" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Asentamiento:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_asen_empresa" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_asentamiento_empresa" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaLab" InitialValue="" />
                                            </div>
                                        </div>
                                    </div>
                                  </div>
                                  <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_vialidad_empresa" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tipo de vialidad:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo_vialidad_empresa" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_vialidad_empresa" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                     <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_calle_empresa" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Calle:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle_empresa" runat="server" ControlToValidate="txt_calle_empresa"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_calle_empresa" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_calle_empresa">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_exterior_empresa" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número exterior:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_exterior_empresa" runat="server" ControlToValidate="txt_exterior_empresa"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_exterior_empresa" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_exterior_empresa">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                  </div>
                                  <div class="module_subsec low_m columned three_columns">
                                     <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_interior_empresa" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <span class="text_input_nice_label">Número interior:</span>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_interior_empresa" runat="server" Enabled="True"
                                                     FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_interior_empresa">
                                                 </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                  </div> 
                             <div class="text_input_nice_div module_sec">
                                    <asp:TextBox ID="txt_Referencia_empresa" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_referencia" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_Referencia_empresa" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <span class="text_input_nice_label">Referencias:</span>
                                </div>
                             </ContentTemplate>
                       </asp:UpdatePanel>
                                </div>
                            </div>
                             
                            </section>
                          <%--</ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="txt_cp_empresa" />
                           </Triggers>
                       </asp:UpdatePanel>--%>
                       <!--------------------------------
                                    EMPLEO
                       --------------------------------->    
                        <asp:UpdatePanel runat="server" ID="upnl_empleo" UpdateMode="Conditional">
                         <ContentTemplate>
                            <section class="panel" runat="server" id="panel_empleo">
                            <header class="panel_header_folder panel-heading" runat="server" id="head_panel_empleo">
                                <span class="panel_folder_toogle_header">Empleo</span>
                                <span class="panel_folder_toogle up" runat="server" id="toggle_panel_empleo">&rsaquo;</span>
                            </header>
                            <div class="panel-body">
                             <div class="panel-body_content" runat="server" id="content_panel_empleo"> 
                              <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                        <asp:TextBox ID="txt_puesto" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>                                          
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Puesto:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_puesto" runat="server" ControlToValidate="txt_puesto"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_puesto" runat="server" Enabled="True"
                                                FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_puesto">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                        <asp:DropDownList ID="cmb_relacion_laboral" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="DUE">DUEÑO(A)</asp:ListItem>
                                            <asp:ListItem Value="EMP">EMPLEADO(A)</asp:ListItem>
                                            <asp:ListItem Value="SOC">SOCIO(A)</asp:ListItem>
                                            <asp:ListItem Value="AMB">SOCIO(A) Y EMPLEADO(A)</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Relación laboral:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_rel_lab" runat="server" ControlToValidate="cmb_relacion_laboral"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" InitialValue ="-1" ValidationGroup="val_PersonaLab">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                        <asp:TextBox ID="txt_antiguedad_empresa" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Años antigüedad:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_antiguedad_empresa" runat="server" ControlToValidate="txt_antiguedad_empresa"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_antiguedad_e" runat="server" Enabled="True"
                                                FilterType="Numbers, Custom" ValidChars="S /N" TargetControlID="txt_antiguedad_empresa">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                              </div>
                              <div class="module_subsec low_m columned three_columns">
                                 <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_sueldo" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Sueldo:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_sueldo" runat="server" ControlToValidate="txt_sueldo"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_sueldo" runat="server" Enabled="True"
                                                    FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_sueldo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:DropDownList ID="cmb_periodo_pago" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Periodo pago:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_periodo_pago" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_periodo_pago" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                              </div>   
                              <!--------------------------------
                                          JEFE DIRECTO
                               --------------------------------->        
                              <h5 style="font-weight: normal" class="resalte_azul module_subsec">Jefe directo</h5>
                                <div class="module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_nombre_jefe" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <span class="text_input_nice_label">Nombre(s):</span>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre_jefe" runat="server" Enabled="True"
                                                     FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre_jefe">
                                                 </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_paterno_jefe" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <span class="text_input_nice_label">Paterno:</span>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno_jefe" runat="server" Enabled="True"
                                                     FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_paterno_jefe">
                                                 </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_materno_jefe" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <span class="text_input_nice_label">Materno:</span>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_materno_jefe" runat="server" Enabled="True"
                                                     FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_materno_jefe">
                                                 </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>           
                                <div class="module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_correo_jefe" class="module_subsec_elements module_subsec_bigger-elements text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Correo electrónico:</span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_correo_jefe" runat="server" class="alertaValidator bold"
                                                    ControlToValidate="txt_correo_jefe" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                                    ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                                                </asp:RegularExpressionValidator>
                                            </div>  
                                        </div>    
                                    </div>    
                                </div>    
                                <div class="module_subsec low_m columned three_columns">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Teléfono</span>
                                </div>    
                                <div class="module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_lada_jefe" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="6"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="module_subsec_elements module_subsec_small-elements title_tag">Lada:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="txt_lada_jefe">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>    
                                        </div>    
                                    </div> 
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_telefono_jefe" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="15"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="module_subsec_elements module_subsec_small-elements title_tag">Número:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_telefono_jefe" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="txt_telefono_jefe">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>    
                                        </div>    
                                    </div>    
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_ext_jefe" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                    FilterType="Numbers" TargetControlID="txt_ext_jefe">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>    
                                        </div>    
                                    </div>
                                  </div> 
                                    </div> 
                                </div> 
                            </section>
                          </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--------------------------------
                               INGRESOS ADICIONALES
                         --------------------------------->  
                        <asp:UpdatePanel runat="server" ID="upnl_ingresos_ad" UpdateMode="Conditional">
                         <ContentTemplate>
                            <section class="panel" runat="server" id="panel_ingresos_ad">
                             <header class="panel_header_folder panel-heading" runat="server" id="head_panel_ingresos_ad">
                                 <span class="panel_folder_toogle_header">Ingresos adicionales</span>
                                 <span class="panel_folder_toogle up" href="#"  runat="server" id="toggle_panel_ingresos_ad">&rsaquo;</span>
                             </header>
                            <div class="panel-body">
                             <div class="panel-body_content" runat="server" id="content_panel_ingresos_ad"> 

                                    <div class="module_subsec low_m columned three_columns">
                                        <div class="module_subsec_elements">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_adicionales" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                     <span class="text_input_nice_label">Ingresos negocio:</span>
                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_adi" runat="server" Enabled="True"
                                                           FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_adicionales">
                                                     </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" module_subsec_elements">
                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_periodo_adicional" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Periodo ingresos:</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements">
                                            <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                                <asp:TextBox ID="txt_descripcion" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>                                          
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Descripción de negocio:</span>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_descripcion" runat="server" Enabled="True"
                                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_descripcion">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                   </div>                                                                              
                                   <div class="module_subsec low_m columned three_columns">
                                        <div class="module_subsec_elements">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_otros_ingresos" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                     <span class="text_input_nice_label">Otros ingresos:</span>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_otros_ing" runat="server" Enabled="True"
                                                           FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_otros_ingresos">
                                                     </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" module_subsec_elements">
                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_periodo_otros" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Periodo ingresos:</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements">
                                            <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                                <asp:TextBox ID="txt_origen" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>                                          
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Origen:</span>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_origen" runat="server" Enabled="True"
                                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_origen">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>

                                   </div>                                                                              
                                   <div class="module_subsec low_m columned three_columns">
                                        <div class="module_subsec_elements">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_dividendos" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                     <span class="text_input_nice_label">Dividendos</span>
                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_dividendos" runat="server" Enabled="True"
                                                         FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_dividendos">
                                                     </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" module_subsec_elements">
                                             <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_periodo_dividendos" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Periodo ingresos:</span>
                                                </div>
                                            </div>
                                        </div>
                                   </div>
                                    <div class="module_subsec low_m columned three_columns">
                                        <div class=" module_subsec_elements">
                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                    <asp:TextBox ID="txt_intereses" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                                    <div class="text_input_nice_labels">
                                                        <span class="text_input_nice_label">Intereses:</span>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_intereses" runat="server" Enabled="True"
                                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_intereses">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        <div class=" module_subsec_elements">
                                             <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_periodo_intereses" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                    AutoPostBack="False">
                                                </asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">Periodo intereses:</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   </div>
                            </section>
                          </ContentTemplate>
                        </asp:UpdatePanel>
                         
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_lab" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_laboral" class="btn btn-primary" OnClick="btn_guardar_laboral_Click" ValidationGroup="val_PersonaLab" runat="server" Text="Guardar" AutoPostBack="False" />
                        </div>
                                                         

                    </div>
                </div>
            </section>

        </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btn_guardar_laboral"/>
                <asp:AsyncPostBackTrigger  ControlID="chk_desempleo"/>
            </Triggers>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL DIGITALIZACION
                --------------------------------->
    <section class="panel" runat="server" id="panel_digitalizacion">
        <header class="panel_header_folder panel-heading" runat="server" id="head_panel_digitalizacion">
            <span class="panel_folder_toogle_header">Digitalización</span>
            <span class="panel_folder_toogle up" href="#" runat="server" id="toggle_panel_digitalizacion">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_digitalizacion">
                <asp:UpdatePanel ID="upd_pnl_digitalizacion" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec_elements vertical">

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_TamMax" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                            <div class="module_sec low_m">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <span class="title_tag">1.- Elegir documento por digitalizar:</span>
                                </div>
                            </div>

                            <div class="module_subsec no_column">
                                <div class="module_subsec_elements w_100 vertical">
                                    <asp:ListBox ID="lst_DocNoDigi" runat="server" Width="100%"></asp:ListBox>
                                </div>
                            </div>

                            <div class="module_subsec low_m">
                                <div class="module_subsec_elements" style="flex: 1">
                                    <asp:Button ID="btn_Digitalizar" CssClass="btn btn-primary" runat="server" Text="Digitalizar" />
                                </div>
                                <div class="module_subsec_elements module_subsec no_column low_m">
                                    <asp:DropDownList ID="lst_DocumentosEspecificos" runat="server" class="module_subsec btn btn-primary2 no_bm" Visible="false" AutoPostBack="true"></asp:DropDownList>                                    
                                    <div class="module_subsec_elements no_bm no_rm">
                                        <asp:Button ID="btn_ElegirDocumento" CssClass="btn btn-primary" runat="server" Text="Escanear" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_AlertaDigitaliza" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_fechadoc" class="text_input_nice_input" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Fecha expedición (DD/MM/AAAA):</span>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechadoc" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechadoc" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender_fechadoc" ControlToValidate="txt_fechadoc" CssClass="textogris"
                                                ErrorMessage="MaskedEditExtender_fechadoc" InvalidValueMessage="Fecha inválida" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechadoc" />
                                            <asp:RequiredFieldValidator runat="server" ID="req_fechadoc" CssClass="alertaValidator bold"
                                                ControlToValidate="txt_fechadoc" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_FechaDoc" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_FechaExp" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                            <div class="module_sec low_m">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <span class="title_tag">2.- Elegir si desea subir el archivo:</span>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns low_m">
                                <div class="module_subsec_elements">
                                    <div class="module_subsec_elements_content">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" Enabled="false" />
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="module_subsec_elements_content">
                                        <asp:Button ID="btn_Subir" CssClass="btn btn-primary" runat="server" Text="Subir" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_UploadEstatus" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                            <div class="module_sec low_m">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <span class="title_tag">3.- Documentos digitalizados:</span>
                                </div>
                            </div>

                            <div class="module_subsec no_column">
                                <div class="module_subsec_elements w_100 vertical">
                                    <asp:ListBox ID="lst_DocDigi" runat="server" Width="100%" Height="90px"></asp:ListBox>
                                </div>
                            </div>

                            <div class="module_subsec">
                                <div class="module_subsec_elements no_m module_subsec no_column">
                                    <asp:Button ID="btn_Ver" CssClass="btn btn-primary module_subsec_elements no_bm no_lm" runat="server" Text="Ver" />
                                    <asp:Button ID="btn_Eliminar" CssClass="btn btn-primary module_subsec_elements no_bm" runat="server" Text="Eliminar" />
                                </div>
                                <div class="module_subsec_elements flex_end module_subsec low_m" style="flex: 1;">
                                    <asp:Button ID="btn_Insertar_ColaValidacion" CssClass="btn btn-primary" runat="server" Text="Enviar a Validación" />
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_AlertaNoBorrar" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_AlertaVerBorrar" runat="server" Text="" class="alerta"></asp:Label>
                            </div>

                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_Subir" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </section>
    <asp:UpdatePanel ID="up_invisible" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" name="hdn_origen_busquedas" id="hdn_origen_busquedas" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
