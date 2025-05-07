<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_DOCUMENTOS_CREACION.aspx.vb" Inherits="SNTE5.CRED_CNF_DOCUMENTOS_CREACION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <asp:UpdatePanel runat="server" >
            <ContentTemplate>
                <section class="panel" >
                    <header class="panel-heading ">
                        <span>Documento</span>
                    </header>
                    <div class="panel-body">
                            <div class="module_subsec no_column align_items_flex_center">
                        <span style="margin-right:20px">Número de documento:</span>
                    <div class="module_subsec_elements" style="flex:1">
                            <asp:TextBox ID="txt_id" runat="server" Enabled="false" class="text_input_nice_input"></asp:TextBox>                                          
                    </div>
                </div> 
                    </div>
                </section>

                <section class="panel" >
                    <header class="panel-heading panel_header_folder">
                        <span>Datos Generales</span>
                        <span class="panel_folder_toogle down">&rsaquo;</span>
                    </header>
                    <div class="panel-body">
                        <div class="panel-body_content init_show">
                            <!--------------------------------------fila------------------------------------------------------------------------------>
                            <div class="module_subsec three_columns columned low_m align_items_flex_center" >
                                <div id="div_estatus" runat="server" class="module_subsec_elements module_subsec no_column no_m">
                                    <span class="module_subsec_elements title_tag no_tbm">Activo:</span>
                                    <asp:CheckBox ID="checkbox_activo" CssClass="mod_check" runat="server" />
                                </div>
                                <span id="lbl_inEdi" runat="server" class="resalte_rojo module_subsec_elements">
                                    Documento en edición o validación.
                                </span>
                                <div  class="module_subsec_elements vertical no_tbm">
                                    <div>
                                        <span class="title_tag">Tipo de documento</span>
                                        <asp:RequiredFieldValidator runat="server" CssClass="resalte_rojo" Display="Dynamic" Text="Falta Dato" ControlToValidate="cmb_tipoDoc" ValidationGroup="datos_generales" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:DropDownList runat="server" ID="cmb_tipoDoc" CssClass="btn btn-primary2">
                                        <asp:ListItem Value="-1" Text="ELIJA" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="PAGARE" Text="PAGARÉ"></asp:ListItem>
                                        <asp:ListItem Value="CONTRATO" Text="CONTRATO"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                </div>
                            <!----------------------------------------------fila---------------------------------------------------------------------->
                            <div class="module_subsec three_columns columned low_m">
                                <div class="module_subsec_elements text_input_nice_div">
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">
                                            *Nombre:
                                        </span>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txt_nombre" FilterType="LowercaseLetters, UppercaseLetters, Numbers,Custom"  ValidChars="_ -" ></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_nombre" ValidationGroup="datos_generales" Display="Dynamic" CssClass="resalte_rojo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div id="div_clave" runat="server" class="module_subsec_elements text_input_nice_div" >
                                    <asp:TextBox ID="txt_clave" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">
                                            *Clave:
                                        </span>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txt_clave" FilterType="LowercaseLetters, UppercaseLetters, Numbers,Custom"  ValidChars="_ -" ></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_clave" ValidationGroup="datos_generales" Display="Dynamic" CssClass="resalte_rojo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                    <div id="div_TIPO_COBRO" runat="server" class="module_subsec_elements text_input_nice_div" >
                                    <asp:TextBox ID="txt_TIPO_COBRO" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">
                                            *Tipo de cobro:
                                        </span>
                                        <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txt_TIPO_COBRO" FilterType="LowercaseLetters, UppercaseLetters, Numbers,Custom"  ValidChars="_ -" ></ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_TIPO_COBRO" ValidationGroup="datos_generales" Display="Dynamic" CssClass="resalte_rojo"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <!-----------------------------------------------fila--------------------------------------------------------------------->
                                <div class="module_subsec three_columns columned">
                                <div class="module_subsec_elements vertical">
                                    <span style="margin-bottom:10px;">Última actualización realizada por: <asp:Label ID="lbl_modificadox" runat="server" CssClass="resalte_verde"></asp:Label></span>
                                    <span>Fecha de última actualización: <asp:Label ID="lbl_fecha_mod" runat="server" CssClass="resalte_verde" Text="Label"></asp:Label></span>
                                </div>
                                       
                            </div>
                            <!--------------------------------------------acaba panel-------------------------------------------------------------------->
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>


