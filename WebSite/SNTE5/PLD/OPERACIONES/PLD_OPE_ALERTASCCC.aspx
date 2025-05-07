<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_ALERTASCCC.aspx.vb" Inherits="SNTE5.PLD_OPE_ALERTASCCC" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=650,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function ResumenPersonaM() {
            window.open("ResumenPersonaM.aspx", "RPM", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        //function det_restructura() {
        //    window.open("../../CREDITO/EXPEDIENTES/CRED_EXP_REESTRUCTURA.aspx", "DR", "width=650px,height=400px,Scrollbars=YES");
        //}
        function det_com() {
            window.open("DetalleDictamen.aspx", "DD", "width=800px,height=650px,Scrollbars=YES");
        }
        function det_EntrevistaPLD() {
            window.open("PLD_OPE_DETALLE_ENTREVISTA.aspx", "DR", "width=800px,height=650px,Scrollbars=YES");
        }
        function his_PPE() {
            window.open("../../CREDITO/EXPEDIENTES/CRED_EXP_PPE.aspx", "DR", "width=650px,height=400px,Scrollbars=YES");
        }
    </script>

    <div class="tamano-cuerpo">
        <div class="module_subsec flex_end">
            <asp:Button ID="btn_ActualizarSesiones" runat="server" class="btn btn-primary" Text="Actualizar" Visible="True" />
        </div>
 
        <section class="panel" runat="server" id="pnl_SesionesPLD">
            <header class="panel_header_folder panel-heading">
                <span><asp:Label ID="Label5" runat="server" Enabled="false" Text="Sesiones pendientes"></asp:Label></span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">                  
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_StatusSesiones" runat="server" class="alerta"></asp:Label>
                    </div>

                    <asp:DataGrid ID="dag_Sesiones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="SESION_COM" HeaderText="Sesión">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_ALERTAS_PENDIENTES" HeaderText="Alertas pendientes">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_ALERTAS_VOTADAS" HeaderText="Alertas votadas">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ABRIR" HeaderText="" Text="Abrir sesión">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="ACTA" HeaderText="" Text="Generar acta">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="DIGITALIZAR" HeaderText="" Text="Digitalizar acta">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                            </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </section>

        <section class="panel" runat="server" ID="pnl_OpePend" Visible="false">
            <header class="panel_header_folder panel-heading">
                <span><asp:Label ID="lbl_subtitulo" runat="server" Enabled="false"></asp:Label></span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
                    <div class="module_subsec flex_center">
                        <label id="lbl_instruccion" class="textogris">Operaciones en proceso de decisión:</label>
                    </div>                        
                    
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_Status" runat="server" class="alerta"></asp:Label>
                    </div>

                    <asp:DataGrid ID="dag_OpePend" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="IDALERTA" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                                <ItemStyle Width="15%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Núm. afiliado">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDOPERACION" Visible="false">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOPRODUCTO" Visible="false">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IDALEOPE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VOTO" HeaderText="Voto">
                                <ItemStyle Width="5%"/>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ABRIR" HeaderText="" Text="Abrir">
                                <ItemStyle Width="5%" />
                            </asp:ButtonColumn>
                            </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </section>

        <asp:Panel ID="pnl_DatosOpe" runat="server" Visible="False">
            <section class="panel" runat="server" id="Section1">
                <header class="panel-heading">
                    <span><asp:Label ID="lbl_Folio" runat="server" Enabled="false" ></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                            <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                    </div>
                </div>
            </section>

            <section class="panel" runat="server" id="pnl_ope">
                <header class="panel_header_folder panel-heading">
                    <span><asp:Label ID="Label1" runat="server" Text="Datos operación"></asp:Label></span>
                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
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
                                                <asp:Label ID="lbl_TipoOpe" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Operación: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_TipoOpeM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div> 
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_Sucursal" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Sucursal: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_SucursalM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_PerfilPersona" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Perfil persona: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_PerfilPersonaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>                                         
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_RealizoEnt" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Resultado entrevista: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_RealizoEntM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div> 
                                        </div>
                                        <div class="module_subsec_elements vertical flex_1">
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_FolioImp" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Folio de impresión: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_FolioImpM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_MontoOpePLD" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Monto: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_MontoOpePLDM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
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
                                                <asp:Label ID="lbl_DictamenOC" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Dictamen de OC: "></asp:Label>
                                                <asp:Textbox Enabled="false" ID="lbl_DictamenOC1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
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
                                </td>
                                <td width="30%"> 
                                    <asp:LinkButton ID="lnk_EntrevistaPLD" runat="server" CssClass="module_subsec flex_center" 
                                        Enabled="False" Text="Entrevista Perfil"></asp:LinkButton>                                            
                                    <asp:LinkButton ID="lnk_HistorialAlertas" runat="server" CssClass="module_subsec flex_center" 
                                        Enabled="False" Text="Historial Alertas"></asp:LinkButton>                                           
                                    <asp:LinkButton ID="lnk_PersonaPolitica" runat="server" CssClass="module_subsec flex_center" 
                                        Enabled="False" Text="Persona Politica"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </section>

            <section class="panel" runat="server" id="pnl_general">
                <header class="panel_header_folder panel-heading">
                    <span><asp:Label ID="Label2" runat="server" Text="Datos generales"></asp:Label></span>
                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <table border="0" width="100%">
                            <tr>                             
                                <td width="70%">
                                    <asp:Panel ID="pnl_DatosCred" runat="server" Visible="False">
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_ProductoDetalleA" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_ProductoDetalleB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_MontoA" runat="server" Text="Monto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_MontoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_TasaNormalA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tasa ordinaria anual: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_TasaNormalB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_PeriodicidadA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Periodicidad: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_PeriodicidadB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_fechaliberaA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Disposición estimada: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fechaliberaB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                            </div>
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_PlazoA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Plazo: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_PlazoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_TasaMoraA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tasa moratoria mensual: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_TasaMoraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tipoplanA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo plan pagos: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_tipoplanB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                    &nbsp;<asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" Text="Ver"></asp:LinkButton>
                                                </div>                                                
                                            </div>                                        
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnl_DatosCaptacion" runat="server" Visible="False">
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1" style="margin-bottom:-15px!important;">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_ProductoCap" runat="server" Text="Producto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_ProductoCap1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_TasaCap" runat="server" Text="Tasa anual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_TasaCap1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_UltFechaDepCap" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Último depósito: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_UltFechaDepCap1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div> 
                                            </div>
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_SaldoCap" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Saldo actual: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_SaldoCap1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div> 
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_UltFechaRetCap" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Último retiro: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_UltFechaRetCap1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div> 
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </td>
                                <td width="30%">
                                    <asp:LinkButton ID="lnk_persona" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Datos Afiliado"></asp:LinkButton>                                           
                                    <asp:LinkButton ID="lnk_docsexp" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Documentación"></asp:LinkButton>                                            
                                    <asp:LinkButton ID="lnk_garantias" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Garantías"></asp:LinkButton>
                                    <asp:LinkButton ID="lnk_redsocial" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Red Social"></asp:LinkButton>
                                    <asp:LinkButton ID="lnk_historial" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Historial"></asp:LinkButton>
                                    <asp:LinkButton ID="lnk_gastos" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Comparación Gastos"></asp:LinkButton>
                                    <asp:LinkButton ID="lnk_restructura" runat="server" CssClass="module_subsec flex_center" 
                                        Text="Detalle Reestructura"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </section>

            <section class="panel" runat="server" id="pnl_foro">
                <header class="panel_header_folder panel-heading">
                    <span><asp:Label ID="Label3" runat="server" Text="Foro/Votación"></asp:Label></span>
                    <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <%--<asp:UpdatePanel ID="up_votacion" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>--%>
                                <div class="center">
                                    <asp:Label ID="lbl_votosf" runat="server" class="subtitulos"></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lbl_votosc" runat="server" class="subtitulos"></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lbl_miembrosc" runat="server" class="subtitulos"></asp:Label>                                
                                </div>

                                <div class= "module_subsec low_m columned three_columns"style="flex:1;">
                                    <div class="module_subsec_elements">
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_voto" runat="server" class="btn btn-primary2 dropdown_label">
                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="JUSTI">OPERACION JUSTIFICADA</asp:ListItem>
                                                <asp:ListItem Value="NOJUSTI">OPERACION NO JUSTIFICADA</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_voto" runat="server" class="text_input_nice_label" Text="Voto:"></asp:Label>                                       
                                                <asp:RequiredFieldValidator ID="req_cmbvoto" runat="server" ControlToValidate="cmb_voto"
                                                        ValidationGroup="valvoto" Text="Falta Dato!" Cssclass="textogris"
                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div> 
                                                    
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_votar" runat="server" class="btn btn-primary" Text="Votar" ValidationGroup="valvoto" />
                                    </div>
                                </div>

                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_StatusVoto" runat="server" class="alerta"></asp:Label>
                                </div>

                                <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox  ID="txt_comentario" runat="server"  CssClass="texto" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                                <div class="text_input_nice_labels"> 
                                                    <asp:Label ID="lbl_comentario" runat="server" class="text_input_nice_label" Text="Comentario:"></asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_comentario" runat="server" Enabled="True" 
                                                        FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" 
                                                        TargetControlID="txt_comentario" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="req_txtcoment" runat="server" ControlToValidate="txt_comentario"
                                                        ValidationGroup="valcoment" Text="Falta Dato!" Cssclass="textogris"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements"> 
                                            <asp:Button ID="btn_enviar" runat="server" class="btn btn-primary"  
                                            Text="Enviar" ValidationGroup="valcoment"/>                                    
                                        </div>
                                    </div>

                                <div class="module_subsec flex_center">
                                    <asp:LinkButton ID="lnk_actualiza" runat="server" Cssclass="textogris" Text="Actualizar"></asp:LinkButton>
                                </div>

                                <div class="module_subsec overflow_x shadow">
                                    <asp:DataGrid ID="DAG_post" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                             <asp:BoundColumn DataField="MIEMBRO" HeaderText="Miembro">
                                             <ItemStyle HorizontalAlign="Center" Width="50px" /></asp:BoundColumn>
                                             <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                             <ItemStyle HorizontalAlign="Center" Width="50px" /></asp:BoundColumn>
                                             <asp:BoundColumn DataField="COMENTARIOS" HeaderText="Comentarios">
                                                 <ItemStyle HorizontalAlign="Left" Width="550px" /></asp:BoundColumn>
                                            </Columns>                             
                                    </asp:DataGrid>
                                </div> 
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </section>

            <section class="panel" runat="server" id="pnl_dictamen" visible="false">
                <header class="panel_header_folder panel-heading">
                    <span><asp:Label ID="Label4" runat="server" Text="Dictamen"></asp:Label></span>
                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <asp:UpdatePanel ID="up_dictamen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class= "module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex:1;">
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_observacion" runat="server" CssClass="texto" 
                                                MaxLength="5000" TextMode="MultiLine" ></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_observacion" runat="server" class="text_input_nice_label" Text="*Observaciones:"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_observacion" runat="server" Enabled="True" 
                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_observacion" 
                                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,."></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_observacion" runat="server" ControlToValidate="txt_observacion" 
                                                    Cssclass="textoazul" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                                ValidationGroup="val_Resultado"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements"> 
	                                    <div class="text_input_nice_div module_subsec_elements_content">

                                        </div>
                                    </div>
                                </div>

                                <div class= "module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements"> 
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_Justificado" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                                <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="JUSTIFICADO">OPERACION JUSTIFICADA</asp:ListItem>
                                                <asp:ListItem Value="NOJUSTIFICADO">OPERACION NO JUSTIFICADA</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="Label6" runat="server" class="text_input_nice_label" Text="*Dictamen:"></asp:Label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Justificado" CssClass="textogris" ControlToValidate="cmb_Justificado"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Resultado" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Label ID="lbl_statusresultado" runat="server" class="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_Resultado" runat="server" class="btn btn-primary" Text="Terminar" 
                                        ValidationGroup="val_Resultado"/>
                                </div>
                            </ContentTemplate>
                              <Triggers>
                                <asp:PostBackTrigger ControlID="btn_Resultado" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>                    
        </asp:Panel>
                        
        <asp:Panel ID="pnl_SigAlerta" runat="server" CssClass="modalPopup">
            <asp:Panel ID="pnl_SigAlertaTitulo" runat="server" CssClass="modalHeader">
                <label id="lbl_SigAlertatit" class="modalTitle">Voto recibido </label>
            </asp:Panel>
            <div class="modalContent">
                <asp:Label ID="lbl_SigAlerta" runat="server" class="subtitulos">Su voto se ha guardado correctamente. ¿Desea que el sistema lo envie a la siguiente alerta disponible?</asp:Label>
                <div class="module_subsec flex_center">
                    <asp:Button ID="btn_SigAlerta" runat="server" class="btn btn-primary" Text="Si"/>&nbsp;&nbsp;
                    <asp:Button ID="btn_NoSigAlerta" runat="server" class="btn btn-primary" Text="No" />
                </div>
            </div>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True"  Enabled="True" PopupControlID="pnl_SigAlerta"
            PopupDragHandleControlID="pnl_SigAlertaTitulo" TargetControlID="hdn_SigAlerta" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>   
        
              <asp:Panel ID="pnl_evalx" runat="server" CssClass="modalPopup">
            <%--    <asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
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
                        <asp:Label ID="lbl_status_modal" runat="server" CssClass="alerta"></asp:Label>
                    </div>
                    <div class="module_subsec flex_end">
                        <div class="module_subsec flex_end">                    
                            <asp:Button ID="btn_guarda_modal" runat="server" class="btn btn-primary" Text="Cancelar" Width="90%"/>                    
                        </div>                
                       
                    </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>
    </div>
      <asp:HiddenField ID="hdn_ctrl" runat="server" />
     <ajaxToolkit:ModalPopupExtender ID="modal_port" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True" Enabled="True" PopupControlID="pnl_evalx" PopupDragHandleControlID="pnl_subevalx"
            TargetControlID="hdn_ctrl" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>

    <input type="hidden" name="hdn_SigAlerta" id="hdn_SigAlerta" value="" runat="server" />    
</asp:Content>
