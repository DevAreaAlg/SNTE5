<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DESP_PERSONAM.aspx.vb" Inherits="SNTE5.COB_DESP_PERSONAM" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }
    </script>


    
    <asp:UpdatePanel ID="up_semaforos" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
          <div class="semaforo_barra">
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Personales" ID="lnk_to_generales">

                    <span id="Semaforo1_r" class="semaforo_img alto" runat="server" visible="true" />
                    <span id="Semaforo1_v" class="semaforo_img prosiga" runat="server" visible="false" />
                </asp:LinkButton>
              <%--  <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Domicilio" ID="lnk_to_adicionales">
                    <span id="Semaforo2_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo2_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>--%>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Domicilio" ID="lnk_to_dependientes">
                    <span id="Semaforo3_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo3_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Contacto" ID="lnk_to_domicilio">
                    <span id="Semaforo4_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo4_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
                <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Asignación de Usuarios" ID="lnk_to_contacto">
                    <span id="Semaforo5_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo5_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>
              <%-- <asp:LinkButton runat="server" OnClick="lnk_semafor_Click" Text="Laborales" ID="lnk_to_laborales">
                    <span id="Semaforo6_v" class="semaforo_img prosiga" runat="server" visible="false" />
                    <span id="Semaforo6_r" class="semaforo_img alto" runat="server" visible="true" />
                </asp:LinkButton>--%>
              
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <section class="panel">
        <header class="panel-heading">
           Clave despacho
        </header>
           <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:UpdatePanel ID="upd_id" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_despacho_id" runat="server" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>

     
    <!-----------------------------
                    PANEL PERSONALES
            --------------------------------->

    <asp:UpdatePanel runat="server" ID="upnl_generales" UpdateMode="Conditional">
        <ContentTemplate>

            <section class="panel" runat="server" id="panel_generales">
                <header id="head_panel_generales" class="panel_header_folder panel-heading">
                    <span>Personales</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_generales">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_generales">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                     <!--NOMBRES-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_razonsocial" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Razón social</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_nombre1" runat="server" ControlToValidate="txt_nombre1"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre1" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre1">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_comercial" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Nombre comercial</span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_nombre2" runat="server" Enabled="True"
                                        FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre2">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                 <%--<div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="TextBox3" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Titular</span>
                                        
                                    </div>
                                </div>--%>
                                <div class="text_input_nice_div module_subsec_elements_content">
                                     <asp:TextBox ID="txt_rfc" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*RFC</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_rfc" runat="server" ControlToValidate="txt_rfc"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_rfc" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Numbers" TargetControlID="txt_rfc">
                                        </ajaxToolkit:FilteredTextBoxExtender>              </div>
                                </div>
                            </div>
                        </div>

                    
                        <!--CURP Y RFC-->
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre1" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Primer nombre</span>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nombre1"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>                            
                                      </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_nombre2" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Segundo(s) Nombre(s)</span>
                                   
                                       </div>
                                </div>
                            </div>
                                  <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_paterno" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Apellido Paterno</span>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_paterno"
                                            CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona">
                                        </asp:RequiredFieldValidator>  
                                       </div>
                                </div>
                            </div>
                                 <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_materno" class="text_input_nice_input" runat="server" MaxLength="13"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Apellido Materno</span>
                                   
                                       </div>
                                </div>
                            </div>
                        </div>
                                               
                        <!--NOTAS-->
                        <div class="module_subsec low_m">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_notas" runat="server" class="text_input_nice_textarea" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                    <span class="text_input_nice_label">Notas:</span>
                                </div>
                            </div>
                        </div>

                        <p align="center">
                            <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        </p>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_guardar_p" class="btn btn-primary" runat="server"  ValidationGroup="val_Persona" Text="Guardar" AutoPostBack="False" />
                        </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_p" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    


    <!-----------------------------
                    PANEL DOMICILIO
            --------------------------------->
    <asp:UpdatePanel ID="upnl_domicilio" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_domicilio">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_domicilio">
                    <span>Domicilio</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_domicilio">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_domicilio">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                
                                <!--TIPO VIALIDAD, CALLE-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_vialidad" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                             
                                            <div class="text_input_nice_labels">
                                              <span class="text_input_nice_label">*Tipo de vialidad:</span>
                                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_vialidad" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_calle" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Calle:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_calle"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                    FilterType="LowercaseLetters, UppercaseLetters, Custom, Numbers" ValidChars=" " TargetControlID="txt_calle">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <!--CP-->
                                <div class="module_subsec columned three_columns ">
                                     <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_exterior" class="text_input_nice_input" runat="server" MaxLength="4"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Número exterior:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_exterior"
                                                    CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                    FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_exterior">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:TextBox ID="txt_interior" class="text_input_nice_input" runat="server" MaxLength="4"></asp:TextBox>
                                            <span class="text_input_nice_label">Número interior:</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                FilterType="Numbers, LowercaseLetters, UppercaseLetters" TargetControlID="txt_interior">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="module_subsec_elements align_items_flex_end">
                                        <div class="text_input_nice_div module_subsec_elements_content" style="margin-right: 20px;">
                                            <asp:TextBox ID="txt_cp" runat="server" class="text_input_nice_input" MaxLength="5" AutoPostBack="True" OnTextChanged="txt_cp_TextChanged"></asp:TextBox>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Código postal:</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_cp" runat="server" ControlToValidate="txt_cp"
                                                    CssClass="alertaValidator text_input_nice_error" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_PersonaDir">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBox_cp" runat="server" Enabled="True"
                                                    TargetControlID="txt_cp" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <asp:ImageButton ID="btn_buscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px" AutoPostBack="False" />
                                    </div>
                                </div>

                                <!--LUGAR DE NACIMIENTO PAIS, ESTADO, ASENTAMIENTO-->
                                <div class="module_subsec low_m columned three_columns">
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_estado" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Estado:</span>
                                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_estado" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_municipio" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Municipio:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_municipio" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_municipio" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_asentamiento" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <div class="text_input_nice_labels">
                                                <span class="text_input_nice_label">*Asentamiento:</span>
                                                <asp:RequiredFieldValidator runat="server" ID="req_asentamiento" CssClass="alertaValidator"
                                                    ControlToValidate="cmb_asentamiento" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                    ValidationGroup="val_PersonaDir" InitialValue="-1" />
                                            </div>
                                        </div>
                                    </div>
                                  
                                </div>
                                
                                <!--REFERENCIAS-->
                                <div class="text_input_nice_div module_sec">
                                    <asp:TextBox ID="txt_referencias" runat="server" class="text_input_nice_textarea" TextMode="MultiLine"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" TargetControlID="txt_referencias" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <span class="text_input_nice_label">Referencias:</span>
                                </div>

                                <!--TIPO VIVIENDA-->
                                <div class="module_subsec low_m columned three_columns">
                                    <%--<div class=" module_subsec_elements">
                                        <div class="text_input_nice_div module_subsec_elements_content">
                                            <asp:DropDownList ID="cmb_tipoviv" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="False">
                                                <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                                <asp:ListItem Value="PRO">PROPIA</asp:ListItem>
                                                <asp:ListItem Value="FAM">FAMILIAR</asp:ListItem>
                                                <asp:ListItem Value="REN">RENTADA</asp:ListItem>
                                                <asp:ListItem Value="PRE">PRESTADA</asp:ListItem>
                                                <asp:ListItem Value="HIP">HIPOTECADA</asp:ListItem>
                                                <asp:ListItem Value="PAG">PAGANDOLA</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>--%>
                                    
                                    
                                </div>

                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_status_dom" runat="server" CssClass="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar_domicilio" class="btn btn-primary" runat="server" ValidationGroup="val_PersonaDir" Text="Guardar" AutoPostBack="False" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_guardar_domicilio" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-----------------------------
                    PANEL CONTACTO
            --------------------------------->
<asp:UpdatePanel ID="upnl_contacto" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="panel" runat="server" id="panel_contacto">
                <header class="panel_header_folder panel-heading" runat="server" id="head_panel_contacto">
                    <span>Contacto</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_contacto">&rsaquo;</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content" runat="server" id="content_panel_contacto">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                          <h5 style="font-weight: normal" class="module_subsec resalte_azul">Primer Contacto</h5>

                                 <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c1_nombre" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Primer nombre</span>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c1_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c1_seg_nombre" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Segundo(s) nombre(s)</span>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c1_seg_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c1_paterno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Apellido paterno</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c1_paterno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c1_materno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Apellido materno</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c1_materno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                      <h5 style="font-weight: normal" class="module_subsec resalte_azul">Teléfono</h5>

                           
                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Celular:</span>
                                    <asp:TextBox ID="txt_c1_lada" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c1_lada">
                                    </ajaxToolkit:FilteredTextBoxExtender>

                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_c1_tel" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c1_tel">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>


                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Trabajo: &nbsp; &nbsp;</span>
                                    <asp:TextBox ID="txt_c1_lada_ofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c1_lada_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_c1_tel_ofi" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c1_tel_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                                    <asp:TextBox ID="txt_c1_ext_ofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c1_ext_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                        
                            <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Correo:</span>
                                    <asp:TextBox ID="txt_c1_email" class="module_subsec_elements module_subsec_bigbig-elements text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" class="alertaValidator bold"
                                        ControlToValidate="txt_c1_email" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                        ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                                    </asp:RegularExpressionValidator>
                         
                                    &nbsp; &nbsp;
                                 </div>
                         
                              
                             <h5 style="font-weight: normal" class="module_subsec resalte_azul">Segundo Contacto</h5>
                                        
                      <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c2_nombre" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Primer nombre</span>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c2_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c2_seg_nombre" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <span class="text_input_nice_label">Segundo(s) nombre(s)</span>
                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c2_seg_nombre">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c2_paterno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Apellido paterno</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c2_paterno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_c2_materno" class="text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">Apellido materno</span>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_c2_materno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                              
                                <!--TELEFONO-->

                                <h5 style="font-weight: normal" class="module_subsec resalte_azul">Teléfono</h5>

                             

                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Celular:</span>
                                    <asp:TextBox ID="txt_c2_lada" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c2_lada">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_c2_tel" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c2_tel">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>


                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Trabajo: &nbsp; &nbsp;</span>
                                    <asp:TextBox ID="txt_c2_lada_ofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="3"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c2_lada_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <asp:TextBox ID="txt_c2_tel_ofi" class="module_subsec_elements module_subsec_big-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c2_tel_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    &nbsp; &nbsp;
                            <span class="module_subsec_elements module_subsec_small-elements title_tag">Extensión:</span>
                                    <asp:TextBox ID="txt_c2_ext_ofi" class="module_subsec_elements module_subsec_small-elements text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txt_c2_ext_ofi">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </div>

                                <!--CORREO-->

                             
                                <div class="module_subsec columned low_m align_items_flex_center">
                                    <span class="module_subsec_elements module_subsec_small-elements title_tag">Correo:</span>
                                    <asp:TextBox ID="txt_c2_email" class="module_subsec_elements module_subsec_bigbig-elements text_input_nice_input" runat="server" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" class="alertaValidator bold"
                                        ControlToValidate="txt_c2_email" Display="Dynamic" ErrorMessage=" Correo Incorrecto!"
                                        ValidationExpression="^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                                    </asp:RegularExpressionValidator>
                                    
                                    &nbsp; &nbsp;
                                 </div>
                                   
                                <div class="module_subsec flex_center">
                                    <asp:Label ID="lbl_statustel" runat="server" CssClass="alerta"></asp:Label>
                                </div>

                                <div class="module_subsec flex_end">
                                    <asp:Button ID="btn_guardar_c" class="btn btn-primary" runat="server" ValidationGroup="val_PersonaCon" Text="Guardar" AutoPostBack="False" />
                                </div>
                 
              </ContentTemplate>
                        <Triggers>
                            
                            <asp:AsyncPostBackTrigger ControlID="btn_adduser"/>
                           
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </section>
  </ContentTemplate>

    </asp:UpdatePanel>
    
    

    
    <asp:UpdatePanel ID="upnl_users" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <section class="panel" runat="server" id="panel_users">
             <header class="panel_header_folder panel-heading" runat="server" id="head_panel_users">
                    <span>Asignación de usuarios</span>
                    <span class=" panel_folder_toogle up" runat="server" id="toggle_panel_users">&rsaquo;</span>
                </header>
            <div class="panel-body">
                <div class="panel-body_content" runat="server" id="content_panel_users">

                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                        <ContentTemplate>
                  
                            <div class="module_subsec">
                <div class="overflow_x shadow" style="flex: 1;">
                    <asp:GridView ID="dag_users" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                        <Columns>
                            <asp:BoundField  DataField="ID" HeaderText="Id Usuario">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField  DataField="USUARIO" HeaderText="Usuario">
                                <ItemStyle Width="85%"></ItemStyle>
                            </asp:BoundField>                             
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_PagAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
                </div>
            </div>
                            

                            <div align="center">
                                <asp:Label ID="lbl_status_user" runat="server" CssClass="alerta"></asp:Label>
                            </div>

                            <div class="module_subsec flex_end">
                                <asp:Button ID="btn_adduser" class="btn btn-primary"  ValidationGroup="val_PersonaLab" runat="server" Text="Guardar" AutoPostBack="False" />
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            
                            <asp:AsyncPostBackTrigger ControlID="btn_adduser"/>
                           
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </section>
  </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
