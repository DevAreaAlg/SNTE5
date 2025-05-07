<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO8.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO8" MaintainScrollPositionOnPostback ="true" %>

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

    <section class="panel" id="panel_formapago">                 
        <header class="panel_header_folder panel-heading">
            <span>Formas de pago</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content init_show">  
                <asp:UpdatePanel ID="UpdatePanelFormaPago" runat="server">
                    <ContentTemplate> 
                        <div class= "module_subsec low_m columned three_columns">
	                        <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">                                   
                                    <asp:DropDownList ID="cmb_tipocobro" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center"></asp:DropDownList> 
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label title_tag">*¿Qué tipo de cobro se permite en este producto?</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipocobro" runat="server" ControlToValidate="cmb_tipocobro" 
                                        Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  
                                        ValidationGroup="val_cobro" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
		                        </div>
	                        </div>

                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content"> 
                                    <asp:DropDownList ID="cmb_cta_operativa" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center"></asp:DropDownList> 
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label title_tag">*¿Procurar tener saldo en cero en cuenta operativa?</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_operativa" runat="server" ControlToValidate="cmb_cta_operativa" 
                                            Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  
                                            ValidationGroup="val_cobro" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                         
                        </div>

                        <div class= "module_subsec low_m columned three_columns">
                            
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statuscobro" Visible="false" runat="server" CssClass="module_subsec low_m align_items_flex_center alerta"></asp:Label>
                        </div>
                                
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardacobro" class="btn btn-primary" runat="server"  OnClick="btn_guardacobro_Click"  ValidationGroup="val_cobro" Text="Guardar" />
                        </div>                   
                                                  
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardacobro" />
                    </Triggers>
                </asp:UpdatePanel>                                  
            </div>
        </div>
    </section>
           
    <section class="panel" id="panel_periodcap" runat="server">                 
        <header class="panel_header_folder panel-heading">
            <span>Periodicidad de capital</span>
            <span class=" panel_folder_toogle up"  href="#" >&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelPeriodCap" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned three_columns">                                
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_ndiascap" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de capital cada "n" días</span>                                                            
                                </div>                                                             
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_ndiasmincap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtenderndiasmincap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_ndiasmincap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_ndiasmaxcap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_ndiasmaxcap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_ndiasmaxcap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_nsemcap" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de capital cada "n" semanas</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nsemmincap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nsemmincap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nsemmincap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nsemmaxcap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nsemmaxcap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nsemmaxcap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>   
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_nmescap" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de capital cada "n" meses</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nmesmincap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nmesmincap" runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="txt_nmesmincap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nmesmaxcap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nmesmaxcap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nmesmaxcap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>   
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_xdiacap" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de capital los dias "x" de cada mes (mínimo y máximo se refieren a días a considerar en cada mes)</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_xdiamincap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_xdiamincap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_xdiamincap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_xdiamaxcap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_xdiamaxcap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_xdiamaxcap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div> 

                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_espcap" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Plan de pagos especial (mínimo y máximo se refieren al número de amortizaciones en la vida del préstamo)</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_espmincap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_espmincap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_espmincap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_espmaxcap" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_espmaxcap" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_espmaxcap">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div> 
                        </div>

                        <div class="module_subsec flex_center">
                             <asp:Label ID="lbl_statpercap" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                                
                        <div class="module_subsec flex_end">                                                       
                            <asp:Button ID="btn_guardapercap" class="btn btn-primary" runat="server" OnClick="btn_guardapercap_Click" Text="Guardar" />
                        </div> 
                                            
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardapercap" />
                    </Triggers>
                </asp:UpdatePanel>                                  
            </div>
        </div>
    </section>

    <section class="panel" id="panel_periodint" runat="server">                 
        <header class="panel_header_folder panel-heading">
            <span>Periodicidad de interés</span>
            <span class=" panel_folder_toogle up"  href="#" >&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelperint" runat="server">
                    <ContentTemplate>                                     
                        <div class="module_subsec columned three_columns" >                                
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_ndiasint" runat="server" CssClass="" AutoPostBack="True" />
                                    &nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span class="text_input_nice_label">Amortización de interés cada "n" días</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_ndiasminint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_ndiasminint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_ndiasminint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_ndiasmaxint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_ndiasmaxint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_ndiasmaxint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_nsemint" runat="server" CssClass="" AutoPostBack="True"/>&nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de interés cada "n" semanas</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nsemminint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nsemminint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nsemminint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nsemmaxint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nsemmaxint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nsemmaxint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>   
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_nmesint" runat="server" CssClass="" AutoPostBack="True"/>
                                    &nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de interés cada "n" meses</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nmesminint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_txt_nmesminint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nmesminint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_nmesmaxint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nmesmaxint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_nmesmaxint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>   
                               
                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_xdiaint" runat="server" CssClass="" AutoPostBack="True"/>
                                    &nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Amortización de interés los dias "x" de cada mes (mínimo y máximo se refieren a días a considerar en cada mes)</span>                                                            
                                </div>                                                             
                            </div>
                                
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_xdiaminint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_xdiaminint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_xdiaminint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_xdiamaxint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_xdiamaxint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_xdiamaxint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div> 

                            <div class="module_subsec_elements">                                                  
                                <asp:CheckBox ID="chk_espint" runat="server" CssClass="" AutoPostBack="True"/>
                                    &nbsp;&nbsp;&nbsp;
                                <div class="text_input_nice_labels">    
                                    <span  class="text_input_nice_label">Plan de pagos especial (mínimo y máximo se refieren al número de amortizaciones en la vida del préstamo)</span>                                                            
                                </div>                                                             
                            </div>
                                 
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_espminint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_espminint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_espminint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Mínimo:</span>                                                       
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_espmaxint" CssClass="text_input_nice_input" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_espmaxint" runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_espmaxint">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">Máximo:</span>                                                       
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statperint" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end" >                            
                            <asp:Button ID="btn_copiadecap" class="btn btn-primary module_subsec_elements" runat="server" OnClick="btn_copiadecap_Click" Text="Copiar Valores de Capital" /> &nbsp;&nbsp;
                            <asp:Button ID="btn_guardaperint" class="btn btn-primary  module_subsec_elements" OnClick="btn_guardaperint_Click" runat="server" Text="Guardar"/>
                        </div>
                                                    
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_copiadecap" />
                        <asp:AsyncPostBackTrigger ControlID="btn_guardaperint" />
                    </Triggers>
                </asp:UpdatePanel>                                  
            </div>
        </div>
    </section>

    <section class="panel" id="panel_formapagocred">
        <header class="panel_header_folder panel-heading">
            <span>Forma de pago del préstamo</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content">                                                                                    
                <asp:UpdatePanel ID="upd_pnl_formapagocred" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardarpagocred" />
                    </Triggers>
                    <ContentTemplate>  
                        <asp:Label ID="label1" runat="server"></asp:Label>
                        <div class="module_subsec">
                            <div class="overflow_x shadow w_100">
                                <asp:GridView ID="dag_formapago" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" TabIndex ="17" Width="100%">
                                    <HeaderStyle CssClass="table_header" />                      
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150" DataField="ID" HeaderText="Id forma de pago" >
                                            <ItemStyle Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="FORMAPAGO" HeaderText="Forma de pago" >
                                            <ItemStyle Width="70%"></ItemStyle>
                                        </asp:BoundField>   
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>          
                                    </Columns>                                    
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_pagocred" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardarpagocred" class="btn btn-primary" OnClick="btn_guardarpagocred_Click" runat="server"  Text="Guardar" />
                        </div>                                                
                        
                    </ContentTemplate>
                </asp:UpdatePanel>                                                
            </div>                          
        </div>
    </section> 
                
</asp:Content>


