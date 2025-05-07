<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CORE_SEG_CONTRASENA_RECUPERAR.aspx.vb" Inherits="SNTE5.CORE_SEG_CONTRASENA_RECUPERAR" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template"/>
    <meta name="author" content="GeeksLabs"/>
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal"/>
    <link rel="shortcut icon" href="img/favicon.png"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>SNTE</title>
    <!-- Bootstrap CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- bootstrap theme -->
    <link href="/css/bootstrap-theme.css" rel="stylesheet"/>
    <!--external css-->
    <!-- font icon -->
    <link href="/css/elegant-icons-style.css" rel="stylesheet" />
    <link href="/css/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="/css/style.css" rel="stylesheet"/>
    <link href="/css/style-responsive.css" rel="stylesheet" />


    <script id="clientEventHandlersJS" type="text/javascript">
        function GMA() {

            if (document.getElementById('hdn_counter').value == 1) {
                var locator = new ActiveXObject("WbemScripting.SWbemLocator");
                var service = locator.ConnectServer(".");
                var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration");
                var e = new Enumerator(properties);
                var c = new String;
                var p;
                for (; !e.atEnd() ; e.moveNext()) {
                    p = e.item();
                    if (p.MACAddress != null) {
                        c += p.MACAddress + ",";
                    }
                }
                document.getElementById('hdn_mcs').value = c;
                document.getElementById('hdn_counter').value = 2;
                __doPostBack('', '');
            }
        }

    </script>
</head>
<body class="login-img3-body" onload="GMA();">
    <div class="container">
        <form class="login-form" runat="server">
             <h1 align="center">RECUPERAR CONTRASEÑA</h1>
            <div class="login-wrap" style="align-content: center">

                <div class="text_input_nice_div module_subsec align_items_flex_start">
                    <asp:TextBox ID="txt_Mail" runat="server" CssClass="form-control w_100" MaxLength="100"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label module_subsec_elements">Ingrese el correo electrónico que tiene registrado: </span>
                    </div>
                </div>
                <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                    <asp:Button ID="btn_Enviar"  runat="server" class="btn btn-primary" Text="Enviar" />
                </div>


                <asp:Panel ID="pnl_usr" runat="server" Visible="False">

                    <div class="text_input_nice_div module_subsec align_items_flex_start w_100">
                        <asp:DropDownList ID="cmb_usuarios" runat="server" AutoPostBack="True" CssClass="btn btn-primary2 dropdown_label w_100"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label module_subsec_elements">Usuario: </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_usuarios"
                                runat="server" ControlToValidate="cmb_usuarios" CssClass="textogris"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                ValidationGroup="val_usuario"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                        <asp:Button ID="btn_usuario" runat="server" class="btn btn-primary" Text="Enviar" ValidationGroup="val_usuario" />
                    </div>


                </asp:Panel>
                <asp:Panel ID="pnl_pregunta" runat="server" Visible="False">

                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:Label ID="lbl_pregunta" runat="server" class="texto"></asp:Label>

                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Pregunta secreta:</span>
                            
                        </div>
                    </div>

                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:TextBox ID="txt_respuesta" runat="server" CssClass="form-control w_100" MaxLength="50" TextMode="Password"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Respuesta secreta:</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_respuesta" runat="server"
                                ControlToValidate="txt_respuesta" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_respuesta">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    

                    <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                        <asp:Button ID="btn_EnviarR" class="btn btn-primary" runat="server" Text="Enviar" ValidationGroup="val_respuesta" />
                    </div>

                </asp:Panel>
                <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_center">
                    <asp:Label ID="lbl_Results" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec no_sm module_subsec_elements module_subsec_big-elements no_column flex_end">
                    <asp:LinkButton ID="lnk_Regresar" runat="server" class="copyright">Regresar</asp:LinkButton>
                </div>
            </div>
            <input type="hidden" name="hdn_mcs" id="hdn_mcs" runat="server" value="" />
            <input type="hidden" name="hdn_counter" id="hdn_counter" runat="server" value="1" />
        </form>
    </div>
    <%-- <asp:Label ID="lbl_hdnmcs" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lbl_hdncounter" runat="server" Text="Label"></asp:Label>--%>
</body>

</html>

