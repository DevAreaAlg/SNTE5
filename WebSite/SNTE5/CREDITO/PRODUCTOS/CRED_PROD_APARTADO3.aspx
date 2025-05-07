<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO3.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO3" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <section class="panel">
        <header class="panel-heading">
            <span> Producto</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div> 
        </div>
    </section>

    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span>Asignación de garantías</span>
            <span class="panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="upd_pnl_AsignarGrantias" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex: 1;">
                            <asp:GridView ID="dag_GtiaAsigandas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="40" DataField="IDG" HeaderText="Id garantía">
                                        <ItemStyle Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="245" DataField="DESCRIPCION" HeaderText="Nombre">
                                        <ItemStyle Width="45%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="245" DataField="TIPO" HeaderText="Tipo">
                                        <ItemStyle Width="25%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_GtiaAsignada" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                    </div>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                    </Triggers>

                </asp:UpdatePanel>

                <div class="module_subsec flex_end low_m">
                        <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" OnClick="btn_asignar_Click" Text="Guardar" />
                    </div>
                        
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span>Crear y editar garantías</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_Garantias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None" >
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="idgarantia" HeaderText="Clave de garantía"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="descripcion" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="tipo" HeaderText="Tipo garantía"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="porcentaje" HeaderText="Porcentaje"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="estatus" HeaderText="Estatus"></asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="HABILITAR" Text="Habilitar/Deshabilitar"></asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar"></asp:ButtonColumn>
                                </Columns>                                                      
                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_verifica" runat="server" class="alerta"></asp:Label>
                        </div>
                      
                        <div class="module_subsec low_m">
                            <h4>Nueva garantía</h4>
                        </div>

                        <div class="module_subsec align_items_flex_center low_m">
                            <span class=" title_tag">Activo:</span>
                            <asp:CheckBox ID="chk_estatus" CssClass="mod_check" runat="server" />
                        </div>                                       

                        <div class="module_subsec columned three_columns low_m">
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_claveGarantia" CssClass="text_input_nice_input" runat="server" Enabled="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Número de garantía:</span>
                                </div>
                            </div>
                                        
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_monto" MaxLength="5" CssClass="text_input_nice_input" runat="server"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Porcentaje de préstamo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_monto" runat="server" ControlToValidate="txt_monto"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_garantias"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_monto">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" runat="server" ControlToValidate="txt_monto" CssClass="textogris"
                                        ErrorMessage=" Error:Valor Incorrecto" Display="Dynamic" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:DropDownList ID="cmb_tipo" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Tipo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipo" CssClass="alertaValidator bold" runat="server" ControlToValidate="cmb_tipo" InitialValue="-1"
                                        ErrorMessage="Falta Dato!" ValidationGroup="val_garantias"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_sec">
                            <div class="text_input_nice_div module_subsec_elements">
                                <asp:TextBox ID="txt_descripcionGarantia" CssClass="text_input_nice_input w_100" runat="server" MaxLength="50"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_descripcion" runat="server" Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                    TargetControlID="txt_descripcionGarantia" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Nombre:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_descripcion" runat="server" ControlToValidate="txt_descripcionGarantia" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_garantias"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_agregar" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agregarGarantias" CssClass="btn btn-primary" OnClick="btn_agregarGarantias_Click" runat="server" Text="Guardar" ValidationGroup="val_garantias" />                                
                        </div> 
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dag_Garantias" />
                </Triggers>
                </asp:UpdatePanel>                                  
            </div>              
        </div>
    </section>
    
</asp:Content>


