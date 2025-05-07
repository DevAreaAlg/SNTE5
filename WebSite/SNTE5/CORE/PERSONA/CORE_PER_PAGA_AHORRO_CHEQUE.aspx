+<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PAGA_AHORRO_CHEQUE.aspx.vb" Inherits="SNTE5.CORE_PER_PAGA_AHORRO_CHEQUE" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Pago de Ahorro con Cheque</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">

                <contenttemplate>

                    <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">

                        <div class="module_subsec columned low_m three_columns">

                            <div class="module_subsec_elements text_input_nice_div">
                                <asp:TextBox ID="txt_IdCliente1" runat="server" class="text_input_nice_input" MaxLength="20"></asp:TextBox>
                                <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="20" Visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="*Ingrese el número de control o de cheque:"></asp:Label>

                                    <ajaxToolkit:FilteredTextBoxExtender ID="FTXT_IDCLIENTE" runat="server" Enabled="True"
                                        FilterType="Numbers,UppercaseLetters, LowercaseLetters" TargetControlID="txt_IdCliente1">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_IdCliente1"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Depe_NumCtrl"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                                <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" AUTOPOSTBACK="TRUE" Text="Continuar" ValidationGroup="val_Depe_NumCtrl" />
                                <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar afiliado" Visible="false" />
                            </div>
                        </div>

                        <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                            <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de afiliado: </span>
                            <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
                        </div>

                        <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
                        <asp:Label ID="lbl_maxreest" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>

                    </div>

                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements text_input_nice_div">

                            <asp:DropDownList ID="cmb_sistema" runat="server" class="btn btn-primary2 dropdown_label" Visible="false" AutoPostBack="true">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_sistema" runat="server" CssClass="text_input_nice_label" Visible="false" Text="*Sistema:"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec columned low_m five_columns">
                        <!-- combobox Región -->
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="ddl_Region" CssClass="btn btn-primary2 dropdown_label"
                                    runat="server" AutoPostBack="false" Visible="false" />
                                <div class="text_input_nice_labels" visible="false">
                                    <span class="text_input_nice_label" visible="false"></span>
                                </div>
                            </div>
                        </div>
                        <!-- combobox Delegación -->
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="ddl_Delegacion" CssClass="btn btn-primary2 dropdown_label"
                                    runat="server" AutoPostBack="false" Visible="false" />
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label" visible="false"></span>
                                </div>
                            </div>
                        </div>
                        <!-- combobox Estatus Cheque -->
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="ddl_EstatusCheque" CssClass="btn btn-primary2 dropdown_label"
                                    runat="server" AutoPostBack="false" Visible="false" />
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label" visible="false"></span>
                                </div>
                            </div>
                        </div>

                        <!-- button Buscar -->
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:Button ID="btnConsultar" runat="server" class="btn btn-primary" Text="Consultar" ValidationGroup="fechaLimitePago" Visible="false" />
                            </div>
                        </div>

                    </div>

                    <div class="module_subsec columned low_m  four_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">

                                <asp:DropDownList ID="cmb_Ciclo" runat="server" class="btn btn-primary2 dropdown_label" Visible="false" AutoPostBack="true">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ciclos" runat="server" CssClass="text_input_nice_label" Visible="false" Text="Ciclo:"></asp:Label>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div visible="false" class="module_subsec columned low_m three_columns">

                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox runat="server" ID="txt_num" CssClass="text_input_nice_input" MaxLength="18" Visible="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lblNumDeChaqueInicial" runat="server" CssClass="text_input_nice_label" Text="N° de cheque inicial:" Visible="false" />
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
                            <asp:Button ID="btn_asignar" CssClass="btn btn-primary" runat="server" Text="Asignar" ValidationGroup="val_prestamo" AUTOPOSTBACK="TRUE" Visible="false" />
                        </div>

                    </div>

                    <div align="right">
                        <asp:Label runat="server" ID="lbl_registros_sel" CssClass="module_subsec_elements module_subsec_medium-elements" Visible="false" />
                    </div>
                    <div align="right">
                        <asp:Label runat="server" ID="lbl_registros_tol" CssClass="module_subsec_elements module_subsec_medium-elements" Visible="false" />
                    </div>

                    <div align="right">
                        <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Total:" Visible="false" />
                        <asp:TextBox runat="server" ID="lbl_acumulado" CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" Enabled="false" Visible="false" />
                    </div>

                    <div align="right">
                        <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Ultimo cheque generado:" Visible="false" />
                        <asp:TextBox runat="server" ID="txt_secuencia" MaxLength="2" Enabled="true" Visible="false"
                            CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements" />
                        <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator" ControlToValidate="txt_secuencia"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_laybancos" />
                        <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                            TargetControlID="txt_secuencia" />
                    </div>
                    <div align="right">
                        <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Imprimir todos:" Visible="false" />
                        <asp:CheckBox runat="server" ID="ckb_ImprimirTodos" AutoPostBack="true" Visible="false" />
                        <div align="right">
                            <asp:Label ID="lbl_deshacer" runat="server" Visible="false" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Deshacer todos:" />
                            <asp:CheckBox runat="server" ID="ckb_deshacer" AutoPostBack="true" Visible="false" />
                        </div>

                        <div align="right">
                            <asp:Label ID="lbl_Entregado" runat="server" Visible="false" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Entregar todos:" />
                            <asp:CheckBox runat="server" ID="ckb_Entregado" AutoPostBack="true" Visible="false" />
                        </div>

                        <div align="right">
                            <asp:Label ID="lbl_pagarTodos" runat="server" Visible="false" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Pagar todos:" />
                            <asp:CheckBox runat="server" ID="ckb_Pagar" AutoPostBack="true" Visible="false" />
                        </div>

                        <div align="right">
                            <asp:Label ID="lbl_Cancelar" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Visible="false" Text="Cancelar todos:" />
                            <asp:CheckBox runat="server" ID="ckb_cancelar" AutoPostBack="true" Visible="false" />
                        </div>
                    </div>
                    <br />
                    <div class="overflow_x shadow ">
                        <asp:GridView ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%" AutoPostBack="true" Visible="false">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>

                                <asp:BoundField DataField="ID_AGREMIADO" HeaderText="Agremiado" Visible="FALSE">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AGREMIADO" HeaderText="AGREMIADO">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RFC" HeaderText="RFC">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RFC_ANTERIOR" HeaderText="RFC_ANTERIOR" Visible="false">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SISTEMA" HeaderText="SISTEMA">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGION" HeaderText="REGION">
                                    <ItemStyle Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DELEGACION" HeaderText="DELEGACIÓN">
                                    <ItemStyle Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SALDO_AHORRO" HeaderText="SALDO AHORRO" ItemStyle-HorizontalAlign="center" DataFormatString="${0:###,###,###.00}">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° CHEQUE" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS_CHEQUE" HeaderText="ESTATUS CHEQUE">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_ESTATUS_CHEQUE" HeaderText="ID_ESTATUS" Visible="FALSE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="NUMCLIENTE" HeaderText="NUMCLIENTE" Visible="FALSE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_OPERACION" HeaderText="OPERACION" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_PLAZA" HeaderText="TIPO_PLAZA" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ID_ESTATUS_TRANSFERENCIA" HeaderText="ID_ESTATUS_TRANSFERENCIA" Visible="FALSE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTATUS_TRANSFERENCIA" HeaderText="ESTATUS_TRANSFERENCIA" Visible="true">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CLAVE_RASTREO" HeaderText="CLAVE_RASTREO" Visible="true">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NUM_PAQUETE" HeaderText="NUM_PAQUETE" Visible="true">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_CVE_RASTREO" HeaderText="FECHA_CVE_RASTREO" Visible="true">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GENERADO_POR" HeaderText="GENERADO POR" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_GENERADO" HeaderText="FECHA GENERADO" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ENTREGADO_POR" HeaderText="ENTREGADO POR" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_ENTREGADO" HeaderText="FECHA ENTREGADO" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PAGADO_POR" HeaderText="PAGADO POR" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_PAGADO" HeaderText="FECHA PAGADO" Visible="TRUE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CANCELADO_POR" HeaderText="CANCELADO POR" Visible="FALSE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_CANCELADO" HeaderText="FECHA CANCELADO" Visible="FALSE">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="IMPRIMIR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chb_Imprimir" runat="server" AutoPostBack="FALSE" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="DESHACER" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chb_Deshacer" runat="server" AutoPostBack="FALSE" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="ENTREGAR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chb_Entregar" runat="server" AutoPostBack="FALSE" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="PAGAR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chb_Pagar" runat="server" AutoPostBack="FALSE" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="CANCELAR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chb_Cancelar" runat="server" AutoPostBack="FALSE" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="NUMCLIENTE" Visible="false" Text='<%#Eval("NUMCLIENTE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label DataField="NUM_CHEQUE" runat="server" ID="NUM_CHEQUE" Visible="FALSE" Text='<%#Eval("NUM_CHEQUE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%-- 
                        --------------------------------
                        BOTONES PARA MANEJO DE CHEQUES--
                        --------------------------------
                    --%>

                    <div>

                        <div class="module_subsec low_m columned four columns top_m flex_end">

                            <%-- Imprimir cheque --%>
                            <div class="module_subsec columned no_m align_items_flex_center">

                                <asp:CheckBox ID="chb_Fecha" runat="server" AutoPostBack="true" Text="Fecha vacia" Visible="false" />
                                &nbsp; &nbsp;

                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox runat="server" ID="txb_FechaCheque" CssClass="text_input_nice_input" MaxLength="10" ValidationGroup="fechaCheque" Visible="false" Enabled="true" />
                                        <asp:Label runat="server" ID="lblFechaDeCheque" Text="Fecha de cheque:" Visible="false" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txb_FechaCheque" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="fechaCheque" />
                                        <ajaxToolkit:MaskedEditValidator runat="server" ID="mev_fecha_vencimiento" ControlExtender="mee_fecha_vencimiento" ControlToValidate="txb_FechaCheque" CssClass="textogris" ErrorMessage="mev_fecha_vencimiento" InvalidValueMessage="Fecha Invalida" ValidationGroup="fechaCheque" />
                                        <ajaxToolkit:MaskedEditExtender runat="server" ID="mee_fecha_vencimiento" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txb_FechaCheque" />
                                        <ajaxToolkit:CalendarExtender runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txb_FechaCheque" />
                                    </div>
                                &nbsp; &nbsp; &nbsp;
                            </div>
                            <asp:Button ID="btn_imprimir" runat="server" class="btn btn-primary" Text="Imprimir Cheque" Visible="false" />
                            &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btnPoliza" runat="server" class="btn btn-primary" Text="Imprimir Poliza" Visible="false" />
                            &nbsp; &nbsp; &nbsp;
                                <%-- Deshacer --%>
                            <asp:Button ID="btn_deshacer" runat="server" class="btn btn-primary" Text="Deshacer" Visible="false" />
                            &nbsp; &nbsp; &nbsp;
                                <%-- Entregado --%>
                            <asp:Button ID="btn_Entregar" runat="server" class="btn btn-primary" Text="Entregar Cheque" Visible="false" />
                            &nbsp; &nbsp; &nbsp;
                                <%-- Pagar --%>
                            <asp:Button ID="btn_PagarCheques" runat="server" class="btn btn-primary" Text="Pagar Cheque" Visible="false" />
                            &nbsp; &nbsp; &nbsp;
                                <%-- Cancelar --%>
                            <asp:Button ID="btnCancelar" runat="server" class="btn btn-primary" Text="Cancelar Cheque" Visible="false" />

                        </div>

                    </div>


                    <div align="center">
                        <br />
                        <asp:Label ID="Label1" runat="server" CssClass="alerta"></asp:Label>
                    </div>


                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="DAG_Analisis" />

                </triggers>


                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text=""></asp:Label>
                </div>

                <div class="module_subsec low_m">
                    <div class="module_subsec_elements w_100">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea" Height="80px" MaxLength="2000"
                                TextMode="MultiLine" Visible="false"></asp:TextBox>
                            <asp:Label ID="lbl_notas" runat="server" class="text_input_nice_label" Visible="false">Notas:</asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_notas" runat="server" Enabled="True"
                                FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                TargetControlID="txt_notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>

                <div>

                    <div class="module_subsec low_m columned four columns top_m flex_end">

                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_cmbTipoNota" runat="server" CssClass="text_input_nice_label" Visible="false" Text="Tipo de nota:"></asp:Label>
                            <asp:DropDownList ID="cmbTipoNota" runat="server" class="btn btn-primary2 dropdown_label" Visible="false" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        &nbsp;&nbsp;&nbsp;
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lblChequeNotas" runat="server" CssClass="text_input_nice_label" Visible="true" Text="Cheque:"></asp:Label>
                            <asp:DropDownList ID="cmbChequeNota" runat="server" class="btn btn-primary2 dropdown_label" Visible="true" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_GuardarNotas" runat="server" class="btn btn-primary" Text="Guardar notas" Visible="false" />

                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnVerNotasCheque" runat="server" class="btn btn-primary" Text="Ver notas de cheque" Visible="false" />

                    </div>

                </div>

            </div>

            <br />
            <br />
            <br />
            <br />

            <div class="overflow_x shadow ">
                <asp:GridView ID="gv_Notas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%" AutoPostBack="true">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>

                        <asp:BoundField DataField="ID_NOTA" HeaderText="ID_NOTA" Visible="FALSE">
                            <ItemStyle Width="25%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ID_CHEQUE" HeaderText="ID_CHEQUE" Visible="FALSE">
                            <ItemStyle Width="25%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° CHEQUE" Visible="TRUE">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOTAS" HeaderText="NOTAS" Visible="TRUE">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ID_ESTATUSOPE" HeaderText="ID_ESTATUSOPE" Visible="FALSE">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CVE_ESTATUSOPE" HeaderText="CVE_ESTATUSOPE" Visible="FALSE">
                            <ItemStyle Width="12%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ID_USUARIO" HeaderText="ID_USUARIO" Visible="FALSE">
                            <ItemStyle Width="12%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ESTATUSDENOTA" HeaderText="ESTATUSDENOTA" Visible="TRUE">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CREADO_POR" HeaderText="CREADO POR" Visible="TRUE">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FECHA_CREADO" HeaderText="FECHA CREACIÓN" Visible="TRUE">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>


                    </Columns>
                </asp:GridView>
            </div>

            <div>


                <br />
                <br />
                <br />
                <br />

                <div class="overflow_x shadow ">
                    <asp:GridView ID="dag_ChequesGenerados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%" AutoPostBack="true">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>

                            <asp:BoundField DataField="ID_AGREMIADO" HeaderText="Agremiado" Visible="FALSE">
                                <ItemStyle Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AGREMIADO" HeaderText="AGREMIADO">
                                <ItemStyle Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RFC" HeaderText="RFC">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RFC_ANTERIOR" HeaderText="RFC_ANTERIOR" Visible="false">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SISTEMA" HeaderText="SISTEMA">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="REGION" HeaderText="REGION">
                                <ItemStyle Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DELEGACION" HeaderText="DELEGACIÓN">
                                <ItemStyle Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SALDO_AHORRO" HeaderText="SALDO AHORRO" ItemStyle-HorizontalAlign="center" DataFormatString="${0:###,###,###.00}">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N° CHEQUE" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS_CHEQUE" HeaderText="ESTATUS CHEQUE">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ID_ESTATUS_CHEQUE" HeaderText="ID_ESTATUS" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="NUMCLIENTE" HeaderText="NUMCLIENTE" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="TIPO_PLAZA" HeaderText="TIPO_PLAZA" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ID_ESTATUS_TRANSFERENCIA" HeaderText="ID_ESTATUS_TRANSFERENCIA" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ESTATUS_TRANSFERENCIA" HeaderText="ESTATUS_TRANSFERENCIA" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CLAVE_RASTREO" HeaderText="CLAVE_RASTREO" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NUM_PAQUETE" HeaderText="NUM_PAQUETE" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_CVE_RASTREO" HeaderText="FECHA_CVE_RASTREO" Visible="FALSE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GENERADO_POR" HeaderText="GENERADO POR" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_GENERADO" HeaderText="FECHA GENERADO" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ENTREGADO_POR" HeaderText="ENTREGADO POR" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_ENTREGADO" HeaderText="FECHA ENTREGADO" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAGADO_POR" HeaderText="PAGADO POR" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_PAGADO" HeaderText="FECHA PAGADO" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CANCELADO_POR" HeaderText="CANCELADO POR" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_CANCELADO" HeaderText="FECHA CANCELADO" Visible="TRUE">
                                <ItemStyle Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="IMPRIMIR" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chb_Imprimir" runat="server" AutoPostBack="FALSE" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="DESHACER" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chb_Deshacer" runat="server" AutoPostBack="FALSE" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="ENTREGAR" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chb_Entregar" runat="server" AutoPostBack="FALSE" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="PAGAR" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chb_Pagar" runat="server" AutoPostBack="FALSE" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="CANCELAR" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chb_Cancelar" runat="server" AutoPostBack="FALSE" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%----------------------------------------------------------------------------------%>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                            <asp:Label runat="server" ID="CLAVE_PRODUCTO" Visible="false" Text='<%#Eval("CLAVE_PRODUCTO") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="NUMCLIENTE" Visible="false" Text='<%#Eval("NUMCLIENTE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label DataField="NUM_CHEQUE" runat="server" ID="NUM_CHEQUE" Visible="FALSE" Text='<%#Eval("NUM_CHEQUE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="module_subsec low_m columned four columns top_m flex_end">
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnListdoDeCheques" runat="server" class="btn btn-primary" Text="Ver listado de cheques" Visible="true" />
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="CHEQUES" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="TRANSFERENCIAS" Visible="false" />
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="SIN ASIGNAR" Visible="false" />

                </div>

            </div>

        </div>


    </section>
</asp:Content>
