<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PEN_PROC_PERIODOS.aspx.vb" Inherits="SNTE5.PEN_PROC_PERIODOS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="panel">
        <header class="panel-heading">
            <span>Procesos</span>
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
                                ValidationGroup="val_CargarPeriodo" InitialValue="-1" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_proceso" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">

                            <span class="text_input_nice_label title_tag">*Proceso: </span>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="alertaValidator"
                                ControlToValidate="ddl_proceso" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_CargarPeriodo" InitialValue="-1" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_tipoperiodo" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" AutoPostBack="True">
                            <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                            <asp:ListItem Value="QUIN">QUINCENAL</asp:ListItem>
                            <asp:ListItem Value="MENS">MENSUAL</asp:ListItem>
                            <asp:ListItem Value="BIM">BIMESTRAL</asp:ListItem>
                            <asp:ListItem Value="TRIM">TRIMESTRAL</asp:ListItem>
                            <asp:ListItem Value="SEM">SEMESTRAL</asp:ListItem>
                            <asp:ListItem Value="ANUAL">ANUAL</asp:ListItem>
                            <asp:ListItem Value="OTRO">OTRO</asp:ListItem>
                        </asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label title_tag">*Tipo periodo: </span>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="alertaValidator"
                                ControlToValidate="ddl_tipoperiodo" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_CargaPeriodo" InitialValue="-1" />
                        </div>
                    </div>
                </div>

                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList ID="ddl_meses" runat="server" class="btn btn-primary2 dropdown_label" Style="text-align: center" AutoPostBack="True" Enabled="false"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <span class="text_input_nice_label title_tag">*Mes: </span>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddl_meses" CssClass="alertaValidator"
                                ControlToValidate="ddl_meses" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                ValidationGroup="val_CargarAmort" InitialValue="-1" />
                        </div>
                    </div>
                </div>


            </div>

            <asp:UpdatePanel ID="upd_pnl_quincenal" runat="server">
                <ContentTemplate>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_DiaIni" class="text_input_nice_input" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Dia inicio: </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" CssClass="alertaValidator"
                                        ControlToValidate="txt_DiaIni" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_CargaPeriodo" InitialValue="-1" />
                                    <asp:RegularExpressionValidator ID="rev"
                                        runat="server" ErrorMessage="Hora incorrecta!" ControlToValidate="txt_HoraIni"
                                         ValidationExpression="^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$">
                                        </asp:RegularExpressionValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DiaIni" runat="server"
                                        TargetControlID="txt_DiaIni" Enabled="True" FilterType="Numbers" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_HoraIni" class="text_input_nice_input" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Hora inicio (formato hh:mm 24 horas): </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" CssClass="alertaValidator"
                                        ControlToValidate="txt_HoraIni" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_CargaPeriodo" InitialValue="-1" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_HoraIni" runat="server"
                                        TargetControlID="txt_HoraIni" Enabled="True" FilterType="Numbers, Custom" ValidChars=":" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_DiaFin" class="text_input_nice_input" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Día fin: </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" CssClass="alertaValidator"
                                        ControlToValidate="txt_DiaFin" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_CargaPeriodo" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_DiaFin" runat="server"
                                        TargetControlID="txt_DiaFin" Enabled="True" FilterType="Numbers" />
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_HoraFin" class="text_input_nice_input" runat="server" MaxLength="5" Enabled="false"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label title_tag">*Hora fin (formato hh:mm 24 horas): </span>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" CssClass="alertaValidator"
                                        ControlToValidate="txt_HoraFin" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_CargaPeriodo" InitialValue="-1" />
                                    <asp:RegularExpressionValidator ID="rev2"
                                        runat="server" ErrorMessage="Hora incorrecta!" ControlToValidate="txt_HoraFin"
                                         ValidationExpression="^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$">
                                        </asp:RegularExpressionValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_HoraFin" runat="server"
                                        TargetControlID="txt_HoraFin" Enabled="True" FilterType="Numbers, Custom" ValidChars=":" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="module_subsec flex_end">
                <asp:Button ID="btn_guarda_periodo" runat="server" Text="Guardar" class="btn btn-primary" ValidationGroup="val_CargaPeriodo"/>
            </div>

            <div align="center">
                <asp:Label ID="lbl_statupol" runat="server" CssClass="alerta"></asp:Label>
            </div>
            <div class="module_subsec">
                <asp:DataGrid ID="dag_Peridos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" TabIndex="17" Visible="false">
                    <Columns>
                        <asp:BoundColumn DataField="IDPROCESO" HeaderText="Proceso" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PROCESO" HeaderText="Proceso"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IDMES" HeaderText="Mes" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MES" HeaderText="Mes"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DIAINI" HeaderText="Día inicial"></asp:BoundColumn>
                        <asp:BoundColumn DataField="HORAINI" HeaderText="Hora inicial (24 horas)"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DIAFIN" HeaderText="Día final"></asp:BoundColumn>
                        <asp:BoundColumn DataField="HORAFIN" HeaderText="Hora final (24 horas)"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="ELIMINAR" HeaderText="" Text="Eliminar">
                            <ItemStyle Width="70px" />
                        </asp:ButtonColumn>
                    </Columns>
                    <HeaderStyle CssClass="table_header"></HeaderStyle>
                </asp:DataGrid>
            </div>
        </div>
    </section>
</asp:Content>

