<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_AMORTIZACION.aspx.vb" Inherits="SNTE5.CRED_VEN_AMORTIZACION" MaintainScrollPositionOnPostback ="true" %>

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

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_info" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

    <section class="panel">
        <header class="panel-heading">
            <span>Afiliado</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec low_m flex_end">
                <asp:LinkButton ID="lnk_ProvRec" runat="server" CssClass="btntextoazul" Text="Proveedor de recursos / Propietario real" Visible="false" />
            </div>

            <div class="module_subsec low_m  columned four_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                        <asp:DropDownList ID="ddl_Instituciones" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Institución:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumCtrl" runat="server" ControlToValidate="ddl_Instituciones"
                                CssClass="textogris" Display="Dynamic" InitialValue="-1" ErrorMessage=" Falta Dato!" ValidationGroup="val_Depe_NumCtrl"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_numcliente">Número de afiliado: </asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cliente" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_cliente">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cliente" Enabled="true"
                                CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                        </div>
                    </div>

                    <asp:LinkButton ID="btn_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />&nbsp;&nbsp;
                     <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                         Style="height: 18px; width: 18px;"></asp:ImageButton>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_folio">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="module_subsec_elements_content">
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_accion" runat="server" CssClass="texto" Text="*Indique quién realiza la acción:" Visible="False"></asp:Label>
                        </div>
                        <asp:RadioButton ID="rad_cliente" runat="server" Text="Cliente" CssClass="texto"
                            GroupName="operaciones" AutoPostBack="True" Visible="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                      
                        <asp:RadioButton ID="rad_usuario" runat="server" Text="Usuario" CssClass="texto" GroupName="operaciones"
                            AutoPostBack="True" Visible="False" />
                    </div>
                </div>
            </div>

            <div class="module_subsec">
                <asp:Label CssClass="textonegritas" ID="lbl_cliente" runat="server" />
            </div>

            <div runat="server" id="pnl_usuario_pf" visible="false">

                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="HeaderPanel_busqueda"
                    Collapsed="true" CollapsedImage="~/img/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="HeaderPanel_busqueda" ExpandedImage="~/img/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_busqueda" SuppressPostBack="true"
                    TargetControlID="pnl_busqueda">
                </ajaxToolkit:CollapsiblePanelExtender>
                <asp:Panel ID="HeaderPanel_busqueda" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_busqueda" runat="server" />
                        <asp:Label ID="Label5" runat="server" class="title_tag" Text="Buscar persona"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_busqueda" runat="server">
                    <div class="texto">

                        <asp:Label ID="lbl_mensaje" runat="server" CssClass="module_subsec" Text="¿Desea buscar una persona guardada en la Base de datos?"></asp:Label>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                    <asp:TextBox runat="server" MaxLength="8" ID="txt_IdCliente" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_numaval" runat="server" CssClass="texto" Text="*Número de afiliado:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" FilterType="Numbers" Enabled="True" TargetControlID="txt_idCliente"
                                            ID="FilteredTextBoxExtender__idcliente">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <asp:ImageButton ID="btn_buscapersona_ori" runat="server" ImageUrl="~/img/img/glass.png" Style="height: 18px; width: 18px;" />
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="btn_seleccionar_ori" runat="server" class="textogris" Text="Seleccionar" />
                            </div>
                        </div>

                        <div class="module_subsec">
                            <asp:Label runat="server" CssClass="textonegritas" ID="lbl_nombre_cliente_ori" Text="" />
                        </div>
                    </div>
                </asp:Panel>

                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" CollapseControlID="HeaderPanel_nuevabusqueda"
                    Collapsed="true" CollapsedImage="~/img/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="HeaderPanel_nuevabusqueda" ExpandedImage="~/img/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_nuevabusqueda" SuppressPostBack="true"
                    TargetControlID="pnl_nuevabusqueda">
                </ajaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="HeaderPanel_nuevabusqueda" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_nuevabusqueda" runat="server" />
                        <asp:Label runat="server" ID="lbl_new_persona" class="textogris">Nueva persona</asp:Label>
                        <br />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_nuevabusqueda" runat="server">

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_nombre1_u" runat="server" class="text_input_nice_input" MaxLength="300" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_nombre1_u" class="texto">*Primer nombre:</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_nombre1_u" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_nombre2_u" runat="server" class="text_input_nice_input" MaxLength="300" ValidationGroup="val_usuario_pf" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_nombre2_u" class="texto">Segundo(s) nombre(s):</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_nombre2_u" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_paterno_u" runat="server" class="text_input_nice_input" MaxLength="100" ValidationGroup="val_usuario_pf" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_paterno_u" class="texto">*Apellido paterno:</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_paterno_u" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_materno_u" runat="server" class="text_input_nice_input" MaxLength="100" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_materno_u" class="texto"> Apellido materno:</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_materno_u" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_sexo" class="texto">*Sexo:</asp:Label>
                                </div>
                                <asp:RadioButton ID="rad_sexo0" runat="server" Checked="True" class="texto" GroupName="sexo" Text="Hombre" />
                                <asp:RadioButton ID="rad_sexo1" runat="server" class="texto" GroupName="sexo" Text="Mujer" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_pais" runat="server" class="btn btn-primary2 dropdown_label" ValidationGroup="val_usuario_pf">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_pais" class="texto">*País de nacimiento:</label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_nac" runat="server" class="btn btn-primary2 dropdown_label" ValidationGroup="val_usuario_pf">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_nac" class="texto">*Nacionalidad:</asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_fechanac" runat="server" class="text_input_nice_input" MaxLength="10" ValidationGroup="val_usuario_pf" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_fechanac" class="texto">*Fecha nacimiento (DD/MM/AAAA):</asp:Label>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechanacperf" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechanac" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechanacperf" runat="server" ControlExtender="MaskedEditExtender_fechanacperf" ControlToValidate="txt_fechanac" Display="Dynamic" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechanacperf" InvalidValueMessage="Fecha inválida" ValidationGroup="val_usuario_pf" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechanac" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_id_oficial" runat="server" class="text_input_nice_input" MaxLength="25" ValidationGroup="val_usuario_pf" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_id_oficial" class="texto">*Id. Oficial:</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_id_oficial" runat="server" FilterType="Custom, Numbers ,UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_id_oficial" ValidChars="-" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" OnTextChanged="txt_cp_TextChanged" />
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" ID="lbl_cp" class="texto">*CP:</asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_cp">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/img/glass.png"
                                Style="height: 16px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_BusquedaCP" runat="server" class="textogris" Text="Buscar CP" />
                        </div>

                    </div>

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_estado" runat="server" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_estado" class="texto">*Estado:</label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_municipio" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_municipio" class="texto">*Municipio:</label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_localidad" runat="server" AutoPostBack="False" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_localidad" class="texto">*Localidad:</label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_colonia" runat="server" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_colonia" class="texto">*Asentamiento:</label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_tipo_via" runat="server" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <label id="lbl_tipo_via" class="texto">*Tipo de vialidad:</label>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="module_subsec low_m  columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_calle" runat="server" class="text_input_nice_input" MaxLength="100" />
                                <div class="text_input_nice_labels">
                                    <label id="lbl_calle" class="texto">*Calle:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle" runat="server" ControlToValidate="txt_calle"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_usuario_pf">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_num_ext" runat="server" class="text_input_nice_input" MaxLength="10" />
                                <div class="text_input_nice_labels">
                                    <label id="lbl_num_ext" class="texto">*Número exterior:</label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_num_ext" runat="server"
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_num_ext" ValidChars=" ,Á,É,Í,Ó,Ú,á,é,í,ó,ú,#,/,-,.,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_num_int" runat="server" class="text_input_nice_input" MaxLength="10" />
                                <div class="text_input_nice_labels">
                                    <label id="lbl_num_int" class="texto">Número interior:</label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_num_int" runat="server"
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                        TargetControlID="txt_num_int" ValidChars=" ,Á,É,Í,Ó,Ú,á,é,í,ó,ú,#,/,-,.,">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_ife" runat="server" CssClass="alerta" />
                <asp:Label ID="lbl_info_ide" CssClass="alerta" Visible="True" runat="server" />
                <asp:Label ID="lbl_info_disp" runat="server" CssClass="alerta" Visible="True" />
            </div>

        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Préstamo</span>
        </header>
        <div class="panel-body">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Monto del préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_montocredito"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_montocreditotxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Saldo del préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_saldocredito"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_saldocreditotxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Fecha de disposición préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechaliberacion"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechaliberaciontxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Fecha último pago:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechaultimopago"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechaultimopagotxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Tasa interés ordinaria:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_intnor"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_intnortxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Tasa interés moratoria:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_intmor"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_intmortxt"></asp:TextBox>

                            </div>
                        </div>

                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Días desde el último pago:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_diasultimopago"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_diasultimopagotxt"></asp:TextBox>

                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Abonos vencidos:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_vencidos"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_vencidostxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Fecha vencimiento:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_fechavenc"></asp:Label>
                                <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_fechavenctxt"></asp:TextBox>
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Fecha de corte:" CssClass="module_subsec_elements module_subsec_bigmedium-elements  " ID="lbl_fechacorte"></asp:Label>
                                <asp:TextBox ID="txt_fechacorte" runat="server" CssClass="module_subsec_elements flex_1 text_input_nice_input" AutoPostBack="True"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechacorte" runat="server"
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechacorte" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechacorte" runat="server"
                                    ControlExtender="MaskedEditExtender_fechacorte" ControlToValidate="txt_fechacorte"
                                    InvalidValueMessage="Fecha inválida" ValidationGroup="val_Personales" CssClass="textogris"
                                    ErrorMessage="MaskedEditValidator_fechacorte" Display="Dynamic" />
                                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="txt_fechacorte"
                                    Format="dd/MM/yyyy" />
                            </div>
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label runat="server" Text="Monto para liquidar el préstamo:" CssClass="module_subsec_elements module_subsec_bigmedium-elements"
                                    ID="lbl_monto_liq"></asp:Label>
                                <asp:TextBox runat="server" Enabled="false" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_monto_liq_txt"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_info_fecha_corte" runat="server" CssClass="alerta"
                            Text="No elegir una fecha anterior a la fecha de sistema. Verifique." Visible="false" />
                    </div>

                    <div class="module_subsec low_m">
                        <div class="shadow w_100">
                            <asp:GridView ID="grd_pago" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">

                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundField DataField="CUENTA" HeaderText="Cuenta" SortExpression="CUENTA" />
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                                    <asp:BoundField DataField="CARGO" HeaderText="Cargo" SortExpression="CARGO" />
                                    <asp:BoundField DataField="ABONO" HeaderText="Abono" SortExpression="ABONO" />
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Tipos de pago</span>
        </header>
        <div class="panel-body">

            <h4 class="module_subsec no_tm">Efectivo</h4>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_cajaori" CssClass="text_input_nice_input" runat="server" Enable="False" MaxLength="12"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_cajaori" runat="server" CssClass="text_input_nice_label" Text="Total efectivo:"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cajaori" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_cajaori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_cajaori" runat="server"
                                class="textogris" ControlToValidate="txt_cajaori" ErrorMessage=" Monto incorrecto!"
                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>

            <h4 class="module_subsec">Depósitos/Transferencias</h4>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_bancoori" CssClass="btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                            ValidationGroup="val_bancosori">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_bancoori" runat="server" CssClass="text_input_nice_label" Text="*Banco:"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancosori"
                                CssClass="textogris" ControlToValidate="cmb_bancoori" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_bancosori" InitialValue="-1" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_montobancosori" CssClass="text_input_nice_input" runat="server" ValidationGroup="val_bancosori"
                            Enabled="False" MaxLength="12"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_montobancosori" runat="server" CssClass="text_input_nice_label" Text="*Monto:"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_montobancosori"
                                runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_montobancosori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_montobancosori" runat="server"
                                class="textogris" ControlToValidate="txt_montobancosori" Display="Dynamic" ErrorMessage=" Monto incorrecto!"
                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtmontobancosori"
                                CssClass="textogris" ControlToValidate="txt_montobancosori" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosori" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_origen_dep" CssClass="btn btn-primary2 dropdown_label" runat="server"
                            AutoPostBack="true" Enabled="false">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_tit_origen_dep" runat="server" CssClass="text_input_nice_label" Text="*Origen: "></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo_origen_dep" CssClass="textogris" ControlToValidate="cmb_origen_dep"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosori" InitialValue="-1" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnl_forma_transferencia" runat="server" Visible="false">
                <div class="module_subsec columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_banco_des_dep" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                ValidationGroup="val_bancosori">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="Label1" runat="server" CssClass="text_input_nice_label" Text="*Banco afiliado:"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="textogris" ControlToValidate="cmb_banco_des_dep"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosori" InitialValue="-1" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_cta_dep" CssClass="text_input_nice_input" runat="server"
                                ValidationGroup="val_bancosori" MaxLength="18"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_tit_dep_origen" runat="server" CssClass="text_input_nice_label" Text=""></asp:Label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_cta_dep">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_cta_dep" runat="server" ControlToValidate="txt_cta_dep"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosori" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_titular_cta_dep" CssClass="text_input_nice_input" runat="server"
                                ValidationGroup="val_bancosori" MaxLength="100"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="Label4" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Titular cuenta:"></asp:Label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_titular_cta_dep" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" runat="server" Enabled="True" TargetControlID="txt_titular_cta_dep">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_infobanco" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_agregarbancosori" runat="server" class="btn btn-primary" Text="Agregar"
                    ValidationGroup="val_bancosori" Enabled="False" />
            </div>

            <div class="module_subsec">
                <div class="overflow_x shadow flex_1" style="flex: 1;">
                    <asp:DataGrid ID="dag_bancosori" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CTA" HeaderText="Banco" Visible="False">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BANCO" HeaderText="Banco"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                <ItemStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                <ItemStyle Width="15%" ForeColor="#054B66" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>

            <div runat="server" id="div_pnl_ctascapori" visible="false">
                <h4 class="module_subsec no_tm">
                    <asp:Label ID="lbl_ctascapori" runat="server" CssClass="textonegritas" Text="Cuentas"></asp:Label></h4>
                <asp:Panel ID="pnl_ctascapori" runat="server" Enabled="False"></asp:Panel>
            </div>

            <h4 class="module_subsec no_tm">Cheques</h4>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_bancochequesori" CssClass="btn btn-primary2 dropdown_label"
                            runat="server" Enabled="False" ValidationGroup="val_chequesori">
                        </asp:DropDownList>
                        <div class="text_innput_nice_labels">
                            <asp:Label runat="server" ID="lbl_bancochequesori" class="text_input_nice_label">Banco emisor:</asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancochequesori"
                                CssClass="textogris" ControlToValidate="cmb_bancochequesori" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" InitialValue="-1" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_ctachequesori" CssClass="text_input_nice_input" runat="server" Enabled="False"
                            ValidationGroup="val_chequesori" MaxLength="20"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_ctachequesori" class="text_input_nice_label">Número de cuenta emisora:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ctachequesori" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_ctachequesori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ctachequesori"
                                CssClass="textogris" ControlToValidate="txt_ctachequesori" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_numchequesori" CssClass="text_input_nice_input" runat="server" Enabled="False"
                            ValidationGroup="val_chequesori" MaxLength="2"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_numchequesori" class="text_input_nice_label">Número de cheque:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_numchequesori" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_numchequesori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_numchequesori"
                                CssClass="textogris" ControlToValidate="txt_numchequesori" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_montochequesori" CssClass="text_input_nice_input" runat="server" Enabled="False"
                            ValidationGroup="val_chequesori" MaxLength="12"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_montochequesori" class="text_input_nice_label">Monto:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_montochequesori"
                                runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_montochequesori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_montochequesori" runat="server" Display="Dynamic"
                                class="textogris" ControlToValidate="txt_montochequesori" ErrorMessage=" Monto incorrecto!"
                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_montochequesori"
                                CssClass="textogris" ControlToValidate="txt_montochequesori" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_modo_rec" CssClass="btn btn-primary2 dropdown_label" runat="server" ValidationGroup="val_chequesori" Enabled="False">
                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                            <asp:ListItem Value="SBC">SALVO BUEN COBRO</asp:ListItem>
                            <asp:ListItem Value="COB">EN FIRME</asp:ListItem>
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_modo_rec" class="text_input_nice_label">Tipo de recepción de cheque:</asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="rfv_modo_rec"
                                CssClass="textogris" ControlToValidate="cmb_modo_rec" Display="Dynamic"
                                ErrorMessage="Falta Dato!" ValidationGroup="val_chequesori" InitialValue="-1" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_infocheques" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_agregarchequesori" runat="server" class="btn btn-primary" Text="Agregar"
                    ValidationGroup="val_chequesori" Enabled="False" />
            </div>

            <div class="module_subsec">
                <div class="overflow_x shadow flex_1" style="flex: 1;">
                    <asp:DataGrid ID="dag_chequesori" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CTA" Visible="False">
                                <ItemStyle Width="50px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_BANCO" Visible="False">
                                <ItemStyle Width="350px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BANCO" HeaderText="Banco"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Cuenta">
                                <ItemStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHEQUE" HeaderText="Número">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTATUS" Visible="False">
                                <ItemStyle Width="50px" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                <ItemStyle Width="10%" ForeColor="#054B66" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>

        </div>

    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Referencia</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec low_m columned four_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_nombre1" runat="server" class="text_input_nice_input" MaxLength="300" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_nombre1" class="text_input_nice_label"> Primer nombre:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombres1" runat="server"
                                TargetControlID="txt_nombre1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_nombre2" runat="server" class="text_input_nice_input" MaxLength="300"
                            ValidationGroup="val_personales" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_nombre2" class="text_input_nice_label">Segundo(s) nombre(s):</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre2" runat="server"
                                TargetControlID="txt_nombre2" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_paterno" runat="server" class="text_input_nice_input" MaxLength="100"
                            ValidationGroup="val_personales" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_paterno" class="text_input_nice_label"> Apellido paterno:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server"
                                TargetControlID="txt_paterno" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_materno" runat="server" class="text_input_nice_input" MaxLength="100" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" ID="lbl_materno" class="text_input_nice_label"> Apellido materno:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txt_materno" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m">
                <div class="module_subsec_elements w_100">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea" Height="50px" MaxLength="3000"
                            TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 3000);"></asp:TextBox>
                        <asp:Label ID="lbl_notas" runat="server" class="text_input_nice_label">Notas:</asp:Label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_notas" runat="server" Enabled="True"
                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                            TargetControlID="txt_notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

    <div class="module_subsec flex_center">

        <asp:Button ID="btn_pagar" runat="server" class="btn btn-primary" Text="Abonar"
            Enabled="False" OnClientClick="btn_pagar.disabled = true; btn_pagar.value = 'Procesando...';" />

    </div>

    <asp:Panel ID="pnl_ide" runat="server" Align="Center" Width="356px" CssClass="modalPopup" Style='display: none;'>
        <asp:Panel ID="pnl_Titulo" runat="server" Align="Center" Height="20px" CssClass="titulosmodal">
            <asp:Label ID="lbl_tit" runat="server" class="subtitulosmodal" Text="Retención IDE"
                ForeColor="White" />
        </asp:Panel>
        <div class="module_subsec flex_center">
            <asp:Label ID="lbl_aviso_ide" runat="server" class="subtitulos" Text="Este depósito será motivo de retención de IDE, ¿desea continuar?" />
            <br />
            <asp:Button ID="btn_ok" runat="server" class="btn btn-primary" Text="Aceptar" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text="Cancelar" />
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_ide" PopupDragHandleControlID="pnl_Titulo"
        TargetControlID="hdn_ide" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnl_AutorPLD" runat="server" Align="Center" Width="600px" CssClass="modalPopup" Style='display: none;'>
        <asp:Panel ID="pnl_AutorPLD_Titulo" runat="server" Align="Center" Height="20px" CssClass="titulosmodal">
            <asp:Label ID="lbl_AutorPLD_Titulo" runat="server" class="subtitulosmodal" Text="Autorización de Movimiento" ForeColor="White" />
        </asp:Panel>
        <div class="module_subsec flex_center">
            <br />
            <asp:Label ID="lbl_AutorPLD" runat="server" class="subtitulos" Text="Ingrese datos del usuario que autoriza la operación." />
            <br />
            <asp:LinkButton ID="lnk_ACT" runat="server" CssClass="textogris" Text="Verificar Autorización Remota" ToolTip="Permite revisar la autorización de un usuario externo."></asp:LinkButton>
            <br />
            <ajaxToolkit:CollapsiblePanelExtender ID="cpe_lst" runat="server"
                CollapseControlID="hp_lst" Collapsed="True" CollapsedImage="~/img/expand.jpg"
                CollapsedText="Expandir" ExpandControlID="hp_lst"
                ExpandedImage="~/img/collapse.jpg" ExpandedText="Contraer" ImageControlID="ToggleImage_dirfis"
                SuppressPostBack="True" TargetControlID="cp_lst">
            </ajaxToolkit:CollapsiblePanelExtender>
            <asp:Panel ID="hp_lst" runat="server" Style="cursor: pointer;">
                <div class="texto">
                    <asp:ImageButton ID="ToggleImage_dirfis" runat="server" />
                    Usuarios autorizados
                </div>
            </asp:Panel>
            <asp:Panel ID="cp_lst" runat="server" Style="overflow: hidden;">
                <br />
                <asp:DataGrid ID="dag_lst" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    GridLines="None" CssClass="table table-striped">
                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="NAME" HeaderText="Usario">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SUC" HeaderText="Sucursal">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMBER" HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </asp:Panel>
            <br />
            <table width="550px">
                <tr>
                    <td width="100px" align="left">
                        <asp:Label ID="lbl_IDAutor" runat="server" Text="Num. Autorización" class="texto"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_IDAutor2" runat="server" class="texto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DataGrid ID="dag_cheques_aut" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            GridLines="None" Width="500px" Visible="False">
                            <AlternatingItemStyle CssClass="AlternativeStyle" />
                            <SelectedItemStyle CssClass="SelectedItemStyle" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <ItemStyle CssClass="ItemStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CHEQUE" HeaderText="Num. Cheque">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left">
                        <asp:Label ID="lbl_Usr" runat="server" Text="Usuario:" class="texto"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_Usr" runat="server" class="textocajas" MaxLength="15" Width="100px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator_Usr" runat="server" ControlToValidate="txt_Usr"
                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_Autorizar">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left">
                        <asp:Label ID="lbl_Pwd" runat="server" class="texto" Text="Contraseña:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_Pwd" runat="server" class="textocajas" MaxLength="15" TextMode="Password" Width="100px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator_Pwd" runat="server" ControlToValidate="txt_Pwd"
                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_Autorizar">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_Acc" runat="server" class="texto" Text="Acción:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="cmb_Acc" runat="server" class="textocajas">
                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                            <asp:ListItem Value="CANCELADO">CANCELAR</asp:ListItem>
                            <asp:ListItem Value="RECHAZADO">RECHAZAR</asp:ListItem>
                            <asp:ListItem Value="AUTORIZADO">AUTORIZAR</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Acc" CssClass="textogris"
                            ControlToValidate="cmb_Acc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_Autorizar" InitialValue="0" />
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left" valign="top">
                        <asp:Label ID="lbl_Mtv" runat="server" class="texto" Text="Motivo:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_Mtv" runat="server" class="textocajas" MaxLength="200" Width="350px" Height="75px" TextMode="MultiLine"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mtv" runat="server" Enabled="True"
                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                            TargetControlID="txt_Mtv" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Mtv" runat="server" ControlToValidate="txt_Mtv"
                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Autorizar">
                            </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_InfoAutoriza" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
            <br />
            <asp:Button ID="btn_AutorPLD_AUTO" runat="server" class="btn btn-primary" Text="Aplicar" ValidationGroup="val_Autorizar" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_AutorPLD_CAN" runat="server" class="btn btn-primary" Text="Cancelar" />
            <br />
            <asp:Button ID="btn_AUTOCerrar" runat="server" BackColor="White" BorderColor="#054B66"
                BorderWidth="2px" class="btntextoazul" Height="19px" Text="Cerrar" Width="74px" Visible="false" />
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_AutorPLD" PopupDragHandleControlID="pnl_AutorPLD_Titulo"
        TargetControlID="hdn_AutorPLD" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>

    <br />
    <asp:Panel ID="pnl_recalculo" runat="server" Align="Center" Width="356px" CssClass="modalPopup" Style='display: none;'>
        <asp:Panel ID="pnl_avisa_recalculo" runat="server" Align="Center" Height="25px" CssClass="titulosmodal">
            <asp:Label ID="lbl_avisa" runat="server" class="subtitulosmodal" Text="AMORTIZACIÓN A CAPITAL"
                ForeColor="White" />
        </asp:Panel>

        <div align="center">
            <br />
            <asp:Label ID="lbl_avisa_2" runat="server" class="subtitulos" Text="Si desea recalcular plan de pagos" />
            <br />
            <asp:Label ID="lbl_avisa_3" runat="server" class="subtitulos" Text="favor de acudir con un ejecutivo" />
            <br />
            <asp:Label ID="lbl_avisa_4" runat="server" class="subtitulos" Text="antes de cerrar día." />
            <br />
            <asp:Button ID="btn_aceptar" runat="server" class="btn btn-primary" Text="Aceptar" />
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_recalculo" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_recalculo" PopupDragHandleControlID="pnl_avisa_recalculo"
        TargetControlID="hdn_recalculo" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>

    <asp:HiddenField ID="HiddenPrinterName" runat="server" />
    <asp:HiddenField ID="HiddenRawData" runat="server" />
    <input type="hidden" name="hdn_ide" id="hdn_ide" runat="server" />
    <input type="hidden" name="hdn_AutorPLD" id="hdn_AutorPLD" runat="server" />
    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" runat="server" value="" />
    <input type="hidden" name="hdn_recalculo" id="hdn_recalculo" runat="server" />

</asp:Content>

