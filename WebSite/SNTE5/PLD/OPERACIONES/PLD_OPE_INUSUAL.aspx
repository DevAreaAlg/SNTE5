<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_INUSUAL.aspx.vb" Inherits="SNTE5.PLD_OPE_INUSUAL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }

        function ClickBotonBusqueda(ControlTextbox, ControlButton) {
            var CTextbox = document.getElementById(ControlTextbox);
            var CButton = document.getElementById(ControlButton);
            if (CTextbox != null && CButton != null) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    if (CTextbox.value != "") {
                        CButton.click();
                        CButton.disabled = true;
                    }
                    else {
                        //alert('Ingrese Datos')
                        CTextbox.focus()
                        return false
                    }
                    //CTextbox.focus();
                    return true
                }
            }
        }
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>Datos Operación</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec columned three_columns align_items_flex_start no_m">
                <div class="module_subsec_elements">
                    <div class="module_subsec_elements_content vertical">
                        <div class="text_input_nice_div ">
                            <asp:DropDownList ID="cmb_ProvProp" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False">
                               <%-- <asp:ListItem Value="-1">ELIJA</asp:ListItem>--%>
                                <asp:ListItem Value="PROV">PROVEEDOR DE RECURSOS</asp:ListItem>
                                <asp:ListItem Value="PROP">PROPIETARIO REAL</asp:ListItem>
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Razón de alerta:</span>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator_ProvProp" runat="server" ControlToValidate="cmb_ProvProp"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_idPersona" InitialValue="-1" ></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="module_subsec low_m columned three_columns flex_start">
                <div class="module_subsec_elements flex_1">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_Notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="435" TextMode="MultiLine"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Notas:</span>
                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator_Notas" runat="server"
                                ControlToValidate="txt_Notas" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_idPersona"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">

                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="Ingrese el número de afiliado"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server"
                                TargetControlID="txt_idCliente" FilterType="Numbers" Enabled="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_IdCliente" runat="server"
                                ControlToValidate="txt_IdCliente" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_idPersona"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" >
                        <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" ValidationGroup="val_idPersona" />
                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar afiliado" />
                        <asp:Button ID="btn_cancelar" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Cancelar" />
                    </div>
                </div>

                <div class="module_subsec flex_start" runat="server">
                    <asp:Label ID="lbl_nompros" runat="server" class="module_subsec"></asp:Label>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>
                </div>
            </div>

            <asp:DataGrid ID="dag_EXPEDIENTES" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None">
                <HeaderStyle CssClass="table_header"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="FOLIO" HeaderText="Contrato">
                        <ItemStyle Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOPROD" HeaderText="Tipo de producto">
                        <ItemStyle Width="15%"/>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="PROVPROP" HeaderText="Proveedor de recursos / Propietario real" Text="Ver"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="REPORTAR" Text="Reportar">
                        <ItemStyle Width="10%" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>

        </div>
    </section>
</asp:Content>

