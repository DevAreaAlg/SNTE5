<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_INDICES.aspx.vb" Inherits="SNTE5.CORE_OPE_INDICES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading panel_header_folder">
            <span>Carga de un valor</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <div class="module_subsec">
                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto"
                        Text="Capture el valor del índice de acuerdo a la fecha de sistema, recuerde que el valor se captura a 6 decimales."></asp:Label>
                </div>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_indice" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_indice" runat="server" CssClass="text_input_nice_label" Text="*Índice:"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_indice" CssClass="textogris"
                                    ControlToValidate="cmb_indice" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_indice" InitialValue="0" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_valor" runat="server" MaxLength="9" CssClass="text_input_nice_input" ValidationGroup="val_indice"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_valor" runat="server" CssClass="text_input_nice_label" Text="*Valor:"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_valor" runat="server" ControlToValidate="txt_valor"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_indice"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_valor" runat="server"
                                    Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_valor">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_valor" runat="server"
                                    class="textogris" ControlToValidate="txt_valor" ErrorMessage="Valor Incorrecto"
                                    ValidationExpression="^[0-9]{1,2}(\.[0-9]{6})?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_repetir" runat="server" MaxLength="9" ValidationGroup="val_indice"
                                CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_repetir" runat="server" CssClass="text_input_nice_label" Text="*Repita valor:"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_repetir" runat="server" ControlToValidate="txt_repetir"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_indice"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_repetir" runat="server"
                                    Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_repetir">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_repetir" runat="server"
                                    class="textogris" ControlToValidate="txt_repetir" ErrorMessage="Valor Incorrecto"
                                    ValidationExpression="^[0-9]{1,2}(\.[0-9]{6})?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ToolTip="Guarda la información capturada" ValidationGroup="val_indice" CssClass="btn btn-primary" />
                </div>



                <h4 class="module_subsec">Valor actual</h4>

                <div class="overflow_x shadow module_subsec">
                    <asp:DataGrid ID="dag_indice" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="INDICE" HeaderText="Índice">
                                <ItemStyle Width="200px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALOR" HeaderText="Valor">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>


                <h4 class="module_subsec low_m">Historial</h4>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements text_input_nice_div">

                        <asp:TextBox runat="server" MaxLength="10" ID="txt_FechaFiltro" class="text_input_nice_input"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_fecha" runat="server" CssClass="text_input_nice_label"> Fecha: </asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_FechaFiltro"
                                CssClass="textogris" ControlToValidate="txt_FechaFiltro" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_FechaFiltro" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_FechaFiltro" runat="server"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_FechaFiltro">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_FechaFiltro" Display="dynamic" runat="server"
                                ControlExtender="MaskedEditExtender_FechaFiltro" ControlToValidate="txt_FechaFiltro"
                                CssClass="textogris" ErrorMessage="MaskedEditValidator_FechaFiltro" InvalidValueMessage="Fecha inválida"
                                ValidationGroup="val_FechaFiltro"></ajaxToolkit:MaskedEditValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txt_FechaFiltro">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </div>


                    <div class="module_subsec_elements">
                        <asp:LinkButton ID="lnk_FechaFiltro" runat="server" class="textogris" Text="Aplicar filtro"
                            ValidationGroup="val_FechaFiltro"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnk_CancelaFechaFiltro" runat="server" class="textogris" Text="Eliminar filtro"></asp:LinkButton>
                    </div>

                </div>
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: flex-end;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="btn_EXCEL">
                                    Descargar histórico de índices
                                    <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>

                </div>
                <div class="overflow_x shadow module_subsec">
                    <asp:DataGrid ID="dag_historial" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <PagerStyle Mode="NumericPages" CssClass="PagerStyle" ForeColor="White" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundColumn DataField="INDICE" HeaderText="índice">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALOR" HeaderText="Valor">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHASIS" HeaderText="Fecha">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading panel_header_folder">
            <span>Índice Nacional de Precios al Consumidor (INPC)</span>
            <span class="panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_anios" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="false" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Año:" />
                                <asp:RequiredFieldValidator ID="rfv_anio" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="ddl_anios" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_ipc" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_ipc" CssClass="text_input_nice_input" MaxLength="9" runat="server" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fte_ipc" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txt_ipc" ValidChars="." />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Valor:" />
                                <asp:RequiredFieldValidator ID="rfv_ipc" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="txt_ipc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_ipc" />
                                <asp:RegularExpressionValidator ID="rev_ipc" runat="server"
                                    ControlToValidate="txt_ipc" CssClass="textogris"
                                    ErrorMessage=" Valor Incorrecto!" lass="textorojo"
                                    ValidationExpression="^[0-9]{1,2}(\.[0-9]{1,6})?$" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_confirmar" CssClass="text_input_nice_input" MaxLength="9" runat="server" Enabled="true" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fte_confirmar" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txt_confirmar" ValidChars="." />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Confirmar el valor:" />
                                <asp:RequiredFieldValidator ID="rfv_confirmar" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="txt_confirmar" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_ipc" />
                                <asp:RegularExpressionValidator ID="rev_confirmar"
                                    runat="server" ControlToValidate="txt_confirmar" CssClass="textogris"
                                    ErrorMessage=" Factor incorrecto" lass="textorojo"
                                    ValidationExpression="^[0-9]{1,2}(\.[0-9]{1,6})?$" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar_ipc" CssClass="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_ipc" />
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
                </div>
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_bitacora_ipc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ANIO" HeaderText="Año"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALOR" HeaderText="Valor de INPC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
                <asp:Panel ID="pnl_modal_confirmar" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                            </header>
                            <div class="panel-body align_items_flex_center">
                                <asp:Label runat="server" CssClass="resalte_azul module_subsec" Text="¿Está seguro de que desea guardar el valor de INPC?" />
                                <asp:Button ID="btn_confirmar" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                                <asp:Button ID="btn_canelar" runat="server" CssClass="btn btn-primary" Text="Cancelar" />
                            </div>
                        </section>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="modal_confirmar" runat="server"
                    Enabled="True" PopupControlID="pnl_modal_confirmar"
                    PopupDragHandleControlID="pnl_modal_confirmar" TargetControlID="hdn_alertas">
                </ajaxToolkit:ModalPopupExtender>
                <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading panel_header_folder">
            <span>Carga de archivo</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">

                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_layout">
                                    Formato layout índices
                                    <i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                    <asp:Label ID="Label2" runat="server" Text="Seleccione el archivo correspondiente para cargar los valores, sólo se permite el siguiente tipo de archivo (*.csv)" CssClass="texto"></asp:Label>
                </div>

                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:FileUpload ID="AsyncFileUpload1" runat="server" />
                        </div>
                    </div>

                    <div class="module_subsec_elements">
                        <asp:Button ID="btn_cargar" runat="server" class="btn btn-primary" Text="Subir" ToolTip="Carga el archivo seleccionado y lo guarda en la Base de Datos" />
                    </div>
                </div>

                <div align="center">
                    <asp:Label ID="lbl_status_indices" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec">
                    <asp:Label ID="lbl_dato" runat="server" CssClass="subtitulos" Text="Últimas fechas existentes:"></asp:Label>
                </div>

                <!----<div class="module_subsec columned low_m align_items_flex_center">
                        <asp:Label ID="lbl_udi" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="UDI:" Height="16px"></asp:Label>
                        <asp:Label ID="lbl_fecha_udi" runat="server" CssClass="texto"></asp:Label>
                    </div>----->

                <div class="module_subsec columned low_m align_items_flex_center">
                    <asp:Label ID="lbl_tiee" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="TIIE 28:" Height="16px"></asp:Label>
                    <asp:Label ID="lbl_fecha_tiee" runat="server" CssClass="texto"></asp:Label>
                </div>

                <div class="module_subsec columned low_m align_items_flex_center">
                    <asp:Label ID="lbl_cete" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="UMA:" Height="16px"></asp:Label>
                    <asp:Label ID="lbl_fecha_cetes" runat="server" CssClass="texto"></asp:Label>
                </div>

            </div>
        </div>
    </section>

</asp:Content>
