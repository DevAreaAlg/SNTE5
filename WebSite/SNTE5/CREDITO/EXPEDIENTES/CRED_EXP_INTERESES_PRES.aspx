<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.Master" CodeBehind="CRED_EXP_INTERESES_PRES.aspx.vb" Inherits="SNTE5.CRED_EXP_INTERESES_PRES" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server">
        <section class="panel">
            <header class="panel-heading">
                <asp:Label runat="server" Text="Intereses por Préstamos" />
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="module_subsec columned low_m four_columns">
                        <div class="module_subsec_elements text_input_nice_div">

                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="Ciclo:" />
                               <asp:DropDownList ID="cmb_ciclo" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack ="true">
                                 </asp:DropDownList>
                            </div>
                        </div>
                        </div>
                    <asp:Label runat="server" ID="lbl_estatus_intereses" CssClass="module_subsec flex_center alerta" />
                    <div class="module_subsec overflow_x shadow">
                        <div class="flex_1">
                            <asp:DataGrid runat="server" ID="dgd_intereses_prestamo" AutoGenerateColumns="False"
                                CssClass="table table-striped" GridLines="None">
                                <HeaderStyle CssClass="table_header"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="ANIO" HeaderText="Año"/>
                                    <asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena"/>
                                    <asp:BoundColumn DataField="ID_PRODUCTO" HeaderText="Id Producto" Visible="false"/>
                                    <asp:BoundColumn DataField="PRODUCTO" HeaderText="Intereses por" />
                                    <asp:BoundColumn DataField="INTERESES" HeaderText="Intereses Devengados"/>
                                    <asp:BoundColumn DataField="INTERESES_TRABAJADOR" HeaderText="Intereses Devengados 75%"/>
                                    <asp:BoundColumn DataField="INTERESES_ENTIDAD" HeaderText="Intereses Devengados 25%" />
                                    <asp:BoundColumn DataField="ID_ESTATUS" HeaderText="Id Estatus" Visible="false"/>
                                    <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus" />
                                    <asp:ButtonColumn CommandName="DISPERSAR" Text="Dispersar" />
                                    <asp:ButtonColumn CommandName="REPORTE" Text="Reporte" />
                                    <asp:ButtonColumn CommandName="CONFIRMAR" Text="Confirmar" Visible ="False" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnl_modal_confirmar" Style="display: none;" Align="Center">
                        <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                            <section class="panel no_bm " style="display: inline-block">
                                <header runat="server" class="panel-heading ">
                                    <asp:Label runat="server" Text="Confirmación de Rendimientos por Préstamo" />
                                </header>
                                <div class="panel-body align_items_flex_center">
                                    <asp:Label runat="server" ID="lbl_anio" CssClass="resalte_azul module_subsec" Visible="false" />
                                    <asp:Label runat="server" ID="lbl_quincena" CssClass="resalte_azul module_subsec" Visible="false" />
                                    <asp:Label runat="server" ID="lbl_id_prod" CssClass="resalte_azul module_subsec" Visible="false" />
                                    <asp:Label runat="server" ID="lbl_periodo" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_intereses" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_dispersion" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_fondo" CssClass="resalte_azul module_subsec" />
                                    <asp:Label runat="server" ID="lbl_pregunta" CssClass="resalte_azul module_subsec" />
                                    <asp:Button runat="server" ID="btn_confirmar" CssClass="btn btn-primary" Text="Aceptar" />
                                    <asp:Button runat="server" ID="btn_canelar" CssClass="btn btn-primary" Text="Cancelar" />
                                </div>
                            </section>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:ModalPopupExtender ID="modal_confirmar" runat="server"
                        Enabled="True" PopupControlID="pnl_modal_confirmar"
                        PopupDragHandleControlID="pnl_modal_confirmar" TargetControlID="hdn_alertas">
                    </ajaxToolkit:ModalPopupExtender>
                    <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>
