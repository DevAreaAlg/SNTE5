<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_OC_ADMEXP.aspx.vb" Inherits="SNTE5.PLD_OPE_OC_ADMEXP" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=650,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function ResumenPersonaM() {
            window.open("ResumenPersonaM.aspx", "RPM", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function det_restructura() {
            window.open("../../CREDITO/EXPEDIENTES/CRED_EXP_REESTRUCTURA.aspx", "DR", "width=650px,height=400px,Scrollbars=YES");
        }
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
        <div class="module_subsec flex_center">
            <asp:Label ID="lbl_CCCActivo" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>
        </div>
        <div class="module_subsec flex_end">
            <asp:Button ID="btn_actualizar" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Actualizar" />
        </div>

        <section class="panel" runat="server" id="pnl_OpePend" visible="true">
            <header class="panel_header_folder panel-heading">
                <span>
                    <asp:Label ID="lbl_opepen" runat="server" Enabled="false" Text="Operaciones PLD pendientes"></asp:Label></span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_Status" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_start">
                        <asp:Label ID="lbl_OpePendTitulo" runat="server" class="texto">Alertas por asignar</asp:Label>
                    </div>
                    <div class="module_subsec">
                        <div class="overflow_x shadow w_100">
                            <asp:DataGrid ID="dag_OpePend" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Visible="False">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="IDALERTA" Visible="False">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Afiliado">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="250px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                        <ItemStyle Width="90px" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDOPERACION" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPOPRODUCTO" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDSESION" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDALEOPE" Visible="False">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ABRIR" HeaderText="" Text="Asignar">
                                        <ItemStyle ForeColor="#054B66" Width="30px" />
                                    </asp:ButtonColumn>
                                </Columns>

                            </asp:DataGrid>
                        </div>
                    </div>

                    <div class="module_subsec flex_start">
                        <asp:Label ID="lbl_SesionesTitulo" runat="server" class="texto">Sesiones abiertas</asp:Label>
                    </div>

                    <div class="module_subsec">
                        <div class="overflow_x shadow w_100">
                            <asp:DataGrid ID="dag_Sesiones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Visible="False">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="SESION_COM" HeaderText="Sesión">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ALERTAS_PEND" HeaderText="Alertas pendientes">
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ALERTAS_SES" HeaderText="Alertas dicataminadas">
                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ABRIR" HeaderText="" Text="Abrir">
                                        <ItemStyle ForeColor="#054B66" Width="70px" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="ACTA" HeaderText="" Text="Generar acta">
                                        <ItemStyle ForeColor="#054B66" Width="80px" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="DIGITALIZAR" HeaderText="" Text="Digitalizar acta">
                                        <ItemStyle ForeColor="#054B66" Width="110px" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="CERRAR" HeaderText="" Text="Cerrar sesión">
                                        <ItemStyle ForeColor="#054B66" Width="90px" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>

            </div>
        </section>

        <section class="panel" runat="server" id="pnl_AbrirAlertas" visible="false">
            <header class="panel_header_folder panel-heading">
                <span>
                    <asp:Label ID="Label1" runat="server" Enabled="false" Text="Seleccionar alerta por dictaminar"></asp:Label></span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">

                <div class="panel-body_content init_show">
                    <div class="overflow_x shadow w_100">
                        <asp:DataGrid ID="dag_AbrirAlertas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Visible="False">
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="IDALERTA" Visible="False">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Afiliado">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                    <ItemStyle Width="250px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="90px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDOPERACION" Visible="false">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPOPRODUCTO" Visible="false">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDSESION" Visible="false">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDALEOPE" Visible="False">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ABRIR" HeaderText="" Text="Abrir">
                                    <ItemStyle Width="30px" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>

            </div>
        </section>

        <section class="panel" runat="server" id="pnl_AsignaSesion" visible="false">
            <header class="panel_header_folder panel-heading">
                <span>
                    <asp:Label ID="lbl_asigna" runat="server" Enabled="false" Text="Asigna alerta a sesión"></asp:Label></span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">

                <div class="panel-body_content init_show">
                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:DropDownList ID="cmb_SesionComite" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_SesionComite" runat="server" class="texto">Sesiones abiertas</asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_SesionComite" runat="server"
                                    ControlToValidate="cmb_SesionComite" CssClass="textogris"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1"
                                    ValidationGroup="val_AsignaComite" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_start">
                        <asp:Label ID="lbl_SesionComite0" runat="server" class="texto" Visible="false">Grupo de Alertas</asp:Label>
                    </div>

                    <div class="module_subsec">
                        <div class="overflow_x shadow w_100">
                            <asp:DataGrid ID="dag_AlertaXComite" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Visible="False">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUCURSAL" HeaderText="Sucursal">
                                        <ItemStyle Width="110px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Afiliado">
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="200px"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                        <ItemStyle Width="90px"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>

                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_AsignarComite" runat="server" CssClass="btn btn-primary module_subsec_elements no_tbm " Text="Asignar" ValidationGroup="val_AsignaComite" />
                    </div>
                </div>
            </div>
        </section>



        <asp:Panel ID="pnl_DatosOpe" runat="server" Visible="False">

            <section class="panel" runat="server" id="Section2">
                <header class="panel-heading">
                    <span>
                        <asp:Label ID="lbl_Folio" runat="server" Enabled="false"></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                            <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </section>



            <section class="panel" runat="server" id="pnl_ope">
                <header class="panel_header_folder panel-heading">
                    <span>
                        <asp:Label ID="Label2" runat="server" Text="Datos operación"></asp:Label></span>
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
                                                <label id="lbl_IDAlerta" class="module_subsec_elements module_subsec_medium-elements">No. Alerta:</label>
                                                <asp:TextBox Enabled="false" ID="lbl_IDAlertaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                                <%--<asp:Label ID="lbl_IDAlertaM" runat="server"  class="module_subsec_elements flex_1 text_input_nice_input"></asp:Label>--%>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_TipoOpe" class="module_subsec_elements module_subsec_medium-elements">Operación:</label>

                                                <asp:TextBox ID="lbl_TipoOpeM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_Sucursal" class="module_subsec_elements module_subsec_medium-elements">Sucursal:</label>

                                                <asp:TextBox ID="lbl_SucursalM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>

                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_FolioImp" class="module_subsec_elements module_subsec_medium-elements">Folio de Impresión:</label>
                                                <asp:TextBox ID="lbl_FolioImpM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_MontoOpePLD" class="module_subsec_elements module_subsec_medium-elements">Monto:</label>
                                                <asp:TextBox ID="lbl_MontoOpePLDM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_FechaAlerta" class="module_subsec_elements module_subsec_medium-elements">Fecha de Alerta:</label>
                                                <asp:TextBox ID="lbl_FechaAlertaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="module_subsec_elements vertical flex_1">
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_RealizoEnt" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="¿Realizó Entrevista?: "></asp:Label>
                                                <asp:TextBox ID="lbl_RealizoEntM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_TipoPerfil" runat="server" class="module_subsec_elements module_subsec_medium-elements"
                                                    Text="Tipo Perfil de Persona:"></asp:Label>
                                                <asp:TextBox ID="lbl_TipoPerfilM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_PerfilPersona" runat="server" class="module_subsec_elements module_subsec_medium-elements"
                                                    Text="Perfil Persona:"></asp:Label>
                                                <asp:TextBox ID="lbl_PerfilPersonaM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_ActEco" runat="server" class="module_subsec_elements module_subsec_medium-elements"
                                                    Text="Actividad Economica:"></asp:Label>
                                                <asp:TextBox ID="lbl_ActEcoM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>

                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <asp:Label ID="lbl_CalActEco" runat="server" class="module_subsec_elements module_subsec_medium-elements"
                                                    Text="Calificación:"></asp:Label>
                                                <asp:TextBox ID="lbl_CalActEcoM" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                        <div class="module_subsec_elements vertical flex_1" style="margin-bottom: -15px!important;">
                                            <div class="module_subsec columned no_m align_items_flex_center ">
                                                <label id="lbl_NotaOpePLD" class="module_subsec_elements module_subsec_medium-elements">Nota:</label>
                                                <%--<asp:Label ID="lbl_NotaOpePLDM" runat="server" class="texto"></asp:Label>--%>
                                                <asp:TextBox ID="lbl_NotaOpePLDM" runat="server" CssClass="module_subsec_elements flex_1 text_input_nice_textarea"
                                                    MaxLength="5000" TextMode="MultiLine" Width="700" Enabled="false"></asp:TextBox>
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



            <section class="panel" runat="server" id="pnl_generales">
                <header class="panel_header_folder panel-heading">
                    <span>
                        <asp:Label ID="Label3" runat="server" Text="Datos generales"></asp:Label></span>
                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                </header>

                <div class="panel-body">
                    <div class="panel-body_content">
                        <table border="0" width="100%">
                            <tr>
                                <td width="70%">


                                    <asp:Panel ID="pnl_DatosCred" runat="server" Visible="False">
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_ProductoDetalleA" class="module_subsec_elements module_subsec_medium-elements">Producto:</label>
                                                    <asp:TextBox ID="lbl_ProductoDetalleB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_MontoA" class="module_subsec_elements module_subsec_medium-elements">Monto:</label>
                                                    <asp:TextBox ID="lbl_MontoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_PlazoA" class="module_subsec_elements module_subsec_medium-elements">Plazo:</label>
                                                    <asp:TextBox ID="lbl_PlazoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_TasaNormalA" class="module_subsec_elements module_subsec_medium-elements">Tasa Ordinaria Anual:</label>
                                                    <asp:TextBox ID="lbl_TasaNormalB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_TasaMoraA" class="module_subsec_elements module_subsec_medium-elements">Tasa Moratoria Mensual:</label>
                                                    <asp:TextBox ID="lbl_TasaMoraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_PeriodicidadA" class="module_subsec_elements module_subsec_medium-elements">Periodicidad:</label>
                                                    <asp:TextBox ID="lbl_PeriodicidadB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_ministraA" class="module_subsec_elements module_subsec_medium-elements">Ministraciones:</label>
                                                    <asp:TextBox ID="lbl_ministraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_fechaliberaA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Fecha disposición estimada: "></asp:Label>
                                                    <asp:TextBox ID="lbl_fechaliberaB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tipoplanA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo Plan Pagos: "></asp:Label>
                                                    <asp:TextBox ID="lbl_tipoplanB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:TextBox>
                                                    &nbsp;<asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" Text="Ver"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnl_DatosCaptacion" runat="server" Visible="False">
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1" style="margin-bottom: -15px!important;">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_ProductoCap" class="texto">Producto: </label>
                                                    <asp:Label ID="lbl_ProductoCap1" runat="server" class="texto"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_TasaCap" class="texto">Tasa Anual:</label>
                                                    <asp:Label ID="lbl_TasaCap1" runat="server" class="texto"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_SaldoCap" class="texto">Saldo Actual:</label>
                                                    <asp:Label ID="lbl_SaldoCap1" runat="server" class="texto"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_UltFechaDepCap" class="texto">Ultima Fecha de Deposito:</label>
                                                    <asp:Label ID="lbl_UltFechaDepCap1" runat="server" class="texto"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                            <div class="module_subsec_elements vertical flex_1">
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <label id="lbl_UltFechaRetCap" class="texto">Ultima Fecha de Deposito: </label>
                                                    <asp:Label ID="lbl_UltFechaRetCap1" runat="server" class="texto"></asp:Label>
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
                                        Text="Ingresos-Gastos"></asp:LinkButton>

                                    <asp:LinkButton ID="lnk_restructura" runat="server" CssClass="module_subsec flex_center"
                                        Text="Detalle Reestructura"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </section>


            <section class="panel" runat="server" id="pnl_dictamen">
                <header class="panel_header_folder panel-heading">
                    <span>
                        <asp:Label ID="Label4" runat="server" Text="Dictamen"></asp:Label></span>
                    <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
                </header>

                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <div class="module_subsec columned three_columns align_items_flex_start no_m">

                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div">
                                        <asp:DropDownList ID="cmb_Justificado" runat="server" class="btn btn-primary2 dropdown_label">
                                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="JUSTIFICADO">OPERACION JUSTIFICADA</asp:ListItem>
                                            <asp:ListItem Value="NOJUSTIFICADO">OPERACION NO JUSTIFICADA</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Dictamen:</span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Justificado" CssClass="alertaValidator"  
                                                ControlToValidate="cmb_Justificado" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                                ValidationGroup="val_Resultado" InitialValue="-1" />
                                        </div>
                                    </div>
                                </div>

                            </div>



                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_observacion" runat="server" CssClass="text_input_nice_textarea"
                                        MaxLength="5000" TextMode="MultiLine" Width="700"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span id="lbl_observacion" class="text_input_nice_label">*Observaciones:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_observacion"
                                            runat="server" ControlToValidate="txt_observacion" CssClass="textoazul"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Resultado"></asp:RequiredFieldValidator>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_observacion"
                                            runat="server" Enabled="True"
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            TargetControlID="txt_observacion"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>


                            </div>

                        </div>


                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_Resultado" runat="server" CssClass="btn btn-primary module_subsec_elements no_tbm"
                                Text="Terminar" ValidationGroup="val_Resultado" />
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusresultado" runat="server" Font-Bold="True"
                                Font-Names="Verdana" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </section>
        </asp:Panel>




        <asp:Panel ID="pnl_CerrarSesion" runat="server" CssClass="modalPopup">
            <asp:Panel ID="pnl_Titulo" runat="server" CssClass="modalHeader">
                <asp:Label ID="lbl_tit" runat="server" class="modalTitle" Text="Dictamen Guardado"></asp:Label>
            </asp:Panel>
            <br />              
            <div class="modalContent">
                <div class="module_subsec">
                    <asp:Label ID="lbl_CerrarSesion" runat="server" class="subtitulos">¿Esta seguro de querer cerrar la sesión?</asp:Label>
                </div>  
                <br />
                <div class="module_subsec flex_center">
                    <asp:Button ID="btn_CerrarSesion" runat="server" class="btn btn-primary" Text="Continuar" />&nbsp;&nbsp;
                    <asp:Button ID="btn_OtraAlerta" runat="server" class="btn btn-primary" Text="Cancelar" />
                </div>
            </div>

        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True" Enabled="True" PopupControlID="pnl_CerrarSesion"
            PopupDragHandleControlID="pnl_Titulo" TargetControlID="hdn_nombrebusqueda"
            DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>
    </div>



    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
</asp:Content>

