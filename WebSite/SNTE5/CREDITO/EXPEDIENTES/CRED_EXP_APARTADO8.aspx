<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_APARTADO8.aspx.vb" Inherits="SNTE5.CRED_EXP_APARTADO8" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="panel">
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Prellenado de pagaré y contrato</span>
        </header>
        <div class="panel-body">
                        
                <h5><label id="lbl_pagare" class="resalte_azul module_subsec">Prellenar pagaré:</label></h5>                    
                                               
                <div class="module_subsec columned two_columns ">
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_FolioPagare" class="btn btn-primary2 dropdown_label" runat="server"></asp:DropDownList>

                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Selecciona folio de pagaré:</span>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_FolioPagare"
                                    CssClass="textogris" ControlToValidate="cmb_FolioPagare"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_pagare" InitialValue="0" />
                            </div>
                        </div>
                    </div>
                    <div class=" module_subsec_elements">
                        <div class="module_subsec_elements_content">
                            <asp:Button ID="btn_Pagare" runat="server" class="btn btn-primary" Text="Generar" ValidationGroup="val_pagare" />
                        </div>
                    </div>
                </div>

                <div align="center">
                    <h3><asp:Label runat="server" CssClass="alerta" ID="lbl_EstatusPagare"></asp:Label></h3>
                </div>
                     
                <h5><label id="Label1" class="resalte_azul module_subsec">Prellenar contrato:</label></h5>
                    
                <div class="module_subsec columned two_columns ">
                    <div class=" module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_FormatoContrato" class="btn btn-primary2 dropdown_label" runat="server" ValidationGroup="val_Contrato"></asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <span class="text_input_nice_label">Selecciona contrato:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FormatoContrato" runat="server"
                                    ControlToValidate="cmb_FormatoContrato" CssClass="textogris"
                                    Display="Dynamic" ErrorMessage=" Falta Dato!" InitialValue="-1"
                                    ValidationGroup="val_Contrato" />
                            </div>
                        </div>
                    </div>
                    <div class=" module_subsec_elements ">
                        <asp:Label ID="lbl_clasi" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Button ID="btn_Contrato" runat="server" class="btn btn-primary" ValidationGroup="val_Contrato" Text="Generar" />                                    
                    </div>
                </div>

                <div class="module_subsec">
                    <asp:Label ID="lbl_status_contrato" runat="server" CssClass="alerta"></asp:Label>                        
                </div>
              

        </div>
    </section>
       
</asp:Content>

