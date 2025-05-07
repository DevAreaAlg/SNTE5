<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_ALE_HISTORIAL.aspx.vb" Inherits="SNTE5.PLD_ALE_HISTORIAL" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

        function onCancel() { }

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

        function his_PPE() {
            window.open("DetallePPE.aspx", "DR", "width=800px,height=650px,Scrollbars=YES");
        }

        function det_EntrevistaPLD() {
            window.open("../OPERACIONES/PLD_OPE_DETALLE_ENTREVISTA.aspx", "DR", "width=800px,height=650px,Scrollbars=YES");
        }
    </script>

   <section class="panel" runat="server" id="pnl_cliente">
        <header class="panel-heading">
            <span>Selección de afiliado</span>
        </header>
        <div class="panel-body">
            <asp:Panel ID="Panel2" runat="server">
                                <br />
                                <br />
                                    
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label id="lbl_BusquedaPersona0" runat="server" class="texto">Ingrese el número de afiliado:</label>

                                &#160;&#160;&#160;&#160;
                                <asp:TextBox ID="txt_IdCliente" runat="server" class="textocajas" MaxLength="8" Width="79px"></asp:TextBox>

                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server"
                                    TargetControlID="txt_idCliente" FilterType="Numbers" Enabled="True" />

                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_BusquedaPersona" runat="server" class="textogris" Text="Buscar Afiliado"></asp:LinkButton>

                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label id="lbl_NombrePersonaBusquedTexto" runat="server" class="texto">Nombre de Afiliado: </label>

                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" CssClass="texto"></asp:Label>

                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_Continuar" runat="server" class="textogris" Text="Continuar"></asp:LinkButton>

                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_statusc" runat="server" Align="center" CssClass="alerta"></asp:Label>

                            </asp:Panel>
        </div>
    </section>

    <section class="panel" >
        <header class="panel-heading">
            <span>Historial</span>
        </header>
        <div class="panel-body">            

            <asp:Panel runat="server" ID="pnl_Lista" Width="100%">
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_AlertPLD" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                            <Columns>
                            <asp:BoundColumn DataField="IDALERTA" Visible="False">
                            <ItemStyle width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDOPERACION" Visible="False">
                            <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                            <ItemStyle Width="20%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                            <ItemStyle Width="10%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                            <ItemStyle Width="100px"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                            <ItemStyle Width="90px"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DICTTAMENOC" HeaderText="Dictamen OC">
                            <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                            <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDALEOPE" Visible="False">
                            <ItemStyle width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="DETALLE" Text="Detalle">
                            <ItemStyle Width="5%" />
                            </asp:ButtonColumn>
                            </Columns>
                    </asp:DataGrid>
                </div>

                <div class="module_subsec flex_end">
                    <asp:Label ID="lbl_Status" runat="server" CssClass="alerta"></asp:Label>
                </div>
                        
            </asp:Panel>

        </div>
    </section>

    <section class="panel" runat="server" id="pnl_detalle" visible="false">
        <header class="panel-heading">
            <span>Detalle</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec flex_end">
                <asp:LinkButton ID="lnk_Cerrar" runat="server" class="textogris" Text="Cerrar"></asp:LinkButton>
            </div>
           
                <table border="0" width="100%">
                    <tr>                             
                        <td width="70%">
                            <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                <div class="module_subsec_elements vertical flex_1">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_IDAlerta" runat="server" Text="Id alerta: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_IDAlertaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_Sucursal" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Sucursal: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_SucursalM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_MontoOpePLD" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Monto: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_MontoOpePLDM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_PerfilPersona" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Perfil persona: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_PerfilPersonaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>                                         
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_RealizoEnt" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Resultado entrevista: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_RealizoEntM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_DictamenOC" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Dictamen de OC: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_DictamenOC1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                </div>
                                <div class="module_subsec_elements vertical flex_1">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_TipoOpe" runat="server" Text="Operación: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_TipoOpeM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_FolioImp" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Folio de impresión: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_FolioImpM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_FechaAlerta" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Fecha de alerta: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_FechaAlertaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div> 
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_TipoPerfil" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo perfil: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_TipoPerfilM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_EntrevistaDig" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Entrevista digitalizada: "></asp:Label>
                                        <asp:LinkButton ID="lnk_EntrevistaPLD" runat="server" class="module_subsec_elements flex_1 texto">Ver entrevista</asp:LinkButton>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_DictamenCCC" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Dictamen de CCC: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_DictamenCCC1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_NotaOpePLD" runat="server" Text="Notas: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_NotaOpePLDM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_ObservacionesOC" runat="server" Text="Observaciones de OC: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_ObservacionesOC1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_ObservacionesCCC" runat="server" Text="Observaciones de CCC: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_ObservacionesCCC1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>

        </div>
    </section>

         <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />

</asp:Content>

