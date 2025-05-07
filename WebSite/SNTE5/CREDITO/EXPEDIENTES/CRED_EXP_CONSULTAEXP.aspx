<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_CONSULTAEXP.aspx.vb" Inherits="SNTE5.CRED_EXP_CONSULTAEXP" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        // Javascript para utilizar la Busqueda de un Afiliado o Persona
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

        function ClickBotonBusqueda(ControlTextbox, ControlButton) {
            var CTextbox = document.getElementById(ControlTextbox);
            var CButton = document.getElementById(ControlButton);
            if (CTextbox != null && CButton != null) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    if (CTextbox.value != "") {
                        CButton.click();
                        CButton.disabled = true;
                    }
                    else {
                        CTextbox.focus()
                        return false
                    }
                    return true
                }
            }
        }

        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=800,height=450,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }

        function det_com() {
            window.open("CRED_EXP_DICTAMEN.aspx", "DD", "width=800px,height=450px,Scrollbars=YES");
        }

        function det_restructura() {
            window.open("CRED_EXP_REESTRUCTURA.aspx", "DR", "width=650px,height=450px,Scrollbars=YES");
        }
    </script>
    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Selección de agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="tbx_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox ID="txt_IdCliente" runat="server" CssClass="text_input_nice_input" MaxLength="10" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage="Falta Dato!"
                                ValidationGroup="val_rfc"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                        <asp:Button runat="server" ID="lnk_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm"
                            Text="Continuar" ValidationGroup="val_rfc"></asp:Button>
                        <asp:Button ID="lnk_BusquedaPersona" runat="server" class="btn btn-primary module_subsec_elements no_tbm" Text="Buscar agremiado"></asp:Button>
                    </div>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                    <label id="lbl_NombrePersonaBusquedTexto" runat="server" class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de Agremiado: </label>
                    <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" CssClass="texto"></asp:Label>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statusc" runat="server" class="alerta"></asp:Label>
                </div>
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

    <section class="panel" runat="server" visible="false" id="pnl_cnfexp">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_cnfexp">
            <span class="panel_folder_toogle_header">Datos del expediente</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_cnfexp">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content " runat="server" id="content_pnl_cnfexp">
                <table border="0" width="100%">
                    <tr>
                        <td width="75%">
                            <asp:Panel ID="pnl_info_Credito" runat="server" Visible="false">
                                <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                    <div class="module_subsec_elements vertical flex_1">
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_folio" runat="server" Text="Folio: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_folioa" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="Label3" runat="server" Text="Número de SIA: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_folioSIA" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>

                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_ProductoDetalleA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_ProductoDetalleB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>

                                        <div class="module_subsec columned no_m align_items_flex_center">
                                            <asp:Label ID="lbl_MontoA" runat="server" Text="Monto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_MontoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>

                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_fechaliberaA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Fecha de inicio estimada: "></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_fechaliberaB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>

                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Fecha final estimada: "></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_fechaV" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>



                                    </div>



                                    <div class="module_subsec_elements vertical flex_1">
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_TasaMoraA" runat="server" Text="Tasa moratoria mensual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_TasaMoraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>


                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_TasaNormalA" runat="server" Text="Tasa ordinaria anual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_TasaNormalB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>


                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_tipoplanA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo de plan de pagos: "></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_tipoplanB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" Text="Ver"></asp:LinkButton>
                                        </div>


                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_PlazoA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_PlazoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>


                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_estatus" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Estatus:"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_estatusB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnl_info_Captacion" runat="server" Visible="False">
                                <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                    <div class="module_subsec_elements vertical flex_1">
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_exp_CapA" runat="server" Text="Núm. de expediente: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_exp_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_num_sia_CapA" runat="server" Text="Número SIA: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_num_sia_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_producto_CapA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_producto_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_fecha_dep_CapA" runat="server" Text="Último depósito: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_fecha_dep_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_fecha_Retiro_CapA" runat="server" Text="Último retiro: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_fecha_Retiro_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements vertical flex_1">
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_Saldo_Actual_CapA" runat="server" Text="Saldo actual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_Saldo_Actual_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_plazo_CapA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_plazo_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_tasa_CapA" runat="server" Text="Tasa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_tasa_CapB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_statusCAPA" runat="server" Text="Estatus: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_statusCAPB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <label id="lbl_Bene_CAPA" runat="server" class="texto">
                                    Beneficiarios
                                </label>
                                <asp:DataGrid ID="DAG_BENE_CAPB" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" Width="100%">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CVEBENEFICIARIO" HeaderText="CVEBENEFICIARIO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PORCENTAJE" HeaderText="Porcentaje"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                            <asp:Panel ID="pnl_info_Inversion" runat="server" Visible="False">
                                <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                    <div class="module_subsec_elements vertical flex_1">
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_num_exp_InvA" runat="server" Text="Núm. de expediente: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_num_exp_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_num_cte_SIA_InvA" runat="server" Text="Número SIA: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_num_cte_SIA_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_nom_prod_InvA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_nom_prod_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_Plazo_INVA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_Plazo_INVB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="Lbl_Fechas_InvA" runat="server" Text="Fechas(inicio-fin): " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="Lbl_Fechas_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_tasa_InvA" runat="server" Text="Tasa: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_tasa_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="Lbl_monto_InvA" runat="server" Text="Monto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="Lbl_monto_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="Lbl_Monto_intgen_InvA" runat="server" Text="Interés generado: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="Lbl_Monto_intgen_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="Lbl_Fecha_Ultimo_Pago_InvA" runat="server" Text="Fecha últ. Cap. Interés: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="Lbl_Fecha_Ultimo_Pago_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_inst_InvA" runat="server" Text="Intrucción de reinversión: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_inst_InvB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                        <div class="module_subsec columned no_m align_items_flex_center ">
                                            <asp:Label ID="lbl_status_INVA" runat="server" Text="Estatus: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                            <asp:TextBox Enabled="false" ID="lbl_status_INVB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <label id="lbl_beneficiario_InvA" runat="server" class="texto">
                                    Beneficiarios
                                </label>
                                <asp:DataGrid ID="dag_Beneficiarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%" Visible="False">
                                    <AlternatingItemStyle CssClass="AlternativeStyle" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CVEBENEFICIARIO" HeaderText="CVEBENEFICIARIO" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PORCENTAJE" HeaderText="Porcentaje">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </td>
                        <td width="25%">
                            <asp:LinkButton ID="lnk_persona" runat="server" CssClass="module_subsec flex_center" Text="Datos agremiado"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_docsexp" runat="server" CssClass="module_subsec flex_center" Text="Documentación"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_busqueda_gtia" runat="server" CssClass="module_subsec flex_center" Text="Garantías"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_notas" runat="server" class="module_subsec flex_center" Text="Notas expediente " ToolTip="Dé click si desea ver las notas del expediente." />
                            <asp:LinkButton ID="lnk_solicitud" runat="server" CssClass="module_subsec flex_center" Text="Descarga Solicitud"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <div class="module_subsec low_m">
                    <h5 class="no_bm">Adelantos</h5>
                </div>
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_reest" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None">
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="folio" HeaderText="Folio" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cveexp" HeaderText="Clave">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha adelanto">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="transcred" HeaderText="Fecha alta" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="transcap" HeaderText="Fecha alta" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="abono" HeaderText="Fecha alta" Visible="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="refpago" HeaderText="Referencia de pago">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="GENERAR" Text="Generar recibo">
                                <ItemStyle Width="5%" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="stn_movimientos">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Movimientos Préstamo</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <div class="module_subsec columned low_m four_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_pagadas" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Quincenas pagadas:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_incompletas" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Quincenas incompletas:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_atrasadas" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Quincenas atrasadas:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_faltantes" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Quincenas faltantes:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec columned low_m four_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_montoPagado" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Monto pagado:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>
                    <%--<div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_montoAtrasado" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Monto atrasado:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_montoxPagar" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Monto por pagar (capital e intereses):" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox Enabled="false" ID="txt_montoxLiquidar" runat="server" CssClass="text_input_nice_input" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="Monto por liquidar:" CssClass="text_input_nice_label" />
                            </div>
                        </div>
                    </div>--%>
                </div>

                <div class="module_subsec low_m columned four columns top_m flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Button ID="btn_generaExcel" CssClass="btn btn-primary"
                            runat="server" Text="Genera Excel" />
                    </div>
                </div>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned low_m four_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_anios_movimiento" CssClass="btn btn-primary2 dropdown_label"
                                        runat="server" AutoPostBack="true" />
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Filtrar por Año:</span>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_estatus_movimiento" CssClass="btn btn-primary2 dropdown_label"
                                        runat="server" AutoPostBack="true" />
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Filtrar por Estatus:</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        &nbsp; &nbsp; &nbsp;
                        <div class="module_subsec overflow_x shadow flex_1" style="width:100% !important">
                            <asp:GridView ID="dgd_movimientos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="QUINCENA" HeaderText="Quincena">
                                        <ItemStyle Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ANIO" HeaderText="Año">
                                        <ItemStyle Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IDTRANS" HeaderText="Id Transacción">
                                        <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CVEFOLIO" HeaderText="Folio">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHADESC" HeaderText="Fecha Descuento">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CAPITAL" HeaderText="Capital">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INTORD" HeaderText="Interés Ordinario">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SEGHIP" HeaderText="Seguro Hipotecario">
                                        <ItemStyle Width="8%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TOTAL" HeaderText="Total">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="APLICADOREAL" HeaderText="Aplicado Real">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                                        <ItemStyle Width="12%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_anios_movimiento" />
                        <asp:AsyncPostBackTrigger ControlID="ddl_estatus_movimiento" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <section class="panel" runat="server" visible="false" id="stn_aportaciones">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Cuotas y Aportaciones</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned low_m four_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_anios_aportaciones" CssClass="btn btn-primary2 dropdown_label"
                                        runat="server" AutoPostBack="true" />
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Filtrar por Año:</span>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"></div>
                            <div class="module_subsec_elements"></div>
                            <div class="module_subsec_elements flex_end">
                                <asp:Button runat="server" ID="btn_info_aportaciones" CssClass="btn btn-primary align_items_flex_end" Text="Mostrar Porcentajes" />
                            </div>
                        </div>
                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView ID="dgd_aportaciones" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                    HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="QUINCENA" HeaderText="Quincena"></asp:BoundField>
                                        <asp:BoundField DataField="ANIO" HeaderText="Año"></asp:BoundField>
                                        <asp:BoundField DataField="APORT_TRABAJADOR" HeaderText="Aportación del Agremiado"></asp:BoundField>
                                        <asp:BoundField DataField="APORT_ENTIDAD" HeaderText="Aportación del Gobierno"></asp:BoundField>
                                        <asp:BoundField DataField="PCT_APORT_TRABAJADOR" HeaderText="Porcentaje Aportación del Agremiado"></asp:BoundField>
                                        <asp:BoundField DataField="PCT_APORT_ENTIDAD" HeaderText="Porcentaje Aportación del Gobierno"></asp:BoundField>
                                        <%--                                        <asp:BoundField DataField="ESTATUS_APORT" HeaderText="Estatus"></asp:BoundField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_anios_aportaciones" />
                        <asp:AsyncPostBackTrigger ControlID="btn_info_aportaciones" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="stn_nomina">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Nómina</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView ID="dag_nomina" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                    HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="QUINCENA" HeaderText="Quincena"></asp:BoundField>
                                        <asp:BoundField DataField="ANIO" HeaderText="Año"></asp:BoundField>
                                        <asp:BoundField DataField="PERCEPCION" HeaderText="Percepciones"></asp:BoundField>
                                        <asp:BoundField DataField="DEDUCCION" HeaderText="Deducciones"></asp:BoundField>
                                        <asp:BoundField DataField="SALARIO" HeaderText="Salario Neto"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_anios_aportaciones" />
                        <asp:AsyncPostBackTrigger ControlID="btn_info_aportaciones" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="sectionAhorro">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Devolución de ahorro</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="module_subsec columned four_columns">
                            <div class="module_subsec_elements no_m">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:DropDownList ID="cmb_tipo" runat="server" AutoPostBack="False" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Medio de pago:</span>
                                            <asp:RequiredFieldValidator ID="rfv_objetivo" CssClass="alertaValidator bold" runat="server"
                                                ControlToValidate="cmb_tipo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_tipo" InitialValue="-1" Enabled="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements no_m">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Guardar" ValidationGroup="val_tipo" />

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" class="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">

                                <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" Width="100%" AutoPostBack="true">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>

                                        <asp:BoundField DataField="ID_AGREMIADO" HeaderText="Agremiado" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AGREMIADO" HeaderText="Agremiado" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CICLO" HeaderText="Ciclo">
                                            <ItemStyle Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RFC" HeaderText="RFC">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RFC_ANTERIOR" HeaderText="RFC_ANTERIOR" Visible="false">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SISTEMA" HeaderText="Sistema">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REGION" HeaderText="Región">
                                            <ItemStyle Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DELEGACION" HeaderText="Delegación">
                                            <ItemStyle Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SALDO_AHORRO" HeaderText="Saldo Ahorro" ItemStyle-HorizontalAlign="center" DataFormatString="${0:###,###,###.00}">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_OPERACION" HeaderText="Tipo" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° Cheque" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTATUS_CHEQUE" HeaderText="Estatus Cheque">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_ESTATUS_CHEQUE" HeaderText="ID_ESTATUS" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NUMCLIENTE" HeaderText="NUMCLIENTE" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TIPO_PLAZA" HeaderText="TIPO_PLAZA" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_ESTATUS_TRANSFERENCIA" HeaderText="ID_ESTATUS_TRANSFERENCIA" Visible="FALSE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTATUS_TRANSFERENCIA" HeaderText="Estatus Transferencia" Visible="true">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLAVE_RASTREO" HeaderText="Clave de Rastreo" Visible="true">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NUM_PAQUETE" HeaderText="No. de Paquete" Visible="true">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_CVE_RASTREO" HeaderText="SPEI Fecha Confirmación" Visible="true">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GENERADO_POR" HeaderText="Generado por" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_GENERADO" HeaderText="Fecha generado" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENTREGADO_POR" HeaderText="Entregado por" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_ENTREGADO" HeaderText="Fecha entregado" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PAGADO_POR" HeaderText="Pagado por" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_PAGADO" HeaderText="Fecha pagado" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANCELADO_POR" HeaderText="Cancelado por" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_CANCELADO" HeaderText="Fecha cancelado" Visible="TRUE">
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="IMPRIMIR" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chb_Imprimir" runat="server" AutoPostBack="FALSE" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="DESHACER" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chb_Deshacer" runat="server" AutoPostBack="FALSE" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="ENTREGAR" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chb_Entregar" runat="server" AutoPostBack="FALSE" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="PAGAR" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chb_Pagar" runat="server" AutoPostBack="FALSE" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="CANCELAR" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chb_Cancelar" runat="server" AutoPostBack="FALSE" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%----------------------------------------------------------------------------------%>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                            <asp:Label runat="server" ID="CLAVE_PRODUCTO" Visible="false" Text='<%#Eval("CLAVE_PRODUCTO") %>' />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="NUMCLIENTE" Visible="false" Text='<%#Eval("NUMCLIENTE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label DataField="NUM_CHEQUE" runat="server" ID="NUM_CHEQUE" Visible="FALSE" Text='<%#Eval("NUM_CHEQUE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="pnl_pensiones">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Pensiones</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView ID="dag_pensiones" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                    HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="CICLO" HeaderText="Ciclo"></asp:BoundField>
                                        <asp:BoundField DataField="BENEFICIARIO" HeaderText="Beneficiario"></asp:BoundField>
                                        <asp:BoundField DataField="MONTO" HeaderText="Monto"></asp:BoundField>
                                        <asp:BoundField DataField="TIPO" HeaderText="Tipo Operación"></asp:BoundField>
                                        <asp:BoundField DataField="CLABE" HeaderText="Clabe"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>


    <%--<section class="panel" runat="server" visible="false" id="stn_incapacidades">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Incapacidades</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned low_m four_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_anios_incapacidades" CssClass="btn btn-primary2 dropdown_label"
                                        runat="server" AutoPostBack="true" />
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Filtrar por Año:</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView ID="dgd_incapacidades" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                    HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="QUINCENA" HeaderText="Quincena">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ANIO" HeaderText="Año">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_INICIO" HeaderText="Fecha Inicio">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha Fin">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                            <ItemStyle Width="40%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_anios_incapacidades" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <section class="panel" runat="server" visible="false" id="stn_permisos">
        <header class="panel-heading panel_header_folder" runat="server">
            <span class="panel_folder_toogle_header">Permisos</span>
            <span class="panel_folder_toogle up" runat="server">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned low_m four_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_anios_permisos" CssClass="btn btn-primary2 dropdown_label"
                                        runat="server" AutoPostBack="true" />
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Filtrar por Año:</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:GridView ID="dgd_permisos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ShowHeader="true" CssClass="table table-striped " GridLines="None" Visible="false"
                                    HorizontalAlign="Center" TabIndex="17" Width="100%" AllowPaging="true" PageSize="10">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="QUINCENA" HeaderText="Quincena">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ANIO" HeaderText="Año">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_INICIO" HeaderText="Fecha Inicio">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha Fin">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción">
                                            <ItemStyle Width="40%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_anios_permisos" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>--%>
    <table cellspacing="0" cellpadding="0" width="780px" align="center" bgcolor="#ffffff"
        border="0">
        <tr>
            <td valign="top">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Panel ID="pnl_notas" runat="server" Align="Center" CssClass="modalPopup"
                    Width="356px" Style='display: none;'>
                    <asp:Panel ID="pnl_Titulo" runat="server" Align="Center" CssClass="titulosmodal">
                        <asp:Label ID="lbl_tit" runat="server" class="subtitulosmodal" ForeColor="White" Text="NOTAS" />
                    </asp:Panel>
                    <br />
                    <asp:TextBox ID="lbl_notasexp" runat="server" class="text_input_nice_textarea" MaxLength="5000"
                        Width="300px" Height="250px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    <p class="center">
                        <br />
                        <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text="Cerrar" Style="margin-bottom: 10px" />
                    </p>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" PopupControlID="pnl_notas" PopupDragHandleControlID="pnl_Titulo"
                    TargetControlID="hdn_notas" DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_bloqueo" runat="server"
                    BackgroundCssClass="modalBackground" DropShadow="True" DynamicServicePath=""
                    Enabled="True" PopupControlID="pnl_bloqueo" PopupDragHandleControlID="pnl_titulobloqueo" TargetControlID="HiddenField1">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnl_bloqueo" runat="server" CssClass="modalPopup" Style='display: none;'>
                    <asp:Panel ID="pnl_titulobloqueo" runat="server" CssClass="titulosmodal" Align="Center">
                        <p align="center">
                            <asp:Label ID="Label1" runat="server" class="subtitulomodal"
                                ForeColor="White" Text="BLOQUEO/DESBLOQUEO DE EXPEDIENTE"></asp:Label>
                        </p>
                    </asp:Panel>
                    <p align="center">
                        <asp:Label ID="lbl_status_modal" runat="server" class="alerta"></asp:Label>
                    </p>
                    <p align="center">
                        <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" ValidationGroup="val_bloqueo" />
                        &nbsp;<asp:Button ID="btn_cancelar1" runat="server" class="btn btn-primary"
                            Text="Cerrar" ToolTip="Cerrar" />
                    </p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_evalx" runat="server" CssClass="modalPopup">
        <asp:Panel ID="pnl_tit_asignacion" runat="server" CssClass="modalHeader">
            <asp:Label ID="LBL_MODport" runat="server" class="modalTitle" Text="Detalle de reestructura"></asp:Label>
        </asp:Panel>
        <div class="module_subsec low_m columned two_columns">
            <div class=" module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:TextBox ID="lbl_folio_origenB" class="text_input_nice_input" runat="server" MaxLength="15" Enabled="false"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Núm expediente origen:</span>
                    </div>
                </div>
            </div>
            <div class=" module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:TextBox ID="lbl_tiporesB" class="text_input_nice_input" runat="server" MaxLength="15" Enabled="false"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Tipo reestrucutra:</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="module_subsec low_m columned two_columns">
            <div class="module_subsec_elements flex_1">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:TextBox ID="lbl_emprobB" runat="server" class="text_input_nice_textarea" MaxLength="3000" Enabled="false"
                        ValidationGroup="val_conf" TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                        onChange="javascript:Check(this, 3000);"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Razón emproblemamiento:</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="module_subsec flex_center">
            <asp:Label ID="Label4" runat="server" CssClass="alerta"></asp:Label>
        </div>
        <div class="module_subsec flex_end">
            <div class="module_subsec flex_end">
                <asp:Button ID="btn_guarda_modal" runat="server" class="btn btn-primary" Text="Cancelar" Width="90%" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdn_ctrl" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="modal_port" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_evalx" PopupDragHandleControlID="pnl_subevalx"
        TargetControlID="hdn_ctrl" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>
    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
    <input type="hidden" name="hdn_notas" id="hdn_notas" runat="server" />
</asp:Content>
