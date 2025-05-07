<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_NOT_CONFIGURACION.aspx.vb" Inherits="SNTE5.CORE_NOT_CONFIGURACION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

        <section class="panel">
            <header class="panel-heading";>
                    Notificación
            </header>

            <div class="panel-body">
                <div class="module_subsec no_column align_items_flex_center">
                    <span class="text_input_nice_label">Número de Notificación:</span>
                   
                     <div class="module_subsec_elements" style="flex:1">
                        <asp:TextBox ID="txt_id_not" runat="server"  class="text_input_nice_input" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div> 
        </section>

        <section class="panel" >
            <header class="panel_header_folder panel-heading">
                <span>Datos</span>
                <span class=" panel_folder_toogle down"  href="#" >&rsaquo;</span>
            </header>

            <div class="panel-body">
                <div class="panel-body_content init_show">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                            <div class="module_sec low_m">
                                <div class="module_subsec no_column">
                                    <span  class=" title_tag">Activo:</span>
                                    <asp:CheckBox ID="ckb_activo" CssClass="mod_check"  runat="server" />
                                    </div>
                            </div>

                            <div class= "module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_nombre_notif" runat="server" class="text_input_nice_input" MaxLength="100" Enabled="false"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Nombre:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server" ControlToValidate="txt_nombre_notif"
                                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="_" TargetControlID="txt_nombre_notif">
                                                </ajaxtoolkit:filteredtextboxextender>
                                            </div>
                                    </div>
                                </div>

                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_clave_notif" runat="server" class="text_input_nice_input"  MaxLength="30" Enabled="false"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">Clave:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_clave_notif"
                                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                    FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars="_" TargetControlID="txt_clave_notif">
                                                </ajaxtoolkit:filteredtextboxextender>
                                            </div>
                                    </div>
                                </div>

                                <div class="module_subsec_elements"> 
	                                <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_tiempo_notif" runat="server" class="text_input_nice_input" MaxLength="9" ></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Tiempo de expiración:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_fallidos" runat="server" ControlToValidate="txt_tiempo_notif"
                                                CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Politica">
                                                </asp:RequiredFieldValidator>
                                                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_fallidos" runat="server" Enabled="True"
                                                FilterType="Numbers" TargetControlID="txt_tiempo_notif">
                                                </ajaxtoolkit:filteredtextboxextender>
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec ">
                                <div class="module_subsec_elements w_100"> 
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_descrip_notif" runat="server" class="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        <span class="text_input_nice_label">Descripción:</span>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec ">
                                <div class="module_subsec_elements w_100"> 
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_text_notif" runat="server" class="text_input_nice_textarea"   MaxLength="2000" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                        <span class="text_input_nice_label">Texto:</span>
                                    </div>
                                </div>
                            </div>  

                            <div class="module_subsec " >
                                <asp:Label ID="lbl_roles_rel" runat="server" Text="Mensaje"></asp:Label> 
                            </div>
                             <div class="module_subsec flex_center"> 
                                    <asp:Label ID="lbl_men" runat="server"  cssclass="alerta" Visible="false" ></asp:Label>                                                                                                                                                                                                                                                        
                                </div>
                            <div class="module_subsec flex_end"  >
                                    <asp:Button ID="btn_guardar"   runat="server" CssClass="btn btn-primary"  Text="Guardar" />                                                                                                                                                                                                                                                         
                               
                            </div>
                           

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_guardar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>

        </section>        

        <asp:Panel ID="pnl_roles" CssClass="panel" runat="server" Visible="false">
                <header class="panel_header_folder panel-heading">
                    <span>Roles relacionados</span>
                    <span class=" panel_folder_toogle up"  href="#" >&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                    
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="module_subsec">
                                <div class="overflow_x shadow" style="flex:1;">
                                    <asp:GridView ID="dag_rol_rel" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped" ShowHeader="true"
                                            GridLines="None"  TabIndex="17" >
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID">
                                                    <ItemStyle  Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                    <ItemStyle  Width="55%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                                                    <ItemStyle  Width="25%" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate> 
                                                        <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'  />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="table_header" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="module_subsec flex_center ">
                                    <asp:Label ID="lbl_rolAsigEstatus" runat="server" CssClass="alerta"  Visible="false" Text="Label"></asp:Label>
                                
                               
                            </div>
                            <div class="module_subsec flex_end">
                                 <asp:Button ID="btn_guardarRolesAsig" runat="server" OnClick="btn_guardarRolesAsig_Click" CssClass="btn btn-primary" Text="Guardar" />
                                </div>
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                                   
                                   
                    </div>
                </div>
            </asp:Panel>
       
</asp:Content>


