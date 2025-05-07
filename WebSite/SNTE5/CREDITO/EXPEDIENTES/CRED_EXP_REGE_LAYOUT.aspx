<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_REGE_LAYOUT.aspx.vb" Inherits="SNTE5.CRED_EXP_REGE_LAYOUT" MaintainScrollPositionOnPostback="true" %>

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

        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
            }
            else {
                var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
             }
             if (wbusf != null) {
                 document.getElementById("hdn_busqueda").value = wbusf;
                 __doPostBack('', '');
             }
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
        
    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Selección del agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="tbx_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox ID="txt_IdCliente" runat="server" CssClass="text_input_nice_input" MaxLength="9" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Depe_NumCtrl">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                        <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" ValidationGroup="val_Depe_NumCtrl" />
                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar agremiado" />
                    </div>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                    <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </span>
                    <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
                </div>
                <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
                <asp:Label ID="lbl_maxreest" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="pnl_expedientes">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_expedientes">
            <span class="panel_folder_toogle_header">Préstamos/Expedientes</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_expedientes">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_expedientes">
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_Expendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None">
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="folio" HeaderText="Folio" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cveexp" HeaderText="Folio">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="producto" HeaderText="Producto">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sucursal" HeaderText="Sucursal">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha alta">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="tipo" HeaderText="Tipo">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="CONSULTAR" Text="Consultar">
                                <ItemStyle Width="5%" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </section>

           <!------------------------------ PANEL BANCARIOS ------------------------------>
    <asp:UpdatePanel ID="upnl_bancarios" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_bancarios">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_bancarios">
                    <span class="panel_folder_toogle_header">Datos bancarios</span>
                    <span class="panel_folder_toogle up" runat="server" id="toggle_panel_bancarios">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_bancarios">
                        <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos bancarios del agremiado:</h5>
                        <div class="module_subsec low_m columned three_columns">
                            <!--------------- Banco --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="cmb_banco" CssClass="btn btn-primary2 dropdown_label"
                                        AutoPostBack="False" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Banco:" />
                                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" Display="Dynamic"
                                            ControlToValidate="cmb_banco" ErrorMessage=" Falta Dato!" InitialValue="-1"
                                            ValidationGroup="val_PersonaBANC" />
                                    </div>
                                </div>
                            </div>
                            <!--------------- Cuenta CLABE --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_clabe" CssClass="text_input_nice_input" MaxLength="18" />
                                    <div class="text_input_nice_labels">
                                        <%--<asp:Label runat="server" ID="lbl_valCta" Text="*Medio:"/>--%>
                                        <asp:Label runat="server" Text="*CLABE:"/>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_clabe"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_PersonaBANC"/>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_clabe"/>
                                    </div>
                                </div>
                            </div>
                            <!--------------- Cuenta CLABE --------------->
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" ID="txt_clabe_conf" CssClass="text_input_nice_input" MaxLength="18" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" Text="*Confirmar CLABE:"/>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_clabe_conf"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_PersonaBANC"/>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_clabe_conf"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns">
                            
                        </div>
                        <div align="center">
                            <asp:Label runat="server" ID="lbl_estatus_bank" CssClass="alerta"/>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button runat="server" ID="btn_guarda_bank" CssClass="btn btn-primary" 
                                OnClick="btn_guarda_bank_Click" ValidationGroup="val_PersonaBANC" Text="Guardar" AutoPostBack="false" />
                        </div>
                    </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

        <section class="panel" runat="server" id="panel_datos"   UpdateMode="Conditional" Visible="false">
                <header class="panel_header_folder panel-heading" runat="server" id="Header1">
                    <span class="panel_folder_toogle_header">Expedientes de Agremiado</span>
                    <span class="panel_folder_toogle up" runat="server" id="Span1">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div align="right">
                            <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" />
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
                        <br />
                        <div class="overflow_x shadow ">
                            <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%" AutoPostBack="true">
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
                                        <ItemStyle Width="8%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                       <asp:BoundField DataField="CREADOX" HeaderText="Capturista">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PAGARE" HeaderText="Pagaré">
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VOTO" HeaderText="Voto" Visible="false">
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                            <asp:Label runat="server" ID="CLAVE_PRODUCTO" Visible="false" Text='<%#Eval("CLAVE_PRODUCTO") %>' />
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
                    </Triggers>
                </asp:UpdatePanel>
                <div class="module_subsec low_m columned four columns top_m flex_end">
                    <asp:Button runat="server" ID="btn_layout_bancos" CssClass="btn btn-primary"
                        Text="Layout para Banco" Visible="false" ValidationGroup="val_laybancos" />
                    &nbsp; &nbsp; &nbsp;
                </div>
                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>
              
            </div>
        </section>
    </div>
</asp:Content>
