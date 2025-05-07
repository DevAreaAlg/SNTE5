<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_CNF_OFICINAS_CREAR.aspx.vb" Inherits="SNTE5.PEN_CNF_OFICINAS_CREAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section class="panel">
        <header class="panel-heading" >
            Oficinas Institucionales
        </header>
        <div class="panel-body">
            <div class="module_subsec no_column align_items_flex_center" >
                <span class="text_input_nice_label" >Número de oficina:</span>
                        
                <div class="module_subsec_elements" style="flex: 1">
                    <asp:TextBox ID="txt_id_oficina" runat="server" class="text_input_nice_input" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
    </section>

     <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header" >Datos</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                       
                <div class="module_subsec no_column low_m">
                    <span class="text_input_nice_label title_tag">Activo:</span>
                    <asp:CheckBox ID="ckb_Activo" CssClass="mod_check" runat="server" />
                </div>

                 <div class="module_subsec low_m columned three_columns">
                     <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_institucion" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label title_tag">*Institucion: </span>
                                <asp:RequiredFieldValidator runat="server" ID="req_tipo" CssClass="alertaValidator" 
                                    ControlToValidate="cmb_institucion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Oficina" InitialValue="-1"/>
                            </div>
                        </div>
                    </div>

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
                        <asp:TextBox ID="txt_nombre" runat="server" class="text_input_nice_input" MaxLength="50"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Nombre:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre"
                                CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre">
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
                            <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>

                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Código postal: </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_estado" runat="server" ControlToValidate="ddl_Estado"
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_municipio" runat="server" ControlToValidate="ddl_municipio"
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_asentamiento" runat="server" ControlToValidate="ddl_asentamiento"
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
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server" Enabled="True"
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
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_extension"
                                CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Oficina">
                            </asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_extension">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>
                 <div class="module_subsec flex_center">
                      <asp:Label ID="lbl_statusofi" runat="server" CssClass="alerta"></asp:Label>
                 </div>
                   
                

                <div class="module_subsec flex_end">
                        <button runat="server" class="btn btn-primary" type="button" id="btn_guardar_oficina" onserverclick="click_btn_guardar_datos" validationgroup="val_Oficina">Guardar</button>

                </div>

               

            </div>
        </div>
    </section>
</asp:Content>

