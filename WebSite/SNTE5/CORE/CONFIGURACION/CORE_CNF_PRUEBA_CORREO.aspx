<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CORE_CNF_PRUEBA_CORREO.aspx.vb" Inherits="SNTE5.CORE_CNF_PRUEBA_CORREO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <asp:Label runat="server" Text="Prueba de correo" />
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:Button runat="server" ID="btn_enviar_correo" CssClass="btn btn-primary" Text="Enviar Correo" />
                        </div>
                    </div>
                     <div class="module_subsec_elements text_input_nice_div">
                            <asp:Button runat="server" ID="btn_del" CssClass="btn btn-primary" Text="Enviar Correo delegaciones" />
                        </div>
                    <asp:Label runat="server" ID="lbl_estatus_correo" CssClass="module_subsec flex_center alerta" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>
