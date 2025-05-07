<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_PAGARES_RECEPCION.aspx.vb" Inherits="SNTE5.CRED_CNF_PAGARES_RECEPCION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>Administración de lote de pagarés</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
          

                            <div class="module_subsec">
                                <span >Dar click en recibir para enviar acuse de recepcion de un lote de pagaré.</span>
                            </div>
                                <div class="overflox-x module_subsec shadow">
                                <asp:DataGrid ID="dag_LotesPend" runat="server" AutoGenerateColumns="False" CellPadding="4" class="table table-striped"
                                        GridLines="None" Width="100%">

                                    <Columns>
                                        <asp:BoundColumn DataField="idlote" HeaderText="Id Lote">
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="folioini" HeaderText="Folio inicial">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="foliofin" HeaderText="Folio final">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="userorig" HeaderText="Usuario origen">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="fechaenvio" HeaderText="Fecha envio">
                                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="RECIBE" HeaderText="" Text="Recibido">
                                            <ItemStyle ForeColor="#054B66" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                </asp:DataGrid>
                            </div>
                            <center>
                                    <asp:Label runat="server" CssClass="alerta" ID="lbl_Alerta"></asp:Label>
                            </center>
                  
                    </div>
                </div>
            </section>
        
    
</asp:Content>

