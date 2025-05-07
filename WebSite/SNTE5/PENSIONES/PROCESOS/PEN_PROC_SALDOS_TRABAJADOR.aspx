<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_PROC_SALDOS_TRABAJADOR.aspx.vb" Inherits="SNTE5.PEN_PROC_SALDOS_TRABAJADOR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

    </script>  

    <section class="panel" runat="server" id="div_selCliente">
            <header class="panel-heading" >
                <span>Selección Afiliado</span>
            </header>
            <div class="panel-body">

                    <div class="module_subsec columned low_m three_columns">
                        <div class="module_subsec_elements text_input_nice_div">
                                <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="Ingrese el número del afiliado"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server" 
                                        TargetControlID="txt_idCliente" FilterType="Numbers" Enabled="True" />
                                </div>
                        </div>
                        <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                            <asp:Button ID="btn_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar" />
                            <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" onClick="btn_BusquedaPersona_Click" runat="server" Text="Buscar afiliado" />
                        </div>
                    </div>
                            
                    <div class="module_subsec columned low_m align_items_flex_center" runat="server" id="div_NombrePersonaBusqueda" visible="false">
                        <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre del afiliado: </span>
                        <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" ></asp:Label>
                    </div>
                            
                    <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>
                
            </div>
        </section>

    <section class="panel" runat="server" id="panel_saldos">
        <header runat="server" id="head_panel_saldos" class="panel_header_folder panel-heading up_folder">
            <span>Saldos</span>
            <span class="panel_folder_toogle up" runat="server" id="toggle_panel_saldos" >&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_saldos" >  
                 <div class="overflow_x shadow module_subsec ">
                    <asp:DataGrid ID="dag_Cuenta" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" >
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="NUM_CUENTA" HeaderText="Número de Cuenta">
                                </asp:BoundColumn>
                             <asp:BoundColumn DataField="NSS" HeaderText="NSS">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>  
                            <asp:BoundColumn DataField="RFC" HeaderText="CURP">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn> 
                            <asp:BoundColumn DataField="TOTAL" HeaderText="Saldo Total">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn> 
                            <asp:ButtonColumn CommandName="HISTORIAL" HeaderText="Historial" Text="Ver">
                                    <ItemStyle Width="10%"  />
                                </asp:ButtonColumn>                           
                        </Columns>
                    </asp:DataGrid>
                </div>
                              
                <div class="overflow_x shadow module_subsec ">
                    <asp:DataGrid ID="dag_Saldos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" >
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="CLAVE" HeaderText="Clave">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUENTA" HeaderText="Subcuenta">
                                </asp:BoundColumn>
                             <asp:BoundColumn DataField="SALDO" HeaderText="Saldo">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>  
                            <asp:ButtonColumn CommandName="MOVIMIENTOS" HeaderText="Movimientos" Text="Ver">
                                    <ItemStyle Width="10%"  />
                                </asp:ButtonColumn>                           
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_mov">
        <header runat="server" id="head_panel_mov" class="panel_header_folder panel-heading up_folder">
            <span>Movimientos</span>
            <span class="panel_folder_toogle up" runat="server" id="toggle_panel_mov" >&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_mov" >  
                <div class="overflow_x shadow module_subsec ">
                    <asp:DataGrid ID="dag_movimientos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" >
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="CONCEPTO" HeaderText="Concepto">
                                </asp:BoundColumn>
                             <asp:BoundColumn DataField="SALDO" HeaderText="Saldo Original">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>  
                             <asp:BoundColumn DataField="COMISIONES" HeaderText="Comision">
                                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                                </asp:BoundColumn>     
                            <asp:BoundColumn DataField="TOTAL" HeaderText="Saldo Final">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>  
                            <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>                 
                        </Columns>
                    </asp:DataGrid>
                </div>

                <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                    <div style="display:flex;align-items:center;"> 
                        <asp:LinkButton runat="server" style="margin-right:25px;font-size:18px;" ID="btn_EXCEL">
                            Descargar Excel Consulta
                            <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" id="panel_historial">
        <header runat="server" id="head_panel_historial" class="panel_header_folder panel-heading up_folder">
            <span>Historial</span>
            <span class="panel_folder_toogle up" runat="server" id="toggle_panel_historial" >&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content" runat="server" id="content_panel_historial" >  

            </div>
        </div>
    </section>

</asp:Content>
