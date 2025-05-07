<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_PRESUPUESTOS.aspx.vb" Inherits="SNTE5.CRED_EXP_PRESUPUESTOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <span>Presupuesto</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content">



                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_Quincenas" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" />
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Periodo:</span>
                                    <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                        ControlToValidate="cmb_Quincenas" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_base">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="txt_saldoAct" CssClass="text_input_nice_input" MaxLength="13" Enabled="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Saldo Actual:" />

                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="false"
                                    FilterType="Numbers" TargetControlID="txt_saldoAct">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>

                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="txt_monto" CssClass="text_input_nice_input" MaxLength="13" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Monto Presupuesto:" />
                                <%--<asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_monto" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_base">
                                </asp:RequiredFieldValidator>--%>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_monto">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>

                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="txt_comisiones" CssClass="text_input_nice_input" MaxLength="13" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Monto Comisiones:" />
                           <%--     <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_comisiones" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_base">
                                </asp:RequiredFieldValidator>--%>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txt_comisiones">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>

                    </div>

                    <div class="module_subsec low_m columned three columns top_m flex_end">
                        <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                            <asp:Button ID="btn_guardar_pres" CssClass="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_base" />

                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_estatusPres" runat="server" CssClass="alerta" />
                    </div>
                    
                    <div class="module_subsec">
                        <div class="overflow_x shadow" style="flex: 1;">

                            <asp:DataGrid ID="dag_AuxPres" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" GridLines="None">
                                <Columns>
                                    <asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" ForeColor="#074b7a" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ANIO" HeaderText="Año" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" ForeColor="#074b7a" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" ForeColor="#074b7a" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA_ABONO" HeaderText="Fecha Abono" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" ForeColor="#074b7a" />
                                    </asp:BoundColumn>
                                     <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus" Visible="true">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" ForeColor="#074b7a" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
