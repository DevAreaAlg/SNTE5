<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_APARTADO4.aspx.vb" Inherits="SNTE5.CAP_EXP_APARTADO4" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType VirtualPath="~/MasterMascore.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%  'If Not Me.IsPostBack Then
        '    Session("DOCUMENTOID") = ""
        '    Session("CLAVE_DOCUMENTO") = ""
        'End If

        'JAVA SCRIPT DENTRO DE VB PARA MANDAR VARIABLES DE SESION A OTRO ASPX POR QUERY STRINGS
        'JavaScript de TWAIN
        Response.Write("<script language=""javascript"">" + vbCrLf)
        Response.Write("function btnUpload_onclick() {" + vbCrLf)
        Response.Write("var strActionPage;" + vbCrLf)
        Response.Write("var strHostIP;" + vbCrLf)
        Response.Write("var CurrentPathName = unescape(location.pathname); " + vbCrLf)
        Response.Write("var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf(""/"") + 1);" + vbCrLf)
        'Response.Write("strActionPage = CurrentPath + ""GuardarScanBD.aspx?Lime=" + Session("FOLIO").ToString + "&Lime2=" + Session("DOCUMENTOID").ToString + "&Lime3=" + Session("USERID").ToString + "&Lime4=" + Session("Sesion").ToString + "&Lime5=" + Session("ESTATUS_EXPEDIENTE").ToString + "&Lime6=" + Session("CLAVE_DOCUMENTO").ToString + "&Lime7=" + txt_fechadoc.Text + """;")
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
        #DynamicWebTwain1 {
            /*width: 251px;*/
            width: 300px;
            height: 400px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>


    <%--  <asp:UpdatePanel ID="UpdatePanelGPersona" runat="server">
            <ContentTemplate> --%>
    <section class="panel">
        <header class="panel-heading">
            <span>Digitalización</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec columned two_columns align_items_flex_start">
                <div class="module_subsec_elements vertical">
                    <asp:Label ID="lbl_estatus" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_TamMax" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_sec low_m">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <span class="title_tag">1.- Elegir documento por digitalizar:</span>
                        </div>
                    </div>

                    <div class="module_subsec no_column">
                        <div class="module_subsec_elements w_100 vertical">
                            <asp:ListBox ID="lst_DocNoDigi" runat="server" Width="100%" Height="90px"></asp:ListBox>
                        </div>
                    </div>

                    <div class="module_subsec no_column ">
                        <div class="module_subsec_elements">

                            <asp:Button ID="btn_Digitalizar" CssClass="btn btn-primary" runat="server" Text="Digitalizar" />

                        </div>
                        <div class="module_subsec_elements" style="max-width: 300px;">
                            <asp:DropDownList ID="lst_DocumentosEspecificos" runat="server" class="btn btn-primary2 dropdown_label w_100" Visible="false" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="module_subsec_elements">

                            <asp:Button ID="btn_ElegirDocumento" CssClass="btn btn-primary" runat="server" Text="Escanear" />

                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_AlertaDigitaliza" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_subsec no_column ">
                        <div class="text_input_nice_div w_100 module_subsec_elements text_input_nice_div">
                            <asp:TextBox ID="txt_fechadoc" class="text_input_nice_input" runat="server" MaxLength="10" Enabled="false"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Fecha expedición (DD/MM/AAAA):</span>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechadoc" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechadoc" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender_fechadoc" ControlToValidate="txt_fechadoc" CssClass="textogris"
                                    ErrorMessage="MaskedEditExtender_fechadoc" InvalidValueMessage="Fecha inválida" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txt_fechadoc" />
                                <asp:RequiredFieldValidator runat="server" ID="req_fechadoc" CssClass="alertaValidator bold"
                                    ControlToValidate="txt_fechadoc" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_FechaDoc" />
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_FechaExp" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_sec ">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <span>2.- Eligir si desea subir el archivo:</span>
                        </div>
                    </div>

                    <div class="module_subsec no_column ">
                        <div class="module_subsec_elements">
                            <asp:FileUpload ID="FileUpload1" runat="server" Enabled="false" />
                        </div>
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Subir" CssClass="btn btn-primary" runat="server" Text="Subir" Width="100%" />
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_UploadEstatus" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_sec low_m">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <span class="title_tag">3.- Documentos digitalizados:</span>
                        </div>
                    </div>

                    <div class="module_subsec no_column">
                        <div class="module_subsec_elements w_100 vertical">
                            <asp:ListBox ID="lst_DocDigi" runat="server" Width="100%" Height="90px"></asp:ListBox>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_AlertaNoBorrar" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_AlertaVerBorrar" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_subsec no_column low_m">
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Ver" CssClass="btn btn-primary" runat="server" Text="Ver" />
                        </div>
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Eliminar" CssClass="btn btn-primary" runat="server" Text="Eliminar" />
                        </div>
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Insertar_ColaValidacion" CssClass="btn btn-primary2" runat="server" Text="Enviar a Validación" />
                        </div>
                    </div>


                </div>

                <div class="module_subsec_elements vertical align_items_flex_center">

                    <object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331" viewastext>
                        <param name="LPKPath" value="/DynamicWebTwain.lpk" />
                    </object>

                    <div class="module_subsec">
                        <span>2.- Elegir si desea escanear el documento:</span>
                    </div>

                    <div class="module_subsec">
                        <span>Vista Previa</span>
                    </div>

                    <div class="module_subsec" style="box-shadow: 0px 0px 1px #888;">
                        <object classid="clsid:E7DA7F8D-27AB-4EE9-8FC0-3FEC9ECFE758" border="0" id="DynamicWebTwain1" codebase="DynamicWebTWAIN.cab#version=5,1">
                            <!-- PARAMETROS DE DIGITALIZADOR -->
                            <param name="_cx" value="5292">
                            <param name="_cy" value="9260">
                            <param name="JpgQuality" value="80">
                            <param name="Manufacturer" value="DynamSoft Corporation">
                            <param name="ProductFamily" value="Dynamic Web TWAIN">
                            <param name="ProductName" value="Dynamic Web TWAIN">
                            <param name="VersionInfo" value="Dynamic Web TWAIN 5.1">
                            <param name="TransferMode" value="0">
                            <param name="BorderStyle" value="0">
                            <param name="FTPUserName" value="">
                            <param name="FTPPassword" value="">
                            <param name="FTPPort" value="21">
                            <param name="HTTPUserName" value="">
                            <param name="HTTPPassword" value="">
                            <param name="HTTPPort" value="80">
                            <param name="ProxyServer" value="">
                            <param name="IfDisableSourceAfterAcquire" value="0">
                            <param name="IfShowUI" value="-1">
                            <param name="IfModalUI" value="-1">
                            <param name="IfTiffMultiPage" value="0">
                            <param name="IfThrowException" value="0">
                            <param name="MaxImagesInBuffer" value="300">
                            <param name="TIFFCompressionType" value="4">
                            <param name="IfFitWindow" value="-1">
                            <param name="IfSSL" value="0">
                        </object>
                    </div>

                    <div class="module_subsec no_column low_m">
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Guardar" CssClass="btn btn-primary" runat="server" Text="Guardar" Enabled="false" />
                        </div>
                        <div class="module_subsec_elements">
                            <asp:Button ID="btn_Borrar" CssClass="btn btn-primary" runat="server" Text="Borrar" Enabled="false" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </section>

    <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Subir" />
               
             </Triggers>
         </asp:UpdatePanel>   --%>
</asp:Content>


