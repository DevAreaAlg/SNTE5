<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_DIRECTOR.aspx.vb" Inherits="SNTE5.CRED_EXP_DIRECTOR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Expedientes de dirección</span>
        </header>

        <div class="panel-body">

            <div class="module_subsec">
                <h5 style="font-weight: normal" class="">Expedientes en proceso de decisión:</h5>
                
            </div>
            <div class="module_subsec">
                <asp:Label ID="Label2" runat="server" Text="Esta página se actualizará automaticamente al haber transcurrido más de un minuto de inactividad del mouse, sin embargo, podrá actualizarla de forma manual presionando el siguiente botón."
                    Visible="True" class="texto"></asp:Label>
            </div>

            <!-- Boton Actualizar -->
            <div class="module_subsec  flex_end">
                <asp:Button ID="btn_actualizar" runat="server" class="btn btn-primary"
                    Text="Actualizar" Visible="True" />
            </div>

            <asp:Label ID="lbl_AlertaNoAcceso" runat="server" CssClass="alerta"></asp:Label>

            <div class="overflow_x shadow module_subsec">
                <!-- Tabla de Expedientes generados por sucursal -->
                <asp:DataGrid ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn ItemStyle-Width="40" DataField="FOLIO" HeaderText="Folio" Visible="false">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="40" DataField="CLAVE" HeaderText="Folio">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="245" DataField="CLIENTE" HeaderText="Afiliado">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="245" DataField="NUMCLIENTE" HeaderText="Núm. afiliado">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="135" DataField="PRODUCTO" HeaderText="Producto">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="50" DataField="MONTO" HeaderText="Monto">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="50" DataField="PLAZO" HeaderText="Plazo">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="135" DataField="SUCURSAL" HeaderText="Sucursal">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn ItemStyle-Width="110" DataField="FECHA" HeaderText="Fecha solicitud">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:ButtonColumn ItemStyle-Width="30" CommandName="Detalle" Text="Detalle">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:ButtonColumn>

                    </Columns>
                </asp:DataGrid>
            </div>

        </div>
    </section>

</asp:Content>
