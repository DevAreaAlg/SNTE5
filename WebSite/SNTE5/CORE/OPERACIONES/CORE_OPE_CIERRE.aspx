<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_CIERRE.aspx.vb" Inherits="SNTE5.CORE_OPE_CIERRE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div id="dvLoading" runat="server" class="loadingMessageFrame" style="display: none;">
        <div class="module_subsec flex_center">
            <asp:label id="lblLoadingMessage" runat="server" text="Procesando..." cssclass="Loading_Message"> </asp:label>
        </div>
        <div class="module_subsec flex_center" id="Loading"> 
            <asp:Image id="lblLoadingMessageGif" runat="server" ImageUrl="~/img/Loading.gif"/>
        </div>            
    </div>
     <div id="dvMain" runat="server">
    <section class="panel">
        <header class="panel-heading">
            <span>Cierre de operaciones</span>
        </header>

        <div class="panel-body">


            <div class="module_subsec flex_center">
                <asp:Label runat="server" CssClass="alerta" ID="lbl_status"></asp:Label>
            </div>

           
            <asp:Panel runat="server" ID="pal_descuentos_conciliar" CssClass="module_subsec flex_end" Visible="false">
                <asp:Button ID="btn_descuentos_conciliar" runat="server" class="btn btn-primary" Text="Reporte" ToolTip="Descargar archivo de descuentos por conciliar"/>
            </asp:Panel>
            <%--<div class="module_subsec flex_end">
                <asp:Button ID="btn_descuentos_conciliar" runat="server" class="btn btn-primary" Text="Reporte" ToolTip="Descargar archivo de descuentos por conciliar" Visible="false"/>
            </div>--%>
            <div class="module_subsec overflow_x shadow">
                <asp:DataGrid ID="dag_desc_conciliar" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="INSTITUCION" HeaderText="Institución"></asp:BoundColumn>
                        <asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena de descuento"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>

            <%-- <br />
                        <h4 id="lbl_if" class="module_subsec">Índices financieros</h4>
                        <div class="module_subsec overflow_x shadow">
                           <asp:DataGrid ID="dag_if" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                         <HeaderStyle CssClass="table_header"> </HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="INDICE" HeaderText="Índice">
                               
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="SEMAFORO" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        </div>
                       <br />
                        <h4 id="lbl_div" class="module_subsec">Divisas</h4>
                       
                           <div class="module_subsec overflow_x shadow">
                           <asp:DataGrid ID="dag_div" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                         <HeaderStyle CssClass="table_header"> </HeaderStyle>                       
                            <Columns>
                                <asp:BoundColumn DataField="DIVISA" HeaderText="Divisa">
                                   
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="SEMAFORO" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="center"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                               </div>
            --%>
            <%-- <br />--%>
            <div class="module_subsec flex_end ">
                <label id="lbl_fecha_sistema" class="texto" style="margin-right: 10px">Presione ejecutar para cerrar el día:</label>

                <asp:Button runat="server" Text="Ejecutar"
                    ToolTip="Ejecuta el cierre diario" ID="btn_cambiar" 
                    CssClass="btn btn-primary" OnClientClick="BtnClick()"></asp:Button>

            </div>

            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_guardar2" runat="server" ConfirmText="¿Esta seguro de ejecutar el cierre de día?" TargetControlID="btn_cambiar">
            </ajaxToolkit:ConfirmButtonExtender>
        </div>


    </section>
       </div>
</asp:Content>

