<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_CNF_PLD.aspx.vb" Inherits="SNTE5.PLD_CNF_PLD" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>Configuración</span>
                    <span class=" panel_folder_toogle down">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                                    <div class="module_subsec_elements vertical flex_1">
                    <h4 class="module_subsec" >Perfiles:</h4>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimMin" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Mínimo de meses para cálculo de Perfil Transaccional:</label>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_LimMin" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>&nbsp;
                            <label id="Label3" class="texto"> MESES</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimMin" runat="server" ControlToValidate="txt_LimMin" Cssclass="textogris" 
                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimMin" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_LimMin"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimMax" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Máximo de meses para cálculo de Perfil Transaccional:</label>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_LimMax" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label4" class="texto">MESES</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimMax" runat="server" ControlToValidate="txt_LimMax" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimMax" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_LimMax"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_PorcMinMov" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Porcentaje mínimo de movimientos para Perfil Transaccional:</label>
                        <asp:TextBox runat="server" MaxLength="3" ID="txt_PorcMinMov" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label5" class="texto">%</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_PorcMinMov" runat="server" ControlToValidate="txt_PorcMinMov" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PorcMinMov" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_PorcMinMov"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimInfFisica" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite inferior para perfil de Persona Física:</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimInfFisica" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label8" class="texto">$ M.N.</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimInfFisica" runat="server" ControlToValidate="txt_LimInfFisica" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimInfFisica" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimInfFisica"></ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimInfFisica" runat="server" ControlToValidate="txt_LimInfFisica" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimInfMoral" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite inferior para perfil de Persona Moral:</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimInfMoral" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label9" class="texto">$ M.N.</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimInfMoral" runat="server" ControlToValidate="txt_LimInfMoral" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimInfMoral" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimInfMoral"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimInfMoral" runat="server" ControlToValidate="txt_LimInfMoral" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_NumDesvEst" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Número de Desviación Estándar:</label>
                        <asp:TextBox runat="server" MaxLength="1" ID="txt_NumDesvEst" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumDesvEst" runat="server" ControlToValidate="txt_NumDesvEst" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NumDesvEst" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_NumDesvEst"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>

                    <h4 class="module_subsec" >Operación Relevante:</h4>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimOpeRel" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite en dólares para Operaciones Relevantes:</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRel" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label7" class="texto">$ USD</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRel" runat="server" ControlToValidate="txt_LimOpeRel" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRel" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRel"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRel" runat="server" ControlToValidate="txt_LimOpeRel" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>           
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimOpeRelDolar" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite de dólares con otras monedas (Operación Relevante):</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRelDolar" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label12" class="texto">$ USD</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRelDolar" runat="server" ControlToValidate="txt_LimOpeRelDolar" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRelDolar" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRelDolar"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRelDolar" runat="server" ControlToValidate="txt_LimOpeRelDolar" Cssclass="textogris" 
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>

                                        
                    <h4 class="module_subsec">Reportes:</h4>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_DiasOpeRel" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Rango de dias de depósitos para Reporte PLD:</label>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_DiasOpeRel" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label6" class="texto">DIAS</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DiasOpeRel" runat="server" ControlToValidate="txt_DiasOpeRel" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DiasOpeRel" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_DiasOpeRel"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                            <label id="lbl_LimOpeRelFisica" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite en pesos para Operaciones Grandes (Persona Física):</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRelFisica" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label10" class="texto">$ M.N.</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRelFisica" runat="server" ControlToValidate="txt_LimOpeRelFisica" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRelFisica" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRelFisica"> </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRelFisica" runat="server" ControlToValidate="txt_LimOpeRelFisica" Cssclass="textogris" Display="Dynamic" 
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimOpeRelMoral" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite en pesos para Operaciones Grandes (Persona Moral):</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRelMoral" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label11" class="texto">$ M.N.</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRelMoral" runat="server" ControlToValidate="txt_LimOpeRelMoral" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRelMoral" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRelMoral"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRelMoral" runat="server" ControlToValidate="txt_LimOpeRelMoral" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                            <label id="lbl_LimOpeRelMensPesos" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite de depósitos en efectivo al mes (Pesos):</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRelMensPesos" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label14" class="texto">$ M.N.</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRelMensPesos" runat="server" ControlToValidate="txt_LimOpeRelMensPesos" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRelMensPesos" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRelMensPesos"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRelMensPesos" runat="server" ControlToValidate="txt_LimOpeRelMensPesos" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_LimOpeRelMensDolar" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Límite de depósitos en efectivo al mes (Dolar u Otra Moneda):</label>
                        <asp:TextBox runat="server" MaxLength="12" ID="txt_LimOpeRelMensDolar" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                            &nbsp;<label id="Label16" class="texto">$ USD</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_LimOpeRelMensDolar" runat="server" ControlToValidate="txt_LimOpeRelMensDolar" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_LimOpeRelMensDolar" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                ValidChars="." TargetControlID="txt_LimOpeRelMensDolar"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_LimOpeRelMensDolar" runat="server" ControlToValidate="txt_LimOpeRelMensDolar" Cssclass="textogris" Display="Dynamic"
                                ErrorMessage="Monto incorrecto!" class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" ValidationGroup="val_Conf"></asp:RegularExpressionValidator>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_AutoOpe" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Aplicar autorización de operaciones superiores a límites PF y PM:</label>
                            <asp:DropDownList ID="cmb_AutoOpe" runat="server" class="module_subsec_elements module_subsec_bigmedium-elements btn btn-primary2 dropdown_label">
                                <asp:ListItem Value="0">NO</asp:ListItem>
                                <asp:ListItem Value="1">SI</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <div class="module_subsec_elements module_subsec_bigger-elements title_tag">
                            <label id="c" class="texto">* Recordatorio de revisión de personas de Alto Riesgo:</label> &nbsp;
                            <label id="Label19" class="texto">CADA</label>
                        </div>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_RevAltRiesgo" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                        &nbsp;<label id="Label20" class="texto">MESES</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_RevAltRiesgo" runat="server" ControlToValidate="txt_RevAltRiesgo" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RevAltRiesgo" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_RevAltRiesgo"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <div class="module_subsec_elements module_subsec_bigger-elements title_tag">
                            <label id="lbl_RevBajRiesgo" class="texto">* Recordatorio de revisión de personas Riesgo Moderado o Bajo:</label> &nbsp;
                            <label id="Label22" class="texto">CADA</label>
                        </div>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_RevBajRiesgo" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                        &nbsp;<label id="Label23" class="texto">MESES</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_RevBajRiesgo" runat="server" ControlToValidate="txt_RevBajRiesgo" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RevBajRiesgo" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_RevBajRiesgo"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>

                    <h4 class="module_subsec" >Operaciones Inusuales:</h4>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="lbl_NumOpeMAx" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Número de operaciones máximo en periodo de pago:</label>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_NumOpeMAx" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                        <label id="Label17" class="texto">Operaciones</label>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumOpeMAx" runat="server" ControlToValidate="txt_NumOpeMAx" Cssclass="textogris" 
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NumOpeMAx" runat="server" Enabled="True" FilterType="Numbers" 
                            TargetControlID="txt_NumOpeMAx"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="module_subsec columned no_m align_items_flex_center" >
                        <label id="Label15" class="module_subsec_elements module_subsec_bigger-elements title_tag">* Número máximo de variaciones de Instrumento Monetario:</label>
                        <asp:TextBox runat="server" MaxLength="2" ID="txt_NumOpeInstMon" class="module_subsec_elements module_subsec_medium-elements text_input_nice_input" ValidationGroup="val_Conf"></asp:TextBox>
                        &nbsp;<label id="Label18" class="texto">Operaciones</label>&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumOpeInstMon" runat="server" ControlToValidate="txt_NumOpeInstMon" Cssclass="textogris" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Conf"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NumOpeInstMon" runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txt_NumOpeInstMon"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                            </div>
                            </div>
                    <p align="center">
                        <asp:Label ID="lbl_InfoConf" runat="server" CssClass="alerta flex_1 module_subsec low_m flex_center" ></asp:Label>
                    </p>
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_GuardarConf" runat="server" class="btn btn-primary" Text="Guardar"  ValidationGroup="val_Conf"/>
                    </div>
                    </div>
                </div>
            </section>

            
            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>Tabulador</span>
                    <span class=" panel_folder_toogle down">&rsaquo;</span>
                </header>                
                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <asp:UpdatePanel ID="upd_tabulador" runat="server" updatemode="Conditional">
                            <ContentTemplate>
                                <div class="module_subsec columned four_columns low_m " >
                        
                                    <div class="module_subsec_elements text_input_nice_div">  
                                        <asp:TextBox runat="server" MaxLength="12" ID="txt_MontoMin" class="text_input_nice_input" ValidationGroup="val_Tabulador"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <label id="lbl_MontoMin" class="text_input_nice_label" >* Monto mínimo:</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_MontoMin" runat="server"  ControlToValidate="txt_MontoMin" Cssclass="resalte_rojo" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Tabulador"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoMin" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                            ValidChars="." TargetControlID="txt_MontoMin"></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoMin" runat="server" ControlToValidate="txt_MontoMin" Cssclass="resalte_rojo" 
                                            ErrorMessage="Monto incorrecto!"  ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"  ValidationGroup="val_Tabulador" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements text_input_nice_div">                                     
                                        <asp:TextBox runat="server" MaxLength="12" ID="txt_MontoMax" class="text_input_nice_input"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <label id="lbl_MontoMax" class="text_input_nice_label">* Monto máximo:</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_MontoMax" runat="server" ControlToValidate="txt_MontoMax" Cssclass="resalte_rojo" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Tabulador"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MontoMax" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                            ValidChars="." TargetControlID="txt_MontoMax"></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_MontoMax" runat="server" ControlToValidate="txt_MontoMax" Cssclass="resalte_rojo" 
                                            ErrorMessage="Monto incorrecto!"  ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"  ValidationGroup="val_Tabulador" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                        </div>                          
                                    </div>

                                    <div class="module_subsec_elements text_input_nice_div">                   
                                        <asp:DropDownList ID="cmb_TipoPersona" runat="server" class=" btn btn-primary2">
                                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="F">FISICA</asp:ListItem>
                                            <asp:ListItem Value="M">MORAL</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <label id="lbl_TipoPersona" class="text_input_nice_label">* Tipo de persona:</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TipoPersona" runat="server" ControlToValidate="cmb_TipoPersona" CssClass="resalte_rojo" 
                                                InitialValue="-1" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Tabulador">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements text_input_nice_div">                     
                                        <asp:TextBox runat="server" MaxLength="5" ID="txt_Multiplicador" class= "text_input_nice_input" ValidationGroup="val_Tabulador"></asp:TextBox>
                                            <div class="text_input_nice_labels">   
                                                <label id="lbl_Multiplicador" class="text_input_nice_label">* Multiplicador:</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Multiplicador" runat="server" ControlToValidate="txt_Multiplicador" Cssclass="resalte_rojo" 
                                                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Tabulador"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Multiplicador" runat="server" Enabled="True" FilterType="Custom, Numbers" 
                                                    ValidChars="." TargetControlID="txt_Multiplicador"></ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_Multiplicador" runat="server" ControlToValidate="txt_Multiplicador" Cssclass="resalte_rojo" 
                                                    ErrorMessage="Monto incorrecto!" display="Dynamic" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"  ValidationGroup="val_Tabulador">
                                                </asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                </div>               
           
                                <div class="module_subsec flex_end low_m">
                                    <asp:Button ID="btn_GuardarTabulador" runat="server"  class="btn btn-primary" Text="Guardar" ValidationGroup="val_Tabulador"/>
                                </div>

                                <p align="center">
                                    <asp:Label ID="lbl_info" runat="server" CssClass="alerta flex_1 module_subsec low_m flex_center"  Text="" Visible="True"></asp:Label>
                                </p>

                                <div class="overflow_x shadow">
                                        <asp:DataGrid ID="dag_Tabulador" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None"  TabIndex ="17" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                            <asp:BoundColumn DataField="MONTOMIN" HeaderText="Monto mínimo">
                                                <ItemStyle Width="125px"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MONTOMAX" HeaderText="Monto máximo">
                                                <ItemStyle Width="125px"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPOPER" HeaderText="Tipo persona">
                                                <ItemStyle Width="125px"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MULTIPLICADOR" HeaderText="Multiplicador">
                                                <ItemStyle Width="125px"/>
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                                <ItemStyle Width ="10%" />
                                            </asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>                    
            </section>                

            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>Operaciones fraccionadas</span>
                    <span class=" panel_folder_toogle down">&rsaquo;</span>
                </header>

                <div class="panel-body">
                    <div class="panel-body_content init_show">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="Conditional">
                            <ContentTemplate>
                            <h4 class="module_subsec">Oficinas:</h4>
                               <div class="module_subsec">
                                    <div class="overflow_x shadow flex_1">                                     
                                        <asp:GridView ID="dag_suc_si" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                            GridLines="None" HorizontalAlign="Center" TabIndex ="17" >                        
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="40" DataField="IDSUC" HeaderText="Id oficina">
                                                    <ItemStyle Width="20%"></ItemStyle>
                                                </asp:BoundField> 
                                                <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                                    <ItemStyle Width="50%"></ItemStyle>
                                                </asp:BoundField> 
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                     
                                            </Columns>
                                            <HeaderStyle Cssclass="table_header" />
                                        </asp:GridView>
                                    </div>  
                                </div>
                            <h4 class="module_subsec">Productos:</h4>
                                <div class="module_subsec">
                                    <div class="overflow_x shadow flex_1">                                     
                                        <asp:GridView ID="dag_prod_si" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                            GridLines="None" Width="100%">                       
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="40" DataField="IDPROD" HeaderText="Id producto">
                                                    <ItemStyle Width="20%"></ItemStyle>
                                                </asp:BoundField> 
                                                <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                                    <ItemStyle Width="50%"></ItemStyle>
                                                </asp:BoundField> 
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                     
                                            </Columns>
                                            <HeaderStyle Cssclass="table_header" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="module_subsec low_m columned three_columns" >
                                    <div class="module_subsec_elements text_input_nice_div">
                                         <asp:TextBox runat="server" MaxLength="10" ID="txt_FraccNumOperaciones" class="text_input_nice_input" ValidationGroup="val_ConfFraccionados"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_FraccNumOperaciones" runat="server" CssClass="text_input_nice_label" Text="*Número máximo de operaciones:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FraccNumOperaciones" runat="server" ControlToValidate="txt_FraccNumOperaciones"
                                             Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_ConfFraccionados"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FraccNumOperaciones" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_FraccNumOperaciones">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>

                                <p align="center">
                                    <asp:Label ID="lbl_InfoFraccionados" runat="server" CssClass="alerta"></asp:Label>
                                </p>
                                          
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_FraccNumOperaciones" runat="server" class="btn btn-primary" Text="Guardar"  
                                        ValidationGroup="val_ConfFraccionados"/>
                                </div>
                                                
                                <asp:Panel ID="pnl_Fraccionados" runat="server">
                                    <div class="module_subsec shadow">
                                        <asp:DataGrid ID="dag_Fraccionados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                            GridLines="None" Width="100%">
                                            <HeaderStyle Cssclass="table_header" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="IDSUCURSAL" Visible="false">
                                                        <ItemStyle Width="5%"/>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SUCURSAL" HeaderText="Oficina">
                                                        <ItemStyle Width="35%"/>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IDPRODUCTO" Visible="false">
                                                        <ItemStyle Width="5%"/>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PRODUCTO" HeaderText="Producto">
                                                        <ItemStyle Width="35%"/>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NUMOPE" HeaderText="Núm. operaciones">
                                                        <ItemStyle Width="15%"/>
                                                    </asp:BoundColumn>
                                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                                        <ItemStyle  Width ="10%" />
                                                    </asp:ButtonColumn>
                                                </Columns>
                                        </asp:DataGrid>
                                    </div>    
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>    
                </div>
            </section>
      
</asp:Content>

