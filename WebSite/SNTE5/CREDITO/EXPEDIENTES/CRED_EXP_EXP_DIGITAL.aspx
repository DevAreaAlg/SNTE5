<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_EXP_DIGITAL.aspx.vb" Inherits="SNTE5.CRED_EXP_EXP_DIGITAL" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
                <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
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
            <span>Documentos</span>
        </header>

        <div class="panel-body">
                
        <div class="overflow_x shadow module_subsec"> 
            <asp:DataGrid ID="DAG_DocDigit" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" Width="100%">

                <HeaderStyle CssClass="table_header" />
                <Columns>
                    <asp:BoundColumn DataField="clave" HeaderText="Num. doc.">
                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="tipodoc" HeaderText="Tipo de documento">
                        <ItemStyle HorizontalAlign="Center" Width="30%"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="documento" HeaderText="Documento">
                                
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="fechadigit" HeaderText="Fecha digitalizado">
                        <ItemStyle HorizontalAlign="Center" Width="25%"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div> 

        <asp:Panel ID="pnl_doc_gar" runat="server" Visible="False">
            <br />
            <asp:Label ID="lbl_doc_gar" runat="server" class="texto" Text="Garantías"></asp:Label>
            <br />
            <asp:DataGrid  ID="dag_doc_gar" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
            GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
            <HeaderStyle CssClass="table_header" />

                <Columns>
                    <asp:BoundColumn DataField="TIPO_GTIA" HeaderText="" Visible="False">
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CVE_GTIA" HeaderText="" Visible="False">
                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_GTIA_S" HeaderText="Tipo de garantia" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DOCUMENTO" HeaderText="Documento" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle ForeColor="#054B66" Width="40px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </asp:Panel>

        <asp:Panel ID="pnl_dictamenes" runat="server" Visible="False">
            <br />
            <asp:Label ID="lbl_dictamenes_title" runat="server" class="texto" Text="Dictámen de Comité y/o Director de préstamo"></asp:Label>
            <br />
            <br />
            <asp:DataGrid ID="dag_dictamenes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                <HeaderStyle CssClass="table_header" />

                <Columns>
                    <asp:BoundColumn DataField="IDSESION" HeaderText="Número de Sesión">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle ForeColor="#054B66" Width="40px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </asp:Panel>
                
        <asp:Panel ID="pnl_disposicion" runat="server" Visible="false">
            <br />
            <asp:Label ID="lbl_dictamenes_title0" runat="server" class="texto" Text="Disposiciones"></asp:Label>
        <br />
            <br />
            <asp:DataGrid ID="dag_disposiciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                <HeaderStyle CssClass="table_header" />

                <Columns>
                    <asp:BoundColumn DataField="clave" HeaderText="Num. Documento">
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="tipodoc" HeaderText="Tipo de Documento">
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="documento" HeaderText="Documento">
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="iddisp" HeaderText="Id Disposición">
                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="fechadigit" HeaderText="Fecha Digitalizado">
                        <ItemStyle HorizontalAlign="Center" Width="240px" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle ForeColor="#054B66" HorizontalAlign="Center" Width="40px" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
                
               
                
        </asp:Panel>
                
        <asp:Panel ID="pnl_carta" runat="server" Visible="false">
            <br />
            <asp:Label ID="lbl_dictamenes_title1" runat="server" class="texto" Text="Cartas de Préstamo"></asp:Label>
            <br />
            <br />
            <asp:DataGrid ID="DAG_CARTA" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
            GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
            <HeaderStyle CssClass="table_header" />

                <Columns>
                    <asp:BoundColumn DataField="clave" HeaderText="Num. Documento">
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="tipodoc" HeaderText="Tipo de Documento">
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="documento" HeaderText="Documento">
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IDCARTA" HeaderText="Id Carta">
                        <ItemStyle HorizontalAlign="Center" Width="5px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="fechadigit" HeaderText="Fecha Digitalizado">
                        <ItemStyle HorizontalAlign="Center" Width="240px" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle ForeColor="#054B66" HorizontalAlign="Center" Width="40px" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
                
                
                
    </asp:Panel>

        <asp:Panel ID="pnl_transbanc" runat="server" Visible="False">
            <br />
            <asp:Label ID="Label1" runat="server" class="texto" Text="Comprobantes Transferencias Bancarias"></asp:Label>
            <br />
            <br />
            <asp:DataGrid ID="dag_transbanc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                <HeaderStyle CssClass="table_header" />

                <Columns>
                    <asp:BoundColumn DataField="IDTRANS" HeaderText="Número de Transacción">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA" HeaderText="Fecha Digitalizado">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="VER" Text="Ver">
                        <ItemStyle ForeColor="#054B66" Width="40px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </asp:Panel>

        <div align="center">
            <br />
            <!--<asp:Button ID="btn_ver_todo" runat="server" class="btn btn-primary" Text="Ver todos los documentos" />-->
        </div>
                
        <div align="center">
            <asp:Label runat="server" CssClass="alerta" ID="lbl_status"></asp:Label>
        </div>
                
        </div>
    </section>
        
</asp:Content>
