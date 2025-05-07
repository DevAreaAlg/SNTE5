<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DOC_GENERAR.aspx.vb" Inherits="SNTE5.COB_DOC_GENERAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="panel">
        <header class="panel-heading">
            <span>Documentos de cobranza</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec">
                <div class="overflow_x shadow" style="flex: 1;">

                    <asp:GridView ID="dag_AsigEventos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                        <Columns>
                            <asp:BoundField DataField="IDEVENTO" HeaderText="Id evento">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EVENTO" HeaderText="Nombre evento">
                                <ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IDPLANTILLA" HeaderText="Id plantilla">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DESTINATARIO"  ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="0%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FOLIO"   ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NOMBRE" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="0%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DIAS_MORA" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="0%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IDOTRA" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RUTA" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TIPODOC" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="0%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAGOSATRAZO" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                                <ItemStyle Width="0%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_PagAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("GENERAR")) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>&nbsp;
                <asp:Label ID="lbl_status_docs" runat="server" CssClass="alerta"></asp:Label>
                
            </div>
            <div class="module_subsec flex_end">
                <asp:Button ID="btn_gegBit" class="btn btn-primary module_subsec" runat="server" Text="Generar Bitacora" ValidationGroup="val_asignaPln" />
                <asp:Button ID="btn_genPre" class="btn btn-primary module_subsec" runat="server" Text="Generar Prellenado" />
            </div>
        </div>
    </section>
</asp:Content>

