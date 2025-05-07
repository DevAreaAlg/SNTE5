<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_CNF_INSTITUCIONES_CREACION.aspx.vb" Inherits="SNTE5.PEN_CNF_INSTITUCIONES_CREACION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            Id Institución
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="txt_id_institucion" runat="server" Enabled="false" CssClass="no_bm module_subsec_elements text_input_nice_input flex_1" />
            </div>
        </div>
    </section>
    <section class="panel" runat="server" id="panel_datos">
        <header runat="server" id="head_panel_datos" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Datos Generales</span>
            <span class="panel_folder_toogle down" runat="server" id="toggle_panel_datos" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_panel_datos">
                <div class="module_subsec_elements vertical">
                    <div class="text_input_nice_div module_sec">
                        <span class="text_input_nice_label">Activa:
                            <asp:CheckBox ID="chk_estatus" runat="server" Style="margin-left: 35px;" />
                        </span>
                    </div>
                </div>
                <div class="module_subsec low_m columned three_columns">
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_tipo" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Tipo de Institución:</span>
                                <asp:RequiredFieldValidator runat="server" ID="req_tipo" CssClass="alertaValidator"
                                    ControlToValidate="cmb_tipo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Insti" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_clave" CssClass="text_input_nice_input" runat="server" MaxLength="10" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Clave:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_clave" runat="server" ControlToValidate="txt_clave"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Insti">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_clave" runat="server" Enabled="True"
                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_clave">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_nombre" CssClass="text_input_nice_input" runat="server" MaxLength="100" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Razón social:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Insti">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                    FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=".,ÁÉÍÓÚÜÑáéíóúüñ " TargetControlID="txt_nombre">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_rfc" CssClass="text_input_nice_input" runat="server" MaxLength="13" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*RFC:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_rfc" runat="server" ControlToValidate="txt_rfc"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Insti">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_materno" runat="server" Enabled="True"
                                    FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars=" " TargetControlID="txt_rfc">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_fecha" CssClass="text_input_nice_input" runat="server" MaxLength="10" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Fecha de inicio de operaciones:</span>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txt_fecha" CssClass="textogris"
                                    ErrorMessage="MaskedEditExtender2" InvalidValueMessage="Fecha inválida" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fecha" />
                                <asp:RequiredFieldValidator runat="server" ID="req_fechanac" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_fecha" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Insti" />
                            </div>
                        </div>
                    </div>
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_estado" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Estado:</span>
                                <asp:RequiredFieldValidator runat="server" ID="req_estado" CssClass="alertaValidator"
                                    ControlToValidate="cmb_estado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Insti" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_director" CssClass="text_input_nice_input" runat="server" MaxLength="100" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Director:</span>
                                <%--<asp:RequiredFieldValidator ID="rfv_txt_director" runat="server" ControlToValidate="txt_director"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Insti">
                                </asp:RequiredFieldValidator>--%>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fte_txt_director" runat="server" Enabled="True"
                                    FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=".ÁÉÍÓÚÜÑáéíóúüñ " TargetControlID="txt_director">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"/>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar" CssClass="btn btn-primary" runat="server" ValidationGroup="val_Insti" Text="Guardar" AutoPostBack="False" OnClick="btn_guardar_Click" />
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_direccion">
        <header runat="server" id="head_panel_direccion" class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Dirección</span>
            <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_direccion">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_direccion">
                <asp:UpdatePanel runat="server" ID="upnl_direccion">
                    <ContentTemplate>
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
                                            ValidationGroup="val_InstitucionDir" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_calle" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Calle:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_calle"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,." TargetControlID="txt_calle">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="module_subsec columned three_columns ">
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_exterior" class="text_input_nice_input" runat="server" MaxLength="4"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Número exterior:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_exterior"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
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
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_cp" runat="server" Enabled="True"
                                            TargetControlID="txt_cp" FilterType="Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" AutoPostBack="True" />
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_estado1" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Estado:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_estado" runat="server" ControlToValidate="cmb_estado1"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
                                        </asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_municipio" runat="server" ControlToValidate="cmb_municipio"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
                                        </asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_asentamiento" runat="server" ControlToValidate="cmb_asentamiento"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_InstitucionDir">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="text_input_nice_div module_sec">
                            <asp:TextBox ID="txt_referencias" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <span class="text_input_nice_label">Referencias:</span>
                        </div>

                        <div align="center">
                            <asp:Label ID="lbl_status_dom" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_domicilio" class="btn btn-primary" runat="server" ValidationGroup="val_InstitucionDir" Text="Guardar" AutoPostBack="False" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_telefono">
        <header runat="server" id="head_panel_telefono" class="panel_header_folder panel-heading up_folder">
            <span class="panel_folder_toogle_header">Teléfono</span>
            <span class="panel_folder_toogle up" runat="server" id="toggle_panel_telefono" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_telefono">
                <asp:UpdatePanel runat="server" ID="upnl_telefono" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="module_subsec low_m columned align_items_flex_center">
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Teléfono 1:</span>
                            <asp:TextBox ID="txt_lada1" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_lada1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <asp:TextBox ID="txt_tel1" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_tel1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                            <asp:TextBox ID="txt_ext1" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_ext1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>

                        <div class="module_subsec columned low_m align_items_flex_center">
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Teléfono 2:</span>
                            <asp:TextBox ID="txt_lada2" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_lada2">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <asp:TextBox ID="txt_tel2" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_tel2">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                            <asp:TextBox ID="txt_ext2" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_ext2">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>

                        <div class="module_subsec columned low_m align_items_flex_center">
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Teléfono 3: &nbsp;</span>
                            <asp:TextBox ID="txt_lada3" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_lada3">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <asp:TextBox ID="txt_tel3" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_tel3">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                            <asp:TextBox ID="txt_ext3" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_ext3">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>

                        <div class="module_subsec columned low_m align_items_flex_center">
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Teléfono 4: &nbsp; &nbsp;</span>
                            <asp:TextBox ID="txt_lada4" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_lada4">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <asp:TextBox ID="txt_tel4" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_tel4">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                            <asp:TextBox ID="txt_ext4" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_ext4">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>

                        <div align="center">
                            <asp:Label ID="lbl_statustel" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_c" class="btn btn-primary" runat="server" ValidationGroup="val_PersonaCon" Text="Guardar" AutoPostBack="False" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_oficinas">
        <header class="panel_header_folder panel-heading up_folder" id="head_panel_oficinas" runat="server">
            <span class="panel_folder_toogle_header">Oficinas</span>
            <span class="panel_folder_toogle up" runat="server" id="toggle_panel_oficinas">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_oficinas">
                <asp:UpdatePanel runat="server" ID="Uupnl_oficina">
                    <ContentTemplate>
                        <div class="module_subsec no_column low_m">
                            <span class="text_input_nice_label title_tag">Activo:</span>
                            <asp:CheckBox ID="ckb_Activo" CssClass="mod_check" runat="server" />
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_tipo" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Tipo:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo" CssClass="alertaValidator"
                                            ControlToValidate="ddl_tipo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_Oficina" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_nombre_ofi" runat="server" class="text_input_nice_input" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Nombre:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_nombre_ofi"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre_abreviatura" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Abreviatura:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_abreviatura" runat="server" ControlToValidate="txt_nombre_abreviatura"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_abreviatura" runat="server" Enabled="True"
                                            FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="txt_nombre_abreviatura">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns flex_end">
                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_calle_num" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Calle y número:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_callenum" runat="server" ControlToValidate="txt_calle_num"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_callenum" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,#,-,_" TargetControlID="txt_calle_num">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                    <asp:TextBox ID="txt_cp_ofi" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>

                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Código postal: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_cp_ofi"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_cp_ofi">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <%--  <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/img/glass.png" Style="height: 16px" AutoPostBack="False" />--%>
                                <asp:ImageButton ID="btn_buscacp" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" AutoPostBack="False" />
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_Estado" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label title_tag">*Estado: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_estado2" runat="server" ControlToValidate="ddl_Estado"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_municipio" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label title_tag">*Municipio: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_municipio1" runat="server" ControlToValidate="ddl_municipio"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_asentamiento" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label title_tag">*Asentamiento: </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_asentamiento1" runat="server" ControlToValidate="ddl_asentamiento"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_lada" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Lada:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_lada" runat="server" ControlToValidate="txt_lada"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_lada">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_telefono" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Teléfono:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tel" runat="server" ControlToValidate="txt_telefono"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tel" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_telefono">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_extension" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Extensión:</span>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_extension"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                                    </asp:RequiredFieldValidator>--%>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_extension">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusofi" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_sec low_m">
                            <div style="display: flex; flex-direction: row-reverse">
                                <asp:Button ID="btn_guardar_oficina" class="btn btn-primary" runat="server" ValidationGroup="val_Oficina" Text="Guardar" AutoPostBack="False" />
                            </div>
                        </div>


                        <br />

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_oficinas" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                                GridLines="None" Width="100%" Visible="true">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDOFICINA" HeaderText="Núm. Oficina">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ABREVIATURA" HeaderText="Clave">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DIRECCION" HeaderText="Dirección">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TELEFONO" HeaderText="Teléfono">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>



                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>



</asp:Content>
