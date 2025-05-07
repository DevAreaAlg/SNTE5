<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_APARTADO4.aspx.vb" Inherits="SNTE5.CAP_PROD_APARTADO4" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            Asignación de productos a oficinas
        </header>
        <div class="panel-body">

            <asp:UpdatePanel ID="upd_pnl_AsignaraSuc" runat="server">
                <ContentTemplate>

                    <h5 style="font-weight: normal" class="module_subsec">Seleccione las oficinas a las que desea asignar el producto</h5>
                    <div class="module_subsec">
                        <div class="overflow_x shadow flex_1" style="flex: 1;">
                            <asp:GridView ID="dag_suc_prod_cap" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="20%" DataField="ID" HeaderText="Id oficina" HeaderStyle-CssClass="table_header">
                                        <ItemStyle></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField ItemStyle-Width="60%" DataField="NOMBRE" HeaderText="Nombre" ControlStyle-CssClass="table_header">
                                        <ItemStyle></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="table_header" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta module_subsec flex_center align_items_flex_center flex_1"></asp:Label>
                    </div>
                    <div class="module_subsec low_m  flex_end">
                        <asp:Button ID="btn_guardar_r" class="btn btn-primary " OnClick="btn_guardar_r_Click" runat="server" Text="Guardar" />
                    </div>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardar_r" />

                </Triggers>

            </asp:UpdatePanel>

        </div>
    </section>

          
</asp:Content>

