<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_EMERGENCIA.aspx.vb" Inherits="SNTE5.CRED_EXP_EMERGENCIA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <section class="panel">
        <header class="panel-heading">
            <span>Autorización de emergencias médicas</span>
        </header>

        <div class = "panel-body">

             <div class="module_subsec columned low_m three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_Instituciones" CssClass="btn btn-primary2 dropdown_label" runat="server" AutoPostBack="true"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">*Institución:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_NumCtrl" runat="server" ControlToValidate="ddl_Instituciones"
                                        CssClass="textogris" Display="Dynamic" InitialValue="-1" ErrorMessage=" Falta Dato!" ValidationGroup="val_Depe_NumCtrl"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>                
            </div>


            <div class="overflow_x shadow ">
                <!-- Tabla de Expedientes generados por sucursal -->
                <asp:GridView  ID="DAG_Analisis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundField DataField="FOLIO" HeaderText="No. control" Visible="false">
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CLAVE" HeaderText="Clave">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CLIENTE" HeaderText="Cliente">
                            <ItemStyle Width="25%"></ItemStyle>
                        </asp:BoundField>                       
                        <asp:BoundField DataField="NUMCLIENTE" HeaderText="Núm. cliente"  Visible="false">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>   
                         <asp:BoundField DataField="NUMCTRL" HeaderText="No. control">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MONTO" HeaderText="Monto">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MONTOREAL" HeaderText="Monto Real">
                            <ItemStyle Width="12%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PLAZO" HeaderText="Plazo">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>                       
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha solicitud">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                      
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Emergencia Médica">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Aplicado" runat="server" AutoPostBack="false"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                               <asp:label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>              
            </div>

              <div class= "module_subsec low_m columned four columns top_m flex_end">
                <asp:Button ID="btn_emergencia" runat="server" class="btn btn-primary" Text="Emergencia Médica" Visible = "TRUE"/>              
            </div>

            <div align  ="center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>                
            </div>

        </div>
        </section>
    </asp:Content>

