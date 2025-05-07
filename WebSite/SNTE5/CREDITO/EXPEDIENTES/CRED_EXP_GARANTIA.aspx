<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_GARANTIA.aspx.vb" Inherits="SNTE5.CRED_EXP_GARANTIA" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

            <section class="panel" > 
                <header class="panel-heading">
                    <span>Garantías asignadas</span>
                </header>                         
                 
                <asp:Panel class="panel-body" ID="pnl_garantias" runat="server">
                   
                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex:1" > 
                            <asp:DataGrid ID="DAG_garantias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />                            
                                <Columns>
                                    <asp:BoundColumn DataField="idtipo" HeaderText="Id tipo">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="cvegarantia" HeaderText="Clave de garantía">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="idgarantia" HeaderText="Id garantía">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tipo" HeaderText="Tipo de garantía">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="garantia" HeaderText="Garantía">
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="valor" HeaderText="Valor($)">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="cobertura" HeaderText="% Cobertura">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="DETALLE" Text="Detalle">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:ButtonColumn>
                                </Columns>                             
                            </asp:DataGrid>
                        </div>
                    </div>
                                        
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_mensaje" runat="server" CssClass="alerta"></asp:Label>
                    </div>
                               
                </asp:Panel>
                
            </section>
            

            <asp:Panel ID="pnl_detalle" runat ="server" Visible ="false">
                <section class="panel"> 
                    <header class="panel-heading">
                    <span>Detalle de garantías</span>
                    </header>                         
                    <div class="panel-body">
                     <div class="panel-body_content" runat="server" id="content_panel_generales">
                       
                       <%--  <asp:Panel ID = "pnl_detalle" runat="server" HorizontalAlign="Left">--%>
                               <h4 style="font-weight: normal" class="module_subsec resalte_azul">Detalles de la garantía:</h4>
                         <div class="module_subsec flex_end">   
                             <asp:LinkButton ID="lnk_foto" runat="server" Cssclass="textoRojo" Text="Visualizar foto de garantía"
                              Enabled="false"></asp:LinkButton>&nbsp;&nbsp; 
                             <asp:LinkButton ID="btn_editar_foto" runat="server" Cssclass="textoRojo" Text="Editar foto de garantía"
                              Enabled="TRUE"></asp:LinkButton>
                      
                        </div>
                        

               <!-- Primera sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!--CLAVE CATASTRAL -->
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



                   <!--FOLIO REAL -->
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



                 <!-- CP -->
                 <div class="module_subsec_elements align_items_flex_end"> 
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" ></asp:TextBox>
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





            <!-- Segunda sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!-- Estado -->
                 <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_Estado" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled ="false"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Estado: </span>
                                            </div>
                                        </div>
                                    </div>





                   <!-- Municipio -->
                  <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="false"
                                                AutoPostBack="True"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Municipio: </span>
                                            </div>
                                        </div>
                                    </div>


                 <!-- Asentamiento  -->
               <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_asentamiento" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="false"
                                                ></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label title_tag">Asentamiento: </span>
                                            </div>
                                        </div>
                                    </div>
            </div>
                       
                             

            <!-- Tercera sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!-- Fecha de escrituración -->
                   <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                     <asp:TextBox ID="txt_calle_inm" class="text_input_nice_input" runat="server" MaxLength="100" ></asp:TextBox>
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



                   <!-- INMUEBLE -->
                  <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_inm" runat="server" class="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Inmueble:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_inm" runat="server" ControlToValidate="txt_inm"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_hip">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_itxt_inm" runat="server" Enabled="True" 
                                                    FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_inm" 
                                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.,/,-,(,),[,],#,$,%,&"></ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>


                 <!-- Valor de garantia  -->
              <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_monto_hip" CssClass="text_input_nice_input" MaxLength="12" runat="server" Enabled="true"></asp:TextBox>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto_hip" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_monto_hip">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Valor de garantia ($): </span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_monto_hip" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto_hip" Display="Dynamic"
                                            runat="server" ControlToValidate="txt_monto_hip" CssClass="textogris"
                                            ErrorMessage=" Error:Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
            </div>




                             <!--  Referencia del inmueble  -->
             <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_referencias_inm" runat="server" class="text_input_nice_textarea" TextMode="MultiLine" Maxlenght="500"></asp:TextBox>
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

             <!-- Cuarta sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!-- Propietario -->
                <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_propietario" CssClass="text_input_nice_input" MaxLength="300" runat="server"></asp:TextBox>
                                           
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Propietario:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_propietario" runat="server" ControlToValidate="txt_propietario" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PROPIETARIO" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_propietario">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>



                   <!-- Tipo de relación con el acreditado -->
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


                 <!--  Sexo -->
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



            <!-- Quinta sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!-- Fecha de nacimiento de garante  -->
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



                   <!--  RFC DEL GARANTE  -->
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


                 <!-- Telefono -->
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
                
                
               <!-- Sexta sección -->
             <div class="module_subsec low_m columned three_columns">
                   <!-- Estado civil -->
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




                   <!-- Nombre del conyuge: -->
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
             
              
              <!-- Descripción del inmueble -->
               <div class="module_subsec">
                                    <div class="module_subsec_elements w_100">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_descripcion_gtia" runat="server" class="text_input_nice_textarea" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
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
                                           
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Nombre del notario:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre_notario" runat="server" ControlToValidate="txt_nombre_notario" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_hip"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NOMBRE_NOTARIO" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre_notario">
                                        </ajaxToolkit:FilteredTextBoxExtender>
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
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_antiguedad" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_antiguedad">
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
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_aforo" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_aforo">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Aforo:</span>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_Aforo"
                                                    runat="server" ControlToValidate="txt_aforo" CssClass="textogris"
                                                    ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
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
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAVALUO" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_valor_avaluo">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Valor avalúo:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_valor_avaluo" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_valor_avaluo" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_hip" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_valor_avaluo"
                                                    runat="server" ControlToValidate="txt_valor_avaluo" CssClass="textogris"
                                                    ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                               
                                            </div>
                                        </div>
                                    </div>
      
                                </div>
                         <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                          <div class="module_subsec flex_end">
                                        <asp:Button ID="btn_guardar_hip" class="btn btn-primary" runat="server" OnClick="btn_guardar_hip_Click" ValidationGroup="val_hip" AutoPostBack="true" Text="Guardar" />                                   
                                </div>
                        
     </div>
 </section>
</asp:Panel>
</asp:Content>