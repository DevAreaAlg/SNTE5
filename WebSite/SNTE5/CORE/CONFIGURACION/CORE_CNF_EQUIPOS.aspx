<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_EQUIPOS.aspx.vb" Inherits="SNTE5.CORE_CNF_EQUIPOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">


        <div class="panel-body">
            <div class="module_subsec no_m">
                <div class="module_subsec columned low_m no_rm five_columns" style="flex: 1;">
                    <div class=" module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <span class="text_input_nice_label title_tag">Oficina:</span>
                            <div class="btn-group min_w ">
                                <asp:DropDownList ID="cmb_sucursal_busqueda" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class=" module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <span class="text_input_nice_label title_tag">Estatus:</span>
                            <div class="btn-group min_w">
                                <asp:DropDownList ID="cmb_status_eq_filter" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False">
                                    <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                    <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="module_subsec low_m  no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_buscarEquipos" CssClass="btn btn-primary" OnClick="btn_buscarEquipos_Click" runat="server" Text="Buscar" />&nbsp;&nbsp;&nbsp;
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
            <asp:Button ID="btn_crearEquipo" CssClass="btn btn-primary" OnClick="redirect_crear_eq" runat="server" Text="Nuevo Equipo" />
        </div>
    </div>

    <section class="panel">
        <div class="overflow_x shadow">
            <asp:DataGrid ID="dag_eq" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" OnItemCommand="dag_eq_ItemCommand">
                <HeaderStyle CssClass="table_header"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="ID_EQ" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="MAC" HeaderText="MAC"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CAJA" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CAJA_TIPO" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SUCURSAL" HeaderText="Oficina"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="EDITAR" Text="Editar"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>

    </section>

</asp:Content>

