<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_ESTADOS_CTA_REGION.aspx.vb" Inherits="SNTE5.CRED_EXP_ESTADOS_CTA_REGION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="panel">
        <header class="panel-heading">
            <span>Estados de Cuenta por Región/Delegación</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m three_columns">         

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">

                        <asp:DropDownList ID="ddl_periodo" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True" />
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Periodo:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddl_periodo" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_region" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_region" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="True" />
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Región:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_region" runat="server"
                                ControlToValidate="ddl_region" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_region" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_delegacion" CssClass="btn btn-primary2 dropdown_label" runat="server" />
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Delegación:</span>
                 <%--           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddl_delegacion" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_region" />--%>
                        </div>
                    </div>
                </div>                
            </div>

            <div class="module_subsec low_m columned three_columns top_m flex_end">
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_descargar_EdoCuentaPrestamo" runat="server" class="btn btn-primary" Text="Descargar Estados de Cuenta" ValidationGroup="val_region"></asp:Button>
                </div>
            </div>

             <div class="module_subsec flex_center">
        <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" Text="" Visible="True" />
    </div>
</asp:Content>
