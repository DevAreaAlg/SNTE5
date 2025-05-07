<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_PAGO_CRED.aspx.vb" Inherits="SNTE5.CRED_VEN_PAGO_CRED" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        var a; //Just a dummy variable to pass in the optional PrintersCount argument
        var x = document.getElementById("HiddenRawData").value;
        //Get printers list (printer names). The names are separated by the pipe "|" character
        var PrinterList = Printer.GetPrinters(a, true);
        if (x != "") {
            var RetVal = Printer.PrintRawData(x, a, document.getElementById("HiddenPrinterName").value);
        }
        document.getElementById("HiddenRawData").value = "";
    </script>

        <section class="panel" >
            <header class="panel-heading">
                <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
            </header>
            <div class="panel-body">
                <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                    <asp:Textbox ID="lbl_Cliente" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                    </div>                    
            </div>
        </section> 

        <section class="panel" >
            <header class="panel-heading">
                <span>Pago</span>
            </header>
            <div class="panel-body">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Textbox ID="lbl_Monto" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" class="text_input_nice_label">Monto:</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
               
                <asp:Panel ID="pnl_Credito" runat="server" Width="760px">                        
                </asp:Panel>

                <asp:Panel ID="pnl_DevAforo" runat="server" Width="760px">
                    <table width="780px">
                        <tr>
                            <td width="190px">
                                <asp:Label ID="Label1" runat="server" class="subtitulos">Devolución de Aforo</asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" class="subtitulos">Proveedor:</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_FactProv" runat="server" CssClass="texto"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="780px">
                        <tr>
                            <td width="190px">
                                <asp:Label ID="Label3" runat="server" class="subtitulos">Monto de Aforo:</asp:Label>
                            </td>
                            <td width="150px" align="right">
                                <asp:Label ID="lbl_FactMontoAforo" runat="server" class="texto"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" class="subtitulos">Interés por Cobrar:</asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_FactInteres" runat="server" class="texto"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" class="subtitulos">IVA por Interés:</asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_FactIva" runat="server" class="texto"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" class="subtitulos">Total a Pagar:</asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_FactTotalPago" runat="server" class="texto"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="subtitulos">Restante de Aforo:</asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_FactRestanteAforo" runat="server" class="texto"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Label ID="lbl_pago_anticipo" runat="server" CssClass="texto" Visible="false"></asp:Label>
                     
                <%-- ----------
                    COMISIONES
                    --------   --%>      
                <asp:Panel ID="pnl_comision" runat="server" Visible="false">     
                    <h5 class="module_subsec resalte_azul">Comisión</h5>
                    <div class="overflow_x shadow module_subsec">
                        <asp:DataGrid ID="dag_Comisiones" AutoGenerateColumns="False" runat="server" CssClass="table table-striped"
                            GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="IDCOMISION" Visible="False">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COMISION" HeaderText="Comision">
                                    <ItemStyle Width="280px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IVA" HeaderText="IVA">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TOTAL" Visible="False">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                        
                    <div class="module_subsec">               
                        <asp:Label ID="lbl_PagaCom" runat="server" CssClass="texto" style =" margin-bottom:5px;">Desactivar si no desea pagar la comision financiado por el credito.</asp:Label>  &nbsp; &nbsp;                                
                        <asp:CheckBox ID="Chk_FinanCom" runat="server" AutoPostBack="True" CssClass="textocajas no_bm" DataTextField="" DataValueField="ID"
                            Enabled="False" Checked="false" Text="Desactivado" />
                    </div>

                    <div class="module_subsec"> 
                        <asp:Label ID="lbl_TotalCom" runat="server" CssClass="texto" Text="Total:"></asp:Label> &nbsp; &nbsp;
                        <asp:Label ID="lbl_TotalComMonto" runat="server" CssClass="texto"></asp:Label>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_InfoCom" runat="server" CssClass="alerta"></asp:Label>
                    </div> 
                </asp:Panel>         
                              
                    <%-- ----------
                    CUENTAS
                    --------   --%>  
                <h5 class="module_subsec low_m resalte_azul"> <asp:Label  runat ="server" id="tit_cuentas"></asp:Label></h5>      
                <div class= "module_subsec low_m columned three_columns">
	                <div class="module_subsec_elements"> 
		                <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:Panel ID="pnl_CtasCap" runat="server" Enabled="false"></asp:Panel>
                        </div>
	                </div>
                </div>
                           
                <%-- ----------
                    EFECTIVO
                    --------   --%> 
                <h5 class="module_subsec low_m resalte_azul">Efectivo</h5>
                                
                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_CajaMonto" CssClass="text_input_nice_input" runat="server"
                                Enable="False" Enabled="False" MaxLength="12"></asp:TextBox>
                            <div class="text_input_nice_labels"> 
                                <asp:Label ID="lbl_CajaMonto" runat="server" CssClass="text_input_nice_label" Text="Monto:"></asp:Label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_CajaMonto"
                                    runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                    TargetControlID="txt_CajaMonto">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_CajaMonto" runat="server"
                                    class="textogris" ControlToValidate="txt_CajaMonto" ErrorMessage=" Monto incorrecto!"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                     
                <%-- ----------
                    TRANSFERENCIAS
                    --------   --%>        
                <asp:UpdatePanel ID="updpnl_CtasBancos" runat="server">
                    <ContentTemplate>&nbsp;
                        <h5 class="module_subsec low_m resalte_azul">Transferencias</h5>
                                        
                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_Banco" CssClass="btn btn-primary2 dropdown_label" runat="server" 
                                            Enabled="False" ValidationGroup="val_Bancos"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_Banco" runat="server" CssClass="text_input_nice_label" Text="Banco:"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Bancos"
                                                CssClass="textogris" ControlToValidate="cmb_Banco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_Bancos" InitialValue="-1" />
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_MontoBanco" CssClass="text_input_nice_input" runat="server" Enabled="False" 
                                            ValidationGroup="val_Bancos" MaxLength="12"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_MontoBanco" runat="server" CssClass="text_input_nice_label" Text="Monto $:" ></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoBanco" runat="server" 
                                                Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoBanco">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoBanco" runat="server" class="textogris" Display="Dynamic"
                                                ControlToValidate="txt_MontoBanco" ErrorMessage=" Monto incorrecto!" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_MontoBanco" CssClass="textogris" 
                                                ControlToValidate="txt_MontoBanco" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bancos" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
	                                    <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_BancoCliente" runat="server" CssClass="btn btn-primary2 dropdown_label"
                                                Enabled="False" ValidationGroup="val_Bancos"></asp:DropDownList>
                                                <div class="text_input_nice_labels">
                                                    <asp:Label ID="lbl_BancoCliente" runat="server" CssClass="text_input_nice_label" Text="Banco destino:"></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_BancoCliente" CssClass="textogris" 
                                                        ControlToValidate="cmb_BancoCliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                        ValidationGroup="val_Bancos" InitialValue="-1" />
                                                </div>
                                        </div>
                                    </div>

                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_ClabeCliente" CssClass="text_input_nice_input" runat="server"
                                            Enabled="False" ValidationGroup="val_Bancos" MaxLength="18"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_ClabeCliente" runat="server" CssClass="text_input_nice_label" Text="Clabe:"></asp:Label>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ClabeCliente" runat="server" 
                                                    Enabled="True" FilterType="Numbers, Custom" ValidChars="-" TargetControlID="txt_ClabeCliente">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ClabeCliente" CssClass="textogris" 
                                                    ControlToValidate="txt_ClabeCliente" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bancos" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_ClabeCliente" runat="server" CssClass="textogris" 
                                                    ControlToValidate="txt_ClabeCliente" ErrorMessage="No contiene 18 digitos!" ValidationExpression="^[0-9]{18}?$">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                    </div>
                                </div>

                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_NumCtaCliente" runat="server" CssClass="text_input_nice_input" 
                                            Enabled="False" MaxLength="20"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <asp:Label ID="lbl_NumCtaCliente" runat="server" CssClass="text_input_nice_label" Text="Número de cuenta:"></asp:Label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NumCtaCliente" CssClass="textogris" 
                                                    ControlToValidate="txt_NumCtaCliente" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Bancos" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_AgregarBanco" runat="server" class="btn btn-primary" Text="Agregar"
                                    ValidationGroup="val_Bancos" Enabled="False" CausesValidation="True" />
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_InfoBanco" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                            </div>

                            <div class="overflow_x shadow module_subsec" > 
                                <asp:DataGrid ID="dag_Bancos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_CTA" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                            <ItemStyle Width="130px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_BANCO_CLIENTE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO_CLIENTE" HeaderText="Banco destino">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CLABE" HeaderText="Clabe cliente">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_CTA_CLIENTE" HeaderText="Núm. cuenta afiliado">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                            <ItemStyle Width="50px" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>

                            <div class="overflow_x shadow module_subsec" > 
                                <asp:DataGrid ID="dag_bancosori" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="95%">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_CTA" HeaderText="Banco" Visible="False">
                                            <ItemStyle Width="350px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                            <ItemStyle Width="350px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        </asp:ButtonColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                           
                <%-- ----------
                    CHEQUES
                    --------   --%> 
                <asp:UpdatePanel ID="updpnl_Cheques" runat="server">
                    <ContentTemplate> &nbsp;
                        <h5 class="module_subsec low_m resalte_azul">Cheques</h5>
                                        
                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_BancoCheques" CssClass="btn btn-primary2 dropdown_label" runat="server" 
                                        Enabled="False" ValidationGroup="val_Cheques"></asp:DropDownList>
                                        <div class="text_input_nice_labels"> 
                                            <asp:Label ID="lbl_BancoCheques" runat="server" CssClass="text_input_nice_label" Text="Banco emisor:"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_BancoCheques"
                                                CssClass="textogris" ControlToValidate="cmb_BancoCheques" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Cheques" InitialValue="-1" />
                                        </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_ctachequesori" CssClass="text_input_nice_input" runat="server"
                                        ValidationGroup="val_chequesori" MaxLength="20"></asp:TextBox>
                                        <div class="text_input_nice_labels"> 
                                            <asp:Label ID="lbl_ctachequesori" runat="server" CssClass="text_input_nice_label" Text="Número de cuenta emisora:"></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ctachequesori" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txt_ctachequesori">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ctachequesori"
                                                CssClass="textogris" ControlToValidate="txt_ctachequesori" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" />
                                        </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_NumCheques" CssClass="text_input_nice_input" runat="server" Enabled="False"
                                        ValidationGroup="val_Cheques" MaxLength="20"></asp:TextBox>
                                        <div class="text_input_nice_labels"> 
                                            <asp:Label ID="lbl_NumCheques" runat="server" CssClass="texto" Text="Número de cheque:"></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NumCheques"
                                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_NumCheques">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_NumCheques"
                                                CssClass="textogris" ControlToValidate="txt_NumCheques" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_Cheques" />
                                        </div>
                                </div>
                            </div>
                        </div>

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MontoCheques" CssClass="text_input_nice_input" runat="server" Enabled="False" 
                                        ValidationGroup="val_Cheques" MaxLength="12"></asp:TextBox>
                                        <div class="text_input_nice_labels"> 
                                            <asp:Label ID="lbl_MontoCheques" runat="server" CssClass="texto" Text="Monto $:"></asp:Label>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoCheques" runat="server"
                                                Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoCheques">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoCheques" runat="server" class="textogris"
                                                ControlToValidate="txt_MontoCheques" ErrorMessage=" Monto incorrecto!" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_MontoCheques" CssClass="textogris"
                                                ControlToValidate="txt_MontoCheques" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_Cheques" />
                                        </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_modo_rec" CssClass="btn btn-primary2 dropdown_label" runat="server" ValidationGroup="val_chequesori">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="SBC" Selected="True">SALVO BUEN COBRO</asp:ListItem>
                                        <asp:ListItem Value="COB">EN FIRME</asp:ListItem>
                                    </asp:DropDownList>
                                        <div class="text_input_nice_labels"> 
                                                <asp:Label ID="lbl_modo_rec" runat="server" CssClass="text_input_nice_label" Text="Tipo de recepción de cheque:"></asp:Label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfv_modo_rec"
                                                CssClass="textogris" ControlToValidate="cmb_modo_rec" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="val_chequesori" InitialValue="-1" />
                                        </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">

                                </div>
                            </div>
                        </div>                                        
                                        
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_AgregarCheques" runat="server" CssClass="btn btn-primary" Text="Agregar" 
                                ValidationGroup="val_Cheques" Enabled="False" />
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_InfoCheques" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
                        </div>

                        <div class="overflow_x shadow module_subsec" >
                            <asp:DataGrid ID="dag_Cheques" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CTA" Visible="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_BANCO" Visible="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="300px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Cuenta" Visible="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CHEQUE" HeaderText="Número">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                        <ItemStyle Width="50px" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <div class="overflow_x shadow module_subsec" >
                            <asp:DataGrid ID="dag_chequesori" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_CTA" Visible="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_BANCO" Visible="False">
                                        <ItemStyle Width="350px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="350px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Cuenta">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CHEQUE" HeaderText="Número">
                                        <ItemStyle Width="325px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESTATUS" Visible="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>                             
                           

                <div class="module_subsec flex_center">                        
                    <asp:Button ID="btn_Aplicar" runat="server" CssClass="btn btn-primary" Text="Aplicar"
                        OnClientClick="btn_Aplicar.disabled = true; btn_Aplicar.value = 'Procesando...';" />
                </div>
                    
		        <div class="module_subsec flex_center">
			        <asp:Label ID="lbl_Info" runat="server" CssClass="alerta"></asp:Label>
		        </div>
                   <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
            </div>
                
        <asp:HiddenField ID="HiddenPrinterName" runat="server" />
        <asp:HiddenField ID="HiddenRawData" runat="server" />
    </section>
</asp:Content>

