<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PERSONAEXT_BUSQUEDA.aspx.vb" Inherits="SNTE5.CORE_PER_PERSONAEXT_BUSQUEDA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                     
    <section class="panel">          
        <div class="panel-body">
       
            <div class= "module_subsec columned low_m no_rm five_columns" >
            <div class="module_subsec_elements text_input_nice_div flex_1">
                <asp:TextBox ID="txt_id" class="text_input_nice_input" runat="server" MaxLength="6"></asp:TextBox>
                <span class="text_input_nice_label">Número de persona:</span>
                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                FilterType="Numbers" TargetControlID="txt_id">
                </ajaxtoolkit:filteredtextboxextender>
            </div> 

            <div class="module_subsec_elements text_input_nice_div flex_1">
                <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                <span class="text_input_nice_label">Nombre:</span>
                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_nombre">
                </ajaxtoolkit:filteredtextboxextender>
            </div>                           

            <div class="module_subsec_elements text_input_nice_div flex_1">
                <asp:TextBox ID="txt_paterno" class="text_input_nice_input" runat="server" MaxLength="50"></asp:TextBox>
                <span class="text_input_nice_label">Apellido paterno:</span>
                <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_paterno" runat="server" Enabled="True"
                    FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_paterno">
                </ajaxtoolkit:filteredtextboxextender>
            </div>
                                    
            <div class="module_subsec_elements vertical flex_1">                                         
                <span class="text_input_nice_label title_tag">Oficina: </span>                                                           
                <asp:DropDownList ID="cmb_sucursal" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center"></asp:DropDownList>                                  
            </div>
          
            <div class="module_subsec_elements flex_1 module_subsec no_m flex_end" >
                <asp:Button ID="btn_busca_persona" runat="server" Text="Buscar" class="btn btn-primary"/>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_elimina_filtro" runat="server" Text="Eliminar" class="btn btn-primary"/>
            </div>
        </div>
        <div class="module_subsec flex_center">
            <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
        </div>
    </div>                             
</section> 

    <div class="panel" style="background-color:transparent;box-shadow:none;">
        <div class="flex_end">
            <asp:Button ID="btn_nuevo" runat="server" class="btn btn-primary" Text="Nueva Persona" />
        </div>
    </div>
          
    <section class="panel" >                          
    <div class="overflow_x shadow" >           
    <!-- Tabla de Personas con coincidencias encontradas-->
    <asp:DataGrid ID="dag_persona" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
        GridLines="None" Width="100%">
        <HeaderStyle CssClass="table_header" />
        <Columns>
            <asp:BoundColumn ItemStyle-Width="15%" DataField="ID" HeaderText="Núm. afiliado">
            </asp:BoundColumn>

            <asp:BoundColumn ItemStyle-Width="30%" DataField="NOMBRE" HeaderText="Nombre">
            </asp:BoundColumn>

            <asp:BoundColumn ItemStyle-Width="20%" DataField="ESTATUS" HeaderText="Estatus">
            </asp:BoundColumn>

            <asp:BoundColumn ItemStyle-Width="20%" DataField="AVANCE" HeaderText="Avance">
            </asp:BoundColumn>

            <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                        <ItemStyle  Width="15%" />
            </asp:ButtonColumn>                        
        </Columns>
                        
    </asp:DataGrid>
            </div>      
        </section>
                    
</asp:Content>