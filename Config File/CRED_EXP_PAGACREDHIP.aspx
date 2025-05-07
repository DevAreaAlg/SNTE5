<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_PAGACREDHIP.aspx.vb" Inherits="SNTE5.CRED_EXP_PAGACREDHIP" MaintainScrollPositionOnPostback="true" %>

<asp:Content runat="Server" ID="Content1" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function BtnClick() {
            document.getElementById("Loading").innerHTML = document.getElementById("Loading").innerHTML;
            var lLoadingMessage = document.getElementById('<%=lblLoadingMessage.ClientID %>');
            var dvLoading = document.getElementById('<%=dvLoading.ClientID %>');
            var dvMain = document.getElementById('<%=dvMain.ClientID %>');
            if (dvMain != null) dvMain.style.display = 'none';
            if (dvLoading != null) dvLoading.style.display = '';
            return true;
        }
    </script>
</asp:Content>

<asp:Content runat="Server" ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1">
    <div runat="server" id="dvLoading" class="loadingMessageFrame" style="display: none;">
        <div class="module_subsec flex_center">
            <asp:Label runat="server" ID="lblLoadingMessage" Text="Procesando..." CssClass="Loading_Message" />
        </div>
        <div id="Loading" class="module_subsec flex_center">
            <asp:Image runat="server" ID="lblLoadingMessageGif" ImageUrl="~/img/Loading.gif" />
        </div>
    </div>
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <span>Expedientes en espera de pago</span>
            </header>
            <div class="panel-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div align="right">
                            <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" />
                        </div>
                        <div align="right">
                            <asp:Label runat="server" ID="lbl_registros_tol" CssClass="module_subsec_elements module_subsec_medium-elements" />
                        </div>
                        <div align="right">
                            <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto actual:</span>
                            <asp:TextBox ID="lbl_presactual" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                        </div>
                        <div align="right">
                            <span class="module_subsec_elements module_subsec_medium-elements">Total:</span>
                            <asp:TextBox ID="lbl_acumulado" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                        </div>
                        <div align="right">
                            <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto final:</span>
                            <asp:TextBox ID="lbl_presfinal" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                        </div>
                        <div align="right">
                            <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Secuencia:" />
                            <asp:TextBox runat="server" ID="txt_secuencia" MaxLength="2" Enabled="true"
                                CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" ControlToValidate="txt_secuencia"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_laybancos" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txt_secuencia" />
                        </div>
                        <div align="right">
                            <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Seleccionar todos:" />
                            <asp:CheckBox runat="server" ID="ckb_todos" AutoPostBack="true" OnCheckedChanged="Suma" />
                        </div>
                        <br />
                        <div class="overflow_x shadow ">
                            <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundField DataField="CLAVE" HeaderText="Folio">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CLIENTE" HeaderText="Agremiado">
                                        <ItemStyle Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUMCTRL" HeaderText="RFC">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                        <ItemStyle Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MONTO" HeaderText="Monto Real">
                                        <ItemStyle Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VOTO" HeaderText="Voto" Visible="false">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NOTARIO" HeaderText="Gasto Notarial" Visible="false">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PAGARE" HeaderText="Pagaré">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Pagar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="true" OnCheckedChanged="Suma" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="NUMCLIENTE" Visible="false" Text='<%#Eval("NUMCLIENTE") %>' />
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
                    <asp:Button runat="server" ID="btn_layout_bancos" CssClass="btn btn-primary"
                        Text="Layout para Banco" Visible="false" ValidationGroup="val_laybancos" />
                </div>
                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec columned low_m four_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:FileUpload runat="server" ID="fud_layout_bancos" CssClass="module_subsec_elements no_tbm "
                                Style="margin-bottom: -100px;" Visible="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="lbl_layout_bancos" CssClass="text_input_nice_label" 
                                    Text="*Layout de Bancos Movimientos Aplicados:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Button runat="server" ID="btn_pagar" CssClass="btn btn-primary module_subsec_elements no_tbm"
                                Text="Pagar" Visible="false" OnClientClick="BtnClick()" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
