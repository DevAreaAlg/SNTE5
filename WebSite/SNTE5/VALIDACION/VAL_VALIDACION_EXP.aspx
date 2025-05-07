<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="VAL_VALIDACION_EXP.aspx.vb" Inherits="SNTE5.VAL_VALIDACION_EXP" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
         // Javascript para utilizar la Busqueda de un Cliente o Persona
function ResumenPersona() {
             window.open("ResumenPersona.aspx", "RP", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
         }
         function ResumenPersonaM() {
             window.open("ResumenPersonaM.aspx", "RPM", "width=1000,height=650,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="panel">
        <header class="panel-heading">
             <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
                <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>
    <asp:UpdatePanel runat="server" id="up1">
        <ContentTemplate>
    <input type="hidden" name="hdn_ResumenPersona" id="hdn_ResumenPersona" value="" runat="server" />  
    <input type="hidden" name="hdn_notas" id="hdn_notas" runat="server" /> 
    
            <section class="panel" id="panel_datos">
                <header class="panel-heading">
                    <span>Documentación digitalizada</span>
                </header>
                <div class="panel-body">
                    <div class="module_subsec columned four_columns">
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                <div class="text_input_nice_div module_subsec_elements vertical">
                                    <asp:DropDownList ID="cmb_personas" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="true" Visible="false" Style="text-align: center"></asp:DropDownList>
                                    <asp:LinkButton ID="lnk_notas" runat="server" class="textogris" Text="Notas expediente " ToolTip = "Dé Click si desea ver las notas del expediente." /> 
                                    <asp:Label ID="lbl_notas" runat="server" class="textogris">  </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                    <asp:LinkButton runat="server" Text="Consultar" ID="lnk_ResumenPersona"  Visible="false"  class="textogris"></asp:LinkButton>
                                    <asp:Label ID="lbl_ResumenPersonaVer" runat="server" class="textogris"></asp:Label>
                                    
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                    
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                    <asp:LinkButton ID="lnk_busquedagtia" runat="server" class="textogris" Text="Búsqueda Garantías expediente " ToolTip = "Dé Click si desea hacer una búsqueda de garantías asignadas al expediente." Visible="false"/>
                                    <asp:Label ID="lbl_GTIAS" runat="server" class="textogris" Visible="false"> </asp:Label>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnl_notas" runat="server" Align="Center" CssClass="modalPopup"
                    Width="356px" Style='display: none;'>
                    <asp:Panel ID="pnl_Titulo" runat="server" Align="Center" CssClass="titulosmodal">
                        <asp:Label ID="lbl_tit" runat="server" class="subtitulosmodal" ForeColor="White" Text="NOTAS" />
                    </asp:Panel>
                        <br />
                        <asp:Panel id="pnl_texto" widht="350px">
                            <asp:Textbox ID="lbl_notasexp" runat="server" class="textocajas" MaxLength="5000" Width="300px" HEIGHT="250" TextMode="MultiLine"></asp:TextBox> 
                        </asp:Panel>
                        <p align="center">
                            <br />
                            <asp:Button ID="btn_cancel" runat="server" class="btn btn-primary" Style="margin-bottom: 10px"  Text="Cerrar"/>
                            <br />
                        </p>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" PopupControlID="pnl_notas" 
                        PopupDragHandleControlID="pnl_Titulo" TargetControlID="hdn_notas" DynamicServicePath="">
                    </ajaxToolkit:ModalPopupExtender>   

                    <div align="center">
                        <asp:Label ID="Label1" runat="server" Text="" Style="color: red;"></asp:Label>
                    </div>

                    <div class="module_sec">
                        <div class="module_subsec_elements w_100 vertical">
                            <asp:ListBox ID="lst_DocumentosXValidar" runat="server" Width="100%" Height="90px"></asp:ListBox>
                        </div>
                    </div>

                    <div class="module_subsec_elements w_100 vertical">
                        <div class="module_subsec columned three_columns low_m">
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content verticaL flex_center">
                                    <asp:Button ID="btn_ver" class="btn btn-primary" runat="server" Text="Ver Documento" />
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content vertical flex_center">
                                    <asp:Label ID="lbl_concepto" runat="server" Text="Elija un estatus:" class="textogris"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_Estatus" class="btn btn-primary2 dropdown_label" Enabled="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="module_subsec_elements_content flex_center">
                                    <asp:Button ID="btn_Guardar" class="btn btn-primary" runat="server" Text="Guardar" Enabled="false"/>
                                </div>
                            </div>
                        </div> 
                    
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_AlertaVer" runat="server" class="alerta"></asp:Label>
                        </div>
                    </div>


                    <div class="module_sec">
                        <div class="module_subsec_elements w_100 vertical">
                            <div class="overflow_x" >
                                <asp:DataGrid ID="DAG_cola"  runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped"   
                                Font-Size="10pt" TabIndex ="17">
                                <HeaderStyle Cssclass="table_header"/>
                                    <Columns>
                                        <asp:BoundColumn ItemStyle-Width="10%" DataField="Id" HeaderText="Id documento"></asp:BoundColumn>
                                        <asp:BoundColumn ItemStyle-Width="60%" DataField="Documento" HeaderText="Documento"></asp:BoundColumn>
                                        <asp:BoundColumn  ItemStyle-Width="20%" DataField="Estatus" HeaderText="Estatus"></asp:BoundColumn>
                                        <asp:ButtonColumn ItemStyle-Width="10%"  CommandName="ELIMINAR" Text="Eliminar">
                                        </asp:ButtonColumn>
                                    </Columns>
                          
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>

                        <div class="module_subsec columned two_columns">
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                <div class="module_subsec_elements flex_end">
                                    <asp:Button ID="btn_DocumentosAprovados" class="btn btn-primary" runat="server" Text="Aprobar Documentos" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content">
                                <div class="module_subsec_elements flex_center">
                                    <asp:Button ID="btn_Enviar" class="btn btn-primary" runat="server" Text="Rechazar Documentos" />
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


