<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_ROLES_CREAR.aspx.vb" Inherits="SNTE5.CORE_CNF_ROLES_CREAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            Rol
        </header>
        <div class="panel-body">
            <div class="module_subsec no_column align_items_flex_center">
                <asp:Label ID="lbl_RolNumero" runat="server" CssClass="text_input_nice_label" Text="Número de rol:"></asp:Label>

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
                    <span class="panel_folder_toogle_header">Datos</span>
                    <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <div class="module_sec low_m">
                            <div class="module_subsec no_column">
                                <span class="text_input_nice_label title_tag">Activo:</span>
                                <asp:CheckBox ID="chk_estatus" CssClass="mod_check" runat="server" />
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Nombre:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre"
                                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Rol">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_descripcion" class="text_input_nice_input" runat="server" MaxLength="200"></asp:TextBox>
                                    <span class="text_input_nice_label">Descripción:</span>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_guardado" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar" class="btn btn-primary" runat="server" Text="Guardar" OnClick="btn_guardar_Click" AutoPostBack="False" ValidationGroup="val_Rol" />
                        </div>                

                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_guardar" />
        </Triggers>
    </asp:UpdatePanel>
        
    <section class="panel" runat="server" id="panel_roles">
        <header class="panel_header_folder panel-heading">
            <span>Permisos</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">

                <asp:UpdatePanel ID="upd_pnl_AsignarModulos" runat="server">
                    <ContentTemplate>
                        <div class="overflow_x shadow">
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
                        <div align="center">
                            <asp:Label ID="lbl_permisos" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                    </Triggers>

                </asp:UpdatePanel>
                <br />
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" OnClick="btn_asignar_Click" Text="Guardar" />
                </div>
            </div>
        </div>
    </section>
     
    <section class="panel" style="display: none;" id="pnl_reportes" runat="server">
        <header class="panel-heading panel_header_folder">
            <span>Asignación de reportes</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%--<div class="module_subsec low_m no_column vertical align_items_flex_start">
                            <asp:LinkButton ID="lnk_BuscarRepGen" CssClass="module_subsec_elements" OnClick="lnk_BuscarRepGen_Click" runat="server">Mostrar todos los reportes disponibles.</asp:LinkButton>
                            <asp:LinkButton ID="lnk_BuscarRepXCateg" CssClass="module_subsec_elements" runat="server" OnClick="lnk_BuscarRepXCateg_Click">Buscar reporte por categoría.</asp:LinkButton>
                        </div>--%>
                        <asp:Panel runat="server" CssClass="module_subsec no_m" Style="display: none; padding: 10px 10px 10px 10px" ID="out_catg_ctrl">
                        </asp:Panel>
                        <div class="module_subsec">
                            <div class="overflow_x module_subsec_elements shadow" style="flex: 1;">
                                <asp:GridView ID="grdVw_reportesAsig" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" GridLines="None">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="ID" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" />
                                        <asp:BoundField HeaderText="Categ" DataField="CATEG" />
                                        <asp:BoundField HeaderText="Clave" DataField="CLAVE" />
                                        <asp:TemplateField HeaderText="Asignado">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_repAsig" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">

                            <asp:Label ID="Label2" runat="server" Text="Label" CssClass="alerta" Visible="false"></asp:Label>
                            <asp:Label ID="lbl_repEstatus" runat="server" Text="Label" CssClass="alerta" Visible="false"></asp:Label>
                            
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardarRep" CssClass="btn btn-primary" OnClick="btn_guardarRep_Click" runat="server" Text="Guardar" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>
