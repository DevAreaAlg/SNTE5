<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_APARTADO1.aspx.vb" Inherits="SNTE5.CAP_PROD_APARTADO1" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_parametros">
        <header class="panel_header_folder panel-heading">
            <span>Parámetros generales</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldo" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Saldo mínimo para capitalizar interés($0.00): </span>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_saldo" runat="server"
                                            ControlToValidate="txt_saldo" CssClass="text_input_nic_label" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_investigacion"></asp:RequiredFieldValidator>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_saldo" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_saldo">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_saldo" runat="server"
                                            ControlToValidate="txt_saldo" CssClass="textogris" ErrorMessage=" Error:Valor Incorrecto" Display="Dynamic"
                                            class="textorojo" ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_captacion" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_captacion" runat="server" CssClass="text_input_nice_label" Text="*Tipo de captación:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_captacion" runat="server"
                                            ControlToValidate="cmb_captacion" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_investigacion" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns align_items_flex_start">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:RadioButton ID="rad_si" runat="server" class="texto" GroupName="Reqman" Text="Si" AutoPostBack="True" />
                                    <asp:RadioButton ID="rad_no" runat="server" class="texto" GroupName="Reqman" Text="No" AutoPostBack="True" />
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_mancomunado" runat="server" CssClass="texto" Text="*¿El producto es mancomunado?"></asp:Label>
                                        <asp:Label ID="lbl_falta_dato_man" runat="server" CssClass="alerta"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_numMan" runat="server" class="text_input_nice_input" MaxLength="1"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label4" runat="server" CssClass="text_input_nice_label" Text="*Número de personas mancomunadas:" ToolTip="Sin incluir al titular"></asp:Label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_numMan" runat="server"
                                            ControlToValidate="txt_numMan" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_investigacion"></asp:RequiredFieldValidator>
                                        <asp:Label ID="Label5" runat="server" CssClass="alerta"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_numMan">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_perAut" runat="server" class="text_input_nice_input" MaxLength="1"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label1" runat="server" CssClass="text_input_nice_label" Text="*Número de firmas aprobadas para retiro: "></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_perAut" runat="server"
                                            ControlToValidate="txt_perAut" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_investigacion"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_mesinv"
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_perAut">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="Label3" runat="server" CssClass="alerta"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:RadioButton ID="rad_ppe_si" runat="server" class="texto" GroupName="Reqppe" Text="Si" />
                                    <asp:RadioButton ID="rad_ppe_no" runat="server" class="texto" GroupName="Reqppe" Text="No" />
                                    <div class="text_input_nice_labels">
                                        <label id="lbl_investigacion_ppe" class="texto">*¿Requiere investigación persona políticamente expuesta?</label>
                                        <asp:Label ID="lbl_falta_dato" runat="server" CssClas="alerta"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:RadioButton ID="rad_fun_si" runat="server" class="texto" GroupName="Reqfun" Text="Si" />
                                    <asp:RadioButton ID="rad_fun_no" runat="server" class="texto" GroupName="Reqfun" Text="No" />
                                    <div class="text_input_nice_labels">
                                        <label id="Label2" class="texto">*¿Requiere captura de relación con funcionarios de la entidad?</label>
                                        <asp:Label ID="lbl_falta_dato_fun" runat="server" CssClass="alerta"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnl_VistaSaldoMin" runat="server" Width="100%" Height="150px">
                            <div class="module_subsec low_m columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_VistaSaldoMin" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                        <div class="text_input_nice_labels">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_VistaSaldoMin"
                                                runat="server" ControlToValidate="txt_VistaSaldoMin" CssClass="textogris"
                                                Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_investigacion"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_VistaSaldoMin"
                                                runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                TargetControlID="txt_VistaSaldoMin" ValidChars=".">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                        <asp:RadioButton ID="rad_VistaSaldoMinReqSI" runat="server" AutoPostBack="True" class="texto" GroupName="VistaMin" Text="Si" />
                                        <asp:RadioButton ID="rad_VistaSaldoMinReqNO" runat="server" AutoPostBack="True" class="texto" GroupName="VistaMin" Text="No" />
                                        <div class="text_input_nice_labels">
                                            <label id="lbl_VistaSaldoMinReq" class="texto">*¿Requiere saldo promedio mínimo para cobro de comisión? </label>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatusgneral" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardargenerales" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_investigacion" />
                            <br />
                        </div>


                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardargenerales" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_refybene">
        <header class="panel_header_folder panel-heading">
            <span>Beneficiarios y referencias</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>


                        <h5 class="module_subsec">*Número de beneficiarios:</h5>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" Text="" CssClass="text_input_nice_input" ID="txt_beneficiario" MaxLength="3" ValidationGroup="val_conf"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Mínimo:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_beneficiario" runat="server" ControlToValidate="txt_beneficiario"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_beneficiario" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_beneficiario">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" Text="" CssClass="text_input_nice_input" ID="txt_maxbeneficiario" MaxLength="3" ValidationGroup="val_conf"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Máximo:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_maxcodeudor" runat="server" ControlToValidate="txt_maxbeneficiario"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_maxbeneficiario" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_maxbeneficiario">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <h5 class="module_subsec">*Número de referencias:</h5>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" Text="" CssClass="text_input_nice_input" ID="txt_referencia" MaxLength="1" ValidationGroup="val_conf"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Mínimo:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_conf" runat="server" ControlToValidate="txt_referencia"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender_referencia" runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_referencia">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox runat="server" Text="" CssClass="text_input_nice_input" ID="txt_maxreferencia" MaxLength="1" ValidationGroup="val_conf"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Máximo:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_maxreferencia" runat="server" ControlToValidate="txt_maxreferencia"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_conf"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender
                                            ID="FilteredTextBoxExtender_maxreferencia" runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_maxreferencia">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta"></asp:Label>

                        </div>


                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_conf" />
                        </div>


                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

    </section>

    <section class="panel" id="panel_documentacion">
        <header class="panel_header_folder panel-heading">
            <span>Documentación requerida</span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="UpdatePanelDocumen" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec">
                            <div class="overflow_x shadow w_100">
                                <asp:DataGrid ID="DAG_documentos" AutoGenerateColumns="False" runat="server" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">

                                    <HeaderStyle CssClass="table_header" />

                                    <Columns>
                                        <asp:BoundColumn DataField="tipodoc" HeaderText="Clave del documento">
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="descripcion" HeaderText="Tipo de documento">
                                            <ItemStyle Width="25%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad">
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="fase" HeaderText="Fase">
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" HeaderText=""
                                            Text="Eliminar">
                                            <ItemStyle ForeColor="#054B66" Width="20%" />
                                        </asp:ButtonColumn>
                                    </Columns>

                                </asp:DataGrid>
                            </div>
                        </div>



                        <div class="module_sec low_m">
                            <div class=" align_items_flex_start module_subsec columned two_columns  ">
                                <div class="module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Tipo de documento:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipodoc" runat="server" ControlToValidate="cmb_tipodoc"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="btn-group min_w">
                                            <asp:DropDownList ID="cmb_tipodoc" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Cantidad:</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cantidad" runat="server" ControlToValidate="cmb_cantidad"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                        ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="btn-group min_w">
                                            <asp:DropDownList ID="cmb_cantidad" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="False">
                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>

                                            </asp:DropDownList>

                                        </div>

                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label title_tag">*Fase de validación:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_fase" runat="server" ControlToValidate="cmb_fase"
                                                CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="0"
                                                ValidationGroup="val_doc"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="btn-group min_w">
                                            <asp:DropDownList ID="cmb_fase" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                                                AutoPostBack="false">
                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="1">FASE 1</asp:ListItem>
                                                <asp:ListItem Value="2">FASE 2</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                    </div>

                                </div>

                                <div class="module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <span class="text_input_nice_label title_tag">Detalle de documentos:</span>
                                        <div class="btn-group min_w">
                                            <asp:ListBox ID="lst_Documentos" runat="server" CssClass="text_input_nice_textarea w_100" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_verifica2" runat="server" CssClass="alerta"></asp:Label>
                            <asp:Label ID="lbl_verifica3" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardardoc" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_doc" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardardoc" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

</asp:Content>
