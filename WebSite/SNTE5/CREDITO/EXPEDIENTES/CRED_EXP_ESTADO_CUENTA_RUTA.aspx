<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_ESTADO_CUENTA_RUTA.aspx.vb" Inherits="SNTE5.CRED_EXP_ESTADO_CUENTA_RUTA" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script type="text/javascript">
        function BtnClick() {
            document.getElementById("Loading").innerHTML = document.getElementById("Loading").innerHTML;
            var lLoadingMessage = document.getElementById('<%=lblLoadingMessage.ClientID %>');
            var dvLoading = document.getElementById('<%=dvLoading.ClientID %>');
            var dvMain = document.getElementById('<%=dvMain.ClientID %>');

            if (dvMain != null) dvMain.style.display = 'none';
            if (dvLoading != null) dvLoading.style.display = '';

            return true;
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }
    </script>

    <%--<div id="dvLoading" runat="server" class="loadingMessageFrame" style="display: none;">
        <div class="module_subsec flex_center">
            <asp:Label ID="lblLoadingMessage" runat="server" Text="Procesando..." CssClass="Loading_Message"> </asp:Label>
        </div>
        <div class="module_subsec flex_center" id="Loading">
            <asp:Image ID="lblLoadingMessageGif" runat="server" ImageUrl="~/img/Loading.gif" />
        </div>
    </div>--%>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_info" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

    <%--    <div id="dvMain" runat="server">--%>

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

            <div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements text_input_nice_div">

                    <asp:DropDownList ID="cmb_sistema" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true">
                    </asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <asp:Label ID="lbl_sistema" runat="server" CssClass="text_input_nice_label" Text="*Sistema:"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </span>
                <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
            </div>
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_estatusnombre" runat="server" CssClass="alerta" Text="" Visible="True" />
            </div>
            <br />
            <br />
            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <%--                    <asp:Button ID="btn_descargar_EdoCuentaAportacion" runat="server" class="btn btn-primary" Text="Descargar Estado de Cuenta - Aportaciones" ValidationGroup="val_Depe_NumCtrl"></asp:Button>--%>
                    <asp:Button ID="btn_descargar_EdoCuentaPrestamo" runat="server" class="btn btn-primary" Text="Descargar Estado de Cuenta - Préstamos" ValidationGroup="val_Depe_NumCtrl"></asp:Button>

                    &nbsp;&nbsp;&nbsp;
                    <%--                    <asp:Button ID="btn_descargar_EdoCuentaPrestamo" runat="server" class="btn btn-primary" Text="Descargar Estado de Cuenta - Préstamos" ValidationGroup="val_Depe_NumCtrl"></asp:Button>--%>
                    <asp:Button ID="btn_descargar_EdoCuentaAportacion" runat="server" class="btn btn-primary" Text="Descargar Estado de Cuenta - Aportaciones" ValidationGroup="val_Depe_NumCtrl"></asp:Button>

                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_ife" runat="server" CssClass="alerta" />
                <asp:Label ID="lbl_info_ide" CssClass="alerta" Visible="True" runat="server" />
                <asp:Label ID="lbl_info_disp" runat="server" CssClass="alerta" Visible="True" />
            </div>

        </div>
    </section>
    <%--        </div>--%>

    <section class="panel">
        <header class="panel-heading">
            <span>Generación de Estados de Cuenta</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="cmb_periodo" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Periodo:</span>
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="cmb_periodo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_periodo">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

            </div>
            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_generar_estado_prest" runat="server" class="btn btn-primary" Text="Generar Estados de Cuenta - Préstamos"></asp:Button>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_generar_estado_aport" runat="server" class="btn btn-primary" Text="Generar Estados de Cuenta - Aportaciones"></asp:Button>
                </div>
            </div>
            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">

                    <asp:Button ID="btn_generar_estados_pres_pensiones" runat="server" class="btn btn-primary" Text="Generar Estados de Cuenta Prestamos-Pensiones"></asp:Button>
                    &nbsp;&nbsp;&nbsp;                    
                    <asp:Button ID="btn_generar_estados_aport_pensiones" runat="server" class="btn btn-primary" Text="Generar Estados de Cuenta Aportaciones-Pensiones"></asp:Button>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_ComprimirEdosCuentaNoEnviados" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Comprimir Estados de Cuenta No Enviados" />
                </div>


            </div>

            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">

                    <asp:Button ID="btn_genera_estados_cuenta_unico" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Estados de Cuenta Unicos" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_genera_estados_cuenta_unico_prest" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Estados de Cuenta Unicos Préstamos" />
                </div>

            </div>

            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">

                    <asp:Button ID="btn_reportes_excel" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Reportes Excel"/>
                    <asp:Button ID="btn_rep_cheque" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Reportes Excel Cheque"/>
                </div>

            </div>

        </div>
    </section>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>
</asp:Content>
