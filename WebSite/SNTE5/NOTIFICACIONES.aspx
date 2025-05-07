<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="NOTIFICACIONES.aspx.vb" Inherits="SNTE5.NOTIFICACIONES" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
               <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
            <asp:Panel runat="server" ID="no_notif" CssClass="no_notif" Visible="false">
              <i class="icon_check_alt" aria-hidden="true" style="font-size:55px;"></i>
               <span  style="font-size:35px; margin-left:25px;font-style:italic;">No tienes notificaciones pendientes</span>
              
            </asp:Panel>
            <asp:Panel runat="server" ID="cont_notif"></asp:Panel>              </ContentTemplate>
          
            </asp:UpdatePanel>

    <asp:Button ID="btn_test" runat="server" Text="Prueba"  Visible =" false"/>
</asp:Content>
