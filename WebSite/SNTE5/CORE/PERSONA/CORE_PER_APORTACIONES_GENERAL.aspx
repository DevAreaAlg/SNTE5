<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CORE_PER_APORTACIONES_GENERAL.aspx.vb" Inherits="SNTE5.CORE_PER_APORTACIONES_GENERAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="panel">
        <header class="panel-heading">
            <span class="panel_folder_toogle_header">Aportaciones</span>
        </header>
        <div class="panel-body">
           
            <div class="panel-body_content" >
                     <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate>
                <div class="module_subsec">
                    <div class="overflow_x shadow flex_1" style="flex: 1;">
                    <asp:GridView ID="dag_Aport" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" 
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="RFC" HeaderText="RFC">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                                                        
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="APORTACION" HeaderText="Total Aportaciones">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="APOR_ENTIDAD" HeaderText=" Aportacion por Entidad">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="SALDOS" HeaderText="Monto PC">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DIF_REAL" HeaderText="Total Real">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>   
                               <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Omitir">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_NoAplicado" runat="server" Checked='<%# Convert.ToBoolean(Eval("BANDERA")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                           </Columns>
                        </asp:GridView>
                        </div>
                </div>

                <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                <div class="module_subsec low_m  flex_end">
                        <asp:Button ID="btn_guardar_r" class="btn btn-primary " runat="server" Text="Hacer devolución" />
                    </div>

                    </ContentTemplate>
                   
                </asp:UpdatePanel>


                </div>
            </div>

    </section>

</asp:Content>
