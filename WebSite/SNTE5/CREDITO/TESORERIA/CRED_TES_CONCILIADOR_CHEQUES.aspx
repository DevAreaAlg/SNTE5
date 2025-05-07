<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_TES_CONCILIADOR_CHEQUES.aspx.vb" Inherits="SNTE5.CRED_TES_CONCILIADOR_CHEQUES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx", wbusf, "center:yes;resizable:no;dialogHeight:550px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_nombrebusqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
</script>

     <section class="panel">
            <header class="panel-heading">
                <span>Conciliación de cheques</span>
            </header>
            <div class="panel-body">              
                   
                <div class="module_subsec columned four_columns ">
                    <div class="module_subsec_elements align_items_flex_end">
                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                             <asp:TextBox ID="txt_filtro_persona" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_filtro_persona" Text="Número de afiliado:" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_filtropersona" runat="server"
                                TargetControlID="txt_filtro_persona" FilterType="Numbers" Enabled="True" />       
                            </div>
                        </div>
                        <asp:LinkButton runat="server" Text="Buscar" CssClass="textogris" ID="lnk_busqueda"></asp:LinkButton>
                    </div>

                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList runat="server" ID="cmb_filtro_sucursal" class="btn btn-primary2 dropdown_label">
                            </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_filtro_sucursal" Text="Sucursal:" />
                            </div>
                        </div>
                    </div>
                
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                             <asp:TextBox ID="txt_filtro_fecha_inicio" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_filtro_fecha_inicio" 
                                    Text="Fecha límite inferior(DD/MM/YYYY):" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaini" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txt_filtro_fecha_inicio" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaini" runat="server" Display="Dynamic"
                                    ControlExtender="MaskedEditExtender_fechaini" ControlToValidate="txt_filtro_fecha_inicio"
                                    InvalidValueMessage="Fecha inválida" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechaini" />
                                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="txt_filtro_fecha_inicio"
                                    Format="dd/MM/yyyy" Enabled="True" /> 
                            </div>
                        </div>
                    </div>

                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                             <asp:TextBox ID="txt_filtro_fecha_fin" runat="server" CssClass="text_input_nice_input" MaxLength="10"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_filtro_fecha_fin"
                                    Text="Fecha límite superior(DD/MM/YYYY):" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechafin" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txt_filtro_fecha_fin" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechafin" runat="server" Display="Dynamic"
                                    ControlExtender="MaskedEditExtender_fechafin" ControlToValidate="txt_filtro_fecha_fin"
                                    InvalidValueMessage="Fecha inválida" CssClass="textogris" ErrorMessage="MaskedEditValidator_fechafin" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_filtro_fecha_fin"
                                    Format="dd/MM/yyyy" Enabled="True" />
                            </div>
                        </div>
                    </div>

                </div>   
                
                <div class="module_subsec columned four_columns ">
                    <div class=" module_subsec_elements">
                         <div class="text_input_nice_div module_subsec_elements_content">  
                            <div class="text_input_nice_labels"> 
                                <asp:CheckBox ID="chk_filtro_tipo_sbc" runat="server" CssClass="texto" Text="   Salvo buen cobro" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chk_filtro_tipo_cob" runat="server" CssClass="texto" Text="En firme" />
                            </div>
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="lbl_filtro_tipo" Text="Tipo de cobro: " />
                            </div>
                        </div>
                    </div>

                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content"> 
                            <div class="text_input_nice_labels">
                            <asp:CheckBox ID="chk_filtro_estatus_cet" runat="server" CssClass="texto" Text="En transito" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chk_filtro_estatus_ces" runat="server" CssClass="texto" Text="En sucursal" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chk_filtro_estatus_cco" runat="server" CssClass="texto" Text="Cobrado" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chk_filtro_estatus_can" runat="server" CssClass="texto" Text="Cancelado"
                                Visible="False" /> 
                            </div>
                            <div class="text_input_nice_labels">
                                 <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" ID="lbl_filtro_estatus" Text="Estatus: " />
                            </div>
                        </div>
                    </div>
                </div>                            
                              
                
                <div class="module_subsec columned low_m align_items_flex_center" >   
                    <asp:Label runat="server" CssClass="texto" ID="Label1" Width="550px" Text="Nota: Para filtrar por fecha, es necesario introducir ambas fechas límite." />
                </div>  

                <p align="center">
                    <asp:LinkButton ID="lnk_filtro" runat="server" class="textogris" Text="Filtrar"></asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="lnk_eliminar_filtro" runat="server" class="textogris" Text="Eliminar filtro"></asp:LinkButton>
                    <br />
                    <asp:Label runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                        ForeColor="Red" ID="lbl_status"></asp:Label>
                </p>
                <div id="up_container">
                   <%-- <asp:UpdatePanel ID="upd_pnl_cheques" runat="server">
                        <ContentTemplate>--%>
                            <div class="module_subsec overflow_x shadow" >  
                                <asp:DataGrid ID="grd_cheques" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                 GridLines="None">
                                 <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Afiliado">
                                        <ItemStyle Width="5%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Número de cuenta">
                                        <ItemStyle Width="15%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ID_CHEQUE" HeaderText="Cheque">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_COBRO" HeaderText="Tipo cobro">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA_RECIBIDO" HeaderText="Fecha recibido">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="CONCILIAR" Text="Conciliar">
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                            </div>
                       <%-- </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnk_filtro" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnk_filtro" EventName="Click" />
                           
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
                
            
    </div>
    </section>
    
</asp:Content>