<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_VEN_DEPRET.aspx.vb" Inherits="SNTE5.CAP_VEN_DEPRET" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px");
            if (wbusf != null) {

                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }

        //function ClickBotonBusqueda(ControlTextbox, ControlButton) {
        //    var CTextbox = document.getElementById(ControlTextbox);
        //    var CButton = document.getElementById(ControlButton);
        //    if (CTextbox != null && CButton != null) {
        //        if (event.keyCode == 13) {
        //            event.returnValue = false;
        //            event.cancel = true;
        //            if (CTextbox.value != "") {
        //                CButton.click();
        //                CButton.disabled = true;
        //            }
        //            else {
        //                //alert('Ingrese Datos')
        //                CTextbox.focus();
        //                return false;
        //            }
        //            //CTextbox.focus();
        //            return true;
        //        }
        //    }
        //}
    </script>

    <object id="Printer" classid="CLSID:402C09CD-68ED-48B0-B008-E7B01DDBD2D5" codebase="RawDataPrinter.CAB#version=2,0,0,0">
    </object>

    <section class="panel">
        <header class="panel-heading">
            <span>Afiliado</span>
        </header>
        <div class="panel-body">
            <!-- Información de afiliado -->
            <div class="module_subsec columned three_columns">
                <div class="module_subsec_elements">
                    <!-- Afiliado -->
                    <div class="module_subsec_elements_content">
                        <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements no_m title_tag" ID="lbl_numcliente">Número de afiliado: </asp:Label>
                        <br />
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input" ValidationGroup="val_cliente"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cliente" CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic"
                            ErrorMessage=" Falta Dato!" ValidationGroup="val_cliente" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cliente" runat="server" FilterType="Numbers" TargetControlID="txt_cliente">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                            Style="height: 16px" Height="16px"></asp:ImageButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="btn_seleccionar" runat="server" CssClass="btntextoazul" Text="Seleccionar" ValidationGroup="val_cliente" Enabled="false" />
                        <br />
                        <br />
                        <asp:Label ID="lbl_cliente" runat="server" CssClass="low_m"></asp:Label>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <!-- Operación -->
                    <div class="module_subsec flex_center">
                        <asp:Panel ID="pnl_operacion" runat="server" Visible="True">
                            <asp:Label ID="lbl_depret" runat="server" CssClass="texto" Text="Seleccione la operación:" Visible="False"></asp:Label>
                            <br />
                            <h4 style="font-weight: normal" class="">
                                <asp:RadioButton ID="rad_deposito" runat="server" Text="Depósito" GroupName="operacion" AutoPostBack="True" Visible="false" />
                                &nbsp;&nbsp;
                                 <asp:RadioButton ID="rad_retiro" runat="server" Text="Retiro" CssClass="texto" GroupName="operacion" AutoPostBack="True" Visible="false" /></h4>
                        </asp:Panel>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <%--<div class="module_subsec columned no_m align_items_flex_center">--%>
                    <!--<div class="module_subsec_elements align_items_flex_end"> -->
                        <!-- Reportar operación -->
                        <asp:LinkButton ID="lnk_RepOp" runat="server" CssClass="btntextoazul" Text="Reportar operación" Enabled="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnk_PerAut" runat="server" CssClass="btntextoazul" Text="Personas autorizadas" Enabled="False" />
                        <br />
                        <br />
                        <br />
                        <br />
                    <%--</div>--%>
                </div>
            </div>

            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements">
                    <!-- Quien realiza la operación -->
                    <div class="module_subsec_elements_content">
                        <asp:Panel ID="pnl_Usuario" runat="server" Visible="False">
                            <asp:Label ID="lbl_accion" runat="server" CssClass="texto" Text="*Indique quién realiza la acción:"></asp:Label>
                            <br />
                            <h4 style="font-weight: normal" class="">
                                <asp:RadioButton ID="rad_cliente" runat="server" Text="Cliente" CssClass="texto"
                                    GroupName="operaciones" AutoPostBack="True" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                      
                                        <asp:RadioButton ID="rad_usuario" runat="server" Text="Usuario" CssClass="texto" GroupName="operaciones"
                                            AutoPostBack="True" /></h4>
                        </asp:Panel>
                    </div>
                </div>
            </div>

             <div runat="server" id="pnl_usuario_pf" visible="false">
            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="HeaderPanel_busqueda"
                Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                Enabled="True" ExpandControlID="HeaderPanel_busqueda" ExpandedImage="~/img/collapse.jpg"
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
                            <asp:ImageButton ID="btn_buscapersona_ori" runat="server" ImageUrl="~/img/glass.png" Style="height: 18px; width: 18px;" />
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
                Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
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
                        <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png"
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


                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_ife" runat="server" CssClass="alerta" />
                    <asp:Label ID="Label6" CssClass="alerta" Visible="True" runat="server" />
                    <asp:Label ID="lbl_info_disp" runat="server" CssClass="alerta" Visible="True" />
                </div>
            </asp:Panel>

        </div>
        </div>

        <%--            <!-- Operación a realizar (Depósito/Retiro) -->
            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <ul>
                        <li>
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_blanco1" />
                        </li>
                        <li>
                            <asp:UpdatePanel ID="upnl_tipo_operacion" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="semaforo_barra">
                                        <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Depósito" ID="lnk_to_deposito">
                                            <span id="Semaforo1_r" class="semaforo_img alto" runat="server" visible="true" />
                                            <span id="Semaforo1_v" class="semaforo_img prosiga" runat="server" visible="false" />
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Retiro" ID="lnk_to_retiro">
                                            <span id="Semaforo2_v" class="semaforo_img prosiga" runat="server" visible="false" />
                                            <span id="Semaforo2_r" class="semaforo_img alto" runat="server" visible="true" />
                                        </asp:LinkButton>
                                    </div>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </div>
                <!-- Quien realiza la operación -->
                <div class="module_subsec_elements align_items_flex_end">
                    <ul>
                        <li>
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_quien_realiza">Quien realiza la operación: </asp:Label>
                        </li>
                        <li>
                            <asp:UpdatePanel ID="upnl_quien_realiza" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="semaforo_barra">
                                        <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Cliente" ID="lnk_to_cliente">
                                            <span id="Span1" class="semaforo_img alto" runat="server" visible="true" />
                                            <span id="Span2" class="semaforo_img prosiga" runat="server" visible="false" />
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Usuario" ID="lnk_to_usuario">
                                            <span id="Span3" class="semaforo_img prosiga" runat="server" visible="false" />
                                            <span id="Span4" class="semaforo_img alto" runat="server" visible="true" />
                                        </asp:LinkButton>
                                    </div>
                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>

               </div>
            </div>--%>
    </section>

    <!-- Label de Información -->
    <div class="module_subsec flex_center" id="div_Informacion_encabezado">
        <asp:Label ID="lbl_info" runat="server" CssClass="textogris" ForeColor="Red" Visible="True" />
    </div>

    <section class="panel" runat="server" id="pnl_origenDeposito">
        <header class="panel-heading">
            <span>Origen</span>
        </header>
        <div class="panel-body">

            <%--<div class= "module_subsec low_m columned two_columns align_items_flex_start">--%>
            <div class="module_subsec_elements vertical">

                <div class="module_subsec low_m columned two_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_cajaori" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enable="False" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_cajaori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Efectivo:"></asp:Label>

                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cajaori" runat="server"
                                    Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_cajaori">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_cajaori" Display="Dynamic" Style="margin-left: 15px;" runat="server"
                                    CssClass="resalte_rojo module_subsec_elements" ControlToValidate="txt_cajaori" ErrorMessage=" Monto incorrecto!"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>



                <asp:Label ID="lbl_ctascapori" runat="server" CssClass="module_subsec no_sm" Text="Cuentas" Visible="False"></asp:Label>

                <div class="module_subsec low_m columned two_columns" id="div_ctascapori" runat="server" visible="True">
                    <div class="module_subsec_elements">
                        <asp:Panel ID="pnl_ctascapori" runat="server" Visible="False">
                        </asp:Panel>
                    </div>
                </div>

                <asp:UpdatePanel ID="updpnl_bancosori" runat="server">
                    <ContentTemplate>
                        <h5 style="font-weight: normal" class="">Depósitos/Transferencias </h5>

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
                                        AutoPostBack="true" Enabled="False">
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
                                            <asp:Label ID="Label3" runat="server" CssClass="text_input_nice_label" Text="*Banco Afiliado:"></asp:Label>
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
                                            <asp:Label ID="Label4" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Titular Cuenta:"></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_titular_cta_dep" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" runat="server" Enabled="True" TargetControlID="txt_titular_cta_dep">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_infobancoori" runat="server" CssClass="alerta"></asp:Label>
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
                                        <asp:BoundColumn DataField="ID_CTA" HeaderText="Id" Visible="False">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ORIGEN" HeaderText="Origen">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO_DESTINO" HeaderText="Banco afiliado">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_CTA_DESTINO" HeaderText="Cta. afiliado">
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
                                <asp:Label ID="Label1" runat="server" CssClass="textonegritas" Text="Cuentas"></asp:Label></h4>
                            <asp:Panel ID="Panel1" runat="server" Enabled="False"></asp:Panel>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="updpnl_chequesori" runat="server">
                    <ContentTemplate>
                        <h5 style="font-weight: normal" class="">Cheques </h5>

                        <div class="module_subsec columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_bancochequesori" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesori">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_bancochequesori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Banco emisor:"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancochequesori"
                                            CssClass="textogris" ControlToValidate="cmb_bancochequesori" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>


                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_ctachequesori" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesori" MaxLength="20"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_ctachequesori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Número de cuenta emisora:"></asp:Label>
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
                                    <asp:TextBox ID="txt_numchequesori" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesori" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_numchequesori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Número de cheque:"></asp:Label>

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

                        <div class="module_subsec columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_montochequesori" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesori" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_montochequesori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Monto:"></asp:Label>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_montochequesori"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_montochequesori">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_montochequesori" runat="server"
                                            class="textogris" ControlToValidate="txt_montochequesori" ErrorMessage=" Monto incorrecto!"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_montochequesori"
                                            CssClass="textogris" ControlToValidate="txt_montochequesori" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" />

                                    </div>
                                    <asp:Label ID="lbl_infochequesori" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_modo_rec" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server" ValidationGroup="val_chequesori" Enabled="false">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="SBC">SALVO BUEN COBRO</asp:ListItem>
                                        <asp:ListItem Value="COB">EN FIRME</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_modo_rec" class="module_subsec_elements module_subsec_small-elements title_tag">Tipo de recepción de cheque:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="rfv_modo_rec"
                                            CssClass="textogris" ControlToValidate="cmb_modo_rec" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agregarchequesori" runat="server" class="btn btn-primary" Text="Agregar"
                                ValidationGroup="val_chequesori" Enabled="False" />
                        </div>

                        <div class="overflow_x shadow module_subsec" runat="server" id="div_dag_chequesori" visible="false">
                            <asp:DataGrid ID="dag_chequesori" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CTA" Visible="False">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_BANCO" Visible="False">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Cuenta">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CHEQUE" HeaderText="Número">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESTATUS" Visible="False">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>


        </div>
    </section>

    <section class="panel" runat="server" id="pnl_origenRetiro">
        <div class="module_subsec low_m columned two_columns" id="div_cmbctascapori" runat="server" visible="false">
            <div class="module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:DropDownList ID="cmb_ctascapori" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server"
                        Visible="true" AutoPostBack="true">
                    </asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <asp:Label ID="lbl_cmbctascapori" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag"
                            Text="Cuenta:" Visible="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="pnl_destinoDeposito">
        <div id="div_cmbctascapdes" runat="server" class="module_subsec low_m columned two_columns" visible="false">
            <div class="module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:DropDownList ID="cmb_ctascapdes" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server"
                        Visible="true" AutoPostBack="true">
                    </asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <asp:Label ID="lbl_cmbctascapdes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Cuenta:"
                            Visible="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="pnl_destinoRetiro">
        <header class="panel-heading">
            <span>Destino</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec_elements vertical">

                <div id="div_cajades" class="module_subsec low_m columned two_columns" visible="false">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_cajades" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enable="False" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_cajades" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Efectivo:"></asp:Label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cajades" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txt_cajades">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_cajades" runat="server" class="textogris" ControlToValidate="txt_cajades"
                                    ErrorMessage=" Monto incorrecto!" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>



                <asp:Label ID="lbl_inv_per" runat="server" CssClass="texto" Text="<br />Seleccione inversión:" Visible="False"></asp:Label>
                <asp:DropDownList ID="cmb_inv_per" CssClass="textocajas" runat="server" Visible="False"></asp:DropDownList>

                <asp:Label ID="lbl_ctascapdes" runat="server" CssClass="textonegritas" Text="Cuentas" Visible="False"></asp:Label>
                <div class="module_subsec low_m columned two_columns" id="div_ctascapdes" runat="server" visible="True">
                    <div class="module_subsec_elements">
                        <asp:Panel ID="pnl_ctascapdes" runat="server" Visible="False"></asp:Panel>
                    </div>
                </div>

                <asp:UpdatePanel ID="updpnl_bancosdes" runat="server">
                    <ContentTemplate>
                        <h5 style="font-weight: normal" class="">Depósitos/Transferencias </h5>

                        <div class="module_subsec columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_bancodes" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                                        ValidationGroup="val_bancosdes">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_bancodes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Banco:"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancodes" CssClass="textogris" ControlToValidate="cmb_bancodes"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosdes" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_montobancodes" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_bancosdes" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_montobancodes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Monto:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_montobancodes" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_montobancodes">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_montobancodes" runat="server"
                                            class="textogris" ControlToValidate="txt_montobancodes" ErrorMessage=" Monto incorrecto!"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtmontobancodes" CssClass="textogris"
                                            ControlToValidate="txt_montobancodes" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosdes" />
                                    </div>
                                    <asp:Label ID="lbl_infobancodes" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_bancodes_des" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                                        ValidationGroup="val_bancosdes">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_bancodes_des" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Banco Destino:"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancodes_des"
                                            CssClass="textogris" ControlToValidate="cmb_bancodes_des" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_bancosdes" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="module_subsec columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_clabe_banco_des" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_bancosdes" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_clabe_banco_des" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Clabe:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_clabe_banco_des"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_clabe_banco_des">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txt_clabe_banco_des"
                                            CssClass="textogris" ControlToValidate="txt_clabe_banco_des" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_bancosdes" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_clabe_banco_des" runat="server" class="textogris" ControlToValidate="txt_clabe_banco_des"
                                            ErrorMessage="No contiene 18 digitos!" ValidationExpression="^[0-9]{18}?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_cta_banco_des" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_bancosdes" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_cta_banco_des" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Núm. Cuenta:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_cta_banco_des"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_cta_banco_des">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agreagrbancosdes" runat="server" class="btn btn-primary" Text="Agregar"
                                ValidationGroup="val_bancosdes" Enabled="False" CausesValidation="True" />
                        </div>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_bancosdes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CTA" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_BANCO_CLIENTE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO_CLIENTE" HeaderText="Banco Destino">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CLABE" HeaderText="Clabe">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUM_CTA_CLIENTE" HeaderText="Núm. Cuenta">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle ForeColor="#054B66" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="updpnl_chequesdes" runat="server">
                    <ContentTemplate>
                        <h5 style="font-weight: normal" class="">CHEQUES </h5>

                        <div class="module_subsec low_m columned two_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_bancochequesdes" CssClass="module_subsec_elements module_subsec_medium-elements btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesdes">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_bancochequesdes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Banco Emisor:"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancochequesdes"
                                            CssClass="textogris" ControlToValidate="cmb_bancochequesdes" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesdes" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned two_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_numchequesdes" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesdes" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_numchequesdes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Número de cheque:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtnumchequesdes"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_numchequesdes">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtnumchequesdes"
                                            CssClass="textogris" ControlToValidate="txt_numchequesdes" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesdes" />
                                    </div>
                                </div>
                            </div>
                            <%--                            </div>

                            <div class="module_subsec low_m columned two_columns">--%>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_montochequesdes" CssClass="module_subsec_elements module_subsec_medium-elements text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_chequesdes" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_montochequesdes" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="Monto:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtmontochequesdes"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_montochequesdes">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_txtmontochequesdes"
                                            runat="server" class="textogris" ControlToValidate="txt_montochequesdes" ErrorMessage=" Monto incorrecto!"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtmontochequesdes"
                                            CssClass="textogris" ControlToValidate="txt_montochequesdes" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesdes" />
                                    </div>
                                    <asp:Label ID="lbl_infochequesdes" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agregarchequesdes" runat="server" class="btn btn-primary" Text="Agregar"
                                ValidationGroup="val_chequesdes" Enabled="False" />
                        </div>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_chequesdes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CTA" Visible="False">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_BANCO" Visible="False">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Cuenta" Visible="False">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CHEQUE" HeaderText="Número">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle ForeColor="#054B66" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <%--</div>--%>
        </div>
    </section>

    <section class="panel" runat="server" id="pnl_referencia">
        <header class="panel-heading">
            <span>Referencia</span>
        </header>

        <div class="panel-body">
            <div class="module_subsec low_m columned four_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_nombre1" runat="server" class="text_input_nice_input" MaxLength="300" />
                        <div class="text_input_nice_labels">
                            <label id="lbl_nombre1" class="text_input_nice_label">Primer nombre:</label>
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
                            <label id="lbl_nombre2" class="text_input_nice_label">Segundo(s) nombre(s):</label>
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
                            <label id="lbl_paterno" class="text_input_nice_label">Apellido paterno:</label>
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
                            <label id="lbl_materno" class="text_input_nice_label">Apellido materno:</label>
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
        <asp:Label ID="lbl_info0" runat="server" CssClass="textogris" ForeColor="Red" />
        <asp:Label ID="lbl_info_ide" CssClass="textogris" ForeColor="Red" runat="server" Visible="False" />
    </div>

    <div class="module_subsec flex_end">
        <asp:CheckBox ID="chk_pdf" runat="server" CssClass="texto" Text="Guardar Ticket en Archivo" />
        <br />
        <asp:Button ID="btn_aplicar" runat="server" class="module_subsec vertical btn btn-primary" Text="Aplicar"
            Enabled="False" OnClientClick="btn_aplicar.disabled = true; btn_aplicar.value = 'Procesando...';" />
        <br />

    </div>

    <asp:Panel ID="pnl_ide" runat="server" Align="Center" Width="356px" CssClass="modalPopup" Style='display: none;'>
        <asp:Panel ID="pnl_Titulo" runat="server" Align="Center" Height="20px" CssClass="titulosmodal">
            <asp:Label ID="lbl_tit" runat="server" class="subtitulosmodal" Text="Retención IDE"
                ForeColor="White" />
        </asp:Panel>
        <div align="center">
            <asp:Label ID="lbl_aviso_ide" runat="server" class="subtitulos" Text="Este deposito será motivo de retencion de IDE, desea continuar?" />
            <br />
            <br />
            <asp:Button ID="btn_ok" runat="server" BackColor="White" BorderColor="#054B66" BorderWidth="2px"
                class="btntextoazul" Height="19px" Text="Aceptar" Width="74px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_cancel" runat="server" BackColor="White" BorderColor="#054B66"
                    BorderWidth="2px" class="btntextoazul" Height="19px" Text="Cancelar" Width="74px" />
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_ide" PopupDragHandleControlID="pnl_Titulo"
        TargetControlID="hdn_ide" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnl_AutorPLD" runat="server" Align="Center" Width="600px" CssClass="modalPopup" Style='display: none;'>
        <asp:Panel ID="pnl_AutorPLD_Titulo" runat="server" Align="Center" Height="20px" CssClass="titulosmodal">
            <asp:Label ID="lbl_AutorPLD_Titulo" runat="server" class="subtitulosmodal" Text="Autorización de Movimiento"
                ForeColor="White" />
        </asp:Panel>
        <div align="center">
            <br />
            <asp:Label ID="lbl_AutorPLD" runat="server" class="subtitulos" Text="Ingrese datos del usuario que autoriza la operación." />
            <br />
            <br />
            <asp:LinkButton ID="lnk_ACT" runat="server" CssClass="textogris" Text="Verificar Autorización Remota" ToolTip="Permite revisar la autorización de un usuario externo."></asp:LinkButton>
            <br />
            <br />
            <ajaxToolkit:CollapsiblePanelExtender ID="cpe_lst" runat="server"
                CollapseControlID="hp_lst" Collapsed="True" CollapsedImage="~/sysimages/expand.jpg"
                CollapsedText="Expandir" ExpandControlID="hp_lst"
                ExpandedImage="~/sysimages/collapse.jpg" ExpandedText="Contraer" ImageControlID="ToggleImage_dirfis"
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
                <div class="overflow_x shadow module_subsec">
                    <asp:DataGrid ID="dag_lst" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
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
                </div>
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
                        <asp:DataGrid ID="dag_cheques_aut" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CHEQUE" HeaderText="Num. Cheque">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="30%" HorizontalAlign="Center" />
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
                        &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Acc" CssClass="textogris"
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
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator_Mtv" runat="server" ControlToValidate="txt_Mtv"
                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_Autorizar">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_InfoAutoriza" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btn_AutorPLD_AUTO" runat="server" BackColor="White" BorderColor="#054B66"
                BorderWidth="2px" class="btntextoazul" Height="19px" Text="Aplicar" Width="74px" ValidationGroup="val_Autorizar" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_AutorPLD_CAN" runat="server" BackColor="White" BorderColor="#054B66"
                        BorderWidth="2px" class="btntextoazul" Height="19px" Text="Cancelar" Width="74px" />
            <br />
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

    <asp:HiddenField ID="HiddenPrinterName" runat="server" />
    <asp:HiddenField ID="HiddenRawData" runat="server" />
    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
    <input type="hidden" name="hdn_ide" id="hdn_ide" runat="server" />
    <input type="hidden" name="hdn_AutorPLD" id="hdn_AutorPLD" runat="server" />
</asp:Content>
