<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_GENERAL.aspx.vb" Inherits="SNTE5.CAP_PROD_GENERAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Producto de Captación </span>
        </header>
        <div class="panel-body">

            <div class="module_subsec low_m  flex_end" style="margin-bottom: 10px;">

                <asp:Button ID="lnk_ProductoNuevo" CssClass="btn btn-primary" runat="server" Text="Crear Producto" />&nbsp;&nbsp;
                    <asp:Button ID="lnk_ProductoEditar" CssClass="btn btn-primary" runat="server" Text="Editar Producto" />&nbsp;&nbsp;
                    <asp:DropDownList ID="cmb_Productos" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label" Visible="false">
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Button ID="lnk_resumen" CssClass="btn btn-primary" runat="server" Text="Resumen" visible="false"/>&nbsp;&nbsp;
         
            </div>


            <div class="module_subsec no_column">
                <span class="text_input_nice_label title_tag">Activar producto:</span>
                <asp:CheckBox ID="Chk_ActivaDesactivar" runat="server" DataTextField=" " DataValueField="ID"
                    CssClass="textocajas" AutoPostBack="True" Enabled="False" />


            </div>

            <div class="module_subsec columned three_columns align_items_flex_start no_m">
                <div class="module_subsec_elements">
                    <div class="module_subsec_elements_content vertical">
                        <div class="text_input_nice_div ">
                            <asp:TextBox ID="txt_NombreProducto" runat="server" class="text_input_nice_input" Enabled="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Nombre de producto:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_NombreProducto" runat="server" ControlToValidate="txt_NombreProducto"
                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_crearproducto">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_NombreProducto" runat="server" Enabled="True"
                                    FilterType="Custom, Numbers,UppercaseLetters,LowercaseLetters" ValidChars="' ',Ñ,ñ" TargetControlID="txt_NombreProducto">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="module_subsec_elements_content vertical">
                        <div class="text_input_nice_div ">
                            <asp:DropDownList ID="cmb_tipo" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Tipo captación:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmb_tipo" CssClass="alertaValidator"
                                    ControlToValidate="cmb_tipo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_crearproducto" InitialValue="0" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="module_subsec_elements_content vertical">
                        <div class="text_input_nice_div ">
                            <asp:DropDownList ID="cmb_tipoper" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False" Enabled="false">
                                <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                <asp:ListItem Value="F">PERSONA FISICA</asp:ListItem>
                                <asp:ListItem Value="M">PERSONA MORAL </asp:ListItem>
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Tipo persona:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmb_tipoper" CssClass="alertaValidator"
                                    ControlToValidate="cmb_tipoper" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_crearproducto" InitialValue="-1" />
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
                                CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_crearproducto">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

            </div>

            <div class="module_subsec columned three_columns no_m">
            </div>

            <div align="center">
                <asp:Label ID="lbl_Alertas" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="module_subsec low_m  flex_end" style="margin-bottom: 10px;">
                &nbsp;&nbsp;
                    <asp:Button ID="btn_GenerarProducto" CssClass="btn btn-primary" runat="server" Text="Generar producto" Visible="false" ValidationGroup="val_crearproducto" />&nbsp;&nbsp;
                    <asp:Button ID="btn_GuardarProducto" CssClass="btn btn-primary" runat="server" Text="Guardar" Visible="false" ValidationGroup="val_crearproducto" />&nbsp;&nbsp;
                    <asp:Button ID="btn_Cancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar" Visible="false" />&nbsp;&nbsp;
                    <asp:Button ID="btn_eliminar" CssClass="btn btn-primary" runat="server" Text="Eliminar producto" Visible="false" />
            </div>

            <br />

            <section class="panel overflow_x low_m module_subsec shadow">
                <asp:DataGrid ID="dag_ConfProductos" runat="server" AutoGenerateColumns="False" class="table table-striped"
                    GridLines="None">
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

    </section>

</asp:Content>
