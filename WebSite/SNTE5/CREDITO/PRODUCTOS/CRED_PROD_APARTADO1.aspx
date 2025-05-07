<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO1.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO1" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">                   
                <div class="module_subsec columned low_m align_items_flex_center">
                    <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                </div>                  
        </div>
    </section>

    <asp:UpdatePanel ID="up_plazos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
    <section class="panel">
        <header class="panel-heading">
            Plazos y montos
        </header>
        <div class="panel-body">
                 
            <div class="module_subsec low_m">
                <h5 class="no_bm">Plazos</h5>
            </div>

            <div class="module_subsec columned three_columns  low_m">
                <div class="module_subsec_elements text_input_nice_div ">                                    
                    <asp:TextBox ID="txt_min" runat="server" class="text_input_nice_input" MaxLength="4"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite inferior (Quincenas):</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtmin" runat="server" ControlToValidate="txt_min"
                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_plazos_montos">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtmin" runat="server" Enabled="True"
                            FilterType="Numbers" TargetControlID="txt_min">
                        </ajaxToolkit:FilteredTextBoxExtender> 
                    </div>
                </div>

                <div class="module_subsec_elements text_input_nice_div">  
                    <asp:TextBox ID="txt_max" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite superior (Quincenas):</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtmax" runat="server" ControlToValidate="txt_max"
                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_plazos_montos">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtmax" runat="server" Enabled="True"
                            FilterType="Numbers" TargetControlID="txt_max">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m">
                <h5 class="no_bm">Montos</h5>
            </div>

            <div class="module_subsec columned three_columns low_m ">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:TextBox ID="txt_infmonto" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite inferior ($):</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtinfmonto" runat="server" ControlToValidate="txt_infmonto"
                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_plazos_montos">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtinfmontos" runat="server" Enabled="True"
                            FilterType="Numbers" TargetControlID="txt_infmonto">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>                                    
                </div>  
                               
                <div class="text_input_nice_div module_subsec_elements ">
                    <asp:TextBox ID="txt_supmonto" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">*Límite superior ($):</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtsupmonto" runat="server" ControlToValidate="txt_supmonto"
                            CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_plazos_montos">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtsupmonto" runat="server" Enabled="True"
                            FilterType="Numbers" TargetControlID="txt_supmonto">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>     
                           
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_guarda_cambios" CssClass="btn btn-primary" OnClick="btn_guardar_montoplazos_Click" runat="server" Text="Guardar" ValidationGroup="val_plazos_montos" />
            </div>            
            
        </div>
    </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <section class="panel">
        <header class=" panel-heading">
            Fuentes de fondeo
        </header>
        <div class="panel-body">
               
            <asp:UpdatePanel ID="upd_pnl_AsignarFuentes" runat="server">
                <ContentTemplate>
                    <div class="module_subsec">
                        <div class="overflow_x shadow flex_1">
                            <asp:GridView ID="dag_mod_si" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="100px" DataField="ID" HeaderText="Id fuente"></asp:BoundField>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>                            
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_asigFuentesEstatus" runat="server" Text="" CssClass="alerta" ></asp:Label>                            
                    </div>

                    <div class="module_subsec flex_end">                             
                        <asp:Button ID="btn_guardar_r" class="btn btn-primary" runat="server" OnClick="btn_guardar_r_Click" Text="Guardar" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar_r" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </section>
</asp:Content>
