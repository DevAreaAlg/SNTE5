<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_CNF_OFICINAS.aspx.vb" Inherits="SNTE5.PEN_CNF_OFICINAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            
     <section class="panel" >
       <div class="panel-body">
            <div class="module_subsec no_m">
                <div class="module_subsec columned low_m no_rm five_columns" style="flex:1;">
                    <div class=" module_subsec_elements no_m">
                           <div class="text_input_nice_div module_subsec_elements_content">
                               <asp:TextBox ID="txt_nombre" class="text_input_nice_input" runat="server" MaxLength="30"></asp:TextBox>
                               <span class="text_input_nice_label">Nombre: </span>
                               <ajaxtoolkit:filteredtextboxextender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                  FilterType ="Custom, LowercaseLetters, UppercaseLetters" ValidChars="' '" TargetControlID="txt_nombre">
                               </ajaxtoolkit:filteredtextboxextender>
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
                       <asp:Button ID="btn_busca_oficina" CssClass="btn btn-primary" OnClick="btn_busca_oficina_Click" runat="server" Text="Buscar" />                                                  
                    </div>
                </div>            
         
       </div>                             
     </section> 


     <div class="panel" style="background-color:transparent;box-shadow:none;">
        <div class="flex_end">
            <asp:Button ID="btn_Agregar_Politica" runat="server" class="btn btn-primary" Text="Nueva Oficina" />
        </div>
    </div>

     <section class="panel" > 
         <div class="overflow_x shadow" >
      <!-- Tabla de OFICINAS con coincidencias encontradas-->
      <asp:DataGrid ID="dag_oficinas" runat="server" AutoGenerateColumns="False"  class="table table-striped"
                   GridLines="None" >
        <Columns>
          <asp:BoundColumn  DataField="ID" HeaderText="ID">
                            <%--<ItemStyle Width="50px"></ItemStyle>--%>
          </asp:BoundColumn>
          <asp:BoundColumn  DataField="ABREVIATURA" HeaderText="Abreviatura">
                            <%--<ItemStyle Width="225px"></ItemStyle>--%>
          </asp:BoundColumn>
          <asp:BoundColumn  DataField="NOMBRE" HeaderText="Nombre">
                            <%--<ItemStyle Width="125px"></ItemStyle>--%>
          </asp:BoundColumn>
          <asp:BoundColumn  DataField="ESTATUS" HeaderText="Estatus">
                            <%--<ItemStyle Width="125px"></ItemStyle>--%>
          </asp:BoundColumn>
          <asp:ButtonColumn CommandName="EDITAR"  Text="Editar">
            <ItemStyle  Width="10%" />
          </asp:ButtonColumn>                        
        </Columns>
        <HeaderStyle CssClass="table_header">
        </HeaderStyle>
      </asp:DataGrid>   
             </div>
    </section>
 
</asp:Content>

