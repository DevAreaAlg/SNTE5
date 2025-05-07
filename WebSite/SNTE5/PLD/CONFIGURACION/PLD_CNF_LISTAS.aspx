<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_CNF_LISTAS.aspx.vb" Inherits="SNTE5.PLD_CNF_LISTAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" language="javascript">
         function busquedapersonafisica(tipo) {
             var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
             if (wbusf != null) {

                 __doPostBack('', '');
             } lnk_seleccionar_Click
         }
        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>

        <section class="panel">
            <header class=" panel-heading">
                <span>Consulta de listas</span>
                <span class=" panel_folder_toogle down">&rsaquo;</span>
            </header>
            <div class="panel-body">

                    <div class= "module_subsec low_m columned three_columns">
                         <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_PLD5IdCliente" runat="server" class="text_input_nice_input" MaxLength="8"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_PLD5FiltroIDCliente" runat="server" CssClass="texto" Text="Número de afiliado:"></asp:Label>
 <%--                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_PLD5IdCliente" runat="server"
                                            TargetControlID="txt_PLD5IdCliente" FilterType="Numbers" />--%>
                                </div>
                            </div>
                        </div>
                   
                        <div class="module_subsec_elements"> 
		                  <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                                Style="height: 18px; width: 18px;"></asp:ImageButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul"
                                   Text="Buscar cliente" />     
                        </div>
                  
                    <%--      <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                        <div style="display:flex;align-items:center;"> 
                                Descargar
                            <asp:LinkButton runat="server" style="margin-right:25px;font-size:18px;" ID="btn_PEPExcel" >
                            <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                            </asp:LinkButton>
                        </div>
                    </div>--%>
                         <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                                <div style="display:flex;align-items:center;"> 
                                    <asp:LinkButton postback="true" runat="server" style="margin-right:25px;font-size:18px;" ID="btn_PEPExcel">
                                        Descargar excel
                                        <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                    </div>
                      
                    <div class="module_subsec">
                        <asp:Label ID="lbl_NombrePersonaBusquedTexto" runat="server" CssClass="texto">Nombre de afiliado: </asp:Label>
                       &nbsp;&nbsp;
                        <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" CssClass="texto"></asp:Label>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                         <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_ListaPEPInst" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ListaPEPInst" runat="server" CssClass="texto" Text="Institución: "></asp:Label>
                                </div>
                            </div>
                        </div>
	                    <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" MaxLength="500" ID="txt_ListaPEPNomb" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ListaPEPNomb" runat="server" CssClass="texto" Text="Nombre: "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:TextBox runat="server" MaxLength="500" ID="txt_ListaPEPPat" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                     <asp:Label ID="lbl_ListaPEPPat" runat="server" CssClass="texto" Text="Apellido paterno: "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" MaxLength="500" ID="txt_ListaPEPMat" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ListaPEPMat" runat="server" CssClass="texto" Text="Apellido materno: "></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
	                   

                        <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:TextBox runat="server" MaxLength="1000" ID="txt_ListaPEPCargo" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                     <asp:Label ID="lbl_ListaPEPCargo" runat="server" CssClass="texto" Text="Cargo: "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" MaxLength="1000" ID="txt_ListaPEPUnAdmin" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_ListaPEPUnAdmin" runat="server" CssClass="texto" Text="Unidad administrativa: "></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="module_subsec">
                        <asp:Label ID="lbl_PEPNotaExcel" runat="server" CssClass="texto" Text="NOTA: Si el resultado de la consulta contiene mas de 100 registros, se descargará un archivo .CSV automáticamente."></asp:Label>
                    </div>
              
                    <div class="module_subsec flex_end">
                        <asp:Button ID="lnk_PEPConsular" runat="server" CssClass="btn btn-primary" ValidationGroup="val_bitacora" Text="Buscar"/> &nbsp;&nbsp;
                        <asp:Button ID="lnk_EliminarFiltro" runat="server" Text="Eliminar" CssClass="btn btn-primary"/>
                     </div>   
                    
                                <div class="module_subsec flex_1">
                                    <asp:DataGrid ID="dag_ListaPEP" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped" GridLines="None">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                            <asp:BoundColumn DataField="INSTITUCION" HeaderText="Institución">
                                                <ItemStyle Width="10%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PATERNO" HeaderText="Apellido paterno">
                                                <ItemStyle Width="15%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="MATERNO" HeaderText="Apellido materno">
                                                <ItemStyle Width="15%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                                                <ItemStyle Width="20%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="CARGO" HeaderText="Cargo">
                                                <ItemStyle Width="15%"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="UNIDAD_ADMINISTRATIVA" HeaderText="Unidad">
                                                <ItemStyle Width="10%"/>
                                            </asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                          
                     <div class="module_subsec flex_center">
                    <asp:Label runat="server" ID="lbl_status" CssClass="alerta"></asp:Label>
                </div>
            </div>
        </section>
      
</asp:Content>


