<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_PAGARES_ADM.aspx.vb" Inherits="SNTE5.CRED_CNF_PAGARES_ADM" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="panel">

        <header class="panel-heading">
            <span>Administración de lotes de pagarés</span>
        </header>

        <div class="panel-body">

            <div class="module_subsec low_m columned three_columns">
                <div class=" module_subsec_elements vertical">
                    <div>
                        <asp:Label runat="server" Text="Sucursal destino:" CssClass=" text_input_nice_label" ID="lbl_SucDest"></asp:Label>
                        <asp:RequiredFieldValidator ID="req_SucDest" runat="server" ControlToValidate="cmb_SucDest"
                            ValidationGroup="val_Lote" Text="Falta Dato!" CssClass="textogris"
                            InitialValue="0" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:DropDownList runat="server" AutoPostBack="True" ID="cmb_SucDest" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                </div>

                <div class="module_subsec_elements text_input_nice_div">
                    <div style="display: flex">
                        <asp:Label runat="server" CssClass="texto" ID="lbl_CSGFolioIni"></asp:Label>
                        <asp:TextBox runat="server" ID="txt_FolioIni" class="text_input_nice_input" Style="flex: 1" MaxLength="14" ValidationGroup="val_Lote"></asp:TextBox>
                    </div>
                    <div class="text_input_nice_labels">
                        <asp:Label runat="server" Text="Folio inicial:" CssClass=" text_input_nice_label" ID="lbl_FolioIni"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_FolioIni" runat="server"
                            ControlToValidate="txt_FolioIni" CssClass="textogris"
                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Lote">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FolioIni" runat="server"
                            TargetControlID="txt_FolioIni" FilterType="Numbers" Enabled="True" />
                    </div>
                </div>
                <div class="module_subsec_elements text_input_nice_div">
                        <div style="display: flex">
                            <asp:Label runat="server" CssClass="texto" ID="lbl_CSGFolioFin"></asp:Label>
                            <asp:TextBox runat="server" ID="txt_FolioFin" class="text_input_nice_input" Style="flex: 1" MaxLength="14"></asp:TextBox>
                        </div>
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" Text="Folio final:" CssClass="text_input_nice_label" ID="lbl_FolioFin" ValidationGroup="val_Lote"></asp:Label>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FolioFin" runat="server"
                                ControlToValidate="txt_FolioFin" CssClass="textogris"
                                Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Lote"> </asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_FolioFin" runat="server"
                                TargetControlID="txt_FolioFin" FilterType="Numbers" Enabled="True" />
                        </div>

                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label runat="server" CssClass="alerta" ID="lbl_Alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_EnviaLote" runat="server" class="btn btn-primary" Text="Enviar" ValidationGroup="val_Lote" />
            </div>

            

            <h4 class="module_subsec"><asp:Label runat="server" Text="Lotes por entregar" CssClass="" ID="lbl_LotesPend"></asp:Label></h4>
                        
            <div class="x-overflow module_subsec" style="-moz-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);-webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);">
                <asp:DataGrid ID="dag_LotesPend" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" TabIndex="17">

                    <Columns>
                        <asp:BoundColumn DataField="idlote" HeaderText="Id lote">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="folioini" HeaderText="Folio inicial">
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="foliofin" HeaderText="Folio final">
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="sucdest" HeaderText="Sucursal destino">
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="fechaenvio" HeaderText="Fecha envío">
                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                </asp:DataGrid> 
            </div>

            <h4 class="module_subsec">
                <asp:Label runat="server" Text="Lotes entregados" CssClass="" ID="lbl_LotesEntrega"></asp:Label></h4>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="module_subsec low_m columned three_columns">

                        <div class="module_subsec_elements text_input_nice_div">

                            <asp:TextBox ID="txt_fechaA" class="text_input_nice_input" runat="server"></asp:TextBox>
                            <div class="text_input_nice_labels">
                                
                                <asp:Label ID="lbl_fechaA" runat="server" CssClass="texto" Text="Fecha mínima:"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator_FechaIni" runat="server"
                                            ControlToValidate="txt_fechaA" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_FiltroPPagare"></asp:RequiredFieldValidator>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaA"
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaA">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaA" runat="server"
                                    ControlExtender="MaskedEditExtender_fechaA" ControlToValidate="txt_fechaA" CssClass="textogris"
                                    ErrorMessage="MaskedEditValidator_fechaA" InvalidValueMessage="Fecha invalida">
                                </ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaA" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" OnClientShown="onCalendarShown" TargetControlID="txt_fechaA">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:TextBox ID="txt_fechaB" class="text_input_nice_input" runat="server"></asp:TextBox>
                             
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_fechaB" runat="server" CssClass="texto" Text="Fecha máxima:"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FechaMax" runat="server"
                                            ControlToValidate="txt_fechaB" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_FiltroPPagare"></asp:RequiredFieldValidator>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender_fechaB"
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaB">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaB" runat="server"
                                    ControlExtender="MaskedEditExtender_fechaB" ControlToValidate="txt_fechaB" CssClass="textogris"
                                    ErrorMessage="MaskedEditValidator_fechaB" InvalidValueMessage="Fecha invalida">
                                </ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender_fechaB" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" OnClientShown="onCalendarShown" TargetControlID="txt_fechaB">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="module_subsec_elements text_input_nice_div">
                            <asp:DropDownList ID="cmb_sucursal" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_sucursal" runat="server" class="texto" CssClass="texto" Text="Oficina:"></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator_sucursal" runat="server" ControlToValidate="cmb_sucursal"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                            ValidationGroup="val_FiltroPPagare"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                    <div class="module_subsec no_m no_column flex_center">
                        <asp:Label runat="server" CssClass="alerta" ID="lbl_AlertaFiltro"></asp:Label>
                    </div>

                    <div class="module_subsec no_column flex_end" style="margin-bottom: 10px;">
                        <asp:Button ID="btn_aceptar1" runat="server" class="btn btn-primary module_subsec_elements" Text="Aplicar" ValidationGroup="val_FiltroPPagare" />

                        <asp:Button ID="btn_cancelar1" runat="server" class="btn btn-primary module_subsec_elements" Text="Eliminar" />
                    </div>



                    <div class="x-overflow module_subsec" style="-moz-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2); -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2); box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);">
                        <asp:DataGrid ID="dag_LotesRecibidos" runat="server" class="table table-striped"
                            AutoGenerateColumns="False" CellPadding="4" GridLines="None">
                            <Columns>
                                <asp:BoundColumn DataField="idlote" HeaderText="Id lote">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folioini" HeaderText="Folio inicial">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="foliofin" HeaderText="Folio final">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="sucdest" HeaderText="Sucursal destino">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="userdest" HeaderText="Usuario recepción">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="userorig" HeaderText="Usuario envío">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechaenvio" HeaderText="Fecha envío">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechaentrega" HeaderText="Fecha entrega">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="table_header"></HeaderStyle>
                        </asp:DataGrid>

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_aceptar1" />
                    <asp:AsyncPostBackTrigger ControlID="btn_cancelar1" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </section>
       
    <script type="text/javascript">
        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>
</asp:Content>
