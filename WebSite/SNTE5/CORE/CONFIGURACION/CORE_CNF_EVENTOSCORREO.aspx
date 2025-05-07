<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_EVENTOSCORREO.aspx.vb" Inherits="SNTE5.CORE_CNF_EVENTOSCORREO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Evento de Correo</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec low_m columned three_columns align_items_flex_start">
                <div class="module_subsec_elements vertical">
                    <asp:Label runat="server" Text="*Evento:" ID="lbl_evento"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddl_eventos" CssClass="btn btn-primary2 dropdown_label" />
                </div>
            </div>
            <asp:Panel ID="pnl_usuarios" runat="server" Visible="false">
                <asp:UpdatePanel ID="udp_usuarios" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec">
                            <div class="overflow_x shadow" style="flex: 1;">
                                <asp:GridView ID="dag_usuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Id Usuario">
                                            <ItemStyle Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARIO" HeaderText="Usuario"></asp:BoundField>
                                        <asp:BoundField DataField="TIPOUSUARIO" HeaderText="Tipo Usuario"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# (Eval("GRANTED")) %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus_activo" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_asignar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
                </div>
            </asp:Panel>
        </div>
    </section>
</asp:Content>

