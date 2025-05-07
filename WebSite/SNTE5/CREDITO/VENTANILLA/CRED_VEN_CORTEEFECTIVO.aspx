<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_CORTEEFECTIVO.aspx.vb" Inherits="SNTE5.CRED_VEN_CORTEEFECTIVO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="module_subsec vertical">
        <span class="text_input_nice_label">Elija su caja:</span>
        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="btn btn-primary2 dropdown_label" ID="cmb_CajaPropia"></asp:DropDownList>
    </div>
    <asp:Label runat="server" class="text_input_nice_label" ForeColor="Red" ID="lbl_Info0"></asp:Label>
    
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%">
        <section class="panel" >
            <div class="panel-body">

                <ajaxToolkit:TabPanel runat="server" HeaderText="Corte Inicial" ToolTip="Corte Inicial" ID="TabPanel1">
                    <HeaderTemplate>Corte Inicial</HeaderTemplate>
                    <ContentTemplate>

                        <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                            <div style="display: flex; align-items: center;">
                                <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_ReportesCI">
                                    Reportes de corte inicial
                                    <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class= "module_subsec low_m columned three_columns">	                        
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Cajas" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <span class="text_input_nice_label">Cajas:</span>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_Monto" runat="server" class="text_input_nice_input" ValidationGroup="val_CorteInicial"></asp:TextBox>
                                    <span class="text_input_nice_label">Monto:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Monto" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_Monto">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Monto" runat="server"
                                        class="textogris" ControlToValidate="txt_Monto" ErrorMessage=" Monto incorrecto!"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_Info" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_Aplicar" runat="server" class="btn btn-primary" Text="Aplicar"
                                Enabled="False"  OnClientClick="TabContainer1$TabPanel1$btn_Aplicar.disabled = true; TabContainer1$TabPanel1$btn_Aplicar.value = 'Procesando...';" />
                        </div>
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                
                <ajaxToolkit:TabPanel runat="server" HeaderText="Corte Final" ToolTip="Corte Final" ID="TabPanel2">
                    <HeaderTemplate>Corte Final</HeaderTemplate>
                    <ContentTemplate>

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">                                    
                                    <asp:DropDownList ID="cmb_Cajas2" runat="server" AutoPostBack="True" CssClass="btn btn-primary2" ValidationGroup="val_CorteFinal"></asp:DropDownList>
                                    <asp:Label ID="Label3" runat="server" CssClass="texto" Text="1.- Seleccionar caja:"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec">
                            <asp:Label ID="Label4" runat="server" CssClass="texto" Text="2.- Revisar y validar registros de efectivo:"></asp:Label>
                        </div>

                        <asp:Panel ID="pnl_registroefect" runat="server" Visible="false">
                            <div class="module_subsec_elements columned align_items_flex_start">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_Flujo" runat="server" class="modue_subsec subtitulos">Flujo Efectivo</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_Caja" runat="server" class="subtitulos">Registro Cajero</asp:Label>
                            </div>

                            <asp:Panel ID="pnl_Denom" runat="server" class="module_subsec">                           
                            
                            </asp:Panel>                            
                        
                            <div class="module_subsec_elements columned align_items_flex_start"> &nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" class="texto">Total:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_TotalFlujo" runat="server" class="texto"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_TotalCaja" runat="server" class="texto"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbl_AlertaTotal" runat="server" class="textogris" ForeColor="Red"></asp:Label>
                            </div>                        
                        </asp:Panel>

                        
                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
		                            <div class="text_input_nice_div module_subsec_elements_content">                                        
                                        <asp:Button ID="btn_Imprimir" runat="server" class="btn btn-primary" Text="Imprime Reporte" Enabled="False" />
                                        <asp:Label ID="lbl_Cajas3" runat="server" CssClass="texto" Text="3.- Imprimir constancia de corte final:"></asp:Label>
                                    </div>
                                </div>
                                <div class="module_subsec_elements"> 
		                            <div class="text_input_nice_div module_subsec_elements_content">                                        
                                        <asp:Button ID="btn_Digitalizar" runat="server" class="btn btn-primary" Text="Digitalizar" Enabled="False" />
                                        <asp:Label ID="lbl_Cajas4" runat="server" CssClass="texto" Text="4.- Digitalizar constancia de corte final:"></asp:Label>
                                    </div>
                                </div>
                                <div class="module_subsec_elements"> 
		                            <div class="text_input_nice_div module_subsec_elements_content">                                        
                                        <asp:Button ID="btn_CorteFinal" runat="server" Text="Corte Final" class="btn btn-primary"
                                            Enabled="False" UseSubmitBehavior="False"  />
                                        <asp:Label ID="lbl_Cajas5" runat="server" CssClass="texto" Text="5.- Terminar corte final o corregir denominaciones:"></asp:Label>
                                        <asp:Button ID="btn_Corregir" runat="server" class="btn btn-primary" Text="Corregir" Visible="False" Enabled="False" />
                                    </div>
                                </div>
                            </div>                 

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>


                <ajaxToolkit:TabPanel runat="server" HeaderText="Corte Final Cajero" ToolTip="Corte Final Cajero" ID="TabPanel3">
                    <HeaderTemplate>Corte Final Cajero</HeaderTemplate>
                    <ContentTemplate>

                        <asp:Label ID="lbl_CorregirTiraEfectivo" runat="server" class="texto" Visible="False">Corregir Registro de Denominación</asp:Label>

                        <center>
                            <table>
                                <tr >
                                    <td VALIGN="TOP">
                                        <asp:Label ID="lbl_BILLETE" runat="server" class="texto" Width="70px">BILLETES</asp:Label>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno1" runat="server" class="texto" Width="70px">1000.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant1" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Cant1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant1">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont1" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno2" runat="server" class="texto" Width="70px">500.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant2" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant2">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont2" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno3" runat="server" class="texto" Width="70px">200.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant3" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant3">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont3" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno4" runat="server" class="texto" Width="70px">100.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant4" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant4">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont4" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno5" runat="server" class="texto" Width="70px">50.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant5" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_Cant5">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont5" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno6" runat="server" class="texto" Width="70px">20.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant6" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="BILLETE"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant6">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont6" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Label ID="lbl_MONEDA" runat="server" class="texto" Width="70px">MONEDAS</asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lbl_Deno7" runat="server" class="texto" Width="70px">100.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant7" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant7">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont7" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno8" runat="server" class="texto" Width="70px">20.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant8" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant8">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont8" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />


                                        <asp:Label ID="lbl_Deno9" runat="server" class="texto" Width="70px">10.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant9" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant9">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont9" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno10" runat="server" class="texto" Width="70px">5.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant10" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant10">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont10" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno11" runat="server" class="texto" Width="70px">2.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant11" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant11">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont11" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno12" runat="server" class="texto" Width="70px">1.00</asp:Label>
                                        <asp:TextBox ID="txt_Cant12" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant12">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont12" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno13" runat="server" class="texto" Width="70px">0.50</asp:Label>
                                        <asp:TextBox ID="txt_Cant13" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant13">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont13" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno14" runat="server" class="texto" Width="70px">0.20</asp:Label>
                                        <asp:TextBox ID="txt_Cant14" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant14">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont14" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno15" runat="server" class="texto" Width="70px">0.10</asp:Label>
                                        <asp:TextBox ID="txt_Cant15" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_Cant15">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont15" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                        <asp:Label ID="lbl_Deno16" runat="server" class="texto" Width="70px">0.05</asp:Label>
                                        <asp:TextBox ID="txt_Cant16" runat="server" class="text_input_nice_input"
                                            AutoPostBack="True" Width="40px" MaxLength="5" ToolTip="MONEDA"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_Cant16">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txt_Mont16" runat="server" class="text_input_nice_input"
                                            Enabled="False" Width="70px"></asp:TextBox>
                                        <br />
                                        <br />

                                    </td>
                                </tr>
                            </table>
                        </center>

                        <center>
                            <asp:Label ID="Label1" runat="server" CssClass="alerta"></asp:Label>
                            <asp:Label ID="lbl_Acum" runat="server" class="texto" Width="90px">Acumulado:</asp:Label>
                            <asp:Label ID="lbl_MontoAcum" runat="server" class="texto"></asp:Label>
                        </center>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_GuardaDenomCaja" runat="server" class="btn btn-primary" Text="Guardar" />
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_GuardaDenomCaja" runat="server"
                                TargetControlID="btn_GuardaDenomCaja" ConfirmText="Confirmar registro de denominaciones?"
                                Enabled="True">
                            </ajaxToolkit:ConfirmButtonExtender>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>


                <ajaxToolkit:TabPanel runat="server" HeaderText="Transferir Efectivo" ToolTip="Transferir Efectivo" ID="TabPanel4">
                    <HeaderTemplate>Transferir Efectivo</HeaderTemplate>
                    <ContentTemplate>

                        <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                            <div style="display: flex; align-items: center;">
                                <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_ReportesTE">
                                    Reportes de transferencia de efectivo
                                    <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                </asp:LinkButton>
                            </div>
                        </div>

                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
		                            <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_CajasTE" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Cajas:</span>
                                        </div>
                                    </div>
                                </div>
                                	                        
                                <div class="module_subsec_elements"> 
		                            <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_MontoTE" runat="server" class="text_input_nice_input"></asp:TextBox>
                                        <span class="text_input_nice_label">Monto:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoTE" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoTE">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_CajasTE" runat="server" class="textogris"
                                            ControlToValidate="txt_MontoTE" ErrorMessage=" Monto incorrecto!"
                                            ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>                        

                        <div class="module_subsec flex_center">
                             <asp:Label ID="lbl_InfoTE" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_AplicarTE" runat="server" class="btn btn-primary" Text="Aplicar"
                                Enabled="False" OnClientClick="TabContainer1$TabPanel4$btn_AplicarTE.disabled = true; TabContainer1$TabPanel4$btn_AplicarTE.value = 'Procesando...'" />
                        </div>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>


                <ajaxToolkit:TabPanel runat="server" HeaderText="Cambio Denominación" ToolTip="Cambio de Denominación" ID="TabPanel5">
                    <HeaderTemplate>Cambio Denominación</HeaderTemplate>
                    <ContentTemplate>

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_MontoCambDenom" runat="server" class="text_input_nice_input"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Monto</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoCambDenom" runat="server"
                                                Enabled="True" FilterType="Custom, Numbers" ValidChars="."
                                                TargetControlID="txt_MontoCambDenom">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoCambDenom" runat="server"
                                                class="textogris" ControlToValidate="txt_MontoCambDenom"
                                                ErrorMessage=" Monto incorrecto!"
                                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_Info1" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                            <div class=" module_subsec flex_end">
                                <asp:Button ID="btn_CambDenom" runat="server" class="btn btn-primary" Text="Aplicar"
                                    UseSubmitBehavior="False" />                              
                            </div>
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>


                <ajaxToolkit:TabPanel runat="server" HeaderText="Movimientos Pendientes" ToolTip="Movimientos Pendientes" ID="TabPanel6">
                    <HeaderTemplate>Movimientos Pendientes</HeaderTemplate>
                    <ContentTemplate>

                        <asp:Label ID="lbl_MovPend" runat="server" CssClass="subtitulos" Text="Movimientos Pendientes"></asp:Label>
                        
                        <asp:DataGrid ID="dag_MovPend" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            class="table table-striped" GridLines="None">

                            <Columns>
                                <asp:BoundColumn DataField="SERIEFOLIO" HeaderText="Serie / Folio">
                                    <ItemStyle Width="200px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="125px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ENTSAL" HeaderText="Entrada/Salida">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="UNIO" HeaderText="Unión">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="REALIZAR" Text="Realizar">
                                    <ItemStyle Width="75px" />
                                </asp:ButtonColumn>
                            </Columns>
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                        </asp:DataGrid>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>


                <ajaxToolkit:TabPanel runat="server" HeaderText="Flujo Efectivo" ToolTip="Flujo Efectivo" ID="TabPanel7">
                    <HeaderTemplate>Flujo Efectivo</HeaderTemplate>
                    <ContentTemplate>

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_CajasCrr" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <span class="text_input_nice_label">Cajas:</span>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_TipoOpe" CssClass="btn btn-primary2 dropdown_label" runat="server">
                                        <asp:ListItem Value="ENTRADA">ENTRADA</asp:ListItem>
                                        <asp:ListItem Value="SALIDA">SALIDA</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="text_input_nice_label">Entrada / Salida:</span>
                                </div>
                            </div>
                            <div class="module_subsec_elements"> 
		                        <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MontoCrr" runat="server" class="text_input_nice_input" ValidationGroup="val_CorreccionFlujo"></asp:TextBox>
                                    <span class="text_input_nice_label">Monto:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoCrr" runat="server" Enabled="True"
                                        FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_MontoCrr">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoCrr" runat="server"
                                        class="textogris" ControlToValidate="txt_MontoCrr"
                                        ErrorMessage=" Monto incorrecto!"
                                        ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_InfoCrr" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_AplicaCrr" runat="server" class="btn btn-primary" Text="Aplicar"
                                Enabled="False" UseSubmitBehavior="False" />
                       </div>
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                </div>
        </section>
            </ajaxToolkit:TabContainer>
            
</asp:Content>

