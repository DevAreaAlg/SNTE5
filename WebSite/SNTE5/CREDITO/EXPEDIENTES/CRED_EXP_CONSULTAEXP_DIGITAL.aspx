<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_CONSULTAEXP_DIGITAL.aspx.vb" Inherits="SNTE5.CRED_EXP_CONSULTAEXP_DIGITAL" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        // Javascript para utilizar la Busqueda de un Cliente o Persona
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

        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=800,height=450,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }

        function det_com() {
            window.open("CRED_EXP_DICTAMEN.aspx", "DD", "width=800px,height=450px,Scrollbars=YES");
        }

        function det_restructura() {
            window.open("CRED_EXP_REESTRUCTURA.aspx", "DR", "width=650px,height=450px,Scrollbars=YES");
        }
    </script>

    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Selección de agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">

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
                        <asp:Button ID="lnk_Continuar" runat="server" class="btn btn-primary module_subsec_elements no_tbm" Text="Continuar"></asp:Button>
                        <asp:Button ID="lnk_BusquedaPersona" runat="server" class="btn btn-primary module_subsec_elements no_tbm" Text="Buscar agremiado"></asp:Button>
                    </div>
                </div>

                <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                    <label id="lbl_NombrePersonaBusquedTexto" runat="server" class="module_subsec_medium-elements module_subsec_elements no_m">Nombre de agremiado: </label>
                    <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" CssClass="texto"></asp:Label>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statusc" runat="server" class="alerta"></asp:Label>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="pnl_expedientes">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_expedientes">
            <span class="panel_folder_toogle_header">Expedientes</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_expedientes">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_expedientes">
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_Expendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None">
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="folio" HeaderText="Folio" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cveexp" HeaderText="Clave">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="producto" HeaderText="Producto">
                                <ItemStyle Width="12%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sucursal" HeaderText="Oficina">
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha alta">
                                <ItemStyle Width="10%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="CONSULTAR" Text="Ver Documentación">
                                <ItemStyle Width="5%" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </section>


    <asp:Panel ID="pnl_editar" runat="server" Visible="false">
        <section class="panel">
            <header class="panel-heading">
                <span>Documentos</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content" runat="server" id="content_panel_generales">


                    <div class="panel-body">
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_DocDigit" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">

                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="clave" HeaderText="Núm. doc." Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="id_tipo_doc" HeaderText="ID Documento" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="nombre_doc" HeaderText="Nombre documento" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="tipodoc" HeaderText="Tipo de documento">
                                        <ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="documento" HeaderText="Documento"></asp:BoundColumn>

                                    <asp:BoundColumn DataField="fechadigit" HeaderText="Fecha digitalizado">
                                        <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                                    </asp:BoundColumn>

                                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:ButtonColumn>

                                    <asp:ButtonColumn CommandName="EDITAR" Text="Editar">
                                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                    </asp:ButtonColumn>

                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <asp:Panel ID="pnl_disposicion" runat="server" Visible="false">
                            <br />
                            <asp:Label ID="lbl_dictamenes_title0" runat="server" class="texto" Text="Disposiciones"></asp:Label>
                            <br />
                            <br />
                            <asp:DataGrid ID="dag_disposiciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />

                                <Columns>
                                    <asp:BoundColumn DataField="clave" HeaderText="Num. Documento">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tipodoc" HeaderText="Tipo de Documento">
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="documento" HeaderText="Documento">
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="iddisp" HeaderText="Id Disposición">
                                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="fechadigit" HeaderText="Fecha Digitalizado">
                                        <ItemStyle HorizontalAlign="Center" Width="240px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                                        <ItemStyle ForeColor="#054B66" HorizontalAlign="Center" Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </asp:Panel>

                        <div class="module_subsec flex_end">
                            <asp:LinkButton ID="lnk_digitalizar" runat="server" CssClass="textogris"
                                Text="Digitalizar Otro"></asp:LinkButton>
                        </div>

                    </div>
                </div>
        </section>
    </asp:Panel>
</asp:Content>



