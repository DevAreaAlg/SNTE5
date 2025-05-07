<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="UNI_BUSQUEDA_CP.aspx.vb" Inherits="SNTE5.UNI_BUSQUEDA_CP" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <title>Búsqueda de C.P.</title>
    <link rel="stylesheet" href="css/fullcalendar.css" />
    <link href="css/widgets.css" rel="stylesheet" />
    <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
    <link href="css/style-responsive.css" rel="stylesheet" />
    <meta charset="utf-8">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins">

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Sección Oficinas" />
    <meta name="author" content="GeeksLabs" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />






    <link rel="shortcut icon" href="img/favicon.png" />

    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap theme -->
    <link href="/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />
    <link href="css/style-responsive.css" rel="stylesheet" />
    <script src="js/jquery.js"></script>
    <script src="js/jquery-ui-1.10.4.min.js"></script>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>

    <!-- bootstrap -->
    <script src="js/bootstrap.min.js"></script>

    <!-- nice scroll -->
    <script src="js/jquery.scrollTo.min.js"></script>
    <script src="js/jquery.nicescroll.js" type="text/javascript"></script>

    <!-- custom select -->
    <script src="js/jquery.customSelect.min.js"></script>
    <script src="assets/chart-master/Chart.js"></script>

    <!--custome script for all page-->
    <script src="js/scripts.js"></script>


    <script language="JavaScript">

        function Minimize() {
            window.innerWidth = 100;
            window.innerHeight = 100;
            window.screenX = screen.width;
            window.screenY = screen.height;
            alwaysLowered = true;
        }

        function Maximize() {
            window.innerWidth = screen.width;
            window.innerHeight = screen.height;
            window.screenX = 0;
            window.screenY = 0;
            alwaysLowered = false;
        }
        function postea() {
            //window.opener.location.reload();
        }
    </script>


</head>
<body onload="history.forward(1);Minimize();Maximize();">
    <form id="form1" runat="server">
        <section class="wrapper">
             <div class="tamano-cuerpo">
                 <section class="panel" id="panel_avales">
            <header class="panel_header_folder panel-heading">
                <span>BÚSQUEDA DE DIRECCIONES</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show">
                    
                    <div align="right" style="width: 78px">
                        <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true"
                            EnableScriptLocalization="true" />
                    </div>
    
                <!--BUSQUEDA DE PERSONAS FISICAS-->
                
                        <asp:Label ID="lbl_datosrequeridos" runat="server" class="textogris" 
                            Text="Debe escribir por lo menos un parámetro:"></asp:Label>
                        
                        <!--CP-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" ></asp:TextBox>
                                <div class="text_input_nice_labels">
                                     <span class="text_input_nice_label">CP:</span>                              
                                
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_CP"
                                        runat="server" TargetControlID="txt_cp" FilterType="Numbers" Enabled="True" />
                                    </div>
                                    </div>
                                </div>
                            </div>

                      <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                     <asp:TextBox ID="txt_estado" runat="server" class="text_input_nice_input" MaxLength="40" ></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                         <span class="text_input_nice_label">Estado:</span>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_estado"
                            runat="server" TargetControlID="txt_estado" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                           ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True"/>
                                        </div>
                                    </div>
                                </div>
                          </div>
                    <div class="module_subsec low_m columned three_columns">
                          <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                     <asp:TextBox ID="txt_municipio" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                       
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Municipio/Deleg:</span>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_municipio"
                            runat="server" TargetControlID="txt_municipio" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True"/>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    <div class="module_subsec low_m columned three_columns">
                          <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                      <asp:TextBox ID="txt_asentamiento" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                       
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Asentamiento:</span>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_asentamiento"
                            runat="server" TargetControlID="txt_asentamiento" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True"/>
                                        </div>
                                    </div>
                                </div>
                          </div>

                        
                    </div>
                               
                
                <asp:Panel ID="pnl_resultados" runat="server" Width="495px">
                    <asp:Label ID="lbl_encontrados" runat="server" CssClass="texto" Text="Resultados:  CP / ESTADO / MUNICIPIO / ASENTAMIENTO" Width="476px"></asp:Label>
                    <br />
                    <asp:ListBox ID="lst_encontrados" runat="server" Width="490px" Height="163px" Font-Size="XX-Small"></asp:ListBox>
                    <p align="center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                    </p>
                </asp:Panel>
                <p align="center">
                        <!--BOTONES DE CONTROL-->
                        <asp:Button ID="btn_buscarCP" runat="server"  class="btn btn-primary"  Text="Buscar"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_cancelarbusqueda" runat="server" class="btn btn-primary" Text="Cerrar"  />
                </p>
    </form>

</body>
</html>
