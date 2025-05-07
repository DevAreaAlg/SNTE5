<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="CRED_EXP_REESTRUCTURA.aspx.vb" Inherits="SNTE5.CRED_EXP_REESTRUCTURA" MaintainScrollPositionOnPostback ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Sección Oficinas" />
    <meta name="author" content="GeeksLabs" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/css/style.css" id="cssArchivo" rel="stylesheet" type="text/css" />
    <link href="/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="/css/style.css" id="Link1" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle de Reestructura</title>
</head>
<body>
    <form id="form1" runat="server">
        <section class="panel" runat="server" id="pnl_cliente">
            <header class="panel-heading panel_header_folder">
                <span>Detalle reestructura</span>
                <span class="panel_folder_toogle down" runat="server" id="toogle_pnl_cliente" >&rsaquo;</span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content init_show" runat="server" id="content_pnl_cliente" >
                    <div class="module_subsec columned two_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_start" >
                                <label id="lbl_folio_origenA" class="module_subsec_elements module_subsec_bigmedium-elements">Núm. expediente origen:</label>
                                <asp:Textbox ID="lbl_folio_origenB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_start" >
                                <label id="lbl_tiporesA" class="module_subsec_elements module_subsec_bigmedium-elements">Tipo reestructura:</label>
                                <asp:Textbox ID="lbl_tiporesB" runat="server" Enabled="false" class="module_subsec_elements flex_1 text_input_nice_input"></asp:Textbox>
                            </div>

                            <div class="module_subsec columned no_m align_items_flex_start" >
                                <label id="lbl_emprobA" class="module_subsec_elements module_subsec_bigmedium-elements">Razón emproblemamiento:</label>
                                <asp:Textbox ID="lbl_emprobB" runat="server" Enabled="false" Width="600px" TextMode="MultiLine" class="module_subsec_elements flex_1 text_input_nice_textarea"></asp:Textbox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</body>
</html>
