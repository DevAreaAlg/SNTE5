<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO5.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO5" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <section class="panel" >
            <header class="panel-heading">
                    <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="row col-lg-12 " style="align-content:flex-end">
                        <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                    <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                    <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                </div> 
                    </div>
                </div>
            </div>
        </section> 
              
                                                         
        <div style="background-color: transparent; flex-wrap: nowrap; box-shadow:none; margin-left:15px; margin-right:15px" class="module_subsec columned panel align_items_flex_center">
            <div class="module_subsec_elements">                          
                <span class="module_subsec_elemnts_content title_tag">*Elija garantía a capturar: </span>
            </div>

            <div class="module_subsec_elements btn-group min_w ">                            
                <asp:DropDownList ID="cmb_tipo_garantias" runat="server" class="btn btn-primary2 dropdown_label w_100" Style="text-align: center"
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo_garantia" CssClass="alertaValidator"
                    ControlToValidate="cmb_tipo_garantias" Display="Dynamic" ErrorMessage=" Falta Dato!"
                    ValidationGroup="val_garantia" InitialValue="-1" />
            </div>
        </div>     
    
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="row col-lg-12 " style="align-content:flex-end">
                    <asp:Label ID="lbl_monto_cred" runat="server" Text="Label" CssClass="h4"></asp:Label><br />
                    <asp:Label ID="lbl_dinero" runat="server" Text="" CssClass="h4"></asp:Label><br />
                           
                </div>
            </div>
        </div>
      
                 
        <asp:UpdatePanel ID="upd_garantias_generales" runat="server">
            <ContentTemplate>
                  
                <div class="overflow_x panel shadow">
                    <asp:DataGrid ID="dag_garantias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">                           
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="idtipo" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cvegarantia" Visible="false" HeaderText="Clave de garantía"></asp:BoundColumn>
                            <asp:BoundColumn DataField="tipo" HeaderText="Tipo de garantía"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idgarantia" HeaderText="Garantía"></asp:BoundColumn>
                            <asp:BoundColumn DataField="valor" HeaderText="Valor ($)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cobertura" HeaderText="% garantía utilizado"></asp:BoundColumn>
                            <asp:BoundColumn DataField="estatus" HeaderText="Estatus" Visible="false"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar garantía"
                                Text="Eliminar"></asp:ButtonColumn>

                            <asp:ButtonColumn CommandName="DIGITALIZAR" HeaderText="Digitalizar garantía"
                                Text="Digitalizar" Visible="true"></asp:ButtonColumn>

                        </Columns>
                    </asp:DataGrid>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status_general" runat="server" CssClass="alerta"></asp:Label>
                </div>                 

                <asp:Panel ID="pnl_gtia_hip" runat="server" Visible="false" CssClass="panel">
                    <asp:Panel ID="pnl_gtia_hip_header" runat="server" CssClass="panel_header_folder panel-heading">
                        <span>Garantía hipotecaria</span>
                        <asp:label ID="lbl_flecha_hip" runat="server" cssclass=" panel_folder_toogle down">&rsaquo;</asp:label>
                    </asp:Panel>
                    <asp:Panel ID="pnl_gtia_hip_body" runat="server" CssClass="panel-body">

                        <asp:Panel ID="pnl_garantia_body_content" runat="server" CssClass="panel-body_content init_show">
                             
                                <div class="module_subsec columned three_columns ">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_clave" CssClass="text_input_nice_input" MaxLength="25" runat="server" Enabled="true"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_clave" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="txt_clave"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Clave catastral:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_monto" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_clave" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_rppc" CssClass="text_input_nice_input" MaxLength="25" runat="server" ToolTip="Folio real del Registro Público de la Propiedad y el Comercio"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rppc" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="txt_rppc"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Folio real:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_rppc" runat="server" ControlToValidate="txt_rppc" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements align_items_flex_end"> 
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" OnTextChanged="txt_cp_TextChanged"></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">*Código postal:</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                                        CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_cp" runat="server" Enabled="True"
                                                        TargetControlID="txt_cp" FilterType="Numbers">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        <asp:ImageButton ID="btn_cp" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" AutoPostBack="False" />
                                    </div>  
                                     
                                </div>
                                                                               
                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_Estado" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Estado: </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Municipio: </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_asentamiento" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Asentamiento: </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>   
                            
                            <div class= "module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex:1;">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <div class="text_input_nice_div module_subsec_elements_content" style="flex:1px;">
                                                <asp:TextBox ID="txt_calle_inm" class="text_input_nice_input" runat="server" ></asp:TextBox>
                                                <div class="text_input_nice_labels">
                                                    <span class="text_input_nice_label">*Calle y número del inmueble:</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_calle_inm"
                                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_calle_inm">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:LinkButton ID="lnk_garantia_hip" runat="server" class="textogris" 
                                            Text="Buscar Garantía" />
                                        </div>
                                    </div>
                                                                          
                                </div>
                                        
                                <!--INMUEBLE-->
                                <div class="module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_inm" runat="server" class="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Inmueble:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_inm" runat="server" ControlToValidate="txt_inm"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_inm" runat="server" Enabled="True" 
                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias_inm" 
                                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>



                                     <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_hip" CssClass="text_input_nice_input" MaxLength="12" runat="server" Enabled="true"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto_hip" runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_monto_hip" >
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Valor de garantia ($): </span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_monto_hip" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                               
                                            </div>
                                        </div>
                                    </div>
                               </div>

                                <!--REFERENCIAS-->
                                <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_referencias_inm" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Referencias del inmueble:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_referencias_inm" runat="server" ControlToValidate="txt_referencias_inm"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_referencias_inm" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias_inm" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_propietario" CssClass="text_input_nice_input" MaxLength="300" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_propietario" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="txt_propietario"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Propietario:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_propietario" runat="server" ControlToValidate="txt_propietario" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">                                                                                               
                                            <asp:DropDownList ID="cmb_relacion" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center">
                                            </asp:DropDownList>                                                
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tipo de relación con el acreditado: </span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="alertaValidator"
                                                ControlToValidate="cmb_relacion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_hip" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_sexo"  CssClass="btn btn-primary2 dropdown_label" runat="server" Style="text-align: center" >
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="M">MUJER</asp:ListItem>
                                        <asp:ListItem Value="H">HOMBRE</asp:ListItem>                                     
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Sexo:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_sexo" CssClass="alertaValidator"
                                            ControlToValidate="cmb_sexo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_hip" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                          </div>


                                <div class="module_subsec columned three_columns ">
                                <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fechanac" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Fecha de nacimiento: (DD/MM/AAAA):</span>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechanac" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechanac" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechanac" runat="server" ControlExtender="MaskedEditExtender_fechanac" ControlToValidate="txt_fechanac" CssClass="textogris"
                                                    ErrorMessage="MaskedEditExtender_fechanac" InvalidValueMessage="Fecha inválida" Display="Dynamic" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechanac" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechanac" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_fechanac" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_fechanac" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                            </div>
                                        </div>
                                    </div>


                                <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_rfc" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span>*RFC:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_rfc" runat="server" ControlToValidate="txt_rfc"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfc" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_rfc">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_rfc" runat="server" class="textogris" ControlToValidate="txt_rfc" 
                                           ErrorMessage="Error: formato RFC  incorrecto." ValidationExpression="^[a-zA-Z]{4}(\d{6})((\D|\d){3})?$" ValidationGroup="val_hip" />
                                    </div>
                                </div>
                            </div>



                                <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_telefono" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_telefono" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_telefono">
                                           </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Teléfono:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_telefono" runat="server" ControlToValidate="txt_telefono" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>




                        <div class="module_subsec columned three_columns ">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_edo_civil" CssClass="btn btn-primary2 dropdown_label" runat="server" Style="text-align: center" AutoPostBack="True">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="SOL">SOLTERO(A)</asp:ListItem>
                                        <asp:ListItem Value="CAS">CASADO(A)</asp:ListItem>
                                        <asp:ListItem Value="UNL">UNION LIBRE</asp:ListItem>
                                        <asp:ListItem Value="DIV">DIVORCIADO(A)</asp:ListItem>
                                        <asp:ListItem Value="VIU">VIUDO(A)</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Estado civil:</span>
                                        <asp:RequiredFieldValidator runat="server" ID="req_edo_civil" CssClass="alertaValidator"
                                            ControlToValidate="cmb_edo_civil" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_hip" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>




                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_conyuge" CssClass="text_input_nice_input" MaxLength="150" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_conyuge" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_conyuge"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label" id ="conyuge" runat ="server">*Nombre del conyuge:</span>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_conyuge" runat="server" ControlToValidate="txt_conyuge" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>                      
                                </div>

                                        
                                <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_descripcion_gtia" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Descripción de la garantía:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_descripcion_gtia"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_descripcion_gtia" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_descripcion_gtia" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                         


                            <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_folioes" CssClass="text_input_nice_input" MaxLength="8" runat="server" ToolTip="Folio real del Registro Público de la Propiedad y el Comercio"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_folioes" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_folioes">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número de la escritura:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_folioes" runat="server" ControlToValidate="txt_folioes" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fechaes" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Fecha de escrituración (DD/MM/AAAA):</span>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaes" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaes" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaes" runat="server" ControlExtender="MaskedEditExtender_fechaes" ControlToValidate="txt_fechaes" CssClass="textogris"
                                                    ErrorMessage="MaskedEditExtender_fechaes" InvalidValueMessage="Fecha inválida" Display="Dynamic" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaes" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaes" />
                                                <asp:RequiredFieldValidator runat="server" ID="req_fechaes" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_fechaes" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                

                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estado_registro" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">*Estado de registro: </span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_estado_registro" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_estado_registro" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_hip" InitialValue="-1" />
                                            </div>  
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio_registro" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">*Municipio de registro: </span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_municipio_registro" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_municipio_registro" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_hip" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_num_notario" CssClass="text_input_nice_input" MaxLength="9" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_num_notario" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_num_notario">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Núm. de notario:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_num_notario" runat="server" ControlToValidate="txt_num_notario" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_nombre_notario" CssClass="text_input_nice_input" MaxLength="300" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre_notario" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="txt_nombre_notario"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Nombre del notario:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre_notario" runat="server" ControlToValidate="txt_nombre_notario" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!--LUGAR DE NACIMIENTO PAIS, ESTADO, ASENTAMIENTO-->
                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estado_notaria" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">*Estado de notaría: </span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_estado_notaria" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_estado_notaria" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_hip" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio_notaria" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">*Municipio de notaría: </span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_municipio_notaria" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_municipio_notaria" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_hip" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                     
                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_m2terreno" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>

                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">M2 Terreno:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_m2terreno" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_m2terreno">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_m2construido" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">M2 Construido:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_m2construido" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_m2construido">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_antiguedad" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Antigüedad (Años): </span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_antiguedad" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_m2construido">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_gravamen" class="text_input_nice_input" runat="server" MaxLength="25"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">*Gravamen:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_gravamen" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_gravamen">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_gravamen" runat="server" ControlToValidate="txt_gravamen" 
                                                  CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                    </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_institucion_Gravamen" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Institución gravamen:</span>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_inggravamen" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_institucion_Gravamen">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_aforo" class="text_input_nice_input" runat="server" MaxLength="25"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Aforo:</span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_Aforo"
                                                    runat="server" ControlToValidate="txt_aforo" CssClass="textogris"
                                                    ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_aforo" runat="server" Enabled="True"
                                                    FilterType="Numbers" ValidChars="." TargetControlID="txt_aforo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec columned three_columns ">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_avaluo" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Avalúo:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_avaluo" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_avaluo" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_avaluo" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_avaluo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_fecha_avaluo" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Fecha de avalúo (DD/MM/AAAA):</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_fecha_avaluo" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_fecha_avaluo" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fecha_avaluo" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fecha_avaluo" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fecha_avaluo" runat="server" ControlExtender="MaskedEditExtender_fecha_avaluo" ControlToValidate="txt_fecha_avaluo" CssClass="textogris"
                                                    ErrorMessage="MaskedEditExtender_fecha_avaluo" InvalidValueMessage="Fecha inválida" Display="Dynamic"/>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fecha_avaluo" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fecha_avaluo" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_valor_avaluo" class="text_input_nice_input" runat="server" MaxLength="20"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Valor avalúo:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_valor_avaluo" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_valor_avaluo" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_valor_avaluo"
                                                    runat="server" ControlToValidate="txt_valor_avaluo" CssClass="textogris"
                                                    ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_valor_Avaluo" runat="server" Enabled="True"
                                                    FilterType="Numbers" ValidChars="." TargetControlID="txt_valor_avaluo">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                

                                <div class="module_subsec flex_end">
                                        <asp:Button ID="btn_guardar_hip" class="btn btn-primary" runat="server" ValidationGroup="val_hip" Text="Guardar" />                                   
                                </div>

                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>

                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_status" runat="server" Text="" class="alerta"></asp:Label>
                                </div>
                   
                <asp:Panel ID="pnl_gtia_pren" runat="server" Visible="false" CssClass="panel">
                    <asp:Panel ID="pnl_gtia_pren_header" runat="server" CssClass="panel_header_folder panel-heading">
                        <span>Garantía prendaria</span>
                        <asp:label ID="lbl_flecha" runat="server" cssclass=" panel_folder_toogle down">&rsaquo;</asp:label>
                    </asp:Panel>

                    <asp:Panel ID="pnl_gtia_pren_body" runat="server" CssClass="panel-body">
                        <asp:Panel ID="pnl_gtia_pren_body_content" runat="server" CssClass="panel-body_content init_show">
                                     
                            <div class="module_subsec columned three_columns ">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_num_serie" CssClass="text_input_nice_input" MaxLength="25" runat="server" Enabled="true"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_num_serie" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="txt_num_serie"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Número de serie:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_num_serie" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_num_serie" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                ValidationGroup="val_pren"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_tipo_prenda" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True">
                                            </asp:DropDownList>                                                                                   
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Tipo de prenda: </span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_tipo_prenda" CssClass="alertaValidator"
                                                ControlToValidate="cmb_tipo_prenda" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_pren" InitialValue="-1" /> 
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_monto_pren" CssClass="text_input_nice_input" MaxLength="12" runat="server" Enabled="true"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Valor de garantia ($): </span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_monto_pren" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_monto_pren" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                ValidationGroup="val_pren"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1_monto_pren"
                                                runat="server" ControlToValidate="txt_monto_pren" CssClass="textogris"
                                                ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto_pren" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                            TargetControlID="txt_monto_pren" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_propietario_pren" CssClass="text_input_nice_input" MaxLength="300" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_propietario_pren" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_propietario_pren"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Propietario:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_propietario_pren" runat="server" ControlToValidate="txt_propietario_pren" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                ValidationGroup="val_pren"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                         <asp:DropDownList ID="cmb_tipo_relacion_pren" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                            AutoPostBack="True">
                                        </asp:DropDownList>                                                                               
                                        <div class="text_input_nice_labels">
                                           <span class="text_input_nice_label title_tag">*Tipo de relación con el acreditado: </span>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_relacion_pren" CssClass="alertaValidator"
                                            ControlToValidate="cmb_tipo_relacion_pren" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_pren" InitialValue="-1" /> 
                                        </div>
                                    </div>
                                </div>

                            </div>                                    

                            <div class="module_subsec">
                                <div class="module_subsec_elements w_100">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_descripcion_pren" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_descripcion_pren" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_descripcion_pren" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Descripción de la garantía:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_descripcion_pren" runat="server" ControlToValidate="txt_descripcion_pren"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_pren">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_responsable" runat="server" class="text_input_nice_input"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Responsable:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_responsable" runat="server" ControlToValidate="txt_responsable"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_pren">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_responsable" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_responsable" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>                            
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_cp_pren" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" OnTextChanged="txt_cp_pren_TextChanged"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Código Postal:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_cp_pren"
                                                CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_pren">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" Enabled="True"
                                                TargetControlID="txt_cp_pren" FilterType="Numbers">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div>
                                        <asp:ImageButton ID="img_glass_pren" runat="server" ImageUrl="~/img/img/glass.png" Style="height: 16px" AutoPostBack="False" />
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_estado_pren" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Estado: </span>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_municipio_pren" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Municipio: </span>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_asentamiento_pren" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Asentamiento: </span>                                            
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class= "module_subsec low_m columned three_columns flex_start">
                                <div class="module_subsec_elements" style="flex:1;">
                                    <div class="text_input_nice_div module_subsec_elements_content">

                                        <asp:TextBox ID="txt_calle_pren" class="text_input_nice_input" runat="server"></asp:TextBox>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Calle y número:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle_pren" runat="server" ControlToValidate="txt_calle_pren"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_pren">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_calle_pren" runat="server" Enabled="True"
                                                FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_calle_pren">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">

                                    </div>
                                </div>
                            </div>

                            <!--REFERENCIAS-->
                            <div class="module_subsec">
                                <div class="module_subsec_elements w_100">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_referencias_pren" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Referencias:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_referencias_pren" runat="server" ControlToValidate="txt_referencias_pren"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_pren">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_referencias_pren" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias_pren" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_marca" CssClass="text_input_nice_input" MaxLength="100" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_marca" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_marca">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Marca:</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_modelo" CssClass="text_input_nice_input" MaxLength="50" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_moelo" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_modelo">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Modelo:</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_año" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_año" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_año">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Año:</span>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="module_subsec columned three_columns ">

                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_uso" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_uso" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_uso">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Uso (Horas, Km, Años):</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_demanda" CssClass="text_input_nice_input" MaxLength="10" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_demanda" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_demanda">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">Demanda para vender:</span>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="module_subsec">
                                <div class="module_subsec_elements w_100">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_tipo" CssClass="text_input_nice_input" MaxLength="500" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&" TargetControlID="txt_tipo">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Estado del vehículo / Tipo (Para que se usa): </span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_valor_factura" class="text_input_nice_input" runat="server" MaxLength="300"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Valor Factura:</span>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_valor_factura"
                                                runat="server" ControlToValidate="txt_valor_factura" CssClass="textogris"
                                                ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>


                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_aforo_pren" class="text_input_nice_input" runat="server" MaxLength="300"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Aforo:</span>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Aforo_pren"
                                                runat="server" ControlToValidate="txt_aforo_pren" CssClass="textogris"
                                                ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_status_pren" runat="server" Text="" CssClass="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_guardar_pren" class="btn btn-primary" runat="server" ValidationGroup="val_pren" Text="Guardar" />
                            </div>                       

                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel> 

                </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_guardar_pren" />
                <asp:AsyncPostBackTrigger ControlID="btn_guardar_hip" />
            </Triggers>
        </asp:UpdatePanel>
          

</asp:Content>


