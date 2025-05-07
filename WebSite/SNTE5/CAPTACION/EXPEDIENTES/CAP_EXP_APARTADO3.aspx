<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_APARTADO3.aspx.vb" Inherits="SNTE5.CAP_EXP_APARTADO3" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
     <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="false" id="pnl_ppe">
        <header class="panel_header_folder panel-heading" runat="server" id="head_pnl_ppe">
            <span>Persona políticamente expuesta</span>
            <span class=" panel_folder_toogle up" href="#" runat="server" id="toggle_pnl_ppe">&rsaquo;</span>
        </header>
        <div class="panel-body">


            <div class="panel-body_content" runat="server" id="content_pnl_ppe">
                <asp:UpdatePanel ID="UpdatePanelCuentaMancumunada" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnl_ppe2">
                            <div class="module_subsec">
                                <span class="text_input_nice_label">Si el afiliado o algun familiar del afiliado son personas políticamente expuestas, captúre los siguientes datos. De lo contrario, de click en el botón "Saltar este paso".</span>
                            </div>
                            <%-- DAG POLITICA --%>
                            <div class="overflow_x shadow module_subsec">
                                <asp:DataGrid ID="dag_politica" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" Width="100%">
                                    <Columns>
                                        <asp:BoundColumn DataField="IDPERPOL" HeaderText="Nombre" Visible="False">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle Width="35%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PUESTO" HeaderText="Puesto">
                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PARENTESCO" HeaderText="Parentesco">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                            <ItemStyle Width="20%" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                </asp:DataGrid>
                            </div>

                            <div class="module_sec">
                                <span class="text_input_nice_label">*Datos requeridos</span>
                            </div>

                            <%-- RADIO BUTTONS SI ES EL AFILIADO O ES ALGUN FAMILIAR --%>
                            <div class="module_sec low_m">
                                <div class="module_subsec columned two_columns ">
                                    <div class=" module_subsec_elements">
                                        <div class="module_subsec_elements_content flex_end">
                                            <asp:RadioButton ID="rad_cliente" runat="server" Text="Afiliado " class="text_input_nice_label" GroupName="rad_ppe" AutoPostBack="True" />
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="module_subsec_elements_content ">
                                            <asp:RadioButton ID="rad_familiar" runat="server" Text="Familiar " class="text_input_nice_label" GroupName="rad_ppe" AutoPostBack="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="module_subsec columned three_columns ">
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_politica_parentesco" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Parentesco:</span>
                                            <asp:RequiredFieldValidator runat="server" ID="req_politica_parentesco" CssClass="textogris"
                                                ControlToValidate="cmb_politica_parentesco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" InitialValue="-1" />
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_politica_nombre1" runat="server" class="text_input_nice_input" MaxLength="300" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Primer nombre:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fil_politica_nombre1"
                                                runat="server" TargetControlID="txt_politica_nombre1" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True" />
                                            <asp:RequiredFieldValidator runat="server" ID="req_politica_nombre1" CssClass="textogris"
                                                ControlToValidate="txt_politica_nombre1" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" />
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_politica_nombre2" runat="server" class="text_input_nice_input" MaxLength="300" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Segundo(s) nombre(s):</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_politica_nombre2"
                                                runat="server" TargetControlID="txt_politica_nombre2" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec columned three_columns ">
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">

                                        <div class="text_input_nice_labels">
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_politica_paterno" runat="server" class="text_input_nice_input" MaxLength="100" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Apellido paterno:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_politica_paterno"
                                                runat="server" TargetControlID="txt_politica_paterno" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True" />
                                            <asp:RequiredFieldValidator runat="server" ID="req_politica_paterno" CssClass="textogris"
                                                ControlToValidate="txt_politica_paterno" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" />
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:TextBox ID="txt_politica_materno" runat="server" class="text_input_nice_input" MaxLength="100" />
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">Apellido materno:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_politica_materno"
                                                runat="server" TargetControlID="txt_politica_materno" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" Enabled="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="module_subsec columned three_columns ">
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_categoria" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="True"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Categoría:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_categoria"
                                                runat="server" ControlToValidate="cmb_categoria" CssClass="textogris"
                                                Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1"
                                                ValidationGroup="val_ppe"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_politca_organo" runat="server" CssClass="btn btn-primary2 dropdown_label" AutoPostBack="True"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <label id="lbl_politica_organo" class="text_input_nice_label">*Organo:</label>
                                            <asp:RequiredFieldValidator runat="server" ID="req_politica_organo" CssClass="textogris"
                                                ControlToValidate="cmb_politca_organo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" InitialValue="-1" />
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_politca_puesto" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Puesto:</span>
                                            <asp:RequiredFieldValidator runat="server" ID="req_politica_puesto" CssClass="textogris"
                                                ControlToValidate="cmb_politca_puesto" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" InitialValue="-1" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class=" align_items_flex_start module_subsec columned three_columns  ">
                                <div class=" module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_labels">
                                            <span class="text_input_nice_label">*Nivel del puesto:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_nivel" runat="server"
                                                ControlToValidate="cmb_nivel" CssClass="textogris" Display="Dynamic"
                                                ErrorMessage=" Falta Dato!" InitialValue="-1" ValidationGroup="val_ppe"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="btn-group min_w">
                                            <asp:DropDownList ID="cmb_nivel" runat="server" CssClass="btn btn-primary2 dropdown_label">
                                                <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class=" module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txtUFecSer" runat="server" class="text_input_nice_input" MaxLength="10" />
                                            <span class="text_input_nice_label">*Ultima fecha de servicio:</span>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderUFS" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date"
                                                TargetControlID="txtUFecSer" Enabled="True" />
                                            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                TargetControlID="txtUFecSer" Enabled="True" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUFecSer" runat="server" ErrorMessage="Falta Dato!"
                                                ValidationGroup="val_ppe" ControlToValidate="txtUFecSer" CssClass="textoazul" Display="Dynamic"></asp:RequiredFieldValidator>

                                        </div>



                                    </div>


                                </div>

                                <div class="module_subsec_elements">
                                    <div class="module_subsec_elements_content vertical">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txt_descripcion" class="text_input_nice_textarea" MaxLength="2000"></asp:TextBox>
                                            <span class="text_input_nice_label">*Descripción del puesto:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_descripcion" runat="server" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_ppe" ControlToValidate="txt_descripcion" CssClass="textoazul" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="rev_descripcion"
                                                ControlToValidate="txt_descripcion"
                                                ValidationExpression="^[\s\S]{0,300}$"
                                                ErrorMessage="Por favor introduzca máximo 300 caracteres."
                                                Display="Dynamic" ValidationGroup="val_ppe" CssClass="textogris"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_estatus_politica" runat="server" CssClass="alerta"></asp:Label>
                            </div>
                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_ppe" />
                            </div>


                        </asp:Panel>

                    </ContentTemplate>

                </asp:UpdatePanel>

               <%-- <div class="module_subsec flex_center">

                    <asp:Button ID="btn_skip" runat="server" class="btn btn-primary" Text="Saltar este paso"  Visible="false"/>

                </div>--%>



            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="pnl_relfun">
        <header class="panel_header_folder panel-heading" runat="server" id="head_pnl_relfun">
            <span>Relaciones funcionarios </span>
            <span class=" panel_folder_toogle up" href="#" runat="server" id="toggle_pnl_relfun">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_relfun">

                <asp:Panel runat="server" ID="panel_relfun">
                    <asp:UpdatePanel ID="UpdatePanelRelFun" runat="server">
                        <ContentTemplate>
                            <div class="module_subsec">
                                <span class="text_input_nice_label">Si el afiliado o algun familiar del mismo, es funcionario de la entidad, capture la siguiente información. De lo contrario, de click en el botón "Saltar este paso".</span>
                            </div>

                            <div class="module_subsec columned three_columns">
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_miembros_consejo" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <span class="text_input_nice_label">Funcionario:</span>
                                    </div>
                                </div>
                                <div class="module_subsec_elements">
                                    <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList ID="cmb_consejo_parentesco" runat="server" class="btn btn-primary2 dropdown_label" Visible="False">
                                        </asp:DropDownList>
                                        <div class="text_input_nice_labels">
                                            <asp:Label ID="lbl_consejo_parentesco" runat="server" CssClass="texto" Text="*Parentesco: " Visible="False" />
                                            <asp:RequiredFieldValidator runat="server" ID="req_consejo_parentesco" CssClass="textogris"
                                                ControlToValidate="cmb_consejo_parentesco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_consejo" InitialValue="-1" Enabled="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="lnk_agregar_consejo" runat="server" class="btn btn-primary" Text="Agregar"
                                    Visible="False" ValidationGroup="val_consejo" />
                            </div>

                            <div class="overflow_x shadow module_subsec">
                                <asp:DataGrid ID="dag_miembros_consejo" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" HorizontalAlign="Center" Width="100%">
                                    <Columns>
                                        <asp:BoundColumn DataField="IDPERCON" HeaderText="Núm. Afiliado">
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle Width="40%" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PARENTESCO" HeaderText="Parentesco">
                                            <ItemStyle HorizontalAlign="Center" Width="30%" />
                                        </asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                            <ItemStyle Width="20%" />
                                        </asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                                </asp:DataGrid>
                            </div>
                            <br />

                            <div class="module_subsec flex_center">
                                <asp:Label ID="lbl_estatus_relfun" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>

                    <%--<div class="module_subsec flex_center">
                        <asp:Button ID="btn_skip_relfun" runat="server" class="btn btn-primary" Text="Saltar este paso" />
                    </div>--%>

                </asp:Panel>

            </div>
        </div>

    </section>

    <%--    <section class="panel" runat="server" id="pnl_pre_sol">
        <header class="panel_header_folder panel-heading" runat="server" id="head_pnl_pre_sol">
            <span>Prellenado de Solicitudes</span>
            <span class=" panel_folder_toogle up" href="#" runat="server" id="toggle_pnl_pre_sol">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_pre_sol">

                <div class="module_subsec flex_center">
                    <asp:Button ID="btn_DescargarPrellenadoCred" runat="server" class="btn btn-primary" Text="Descargar Prellenado" />
                </div>

            </div>
        </div>
    </section>--%>
    <section class="panel" runat="server" id="pnl_pre_sol">
        <header class="panel_header_folder panel-heading" runat="server" id="head_pnl_pre_sol">
            <span>Prellenado de documentos</span>
            <span class=" panel_folder_toogle up" href="#" runat="server" id="toggle_pnl_pre_sol">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_pre_sol">
                <div class="module_subsec columned two_columns ">
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_documento" class="btn btn-primary2 dropdown_label" runat="server"></asp:DropDownList>

                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Seleccione documento:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_FolioPagare"
                                    CssClass="textogris" ControlToValidate="cmb_documento"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_doc" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class=" module_subsec_elements">
                        <div class="module_subsec_elements_content">
                            <asp:Button ID="btn_docs" runat="server" class="btn btn-primary" Text="Generar" ValidationGroup="val_doc" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec">
                    <asp:Label ID="lbl_status_docs" runat="server" CssClass="alerta"></asp:Label>
                </div>
            </div>




        </div>





    </section>

</asp:Content>
