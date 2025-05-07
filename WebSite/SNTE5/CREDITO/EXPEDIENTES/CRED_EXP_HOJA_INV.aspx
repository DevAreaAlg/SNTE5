<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_HOJA_INV.aspx.vb" Inherits="SNTE5.CRED_EXP_HOJA_INV" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/javascript">
    function PrintDiv() {
        //        var myContentToPrint = document.getElementById(lol);
        ////        var myWindowToPrint = window.open('', '', 'width=1270,height=1654,toolbar=0,scrollbars=yes,status=0,resizable=0,location=0,directories=0,top=1,left=1');
        ////        myWindowToPrint.document.write('<link href="css/estilosmascore.css" type="text/css" rel="stylesheet"  media="print"/>');
        //        myWindowToPrint.document.write(myContentToPrint.innerHTML);
        //        myWindowToPrint.document.body.
        //        myWindowToPrint.document.close();
        //        myWindowToPrint.focus();
        //        myWindowToPrint.print();
        //        myWindowToPrint.close();
        this.print();
    }
</script>
</head>
<body style="width: 804px; height: 1006px;">
    <form id="form1" runat="server">
    <p align="center" style="width: 804px">
        <asp:Button ID="btn_imprimir" runat="server" BackColor="White" BorderColor="#054B66"
            BorderWidth="2px" class="btntextoazul" Height="19px" Text="Imprimir" OnClientClick="PrintDiv()" />
    </p>
    <div style="position: absolute; top: 53px; left: 10px; width: 804px; height: 1006px;">
        <asp:Panel ID="Panel1" runat="server" BorderStyle="None" Width="804px" Height="1006px">
            <!--RESUMEN DE INFORMACION CAPTURADA-->
            <br />
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_sucursall" runat="server" CssClass="texto_pnlinv" Font-Size="Small">Sucursal:</asp:Label>
            <asp:Label ID="lbl_sucursalt" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_clientel" runat="server" CssClass="texto_pnlinv" Font-Size="Small">Afiliado No.:</asp:Label>
            <asp:Label ID="lbl_clientet" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_fechal" runat="server" CssClass="texto_pnlinv" Font-Size="Small">Fecha:</asp:Label>
            <asp:Label ID="lbl_fechat" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_nombrel" runat="server" CssClass="texto_pnlinv" Font-Size="Small">Nombre:</asp:Label>
            <asp:Label ID="lbl_nombret" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_calleynuml" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Calle y No.:"></asp:Label>
            <asp:Label ID="lbl_calleynumt" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
           
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_municipiol" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Municipio:"></asp:Label>
            <asp:Label ID="lbl_municipiot" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_estadol" runat="server" CssClass="texto_pnlinv" Text="Estado:"
                Font-Size="Small"></asp:Label>
            <asp:Label ID="lbl_estador" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_cpl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="CP:"></asp:Label>
            <asp:Label ID="lbl_cpt" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_edocivill" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Estado civil:"></asp:Label>
            <asp:Label ID="lbl_edocivilt" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_tipoviviendal" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Tipo de vivienda:"></asp:Label>
            <asp:Label ID="lbl_tipoviviendat" runat="server" CssClass="texto_pnlinv_negritas"
                Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_firmal" runat="server" CssClass="texto_pnlinv" Font-Size="Small">A quien le firma:</asp:Label>
            <asp:Label ID="lbl_firmat" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_nombreconyugel" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Nombre del cónyuge:"></asp:Label>
            <asp:Label ID="lbl_nombreconyuget" runat="server" CssClass="texto_pnlinv_negritas"
                Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_empresal" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Nombre de la empresa en la que labora:"></asp:Label>
            <asp:Label ID="lbl_empresat" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_puestol" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Puesto que desempeña:"></asp:Label>
            <asp:Label ID="lbl_puestot" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_montol" runat="server" CssClass="texto_pnlinv" Font-Size="Small">Monto solicitado:</asp:Label>
            <asp:Label ID="lbl_montot" runat="server" CssClass="texto_pnlinv_negritas" Font-Size="Small"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_line2" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________________________________________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_line3" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________________________________________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_numdepl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Número dependientes:"></asp:Label>
            <asp:Label ID="lbl_numdept" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="_____"></asp:Label>
            &nbsp;<asp:Label ID="lbl_ocupconl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Ocupación cónyuge:"></asp:Label>
            <asp:Label ID="lbl_ocupcont" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________________</asp:Label>
            &nbsp;
            <asp:Label ID="lbl_tiempocasal" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Tiempo en domicilio:"></asp:Label>
            <asp:Label ID="lbl_tiempocasat" runat="server" CssClass="texto_pnlinv" Font-Size="Small">_______</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_nompersonainfol" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Nombre del entrevistado(a):"></asp:Label>
            <asp:Label ID="lbl_nompersonainfot" runat="server" CssClass="texto_pnlinv" Font-Size="Small">______________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_relacionl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Relación con el investigado:"></asp:Label>
            <asp:Label ID="lbl_relaciont" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________</asp:Label>
            &nbsp;
            <asp:Label ID="lbl_otroingresol" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Otro ingreso:"></asp:Label>
            <asp:Label ID="lbl_otroingresot0" runat="server" CssClass="texto_pnlinv" Font-Size="Small">_____</asp:Label>
            &nbsp;
            <asp:Label ID="lbl_fuenteingresol" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Fuente del ingreso:"></asp:Label>
            <asp:Label ID="lbl_fuenteingresot" runat="server" CssClass="texto_pnlinv" Font-Size="Small">_________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_Servicios" runat="server" CssClass="texto_pnlinv" Text="Gasto servicios:"
                Font-Size="Small"></asp:Label>
            <br />
            <asp:Panel ID="pnl_servicios" runat="server" Height="83px" Width="758px">
            </asp:Panel>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_line5" runat="server" CssClass="texto_pnlinv" Font-Size="Small">____________________________________________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_line6" runat="server" CssClass="texto_pnlinv" Font-Size="Small">____________________________________________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_line7" runat="server" CssClass="texto_pnlinv" Font-Size="Small">____________________________________________________________________________________</asp:Label>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_latitudl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Latitud:"></asp:Label>
            <asp:Label ID="lbl_latitudt" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________________________</asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lbl_longitudl" runat="server" CssClass="texto_pnlinv" Font-Size="Small"
                Text="Longitud:"></asp:Label>
            <asp:Label ID="lbl_longitudt" runat="server" CssClass="texto_pnlinv" Font-Size="Small">___________________________</asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <%-- <%
                Response.Write("<img id=""Mapa"" alt=""ubicacion"" src=""http://maps.google.com/maps/api/staticmap?" + Session("MAPA").ToString + "&sensor=false&format=png"" width=""680"" height=""185"" />")
            %>--%>
        </asp:Panel>
    </div>
    </form>
</body>
</html>


