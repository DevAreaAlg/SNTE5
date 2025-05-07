<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_GENERAL.aspx.vb" Inherits="SNTE5.CRED_PROD_GENERAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Producto de Préstamo </span>
        </header>
        <div class="panel-body">

            <div class="panel-body_content init_show">

                <div class="module_subsec no_m no_column flex_end" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_crear" CssClass="btn btn-primary module_subsec_elements" OnClick="btn_crear_Click" runat="server" Text="Crear Producto" />
                    <asp:Button ID="btn_editar" CssClass="btn btn-primary module_subsec_elements" runat="server" Text="Editar Producto" />
                    <asp:DropDownList ID="ddl_productos" runat="server" AutoPostBack="True" class="btn btn-primary2  module_subsec_elements" Visible="false">
                    </asp:DropDownList>
                    <asp:Button ID="lnk_resumen" CssClass="btn btn-primary module_subsec_elements" runat="server" Text="Resumen" Visible="false" />

                </div>


                <div class="module_subsec no_column">
                    <span class="text_input_nice_label title_tag">Activar producto: </span>
                    <asp:CheckBox ID="Chk_ActivaDesactivar" runat="server" DataTextField=" " DataValueField="ID"
                        CssClass="textocajas" AutoPostBack="True" Enabled="False" />
                </div>

                <div class="module_subsec columned three_columns align_items_flex_start no_m">
                    <div class="module_subsec_elements">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:TextBox ID="txt_nombre" runat="server" class="text_input_nice_input" Enabled="false" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Nombre de producto:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_nombre" runat="server" ControlToValidate="txt_nombre"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_producto">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_nombre" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" .,ÁÉÍÓÚÑáéíóúñ" TargetControlID="txt_nombre">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:TextBox ID="txt_cveProd" runat="server" class="text_input_nice_input" Enabled="false" MaxLength="50"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Clave de producto:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_cveProd" runat="server" ControlToValidate="txt_cveProd"
                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_producto">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_cveProd" runat="server" Enabled="True"
                                        FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters" ValidChars="-_." TargetControlID="txt_cveProd">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned three_columns flex_start">
                    <div class="module_subsec_elements" style="flex: 1;">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_DescripcionProducto" runat="server" CssClass="text_input_nice_textarea" MaxLength="300" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Descripción de producto:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_DescripcionProducto" runat="server" ControlToValidate="txt_DescripcionProducto"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_producto">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" .,ÁÉÍÓÚÜÑáéíóúüñ" TargetControlID="txt_DescripcionProducto">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="module_subsec columned three_columns no_m">
                    <div class="module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:DropDownList ID="ddl_clasificacion" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Clasificación producto:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddl_clasificacion" CssClass="alertaValidator"
                                        ControlToValidate="ddl_clasificacion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_producto" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:DropDownList ID="ddl_tipo_persona" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                                    <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                    <asp:ListItem Value="F">PERSONA FISICA</asp:ListItem>
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Tipo de persona:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddl_tipo_persona" CssClass="alertaValidator"
                                        ControlToValidate="ddl_tipo_persona" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_producto" InitialValue="-1" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:DropDownList ID="ddl_destino" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Destino:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddl_destino" CssClass="alertaValidator"
                                        ControlToValidate="ddl_destino" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_producto" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div ">
                                <asp:DropDownList ID="cmb_cvedscto" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Clave descuento:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="alertaValidator"
                                        ControlToValidate="cmb_cvedscto" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_producto" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div align="center">
                    <asp:Label ID="lbl_Alertas" runat="server" CssClass="alerta">
                    </asp:Label>
                </div>

                <div align="center">
                    <asp:Label ID="lbl_AlertaDestino" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec low_m  flex_end" style="margin-bottom: 10px;">
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_generarP" CssClass="btn btn-primary" OnClick="btn_generarP_Click" runat="server" Text="Generar producto" Visible="false" ValidationGroup="val_producto" />&nbsp;&nbsp;
                            <asp:Button ID="btn_guarda_cambios" CssClass="btn btn-primary" runat="server" Text="Guardar" Visible="false" ValidationGroup="val_producto" />&nbsp;&nbsp;
                            <asp:Button ID="btn_cancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" Visible="false" />&nbsp;&nbsp;
                            <asp:Button ID="btn_eliminar" CssClass="btn btn-primary" runat="server" Text="Eliminar producto" Visible="false" />
                </div>




                <br />

                <section class="shadow module_subsec">
                    <asp:DataGrid ID="dag_ConfProductos" runat="server" AutoGenerateColumns="False" class="table table-striped"
                        GridLines="None"
                        TabIndex="17">
                        <Columns>
                            <asp:BoundColumn DataField="IdConf" HeaderText="">
                                <ItemStyle Width="10px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Estatus" HeaderText="">
                                <ItemStyle Width="10px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="Semaforo" AlternateText="BLANK" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Descripcion" HeaderText="Apartados">
                                <ItemStyle Width="520px" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="MODIFICAR" HeaderText=" " Text="Entrar">
                                <ItemStyle Width="20px" HorizontalAlign="center"></ItemStyle>
                            </asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </section>
            </div>
        </div>
    </section>

</asp:Content>

