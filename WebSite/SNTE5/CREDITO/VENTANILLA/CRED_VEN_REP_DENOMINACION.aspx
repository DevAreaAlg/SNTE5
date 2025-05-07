<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_REP_DENOMINACION.aspx.vb" Inherits="SNTE5.CRED_VEN_REP_DENOMINACION" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="panel" >
            <div class="panel-body">
                <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" EnablePartialRendering="true" EnableScriptLocalization="true" />
                     
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_RepDenom" runat="server" class="h4">Reportes de Efectivo</asp:Label>
                </div>
        
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_TipoRep" runat="server" class="h5"></asp:Label>
                </div>                     
          
                <div class="module_subsec flex_center overflow_x shadow">  
                    <asp:DataGrid ID="dag_Reportes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        class="table table-striped" GridLines="None" >
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="IDCAJA" HeaderText="" Visible="false">
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CAJA" HeaderText="Caja">
                                <ItemStyle Width="45%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SERIEFOLIO" HeaderText="Serie / Folio">
                                <ItemStyle Width="25%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="REPORTE" Text="Reporte">
                                <ItemStyle Width ="25%" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid> 
                </div>
            </div>
        </section>             
    </form>
</body>
</html>
