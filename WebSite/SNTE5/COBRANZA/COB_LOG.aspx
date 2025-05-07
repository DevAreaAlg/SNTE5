<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_LOG.aspx.vb" Inherits="SNTE5.COB_LOG" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
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
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input" MaxLength="9"></asp:TextBox>
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
            <span>Bitácora</span>
        </header>
        <div class="panel-body">
            
            <asp:Panel ID="pnl_cobranza" runat="server" Visible="false" >
                <div class="module_subsec low_m columned three_columns ">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_evento" runat="server" class="btn btn-primary2 dropdown_label" AUTOPOSTBACK="true"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_evento" runat="server" CssClass="text_input_nice_label">Evento:</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                             
            </asp:Panel>
                  
             <asp:Panel ID="pnl_llamada" runat="server" Visible="false">
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:Textbox ID="lbl_realizo" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_responsable" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_destinatario_llamada" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_destinatario_llamada" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" Enabled="true" CssClass="textogris" InitialValue="0" 
                                                    ControlToValidate="cmb_destinatario_llamada" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_nombre_destinatario" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_nombre_dest_llamada" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_nombre_destinatario" Display="Dynamic"
                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_fechaejecucion" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_fechaejecucion" runat="server" CssClass="text_input_nice_label" Text="*Fecha: "></asp:Label>                                
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" 
                                                    TargetControlID="txt_fechaejecucion" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txt_fechaejecucion" InvalidValueMessage="Fecha inválida" ValidationGroup="val_bitacora"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage="MaskedEditExtender2" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fechaejecucion"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                                <asp:RequiredFieldValidator runat="server" ID="req_feceje" CssClass="textogris" ControlToValidate="txt_fechaejecucion"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:DropDownList ID="cmb_tipo_tel" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tipotel" runat="server" CssClass="text_input_nice_label" Text="*Tipo de télefono:"></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmb_tipo_tel"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>                               
                                
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_lada" runat="server" class="text_input_nice_input" MaxLength="6"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_lada" runat="server" CssClass="texto" Text="Lada"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_lada" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_lada"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_tel" runat="server" class="text_input_nice_input" MaxLength="15"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_telmov" runat="server" CssClass="text_input_nice_label" Text="*Teléfono:"></asp:Label>                                                   
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_telmov" runat="server" ControlToValidate="txt_tel"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tel" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txt_tel"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_ext" runat="server" class="text_input_nice_input" MaxLength="3" Visible="false" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_extofi" runat="server" CssClass="text_input_nice_label" Text="Extensión:" Visible="false" />                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ext" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txt_ext"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="module_subsec no_m  columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:TextBox ID="txt_tiempo" runat="server" CssClass="text_input_nice_input" MaxLength="4" ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tiempo" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    TargetControlID="txt_tiempo" FilterType="Numbers" Enabled="True" />
                                                <asp:RequiredFieldValidator ID="req_tiempo" runat="server" ControlToValidate="txt_tiempo"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                    ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_resultado" runat="server" CssClass="btn btn-primary2 dropsown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_resacc" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="req_resacc" runat="server" ControlToValidate="cmb_resultado"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_bitacora"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnl_personas" runat="server" Visible="false">
                                    <div class="module_subsec no_m  columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:DropDownList ID="cmb_tiporel" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tiporel" runat="server" CssClass="text_input_nice_label" Text="*Tipo de relación:"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporel" runat="server" ControlToValidate="cmb_tiporel"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:TextBox ID="txt_nombres" runat="server" class="text_input_nice_input" MaxLength="300" ValidationGroup="val_referencias"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_nombres" runat="server" CssClass="text_input_nice_label" Text="Primer nombre:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombres" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_nombres"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_nombre2" runat="server" class="text_input_nice_input" MaxLength="300" ValidationGroup="val_personales" />
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_nombre2" class="text_input_nice_label">Segundo(s) nombre(s):</label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre2" runat="server"
                                                        TargetControlID="txt_nombre2" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                                <asp:TextBox ID="txt_paterno" runat="server" class="text_input_nice_input" MaxLength="100" ValidationGroup="val_Referencias"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_paterno" runat="server" CssClass="text_input_nice_label" Text="Apellido paterno:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_paterno"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_materno" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_materno" runat="server" CssClass="texto" Text="Apellido materno:"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_materno"
                                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_prom_pago_llamada" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_bitacora"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_prom_pago_llamada" runat="server" CssClass="texto" Text="Promesa pago : "></asp:Label>                                
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_prom_pago_llamada" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_prom_pago_llamada" InvalidValueMessage="Fecha inválida"
                                                    ValidationGroup="val_bitacora" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txt_prom_pago_llamada"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_prom_pago_llamada" runat="server" CssClass="text_input_nice_input" MaxLength="10" ></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_monto_prom_pago_llamada" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label>                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_llamada"
                                                    ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_monto_prom_pago_llamada"
                                                    CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>   

                                <div class= "module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex:1;">
                                        <div class="module_subsec_elements_content vertical">
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_detacc" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>
                                                <asp:RequiredFieldValidator runat="server" ID="req_detacc" CssClass="textogris" ControlToValidate="txt_detacc"
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_bitacora" SetFocusOnError="True" />
                                            </div>
                                             <asp:TextBox ID="txt_detacc" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"
                                                 onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                         <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_personas" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>    
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="lnk_guardar" runat="server" CssClass="btn btn-primary" ValidationGroup="val_bitacora" Text="Guardar"/> &nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_llamda" runat="server" Text="Cancelar" CssClass="btn btn-primary"/>
                                </div>                                
                            </asp:Panel>

                            <asp:Panel ID="pnl_evento_cita" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                            <asp:DropDownList ID="cmb_evento_cita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_accion" runat="server" class="text_input_nice_label">*Acción:</asp:Label>                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_evento_cita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_evento" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seg" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                 <asp:DropDownList ID="cmb_cita_seguimiento" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="Label11" runat="server" CssClass="text_input_nice_label" Text="*Cita: "></asp:Label>                                   
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_cita_seguimiento" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                    
                                </asp:Panel>

                                <asp:Panel ID="pnl_agendar" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_c_usuario" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_usuario" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_destinatario" runat="server" class="btn btn-primary2 dropdow_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_destinatario" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_destinatario" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_persona_cita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="LBL_nombre_destinatario" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_persona_cita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_Bitacoras" class="text_input_nice_label">*Sucursal:</label>                                   
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_sucursal" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_cita_fecha" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_fecha" runat="server" CssClass="text_input_nice_label" Text="*Fecha de cita: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_cita_fecha" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_cita_fecha" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_cita_fecha"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="textogris"
                                                        ControlToValidate="txt_cita_fecha" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_cita_hora" runat="server" CssClass="text_input_nice_label" Text="*Hora cita (formato 24 hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txt_hora_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_hora"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cita" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_hora" runat="server" Display="Dynamic"
                                                        ControlToValidate="txt_hora" CssClass="textogris" ErrorMessage=" Error:Formato de hora incorrecto"
                                                         ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_cita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                                                
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" CssClass="textogris"
                                                        ControlToValidate="txt_cita_notas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita" SetFocusOnError="True" />
                                                </div>
                                                 <asp:TextBox ID="txt_cita_notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                     <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_agendar" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_cita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_cita" Text="Guardar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_cita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_usuario_atendio" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_usuario_atendio" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_usuario_atendio" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_atendio" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="Label10" class="text_input_nice_label">*Sucursal registro:</label>                                     
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_atendio" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_atencion" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_resultado_atencion" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="cmb_resultado_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_cita_a"></asp:RequiredFieldValidator>
                                                </div>                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_atencion" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_atendio" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                   
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_atencion" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_atencion" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_fecha_atencion"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_atencion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita_a" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_atencion" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_hora_atendio" runat="server" CssClass="text_input_nice_label" Text="*Hora de registro (formato 24 hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_atencion"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_hora_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cita_a" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_hora_atencion"
                                                        CssClass="textogris" ErrorMessage=" Error:Formato incorrecto" Display="Dynamic"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_duracion_atencion" runat="server" CssClass="text_input_nice_input" MaxLength="4" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_duracion_atencion" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        TargetControlID="txt_duracion_atencion" FilterType="Numbers" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_duracion_atencion"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                        ValidationGroup="val_cita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_cita" runat="server" CssClass="texto" Text="Promesa pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999" TargetControlID="txt_prom_pago_cita" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" MaskType="Date"
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_cita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_cita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txt_prom_pago_cita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>  
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prom_pago_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_cita" runat="server" CssClass="texto" Text="Monto promesa pago: "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_cita"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_monto_prom_pago_cita"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                   <asp:Label ID="lbl_motivo_cita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" CssClass="textogris"
                                                        ControlToValidate="txt_motivo_atencion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_cita_a" SetFocusOnError="True" />
                                                </div>
                                                 <asp:TextBox ID="txt_motivo_atencion" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_seg" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_seguimiento" runat="server" CssClass="btn btn-primary" ValidationGroup="val_cita_a" Text="Guardar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_cita_seg" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>      
                                      
                                </asp:Panel>

                            <asp:Panel ID="pnl_aviso" runat="server" Visible="false">                                    
                                <asp:Label ID="lbl_avisos_gen" runat="server" CssClass="module_subsec h4" Text="Avisos generados "></asp:Label>

                                <div class="module_subsec overflow_x shadow">
                                    <asp:DataGrid ID="dag_aviso" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_AVISO">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PLANTILLA">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PERSONA">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                                                <ItemStyle Width="20px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle Width="300px"/></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="SEGUIMIENTO" Text="Seguimiento">
                                                <ItemStyle Width="20"/></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </asp:Panel>
                            
                                <asp:Panel ID="pnl_seguimiento_aviso" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_destino_aviso" runat="server" CssClass="text_input_nice_input" Enabled="False" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_destino_aviso" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_destinatario_aviso" runat="server" CssClass="text_input_nice_input" Enabled="false" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_nombre_Destinatario_Aviso" runat="server" CssClass="text_input_nice_labels"
                                                        Text="*Nombre destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_aviso" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_Resultado_aviso" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="cmb_resultado_aviso"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_aviso"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_aviso"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_resultado" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_aviso" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_aviso" InvalidValueMessage="Fecha inválida" ValidationGroup="val_aviso"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_fecha_aviso"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_aviso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_aviso" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_usuario_aviso" runat="server" CssClass="text_input_nice_input" Enabled="False" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_usuario_aviso" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_aviso"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_aviso" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_aviso" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_aviso" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_aviso" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txt_prom_pago_aviso"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prom_pago_aviso" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_aviso" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago_aviso"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_monto_prom_pago_aviso"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>

                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_aviso" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" CssClass="textogris"
                                                        ControlToValidate="txt_notas_aviso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_aviso" SetFocusOnError="True" />
                                                </div>
                                                 <asp:TextBox ID="txt_notas_aviso" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000" TextMode="MultiLine" 
                                                    onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                   
                                     <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_aviso" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_aviso" runat="server" CssClass="btn btn-primary" ValidationGroup="val_aviso" Text="Guardar"/>&nbsp;
                                        <asp:Button ID="lnk_guardar_cancelar" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                    
                                    </asp:Panel>
                            
                                <asp:Panel ID="pnl_digitalizar_aviso" runat="server" Visible="false">
                                    <asp:Label ID="lbl_tit_aviso_dig" runat="server" CssClass="module_subsec flex_center" Text="¿Desea digitalizar aviso?"></asp:Label>
                                    <div class="module_subsec flex_center">
                                        <asp:LinkButton ID="lnl_si_aviso" runat="server" CssClass="textogris" Text="Si"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnk_no_aviso" runat="server" CssClass="textogris" Text="No"></asp:LinkButton>
                                    </div>
                                </asp:Panel>

                            <asp:Panel ID="pnl_citatorio" runat="server" Visible="false">
                                <asp:Label ID="lbl_citatorios_sub" runat="server" CssClass="module_subsec subtitulos" Text="Citatorios generados "></asp:Label>

                                <div class="module_subsec overflow_x shadow">
                                    <asp:DataGrid ID="dag_citatorios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_CITATORIO">
                                            <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PLANTILLA">
                                                <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_PERSONA">
                                            <ItemStyle Width="5px"/></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                                                <ItemStyle Width="20px" /></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle Width="300px" /></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="SEGUIMIENTO" Text="Seguimiento">
                                                <ItemStyle Width="20"></ItemStyle></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento_citatorio" runat="server" Visible="false">
                                    <div class="module_subsec low_m  columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_destino_cit" runat="server" CssClass="text_input_nice_input" Enabled="False" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_destinatario_tit" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_nombre_destinatario_cit" runat="server" CssClass="text_input_nice_input" Enabled="false" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="bl_nom_destinatario_tit" runat="server" CssClass="text_input_nice_label" Text="*Nombre destinatario: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_citatorio" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_resultado_cit" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cmb_resultado_citatorio"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_citatorio"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_citatorio" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_citatorio"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_Fecha_citatorio" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_citatorio" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_citatorio" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_citatorio" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txt_fecha_citatorio"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator22" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_citatorio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_citatorio" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_citatorio" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_citatorio"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_prom_pago_citatorio" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>                                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_citatorio" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_citatorio" InvalidValueMessage="Fecha inválida"
                                                        ValidationGroup="val_citatorio" CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txt_prom_pago_citatorio"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_pago_citatorio" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prom_pago_citatorio" runat="server" CssClass="texto" Text="Monto promesa pago: "></asp:Label>                                        
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_pago_citatorio"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_monto_pago_citatorio"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_notas_citatorio" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator24" CssClass="textogris"
                                                        ControlToValidate="txt_notas_citatorio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_citatorio" SetFocusOnError="True" />
                                                </div>
                                                 <asp:TextBox ID="txt_notas_citatorio" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000" TextMode="MultiLine" 
                                                     onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>         
                                       
                                     <div class="module_subsec low_m  columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_usuario_citatorio" runat="server" CssClass="text_input_nice_input" Enabled="false" Text=""></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_usr_cit" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_citatorio" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_Citatorio" runat="server" CssClass="btn btn-primary" ValidationGroup="val_citatorio" Text="Guardar"/>&#160;
                                        <asp:Button ID="lnk_cancelar_citatorio" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                    
                                    
                                </asp:Panel>

                        </asp:Panel>

                                <asp:Panel ID="pnl_digitalizar_citatorio" runat="server" Visible="false">
                                    <asp:Label ID="lbl_tit_digitalizar" runat="server" CssClass="module_subsec flex_center" Text="¿Desea digitalizar citatorio?"></asp:Label>
                                    <div class="module_subsec flex_center">
                                       <asp:LinkButton ID="lnk_si" runat="server" CssClass="textogris" Text="Si"></asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnk_no" runat="server" CssClass="textogris" Text="No"></asp:LinkButton>
                                    </div>
                                </asp:Panel>

                            <asp:Panel ID="pnl_reg_juicio" runat="server" Visible="false"> 
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:Textbox ID="lbl_user_despacho" runat="server" Enabled="false" CssClass="text_input_nice_input" Text=""></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_realizo" runat="server" CssClass="text_input-nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_ingreso" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_fecha_diligencia" runat="server" CssClass="text_input_nice_label" Text="*Fecha ingreso de demanda: "></asp:Label></td><td>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_ingreso" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender7"
                                                    ControlToValidate="txt_fecha_ingreso" InvalidValueMessage="Fecha inválida" Display="Dynamic"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender7" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txt_fecha_ingreso"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator27" CssClass="textogris" Display="Dynamic"
                                                    ControlToValidate="txt_fecha_ingreso" ErrorMessage=" Falta Dato!" ValidationGroup="val_juicio" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estatus_juicio" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_estatus" runat="server" CssClass="text_input_nice_label" Text="*Estatus juicio: "></asp:Label>                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="cmb_estatus_juicio"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_juicio"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_juzgado" runat="server" CssClass="text_input_nice_input" MaxLength="3000"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_juzgado" runat="server" CssClass="text_input_nice_label" Text="*Juzgado: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_juzgado" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator25" CssClass="textogris"
                                                    ControlToValidate="txt_juzgado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_juicio" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_emp_tit" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                             ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_fecha_emp_tit" runat="server" CssClass="text_input_nice_label" Text="Fecha emplazamiento titular: "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender13" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_emp_tit" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator13" runat="server" ControlExtender="MaskedEditExtender13"
                                                    ControlToValidate="txt_fecha_emp_tit" InvalidValueMessage="Fecha inválida"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender13" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txt_fecha_emp_tit"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_emp_Aval" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_fecha_emp_aval" runat="server" CssClass="text_input_nice_label" Text="Fecha emplazamiento aval: "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender14" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_fecha_emp_Aval" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator14" runat="server" ControlExtender="MaskedEditExtender14"
                                                    ControlToValidate="txt_fecha_emp_Aval" InvalidValueMessage="Fecha inválida" Display="Dynamic"
                                                    ValidationGroup="val_juicio" CssClass="textogris" ErrorMessage="MaskedEditExtender14" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txt_fecha_emp_Aval"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_exhorto" runat="server" CssClass="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_exhorto" runat="server" CssClass="text_input_nice_labels" Text="Exhorto: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_exhorto" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_juzgado_exhorto" runat="server" CssClass="text_input_nice_input" MaxLength="3000"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_juzgado_exhorto" runat="server" CssClass="text_input_nice_label" Text="Juzgado exhortado: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_exhorto" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_gestor" runat="server" CssClass="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ti_Gestor" runat="server" CssClass="text_input_nice_label" Text="*Gestor: "></asp:Label>                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                    TargetControlID="txt_gestor" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,-,.,"></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txt_gestor"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                    ValidationGroup="val_juicio" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                           
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_cita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_cita_juicio" runat="server" CssClass="texto" Text="Cita : "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_cita" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator12" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_cita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_juicio"
                                                    CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txt_cita"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_prom_pago" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                ValidationGroup="val_juicio"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_prom_pago" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txt_prom_pago" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txt_prom_pago" InvalidValueMessage="Fecha inválida" ValidationGroup="val_juicio"
                                                    CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt_prom_pago"
                                                    Format="dd/MM/yyyy" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_prom_pago" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                 <asp:Label ID="Label16" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prom_pago"
                                                    ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" runat="server"
                                                    ControlToValidate="txt_monto_prom_pago" CssClass="textogris" ErrorMessage=" Error:Monto incorrecto"
                                                    lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        
                                <div class= "module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex:1;">
                                        <div class="module_subsec_elements_content vertical">
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_detalle" runat="server" CssClass="text_input_nice_label" Text="*Detalle: "></asp:Label>                               
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" CssClass="textogris"
                                                    ControlToValidate="txt_detalle" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_juicio" SetFocusOnError="True" />
                                            </div>
                                             <asp:TextBox ID="txt_detalle" runat="server" CssClass="text_input_nice_textarea" MaxLength="8000"
                                             TextMode="MultiLine" onKeyUp="javascript:Check(this, 8000);" onChange="javascript:Check(this, 8000);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                                
                                     <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_juicio" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="lnk_agregar_reg_juicio" runat="server" CssClass="btn btn-primary" ValidationGroup="val_juicio" Text="Guardar" />&nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_reg_juicio" runat="server" CssClass="btn btn-primary" Text="Cancelar" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnl_estatus" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:Textbox ID="lbl_user_estatus" runat="server" CssClass="text_input_nice_input" Enabled="False" Text=""></asp:Textbox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_user_estatus" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estatus_cob" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_tit_estatus_cob" runat="server" CssClass="text_input_nice_label" Text="*Estatus cobranza: "></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="cmb_estatus_cob"
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="val_estatus"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                     <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_estatus" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="lnk_agregar_estatus" runat="server" CssClass="btn btn-primary" ValidationGroup="val_estatus" Text="Guardar"/>&nbsp;&nbsp;
                                    <asp:Button ID="lnk_cancelar_estatus" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                </div> 
                            </asp:Panel>

                            <asp:Panel ID="pnl_evento_visita" runat="server" Visible="false">
                                <div class="module_subsec no_m columned three_columns">
                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_evento_visita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <label id="lbl_tit_accion_visita" class="text_input_nice_label">*Acción:</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29" Enabled="true"
                                                    CssClass="textogris" ControlToValidate="cmb_evento_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_visita_evento" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                                <asp:Panel ID="pnl_seg_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_visita_seguimiento" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="Label17" runat="server" CssClass="text_input_nice_label" Text="*Visita: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator30" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_visita_seguimiento" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_agendar_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:Textbox ID="lbl_usuario_visita" runat="server" CssClass="text_input_nice_input" Enabled="false"></asp:Textbox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_realizo_visita" runat="server" CssClass="text_input_nice_label" Text="*Realizó: "></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_destinatario_visita" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_destinatario_visita" runat="server" CssClass="text_input_nice_label" Text="*Destinatario: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator33" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_destinatario_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_persona_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_nombre_visita" runat="server" CssClass="text_input_nice_label" Text="*Nombre: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator34" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_persona_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_tit_sucursal_visita" class="text_input_nice_label">*Sucursal:</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator35" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_visita"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_fecha_visita" runat="server" CssClass="text_input_nice_label" Text="*Fecha de visita: "></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender15" runat="server" Mask="99/99/9999" CultureTimePlaceholder="" Enabled="True"
                                                        MaskType="Date" TargetControlID="txt_fecha_visita" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""/>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator15" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_visita"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txt_fecha_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator36" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_visita" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_hora_visita" runat="server" CssClass="text_input_nice_label" Text="*Hora visita (formato 24 hrs (HH:MM)):"></asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_visita"></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_hora_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_visita" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                                        ControlToValidate="txt_hora_visita" CssClass="textogris" ErrorMessage=" Error:Formato incorrecto"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_notas_visita" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator38" CssClass="textogris"
                                                        ControlToValidate="txt_noras_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_visita" SetFocusOnError="True" />
                                                </div>
                                                <asp:TextBox ID="txt_noras_visita" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000"
                                                TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                onChange="javascript:Check(this, 2000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec flex_center">
                                        <asp:Label ID="lbl_status_visita" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                    </div>  
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_agregar_visita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_visita" Text="Guardar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_visita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnl_seguimiento_visita" runat="server" Visible="false">
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_usuario_seg_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_user_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Atendió: "></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator45" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_usuario_seg_visita" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_resultado_visita" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_resultado_visita" runat="server" CssClass="text_input_nice_label" Text="*Resultado obtenido: "></asp:Label>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="cmb_resultado_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="val_seg_visita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:DropDownList ID="cmb_sucursal_seg_visita" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <label id="lbl_tit_seg_suc" class="text_input_nice_label">*Sucursal registro:</label>                                     
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" Enabled="true"
                                                        CssClass="textogris" ControlToValidate="cmb_sucursal_seg_visita" Display="Dynamic"
                                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" InitialValue="0" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                       
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_fecha_seg_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="val_seg_visita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_fecha_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Fecha de registro: "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender16" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_fecha_atencion" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator16" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_fecha_seg_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_seg_visita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txt_fecha_seg_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator41" CssClass="textogris"
                                                        ControlToValidate="txt_fecha_seg_visita" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_seg_visita_a" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_hora_seg_visita" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_hora_seg_visita" runat="server" CssClass="text_input_nice_label" Text="*Hora de registro (formato 24 hrs (HH:MM)):"></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                        Enabled="True" FilterType="Numbers,Custom" ValidChars=":" TargetControlID="txt_hora_seg_visita"></ajaxToolkit:FilteredTextBoxExtender>                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_hora_seg_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_seg_visita_a" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_hora_seg_visita"
                                                        CssClass="textogris" ErrorMessage=" Error:Formato incorrecto" Display="Dynamic"
                                                        ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_duracion_visita" runat="server" CssClass="text_input_nice_input" MaxLength="4"
                                                    ValidationGroup="val_cita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_duracion_visita" runat="server" CssClass="text_input_nice_label" Text="*Duración (minutos): "></asp:Label>                                    
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                        TargetControlID="txt_duracion_visita" FilterType="Numbers" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_duracion_visita"
                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" SetFocusOnError="True"
                                                        ValidationGroup="val_seg_visita_a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="module_subsec no_m columned three_columns">
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_prom_pago_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"
                                                    ValidationGroup="val_seg_visita_a"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_seg_prompago" runat="server" CssClass="text_input_nice_label" Text="Promesa de pago : "></asp:Label>                                    
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender17" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_prom_pago_visita" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator17" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txt_prom_pago_visita" InvalidValueMessage="Fecha inválida" ValidationGroup="val_seg_visita_a"
                                                        CssClass="textogris" ErrorMessage="MaskedEditExtender1" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="txt_prom_pago_visita"
                                                        Format="dd/MM/yyyy" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="module_subsec_elements align_items_flex_end">
                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                <asp:TextBox ID="txt_monto_prompago_visita" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_monto_prompago_visita" runat="server" CssClass="text_input_nice_label" Text="Monto promesa pago: "></asp:Label></td><td>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_monto_prompago_visita"
                                                        ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_monto_prompago_visita"
                                                        CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class= "module_subsec low_m columned three_columns flex_start">
                                        <div class="module_subsec_elements" style="flex:1;">
                                            <div class="module_subsec_elements_content vertical">
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_tit_seg_notas" runat="server" CssClass="text_input_nice_label" Text="*Notas:"></asp:Label>                                    
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator44" CssClass="textogris"
                                                        ControlToValidate="txt_seg_notas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_seg_visita_a" SetFocusOnError="True" />
                                                </div>
                                                <asp:TextBox ID="txt_seg_notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="3000"
                                                    TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                                    onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec flex_center">
                                         <asp:Label ID="lbl_status_seg_vis" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                   </div>
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="lnk_guardar_seg_visita" runat="server" CssClass="btn btn-primary" ValidationGroup="val_seg_visita_a" Text="Guardar"/>&nbsp;&nbsp;
                                        <asp:Button ID="lnk_cancelar_seg_visita" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                                    </div>                                                
                                </asp:Panel>
                             <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                                </div> 

                            

        </div>
    </section>        
</asp:Content>
