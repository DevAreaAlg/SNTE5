<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_BENEFICIARIOS.aspx.vb" Inherits="SNTE5.CAP_EXP_BENEFICIARIOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--FUNCION DE BUSQUEDA DE PERSONA FISICA--%>
    <script type="text/javascript">

        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
        function busquedaCP() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }

        function ClickBotonBusqueda(ControlTextbox, ControlButton) {
            var CTextbox = document.getElementById(ControlTextbox);
            var CButton = document.getElementById(ControlButton);
            if (CTextbox != null && CButton != null) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    if (CTextbox.value != "") {
                        CButton.click();
                        CButton.disabled = true;
                    }
                    else {
                        //alert('Ingrese Datos')
                        CTextbox.focus()
                        return false
                    }
                    //CTextbox.focus();
                    return true
                }
            }
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_avales">
        <header class="panel_header_folder panel-heading">
            <span>Beneficiario existente</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>


                        <asp:Label ID="lbl_beneficiario" runat="server" CssClass="textogris module_subsec"></asp:Label>

                        <asp:LinkButton ID="lnk_copiar_beneficiario" runat="server" CssClass="textogris module_subsec" Text="Copiar beneficiarios" Visible="false"></asp:LinkButton>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_BEN" AutoGenerateColumns="False" CssClass="table table-striped"
                               runat="server" GridLines="None" Width="100%" >
                                <Columns>
                                    <asp:BoundColumn DataField="cvebeneficiario" HeaderText="Id beneficiario" >
                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="porcentaje" HeaderText="Porcentaje">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header" />

                            </asp:DataGrid>
                        </div>

                        <p align="center">
                            <asp:Label ID="lbl_count" runat="server" CssClass="texto"></asp:Label>
                        </p>

                        <%--INICIA EL PANEL EXTENDER DE LA BUSQUEDA--%>

                        <asp:Panel ID="ContentPanel_busqueda" runat="server" Style="overflow: hidden;">



                           

                            <div class="module_subsec">
                                <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic"></asp:Label>&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_busqueda" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class=" module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_div ">
                                            <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="8"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número de afiliado:</span>

                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente"
                                                    runat="server" Enabled="True" FilterType="Numbers"
                                                    TargetControlID="txt_idCliente">
                                                </ajaxToolkit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_idcliente"
                                                    runat="server" ControlToValidate="txt_idcliente" CssClass="textogris"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical flex_end">
                                        <div class="text_input_nice_div ">
                                            <asp:TextBox ID="txt_porcentajebus" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Porcentaje:</span>

                                                <asp:Label ID="lbl_porciento" runat="server" CssClass="texto" Text="%"
                                                    Width="19px"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txt_porcentajebus_FilteredTextBoxExtender"
                                                    runat="server" Enabled="True" FilterType="Numbers"
                                                    TargetControlID="txt_porcentajebus">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_porcentajebus" runat="server"
                                                    ControlToValidate="txt_porcentajebus" CssClass="textogris" Display="Dynamic"
                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_div ">
                                            <asp:DropDownList ID="cmb_relacion" runat="server" class="btn btn-primary2 dropdown_label">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tipo de relación:</span>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_relacion" runat="server" ControlToValidate="cmb_relacion"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_agregar" runat="server" CssClass="btn btn-primary" Text="Guardar" ValidationGroup="val_busqueda" />
                            </div>


                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_agregar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <%--TERMINA EL PANEL DEL PANEL EXTENDER DE LA BUSQUEDA--%>

    <section class="panel" id="panel_avalesn">
        <header class="panel_header_folder panel-heading">
            <span>Nuevo beneficiario </span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_AsignarModulos" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec">
                            <asp:TextBox ID="txt_clave" runat="server" class="text_input_nice_input" Enabled="False" MaxLength="10"
                                Visible="False"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="Label2" runat="server" CssClass="texto" Text="Número de beneficiario:"
                                    Visible="False"></asp:Label>
                            </div>
                        </div>

                        <div class="module_subsec columned three_columns low_m">
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_porcentaje" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>


                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Porcentaje %:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_antviv" runat="server"
                                                ControlToValidate="txt_porcentaje" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_porcentaje"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_porcentaje">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:DropDownList ID="cmb_tiporel" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Tipo de relación:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporel" runat="server" ControlToValidate="cmb_tiporel"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="module_subsec columned three_columns ">
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_nombres" runat="server" class="text_input_nice_input"
                                            MaxLength="300" ValidationGroup="val_Beneficiario"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Nombre(s):</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombres"
                                                runat="server" Enabled="True"
                                                FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txt_nombres" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="Req_nombres" runat="server"
                                                ControlToValidate="txt_nombres" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_paterno" runat="server" class="text_input_nice_input" MaxLength="100"
                                            ValidationGroup="val_Beneficiario"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Apellido paterno:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno"
                                                runat="server" Enabled="True"
                                                FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txt_paterno" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="Req_paterno" runat="server" ControlToValidate="txt_paterno"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_materno" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Apellido materno:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txt_materno" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec ">
                            <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Código postal:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_cp">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <asp:ImageButton ID="btn_buscadat" runat="server"  ImageUrl="~/img/img/glass.png" Style="height: 16px" />
                            &nbsp;&nbsp;
                                            <asp:LinkButton ID="lnk_BusquedaCP" runat="server" class="textogris" Text="Buscar CP"></asp:LinkButton>

                        </div>

                        <div class="module_subsec columned three_columns ">
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_estado" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Estado:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_estado" runat="server"
                                                ControlToValidate="cmb_estado" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_municipio" runat="server" AutoPostBack="True"
                                            class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Municipio:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_municipio"
                                                runat="server" ControlToValidate="cmb_municipio" CssClass="textogris"
                                                Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_colonia" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Localidad:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_colonia" runat="server"
                                                ControlToValidate="cmb_colonia" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec columned three_columns flex_end">
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_tipo_via" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Tipo de vialidad:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipo_via" runat="server"
                                                ControlToValidate="cmb_tipo_via" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:TextBox ID="txt_calle" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Calle y número:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle" runat="server"
                                                ControlToValidate="txt_calle" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_calle" runat="server"
                                                Enabled="True"
                                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_calle"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,#">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_sec text_input_nice_div">
                            <asp:TextBox ID="txt_referencia" runat="server" CssClass="text_input_nice_textarea" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label title_tag">Referencias:</span>

                            </div>
                        </div>

                        <span class="text_input_nice_label title_tag module_subsec">Teléfono:</span>

                        <div class="module_subsec columned three_columns ">
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_ladamov" runat="server" class="text_input_nice_input" MaxLength="6"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Lada:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ladamov"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_ladamov">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:TextBox ID="txt_telmov" runat="server" class="text_input_nice_input" MaxLength="15"></asp:TextBox>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Número:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_telmov" runat="server"
                                                ControlToValidate="txt_telmov" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Beneficiario"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_telmov"
                                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_telmov">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusnuevoben" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <div class=" module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">

                                    <div class="text_input_nice_labels">
                                        <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-primary" Text="Guardar" ValidationGroup="val_Beneficiario" AutopostBack="True" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

</asp:Content>

