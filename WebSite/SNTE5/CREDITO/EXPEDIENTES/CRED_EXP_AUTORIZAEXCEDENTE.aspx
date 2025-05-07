<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_AUTORIZAEXCEDENTE.aspx.vb" Inherits="SNTE5.CRED_EXP_AUTORIZAEXCEDENTE" MaintainScrollPositionOnPostback="true" %>

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
                <span>Expedientes excedentes en espera de autorización</span>
            </header>
            <div class="panel-body">
                <div align="right">
                    <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" />
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
                            <asp:BoundField DataField="MONTO" HeaderText="Monto Real">
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="PAGOFIJO" HeaderText="Pago Fijo">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="CAPPAGO" HeaderText="Capacidad Pago">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="true" />
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
              
                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec columned low_m four_columns">
                    
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Button runat="server" ID="btn_autorizar" CssClass="btn btn-primary module_subsec_elements no_tbm"
                                Text="Aprobar" Visible="false" OnClientClick="BtnClick()" />

                        </div>
                    </div>
                     <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Button runat="server" ID="btn_rechazar" CssClass="btn btn-primary module_subsec_elements no_tbm"
                                Text="Rechazar" Visible="false" OnClientClick="BtnClick()" />

                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
