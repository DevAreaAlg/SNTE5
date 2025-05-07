<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO5.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO5" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">                   
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:textbox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:textbox>
            </div>       
        </div>
    </section>
                  

    <section class="panel" id="panel_datos">                 
        <header class="panel-heading">
            <span>Parámetros</span>
        </header>                       
        <div class="panel-body">
                        
                <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                <ContentTemplate> 

                <div class="module_subsec columned three_columns" >
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content"  >
                            <asp:TextBox ID="txt_diasven" CssClass="text_input_nice_input" runat="server" MaxLength ="5" Enabled="false"></asp:TextBox>
                            <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_diasvencida" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_diasven">
                            </ajaxtoolkit:filteredtextboxextender>
                            <div class="text_input_nice_labels">    
                                <span  class="text_input_nice_label">*Días para caer en cartera vencida:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_diasvencida"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_diasven" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    validationgroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_pagos"   CssClass="text_input_nice_input" runat="server" MaxLength ="2" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_pagos" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_pagos" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Número de pagos sostenidos:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_pagos" runat="server" ControlToValidate="txt_pagos" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_ingreso"   CssClass="text_input_nice_input" runat="server" MaxLength ="2" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ingreso" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_ingreso" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Razón ingreso-abono:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_ingreso" runat="server" ControlToValidate="txt_ingreso" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                </div>

                <div class="module_subsec columned three_columns" >
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_normal" CssClass="text_input_nice_input" runat="server" MaxLength ="2" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_normal" runat="server" Enabled="false" FilterType="Numbers" 
                            TargetControlID="txt_normal" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Días de gracia de interés ordinario:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_normal" runat="server" ControlToValidate="txt_normal" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_mora"   CssClass="text_input_nice_input" runat="server" MaxLength ="2" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_mora" runat="server" Enabled="false" FilterType="Numbers" 
                            TargetControlID="txt_mora" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Días de gracia de interés moratorio:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_mora" runat="server" ControlToValidate="txt_mora" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                </div>

                <div class="module_subsec low_m columned three_columns" >
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                                                                                                             
                            <asp:DropDownList ID="cmb_periodo" runat="server" class="btn btn-primary2 dropdown_label w_100" style="text-align:center" Enabled="false">
                                <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                <asp:ListItem Value="1">SI</asp:ListItem>
                                <asp:ListItem Value="0">NO</asp:ListItem>
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label title_tag">*¿Periodos de gracia sobre interés ordinario?</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_pnormal" runat="server" ControlToValidate="cmb_periodo" 
                                Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  
                                ValidationGroup="val_diasven" InitialValue="-1"></asp:RequiredFieldValidator>   
                            </div>                                                                                
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <div style="margin:0" class="radio">
                                <asp:RadioButton runat="server" GroupName="mora" Text=" Abono Vencido" ID="rad_abono" class="" Checked="True"></asp:RadioButton>
                            </div> 
                            <div style="margin:0" class="radio">
                                <asp:RadioButton runat="server" GroupName="mora" Text=" Capital" ID="rad_capital" class="" Enabled="false"></asp:RadioButton>
                            </div>
                                <span class="text_input_nice_label">*Cobro de interés moratorio sobre:</span>
                        </div>
                    </div>
                </div>

                <div class="module_subsec columned three_columns" >
                     <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_director"   CssClass="text_input_nice_input" runat="server" MaxLength ="9" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_director" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_director" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Monto mínimo para evaluación de director($):</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_director" runat="server" ControlToValidate="txt_director" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_monto"   CssClass="text_input_nice_input" runat="server" MaxLength ="9" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_monto" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Monto mínimo para evaluación en comité($):</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_monto" runat="server" ControlToValidate="txt_monto" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_garantia"   CssClass="text_input_nice_input" runat="server" MaxLength ="9" Enabled="false"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_garantia" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_garantia" ></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Monto mínimo obligatorio para seguro de vida($):</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_garantia" runat="server" ControlToValidate="txt_garantia" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_diasven"></asp:RequiredFieldValidator>
                            </div>
                        </div> 
                    </div>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">                    
                    <asp:Button ID="btn_guardar" class="btn btn-primary" runat="server" validationgroup="val_diasven" OnClick="btn_guardar_Click" Text="Guardar" />                              
                </div> 
                                  
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar" />
                </Triggers>
            </asp:UpdatePanel>                                  
            </div>
                 
        </section>
           
</asp:Content>


