<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_LISTA_NEGRA.aspx.vb" Inherits="SNTE5.CORE_PER_LISTA_NEGRA" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Lista Negra de Agremiados</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="module_subsec columned low_m three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList runat="server" ID="ddl_estatus_agremiado" 
                                        CssClass="btn btn-primary2 dropdown_label" AutoPostBack="true"/>
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="text_input_nice_label" Text="Estatus:" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements"></div>
                            <div class="module_subsec_elements flex_end">
                                <asp:Button runat="server" ID="btn_nuevo_bl" CssClass="btn btn-primary align_items_flex_end"
                                    Text="Agregar Agremiado" />
                            </div>
                        </div>
                        <div class="module_subsec overflow_x shadow">
                            <div class="flex_1">
                                <asp:DataGrid runat="server" ID="dgd_lista_negra_agr" AutoGenerateColumns="False"
                                    CssClass="table table-striped" GridLines="None">
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_REGISTRO" HeaderText="Id Registro" Visible="false" />
                                        <asp:BoundColumn DataField="ID_PERSONA" HeaderText="Id Persona" Visible="false" />
                                        <asp:BoundColumn DataField="RFC" HeaderText="RFC" />
                                        <asp:BoundColumn DataField="AGREMIADO" HeaderText="Agremiado" />
                                        <asp:BoundColumn DataField="CREADOX" HeaderText="Capturista" />
                                        <asp:BoundColumn DataField="FECHA_CREADO" HeaderText="Fecha Captura" />
                                        <asp:BoundColumn DataField="DESBLOQUEADOX" HeaderText="Desbloqueado" Visible="false" />
                                        <asp:BoundColumn DataField="FECHA_DESBLOQUEO" HeaderText="Fecha Desbloqueo" Visible="false" />
                                        <asp:ButtonColumn CommandName="EDITAR" Text="Editar" />
                                        <asp:BoundColumn DataField="ID_ESTATUS" HeaderText="Id Estatus" Visible="false" />
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_estatus_agremiado" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>
