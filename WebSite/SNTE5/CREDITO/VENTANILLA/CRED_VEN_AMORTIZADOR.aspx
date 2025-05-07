<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_AMORTIZADOR.aspx.vb" Inherits="SNTE5.CRED_VEN_AMORTIZADOR" MaintainScrollPositionOnPostback="true" %>

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
            <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                        <asp:TextBox ID="tbx_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox ID="txt_IdCliente" runat="server" CssClass="text_input_nice_input" MaxLength="10" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage="Falta Dato!"
                                ValidationGroup="val_cliente"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
                        </div>
                    </div>
                    <asp:LinkButton ID="btn_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />&nbsp;&nbsp;
                     <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                         Style="height: 18px; width: 18px;"></asp:ImageButton>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_folio">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:Label CssClass="textonegritas" ID="lbl_cliente" runat="server" />
                    </div>
                </div>
            </div>


            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_ife" runat="server" CssClass="alerta" />
                <asp:Label ID="lbl_info_ide" CssClass="alerta" Visible="True" runat="server" />
                <asp:Label ID="lbl_info_disp" runat="server" CssClass="alerta" Visible="True" />
            </div>

        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Amortizador</span>
        </header>
        <div class="panel-body">

            <table border="0" width="70%">
                <tr>
                    <td width="30%">
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Fecha de aplicación:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label6"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_fecha"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Monto prestado:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label7"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_monto"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Total con interés original:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label8"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_total"></asp:TextBox>
                                </div>

                                <div class="module_subsec columned no_m align_items_flex_center">
                                    <asp:Label runat="server" Text="Total recalculado: " CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label10"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_recalculado"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Descontado total: " CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label12"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_descontado"></asp:TextBox>
                                </div>


                            </div>

                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Monto del préstamo para liquidar:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_montocredito"></asp:Label>
                                    <asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="lbl_monto_liq_txt"></asp:TextBox>
                                    <asp:TextBox Visible="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_cap"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </td>

                </tr>
            </table>

            <%--  <table border="0" width="70%">
                <tr>
                    <td width="30%">
                        <div class="module_subsec columned two_columns low_m align_items_flex_start">
                            <div class="module_subsec_elements vertical flex_1">
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Saldo insoluto:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_saldo_anterior"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_saldo_anterior"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Descuento programado:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="Label2"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_desc_pend"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Sobrantes:" CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_desc_sob"></asp:Label>
                                    &nbsp;<asp:Label ID="Label4" runat="server" Text="-"></asp:Label><asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_desc_sob"></asp:TextBox>
                                </div>

                                <div class="module_subsec columned no_m align_items_flex_center">
                                    <asp:Label runat="server" Text="Adelanto pendiente: " CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_adelanto_pend"></asp:Label>
                                    &nbsp;<asp:Label ID="Label3" runat="server" Text="-"></asp:Label><asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_adelanto_pend"></asp:TextBox>
                                </div>
                                <div class="module_subsec columned no_m align_items_flex_center ">
                                    <asp:Label runat="server" Text="Interés por descuento faltante: " CssClass="module_subsec_elements module_subsec_bigmedium-elements" ID="lbl_inte_falta"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="+"></asp:Label><asp:TextBox Enabled="false" runat="server" Text="" CssClass="module_subsec_elements flex_1 text_input_nice_input" Font-Bold="true" ID="txt_inte_falta"></asp:TextBox>
                                </div>
                              

                            </div>

                            
                        </div>
                    </td>

                </tr>
            </table>--%>



            <div class="module_subsec low_m columned three_columns">

                <%--  <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_tipo_reduccion" CssClass="btn btn-primary2 dropdown_label" runat="server" Enabled="False"
                            ValidationGroup="val_tipo">
                            <asp:ListItem Value="MONTO">REDUCCION DE MONTO DE DESCUENTO</asp:ListItem>
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_tipo" runat="server" CssClass="text_input_nice_label" Text="*Tipo de recálculo deseado:"></asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancosori"
                                CssClass="textogris" ControlToValidate="cmb_tipo_reduccion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_datos" InitialValue="-1" />
                        </div>
                    </div>
                </div>--%>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_cajaori" CssClass="text_input_nice_input" runat="server" Enable="False" MaxLength="12"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_cajaori" runat="server" CssClass="text_input_nice_label" Text="*Total:"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cajaori" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_cajaori">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_cajaori" runat="server"
                                class="textogris" ControlToValidate="txt_cajaori" ErrorMessage=" Monto incorrecto!"
                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_fechaIniOpen" runat="server"
                                ControlToValidate="txt_cajaori" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_datos"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_refpago" CssClass="text_input_nice_input" runat="server" Enable="False" MaxLength="100"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="Label5" runat="server" CssClass="text_input_nice_label flex_end" Text="*Referencia de pago:"></asp:Label>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txt_refpago" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_datos"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">

                <div style="display: flex; align-items: center;">
                    <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_recibo" Visible="false">
                            Descargar recibo<i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div>

    </section>
    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_alerta" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

    <div class="module_subsec flex_center">

        <asp:Button ID="btn_pagar" runat="server" class="btn btn-primary" Text="Aplicar" ValidationGroup="val_datos"
            Enabled="False" OnClientClick="btn_pagar.disabled = true; btn_pagar.value = 'Procesando...';" />

    </div>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_ide" PopupDragHandleControlID="pnl_Titulo"
        TargetControlID="hdn_ide" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>


    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_AutorPLD" PopupDragHandleControlID="pnl_AutorPLD_Titulo"
        TargetControlID="hdn_AutorPLD" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>


    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_recalculo" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="True" Enabled="True" PopupControlID="pnl_recalculo" PopupDragHandleControlID="pnl_avisa_recalculo"
        TargetControlID="hdn_recalculo" DynamicServicePath="">
    </ajaxToolkit:ModalPopupExtender>

    <asp:HiddenField ID="HiddenPrinterName" runat="server" />
    <asp:HiddenField ID="HiddenRawData" runat="server" />
    <input type="hidden" name="hdn_ide" id="hdn_ide" runat="server" />
    <input type="hidden" name="hdn_AutorPLD" id="hdn_AutorPLD" runat="server" />
    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" runat="server" value="" />
    <input type="hidden" name="hdn_recalculo" id="hdn_recalculo" runat="server" />

</asp:Content>

