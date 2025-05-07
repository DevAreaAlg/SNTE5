<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO6.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO6" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         
    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">                   
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:Textbox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>  
        </div>
    </section>
                    
    <section class="panel" id="panel_datos">                 
        <header class="panel_header_folder panel-heading">
            <span>Tasa de interés ordinaria (Anual)</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content init_show"> 
                <asp:UpdatePanel ID="UpdatePanelTasaNormalFija" runat="server">
                    <ContentTemplate>                                   
                        <h5 class="module_subsec">Tasa Fija</h5>
                                                                      
                            <div class="module_subsec columned low_m three_columns" >
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content"  >
                                        <asp:TextBox ID="txt_tasa_normal_fija_min" CssClass="text_input_nice_input" MaxLength="6" runat="server" Enabled="true"></asp:TextBox>
                                        <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tnfmin" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                            ValidChars="." TargetControlID="txt_tasa_normal_fija_min">
                                        </ajaxtoolkit:filteredtextboxextender>
                                        <div class="text_input_nice_labels">    
                                            <span  class="text_input_nice_label">*Tasa fija mínima:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tnfmin"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_normal_fija_min" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                validationgroup="val_tasanormalfija"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_tnfmin" runat="server" class=""
                                                ControlToValidate="txt_tasa_normal_fija_min" ErrorMessage=" Tasa Incorrecta!"
                                                ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content"  >
                                        <asp:TextBox ID="txt_tasa_normal_fija_max" CssClass="text_input_nice_input" MaxLength="6" runat="server" Enabled="true"></asp:TextBox>
                                        <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tnfmax" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                            ValidChars="." TargetControlID="txt_tasa_normal_fija_max">
                                        </ajaxtoolkit:filteredtextboxextender>
                                        <div class="text_input_nice_labels">    
                                            <span  class="text_input_nice_label">*Tasa fija máxima:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_normal_fija_min" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                validationgroup="val_tasanormalfija"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_tnfmax" runat="server" class=""
                                                ControlToValidate="txt_tasa_normal_fija_max" ErrorMessage=" Tasa Incorrecta!"
                                                ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>

                                    <div class="module_subsec_elements">
                                        <div class="module_subsec_elements_content flex_center"  >
                                                            
                                                    <span  class="text_input_nice_label">Tasa activa:</span>
                                                    <asp:CheckBox ID="chk_estatus_normal_fija" runat="server" CssClass="" />
                                                            
                                                        
                                        </div>
                                        <div class="module_subsec_elements_content flex_center"  >
                                                            
                                                    <span  class="text_input_nice_label">Tasa variable:</span>
                                                    <asp:CheckBox ID="chk_variable_normal_fija" runat="server" CssClass="" />
                                                            
                                                        
                                        </div>
                                    </div>
                            </div>
                                  
                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_statusnorfij" runat="server" CssClass="alerta"></asp:Label>
                            </div>        
                        
                            <div class="module_subsec flex_end">                        
                                <asp:Button ID="btn_guardarnormfija" class="btn btn-primary" runat="server"  validationgroup="val_tasanormalfija" Text="Guardar" />
                            </div>
                                
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardarnormfija" />
                    </Triggers>
                </asp:UpdatePanel>
                             
                <asp:UpdatePanel ID="UpdatePanelTasaIndFija" runat="server">
                    <ContentTemplate> 
                          
                        <h5 class="module_subsec">Tasa Indizada</h5>
                                  
                        <div class="module_subsec columned low_m three_columns" >
                            <div class="module_subsec_elements vertical">
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Índice financiero:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_tni" runat="server" ControlToValidate="cmb_indice_normal_ind" 
                                        Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  InitialValue="0" ValidationGroup="val_tasanormalindi"></asp:RequiredFieldValidator>
                                </div>                                                                  
                                <asp:DropDownList ID="cmb_indice_normal_ind" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center"></asp:DropDownList>   
                            </div>                                       
                        </div>
                                        
                        <div class="module_subsec columned low_m three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_normal_ind_min" CssClass="text_input_nice_input" MaxLength="5" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tnimin" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_normal_ind_min">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Puntos mínimos(%):</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tnimin" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_normal_ind_min" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_tasanormalindi"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tnimin" runat="server" class=""
                                            ControlToValidate="txt_tasa_normal_ind_min" ErrorMessage=" Puntos Incorrectos!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_normal_ind_max" CssClass="text_input_nice_input" MaxLength="5" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tnimax" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_normal_ind_max">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Puntos máximos:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tnimax"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_normal_ind_max" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_tasanormalindi"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tnimax" runat="server" class=""
                                            ControlToValidate="txt_tasa_normal_ind_max" ErrorMessage=" Puntos Incorrectos!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content flex_center"  >
                                    <span  class="text_input_nice_label">Tasa activa:</span>
                                    <asp:CheckBox ID="chk_estatus_normal_ind" runat="server" CssClass="" />
                                </div>                                                
                                <div class="module_subsec_elements_content flex_center"  >
                                    <span  class="text_input_nice_label">Tasa variable:</span>
                                    <asp:CheckBox ID="chk_variable_normal_ind" runat="server" CssClass="" />
                                </div>    
                            </div>
                        </div>
                                                   
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusnorind" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                                     
                        <div class="module_subsec flex_end">                                                            
                            <asp:Button ID="btn_guardarnormind" class="btn btn-primary" runat="server" validationgroup="val_tasanormalindi" Text="Guardar" />
                        </div> 
                </ContentTemplate>
                        <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardarnormind" />
                </Triggers>
                </asp:UpdatePanel>
                </div>

        </div>
    </section>
                      
    <section class="panel" id="panel_mora">                 
        <header class="panel_header_folder panel-heading">
            <span>Tasa de interés moratoria (Mensual)</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelMoraFija" runat="server">
                    <ContentTemplate> 
                             
                        <h5 class="module_subsec">Tasa Fija</h5>                                    
                                  
                        <div class="module_subsec columned low_m three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_mora_fija_min" CssClass="text_input_nice_input" MaxLength="6" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tmfmin" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_mora_fija_min">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Tasa fija mínima:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tmfmin"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_mora_fija_min" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_tasamorafija"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tmfmin" runat="server" class=""
                                            ControlToValidate="txt_tasa_mora_fija_min" ErrorMessage=" Tasa Incorrecta!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_mora_fija_max" CssClass="text_input_nice_input" MaxLength="6" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_tmfmax" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_mora_fija_max">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Tasa fija máxima:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tmfmax"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_tasa_mora_fija_max" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_tasamorafija"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_tmfmax" runat="server" class=""
                                            ControlToValidate="txt_tasa_mora_fija_max" ErrorMessage=" Tasa Incorrecta!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content flex_center"  >
                                            <span  class="text_input_nice_label">Tasa activa:</span>
                                            <asp:CheckBox ID="chk_estatus_mora_fija" runat="server" CssClass="" />
                                                        
                                </div>
                                <div class="module_subsec_elements_content flex_center"  >
                                    <span  class="text_input_nice_label">Tasa variable:</span>
                                    <asp:CheckBox ID="chk_variable_mora_fija" runat="server" CssClass="" />
                                </div>
                            </div>
                        </div>
                                  
                        <div class="module_subsec flex_center">
                             <asp:Label ID="lbl_statusmorfij" runat="server" CssClass="alerta"></asp:Label>
                        </div>     
                           
                        <div class="module_subsec flex_end">                  
                            <asp:Button ID="btn_guardarmorafija" class="btn btn-primary" runat="server" validationgroup="val_tasamorafija" Text="Guardar" />                               
                        </div>
                                
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardarmorafija" />
                </Triggers>
                </asp:UpdatePanel>
                </div>

            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelMoraIndi" runat="server">
                    <ContentTemplate> 
                                  
                        <h5 class="module_subsec">Tasa Indizada</h5>
                                                                 
                        <div class="module_subsec columned low_m three_columns" >
                            <div class="module_subsec_elements vertical">
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Índice financiero:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_indimora" runat="server" ControlToValidate="cmb_indice_mora_ind" 
                                        Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                        ValidationGroup="val_tasamoraindi"></asp:RequiredFieldValidator>
                                </div>                       
                                <asp:DropDownList ID="cmb_indice_mora_ind" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center"></asp:DropDownList>                                                                                           
                            </div>
                        </div>
                                        
                        <div class="module_subsec columned low_m three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_mora_ind_min" MaxLength="5" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_puntosmoramin" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_mora_ind_min"></ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Puntos mínimos(%):</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_puntosmoramin"  CssClass="alertaValidator bold" runat="server" Display="Dynamic" 
                                            ControlToValidate="txt_tasa_mora_ind_min" ErrorMessage="Falta Dato!" validationgroup="val_tasamoraindi"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_puntosmoramin" runat="server" class="" ControlToValidate="txt_tasa_mora_ind_min" 
                                            ErrorMessage=" Puntos Incorrectos!" ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_tasa_mora_ind_max" CssClass="text_input_nice_input" MaxLength="5" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_puntosmoramax" runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                        ValidChars="." TargetControlID="txt_tasa_mora_ind_max">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Puntos máximos:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_puntosmoramax"  CssClass="alertaValidator bold" runat="server" Display="Dynamic" 
                                            ControlToValidate="txt_tasa_mora_ind_max" ErrorMessage="Falta Dato!" validationgroup="val_tasamoraindi"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_puntosmoramx" runat="server" class=""
                                            ControlToValidate="txt_tasa_mora_ind_max" ErrorMessage=" Puntos Incorrectos!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content flex_center"  >
                                    <span  class="text_input_nice_label">Tasa activa:</span>
                                    <asp:CheckBox ID="chk_estatus_mora_ind" runat="server" CssClass="" />
                                </div> 
                                                    
                                <div class="module_subsec_elements_content flex_center"  >
                                    <span  class="text_input_nice_label">Tasa variable:</span>
                                    <asp:CheckBox ID="chk_variable_mora_ind" runat="server" CssClass="" />
                                </div>
                            </div>
                        </div>                                
                                
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusmorind" runat="server" CssClass="alerta"></asp:Label>
                        </div> 
                               
                        <div class="module_subsec flex_end">                      
                            <asp:Button ID="btn_guardarmoraind" class="btn btn-primary" runat="server" validationgroup="val_tasamoraindi" Text="Guardar" />
                        </div>
                                    
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardarmoraind" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </div>
    </section>
                       
    <section class="panel" id="panel_iva">                 
        <header class="panel_header_folder panel-heading">
            <span>Configuración de IVA</span>
            <span class=" panel_folder_toogle up"  href="#" >&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelIVA" runat="server">
                    <ContentTemplate>                                    
                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:RadioButton ID="rad_IVA_Normal" runat="server" class="" GroupName="ManejoIVA" 
                                    Text=" Aplicar efecto inflacionario sobre IVA" Checked="true"></asp:RadioButton>    
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:RadioButton ID="rad_Interes_Gravado" runat="server" class="" GroupName="ManejoIVA" 
                                        Text=" Interés gravado al 100%"></asp:RadioButton>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:RadioButton ID="rad_IVA_Exento" runat="server" class="" GroupName="ManejoIVA" 
                                        Text=" Exentar IVA"></asp:RadioButton>
                                </div>
                            </div>
                        </div>    

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusiva" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">                                                              
                            <asp:Button ID="btn_guardaiva" class="btn btn-primary" runat="server" Text="Guardar" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardaiva" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>  

</asp:Content>

