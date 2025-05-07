<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_PROC_LAYOUT.aspx.vb" Inherits="SNTE5.PEN_PROC_LAYOUT" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Carga Archivo</span>
        </header>
        <div class="panel-body">

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_institucion" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label title_tag">*Institución: </span>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="alertaValidator" 
                                                ControlToValidate="ddl_institucion" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_CargarAmort" InitialValue="-1" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_proceso" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">

                            <span class="text_input_nice_label title_tag">*Proceso: </span>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator" 
                                                ControlToValidate="ddl_proceso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_CargarAmort" InitialValue="-1" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_layout" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label title_tag">*Subproceso: </span>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="alertaValidator" 
                                                ControlToValidate="ddl_layout" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_CargarAmort" InitialValue="-1" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec_elements low_m columned">
                <div class="module_subsec no_column  align_items_flex_center">
                    <span class="module_subsec_elements">Seleccionar archivo de carga:</span>
                    <asp:FileUpload ID="AsyncFileUpload1" CssClass="module_subsec_elements" runat="server" />
                </div>
            </div>
            <div class="module_subsec flex_end">
                <asp:Button ID="btn_CargarArch" runat="server" class="btn btn-primary" Text="Cargar" UseSubmitBehavior="False" 
                    OnClientClick="ctl00_ContentPlaceHolder1_btn_CargarArch.disabled = true; ctl00_ContentPlaceHolder1_btn_CargarArch.value = 'Procesando...';" ValidationGroup="val_CargarAmort" />
            </div>
            <div class="module_subsec">
                <div class="overflow_x shadow">

                    <asp:DataGrid ID="dag_Persona_Ex" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" Width="100%" Visible="true">
                        <Columns>
                           <asp:BoundColumn DataField="TIPO" HeaderText="Tipo Evento">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>  
                            <asp:BoundColumn DataField="EVENTO" HeaderText="Evento">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="TRABAJADOR" HeaderText="Afiliado">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUENTA" HeaderText="Cuenta">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Detalle">                                
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHACREADO" HeaderText="Fecha de Creación">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </div>
            </div>

            <div class="module_subsec">
                <div class="overflow_x shadow">
                    <asp:DataGrid ID="Dag_Persona_NoEx" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" Width="100%" Visible="true">

                        <Columns>
                            <asp:BoundColumn DataField="IDRESPUESTA" HeaderText="Fila">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESPUESTA" HeaderText="Nombres">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </div>
            </div>

            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_Status_Carga" runat="server" CssClass="alerta"></asp:Label>
            </div>

             <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_consulta" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label title_tag">Búsqueda de lote: </span>
                        </div>
                    </div>
                </div>
            </div>


            <div class="module_subsec flex_end">
                <asp:Button ID="btn_buscar" runat="server" class="btn btn-primary" Text="Buscar"/>
            </div>

             <div class="module_subsec">
                <div class="overflow_x shadow">
                    <asp:DataGrid ID="dag_lotes" runat="server" class="table table-striped" AutoGenerateColumns="False" CellPadding="4"
                        GridLines="None" Width="100%" Visible="true">

                        <Columns>
                           <asp:BoundColumn DataField="TIPO" HeaderText="Tipo Evento">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>  
                            <asp:BoundColumn DataField="EVENTO" HeaderText="Evento">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="TRABAJADOR" HeaderText="Afiliado">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUENTA" HeaderText="Cuenta">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPCION" HeaderText="Detalle">                                
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FECHACREADO" HeaderText="Fecha de Creación">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="table_header"></HeaderStyle>
                    </asp:DataGrid>
                </div>
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
    </section>
</asp:Content>

