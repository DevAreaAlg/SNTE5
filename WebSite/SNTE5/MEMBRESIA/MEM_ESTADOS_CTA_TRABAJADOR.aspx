<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="MEM_ESTADOS_CTA_TRABAJADOR.aspx.vb" Inherits="SNTE5.MEM_ESTADOS_CTA_TRABAJADOR" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function busquedapersonafisica() {
            if (!window.showModalDialog) {
                var wbusf = window.open('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
        }
        else {
            var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
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

    </script>

    <section class="panel" runat="server" id="div_selCliente">
        <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
            <span class="panel_folder_toogle_header">Busqueda de agremiado</span>
            <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
               <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="font-size: 18px;" ID="lnk_vermanual" Enabled="false">
                            Manual Estados Cuenta<i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div> 
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
                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar Agremiado" />
                         
                      
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

    <section class="panel" runat="server" visible="false" id="pnl_expedientes">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_expedientes">
            <span class="panel_folder_toogle_header">Datos Trabajador</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_expedientes">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_expedientes">


                <div class="module_subsec align_items_flex_center columned low_m">
                    <span class="module_subsec_elements module_subsec_medium-elements">Agremiado:</span>
                    <asp:TextBox ID="lbl_nompros" runat="server" class="text_input_nice_input module_subsec_elements module_subsec_elements module_subsec_bigger-elements" Enabled="false"></asp:TextBox>
                   &nbsp;&nbsp;&nbsp; <asp:Label ID="lbl_sistema" runat="server" CssClass="text_input_nice_label" Text="Sistema:"></asp:Label>
                    <asp:DropDownList ID="cmb_sistema" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack ="true">
                    </asp:DropDownList>
                </div>
                </div>

                <%--DATOS DE REGION,DELEGACION Y CCT--%>
                <div class="module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_region" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE" >
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Región:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="alertaValidator"
                                    ControlToValidate="ddl_region" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_delegacion" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Delegación:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                    ControlToValidate="ddl_delegacion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_cct" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Centro de trabajo:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="alertaValidator"
                                    ControlToValidate="ddl_cct" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" InitialValue="-1" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Label ID="lbliduser" runat="server" Text="Labe1" Visible="false"></asp:Label>
                <asp:Label ID="lblidsesion" runat="server" Text="Labe2" Visible="false"></asp:Label>
                <asp:Label ID="lblidsucu" runat="server" Text="Labe4" Visible="false"></asp:Label>
                <asp:Label ID="lblidperson" runat="server" Text="Labe3" Visible="false"></asp:Label>
                <asp:Label ID="lbltipoproducto" runat="server" Text="Labe3" Visible="false"></asp:Label>
                <asp:Label ID="lblidproducto" runat="server" Text="Labe3" Visible="false"></asp:Label>


                <div class="module_subsec columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="txt_correo" CssClass="text_input_nice_input" MaxLength="150"/>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Correo electrónico:" />
                                <asp:RegularExpressionValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_correo" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                    ValidationGroup="val_Persona"
                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$" />
                             
                            </div>
                        </div>

                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="txt_num" CssClass="text_input_nice_input" MaxLength="10" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Teléfono personal:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_num" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" Enabled="false" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                    TargetControlID="txt_num" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">

                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="Combo_Estatus" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" Enabled="TRUE">
                            </asp:DropDownList>
                             <asp:Label runat="server" CssClass="text_input_nice_label" Text="Estatus Trabajador:" />

                            </div> 
                        </div>

                </div>


            
            <div class="module_subsec flex_end">
                <asp:Label ID="lbl_llen_solic" runat="server" CssClass="alerta"></asp:Label>
                <asp:Button ID="bnt_enviar_correo" runat="server" class="btn btn-primary" ValidationGroup="val_Persona" Text="Enviar Estado de Cuenta por Correo" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_genera_estado_p" runat="server" class="btn btn-primary" ValidationGroup="val_Persona" Text="Descargar Estado Cuenta Prestamo" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_genera_estado_a" runat="server" class="btn btn-primary" ValidationGroup="val_Persona" Text="Descargar Estado Cuenta Aportaciones" />&nbsp;&nbsp;&nbsp;
               
            </div>
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta"></asp:Label>
            </div>
        </div>

        </div>

        
        <ajaxToolkit:ModalPopupExtender ID="modal_confirmar" runat="server"
            Enabled="True" PopupControlID="pnl_modal_confirmar"
            PopupDragHandleControlID="pnl_modal_confirmar" TargetControlID="hdn_alertas">
        </ajaxToolkit:ModalPopupExtender>
        <input type="hidden" name="hdn_alertas" id="Hidden1" value="" runat="server" />
    </section>



    <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
    <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />


</asp:Content>
