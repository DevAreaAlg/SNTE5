<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_ALE_CONSULTASESIONES.aspx.vb" Inherits="SNTE5.PLD_ALE_CONSULTASESIONES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript"  language="javascript">
        function busquedapersonafisica() {
            var wbusf = window.showModalDialog('<%= ResolveClientUrl("../../UNIVERSAL/UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE") %>', wbusf, "center:yes;resizable:no;dialogHeight:650px;dialogWidth:650px");
            if (wbusf != null) {
                document.getElementById("hdn_busqueda").value = wbusf;
                __doPostBack('', '');
            }
        }

        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 40002;
        }

        function ClickBotonBusqueda(ControlTextbox, ControlButton) {
            var CTextbox = document.getElementById(ControlTextbox);
            var CButton = document.getElementById(ControlButton);
            if (CTextbox != null && CButton != null) {
                if (event.keyCode == 13) {
                    event.returnValue = false;
                    event.cancel = true;
                    if (CTextbox.value != "") {
                        CButton.click();
                        CButton.disabled = true;
                    }
                    else {
                        //alert('Ingrese Datos')
                        CTextbox.focus()
                        return false
                    }
                    //CTextbox.focus();
                    return true
                }
            }
        }
    </script>
    <section class="panel">
        <header class="panel-heading">
            <span>DATOS FILTRO</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show" runat="server" id="content_div_selCliente">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="txt_IdCliente" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="Ingrese el número de afiliado"></asp:Label>
                            <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender__idcliente" runat="server"
                                targetcontrolid="txt_idCliente" filtertype="Numbers" enabled="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_IdCliente" runat="server"
                                ControlToValidate="txt_IdCliente" CssClass="textogris" Display="Dynamic"
                                ErrorMessage=" Falta Dato!" ValidationGroup="val_idPersona"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">

                        <asp:Button ID="btn_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar afiliado" />

                    </div>
                </div>
            </div>
            <div class="panel-body_content init_show" runat="server">
                <div class="module_subsec columned low_m three_columns">
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="txt_fechaini" runat="server" class="text_input_nice_input"
                            MaxLength="10"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Fecha inicial:</span>
                            <ajaxtoolkit:maskededitextender id="MaskedEditExtender_fechaini"
                                runat="server" cultureampmplaceholder="" culturecurrencysymbolplaceholder=""
                                culturedateformat="" culturedateplaceholder="" culturedecimalplaceholder=""
                                culturethousandsplaceholder="" culturetimeplaceholder="" enabled="True"
                                mask="99/99/9999" masktype="Date" targetcontrolid="txt_fechaini">
                                    </ajaxtoolkit:maskededitextender>
                            <ajaxtoolkit:calendarextender id="CalendarExtender_fechaini" runat="server"
                                enabled="True" format="dd/MM/yyyy" targetcontrolid="txt_fechaini">
                                    </ajaxtoolkit:calendarextender>

                            <ajaxtoolkit:maskededitvalidator id="MaskedEditValidator_fechaini"
                                runat="server" controlextender="MaskedEditExtender_fechaini"
                                controltovalidate="txt_fechaini" cssclass="textogris"
                                errormessage="MaskedEditValidator_fechaIniOpen" display="Dynamic"
                                invalidvaluemessage="Fecha inválida" validationgroup="val_fecha"></ajaxtoolkit:maskededitvalidator>
                        </div>
                    </div>
                    <div class="module_subsec_elements text_input_nice_div">
                        <asp:TextBox ID="txt_fechafin" runat="server" class="text_input_nice_input" MaxLength="9"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label">Fecha final:</span>
                             <ajaxtoolkit:maskededitextender id="MaskedEditExtender_fechafin"
                                runat="server" cultureampmplaceholder="" culturecurrencysymbolplaceholder=""
                                culturedateformat="" culturedateplaceholder="" culturedecimalplaceholder=""
                                culturethousandsplaceholder="" culturetimeplaceholder="" enabled="True"
                                mask="99/99/9999" masktype="Date" targetcontrolid="txt_fechafin">
                                    </ajaxtoolkit:maskededitextender>
                            <ajaxtoolkit:calendarextender id="CalendarExtender_fechafin" runat="server"
                                enabled="True" format="dd/MM/yyyy" targetcontrolid="txt_fechafin">
                                    </ajaxtoolkit:calendarextender>

                            <ajaxtoolkit:maskededitvalidator id="MaskedEditValidator_fechafin"
                                runat="server" controlextender="MaskedEditExtender_fechafin"
                                controltovalidate="txt_fechafin" cssclass="textogris"
                                errormessage="MaskedEditValidator_fechafin" display="Dynamic"
                                invalidvaluemessage="Fecha inválida" validationgroup="val_fecha"></ajaxtoolkit:maskededitvalidator>
                        </div>
                    </div>
                    <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">

                        <asp:Button ID="btn_creaF" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Aplicar filtro" />
                        <asp:Button ID="btn_eliminaF" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Eliminar filtro" />

                    </div>
                </div>
            </div>
            <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>
                </div>
            <asp:DataGrid ID="dag_Sesiones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                GridLines="None">
                <HeaderStyle CssClass="table_header"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="SESION_COM" HeaderText="Sesión">
                        <ItemStyle Width="100px"/>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IDACTOR" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ACTOR" HeaderText="Actor">
                        <ItemStyle Width="180px"/>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_ALERTAS" HeaderText="Num. alertas">
                        <ItemStyle Width="90px"/>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FECHACREADO" HeaderText="Fecha creado">
                        <ItemStyle Width="90px"/>
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="ACTA" HeaderText="" Text="Generar acta">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="VERDIGIT" HeaderText="" Text="Ver digitalización">
                        <ItemStyle Width="100px" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="DIGIT" HeaderText="" Text="Digitalizar">
                        <ItemStyle Width="90px" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </section>
</asp:Content>

