<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_ANA_FASE1.aspx.vb" Inherits="SNTE5.CRED_EXP_ANA_FASE1" MaintainScrollPositionOnPostback ="true" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
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

        <asp:UpdatePanel runat="server" id="up1">
        <ContentTemplate>
            <input type="hidden" name="hdn_ResumenPersona" id="hdn_ResumenPersona" value="" runat="server" />
            <input type="hidden" name="hdn_notas" id="hdn_notas" runat="server" />
            <section class="panel" id="panel_datos">
                <header class="panel-heading">
                    <span>Análisis inicial</span>
                </header>
                <div class="panel-body">

                    <div class= "module_subsec low_m columned three_columns">
	                    <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_comites" runat="server" class="btn btn-primary2 dropdown_label"
                                    Enabled="False"></asp:DropDownList>
                                <span class="text_input_nice_label">Enviar expediente para análisis en:</span>
		                    </div>
	                    </div>
                    </div>                    

                    <div class="module_subsec vertical align_items_flex_start">
                    </div>

                    <div class= "module_subsec low_m columned three_columns flex_start">
                        <div class="module_subsec_elements" style="flex:1;">
                            <div class="module_subsec_elements_content vertical">
                                <span class="text_input_nice_label">Razón:</span>
                                <asp:TextBox ID="txt_razoncom" runat="server" class="text_input_nice_textarea"
                                    MaxLength="2000" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_razoncom"
                                    runat="server" Enabled="True"
                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                    TargetControlID="txt_razoncom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                </ajaxToolkit:FilteredTextBoxExtender>
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
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_buro" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="BURO">BURO DE CREDITO</asp:ListItem>
                                            <asp:ListItem Value="CIRCULO">CIRCULO DE CREDITO</asp:ListItem>
                                            <asp:ListItem Value="NODISP">NO DISPONIBLE</asp:ListItem>
                                        </asp:DropDownList>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Seleccione el buró:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_buro" runat="server" ControlToValidate="cmb_buro" 
                                                CssClass="textogris" Display="Dynamic" InitialValue="0" ErrorMessage=" Falta Dato!" ValidationGroup="val_score"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_score" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Calificación:</span>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_score" runat="server" ControlToValidate="cmb_score"
                                                CssClass="textogris" Display="Dynamic" InitialValue="0" ErrorMessage=" Falta Dato!" ValidationGroup="val_score"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-primary" Text="Guardar" ValidationGroup="val_score" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_guardar" runat="server" ConfirmText="¿Esta seguro de continuar con la información capturada?"
                                TargetControlID="btn_guardar">
                            </ajaxToolkit:ConfirmButtonExtender>
                    </div>  

                    <div class="module_subsec flex_end">
                        <asp:Panel ID="pnl_acta" runat="server" Visible="True">
                            <asp:LinkButton ID="lnk_generar_acta" runat="server" CssClass="textogris" Text="Generar Acta" Enabled="False"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            <asp:LinkButton ID="lnk_digitalizar" runat="server" CssClass="textogris" Text="Digitalizar Dictamen" Enabled="False"></asp:LinkButton>
                        </asp:Panel>
                    </div>                    
                </div>

            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnk_generar_acta" />
        </Triggers>
</asp:UpdatePanel>
</asp:Content>

