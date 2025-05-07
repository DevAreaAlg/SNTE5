<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_COMITE.aspx.vb" Inherits="SNTE5.CRED_EXP_COMITE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script type="text/javascript">
        // Java Script para actualizar la pagina cada minuto de inactividad
        if (document.layers) {
            window.captureEvent(onMouseMove);
        }

        document.onmousemove = oreset
        var tID = '';

        function oreset() {
            clearTimeout(tID)
            count = 0
            reloadPage()
        }

        function reloadPage() {
            count++

            if (count == 10) { // 1 minuto
               
                window.location = 'CRED_EXP_COMITE.aspx'
            }
            tID = setTimeout("reloadPage()", 1000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <section class="panel">
        <header class="panel-heading">
            <span>Expedientes de comité</span>
        </header>

        <div class="panel-body">

            <div class="module_subsec">
                <asp:Label ID="lbl_actualiza" runat="server"
                Text="Esta página se actualizará automaticamente al haber transcurrido más de un minuto de inactividad del mouse, sin embargo, podrá actualizarla de forma manual presionando el siguiente botón."
                Visible="True" class="texto"></asp:Label>
            </div>

            <div class="module_subsec  flex_end">
                <asp:Button ID="btn_actualizar" runat="server" class="btn btn-primary" Text="Actualizar" Visible="True" />
            </div>

            <div align="center">
                <asp:Label ID="lbl_AlertaNoAcceso" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="overflow_x shadow module_subsec">
                <!-- Tabla de Expedientes generados por sucursal -->
                <asp:DataGrid ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CLIENTE" HeaderText="Afiliado">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMCTRL" HeaderText="Núm. control">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMCLIENTE" HeaderText="Núm. afiliado"  Visible="false">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PRODUCTO" HeaderText="Producto">
                            
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PLAZO" HeaderText="Plazo">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA" HeaderText="Fecha solicitud">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOTO" HeaderText="Voto" Visible="false">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Detalle" Text="Detalle">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>              
            </div>
       
        </div>
    </section>
       
</asp:Content>
