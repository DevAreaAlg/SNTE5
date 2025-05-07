<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO2.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO2" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="module_subsec columned low_m align_items_flex_center">
                    <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                </div>
            </div>
        </div>
    </section>
                    
    <section class="panel" id="panel_datos">                 
        <header class="panel_header_folder panel-heading">
            <span>Referencias, avales y codeudores</span>
            <span class=" panel_folder_toogle down"  href="#" >&rsaquo;</span>
        </header>                       
        <div class="panel-body">
            <div class="panel-body_content init_show"> 
                <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate> 

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_min_referencias" CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_minrefe" runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="txt_min_referencias">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Número mínimo de referencias:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_minrefe"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_min_referencias" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_max_referencias"   CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maxrefe" runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_max_referencias" ></ajaxToolkit:FilteredTextBoxExtender>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Número máximo de referencias:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_maxrefe" runat="server" ControlToValidate="txt_max_referencias" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_min_avales" CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_minavales" runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="txt_min_avales">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Número mínimo de avales:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_minavales"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_min_avales" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            validationgroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_max_avales"   CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maxavales" runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_max_avales" ></ajaxToolkit:FilteredTextBoxExtender>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Número máximo de avales:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_maxavales" runat="server" ControlToValidate="txt_max_avales" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content"  >
                                    <asp:TextBox ID="txt_min_code" CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_mincode" runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="txt_min_code">
                                    </ajaxtoolkit:filteredtextboxextender>
                                    <div class="text_input_nice_labels">    
                                        <span  class="text_input_nice_label">*Número mínimo de codeudores:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_mincode"  CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_min_code" ErrorMessage="Falta Dato!"
                                            validationgroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_max_code"   CssClass="text_input_nice_input" runat="server" MaxLength="2"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maxcode" runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_max_code" ></ajaxToolkit:FilteredTextBoxExtender>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Número máximo de codeudores:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_maxcode" runat="server" ControlToValidate="txt_max_code" Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_datos"></asp:RequiredFieldValidator>
                                    </div>
                                </div> 
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_guardado" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                           
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_datos" class="btn btn-primary" runat="server" onClick="btn_guardar_datos_Click" validationgroup="val_datos" Text="Guardar" /> 
                        </div> 
                                            
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar_datos" />
                </Triggers>
                </asp:UpdatePanel>                                  
            </div>
        </div>
    </section>

    <section class="panel" id="panel_doctos">
        <header class="panel_header_folder panel-heading">
            <span>Documentación requerida</span>
            <span class=" panel_folder_toogle up"  href="#" >&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content"> 
                <asp:UpdatePanel ID="UpdatePanelDoctos" runat="server">
                    <ContentTemplate> 
                        <div class=" module_subsec overflow_x shadow module_subsec" >
                            <asp:DataGrid ID="DAG_documentos" runat="server" AutoGenerateColumns="False" class="table table-striped"
                                GridLines="None" >
                                <HeaderStyle CssClass="table_header" />
                                <Columns>                                                
                                    <asp:BoundColumn DataField="TIPODOC" HeaderText="Clave">
                                       <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Tipo documento">                                   
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CANTIDAD" HeaderText="Cantidad">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FASE" HeaderText="Fase">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn  CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                        <ItemStyle Width="15%" />
                                    </asp:ButtonColumn>
                                </Columns>                            
                        </asp:DataGrid>
                    </div>
                             
                        <div class=" align_items_flex_start module_subsec columned two_columns  ">
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_labels"> 
                                        <span class="text_input_nice_label title_tag">*Tipo de documento:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipodoc" runat="server" ControlToValidate="cmb_tipodoc" 
                                            Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  InitialValue="0"
                                            ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-group min_w">                       
                                        <asp:DropDownList ID="cmb_tipodoc" runat="server" class="btn btn-primary2 dropdown_label w_100" style="text-align:center"
                                        AutoPostBack="True">
                                        </asp:DropDownList>   
                                    </div>

                                    <div class="text_input_nice_labels"> 
                                        <span class="text_input_nice_label title_tag">*Cantidad:</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_cantidad" runat="server" ControlToValidate="cmb_cantidad" 
                                            Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" 
                                            ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-group min_w">                       
                                        <asp:DropDownList ID="cmb_cantidad" runat="server" class="btn btn-primary2 dropdown_label w_100" style="text-align:center"
                                        AutoPostBack="False">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                
                                        </asp:DropDownList>
                                                    
                                    </div>

                                    <div class="text_input_nice_labels"> 
                                        <span class="text_input_nice_label title_tag">*Fase de validación:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_fase" runat="server" ControlToValidate="cmb_fase" 
                                                Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0" 
                                                ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="btn-group min_w">                       
                                        <asp:DropDownList ID="cmb_fase" runat="server" class="btn btn-primary2 dropdown_label w_100" style="text-align:center"
                                        AutoPostBack="false">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="1">FASE 1</asp:ListItem>
                                            <%--<asp:ListItem Value="2">FASE 2</asp:ListItem>--%>
                                        </asp:DropDownList>                                                    
                                    </div>
                                </div>                                           
                            </div>
                                        
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <span class="text_input_nice_label title_tag">Detalle de documentos:</span>
                                    <div class="btn-group min_w">                       
                                        <asp:ListBox ID="lst_Documentos" runat="server" CssClass="text_input_nice_textarea w_100" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </div>
                                </div>
                            </div>                                 
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_guardado_doctos" runat="server" CssClass="alerta"></asp:Label>
                        </div>   

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_doctos" class="btn btn-primary" runat="server" onClick="btn_guardar_doctos_Click" validationgroup="val_doc" Text="Guardar" />
                        </div> 

                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar_doctos" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    
    
                
</asp:Content>


