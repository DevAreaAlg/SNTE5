<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_VEN_AMORTIZACION_MULTIPLE.aspx.vb" Inherits="SNTE5.CRED_VEN_AMORTIZACION_MULTIPLE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <section class="panel">
            <header class="panel-heading">
                <span>Amortización a Préstamo</span>
            </header>
            <div class="panel-body">           

                    <div class="module_subsec columned low_m align_items_flex_start" >
                            <asp:Label ID="lbl_CargaArch" runat="server" CssClass="text_input_nice_label" 
                                Text="Seleccionar archivo de pago:"></asp:Label>
                            <asp:FileUpload ID="AsyncFileUpload1" runat="server" />
                            <asp:Label ID="lbl_Permit" runat="server" CssClass="module_subsec_elements module_subsec_medium-elements title_tag" Text="Nota: Sólo archivos *.csv"></asp:Label>
                    </div><br />

                    <div class="module_subsec flex_center">
                        <asp:Button ID="btn_CargarArch" runat="server"  class="btn btn-primary" Text="Cargar" ValidationGroup="val_CargarAmort"/>
                    </div>

                    <asp:Panel ID="pnl_Pagos" runat="server" Visible="False">
                        <div class="module_subsec columned low_m align_items_flex_center" >
                            <asp:Label ID="lbl_banco" runat="server" CssClass="module_subsec_elements module_subsec_small-elements title_tag" Text="*Banco:" ></asp:Label>
                            <asp:DropDownList ID="cmb_banco" CssClass="module_subsec_elements module_subsec_big-elements btn btn-primary2 dropdown_label" runat="server"
                                ValidationGroup="val_AplicarAmort" Enabled="False">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbbancos"
                                CssClass="textogris" ControlToValidate="cmb_banco" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_AplicarAmort" InitialValue="-1" />
                        </div>

                        <div class="module_subsec">
                            <div class="overflow_x shadow w_100">
                                <asp:DataGrid ID="dag_Pagos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                    GridLines="None" Width="100%">
                                    <HeaderStyle CssClass="table_header" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_CLIENTE" HeaderText="No. Afiliado">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                            <ItemStyle Width="300px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ABONO" HeaderText="Abono">
                                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ERROR">
                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AUXERROR" Visible="false">
                                            <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>                        

                        <div class="module_subsec flex_center">
                            <asp:Button ID="btn_Aplicar" runat="server" class="btn btn-primary" Text="Aplicar" Enabled="False"
                                ValidationGroup="val_AplicarAmort"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Cancelar" runat="server" class="btn btn-primary" Enabled="False" 
                                Text="Cancelar" />
                        </div>
                    </asp:Panel>

                    <div align="center">
                        <asp:Label ID="lbl_Status_Carga" runat="server" CssClass="alerta"></asp:Label>
                    </div>

            </div>
          
        </section>
        
</asp:Content>
