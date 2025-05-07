<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PERSONA_MULTIPLE.aspx.vb" Inherits="SNTE5.CORE_PER_PERSONA_MULTIPLE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
            <section class="panel">
                <header class="panel-heading">
                    <span>Carga Archivo</span>
                </header>
              <div class="panel-body">
               
                <div class="module_subsec no_column  align_items_flex_center">
                    <span class="module_subsec_elements">Seleccionar archivo de carga:</span>
                    <asp:FileUpload ID="AsyncFileUpload1" CssClass="module_subsec_elements" runat="server" />
                    <span class="module_subsec_elements">Nota: Sólo archivos *.csv</span>
                </div>

                <div class="module_subsec flex_center">
                    <asp:Button ID="btn_CargarArch" runat="server" class="btn btn-primary" Text="Cargar" ValidationGroup="val_CargarAmort" />
                </div>

           
                    <div class="overflow_x shadow module_subsec">                
                        <asp:DataGrid ID="dag_Persona_Ex" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                             GridLines="None" Width="100%" Visible="true">
                            <Columns>
                                <asp:BoundColumn DataField="FILA" HeaderText="Fila">
                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_PERSONA" HeaderText="Numero Persona">
                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRES" HeaderText="Nombres">
                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="APELLIDOS" HeaderText="Apellidos">
                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                        </asp:DataGrid>
                    </div> 
     
                    <div class="overflow_x shadow module_subsec">
                        <asp:DataGrid ID="Dag_Persona_NoEx" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                            GridLines="None" Width="100%" Visible="true">
                            <Columns>
                                <asp:BoundColumn DataField="FILA" HeaderText="Fila">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRES" HeaderText="Nombre(s)">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="APELLIDOS" HeaderText="Apellidos">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DETALLE" HeaderText="Detalle">
                                    <ItemStyle Width="50%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                        </asp:DataGrid>
                        </div>
                    </div>
                <asp:Label ID="lbl_Status_Carga" runat="server" CssClass="alerta"></asp:Label>
            
            </section>
        
</asp:Content>
