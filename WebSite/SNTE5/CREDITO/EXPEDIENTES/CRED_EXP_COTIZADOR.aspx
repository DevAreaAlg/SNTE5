<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_COTIZADOR.aspx.vb" Inherits="SNTE5.CRED_EXP_COTIZADOR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <section class="panel" >
        <header class="panel-heading">
            <span>Cotizador</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec columned low_m align_items_flex_center" >
                    <label id="lbl_Producto0" class=" module_subsec_elements module_subsec_medium-elements title_tag" >*Producto: </label>
                <div class="module_subsec_elements module_subsec_big-elements">
                    <asp:DropDownList ID="cmb_Producto" runat="server" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" AutoPostBack="True" style="max-width:299px;">
                            </asp:DropDownList>
                </div>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_lmonto">*Monto: </asp:Label>
                <asp:TextBox ID="txt_monto" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" style="max-width:299px;"
                        MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox> &nbsp;
                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_rango"></asp:Label>
            </div>  

            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_plazo">*Plazo: </asp:Label>
                <asp:TextBox ID="txt_plazo" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input"
                        MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox> &nbsp;
                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_rangoPlazo"></asp:Label>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label ID="lbl_fechaliberacion" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">*Fecha pago: </asp:Label>
                <asp:TextBox ID="txt_fechaliberacion" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input"
                        MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox>
                <div class="text_input_nice_labels">
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
                        ErrorMessage="MaskedEditValidator_fechaliberacion"
                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechaliberacion" runat="server"
                        ControlToValidate="txt_fechaliberacion" CssClass="textogris" Display="Dynamic"
                        ErrorMessage=" Falta Dato!" ValidationGroup="val_fecha"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label runat="server" Text="*Elija el tipo de plan:" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_tipoplan"  ></asp:Label>
                <asp:DropDownList runat="server" ID="cmb_tipoplan" AutoPostBack="true"
                        class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnk_eliminar" runat="server" class="textogris" Enabled="False" Visible="false" Text="Eliminar cotización"></asp:LinkButton>
            </div>

            <div align="center">
                <asp:Label ID="lbl_status_general" runat="server" CssClass="alerta"></asp:Label>
            </div>

                 
            <%--  SALDOS INSOLUTOS--%>
            <asp:UpdatePanel ID="upd_pnl_si" runat="server" Visible="false" >
                <ContentTemplate>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tipotasasi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">*Tipo tasa ordinaria:</asp:Label>
                        <asp:DropDownList ID="cmb_tipotasasi" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                                    ValidationGroup="val_planpagosi" AutoPostBack="True"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipotasasi" runat="server"
                                    ControlToValidate="cmb_tipotasasi" CssClass="textogris" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <div class="module_subsec_elements module_subsec_medium-elements">
                            <asp:Label ID="lbl_ast1" runat="server" CssClass="module_subsec_elements title_tag" Text="*" Visible="true"></asp:Label>
                            <asp:Label ID="lbl_indicenorsi" runat="server" CssClass="texto" Visible="False" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lbl_tasasi" runat="server" CssClass="texto" Text="(Desde - % hasta - %)" Visible="true"></asp:Label>
                        </div>
                        <asp:TextBox ID="txt_tasasi" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="6"
                                Width="37px" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasai"
                                runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasasi" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:Label ID="lbl_porcentaje" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=" %(Anual)"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasasi" runat="server"
                                ControlToValidate="txt_tasasi" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tipotasamorsi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">*Tipo tasa moratoria:</asp:Label>
                        <asp:DropDownList ID="cmb_tipotasamorsi" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                                AutoPostBack="True" ValidationGroup="val_planpagosi"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipotasamorsi"
                                    runat="server" ControlToValidate="cmb_tipotasamorsi" CssClass="textogris"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                    ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <div class="module_subsec_elements module_subsec_medium-elements">
                            <asp:Label ID="lbl_ast" runat="server" CssClass="texto" Text="*" Visible="true"></asp:Label>
                            <asp:Label ID="lbl_indicemorsi" runat="server" CssClass="texto" Visible="False" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lbl_tasamorsi" runat="server" CssClass="texto" Text="(Desde - % hasta - %)" Visible="true"></asp:Label>
                        </div>
                            <asp:TextBox ID="txt_tasamorsi" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input"
                                MaxLength="6" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txt_tasamorsi_FilteredTextBoxExtender"
                                runat="server" Enabled="True" FilterType="Custom, Numbers"
                                TargetControlID="txt_tasamorsi" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:Label ID="lbl_porcentajemor" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text=" %(Mensual)"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamorsi"
                                runat="server" ControlToValidate="txt_tasamorsi" CssClass="textogris"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tipopersi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Tipo periodicidad: "></asp:Label>
                        <asp:DropDownList ID="cmb_tipopersi" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                            AutoPostBack="True" ValidationGroup="val_planpagosi"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipopersi" runat="server"
                                ControlToValidate="cmb_tipopersi" CssClass="textogris"  Display="Dynamic" ErrorMessage=" Falta Dato!"
                                InitialValue="0" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_persi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Periodicidad:"></asp:Label>
                        <asp:DropDownList ID="cmb_periodicidadSI" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                            ValidationGroup="val_planpagosi"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Req_periodicidadSI" runat="server" ControlToValidate="cmb_periodicidadSI" CssClass="textogris" Display="Dynamic"
                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosi"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnk_generar1" runat="server" class="textogris"
                            Text="Agregar Pago" ValidationGroup="val_planpagosi" Visible="False"></asp:LinkButton>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_periodos_SI" runat="server" CssClass="texto" Text="*Días de pago en el mes: " Visible="False"></asp:Label>
                        <asp:Label ID="lbl_diaspago_SI" runat="server" CssClass="texto" Visible="False"></asp:Label>
                    </div>

                    <div align="center">
                        <asp:LinkButton ID="lnk_generar_si0" runat="server" class="textogris" Text="Generar Plan" ValidationGroup="val_planpagosi"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lbl_status" runat="server" CssCLass="alerta"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_errortasasi" runat="server" Font-Bold="True" CssClass="alerta"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasasi" runat="server" ControlToValidate="txt_tasasi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Label ID="lbl_errortasamorsi" runat="server" CssClass="alerta"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamorsi" runat="server" ControlToValidate="txt_tasamorsi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        <br />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <%--  PAGOS FIJOS SALDOS INSOLUTOS--%>
            <asp:UpdatePanel ID="upd_pnl_pfsi" runat="server" Visible="false">
                <ContentTemplate>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tasanorpfsi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                        <asp:TextBox ID="txt_tasanorpfsi" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="6" ></asp:TextBox>
                        <asp:Label ID="lbl_porcentajefijo" runat="server" CssClass="texto" Text=" %(Anual)"></asp:Label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tasanorpfsi" CssClass="textogris" ControlToValidate="txt_tasanorpfsi"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasanorpfsi" runat="server"
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasanorpfsi" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tasamorpfsi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                        <asp:TextBox ID="txt_tasamorpfsi" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="6"></asp:TextBox>
                        <asp:Label ID="lbl_porcentajepfsi" runat="server" CssClass="texto" Text="%(Mensual)"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tasamorpfsi" CssClass="textogris" ControlToValidate="txt_tasamorpfsi"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasamorpfsi" runat="server" 
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasamorpfsi" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_tipoperiodicidad" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Tipo periodicidad: "></asp:Label>
                        <asp:DropDownList ID="cmb_tipoperiodicidad" runat="server" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                            AutoPostBack="True" ValidationGroup="val_planpago"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Req_tipoperiodicidad" runat="server"
                            ControlToValidate="cmb_tipoperiodicidad" CssClass="textogris" Display="Dynamic"
                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpago"></asp:RequiredFieldValidator>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_periodicidad" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Periodicidad:"></asp:Label>
                            <asp:DropDownList ID="cmb_periodicidad" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                            ValidationGroup="val_planpago"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Req_periodicidad" runat="server" ControlToValidate="cmb_periodicidad" CssClass="textogris" Display="Dynamic"
                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpago"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnk_generar0" runat="server" class="textogris"
                            Text="Agregar Pago" ValidationGroup="val_planpago" Visible="False"></asp:LinkButton>
                    </div>

                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label ID="lbl_periodos" runat="server" CssClass="texto" Text="*Días de pago en el mes: " Visible="False"></asp:Label>
                            <asp:Label ID="lbl_diaspago" runat="server" CssClass="texto" Visible="False"></asp:Label>
                    </div>                          

                    <div align="center">
                        <asp:LinkButton ID="lnk_generar" runat="server" class="textogris" Text="Generar Plan" ValidationGroup="val_planpago"></asp:LinkButton>
                        <br />
                            <asp:Label ID="lbl_status_pfsi" runat="server" CssClass="alerta"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_errortasanorpfsi" runat="server" CssCLass="alerta"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasanorpfsi" runat="server" ControlToValidate="txt_tasanorpfsi" CssClass="textogris" ErrorMessage=" Error:Tasa incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Label ID="lbl_errortasamorpfsi" runat="server" CssClass="alerta"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamorpfsi" runat="server" ControlToValidate="txt_tasamorpfsi" CssClass="textogris" ErrorMessage=" Error:Tasa incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                        <br />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <%--  SIMPLE REVOLVENTE--%>
            <asp:Panel ID="pnl_srev" runat="server" Visible="False" Height="152px">
                <table>
                                                     
                    <asp:Panel  runat="server" Visible="False" ID="pnl_disp_srev">
                        <tr>
                            <td width="330px">
                                <label id="Label10" class="texto"> *Monto de Disposición:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_monto_disp_srev" runat="server" CssClass="textocajas" MaxLength="9" ValidationGroup="val_planpagoSrev" Width="72px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_monto_disp_srev" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagoSrev"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_monto_disp_srev" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_monto_disp_srev" CssClass="textogris" ErrorMessage=" Error:Monto incorrecto" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </asp:Panel>

                    <asp:Panel runat="server" Visible="False" ID="pnl_Carta_credito">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_rango_cptl_cc" runat="server" CssClass="texto" Text="*Rango de capital:"></asp:Label>                                                    
                            </td>
                            <td>
                                <asp:TextBox ID="txt_rango_cptl_cc" runat="server" class="textocajas" MaxLength="9"
                                    ValidationGroup="val_planpagosi" Width="72px"></asp:TextBox>
                                &nbsp;
                            <asp:Label ID="lbl_rango_cc" runat="server" CssClass="texto"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txt_rango_cptl_cc" CssClass="textogris" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagoSrev"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                                    runat="server" Enabled="True" FilterType="Custom, Numbers"
                                    TargetControlID="txt_rango_cptl_cc" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                    runat="server" class="textogris" ControlToValidate="txt_rango_cptl_cc"
                                    ErrorMessage="Monto Incorrecto!"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </asp:Panel>
                                
                        <tr>
                            <td width="250px">
                                <label id="Label12" class="texto"  > *Tipo tasa:</label>
                            </td>
                            <td>
                            <%--<asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" Checked="True" CssClass="texto" GroupName="plazo" Text="Días" />--%>
                                <asp:DropDownList ID="cmb_tipo_tasa_srev" runat="server" CssClass="textocajas" AutoPostBack = "true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    <asp:Panel runat="server" Visible="False" ID="pnl_tasas_fija">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_tasa_ord_srev" runat="server" CssClass="texto" 
                                    Enabled="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_tasa_ord_srev" runat="server" CssClass="textocajas" 
                                    MaxLength="6" Width="40px" Enabled="False"></asp:TextBox>
                                <label id="Label6" class="texto">
                                    %(Anual)</label>
                                <asp:RequiredFieldValidator ID="rfv_tasa_ord_srev" runat="server" ControlToValidate="txt_tasa_ord_srev" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagoSrev" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="fte_tasa_ord_srev" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_tasa_ord_srev" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:Label ID="lbl_error_tasa_srev" runat="server" CssClass="alerta"></asp:Label>
                                <asp:RegularExpressionValidator ID="rev_tasa_ord_srev" runat="server" 
                                    ControlToValidate="txt_tasa_ord_srev" CssClass="textogris" 
                                    ErrorMessage=" Error:Tasa Incorrecta" 
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_tasa_mor_srev" runat="server" CssClass="texto"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_tasa_mor_srev" runat="server" CssClass="textocajas" 
                                    MaxLength="6" Width="40px" Enabled="False"></asp:TextBox>
                                <label id="Label9" class="texto">
                                    %(Mensual)</label>
                                <asp:RequiredFieldValidator ID="rfv_tasa_mor_srev" runat="server" ControlToValidate="txt_tasa_mor_srev" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagoSrev" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="fte_tasa_mor_srev" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_tasa_mor_srev" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:Label ID="lbl_error_tasa_srev0" runat="server" CssClass="alerta"></asp:Label>
                                <asp:RegularExpressionValidator ID="rev_tasa_mor_srev" runat="server" 
                                    ControlToValidate="txt_tasa_mor_srev" CssClass="textogris" 
                                    ErrorMessage=" Error:Tasa Incorrecta" 
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                            <asp:Label ID="lbl_error_pts_srev" runat="server" CssClass="alerta"></asp:Label>
                        </tr>
                    </asp:Panel>

                    <asp:Panel ID="pnl_tasas_indizadas" runat="server">
                        <tr>
                            <td>
                                <label id="Label13" class="texto">*Tasa Indizada Normal:</label>
                            </td>
                            <td>
                                    <asp:Label ID="lbl_indice_tasa_nor_srev" runat="server" CssClass="texto"></asp:Label>
                                <asp:Label ID="lbl_tasa_ind_srev" runat="server" CssClass="texto"></asp:Label>
                                <asp:TextBox ID="txt_pts_srev" runat="server" CssClass="textocajas" 
                                    MaxLength="6" Width="40px" Enabled="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_pts_srev" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagoSrev"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_pts_srev" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:Label ID="lbl_error_mora_srev1" runat="server" CssClass="alerta"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                    ControlToValidate="txt_pts_srev" CssClass="textogris" 
                                    ErrorMessage=" Error:Tasa Incorrecta" 
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lbl_ind_mora_srev" class="texto">*Tasa Indizada Moratoria:</label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_indice_tasa_mora_srev" runat="server" CssClass="texto" ></asp:Label>
                                <asp:Label ID="lbl_tasa_ind_mora_srev" runat="server" CssClass="texto"></asp:Label>
                                  
                                <asp:TextBox ID="txt_tasa_ind_mora_rev" runat="server" CssClass="textocajas" 
                                    MaxLength="6" Width="40px" Enabled="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_tasa_ind_mora_rev" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagoSrev"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_tasa_ind_mora_rev" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:Label ID="lbl_error_puntos_srev2" runat="server" CssClass="alerta"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                    ControlToValidate="txt_pts_srev" CssClass="textogris" 
                                    ErrorMessage=" Error:Tasa Incorrecta" 
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </asp:Panel>
                        <tr>
                            <td>
                                <label id="lbl_per_srev" class="texto">*Fecha de Corte:</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmb_per_srev" runat="server" CssClass="textocajas">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_per_srev" runat="server" ControlToValidate="cmb_per" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_planpagoSrev"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_dias_pago_srev" runat="server" CssClass="texto" Text="Días de pago en el mes:" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_pagomes_srev" runat="server" CssClass="texto" Visible="False"></asp:Label>
                                             
                            </td>
                        </tr>
                </table>
                <br />

                <div align="center">
                    <asp:LinkButton ID="lnk_generar_plan_pagos_srev" runat="server" 
                        CssClass="textogris" Text="Generar Plan" ValidationGroup="val_planpagoSrev" 
                        Visible="true"></asp:LinkButton>
                    <br />
                    <asp:Label ID="lbl_status_srev" runat="server" CssClass="alerta"></asp:Label>
                    gaga</div>
                      
                <br />
                      
            </asp:Panel>


            <asp:Panel ID="pnl_arfin" runat="server" Visible="False">
            <table>
                <tr>
                    <td width="330px">
                        <label id="lbl_divisa" class="texto">*Divisa:</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmb_divisa" runat="server" CssClass="textocajas">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_divisa" runat="server" ControlToValidate="cmb_divisa" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_cot" />
                    </td>
                </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lbl_tasa_ord" runat="server" CssClass="texto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_tasa_ord" runat="server" CssClass="textocajas" MaxLength="6" Width="40px"></asp:TextBox>
                        <label id="lbl_porcentaje_ord" class="texto">
                            %(Anual)</label>
                        <asp:RequiredFieldValidator ID="rfv_tasa_ord" runat="server" ControlToValidate="txt_tasa_ord" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cot" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fte_tasa_ord" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_tasa_ord" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="rev_tasa_ord" runat="server" ControlToValidate="txt_tasa_ord" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_tasa_mor" runat="server" CssClass="texto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_tasa_mor" runat="server" CssClass="textocajas" MaxLength="6" Width="40px"></asp:TextBox>
                        <label id="lbl_porcentaje_mor" class="texto">
                            %(Mensual)</label>
                        <asp:RequiredFieldValidator ID="rfv_tasa_mor" runat="server" ControlToValidate="txt_tasa_mor" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cot" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fte_tasa_mor" runat="server" FilterType="Custom, Numbers" TargetControlID="txt_tasa_mor" ValidChars=".">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="rev_tasa_mor" runat="server" ControlToValidate="txt_tasa_mor" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lbl_tipo_per" class="texto">
                            *Tipo periodicidad:</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmb_tipo_per" runat="server" AutoPostBack="True" CssClass="textocajas">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_tipo_per" runat="server" ControlToValidate="cmb_tipo_per" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_cot"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lbl_per" class="texto">
                            *Periodicidad:</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmb_per" runat="server" CssClass="textocajas">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_per" runat="server" ControlToValidate="cmb_per" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_cot"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <br />
            <div align="center">
                <asp:LinkButton ID="lnk_generar_plan_pagos" runat="server" CssClass="textogris" 
                    Text="Generar Plan" ValidationGroup="val_planpago"></asp:LinkButton>
                <br />
                <asp:Label ID="Label2" runat="server" CssClass="alerta"></asp:Label>
            </div>
        </asp:Panel>

            <%-- PLAN ESPECIAL--%>
            <asp:UpdatePanel ID="upd_especial" runat="server" Visible="false" >
                <ContentTemplate>

                    <asp:Label ID="lbl_max_fecha" runat="server" CssClass="textoazul" Font-Bold="True" Font-Size="8pt"></asp:Label> 
                    <br />
                    <asp:Label ID="lbl_fechamax" runat="server" CssClass="textoazul" Font-Bold="True" Font-Size="8pt"></asp:Label>
                                 
                <ajaxToolkit:Accordion ID="Accordion1" runat="server" AutoSize="none" ContentCssClass="accordionContent" Enabled="false" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" RequireOpenedPane="false" SelectedIndex="-1" SuppressHeaderPostbacks="true" TransitionDuration="250">
                    <panes>
                        <%-- INTERESES--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server" Enabled="true">
                            <header>
                                <asp:Label ID="lbl_cred" runat="server" Text="PAGO DE INTERÉS"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <asp:Label ID="lbl_pagointeres" runat="server" CssClass="textoazul" Font-Size="9pt" Text="Capture las fechas en que se pagarán los intereses."></asp:Label>
                                <br />

                                <asp:UpdatePanel ID="updpnl_interes_general" runat="server">
                                    <ContentTemplate>

                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                        <asp:Label ID="Label3" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag">*Forma de pago:</asp:Label>
                                        <asp:DropDownList ID="cmb_pagoint" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="UNOXUNO">UNO A LA VEZ</asp:ListItem>
                                            <asp:ListItem Value="RECURRENTE">RECURRENTE</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagoint" runat="server" ControlToValidate="cmb_pagoint" CssClass="textogris" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>       
                                    </div>
                                
                                    <asp:UpdatePanel ID="updpnl_interes" runat="server">
                                        <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_fechainicialperiodointeres" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha inicio periodo:" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_fechainicialperiodointeres" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="10" Visible="false"></asp:TextBox>
                                                    <asp:Label ID="lbl_formato_fini_interes" runat="server" CssClass="texto" Height="16px" Text=" (DD/MM/AAAA)" Visible="false"></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechainicialperiodointeres" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechainicialperiodointeres">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechainicialperiodointeres" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechainicialperiodointeres">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechainicialperiodointeres" runat="server" ControlToValidate="txt_fechainicialperiodointeres" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechainicialperiodointeres" runat="server" ControlExtender="MaskedEditExtender_fechainicialperiodointeres" ControlToValidate="txt_fechainicialperiodointeres" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechainicialperiodointeres" InvalidValueMessage="Fecha inválida">
                                                    </ajaxToolkit:MaskedEditValidator>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_fechafinalinteres" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha fin periodo: " Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_fechafinalinteres" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="10" Visible="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinalinteres" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinalinteres">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinalinteres" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechafinalinteres">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:Label ID="lbl_formato_ffinal_int" runat="server" CssClass="texto" Height="16px" Text=" (DD/MM/AAAA)" Visible="false" Width="98px"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinalinteres" runat="server" ControlToValidate="txt_fechafinalinteres" CssClass="textogris" Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinalinteres" runat="server" ControlExtender="MaskedEditExtender_fechafinalinteres" ControlToValidate="txt_fechafinalinteres" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechafinalinteres" InvalidValueMessage="Fecha inválida" ValidationGroup="val_planpagosespinteres">
                                                    </ajaxToolkit:MaskedEditValidator>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_pago_rec_interes" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Pago recurrente:" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="cmb_tiporecurrencia" runat="server" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label"
                                                        AutoPostBack="True" Visible="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrencia" runat="server" ControlToValidate="cmb_tiporecurrencia" 
                                                        CssClass="textogris" Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;
                                                <asp:DropDownList ID="cmb_tiporecurrenciainteres" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" Visible="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrenciainteres" runat="server" ControlToValidate="cmb_tiporecurrenciainteres" CssClass="textogris" Display="Dynamic"
                                                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespinteres"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_diaper_interes" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Día de periodicidad:" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="cmb_diainteres" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" Visible="False">
                                                    </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_diasemanainteres" runat="server" ControlToValidate="cmb_diainteres" CssClass="textogris" 
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespinteres" Visible="False"></asp:RequiredFieldValidator>
                                            </div>
    
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="updpnl_guardar_interes" runat="server">
                                        <ContentTemplate>
                                                <p align="center">
                                                    <asp:Button ID="btn_agregarinteres" runat="server" class="btn btn-primary"  Text="Agregar" ToolTip="Agrega la(s) fecha(s) capturadas de Interes" ValidationGroup="val_planpagosespinteres" />
                                                    &nbsp;
                                                    <asp:Button ID="Btn_eliminarinteres" runat="server" class="btn btn-primary" Text="Eliminar" ToolTip="Elimina todas las fechas capturadas de Interes" />
                                                </p>

                                                <p align="center">
                                                    <asp:Label ID="lbl_statusinteres" runat="server" CssClass="alerta" ValidationGroup="val_planpagosespinteres"></asp:Label>
                                                </p>

                                            <div class="overflow_x" >  
                                                    <asp:DataGrid ID="dag_pagointeres" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="40%">
                                                        <Columns>
                                                            <asp:BoundColumn DataField="fechapago" HeaderText="Fecha Pago">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="table_header" />
                                                    </asp:DataGrid>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </content>
                        </ajaxToolkit:AccordionPane>

                        <%-- CAPITAL--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server" Enabled="false">
                            <header>
                                <asp:Label ID="lbl_titcapital" runat="server" Text="PAGO DE CAPITAL"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <asp:Label ID="lbl_mensaje" runat="server" CssClass="textoazul" Font-Size="9pt">Capture como desea hacer los pagos de capital.</asp:Label>
                                <asp:Label ID="lbl_error" runat="server" CssClass="alerta"></asp:Label>
                                <br />
                                <br />
                                <asp:UpdatePanel ID="updpnl_capital_general" runat="server">
                                    <ContentTemplate>

                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                        <asp:Label ID="lbl_elegir" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag">*Forma de pago:</asp:Label>
                                        <asp:DropDownList ID="cmb_pagocapital" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="UNOXUNO">UNO A LA VEZ</asp:ListItem>
                                            <asp:ListItem Value="RECURRENTE">RECURRENTE</asp:ListItem>
                                            <asp:ListItem Value="COPIA">COPIAR FECHAS INTERES</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagocapital" runat="server" ControlToValidate="cmb_pagocapital" CssClass="textogris" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                    </div>
                                             
                                    <asp:UpdatePanel ID="updpnl_capital" runat="server" Visible="false">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_fechainiperiodo" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha inicio periodo: " Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_fechainiperiodo" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="10" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lbl_formato_fecha_ini" runat="server" CssClass="texto" Text=" (DD/MM/AAAA)" Visible="false"></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechainiperiodo" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechainiperiodo"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechainiperiodo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechainiperiodo"></ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechainiperiodo" runat="server" ControlToValidate="txt_fechainiperiodo" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechainiperiodo" runat="server" ControlExtender="MaskedEditExtender_fechainiperiodo" ControlToValidate="txt_fechainiperiodo" CssClass="textogris" 
                                                        ErrorMessage="MaskedEditValidator_fechainiperiodo" InvalidValueMessage="Fecha inválida" ValidationGroup="val_planpagosespcapital"></ajaxToolkit:MaskedEditValidator>
                                            </div>
                                                     
                                                <asp:UpdatePanel ID="updpnl_periodo" runat="server" Visible="false">
                                                    <ContentTemplate>

                                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                                        <asp:Label ID="lbl_fechafinperiodo" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha fin periodo: " Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txt_fechafinperiodo" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="10" Visible="false"></asp:TextBox>
                                                            <asp:Label ID="lbl_formato_fecha_fin" runat="server" CssClass="texto" Text=" (DD/MM/AAAA)" Visible="false"></asp:Label>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinperiodo" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinperiodo">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinperiodo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechafinperiodo">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinperiodo" runat="server" ControlToValidate="txt_fechafinperiodo" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinperiodo" runat="server" ControlExtender="MaskedEditExtender_fechafinperiodo" ControlToValidate="txt_fechafinperiodo" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechafinperiodo" InvalidValueMessage="Fecha inválida" ValidationGroup="val_planpagosespcapital">
                                                            </ajaxToolkit:MaskedEditValidator>
                                                    </div>

                                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                                        <asp:Label ID="lbl_pagorecu" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Pago Recurrente:" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="cmb_pagorecu" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagorecu" runat="server" ControlToValidate="cmb_pagorecu" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:DropDownList ID="cmb_tiporecurrenciacapital" runat="server" AutoPostBack="true" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" Visible="false"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporecurrenciacapital" runat="server" ControlToValidate="cmb_tiporecurrenciacapital" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                                        <asp:Label ID="lbl_dia" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Día de periodicidad:" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="cmb_dia" runat="server" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" Visible="False"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_dia" runat="server" ControlToValidate="cmb_dia" CssClass="textogris" Display="Dynamic" EnableClientScript="False" ErrorMessage=" Falta Dato!" InitialValue="0" 
                                                                ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                                    </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:updatePanel ID="updpnl_porcentaje" runat="server">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_capital" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Capital en:"></asp:Label>
                                                <asp:DropDownList ID="cmb_capporcentaje" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label">
                                                    <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                    <asp:ListItem Value="MONTO">MONTO</asp:ListItem>
                                                    <asp:ListItem Value="PORCENTAJE">PORCENTAJE</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_capporcentaje" runat="server" ControlToValidate="cmb_capporcentaje" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                                    InitialValue="0" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_cantidad" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag"></asp:Label>
                                                <asp:TextBox ID="txt_capital" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="9" Visible="False"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_capital" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_capital" ValidChars=".">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cantidad" runat="server" ControlToValidate="txt_capital" 
                                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_planpagosespcapital"></asp:RequiredFieldValidator>
                                            </div>

                                            </ContentTemplate>
                                        </asp:updatePanel>

                                        <p align="center">
                                            <asp:Button ID="btn_agregarcapital" runat="server"  class="btn btn-primary" Text="Agregar" ToolTip="Agrega la(s) fecha(s) capturadas de Capital" ValidationGroup="val_planpagosespcapital" />
                                            &nbsp;
                                            <asp:Button ID="btn_eliminarplanpago" runat="server" class="btn btn-primary" Text="Eliminar" ToolTip="Elimina todas las fechas capturadas de Capital" />
                                        </p>
                                             
                                        <p align="center">
                                            <asp:Label ID="lbl_suma" runat="server" CssClass="alerta" ValidationGroup="val_planpagosespcapital"></asp:Label>
                                        </p>

                                        <div class="overflow_x" > 
                                        <asp:DataGrid ID="dagcapital" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="40%">
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </content>
                        </ajaxToolkit:AccordionPane>

                        <%--TASA ORDINARIA--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server" Enabled="false">
                            <header>
                                <asp:Label ID="lbl_titordinaria" runat="server" Text="TASA ORDINARIA"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <asp:Label ID="lbl_pnlnormal" runat="server" CssClass="textoazul" Font-Size="9pt">Capture las tasas ordinarias.</asp:Label>
                                <br />
                                <asp:UpdatePanel ID="updpnl_tasas_ordinarias" runat="server" Visible="true">
                                    <ContentTemplate>

                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                        <asp:Label ID="lbl_tipo_tasa_normal" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Width="150px">*Tipo Tasa Normal:</asp:Label>
                                        <asp:DropDownList ID="cmb_tasanormal" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" ValidationGroup="val_normal"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasanormal" runat="server" ControlToValidate="cmb_tasanormal" CssClass="textogris" Display="Dynamic" 
                                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                    </div>
                                            
                                        <asp:updatePanel ID="updpnl_tipo_tasas" runat="server" Visible="false">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <div class="module_subsec_elements module_subsec_medium-elements">
                                                    <asp:Label ID="lbl_tasanor" runat="server" CssClass="texto"></asp:Label>
                                                    <asp:Label ID="lbl_indicenormal" runat="server" CssClass="texto" Visible="False"></asp:Label>
                                                    <asp:Label ID="lbl_puntosnormal" runat="server" CssClass="texto"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txt_tasanormal" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="6" Visible="false"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasanormal" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasanormal" ValidChars=".">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasanormaln" runat="server" ControlToValidate="txt_tasanormal" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasanormal" runat="server" ControlToValidate="txt_tasanormal" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                    <asp:Label ID="lbl_error_tasanormal" runat="server" CssClass="alerta"></asp:Label>
                                            </div>

                                            </ContentTemplate>
                                        </asp:updatePanel>

                                        <asp:UpdatePanel ID="uppnl_fechas_Tasas" runat="server">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_fechaininormal" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha inicio periodo:"></asp:Label>
                                                <asp:Label ID="lbl_fechainiperiodonormal" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" Enabled="false"></asp:Label>
                                                <asp:Label ID="lbl_formato_tasa_ini" runat="server" CssClass="texto" Text="(DD/MM/AAAA)"></asp:Label>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_fechafinnormal" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha fin periodo:"></asp:Label>
                                                <asp:TextBox ID="txt_fechafinnormal" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" Enabled="False" MaxLength="10"></asp:TextBox>
                                                    <asp:Label ID="lbl_formato_tasa_fin" runat="server" CssClass="texto" Text="(DD/MM/AAAA)"></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinnormal" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinnormal">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinnormal" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechafinnormal">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechafinnormal" runat="server" ControlToValidate="txt_fechafinnormal" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_normal"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinnormal" runat="server" ControlExtender="MaskedEditExtender_fechafinnormal" ControlToValidate="txt_fechafinnormal" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechafinnormal" InvalidValueMessage="Fecha inválida" ValidationGroup="val_normal">
                                                    </ajaxToolkit:MaskedEditValidator>
                                            </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <p align="center">
                                            <asp:Button ID="btn_agregartasanormal" runat="server" class="btn btn-primary" Text="Agregar" ToolTip="Agregar periodo de tasa ordinaria" ValidationGroup="val_normal" />
                                            &nbsp;
                                            <asp:Button ID="btn_eliminarultimo" runat="server" class="btn btn-primary" Text="Eliminar" ToolTip="Eliminar último periodo de tasa ordinaria" />
                                        </p>

                                        <p align="center">
                                            <asp:Label ID="lbl_statusnormal" runat="server" CssClass="alerta" ValidationGroup="val_normal"></asp:Label>
                                        </p>

                                        <div class="overflow_x" >
                                            <asp:DataGrid ID="dag_normal" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </content>
                        </ajaxToolkit:AccordionPane>

                        <%--TASA MORATORIA--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server" Enabled="false">
                            <header>
                                <asp:Label ID="lbl_titmoratoria" runat="server" Text="TASA MORATORIA"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <asp:Label ID="lbl_moratorio" runat="server" CssClass="textoazul" Font-Size="9pt" Text="Capture las tasas moratorias."></asp:Label>
                                <br />
                                <asp:UpdatePanel ID="updpnl_tipo_tasa_mora" runat="server">
                                    <ContentTemplate>

                                    <div class="module_subsec columned low_m align_items_flex_center" >
                                        <asp:Label ID="lbl_tasamoratorio" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">*Tipo Tasa moratorio:</asp:Label>
                                        <asp:DropDownList ID="cmb_tasamora" runat="server" AutoPostBack="True" class="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" ValidationGroup="val_moratorio"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamora" runat="server" ControlToValidate="cmb_tasamora" CssClass="textogris" Display="Dynamic" 
                                            ErrorMessage=" Falta Dato!" InitialValue="0" ValidationGroup="val_moratorio"></asp:RequiredFieldValidator>
                                    </div>
                                            
                                        <asp:UpdatePanel ID="updpnl_moratorio" runat="server">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <div class="module_subsec_elements module_subsec_medium-elements">
                                                    <asp:Label ID="lbl_tasamora" runat="server" CssClass="texto"></asp:Label>
                                                    <asp:Label ID="lbl_indicemora" runat="server" CssClass="texto"></asp:Label>
                                                    <asp:Label ID="lbl_puntosmora" runat="server" CssClass="texto"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txt_tasamora" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="6" Visible="false"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasamora" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasamora" ValidChars=".">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasamoram" runat="server" ControlToValidate="txt_tasamora" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_moratorio"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamora" runat="server" ControlToValidate="txt_tasamora" CssClass="textogris" 
                                                        ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                    <asp:Label ID="lbl_error_tasamora" runat="server" CssClass="alerta"></asp:Label>
                                            </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="updpnl_fechas_moratorio" runat="server">
                                            <ContentTemplate>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_inimoratorio" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha inicio periodo:"></asp:Label>
                                                <asp:Label ID="lbl_fechainiperiodomoratorio" runat="server" CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input"></asp:Label>
                                                <asp:Label ID="lbl_formato_ini_mor" runat="server" CssClass="texto" Text="(DD/MM/AAAA)"></asp:Label>
                                            </div>

                                            <div class="module_subsec columned low_m align_items_flex_center" >
                                                <asp:Label ID="lbl_finmoratorio" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="*Fecha fin periodo:"></asp:Label>
                                                <asp:TextBox ID="txt_fechafinmoratorio" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" MaxLength="10"></asp:TextBox>
                                                    <asp:Label ID="lbl_formato_fin_mora" runat="server" CssClass="texto" Text="(DD/MM/AAAA)"></asp:Label>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafinmoratorio" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafinmoratorio"></ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafinmoratorio" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechafinmoratorio"></ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafinmoratorio" runat="server" ControlExtender="MaskedEditExtender_fechafinmoratorio" ControlToValidate="txt_fechafinmoratorio" CssClass="textogris" 
                                                        ErrorMessage="MaskedEditValidator_fechafinmoratorio" InvalidValueMessage="Fecha inválida" ValidationGroup="val_moratorio"></ajaxToolkit:MaskedEditValidator>
                                            </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <p align="center">
                                            <asp:Button ID="btn_agregarmora" runat="server" class="btn btn-primary" Text="Agregar" ToolTip="Agregar periodo de tasa moratoria" ValidationGroup="val_moratorio" />
                                            &nbsp;
                                            <asp:Button ID="btn_eliminarultimomora" runat="server" class="btn btn-primary" Text="Eliminar" ToolTip="Eliminar último periodo de tasa moratoria" />
                                        </p>

                                        <p align="center">
                                            <asp:Label ID="lbl_statusmora" runat="server" CssClass="alerta" ValidationGroup="val_moratorio"></asp:Label>
                                        </p>

                                        <div class="overflow_x" >
                                            <!-- Tabla de Expedientes generados por sucursal -->
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </content>
                        </ajaxToolkit:AccordionPane>

                        <%--PERIODOS DE GRACIA--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server" Enabled="true" >
                            <header>
                                <asp:Label ID="lbl_tit_pgracia" runat="server" Text="PERIODOS DE GRACIA E INTERES"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <br />
                                <asp:Label ID="lbl_tit_pgracia_mns" runat="server" CssClass="textoazul" Font-Size="9pt" Text="Capture los periodos de gracia de intereses."></asp:Label>
                                <br />

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_fechaini" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">Fecha Inicio (DD/MM/YYYY):</asp:Label>
                                <asp:TextBox ID="txt_fechaini" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" Enabled="False" MaxLength="10" ValidationGroup="val_pgracia"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaini" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaini" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaini" runat="server" ControlExtender="MaskedEditExtender_fechaini" ControlToValidate="txt_fechaini" CssClass="textogris" 
                                        ErrorMessage="MaskedEditValidator_fechaini" InvalidValueMessage="Fecha inválida" ValidationGroup="val_pgracia" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechaini" />
                                    <asp:RequiredFieldValidator ID="Req_fechaini" runat="server" ControlToValidate="txt_fechaini" CssClass="textogris" Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!" ValidationGroup="val_pgracia" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label ID="lbl_fechafin" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag">Fecha Fin (DD/MM/YYYY):</asp:Label>
                                <asp:TextBox ID="txt_fechafin" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" Enabled="False" MaxLength="10" ValidationGroup="val_pgracia"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafin" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechafin" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafin" runat="server" ControlExtender="MaskedEditExtender_fechafin" ControlToValidate="txt_fechafin" CssClass="textogris" 
                                        ErrorMessage="MaskedEditValidator_fechafin" InvalidValueMessage="Fecha inválida" ValidationGroup="val_pgracia" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechafin" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fechafin" />
                                    <asp:RequiredFieldValidator ID="Req_fechafin" runat="server" ControlToValidate="txt_fechafin" CssClass="textogris" Display="Dynamic" Enabled="False" ErrorMessage=" Falta Dato!" ValidationGroup="val_pgracia" />
                            </div>                                     
                                        
                            <p align="center">
                                <asp:Label ID="lbl_statuspgracia" runat="server" CssClass="alerta"></asp:Label>
                            </p>
                                             
                            <p align="center">   
                                <asp:Button ID="btn_guardapgracia" runat="server" class="btn btn-primary" Enabled="False" Text="Agregar" ValidationGroup="val_pgracia" />
                            </p>      
                                        
                            <div class="overflow_x">
                                <asp:DataGrid ID="dag_periodos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                    <Columns>
                                        <asp:BoundColumn DataField="idper">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="fechaini" HeaderText="Fecha Inicio">
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="fechafin" HeaderText="Fecha Fin">
                                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                            <ItemStyle ForeColor="#054B66" Width="40px" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:DataGrid>
                                </div>        
                                     
                            </content>
                        </ajaxToolkit:AccordionPane>

                        <%-- COMISIONES--%>
                        <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server" Enabled="true">
                            <header>
                                <asp:Label ID="lbl_comisiones" runat="server" Text="COMISIONES"></asp:Label>
                                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
                            </header>
                            <content>
                                <br />
                                <asp:Label ID="lbl_tit_com" runat="server" CssClass="textoazul" Font-Size="9pt" Text="Asigne las comisiones a aplicar."></asp:Label>
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td align="center" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_ComAsg" runat="server" CssClass="texto" Text="Comisiones asignadas" ToolTip="Se refiere a las comisiones que si se aplicaran para este préstamo por su apertura"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbl_ComDsp" runat="server" CssClass="texto" Text="Comisiones disponibles" ToolTip="Se refiere a las comisiones disponibles para la apertura de un préstamo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:ListBox ID="lst_ComAsg" runat="server" CssClass="textocajas" Height="150" Width="250px"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lst_ComDsp" runat="server" CssClass="textocajas" Height="150" Width="250"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btn_add" runat="server" Text="&lt;&lt;" />
                                            &nbsp;
                                            <asp:Button ID="btn_rem" runat="server" Text="&gt;&gt;" />
                                        </td>
                                    </tr>
                                </table>
                                <div align="center">
                                    <asp:Label ID="lbl_status_comision" runat="server" CssClass="alerta" ValidationGroup="val_comision"></asp:Label>
                                </div>

                                      
                        <br />
                                 

                            </content>
                        </ajaxToolkit:AccordionPane>
                         
                </panes>
            </ajaxToolkit:Accordion>

            <div align="center">
                    <asp:LinkButton ID="lnk_verplanespecial" runat="server" class="textogris"
                        Text="Generar plan" Enabled="false"></asp:LinkButton>
                    <br />
                    <asp:Label ID="lbl_statusfinal" runat="server" CssClass="alerta"></asp:Label>
            </div>
                           
                </ContentTemplate>
            </asp:UpdatePanel>
                    
    </div>
</section>

</asp:Content>

