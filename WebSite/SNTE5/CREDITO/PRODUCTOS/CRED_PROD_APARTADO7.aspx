<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO7.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO7" MaintainScrollPositionOnPostback ="true" %>

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
        <header class="panel_header_folder panel-heading">
            <span>Asignación de comisiones</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="upd_pnl_AsignarComisiones" runat="server">
                    <ContentTemplate> 
                        <div class="module_subsec">
                            <div class="overflow_x shadow flex_1">
                                <asp:GridView ID="dag_ComiAsigandas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">   
                                                       
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="40" DataField="IDC" HeaderText="Id comisión">
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                                <ItemStyle HorizontalAlign="Center" Width="70%"></ItemStyle>
                                        </asp:BoundField> 
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_ComiAsignada" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>'/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div> 
                        </div>

                        <div class="module_subsec flex_center"> 
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>       
                        </div>                    

                        <div class="module_subsec low_m align_items_flex_center flex_end">
                            <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" onClick="btn_asignar_Click" Text="Guardar" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </section>

    <section class="panel" >
        <header class="panel_header_folder panel-heading">
            <span>Atributos de comisiones</span>
            <span class=" panel_folder_toogle down"  href="#" >&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
               <asp:UpdatePanel ID="UpdatePanelperint" runat="server">
                    <ContentTemplate> 
                        <asp:Label ID="lbl_atributos" runat="server" class="subtitulos">Atributos de una comisión asignada a un producto.</asp:Label>
                 
                        <div class="module_subsec flex_end">
                            <asp:LinkButton ID="lnk_RegresarTotal" runat="server" CssClass="textogris" Text="Cerrar comisión" Visible="False"></asp:LinkButton>
                        </div>                
                                  
                        <asp:DataGrid ID="dag_Comisiones" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-striped" GridLines="None" >
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="IDCOMISION" Visible = "False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CLAVECOMISION" Visible = "False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMISION" HeaderText="Comisión">
                                    <ItemStyle Width="400px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ATRIBUTOS" HeaderText=" " Text="Atributos">
                                    <ItemStyle ForeColor="#054B66" Width="100px" HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>

                        <%--COMISION POR OPERACIONES (SEGURO Y FONDO DE GARANTIA--%>
                        <asp:Panel ID="pnl_COM_SEG" runat="server" Visible="False" Width="100%">
                            <div class="overflow_x shadow">
                                <asp:GridView ID="dag_com_seg" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-striped" GridLines="None" 
                                    HorizontalAlign="Center" TabIndex="17" Visible="False">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundField DataField="TIPO_PERSONAL" HeaderText="Tipo de Personal">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de Cobro">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddl_COM_SEG" SelectedValue='<%# Eval("TIPO_COBRO") %>' DataSource='<%# GetTipoCobroComSeg() %>' DataTextField="TIPO_COBRO" DataValueField="ID" CssClass="btn btn-primary2 dropdown_label w_70" runat="server"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center" HeaderText="Monto de Cobro" ItemStyle-Height="50px">
                                            <ItemTemplate>
                                                <asp:Textbox ID="txt_COM_SEG" Text='<%# Eval("MONTO_COBRO") %>' runat="server" CssClass="text_input_nice_input w_70"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_COM_SEG" runat="server" OnClick="btn_COM_SEG_Click" class="btn btn-primary" Text="Guardar" Enabled ="False"/>
                            </div>
                        </asp:Panel> 
                
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_atributos" runat="server" Font-Bold="True" 
                                Font-Names="Verdana" Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                        </div>                                 
                    </ContentTemplate>
                </asp:UpdatePanel>        
            </div>
        </div>
    </section>
</asp:Content>

