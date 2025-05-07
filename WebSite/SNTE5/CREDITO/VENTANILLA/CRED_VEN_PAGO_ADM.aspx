<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_PAGO_ADM.aspx.vb" Inherits="SNTE5.CRED_VEN_PAGO_ADM" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Estatus" runat="server" Text="Préstamos por pagar"></asp:Label>
            </span>
        </header>

        <div class="panel-body">

            <div class="module_subsec">
                <asp:Label ID="lbl_Nota" runat="server" class="texto">Nota: La fecha en la que se pagará este préstamo debe ser igual a la fecha asignada en el Plan de Pagos. De lo contrario será necesario modificar la fecha de pago en el Configurador de Expedientes.</asp:Label>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label runat="server" CssClass="alerta" ID="lbl_status"></asp:Label>
            </div>

            <asp:Label ID="lblideq" runat="server" Text=""></asp:Label>                        
            <asp:Label ID="lblidcaurs" runat="server" Text=""></asp:Label>

            <div class="overflow_x shadow module_subsec">
                <asp:DataGrid ID="dag_CredLib" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <Columns>
                        <asp:BoundColumn DataField="folio" HeaderText="Folio">
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="idper" HeaderText="Núm. afiliado">
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="prospecto" HeaderText="Afiliado">
                            <ItemStyle Width="25%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="producto" HeaderText="Producto">
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="monto" HeaderText="Monto">
                            <ItemStyle Width="10%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="fechalib" HeaderText="Fecha liberado">
                            <ItemStyle Width="12%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="fechapago" HeaderText="Fecha de pago">
                            <ItemStyle Width="12%"/>
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="PAGAR" HeaderText="" Text="Pagar">
                        </asp:ButtonColumn>
                    </Columns>
                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                </asp:DataGrid>
            </div>                   
        </div>
    </section>
        
</asp:Content>

