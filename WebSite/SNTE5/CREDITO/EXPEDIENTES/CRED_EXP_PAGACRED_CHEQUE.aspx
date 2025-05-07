<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_PAGACRED_CHEQUE.aspx.vb" Inherits="SNTE5.CRED_EXP_PAGACRED_CHEQUE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Pago de Préstamos con cheque</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_pres" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Préstamo:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_prestamo" runat="server" ControlToValidate="ddl_pres"
                                CssClass="textogris" Display="Dynamic" InitialValue="-1" ErrorMessage=" Falta Dato!" ValidationGroup="val_prestamo"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements text_input_nice_div">
                    <asp:TextBox runat="server" ID="txt_num" CssClass="text_input_nice_input" MaxLength="7" />
                    <div class="text_input_nice_labels">
                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="N° de cheque:" />
                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                            ControlToValidate="txt_num" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_prestamo">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                            FilterType="Numbers" TargetControlID="txt_num">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_guardar" CssClass="btn btn-primary" runat="server" Text="Guardar"  />

                </div>
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_imp" CssClass="btn btn-primary" runat="server" Text="IMPRIMIR"  />

                </div>
            </div>



            <div align="center">
                <asp:Label ID="lbl_guardar" runat="server" CssClass="alerta"></asp:Label>
            </div>




        </div>
        <div class="panel-body">

            <div align="right">
                <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" />
            </div>
            <div align="right">
                <asp:Label runat="server" ID="lbl_registros_tol" CssClass="module_subsec_elements module_subsec_medium-elements" />
            </div>
            <div align="right">
                <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto actual:</span>
                <asp:TextBox ID="lbl_presactual" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
            </div>
            <div align="right">
                <span class="module_subsec_elements module_subsec_medium-elements">Total:</span>
                <asp:TextBox ID="lbl_acumulado" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
            </div>
            <div align="right">
                <span class="module_subsec_elements module_subsec_medium-elements">Presupuesto final:</span>
                <asp:TextBox ID="lbl_presfinal" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
            </div>
            <%-- <div align="right">
                <span class="module_subsec_elements module_subsec_medium-elements">Número de archivo:</span>
                <asp:TextBox ID="txt_archivo" MaxLength="3" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="true"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_archivo" CssClass="alertaValidator" ControlToValidate="txt_archivo"
                    Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_laybancos" />
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_archivo" runat="server" Enabled="True"
                    FilterType="Numbers" TargetControlID="txt_archivo">
                </ajaxToolkit:FilteredTextBoxExtender>
            </div>--%>

            <br />
            <div class="overflow_x shadow ">
                <!-- Tabla de Expedientes generados por sucursal -->
                <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundField DataField="CLAVE" HeaderText="Folio">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CLIENTE" HeaderText="Agremiado">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NUMCTRL" HeaderText="RFC">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MONTO" HeaderText="Monto Real">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="VOTO" HeaderText="Voto" Visible="false">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° cheque">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PAGARE" HeaderText="Pagaré">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderText="Pagar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="true" OnCheckedChanged="Suma" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                <asp:Label runat="server" ID="CLAVE_PRODUCTO" Visible="false" Text='<%#Eval("CLAVE_PRODUCTO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NUMCLIENTE" Visible="false" Text='<%#Eval("NUMCLIENTE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="module_subsec low_m columned four columns top_m flex_end">
                <%--  <asp:Button ID="btn_prov" runat="server" Class="btn btn-primary" Text="Layout Proveedores" Visible="False" ValidationGroup="val_layproveedores" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btn_pagos" runat="server" class="btn btn-primary" Text="Layout para Banco" Visible="false" ValidationGroup="val_laybancos" />
                &nbsp; &nbsp; &nbsp;--%>
                <asp:Button ID="btn_pagar" runat="server" class="btn btn-primary" Text="Pagar" Visible="False" />
            </div>

            <div align="center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
            </div>
        </div>


    </section>
</asp:Content>
