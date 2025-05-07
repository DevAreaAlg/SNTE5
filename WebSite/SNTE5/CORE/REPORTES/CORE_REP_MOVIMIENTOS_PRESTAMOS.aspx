<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CORE_REP_MOVIMIENTOS_PRESTAMOS.aspx.vb" Inherits="SNTE5.CORE_REP_MOVIMIENTOS_PRESTAMOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

                    <asp:LinkButton ID="btn_seleccionar"  runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />&nbsp;&nbsp;
                        <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_BusquedaPersona"
                            Style="height: 18px; width: 18px;"></asp:ImageButton>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_folio" runat="server" AutoPostBack="False" class="btn btn-primary2  module_subsec_elements"></asp:DropDownList>
                        <span class="text_input_nice_label title_tag">Folio:</span>
                    </div>
                </div>

            </div>

            <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </span>
                <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton ID="lnk_reporte" runat="server" Style="font-size: 18px;" ValidationGroup="val_Depe_NumCtrl">
                                    Descargar Reporte
                                    <i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:20px; margin-left:5px;"></i>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            

        </div>
    </section>

    <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>

</asp:Content>
