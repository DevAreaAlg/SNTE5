<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_OPE_OPERACIONES_24HORAS.aspx.vb" Inherits="SNTE5.PLD_OPE_OPERACIONES_24HORAS" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tamano-cuerpo">

        <section class="panel" runat="server" id="pnl_captura">
            <header class="panel-heading">
                <span><asp:Label ID="Label3" runat="server" Text="Búsqueda" Enabled="false" ></asp:Label></span>
            </header>
            <div class="panel-body">
                <div class="panel-body_content">
                    <asp:Label ID="Label1" runat="server" class="module_subsec texto" Text="Favor de ingresar alguno de los parámetros para realizar la búsqueda."/>
                    <div class="module_subsec low_m  columned three_columns">
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_folio" runat="server" MaxLength="8" CssClass="text_input_nice_input" ValidationGroup="val_SerieFolio"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label2" runat="server" class="text_input_nice_label" Text="Folio de préstamo:"/>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server"
                                            TargetControlID="txt_folio" FilterType="Numbers" Enabled="True" />
                                    </div>
                                </div>
                            </div>
                            <div class="module_subsec_elements align_items_flex_end">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_FolioImp" runat="server" MaxLength="11" Style="text-transform: uppercase"
                                    CssClass="text_input_nice_input" ValidationGroup="val_SerieFolio"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="Label7" runat="server" class="text_input_nice_label" Text="Folio impresión (Ej. AAA0000001):"/>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_folio_imp" runat="server"
                                            class="textogris" ControlToValidate="txt_FolioImp" ErrorMessage="Folio Incorrecto!"
                                            ValidationExpression="^[a-zA-Z]{3}[0-9]{8}?$" ValidationGroup="val_SerieFolio">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                    <div class="module_subsec flex_center">
                        <asp:Label runat="server" class="alerta" ID="lbl_busqueda"></asp:Label>
                    </div>

                    <div class="module_subsec flex_end">
                        <asp:Button ID="lnk_Aceptar" runat="server" class="btn btn-primary" Text="Buscar" ValidationGroup="val_SerieFolio"></asp:Button> 
                            &nbsp;&nbsp;
                        <asp:Button ID="lnk_limpiafiltro" runat="server" class="btn btn-primary" Text="Eliminar"></asp:Button> 
                    </div>

                    <div class="module_subsec overflow_x shadow">
                        <asp:DataGrid ID="dag_Movimientos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="IDPERSONA" HeaderText="Núm. afiliado" Visible="true">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRE_PERSONA" HeaderText="Afiliado">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                    <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO_IMP" HeaderText="Folio de impresión">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO"  HeaderText="Monto">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA_OPERACION" HeaderText="Fecha de operación">
                                    <ItemStyle Width="15%"/>
                                </asp:BoundColumn> 
                                <asp:BoundColumn DataField="MOVIMIENTO" HeaderText="Tipo de operación">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>                                         
                                <asp:ButtonColumn CommandName="REPORTAR" Text="Reportar">
                                    <ItemStyle  Width="10%"/>
                                </asp:ButtonColumn>                                        
                            </Columns>
                        </asp:DataGrid>

                    </div>
                   
                    <div class="module_subsec flex_center">
                        <asp:Label runat="server" class="alerta" ID="lbl_status" align="center"></asp:Label>
                    </div>

                    <asp:Panel ID="pnl_Observaciones" runat="server" Visible="false" align="center">
                        <div class="module_subsec">
                            <div class="module_subsec_elements w_100">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_Notas" runat="server" class="text_input_nice_textarea" MaxLength="200" 
                                        ValidationGroup="val_OpPre" TextMode="MultiLine"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Notas" runat="server" class="text_input_nice_label" align="center">Razón de reporte:</asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_notas" 
                                             runat="server" Enabled="True" FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" 
                                            TargetControlID="txt_Notas" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec flex_end">
                            <asp:Button ID="btn_Confirma" runat="server" class="btn btn-primary" Text="Confirmar" ValidationGroup="val_OpPre"/>
                                &nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btn_Cancela" runat="server" class="btn btn-primary" Text="Cancelar" Enabled="False" CausesValidation="False"/>
                        </div>
                    </asp:Panel>
                        

                </div>
            </div>
        </section>

&nbsp;<asp:panel ID="pnl_ConfirmaCancela" runat ="server" Align="center" Width ="356px" CssClass ="modalPopup" style='display: none;' >
                            <asp:Panel ID="pnl_Titulo" runat ="server" Align="Center" Height ="20px" CssClass ="titulosmodal" >
                                <asp:Label ID="lbl_tit" runat="server" class="subtitulosmodal" Text="Confirmar Cancelación" ForeColor="White"></asp:Label>
                            </asp:Panel>
                            <p align="center" >
                                <asp:Label ID="lbl_Alerta" runat="server" class="subtitulos"></asp:Label>
                                <br />
                                <br />
                                <asp:Button ID="btn_Si" runat="server" BackColor="White" BorderColor="#054B66" 
                                            BorderWidth="2px" class="btntextoazul" Height="19px" Text="Aceptar" Width="74px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_No" runat="server" BackColor="White" BorderColor="#054B66" 
                                            BorderWidth="2px" class="btntextoazul" Height="19px" Text="Cancelar" Width="74px" />
                            </p>
                        </asp:panel>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackgroud" DropShadow="true" Enabled="true" PopupControlID="pnl_ConfirmaCancela" PopupDragHandleControlID="pnl_Titulo" TargetControlID="hdn_CancelaMov" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
            

    </div>

    <input type="hidden" name="hdn_CancelaMov" id="hdn_CancelaMov" value="" runat="server" />
</asp:Content>