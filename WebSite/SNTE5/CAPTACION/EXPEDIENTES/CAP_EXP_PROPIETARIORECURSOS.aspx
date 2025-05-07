<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_PROPIETARIORECURSOS.aspx.vb" Inherits="SNTE5.CAP_EXP_PROPIETARIORECURSOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:687px;dialogWidth:670px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
        function busquedaCP() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
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
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_avales">
        <header class="panel_header_folder panel-heading">
            <span>Propietario real</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="dag_propietario" AutoGenerateColumns="False" CssClass="table table-striped"
                                runat="server" GridLines="None" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="idpropietario" HeaderText="Número de afiliado" Visible ="False">
                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="porcentaje" HeaderText="Porcentaje">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                        
                        <div class="module_subsec">
                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic "></asp:Label>&nbsp;&nbsp;
                                <asp:LinkButton ID="lnk_busqueda" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>
                        </div>
                        <div class="module_subsec columned three_columns ">
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:TextBox ID="txt_numCliente" runat="server" class="text_input_nice_input" MaxLength="8"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Número de afiliado:</span>

                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_numCliente"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_numCliente">
                                            </ajaxToolkit:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_txt_numCliente"
                                                runat="server" ControlToValidate="txt_numCliente" CssClass="textogris"
                                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="propietarioexistente"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical flex_end">
                                    <div class="text_input_nice_div ">
                                        <asp:TextBox ID="txt_pctje_exi" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Porcentaje: %</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1_pctje_exi"
                                                runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_pctje_exi">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_pctje_exi" runat="server"
                                                ControlToValidate="txt_pctje_exi" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" ValidationGroup="propietarioexistente"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" module_subsec_elements">
                                <div class="module_subsec_elements_content vertical">
                                    <div class="text_input_nice_div ">
                                        <asp:DropDownList ID="cmb_tipo_relacion" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Tipo de relación:</span>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipo_relacion" runat="server" ControlToValidate="cmb_tipo_relacion"
                                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="propietarioexistente"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server"  CssClass="alerta"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                              <asp:Button ID="btn_agregar" runat="server" CssClass="btn btn-primary" Text="Guardar" ValidationGroup="propietarioexistente" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>

