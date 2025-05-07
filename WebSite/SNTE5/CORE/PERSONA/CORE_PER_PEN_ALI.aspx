<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_PER_PEN_ALI.aspx.vb" Inherits="SNTE5.CORE_PER_PEN_ALI" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Lista Demandados Pension Alimenticia</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="module_subsec no_m">
                    <div class="module_subsec columned low_m no_rm seven_columns" style="flex: 1;">
                        <div class="module_subsec_elements text_input_nice_div flex_1">
                            <asp:TextBox ID="txt_nombre" class="text_input_nice_input" Style='text-transform: uppercase' runat="server" MaxLength="30"></asp:TextBox>
                            <span class="text_input_nice_label">Nombre demandado:</span>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters" TargetControlID="txt_nombre">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <div class="module_subsec_elements text_input_nice_div flex_1">
                            <asp:TextBox ID="txt_RFC" class="text_input_nice_input" Style='text-transform: uppercase' runat="server" MaxLength="30"></asp:TextBox>
                            <span class="text_input_nice_label">RFC demandado:</span>
                        </div>
                    </div>
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_busca_usuario" runat="server" Text="Buscar Agremiado" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_eliminarB" runat="server" Text="Eliminar Filtro" class="btn btn-primary" />
                    </div>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec columned low_m two_columns">
                    <div class="module_subsec_elements"></div>
                    <div class="module_subsec_elements flex_end">
                        <asp:Button runat="server" ID="btn_nueva_pea" CssClass="btn btn-primary align_items_flex_end"
                            Text="Agregar Demandado" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_calcular_pen" enable="false" CssClass="btn btn-primary align_items_flex_end"
                            Text="Calcular Pensiones" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_descargar_pen" CssClass="btn btn-primary align_items_flex_end"
                            Text="Reporte Pensiones" />
                    </div>
                </div>
                <div class="module_subsec overflow_x shadow">
                    <div class="flex_1">
                        <asp:GridView runat="server" ID="gvw_pensiones" AutoGenerateColumns="false" CssClass="table table-striped" GridLines="None"
                            AllowSorting="true" AllowPaging="true" HeaderStyle-HorizontalAlign="Center">
                            <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:TemplateField Visible="false" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ID" Visible="false" Text='<%#Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RFC_DEMANDADO" HeaderText="RFC Demandado" />
                                <asp:BoundField DataField="DEMANDADO" HeaderText="Demandado" />
                                <asp:BoundField DataField="BENEFICIARIO" HeaderText="Beneficiario" />
                                <asp:BoundField DataField="PORCENTAJE" HeaderText="Porcentaje Beneficiario (%)" />
                                <asp:BoundField DataField="FECHA_DE_VENCIMIENTO" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField Visible="false" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="NOMBRE_DOC" Visible="false" Text='<%#Eval("NOMBRE_DOC") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ESTATUS_DOC" HeaderText="¿Digitalización?" />
                                <asp:TemplateField SortExpression="TYPE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbn_ver" CommandName="VER" Text="Ver Expediente" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TYPE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbn_editar" CommandName="EDITAR" Text="Editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField SortExpression="TYPE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbn_eliminar" CommandName="ELIMINAR" Text="Eliminar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                
            </div>
        </div>
        <asp:Panel ID="pnl_modal_confirmar" runat="server" Style="display: none;" Align="Center">
            <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                <section class="panel no_bm " style="display: inline-block">
                    <header runat="server" class="panel-heading ">
                    </header>
                    <div class="panel-body align_items_flex_center">
                        <asp:Label runat="server" CssClass="resalte_azul module_subsec align_center" Text="¡Alerta!" />
                        <asp:Label runat="server" Text="¿Está seguro que desea eliminar la pensión alimenticia?" class="resalte_azul module_subsec" TextMode="MultiLine"/> 
                    
                        <asp:Button ID="btn_confirmar" runat="server" CssClass="btn btn-primary" Text="Aceptar" /> 
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-primary" Text="Cancelar" />
                                
                    </div>
                </section>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="modal_confirmar" runat="server"
            Enabled="True" PopupControlID="pnl_modal_confirmar"
            PopupDragHandleControlID="pnl_modal_confirmar" TargetControlID="hdn_alertas">
        </ajaxToolkit:ModalPopupExtender>
        <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />
    </section>
</asp:Content>
