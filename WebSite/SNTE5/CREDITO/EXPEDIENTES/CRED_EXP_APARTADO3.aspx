<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO3.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO3" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
            <%-- panel datos expediente--%>
    <section class="panel" >
        <%-- header del panel--%>
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <%-- cuerpo del panel--%>
        <div class="panel-body">
            <%-- contenido del panel--%>
            <div class="panel-body_content">
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
    </section>

    <div class="module_subsec">
        <h4 style="font-weight:normal;"  class="module_subsec_elements no_tbm" >Ganancias Mensuales:</h4>
        <asp:Label ID="lbl_gananciasMesuales" runat="server"  CssClass="module_subsec_elements  to_title_fix no_tbm"   Font-Bold="true" Text="$00"></asp:Label>          
    </div>

    <%-- panel ingresos mensuales--%>
    <section class="panel" >
        <%-- header del panel--%>
            <header class="panel_header_folder panel-heading">
                <span  class="panel_folder_toogle_header" >Ingresos mensuales</span>
                <span class="panel_folder_toogle down">&rsaquo;</span>
            </header>
            <%-- cuerpo del panel--%>
        <div class="panel-body">
            <%-- contenido del panel--%>
            <div class="panel-body_content init_show"> 
                <div class="module_subsec  no_column">
                    <div class="module_subsec_elements no_lm module_subsec_free-elements">
                                    
                            <div class="module_subsec no_sm no_column">
                                <h4 style="font-weight:normal">Titular:</h4>
                            </div>
                            <div class="module_subsec  no_m  columned">
                                <span  class="module_subsec_elements title_tag">Empleo:</span>
                                <asp:Label ID="lbl_montoempleo" runat="server" CssClass="module_subsec_elements module_subsec_free-elements"  Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec no_m  columned">
                                <span  class="module_subsec_elements title_tag">Adicional:</span>
                                <asp:Label ID="lbl_montoadicional" runat="server" CssClass="module_subsec_elements module_subsec_free-elements"  Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec no_m  columned">
                                <span  class="module_subsec_elements title_tag">Otros:</span>
                                <asp:Label ID="lbl_montootros" runat="server" CssClass="module_subsec_elements module_subsec_free-elements" Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec no_m  columned">
                                <span style="font-weight: bold;"  class="module_subsec_elements title_tag align_items_flex_center">Sub-Total:</span>
                                <asp:Label ID="lbl_montosub" runat="server" CssClass="module_subsec_elements module_subsec_free-elements" Text="$00"></asp:Label>
                            </div>
                                   
                    </div>
                    <asp:Panel CssClass="module_subsec_elements  module_subsec_free-elements" ID="pnl_codeudor" Visible="false" runat="server">
                                   
                            <div class="module_subsec  no_column">
                                <h4 style="font-weight:normal">Codeudor:</h4>
                            </div>
                            <div class="module_subsec  low_m  columned">
                                <span  class="module_subsec_elements title_tag">Empleo:</span>
                                <asp:Label ID="lbl_mempleocony" runat="server" CssClass="module_subsec_elements module_subsec_free-elements"  Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec low_m columned">
                                <span  class="module_subsec_elements title_tag">Adicional:</span>
                                <asp:Label ID="lbl_montoadicionalcony" runat="server" CssClass="module_subsec_elements module_subsec_free-elements"  Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec low_m  columned">
                                <span  class="module_subsec_elements title_tag">Otros:</span>
                                <asp:Label ID="lbl_montootroscony" runat="server" CssClass="module_subsec_elements module_subsec_free-elements" Text="$00"></asp:Label>
                            </div>
                            <div class="module_subsec low_m columned">
                                <span style="font-weight: bold;"  class="module_subsec_elements title_tag">Sub-Total:</span>
                                <asp:Label ID="lbl_subcony" runat="server" CssClass="module_subsec_elements module_subsec_free-elements" Text="$00"></asp:Label>
                            </div>
                                    
                    </asp:Panel>
                        <div class="module_subsec_elements  module_subsec_free-elements">
                                    
                            <div class="module_subsec low_m columned">
                                <h4 style="font-weight:normal;margin-right:10px"  class="module_subsec_elements module_subsec_free-elements">Total de Ingresos:</h4>
                                <asp:Label ID="lbl_montototal" runat="server"  CssClass="module_subsec_elements module_subsec_free-elements to_title_fix" Text="$00"></asp:Label>
                            </div>
                                    
                    </div>
                    </div>
                </div>
            </div>
                  
    </section>
    <%-- panel egresos mensuales--%>
    <section class="panel" >
        <%-- header del panel--%> 
            <header class="panel-heading panel_header_folder">
                <span  class="panel_folder_toogle_header" >Egresos mensuales</span>
                <span class="panel_folder_toogle down">&rsaquo;</span>
            </header>
            <%-- cuerpo del panel--%>
        <div class="panel-body">
            <%-- contenido del panel--%>
            <div class="panel-body_content init_show">
                <asp:Panel CssClass="module_subsec columned top_m five_columns" ID="pnl_egresosInputs" runat="server"></asp:Panel>
                <div style="justify-content:space-between" class="module_subsec" >
                    <div class="module_subsec_elements ">
                        <div class="module_subsec no_column no_m align_items_flex_center">
                            <h4 style="font-weight:normal;"  class="module_subsec_elements">Gastos Mensuales:</h4>
                            <asp:Label ID="lbl_gastosMensuales" runat="server"  CssClass="module_subsec_elements to_title_fix"  Font-Bold="true" Text="$00"></asp:Label>
                        </div>
                    </div>                           
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_mensageEstado" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec flex_end">                         
                    <asp:Button ID="btn_guardarEgresos" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                </div>
            </div>
        </div>
    </section>
    <%-- panel activos--%>
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Activos</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">

                <asp:UpdatePanel runat="server" ID="upnl_generales" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--EFECTIVO !-->
                    <div class="module_subsec" runat="server">
                        <asp:Label ID="Label5" runat="server" class="subtitulos" Text="Efectivo:"></asp:Label>
                    </div>

                    <div class="module_subsec shadow">
                        <asp:DataGrid ID="DAG_efectivo" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CVE_EFECTIVO"  visible ="false" HeaderText="Clave Referencia" >
                                    <ItemStyle Width="5px"/>
                                </asp:BoundColumn>   
                                <asp:BoundColumn DataField="INSTITUCION" HeaderText="Institución">
                                    <ItemStyle Width="30%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CTA" HeaderText="No. cuenta">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle  Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>        
                        </asp:DataGrid>
                    </div>
                                            
                    <!--Nueva cuenta efectivo!-->
                    <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" CollapseControlID="HeaderPanel_nuevabusqueda"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="HeaderPanel_nuevabusqueda" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_nuevabusqueda" SuppressPostBack="true"
                    TargetControlID="pnl_nuevoAcEfectivo">
                    </AjaxToolkit:CollapsiblePanelExtender>
                   
                    <asp:Panel ID="HeaderPanel_nuevabusqueda" runat="server" Style="cursor: pointer;">
                        <div class="texto">
                            <asp:ImageButton ID="ToggleImage_nuevabusqueda" runat="server" />
                             Nueva cuenta efectivo                   
                        </div>
                    </asp:Panel> 

                    <asp:Panel ID="pnl_nuevoAcEfectivo" runat="server" >                        
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_institucion_Efe" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_institucion_Efe" runat="server" CssClass="text_input_nice_label" Text="*Institución:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_institucion_Efe" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucion_Efe" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="Req_institucion_Efe" runat="server" ControlToValidate="txt_institucion_Efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_Efectivo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nocta_efe" runat="server" class="text_input_nice_input" MaxLength="20"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_nocta_efe" runat="server" CssClass="text_input_nice_label" Text="*No. cuenta:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nocta_efe" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_nocta_efe"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nocta_efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_Efectivo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldo_efe" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_saldo_efe" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldo_efe" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_saldo_efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_Efectivo"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto"
                                            runat="server" ControlToValidate="txt_saldo_efe" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
        
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcEfectivo" runat="server" class="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcEfectivo" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Ac_Efectivo"/>
                        </div>
        
                    </asp:Panel> 
                    <br />

                    <!--CUENTAS POR COBRAR!-->
                    <div class="module_subsec" runat="server">
                         <asp:Label ID="Label6" runat="server" class="subtitulos" Text="Cuentas por cobrar (No documentadas):"></asp:Label>
                    </div> 

                    <div class="module_subsec shadow">
                        <asp:DataGrid ID="DAG_CXCnoDOC" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CVE_DEUDOR"  visible ="false" HeaderText="Clave Deudor" >
                                    <ItemStyle Width="5px"/>
                                </asp:BoundColumn>   
                                <asp:BoundColumn DataField="DEUDOR" HeaderText="Deudor">
                                    <ItemStyle Width="35%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHAVENC" HeaderText="Fecha vencimiento">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>        
                        </asp:DataGrid>
                    </div>
                         
                    <!--Nueva cuenta por cobrar!-->
                    <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="pnl_ingreso_CXC"
                        Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                        Enabled="True" ExpandControlID="pnl_ingreso_CXC" ExpandedImage="~/img/collapse.jpg"
                        ExpandedText="Contraer" ImageControlID="toggle_CXC" SuppressPostBack="true"
                        TargetControlID="pnl_nuevoAcCXCnoDOC">
                    </AjaxToolkit:CollapsiblePanelExtender>
                   
                    <asp:Panel ID="pnl_ingreso_CXC" runat="server" Style="cursor: pointer;">
                        <div class="texto">
                            <asp:ImageButton ID="toggle_CXC" runat="server" />
                                Nueva cuenta por cobrar (No documentada)                    
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnl_nuevoAcCXCnoDOC" runat="server" >
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_deudorCXCNoDoc" runat="server" class="text_input_nice_input" MaxLength="200" ></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label7" runat="server" CssClass="text_input_nice_label" Text="*Deudor:"></asp:Label>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucion_Efe" 
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                            </AjaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_deudorCXCNoDoc"
                                                Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="Val_Ac_CXCNoDoc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fechaVencCXCNoDoc" runat="server" class="text_input_nice_input" MaxLength="10"/>
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_fecha_venCXCNoDoc" class="text_input_nice_label">*Fecha venc.(dd/mm/aaaa):</label> 
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaVencCXCNoDoc" runat="server" CultureAMPMPlaceholder="" 
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaVencCXCNoDoc" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaVencCXCNoDoc" runat="server" ControlExtender="MaskedEditExtender_fechaVencCXCNoDoc" Display="Dynamic"
                                            ControlToValidate="txt_fechaVencCXCNoDoc" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechaVencCXCNoDoc" InvalidValueMessage="Fecha inválida"/>
                                        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaVencCXCNoDoc" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_fechaVencCXCNoDoc"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="Val_Ac_CXCNoDoc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_SaldoAcCXCnoDOC" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label9" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" FilterType="Numbers, Custom" 
                                            ValidChars="." TargetControlID="txt_SaldoAcCXCnoDOC"></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_SaldoAcCXCnoDOC"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="Val_Ac_CXCNoDoc"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            runat="server" ControlToValidate="txt_SaldoAcCXCnoDOC" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
   
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcCXCnoDOC" runat="server" class="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcCXCnoDOC" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup ="Val_Ac_CXCNoDoc"/>
                        </div>
                  
                    </asp:Panel>  
                    <br />
                    
                    <!--DOCUMENTOS POR COBRAR!-->
                    <div class="module_subsec" runat="server">
                        <asp:Label ID="Label8" runat="server" class="subtitulos" Text="Documentos por cobrar (Pagarés y/o contratos):"></asp:Label>
                    </div>

                    <div class="module_subsec shadow">
                        <asp:DataGrid ID="DAG_CXCDoc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header"/>
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CVE_DEUDORCXCDOC"  visible ="false" HeaderText="Clave Referencia" >
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>   
                                <asp:BoundColumn DataField="DEUDOR" HeaderText="Deudor">
                                    <ItemStyle Width="35%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHAVENC" HeaderText="Fecha vencimiento">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>        
                        </asp:DataGrid>
                    </div>

                    <!--Nuevo Documento por cobrar!-->
                    <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" CollapseControlID="HeaderPanel__CxCDoc"
                        Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                        Enabled="True" ExpandControlID="HeaderPanel__CxCDoc" ExpandedImage="~/img/collapse.jpg"
                        ExpandedText="Contraer" ImageControlID="ToggleImage_CxCDoc" SuppressPostBack="true"
                        TargetControlID="pnl_nuevoAcCXCDOC">
                    </AjaxToolkit:CollapsiblePanelExtender>
               
                    <asp:Panel ID="HeaderPanel__CxCDoc" runat="server" Style="cursor: pointer;">
                        <div class="texto">
                            <asp:ImageButton ID="ToggleImage_CxCDoc" runat="server" />
                             Nuevo documento por cobrar (Pagaré y/o contrato)                   
                        </div>
                   </asp:Panel>

                    <asp:Panel ID="pnl_nuevoAcCXCDOC" runat="server" > 
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_DedudorCXCDoc" runat="server" class="text_input_nice_input" MaxLength="200" ></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label10" runat="server" CssClass="text_input_nice_label" Height="16px" Text="*Deudor:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DedudorCXCDoc" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_DedudorCXCDoc" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_DedudorCXCDoc"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXCDoc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_fechaVencCXCDoc" runat="server" class="text_input_nice_input" MaxLength="10"/>
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_fecha_venCXCDoc" class="text_input_nice_label">*Fecha venc.(dd/mm/aaaa):</label>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaVencCXCDoc" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaVencCXCDoc" />
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaVencCXCDoc" runat="server" ControlExtender="MaskedEditExtender_fechaVencCXCDoc" Display="Dynamic"
                                            ControlToValidate="txt_fechaVencCXCDoc" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechaVencCXCDoc" InvalidValueMessage="Fecha inválida" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechaVencCXCDoc" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_fechaVencCXCDoc"
                                             Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXCDoc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_SaldoAcCXCDOC" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label11" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_SaldoAcCXCDOC" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_SaldoAcCXCDOC"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXCDoc"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            runat="server" ControlToValidate="txt_SaldoAcCXCDOC" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcCXCDoc" runat="server" class="alerta"></asp:Label>
                       </div>
        
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcCXCDoc" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup ="val_Ac_CXCDoc"/>
                        </div>                    
                  
                    </asp:Panel>  
                    <br />

                    <!--HIPOTECAS Y FIDEICOMISOS!-->
                    <div class="module_subsec">
                        <asp:Label ID="Label12" runat="server" class="subtitulos" Text="Hipotecas y fideicomisos a favor:"></asp:Label>
                    </div>

                    <div class="module_subsec shadow">
                        <asp:DataGrid ID="DAG_HipFid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header"/>
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CVE_HIPFID"  visible ="false" HeaderText="Clave Hipoteca-Fideicomiso" >
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>   
                                <asp:BoundColumn DataField="DEUDORHIPFID" HeaderText="Deudor">
                                    <ItemStyle Width="35%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PORCPROPHIPFID" HeaderText="% Propiedad">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>        
                        </asp:DataGrid>
                    </div>
         
                    <!--Nueva hipoteca o fideicomiso!-->
                    <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" CollapseControlID="HeaderPanel__HipFid"
                        Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                        Enabled="True" ExpandControlID="HeaderPanel__HipFid" ExpandedImage="~/img/collapse.jpg"
                        ExpandedText="Contraer" ImageControlID="ToggleImage_HipFid" SuppressPostBack="true"
                        TargetControlID="pnl_nuevoAcHipFid">
                    </AjaxToolkit:CollapsiblePanelExtender>
                
                    <asp:Panel ID="HeaderPanel__HipFid" runat="server" Style="cursor: pointer;">
                        <div class="texto">
                            <asp:ImageButton ID="ToggleImage_HipFid" runat="server" />
                             Nueva hipoteca o fideicomiso a favor
                        </div>
                   </asp:Panel>

                    <asp:Panel ID="pnl_nuevoAcHipFid" runat="server" > 
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_DedudorHipFid" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label13" runat="server" CssClass="text_input_nice_label" Text="*Deudor:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DedudorHipFid" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_DedudorHipFid" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_DedudorHipFid"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_HipFid"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_porcPropHipFid" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_porcePropHipFid" class="text_input_nice_label">*% Propiedad:</label>
                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_porcPropHipFid">
                                           </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_porcPropHipFid"
                                             Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_HipFid"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_SaldoHipFid" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label14" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_SaldoHipFid" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_SaldoHipFid"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_HipFid"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                            runat="server" ControlToValidate="txt_SaldoHipFid" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcHipFid" runat="server" class="alerta"></asp:Label>
                       </div>
                        
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcHipFid" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup ="val_Ac_HipFid"/>
                        </div>
                  
                    </asp:Panel>
                    <br />
                    
                    <!--INVERSIONES!-->
                    <div class="module_subsec">
                        <asp:Label ID="Label15" runat="server" class="subtitulos" Text="Inversiones en acciones:"></asp:Label>
                    </div>

                    <div class="module_subsec shadow">
                        <asp:DataGrid ID="DAG_InvAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header"/>
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CVE_EMPINVACC"  visible ="false" HeaderText="Clave Referencia" >
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>   
                                <asp:BoundColumn DataField="EMPRESAHIPFID" HeaderText="Empresa">
                                    <ItemStyle Width="35%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PORCPRARTINVACC" HeaderText="% Participación">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
        
        </asp:DataGrid>
                    </div>
                    <!--Nueva inversion!-->
                    <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="server" CollapseControlID="HeaderPanel__InvAcc"
                        Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                        Enabled="True" ExpandControlID="HeaderPanel__InvAcc" ExpandedImage="~/img/collapse.jpg"
                        ExpandedText="Contraer" ImageControlID="ToggleImage_InvAcc" SuppressPostBack="true"
                        TargetControlID="pnl_nuevoAcInvAcc">
                    </AjaxToolkit:CollapsiblePanelExtender>
                
                    <asp:Panel ID="HeaderPanel__InvAcc" runat="server" Style="cursor: pointer;">
                        <div class="texto">
                            <asp:ImageButton ID="ToggleImage_InvAcc" runat="server" />
                             Nueva inversión en acciones                   
                        </div>
                    </asp:Panel>
                     
                    <asp:Panel ID="pnl_nuevoAcInvAcc" runat="server" > 
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_DedudorInvAcc" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label16" runat="server" CssClass="text_input_nice_label" Text="*Empresa:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DedudorInvAcc" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_DedudorInvAcc" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_DedudorInvAcc"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_InvAcc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_PorcInvAcc" runat="server" class="text_input_nice_input" MaxLength="5"/>
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_pro_venInvAcc" class="text_input_nice_label">* % Participación:</label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Numbers, Custom" 
                                            ValidChars="." TargetControlID="txt_PorcInvAcc"></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_PorcInvAcc"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_InvAcc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldoInvAcc" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label17" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldo_efe" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_saldoInvAcc"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_InvAcc"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                            runat="server" ControlToValidate="txt_saldoInvAcc" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                   
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcInvAcc" runat="server" class="alerta"></asp:Label>
                       </div>
        
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcInvAcc" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Ac_InvAcc"/>
                        </div>                    
                  
                    </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <%-- panel pasivos--%>
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Pasivos</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">

                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                <!--CUENTAS POR PAGAR !-->
                <div class="module_subsec">
                    <asp:Label ID="Label18" runat="server" class="subtitulos" Text="Cuentas por pagar (No documentadas):"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_CXPnoDOC" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVE_INSTITUCION"  visible ="false" HeaderText="Clave Institucion" >
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="INSTITUCION" HeaderText="Acreedor">
                                <ItemStyle Width="35%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOCRED" HeaderText="Tipo de préstamo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>
        
                    </asp:DataGrid>
                </div>

                <!--Nueva cuenta por pagar!-->
                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="server" CollapseControlID="pnl_ingreso_CXPNODOC"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_CXPNODOC" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="toggle_CXPNODOC" SuppressPostBack="true"
                    TargetControlID="pnl_nuevoAcCXPnoDOC">
                </AjaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="pnl_ingreso_CXPNODOC" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="toggle_CXPNODOC" runat="server" />
                         Nueva cuenta por pagar (No documentada)
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_nuevoAcCXPnoDOC" runat="server" >
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_acreedorCXPNoDoc" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="Label19" runat="server" CssClass="text_input_nice_label" Text="*Acreedor:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_acreedorCXPNoDoc" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_acreedorCXPNoDoc"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPNoDoc"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tipoCredCXPNoDoc" runat="server" class="text_input_nice_input" MaxLength="200"/>
                                <div class="text_input_nice_labels">
                                     <asp:label runat="server" id="lbl_tipoCredCXPNoDoc" class="text_input_nice_label" text="*Tipo de préstamo"></asp:label> 
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_tipoCredCXPNoDoc" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" >
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_tipoCredCXPNoDoc"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPNoDoc"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_SaldoCXPnoDOC" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldoCXPNoDoc" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_SaldoCXPnoDOC">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_SaldoCXPnoDOC"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPNoDoc"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                        runat="server" ControlToValidate="txt_SaldoCXPnoDOC" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                        
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_AcCXPnoDOC" runat="server" class="alerta"></asp:Label>
                    </div>
        
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guarda_CXPnoDOC" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Ac_CXPNoDoc"></asp:Button>
                    </div>                    
                  
                </asp:Panel>
                <br />

                <!--DOCUMENTOS POR PAGAR !-->
                <div class="module_subsec">
                    <asp:Label ID="Label21" runat="server" class="subtitulos" Text="Documentos por pagar (Pagarés y/o contratos):"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_CXPdoC" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVE_INSTITUCION"  visible ="false" HeaderText="Clave Institucion" >
                                <ItemStyle Width="5px" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="INSTITUCION" HeaderText="Acreedor">
                                <ItemStyle Width="35%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOCRED" HeaderText="Tipo de préstamo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>
        
                    </asp:DataGrid>
                </div>

                <!--Nuevo documento por pagar!-->
                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="server" CollapseControlID="pnl_ingreso_CXPDOC"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_CXPDOC" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="toggle_CXPDOC" SuppressPostBack="true"
                    TargetControlID="pnl_nuevoAcCXPdoC">
                </AjaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="pnl_ingreso_CXPDOC" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="toggle_CXPDOC" runat="server" />
                         Nuevo documento por pagar (Pagaré y/o contrato)
                    </div>
                </asp:Panel> 

                <asp:Panel ID="pnl_nuevoAcCXPdoC" runat="server" >
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_acreedorCXPDoC" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_acreedorCXPdoc" runat="server" CssClass="text_input_nice_label" Text="*Acreedor:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_acreedorCXPDoC" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_acreedorCXPDoC"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPDoc"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tipoCredCXPDoC" runat="server" class="text_input_nice_input" MaxLength="200"/>
                                <div class="text_input_nice_labels">
                                    <asp:label runat="server" id="lbl_tipocredCXPDoC" class="text_input_nice_label" Text="*Tipo de préstamo:"></asp:label>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                         FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_tipoCredCXPDoC" 
                                         ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" ></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_tipoCredCXPDoC"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPDoc"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_SaldoCXPdoC" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldoCXPDoc" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="txt_SaldoCXPdoC"></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_SaldoCXPdoC"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Ac_CXPDoc"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                                        runat="server" ControlToValidate="txt_SaldoCXPdoC" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
   
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_AcCXPdoC" runat="server" class="alerta"></asp:Label>
                    </div>         
                    
                    <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guarda_CXPdoC" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Ac_CXPDoc"/>
                    </div>
                  
                </asp:Panel> 
                <br />

                <!--IMPUESTOS POR PAGAR !-->
                <div class="module_subsec">
                    <asp:Label ID="Label24" runat="server" class="subtitulos" Text="Impuestos por pagar:"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_IxP" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVE_INSTITUCION"  visible ="false" HeaderText="Clave Institucion" >
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="INSTITUCION" HeaderText="Acreedor">
                                <ItemStyle Width="35%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOCRED" HeaderText="Tipo de préstamo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>
                
                <!--Nuevo impuesto por pagar!-->
                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" runat="server" CollapseControlID="pnl_ingreso_IxP"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_IxP" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_IxP" SuppressPostBack="true"
                    TargetControlID="pnl_nuevoIxP">
                </AjaxToolkit:CollapsiblePanelExtender>
                
                <asp:Panel ID="pnl_ingreso_IxP" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_IxP" runat="server" />
                         Nuevo impuesto por pagar                   
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_nuevoIxP" runat="server" >
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_acreedorIxP" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_acreedorIXP" runat="server" CssClass="text_input_nice_label" Text="*Acreedor:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_acreedorIxP" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_acreedorIxP"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_IxP"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tipocredIxP" runat="server" class="text_input_nice_input" MaxLength="200"/>
                                <div class="text_input_nice_labels">
                                    <asp:label runat="server" id="lbl_tipocredixp" class="text_input_nice_label" Text="*Tipo de préstamo:"></asp:label> 
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tipocredIxP" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_tipocredIxP" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_tipocredIxP"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_IxP"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_saldoIxP" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldoIXP" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldoIxP" >
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_saldoIxP"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_IxP"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                        runat="server" ControlToValidate="txt_saldoIxP" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusIxP" runat="server" class="alerta"></asp:Label>
                    </div>
       
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardaIxP" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_PAS_IxP"/>
                    </div>                    
                  
                </asp:Panel> 
                <br />

                <!--PASIVOS CONTINGENTES!-->
                <div class="module_subsec">
                    <asp:Label ID="Label30" runat="server" class="subtitulos" Text="Pasivos contingentes:"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_cntgnt" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVECNT"  visible ="false" HeaderText="Clave CNTGNT" >
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="TIPOCNT" HeaderText="Tipo contingente">
                                <ItemStyle Width="45%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle  Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>

                <!--Nuevo pasivo contingente!-->
                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender10" runat="server" CollapseControlID="pnl_nuevocnt"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_nuevocnt" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_cnt" SuppressPostBack="true"
                    TargetControlID="pnl_ingreso_cnt">
                </AjaxToolkit:CollapsiblePanelExtender>
               
                <asp:Panel ID="pnl_nuevocnt" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_cnt" runat="server" />
                         Nuevo pasivo contingente
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnl_ingreso_cnt" runat="server" > 
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tipoCnt" runat="server" class="text_input_nice_input" MaxLength="200"/>
                                <div class="text_input_nice_labels">
                                    <asp:label runat="server" id="lbl_cnttipo" class="text_input_nice_label" Text="*Tipo:"></asp:label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_tipoCnt" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_tipoCnt"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Cnt"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_saldoCnt" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldoCnt" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldoCnt" >
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_saldoCnt"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Cnt"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                        runat="server" ControlToValidate="txt_saldoCnt" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusCnt" runat="server" class="alerta"></asp:Label>
                    </div>                
        
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardaCnt" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Cnt"/>
                    </div>                   
                  
                </asp:Panel>
                <br />
    
                <!--OTROS PASIVOS !-->
                <div class="module_subsec">
                    <asp:Label ID="Label27" runat="server" class="subtitulos" Text="Otros pasivos:"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_Otros" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVE_INSTITUCION"  visible ="false" HeaderText="Clave Institucion" >
                                <ItemStyle Width="5%"/>
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="INSTITUCION" HeaderText="Acreedor">
                                <ItemStyle Width="35%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOCRED" HeaderText="Tipo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>

                <!--Nuevo pasivo!-->
                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="server" CollapseControlID="pnl_ingreso_otros"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_otros" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="ToggleImage_otros" SuppressPostBack="true"
                    TargetControlID="pnl_nuevootros">
                </AjaxToolkit:CollapsiblePanelExtender>
             
                <asp:Panel ID="pnl_ingreso_otros" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_otros" runat="server" />
                         Nuevo pasivo por pagar         
                    </div>
               </asp:Panel> 

                <asp:Panel ID="pnl_nuevootros" runat="server" > 
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_institucionOtros" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_institucionOtros" runat="server" CssClass="text_input_nice_label" Height="16px" Text="*Acreedor:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucionOtros" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_institucionOtros"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_Otros"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tipocredOtros" runat="server" class="text_input_nice_input" MaxLength="200"/>
                                <div class="text_input_nice_labels">
                                    <asp:label runat="server" id="lbl_tipocredotros" class="text_input_nice_label" Text="*Tipo:"></asp:label> 
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_tipocredOtros" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_tipocredOtros"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_Otros"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_saldoOtros" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldoOtros" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldoOtros" >
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_saldoOtros"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PAS_Otros"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9"
                                        runat="server" ControlToValidate="txt_saldoOtros" CssClass="textogris" ErrorMessage="Monto incorrecto"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusOtros" runat="server" class="alerta"></asp:Label>
                    </div>
        
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardaOtros" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_PAS_Otros"/>
                    </div>                   
                  
                </asp:Panel> 

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <%-- bienes inmuebles--%>   
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Bienes inmuebles</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="module_subsec">
                    <asp:Label ID="Label20" runat="server" class="subtitulos" Text="Bienes inmuebles:"></asp:Label>
                </div>

                <div class="module_subsec shadow" style="overflow:auto; height:100%;">
                    <asp:DataGrid ID="DAG_BienesIn" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVE_BIEN" visible ="false" HeaderText="Clave bien" >
                                <ItemStyle Width="5%"/>
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="TIPO" HeaderText="Tipo">
                                <ItemStyle Width="20%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CALLE" HeaderText="Calle y núm.">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ASENTAMIENTO" visible ="false" HeaderText="Asentamiento">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MUNICIPIO" HeaderText="Municipio">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CIUDAD" visible ="false" HeaderText="Ciudad">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CP" visible ="false" HeaderText="CP">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PCTPROP" HeaderText="Pctje. prop.">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INGRENTA" HeaderText="Ingreso renta">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALORM" HeaderText="Valor mercado">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUPT" visible ="false" HeaderText="Sup. terreno m2">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUPC" visible ="false" HeaderText="Sup. const. m2">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RPPC" HeaderText="Datos RPPC">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="GRAVADO" HeaderText="Gravamen">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACREEDOR1" HeaderText="Acreedor 1ero">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACREEDOR2" visible ="false" HeaderText="Acreedor 2o">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAZO" HeaderText="Plazo préstamo">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESTANTE" HeaderText="Plazo restante">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTO" HeaderText="Monto préstamo">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ABONO" HeaderText="Pago mensual">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TASA" HeaderText="Tasa interés">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHAV" HeaderText="Fecha venc.">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SALDO" HeaderText="Saldo restante">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALORN" HeaderText="Valor neto">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>

                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender11" runat="server" CollapseControlID="pnl_ingreso_bienin"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_bienin" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="Toggle_bienesin" SuppressPostBack="true" TargetControlID="pnl_nuevobienin">
                </AjaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="pnl_ingreso_bienin" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="Toggle_bienesin" runat="server" />
                         Nuevo bien inmueble
                    </div>
               </asp:Panel> 
                                 
                <asp:Panel ID="pnl_nuevobienin" runat="server" > 
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_bienin_tipo" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_bienin_tipo" runat="server" CssClass="text_input_nice_label" Height="16px" Text="Tipo de Inmueble:" Width="161px"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_bienin_tipo" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_cp" runat="server" CssClass="text_input_nice_label" Text="*CP:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server" 
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_cp">
                                    </AjaxToolkit:FilteredTextBoxExtender>                                    
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_cp"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class=" module_subsec_elements_content">
                                <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" />
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_estado" runat="server" class="btn btn-primary2 dropdown"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_estado" runat="server" CssClass="text_input_nice_label" Text="*Estado:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="cmb_estado"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_municipio" runat="server" AutoPostBack="True" class="btn btn-primary2 dropdown"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_municipio" runat="server" CssClass="text_input_nice_label" Text="*Municipio:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="cmb_municipio"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_colonia" runat="server" class="btn btn-primary2 dropdown"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_colonia" runat="server" CssClass="text_input_nice_label" Text="*Asentamiento:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="cmb_colonia"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_ciudad" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ciudad" runat="server" CssClass="text_input_nice_label" Text="Ciudad:"></asp:Label>            
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_ciudad" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_calle" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_calle" runat="server" CssClass="text_input_nice_label" Text="*Calle:"></asp:Label>
                                     <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_calle" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_calle"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_num_ext" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_num_ext" runat="server" CssClass="text_input_nice_label" Text="*Número exterior:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_num_ext" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txt_num_ext"
                                         Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_num_int" runat="server" class="text_input_nice_input" MaxLength="10"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_num_int" runat="server" CssClass="text_input_nice_label" Text="Número interior:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_num_int" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_pctprop" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_pctprop" runat="server" CssClass="text_input_nice_label" Text="Pctje. propiedad inmueble:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txt_pctprop" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_ingrenta" runat="server" class="text_input_nice_input" MaxLength="19"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ingrenta" runat="server" CssClass="text_input_nice_label" Text="Ingreso anual por renta:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txt_ingrenta" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_valorm" runat="server" class="text_input_nice_input" MaxLength="19"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_valorm" runat="server" CssClass="text_input_nice_label" Text="Valor de mercado:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txt_valorm" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_supert" runat="server" class="text_input_nice_input" MaxLength="19"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_supert" runat="server" CssClass="text_input_nice_label" Text="*Superficie terreno m2:"></asp:Label>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" FilterType="Custom,Numbers"
                                    Enabled="True" TargetControlID="txt_supert" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txt_supert"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_superc" runat="server" class="text_input_nice_input" MaxLength="19"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_superc" runat="server" CssClass="text_input_nice_label" Text="*Superficie construcción m2:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txt_superc" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txt_superc"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_valorn" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_valorn" runat="server" CssClass="text_input_nice_label" Text="*Valor neto:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" FilterType="Custom, Numbers"
                                        Enabled="True" TargetControlID="txt_valorn" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_valorn"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_rppc" runat="server" class="text_input_nice_input" MaxLength="500"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_rppc" runat="server" CssClass="text_input_nice_label" Text="Datos RPPC:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_rppc" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_gravamen" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true" ></asp:DropDownList> 
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_gravamen" runat="server" CssClass="text_input_nice_label" Text="*Inmueble gravado:"></asp:Label >              
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator63" CssClass="textogris" runat="server" ControlToValidate="cmb_gravamen" Display="Dynamic"
                                        ErrorMessage="Falta Dato!" ValidationGroup="val_BienesIn" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_acree1" runat="server" class="text_input_nice_input" MaxLength="200" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_acree1" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Acreedor 1er lugar:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_acree1" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_acree1"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_acree2" runat="server" class="text_input_nice_input" MaxLength="200" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_acree2" runat="server" CssClass="text_input_nice_label" visible="false" Text="Acreedor 2o lugar:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_acree2" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_plazo" runat="server" class="text_input_nice_input" MaxLength="9" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_plazo" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Plazo préstamo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_plazo"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_plazo"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_plazores" runat="server" class="text_input_nice_input" MaxLength="9" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_plazores" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Plazo restante:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_plazores"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_plazores"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_monto" runat="server" class="text_input_nice_input" MaxLength="18" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_monto" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Monto préstamo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" FilterType="Custom, Numbers"
                                        Enabled="True" TargetControlID="txt_monto" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_monto"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_abono" runat="server" class="text_input_nice_input" MaxLength="18" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_abono" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Pago mensual préstamo:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" FilterType="Custom, Numbers"
                                        Enabled="True" TargetControlID="txt_abono" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_abono"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>                      
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_tasa" runat="server" class="text_input_nice_input" MaxLength="6" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_tasa" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Tasa de interés:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server" FilterType="Custom, Numbers"
                                        Enabled="True" TargetControlID="txt_tasa" ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txt_tasa"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_fechav" runat="server" class="text_input_nice_input" MaxLength="10" visible="false"/>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_fechav" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Fecha vencimiento (dd/mm/aaaa):"></asp:Label>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechav" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechav" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender_fechav" ControlToValidate="txt_fechav" CssClass="textogris" 
                                        ErrorMessage="MaskedEditValidator_fechav" Display="Dynamic" InvalidValueMessage="Fecha inválida"  />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechav" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechav" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txt_fechav"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_saldo" runat="server" class="text_input_nice_input" MaxLength="18" visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_saldo" runat="server" CssClass="text_input_nice_label" visible="false" Text="*Saldo restante:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_saldo" 
                                        ValidChars="."></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="txt_saldo"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_BienesIn"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_bienin" runat="server" class="alerta"></asp:Label>
                    </div> 
        
                    <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guarda_bienin" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_BienesIn" AutoPostBack="true"/>
                    </div>                    
                  
          </asp:Panel>  
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section> 
    <%-- bienes muebles--%>   
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Bienes muebles</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="module_subsec">
                    <asp:Label ID="Label22" runat="server" class="subtitulos" Text="Bienes muebles:"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_bienesmu" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                             <asp:BoundColumn DataField="IDPERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVEBIEN"  visible ="false" HeaderText="Clave bien" >
                                <ItemStyle Width="5%"/>
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="MARCA" HeaderText="Marca">
                                <ItemStyle Width="30%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MODELO" HeaderText="Modelo">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AÑO" HeaderText="Año">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALORC" HeaderText="Valor comercial">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>

                 <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender12" runat="server" CollapseControlID="pnl_ingreso_bienmu"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_bienmu" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="Toggle_bienesmu" SuppressPostBack="true"
                    TargetControlID="pnl_nuevobienmu">
                </AjaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="pnl_ingreso_bienmu" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="Toggle_bienesmu" runat="server" />
                        Nuevo bien mueble
                    </div>
                </asp:Panel> 
                                   
                <asp:Panel ID="pnl_nuevobienmu" runat="server" >
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_marca" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_marca" runat="server" CssClass="text_input_nice_label" Text="*Marca:"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_marca" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txt_marca"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bienes"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_modelo" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_modelo" runat="server" CssClass="text_input_nice_label" Text="*Modelo:"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_modelo" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="txt_modelo"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bienes"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_ano" runat="server" class="text_input_nice_input" MaxLength="4"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ano" runat="server" CssClass="text_input_nice_label" Text="*Año:" Width="161px"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_ano" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txt_ano"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bienes"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                     
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_valorbienmu" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_valorbienmu" runat="server" CssClass="text_input_nice_label" Text="*Valor comercial:"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server"
                                        Enabled="True" FilterType="Numbers, Custom" TargetControlID="txt_valorbienmu" validchars=".">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="txt_valorbienmu"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bienes"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusbienmu" runat="server" class="alerta"></asp:Label>
                    </div>    
                                   
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardabienmu" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_Bienes"/>
                    </div>                                            
                </asp:Panel>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <%-- productos servicios y promoción--%>   
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span  class="panel_folder_toogle_header" >Productos, servicios y promoción</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                <ContentTemplate>

                <div class="module_subsec">
                    <asp:Label ID="Label23" runat="server" class="subtitulos" Height="16px" Text="Productos, servicios, publicidad y promoción"></asp:Label> 
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod1" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod1" runat="server" CssClass="text_input_nice_label" Text="*Nombre del producto o servicio (1):"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_prod1" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txt_prod1"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod1vta" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod1vtas" runat="server" CssClass="text_input_nice_label" Text="*Ventas último año ($):"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txt_prod1vta" validchars=".">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txt_prod1vta"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>                    
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod2" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod2" runat="server" CssClass="text_input_nice_label" Text="Nombre del producto o servicio (2):"></asp:Label>                                                                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server"
                                Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_prod2" 
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                </AjaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod2vta" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod2vta" runat="server" CssClass="text_input_nice_label" Text="Ventas último año ($):"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txt_prod2vta" validchars=".">
                                </AjaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod3" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod3" runat="server" CssClass="text_input_nice_label" Text="Nombre del producto o servicio (3):"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_prod3" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_prod3vta" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_prod3vta" runat="server" CssClass="text_input_nice_label" Text="Ventas último año ($):"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txt_prod3vta" validchars=".">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_procprod" runat="server" class="text_input_nice_input" MaxLength="500"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_procprod" runat="server" CssClass="text_input_nice_label" Text="*Proceso productivo o de servicios:"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_procprod" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txt_procprod"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_nuevosp" runat="server" class="text_input_nice_input" MaxLength="500"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_nuevosp" runat="server" CssClass="text_input_nice_label" Text="*Plan expansión o nuevos productos:"></asp:Label>                                                                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_nuevosp" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="txt_nuevosp"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_mercado" runat="server" class="text_input_nice_input" MaxLength="500"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_mercado" runat="server" CssClass="text_input_nice_label" Text="*Expectativas del mercado:"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_mercado" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txt_mercado"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
               
                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_promo" runat="server" class="btn btn-primary2 dropdown" AutoPostBack="true" ></asp:DropDownList> 
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_promo" runat="server" CssClass="text_input_nice_label" Text="*Realiza promoción de sus productos:"></asp:Label>                   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" CssClass="textogris" runat="server" ControlToValidate="cmb_promo" Display="Dynamic"
                                        ErrorMessage="Falta Dato!" ValidationGroup="val_Prod" InitialValue="-1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_promotipo" runat="server" class="text_input_nice_input" MaxLength="500" Visible="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_promotipo" runat="server" CssClass="text_input_nice_label" Text="*Tipo de promoción:" Visible="false"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_promotipo" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txt_promotipo"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_promomedios" runat="server" class="text_input_nice_input" MaxLength="500" Visible="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_promomedios" runat="server" CssClass="text_input_nice_label" Text="*Medios de publicidad:" Visible="false"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_promomedios" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txt_promomedios"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_promocosto" runat="server" class="text_input_nice_input" MaxLength="18" Visible="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_promocosto" runat="server" CssClass="text_input_nice_label" Text="*Costo promedio mensual($):" Visible="false"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txt_promocosto" validchars=".">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txt_promocosto"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Prod"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status_prodserv" runat="server" class="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guarda_prodserv" runat="server" class="btn btn-primary" text="Guardar" ValidationGroup="val_Prod"/>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
    <%-- adicionales--%>   
    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header"  >Préstamos adicionales</span>
            <span class="panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="module_subsec">
                    <asp:Label ID="Label25" runat="server" class="subtitulos" Text="Préstamos adicionales:"></asp:Label>
                </div>

                <div class="module_subsec shadow">
                    <asp:DataGrid ID="DAG_creditos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                             <asp:BoundColumn DataField="IDPERSONA" visible ="false">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CVECRED"  visible ="false" HeaderText="Clave cred" >
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>   
                            <asp:BoundColumn DataField="TIPOCRED" HeaderText="Tipo préstamos">
                                <ItemStyle Width="30%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INSTI" HeaderText="Institución">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TASA" HeaderText="Tasa">
                                <ItemStyle  Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHA" HeaderText="Fecha Apertura">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="GTIAS" HeaderText="Garantías">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>        
                    </asp:DataGrid>
                </div>

                <AjaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender13" runat="server" CollapseControlID="pnl_ingreso_credito"
                    Collapsed="true" CollapsedImage="~/img/expand.jpg" CollapsedText="Expandir"
                    Enabled="True" ExpandControlID="pnl_ingreso_credito" ExpandedImage="~/img/collapse.jpg"
                    ExpandedText="Contraer" ImageControlID="Toggle_credito" SuppressPostBack="true"
                    TargetControlID="pnl_nuevo_credito">
                </AjaxToolkit:CollapsiblePanelExtender>

                <asp:Panel ID="pnl_ingreso_credito" runat="server" Style="cursor: pointer;">
                    <div class="texto">
                        <asp:ImageButton ID="Toggle_credito" runat="server" />
                        Nuevo préstamo
                    </div>
                </asp:Panel> 
                                  
                <asp:Panel ID="pnl_nuevo_credito" runat="server" >
                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_tipo" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_tipo" runat="server" CssClass="text_input_nice_label" Text="*Tipo préstamo:"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_credito_tipo" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txt_credito_tipo"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cred"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_insti" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_insti" runat="server" CssClass="text_input_nice_label" Text="*Institución:"></asp:Label>                   
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender59" runat="server"
                                    Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_credito_insti" 
                                    ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txt_credito_insti"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cred"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_monto" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_monto" runat="server" CssClass="text_input_nice_label" Text="*Monto ($):"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender60" runat="server"
                                        Enabled="True" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_credito_monto" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="txt_credito_monto"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cred"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_plazo" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_plazo" runat="server" CssClass="text_input_nice_label" Text="Plazo (Meses):"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender61" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_credito_plazo">
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_tasa" runat="server" class="text_input_nice_input" MaxLength="6"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_tasa" runat="server" CssClass="text_input_nice_label" Text="Tasa (Anual):"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender62" runat="server"
                                        Enabled="True" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_credito_tasa" >
                                    </AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_Fecha" runat="server" class="text_input_nice_input" MaxLength="10"/>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_fecha" runat="server" CssClass="text_input_nice_label" Text="Fecha apertura (dd/mm/aaaa):"></asp:Label>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_creditofecha" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_credito_fecha" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender_creditofecha" ControlToValidate="txt_credito_fecha" 
                                        CssClass="textogris" ErrorMessage="MaskedEditValidator_creditofecha" Display="Dynamic" InvalidValueMessage="Fecha inválida"  />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_credito_fecha" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_credito_gtias" runat="server" class="text_input_nice_input" MaxLength="500"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_credito_gtias" runat="server" CssClass="text_input_nice_label" Text="Garantías:"></asp:Label>                   
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender63" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_credito_gtias" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>

                   <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_credito" runat="server" class="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guarda_credito" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_cred"/>
                    </div>
                            
                </asp:Panel> 
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
 </asp:Content>
