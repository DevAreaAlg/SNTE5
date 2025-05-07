<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_PROC_UPLOAD_TOTAL_TRABAJADOR.aspx.vb" Inherits="SNTE5.PEN_PROC_UPLOAD_TOTAL_TRABAJADOR" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
            <asp:Label ID="lblLoadingMessage" runat="server" Text="Procesando..." CssClass="Loading_Message"> </asp:Label>
        </div>
        <div class="module_subsec flex_center" id="Loading">
            <asp:Image ID="lblLoadingMessageGif" runat="server" ImageUrl="~/img/Loading.gif" />
        </div>
    </div>
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <span>Procesa información del agremiado</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="module_subsec low_m top_m flex_start">
                        <asp:Label ID="lbl_TamMax" runat="server" Text="Nota: La información dentro del layout no debe contener comas (,)."></asp:Label>
                    </div>
                    <div class="module_subsec columned low_m four_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_Layout" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="true" />
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Layout:</span>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_Quincenas" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Cortes de Nómina:</span>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="module_subsec_elements no_tbm " Enabled="false" Style="margin-bottom: -100px;" Visible="false" />
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
<%--                                 OnClientClick="BtnClick()" --%>
                                <asp:Button ID="btn_Subir" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Validar" Visible="false"/>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_status_ex" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_status_exito" runat="server" CssClass="alertaExito"></asp:Label>
                    </div>


                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex: 1;">

                            <asp:DataGrid ID="dag_PagosXAplicar" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None">
                                <Columns>
                                    <%--0--%><asp:BoundColumn DataField="LOTE" HeaderText="No. Lote">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <%--1--%><asp:BoundColumn DataField="FECHA" HeaderText="Fecha/hora carga">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <%--2--%><asp:BoundColumn DataField="IDUSUARIO" HeaderText="Usuario">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <%--3--%><asp:ButtonColumn DataTextField="APLICA" ButtonType="LinkButton" CommandName="aplica" HeaderText="Correctos">
                                        <ItemStyle ForeColor="#074b7a" Font-Underline="true" Font-Bold="true" Width="10%" />
                                    </asp:ButtonColumn>
                                    <%--4--%><asp:ButtonColumn DataTextField="NOAPLICA" CommandName="noaplica" HeaderText="Incorrectos">
                                        <ItemStyle ForeColor="#074b7a" Font-Bold="true" Font-Underline="true" Width="10%" />
                                    </asp:ButtonColumn>
                                    <%--5--%><asp:ButtonColumn DataTextField="" CommandName="procesar" Text="Procesar">
                                        <ItemStyle Font-Bold="true" ForeColor="#074b7a" Font-Underline="true" Width="10%" />
                                    </asp:ButtonColumn>
                                    <%--6--%><asp:ButtonColumn DataTextField="" CommandName="cancelar" Text="Cancelar">
                                        <ItemStyle Width="10%" Font-Bold="true" ForeColor="#074b7a" Font-Underline="true" />
                                    </asp:ButtonColumn>
                                    <%--7--%><asp:BoundColumn DataField="TRABAJADORES"  HeaderText="TRABAJADORES">
                                    </asp:BoundColumn>
                                    <%--8--%><asp:BoundColumn DataField="TOTAL_APORTACION" HeaderText="TOTAL APORTACIÓN">
                                    </asp:BoundColumn>
                                    <%--9--%><asp:BoundColumn DataField="ANIO" HeaderText="Año" Visible="true"></asp:BoundColumn>
                                    <%--10--%><asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena" Visible="true"></asp:BoundColumn>
                                    <%--11--%><asp:BoundColumn DataField="APLICA" HeaderText="APLICA" Visible="False"></asp:BoundColumn>
                                    <%--12--%><asp:BoundColumn DataField="NOAPLICA" HeaderText="NOAPLICA" Visible="False"></asp:BoundColumn>
                                    <%--13--%><asp:BoundColumn DataField="ESTATUSPRO" HeaderText="Estatus Proceso" Visible="true"></asp:BoundColumn>
                                    <%--14--%><asp:BoundColumn DataField="TIPO" HeaderText="Tipo aportación" Visible="false"></asp:BoundColumn>
                                    <%--15--%><asp:BoundColumn DataField="IDTIPO" HeaderText="Tipo aportación" Visible="false"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="module_subsec overflow_x shadow">
                        <div class="flex_1">
                            <asp:DataGrid ID="dgd_prejubilados" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                HorizontalAlign="Center">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="RFC" HeaderText="RFC">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE_PERSONA" HeaderText="Agremiado">
                                        <ItemStyle Width="40%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Resultado">
                                        <ItemStyle Width="40%" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>


</asp:Content>
