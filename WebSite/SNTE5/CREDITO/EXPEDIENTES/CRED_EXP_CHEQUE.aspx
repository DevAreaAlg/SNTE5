<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_CHEQUE.aspx.vb" Inherits="SNTE5.CRED_EXP_CHEQUE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
            }
            else {
                var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:640px;dialogWidth:650px");
            }
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
    </script>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_info" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>
    <section class="panel">
        <header class="panel-heading">
            <span>Agremiado</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements text_input_nice_div">
                    <asp:TextBox runat="server" ID="tbx_rfc" CssClass="text_input_nice_input" MaxLength="13" />
                    <asp:TextBox runat="server" ID="txt_IdCliente" CssClass="text_input_nice_input" MaxLength="9" Visible="false" />
                    <div class="text_input_nice_labels">
                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                            ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_Depe_NumCtrl">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                            FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="tbx_rfc">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
                <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" ValidationGroup="val_Depe_NumCtrl" />
                    <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar agremiado" />
                </div>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </span>
                <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
            </div>

            <div align="right">
                <asp:Label runat="server" ID="lbl_num" CssClass="module_subsec_elements module_subsec_medium-elements" Text="N° de cheque:" Visible="false" />
                <asp:TextBox runat="server" ID="txt_num" MaxLength="7" Enabled="true"
                    CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Visible="false" />
                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                    TargetControlID="txt_num" />

            </div>

            <br />
            <div class="overflow_x shadow ">
                <!-- Tabla de Expedientes generados por sucursal -->
                <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundField DataField="CLAVE" HeaderText="Folio">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° de cheque">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Deshacer">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Eliminar" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Reposición">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Reposicion" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Imprimir">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Imprimir" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
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
                <asp:Button ID="btn_eliminar" runat="server" class="btn btn-primary" Text="Deshacer" Visible="false" ValidationGroup="val_Depe_NumCtrl"></asp:Button>
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btn_reposicion" runat="server" class="btn btn-primary" Text="Reposición" Visible="false" ValidationGroup="val_Depe_NumCtrl"></asp:Button>
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btn_imprimir" runat="server" class="btn btn-primary" Text="Imprimir" Visible="false" ValidationGroup="val_Depe_NumCtrl"></asp:Button>
            </div>


            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_ife" runat="server" CssClass="alerta" />
                <asp:Label ID="lbl_info_ide" CssClass="alerta" Visible="True" runat="server" />
                <asp:Label ID="lbl_info_disp" runat="server" CssClass="alerta" Visible="True" />
            </div>

        </div>
    </section>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

</asp:Content>
