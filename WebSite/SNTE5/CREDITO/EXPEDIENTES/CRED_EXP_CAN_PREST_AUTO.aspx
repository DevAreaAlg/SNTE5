<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_CAN_PREST_AUTO.aspx.vb" Inherits="SNTE5.CRED_EXP_CAN_PREST_AUTO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Cancelacion de préstamos</span>
        </header>
        <div class="panel-body">
            <%--<div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_Instituciones" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Institución:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumCtrl" runat="server" ControlToValidate="ddl_Instituciones"
                                CssClass="textogris" Display="Dynamic" InitialValue="-1" ErrorMessage=" Falta Dato!" ValidationGroup="val_Depe_NumCtrl"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>--%>


            <br />
            <div class="overflow_x shadow module_subsec">

                <asp:GridView ID="dag_cancelacion" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>

                        <%--<asp:BoundField DataField="INSTITUCION" HeaderText="Institución">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="FOLIO" HeaderText="No. control" Visible="false">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CLAVE" HeaderText="Clave">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOMBRE" HeaderText="Agremiado">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="RFC" HeaderText="RFC">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MONTO" HeaderText="Monto">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Cancelar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

            <div class="module_subsec low_m columned four columns top_m flex_end">
                <asp:Button ID="btn_cancelar" runat="server" class="btn btn-primary" Text="Cancelar" Visible="false"/>
            </div>
            <div align="center">
                <asp:Label ID="lbl_cancelar" runat="server" CssClass="alerta"></asp:Label>
            </div>

        </div>
    </section>
</asp:Content>
