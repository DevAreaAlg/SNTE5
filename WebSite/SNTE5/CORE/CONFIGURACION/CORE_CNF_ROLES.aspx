<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_ROLES.aspx.vb" Inherits="SNTE5.CORE_CNF_ROLES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">

        <div class="panel-body">

            <div class="module_subsec no_m">
                <div class="module_subsec columned low_m no_rm five_columns" style="flex: 1;">
                    <div class=" module_subsec_elements no_m">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_nombre" class="text_input_nice_input" MaxLength="100" runat="server"></asp:TextBox>
                            <span class="text_input_nice_label">Nombre: </span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars="-_ " TargetControlID="txt_nombre">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                    </div>

                    <div class=" module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <span class="text_input_nice_label title_tag">Estatus:</span>
                            <div class="btn-group min_w">
                                <asp:DropDownList ID="cmb_status_eq_filter" runat="server" class="btn btn-primary2 dropdown_label">
                                    <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                    <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec low_m  no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_busca_rol" runat="server" Text="Buscar" OnClick="btn_busca_rol_Click" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_eliminarB" runat="server" Text="Eliminar" class="btn btn-primary" />
                </div>
            </div>
             <div class="module_subsec flex_center">
                <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
            </div>

        </div>
    </section>

    <div class="panel" style="background-color: transparent; box-shadow: none;">
        <div class="flex_end">
            <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Nuevo Rol" />
        </div>
    </div>

    <section class="panel">
        <div class="overflow_x shadow">
            <asp:DataGrid ID="dag_Roles" runat="server" AutoGenerateColumns="False" class="table table-striped"
                GridLines="None">
                <Columns>
                    <asp:BoundColumn DataField="ROLID" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Descripción"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                        <ItemStyle Width="10%" />
                    </asp:ButtonColumn>
                </Columns>
                <HeaderStyle CssClass="table_header" />
            </asp:DataGrid>
        </div>
    </section>

</asp:Content>

