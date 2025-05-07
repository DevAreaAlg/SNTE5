<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_CONSULTA_BIT.aspx.vb" Inherits="SNTE5.COB_CONSULTA_BIT" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px");
            if (wbusf != null) {

                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>Afiliado</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">                        
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_numcliente">Número de afiliado: </asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_cliente">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Enabled="true"
                                CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                        </div>
                    </div>
                    <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                        Style="height: 18px; width: 18px;"></asp:ImageButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_folio">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="Label8" runat="server" class="alerta"></asp:Label>                  
            </div>           

            <div class="module_subsec">
                <asp:Label ID="lbl_clienteA" runat="server" class="subtitulos" Width="100px">Cliente:</asp:Label>
                <asp:Label ID="lbl_clienteB" runat="server" class="texto" Width="600px"></asp:Label>
            </div>
            <div class="module_subsec">
                <asp:Label ID="lbl_ProductoA" runat="server" class="subtitulos" Width="100px">Producto:</asp:Label>
                <asp:Label ID="lbl_ProductoB" runat="server" class="texto" Width="600px"></asp:Label>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_info" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
            </div>

        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Consulta</span>
        </header>
        <div class="panel-body">
            
            <asp:Panel ID="pnl_cobranza" runat="server" Visible="false" >
                <div class="module_subsec low_m columned three_columns ">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_estatus" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_estatus" runat="server" CssClass="text_input_nice_label">Fase:</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_evento" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_evento" runat="server" CssClass="text_input_nice_label">Evento:</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Label ID="Label1" runat="server" CssClass="module_subsec" Text="Para filtrar por fecha, es necesario introducir ambas fechas."/>

                <div class="module_subsec low_m columned three_columns ">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" MaxLength="10" ID="txt_FechaIni" class="text_input_nice_input"
                                ToolTip="(DD/MM/AAAA)"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                 <label id="lbl_FechaIni" runat="server" class="text_input_nice_label">Fecha inicial:</label>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaini" runat="server" 
                                    ControlExtender="MaskedEditExtender_fechaini" ControlToValidate="txt_fechaini" CssClass="textogris" 
                                    ErrorMessage="MaskedEditValidator_fechaini" InvalidValueMessage="Fecha inválida" />                                 
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaini" runat="server" Mask="99/99/9999" MaskType="Date" 
                                    TargetControlID="txt_fechaini" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" />
                                <ajaxToolkit:CalendarExtender ID="calext_fechaini" runat="server" TargetControlID="txt_fechaini" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" MaxLength="10" ID="txt_FechaFin" class="text_input_nice_input"
                                ToolTip="(DD/MM/AAAA)"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <label id="lbl_FechaFin" runat="server" class="texto">Fecha final:</label>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafin" runat="server" ControlExtender="MaskedEditExtender_fechafin" 
                                    ControlToValidate="txt_fechafin" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechafin" InvalidValueMessage="Fecha inválida" />                                  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafin" runat="server" Mask="99/99/9999" MaskType="Date" 
                                    TargetControlID="txt_fechafin" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" />
                                <ajaxToolkit:CalendarExtender ID="calext_fechafin" runat="server" TargetControlID="txt_fechafin"
                                    Format="dd/MM/yyyy" />
                            </div>
                        </div>
                    </div>
                </div>

                 <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                     <asp:Button ID="lnk_Consultar" runat="server" CssClass="btn btn-primary" Text="Consultar"
                        Visible="True" Enabled="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="lnk_limpiar" runat="server" CssClass="btn btn-primary" Text="Eliminar Filtros"
                        Visible="True" Enabled="true" />
                </div>
                        
            </asp:Panel>
                  
            <div class="module_subsec overflow_x shadow">
                <asp:DataGrid ID="dag_historial" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="IDLOG">
                            <ItemStyle Width="5px"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="FOLIO">
                            <ItemStyle Width="5px"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHASIS" HeaderText="Fecha sistema">
                            <ItemStyle Width="15%"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA" HeaderText="Fecha real">
                            <ItemStyle Width="15%"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="FASE" HeaderText="Fase">
                            <ItemStyle Width="20%"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="CVE_EVENTO">
                            <ItemStyle Width="5px"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="EVENTO" HeaderText="Evento">
                            <ItemStyle Width="20%"/></asp:BoundColumn>
                        <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario">
                            <ItemStyle Width="15%"/></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="DETALLE" Text="Detalle">
                            <ItemStyle Width="10%" /></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="VER" Text="Ver">
                            <ItemStyle Width="5%"/></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_status_dag" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
            </div>

            <asp:Panel ID="pnl_llamada" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_titulollamada" runat="server" CssClass="titulosmodal" Align="Center">
                    <div class="center">
                        <label id="lbl_tit" class="subtitulosmodal" style='color: White;'>Detalle llamada</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_id_llamada" runat="server" CssClass="texto" Text="Id llamada:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_id_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_destino_llamada_tit" runat="server" CssClass="texto" Text="Destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_destino_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_nombre_destino_llamada_tit" runat="server" CssClass="texto" Text="Nombre destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombre_destino_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_notas_llamada_tit" runat="server" CssClass="texto" Text="Notas:"></asp:Label>&nbsp;
                                <asp:TextBox ID="txt_notas_llamada" width="677px" runat="server" CssClass="texto" BorderStyle="None"
                                    Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tel_llamada_tit" runat="server" CssClass="texto" Text="Teléfono:"></asp:Label>&nbsp
                                <asp:Label ID="lbl_tel_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_duracion_llamada_tit" runat="server" CssClass="texto" Text="Duración:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_duracion_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_resultado_llamada_tit" runat="server" CssClass="texto" Text="Resultado:"></asp:Label>&nbsp;
                                <asp:Label ID="txt_resultado_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_persona_resp_llamada_tit" runat="server" CssClass="texto" Text="Nombre persona que respondió:"></asp:Label>&nbsp;
                            <asp:Label ID="lbl_persona_resp_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_prom_pago_llamada" runat="server" CssClass="texto" Text="Promesa de pago:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_prom_pago_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_Tit_monto_pago_llamada" runat="server" CssClass="texto" Text="Monto de pago:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_monto_pago_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_fecha_llamada_tit" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_usuario_llamada_tit" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_usuario_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_fecha_creado_llamada_tit" runat="server" CssClass="texto" Text="Fecha real registro:"></asp:Label>
                                <asp:Label ID="lbl_fecha_creado_llamada" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table><br />

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_cerrar" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_llamada" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_llamada"
                    PopupDragHandleControlID="pnl_titulollamada" TargetControlID="hdn_detalle_llamada">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_citas" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_titulocitas" runat="server" CssClass="titulosmodal" Align="Center">
                    <div class="center">
                        <label id="lbl_tit_citas" class="subtitulosmodal" style='color: White;'>Detalle cita</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_id_cita" runat="server" CssClass="texto" Text="Id cita:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_id_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_destinatario_cita" runat="server" CssClass="texto" Text="Destinatario:"></asp:Label>&nbsp;
                            <asp:Label ID="lbl_destinatario_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_nombre_destinatario_cita" runat="server" CssClass="texto" Text="Nombre destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombre_destinatario_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_hora_fecha_cita" runat="server" CssClass="texto" Text="Fecha y hora cita:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_hora_Fecha_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_suc_cita" runat="server" CssClass="texto" Text="Sucursal cita:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_suc_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="LBL_tit_notas_cita" runat="server" CssClass="texto" Text="Notas:"></asp:Label>
                                <asp:TextBox ID="txt_notas_cita" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_fecha_registro_cita" runat="server" CssClass="texto" Text="Fecha de registro:"></asp:Label>
                            <asp:Label ID="lbl_fecha_registro_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_creado_cita" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>
                                <asp:Label ID="lbl_creado_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_fecha_creado_cita" runat="server" CssClass="texto" Text="Fecha Real de Registro:"></asp:Label>
                            <asp:Label ID="lbl_fecha_creado_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Seguimiento</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_resultado_cita" runat="server" CssClass="texto" Text="Resultado:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_resultado_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_fecha_hora_seg_cita" runat="server" CssClass="texto" Text="Fecha y hora atención:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_hora_seg_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                 <asp:Label ID="lbl_tit_duracion_seg_cita" runat="server" CssClass="texto" Text="Duración:"></asp:Label>
                                <asp:Label ID="lbl_duracion_seg_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_suc_seg_cita" runat="server" CssClass="texto" Text="Sucursal atención:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_suc_seg_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_usuario_seg_cita" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>
                                <asp:Label ID="lbl_usuario_seg_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas_seg_cita" runat="server" CssClass="texto" Text="Notas del seguimiento:"></asp:Label>
                                <asp:TextBox ID="txt_notas_seg_cita" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_prom_pago_Cita" runat="server" CssClass="texto" Text="Promesa de Pago:"></asp:Label>
                                <asp:Label ID="lbl_prom_pago_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_monto_pago_cita" runat="server" CssClass="texto" Text="Monto de pago:"></asp:Label>
                                <asp:Label ID="lbl_monto_pago_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_Fecha_registro_seg_cita" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>
                                <asp:Label ID="lbl_fecha_registro_seg" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_real_seg_cita" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>
                                <asp:Label ID="lbl_fecha_real_seg_cita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table><br />
                   
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_cerrar_cita" runat="server" class="btn btn-primary"  Text="Cerrar" />
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_cita" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_citas"
                    PopupDragHandleControlID="pnl_titulocitas" TargetControlID="hdn_detalle_cita">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_citatorios" runat="server" CssClass="modalPopup" Width="100%"
                Style='display: none;'>
                <asp:Panel ID="pnl_titulocitatorios" runat="server" CssClass="titulosmodal" Align="Center">
                    <div class="center">
                        <label id="Label2" class="subtitulosmodal" style='color: White;'>Detalle citatorio</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="LBL_id_citatorio" runat="server" CssClass="texto" Text="Id citatorio:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_citatorio" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_destinatario_desti" runat="server" CssClass="texto" Text="Destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_destinatario_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_nombre_destinatario_desti" runat="server" CssClass="texto" Text="Nombre destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombre_destinatario_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_plantilla_desti" runat="server" CssClass="texto" Text="Plantilla:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_plantilla_desti" runat="server" CssClass="texto" Width="300px" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_registro_desti" runat="server" CssClass="texto" Text="Fecha de registro :"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_registro_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_usuario_desti" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_usuario_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_desti" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label9" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label10" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table_header">
                            <td colspan="6">Seguimiento</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_resultado_desti" runat="server" CssClass="texto" Text="Resultado:"></asp:Label>
                                <asp:Label ID="lbl_resultado_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label11" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label12" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas_desti" runat="server" CssClass="texto" Text="Notas del seguimiento:"></asp:Label>
                                <asp:TextBox ID="txt_notas_desti" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_prom_pago_ctro" runat="server" CssClass="texto" Text="Promesa de pago:"></asp:Label>
                                <asp:Label ID="lbl_prom_pago_ctro" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_monto_pago_ctro" runat="server" CssClass="texto" Text="Monto de pago:"></asp:Label>
                                <asp:Label ID="lbl_monto_pago_ctro" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_seg_desti" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_seg_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_usuario_seg_desti" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_usuario_seg_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_real_seg_desti" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_Real_seg_desti" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label13" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label14" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table><br />
                    
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_cerrar_citatorios" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_citatorios" runat="server"
                    BackgroundCssClass="modalBackground" DropShadow="True" DynamicServicePath=""
                    Enabled="True" PopupControlID="pnl_citatorios" PopupDragHandleControlID="pnl_titulocitatorios"
                    TargetControlID="hdn_detalle_citatorios">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_avisos" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_tituloavisos" runat="server" CssClass="titulosmodal" Align="Center">
                    <div align="center">
                        <label id="Label3" class="subtitulosmodal" style='color: White;'>Detalle aviso</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_id_aviso" runat="server" CssClass="texto" Text="Id aviso:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_destinatario_aviso" runat="server" CssClass="texto" Text="Destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_destinatario_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_nombre_destinatario_aviso" runat="server" CssClass="texto" Text="Nombre destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombre_destinatario_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_plantilla_aviso" runat="server" CssClass="texto" Text="Plantilla:"></asp:Label>&nbsp;
                            <asp:Label ID="lbl_plantilla_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_registro_aviso" runat="server" CssClass="texto" Text="Fecha de registro :"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_registro_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_usuario_aviso" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_usuario_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_Real_aviso" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>&nbsp;
                            <asp:Label ID="lbl_fecha_real_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label15" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label16" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table_header">
                            <td colspan="6">Seguimiento</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_resultado_aviso" runat="server" CssClass="texto" Text="Resultado:"></asp:Label>
                                <asp:Label ID="lbl_resultado_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label17" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label18" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas_seg_aviso" runat="server" CssClass="texto" Text="Notas del Seguimiento:"></asp:Label>
                                <asp:TextBox ID="txt_notas_seg_aviso" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_prom_pago_aviso" runat="server" CssClass="texto" Text="Promesa de pago:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_prom_pago_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_monto_pago_aviso" runat="server" CssClass="texto" Text="Monto de pago:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_monto_pago_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_registro_seg_aviso" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_registro_seg_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_usuario_Seg_aviso" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_usuario_seg_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_real_seg_aviso" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_real_seg_aviso" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label19" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="Label20" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table><br />
                   
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_cerrar_avisos" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_avisos" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_avisos"
                    PopupDragHandleControlID="pnl_tituloavisos" TargetControlID="hdn_detalle_avisos">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_juicio" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_detalle_juicio" runat="server" CssClass="titulosmodal" Align="Center">
                    <div class="center">
                        <label id="Label4" class="subtitulosmodal" style='color: White;'>Detalle registro juicio</label>
                    </div>
                </asp:Panel>
                    <div>
                        <table border="1" style = "width:100%; text-align:left;">
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_user" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_user" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_fecha_diligencia" runat="server" CssClass="texto" Text="Fecha ingreso de demanda:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_fecha_diligencia" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_estatus_juicio" runat="server" CssClass="texto" Text="Estatus juicio:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_estatus_juicio" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_Juzgado" runat="server" CssClass="texto" Text="Juzgado:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_juzgado" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_f_emp_tit" runat="server" CssClass="texto" Text="Fecha emplazamiento titular:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_f_emp_tit" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_f_emp_aval" runat="server" CssClass="texto" Text="Fecha emplazamiento aval:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_f_emp_aval" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_exhorto" runat="server" CssClass="texto" Text="Exhorto:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_exhorto" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_juzgado_exhorto" runat="server" CssClass="texto" Text="Juzgado exhortado:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_juzgado_exhortado" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_gestor" runat="server" CssClass="texto" Text="Gestor:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_Gestor" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_cita" runat="server" CssClass="texto" Text="Cita:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_cita" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Label ID="lbl_tit_detalle" runat="server" CssClass="texto" Text="Detalle:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_detalle" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_prom_pago" runat="server" CssClass="texto" Text="Promesa pago:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_fecha_prom_pago" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_monto_prom_pago" runat="server" CssClass="texto" Text="Monto promesa pago:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_monto_prom_pago" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lbl_tit_fecha_sistema" runat="server" CssClass="texto" Text="Fecha de registro :"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_fecha_sistema" runat="server" CssClass="texto"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lbl_tit_fecha_real" runat="server" CssClass="texto" Text="Fecha real del registro:"></asp:Label>&nbsp;
                                    <asp:Label ID="lbl_fecha_Real" runat="server" CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                        </table><br />

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_cerrar_juicio" runat="server" class="btn btn-primary" Text="Cerrar"/>
                        </div>
            </div>
        </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_juicio" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_juicio"
                    PopupDragHandleControlID="pnl_detalle_juicio" TargetControlID="hdn_detalle_juicio">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_tit_asignacion" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_asignacion" runat="server" CssClass="titulosmodal" Align="Center">
                    <div align="center">
                        <label id="Label5" class="subtitulosmodal" style='color: White;'>
                            Detalle asignación despacho/abogado</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_user_dexp" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_user_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fase_dexp" runat="server" CssClass="texto" Text="Fase:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fase_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_despacho_dexp" runat="server" CssClass="texto" Text="Despacho/Abogado:"></asp:Label>
                                <asp:Label ID="lbl_despacho_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_com_dexp" runat="server" CssClass="texto" Text="Comisón:"></asp:Label>
                                <asp:Label ID="lbl_com_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas_dexp" runat="server" CssClass="texto" Text="Motivo:"></asp:Label>&nbsp;
                                <asp:TextBox ID="txt_notas_dexp" width="677px" runat="server" CssClass="texto" BorderStyle="None"
                                    Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_fr_dexp" runat="server" CssClass="texto" Text="Fecha de registro :"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fr_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_frr_dexp" runat="server" CssClass="texto" Text="Fecha real del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_frr_dexp" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                    </table><br />

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_ok_dexp" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_detalle_asignacion" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_tit_asignacion"
                    PopupDragHandleControlID="pnl_asignacion" TargetControlID="hdn_detalle_asignacion">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_tit_estatus" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_estatus" runat="server" CssClass="titulosmodal" Align="Center">
                    <div class="center">
                        <label id="lbl_tit_estatus" class="subtitulosmodal" style='color: White;'>Detalle estatus de cobranza</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_user_estatus" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_user_estatus" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_estatus_cob" runat="server" CssClass="texto" Text="Estatus cobranza:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_estatus_cob" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_fecha_creado_estatus" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_estatus_fecha_sistema" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_sistema_estatus" runat="server" CssClass="texto" Text="Fecha real del registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_estatus_fecha_creado" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                    </table><br />

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_cerrar_estatus" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_detalle_estatus" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_tit_estatus"
                    PopupDragHandleControlID="pnl_estatus" TargetControlID="hdn_detalle_estatus">
                </ajaxToolkit:ModalPopupExtender>
            </div>

            <asp:Panel ID="pnl_tit_visita" runat="server" CssClass="modalPopup" Width="100%" Style='display: none;'>
                <asp:Panel ID="pnl_visita" runat="server" CssClass="titulosmodal" Align="Center">
                    <div align="center">
                        <label id="lbl_tit_visita" class="subtitulosmodal" style='color: White;'> Detalle visita</label>
                    </div>
                </asp:Panel>
                <div>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Datos</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_id_visita" runat="server" CssClass="texto" Text="Id visita:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_id_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_destinatario_visita" runat="server" CssClass="texto" Text="Destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_destinatario_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_nombre_destinatario_visita" runat="server" CssClass="texto" Text="Nombre destinatario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_nombre_destinatario_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_visita" runat="server" CssClass="texto" Text="Fecha y hora visita:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_fecha_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_suc_visita" runat="server" CssClass="texto" Text="Sucursal registro:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_suc_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas" runat="server" CssClass="texto" Text="Notas:"></asp:Label>&nbsp;
                                <asp:TextBox ID="txt_notas_visita" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_Fecha_registro_visita" runat="server" CssClass="texto" Text="Fecha de registro:"></asp:Label>&nbsp;
                            <asp:Label ID="lbl_fecha_registro_Visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_users_visita" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>&nbsp;
                                <asp:Label ID="lbl_user_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_fecha_reals_visita" runat="server" CssClass="texto" Visible="true" Text="Fecha Real de Registro:"></asp:Label>
                            <asp:Label ID="lbl_fecha_real_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="1" style = "width:100%; text-align:left;">
                        <tr class="table_header">
                            <td colspan="6">Seguimiento</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_resultado_visita" runat="server" CssClass="texto" Text="Resultado:"></asp:Label>
                                <asp:Label ID="lbl_resultado_Visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_hora_seg_visita" runat="server" CssClass="texto" Text="Fecha y hora atención:"></asp:Label>
                            <asp:Label ID="lbl_hora_seg_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_suc_seg_visita" runat="server" CssClass="texto" Text="Sucursal registro:"></asp:Label>
                                <asp:Label ID="lbl_suc_seg_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_duracion_visita" runat="server" CssClass="texto" Text="Duración:"></asp:Label>
                                <asp:Label ID="lbl_duracion_Visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_user_seg_visita" runat="server" CssClass="texto" Text="Usuario:"></asp:Label>
                                <asp:Label ID="lbl_user_seg_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lbl_tit_notas_Seg_visita" runat="server" CssClass="texto" Text="Notas del Seguimiento:"></asp:Label>
                                <asp:TextBox ID="txt_notas_Seg_visita" runat="server" CssClass="texto" Text="" TextMode="MultiLine"
                                    BorderStyle="None" Style="vertical-align: bottom; overflow: hidden"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_prompago_visita" runat="server" CssClass="texto" Text="Promesa de pago:"></asp:Label>
                                <asp:Label ID="lbl_prompago_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_monto_pago_visita" runat="server" CssClass="texto" Text="Monto de pago:"></asp:Label>
                                <asp:Label ID="lbl_monto_pago_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_tit_fecha_Reg_seg_visita" runat="server" CssClass="texto" Text="Fecha del registro:"></asp:Label>
                                <asp:Label ID="lbl_fecha_reg_seg_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lbl_tit_fecha_real_seg_visita" runat="server" CssClass="texto" Text="Fecha real de registro:"></asp:Label>
                                <asp:Label ID="lbl_fecha_Real_seg_visita" runat="server" CssClass="texto" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table><br />
                        
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_ok_visita" runat="server" class="btn btn-primary" Text="Cerrar"/>
                    </div>
                </div>
            </asp:Panel>

            <div align="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_visita" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_tit_visita"
                    PopupDragHandleControlID="pnl_visita" TargetControlID="hdn_detalle_visita">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </div>
    </section>        

    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
    <input type="hidden" name="hdn_detalle_llamada" id="hdn_detalle_llamada" runat="server" />
    <input type="hidden" name="hdn_detalle_cita" id="hdn_detalle_cita" runat="server" />
    <input type="hidden" name="hdn_detalle_citatorios" id="hdn_detalle_citatorios" runat="server" />
    <input type="hidden" name="hdn_detalle_avisos" id="hdn_detalle_avisos" runat="server" />
    <input type="hidden" name="hdn_detalle_juicio" id="hdn_detalle_juicio" runat="server" />
    <input type="hidden" name="hdn_detalle_asignacion" id="hdn_detalle_asignacion" runat="server" />
    <input type="hidden" name="hdn_detalle_estatus" id="hdn_detalle_estatus" runat="server" />
    <input type="hidden" name="hdn_detalle_visita" id="hdn_detalle_visita" runat="server" />

</asp:Content>
