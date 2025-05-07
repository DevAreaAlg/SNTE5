<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_APARTADO2.aspx.vb" Inherits="SNTE5.CAP_EXP_APARTADO2" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <section class="panel">
        <header class="panel-heading">
                <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
                <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>

    <section class="panel" id="panel_requisitos">
        <header class="panel-heading">
            <span>Apartados</span>
        </header>
        <div class="panel-body">
                <div class="text_input_nice_div module_sec" style="margin-top: 20px; align-items: center;">
                    <span class="text_input_nice_label">Capture la información de los siguientes apartados:</span>
                </div>

                <div class="text_input_nice_div module_sec" style="margin-top: 20px; align-items: center">
                    <asp:Panel ID="pnl_links" runat="server" Width="780" Style="text-align: center"></asp:Panel>
                </div>
        </div>
    </section>

</asp:Content>
