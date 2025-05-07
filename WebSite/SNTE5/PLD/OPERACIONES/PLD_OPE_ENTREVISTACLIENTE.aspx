<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_ENTREVISTACLIENTE.aspx.vb" Inherits="SNTE5.PLD_OPE_ENTREVISTACLIENTE" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function ResumenPersona() {
            window.open("../../CORE/PERSONA/CORE_PER_RESUMEN.aspx", "RP", "width=650px,height=650px,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }
        function ResumenPersonaM() {
            window.open("ResumenPersonaM.aspx", "RPM", "width=500px,height=400px,resizable=NO,Location=NO,Scrollbars=YES,Status=YES,top=1,left=1");
        }

        function busquedaCP() {
            var wbusf = window.showModalDialog("../../UNIVERSAL/UNI_BUSQUEDA_CP.aspx", wbusf, "center:yes;resizable:no;dialogHeight:500px;dialogWidth:600px");
        }
    </script>

    <div class="tamano-cuerpo">
        <section class="panel" runat="server" id="pnl_general">
                <header class="panel-heading">
                     <span><asp:Label ID="Label2" runat="server" Text="Entrevistas pendientes " Enabled="false" ></asp:Label></span>
                </header>
                <div class="panel-body">
                    <div class="panel-body_content">
                        <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                            <div style="display:flex;align-items:center;"> 
                                <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" style="margin-right:25px;font-size:18px;" ID="lnk_FormatoEntrevista">
                                    Descargar Formato Entrevistas
                                    <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="module_subsec overflow_x shadow">
                            <asp:DataGrid ID="dag_EntPend" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                                GridLines="None" Width="100%">
                                <HeaderStyle CssClass="table_header" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDALERTA" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDCLIENTE" HeaderText="Id afiliado">
                                    <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                    <ItemStyle Width="25%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                    <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="10%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario">
                                    <ItemStyle Width="25%"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="DIGITALIZAR" Text="Digitalizar">
                                    <ItemStyle Width="10%" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn CommandName="CAPTURAR" Text="Capturar">
                                    <ItemStyle Width="10%" />
                                    </asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>                            
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_Status" runat="server" class="alerta"></asp:Label>
                        </div>
                    </div>
                </div>
        </section>

        <section class="panel" runat="server" id="pnl_captura" Visible =" False">
            <header class="panel-heading">
                <span><asp:Label ID="Label3" runat="server" Text="Captura de entrevista" Enabled="false" ></asp:Label></span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <div class="module_subsec columned three_columns low_m align_items_flex_start">
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label ID="lbl_IDAlerta" runat="server" class="module_subsec_elements module_subsec_small-elements">Núm. alerta:</asp:Label>
                                <asp:Textbox ID="lbl_IDAlerta1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:Textbox>
                            </div>
                        </div>
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label ID="lbl_Cliente" runat="server" class="module_subsec_elements module_subsec_small-elements">Afiliado:</asp:Label>
                                <asp:Textbox ID="lbl_Cliente1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:Textbox>
                            </div>
                        </div>
                        <div class="module_subsec_elements vertical flex_1">
                            <div class="module_subsec columned no_m align_items_flex_center ">
                                <asp:Label ID="lbl_Folio" runat="server" class="module_subsec_elements module_subsec_small-elements">Expediente:</asp:Label>
                                <asp:Textbox ID="lbl_Folio1" runat="server" class="module_subsec_elements flex_1 text_input_nice_input" Enabled="false"></asp:Textbox>
                            </div>
                        </div>
                    </div>
                       
                    <div class="module_subsec overflow_x shadow">
                        <asp:DataGrid ID="dag_Operacion" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="OPERACION" HeaderText="Operación">
                                <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTA" HeaderText="Nota">
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    
                    <div class="module_subsec">
                        <asp:Label ID="lbl_ResumenPersonaVer" runat="server" class="texto">Ver resumen de la persona: </asp:Label>
                        &nbsp;
                        <asp:LinkButton runat="server" Text="Resumen prospecto" ID="lnk_ResumenPersona" class="textogris" ToolTip = "Ver los datos de la persona"></asp:LinkButton>
                    </div>

                    <div class="module_subsec module_subsec_elements">
                        <div class="text_input_nice_div module_subsec_elements_content" >
                            <div style="margin:0" class="radio">
                                <asp:RadioButton ID="rad_SiEnt" runat="server" class="textogris" 
                                    GroupName="EntrevistaPLD" Text="Si" Width="50px" AutoPostBack="True"></asp:RadioButton>
                                    &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rad_NoEnt" runat="server" class="textogris" 
                                    GroupName="EntrevistaPLD" Text="No" Width="50px" AutoPostBack="True"></asp:RadioButton>
                            </div>
                            <div class="text_input_nice_labels">
                                 <asp:Label ID="lbl_RealizoEnt" runat="server" class="texto">¿Realizó la entrevista?</asp:Label>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnl_NoRealizoEnt" runat="server" Visible="False">
                        <div class="module_subsec">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_RazonNoEnt" runat="server" class="text_input_nice_textarea" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_RazonNoEnt" runat="server" class="text_input_nice_label">* Razon por la que no realizó la entrevista:</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_RazonNoEnt" runat="server" ControlToValidate="txt_RazonNoEnt" 
                                            Cssclass="textoazul" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_NoEnt"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_RazonNoEnt" runat="server" TargetControlID="txt_RazonNoEnt"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" Enabled="True" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>                                            
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_NoEntObservaciones" runat="server" class="text_input_nice_textarea" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_NoEntObservaciones" runat="server" class="text_input_nice_label">Observaciones:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_NoEntObservaciones" runat="server" TargetControlID="txt_NoEntObservaciones"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" Enabled="True" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_StatusNoEnt" runat="server" class="alerta"></asp:Label>
                        </div>
                            
                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_GuardaNoEnt" runat="server" class="btn btn-primary" 
                                Text="Guardar" ValidationGroup="val_NoEnt"></asp:Button>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnl_SiRealizoEnt" runat="server" Visible="False">
                        <div class="module_subsec low_m">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_OrigenDeposito" runat="server" class="text_input_nice_textarea" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_OrigenDeposito" runat="server" class="text_input_nice_label">* Origen del depósito:</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_OrigenDeposito" runat="server" 
                                            ControlToValidate="txt_OrigenDeposito" Cssclass="textoazul" 
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_SiEnt"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_OrigenDeposito" 
                                            runat="server" Enabled="True" 
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" 
                                            TargetControlID="txt_OrigenDeposito" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_PeriodicidadDeposito" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_PeriodicidadDeposito" runat="server" class="text_input_nice_label">* Periodicidad del depósito cada:</asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_PeriodicidadDeposito" runat="server" Cssclass="textoazul"
                                            ControlToValidate="txt_PeriodicidadDeposito" Display="Dynamic" ErrorMessage=" Falta Dato!" 
                                            ValidationGroup="val_SiEnt"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="filtered_periodicidad" runat="server" Enabled="True"
                                            TargetControlID="txt_PeriodicidadDeposito" FilterType="Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_PeriodicidadDeposito" runat="server" class="btn btn-primary2 dropdown_label" ><asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="DIA">DIA(S)</asp:ListItem>
                                        <asp:ListItem Value="SEM">SEMANA(S)</asp:ListItem>
                                        <asp:ListItem Value="MES">MES(ES)</asp:ListItem>
                                        <asp:ListItem Value="ANIO">AÑO(S)</asp:ListItem>
                                        <asp:ListItem Value="UNIC">ÚNICA OCASIÓN</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cmbPeriodicidadDeposito" CssClass="textogris" ControlToValidate="cmb_PeriodicidadDeposito"
                                            Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_SiEnt" InitialValue="-1" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_PuestoPublico" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_PuestoPublico" runat="server" class="text_input_nice_label">Puesto público:</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_AntPuestoPublico" runat="server" class="text_input_nice_input" MaxLength="3" Visible="False"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_AntPuestoPublico" runat="server" class="text_input_nice_label" 
                                            Visible="False">* Antigüedad en puesto público:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_AntPuestoPublico" runat="server" 
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_AntPuestoPublico"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:Label ID="lbl_AntPuestoPublico0" runat="server" class="texto" 
                                        Visible="False">año(s)</asp:Label>
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="lbl_Extranjero" runat="server" class="module_subsec h4">Extranjero</asp:Label>
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TiempoEnMex" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TiempoEnMex" runat="server" class="text_input_nice_label">Tiempo viviendo en México (años):</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TiempoEnMex" runat="server" 
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_TiempoEnMex"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements flex_1">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MotivoEnMex" runat="server" class="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MotivoEnMex" runat="server" class="text_input_nice_label">Motivo para vivir en México:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MotivoEnMex" 
                                            runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" 
                                            TargetControlID="txt_MotivoEnMex" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TiempoMasMexico" runat="server" class="text_input_nice_input" MaxLength="3"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TiempoMasMexico" runat="server" class="text_input_nice_label">Cuanto tiempo más vivirá en México (años):</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxt_TiempoMasMexico" runat="server" 
                                            Enabled="True" FilterType="Numbers" TargetControlID="txt_TiempoMasMexico"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_CategoriaPasaporte" runat="server" class="text_input_nice_input" MaxLength="50"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_CategoriaPasaporte" runat="server" class="text_input_nice_label">Categoria de pasaporte:</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="lbl_Moral" runat="server" class="module_subsec h4">Persona moral</asp:Label>
                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralNombres" runat="server" class="text_input_nice_input" MaxLength="300"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralNombres" runat="server" class="text_input_nice_label">Nombres:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_nombres" runat="server" TargetControlID="txt_MoralNombres"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑáéíóúñ" Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralPaterno" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralPaterno" runat="server" class="text_input_nice_label">Apellido paterno:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_moralpaterno" runat="server" TargetControlID="txt_MoralPaterno"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars="ÁÉÍÓÚÑáéíóúñ " Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralMaterno" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralMaterno" runat="server" class="text_input_nice_label">Apellido materno:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_materno" runat="server" TargetControlID="txt_MoralMaterno"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑáéíóúñ" Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_MoralRelacion" runat="server" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                        <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                        <asp:ListItem Value="REPLEGAL">REPRESENTANTE LEGAL</asp:ListItem>
                                        <asp:ListItem Value="CONTADOR">CONTADOR</asp:ListItem>
                                        <asp:ListItem Value="ABOGADO">ABOGADO</asp:ListItem>
                                        <asp:ListItem Value="AFILIADO">AFILIADO</asp:ListItem>
                                        <asp:ListItem Value="OFICINISTA">OFICINISTA</asp:ListItem>
                                        <asp:ListItem Value="AGENTE">AGENTE</asp:ListItem>
                                        <asp:ListItem Value="PRESTATARIO">PRESTATARIO</asp:ListItem>
                                        <asp:ListItem Value="DIRECTOR">DIRECTOR</asp:ListItem>
                                        <asp:ListItem Value="ACCIONISTA">ACCIONISTA</asp:ListItem>
                                        <asp:ListItem Value="VALORADOR">VALORADOR</asp:ListItem>
                                        <asp:ListItem Value="CORREDOR">CORREDOR</asp:ListItem>
                                        <asp:ListItem Value="EMPLEADO">EMPLEADO</asp:ListItem>
                                        <asp:ListItem Value="OTRO">OTRO</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralRelacion" runat="server" CssClass="text_input_nice_label" Text="Tipo de relación:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralRelOtro" runat="server" class="text_input_nice_input" MaxLength="100" Visible="False"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralRelOtro" runat="server" CssClass="text_input_nice_label" Text="Especifique:" Visible="False"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralCP" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralCP" runat="server" CssClass="texto" Text="Código postal:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MoralCP" 
                                            runat="server" Enabled="True" FilterType="Numbers" 
                                            TargetControlID="txt_MoralCP"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div>
                                    &nbsp;
                                    <asp:ImageButton ID="btn_Moralbuscadat" runat="server" 
                                    ImageUrl="~/img/glass.png" Style="height: 16px"></asp:ImageButton>
                                    &nbsp; &nbsp;<asp:LinkButton ID="lnk_MoralBusquedaCP" runat="server" class="textogris" 
                                    Text="Buscar CP"></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Moralestado" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Moralestado" runat="server" CssClass="text_input_nice_label" Text="Estado:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Moralmunicipio" runat="server" AutoPostBack="True" 
                                    class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Moralmunicipio" runat="server" CssClass="text_input_nice_label" Text="Municipio:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Morallocalidad" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Morallocalidad" runat="server" CssClass="text_input_nice_label" Text="Localidad:" ></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements flex_1">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralCalle" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralCalle" runat="server" class="text_input_nice_label">Calle y número:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_MoralCalle" 
                                            runat="server" Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                            TargetControlID="txt_MoralCalle" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ ,#"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Moralpais" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Moralpais" runat="server" CssClass="text_input_nice_label" Text="Nacionalidad:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralTel" runat="server" class="text_input_nice_input" MaxLength="50"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralTel" runat="server" CssClass="text_input_nice_label" Text="Teléfono:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_telefono" runat="server" Enabled="True"
                                            TargetControlID="txt_MoralTel" FilterType="Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_MoralCel" runat="server" class="text_input_nice_input" MaxLength="50"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_MoralCel" runat="server" CssClass="text_input_nice_label" Text="Celular:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_celular" runat="server" Enabled="True"
                                            TargetControlID="txt_MoralCel" FilterType="Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="lbl_TerceraPersona" runat="server" class="module_subsec h4">Tercera persona</asp:Label>
                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TerNombre" runat="server" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerNombre" runat="server" class="text_input_nice_label">Nombres:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_TerNombre"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑáéíóúñ" Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TerPaterno" runat="server" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerPaterno" runat="server" class="text_input_nice_label">Apellido paterno:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_terpaterno" runat="server" TargetControlID="txt_TerPaterno"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑáéíóúñ" Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TerMaterno" runat="server" class="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerMaterno" runat="server" class="text_input_nice_label">Apellido materno:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filtered_Termaterno" runat="server" TargetControlID="txt_TerMaterno"
                                            FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" ÁÉÍÓÚÑáéíóúñ" Enabled="True">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_TerRelacion" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerRelacion" runat="server" CssClass="text_input_nice_label" Text="Tipo de relación:"/>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TerCP" runat="server" class="text_input_nice_input" MaxLength="5"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerCP" runat="server" CssClass="text_input_nice_label" Text="Código postal:"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TerCP" 
                                            runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_TerCP"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="module_subsec_elements">
                                <div>
                                    &nbsp;
                                    <asp:ImageButton ID="btn_Terbuscadat" runat="server" ImageUrl="~/img/glass.png" Style="height: 16px"></asp:ImageButton>
                                    &nbsp; &nbsp;<asp:LinkButton ID="lnk_TerBusquedaCP" runat="server" class="textogris" Text="Buscar CP"></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Terestado" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Terestado" runat="server" CssClass="text_input_nice_label" Text="Estado:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Termunicipio" runat="server" AutoPostBack="True" 
                                    class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Termunicipio" runat="server" CssClass="text_input_nice_label" Text="Municipio:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Tercolonia" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Tercolonia" runat="server" CssClass="text_input_nice_label" Text="Localidad:"></asp:Label>
                                        </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec low_m columned three_columns flex_start">
                            <div class="module_subsec_elements flex_1">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_TerCalle" runat="server" class="text_input_nice_input" MaxLength="200"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_TerCalle" runat="server" class="text_input_nice_label">Calle y número:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_TerCalle" 
                                            runat="server" Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" 
                                            TargetControlID="txt_TerCalle" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ ,#"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:DropDownList ID="cmb_Terpais" runat="server" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Terpais" runat="server" CssClass="text_input_nice_label" Text="Nacionalidad:"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_SiEntObservaciones" runat="server" class="text_input_nice_textarea" 
                                    MaxLength="1000" TextMode="MultiLine" ></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_SiEntObservaciones" runat="server" class="text_input_nice_label">Observaciones:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_SiEntObservaciones" 
                                            runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" 
                                            TargetControlID="txt_SiEntObservaciones" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_StatusSiEnt" runat="server" class="alerta"></asp:Label>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_GuardaSiEnt" runat="server" class="btn btn-primary" Text="Guardar" ValidationGroup="val_SiEnt" />
                        </div>

                        <div class="module_subsec">
                            <asp:Label ID="Label1" runat="server" class="texto">Nota: Recuerde modificar los datos que sean necesarios en edición de persona </asp:Label>                        
                        </div>

                    </asp:Panel>
                                        
                </div>
            </div>
        </section>
    </div>
</asp:Content>
