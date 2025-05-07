<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_CONDONACIONES.aspx.vb" Inherits="SNTE5.COB_CONDONACIONES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
            if (wbusf != null) {

                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>Afiliado</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">                        
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input" MaxLength="9"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="Label1">Número de afiliado: </asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_cliente">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Enabled="true"
                                CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                        </div>
                    </div>
                    <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                        Style="height: 18px; width: 18px;"></asp:ImageButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="Label2">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Textbox ID="lbl_Cartera" runat="server" CssClass="text_input_nice_input" Enabled="false"></asp:Textbox>
                         <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_cartera_tag">Cartera: </asp:Label>                            
                        </div>
                    </div>
                </div>
            </div>
                        
            <asp:Label CssClass="module_subsec" ID="lbl_Cliente" runat="server" ></asp:Label>
                   
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_Info" runat="server" class="alerta"></asp:Label>                  
            </div>           

        </div>
    </section>

    <section class="panel" runat="server" id="pnl_interes">
        <header class="panel-heading">
            <span>Interés/Capital (Préstamo)</span>
        </header>
        <div class="panel-body">             
            <div class="module_subsec low_m columned four_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_TipoCondonacion" runat="server" class="btn btn-primary2 dropdown_label" 
                            AutoPostBack = "True" Enabled="False">
                            <asp:ListItem Value="M">MONTO</asp:ListItem>
                            <asp:ListItem Value="P">PORCENTAJE</asp:ListItem>
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_TipoCondonacion" runat="server" CssClass="text_input_nice_label" Text="Tipo de condonación: "></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
                        
            <div class="module_subsec no_m columned four_columns" style ="margin-bottom:-20px !important;">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <div class="text_input_nice_labels">
                            <span runat="server" id="lbl_IntNormal" class="text_input_nice_label" style="width:50%;">Interés normal:</span>
                            <asp:Label ID="lbl_IntNormalDebe" runat="server" CssClass="text_input_nice_label"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_IntNormal" class="text_input_nice_input" Enabled="False" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                             <asp:Label ID="lbl_monto" class="text_input_nice_label" runat="server"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_IntNormal" runat="server" 
                                Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_IntNormal">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_IntNormal" runat="server" Display="Dynamic"
                                class="textogris" ControlToValidate="txt_IntNormal" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_IntNormalConf" class="text_input_nice_input" Enabled="false" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_confmonto" class="text_input_nice_label" runat="server"></asp:Label>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_IntNormalConf" runat="server" 
                                Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_IntNormalConf">
                            </ajaxToolkit:FilteredTextBoxExtender> 
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_IntNormalConf" runat="server"
                                class="textogris" ControlToValidate="txt_IntNormalConf" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator> 
                        </div>
                    </div>
                </div>
            </div>
                    
            <div class="module_subsec no_m columned four_columns" style ="margin-bottom:-20px !important;">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <div class="text_input_nice_labels">
                            <span runat="server" id="lbl_IntMoratorio" class="text_input_nice_label" style="width:50%;">Interés moratorio:</span>
                            <asp:Label ID="lbl_IntMoratorioDebe" runat="server" CssClass="text_input_nice_label" ></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_IntMoratorio" class="text_input_nice_input" Enabled="False" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxt_IntMoratorio" runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                TargetControlID="txt_IntMoratorio">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_IntMoratorio" runat="server" Display="Dynamic"
                                class="textogris" ControlToValidate="txt_IntMoratorio" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_IntMoratorioConf" class="text_input_nice_input" Enabled="false" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txt_IntMoratorioConf" runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                TargetControlID="txt_IntMoratorioConf">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_IntMoratorioConf" runat="server"
                                class="textogris" ControlToValidate="txt_IntMoratorioConf" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec no_m columned four_columns" style ="margin-bottom:-20px !important;">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <div class="text_input_nice_labels">
                            <span runat="server" id="lbl_Capital" class="text_input_nice_label" style="width:50%;">Capital:</span>
                            <asp:Label ID="lbl_CapitalDebe" runat="server" CssClass="text_input_nice_label"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_Capital" class="text_input_nice_input" Enabled="False" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Capital" runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                TargetControlID="txt_Capital">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Capital" runat="server" Display="Dynamic"
                                class="textogris" ControlToValidate="txt_Capital" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_CapitalConf" class="text_input_nice_input" Enabled="false" runat="server" MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_CapitalConf" runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                TargetControlID="txt_CapitalConf">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_CapitalConf" runat="server"
                                class="textogris" ControlToValidate="txt_CapitalConf" ErrorMessage="Valor Incorrecto"  
                                ValidationExpression="^[0-9]+(\.[0-9]{2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div><br />
                                           
            <div class= "module_subsec low_m columned four_columns flex_start">
                <div class="module_subsec_elements" style="flex:1;">
                    <div class="module_subsec_elements_content vertical">
                        <div class="text_input_nice_labels">
                            <span runat="server" id="lbl_Notas" class="text_input_nice_label">*Notas de operación:</span>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator_Notas" runat="server" 
                                ControlToValidate="txt_Notas" Cssclass="textoazul" Display="Dynamic" 
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_cond"></asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="txt_Notas" runat="server" class="text_input_nice_textarea"
                            MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Notas" runat="server" Enabled="True" 
                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_Notas" 
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,."></ajaxToolkit:FilteredTextBoxExtender>
                        
                    </div>
                </div>
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">

                    </div>
                </div>
            </div>
                   
             <asp:Label ID="Condono" runat="server" Cssclass="module_subsec textogris">Intereses condonados:</asp:Label>
                    
            <div class="module_subsec no_m columned four_columns">                
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Textbox ID="lbl_IntN" runat="server" Cssclass="text_input_nice_input" Enabled="false"></asp:Textbox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_CondIntN" runat="server" Cssclass="text_input_nice_label">Interés normal</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Textbox ID="lbl_IntM" runat="server" Cssclass="text_input_nice_input" Enabled ="false"></asp:Textbox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_CondIntM" runat="server" Cssclass="text_input_nice_label">Interés moratorio</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Textbox ID="lbl_Cap" runat="server" Cssclass="text_input_nice_input" Enabled="false"></asp:Textbox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_CondCapital" runat="server" Cssclass="text_input_nice_label">Capital</asp:Label>
                        </div>
                    </div>
                </div>
            </div>
             
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_estatus" runat="server" class="alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_Condonar" runat="server" class="btn btn-primary" Text="Condonar" 
                    Enabled="False" ValidationGroup="val_cond"/>
            </div>
                         
            <p align="center">                        
                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_CondonaInt" runat="server"
                    TargetControlID="btn_Condonar" ConfirmText="¿Confirma la condonacion de estos montos en intereses?" 
                    Enabled="True"></ajaxToolkit:ConfirmButtonExtender> 
            </p>
        </div>
    </section>

    <section class="panel" runat="server" id="pnl_comisiones">
        <header class="panel-heading">
            <span>Comisiones</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec overflow_x shadow">
                <asp:DataGrid ID="dag_CondComisiones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>                                                        
                        <asp:BoundColumn DataField="IDCOMISION" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COMISION" HeaderText="Comisión">
                            <ItemStyle Width="20%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MONTO_COMISION" HeaderText="Monto comisión">
                            <ItemStyle Width="10%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IVA_COMISION" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MONTO_COBRADO" HeaderText="Monto cobrado">
                            <ItemStyle Width="10%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IVA_COBRADO" HeaderText="IVA cobrado">
                            <ItemStyle Width="10%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PAGADO" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA_VISTA" HeaderText="Fecha cobrado">
                            <ItemStyle Width="10%"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SERIE" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FOLIO_IMP" Visible="False">
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="CONDONAR" HeaderText=" " Text="Condonar">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </div>                            
            
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_NotasCondCom" runat="server" CssClass="alerta" Text=""></asp:Label>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_InfoCondComision" runat="server" Class="alerta"></asp:Label>
            </div>
            <asp:Panel ID="pnl_CondComision" runat="server" CssClass="modalPopup" Width="350px">
                <asp:Panel ID="pnl_tituloCondComision" runat="server" CssClass="modalHeader">
                    <label id="lbl_tituloCondComision" class="modalTitle">Condonar comisión</label>
                </asp:Panel>

                <div class="modalContent">
                    <div class= "module_subsec low_m columned four_columns flex_start">
                        <div class="module_subsec_elements" style="flex:1;">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_labels">                                     
                                    <asp:Label ID="Label3" runat="server" CssClass="text_input_nice_label" Text="Notas:"></asp:Label>                                
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NotasCondCom" runat="server" 
                                        Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                        TargetControlID="txt_NotasCondCom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,."></ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_NotasCondCom" runat="server" 
                                        ControlToValidate="txt_NotasCondCom" Cssclass="textoazul" Display="Dynamic" 
                                        ErrorMessage="Error: Debe llenar las notas." ValidationGroup="val_confCom"></asp:RequiredFieldValidator>
                                </div>
                                <asp:TextBox ID="txt_NotasCondCom" runat="server" class="text_input_nice_textarea" MaxLength="1000" 
                                    ValidationGroup="val_confCom" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Button ID="btn_ConfirmarCondCom" runat="server" class="btn btn-primary" Text="Condonar" ValidationGroup="val_confCom"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_CancelarCondCom" runat="server" class="btn btn-primary" Text="Cancelar" />
                    </div>
                </div>
            </asp:Panel>
            <div class="center">
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_CondComision" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_CondComision"
                    PopupDragHandleControlID="pnl_tituloCondComision" TargetControlID="hdn_CondComision">
                </ajaxToolkit:ModalPopupExtender>
            </div>
        </div>
    </section>
 
    <input type="hidden" name="hdn_CondComision" id="hdn_CondComision" runat="server" />
</asp:Content>
