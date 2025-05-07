<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_DOCUMENTOS.aspx.vb" Inherits="SNTE5.CRED_CNF_DOCUMENTOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="panel">

                <div class="panel-body">
                    <div class="module_subsec no_m">
                        <div class="module_subsec columned low_m no_rm five_columns" style="flex: 1;">

                            <div class=" module_subsec_elements text_input_nice_div ">
                                <asp:TextBox ID="txt_nombre" runat="server" CssClass="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Nombre:</span>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" TargetControlID="txt_nombre" FilterType="LowercaseLetters, UppercaseLetters, Numbers,Custom" ValidChars="_ -"></ajaxToolkit:FilteredTextBoxExtender>
                                </div>


                            </div>
                            <div class=" module_subsec_elements vertical ">

                                <span>Tipo de documento:</span>

                                <asp:DropDownList ID="cmb_tipo_doc" runat="server" class="btn btn-primary2 " AutoPostBack="False">
                                    <asp:ListItem Text="ELIJA" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="PAGARÉ" Value="PAGARE"></asp:ListItem>
                                    <asp:ListItem Text="CONTRATO" Value="CONTRATO"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class=" module_subsec_elements vertical">
                                <span>Estatus:</span>

                                <asp:DropDownList ID="cmb_estatus" runat="server" class="btn btn-primary2 " AutoPostBack="False">
                                    <asp:ListItem Value="-1" Selected="true">ELIJA</asp:ListItem>
                                    <asp:ListItem Value="0">INACTIVO</asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                    <asp:ListItem Value="2" Text="EN EDICIÓN"></asp:ListItem>

                                </asp:DropDownList>

                            </div>


                        </div>
                        <div class="module_subsec low_m  no_lm" style="margin-bottom: 10px;">
                            <asp:Button ID="btn_buscarDoc" CssClass="btn btn-primary" OnClick="btn_buscarDoc_Click" runat="server" Text="Buscar" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_eliminarB" runat="server" Text="Eliminar" class="btn btn-primary" />

                        </div>
                    </div>
                    <div class="module_subsec flex_center">
                        <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
                    </div>
                </div>
            </section>

            <div class="panel" style="background-color: transparent; box-shadow: none;">
                <div class="flex_end">
                    <asp:Button ID="btn_crearDoc" CssClass="btn btn-primary" OnClick="btn_crearDoc_Click" runat="server" Text="Nuevo Documento" />
                </div>
            </div>
            <div class="panel overflow_x shadow">
                <asp:GridView ID="grdVw_busqueda" GridLines="None" runat="server" CssClass="table table-striped" OnRowCommand="grdVw_busqueda_RowCommand" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Id contrato/pagaré" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                        <asp:HyperLinkField Text="Ver Documento" DataNavigateUrlFields="URL" HeaderText="Vista previa" />
                        <asp:BoundField DataField="ESTATUS" HeaderText="Estatus" />
                        <asp:ButtonField Text="Editar" CommandName="EDITAR" />
                    </Columns>
                    <HeaderStyle CssClass="table_header" />
                </asp:GridView>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


