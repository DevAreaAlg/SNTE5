<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO6.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO6" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
        function generahoja() {
            var wbusf = window.open("CRED_EXP_HOJA_INV.aspx", wbusf, "width=1000,height=1600,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
    </script> 
        <div class="tamano-cuerpo">
         <section class="panel" >
                <header class="panel-heading">
                     <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                            <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                            <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div> 
                    </div>
                </div>
            </section>

            <section class="panel" id="panel_exp">
                <header class="panel-heading">
                    <span>Informe de expediente</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_persona" runat="server" class="btn btn-primary2 dropdown_label" 
                                        AutoPostBack="True"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_persona" runat="server" CssClass="texto" Text="Persona:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmb_persona"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_per"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_persona" runat="server" CssClass="btn btn-primary" Text="Actualizar" ValidationGroup="val_per"/>
                        </div>  
                            <asp:Label ID="lbl_aviso" runat="server" class="subtitulos" Text="Se utilizará la fecha de la última investigación, puesto que no ha expirado"></asp:Label>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_ok" runat="server"  class="btn btn-primary" Text="Aceptar" />
                                &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_generahoja" runat="server"  class="btn btn-primary"
                                Text="Generar hoja de investigación" ValidationGroup="val_per"/>
                            </div>
                        
                    </div>
                </div>
            </section>

            <section class="panel" id="panel_requisitos">
                <header class="panel-heading">
                    <span>Informe de Investigación</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                            <asp:Panel ID="pnl_investigacion" runat="server">
                                <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea"
                                            MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                                <div class="text_input_nice_labels"> 
                                                    <asp:Label ID="lbl_notas" runat="server" CssClass="text_input_nice_label" Text="Notas del Investigador:"></asp:Label> 
                                                        
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="txt_notas" Cssclass="textogris" Display="Dynamic" 
                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_inv"></asp:RequiredFieldValidator>
                                                </div>      
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_nom_info" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>  
                                                <div class="text_input_nice_labels">   
                                                    <asp:Label ID="lbl_nom_info" runat="server" CssClass="text_input_nice_label" Text="*Nombre de la persona que proporciona la información:">
                                                    </asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle" runat="server" ControlToValidate="txt_nom_info"
                                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_inv"></asp:RequiredFieldValidator>
                                                </div>            
                                        </div>
                                    </div>
                                </div>

                              
                                <div class="module_subsec">
                                <div class="module_subsec_elements w_100">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_datos" runat="server" class="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"></asp:TextBox> 
                                                <div class="text_input_nice_labels">  
                                                <asp:Label ID="lbl_datos" runat="server" CssClass="text_input_nice_label" Text="Datos Reales Encontrados:"></asp:Label>
                                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_datos" 
                                                    runat="server" Enabled="True" 
                                                    FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                                    TargetControlID="txt_datos" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txt_datos" Cssclass="textogris" Display="Dynamic" 
                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_inv"></asp:RequiredFieldValidator>
                                                </div>         
                                    </div>
                                </div>
                            </div>

                            <div class= "module_subsec low_m columned three_columns">
                                
                                <div class="module_subsec_elements"> 
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_latitud" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels"> 
                                                    <asp:Label ID="lbl_latitud" runat="server" CssClass="text_input_nice_label" Text="Latitud:"></asp:Label>
                                
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_latitud" runat="server"
                                                        TargetControlID="txt_latitud" FilterType="Custom, Numbers"
                                                        ValidChars="-,." Enabled="True" />
                                                </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements"> 
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_longitud" runat="server" class="text_input_nice_input" MaxLength="11"></asp:TextBox>
                                                <div class="text_input_nice_labels"> 
                                                        <asp:Label ID="lbl_longitud" runat="server" CssClass="text_input_nice_label" Text="Longitud:"></asp:Label>

                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_longitud" runat="server"
                                                        TargetControlID="txt_longitud" FilterType="Custom, Numbers"
                                                        ValidChars="-,." Enabled="True" />
                                                </div>
                                        </div>
                                    </div>
                                </div>
                           
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Guardar" />
                                </div>

                            </asp:Panel>
                            </div>
                        </div>
                    </section>

            <section class="panel" id="panel_servicios">
                <header class="panel-heading">
                    <span>Servicios</span>
                </header>
                    <div class="panel-body">
                        <div class="panel-body_content init_show">
                    <!--web control Servicios-->
                             
                        <asp:Panel ID="pnl_web_ctrl" CssClass="module_subsec columned top_m five_columns" runat="server" > 
                                    
                        <div style="justify-content:space-between" class="module_subsec" >
                            <div class="module_subsec_elements ">
                                <div class="module_subsec no_column no_m">
                                    <h4 style="font-weight:normal;"  class="module_subsec_elements  resalte_azul">Gastos Mensuales:</h4>
                                    <asp:Label ID="lbl_gastosMensuales" runat="server"  CssClass="module_subsec_elements to_title_fix"  Font-Bold="true" Text="$00"></asp:Label>
                                </div>
                            </div>
                                    
                        </div> 
                        </asp:Panel>

                        <asp:Label ID="lbl_mensageEstado" ForeColor="red" runat="server" Text=""></asp:Label>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardarEgresos" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                        </div>
                    </div>
                </div>
            </section>
    
    <input type="hidden" name="hdn_coordenadas" id="hdn_coordenadas" value="" runat="server" />
    <input type="hidden" name="hdn_zoom" id="hdn_zoom" value="" runat="server" />
    <input type="hidden" name="hdn_servicios" id="hdn_servicios" value="" runat="server" />
    <input type="hidden" name="hdn_moral" id="hdn_moral" value="" runat="server" />
            </div>
    
 </asp:Content>
