<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_DESBLOQUEO_EXP.aspx.vb" Inherits="SNTE5.CRED_CNF_DESBLOQUEO_EXP" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            Desbloqueo de expedientes y Actualización de usuario validador
        </header>

        <div class="panel-body">

             <h5 style="font-weight: normal" class="resalte_azul module_subsec">Desbloqueo de expedientes</h5>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:DropDownList ID="cmb_DesExpedienteDig" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Seleccione expediente a desbloquear:</span>
                    </div>
                </div>
            </div>

            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Label ID="lbl_AlertaDesExpedienteDig" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="XX-Small" ForeColor="Red"></asp:Label>
            </div>
            <div class="module_subsec flex_center low_m align_items_flex_center">
                 <asp:Button ID="btn_DesExpedienteDig" class="btn btn-primary" runat="server" ValidationGroup="val_exp" Text="Desbloquear" ToolTip="Presione si realmente desea desbloquear el expediente"/>
            </div>

            <h5 style="font-weight: normal" class="resalte_azul module_subsec">Actualización de usuario validador</h5>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:DropDownList ID="cmb_folio" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Seleccione expediente:</span>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="text_input_nice_div module_subsec_elements">
                    <asp:DropDownList ID="cmb_usuario" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Seleccione nuevo usuario:</span>
                    </div>
                </div>
            </div>

            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Label ID="lbl_status" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                        ForeColor="Red"></asp:Label> 
            </div>
            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Button ID="btn_modificar" class="btn btn-primary" runat="server" ValidationGroup="val_exp" Text="Desbloquear" ToolTip="Presione si realmente desea modificar el usuario validador"/>
            </div>
        </div>
    </section>
</asp:Content>
