<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_REP_CATEGORIAS.aspx.vb" Inherits="SNTE5.CORE_REP_CATEGORIAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span>CATEGORÍAS PARA REPORTES</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                    <asp:Panel runat="server" ID="ayuda"></asp:Panel>
            </div>
        </div>
    </section>  

</asp:Content>

