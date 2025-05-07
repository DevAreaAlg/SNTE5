<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_USUARIOS_CREAR.aspx.vb" Inherits="SNTE5.CORE_CNF_USUARIOS_CREAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            Usuario
        </header>
        <div class="panel-body">
            <div class="module_subsec no_column align_items_flex_center">
                <asp:Label ID="lbl_RolNumero" runat="server" CssClass="text_input_nice_label" Text="Número de usuario:"></asp:Label>

                <div class="module_subsec_elements" style="flex: 1">
                    <asp:TextBox ID="txt_id" runat="server" disabled class="text_input_nice_input"></asp:TextBox>
                </div>
            </div>
        </div>
    </section>

    <asp:UpdatePanel ID="UpdatePanelDatos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_datos">
                <header class="panel_header_folder panel-heading">
                    <span>Datos</span>
                    <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">


                        <div class="module_subsec_elements vertical">
                            <div class="text_input_nice_div module_sec">
                                <span class="text_input_nice_label">Activo:
                                        <asp:CheckBox ID="chk_estatus" runat="server" Style="margin-left: 35px;" /></span>
                            </div>
                        </div>




                        
                  <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                      <asp:DropDownList ID="cmb_tipo_usuario" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Tipo de usuario:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="alertaValidator"
                                            ControlToValidate="cmb_tipo_usuario" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            </div>


                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Nombre(s):</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_User">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txt_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>


                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_paterno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Apellido paterno:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_paterno" runat="server" ControlToValidate="txt_paterno"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_User">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars="Ññ " TargetControlID="txt_paterno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_materno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Apellido materno:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_materno" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txt_materno">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>

                       





                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_usuario" CssClass="text_input_nice_input" runat="server" MaxLength="15"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Usuario:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_usuario" runat="server" ControlToValidate="txt_usuario"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_User">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>



                             
                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_area" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                        AutoPostBack="False">  </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Unidad de negocio:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_area" CssClass="alertaValidator"
                                            ControlToValidate="cmb_area" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_puesto" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Puesto:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_puesto" CssClass="alertaValidator"
                                            ControlToValidate="cmb_puesto" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_politica" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Política:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_politica" CssClass="alertaValidator"
                                            ControlToValidate="cmb_politica" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_sucursal" runat="server" CssClass="btn btn-primary2 dropdown_label"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Oficina:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_oficina" CssClass="alertaValidator"
                                            ControlToValidate="cmb_sucursal" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_institucion" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Institución:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                            ControlToValidate="cmb_institucion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_User" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements flex_1">
                                <div class="text_input_nice_div  module_subsec_elements_content">
                                    <asp:TextBox ID="txt_email" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Correo:</span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_email" ControlToValidate="txt_email"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$"
                                            runat="server" CssClass="alertaValidator" ErrorMessage="Correo Incorrecto"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredCorreo" runat="server" ControlToValidate="txt_email"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_User">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div  module_subsec_elements_content">
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div  module_subsec_elements_content">
                                    <asp:TextBox ID="txt_lada" class="text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Lada:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_lada">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div  module_subsec_elements_content">
                                    <asp:TextBox ID="txt_tel" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <span class="text_input_nice_label">Teléfono:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_tel">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div  module_subsec_elements_content">
                                    <asp:TextBox ID="txt_ext" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Extensión:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_ext">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_guardado" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar1" class="btn btn-primary" runat="server" OnClick="btn_guardar1_Click" ValidationGroup="val_User" Text="Guardar" />
                        </div>


                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_guardar1" />
        </Triggers>
    </asp:UpdatePanel>

    <section class="panel" runat="server" id="panel_roles">
        <header class="panel_header_folder panel-heading">
            <span>Roles</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_AsignarRoles" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec">
                            <div class="overflow_x shadow" style="flex: 1;">
                                <asp:GridView ID="dag_rol_usu" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeader="true"
                                    CssClass="table table-striped " GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="40" DataField="ID" HeaderText="ID Rol" HeaderStyle-CssClass="table_header">
                                            <ItemStyle Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre" ControlStyle-CssClass="table_header"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="label1" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar_r" />
                    </Triggers>
                </asp:UpdatePanel>

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar_r" class="btn btn-primary" runat="server" OnClick="btn_guardar_r_Click" Text="Guardar" />
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_modulos">
        <header class="panel_header_folder panel-heading">
            <span>Permisos</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec">
                            <div class="overflow_x shadow" style="flex: 1;">
                                <asp:GridView ID="dag_mod_si" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="40" DataField="ID" HeaderText="ID Permiso">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="CATEGORIA" HeaderText="Categoría">
                                            <ItemStyle Width="25%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle Width="40%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="TIPO" HeaderText="Tipo">
                                            <ItemStyle Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="label2" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar_m" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar_m" class="btn btn-primary" runat="server" OnClick="btn_guardar_m_Click" Text="Guardar" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>

