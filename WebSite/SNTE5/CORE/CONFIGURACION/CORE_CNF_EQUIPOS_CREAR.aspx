<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_CNF_EQUIPOS_CREAR.aspx.vb" Inherits="SNTE5.CORE_CNF_EQUIPOS_CREAR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            Equipo
        </header>

        <div class="panel-body">

            <div class="module_subsec no_column align_items_flex_center">
                <span class="text_input_nice_label">Número de equipo: </span>
                       
                <div class="module_subsec_elements" style="flex:1">
                    <asp:TextBox ID="txt_numEquipo" CssClass="text_input_nice_input" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>
              
    </section>

    <section class="panel">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Datos</span>
            <span class=" panel_folder_toogle down" href="#">&rsaquo;</span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content init_show">

                <div class="module_sec low_m">
                    <div class="module_subsec no_column">
                        <span class="text_input_nice_label title_tag">Activo:</span>
                        <asp:CheckBox ID="checkbox_activo" CssClass="mod_check" runat="server" />
                    </div>
                </div>
                        
                <div class="module_subsec low_m columned three_columns">

                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="input_txt_nombreEquipo" CssClass="text_input_nice_input" runat="server"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="LowercaseLetters, UppercaseLetters, Numbers,Custom" ValidChars="_ -" TargetControlID="input_txt_nombreEquipo"></ajaxToolkit:FilteredTextBoxExtender>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Nombre Equipo:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" CssClass="alertaValidator bold" runat="server" ControlToValidate="input_txt_nombreEquipo" ErrorMessage="Falta Dato"  ValidationGroup="val_eq"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox ID="input_txt_direccionMac" CssClass="text_input_nice_input" runat="server" MaxLength="17"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre" runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters, Numbers, Custom" ValidChars=":" TargetControlID="input_txt_direccionMac">
                            </ajaxToolkit:FilteredTextBoxExtender>

                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">*Dirección MAC (Ejemplo CC:CC:CC:CC:CC:CC):</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" CssClass="alertaValidator bold" runat="server" ControlToValidate="input_txt_direccionMac" ErrorMessage="Falta Dato"  ValidationGroup="val_eq"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" CssClass="alertaValidator bold" runat="server" ControlToValidate="input_txt_direccionMac" ValidationExpression="^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$"  ValidationGroup="val_eq" ErrorMessage="Formato de MAC erróneo"></asp:RegularExpressionValidator>
                                <asp:Label ID="lbl_macRepetida" CssClass="alertaValidator bold" Visible="false" ForeColor="red" runat="server" Text="Ya existe un equipio con esa MAC"></asp:Label>
                            </div>

                        </div>
                    </div>
                    <div class="module_subsec_elements">
                        <div class="module_subsec_elements_content vertical">
                            <div class="text_input_nice_labels title_tag">
                                <span class="text_input_nice_label">*Oficina:</span>
                                <asp:RequiredFieldValidator  runat="server" ID="Req_sucursal" CssClass="textogris"
                                    ControlToValidate="cmb_sucursal_busqueda" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_eq" InitialValue="-1"></asp:RequiredFieldValidator>
                            </div>

                            <asp:DropDownList ID="cmb_sucursal_busqueda" runat="server" class="btn btn-primary2 dropdown_label">
                            </asp:DropDownList>

                        </div>
                    </div>
                </div>
                        
                
  
                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statuseq" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div class="module_subsec flex_end">
                    
                        <asp:Button ID="lnk_guardar" CssClass="btn btn-primary"  runat="server" Text="Guardar" validationgroup="val_eq"/>
                    
                </div>

                

            </div>
        </div>
       
    </section>
        
</asp:Content>
