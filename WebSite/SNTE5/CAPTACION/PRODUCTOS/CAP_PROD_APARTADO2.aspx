<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_PROD_APARTADO2.aspx.vb" Inherits="SNTE5.CAP_PROD_APARTADO2" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_refybene">
        <header class="panel-heading">
            <span>Creación/Asignación de tasa ordinaria</span>

        </header>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h5 class="module_subsec">Tasa ordinaria fija (anual) </h5>
                    <div class="module_subsec_elements module_subsec no_column align_items_flex_center">
                        <span class="module_subsec_elements">Activa:</span>
                        <asp:CheckBox ID="chk_estatus_normal_fija" Text=" " CssClass="textocajas module_subsec_elements" runat="server" TextAlign="Left" />
                    </div>
                    <div class="module_subsec columned three_columns no_m">
                        <div class="module_subsec_elements">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_div ">
                                    <asp:TextBox ID="txt_tasa_normal_fija_min" runat="server" MaxLength="5" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Tasa mínima %:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasa_normal_fija_min" runat="server"
                                            ControlToValidate="txt_tasa_normal_fija_min" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_taza_fija"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" ValidChars="."
                                            TargetControlID="txt_tasa_normal_fija_min">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1" runat="server" Display="Dynamic" class="textogris"
                                            ControlToValidate="txt_tasa_normal_fija_min" ErrorMessage=" Tasa Incorrecta!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class=" module_subsec_elements">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_div ">
                                    <asp:TextBox ID="txt_tasa_normal_fija_max" runat="server" MaxLength="5" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Tasa máxima %:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasa_normal_fija_max" runat="server"
                                            ControlToValidate="txt_tasa_normal_fija_max" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_taza_fija"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers" ValidChars="."
                                            TargetControlID="txt_tasa_normal_fija_max">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                            class="textogris" ControlToValidate="txt_tasa_normal_fija_max" ErrorMessage=" Tasa Incorrecta!"
                                            ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusnorfij" runat="server" CssClass="alerta module_subsec flex_center align_items_flex_center flex_1"></asp:Label>
                    </div>
                    <div class="module_subsec low_m  align_items_flex_center flex_end">

                        <asp:Button ID="btn_guardarnormfija" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_taza_fija" />
                    </div>


                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardarnormfija" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <h5 class="module_subsec">Tasa ordinaria indizada (anual) </h5>

                    <div class="module_subsec_elements module_subsec no_column align_items_flex_center">
                        <span class="module_subsec_elements">Activa:</span>
                        <asp:CheckBox ID="chk_estatus_normal_ind" Text="" CssClass="textocajas module_subsec_elements" runat="server" TextAlign="Left" />
                    </div>


                    <div class="module_subsec columned low_m three_columns ">
                        <div class=" module_subsec_elements">
                            <div class="module_subsec_elements_content vertical">

                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Índice: </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_indice_normal_ind" runat="server"
                                        ControlToValidate="cmb_indice_normal_ind" CssClass="textogris" Display="Dynamic"
                                        ErrorMessage=" Falta Dato!" ValidationGroup="val_taza_indi" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <asp:DropDownList ID="cmb_indice_normal_ind" runat="server" class="btn btn-primary2 dropdown_label">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class=" module_subsec_elements">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_div ">
                                    <asp:TextBox ID="txt_tasa_normal_ind_min" runat="server" MaxLength="5" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Puntos mínimos %:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasa_normal_ind_min" runat="server"
                                            ControlToValidate="txt_tasa_normal_ind_min" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_taza_indi"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            ValidChars="." TargetControlID="txt_tasa_normal_ind_min">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            class="textogris" ControlToValidate="txt_tasa_normal_ind_min" Display="Dynamic"
                                            ErrorMessage="Puntos Incorrectos!" ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class=" module_subsec_elements">
                            <div class="module_subsec_elements_content vertical">
                                <div class="text_input_nice_div ">
                                    <asp:TextBox ID="txt_tasa_normal_ind_max" runat="server" FilterType="Numbers" MaxLength="5" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <span class="text_input_nice_label">*Puntos máximos %:</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_tasa_normal_ind_max" runat="server"
                                            ControlToValidate="txt_tasa_normal_ind_max" CssClass="textogris" Display="Dynamic"
                                            ErrorMessage=" Falta Dato!" ValidationGroup="val_taza_indi"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"
                                            runat="server" Enabled="True" FilterType="Custom, Numbers"
                                            ValidChars="." TargetControlID="txt_tasa_normal_ind_max">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                            class="textogris" ControlToValidate="txt_tasa_normal_ind_max" Display="Dynamic"
                                            ErrorMessage="Puntos Incorrectos!" ValidationExpression="^[0-9]?[0-9]?[0-9]{1}(\.[0-9]{1}[0-9]?)?$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_statusnorind" runat="server" CssClass="alerta module_subsec flex_center align_items_flex_center flex_1"></asp:Label>
                    </div>
                    <div class="module_subsec align_items_flex_center low_m flex_end">
                        <asp:Button ID="btn_guardarnormind" runat="server" class="btn btn-primary" Text="Guardar" ToolTip="Guardar" ValidationGroup="val_taza_indi" />
                    </div>





                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardarnormind" />
                </Triggers>
            </asp:UpdatePanel>


        </div>
    </section>

</asp:Content>
