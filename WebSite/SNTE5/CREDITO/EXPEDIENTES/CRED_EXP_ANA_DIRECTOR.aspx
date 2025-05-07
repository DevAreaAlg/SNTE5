<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_ANA_DIRECTOR.aspx.vb" Inherits="SNTE5.CRED_EXP_ANA_DIRECTOR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
            // Javascript para utilizar la Busqueda de un Afiliado o Persona

        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=650,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function ResumenPersonaM() {
            window.open("ResumenPersonaM.aspx", "RPM", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function det_com() {
            window.open("DetalleDictamen.aspx", "DD", "width=800px,height=650px,Scrollbars=YES");
        }
        function det_restructura() {
            window.open("DetalleRestructura.aspx", "DR", "width=500px,height=650px,Scrollbars=YES");
        }
    </script>
    
            <section class="panel">
                <header class="panel-heading">
                    <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                        <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                    </div>
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                        <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
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
                                                    <asp:Label ID="lbl_clasA" runat="server" Text="Clasificación del préstamo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_clasB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_MontoA" runat="server" Text="Monto: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_MontoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_PlazoA" runat="server" Text="Plazo: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_PlazoB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_PeriodicidadA" runat="server" Text="Periodicidad: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_PeriodicidadB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>  
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_fechaliberaA" runat="server" Text="Disposición estimada: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_fechaliberaB" runat="server"  class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div> 
                                            </div>

                                            <div class="module_subsec_elements vertical flex_1">                                                                                            
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_tipoplanA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Tipo de plan de pagos: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_tipoplanB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                    &nbsp;<asp:LinkButton ID="lnk_PlanPagos" runat="server" class="textogris" Text="Ver"></asp:LinkButton>
                                                </div>
                                                 <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_TasaNormalA" runat="server" Text="Tasa ordinaria anual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_TasaNormalB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center">
                                                    <asp:Label ID="lbl_TasaMoraA" runat="server" Text="Tasa moratoria mensual: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_TasaMoraB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_emergA" runat="server" Text="Emergencia médica: " class="module_subsec_elements module_subsec_medium-elements"></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_emergB" runat="server"  class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div> 
                                                <%--<div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_razonA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Razón: "></asp:Label>
                                                    <asp:Textbox Enabled="false" ID="lbl_razonB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>                                            
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:label id="lbl_res" runat="server" class="module_subsec_elements module_subsec_medium-elements"  >Investigación PEP:</asp:label>
                                                    <asp:Textbox Enabled="false" ID="lbl_res_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:label id="Label2" runat="server" class="module_subsec_elements module_subsec_medium-elements">Descripción puesto PEP:</asp:label>
                                                    <asp:Textbox Enabled="false" ID="lbl_desc_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:label id="LBL_NIv" runat="server" class="module_subsec_elements module_subsec_medium-elements">Nivel puesto PEP:</asp:label>
                                                    <asp:Textbox Enabled="false" ID="lbl_nivel_pep" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                                                </div>
                                                <div class="module_subsec columned no_m align_items_flex_center ">
                                                    <asp:Label ID="lbl_notasA" runat="server" class="module_subsec_elements module_subsec_medium-elements" Text="Notas: "></asp:Label>
                                                    <asp:Label Enabled="false" ID="lbl_notasB" runat="server" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Label>
                                                </div>--%>
                                            </div>
                                        </div>                                    
                                    </td>

                                    <td width="30%">                                   
                                        <asp:LinkButton ID="lnk_persona" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Datos Afiliado"></asp:LinkButton>
                                   
                                        <asp:LinkButton ID="lnk_docsexp" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Documentación"></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnk_garantias" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Garantías"></asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnk_redsocial" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Red Social"></asp:LinkButton>

                                        <asp:LinkButton ID="lnk_historial" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Historial"></asp:LinkButton>

                                        <!---<asp:LinkButton ID="lnk_gastos" runat="server" Cssclass="module_subsec flex_center" 
                                            Text="Ingresos-Gastos"></asp:LinkButton>-->
                                        
                                        <asp:LinkButton ID="lnk_restructura" runat="server" CssClass="module_subsec flex_center" 
                                            Text="Detalle Reestructura"></asp:LinkButton>

                                        <asp:LinkButton ID="lnk_notas" runat="server" class="module_subsec flex_center" 
                                            Text="Notas expediente " ToolTip = "Dé Click si desea ver las notas del expediente." />

                                    </td>
                                </tr>
                            </table>
                        
                        <asp:Panel ID="pnl_notas" runat="server" Width="356px" Style="display: none;" Align="Center">
                            <div style="position:fixed; top:0; left:0; display:flex;width:100%;height:100%;background-color:rgba(0,0,0,.6); justify-content:center; align-items:center;">
                                <section class="panel no_bm " style="display:inline-block" > 
                                    <header runat="server" class="panel-heading ">
                                        <span>Notas</span>
                                    </header>  
            
                                    <div class="panel-body vertical align_items_flex_center">
                                        <asp:Panel runat="server" id="pnl_texto" widht="350px">               
                                            <asp:Textbox ID="lbl_notasexp" runat="server" class="textocajas" MaxLength="5000" 
                                                Width="300px" HEIGHT="250px" TextMode="MultiLine"></asp:Textbox>               
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
                    TargetControlID="hdn_notas" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
                    </div>
                </div>
            </section>

            <!-----------------------------
                        PANEL DICTAMEN
                ---------------------------------> 
            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>Dictamen</span>
                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <asp:UpdatePanel ID="up_dictamen" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                            <div class= "module_subsec low_m columned three_columns flex_start">
                                <div class="module_subsec_elements" style="flex:1;">
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_observacion" runat="server" CssClass="text_input_nice_input" 
                                            MaxLength="1000" TextMode="MultiLine" ></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_observacion" runat="server" class="text_input_nice_label" Text="Observación final:"></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_observacion" 
                                                runat="server" Enabled="True" 
                                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
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

                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_resultado" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="APROBADO">APROBADO</asp:ListItem>
                                            <asp:ListItem Value="RECHAZADO">RECHAZADO</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_resultado" runat="server" class="text_input_nice_label" Text="*Resultado del dictamen:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_result" runat="server" 
                                                ControlToValidate="cmb_resultado" Cssclass="textogris" Display="Dynamic" 
                                                InitialValue="0" Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <!--<div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_MontoAutor" runat="server" CssClass="text_input_nice_input" 
                                                Enabled = "False" ValidationGroup="valresultado"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_MontoAutor" runat="server" class="text_input_nice_label" Text="Monto autorizado:"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_conf" runat="server" Enabled="True"
                                                    FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoAutor" >
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxt_MontoAutor" runat="server" 
                                                    ControlToValidate="txt_MontoAutor" Cssclass="textogris" Display="Dynamic" 
                                                    Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                            </div>
                                    </div>
                                </div>-->
                            </div>

                            <!--<div class= "module_subsec low_m columned three_columns flex_start">
                                <div class="module_subsec_elements" style="flex:1;">
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_Recomendaciones" runat="server" CssClass="texto" 
                                            MaxLength="1000" TextMode="MultiLine" Enabled = "False" 
                                            ValidationGroup="valresultado"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_Recomendaciones" runat="server" class="text_input_nice_label" 
                                                    Text="Motivo del rechazo y recomendaciones:"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Recomendaciones" 
                                                    runat="server" Enabled="True" 
                                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                                    TargetControlID="txt_Recomendaciones" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxt_Recomendaciones" runat="server" 
                                                    ControlToValidate="txt_Recomendaciones" Cssclass="textogris" Display="Dynamic" 
                                                    Text="Falta Dato!" ValidationGroup="valresultado"></asp:RequiredFieldValidator>
                                            </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">

                                    </div>
                                </div>
                            </div>-->
                                  
                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_resdictamen" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_end">                                  
                                <asp:Button ID="btn_resultado" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="valresultado" AutoPostBack="False" />
                            </div>
                              
                            <!--<div class="module_subsec flex_end"> 
                                <asp:LinkButton ID="lnk_dictamen" runat="server" CssClass="textogris" 
                                    Enabled="False" Text="Generar Dictamen"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_digitalizar" runat="server" CssClass="textogris" 
                                    Enabled="False" Text="Digitalizar Dictamen"></asp:LinkButton>
                            </div>-->
                        </ContentTemplate>
                            <%--<Triggers>
                                <asp:PostBackTrigger ControlID="btn_resultado" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        <input type="hidden" name="hdn_notas" id="hdn_notas" runat="server" />
</asp:Content>

