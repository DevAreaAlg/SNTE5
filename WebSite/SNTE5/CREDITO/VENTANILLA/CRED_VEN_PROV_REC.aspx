<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_PROV_REC.aspx.vb" Inherits="SNTE5.CRED_VEN_PROV_REC" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Proveedor de recursos</title>
    <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
    <link href="/css/bootstrap-theme.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="module_subsec flex_center">
            <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />
        </div>
       
        <div class="module_subsec flex_center">
        <asp:Label ID="lbl_EntradaSalida" runat="server" class="subtitulos" Text="Proveedor de Recursos"></asp:Label>
        </div>

        <div class="module_subsec">
            <asp:DataGrid ID="dag_Proveedor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" Width="100%">
                <HeaderStyle CssClass="table_header"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="IDPROVEEDOR" HeaderText="Núm. afiliado">
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RELACION" HeaderText="Relación">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <div class="module_subsec flex_center">
            <asp:Label ID="Label1" runat="server" class="subtitulos" Text="Propietarios Reales"></asp:Label>
        </div>

        <div class="module_subsec">
            <asp:DataGrid ID="dag_Propietario" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" Width="100%">
                <HeaderStyle CssClass="table_header"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="IDPROPIETARIO" HeaderText="Núm. afiliado">
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RELACION" HeaderText="Relación">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PORCENTAJE" HeaderText="Porcentaje">
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                    </asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <div class="module_subsec flex_center">
            <asp:Label ID="lbl_Info" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label>
        </div>            
   
    </form>
</body>
</html>
