<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PERSONA.aspx.vb" Inherits="SNTE5.CORE_PER_PERSONA" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
            }
            else {
                var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
            }
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>
    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Selección del agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="tbx_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox ID="txt_IdCliente" runat="server" CssClass="text_input_nice_input" MaxLength="9" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Depe_NumCtrl">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                        <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" ValidationGroup="val_Depe_NumCtrl" />
                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar agremiado" />
                    </div>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                    <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </span>
                    <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
                </div>
                <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
                <asp:Label ID="lbl_maxreest" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
            </div>
        </div>
    </section>
    <!-------------------- PANEL DE DATOS GENERALES -------------------->
    <asp:UpdatePanel runat="server" ID="upnl_generales" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_generales">
                <header runat="server" id="head_panel_generales" class="panel_header_folder panel-heading up_folder">
                    <span class="panel_folder_toogle_header">Datos generales</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_generales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_generales">
                        <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos generales del agremiado:</h5>
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_region" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Región:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="alertaValidator"
                                            ControlToValidate="ddl_region" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_delegacion" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Delegación:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                            ControlToValidate="ddl_delegacion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_cct" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Centro de trabajo:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="alertaValidator"
                                            ControlToValidate="ddl_cct" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Persona" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_agremiado" CssClass="text_input_nice_input" MaxLength="300" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Agremiado:" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_agremiado" Display="Dynamic"
                                            CssClass="alertaValidator bold" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona" />
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_agremiado" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_curp" class="text_input_nice_input" runat="server" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">CURP:</span>
                                        
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
                                        <span class="text_input_nice_label">*RFC:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfc" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_rfc">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_rfc"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Fecha de nacimiento afiliado-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fecha" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Fecha de nacimiento (DD/MM/AAAA):</span>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txt_fecha" CssClass="textogris"
                                            ErrorMessage="MaskedEditExtender2" Display="Dynamic" InvalidValueMessage="Fecha inválida" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fecha" />
                                    </div>
                                </div>
                            </div>


                            <!--Sexo afiliado-->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="rad_sexcon1" runat="server" class="texto" GroupName="sexoafiliado"
                                            Text="Hombre"></asp:RadioButton>
                                    </div>
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="rad_sexcon2" runat="server" class="texto" GroupName="sexoafiliado"
                                            Text="Mujer"></asp:RadioButton>
                                    </div>
                                    <span class="text_input_nice_label">Sexo:</span>
                                </div>
                            </div>
                        </div>

                        <!--Notas afiliado-->
                        <div class="module_subsec low_m">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                    <span class="text_input_nice_label">Notas:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="custom,Numbers ,LowercaseLetters, UppercaseLetters" TargetControlID="txt_notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,-,,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_g" class="btn btn-primary" runat="server" OnClick="btn_guardar_g_Click" ValidationGroup="val_Persona" Text="Guardar" AutoPostBack="true" />
                        </div>

                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-------------------- PANEL DE DATOS GENERALES -------------------->

    <%-- <!-----------------------------
                    PANEL ADICIONALES
            --------------------------------->
    <asp:UpdatePanel ID="upnl_adicionales" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_adicionales">
                <header runat="server" id="head_panel_adicionales" class="panel_header_folder panel-heading up_folder">
                    <span class="panel_folder_toogle_header">Datos adicionales</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_adicionales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" id="content_panel_adicionales" runat="server">
                        <h5 style="font-weight: normal" class="resalte_azul module_subsec">Datos adicionales del afiliado:</h5>
                        <!--NSS afiliado-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nss" class="text_input_nice_input" runat="server" MaxLength="11"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Número de seguridad social:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="req_nss" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_nss">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <!--Estado civil afiliado-->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_edo_civil" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true">
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


                            <!-- Tipo de conyuge -->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_tipo_conyuge" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="INT">INTERNO(A)</asp:ListItem>
                                        <asp:ListItem Value="EXT">EXTERNO(A)</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label" id="lbl_tipo_con" runat="server">*Tipo de cónyuge:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo_conyuge" CssClass="alertaValidator"
                                            ControlToValidate="ddl_tipo_conyuge" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                        </div>


                        <!-- Captura de datos del conyuge -->
                        <h5 id="h5_con" style="font-weight: normal" class="resalte_azul module_subsec" runat="server">Datos del cónyuge:</h5>

                        <div class="module_subsec low_m">
                            <asp:Label ID="lbl_buscarpersonas1" runat="server" CssClass="text_input_nice_label" Text=" Se ha seleccionado el tipo de cónyuge interno, por favor realice una:"
                                Visible="False" />
                            <asp:LinkButton ID="lnk_busqueda_coyuge" runat="server" CssClass="textoRojo" Text="BUSQUEDA DE PERSONA"
                                Visible="False" />
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <!-- Nombre del conyuge-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_conyuge" CssClass="text_input_nice_input" MaxLength="200" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label" id="nombre1c" runat="server" visible="false">*Primer nombre:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_conyuge" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_conyuge"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre1_conyuge" runat="server" ControlToValidate="txt_conyuge" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_PersonaAdi" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <!-- Segundo nombre del conyuge-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_conyuge2" CssClass="text_input_nice_input" MaxLength="200" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    <span class="text_input_nice_label" id="nombre2_c" runat="server" visible="false">Segundo(s) nombre(s):</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_conyuge2"
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                    </ajaxToolkit:FilteredTextBoxExtender>

                                </div>
                            </div>


                            <!-- Paterno conyuge-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_paternoc" CssClass="text_input_nice_input" MaxLength="200" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label" id="paternoc" runat="server" visible="false">*Apellido paterno:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_paternoc"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_paterno_conyuge" runat="server" ControlToValidate="txt_paternoc" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_PersonaAdi" Enabled="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>




                        </div>


                        <div class="module_subsec low_m columned three_columns">
                            <!-- Materno conyuge-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_maternoc" CssClass="text_input_nice_input" MaxLength="200" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    <span class="text_input_nice_label" id="maternoc" runat="server" visible="false">Apellido materno:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_maternoc"
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                    </ajaxToolkit:FilteredTextBoxExtender>

                                </div>
                            </div>


                            <!-- Curp conyuge-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_CURPc" CssClass="text_input_nice_input" MaxLength="18" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label" id="CURPc" runat="server" visible="false">*CURP:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Numbers,UppercaseLetters, LowercaseLetters" TargetControlID="txt_CURPc">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_curp_conyuge" runat="server" ControlToValidate="txt_CURPc" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_PersonaAdi">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" class="textogris"
                                            ControlToValidate="txt_CURPc" ErrorMessage="Error: formato CURP  incorrecto." Display="Dynamic"
                                            ValidationExpression="^[a-zA-Z]{3,4}(\d{6})[hmHM]{1}[a-zA-Z]{4,5}[0-9A-Z]{1}[0-9A-Z]{1}" ValidationGroup="val_PersonaAdi" Enabled="false" />
                                    </div>
                                </div>
                            </div>



                            <!-- RFC Conyuge-->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_RFCc" class="text_input_nice_input" runat="server" MaxLength="13" Enabled="false" Visible="false"></asp:TextBox>
                                    <span class="text_input_nice_label" id="RFCc" runat="server" visible="false">RFC</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_RFCc">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                        </div>


                        <!-- Fecha de nacimiento conyuge-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fechaconyuge" class="text_input_nice_input" runat="server" MaxLength="10" Enabled="false" Visible="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label" id="fechanacC" runat="server" visible="false">*Fecha de nacimiento (DD/MM/AAAA):</span>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaconyuge" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fecha_conyuge" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txt_fechaconyuge" CssClass="textogris"
                                            ErrorMessage="MaskedEditExtender2" Display="Dynamic" InvalidValueMessage="Fecha inválida" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaconyuge" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_fechanac_CON" CssClass="alertaValidator bold"
                                            ControlToValidate="txt_fechaconyuge" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PersonaAdi" Enabled="false" />
                                    </div>
                                </div>
                            </div>


                            <!-- Sexo conyuge-->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="conyuge_sexo1" runat="server" class="texto" GroupName="sexoconyug" Enabled="false" Visible="false"
                                            Text="Hombre"></asp:RadioButton>
                                    </div>
                                    <div style="margin: 0" class="radio">
                                        <asp:RadioButton ID="conyuge_sexo2" runat="server" class="texto" GroupName="sexoconyug" Enabled="false" Visible="false"
                                            Text="Mujer"></asp:RadioButton>
                                    </div>
                                    <span class="text_input_nice_label" id="sexoC" runat="server" visible="false">*Sexo:</span>
                                </div>
                            </div>

                        </div>

                        <!-- Número de control e institución del cónyuge-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements" id="control_institu" runat="server" visible="FALSE">

                                <div class="text_input_nice_div module_subsec_elements_content">

                                    <asp:TextBox ID="txt_control_conyuge" CssClass="text_input_nice_input" runat="server" Enabled="false" Visible="true"></asp:TextBox>

                                    <span class="text_input_nice_label">No. control del  cónyuge:</span>
                                </div>
                                &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                                  <div class="text_input_nice_div module_subsec_elements_content">

                                      <asp:TextBox ID="txt_inst_conyuge" CssClass="text_input_nice_input" runat="server" Enabled="false" Visible="true"></asp:TextBox>

                                      <span class="text_input_nice_label">Institución del  cónyuge:</span>
                                  </div>
                            </div>



                        </div>


                        <!-- Botón y lbl de adicionales-->
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_adi" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_a" class="btn btn-primary" runat="server" OnClick="btn_guardar_a_Click" ValidationGroup="val_PersonaAdi" Text="Guardar" />
                        </div>
                        <!-------------------------------->
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <!-----------------------------
                    PANEL DOMICILIO
            --------------------------------->
    <asp:UpdatePanel ID="upnl_domicilio" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_domicilio">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_domicilio">
                    <span class="panel_folder_toogle_header">Datos domiciliarios</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_domicilio">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_domicilio">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos domiciliarios del agremiado:</h5>


                                <div class="module_subsec low_m columned three_columns">
                                    <!-- Calle -->
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_calle" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Calle:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle" runat="server" ControlToValidate="txt_calle"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_calle" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txt_calle">
                                                </ajaxToolkit:FilteredTextBoxExtender>


                                            </div>
                                        </div>
                                    </div>

                                </div>


                                <!--Num exterior-->
                                <div class="module_subsec columned three_columns ">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_exterior" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número exterior:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_exterior" runat="server" ControlToValidate="txt_exterior"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_exterior" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_exterior">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>


                                    <!-- Num interior-->
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_interior" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Número interior:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_numint" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_interior">
                                                </ajaxToolkit:FilteredTextBoxExtender>

                                            </div>
                                        </div>
                                    </div>



                                    <!-- CP -->
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="False"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Código postal:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_cp" runat="server" Enabled="True"
                                                    TargetControlID="txt_cp" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!--Estado-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_estado" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                            <span class="text_input_nice_label">Estado:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_estado" runat="server" Enabled="True"
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" TargetControlID="txt_estado" ValidChars=" ÁÉÍÓÚÑáéíóúñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>


                                    <!-- Municipio -->
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_municipio" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                            <span class="text_input_nice_label">Municipio:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_municipio" runat="server" Enabled="True"
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" TargetControlID="txt_municipio" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>



                                    <!-- Asentamiento -->
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_asentamiento" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                            <span class="text_input_nice_label">Asentamiento:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_asentamiento" runat="server" Enabled="True"
                                                FilterType="Custom, LowercaseLetters, UppercaseLetters" TargetControlID="txt_asentamiento" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                </div>

                                <!--Referencias-->
                                <div class="text_input_nice_div module_sec">
                                    <asp:TextBox ID="txt_referencias" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_referencias" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <span class="text_input_nice_label">Referencias:</span>
                                </div>


                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_status_dom" runat="server" CssClass="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar_domicilio" class="btn btn-primary" OnClick="btn_guardar_domicilio_Click" runat="server" ValidationGroup="val_PersonaDir" Text="Guardar" AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-------------------- PANEL DE DATOS DE CONTACTO -------------------->
    <asp:UpdatePanel ID="upnl_contacto" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_contacto">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_contacto">
                    <span class="panel_folder_toogle_header">Datos de contacto</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_contacto">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_contacto">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos de contacto del agremiado:</h5>
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox runat="server" ID="txt_correo" MaxLength="100"
                                                CssClass="module_subsec_elements module_subsec_bigger-elements text_input_nice_input" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Correo electrónico:" />
                                                <asp:RegularExpressionValidator runat="server" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_correo" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox runat="server" ID="txt_telcasa" MaxLength="10"
                                                CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Teléfono personal:" />
                                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                                    TargetControlID="txt_telcasa">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec flex_center">
                                    <asp:Label runat="server" ID="lbl_estatus_contacto" CssClass="alerta" />
                                </div>
                                <div class="module_subsec flex_end">
                                    <asp:Button runat="server" ID="btn_guardar_contactos" CssClass="btn btn-primary" Text="Guardar"
                                        AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_contactos" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-------------------- PANEL DE DATOS DE CONTACTO -------------------->

    <!-----------------------------
                    PANEL LABORALES
            --------------------------------->
    <%--<asp:UpdatePanel ID="upnl_laborales" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>

            <section class="panel" runat="server" id="panel_laborales">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_laborales">
                    <span class="panel_folder_toogle_header">Datos de plaza</span>
                    <span class="panel_folder_toogle up" runat="server" id="toggle_panel_laborales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_laborales">

                        <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos laborales del afiliado:</h5>

                        <!-- PLAZA-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_plaza" class="text_input_nice_input" runat="server" MaxLength="15"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Plaza:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_plaza" runat="server"
                                            ControlToValidate="txt_plaza" TargetControlID="txt_plaza" CssClass="alertaValidator bold" Display="Dynamic"
                                            ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_plaza" runat="server" Enabled="True"
                                            FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_plaza" ValidChars="A,E,I,O,U,Ñ,a,e,i,o,u,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>


                            <!-- Nombramiento / puesto-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_puesto" class="text_input_nice_input" runat="server" MaxLength="250"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Puesto:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_puesto"
                                            CssClass="alertaValidator bold" Display="Dynamic" ValidationGroup="val_PersonaLab">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_txt_puesto" runat="server" Enabled="True"
                                            FilterType="Custom ,LowercaseLetters, UppercaseLetters" TargetControlID="txt_puesto" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>



                            <!--Salario integrado-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_sueldo" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Salario integrado:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_sueldo"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_sueldo">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" Display="Dynamic"
                                            runat="server" ControlToValidate="txt_sueldo" CssClass="textogris"
                                            ErrorMessage=" Error:Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>


                            <!-- Sueldo Neto -->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_sueldo_neto" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Sueldo Neto:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_sueldo_neto"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_sueldo_neto">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic"
                                            runat="server" ControlToValidate="txt_sueldo_neto" CssClass="textogris"
                                            ErrorMessage=" Error:Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>



                            <!-- Salario de cotización -->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_sueldo_cot" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Salario de Cotización:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_sueldo_cot"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaLab">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_sueldo_cot">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic"
                                            runat="server" ControlToValidate="txt_sueldo_cot" CssClass="textogris"
                                            ErrorMessage=" Error:Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>



                            <!--Periodicidad de pago-->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_persuel" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Periodicidad de pago:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" CssClass="alertaValidator"
                                            ControlToValidate="cmb_persuel" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>




                            <!--Fecha de ingreso -->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fechaIniOpe" runat="server" class="text_input_nice_input"
                                        MaxLength="10" ValidationGroup="val_PersonaLab"></asp:TextBox>

                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Fecha de ingreso</span>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3"
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaIniOpe">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechaIniOpe">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="txt_fechaIniOpe" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_PersonaLab"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <!-- Estatus del trabajador -->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_estatus" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False" ToolTip="*Estatus del trabajador">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Estatus del trabajador</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" CssClass="alertaValidator"
                                            ControlToValidate="cmb_estatus" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>



                            <!-- Tipo de trabajador -->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_tipotrab" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="1">SINDICALIZADO</asp:ListItem>
                                        <asp:ListItem Value="2">DE CONFIANZA</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Tipo de trabajador:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" CssClass="alertaValidator"
                                            ControlToValidate="cmb_tipotrab" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>



                            <!-- Antiguedad del trabajador -->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_antiguedad" class="text_input_nice_input" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Antiguedad(Años-meses):</span>

                                    </div>
                                </div>
                            </div>



                            <!-- Generación de trabajador -->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_generacion" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="1">TRANSICIÓN </asp:ListItem>
                                        <asp:ListItem Value="2">NUEVA GENERACIÓN</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Generación del trabajador:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="alertaValidator"
                                            ControlToValidate="cmb_generacion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_PersonaLab" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>


                            <!-- Usuario -->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_user_afiliado" class="text_input_nice_input" runat="server" MaxLength="200" Enabled="true"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Usuario:</span>
                                    </div>
                                </div>
                            </div>



                            <!-- Contraseña -->
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_pass_afiliado" class="text_input_nice_input" runat="server" MaxLength="200" Enabled="true"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Contraseña:</span>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div align="center">
                            <asp:Label ID="lbl_status_lab" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_l" class="btn btn-primary" OnClick="btn_guardar_l_Click" ValidationGroup="val_PersonaLab" runat="server" Text="Guardar" AutoPostBack="false" />
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_guardar_l" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <asp:UpdatePanel ID="up_invisible" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" name="hdn_origen_busquedas" id="hdn_origen_busquedas" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <!------------------------------ PANEL BANCARIOS ------------------------------>
    <asp:UpdatePanel ID="upnl_bancarios" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_bancarios">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_bancarios">
                    <span class="panel_folder_toogle_header">Datos bancarios</span>
                    <span class="panel_folder_toogle up" runat="server" id="toggle_panel_bancarios">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_bancarios">
                        <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos bancarios del agremiado:</h5>
                        <div class="module_subsec low_m columned three_columns">
                            <!--------------- Banco --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="cmb_banco" CssClass="btn btn-primary2 dropdown_label"
                                        AutoPostBack="True" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Banco:" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" Display="Dynamic"
                                            ControlToValidate="cmb_banco" ErrorMessage=" Falta Dato!" InitialValue="-1"
                                            ValidationGroup="val_PersonaBANC" />
                                    </div>
                                </div>
                            </div>
                            <!--------------- Medio de Pago --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_medio_paga" CssClass="text_input_nice_input"
                                        MaxLength="20" Enabled="false"/>
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" Text="Medio de pago:"/>
                                    </div>
                                </div>
                            </div>
                            <!--------------- Cuenta CLABE --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_clabe" CssClass="text_input_nice_input" MaxLength="18" />
                                    <div class="text_input_nice_labels">
                                        <%--<asp:Label runat="server" ID="lbl_valCta" Text="*Medio:"/>--%>
                                        <asp:Label runat="server" Text="*CLABE:"/>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_clabe"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_PersonaBANC"/>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_clabe"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns">
                            <!--------------- Cuenta CLABE --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_clabe_conf" CssClass="text_input_nice_input" MaxLength="18" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" Text="*Confirmar CLABE:"/>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_clabe_conf"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_PersonaBANC"/>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_clabe_conf"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div align="center">
                            <asp:Label runat="server" ID="lbl_estatus_bank" CssClass="alerta"/>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button runat="server" ID="btn_guarda_bank" CssClass="btn btn-primary" 
                                OnClick="btn_guarda_bank_Click" ValidationGroup="val_PersonaBANC" Text="Guardar" AutoPostBack="false" />
                        </div>
                    </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel> 
    <!------------------------------ PANEL ESTATUS DE TRABAJADOR ------------------------------>
    <asp:UpdatePanel ID="upnl_estatus_trab" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_estatus">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_estatus">
                    <span class="panel_folder_toogle_header">Estatus Trabajador</span>
                    <span class="panel_folder_toogle up" runat="server" id="toggle_panel_estatus">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_estatus">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <div class="module_subsec low_m columned two_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="Combo_Estatus" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                                    </asp:DropDownList>
                                     <asp:Label runat="server" CssClass="text_input_nice_label" Text="Estatus Trabajador:" />

                                    </div>

                            </div>
                      </div> 
                        <div class="module_subsec low_m columned two_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                   <asp:TextBox runat="server" class="text_input_nice_textarea" ID="Notas_baja" MaxLength ="2000" TextMode="MultiLine" />                        
                                   <asp:Label runat="server" CssClass="text_input_nice_label" Text="Notas:" />

                                </div>
                            </div>
                         </div>                         
                        <div align="center">
                            <asp:Label runat="server" ID="lbl_cambio_estatus" CssClass="alerta"/>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button runat="server" ID="Btn_Guardar_Estatus" CssClass="btn btn-primary" 
                                OnClick="Btn_Guardar_Estatus_Click" Text="Guardar" AutoPostBack="false" />
                        </div>
                                   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Btn_Guardar_Estatus" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
        </div>
    </section>
    </contentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" name="hdn_origen_busquedas" id="Hidden1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

