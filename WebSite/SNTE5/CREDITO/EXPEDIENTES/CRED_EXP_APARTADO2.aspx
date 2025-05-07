<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO2.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO2" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <section class="panel" id="panel_datos_pagos">
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

    <section class="panel" id="panel_pagos">
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Pagos</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_verplanpago" Enabled="false">
                            Ver plan de pagos<i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div> 

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Textbox Enabled="false" runat="server" CssClass="text_input_nice_input" ID="lbl_monto"></asp:Textbox>                                  
                            <div class="text_input_nice_labels"> 
                                <asp:Label runat="server" Text="*Monto:" CssClass="text_input_nice_label" ID="lbl_lmonto"></asp:Label>                                
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">                                    
                            <asp:Textbox Enabled="False" runat="server" CssClass="text_input_nice_input" ID="lbl_plazo"></asp:Textbox>
                            <div class="text_input_nice_labels"> 
                                <asp:Label runat="server" CssClass="text_input_nice_label" ID="lbl_lplazo"></asp:Label>                                
                            </div>
                        </div>
                    </div>
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_fechaliberacion" runat="server" class="text_input_nice_input"
                            MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox>
                                <div class="text_input_nice_labels"> 
                                    <asp:Label ID="lbl_fechaliberacion" runat="server" CssClass="text_input_nice_label"
                                    Text="*Fecha pago del préstamo:(DD/MM/AAAA)"></asp:Label>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaliberacion"
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaliberacion">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaliberacion" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechaliberacion">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaliberacion"
                                    runat="server" ControlExtender="MaskedEditExtender_fechaliberacion"
                                    ControlToValidate="txt_fechaliberacion" CssClass="textogris"
                                    ErrorMessage="MaskedEditValidator_fechaliberacion" Display="Dynamic"
                                    InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechaliberacion" runat="server"
                                    ControlToValidate="txt_fechaliberacion" CssClass="textogris" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_fecha"></asp:RequiredFieldValidator>
                                </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList runat="server" ID="cmb_tipoplan" AutoPostBack="true" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                            <div class="text_input_nice_labels"> 
                                <asp:Label runat="server" Text="*Elija un tipo de plan:" CssClass="text_input_nice_label" ID="lbl_tipoplan"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                 
                <%--  PANEL SALDOS INSOLUTOS--%>
                <asp:UpdatePanel ID="upd_pnl_si" runat="server" Visible ="false"  Width="50%">
                <ContentTemplate>
                    <div class="tamano-cuerpo">
                    <div class= "module_subsec low_m columned three_columns top_m">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_tipotasasi" runat="server" class="btn btn-primary2 dropdown_label"
                                            ValidationGroup="val_planpagosi" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels"> 
                                        <asp:Label ID="lbl_tipotasasi" runat="server" CssClass="text_input_nice_label">*Tipo tasa ordinaria:</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipotasasi" runat="server"
                                        ControlToValidate="cmb_tipotasasi" CssClass="textogris" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_tasasi" runat="server" CssClass="text_input_nice_input" MaxLength="6"
                                        Enabled="false"></asp:TextBox>
                                        <div class="text_input_nice_labels"> 
                                            <asp:Label ID="lbl_ast1" runat="server" CssClass="texto" Text="*" Visible="true"></asp:Label>
                                            <asp:Label ID="lbl_indicenorsi" runat="server" CssClass="text_input_nice_label" Visible="False" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lbl_tasasi" runat="server" CssClass="text_input_nice_label"
                                            Text="(Desde - % hasta - %)" Visible="true"></asp:Label>

                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasai"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            TargetControlID="txt_tasasi" ValidChars=".">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:Label ID="lbl_porcentaje" runat="server" CssClass="text_input_nice_label" Text="Anual"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasasi" runat="server"
                                            ControlToValidate="txt_tasasi" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                                        </div>
                                </div>
                            </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_tipotasamorsi" runat="server" AutoPostBack="True"
                                            class="btn btn-primary2 dropdown_label" ValidationGroup="val_planpagosi">
                                </asp:DropDownList>
                                    <div class="text_input_nice_labels"> 
                                        <asp:Label ID="lbl_tipotasamorsi" runat="server" CssClass="texto" Width="156px">*Tipo tasa moratoria:</asp:Label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipotasamorsi"
                                            runat="server" ControlToValidate="cmb_tipotasamorsi" CssClass="textogris"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                            ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                                    </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasamorsi" runat="server" CssClass="text_input_nice_input"
                                    MaxLength="6" Enabled="false"></asp:TextBox>
                                    <div class="text_input_nice_labels"> 
                                        <asp:Label ID="lbl_ast" runat="server" CssClass="texto" Text="*" Visible="true"></asp:Label>
                                        <asp:Label ID="lbl_indicemorsi" runat="server" CssClass="text_input_nice_label" Visible="False" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lbl_tasamorsi" runat="server" CssClass="text_input_nice_label" Text="(Desde - % hasta - %)" Visible="true"></asp:Label>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="txt_tasamorsi_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            TargetControlID="txt_tasamorsi" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="lbl_porcentajemor" runat="server" CssClass="text_input_nice_label"
                                            Text="Mensual"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamorsi"
                                            runat="server" ControlToValidate="txt_tasamorsi" CssClass="textogris"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                                    </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_tipopersi" runat="server" AutoPostBack="True"
                                    class="btn btn-primary2 dropdown_label" ValidationGroup="val_planpagosi">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels"> 
                                    <asp:Label ID="lbl_tipopersi" runat="server" CssClass="text_input_nice_label"
                                        Text="*Tipo periodicidad:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipopersi"
                                        runat="server" ControlToValidate="cmb_tipopersi" CssClass="textogris"
                                        Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                        ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_periodicidadSI" runat="server" class="btn btn-primary2 dropdown_label"
                                    ValidationGroup="val_planpagosi"></asp:DropDownList>
                                <div class="text_input_nice_labels"> 
                                    <asp:Label ID="lbl_persi" runat="server" CssClass="text_input_nice_label" Text="*Periodicidad:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Req_periodicidadSI" runat="server"
                                    ControlToValidate="cmb_periodicidadSI" CssClass="textogris" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>    
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:LinkButton ID="lnk_generar1" runat="server" class="textogris"
                                    Text="Agregar día" ValidationGroup="val_planpagosi" Visible="False"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                  

                <div class= "module_subsec low_m columned three_columns"> 
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Label ID="lbl_diaspago_SI" runat="server" CssClass="text_input_nice_input" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_periodos_SI" runat="server" CssClass="texto"
                                Text="*Días de pago en el mes:" Visible="False"></asp:Label>                             
                        </div>
                    </div>
                </div>
                               
                <div class="module_subsec flex_end">
                        <asp:Button ID="lnk_generar_si" runat="server" CssClass="btn btn-primary" Text="Generar Plan" ValidationGroup="val_planpagosi"></asp:Button>
                </div>

                <p align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>                        
                    <asp:Label ID="lbl_errortasasi" runat="server" CssClass="alerta"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasasi" runat="server" Display="Dynamic" ControlToValidate="txt_tasasi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    <asp:Label ID="lbl_errortasamorsi" runat="server" CssClass="alerta"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamorsi" runat="server" ControlToValidate="txt_tasamorsi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" display="Dynamic" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                </p>
                    
            </div> 
         
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnk_generar_si" />
                        </Triggers>
        </asp:UpdatePanel>
                                 
                <%--  PANEL PAGOS FIJOS SALDOS INSOLUTOS--%>
                <asp:UpdatePanel ID="upd_pnl_pfsi" runat="server" Visible="false">
                    <ContentTemplate>
                    <div class="tamano-cuerpo">
                    <div class= "module_subsec low_m columned three_columns top_m">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasanorpfsi" runat="server" CssClass="text_input_nice_input" MaxLength="6">
                                </asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tasanorpfsi" runat="server" CssClass="texto"></asp:Label>
                                        <asp:Label ID="lbl_porcentajefijo" runat="server" CssClass="texto" Text="Anual"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tasanorpfsi" CssClass="textogris" ControlToValidate="txt_tasanorpfsi"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasanorpfsi"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            TargetControlID="txt_tasanorpfsi" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasamorpfsi" runat="server" CssClass="text_input_nice_input" MaxLength="6">
                                </asp:TextBox>
                                    <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_tasamorpfsi" runat="server" CssClass="texto"></asp:Label>
                                            <asp:Label ID="lbl_porcentajepfsi" runat="server" CssClass="texto" Text="Mensual"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tasamorpfsi" CssClass="textogris" ControlToValidate="txt_tasamorpfsi"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasamorpfsi"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            TargetControlID="txt_tasamorpfsi" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns top_m">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_tipoperiodicidad" runat="server" AutoPostBack="True"
                                            CssClass="btn btn-primary2 dropdown_label" ValidationGroup="val_planpago">
                                </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tipoperiodicidad" runat="server" CssClass="text_input_nice_label"
                                        Text="*Tipo periodicidad: "></asp:Label>
                                        <asp:RequiredFieldValidator ID="Req_tipoperiodicidad" runat="server"
                                            ControlToValidate="cmb_tipoperiodicidad" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpago"></asp:RequiredFieldValidator>
                                    </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_periodicidad" runat="server" class="btn btn-primary2 dropdown_label"
                                            ValidationGroup="val_planpago">
                                </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_periodicidad" runat="server" CssClass="text_input_nice_label"
                                            Text="*Periodicidad:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="Req_periodicidad" runat="server"
                                            ControlToValidate="cmb_periodicidad" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpago"></asp:RequiredFieldValidator>
                                        
                                    </div> 
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:LinkButton ID="lnk_generar0" runat="server" class="textogris"
                                    Text="Agregar día" ValidationGroup="val_planpago" Visible="False"></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns top_m">                        

                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:Label ID="lbl_diaspago" runat="server" CssClass="text_input_nice_input" Visible="False"></asp:Label>
                                <asp:Label ID="lbl_periodos" runat="server" CssClass="text_input_nice_label"
                                    Text="*Días de pago en el mes:" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>

                        <div class="module_subsec flex_end">
                            <asp:LinkButton ID="lnk_generar" runat="server" class="btn btn-primary" Text="Generar Plan" ValidationGroup="val_planpago"></asp:LinkButton>
                        </div>

                        <p align="center">
                            <asp:Label ID="lbl_status_pfsi" runat="server" CssClass="alerta"></asp:Label>
                            <asp:Label ID="lbl_errortasanorpfsi" runat="server" CssClass="alerta"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasanorpfsi" runat="server" ControlToValidate="txt_tasanorpfsi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        </p>

                        <p align="center">
                                <asp:Label ID="lbl_errortasamorpfsi" runat="server" Cssclass="alerta"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamorpfsi" runat="server" ControlToValidate="txt_tasamorpfsi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>       
                        </p>

                    </div>
                        </ContentTemplate>
                                <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnk_generar" />
                                    </Triggers>
                    </asp:UpdatePanel>
                        
                <div align="center">
                    <asp:Label runat="server" CssClass="alerta" ID="lbl_statusapartado"></asp:Label>
                </div>              
           
            </div>
        </div>
    </section>
 </asp:Content>
