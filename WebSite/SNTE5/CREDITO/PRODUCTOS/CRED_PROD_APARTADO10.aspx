<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO10.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO10" MaintainScrollPositionOnPostback ="true" %>

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
            Asignación de productos a oficinas
        </header>
        <div class="panel-body">
               
            <asp:UpdatePanel ID="upd_pnl_AsignarRoles" runat="server">
                <ContentTemplate>  
                    <div class="module_subsec">
                        <div class="overflow_x flex_1 shadow">
                            <asp:GridView ID="dag_suc_prod" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="40" DataField="ID" HeaderText="Id oficina" HeaderStyle-CssClass="table_header">
                                        <ItemStyle></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre" ControlStyle-CssClass="table_header">
                                        <ItemStyle Width="80%"></ItemStyle>
                                    </asp:BoundField>   
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>'/>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:TemplateField>          
                                </Columns>                                
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end" >
                        <asp:Button ID="btn_guardar_r" class="btn btn-primary " OnClick="btn_guardar_r_Click" runat="server" Text="Guardar"/>
                    </div>                                   

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar_r" />
                </Triggers>                                       
            </asp:UpdatePanel>   
        </div>                          
    </section> 
        
</asp:Content>