<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_GENERAL.aspx.vb" Inherits="SNTE5.CRED_EXP_GENERAL" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            }
            else {
                var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            }

            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }

        }

        //function busquedapersonafisica() {
        //  var url = "../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE";
        //var popupSetting = "center=yes,height=355,width=700,toolbar=no,directories=no,status=no,scrollbars=no,resizable=no,modal=yes";
        //  var pmx = window.open(url, 'Add  Window', popupSetting);
        //document.getElementById("hdn_busqueda").value = pmx;
        //  __doPostBack('', '');
        //return true;

        //}

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
                        //alert('Ingrese Datos')
                        CTextbox.focus()
                        return false
                    }
                    //CTextbox.focus();
                    return true
                }
            }
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
                        <asp:TextBox runat="server" ID="tbx_rfc" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox runat="server" ID="txt_IdCliente" CssClass="text_input_nice_input" MaxLength="9" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Depe_NumCtrl">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="tbx_rfc">
                            </ajaxToolkit:FilteredTextBoxExtender>
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
            <span class="panel_folder_toogle_header">Expedientes</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_expedientes">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_expedientes">


                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_medium-elements">Agremiado:</span>
                    <asp:TextBox ID="lbl_nompros" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; 
                <asp:LinkButton ID="lnk_editar_agremiado" runat="server" CssClass="textoRojo" Text="Editar" />
                   &nbsp;&nbsp;&nbsp; 
                <asp:LinkButton ID="lnk_info_agremiado" runat="server" CssClass="textoRojo" Text="Información Agremiado" />
                     &nbsp;&nbsp;&nbsp; 
                <asp:LinkButton ID="lnk_pas" runat="server" CssClass="textoRojo" Text="Préstamo de Ahorro activo" />

                    </div>
                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_medium-elements">Salario (Quincenal):</span>
                    <asp:TextBox ID="lbl_salario" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                      <span class="module_subsec_elements module_subsec_medium-elements">Capacidad de pago (30%):</span>
                    <asp:TextBox ID="lbl_capacidad" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                   

                </div>
                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_medium-elements">Pago por otros préstamos:</span>
                    <asp:TextBox ID="lbl_otroscred" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Región:" />
                    <asp:TextBox runat="server" ID="tbx_region" Enabled="false"
                        CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements"  />
                      
                </div>
                <div class="module_subsec align_items_flex_center columned low_m">
                    <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Delegación:" />
                    <asp:TextBox runat="server" ID="tbx_delegacion" Enabled="false"
                        CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" />
                      &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="CT:" />
                    <asp:TextBox runat="server" ID="tbx_ct" Enabled="false"
                        CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" />

                </div>

                <div class="module_subsec align_items_flex_center columned low_m" >
                    <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Aportaciones:" />
                    <asp:TextBox runat="server" ID="tbx_aportaciones" Enabled="false"
                        CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements"  />
                      &nbsp;&nbsp;&nbsp;
                    <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Pensión Alimenticia:" />
                    <asp:TextBox runat="server" ID="tbx_pension" Enabled="false"
                        CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" />
                </div>

                <asp:Label ID="lbliduser" runat="server" Text="Labe1" Visible="false"></asp:Label>
                <asp:Label ID="lblidsesion" runat="server" Text="Labe2" Visible="false"></asp:Label>
                <asp:Label ID="lblidsucu" runat="server" Text="Labe4" Visible="false"></asp:Label>
                <asp:Label ID="lblidperson" runat="server" Text="Labe3" Visible="false"></asp:Label>
                <asp:Label ID="lbltipoproducto" runat="server" Text="Labe3" Visible="false"></asp:Label>
                <asp:Label ID="lblidproducto" runat="server" Text="Labe3" Visible="false"></asp:Label>


                 <asp:Panel ID="pnl_modal_hist" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <span>Descuentos efectivos de PA</span>
                            </header>
                            <div class="panel-body align_items_flex_center">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DataGrid ID="dag_Historico" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%" Visible="true">
                                        <AlternatingItemStyle CssClass="AlternativeStyle" />
                                        <Columns>
                                            <asp:BoundColumn DataField="QNA" HeaderText="Quincena">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ANIO" HeaderText="Año">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCUENTO" HeaderText="Descuento">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                <br />
                                <asp:Button ID="btn_cerrar" runat="server" class="btn btn-primary" Text="Cerrar" />
                            </div>
                        </section>
                    </div>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="mpe_confirmar" runat="server"
                    Enabled="True" PopupControlID="pnl_modal_hist"
                    PopupDragHandleControlID="pnl_modal_hist" TargetControlID="hdn_historico">
                </ajaxToolkit:ModalPopupExtender>

                <div class="module_subsec columned five_columns">

                    <%--<div class="module_subsec_elements no_m">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_div">
                                    <asp:DropDownList ID="cmb_destino" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">1.-&nbsp;  Destino:</span>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    <div class="module_subsec_elements no_m">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div">
                                <%-- <asp:Label ID="lbl_productos" runat="server" Text="Label"></asp:Label>--%>
                                <asp:DropDownList ID="cmb_Productos" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">1.-&nbsp; Producto:</span>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="module_subsec flex_end  top_m">
                    <asp:Button ID="lnk_GeneraExp" runat="server" CssClass="btn btn-primary"
                        Text="Generar Expediente"></asp:Button>
                </div>

                <asp:Label ID="lbl_info" runat="server" CssClass="module_subsec  flex_center alerta"></asp:Label>
                <asp:Label ID="lbl_status" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>

                <asp:Panel ID="pnl_alertas" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <span>Se ha generado la siguiente alerta</span>
                            </header>
                            <div class="panel-body vertical align_items_flex_center">
                                <asp:Label ID="lbl_alertas" runat="server" class="resalte_azul module_subsec"></asp:Label>
                                <asp:Button ID="btn_seguir_alertas" runat="server" class="btn btn-primary" Text="Continuar" />
                            </div>
                        </section>
                    </div>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="modal_alertas" runat="server"
                    Enabled="True" PopupControlID="pnl_alertas"
                    PopupDragHandleControlID="pnl_alertas" TargetControlID="hdn_alertas">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="pnl_ExpedienteNuevo" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <span>Nuevo Expediente</span>
                            </header>
                            <div class="panel-body vertical align_items_flex_center">
                                <asp:Label ID="lbl_FolioCreado" runat="server" class="resalte_azul module_subsec"></asp:Label>
                                <asp:Button ID="btn_ok" runat="server" class="btn btn-primary" Text="Aceptar" />
                            </div>
                        </section>
                    </div>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                    Enabled="True" PopupControlID="pnl_ExpedienteNuevo"
                    PopupDragHandleControlID="pnl_Titulo" TargetControlID="hdn_nombrebusqueda">
                </ajaxToolkit:ModalPopupExtender>

                <div class="overflow_x shadow module_subsec ">
                    <asp:DataGrid ID="dag_Expendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None">

                        <Columns>
                            <asp:BoundColumn DataField="idprod" HeaderText="IDPROD" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="folio" HeaderText="Folio" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cveexpe" HeaderText="Folio">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="producto" HeaderText="Producto">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha alta">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="tipo" HeaderText="Tipo">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="creadox" HeaderText="Capturista">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="CONTINUAR" Text="Continuar">
                                <ItemStyle Width="8%" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                <ItemStyle Width="7%" />
                            </asp:ButtonColumn>
                            <asp:BoundColumn DataField="fechacreado" HeaderText="Fecha creado">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CATTIPROD_CLAVE" HeaderText="PROD_CLAVE" Visible="False"></asp:BoundColumn>

                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </div>

            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="pnl_cnfexp">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_cnfexp">
            <span class="panel_folder_toogle_header">Configuración de expediente</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_cnfexp">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content " runat="server" id="content_pnl_cnfexp">

                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Folio:</span>
                    <asp:TextBox ID="lbl_subtitfolio" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>

                </div>
                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Producto:</span>
                    <asp:TextBox ID="lbl_subtitprod" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                </div>
                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Agremiado:</span>
                    <asp:TextBox ID="lbl_subtitcli" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                </div>

                <asp:Label ID="lbl_statusconf" runat="server" CssClass="alerta module_subsec flex_center "></asp:Label>

                <div class="shadow module_subsec">

                    <asp:DataGrid ID="dag_ConfExpediente" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <AlternatingItemStyle CssClass="AlternativeStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="IdConf">
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Estatus">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Image ID="Semaforo" runat="server" AlternateText="BLANK" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Descripcion" HeaderText="Apartados">
                                <ItemStyle HorizontalAlign="Left" Width="55%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Faltan" HeaderText="Apartados pendientes">
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="MODIFICAR" Text="Entrar">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                            <asp:BoundColumn DataField="ASPX">
                                <ItemStyle HorizontalAlign="Left" Width="15px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="multiple" Visible="False">
                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ENUSO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTATUS_EXP" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>

                    </asp:DataGrid>

                </div>
                <br />

                <asp:Panel ID="pnl_comite" runat="server" Align="Center"
                    Width="356px" CssClass="modalPopup">
                    <asp:Panel ID="Pnl_comitetitulo" runat="server" Align="Center" Height="20px"
                        CssClass="titulosmodal">
                        <div class="text_input_nice_div module_sec">
                            <asp:Label ID="lbl_comitemodal" runat="server" class="subtitulosmodal"
                                ForeColor="White"></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="text_input_nice_div module_sec">
                        <asp:Label ID="lbl_razoncom" runat="server" class="texto" Text="Razón"></asp:Label>
                        <br />
                        <asp:TextBox ID="txt_razoncom" runat="server" class="text_input_nice_textarea"
                            MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                    </div>

                    <div class="text_input_nice_div module_sec">
                        <asp:Button ID="btn_okcomite" runat="server" class="btn btn-primary" Text="Aceptar" />
                        <asp:Button ID="btn_cancelcomite" runat="server" class="btn btn-primary" Text="Cancelar" />
                    </div>

                    <div class="text_input_nice_div module_sec">
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_comite" runat="server"
                            Enabled="True" PopupControlID="pnl_comite"
                            PopupDragHandleControlID="pnl_comitetitulo" TargetControlID="hdn_nombrebusqueda">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </section>

    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
    <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />
    <input type="hidden" name="hdn_historico" id="hdn_historico" value="" runat="server" />

</asp:Content>
