<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO7.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO7" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>
                <asp:Label runat="server" ID="lbl_Folio" Text="Datos del Expediente: " Enabled="false" />
            </span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
                <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>
    <section class="panel" runat="server" id="pnl_pre_sol">
        <header class="panel_header_folder panel-heading" runat="server" id="head_pnl_pre_sol">
            <span class="panel_folder_toogle_header">Prellenado de documentos</span>
            <span class=" panel_folder_toogle up" href="#" runat="server" id="toggle_pnl_pre_sol">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_pnl_pre_sol">
                <div class="module_subsec columned three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList runat="server" ID="ddl_medio_pago" CssClass="btn btn-primary2 dropdown_label"
                                AutoPostBack="true" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Método de pago:" />
                                <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator"
                                    ControlToValidate="ddl_medio_pago" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList runat="server" ID="cmb_banco" CssClass="btn btn-primary2 dropdown_label"
                                AutoPostBack="true" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Banco:" />
                                <asp:RequiredFieldValidator runat="server" ID="rfv_ddl_banco" CssClass="alertaValidator"
                                    ControlToValidate="cmb_banco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" InitialValue="-1" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox runat="server" ID="txt_correo" CssClass="text_input_nice_input" MaxLength="100" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Correo electrónico:" />
                            <asp:RegularExpressionValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="txt_correo" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+){1,3}$" />
                        </div>
                    </div>
              <!-- Categoría -->
                    <%--<div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox runat="server" ID="txt_cat" CssClass="text_input_nice_input" MaxLength="20" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Categoría:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="txt_cat" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="Custom,Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_cat"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" />
                        </div>
                    </div>--%>
                </div>
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="txt_clabe" CssClass="text_input_nice_input" runat="server" MaxLength="18" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="*CLABE:" />
                                <asp:RequiredFieldValidator ID="rfv_txt_clabe" runat="server" ControlToValidate="txt_clabe"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                    TargetControlID="txt_clabe" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="tbx_clabe_conf" CssClass="text_input_nice_input" oncopy="return false" oncut="return false" onpaste="return false" autocomplete="off" runat="server" MaxLength="18" />
                            <div class="text_input_nice_labels">
                                <asp:Label runat="server" Text="*Confirmar CLABE:" />
                                <asp:RequiredFieldValidator runat="server" ID="rfv_tbx_clabe_conf" ControlToValidate="tbx_clabe_conf"
                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Persona" />
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                    TargetControlID="tbx_clabe_conf" />
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox runat="server" ID="txt_num" CssClass="text_input_nice_input" MaxLength="10" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Teléfono personal:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="txt_num" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txt_num" />
                        </div>
                    </div> 
                    <!-- Años de servicio -->
                    <%--<div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox runat="server" ID="txt_anios" CssClass="text_input_nice_input" MaxLength="5" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="Años de servicio:" />
                            <asp:RequiredFieldValidator runat="server" CssClass="alertaValidator bold"
                                ControlToValidate="txt_anios" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True" FilterType="Numbers"
                                TargetControlID="txt_anios" />
                        </div>
                    </div>--%>
                </div>
                <div class="module_subsec flex_end">
                    <asp:Label ID="lbl_llen_solic" runat="server" CssClass="alerta"></asp:Label>
                    <asp:Button ID="btn_guardaBanco" runat="server" class="btn btn-primary" ValidationGroup="val_Persona" Text="Guardar" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_DescargarPrellenadoCred" runat="server" class="btn btn-primary" Text="Generar" Enabled="false" />
                </div>
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta"></asp:Label>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
