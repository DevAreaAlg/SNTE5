<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_COTIZADOR_TAB.aspx.vb" Inherits="SNTE5.CRED_EXP_COTIZADOR_TAB" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=800,height=450,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Cotizador</span>
        </header>
        <div class="panel-body">
            <br />
            <div class="module_subsec low_m  columned four_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox runat="server" ID="tbx_rfc" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox runat="server" ID="txt_cliente" CssClass="text_input_nice_input" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <asp:LinkButton ID="btn_seleccionar" runat="server" class="btntextoazul" Text="Seleccionar"
                        ValidationGroup="val_cliente" />&nbsp;&nbsp;
                    <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                        Style="height: 18px; width: 18px;"></asp:ImageButton>
                </div>
                <div class="module_subsec_elements align_items_flex_end">
                </div>
                <div class="module_subsec_elements align_items_flex_end">
                    <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_resumen" Enabled="False" Visible="True">
                           Resumen agremiado
                    </asp:LinkButton>
                </div>
            </div>
            <div runat="server" id="pnl_cotizador" visible="false">
                <div class="module_subsec low_m columned two_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content ">
                            <asp:TextBox ID="lbl_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" class="text_input_nice_label" ID="Label2">Nombre del agremiado: </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned four_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_Producto" CssClass="btn btn-primary2 dropdown_label"
                                runat="server" ValidationGroup="val_planpago" AutoPostBack="true">
                            </asp:DropDownList>
                            <div class="text_innput_nice_labels">
                                <asp:Label runat="server" ID="lbl_bancochequesori" class="text_input_nice_label">*Producto:</asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmb_Producto"
                                    CssClass="textogris" ControlToValidate="cmb_Producto" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" InitialValue="0" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_sueldo" CssClass="text_input_nice_input" runat="server" Enabled="False"
                                ValidationGroup="val_planpago" MaxLength="20"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="lbl_sueldo" class="text_input_nice_label">*Sueldo neto quincenal:</asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_sueldos"
                                    CssClass="textogris" ControlToValidate="txt_sueldo" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_tipo_trabajador" CssClass="text_input_nice_input" runat="server" Enabled="False"
                                ValidationGroup="val_planpago" MaxLength="20"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="Label3" class="text_input_nice_label">*Tipo de agremiado:</asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2"
                                    CssClass="textogris" ControlToValidate="txt_tipo_trabajador" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_tasa" CssClass="text_input_nice_input" runat="server" Enabled="False"
                                ValidationGroup="val_planpago" MaxLength="10"></asp:TextBox>
                            <div class="text_innput_nice_labels">
                                <asp:Label runat="server" ID="lbl_tasa" class="text_input_nice_label">*Tasa ordinaria anual (%):</asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5"
                                    CssClass="textogris" ControlToValidate="txt_tasa" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned four_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_monto" CssClass="text_input_nice_input" runat="server" Enabled="false"
                                ValidationGroup="val_planpago" MaxLength="10"></asp:TextBox>
                            <div class="text_innput_nice_labels">
                                <asp:Label runat="server" ID="Label6" class="text_input_nice_label">*Monto a solicitar:</asp:Label>
                                <asp:Label ID="lbl_rango_monto" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6"
                                    CssClass="textogris" ControlToValidate="txt_monto" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    Enabled="True" FilterType="Numbers, Custom" ValidChars=",." TargetControlID="txt_monto">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_plazo" CssClass="text_input_nice_input" runat="server" Enabled="false"
                                ValidationGroup="val_planpago" MaxLength="2"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_plazo">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="Label1" class="text_input_nice_label">*Plazo del préstamo:</asp:Label>
                                <asp:Label ID="lbl_rango_plazo" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3"
                                    CssClass="textogris" ControlToValidate="txt_plazo" Display="Dynamic"
                                    ErrorMessage=" Falta Dato!" ValidationGroup="val_planpago" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="txt_cappago" CssClass="text_input_nice_input" Enabled="False" />
                            <div class="text_innput_nice_labels">
                                <asp:Label runat="server" ID="lbl_cappago" CssClass="text_input_nice_label"
                                    Text="*Capacidad de pago:" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="txb_saldo_restante" CssClass="text_input_nice_input" Enabled="False" />
                            <div class="text_innput_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Saldo Restante (PA o PD):" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Label ID="lbl_PARAM" runat="server" Text="Label" CssClass="alerta" Visible="false"></asp:Label>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_cotizar" class="btn btn-primary" runat="server" ValidationGroup="val_planpago" Text="Cotizar" />
                    &nbsp;&nbsp;&nbsp;&nbsp; 
                               <asp:Button ID="btn_limpiar" class="btn btn-primary" runat="server" Text="Limpiar" />
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status_pfsi" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <asp:Label ID="LBL_ESTA" runat="server" CssClass="alerta"></asp:Label>
                <%--<div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_descargar" Enabled="true" Visible="false">
                           Descargar cotización<i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div>--%>
                <div class="overflow_x panel shadow">
                    <asp:DataGrid ID="dag_sugeridor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="MONTO" HeaderText="Monto máximo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MONTO_DEC" HeaderText="Monto máximo" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLAZO" HeaderText="Plazo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TASA_ORD" HeaderText="Tasa ordinaria anual (%)"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAGO" HeaderText="Pago fijo"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="PLANPAGO" Text="Ver plan de pago">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
            <div align="center">
                <asp:Label ID="lbl_status_general" runat="server" CssClass="alerta"></asp:Label>
            </div>
        </div>
    </section>
</asp:Content>
