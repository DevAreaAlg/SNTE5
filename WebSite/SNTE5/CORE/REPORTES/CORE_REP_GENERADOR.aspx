<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_REP_GENERADOR.aspx.vb" Inherits="SNTE5.CORE_REP_GENERADOR" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel" id="pnl_selRep">
        <header class="panel_header_folder panel-heading">
            <span class="panel_folder_toogle_header">Selección de reporte</span>
            <span class=" panel_folder_toogle down">&rsaquo;</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec low_m ">
                    Favor de seleccionar un reporte de entre la lista de reportes disponibles para cada categoría.
                </div>
                <div class="module_subsec  low_m no_lm align_items_flex_start ">
                    <asp:Panel runat="server" CssClass="module_subsec_elements" ID="pnl_outArbolCat"></asp:Panel>
                    <div style="flex: 1; padding: 10px; min-width: 250px" class="module_subsec_elements vertical align_items_flex_start">
                        <h4 style="margin-top: 0;">Categoría:
                            <asp:Label ID="lbl_categoria" runat="server" Text="No se ha seleccionado ninguna categoría."></asp:Label></h4>
                        <asp:Panel runat="server" ID="pnl_reportes" CssClass="w_100"></asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <asp:Panel runat="server" Visible="false" ID="pnl_datosBRep">
        <section class="panel">
            <header class="panel-heading">
                <span>Generar reporte</span>
            </header>
            <div class="panel-body">
                <div runat="server" id="div_contDatosRep" class="panel-body_content ">
                    <asp:Label ID="lbl_noRep" runat="server" Text="No se ha seleccionado ningun reporte a generar." CssClass="module_subsec low_m alerta"></asp:Label>

                    <h4 runat="server" id="lbl_repName" class="module_subsec"></h4>
                    <div runat="server" id="div_repDes" class="module_subsec"></div>
                    <div class="module_subsec" style="border-bottom: dashed 2px #688a7e;"></div>

                    <asp:Panel runat="server" ID="pnl_repParam" CssClass="module_subsec columned three_columns low_m"></asp:Panel>







<%--                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="FECHA_INICIO" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="FECHA_FIN" />--%>

                    <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="FECHA_INICIO" />

                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaIni"
                        runat="server" ControlExtender="MaskedEditExtender2"
                        ControlToValidate="FECHA_INICIO" CssClass="textogris"
                        ErrorMessage="MaskedEditValidator_fechaIniOpen" Display="Dynamic"
                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>--%>
                    <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Mask="99/99/9999" MaskType="Date" TargetControlID="FECHA_FIN" />--%>



                    <%--                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator_fechaFin"
                        runat="server" ControlExtender="MaskedEditExtender1"
                        ControlToValidate="FECHA_FIN" CssClass="textogris"
                        ErrorMessage="MaskedEditValidator_fechaIniOpen" Display="Dynamic"
                        InvalidValueMessage="Fecha inválida" ValidationGroup="val_fecha"></ajaxToolkit:MaskedEditValidator>--%>

                    <div class="module_subsec" style="justify-content: space-between; flex-direction: row-reverse;">
                        <div style="display: flex; align-items: center;">
                            <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" Style="margin-right: 25px; font-size: 18px;" ID="btn_generarExel" OnClick="generarExel">
                                Descargar Reporte
                                <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                            </asp:LinkButton>
                            <asp:Button ID="btn_GenerarRep" ValidationGroup="generar_reporte_param" runat="server" OnClick="generar_GridView" Text="Generar Reporte" CssClass="btn btn-primary" />
                            
                        </div>
                    </div>

                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_estatus" Visible="false" runat="server" Text="El reporte no cuenta con registros." CssClass="alerta" ValidationGroup="val_fecha"></asp:Label>
                    </div>

                </div>
            </div>
        </section>
    </asp:Panel>

    <section class="panel " style="display: none;" id="div_outGridViewRep">
        <div class="overflow_x">
            <asp:GridView ID="grv_Reporte" runat="server" CssClass="table table-striped" GridLines="None">
                <HeaderStyle CssClass="table_header" />
            </asp:GridView>
        </div>
    </section>
</asp:Content>

