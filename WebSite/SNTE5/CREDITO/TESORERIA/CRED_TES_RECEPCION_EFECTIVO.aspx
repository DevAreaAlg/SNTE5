<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_TES_RECEPCION_EFECTIVO.aspx.vb" Inherits="SNTE5.CRED_TES_RECEPCION_EFECTIVO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    

    <section class="panel">
            <header class="panel-heading">
                <span>Recepción de Cheques/Efectivo</span>
            </header>
            <div class="panel-body">
              
                <br />
                <div class="module_subsec overflow_x shadow">
                <asp:DataGrid ID="grd_operaciones" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped" GridLines="None">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_OPERACION" HeaderText="Id. operación">
                            
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ORIGEN" HeaderText="Origen">
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESTINO" HeaderText="Destino">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ENVIA" HeaderText="Envía">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TRANSPORTA" HeaderText="Transporta">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RECIBE" HeaderText="Recibe">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="DETALLE" Text="Detalle">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
                </div>

                <div id="up_container">
                    <asp:UpdatePanel ID="upd_pnl_info_recepcion" runat="server">
                        <ContentTemplate>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_origen" Text="Origen:" />
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_origen_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_destino" Text="Destino:" />
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_destino_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_envia" Text="Envía:" />
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_envia_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_transporta" Text="Transporta:" />
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_transporta_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_recibe" Text="Recibe:" />
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_recibe_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_fecha" Text="Fecha:" Visible="true" />
                                <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_fecha_txt" Text="" />
                            </div>

                            <div class="module_subsec columned low_m align_items_flex_center" >
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_monto" Text="Efectivo:" Visible="true" />
                                 <asp:Label runat="server" class="module_subsec_elements module_subsec_big-elements title_tag" ID="lbl_monto_txt" Text="" />
                            </div>

                            <div class="module_subsec overflow_x shadow">
                                <asp:DataGrid ID="grd_info_recepcion" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped" GridLines="None" Enabled="false">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="FOLIO" HeaderText="Cuenta"> <ItemStyle Width="5%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="BANCO" HeaderText="Nombre"> <ItemStyle Width="15%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUMCUENTA" HeaderText="Núm. Cuenta"> <ItemStyle Width="10%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_CHEQUE" HeaderText="Núm. Cheque"> <ItemStyle Width="10%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MONTO" HeaderText="Monto"> <ItemStyle Width="10%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus"> <ItemStyle Width="10%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FECHA_RECIBIDO" HeaderText="Fecha recibido"> <ItemStyle Width="15%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FECHA_COBRADO" HeaderText="Fecha cobrado"> <ItemStyle Width="15%" /></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPO_COBRO" HeaderText="Tipo cobro"> <ItemStyle Width="10%" /></asp:BoundColumn>
                                    </Columns>                                    
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="grd_operaciones" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <ajaxToolkit:UpdatePanelAnimationExtender ID="ae" runat="server" BehaviorID="animation"
                    TargetControlID="upd_pnl_info_recepcion">
                    <Animations>
                <OnUpdating>
                    <Sequence>
                        <%-- Store the original height of the panel --%>
                        <ScriptAction Script="var b = $find('animation'); b._originalHeight = b._element.offsetHeight;" />
                        <StyleAction Attribute="overflow" Value="hidden" />
                        <%-- Do each of the selected effects --%>
                        <Parallel duration=".25" Fps="30">
                                <FadeOut AnimationTarget="up_container" minimumOpacity=".2" />
                            <%--<Resize Height="0" />--%>
                         <%-- <Color AnimationTarget="up_container" PropertyKey="backgroundColor"
                                    EndValue="#FF0000" StartValue="#40669A" />--%>
                        </Parallel>
                    </Sequence>
                </OnUpdating>
                <OnUpdated>
                    <Sequence>
                        <%-- Do each of the selected effects --%>
                        <Parallel duration=".25" Fps="30">
                                <FadeIn AnimationTarget="up_container" minimumOpacity=".2" />
                                <%-- Get the stored height --%>
                                <%--<Resize HeightScript="$find('animation')._originalHeight" />--%>
                               <%-- <Color AnimationTarget="up_container" PropertyKey="backgroundColor"
                                    StartValue="#FF0000" EndValue="#40669A" />--%>
                        </Parallel>
                        </Sequence>
                </OnUpdated>
                    </Animations>
                </ajaxToolkit:UpdatePanelAnimationExtender>
                <p align="center">
                    <asp:Button ID="btn_digit" runat="server" class="btn btn-primary" Text="Digitalizar Constancia" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_recibir" runat="server" class="btn btn-primary" Text="Recibir"
                        ValidationGroup="val_operacion" />
                    <br />
                    <asp:Label runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                        ForeColor="Red" ID="lbl_status"></asp:Label>
                </p>
            
    </div>
    </section>
    
</asp:Content>
