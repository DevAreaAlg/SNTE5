<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DOC_PLANTILLAS.aspx.vb" Inherits="SNTE5.COB_DOC_PLANTILLAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="panel">
        <header class="panel-heading">
            <span>Asignar plantillas</span>
        </header>
        <div class="panel-body">


            <div class="module_subsec">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:DropDownList ID="cmb_Eventos" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True"></asp:DropDownList>

                    <div class="text_input_nice_labels">
                        <asp:Label runat="server" class="text_input_nice_label" ID="lbl_folio">Eventos: </asp:Label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Eventos" CssClass="alertaValidator"
                                    ControlToValidate="cmb_Eventos" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_asignaPln" InitialValue="0" />
                    </div>
                </div>
            </div>

            <div class="module_subsec">
                <div class="overflow_x shadow" style="flex: 1;">
                    <asp:GridView ID="dag_AsigPlantillas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        Font-Size="10pt" GridLines="None" HorizontalAlign="Center" TabIndex="17">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="40" DataField="IDPLANTILLA" HeaderText="Id plantilla">
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE" HeaderText="Nombre">
                                <ItemStyle Width="90%"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_PagAsignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table_header" />
                    </asp:GridView>
                </div>
            </div>
                <div class="module_subsec flex_center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta" Text=""></asp:Label>
            </div>
            
            <div class="module_subsec flex_end">
                <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_asignaPln"/>
            </div>

        </div>
    </section>
</asp:Content>

