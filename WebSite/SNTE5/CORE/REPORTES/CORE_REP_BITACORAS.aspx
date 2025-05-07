<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_REP_BITACORAS.aspx.vb" Inherits="SNTE5.CORE_REP_BITACORAS" MaintainScrollPositionOnPostback ="true" %>

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
            <asp:Label ID="lblLoadingMessage" runat="server" Text="Procesando..." CssClass="Loading_Message" />
        </div>
        <div class="module_subsec flex_center" id="Loading">
            <asp:Image ID="lblLoadingMessageGif" runat="server" ImageUrl="img/Loading.gif" />
        </div>
    </div>

    <div id="dvMain" runat="server">
        <asp:UpdatePanel ID="udp_LOGS" runat="server">
            <ContentTemplate>
                <section class="panel">
                    <header class="panel_header_folder panel-heading">
                        <span>SELECCIÓN DE CRITERIOS</span>
                        <span class=" panel_folder_toogle down">&rsaquo;</span>
                    </header>
                    <div class="panel-body">
                        <div class="panel-body_content init_show">
                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="ddl_logs" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Seleccione bitácora:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_logs" runat="server"
                                                ControlToValidate="ddl_logs" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_fecha" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_initdate" runat="server" CssClass="text_input_nice_input"
                                            MaxLength="10" ValidationGroup="val_fecha" AutoPostBack="false" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_initdate"
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_initdate" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_initdate" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_initdate" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Fecha inicial:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_initdate" runat="server"
                                                ControlToValidate="txt_initdate" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_fecha" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_initdate"
                                                runat="server" ControlExtender="MaskedEditExtender_initdate"
                                                ControlToValidate="txt_initdate" CssClass="textogris"
                                                ErrorMessage="MaskedEditValidator_initdate"
                                                InvalidValueMessage="Fecha Invalida" />
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements" style="flex: 1;">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_enddate" runat="server" CssClass="text_input_nice_input"
                                            MaxLength="10" ValidationGroup="val_fecha" AutoPostBack="false" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_enddate" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_txt_enddate"
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txt_enddate" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Fecha final:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_enddate" runat="server"
                                                ControlToValidate="txt_enddate" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_fecha" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_txt_enddate"
                                                runat="server" ControlExtender="MaskedEditExtender_txt_enddate"
                                                ControlToValidate="txt_enddate" CssClass="textogris"
                                                ErrorMessage="MaskedEditValidator_txt_enddate"
                                                InvalidValueMessage="Fecha Invalida" ValidationGroup="val_fecha" />
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements" style="flex: 1;">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_folio" CssClass="text_input_nice_input" MaxLength="15" runat="server" Enabled="false" />
                                        <div class="text_input_nice_labels">
                                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Folio expediente: (Ejemplo PC-2019-000001)" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_txt_folio" ControlToValidate="txt_folio"
                                                Display="Dynamic" ValidationExpression="^([pa|pc|pd|PA|PC|PD]{2}[-]{1}[\d]{4}[-]{1}[\d]{6})$"
                                                runat="server" CssClass="alertaValidator" ErrorMessage="Folio Incorrecto!" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_folio" runat="server" Enabled="True"
                                                FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom" ValidChars="-" TargetControlID="txt_folio" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="ddl_users" CssClass="btn btn-primary2 dropdown_label" runat="server" Enabled="false" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Usuario:</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="ddl_events" CssClass="btn btn-primary2 dropdown_label" runat="server" Enabled="false" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Evento:</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="ddl_modules" CssClass="btn btn-primary2 dropdown_label" runat="server" Enabled="false" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Módulo:</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec low_m columned three_columns flex_start">
                                <div class="module_subsec_elements">
                                    <asp:Button ID="btn_search" runat="server" class="btn btn-primary btn-Exposures" Text="Buscar"
                                        Visible="true" ValidationGroup="val_fecha" Enabled="false" />
                                </div>
                            </div>
                            <div class="module_subsec flex_center">
                                <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta" />
                            </div>
                        </div>
                    </div>
                </section>
                <section class="panel">
                    <header class="panel_header_folder panel-heading">
                        <span>DESCARGA DE BITÁCORAS</span>
                        <span class=" panel_folder_toogle down">&rsaquo;</span>
                    </header>
                    <div class="panel-body">
                        <div class="panel-body_content init_show">
                            <div class="module_subsec">
                                <div class="overflow_x shadow" style="flex: 1;">
                                    <label id="lbl_Excel" class="texto" visible="false">Descarga:</label>&nbsp;&nbsp;
                                    <asp:ImageButton ID="btn_EXCEL" runat="server" Height="30px"
                                        ImageUrl="~/img/excel.png" ToolTip="Download" Visible="false" />
                                </div>
                            </div>
                            <div class="module_subsec">
                                <div class="overflow_x shadow" style="flex: 1;">
                                    <%-- GRID ERROR --%>
                                    <%--<asp:GridView ID="DAG_LOGERROR" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="EVENTO" HeaderText="Evento">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ERROR" HeaderText="Error">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDSESION" HeaderText="Sesión">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>--%>
                                    <%-- GRID SESIONES --%>
                                    <asp:GridView ID="DAG_LOGSESION" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="EVENTO" HeaderText="Evento">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                                <ItemStyle Width="160px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MAC" HeaderText="MAC">
                                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MODULO" HeaderText="Módulo">
                                                <ItemStyle Width="160px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDSESION" HeaderText="Sesión">
                                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                    <%-- GRID TRANSACCIONES --%>
                                    <asp:GridView ID="DAG_LOGTRANS" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="EVENTO" HeaderText="Evento">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TRANS" HeaderText="Transacción">
                                                <ItemStyle Width="170px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDSESION" HeaderText="Sesión">
                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                    <%-- GRID PERMISOS --%>
                                    <asp:GridView ID="DAG_PERMISOS" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="USUARIOID" HeaderText="Número">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Center" Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PUESTO" HeaderText="Puesto">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SUCURSAL" HeaderText="Sucursal">
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MASCOREUSR" HeaderText="Usuario">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MODULO" HeaderText="Módulo">
                                                <ItemStyle HorizontalAlign="Center" Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TIPOMODULO" HeaderText="Tipo">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHAASIGNADO" HeaderText="Fecha">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                    <%-- GRID CIERRE DE DÍAS --%>
                                    <asp:GridView ID="DAG_CIERREDIAS" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="FECHA_SISTEMA" HeaderText="Fecha de Sistema">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NUM_PROCESO" HeaderText="Núm. Proceso">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Center" Width="140px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="FECHA_INICIO" HeaderText="Hora Inicio">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_FINAL" HeaderText="Hora Fin">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SEGUNDOS" HeaderText="Segundos">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOTAL" HeaderText="Total Segundos">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                    <%-- GRID ESTATUS EXPEDIENTE --%>
                                    <asp:GridView ID="DAG_ESTEXP" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CssClass="table table-striped"
                                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="false" AllowPaging="true" PagerStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="FOLIO" HeaderText="Expediente">
                                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Sistema">
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_CREADO" HeaderText="Fecha Creación">
                                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TIEMPO (DIA HRA:MIN)" HeaderText="Tiempo (Día hra:min)">
                                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_search" />
                <asp:PostBackTrigger ControlID="btn_EXCEL" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
