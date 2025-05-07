<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_DICTAMEN.aspx.vb" Inherits="SNTE5.CRED_EXP_DICTAMEN" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
    <table>
            <tr>
                <td valign="top">
                    <p class="subtitulos" align="center">Comité de préstamo </p>
                    <label id="lbl_com_info_num_ses" class="texto"> Número de sesión:</label>
                    <asp:Label ID="lbl_com_num_ses" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_com_info_monto" class="texto">Monto:</label>
                    <asp:Label ID="lbl_com_monto" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_com_info_res" class="texto">Resultado del comité:</label>
                    <asp:Label ID="lbl_com_res" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_com_info_fecha" class="texto">Fecha de sesión:</label>
                    <asp:Label ID="lbl_com_fecha" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_com_info_obs" class="texto">Observaciones:</label>
                    <asp:Label ID="lbl_com_obs" runat="server" class="texto" TextMode="MultiLine" Width="300px"></asp:Label>
                    <br />
                    <label id="lbl_com_info_rec" class="texto">Recomendaciones:</label>
                    <asp:Label ID="lbl_com_rec" runat="server" class="texto" TextMode="MultiLine" Width="300px"></asp:Label>
                </td>
                <td style="border-style: solid; border-width: 2px; border-color: #FFFFFF #FFFFFF #FFFFFF #054B66;" valign="top">
                    <p class="subtitulos" align="center">Dirección de préstamo</p>
                    <label id="lbl_dir_info_num_ses" class="texto">Número de sesión:</label>
                    <asp:Label ID="lbl_dir_num_ses" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_dir_info_monto" class="texto">Monto:</label>
                    <asp:Label ID="lbl_dir_monto" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_dir_info_res" class="texto">Resultado del comité:</label>
                    <asp:Label ID="lbl_dir_res" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_dir_info_fecha" class="texto">Fecha de sesión:</label>
                    <asp:Label ID="lbl_dir_fecha" runat="server" class="texto"></asp:Label>
                    <br />
                    <label id="lbl_dir_info_obs" class="texto">Observaciones:</label>
                    <asp:Label ID="lbl_dir_obs" runat="server" class="texto" TextMode="MultiLine" Width="300px"></asp:Label>
                    <br />
                    <label id="lbl_dir_info_rec" class="texto">Recomendaciones:</label>
                    <asp:Label ID="lbl_dir_rec" runat="server" class="texto" TextMode="MultiLine" Width="300px"></asp:Label>
                    <%--<asp:TextBox ID="txt_notas" runat="server" class="textocajas" Height="100px" TextMode="MultiLine"
                        Width="300px" Enabled="False"></asp:TextBox>--%>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

