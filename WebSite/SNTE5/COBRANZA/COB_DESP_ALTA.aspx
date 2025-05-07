<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DESP_ALTA.aspx.vb" Inherits="SNTE5.COB_DESP_ALTA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>


    <asp:UpdatePanel ID="upnl_adicionales" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

              <section class="panel" runat="server" id="div_selCliente">
            <header runat="server" id="head_div_selCliente" class="panel-heading panel_header_folder">
                <span>Selección de abogado/despacho</span>
                <span class="panel_folder_toogle down" runat="server" id="toogle_div_selCliente" >&rsaquo;</span>
            </header>
       
                <div class="panel-body">
                   <div class="panel-body_content init_show" runat="server" id="content_div_selCliente" >
                         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="module_subsec low_m columned three_columns">
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_despacho" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Seleccione que desea capturar o buscar:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_sector" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_despacho" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_PersonaAdi" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                    
                                </div>
                            </ContentTemplate>
                        <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cmb_despacho" />
                            </Triggers>
                        </asp:UpdatePanel>
                                                                      
                    </div>
                </div>
            </section>

        </ContentTemplate>

    </asp:UpdatePanel>
    </asp:Content>

