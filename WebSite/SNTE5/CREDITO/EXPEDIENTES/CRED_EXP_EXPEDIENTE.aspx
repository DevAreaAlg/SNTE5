<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_EXPEDIENTE.aspx.vb" Inherits="SNTE5.CRED_EXP_EXPEDIENTE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        // Javascript para utilizar la Busqueda de un Afiliado o Persona

        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=650,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function ResumenPersonaM() {
            window.open("ResumenPersonaM.aspx", "RPM", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function det_com() {
            window.open("CRED_EXP_DICTAMEN.aspx", "DD", "width=650px,height=400px,Scrollbars=YES");
        }
        function det_restructura() {
            window.open("CRED_EXP_REESTRUCTURA.aspx", "DR", "width=650px,height=400px,Scrollbars=YES");
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>
    <!-----------------------------
            PANEL GENERALES
    --------------------------------->
    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span>Datos generales</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content init_show">

                <table border="0" width="100%">
                    <tr>
                        <td width="70%">
                            <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                <div class="module_subsec_elements vertical flex_1">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <label id="Label1" runat="server" class="module_subsec_elements module_subsec_medium-elements">Núm. de expediente:</label>
                                        <asp:TextBox Enabled="false" ID="lbl_folioa" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_clasA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Clasificación del préstamo: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_clasB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_MontoA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Monto: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_MontoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_PlazoA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Plazo: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_PlazoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>

                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_PeriodicidadA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Periodicidad: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_PeriodicidadB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="module_subsec_elements vertical flex_1">
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_TasaNormalA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tasa ordinaria anual: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_TasaNormalB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_TasaMoraA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tasa moratoria mensual: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_TasaMoraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>

                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_fechaliberaA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Disposición estimada: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_fechaliberaB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                    </div>
                                    <div class="module_subsec columned no_m align_items_flex_center ">
                                        <asp:Label ID="lbl_tipoplanA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo de plan de pagos: "></asp:Label>
                                        <asp:TextBox Enabled="false" ID="lbl_tipoplanB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:TextBox>
                                        &nbsp;<asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" Text="Ver"></asp:LinkButton>
                                    </div>
                                    <%-- <div class="module_subsec columned no_m align_items_flex_center ">  
                                        <asp:Label ID="lbl_razonA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Razón:"></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_razonB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>   --%>
                                    <%--<div class="module_subsec columned no_m align_items_flex_center ">  
                                        <asp:Label ID="lbl_notasA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Notas: "></asp:Label>
                                        <asp:Textbox Enabled="false" ID="lbl_notasB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>--%>
                                    <%-- <div class="module_subsec columned no_m align_items_flex_center ">  
                                        <label id="lbl_res" runat="server" class="module_subsec_elements module_subsec_medium-elements">Investigación PEP:</label>
                                        <asp:Textbox Enabled="false" ID="lbl_res_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>--%>
                                    <%--<div class="module_subsec columned no_m align_items_flex_center "> 
                                        <label id="Label2" runat="server" class="module_subsec_elements module_subsec_medium-elements">Descripción puesto PEP:</label>
                                        <asp:Textbox Enabled="false" ID="lbl_desc_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                    </div>--%>
                                    <%-- <div class="module_subsec columned no_m align_items_flex_center "> 
                                        <label id="LBL_NIv" runat="server" class="module_subsec_elements module_subsec_medium-elements">Nivel puesto PEP:</label>
                                        <asp:Textbox Enabled="false" ID="lbl_nivel_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"   ></asp:Textbox>
                                    </div>--%>
                                </div>
                            </div>
                        </td>

                        <td width="30%">

                            <asp:LinkButton ID="lnk_persona" runat="server" CssClass="module_subsec flex_center"
                                Text="Datos afiliado"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_docsexp" runat="server" CssClass="module_subsec flex_center"
                                Text="Documentación"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_garantias" runat="server" CssClass="module_subsec flex_center"
                                Text="Garantías"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_redsocial" runat="server" CssClass="module_subsec flex_center"
                                Text="Red social"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_historial" runat="server" CssClass="module_subsec flex_center"
                                Text="Historial"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_gastos" runat="server" CssClass="module_subsec flex_center"
                                Text="Ingresos-Gastos"></asp:LinkButton>

                           <%-- <asp:LinkButton ID="lnk_detalle_comite" runat="server" CssClass="module_subsec flex_center"
                                Text="Detalle dictamen comité"></asp:LinkButton>--%>

                            <asp:LinkButton ID="lnk_restructura" runat="server" CssClass="module_subsec flex_center"
                                Text="Detalle reestructura"></asp:LinkButton>

                            <asp:LinkButton ID="lnk_notas" runat="server" class="module_subsec flex_center"
                                Text="Notas expediente " ToolTip="Dé Click si desea ver las notas del expediente." />

                        </td>
                    </tr>
                </table>


                <asp:Panel ID="pnl_notas" runat="server" Width="356px" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <span>Notas</span>
                            </header>

                            <div class="panel-body vertical align_items_flex_center">
                                <asp:Panel runat="server" ID="pnl_texto" widht="350px">
                                    <asp:TextBox ID="lbl_notasexp" runat="server" class="textocajas" MaxLength="5000"
                                        Width="300px" Height="250px" TextMode="MultiLine"></asp:TextBox>
                                </asp:Panel>

                                <div align="center">
                                    <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Text="Cerrar" />
                                </div>
                            </div>
                        </section>
                    </div>
                </asp:Panel>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" Enabled="True" PopupControlID="pnl_notas" PopupDragHandleControlID="pnl_Titulo"
                    TargetControlID="hdn_notas" DynamicServicePath="">
                </ajaxToolkit:ModalPopupExtender>

            </div>
        </div>
    </section>

    <!-----------------------------
                PANEL FORO VOTACION
        --------------------------------->
    <section class="panel" visible="false" runat="server" id="TabPanel2">
        <header class="panel_header_folder panel-heading">
            <span>Foro/Votación</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="up_votacion" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <br />
                        <div align="center">
                            <asp:Label ID="lbl_votosf" runat="server" class="subtitulos"></asp:Label>
                            &nbsp;&nbsp;
                                <asp:Label ID="lbl_votosc" runat="server" class="subtitulos"></asp:Label>
                            &nbsp;&nbsp;
                                <asp:Label ID="lbl_votosa" runat="server" class="subtitulos"></asp:Label>
                            &nbsp;&nbsp;
                                <asp:Label ID="lbl_miembrosc" runat="server" class="subtitulos"></asp:Label>
                        </div>

                        <div class="module_subsec low_m columned three_columns" style="flex: 1;">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_voto" runat="server" class="btn btn-primary2 dropdown_label">
                                        <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="FAVOR">A FAVOR</asp:ListItem>
                                        <asp:ListItem Value="CONTRA">EN CONTRA</asp:ListItem>
                                        <asp:ListItem Value="ABSTENCION">ABSTENCION</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_voto" runat="server" class="text_input_nice_label" Text="Voto:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="req_cmbvoto" runat="server" ControlToValidate="cmb_voto"
                                            ValidationGroup="valvoto" Text="Falta Dato!" CssClass="textogris"
                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <asp:Button ID="btn_votar" runat="server" class="btn btn-primary" Text="Votar" ValidationGroup="valvoto" />
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_comentario" runat="server" CssClass="texto" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_comentario" runat="server" class="text_input_nice_label" Text="Comentario:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_comentario"
                                            runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                            TargetControlID="txt_comentario" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="req_txtcoment" runat="server" ControlToValidate="txt_comentario"
                                            ValidationGroup="valcoment" Text="Falta Dato!" CssClass="textogris"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <asp:Button ID="btn_enviar" runat="server" class="btn btn-primary"
                                    Text="Enviar" ValidationGroup="valcoment" />
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:LinkButton ID="lnk_actualiza" runat="server" CssClass="textogris" Text="Actualizar"></asp:LinkButton>
                        </div>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_post" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="MIEMBRO" HeaderText="Miembro">
                                        <ItemStyle Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                        <ItemStyle Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMENTARIOS" HeaderText="Comentarios">
                                        <ItemStyle Width="50%" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <!-----------------------------
                        PANEL DICTAMEN
                --------------------------------->
    <section class="panel" visible="false" runat="server" id="TabPanel3">
        <header class="panel_header_folder panel-heading">
            <span>Dictamen</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="up_dictamen" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_observacion" runat="server" CssClass="text_input_nice_textarea"
                                        MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_observacion" runat="server" class="text_input_nice_label" Text="Observación final:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_observacion"
                                            runat="server" Enabled="True"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                            TargetControlID="txt_observacion" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_resultado" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                        <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="APROBADO">APROBADO</asp:ListItem>
                                        <asp:ListItem Value="RECHAZADO">RECHAZADO</asp:ListItem>
                                        <asp:ListItem Value="RECHAZOREC">RECHAZADO CON RECOMENDACION</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_resultado" runat="server" class="text_input_nice_label" Text="Resultado del dictamen:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_result" runat="server"
                                            ControlToValidate="cmb_resultado" CssClass="textogris" Display="Dynamic"
                                            InitialValue="0" Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MontoAutor" runat="server" CssClass="text_input_nice_input" Enabled="false" ValidationGroup="valresultado"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MontoAutor" runat="server" class="text_input_nice_label" Text="Monto autorizado:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_conf" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoAutor">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxt_MontoAutor" runat="server"
                                            ControlToValidate="txt_MontoAutor" CssClass="textogris" Display="Dynamic"
                                            Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_Recomendaciones" runat="server" CssClass="text_input_nice_textarea"
                                        MaxLength="1000" TextMode="MultiLine" Enabled="false" ValidationGroup="valresultado"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Recomendaciones" runat="server" class="text_input_nice_label" Text="Motivo del rechazo y recomendaciones:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Recomendaciones"
                                            runat="server" Enabled="True"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                            TargetControlID="txt_Recomendaciones" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxt_Recomendaciones" runat="server"
                                            ControlToValidate="txt_Recomendaciones" CssClass="textogris" Display="Dynamic"
                                            Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">

                            <asp:Label ID="lbl_resdictamen" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_resultado" runat="server" class="btn btn-primary"
                                Text="Guardar" ValidationGroup="valresultado" />
                            <br />
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:LinkButton ID="lnk_dictamen" runat="server" CssClass="textogris"
                                Text="Generar Dictamen" Enabled="False" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_digitalizar" runat="server" CssClass="textogris"
                                    Text="Digitalizar Dictamen" Enabled="False" Visible="false"></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnk_dictamen" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <input type="hidden" name="hdn_notas" id="hdn_notas" runat="server" />

</asp:Content>

