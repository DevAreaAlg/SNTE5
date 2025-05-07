<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_ALE_OPERACIONES.aspx.vb" Inherits="SNTE5.PLD_ALE_OPERACIONES" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
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
                        <table style="width: 600px">
                            <tr >
                                <td align="center" style="border-style: solid; border-width: 2px; border-color: #054B66; width: 600px">
                                    <br />
                                    <asp:Label ID="lbl_OpePre" runat="server" class="subtitulos">Operación Inusual</asp:Label>
                                    <p align="left" style="width: 499px">
                                        <asp:Label ID="lbl_IDPersona" runat="server" class="texto"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_Persona" runat="server" class="texto"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_Folio" runat="server" class="texto"></asp:Label>
                                    </p>
                                    
                                    <table >
                                        <tr align="center">
                                            <td>                                                
                                                <asp:DataGrid ID="dag_Operacion" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="500px">
                                                    <HeaderStyle CssClass="HeaderStyle" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="OPERACION" HeaderText="Operacion">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NOTA" HeaderText="Nota">
                                                            <ItemStyle Width="300px" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="btn_Guardar" runat="server" ValidationGroup="val_OpPre"  class="btn btn-primary"  Text="Guardar"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>  
            </table>
        </form>
</body>
</html>

