<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_ENTIDAD.aspx.vb" Inherits="SNTE5.CORE_OPE_ENTIDAD" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Datos</span>
        </header>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel_PagoInteres" runat="server">
                <ContentTemplate>
                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_CVENTIDAD" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Clave entidad:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_CVENTIDAD" runat="server"
                                        TargetControlID="txt_CVENTIDAD" FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" />
                                    <asp:RequiredFieldValidator runat="server" ID="Req_CVENTIDAD"
                                        ControlToValidate="txt_CVENTIDAD" CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>

                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_ABREEMP" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Abreviatura entidad:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ABREEMP" runat="server"
                                        TargetControlID="txt_ABREEMP" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars="-" />
                                    <asp:RequiredFieldValidator runat="server" ID="Req_ABREEMP"
                                        ControlToValidate="txt_ABREEMP" CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">

                                <asp:TextBox ID="txt_RFCENTIDAD" class="text_input_nice_input" runat="server" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*RFC entidad:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RFCENTIDAD" runat="server"
                                        TargetControlID="txt_RFCENTIDAD" FilterType="LowercaseLetters, UppercaseLetters, Numbers" />
                                    <asp:RequiredFieldValidator runat="server" ID="Req_RFCENTIDAD" ControlToValidate="txt_RFCENTIDAD"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_RFCENTIDAD" runat="server"
                                        class="textogris" ControlToValidate="txt_RFCENTIDAD" ErrorMessage=" RFC incorrecto!"
                                        ValidationExpression="^[a-zA-Z]{2,3}(\d{6})((\D|\d){3})?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_RAZONENTIDAD" class="text_input_nice_input" runat="server" MaxLength="500"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Razón social entidad:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="Req_RAZONENTIDAD" ControlToValidate="txt_RAZONENTIDAD"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements align_items_flex_end">
                            <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>

                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Código postal: </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_cp">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" />
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
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_CALLE" class="text_input_nice_input" runat="server" MaxLength="1000"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Calle:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="Req_CALLE"
                                        ControlToValidate="txt_CALLE" CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_NUMEXT" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Número exterior:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="Req_NUMEXT"
                                        ControlToValidate="txt_NUMEXT" CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_NUMINT" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Número interior:</span>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_FECHINIOPE" class="text_input_nice_input" runat="server" MaxLength="500"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Fecha inicio operación:</span>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_FECHINIOPE"
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FECHINIOPE">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_FECHINIOPE" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_FECHINIOPE">
                                    </ajaxToolkit:CalendarExtender>

                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_FECHINIOPE"
                                        runat="server" ControlExtender="MaskedEditExtender_FECHINIOPE"
                                        ControlToValidate="txt_FECHINIOPE" CssClass="textogris"
                                        ErrorMessage="MaskedEditValidator_FECHINIOPE" Display="Dynamic"
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_entidad"></ajaxToolkit:MaskedEditValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_FECHINIOPE" runat="server"
                                        ControlToValidate="txt_FECHINIOPE" CssClass="textogris" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_entidad"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_REPLEGAL" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Representante legal:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="Req_REPLEGAL"
                                        ControlToValidate="txt_REPLEGAL" CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_entidad" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_Alerta" runat="server" CssClass="flex_1 alerta flex_center module_subsec low_m"></asp:Label>
                    </div>
                    <div class="module_subsec flex_end low_m align_items_flex_center">

                        <asp:Button ID="btn_Guardar" class="btn btn-primary" runat="server"  ValidationGroup="val_entidad" Text="Guardar" />
                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_Guardar" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </section>

</asp:Content>
