<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_MESA_CTRL.aspx.vb" Inherits="SNTE5.CRED_EXP_MESA_CTRL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span>Asignación de miembros a comités</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec low_m columned three_columns align_items_flex_start">
                <div class="module_subsec_elements vertical">
                    <asp:Label runat="server" Text="*Seleccione comité:" ID="lbl_comite"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_comite" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                </div>
                <asp:Panel CssClass="module_subsec_elements module_subsec no_column align_items_flex_center" runat="server" Visible="false" ID="pnl_chkActivarDes">
                    <asp:Label runat="server" Text="Activar comité:" CssClass="module_subsec_elements" ID="Label1"></asp:Label>
                    <asp:CheckBox ID="Chk_ActivaDesactivar" runat="server" DataTextField=" " DataValueField="ID"
                        CssClass="textocajas module_subsec_elements" AutoPostBack="True"></asp:CheckBox>

                </asp:Panel>
            </div>

            <asp:Panel ID="pnl_dtgCom" runat="server" Visible="false">


                <div class="module_subsec">
                    <div class="overflow_x shadow" style="flex: 1;">
                        <asp:GridView ID="dag_comite" runat="server" AutoGenerateColumns="False" CssClass="table table-striped "
                            GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="Id usuario">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USUARIO" HeaderText="Nombre"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("GRANTED")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="table_header" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_StatusActivo" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" OnClick="btn_asignar_Click" Text="Guardar" />
                </div>
            </asp:Panel>
        </div>


    </section>

</asp:Content>
