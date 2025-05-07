<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="UNI_BUSQUEDA_PERSONA.aspx.vb" Inherits="SNTE5.UNI_BUSQUEDA_PERSONA" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BUSQUEDA PERSONA</title>
    <meta charset="utf-8"/>
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal, timepicki, timepicker, time, jquery, plugins"/>

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

    <script language="JavaScript" type="text/javascript">

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

        <section class="panel" id="panel_persona">
            <div class="panel-body">
                <div class="panel-body_content init_show">
   
    <div align="center">
        <h4 style="font-weight:normal"  class="resalte_azul">Búsqueda de Agremiados</h4>
    </div>

    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" />

        <asp:Label runat="server" Text="Tipo persona:" CssClass="texto" ID="lbl_tipoPersona"></asp:Label>&nbsp;&nbsp;

        <asp:RadioButton runat="server" GroupName="tipop" Text="F&#237;sica" ID="rad_pf"  class="text_input_nice_label" AutoPostBack="True"></asp:RadioButton> &nbsp;&nbsp;
        <asp:RadioButton runat="server" GroupName="tipop" Text="Moral" ID="rad_pm"  class="text_input_nice_label" AutoPostBack="True"></asp:RadioButton>
             
        <!--BUSQUEDA DE PERSONAS FISICAS-->
        <asp:Panel ID="pnl_busquedafisica" runat="server" Visible="False">
            
            <div class="module_subsec">
          &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <span>Nota: Debe escribir por lo menos un nombre para realizar la búsqueda</span>
               
            </div>            
            
            

            <!-- Institución  del afiliado -->
              <%--             <div class= "module_subsec no_m columned two_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_institucionBusq" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center">
                                          
                                    </asp:DropDownList>
                                  
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Institución</span>
                                    </div>
                                </div>
                            </div>
                               </div>
              <div class="module_subsec">
                <span>Si no desea filtrar por institución, por favor seleccione el valor ELIJA en el catálogo de instituciones.</span>
              </div>--%>

            <!--NOMBRES-->
            <div class= "module_subsec no_m columned two_columns">
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_nombre1buscar1" runat="server" CssClass="text_input_nice_input" MaxLength="300"
                                ValidationGroup="val_buscarf1"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_nombrebuscar1" class="text_input_nice_label">Nombre(s):</label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre1buscar1"
                            runat="server" TargetControlID="txt_nombre1buscar1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>
                <%-- <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_paternobuscar1" runat="server" CssClass="text_input_nice_input" MaxLength="100"
                                ValidationGroup="val_buscarf1"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_paternobuscar1" class="text_input_nice_label">Apellido paterno:</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paternobuscar1"
                                runat="server" TargetControlID="txt_paternobuscar1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />   
                        </div>
                    </div>
                </div>--%>
      
                
                <%--<div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_maternobuscar1" runat="server" CssClass="text_input_nice_input" MaxLength="100">
                        </asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_maternobuscar1" class="text_input_nice_label">Apellido materno:</label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maternobuscar1"
                                runat="server" TargetControlID="txt_maternobuscar1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>--%>
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_rfc" runat="server" CssClass="text_input_nice_input" MaxLength="13"
                                ValidationGroup="val_buscarf1"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_rfcbuscar1" class="text_input_nice_label">RFC:</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfc"
                                runat="server" TargetControlID="txt_rfc" FilterType="Numbers, UppercaseLetters, LowercaseLetters"
                                ValidChars="" />   
                        </div>
                    </div>
                </div>   
            </div>
        </asp:Panel>

        <!--PANEL DE BUSQUEDA DE PERSONAS MORALES-->
        <asp:Panel ID="pnl_busquedamoral" runat="server" Visible="False">
            <div class="module_subsec">
                <span>Debe escribir la Razon Social o RFC de la persona moral</span>
            </div> 
            <!--NÚMERO DE AFILIADO-->
            <div class= "module_subsec no_m columned two_columns">
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_numclientem" runat="server" CssClass="text_input_nice_input" MaxLength="9"
                                ValidationGroup="val_buscarm"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_numctem" class="text_input_nice_label">Número de afiliado:</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_numclientem"
                                runat="server" TargetControlID="txt_numclientem" FilterType="Numbers" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_razonsocial" runat="server" CssClass="text_input_nice_input" MaxLength="300" 
                                ValidationGroup="val_buscarm"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_razon" class="text_input_nice_label">Razon social:</label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_razon"
                            runat="server" TargetControlID="txt_razonsocial" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>
            </div>
            
            <!--NOMBRE COMERCIAL-->
            <div class= "module_subsec no_m columned two_columns">
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_nombrecomercial" runat="server" CssClass="text_input_nice_input" MaxLength="300"
                                ValidationGroup="val_buscarm"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_comercial" class="text_input_nice_label">Nombre comercial:</label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_comercial"
                            runat="server" TargetControlID="txt_nombrecomercial" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements"> 
	                <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_rfcm" runat="server" CssClass="text_input_nice_input" MaxLength="15"
                                ValidationGroup="val_buscarm"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <label id="lbl_rfcwrong1" class="text_input_nice_label">RFC:</label>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfcm"
                                runat="server" TargetControlID="txt_rfcm" FilterType="Numbers, UppercaseLetters, LowercaseLetters"
                                ValidChars="" />   
                        </div>
                    </div>
                </div>
            </div>
            
        </asp:Panel>                                         

        <!--RESULTADOS-->                  
        <asp:Panel ID="pnl_resultados" runat="server" Visible="False">
            <label id="lbl_encontrados" class="texto">Personas encontradas:</label>
            <div class="module_subsec">
                <asp:ListBox ID="lst_encontrados" runat="server" class="text_input_nice_textarea" Width="620px" Height="119px"
                    Font-Size="XX-Small"></asp:ListBox>
            </div>
            <div align="center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                <br />
                            
                <!--BOTONES DE CONTROL-->
                <asp:Button ID="btn_buscarmoral" runat="server" class="btn btn-primary" Text="Buscar" Visible="false"
                    ValidationGroup="val_buscarm1" />
                <asp:Button ID="btn_buscarfisica" runat="server" class="btn btn-primary" Text="Buscar" Visible="false"
                    ValidationGroup="val_buscarf1" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_aceptarbusqueda" runat="server" class="btn btn-primary" Text="Aceptar" ValidationGroup="val_buscar1"  />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancelarbusqueda" runat="server" class="btn btn-primary" Text="Cancelar" ValidationGroup="val_buscar1" />
            </div>
        </asp:Panel>              

            <input type="hidden" name="hdn_conyuge" id="hdn_conyuge" value="" runat="server" />

            </div>
        </div>
    </section>
    </form>
</body>
</html>
