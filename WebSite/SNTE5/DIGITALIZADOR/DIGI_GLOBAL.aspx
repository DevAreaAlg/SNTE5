<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="DIGI_GLOBAL.aspx.vb" Inherits="SNTE5.DIGI_GLOBAL" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

    <%  If Not Me.IsPostBack Then
            Session("DOCUMENTOID") = ""
            Session("CLAVE_DOCUMENTO") = ""
        End If%>  
    
    <head id="Head1" runat="server">
        <title>SNTE</title>
        <link href="/css/bootstrap-theme.css" rel="stylesheet" />
        <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />

        <script type="text/javascript">document.onkeydown =function(){if (122 == event.keyCode){event.keyCode = 0; return false; } }</script>
     
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
                Response.Write("function btnUploadGarantia_onclick() {" + vbCrLf)
                Response.Write("var strActionPage;" + vbCrLf)
                Response.Write("var strHostIP;" + vbCrLf)
                Response.Write("var CurrentPathName = unescape(location.pathname); " + vbCrLf)
                Response.Write("var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf(""/"") + 1);" + vbCrLf)
                'Response.Write("strActionPage = CurrentPath + ""GuardaScanGarantias.aspx?Lime=" + Session("CVEGARANTIA").ToString + "&Lime2=" + Session("TIPOGARANTIA").ToString + "&Lime3=" + Session("USERID").ToString + "&Lime4=" + Session("Sesion").ToString + "&Lime5=" + Session("NOMBRE_DOCUMENTO").ToString + "&Lime6=" + Session("FOLIO").ToString + """;")
                Response.Write("strActionPage =  ""/DIGI_SCAN.aspx?Lime=" + Session("FOLIO").ToString + "&Lime2=" + Session("DOCUMENTOID").ToString + "&Lime3=" + Session("USERID").ToString + "&Lime4=" + Session("Sesion").ToString + "&Lime5=" + Session("ESTATUS_EXPEDIENTE").ToString + "&Lime6=" + Session("CLAVE").ToString + "&Lime7=" + Session("FECHA").ToString + """;")
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
        
          
           <%  'JAVA SCRIPT DENTRO DE VB PARA MANDAR VARIABLES DE SESION A OTRO ASPX POR QUERY STRINGS
               'JavaScript de TWAIN
               Response.Write("<script language=""javascript"">" + vbCrLf)
               Response.Write("function btnUpload_onclick() {" + vbCrLf)
               Response.Write("var strActionPage;" + vbCrLf)
               Response.Write("var strHostIP;" + vbCrLf)
               Response.Write("var CurrentPathName = unescape(location.pathname); " + vbCrLf)
               Response.Write("var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf(""/"") + 1);" + vbCrLf)
               Response.Write("strActionPage = ""/DIGITALIZADOR/DIGI_SCAN.aspx?Lime=" + Session("FOLIO").ToString + "&Lime2=" + Session("DOCUMENTOID").ToString + "&Lime3=" + Session("USERID").ToString + "&Lime4=" + Session("Sesion").ToString + "&Lime5=" + Session("ESTATUS_EXPEDIENTE").ToString + "&Lime6=" + Session("CLAVE").ToString + "&Lime7=" + Session("FECHA").ToString + """;")

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
            function DynamicWebTwain1_OnPostAllTransfers() {
                if (Form1.DynamicWebTwain1.HowManyImagesInBuffer > 0) {
                    document.getElementById("btn_Guardar").disabled = null;
                }
            }
            function presentation_onclick() { }
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
                <asp:Label CssClass="page-header"  ID="lbl_tituloASPX" runat="server" Text="Digitalización Global"></asp:Label>

                 <div class="panel" style="background-color:#F8F9F9;line-height:34px;padding:0px 15px;font-size:14px; border:solid 1px #c0cdd5; " >
                     <div class="module_subsec low_m align_items_flex_center">
                        <asp:LinkButton ID="lnk_BreadInicio" CssClass="module_subsec_elements module_subsec_small-elements" href="../../NOTIFICACIONES.aspx" runat="server">Inicio </asp:LinkButton> 
                            &nbsp;<asp:Label runat="server" Text=" / "></asp:Label>&nbsp;
                        <asp:LinkButton ID="lnk_regresar" CssClass="module_subsec_elements module_subsec_small-elements" runat="server">Consulta </asp:LinkButton>
                        <asp:Panel runat="server" id="breadcrumb" ></asp:Panel>
                    </div>
                 </div>
 
<section class="panel" runat="server" id="panel_generales">
                             
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_generales">
                        <p class="center">
                           <asp:Label ID="lbl_ArchPermitidos" runat="server" Class="alerta"></asp:Label>
                        </p>

                        <div class="module_subsec low_m columned two_columns">
                                                         
                            <div class="module_subsec_elements vertical">
                                <div class=" no_column" style="align-items:flex-start !important; display:flex;">
                                   <div class="module_subsec_elements text_input_nice_div">                                       
                                        <asp:DropDownList ID="cmb_DocNoDigi" class="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="True">
                                        </asp:DropDownList>

                                       <span class="text_input_nice_label">1.- Elegir documento por digitalizar:</span>
                                    </div>                   
                                </div>
                            </div>   
                            
                          
                                <div class=" no_column" style="align-items:flex-start !important; display:block; text-align:Center">
                                    <div class="module_subsec_elements text_input_nice_div">
                                        <asp:Button ID="btn_ElegirDocumento" runat="server" class="btn btn-primary" Text="Escanear" Enabled="false" align="center"></asp:Button>
                               
                                        <span class="text_input_nice_label">2.- Elegir si desea escanear el documento:</span>

                                    </div>
                                </div>
                          
                
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

                                <p class="center">
                                   <asp:Label ID="lbl_TamMax" runat="server" Class="alerta"></asp:Label>
                                </p>

                                <div class="module_subsec no_column ">
                                    <div class="module_subsec_elements">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Enabled="false"/>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <asp:Button ID="btn_Upload" cssclass="btn btn-primary" runat="server" Text="Subir" width="100%" Enabled="false"/>
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
                                   
                                </div> 


                                <div align="center">
                                    <asp:Label ID="lbl_AlertaVerBorrar" runat="server" Text="" class="alerta"></asp:Label>
                                </div>
                            </div>

                            <div class="module_subsec_elements vertical align_items_flex_center">                
                    
                                     
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

                                  
                        <p class="center">
                           <asp:Label ID="lbl_AlertaNoBorrar" runat="server" Class="alerta"></asp:Label>
                        </p>

                        <%--<div class="module_subsec flex_end">
                            <asp:Button ID="btn_Guardar" runat="server" class="btn btn-primary" Text="Guardar"  visible="TRUE"></asp:Button>
                                        &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Borrar" runat="server" class="btn btn-primary" Text="Eliminar" visible="TRUE"></asp:Button>
                        </div>--%>
                            
                    </div>
                </div>
            </section>
                 </div>   
          
        </form>


        </body>
</html>



