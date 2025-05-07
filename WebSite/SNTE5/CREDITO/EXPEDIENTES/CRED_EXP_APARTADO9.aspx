<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CRED_EXP_APARTADO9.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO9" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

    <%  If Not Me.IsPostBack Then
            Session("DOCUMENTOID") = ""
            Session("CLAVE_DOCUMENTO") = ""
        End If%>  
    
    <head id="Head1" runat="server">
        <title>SNTE SECCIÓN 5</title>
        <link href="/css/bootstrap-theme.css" rel="stylesheet" />
        <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />

        <script type="text/javascript">document.onkeydown =function(){if (122 == event.keyCode){event.keyCode = 0;return false;}}</script>
     
        <script type="text/javascript">
        // Javascript para inicializar botones de scan con TWAIN
            function btnScan_onclick() {
                Form1.DynamicWebTwain1.SelectSource();
                Form1.DynamicWebTwain1.MaxImagesInBuffer = 500;
                Form1.DynamicWebTwain1.AcquireImage();
                //Form1.DynamicWebTwain1.Resolution(600);
            }

            function btnBorrar_onclick() {
                Form1.DynamicWebTwain1.CloseSource();
            }
        </script>

        </head>

   <body onload="history.forward(1);">
             
        <%  'JAVA SCRIPT DENTRO DE VB PARA MANDAR VARIABLES DE SESION A OTRO ASPX POR QUERY STRINGS
            'JavaScript de TWAIN
            Response.Write("<script language=""javascript"">" + vbCrLf)
            Response.Write("function btnUpload_onclick() {" + vbCrLf)
            Response.Write("var strActionPage;" + vbCrLf)
            Response.Write("var strHostIP;" + vbCrLf)
            Response.Write("var CurrentPathName = unescape(location.pathname); " + vbCrLf)
            Response.Write("var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf(""/"") + 1);" + vbCrLf)
            Response.Write("strActionPage = ""/DIGITALIZADOR/DIGI_SCAN.aspx?Lime=" + Session("FOLIO").ToString + "&Lime2=" + Session("DOCUMENTOID").ToString + "&Lime3=" + Session("USERID").ToString + "&Lime4=" + Session("Sesion").ToString + "&Lime5=" + Session("ESTATUS_EXPEDIENTE").ToString + "&Lime6=" + Session("CLAVE_DOCUMENTO").ToString + "&Lime7=" + txt_fechadoc.Text + """;")

            Response.Write("strHostIP = """ + Split(Request.ServerVariables("HTTP_HOST"), ":")(0) + """;" + vbCrLf)
            Response.Write("Form1.DynamicWebTwain1.HTTPPort = """ + Request.ServerVariables("SERVER_PORT") + """;" + vbCrLf)
            If Request.ServerVariables("SERVER_PORT") = "443" Then
                Response.Write("{Form1.DynamicWebTwain1.IfSSL = true;}" + vbCrLf)
            Else
                Response.Write("{Form1.DynamicWebTwain1.IfSSL = false;}" + vbCrLf)
            End If
            Response.Write("var Nombre = ""pic""" + vbCrLf)
            Response.Write("Form1.DynamicWebTwain1.HTTPUploadAllThroughPostAsPDF(strHostIP, strActionPage, Nombre + "".pdf"");" + vbCrLf)
            Response.Write("if (Form1.DynamicWebTwain1.ErrorCode != 0){" + vbCrLf)
            Response.Write("alert(Form1.DynamicWebTwain1.ErrorString);" + vbCrLf)
            Response.Write("alert(Form1.DynamicWebTwain1.HTTPPostResponseString);}}" + vbCrLf)

            Response.Write("</script>" + vbCrLf)
        %>
    
        <script type="text/javascript">
            function DynamicWebTwain1_OnPostAllTransfers(){
                if (Form1.DynamicWebTwain1.HowManyImagesInBuffer > 0){
                    document.getElementById("btn_Guardar").disabled = null;
                }
            } 
            function presentation_onclick() {}
        </script>
    
        <script language="javascript" type="text/javascript" for="presentation" event="onclick">
            return presentation_onclick()  
        </script>

        <style type="text/css">
            #DynamicWebTwain1
            {
                height: 250px;
                width: 150px;
            }
        </style>

        <form id="Form1" runat="server">
            <ajaxtoolkit:toolkitscriptmanager runat="Server" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
                      
            <div class="tamano-cuerpo">
                <asp:Label CssClass="page-header"  ID="lbl_tituloASPX" runat="server" Text="Digitalización de Documentos"></asp:Label>

                 <div class="panel" style="background-color:#F8F9F9;line-height:34px;padding:0px 15px;font-size:14px; border:solid 1px #c0cdd5; " >
                     <div class="module_subsec low_m align_items_flex_center">
                        <asp:LinkButton ID="lnk_BreadInicio" CssClass="module_subsec_elements module_subsec_small-elements" href="../../NOTIFICACIONES.aspx" runat="server">Inicio </asp:LinkButton> 
                            &nbsp;<asp:Label runat="server" Text=" / "></asp:Label>&nbsp;
                        <asp:LinkButton ID="lnk_regresar" CssClass="module_subsec_elements module_subsec_small-elements" runat="server">Préstamo </asp:LinkButton>
                        <asp:Panel runat="server" id="breadcrumb" ></asp:Panel>
                    </div>
                 </div>

                <section class="panel">
                    <header class="panel-heading">
                        <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
                    </header>
                    <div class="panel-body">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
                            <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                            <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div> 
                    </div>
                </section>
                
                <section class="panel">
                    <header class="panel-heading">
                        <span>Digitalización</span>
                    </header>
                    <div class="panel-body">

                        <div class="module_subsec columned two_columns">
                            <div class="module_subsec_elements vertical">
                                <div class=" no_column" style="align-items:flex-start !important; display:flex;">
                                    <div class="w_100 module_subsec_elements">
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">1.- Elegir documento por digitalizar:</span>
                                        </div>
                                        <asp:ListBox ID="lst_DocNoDigi" runat="server" Width="100%" ></asp:ListBox>
                                        <div class="low_m">
                                            <asp:Label ID="lbl_TamMax" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements vertical">
                                <div class="module_subsec no_column">
                                    <div class="w_100 module_subsec_elements text_input_nice_div">
                                        <asp:TextBox ID="txt_fechadoc" class="text_input_nice_input" runat="server" MaxLength="10" Enabled="false"></asp:TextBox> 
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Fecha expedición (DD/MM/AAAA):</span>
                                            <asp:Label ID="lbl_FechaExp" runat="server" Text="" class="alerta"></asp:Label>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechadoc" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechadoc" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender_fechadoc" ControlToValidate="txt_fechadoc" CssClass="textogris" 
                                            ErrorMessage="MaskedEditExtender_fechadoc" InvalidValueMessage="Fecha inválida" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechadoc" />
                                            <asp:RequiredFieldValidator runat="server" ID="req_fechadoc" CssClass="alertaValidator bold"
                                            ControlToValidate="txt_fechadoc" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_FechaDoc"/>
                                        </div>
                                        
                                    </div>
                               
                                    <div class="module_subsec_elements text_input_nice_div">
                                        <asp:Button ID="btn_Digitalizar" CssClass="btn btn-primary" runat="server" Text="Elegir" />
                                    </div>

                                    <div class="module_subsec_elements" style="max-width:300px;">
                                        <asp:DropDownList ID="lst_DocumentosEspecificos" runat="server" class="btn btn-primary2 dropdown_label w_100" Visible="false" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_ElegirDocumento" CssClass="btn btn-primary" runat="server" Text="Escanear" Visible="false"/>
                                    </div>
                                </div>
                            </div>
                                
                        </div>

                        <div align="center">
                            <asp:Label ID="lbl_AlertaDigitaliza" runat="server" class="alerta" Text=""></asp:Label>
                        </div>
                        

                        <object classid = "clsid:5220cb21-c88d-11cf-b347-00aa00a28331" VIEWASTEXT>
	                                <param name="LPKPath" value="DynamicWebTwain.lpk" />
                                </object>

                        <div class="module_subsec columned two_columns align_items_flex_start">
                            <div class="module_subsec_elements vertical">

                                <div class="module_sec ">
                                    <div class="text_input_nice_div module_subsec_elements_content"  >
                                        <span>2.- Elegir si desea subir el archivo:</span>
                                    </div>  
                                </div>

                                <div class="module_subsec no_column ">
                                    <div class="module_subsec_elements">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Enabled="false"/>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Subir" cssclass="btn btn-primary" runat="server" Text="Subir" width="100%" Enabled="false"/>
                                    </div>
                                </div>

                                <div align="center">
                                    <asp:Label ID="lbl_UploadEstatus" runat="server" class="alerta"></asp:Label>
                                </div>

                                <div class="module_sec low_m">
                                    <div class="text_input_nice_div module_subsec_elements_content"  >
                                        <span  class="title_tag">3.- Documentos digitalizados:</span>
                                    </div>  
                                </div>

                                <div class="module_subsec no_column">
                                        <div class="module_subsec_elements w_100 vertical">
                                        <asp:ListBox ID="lst_DocDigi" runat="server" Width="100%" Height="90px"></asp:ListBox>
                                    </div>
                                </div>

                                <div class="module_subsec no_column low_m">
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Ver" CssClass="btn btn-primary" runat="server" Text="Ver"/>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Eliminar" CssClass="btn btn-primary" runat="server" Text="Eliminar" />
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Insertar_ColaValidacion" CssClass="btn btn-primary2" runat="server" Text="Concluir digitalización" />
                                    </div>
                                </div> 

                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_AlertaNoBorrar" runat="server" class="alerta"></asp:Label>
                                </div>

                                <div align="center">
                                    <asp:Label ID="lbl_AlertaVerBorrar" runat="server" Text="" class="alerta"></asp:Label>
                                </div>
                            </div>

                            <div class="module_subsec_elements vertical align_items_flex_center">                
                    
                                

                                <div class="module_subsec">
                                    <span>2.- Elegir si desea escanear el documento:</span>
                                </div> 
                                     
                                <div class="module_subsec">
                                    <span>Vista Previa</span>
                                </div>

                                <div class="module_subsec" style="box-shadow:0px 0px 1px #888;">                
                                        <OBJECT classid="clsid:E7DA7F8D-27AB-4EE9-8FC0-3FEC9ECFE758" border="0" 
                                            id="DynamicWebTwain1" CodeBase = "DynamicWebTWAIN.cab#version=5,1">
					                        <!-- PARAMETROS DE DIGITALIZADOR -->
					                        <PARAM NAME="_cx" VALUE="5292">
							                <PARAM NAME="_cy" VALUE="9260">
							                <PARAM NAME="JpgQuality" VALUE="80">
							                <PARAM NAME="Manufacturer" VALUE="DynamSoft Corporation">
							                <PARAM NAME="ProductFamily" VALUE="Dynamic Web TWAIN">
							                <PARAM NAME="ProductName" VALUE="Dynamic Web TWAIN">
							                <PARAM NAME="VersionInfo" VALUE="Dynamic Web TWAIN 5.1">
							                <PARAM NAME="TransferMode" VALUE="0">
							                <PARAM NAME="BorderStyle" VALUE="0">
							                <PARAM NAME="FTPUserName" VALUE="">
							                <PARAM NAME="FTPPassword" VALUE="">
							                <PARAM NAME="FTPPort" VALUE="21">
							                <PARAM NAME="HTTPUserName" VALUE="">
							                <PARAM NAME="HTTPPassword" VALUE="">
							                <PARAM NAME="HTTPPort" VALUE="80">
							                <PARAM NAME="ProxyServer" VALUE="">
							                <PARAM NAME="IfDisableSourceAfterAcquire" VALUE="0">
							                <PARAM NAME="IfShowUI" VALUE="-1">
							                <PARAM NAME="IfModalUI" VALUE="-1">
							                <PARAM NAME="IfTiffMultiPage" VALUE="0">
							                <PARAM NAME="IfThrowException" VALUE="0">
							                <PARAM NAME="MaxImagesInBuffer" VALUE="300">
							                <PARAM NAME="TIFFCompressionType" VALUE="4">
							                <PARAM NAME="IfFitWindow" VALUE="-1">
							                <PARAM NAME="IfSSL" VALUE="0">
						                </OBJECT>
                                    </div>

                                <div class="module_subsec no_column low_m">
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Guardar" CssClass="btn btn-primary" runat="server" Text="Guardar"  Enabled="false"/>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Borrar" CssClass="btn btn-primary" runat="server" Text="Borrar"  Enabled="false"/>
                                    </div>
                                </div> 
                            </div>
                        </div>
                        
                    </div>
                </section>
            </div>   
          
        </form>
    </body>
</html>


