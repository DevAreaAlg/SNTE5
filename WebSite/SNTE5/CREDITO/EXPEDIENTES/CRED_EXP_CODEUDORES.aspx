<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_CODEUDORES.aspx.vb" Inherits="SNTE5.CRED_EXP_CODEUDORES" MaintainScrollPositionOnPostback ="true" %>

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
    
        function busquedaCP() {
            var wbusf = window.showModalDialog("~/UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
   </script>
   
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

            <section class="panel" id="panel_avales">
                <header class="panel-heading">
                    <span>Codeudores</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">

                   <asp:Label ID="lbl_codeudores" runat="server" cssclass="textogris module_subsec" ></asp:Label>
    
             <%--INICIA EL DATAGRID--%>
            <div class="overflow_x shadow module_subsec">
                <asp:DataGrid ID="DAG_codeudor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                 GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="idcodeudor" HeaderText="Núm. persona">
                            <ItemStyle Width="20%"  HorizontalAlign="Center"/>
                        </asp:BoundColumn>   
                        <asp:BoundColumn DataField="nombre" HeaderText="Nombre(s)">
                            <ItemStyle Width="25%"  HorizontalAlign="Center"/>
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="edad" HeaderText="Edad">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="lugarnac" HeaderText="Lugar de nacimiento">
                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
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
                <asp:LinkButton ID="lnk_busqueda" runat="server" Cssclass="textogris" Text="aquí"></asp:LinkButton>               
            </div>

                <div class= "module_subsec low_m columned three_columns">
                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:TextBox runat="server" MaxLength="8" ID="txt_IdCliente" class="text_input_nice_input"></asp:TextBox>
                             <div class="text_input_nice_labels">
                                 <asp:Label ID="lbl_numcodeudor" runat="server" CssClass="texto" Text="*Id de la persona:" Width="128px"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" 
                                    FilterType="Numbers" Enabled="True" TargetControlID="txt_idCliente" 
                                    ID="FilteredTextBoxExtender__idcliente"></ajaxToolkit:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_idcliente" runat="server" ControlToValidate="txt_idcliente"
                                    Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                    ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                             </div>
                        </div>
                    </div>

                    <div class="module_subsec_elements"> 
	                    <div class="text_input_nice_div module_subsec_elements_content">
                            <asp:DropDownList ID="cmb_relacion" runat="server" class="btn btn-primary2 dropdown_label"> </asp:DropDownList>
                            <div class="text_input_nice_labels">
                                <asp:Label ID="lbl_relacion" runat="server" CssClass="text_input_nice_label" Text="*Tipo de Relación:"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_relacion" runat="server" ControlToValidate="cmb_relacion"
                                Cssclass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_busqueda"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="module_subsec flex_end">
                    <asp:Button ID="btn_agregar" runat="server"  class="btn btn-primary" Text="Agregar" ValidationGroup="val_busqueda"/>
                </div>

                <div align="center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div align="center">
                    <asp:Label ID="lbl_alerta" runat="server" CssClass="alerta"></asp:Label>
                </div>

                <div align="center">
                    <asp:Label ID="lbl_alertaaval" runat="server" CssClass="alerta"></asp:Label>
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
