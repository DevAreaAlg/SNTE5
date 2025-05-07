<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_POLITICAS.aspx.vb" Inherits="SNTE5.CORE_CNF_POLITICAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        
        <div class="panel-body">

                <div class="module_subsec no_m">
                    <div class="module_subsec columned low_m no_rm five_columns" style="flex:1;">
                        <div class=" module_subsec_elements no_m"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="30"></asp:TextBox>
                                <span class="text_input_nice_label">Nombre: </span>
                            </div>
                        </div>
                        <div class=" module_subsec_elements no_m">
	                        <div class="module_subsec_elements_content vertical">
                                        <span class="text_input_nice_label title_tag">Estatus: </span>
                                    <asp:DropDownList ID="ddl_estatus" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="False">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                        <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m  no_lm" style="margin-bottom:10px;">
                                <asp:Button ID="btn_busca_politica" runat="server" Text="Buscar" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_eliminarB" runat="server" Text="Eliminar" class="btn btn-primary" />
                    </div>
                </div>
             <div class="module_subsec flex_center">
                <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
            </div>

        </div>
    </section>

    <div class="panel" style="background-color:transparent;box-shadow:none;">
        <div class="flex_end">
            <asp:Button ID="btn_Agregar_Politica" runat="server" class="btn btn-primary" Text="Nueva Política" />
        </div>
    </div>

    <section class="panel">
        <div class="overflow_x shadow">

            <asp:DataGrid ID="dag_Politicas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" TabIndex="17">
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Nombre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                        <ItemStyle Width="15%" />
                    </asp:ButtonColumn>
                </Columns>
                <HeaderStyle CssClass="table_header"></HeaderStyle>
            </asp:DataGrid>
        </div>

    </section>
      
</asp:Content>


