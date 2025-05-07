<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_ORDEN_DESC.aspx.vb" Inherits="SNTE5.CRED_VEN_ORDEN_DESC" MaintainScrollPositionOnPostback="true" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Orden de Descuento</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_quincenas" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Orden de Descuento:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_quincenas" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="ddl_quincenas" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_orden_desc" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_descargar" CssClass="btn btn-primary" runat="server" Text="Descargar" ValidationGroup="val_orden_desc" />
                </div>
                <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                    <div style="display: flex; align-items: center;">
                        <asp:LinkButton ID="lnk_oficio_descuentos" runat="server" Style="font-size: 18px;">
                                    Oficio
                                    <i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:20px; margin-left:5px;"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>

