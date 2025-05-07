<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_TIRAEFECTIVO.aspx.vb" Inherits="SNTE5.CRED_VEN_TIRAEFECTIVO" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
           <meta charset="utf-8">
        <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins">

        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta name="description" content="Sección Oficinas" />
        <meta name="author" content="GeeksLabs" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

        <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
        <link rel="shortcut icon" href="img/favicon.png" />

        <!-- Bootstrap CSS -->
        <link href="/css/bootstrap.min.css" rel="stylesheet" />
        <!-- bootstrap theme -->
        <link href="/css/bootstrap-theme.css" rel="stylesheet" />
        <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />
        <link href="/css/style-responsive.css" rel="stylesheet" />

        <!-- bootstrap -->
        <script src="/js/bootstrap.min.js"></script>

        <!-- nice scroll -->
        <script src="/js/jquery.scrollTo.min.js"></script>
        <script src="/js/jquery.nicescroll.js" type="text/javascript"></script>

        <!-- custom select -->
        <script src="/js/jquery.customSelect.min.js"></script>
        <script src="/assets/chart-master/Chart.js"></script>

        <!--custome script for all page-->
        <script src="js/scripts.js"></script>
        <script type="text/javascript">
                function doParentActivatePostBack()
                {
                    if (window.opener.ActivatePostBack)
                    {
                        window.opener.ActivatePostBack();
                    }
                }

                function presionaTecla(ControlTexto,ControlTextoAnterior,ControlTextoSiguiente,event) {
                    var CTextbox = document.getElementById(ControlTexto);
                    var CTextboxAnterior = document.getElementById(ControlTextoAnterior);
                    var CTextboxSiguiente = document.getElementById(ControlTextoSiguiente);
                    var CLink = document.getElementById('lnk_Confirmar');
                    //var CButton = document.getElementById(ControlButton);
                    switch (event.keyCode) {
                        case 38:
                            //Flecha arriba
                            if (CTextboxAnterior != null) {
                                event.returnValue = false;
                                event.cancel = true;
                                CTextboxAnterior.focus()
                                return true;
                            }
                            break;

                        case 40:
                            //Flecha abajo
                            if (CTextboxSiguiente != null) {
                                event.returnValue = false;
                                event.cancel = true;
                                CTextboxSiguiente.focus()
                                return true;
                            }
                            else {
                                event.returnValue = false;
                                event.cancel = true;
                                CLink.focus();
                                return true;
                            }
                            break;

                        case 35:
                            //Tecla FIN
                            event.returnValue = false;
                            event.cancel = true;
                            CLink.focus();
                            return true;
                            break;

                        default:
                            break;

                    }
                }
                

           </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:Panel ID="pnl_cobranza" runat="server" Visible="true" >
                    <div>
                        <asp:Label ID="lbl_EntradaSalida" runat="server" class="subtitulos flex_center"></asp:Label>
                    </div>

                    <table>
                        <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />
                        <tr style="vertical-align:top !important;">
                            <td>                                                
                                <asp:Label ID="lbl_BILLETE" runat="server" class="texto" Width="70px">BILLETES</asp:Label>
                                    <br /><br />                                      
                                <asp:Label ID="lbl_Deno1" runat="server" class="texto" Width="70px">1000.00</asp:Label>
                                <asp:TextBox ID="txt_Cant1" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Cant1" 
                                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant1">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txt_Mont1" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                        <br /><br />                                       
                                <asp:Label ID="lbl_Deno2" runat="server" class="texto" Width="70px">500.00</asp:Label>
                                <asp:TextBox ID="txt_Cant2" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant2">
                                </ajaxToolkit:FilteredTextBoxExtender> 
                                <asp:TextBox ID="txt_Mont2" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno3" runat="server" class="texto" Width="70px">200.00</asp:Label>
                                <asp:TextBox ID="txt_Cant3" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant3">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont3" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno4" runat="server" class="texto" Width="70px">100.00</asp:Label>
                                <asp:TextBox ID="txt_Cant4" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant4">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont4" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno5" runat="server" class="texto" Width="70px">50.00</asp:Label>
                                <asp:TextBox ID="txt_Cant5" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                    runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_Cant5">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont5" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno6" runat="server" class="texto" Width="70px">20.00</asp:Label>
                                <asp:TextBox ID="txt_Cant6" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="BILLETE"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant6">
                                </ajaxToolkit:FilteredTextBoxExtender>                                               
                                <asp:TextBox ID="txt_Mont6" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                            </td>
                            <td>                                               
                                <asp:Label ID="lbl_MONEDA" runat="server" class="texto" Width="70px">MONEDAS</asp:Label>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno7" runat="server" class="texto" Width="70px">100.00</asp:Label>
                                <asp:TextBox ID="txt_Cant7" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant7">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont7" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno8" runat="server" class="texto" Width="70px">20.00</asp:Label>
                                <asp:TextBox ID="txt_Cant8" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <asp:TextBox ID="txt_Mont8" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant8">
                                </ajaxToolkit:FilteredTextBoxExtender>                                       
                                    <br /><br />
                                <asp:Label ID="lbl_Deno9" runat="server" class="texto" Width="70px">10.00</asp:Label>
                                <asp:TextBox ID="txt_Cant9" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant9">
                                </ajaxToolkit:FilteredTextBoxExtender> 
                                <asp:TextBox ID="txt_Mont9" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno10" runat="server" class="texto" Width="70px">5.00</asp:Label>
                                <asp:TextBox ID="txt_Cant10" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant10">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont10" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno11" runat="server" class="texto" Width="70px">2.00</asp:Label>
                                <asp:TextBox ID="txt_Cant11" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant11">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont11" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno12" runat="server" class="texto" Width="70px">1.00</asp:Label>
                                <asp:TextBox ID="txt_Cant12" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant12">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont12" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno13" runat="server" class="texto" Width="70px">0.50</asp:Label>
                                <asp:TextBox ID="txt_Cant13" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant13">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont13" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno14" runat="server" class="texto" Width="70px">0.20</asp:Label>
                                <asp:TextBox ID="txt_Cant14" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant14">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont14" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno15" runat="server" class="texto" Width="70px">0.10</asp:Label>
                                <asp:TextBox ID="txt_Cant15" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant15">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont15" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br />
                                <asp:Label ID="lbl_Deno16" runat="server" class="texto" Width="70px">0.05</asp:Label>
                                <asp:TextBox ID="txt_Cant16" runat="server" CssClass="text_input_nice_input" AutoPostBack = "true" Width = "40px" MaxLength = "5" ToolTip="MONEDA"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" 
                                    runat="server" Enabled="True" FilterType="Numbers" 
                                    TargetControlID="txt_Cant16">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="txt_Mont16" runat="server" CssClass="text_input_nice_input" Enabled = "false" Width = "70px"></asp:TextBox>
                                    <br /><br /><br /><br />
                            </td>
                            <td style="align-content:flex-start !important">
                                <asp:Label ID="lbl_Req" runat="server" class="texto">Requerido:</asp:Label>
                                <asp:Label ID="lbl_MontoReq" runat="server" class="texto"></asp:Label>
                                    <br /><br />
                                <asp:Label ID="lbl_Acum" runat="server" class="texto">Acumulado:</asp:Label>
                                <asp:Label ID="lbl_MontoAcum" runat="server" class="texto"></asp:Label>
                                    <br /><br />
                                <asp:Label ID="lbl_Falt" runat="server" class="texto">Faltante:</asp:Label>
                                <asp:Label ID="lbl_MontoFalt" runat="server" class="texto"></asp:Label>
                                    <br /><br />
                                <asp:Button ID="lnk_Confirmar" runat="server" class="btn btn-primary" Text="Confirmar"></asp:Button>
                                    <br /><br />                                            
                                <asp:Button ID="lnk_Union" runat="server" class="btn btn-primary" Text="Unir Operacion"></asp:Button>
                                    <br /><br />                
                                <asp:Button ID="lnk_Cancelar" runat="server"  class="btn btn-primary" Text="Cancelar"></asp:Button>
                                    <br /><br />                                          
                                <asp:Button ID="lnk_Limpia" runat="server" class="btn btn-primary" Text="Limpiar"></asp:Button>
                                    <br /><br /> 
                                <asp:RadioButton ID="rad_Indepen" runat="server" class="texto" GroupName="UnionOp" Text="Independiente" Visible="false" AutoPostBack = "true"/>
                                <asp:RadioButton ID="rad_Union" runat="server" class="texto" GroupName="UnionOp" Text="Union" Visible="false" AutoPostBack = "true"/>
                            </td> 
                        </tr>                                  
                    </table>
                    <table>
                        <td>   
                            <asp:Label ID="lbl_Info" runat="server" CssClass="alerta"></asp:Label>                                   
                        </td>
                    </table>
                    <br />
                </asp:Panel>
            </div>
        </form>
    </body>
</html>

