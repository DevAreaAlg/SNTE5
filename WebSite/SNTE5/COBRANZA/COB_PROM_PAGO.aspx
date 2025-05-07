<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_PROM_PAGO.aspx.vb" Inherits="SNTE5.COB_PROM_PAGO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&tipo=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px");
            if (wbusf != null) {

                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

   
        
             <section class="panel">
                <header class="panel-heading">
                    <span>Afiliado</span>
                </header>
           <div class="panel-body">
             <div class="module_subsec low_m  columned three_columns">
                <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">                        
                        <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_numcliente">Número de afiliado: </asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txt_cliente">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Enabled="true"
                                CssClass="textogris" ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_cliente" />
                        </div>
                    </div>
                    <asp:ImageButton runat="server" ImageUrl="~/img/glass.png" ID="btn_buscapersona"
                        Style="height: 18px; width: 18px;" ValidationGroup="val_cliente"></asp:ImageButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul"
                        Text="Seleccionar" ValidationGroup="val_cliente" />
                    
                </div>
                
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_folio" class="btn btn-primary2 dropdown_label"
                            Enabled="False">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" class="text_input_nice_label" ID="lbl_folio">Número de folio: </asp:Label>
                        </div>
                    </div>
                </div>
            
              </div>
            </div>
             <div class="module_subsec flex_center">
                         <asp:Label ID="lbl_infoc" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                     </div> 
           </section>   
           
      
       

   
             
                   
                           <div class="panel-body_content init_show" runat="server" id="Div2" >
                           
               <asp:Panel ID="pnl_amortizaciones" runat="server" Visible="false">
                    <section class="panel" id="Section1">
                                <header class="panel-heading" id="header_amort">
                                    <span>Amortizaciones</span>
                                </header>
                           <div class="panel-body">
                         <div class="module_subsec low_m  columned three_columns">
                         <div class="module_subsec_elements align_items_flex_end">
                            <div class="text_input_nice_div module_subsec_elements_content"> 
                                 <asp:Textbox ID="lbl_prom" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                  <div class="text_input_nice_labels">
                                     <asp:Label ID="lbl_prom_pago" runat="server" CssClass="text_input_nice_label" Text="Fecha de promesa de pago:: "></asp:Label>
                                  </div>
                             </div>
                            </div>

                         <div class="module_subsec_elements align_items_flex_end">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_estatus_prom" runat="server" class="btn btn-primary2 dropdown_label" AUTOPOSTBACK="true"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_evento" runat="server" CssClass="text_input_nice_label">*Estatus promesa:</asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmb_estatus_prom"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_aviso"  InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>                      
                                    </div>
                                </div> 

                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content"> 
                                     <asp:Textbox ID="lbl_user_estatus" runat="server" Enabled="false" CssClass="text_input_nice_input"></asp:Textbox>
                                     <div class="text_input_nice_labels">
                                       <asp:Label ID="lbl_tit_user_estatus" runat="server" CssClass="text_input_nice_label" Text="Realizó: "></asp:Label>
                                    </div>
                             </div>
                            </div>
                         
                                                    
                         </div>
                        
                          <div class= "module_subsec low_m columned three_columns flex_start">
                           <div class="module_subsec_elements" style="flex:1;">
                        <div class="module_subsec_elements_content vertical">
                             <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_detacc" runat="server" CssClass="text_input_nice_label" Text="*Descripcion pago:"></asp:Label>
                                <asp:RequiredFieldValidator ID="req_valor" runat="server" ControlToValidate="txt_notas_aviso"
                                  CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_aviso">
                                  </asp:RequiredFieldValidator>
                              </div>
                              <asp:TextBox ID="txt_notas_aviso" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"
                                   onKeyUp="javascript:Check(this, 3000);" onChange="javascript:Check(this, 2000);"></asp:TextBox>
                              </div>
                           </div>
                              </div>
                       <div class="module_subsec overflow_x shadow">
                    <asp:GridView ID="dag_Ini" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                        <Columns>
                            <asp:BoundField  DataField="IDTRANS" HeaderText="Id">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField  DataField="CADENA" HeaderText="Cadena">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>                                                     
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_PagAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
                </div>

                        <div class="module_subsec_elements align_items_flex_end">
                    <div class="text_input_nice_div module_subsec_elements_content"> 
                    </div>
                      &nbsp;&nbsp;
                </div>
                     <div class="module_subsec flex_center">
                         <asp:Label ID="lbl_info" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                     </div> 

                        <div class="module_subsec flex_end">
                       <asp:Button ID="lnk_guardar_aviso" runat="server" CssClass="btn btn-primary" ValidationGroup="val_aviso" Text="Guardar"/>&nbsp;&nbsp;
                       <asp:Button ID="lnk_guardar_cancelar" runat="server" CssClass="btn btn-primary" Text="Cancelar"/>
                         </div>  
                       </div>
                          </section>
                     </asp:Panel>   
                     
                    </div>
                 
               
        
 
 
       
             <asp:Panel ID="pnl_promesas" runat="server" Visible="false"> 
                    <section class="panel">
                    <header class="panel-heading">
                        <span>Promesas</span>
                    </header>
                         <div class="panel-body">
                       <div class="module_subsec overflow_x shadow">
                                    <asp:DataGrid ID="dag_promesa" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="table_header" />
                                        <Columns>
                                                <asp:BoundColumn ItemStyle-Width="10%" DataField="IDPROM" HeaderText="Id promesa"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="10%" DataField="IDLOG" HeaderText="Id log"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="15%" DataField="FECHA" HeaderText="Fecha promesa"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="20%" DataField="MONTO" HeaderText="Monto promesa"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="20%" DataField="EVENTO" HeaderText="Evento"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="20%" DataField="ESTATUS" HeaderText="Estatus"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="10%" DataField="IDTRANS" HeaderText="Id trans" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn ItemStyle-Width="10%" DataField="SERIE" HeaderText="Serie-folio"></asp:BoundColumn>
                                               <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                                        <ItemStyle Width="10%" />
                                        </asp:ButtonColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                            <ItemStyle Width="10%" />
                                         </asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                        <div class="module_subsec flex_center">
                              <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                            </div> 
                              </div>
                  </section>
                </asp:Panel>
           
      
      
                                      
         
  </asp:Content>          
