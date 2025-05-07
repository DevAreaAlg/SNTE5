<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_DIVISAS.aspx.vb" Inherits="SNTE5.CORE_OPE_DIVISAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                          
            <section class="panel" >
                <header class="panel-heading">
                    <span>Carga de un valor</span>
                </header>
                <div class="panel-body">
                        <div class="module_subsec">
                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" 
                            Text="Capture el valor de la divisa de acuerdo a la fecha de sistema, recuerde que el valor se captura a 4 decimales."></asp:Label>
                        </div>                         

                        <div class= "module_subsec low_m columned three_columns flex_end">
	                        <div class="module_subsec_elements" style="flex:1;"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="cmb_Divisas" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Divisas" runat="server" CssClass="text_input_nice_label">Divisas:</asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Divisas" Cssclass="textogris" ControlToValidate="cmb_Divisas" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!"  ValidationGroup="val_ValorDivisa" initialvalue="0"/>
                                    </div>
		                        </div>
	                        </div>

                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_ValorMXP" runat="server" class="text_input_nice_input" MaxLength="9" ValidationGroup="val_ValorDivisa"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_ValorMXP" runat="server" CssClass="text_input_nice_label">Valor(MXP):</asp:Label>
                                        <asp:Label ID="lbl_ValorPeso" runat="server" CssClass="text_input_nice_label">$</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_ValorMXP" runat="server" ControlToValidate="txt_ValorMXP" 
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_ValorDivisa">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ValorMXP" runat="server" 
                                            Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_ValorMXP">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_ValorMXP" runat="server" Display="Dynamic"
                                            ControlToValidate="txt_ValorMXP" Cssclass="textogris" ErrorMessage=" Error:Monto incorrecto" class="textorojo" 
                                            ValidationExpression="^[0-9]+(\.[0-9]{3}[0-9]?)?$" ValidationGroup = "val_ValorDivisa">
                                        </asp:RegularExpressionValidator>
                                    </div>
		                        </div>
	                        </div>

                            <div class="module_subsec_elements" style="flex:1;">
                                <div class="text_input_nice_div flex_end">
                                     <asp:Button ID="btn_Asignar" runat="server" ValidationGroup="val_ValorDivisa" class="btn btn-primary"  Text="Asignar"/>
                                </div>
                            </div>
                        </div> 
                        
	                    <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                            <div style="display:flex;align-items:center;"> 
                                <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" style="margin-right:25px;font-size:18px;" ID="btn_EXCEL">
                                    Descargar Excel Divisas
                                    <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                </asp:LinkButton>
                            </div>
                        </div>

                        <h4 class="module_subsec ">Valor actual</h4>

                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_divisas0" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="DIVISA" HeaderText="Divisa">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VALOR" HeaderText="Valor">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>                        

                        <h4 class="module_subsec low_m">Historial</h4>
                        
                        <div class= "module_subsec low_m columned three_columns">
	                        <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" MaxLength="10" ID="txt_FechaFiltro" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_fecha" runat="server" CssClass="text_input_nice_label"> Fecha: </asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_FechaFiltro" Enabled="true"
                                        CssClass="textogris" ControlToValidate="txt_FechaFiltro" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_FechaFiltro" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_FechaFiltro" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" 
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaFiltro">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_FechaFiltro" runat="server" 
                                        ControlExtender="MaskedEditExtender_FechaFiltro" ControlToValidate="txt_FechaFiltro" 
                                        Cssclass="textogris" ErrorMessage="MaskedEditValidator_FechaFiltro" Display="Dynamic"
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_FechaFiltro">
                                    </ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                    TargetControlID="txt_FechaFiltro"></ajaxToolkit:CalendarExtender>
                                    </div>
		                        </div>
	                        </div>
                            <div class="module_subsec_elements"> 
                                    <asp:LinkButton ID="lnk_FechaFiltro" runat="server" class="textogris" Text="Aplicar filtro" ValidationGroup="val_FechaFiltro"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnk_CancelaFechaFiltro" runat="server" class="textogris" Text="Eliminar filtro"></asp:LinkButton>
                            </div>
                        </div>  
                      
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_divisas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="FECHASIS" HeaderText="Fecha">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="USD" HeaderText="Dolar (USD)">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EUR" HeaderText="Euro (EUR)">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                </div>
            </section>        
           
            <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />        
   
</asp:Content>

