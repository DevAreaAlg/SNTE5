<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="REP_CRED_PROX_PAGOS.aspx.vb" Inherits="SNTE5.REP_CRED_PROX_PAGOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <section class="panel" >
        <header class="panel-heading">
            <span>Próximos Pagos</span>
        </header>
        <div class="panel-body">
            <br />
            <div class="module_subsec columned low_m align_items_flex_center" >
                    <label id="lbl_Producto0" class=" module_subsec_elements module_subsec_medium-elements title_tag" >*Tipo de reporte: </label>
                <div class="module_subsec_elements module_subsec_big-elements">
                    <asp:DropDownList ID="cmb_tipo_rep" runat="server" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" AutoPostBack="True" style="max-width:299px;">
                            </asp:DropDownList>
                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_tipo_rep" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_reporte" InitialValue="0" />
                </div>
            </div>

           

            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_plazo">Oficina: </asp:Label>
                 <div class="module_subsec_elements module_subsec_big-elements">
                    <asp:DropDownList ID="cmb_suc" runat="server" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" style="max-width:299px;">
                            </asp:DropDownList>
                   
                </div>
            </div>
             
            <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label ID="lbl_fechaliberacion" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag"> Fecha inicial (DD/MM/AAAA):</asp:Label>
                <asp:TextBox ID="txt_fecha_ini" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input"
                        MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox>
                <div class="text_input_nice_labels">

                   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaliberacion"
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha_ini">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaliberacion" runat="server"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fecha_ini">
                    </ajaxToolkit:CalendarExtender>

                                                   
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaliberacion"
                        runat="server" ControlExtender="MaskedEditExtender_fechaliberacion"
                        ControlToValidate="txt_fecha_ini" CssClass="textogris"
                        ErrorMessage="MaskedEditValidator_fechaliberacion"
                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>
                   
                </div>
                   </div>
                 <div class="module_subsec columned low_m align_items_flex_center" >
                <asp:Label ID="Label1" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag"> Fecha final (DD/MM/AAAA): </asp:Label>
                <asp:TextBox ID="txt_fecha_fin" runat="server" class="module_subsec_elements module_subsec_big-elements text_input_nice_input"
                        MaxLength="10" ValidationGroup="val_fecha"></asp:TextBox>
                <div class="text_input_nice_labels">

                   
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha_fin">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txt_fecha_ini">
                    </ajaxToolkit:CalendarExtender>

                                                   
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1"
                        runat="server" ControlExtender="MaskedEditExtender_fechaliberacion"
                        ControlToValidate="txt_fecha_fin" CssClass="textogris"
                        ErrorMessage="MaskedEditValidator_fechaliberacion"
                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>
                   
                </div>
                   </div>

                   
                           <div class="module_subsec flex_center">
                               <asp:Button ID="lnk_genera_rep" class="btn btn-primary" runat="server" ValidationGroup="val_planpago" Text="Generar" /> 
                             
                               </div>
                   
                                         

           
            <div align="center">
                <asp:Label ID="lbl_status_general" runat="server" CssClass="alerta"></asp:Label>
            </div>

         
                    
    </div>
</section>

</asp:Content>


