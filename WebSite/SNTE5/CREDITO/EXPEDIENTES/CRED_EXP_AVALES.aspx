<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_AVALES.aspx.vb" Inherits="SNTE5.CRED_EXP_AVALES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

    function busquedapersonafisica() {
        var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
        if (wbusf != null) {
            document.getElementById("hdn_busqueda").value = wbusf;
            __doPostBack('', '');
        }
    }

    function ClickBotonBusqueda(ControlTextbox, ControlButton) {
        var CTextbox = document.getElementById(ControlTextbox);
        var CButton = document.getElementById(ControlButton);
        if (CTextbox != null && CButton != null) {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                if (CTextbox.value != "") {
                    CButton.click();
                    CButton.disabled = true;
                }
                else {
                    //alert('Ingrese Datos')
                    CTextbox.focus()
                    return false
                }
                //CTextbox.focus();
                return true
            }
        }
    }
    </script>
    
         <section class="panel" id="panel_datos_pagos">
            <header class="panel-heading">
                 <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
            </header>

            <div class="panel-body">
                     <div class="panel-body_content">
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Agremiado:</h5>
                            <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                        <div class="module_subsec columned low_m align_items_flex_center">
                            <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                            <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
                        </div>
                    </div>
             </div>
        </section>

        <section class="panel" id="panel_avales">
            <header class="panel-heading">
                <span>Avales</span>
            </header>
            <div class="panel-body">

                    <asp:Label ID=  "lbl_maxavales" runat="server" CssClass="module_subsec"></asp:Label>
                    
                 <%--INICIA EL DATAGRID--%>
                <div class="overflow_x shadow module_subsec">
                    <asp:DataGrid ID="DAG_aval" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                       GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="idaval" HeaderText="" visible="false"></asp:BoundColumn>

                                <asp:BoundColumn DataField="numtrab" HeaderText="Núm. agremiado">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>

                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="edad" HeaderText="Edad">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="lugarnac" HeaderText="Lugar de nacimiento">
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="Eliminar" Text="Eliminar">
                                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:ButtonColumn>
                          </Columns>
                    </asp:DataGrid><br />
                </div>

                <div class="module_subsec">                                                
                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar un agremiado guardado en la base de datos? Dé clic"></asp:Label> &nbsp;&nbsp;
                    <asp:LinkButton ID="lnk_busqueda" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>                       
                </div> 

                <div class= "module_subsec low_m columned three_columns">
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
                            <asp:TextBox runat="server" MaxLength="10" ID="txt_IdCliente1" class="text_input_nice_input"></asp:TextBox>
                            <asp:TextBox runat="server" MaxLength="10" ID="txt_IdCliente" class="text_input_nice_input" Visible="false"></asp:TextBox>
                                <div class="text_input_nice_labels">

                                <asp:Label ID="lbl_numaval" runat="server" CssClass="text_input_nice_label"
                                Text="*Número de control:" Width="128px"></asp:Label>

                                <ajaxToolkit:FilteredTextBoxExtender runat="server" FilterType="Numbers" Enabled="True"
                                    TargetControlID="txt_idCliente" ID="FilteredTextBoxExtender__idcliente">
                                </ajaxToolkit:FilteredTextBoxExtender>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_idcliente" runat="server" ControlToValidate="txt_idcliente1"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_relacion" runat="server" class="btn btn-primary2 dropdown_label">
                                        </asp:DropDownList>                                          
                                    <div class="text_input_nice_labels"> 
                                        <asp:Label ID="lbl_relacion" runat="server" CssClass="text_input_nice_label" Text="*Tipo de relación:"
                                        Width="129px"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_relacion" runat="server" ControlToValidate="cmb_relacion"
                                            CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                                    </div>
                        </div>
                    </div>
                </div>
               

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_agregar" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="val_busqueda" />
                </div>

                    <div align="center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div align="center">
                        <asp:Label ID="lbl_alerta" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div align="center">
                        <asp:Label ID="lbl_alertacodeudor" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div align="center">
                        <asp:Label ID="lbl_alertadependiente" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div align="center">
                        <asp:Label ID="lbl_alertaconsejo" runat="server" CssClass="alerta"></asp:Label>
                    </div>
        
    <input type="hidden" name="hdn_busqueda" id="hdn_busqueda" value="" runat="server" />

        </div>
       </section>
      
</asp:Content>