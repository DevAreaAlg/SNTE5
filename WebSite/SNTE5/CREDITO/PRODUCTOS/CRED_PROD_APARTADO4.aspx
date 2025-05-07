<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_PROD_APARTADO4.aspx.vb" Inherits="SNTE5.CRED_PROD_APARTADO4" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span>Producto</span>
        </header>
        <div class="panel-body">                   
            <div class="module_subsec columned low_m align_items_flex_center">
                <asp:TextBox ID="lbl_Producto" runat="server" Text="Label" Enabled="false" CssClass=" no_bm module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_datos">                 
        <header class="panel-heading">
            <span>Parámetros contables</span>
        </header>                       
        <div class="panel-body">           
            <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
                <ContentTemplate> 

                    <span class="text_input_nice_label title_tag module_subsec">*¿Qué cuenta será usada como cuenta de capital vigente para este producto?</span>

                    <div class="module_subsec no_m columned three_columns" >
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <div class="btn-group min_w">          
                                    <asp:DropDownList ID="cmb_CUENTAS" runat="server" class="btn btn-primary2 dropdown_label" style="text-align:center">                
                                    </asp:DropDownList>   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cuentas" runat="server" ControlToValidate="cmb_CUENTAS" 
                                    Cssclass="alertaValidator bold" Display="Dynamic" ErrorMessage=" Falta Dato!"  
                                    ValidationGroup="val_cuentas" InitialValue="-1"></asp:RequiredFieldValidator>   
                                </div>                                              
                            </div>
                        </div>  
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_ctacont" runat="server" CssClass="alerta"></asp:Label>
                    </div>
                                
                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_guardacobro" class="btn btn-primary" runat="server"  OnClick="btn_guardacobro_Click" ValidationGroup="val_cuentas" Text="Guardar" />
                    </div> 
                                            
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_guardacobro" />
                </Triggers>
            </asp:UpdatePanel>                                  
        </div>
                       
    </section>
           
</asp:Content>

