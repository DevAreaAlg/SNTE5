<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="UNI_REFERENCIAS.aspx.vb" Inherits="SNTE5.UNI_REFERENCIAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">

        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <section class="panel" id="panel_datos_referencias">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="text_input_nice_input module_subsec_elements module_subsec_bigger-elements"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_referencias">
        <header class="panel_header_folder panel-heading">
            <span class=" panel_folder_toogle_header">Referencia existente</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="upd_referencia" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbl_refer" runat="server" CssClass="textogris module_subsec"></asp:Label>

                        <%--INICIA EL DATAGRID--%>
                        <div class="overflow_x shadow module_subsec">
                            <asp:DataGrid ID="DAG_referencias" runat="server" GridLines="None"  CssClass="table table-striped" 
                                 AutoGenerateColumns="False" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="cvereferencia" HeaderText="Clave Referencia">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="tiempoconocer" HeaderText="Años de conocerlo">
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText=""
                                        Text="Eliminar">
                                        <ItemStyle Width="10%" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid><br />
                        </div>

                        <div class="module_subsec">                           
                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic"></asp:Label>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_busqueda" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>                            
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="ddl_Instituciones" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Institución:</span>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumCtrl" runat="server" ControlToValidate="ddl_Instituciones"
                                                    CssClass="textogris" Display="Dynamic" InitialValue="-1" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">

                                    <asp:TextBox runat="server" MaxLength="10" ID="txt_IdCliente" class="text_input_nice_input" Visible="false"></asp:TextBox>
                                    <asp:TextBox runat="server" MaxLength="10" ID="txt_IdCliente1" class="text_input_nice_input"></asp:TextBox>

                                    
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_numaval" runat="server" CssClass="text_input_nice_label"
                                            Text="*Número de control: "></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" FilterType="Numbers" Enabled="True"
                                            TargetControlID="txt_idCliente" ID="FilteredTextBoxExtender__idcliente">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_idcliente" runat="server" ControlToValidate="txt_idcliente1"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                             <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_conocer2" runat="server" Class="text_input_nice_input" MaxLength="2"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_conocer0" runat="server" CssClass="text_input_nice_label"
                                            Text="Años de conocerlo:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txt_conocer2_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" FilterType="Numbers"
                                            TargetControlID="txt_conocer2">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                   
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                           
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_relacion" runat="server" class="btn btn-primary2 dropdown_label">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_relacion" runat="server" CssClass="text_input_nice_label" Text="*Tipo de relación:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_relacion" runat="server" ControlToValidate="cmb_relacion"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_agregar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_busqueda" />
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusgeneral" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_nueva_referencia">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header" >Nueva referencia </span>
            <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="up_semaforos" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        
                        <asp:Label ID="lbl_clave" runat="server" CssClass="texto" Text="Número de referencia:"
                            Visible="False"></asp:Label>

                        <asp:TextBox ID="txt_clave" runat="server" class="textocajas" Enabled="False" MaxLength="10"
                            Visible="False"></asp:TextBox>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_tiporel" runat="server" class="btn btn-primary2 dropdown_label">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tiporel" runat="server" CssClass="text_input_nice_label" Text="*Tipo de relación:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tiporel" runat="server" ControlToValidate="cmb_tiporel"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_conocer" runat="server" Class="text_input_nice_input" MaxLength="2"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_conocer" runat="server" CssClass="text_input_nice_label" Text="Años de conocerlo(a):"></asp:Label>
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator_antviv" runat="server" ControlToValidate="txt_conocer"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>--%>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_conocer" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_conocer">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombres" runat="server" class="text_input_nice_input" MaxLength="200" ValidationGroup="val_referencias"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_nombres" runat="server" CssClass="text_input_nice_label" Text="*Nombre(s):"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombres" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_nombres"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="Req_nombres" runat="server" ControlToValidate="txt_nombres"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_paterno" runat="server" class="text_input_nice_input" MaxLength="100" ValidationGroup="val_Referencias"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_paterno" runat="server" CssClass="text_input_nice_label" Text="*Apellido paterno:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_paterno" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_paterno"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="Req_paterno" runat="server" ControlToValidate="txt_paterno"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_materno" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_materno" runat="server" CssClass="text_input_nice_label" Text="Apellido materno:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters" TargetControlID="txt_materno"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_cp" runat="server" CssClass="text_input_nice_label" Text="*Código postal:"></asp:Label>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cp" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_cp">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div>
                                    &nbsp;
                                    <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" />
                                    &nbsp; &nbsp;<asp:LinkButton ID="lnk_BusquedaCP" runat="server" class="textogris"
                                        Text="Buscar CP"></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_estado" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_estado" runat="server" CssClass="text_input_nice_label" Text="*Estado:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_estado" runat="server" ControlToValidate="cmb_estado"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_municipio" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_municipio" runat="server" CssClass="text_input_nice_label" Text="*Municipio:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_municipio" runat="server"
                                            ControlToValidate="cmb_municipio" CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!"
                                            ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_colonia" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_colonia" runat="server" CssClass="text_input_nice_label" Text="*Localidad:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_colonia" runat="server" ControlToValidate="cmb_colonia"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_tipo_via" runat="server" class="btn btn-primary2 dropdown_label">
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_tipo_via" runat="server" CssClass="text_input_nice_label" Text="*Tipo de vialidad:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tipo_via" runat="server" ControlToValidate="cmb_tipo_via"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements" style="flex: 1;">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_calle" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_calle" runat="server" CssClass="text_input_nice_label" Text="*Calle y número:"></asp:Label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_calle" runat="server" ControlToValidate="txt_calle"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_calle" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_calle"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ ,#">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_referencia" runat="server" CssClass="text_input_nice_textarea" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                                    <asp:Label ID="lbl_referencia" runat="server" CssClass="text_input_nice_label" Text="Referencias:"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <span class="text_input_nice_label title_tag module_subsec">Teléfono:</span>
                        <div class="module_subsec low_m columned three_columns top_m">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_ladamov" runat="server" class="text_input_nice_input" MaxLength="4"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label1" runat="server" CssClass="text_input_nice_label" Text="Lada:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_ladamov" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_ladamov">
                                        </ajaxToolkit:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_telmov" runat="server" class="text_input_nice_input" MaxLength="15"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label2" runat="server" CssClass="text_input_nice_label" Text="*Número:"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_telmov" runat="server" ControlToValidate="txt_telmov"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Referencias"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_telmov" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_telmov">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_statusnuevaref" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_Referencias" AutopostBack="True"/>
                        </div>                        

                        <input type="hidden" name="hdn_busqueda" id="hdn_busqueda" value="" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
   
</asp:Content>