<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_FACTOR_SH.aspx.vb" Inherits="SNTE5.CORE_OPE_FACTOR_SH" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Factor para Seguro Hipotecario</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="ddl_anios" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Año:" />
                                <asp:RequiredFieldValidator ID="rfv_anio" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="ddl_anios" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_factor" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_factor" CssClass="text_input_nice_input" MaxLength="10" runat="server" Enabled="true" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fte_factor" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txt_factor" ValidChars="." />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Factor de seguro hipotecario:" />
                                <asp:RequiredFieldValidator ID="rfv_factor" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="txt_factor" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_factor" />
                                <asp:RegularExpressionValidator ID="rev_factor"
                                    runat="server" ControlToValidate="txt_factor" CssClass="textogris"
                                    ErrorMessage=" Factor incorrecto" lass="textorojo"
                                    ValidationExpression="^[0-9]{1}\.{1}[0-9]{8}$" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_confirmar" CssClass="text_input_nice_input" MaxLength="10" runat="server" Enabled="true" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fte_confirmar" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txt_confirmar" ValidChars="." />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Confirmar el factor:" />
                                <asp:RequiredFieldValidator ID="rfv_confirmar" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="txt_confirmar" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_factor" />
                                <asp:RegularExpressionValidator ID="rev_confirmar"
                                    runat="server" ControlToValidate="txt_confirmar" CssClass="textogris"
                                    ErrorMessage=" Factor incorrecto" lass="textorojo"
                                    ValidationExpression="^[0-9]{1}\.{1}[0-9]{8}$" Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar" CssClass="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_factor" />
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
                </div>
                <div class="module_subsec overflow_x shadow">
                    <asp:DataGrid ID="dag_bitacora_factores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="ANIO" HeaderText="Año"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FACTOR" HeaderText="Factor de Seguro Hipotecario"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHA" HeaderText="Fecha"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
                <asp:Panel ID="pnl_modal_confirmar" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <%--<span>Se ha generado la siguiente confirmación</span>--%>
                            </header>
                            <div class="panel-body align_items_flex_center">
                                <asp:Label runat="server" CssClass="resalte_azul module_subsec" Text="¿Está seguro de que desea guardar el factor de seguro hipotecario?" />
                                <asp:Button ID="btn_confirmar" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                                <asp:Button ID="btn_canelar" runat="server" CssClass="btn btn-primary" Text="Cancelar" />
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
</asp:Content>
