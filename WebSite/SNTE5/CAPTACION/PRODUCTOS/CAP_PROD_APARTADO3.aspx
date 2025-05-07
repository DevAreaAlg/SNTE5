<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_APARTADO3.aspx.vb" Inherits="SNTE5.CAP_PROD_APARTADO3" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <section class="panel" id="panel_datos_pagos">
            <header class="panel-heading">
                <span>Producto</span>
            </header>
            <div class="panel-body">
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <asp:Textbox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                    </div>
            </div>
        </section>

        <section class="panel" id="panel_parametros">
            <header class="panel_header_folder panel-heading">
                <span>Comisiones</span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
                     
                    <h5 class="module_subsec">Asignación de comisiones a un producto en específico</h5>

                    
                        
                            <asp:GridView ID="dag_ComiAsigandas" runat="server" AutoGenerateColumns="False" GridLines="None" Width="100%">                        
                                <Columns>
                                    <asp:BoundField DataField="IDC" HeaderText="ID Comisión">
                                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField  DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Center" Width="70%"></ItemStyle>
                                    </asp:BoundField>   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_ComiAsignada" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>                     
                                </Columns>
                                <HeaderStyle Cssclass="table table_header" />
                            </asp:GridView>
                        
                    
                     <div class="module_subsec flex_center">
                          <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                         </div>
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" onClick="btn_asignar_Click" Text="Guardar" />
                    </div>             
                      
                </div>
            </div>
        </section>

        <section class="panel" id="panel_atributos">
            <header class="panel_header_folder panel-heading">
                <span>Atributos</span>
                <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
        
                        <h5 style="font-weight: normal" class="module_subsec">Atributos de una comisión asignada a un producto.</h5>

                    <div class="module_subsec flex_end">
                        <asp:Label ID="lbl_atributos" runat="server" class="subtitulos"></asp:Label>
                        <asp:LinkButton ID="lnk_RegresarTotal" runat="server" CssClass="textogris" Text="Cerrar Comisión" Visible="False"></asp:LinkButton>
                    </div>
                    <div class="module_subsec">
                    <div class="overflow_x shadow">
                        <asp:DataGrid ID="dag_Comisiones" runat="server" AutoGenerateColumns="False"  Width="100%"
                            GridLines="None"  > 
                                                
                            <Columns>
                                <asp:BoundColumn DataField="IDCOMISION" Visible = "False"><ItemStyle Width="20%" HorizontalAlign="Center" /></asp:BoundColumn>
                                <asp:BoundColumn DataField="CLAVECOMISION" Visible = "False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMISION" HeaderText="Comisión">
                                    <ItemStyle Width="80%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ATRIBUTOS" HeaderText=" " Text="Atributos">
                                    <ItemStyle  Width="20%" HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonColumn>
                            </Columns>
                            <HeaderStyle Cssclass="table table_header" />
                        </asp:DataGrid>
                    </div>
                                </div>      
                                    
                    <asp:Panel ID="pnl_SaldoMinVista" runat="server" Visible="False" >

                        <div class= "module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:RadioButton ID="rad_SaldoMinVista_Monto" class="texto" runat="server" GroupName="SaldoMinVista" 
                                    Text="Monto" AutoPostBack ="True"/>
                                    <asp:RadioButton ID="rad_SaldoMinVista_Porcentaje" class="texto" runat="server" 
                                    GroupName="SaldoMinVista" Text="Porcentaje" AutoPostBack ="True"/>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_SaldoMinVistaTipoCobro" runat="server" CssClass="texto">Tipo de Cobro:</asp:Label>
                                        </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements"> 
	                            <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_Valor" runat="server" class="text_input_nice_input" MaxLength="9" ValidationGroup="val_SaldoMinVista">
                                        </asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_ValorMin" runat="server" CssClass="texto">*Valor:</asp:Label>
                                            <asp:Label ID="lbl_TipoMin" runat="server" CssClass="texto"></asp:Label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Valor" runat="server" ControlToValidate="txt_Valor"
                                                Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_SaldoMinVista"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Valor" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_Valor"></ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Valor" runat="server" Display="Dynamic" 
                                                ControlToValidate="txt_Valor" Cssclass="textogris" ErrorMessage=" Error:Monto incorrecto" 
                                                class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                </div>
                            </div>
                        </div>

                                            <div class="module_subsec align_items_flex_center low_m flex_end">
                                                    <asp:Label ID="lbl_status_atributos" runat="server" CssClass="alerta module_subsec flex_center align_items_flex_center flex_1"></asp:Label>>
                            <asp:Button ID="btn_SaldoMinVista" runat="server" ValidationGroup="val_SaldoMinVista" class="btn btn-primary"  
                                    Text="Guardar" Enabled ="False"/>
                        </div>
                            
                    </asp:Panel>

                   
                    </div>
                </div>
            </section>
        
</asp:Content>

