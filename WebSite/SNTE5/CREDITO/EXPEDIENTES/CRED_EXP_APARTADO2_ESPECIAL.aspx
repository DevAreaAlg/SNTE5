<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO2_ESPECIAL.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO2_ESPECIAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
        <section class="panel" >
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

        <section class="panel" id="panel_pagose">
            <header class="panel_header_folder panel-heading">
                <span>PAGOS</span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">

                        <div class="module_subsec flex_center">
                            <asp:LinkButton runat="server" Text="Ver plan pago" 
                             ID="lnk_verplanpago" class="textogris" Enabled="false"></asp:LinkButton>
                        </div>

                        <div class="module_subsec columned low_m">
                            <span  class="module_subsec_small-elements module_subsec_elements">Monto:</span>
                           <asp:Label runat="server" CssClass="module_subsec_elements" ID="lbl_monto"></asp:Label>
                        </div>

                        <div class="module_subsec columned low_m">
                            <asp:Label runat="server" CssClass="module_subsec_small-elements module_subsec_elements" ID="lbl_lplazo" ></asp:Label>
                             <asp:Label runat="server" CssClass="module_subsec_elements" ID="lbl_plazo"></asp:Label>
                        </div>

                        <div class="module_subsec columned low_m">
                            <span  class="module_subsec_small-elements module_subsec_elements">Tipo de plan:</span>
                            <asp:Label ID="lbl_planespecial" runat="server" class="module_subsec_elements" >Plan Especial</asp:Label>
                        </div>

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fechaliberacion" runat="server" class="text_input_nice_input" 
                                     MaxLength="10" ValidationGroup="val_fecha" ></asp:TextBox>
                                        <div class="text_input_nice_labels"> 
                                        <asp:Label ID="lbl_fechaliberacion" runat="server" CssClass="texto" 
                                        Text="*Fecha Pago del préstamo:(DD/MM/AAAA):" ></asp:Label>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaliberacion" 
                                           runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                           CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                           CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                           Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaliberacion"></ajaxToolkit:MaskedEditExtender>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaliberacion" runat="server" 
                                           Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechaliberacion"></ajaxToolkit:CalendarExtender>
                                         <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaliberacion" 
                                        runat="server" ControlExtender="MaskedEditExtender_fechaliberacion" 
                                        ControlToValidate="txt_fechaliberacion" Display="Dynamic" Cssclass="textogris" 
                                        ErrorMessage="MaskedEditValidator_fechaliberacion" 
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechaliberacion" runat="server" 
                                        ControlToValidate="txt_fechaliberacion" Cssclass="textogris" Display="Dynamic" 
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_fecha"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:LinkButton runat="server" Text="Guardar Fecha" ValidationGroup="val_fecha" 
                                    ID="lnk_fecha" class="textogris"></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div align="center">
                            <asp:Label runat="server" CssClass="alerta" ID="lbl_statusapartado"></asp:Label>
                        </div>
                                
                <%--  PLAN ESPECIAL   --%>
                
                    <asp:Panel ID="pnl_especial" runat="server" HorizontalAlign="left" ScrollBars="Auto">

                            <asp:UpdatePanel ID="upd_fecha_max" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_fechamax" runat="server" Font-Bold="True"  CssClass="text_input_nice_label"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <ajaxToolkit:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" FadeTransitions="true" FramesPerSecond="40" SelectedIndex="-1"
                                TransitionDuration="250" AutoSize="none" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Enabled="false">


                            <Panes>

                                        <%---------------------------------
                                                PANEL INTERES
                                        --------------------------------%>
                                        
                            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server" Enabled="true">
                                <Header>
                                    <span>PAGO DE INTERÉS</span>
                                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                                </Header>
                                <Content>
                                    <div class="module_subsec no_m">
                                        <h5 style="font-weight:normal"  class="resalte_azul">Capture las fechas en que se pagarán los intereses.</h5>
                                    </div>

                                    <asp:Panel ID="updpnl_interes_general" runat="server" ScrollBars="Auto">
                                            <%-- Intereses Forma de Pago--%>
                                            <div class= "module_subsec low_m columned three_columns">
                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:DropDownList ID="cmb_pagoint" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                                <asp:ListItem Value="UNOXUNO">UNO A LA VEZ</asp:ListItem>
                                                                <asp:ListItem Value="RECURRENTE">RECURRENTE</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="text_input_nice_labels">
                                                                <asp:Label ID="Label1" runat="server" class="text_input_nice_labels" Width="150px">*Forma de pago:</asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagoint" runat="server"
                                                                    ControlToValidate="cmb_pagoint" CssClass="textogris" Display="Dynamic"
                                                                    ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                    ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>   

                                            <asp:Panel ID="updpnl_interes" runat="server" ScrollBars="Auto">

                                                <%-- Intereses fecha inicio y fin--%>
                                                <div class= "module_subsec low_m columned three_columns">
                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:TextBox ID="txt_fechainicialperiodointeres" runat="server" class="text_input_nice_input"
                                                             Visible="false" MaxLength="10"></asp:TextBox>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:Label ID="lbl_fechainicialperiodointeres" runat="server" CssClass="texto"
                                                                    Text="*Fecha inicio periodo:" Visible="false"></asp:Label>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechainicialperiodointeres"
                                                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    TargetControlID="txt_fechainicialperiodointeres">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechainicialperiodointeres" runat="server"
                                                                    Enabled="True" Format="dd/MM/yyyy"
                                                                    TargetControlID="txt_fechainicialperiodointeres">
                                                                </ajaxToolkit:CalendarExtender>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechainicialperiodointeres"
                                                                    runat="server" ControlToValidate="txt_fechainicialperiodointeres"
                                                                    CssClass="textogris" Display="Dynamic"
                                                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechainicialperiodointeres"
                                                                    runat="server" ControlExtender="MaskedEditExtender_fechainicialperiodointeres"
                                                                    ControlToValidate="txt_fechainicialperiodointeres" CssClass="textogris"
                                                                    ErrorMessage="MaskedEditValidator_fechainicialperiodointeres" Display="Dynamic"
                                                                    InvalidValueMessage="Fecha inválida"></ajaxToolkit:MaskedEditValidator>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:TextBox ID="txt_fechafinalinteres" runat="server" class="text_input_nice_input"
                                                             Visible="false" MaxLength="10"></asp:TextBox>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:Label ID="lbl_fechafinalinteres" runat="server" CssClass="texto"
                                                                    Text="*Fecha fin periodo: " Visible="false"></asp:Label>
                                                                     <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinalinteres"
                                                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinalinteres">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinalinteres"
                                                                    runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    TargetControlID="txt_fechafinalinteres">
                                                                </ajaxToolkit:CalendarExtender>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinalinteres"
                                                                    runat="server" ControlToValidate="txt_fechafinalinteres" CssClass="textogris"
                                                                    Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!"
                                                                    ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinalinteres"
                                                                    runat="server" ControlExtender="MaskedEditExtender_fechafinalinteres"
                                                                    ControlToValidate="txt_fechafinalinteres" CssClass="textogris"
                                                                    ErrorMessage="MaskedEditValidator_fechafinalinteres"
                                                                    InvalidValueMessage="Fecha inválida" Display="Dynamic"
                                                                    ValidationGroup="val_planpagosespinteres"></ajaxToolkit:MaskedEditValidator>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%-- Intereses Pago recurrente--%>
                                                <div class= "module_subsec low_m columned three_columns">
                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                             <asp:DropDownList ID="cmb_tiporecurrencia" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label" Visible="false">
                                                             </asp:DropDownList>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:Label ID="lbl_pago_rec_interes" runat="server" CssClass="texto"
                                                                    Text="*Pago Recurrente:" Width="129px" Visible="false"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrencia"
                                                                    runat="server" ControlToValidate="cmb_tiporecurrencia" CssClass="textogris"
                                                                    Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                    ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:DropDownList ID="cmb_tiporecurrenciainteres" runat="server" class="btn btn-primary2 dropdown_label" Visible="false">
                                                            </asp:DropDownList>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrenciainteres"
                                                                    runat="server" ControlToValidate="cmb_tiporecurrenciainteres"
                                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                    InitialValue="0"
                                                                    ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:DropDownList ID="cmb_diainteres" runat="server" class="btn btn-primary2 dropdown_label" Visible="False">
                                                            </asp:DropDownList>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:Label ID="lbl_diaper_interes" runat="server" CssClass="texto"
                                                                    Text="*Día de periodicidad:" Width="150px" Visible="false"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_diasemanainteres"
                                                                    runat="server" ControlToValidate="cmb_diainteres" CssClass="textogris"
                                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                    ValidationGroup="val_planpagosespinteres" Visible="False"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
     
                                            </asp:Panel>

                                            <asp:Panel ID="updpnl_guardar_interes" runat="server" ScrollBars="Auto">
                                                <p align="center">
                                                    <asp:Button ID="btn_agregarinteres" runat="server" class="btn btn-primary"
                                                        Text="Agregar" ValidationGroup="val_planpagosespinteres" ToolTip="Agrega la(s) fecha(s) capturadas de Interes" />
                                                    &nbsp;
                                                    <asp:Button ID="Btn_eliminarinteres" runat="server" class="btn btn-primary" Text="Eliminar"
                                                        ToolTip="Elimina todas las fechas capturadas de Interes" />
                                                </p>

                                                <div class="overflow_x shadow" >
                                                    <p align="center">
                                                        <asp:DataGrid ID="dag_pagointeres" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None">
                                                            <Columns>
                                                                <asp:BoundColumn DataField="fechapago" HeaderText="Fecha Pago">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                            <HeaderStyle CssClass="table_header" />
                                                        </asp:DataGrid>
                                                    </p>
                                                </div>

                                                <div align="center">
                                                    <asp:Label ID="lbl_statusinteres" runat="server" CssClass="alerta"
                                                        ValidationGroup="val_planpagosespinteres"></asp:Label>
                                                </div>
                                                
                                            </asp:Panel>
                                    </asp:Panel>
                                   
                                 </Content>
                             </ajaxToolkit:AccordionPane>

                                        <%---------------------------------
                                                PANEL CAPITAL
                                        --------------------------------%>

                            <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server" Enabled="false">
                                <Header>
                                    <span>PAGO DE CAPITAL</span>
                                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                                </Header>
                                <Content>
                                    <div class="module_subsec no_m">
                                        <h5 style="font-weight:normal"  class="resalte_azul">Capture como desea hacer los pagos de capital.</h5>
                                    </div>
                                    <asp:Label ID="lbl_error" runat="server" CssClass="alerta"></asp:Label>

                                    <asp:Panel ID="updpnl_capital_general" runat="server" ScrollBars="Auto">


                                        <%-- Capital forma de pago--%>
                                        <div class= "module_subsec low_m columned three_columns">
                                            <div class="module_subsec_elements"> 
	                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                    <asp:DropDownList ID="cmb_pagocapital" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                            <asp:ListItem Value="UNOXUNO">UNO A LA VEZ</asp:ListItem>
                                                            <asp:ListItem Value="RECURRENTE">RECURRENTE</asp:ListItem>
                                                            <asp:ListItem Value="COPIA">COPIAR FECHAS INTERES</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="text_input_nice_labels">
                                                                <asp:Label ID="lbl_elegir" runat="server" class="texto">*Forma de pago:</asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagocapital" runat="server"
                                                                ControlToValidate="cmb_pagocapital" CssClass="textogris" Display="Dynamic"
                                                                ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                        </div>
                                                </div>
                                            </div>
                                            
                                        </div>

                                        <%-- Capital panel emergente en caso de recurrente o n dia--%>
                                        <asp:Panel ID="updpnl_capital" runat="server" Visible="false" ScrollBars="Auto">                                                
                                                
                                                <div class= "module_subsec low_m columned three_columns">
                                                    <div class="module_subsec_elements"> 
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:TextBox ID="txt_fechainiperiodo" runat="server" class="text_input_nice_input"
                                                              MaxLength="10" Visible="false"></asp:TextBox>
                                                                <div class="text_input_nice_labels"> 
                                                                    <asp:Label ID="lbl_fechainiperiodo" runat="server" CssClass="texto"
                                                                    Text="*Fecha inicio periodo: " Visible="false"></asp:Label> 
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechainiperiodo"
                                                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechainiperiodo">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechainiperiodo"
                                                                        runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="txt_fechainiperiodo">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechainiperiodo"
                                                                        runat="server" ControlToValidate="txt_fechainiperiodo" CssClass="textogris"
                                                                        Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                        ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechainiperiodo"
                                                                        runat="server" ControlExtender="MaskedEditExtender_fechainiperiodo"
                                                                        ControlToValidate="txt_fechainiperiodo" CssClass="textogris"
                                                                        ErrorMessage="MaskedEditValidator_fechainiperiodo" Display="Dynamic"
                                                                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_planpagosespcapital"></ajaxToolkit:MaskedEditValidator>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div class="module_subsec_elements"> 
	                                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                <asp:TextBox ID="txt_fechafinperiodo" runat="server" class="text_input_nice_input"
                                                                    MaxLength="10" Visible="false"></asp:TextBox>
                                                                    <div class="text_input_nice_labels"> 
                                                                        <asp:Label ID="lbl_fechafinperiodo" runat="server" CssClass="texto"
                                                                        Text="*Fecha fin periodo: " Visible="false"></asp:Label>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinperiodo"
                                                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinperiodo">
                                                                        </ajaxToolkit:MaskedEditExtender>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinperiodo"
                                                                            runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                            TargetControlID="txt_fechafinperiodo">
                                                                        </ajaxToolkit:CalendarExtender>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinperiodo"
                                                                            runat="server" ControlToValidate="txt_fechafinperiodo" CssClass="textogris"
                                                                            Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                            ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinperiodo"
                                                                            runat="server" ControlExtender="MaskedEditExtender_fechafinperiodo"
                                                                            ControlToValidate="txt_fechafinperiodo" CssClass="textogris"
                                                                            ErrorMessage="MaskedEditValidator_fechafinperiodo" Display="Dynamic"
                                                                            InvalidValueMessage="Fecha inválida" ValidationGroup="val_planpagosespcapital"></ajaxToolkit:MaskedEditValidator>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                </div>    

                                                <asp:Panel ID="updpnl_periodo" runat="server" Visible="false" ScrollBars="Auto">                      
                                                  
                                                    <div class= "module_subsec low_m columned three_columns">
                                                        <div class="module_subsec_elements"> 
	                                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                <asp:DropDownList ID="cmb_pagorecu" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label" Visible="false">
                                                                </asp:DropDownList>
                                                                    <div class="text_input_nice_labels"> 
                                                                        <asp:Label ID="lbl_pagorecu" runat="server" CssClass="texto"
                                                                        Text="*Pago Recurrente:" Width="129px" Visible="false"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagorecu" runat="server"
                                                                        ControlToValidate="cmb_pagorecu" CssClass="textogris" Display="Dynamic"
                                                                        ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                        ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                    </div>  
                                                            </div>
                                                        </div>

                                                        <div class="module_subsec_elements"> 
	                                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                <asp:DropDownList ID="cmb_tiporecurrenciacapital" runat="server" class="btn btn-primary2 dropdown_label" Visible="false" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                    <div class="text_input_nice_labels"> 
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrenciacapital"
                                                                        runat="server" ControlToValidate="cmb_tiporecurrenciacapital"
                                                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                                        InitialValue="0" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                    </div>
                                                            </div>
                                                        </div>

                                                        <div class="module_subsec_elements"> 
	                                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                <asp:DropDownList ID="cmb_dia" runat="server" class="btn btn-primary2 dropdown_label" Visible="False">
                                                                    </asp:DropDownList>
                                                                    <div class="text_input_nice_labels">
                                                                        <asp:Label ID="lbl_dia" runat="server" CssClass="texto"
                                                                        Text="*Día de periodicidad:" Visible="false"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_dia" runat="server"
                                                                        ControlToValidate="cmb_dia" CssClass="textogris" Display="Dynamic"
                                                                        EnableClientScript="False" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                        ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                        
                                        </asp:Panel>

                                        <%-- Capital panel sobre capital en monto o porcentaje--%>
                                        <asp:Panel ID="updpnl_porcentaje" runat="server" ScrollBars="Auto">

                                            <div class= "module_subsec low_m columned three_columns">
                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:DropDownList ID="cmb_capporcentaje" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                                <asp:ListItem Value="MONTO">MONTO</asp:ListItem>
                                                                <asp:ListItem Value="PORCENTAJE">PORCENTAJE</asp:ListItem>
                                                            </asp:DropDownList>
                                                                <div class="text_input_nice_labels">  
                                                                    <asp:Label ID="lbl_capital" runat="server" CssClass="texto" Height="16px"
                                                                    Text="*Capital en:"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_capporcentaje"
                                                                runat="server" ControlToValidate="cmb_capporcentaje" CssClass="textogris"
                                                                Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                                </div>
                                                    </div>
                                                </div>

                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_capital" runat="server" CssClass="text_input_nice_input"
                                                                MaxLength="9" Visible="False"></asp:TextBox>
                                                            <div class="text_input_nice_labels"> 
                                                                    <asp:Label ID="lbl_cantidad" runat="server" CssClass="texto"></asp:Label>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_capital"
                                                                    runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                    TargetControlID="txt_capital" ValidChars=".">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cantidad" runat="server"
                                                                ControlToValidate="txt_capital" CssClass="textogris" Display="Dynamic"
                                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <p align="center">
                                            <asp:Button ID="btn_agregarcapital" runat="server" class="btn btn-primary" Text="Agregar" 
                                                        ToolTip="Agrega la(s) fecha(s) capturadas de Capital" ValidationGroup="val_planpagosespcapital" />
                                            &nbsp;
                                             <asp:Button ID="btn_eliminarplanpago" runat="server"  class="btn btn-primary" 
                                                        Text="Eliminar" ToolTip="Elimina todas las fechas capturadas de Capital" />
                                        </p>
                                           
                                        <div align="center">
                                            <asp:Label ID="lbl_suma" runat="server" CssClass="alerta" ValidationGroup="val_planpagosespcapital"></asp:Label>
                                        </div>

                                        <div class="overflow_x shadow" >
                                            <asp:DataGrid ID="dagcapital" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                                 GridLines="None" TabIndex="10">
                                                <Columns>
                                                    <asp:BoundColumn DataField="fechapago" HeaderText="Fecha Pago">
                                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="capital" HeaderText="Capital">
                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="table_header" />
                                            </asp:DataGrid>
                                        </div>

                                        <p align="center">
                                            <asp:Label ID="lbl_capitalstatus" runat="server" CssClass="alerta" ValidationGroup="val_planpagosespcapital"></asp:Label>
                                        </p>
                                        
                                    </asp:Panel>

                                </Content>
                            </ajaxToolkit:AccordionPane>

                                        <%---------------------------------
                                                PANEL ORDINARIOS
                                        --------------------------------%>

                            <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server" Enabled="false">
                                <Header>
                                    <span>TASA ORDINARIA</span> &nbsp&nbsp
                                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                                </Header>
                                <Content>
                                    <div class="module_subsec no_m">
                                        <h5 style="font-weight:normal"  class="resalte_azul">Capture las tasas ordinarias.</h5>
                                    </div>

                                    <asp:Panel ID="updpnl_tasas_ordinarias" runat="server" Visible="true" ScrollBars="Auto">
                                        
                                        <div class= "module_subsec low_m columned three_columns">
                                            <div class="module_subsec_elements"> 
	                                            <div class="text_input_nice_div module_subsec_elements_content">
                                                    <asp:DropDownList ID="cmb_tasanormal" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label" ValidationGroup="val_normal">
                                                    </asp:DropDownList>
                                                        <div class="text_input_nice_labels"> 
                                                            <asp:Label ID="lbl_tipo_tasa_normal" runat="server" CssClass="texto"
                                                            Width="150px">*Tipo Tasa Normal:</asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasanormal"
                                                        runat="server" ControlToValidate="cmb_tasanormal" CssClass="textogris"
                                                        Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                        ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                                        </div>
                                                    <div class="text_input_nice_labels"> 

                                                    </div>
                                                </div>
                                            </div>
                                                
                                                <div class="module_subsec_elements"> 
                                                    <asp:Panel ID="updpnl_tipo_tasas" runat="server">
                                                    <ContentTemplate>
	                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                            <asp:TextBox ID="txt_tasanormal" runat="server" CssClass="text_input_nice_input"
                                                                MaxLength="3" Visible="false"></asp:TextBox>
                                                                <div class="text_input_nice_labels">
                                                                    <asp:Label ID="lbl_tasanor" runat="server" CssClass="texto"></asp:Label>
                                                                    <asp:Label ID="lbl_indicenormal" runat="server" CssClass="texto" Visible="False"></asp:Label>
                                                                    <asp:Label ID="lbl_puntosnormal" runat="server" CssClass="texto"></asp:Label>

                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasanormal"
                                                                        runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasanormal" ValidChars=".">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasanormaln"
                                                                        runat="server" ControlToValidate="txt_tasanormal" CssClass="textogris"
                                                                        Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasanormal"
                                                                        runat="server" ControlToValidate="txt_tasanormal" CssClass="textogris"
                                                                        ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo"
                                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                                    <asp:Label ID="lbl_error_tasanormal" runat="server" CssClass="alerta"></asp:Label>
                                                                </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    </asp:Panel>
                                                </div>
                                        </div>

                                        <asp:Panel ID="uppnl_fechas_Tasas" runat="server" ScrollBars="Auto">
                                            <div class= "module_subsec low_m columned three_columns">
                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:Label ID="lbl_fechainiperiodonormal" runat="server" CssClass="text_input_nice_input"></asp:Label>
                                                            <div class="text_input_nice_labels"> 
                                                                <asp:Label ID="lbl_fechaininormal" runat="server" CssClass="texto"
                                                                Text="*Fecha inicio periodo:" Width="150px"></asp:Label>
                                                            </div>
                                                    </div>
                                                </div>

                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:TextBox ID="txt_fechafinnormal" runat="server" class="text_input_nice_input"
                                                        Enabled="False" MaxLength="10"></asp:TextBox>
                                                            <div class="text_input_nice_labels"> 
                                                                <asp:Label ID="lbl_fechafinnormal" runat="server" CssClass="texto"
                                                                Text="*Fecha fin periodo:" Width="150px"></asp:Label>
                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinnormal"
                                                                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinnormal">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinnormal"
                                                                runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_fechafinnormal">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinnormal"
                                                                runat="server" ControlToValidate="txt_fechafinnormal" CssClass="textogris"
                                                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinnormal"
                                                                runat="server" ControlExtender="MaskedEditExtender_fechafinnormal"
                                                                ControlToValidate="txt_fechafinnormal" CssClass="textogris" Display="Dynamic"
                                                                ErrorMessage="MaskedEditValidator_fechafinnormal"
                                                                InvalidValueMessage="Fecha inválida" ValidationGroup="val_normal"></ajaxToolkit:MaskedEditValidator>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <p align="center">
                                            <asp:Button ID="btn_agregartasanormal" runat="server" class="btn btn-primary" 
                                                Text="Agregar" ValidationGroup="val_normal" ToolTip="Agregar periodo de tasa ordinaria" />
                                            &nbsp;
                                            <asp:Button ID="btn_eliminarultimo" runat="server" class="btn btn-primary"
                                                Text="Eliminar" ToolTip="Eliminar último periodo de tasa ordinaria" />
                                        </p>

                                        <div align="center">
                                            <asp:Label ID="lbl_statusnormal" runat="server" CssClass="alerta"
                                                ValidationGroup="val_normal"></asp:Label>
                                        </div>

                                        <div class="overflow_x shadow" >
                                            <asp:DataGrid ID="dag_normal" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                                    GridLines="None" TabIndex ="17" Width="100%">
                                                <Columns>
                                                    <asp:BoundColumn DataField="fechai" HeaderText="Fecha inicio periodo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="fechaf" HeaderText="Fecha fin periodo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="clasificacion" HeaderText="Clasificación">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="tasa" HeaderText="Tasa">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="indice" HeaderText="Indice">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="puntos" HeaderText="Puntos(%)">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="table_header" />
                                            </asp:DataGrid>
                                        </div>

                                    </asp:Panel>
                                
                                </Content>
                            </ajaxToolkit:AccordionPane>

                                        <%---------------------------------
                                                PANEL MORATORIOS
                                        --------------------------------%>

                            <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server" Enabled="false">
                                <Header>
                                    <span>TASA MORATORIA</span>
                                    <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                                </Header>
                                    <Content>
                                        <div class="module_subsec no_m">
                                            <h5 style="font-weight:normal"  class="resalte_azul">Capture las tasas moratorias.</h5>
                                        </div>

                                        <asp:UpdatePanel ID="updpnl_tipo_tasa_mora" runat="server">
                                        <ContentTemplate>

                                            <div class= "module_subsec low_m columned three_columns">
                                                <div class="module_subsec_elements"> 
	                                                <div class="text_input_nice_div module_subsec_elements_content">
                                                        <asp:DropDownList ID="cmb_tasamora" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown_label" ValidationGroup="val_moratorio" >
                                                        </asp:DropDownList>
                                                        <div class="text_input_nice_labels">  
                                                            <asp:Label ID="lbl_tasamoratorio" runat="server" CssClass="texto"
                                                                Width="153px">*Tipo Tasa Moratorio:</asp:Label>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamora"
                                                                runat="server" ControlToValidate="cmb_tasamora" CssClass="textogris"
                                                                Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                                ValidationGroup="val_moratorio"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="module_subsec_elements">  
                                                    <asp:UpdatePanel ID="updpnl_moratorio" runat="server">
                                                    <ContentTemplate>
	                                                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                <asp:TextBox ID="txt_tasamora" runat="server" CssClass="text_input_nice_input"
                                                                    MaxLength="6" Visible="false"></asp:TextBox>
                                                                    <div class="text_input_nice_labels">  
                                                                        <asp:Label ID="lbl_tasamora" runat="server" CssClass="texto"></asp:Label>
                                                                        <asp:Label ID="lbl_indicemora" runat="server" CssClass="texto"></asp:Label>
                                                                        <asp:Label ID="lbl_puntosmora" runat="server" CssClass="texto"></asp:Label>

                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasamora" runat="server" Enabled="True" 
                                                                            FilterType="Custom, Numbers" TargetControlID="txt_tasamora" ValidChars=".">
                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamoram" runat="server" ControlToValidate="txt_tasamora" 
                                                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_moratorio">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamora" runat="server" 
                                                                            ControlToValidate="txt_tasamora" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" class="textorojo"
                                                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                                        <asp:Label ID="lbl_error_tasamora" runat="server" CssClass="alerta"></asp:Label>
                                                                    </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                        </div>

                                            <asp:UpdatePanel ID="updpnl_fechas_moratorio" runat="server">
                                                            <ContentTemplate>

                                                                <div class= "module_subsec low_m columned three_columns">
                                                                    <div class="module_subsec_elements"> 
	                                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                                            <asp:Label ID="lbl_fechainiperiodomoratorio" runat="server" CssClass="text_input_nice_input"></asp:Label>
                                                                                <div class="text_input_nice_labels">  
                                                                                    <asp:Label ID="lbl_inimoratorio" runat="server" CssClass="texto"
                                                                                    Text="*Fecha inicio periodo:" Width="150px"></asp:Label>
                                                                                </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="module_subsec_elements"> 
	                                                                    <div class="text_input_nice_div module_subsec_elements_content">
                                                                            <asp:TextBox ID="txt_fechafinmoratorio" runat="server" class="text_input_nice_input"
                                                                            MaxLength="10" ></asp:TextBox>
                                                                                <div class="text_input_nice_labels"> 
                                                                                    <asp:Label ID="lbl_finmoratorio" runat="server" CssClass="texto"
                                                                                    Text="*Fecha fin periodo:" Width="150px"></asp:Label>

                                                                                    <asp:Label ID="lbl_formato_fin_mora" runat="server" CssClass="texto"
                                                                                Text="(DD/MM/AAAA)"></asp:Label>
                                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinmoratorio"
                                                                                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinmoratorio">
                                                                            </ajaxToolkit:MaskedEditExtender>
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinmoratorio"
                                                                                runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                TargetControlID="txt_fechafinmoratorio">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinmoratorio"
                                                                                runat="server" ControlExtender="MaskedEditExtender_fechafinmoratorio"
                                                                                ControlToValidate="txt_fechafinmoratorio" CssClass="textogris"
                                                                                ErrorMessage="MaskedEditValidator_fechafinmoratorio" Display="Dynamic"
                                                                                InvalidValueMessage="Fecha inválida" ValidationGroup="val_moratorio"></ajaxToolkit:MaskedEditValidator>
                                                                                </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        
                                            <p align="center">
                                                <asp:Button ID="btn_agregarmora" runat="server"  class="btn btn-primary"
                                                    Text="Agregar" ValidationGroup="val_moratorio" ToolTip="Agregar periodo de tasa moratoria" />
                                                &nbsp;      
                                                <asp:Button ID="btn_eliminarultimomora" runat="server" class="btn btn-primary" 
                                                    Text="Eliminar" ToolTip="Eliminar último periodo de tasa moratoria" />
                                            </p>

                                            <div align="center">
                                                <asp:Label ID="lbl_statusmora" runat="server" CssClass="alerta"
                                                    ValidationGroup="val_moratorio"></asp:Label>
                                            </div>

                                            <!-- Tabla de Expedientes generados por sucursal -->
                                            <div class="overflow_x shadow" >
                                                <asp:DataGrid ID="dag_moratorio" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                                    <Columns>
                                                        <asp:BoundColumn DataField="fechai" HeaderText="Fecha inicio periodo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="fechaf" HeaderText="Fecha fin periodo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="clasificacion" HeaderText="Clasificación">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="tasa" HeaderText="Tasa">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="indice" HeaderText="Indice">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="puntos" HeaderText="Puntos(%)">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="table_header" />
                                                </asp:DataGrid>
                                            </div>

                                            <div align="center">
                                                <asp:Label runat="server" CssClass="alerta" ID="lbl_statusfinal"></asp:Label>
                                            </div>

                                            <p align="center">
                                                <asp:LinkButton ID="lnk_verplanespecial" runat="server" class="textogris"
                                                    Text="Generar plan"></asp:LinkButton>&nbsp;&nbsp;
                                            </p>
                                            
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    
                                    </Content>
                            </ajaxToolkit:AccordionPane>

                           </Panes>
                     </ajaxToolkit:Accordion>
                 </asp:Panel>
            </div>
        </section>
    
</asp:Content>