<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO2_PLAN_INSTITUCIONAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
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
                                 
                <div class="tamano-cuerpo">
                    <div class= "module_subsec low_m columned three_columns top_m">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasanorpfsi" runat="server" CssClass="text_input_nice_input" MaxLength="6" Enabled="false">
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

                        <%--<div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasamorpfsi" runat="server" CssClass="text_input_nice_input" MaxLength="6" Enabled="false">
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
                        </div>--%>
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

                <%--<p align="center">
                        <asp:Label ID="lbl_errortasamorpfsi" runat="server" Cssclass="alerta"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasamorpfsi" runat="server" ControlToValidate="txt_tasamorpfsi" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" lass="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>       
                </p>--%>

            </div>
                                            
            <div align="center">
                <asp:Label runat="server" CssClass="alerta" ID="lbl_statusapartado"></asp:Label>
            </div>              
           
        </div>
    </section>
 </asp:Content>