<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO9.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO9" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                
    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body"> 
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:Textbox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>

    <section class="panel" >
        <header class="panel-heading">
            <span>Asignación de pagarés</span>
        </header>
        <div class="panel-body">             
            <asp:UpdatePanel ID="upd_PagAsignados" runat="server">
                <ContentTemplate> 
                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex:1;">
                            <asp:GridView ID="dag_PagAsigandos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                Font-Size="10pt"  GridLines="None" HorizontalAlign="Center" TabIndex ="17">                        
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="40" DataField="IDPAGARE" HeaderText="Id pagaré">
                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="55%"></ItemStyle>
                                    </asp:BoundField>   
                                    <asp:BoundField ItemStyle-Width="245" DataField="TIPO" HeaderText="Tipo">
                                        <ItemStyle Width="30%"></ItemStyle>
                                    </asp:BoundField>   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_PagAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>                     
                                </Columns>
                                <HeaderStyle Cssclass="table_header"/>
                            </asp:GridView>
                            </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>    
                    </div>

                    <div class="module_subsec flex_end">                        
                        <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" onClick="btn_asignar_Click" Text="Guardar" />
                    </div>
                          
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                </Triggers>
            </asp:UpdatePanel>   
        </div>         
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Asignación de contratos</span>
        </header>
        <div class="panel-body">         
           <asp:UpdatePanel ID="upd_contratos" runat="server">
                <ContentTemplate> 
                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex:1;">
                            <asp:GridView ID="dag_contratos" runat="server" AutoGenerateColumns="False"   CssClass="table table-striped"
                                    GridLines="None" >                        
                                <Columns>
                                    <asp:BoundField  DataField="IDCONTRATO" HeaderText="Id contrato">
                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                        <ItemStyle Width="85%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                  
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_contrAsig" runat="server"  Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>                     
                                </Columns>
                                <HeaderStyle Cssclass="table_header"/>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_contratosEstatus" CssClass="module_subsec low_m alerta" runat="server"  Text="Label" Visible="false"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">                
                        <asp:Button ID="btn_guardarContratosAsig" runat="server" OnClick="btn_asignar_Contrato_Click" CssClass="btn btn-primary" Text="Guardar" />
                    </div>
                   
                </ContentTemplate>

                 <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_guardarContratosAsig" />
                </Triggers>


            </asp:UpdatePanel>                       
        </div>             
</section>
    

    <section class="panel">
        <header class="panel-heading">
            <span>Asignación de solicitudes</span>
        </header>
        <div class="panel-body">         
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex:1;">
                            <asp:GridView ID="dag_solic" runat="server" AutoGenerateColumns="False"   CssClass="table table-striped"
                                    GridLines="None" >                        
                                <Columns>
                                    <asp:BoundField  DataField="IDSOLIC" HeaderText="Id solicitud">
                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NOMBRE" HeaderText="Solicitud">
                                        <ItemStyle Width="85%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundField>
                                  
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                              <asp:CheckBox ID="chk_SAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>                     
                                </Columns>
                                <HeaderStyle Cssclass="table_header"/>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_solicitudes" CssClass="module_subsec low_m alerta" runat="server"  Text="Label" Visible="false"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">                
                        <asp:Button ID="btn_solicitudes" runat="server" OnClick="btn_solicitudes_Click" CssClass="btn btn-primary" Text="Guardar" />
                    </div>
                   
                </ContentTemplate>
                  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_solicitudes" />
                </Triggers>

             


            </asp:UpdatePanel>                       
        </div>             
</section>
</asp:Content>

