<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CORE_REP_DENOMINACIONES.aspx.vb" Inherits="SNTE5.CORE_REP_DENOMINACIONES" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>MAS.Core Servicios en Línea</title>
        <link href="css/estilosmascore.css" type="text/css" rel="stylesheet" /> <script type="text/javascript">document.onkeydown =function(){if (122 == event.keyCode){event.keyCode = 0;return false;}}</script>     
               
        <script type="text/javascript">
            //If you want the FillForm function to fire when the user closes the window, you can do this
            //window.onunload = function() { doParentActivatePostBack(); }
            //if you want it to do the fillform on a button click
            //<input onclick ="doParentActivatePostBack()"/>
            //function doParentActivatePostBack(){if(window.opener.ActivatePostBack){window.opener.ActivatePostBack();}}
       </script>
               
    </head>

    <body onload="history.forward(1);">
        <form id="form1" runat="server">
            <table cellspacing="0" cellpadding="0" align="center" bgcolor="#ffffff" border="0" width: "300px">
              
                <tr>
                    <td height="150">
                    </td>
                    <td valign="top">
                        <table style="width: 300px">
                            <tr>
                                <td width="260">  
                   
                            <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />
                
                                </td>
                              
                            </tr>
                        </table>
                        <table style="width: 300px">
                            <tr >
                                <td align="center" style="border-style: solid; border-width: 2px; border-color: #054B66; width: 300px">
                                    <asp:Label ID="lbl_RepDenom" runat="server" class="subtitulos">Reportes de Efectivo</asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="lbl_TipoRep" runat="server" class="subtitulos"></asp:Label>
                                    <br />
                                    <br />
                                    <table >
                                        <tr align="center">
                                            <td>
                                                <asp:DataGrid ID="dag_Reportes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                              GridLines="None" TabIndex="17" Width="300px">
                                                    <AlternatingItemStyle CssClass="AlternativeStyle" />
                                                    <SelectedItemStyle CssClass="SelectedItemStyle" />
                                                    <HeaderStyle CssClass="HeaderStyle" />
                                                    <FooterStyle CssClass="FooterStyle" />
                                                    <ItemStyle CssClass="ItemStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="IDCAJA" HeaderText="" Visible="false">
                                                            <ItemStyle Width="50px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CAJA" HeaderText="Caja">
                                                            <ItemStyle Width="150px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SERIEFOLIO" HeaderText="Serie / Folio">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundColumn>
                                                        <asp:ButtonColumn CommandName="REPORTE" Text="Reporte">
                                                            <ItemStyle ForeColor="#054B66" Width ="50px" />
                                                        </asp:ButtonColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <br />
                                    <asp:LinkButton ID="lnk_Cerrar" runat="server" class="textogris" Text="Cerrar"></asp:LinkButton>
                                    <br />
                                   <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>  
            </table>
        </form>
    </body>
</html>

