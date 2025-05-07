<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO1.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO1" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <div class="row col-lg-12 " style="align-content: flex-end">
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
                        <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                    </div>
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                        <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="panel" id="panel_datos">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Monto / Plazo / Finalidad</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <%--<asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                    <ContentTemplate>--%>
                <div class="module_subsec low_m columned three_columns top_m">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_monto" CssClass="text_input_nice_input" MaxLength="9" runat="server" Enabled="true"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txt_monto" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_MinReest" runat="server" CssClass="texto"></asp:Label>
                                <asp:Label ID="lbl_NotaReest" runat="server" CssClass="texto"></asp:Label>
                            </div>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Monto del préstamo:</span>
                                <asp:Label ID="lbl_rango" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_monto" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_monto" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto"
                                    runat="server" ControlToValidate="txt_monto" CssClass="textogris"
                                    ErrorMessage=" Error:Monto incorrecto" lass="textorojo"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" ID="txt_plazo" CssClass="text_input_nice_input" MaxLength="9" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_plazo" runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txt_plazo">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Plazo del préstamo:</span>
                                <asp:Label ID="lbl_rango_plazo" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_plazo" runat="server" ControlToValidate="txt_plazo" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                    ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_objetivo" runat="server" CssClass="btn btn-primary2 dropdown_label"
                                Style="text-align: center" />
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Finalidad del préstamo:</span>
                                <asp:RequiredFieldValidator ID="rfv_objetivo" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="cmb_objetivo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_conf" InitialValue="-1" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m columned three_columns">
                    <%--<div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_gasto_notarial" CssClass="text_input_nice_input"
                                MaxLength="9" runat="server" Enabled="false" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftb_gasto_notarial" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txt_gasto_notarial" ValidChars="." />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" ID="lbl_gasgasto_notarial" class="text_input_nice_label">Gasto notarial:</asp:Label>
                                <asp:RequiredFieldValidator ID="rfv_gasto_notarial" CssClass="alertaValidator bold" runat="server"
                                    ControlToValidate="txt_gasto_notarial" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_conf" InitialValue="" Enabled="false" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txt_gasto_notarial" CssClass="textogris"
                                    ErrorMessage=" Error: Monto incorrecto!" lass="textorojo" Display="Dynamic"
                                    ValidationExpression="^[0-9]{1,6}(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="module_subsec no_column">
                    <div class="text_input_nice_div  w_100">
                        <asp:TextBox ID="txt_notas" CssClass="text_input_nice_input" runat="server" TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_notas" runat="server" Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                            TargetControlID="txt_notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Comentarios adicionales:</span>
                        </div>
                    </div>
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_guardar" class="btn btn-primary" runat="server" OnClick="btn_guardar_Click" ValidationGroup="val_conf" Text="Guardar" />
                </div>
                <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_fondeo" hidden="hidden">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Fuentes de fondeo</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanelFondeo" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec columned two_columns align_items_flex_start">

                            <div class="module_subsec_elements vertical">
                                <div class="module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex: 1;">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_fondeo" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"></asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Fuentes de fondeo disponibles:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="alertaValidator bold" runat="server" ControlToValidate="cmb_fondeo" Display="Dynamic"
                                                    ErrorMessage="Falta Dato!" ValidationGroup="val_fondeo" InitialValue="-1"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec low_m columned three_columns flex_start">
                                    <div class="module_subsec_elements" style="flex: 1;">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_porcentaje" CssClass="text_input_nice_input" runat="server" Enabled="true" MaxLength="3"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_porcent" runat="server" Enabled="True" FilterType="Numbers"
                                                TargetControlID="txt_porcentaje">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Porcentaje de fondeo:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_porcent" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_porcentaje" Display="Dynamic" ErrorMessage="Falta Dato!"
                                                    ValidationGroup="val_fondeo"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                        </div>
                                    </div>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" OnClick="btn_asignar_Click" ValidationGroup="val_fondeo" Text="Asignar" />
                                </div>
                            </div>

                            <div class="module_subsec_elements vertical">
                                <div class="module_subsec no_column no_m">
                                    <div class="text_input_nice_div module_subsec_elements w_100  no_tbm">
                                        <asp:ListBox ID="lst_fond" class="text_input_nice_textarea" runat="server" Width="100%" Height="120px"></asp:ListBox>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Fuentes de fondeo asignadas:</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_remover" class="btn btn-primary" OnClick="btn_remover_Click" runat="server" Text="Remover" />
                                </div>
                            </div>

                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusfondeo" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                        <asp:AsyncPostBackTrigger ControlID="btn_remover" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_gracia" hidden="hidden">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Días de gracia de intereses</span>
            <span class=" panel_folder_toogle up">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanelGracia" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec low_m columned three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_diasgracia" CssClass="text_input_nice_input" MaxLength="9" runat="server" Enabled="true"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_diasgracia" runat="server" Enabled="True" FilterType="Numbers"
                                        TargetControlID="txt_diasgracia">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Días de gracia de intereses ordinarios:</span>
                                        <asp:Label ID="lbl_DiasGracia" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_diasgracia" CssClass="alertaValidator bold" runat="server" ControlToValidate="txt_diasgracia" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_gracia"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_diasgraciamora" CssClass="text_input_nice_input" MaxLength="9" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_diasgraciamora" runat="server" Enabled="True" FilterType="Numbers"
                                        TargetControlID="txt_diasgraciamora">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Días de Gracia intereses moratorios:</span>
                                        <asp:Label ID="lbl_DiasGraciaMora" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_diasgraciamora" runat="server" ControlToValidate="txt_diasgraciamora" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_gracia"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardardiasdegracia" class="btn btn-primary" runat="server" ValidationGroup="val_gracia" Text="Guardar" />
                        </div>

                        <div align="center">
                            <asp:Label ID="lbl_statusgracia" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardardiasdegracia" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <asp:Panel ID="pnl_Modal_Confirmar" runat="server" Style="display: none;" Align="Center">
                    <div style="position: fixed; top: 0; left: 0; display: flex; width: 100%; height: 100%; background-color: rgba(0,0,0,.6); justify-content: center; align-items: center;">
                        <section class="panel no_bm " style="display: inline-block">
                            <header runat="server" class="panel-heading ">
                                <%--<span>Se ha generado la siguiente confirmación</span>--%>
                            </header>
                            <div class="panel-body align_items_flex_center">
                                <asp:Label ID="lbl_alerta" runat="server" class="resalte_azul module_subsec" />
                                <asp:Label ID="lbl_mensaje" runat="server" class="resalte_azul module_subsec align_items_flex_center"  />
                                <asp:Button ID="btn_confirmar" runat="server" class="btn btn-primary" Text="Aceptar" />
                                <asp:Button ID="btn_cancelar" runat="server" class="btn btn-primary" Text="Cancelar" />
                                
                            </div>
                        </section>

                    </div>
                </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="modal_Confirmar" runat="server"
                    Enabled="True" PopupControlID="pnl_Modal_Confirmar"
                    PopupDragHandleControlID="pnl_Modal_Confirmar" TargetControlID="hdn_alertas">
                </ajaxToolkit:ModalPopupExtender>

    <section class="panel" runat="server" runat="server" id="panel_comisiones">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Comisiones</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_pnl_AsignarCom" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec">
                            <div class="overflow_x shadow flex_1" style="flex: 1;">
                                <asp:GridView ID="dag_comisiones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="100px" DataField="ID" HeaderText="Id fuente"></asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("asignado")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_asigFuentesEstatus" runat="server" Style="max-height: 0; overflow: hidden;" Visible="false" Text="Label" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_c" class="btn btn-primary" runat="server" OnClick="btn_guardar_c_Click" Text="Guardar" />
                        </div>

                        <asp:LinkButton ID="lnk_RegresarTotal" runat="server" CssClass="textogris" Text="Cerrar Comisión" Visible="False"></asp:LinkButton>

                        <asp:DataGrid ID="dag_atributos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%" Visible="False">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="IDCOMISION" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CLAVECOMISION" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMISION" HeaderText="Comisión">
                                    <ItemStyle Width="400px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ATRIBUTOS" HeaderText=" " Text="Atributos">
                                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>

                        <asp:Panel ID="pnl_APE_CRED" runat="server" Visible="False" Width="100%">
                            <asp:DataGrid ID="dag_APE_CRED" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" TabIndex="17" Visible="False">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="TIPOCOBRO" HeaderText="Tipo cobro">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VALOR" HeaderText="Valor">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>

                            <asp:Label ID="lbl_titulo_atributo_ape" runat="server" CssClass="module_subsec text_input_nice_label"></asp:Label>

                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_Valor" runat="server" class="text_input_nice_input" MaxLength="9" ValidationGroup="val_APE_CRED"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_Valor" runat="server" CssClass="text_input_nice_label">*Valor:</asp:Label>
                                            &nbsp;<asp:Label ID="lbl_ValorMinMax" runat="server" CssClass="text_input_nice_label"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Valor" runat="server" ControlToValidate="txt_Valor"
                                                CssClass="alertaValidator" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_APE_CRED"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Valor" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_Valor">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Valor"
                                                runat="server" ControlToValidate="txt_Valor" CssClass="textogris"
                                                ErrorMessage=" Error: Valor incorrecto" lass="textorojo"
                                                ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_status_atrib" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_APE_CRED" runat="server" ValidationGroup="val_APE_CRED"
                                    class="btn btn-primary" Text="Guardar" />
                            </div>

                        </asp:Panel>



                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar_c" />
                    </Triggers>

                </asp:UpdatePanel>
            </div>
        </div>
    </section>

        <input type="hidden" name="hdn_alertas" id="hdn_alertas" value="" runat="server" />

</asp:Content>


