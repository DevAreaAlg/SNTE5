<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_DESCUENTOS_PROC.aspx.vb" Inherits="SNTE5.CRED_EXP_DESCUENTOS_PROC" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Pagos y descuentos por aplicar de préstamos a afiliado</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">

                <%-- <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate>--%>
                <div class="module_subsec low_m columned four_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_Periodos" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Fecha de Liberación de Préstamos:</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec">
                    <div class="module_subsec">
                        <asp:Button ID="btn_GetPolizaDescuentos" runat="server" class="btn btn-primary" Text="Descargar Póliza de Sueldos por Aplicar" ToolTip="Descargar Póliza Descuentos." Visible="false" />
                        &nbsp; &nbsp; &nbsp;
                    </div>
                    <div class="module_subsec">
                        <asp:Button ID="btn_CSVTESORERIA" runat="server" class="btn btn-primary" Text="Descargar Préstamos por Pagar " Visible="false" />
                    </div>
                    <div class="module_subsec">
                        <asp:Button ID="btn_CSVINSTITUCION" runat="server" class="btn btn-primary" Text="Descargar Información Préstamos" Visible="false" />
                    </div>
                </div>
                <div class="module_subsec_elements flex_end">
                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
            </div>

            <div class="module_subsec">
                <div class="overflow_x shadow" style="flex: 1;">
                    <asp:GridView ID="dag_Descuentos" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeader="true" Visible="false"
                        CssClass="table table-striped " GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <Columns>

                            <asp:BoundField DataField="NUM_TRAB" HeaderText="Núm. Afiliado">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPLEADO" HeaderText="Nombre">
                                <ItemStyle Width="25%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CVE_EXP" HeaderText="Folio de Préstamo">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NUMERO_PRESTAMO" HeaderText="Folio de Préstamo">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CLAVE_DESCUENTO" HeaderText="Clave de Descuento">
                                <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IMPORTE_CREDITO" HeaderText="Monto de Préstamo">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IMPORTE_DESCUENTO" HeaderText="Monto de Descuento">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NUMERO_DESCUENTOS" HeaderText="Núm. Descuentos">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
                </div>
            </div>
            <%-- </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="dag_Descuentos" />
                        <asp:PostBackTrigger ControlID="cmb_Periodos" />
                        <asp:PostBackTrigger ControlID="btn_GetPolizaDescuentos" />
                    </Triggers>
                </asp:UpdatePanel>--%>
        </div>
        </div>
    </section>
</asp:Content>
