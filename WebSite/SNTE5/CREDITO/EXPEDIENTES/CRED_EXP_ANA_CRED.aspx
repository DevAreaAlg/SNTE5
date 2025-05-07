<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_ANA_CRED.aspx.vb" Inherits="SNTE5.CRED_EXP_ANA_CRED" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
            <section class="panel">
                <header class="panel-heading">
                    <span>Análisis</span>
                </header>
                <div class="panel-body">

                   <div class="panel-body_content init_show">
                        <div class="module_subsec  flex_end">
                            <asp:Button ID="btn_actualizar" class="btn btn-primary" runat="server" OnClick="btn_actualizar_Click" Text="Actualizar" />
                        </div>


                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_AlertaNoAcceso" runat="server" Class="alerta"></asp:Label>
                        </div>
                        
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <Columns>
                                    <asp:BoundColumn ItemStyle-Width="40" DataField="FOLIO" HeaderText="Folio">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn ItemStyle-Width="40" DataField="PRODUCTO" HeaderText="Producto">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn ItemStyle-Width="40" DataField="Cliente" HeaderText="Núm. afiliado">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn ItemStyle-Width="245" DataField="Prospecto" HeaderText="Prospecto">
                                        <ItemStyle Width="20%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn ItemStyle-Width="135" DataField="Estatus" HeaderText="Estatus">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn ItemStyle-Width="110" DataField="Fecha" HeaderText="Fecha">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn ItemStyle-Width="30" CommandName="ANALISIS1"
                                        HeaderText="Primer análisis" Text="Analizar">
                                        <ItemStyle Width="10%" ></ItemStyle>
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn ItemStyle-Width="20" DataField="Procesando" HeaderText="En uso">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                            </asp:DataGrid>
                        </div>            
                       
                        <div align="center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_cola1" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped"
                                Font-Size="10pt" HorizontalAlign="Center" TabIndex="17">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Cliente" HeaderText="Afiliado">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Prospecto" HeaderText="Prospecto">
                                        <ItemStyle Width="35%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Sucursal" HeaderText="Sucursal">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="VALIDAR" Text="Validar">
                                        <ItemStyle Width="10%" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="IDDISP">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                    </div>
                </div>
            </section>

</asp:Content>

