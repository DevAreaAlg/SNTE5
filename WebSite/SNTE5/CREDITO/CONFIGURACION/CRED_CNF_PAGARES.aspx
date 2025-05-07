<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_CNF_PAGARES.aspx.vb" Inherits="SNTE5.CRED_CNF_PAGARES" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="wrapper">
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header"><i class="icon_group"></i>LOTES PAGARE</h3>
                <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i><a href="INDEX.aspx">Inicio</a></li>
                </ol>
            </div>
        </div>
        <div class="tamano-cuerpo">
            <section class="panel">
                <header class="panel_header_folder panel-heading">
                    <span>CONFIGURACIÓN DE PAGARES</span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content init_show">

                        <asp:Label ID="lbl_subtitulo" runat="server" class="subtitulos"></asp:Label>

                        <asp:Label ID="lbl_Producto" runat="server" class="texto"></asp:Label>

                        <asp:Label ID="lbl_datosrequeridos" class="textogris" runat="server" Text="* Datos Requeridos"></asp:Label>


                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server"
                            ActiveTabIndex="2" Width="741px">

                            <ajaxToolkit:TabPanel runat="server" HeaderText="Elegir Producto" ToolTip="Elegir un producto para la asigancion de sus pagares" ID="TabPanel0">
                                <HeaderTemplate>Elegir Producto </HeaderTemplate>
                                <ContentTemplate>


                                    <div class="module_subsec no_m">
                                        <div class="module_subsec columned low_m no_rm no_column" style="flex: 1;">
                                            <div class=" module_subsec_elements">
                                                <div class="text_input_nice_div">

                                                    <asp:DropDownList ID="cmb_Productos" runat="server" AutoPostBack="True"
                                                        class="textocajas" ValidationGroup="val_elegirproducto">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Productos"
                                                        runat="server" ControlToValidate="cmb_Productos"
                                                        CssClass="textogris" InitialValue="0"
                                                        Display="Dynamic" ErrorMessage=" Elije Un Producto!"
                                                        ValidationGroup="val_elegirproducto"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="Label1" runat="server" CssClass="texto" Text="Producto: "></asp:Label>
                                                </div>
                                            </div>
                                            <div style="flex: 1; display: flex; justify-content: flex-end">
                                                <asp:Button ID="btn_Aceptar" runat="server" BackColor="White"
                                                    BorderColor="#054B66" BorderWidth="2px" class="btntextoazul" Height="19px"
                                                    Text="Aceptar" Width="120px" ValidationGroup="val_elegirproducto" />
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>



                            <ajaxToolkit:TabPanel runat="server" HeaderText="Asignar Pagaré" ToolTip="Asiganción de pagarés a productos" ID="TabPanel1">
                                <HeaderTemplate>Asignar Pagaré </HeaderTemplate>
                                <ContentTemplate>
                                    <br />
                                    <br />

                                    <asp:UpdatePanel ID="upd_pnl_AsignarPagares" runat="server">
                                        <ContentTemplate>
                                            <div class="overflow_x shadow">
                                                <asp:GridView ID="dag_mod_si" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                                    GridLines="None" HorizontalAlign="Center" TabIndex="17" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="40" DataField="IDPAGARE" HeaderText="ID Pagaré">
                                                            <ItemStyle Width="10%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField ItemStyle-Width="245" DataField="NOMBRE_DESC" HeaderText="Descripción">
                                                            <ItemStyle Width="25%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField ItemStyle-Width="245" DataField="CONTENIDO" HeaderText="Contenido">
                                                            <ItemStyle Width="40%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_asignado" runat="server" Checked='<%# Convert.ToBoolean(Eval("ASIGNADO")) %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="table_header" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_asignar" />
                                        </Triggers>

                                    </asp:UpdatePanel>




                                    <div class="module_subsec flex_end">
                                        <asp:Button ID="btn_asignar" class="btn btn-primary" runat="server" OnClick="btn_asignar_Click" Text="Guardar" />
                                    </div>

                                    <asp:Label ID="lbl_RolNumero" runat="server" CssClass="text_input_nice_label" style="margin-left:20px" Text="Número de Rol"></asp:Label>


                                    <div align="center">
                                        <asp:Button ID="btn_VerPagare0" runat="server" align="center" BackColor="White"
                                            BorderColor="#054B66" BorderWidth="2px" class="btntextoazul"
                                            Height="19px" Text="Ver Pagaré" Width="120px" />
                                    </div>

                                    <div align="center">
                                        <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>
                                    </div>

                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>

                            <ajaxToolkit:TabPanel runat="server" HeaderText="Crear Pagaré" ToolTip="Crear nuevas plantillas de pagaré" ID="TabPanel2">
                                <HeaderTemplate>Crear Pagaré </HeaderTemplate>
                                <ContentTemplate>
                                    <br />
                                    <br />



                                    <asp:LinkButton ID="lnk_AgrMunicipio" runat="server" class="copyright"
                                        ToolTip="Agrega Municipio">Municipio/Ciudad</asp:LinkButton>


                                    <asp:LinkButton ID="lnk_AgrEstado" runat="server" class="copyright"
                                        ToolTip="Agrega Estado">Estado</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrDiaSuscribe" runat="server" class="copyright"
                                        ToolTip="Agrega Dia de Fecha de Suscripción de Pagaré">Dia Suscripción de Pagaré</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrTasaInteresNormal" runat="server" class="copyright"
                                        ToolTip="Agrega la Tasa de interes normal">Tasa Interes Normal</asp:LinkButton>




                                    <asp:LinkButton ID="lnk_AgrAnioSuscribe" runat="server" class="copyright"
                                        ToolTip="Agrega Año de Fecha de Suscripción de Pagaré">Año Suscripción de Pagaré</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_FechaPagoPagare" runat="server" class="copyright"
                                        ToolTip="Agrega Fecha de Liquidacion de Monto de Pagaré">Fecha Pago de Pagaré</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrMonto" runat="server" class="copyright"
                                        ToolTip="Agrega Monto de Credito Solicitado">Monto de Credito</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrTipoPeriodicidad" runat="server" class="copyright"
                                        ToolTip="Agrega el tipo de periodicidad de los abonos(Quincenal, Mensual, etc.)">Tipo de Periodicidad</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_TipoCobro" runat="server" class="copyright"
                                        ToolTip="Tipo de cobro que se le hara al afiliado (Saldos Insolutos, Paghos Fijos, etc.).">Tipo Cobro</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_NumAbonos" runat="server" class="copyright"
                                        ToolTip="Agrega Numero de Abonos para cubrir el pago total">Numero de Abonos</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrPeriodicidad" runat="server" class="copyright"
                                        ToolTip="Agrega la periodicidad de los abonos(Quincenal, Mensual, etc.)">Periodicidad</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrMontoLetra" runat="server" class="copyright"
                                        ToolTip="Agrega el monto solicitado en formato de letra">Monto en Letra</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrTasaInteresMora" runat="server" class="copyright"
                                        ToolTip="Agrega tasa de interes moratorio">Tasa Interes Moratorio</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrVencimientoAnt" runat="server" class="copyright"
                                        ToolTip="Agrega Vencimiento Anticipado por falta de pago">Vencimiento Anticipado</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrMesSuscribe" runat="server" class="copyright"
                                        ToolTip="Agrega Mes de Fecha de Suscripción de Pagaré">Mes Suscripción de Pagaré</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_TasaCastigo" runat="server" class="copyright"
                                        ToolTip="Tasa de Castigo para algunos creditos en caso de no cumplir con lo estipulado en el pagaré.">Tasa Castigo</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_AgrMontodisp" runat="server" class="copyright"
                                        ToolTip="Agrega Monto de Disposición">Monto de Disposición</asp:LinkButton>




                                    <asp:LinkButton ID="lnk_disp" runat="server" class="copyright"
                                        ToolTip="Número de disposición">Número de Disposición</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_fechacorte" runat="server" class="copyright"
                                        ToolTip="Número de disposición">Fecha de Corte</asp:LinkButton>




                                    <asp:LinkButton ID="lnk_fecha_vigencia" runat="server" class="copyright"
                                        ToolTip="Fecha Vigencia Carta Préstamo">Fecha Vigencia Carta Préstamo</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_fecha_contrato" runat="server" class="copyright"
                                        ToolTip="Fecha contrato Carta Préstamo">Fecha contrato Carta Préstamo</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_contrato" runat="server" class="copyright"
                                        ToolTip="Número de contrato Carta Préstamo">Número contrato Carta Préstamo</asp:LinkButton>



                                    <asp:LinkButton ID="lnk_acreditado" runat="server" class="copyright"
                                        ToolTip="Acreditado">Acreditado</asp:LinkButton>




                                    <asp:LinkButton ID="lnk_monto_Carta" runat="server" class="copyright"
                                        ToolTip="Agrega Monto de Carta de Préstamo">Monto de Carta Préstamo</asp:LinkButton>



                                    <p>

                                        <asp:Label ID="lbl_PagareNombre" runat="server" CssClass="texto" Text="Nombre de Pagaré * "
                                            Width="161px"></asp:Label>
                                    </p>

                                    <p align="center">

                                        <asp:TextBox ID="txt_PagareNombre" runat="server" class="textocajas"
                                            MaxLength="50" Width="700px"></asp:TextBox>
                                    </p>

                                    <p>

                                        <asp:Label ID="lbl_ContenidoPagare" runat="server" CssClass="texto" Text="Contenido de Pagaré * "
                                            Width="161px"></asp:Label>
                                    </p>

                                    <p align="center">

                                        <asp:TextBox ID="txt_PlantillaPagare" runat="server" class="textocajas"
                                            MaxLength="5000" Width="700px" onfocus="SetEnd(this)"
                                            Height="248px" TextMode="MultiLine"></asp:TextBox>
                                    </p>

                                    <p align="center">

                                        <asp:Label ID="lbl_TipoCobro" runat="server" CssClass="texto" Text="Tipo de Cobro:" Width="161px"></asp:Label>
                                        <asp:DropDownList ID="cmb_TipoCobro" runat="server" class="textocajas">
                                            <asp:ListItem Value="0">ELIJA</asp:ListItem>
                                            <asp:ListItem Value="SI">SALDOS INSOLUTOS</asp:ListItem>
                                            <asp:ListItem Value="PFSI">PAGOS FIJOS SALDOS INSOLUTOS</asp:ListItem>
                                            <asp:ListItem Value="ES">PLAN ESPECIAL</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_TipoCobro"
                                            CssClass="textogris" ControlToValidate="cmb_TipoCobro"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!"
                                            ValidationGroup="val_GeneraPlantilla" InitialValue="0" />

                                    </p>

                                    <p align="center">

                                        <asp:Button ID="btn_GeneraPlantillaPagare" runat="server" BackColor="White"
                                            BorderColor="#054B66" BorderWidth="2px" class="btntextoazul" Height="19px"
                                            Text="Generar Plantilla" Width="120px" align="center" ValidationGroup="val_GeneraPlantilla" />

                                    </p>

                                    <p align="center">

                                        <asp:Label ID="lbl_AlertaGenerarPlantilla" runat="server" CssClass="alerta"></asp:Label>

                                    </p>

                                    <p align="center">
                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_VerPagare" runat="server" align="center"
                                            BackColor="White" BorderColor="#054B66" BorderWidth="2px" class="btntextoazul"
                                            Enabled="False" Height="19px" Text="Ver Pagaré" Width="120px" />
                                        &nbsp;&nbsp;
                                      <asp:Button ID="btn_NuevoPagare" runat="server" align="center"
                                          BackColor="White" BorderColor="#054B66" BorderWidth="2px" class="btntextoazul"
                                          Height="19px" Text="Crear Nuevo" Width="120px" />
                                        &nbsp;&nbsp;
                                      <asp:Button ID="btn_Terminar" runat="server" align="center" BackColor="White"
                                          BorderColor="#054B66" BorderWidth="2px" class="btntextoazul" Height="19px"
                                          Text="Terminar" Width="120px" />
                                        &nbsp;&nbsp;
                                      <asp:Button ID="btn_GuardarEdicion" runat="server" align="center"
                                          BackColor="White" BorderColor="#054B66" BorderWidth="2px" class="btntextoazul"
                                          Enabled="False" Height="19px" Text="Guardar Edición" Width="120px" />

                                    </p>

                                    <div class="overflow_x shadow">
                                        <asp:DataGrid ID="DAG_PlantPag" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" GridLines="None" Width="705px">
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <Columns>
                                                <asp:BoundColumn DataField="idpagare" HeaderText="ID Pagaré">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="nombrepagare" HeaderText="Nombre (Tipo Cobro)">
                                                    <ItemStyle Width="350px" />
                                                </asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="EDITAR"
                                                    HeaderText="Editar Pagaré" Text="Editar">
                                                    <ItemStyle ForeColor="#054B66" Width="50px" />
                                                </asp:ButtonColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>

                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender_GeneraPlantillaPagare" runat="server"
                                        TargetControlID="btn_GeneraPlantillaPagare"
                                        ConfirmText="Confirmar la creación de esta plantilla de pagaré?"
                                        Enabled="True">
                                    </ajaxToolkit:ConfirmButtonExtender>

                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                                        TargetControlID="btn_GuardarEdicion"
                                        ConfirmText="Confirmar la edición de esta plantilla de pagaré?" Enabled="True">
                                    </ajaxToolkit:ConfirmButtonExtender>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>

                        </ajaxToolkit:TabContainer>
                    </div>
                </div>
            </section>
        </div>
    </section>
</asp:Content>



