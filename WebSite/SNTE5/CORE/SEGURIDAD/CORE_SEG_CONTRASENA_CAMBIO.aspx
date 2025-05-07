<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CORE_SEG_CONTRASENA_CAMBIO.aspx.vb" Inherits="SNTE5.CORE_SEG_CONTRASENA_CAMBIO" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>
<html lang="en">
<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template">
    <meta name="author" content="GeeksLabs">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal">
    <link rel="shortcut icon" href="img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>SNTE</title>
    <!-- Bootstrap CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet">
    <!-- bootstrap theme -->
    <link href="/css/bootstrap-theme.css" rel="stylesheet">
    <!--external css-->
    <!-- font icon -->
    <link href="/css/elegant-icons-style.css" rel="stylesheet" />
    <link href="/css/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="/css/style.css" rel="stylesheet">
    <link href="/css/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
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

            <div class="login-wrap" style="align-content: center">

                <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />

                <asp:Panel ID="pnl_pswd" runat="server" Visible="true" Width="100%">
                    <h3 align="center" class="texto">Cambio Contraseña:</h3>
                    <asp:Label ID="lbl_subtitulo" runat="server" class="text-info">Su contraseña debe cumplir con los siguientes requerimientos:</asp:Label>
                    <ul class="texto">
                        <li>Debe contener números. </li>
                        <li>Debe contener letras en mayúsculas y minúsculas. </li>
                        <li>Longitud mínima de 8 caracteres. </li>
                    </ul>


                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:TextBox ID="txt_antpwd" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input w_60"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Contraseña actual: </span>
                        </div>
                    </div>





                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:TextBox ID="txt_pwdn1" runat="server" MaxLength="15" class="text_input_nice_input w_60" TextMode="Password"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Nueva contraseña: </span>
                            <ajaxToolkit:PasswordStrength ID="txt_pwdn1_PasswordStrength" runat="server" Enabled="True"
                                TargetControlID="txt_pwdn1" StrengthIndicatorType="Text" DisplayPosition="BelowRight"
                                PrefixText="Fortaleza: " TextCssClass="TextIndicator_TextBox1" MinimumUpperCaseCharacters="1"
                                TextStrengthDescriptions="No cumple;Muy bajo;Bajo;Promedio;Buena;Muy buena;Excelente"
                                PreferredPasswordLength="8" MinimumNumericCharacters="1" RequiresUpperAndLowerCaseCharacters="true" />
                        </div>
                    </div>




                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:TextBox ID="txt_pwdn2" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input w_60"></asp:TextBox>

                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Repita contraseña: </span>


                            <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" Enabled="True"
                                TargetControlID="txt_pwdn2" StrengthIndicatorType="Text"
                                PrefixText="Fortaleza: " TextCssClass="TextIndicator_TextBox1" MinimumUpperCaseCharacters="1"
                                TextStrengthDescriptions="No cumple;Muy bajo;Bajo;Promedio;Buena;Muy buena;Excelente"
                                PreferredPasswordLength="8" MinimumNumericCharacters="1" RequiresUpperAndLowerCaseCharacters="true" />
                        </div>
                    </div>



                    <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary" Text="Guardar" />

                    <asp:LinkButton runat="server" Text="Cambiar Respuesta Secreta" ID="lnk_respuesta" class="textogris"></asp:LinkButton>
                    <br />

                    <asp:Label ID="lbl_Results" runat="server" CssClass="alerta"></asp:Label>




                </asp:Panel>

                <asp:Panel ID="pnl_secreta" runat="server" Visible="False" Width="100%">
                    <h3 align="center" class="texto">Cambio Pregunta y Respuesta Secreta</h3>
                    <asp:Label ID="Label1" runat="server" class="subtitulos"></asp:Label>

    
                        <asp:LinkButton ID="lnk_regresar_respuesta" runat="server" CssClass="textogris" Text="Regresar a Cambio Contraseña"></asp:LinkButton>


                        <asp:Label ID="lbl_descripcion" runat="server" CssClass="texto">Nota: Recuerde que el uso de pregunta y respuesta secreta es para ayudarle a recuperar su contraseña del sistema en caso de olvido. </asp:Label>
                    <br />
                    <br />



                    <div class="text_input_nice_div module_subsec align_items_flex_start">
                        <asp:DropDownList ID="cmb_pregunta" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="true">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Pregunta secreta: </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_pregunta" runat="server" ControlToValidate="cmb_pregunta"
                                CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                        </div>
                    </div>


                 
                            <div class="text_input_nice_div module_subsec align_items_flex_start">
                                <asp:TextBox ID="txt_respuesta" runat="server" class="text_input_nice_input" MaxLength="30"
                                    ValidationGroup="val_respuesta" TextMode="Password" Width="100%"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Respuesta secreta: </span>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_respuesta" runat="server"
                                        ControlToValidate="txt_respuesta" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_respuesta" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers"
                                        TargetControlID="txt_respuesta">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                       

                   
                            <div class="text_input_nice_div module_subsec align_items_flex_start">
                                <asp:TextBox ID="txt_r2" runat="server" class="text_input_nice_input w_100" MaxLength="30"
                                    TextMode="Password"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Repita respuesta secreta:</span>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_r2" runat="server" ControlToValidate="txt_r2"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_r2" runat="server"
                                        Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers"
                                        TargetControlID="txt_r2">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                  



                  
                            <div class="text_input_nice_div module_subsec align_items_flex_start">
                                <asp:TextBox ID="txt_antpwd0" runat="server" MaxLength="15" TextMode="Password" class="text_input_nice_input w_100"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Contraseña actual:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_antpwd0" runat="server" ControlToValidate="txt_antpwd0"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_respuesta"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                   

                    <asp:Button ID="BTN_guardar_secreta" runat="server" ValidationGroup="val_respuesta" class="btn btn-primary" Text="Guardar" Enabled="true" />


                    <asp:Button ID="BTN_cancelar" runat="server" class="btn btn-primary" Enabled="true" Text="Cancelar" />

                    <div class="text_input_nice_div module_subsec align_items_flex_center">
                        <asp:Label ID="lbl_Resultado" runat="server" CssClass="alerta"></asp:Label>
                        <%--  --%>
                 </div>


                </asp:Panel>
            </div>
        </form>
    </div>

</body>
</html>

