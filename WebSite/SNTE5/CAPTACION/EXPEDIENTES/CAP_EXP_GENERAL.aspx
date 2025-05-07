<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_GENERAL.aspx.vb" Inherits="SNTE5.CAP_EXP_GENERAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">
        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:687px;dialogWidth:670px");
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

    <!------selecccion del afiliado------>
    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span>Selección de afiliado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">

                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="Ingrese el número de afiliado"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server"
                                TargetControlID="txt_idCliente" FilterType="Numbers" Enabled="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_IdCliente" runat="server"
                                ControlToValidate="txt_IdCliente" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_idPersona"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                        <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" ValidationGroup="val_idPersona" />
                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar afiliado" />
                    </div>
                </div>

                <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                    <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre del afiliado: </span>
                    <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
                </div>

                <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>

            </div>
        </div>
    </section>
    <!------expedientes------>
    <section class="panel" runat="server" visible="false" id="pnl_expedientes">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_expedientes">
            <span>Expedientes</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_expedientes">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_expedientes">

                <asp:Label ID="lbl_nompros" runat="server" class="module_subsec"></asp:Label>



                <div class="module_subsec columned low_m three_columns">

                    <div class="module_subsec_elements">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_div">
                                <asp:DropDownList ID="cmb_Productos" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Producto:</span>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="module_subsec">
                    <asp:Label ID="lbl_DescProducto" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_descripcion" runat="server"></asp:Label>
                </div>

                <div class="module_subsec low_m flex_end">

                    <asp:Button ID="lnk_GeneraExp" runat="server" CssClass="btn btn-primary"
                        Text="Generar Expediente"></asp:Button>
                </div>





                <asp:Label ID="lbl_status" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>


                <asp:Panel ID="pnl_ExpedienteNuevo" runat="server"
                    Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center; z-index: 13000;">
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
                    TargetControlID="hdn_nombrebusqueda">
                </ajaxToolkit:ModalPopupExtender>

                <div class="overflow_x shadow module_subsec low_m">

                    <asp:DataGrid ID="dag_Expendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="idprod" HeaderText="IDPROD" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="producto" HeaderText="Producto">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha alta">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="tipo" HeaderText="Tipo">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="CONTINUAR" Text="Continuar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                            <asp:BoundColumn DataField="fechacreado" HeaderText="Fecha creado">
                                <ItemStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CATTIPROD_CLAVE" HeaderText="PROD_CLAVE" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>


                </div>
            </div>
        </div>
    </section>
    <!------config exp------->
    <section class="panel" runat="server" visible="false" id="pnl_cnfexp">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_cnfexp">
            <span>Configuración de expediente</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_cnfexp">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content " runat="server" id="content_pnl_cnfexp">

                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Folio:</span>
                    <asp:TextBox ID="lbl_subtitfolio" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                </div>

                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Afiliado:</span>
                    <asp:TextBox ID="lbl_subtitcli" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                </div>

                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_small-elements">Producto:</span>
                    <asp:TextBox ID="lbl_subtitprod" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                </div>
                

                <asp:Label ID="lbl_statusconf" runat="server" CssClass="alerta module_subsec flex_center"></asp:Label>

                <div class="overflow_x shadow module_subsec">
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


                <div class="module_subsec low_m flex_end">
                    <asp:Button ID="btn_liberar" runat="server" CssClass="btn btn-primary" Text="Liberar" />

                </div>


                <asp:Panel ID="pnl_comite" runat="server" Align="Center"
                    Style="display: none;" CssClass="modalPopup">
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
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_comite" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" PopupControlID="pnl_comite"
                    PopupDragHandleControlID="pnl_comitetitulo" TargetControlID="hdn_nombrebusqueda">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </div>
    </section>
    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />

</asp:Content>


