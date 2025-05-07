<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_REF_COM.aspx.vb" Inherits="SNTE5.CRED_EXP_REF_COM" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
                <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>

        <div class="panel-body">
                    <div class="panel-body_content">
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                        <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                    </div>
                    <div class="module_subsec columned low_m align_items_flex_center">
                        <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                        <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                    </div>
                </div>
            </div>
    </section>

    <section class="panel" id="panel_ref_banc">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header" >Referencias bancarias</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <asp:UpdatePanel ID="upd_referencia" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec">
                            <asp:DataGrid ID="DAG_efectivo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                CssClass="table table-striped" GridLines="None" TabIndex="17" >
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CVE_BANCO" visible ="false">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="BANCO" HeaderText="Nombre banco" >
                                        <ItemStyle Width="15%"  HorizontalAlign="Center" />
                                    </asp:BoundColumn>   
                                    <asp:BoundColumn DataField="NUM_CUENTA" HeaderText="Número de cuenta">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOM_EJEC" HeaderText="Nombre ejecutivo o contacto">
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TELEFONO_EJEC" HeaderText="Teléfono">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SALDO_EJEC" HeaderText="Saldo">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText=""
                                        Text="Eliminar">
                                        <ItemStyle Width="5%" />
                                    </asp:ButtonColumn>
                                </Columns>        
                            </asp:DataGrid> 
                        </div>
        
                
                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_institucion_Efe" runat="server" class="text_input_nice_input" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_institucion_Efe" runat="server" CssClass="text_input_nice_label" Text="*Nombre del banco:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_institucion_Efe" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucion_Efe" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txt_institucion_Efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_RefCom"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nocta_efe" runat="server" class="text_input_nice_input" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_nocta_efe" runat="server" CssClass="text_input_nice_label" Text="*No. cuenta:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nocta_efe" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_nocta_efe"
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nocta_efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_RefCom"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldo_efe" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_saldo_efe" runat="server" CssClass="text_input_nice_label" Text="*Saldo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_saldo_efe" >
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_saldo_efe"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_RefCom"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>                            
                        </div>

                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_NomEjec" runat="server" class="text_input_nice_input" MaxLength="150"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_NomEjec" runat="server" CssClass="text_input_nice_label" Text="*Nombre del ejecutivo o contacto:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_NomEjec" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_NomEjec"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_RefCom"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_telefono" runat="server" class="text_input_nice_input" MaxLength="19" ></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Telefono" runat="server" CssClass="text_input_nice_label" Text="*Teléfono:"></asp:Label> 
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_telefono"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_telefono"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_RefCom"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>                            
                        </div>
                
                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcEfectivo" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_AcEfectivo" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_RefCom"/>
                        </div>                 

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_prov">            
        <header class="panel_header_folder panel-heading">
                <span  class="panel_folder_toogle_header" >Principales proveedores</span>
                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_prov" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec">
                            <asp:DataGrid ID="DAG_CXPnoDOC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                CssClass="table table-striped" GridLines="None" TabIndex="17">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CVE_PROV"  visible ="false" HeaderText="Clave proveedor" >
                                        <ItemStyle Width="5px"  HorizontalAlign="Center" />
                                    </asp:BoundColumn>   
                                    <asp:BoundColumn DataField="NOM_PROV" HeaderText="Nombre proveedor">
                                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LIMITE_CRED" HeaderText="Límite de préstamo">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOM_EJEC" HeaderText="Nombre ejecutivo">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TELEFONO_EJEC" HeaderText="Teléfono ejecutivo">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PLAZO" HeaderText="Plazo">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText=""
                                        Text="Eliminar">
                                        <ItemStyle Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>        
                            </asp:DataGrid>  
                        </div>             


                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_institucion_Prov" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_institucion_Prov" runat="server" CssClass="text_input_nice_label" Text="*Nombre del proveedor:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_institucion_Prov" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucion_Prov" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_institucion_Prov"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_prov"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nocta_Prov" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_nocta_Prov" runat="server" CssClass="text_input_nice_label" Text="*Límite de préstamo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_nocta_Prov">
                                        </AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_nocta_Prov"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_prov"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_NomEjec_Prov" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label1" runat="server" CssClass="text_input_nice_label" Text="*Nombre del ejecutivo o contacto:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_NomEjec_Prov" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>      
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_NomEjec_Prov"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_prov"></asp:RequiredFieldValidator> 
                                    </div>
                                </div>
                            </div>
                        </div>
   
                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_telefono_Prov" runat="server" class="text_input_nice_input" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Telefono_Prov" runat="server" CssClass="text_input_nice_label" Text="*Teléfono:"></asp:Label> 
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_telefono_Prov">
                                    </AjaxToolkit:FilteredTextBoxExtender>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_telefono_Prov"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_prov"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldo_efe_Prov" runat="server" class="text_input_nice_input" MaxLength="4"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_saldo_efe_Prov" runat="server" CssClass="text_input_nice_label" Text="*Plazo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_saldo_efe_Prov" ></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_saldo_efe_Prov"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_prov"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_status_AcCXPnoDOC" runat="server" CssClass="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guarda_CXPnoDOC" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_prov"/>
                        </div>
                            
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_clientes">            
        <header class="panel_header_folder panel-heading">
                <span  class="panel_folder_toogle_header" >Principales afiliados</span>
                <span class=" panel_folder_toogle up" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <asp:UpdatePanel ID="upd_clientes" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <div class="module_subsec">
                            <asp:DataGrid ID="DAG_bienesmu" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                CssClass="table table-striped" GridLines="None" TabIndex="17">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                     <asp:BoundColumn DataField="ID_PERSONA" visible ="false">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CVE_CLIENTE"  visible ="false" >
                                        <ItemStyle Width="5px"  HorizontalAlign="Center" />
                                    </asp:BoundColumn>   
                                    <asp:BoundColumn DataField="NOM_CLIENTE" HeaderText="Nombre afiliado">
                                        <ItemStyle Width="350px" HorizontalAlign="center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VTAS_ANUALES" HeaderText="Ventas anuales">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOM_CONTACTO" HeaderText="Nombre contacto">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TELEFONO_CONTACTO" HeaderText="Teléfono contacto">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                      <asp:BoundColumn DataField="PLAZO" HeaderText="Plazo">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="ELIMINAR" HeaderText=""
                                        Text="Eliminar">
                                        <ItemStyle Width="40px" />
                                    </asp:ButtonColumn>
                                </Columns>        
                            </asp:DataGrid>
                        </div>                                      

                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_institucion_Cte" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_institucion_Cte" runat="server" CssClass="text_input_nice_label" Text="*Nombre del cliente:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_institucion_Cte" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_institucion_Cte" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_institucion_Cte"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cte"></asp:RequiredFieldValidator> 
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nocta_Cte" runat="server" class="text_input_nice_input" MaxLength="12"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_nocta_Cte" runat="server" CssClass="text_input_nice_label" Text="*Ventas anuales:"></asp:Label>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txt_nocta_Cte"></AjaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_nocta_Cte"
                                        Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cte"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_NomEjec_Cte" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label2" runat="server" CssClass="text_input_nice_label" Text="*Nombre del contacto:"></asp:Label>        
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                            Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txt_NomEjec_Cte" 
                                            ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ"></AjaxToolkit:FilteredTextBoxExtender>   
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_NomEjec_Cte"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cte"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns" >
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_telefono_Cte" runat="server" class="text_input_nice_input" MaxLength="18"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Telefono_Cte" runat="server" CssClass="text_input_nice_label" Text="*Teléfono:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_telefono_Cte"></AjaxToolkit:FilteredTextBoxExtender>      
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_telefono_Cte"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cte"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_saldo_efe_Cte" runat="server" class="text_input_nice_input" MaxLength="4"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_saldo_efe_Cte" runat="server" CssClass="text_input_nice_label" Text="*Plazo:"></asp:Label>
                                        <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                            FilterType="Numbers" TargetControlID="txt_saldo_efe_Cte"></AjaxToolkit:FilteredTextBoxExtender>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_saldo_efe_Cte"
                                            Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cte"></asp:RequiredFieldValidator> 
                                    </div>
                                </div>
                            </div>
                        </div>
            
                        <div class="module_subsec flex_center">
                             <asp:Label ID="lbl_statusbienmu" runat="server" CssClass="alerta"></asp:Label>
                        </div>
                    
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardabienmu" runat="server"  class="btn btn-primary" Text="Agregar" ValidationGroup="val_cte"/>
                        </div> 

                    </ContentTemplate>
                </asp:UpdatePanel>       
            </div>
        </div>
    </section>

</asp:Content>

