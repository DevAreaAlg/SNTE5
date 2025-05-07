<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_USUARIOS.aspx.vb" Inherits="SNTE5.CORE_CNF_USUARIOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">

        <div class="panel-body">

            <div class="module_subsec no_m">

                <div class="module_subsec columned low_m no_rm seven_columns" style="flex: 1;">
                    <div class="module_subsec_elements text_input_nice_div flex_1">
                        <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                        <span class="text_input_nice_label">Nombre:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                            FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_nombre">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec_elements text_input_nice_div flex_1">
                        <asp:TextBox ID="txt_paterno" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                        <span class="text_input_nice_label">Apellido paterno:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server" Enabled="True"
                            FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_paterno">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec_elements text_input_nice_div flex_1">
                        <asp:TextBox ID="txt_usuario" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                        <span class="text_input_nice_label">Usuario:</span>
                    </div>


                    <div class="module_subsec_elements vertical flex_1">
                        <span class="text_input_nice_label title_tag">Oficina: </span>
                        <asp:DropDownList ID="cmb_sucursal" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>

                    </div>

                    <div class="module_subsec_elements vertical flex_1">
                        <span class="text_input_nice_label title_tag">Estatus:</span>
                        <asp:DropDownList ID="cmb_estatus" runat="server" class="btn btn-primary2 dropdown_label">
                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                            <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                            <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                        </asp:DropDownList>

                    </div>

                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_busca_usuario" runat="server" Text="Buscar" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
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
            <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Nuevo Usuario" />
        </div>
    </div>

    <section class="panel">
        <div class="overflow_x shadow">
            <!-- Tabla de Usuarios con coincidencias encontradas-->
            <asp:DataGrid ID="dag_usuario" runat="server" AutoGenerateColumns="False" class="table table-striped"
                GridLines="None">
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="Núm. usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                        <ItemStyle Width="10%" />
                    </asp:ButtonColumn>
                </Columns>
                <HeaderStyle CssClass="table_header"></HeaderStyle>
            </asp:DataGrid>
        </div>
    </section>

</asp:Content>
