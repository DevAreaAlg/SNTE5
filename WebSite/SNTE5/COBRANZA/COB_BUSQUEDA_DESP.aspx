<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_BUSQUEDA_DESP.aspx.vb" Inherits="SNTE5.COB_BUSQUEDA_DESP" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <div class="panel-body">

            <div class="module_subsec columned low_m no_rm five_columns">
                <div class="module_subsec_elements text_input_nice_div flex_1">
                   <asp:TextBox ID="txt_id" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                     <span class="text_input_nice_label">Clave despacho:</span>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                        FilterType="Numbers" TargetControlID="txt_id">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </div>

                <div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                    <span class="text_input_nice_label">Nombre:</span>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                        FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_nombre">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </div>

                <div class="module_subsec_elements flex_1 module_subsec no_m flex_end">
                    <asp:Button ID="btn_busca" runat="server" Text="Buscar" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_elimina_filtro" runat="server" Text="Eliminar" class="btn btn-primary"/>
                </div>
            </div>
            <div class="module_subsec flex_center">
                <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
            </div>
        </div>
    </section>

    <div class="panel" style="background-color: transparent; box-shadow: none;">
        <div class="flex_end">
            <asp:Button ID="btn_nuevo" runat="server" class="btn btn-primary" Text="Nuevo Despacho" />
        </div>
    </div>

    <section class="panel">
        <div class="overflow_x shadow">
            <!-- Tabla de Personas con coincidencias encontradas-->
            <asp:DataGrid ID="dag_pendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" Width="100%">
                <HeaderStyle CssClass="table_header" />
                <Columns>
                    <asp:BoundColumn ItemStyle-Width="15%" DataField="IDDESPACHO" HeaderText="Clave despacho"></asp:BoundColumn>
                    <asp:BoundColumn ItemStyle-Width="30%" DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                    <asp:BoundColumn ItemStyle-Width="20%" DataField="FECHAALTA" HeaderText="Fecha alta"></asp:BoundColumn>
                    <asp:BoundColumn ItemStyle-Width="20%" DataField="AVANCE" HeaderText="Avance"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                        <ItemStyle Width="15%" />
                    </asp:ButtonColumn>
                      <asp:ButtonColumn CommandName="RESUMEN" HeaderText="" Text="Resumen">
                        <ItemStyle Width="15%" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Visible = "False" Text="Eliminar">
                        <ItemStyle Width="15%" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </section>

</asp:Content>

