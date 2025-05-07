<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_CNF_CALENDARIOS.aspx.vb" Inherits="SNTE5.PEN_CNF_CALENDARIOS" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel" runat="server" visible="true" id="pnl_calendario">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_calendario">
            <span class="panel_folder_toogle_header">Carga Calendario</span>
            <span class="panel_folder_toogle up" runat="server" id="Span1">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                <div style="display: flex; align-items: center;">
                    <asp:LinkButton ID="lnk_layout" runat="server" Style="font-size: 18px;">
                                    Formato layout calendarios
                                    <i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:20px; margin-left:5px;"></i>
                    </asp:LinkButton>
                </div>
                <asp:Label runat="server" Text="Seleccione el archivo correspondiente para cargar el calendario, sólo se permite el siguiente tipo de archivo (*.csv)" CssClass="texto"></asp:Label>
            </div>
            <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                <div style="display: flex; align-items: center;">
                    <asp:LinkButton ID="lnk_calendario_actual" runat="server" Style="font-size: 18px;">
                                    Descargar calendario actual
                                    <i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size:20px; margin-left:5px;"></i>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="module_subsec no_m">
                <div class="module_subsec columned low_m no_rm three_columns" style="flex: 1;">
                   
                    <div class="module_subsec_elements">
                        <div class="module_subsec_elements">
                            <asp:FileUpload ID="fud_carga_calendario" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                    <asp:Button ID="btn_carga_calendario" CssClass="btn btn-primary" runat="server" Text="Cargar" />
                </div>
            </div>
            <div class="module_subsec flex_center">
                <asp:Label ID="lbl_estatus" runat="server" CssClass="alerta" />
            </div>
            <div class="module_subsec overflow_x shadow">
                <asp:DataGrid ID="dag_estatus_fechas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="QUINCENA" HeaderText="Quincena"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ESTATUS" HeaderText="Resultado"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </section>

    <section class="panel" runat="server" visible="true" id="pnl_quincenas">
        <header class="panel-heading panel_header_folder" runat="server" id="head_pnl_quincenas">
            <span class="panel_folder_toogle_header">Seleccionar Año/Quincena Base</span>
            <span class="panel_folder_toogle up" runat="server" id="toogle_pnl_quincenas">&rsaquo;</span>
        </header>

        <div class="module_subsec columned low_m four_columns">
            <div class="module_subsec_elements">
                 <div class="text_input_nice_div module_subsec_elements_content">
                <asp:TextBox ID="txt_actual" class="text_input_nice_input" runat="server" Enabled="false" MaxLength="100"></asp:TextBox>
                <div class="text_input_nice_labels">
                    <span class="text_input_nice_label">*Actual:</span>                  
                </div>
            </div>
            </div>
        </div>

        <div class="module_subsec columned low_m four_columns">
            <div class="module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:DropDownList ID="ddl_anio" CssClass="btn btn-primary2 dropdown_label"
                        runat="server" AutoPostBack="true" />
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Año:</span>

                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_años" Enabled="true"
                            CssClass="textogris" ControlToValidate="ddl_anio" Display="Dynamic" InitialValue="0" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_base" />


                    </div>
                </div>
            </div>
            <div class="module_subsec_elements">
                <div class="text_input_nice_div module_subsec_elements_content">
                    <asp:DropDownList ID="ddl_quincena" CssClass="btn btn-primary2 dropdown_label"
                        runat="server" AutoPostBack="true" />
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Quincena:</span>

                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_qna" Enabled="true"
                            CssClass="textogris" ControlToValidate="ddl_quincena" Display="Dynamic" InitialValue="0" ErrorMessage=" Falta Dato!"
                            ValidationGroup="val_base" />
                    </div>
                </div>
            </div>
        </div>

        <div class="module_subsec low_m columned four columns top_m flex_end">
            <div class="module_subsec low_m no_lm" style="margin-bottom: 10px;">
                <asp:Button ID="btn_guardar_qna" CssClass="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="val_base" />

            </div>
        </div>
        <div class="module_subsec flex_center">
                <asp:Label ID="lbl_estatusQna" runat="server" CssClass="alerta" />
            </div>
    </section>
</asp:Content>
