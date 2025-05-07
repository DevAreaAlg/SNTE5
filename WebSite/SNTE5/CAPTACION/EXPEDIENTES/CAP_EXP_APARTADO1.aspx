<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_APARTADO1.aspx.vb" Inherits="SNTE5.CAP_EXP_APARTADO1" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <script type="text/javascript">

       
        function busquedapersonafisica(tipo) {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=" + tipo, wbusf, "center:yes;resizable:no;dialogHeight:687px;dialogWidth:670px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }

      
    </script>

    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del expediente:  " Enabled="false"></asp:Label></span>
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

    <%-- MANCOMUNADA--%>
    <asp:Panel ID="TabPanel2" runat="server" CssClass="panel">
        <header class="panel_header_folder panel-heading">
            <span>Cuenta mancomunada</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="UpdatePanelCuentaMancumunada" runat="server">
                    <ContentTemplate>

                        <div class="overflow_x shadow module_subsec">
                            <asp:Label ID="lbl_statusmancomunadocont" runat="server" CssClass="textogris module_subsec"></asp:Label>
                            <asp:DataGrid ID="DAG_persona" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" HorizontalAlign="Center" Width="100%">
                                <HeaderStyle CssClass="table_header" />

                                <Columns>
                                    <asp:BoundColumn DataField="idmancomunado" HeaderText="Núm. afiliado">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre">
                                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="edad" HeaderText="Edad">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="lugarnac" HeaderText="Lugar de nacimiento">
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle Width="20%" />
                                    </asp:ButtonColumn>
                                </Columns>

                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec">
                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic"></asp:Label>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_busqMan" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>
                        </div>

                        <div class="module_subsec columned low_m three_columns">
                            <div class="module_subsec_elements text_input_nice_div">
                               
                                <asp:TextBox ID="lbl_nombre1" runat="server" class="text_input_nice_input" MaxLength="8"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Número de afiliado:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server"
                                        TargetControlID="lbl_nombre1" FilterType="Numbers" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre" runat="server"
                                        ControlToValidate="lbl_nombre1" CssClass="textogris" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_idPerMancomunada"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>




                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusmancomunado" runat="server" CssClass="alerta flex_1 module_subsec no_m  flex_center align_items_flex_center"></asp:Label>
                        </div>
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_mancomunada" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_idPerMancomunada" />
                        </div>
                    </ContentTemplate>
                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="btn_guardar_mancomunada" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </asp:Panel>

    <%-- TASAS--%>
    <section class="panel" id="panel_tasas">
        <header class="panel_header_folder panel-heading">
            <span>Tasas </span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">

                <asp:UpdatePanel ID="UpdatePanelYTazasNotas" runat="server">
                    <ContentTemplate>
                        <div class="module_subsec no_m">
                            <span class="module_subsec">Establezca las tasas requeridas</span>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_tipotasa" runat="server" class="btn btn-primary2 dropdown_label"
                                        ValidationGroup="val_planpagosi" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tipotasa" runat="server" CssClass="text_input_nice_label">*Tipo tasa ordinaria:</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipotasa" runat="server"
                                            ControlToValidate="cmb_tipotasa" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_tasa" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_tasa" runat="server" CssClass="text_input_nice_input" MaxLength="6" Enabled="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tasa" runat="server" CssClass="texto"></asp:Label>

                                        
                                        <asp:Label ID="lbl_porcentaje" runat="server" CssClass="text_input_nice_label" Text="% Anual "></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasa" runat="server"
                                            ControlToValidate="txt_tasa" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage="  Falta Dato!" ValidationGroup="val_tasa" Enabled="false"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_tasai" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_tasa" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="lbl_errortasasi" runat="server" CssClass="alerta"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_tasa" runat="server" Display="Dynamic"
                                            ControlToValidate="txt_tasa" CssClass="textogris" ErrorMessage=" Error:Tasa Incorrecta" class="textorojo"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_puntos" runat="server" CssClass="text_input_nice_input" MaxLength="6" Enabled="false"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tasasiind" runat="server" CssClass="text_input_nice_label" Text="*Tasa indizada:"></asp:Label>
                                        <asp:Label ID="lbl_indice" runat="server" CssClass="texto" Visible="False"></asp:Label>
                                        <asp:Label ID="lbl_puntos" runat="server" CssClass="texto"></asp:Label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_puntos" runat="server"
                                            ControlToValidate="txt_puntos" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_tasa" Enabled="false"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_puntos" runat="server" Display="Dynamic"
                                            ControlToValidate="txt_puntos" CssClass="textogris" ErrorMessage=" Error:Puntos Incorrectos" class="textorojo"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_puntos" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txt_puntos" ValidChars=".">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="lbl_errorpuntossi" runat="server" CssClass="alerta"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="module_subsec low_m">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_notas" runat="server" CssClass="text_input_nice_textarea" MaxLength="2000"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_titnotas" runat="server" class="subtitulos">Notas:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_notas"
                                            runat="server" Enabled="True"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers"
                                            TargetControlID="txt_notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agregar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_tasa" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_agregar" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </section>

    <%-- PERSONAS AUTORIZADAS--%>
    <section class="panel" id="panel_autorizados">
        <header class="panel_header_folder panel-heading">
            <span>Personas autorizadas</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>


        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="UpdatePanelPersonaAutorizadas" runat="server">
                    <ContentTemplate>
                        <div class="overflow_x shadow module_subsec">

                            <asp:Label ID="lbl_firmasCount" runat="server" CssClass="textogris module_subsec"></asp:Label>
                            <asp:DataGrid ID="DAG_PerAcep" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <Columns>
                                    <asp:BoundColumn DataField="idfirma" HeaderText="Núm. afiliado">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                        <ItemStyle HorizontalAlign="Center" Width="70%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" Text="Eliminar">
                                        <ItemStyle Width="20%" />
                                    </asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle CssClass="table_header" />

                            </asp:DataGrid>
                        </div>

                        <div class="module_subsec">
                            <asp:Label ID="Label2" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic "></asp:Label> &nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_busq" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="lbl_nombrebusqueda" runat="server" class="text_input_nice_input" MaxLength="8"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Número de afiliado:</span>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombrebusqueda" runat="server"
                                            TargetControlID="lbl_nombrebusqueda" FilterType="Numbers" Enabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombrebusqueda" runat="server"
                                            ControlToValidate="lbl_nombrebusqueda" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_idPerFirma"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_bsq_persona" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="Btn_GuardarBusq" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_idPerFirma" />
                              
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Btn_GuardarBusq" />

                    </Triggers>
                </asp:UpdatePanel>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_validaPrellenar" runat="server" CssClass="alerta"></asp:Label>
                </div>
                <div class="module_subsec flex_center">
<asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Prellenar" ToolTip="Prellenar tarjeta de personas autorizadas" />

                  
                </div>

            </div>
        </div>

    </section>

    <input type="hidden" name="hdn_busqueda" id="hdn_busqueda" value="" runat="server" />
</asp:Content>

