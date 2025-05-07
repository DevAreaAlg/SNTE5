<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_NOT_NOTIFICACIONES_ADM.aspx.vb" Inherits="SNTE5.CORE_NOT_NOTIFICACIONES_ADM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <section class="panel">

                <div class="panel-body ">
                    <div class="panel-body_content init_show">

                        <div class="module_subsec no_m">
                            <div class="module_subsec columned low_m no_rm five_columns" style="flex: 1;">
                                <div class=" module_subsec_elements no_m">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Nombre:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                                FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="-_ " TargetControlID="txt_nombre">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements no_m vertical">
                                    <span>Rol:</span>
                                    <asp:DropDownList ID="ddl_rol" runat="server" CssClass="btn btn-primary2">
                                        <asp:ListItem Text="ELIJA"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class=" module_subsec_elements no_m">
                                    <div class="module_subsec_elements_content vertical">
                                        <span class="text_input_nice_label title_tag">Estatus:</span>
                                        <asp:DropDownList ID="ddl_estatus" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False">
                                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                            <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec low_m  no_lm" style="margin-bottom: 10px;">
                                <asp:Button ID="btn_bucar" runat="server" Text="Buscar" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_eliminarB" runat="server" Text="Eliminar" class="btn btn-primary" />
                            </div>

                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
                        </div>
                    </div>
                </div>
            </section>

            <section class="panel">
                <div class="overflow_x shadow">

                    <asp:DataGrid ID="dag_catnot" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None">
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID">
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAVE" HeaderText="Clave">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Descripción">
                                <ItemStyle Width="35%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>

                            <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

