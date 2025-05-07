<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CAP_EXP_PROVEEDORREQ.aspx.vb" Inherits="SNTE5.CAP_EXP_PROVEEDORREQ" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function busquedapersonafisica() {
            var wbusf = window.showModalDialog("/UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }
    </script>

    <section class="panel" id="panel_datos_pagos">
        <header class="panel-heading">
            <span>
                <asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false"></asp:Label></span>
        </header>

        <div class="panel-body">
            <div class="panel-body_content">
                <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                    <asp:TextBox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                </div>
                <div class="module_subsec columned low_m align_items_flex_center">
                    <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                    <asp:TextBox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:TextBox>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" id="panel_avales">
        <header class="panel-heading">
            <span>Proveedores de recursos</span>
        </header>
        <div class="panel-body">
            <asp:UpdatePanel ID="upd_provrec" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <%--INICIA EL DATAGRID--%>
                    <asp:Label ID="lbl_count" runat="server" CssClass="module_subsec"></asp:Label>

                    <div class="overflow_x shadow module_subsec">
                        <asp:DataGrid ID="dag_Proveedor" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            CssClass="table table-striped" GridLines="None" TabIndex="17">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="IDPROVEEDOR" HeaderText="Número de Afiliado">
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre(s)">
                                    <ItemStyle Width="300px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RELACION" HeaderText="Relación">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                                    <ItemStyle Width="50px" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>


                    <div class="module_subsec">
                        <asp:Label ID="lbl_mensaje" runat="server" CssClass="texto" Text="¿Desea buscar una persona guardada en la base de datos? Dé clic "></asp:Label>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnk_busqueda" runat="server" CssClass="textogris" Text="aquí"></asp:LinkButton>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox runat="server" MaxLength="8" ID="txt_IdCliente" class="text_input_nice_input"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_numaval" runat="server" CssClass="text_input_nice_label" Text="*Número de afiliado:"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" FilterType="Numbers" Enabled="True"
                                        TargetControlID="txt_idCliente" ID="FilteredTextBoxExtender_idcliente">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_idcliente" runat="server"
                                        ControlToValidate="txt_idcliente" CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="Proveedorexistente"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_tipo_relacion" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_relacion" runat="server" CssClass="text_input_nice_label" Text="*Tipo de relación:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_relacion" runat="server" ControlToValidate="cmb_tipo_relacion"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="Proveedorexistente"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_alerta" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_alertacodeudor" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_alertadependiente" runat="server" CssClass="alerta"></asp:Label>
                        <asp:Label ID="lbl_alertaconsejo" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">
                        <asp:Button ID="btn_agregar" runat="server" class="btn btn-primary" Text="Agregar" ValidationGroup="Proveedorexistente" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
</asp:Content>


