<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_PORPAGARPER.aspx.vb" Inherits="SNTE5.CRED_EXP_PORPAGARPER" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Expedientes en espera de aprobación</span>
        </header>
        <div class="panel-body">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="module_subsec align_items_flex_center columned low_m">
                        <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto actual:</span>
                        <asp:TextBox ID="lbl_presactual" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_elements module_subsec_medium-elements" Enabled="false"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp; 
                    </div>
                    <div class="module_subsec align_items_flex_center columned low_m ">
                        <span class="module_subsec_elements module_subsec_medium-elements">Total:</span>
                        <asp:TextBox ID="lbl_acumulado" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_medium-elements" Enabled="false"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="module_subsec align_items_flex_center columned low_m ">
                        <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto final:</span>
                        <asp:TextBox ID="lbl_presfinal" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_medium-elements" Enabled="false"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                    </div>

                    <div align="right">
                        <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" />
                    </div>
                    <div align="right">
                        <asp:Label runat="server" ID="lbl_registros_tol" CssClass="module_subsec_elements module_subsec_medium-elements" />
                    </div>
                    <div align="right">
                        <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Seleccionar todos:" />
                        <asp:CheckBox runat="server" ID="ckb_todos" AutoPostBack="true" OnCheckedChanged="Suma" />
                    </div>
                    <br />
                    <div class="overflow_x shadow module_subsec">
                        <!-- Tabla de Expedientes generados por sucursal -->
                        <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundField DataField="FOLIO" HeaderText="No. control" Visible="false">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CLAVE" HeaderText="Folio">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENTE" HeaderText="Agremiado">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NUMCTRL" HeaderText="RFC">
                                    <ItemStyle Width="7%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NUMCLIENTE" HeaderText="Núm. cliente" Visible="false">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCTO" HeaderText="Producto"></asp:BoundField>
                                <asp:BoundField DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MONTOANT" HeaderText="Monto Anterior">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MONTOREAL" HeaderText="Monto Real">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                                    <ItemStyle Width="8%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CREADOX" HeaderText="Capturista">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VOTO" HeaderText="Voto" Visible="false">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Autorizar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="true" OnCheckedChanged="Suma" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                        <asp:Label runat="server" ID="CLAVE_PRODUCTO" Visible="false" Text='<%#Eval("CLAVE_PRODUCTO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DAG_Analisis" />
                    <asp:AsyncPostBackTrigger ControlID="ckb_todos" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="module_subsec low_m columned four columns top_m flex_end">
                <div class="module_subsec_elements flex_end">
                    <asp:Button ID="btn_aprobar" runat="server" class="btn btn-primary" Text="Aprobar" ToolTip="Descargar Archivo de Pago a Bancos." Visible="false" />
                </div>
            </div>
            <div align="center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
            </div>
        </div>
    </section>
</asp:Content>

