<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PERSONA_API.aspx.vb" Inherits="SNTE5.CORE_PER_PERSONA_API" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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
        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>
    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Selección del agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="tbx_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13" />
                        <asp:TextBox ID="txt_IdCliente" runat="server" CssClass="text_input_nice_input" MaxLength="9" Visible="false" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ingrese el RFC:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="tbx_rfc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Depe_NumCtrl">
                            </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="tbx_rfc"
                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" Enabled="True" />
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
                <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
                <asp:Label ID="lbl_maxreest" runat="server" CssClass="module_subsec flex_center alerta"></asp:Label>
            </div>
        </div>
    </section>

    <!-------------------- PANEL DE DATOS DE CONTACTO -------------------->
    <asp:UpdatePanel ID="upnl_contacto" runat="server" UpdateMode="Conditional" Visible="True">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_contacto">
                <header class="panel-heading panel_header_folder" runat="server" id="head_panel_contacto">
                    <span class="panel_folder_toogle_header">Datos de contacto</span>
                    <span class=" panel_folder_toogle down" runat="server" id="toggle_panel_contacto">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show" runat="server" id="content_panel_contacto">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Datos de contacto del agremiado:</h5>
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox runat="server" ID="txt_correo" MaxLength="100"
                                                CssClass="module_subsec_elements module_subsec_bigger-elements text_input_nice_input" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Correo electrónico:" />
                                                <asp:RegularExpressionValidator runat="server" CssClass="alertaValidator bold"
                                                    ControlToValidate="txt_correo" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox runat="server" ID="txt_telcasa" MaxLength="10"
                                                CssClass="module_subsec_elements module_subsec_big-elements text_input_nice_input" />
                                            <div class="text_input_nice_labels">
                                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Teléfono personal:" />
                                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                                    TargetControlID="txt_telcasa">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="module_subsec flex_center">
                                    <asp:Label runat="server" ID="lbl_estatus_contacto" CssClass="alerta" />
                                </div>
                                <div class="module_subsec flex_end">
                                    <asp:Button runat="server" ID="btn_guardar_contactos" CssClass="btn btn-primary" Text="Guardar Datos de Agremiado"
                                        AutoPostBack="False" />          
                                    
                                    &nbsp;
                                    &nbsp;

                                    <asp:Button runat="server" ID="btn_activar_app" CssClass="btn btn-primary" Text="Activar APP Movil"
                                        AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_contactos" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="up_invisible" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" name="hdn_origen_busquedas" id="hdn_origen_busquedas" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input type="hidden" name="hdn_origen_busquedas" id="Hidden1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

