<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_TES_CHEQUES_AUTORIZACION.aspx.vb" Inherits="SNTE5.CRED_TES_CHEQUES_AUTORIZACION" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <section class="panel" >   

        <asp:UpdatePanel ID="upd_pnl" runat="server" >
            <ContentTemplate>
                <br />
                <div class="module_subsec">
                    <div class="overflow_x shadow" style="flex:1" > 
                        <asp:DataGrid ID="dag_aut_cheques" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_AUT" HeaderText="Núm. autorización">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Expediente">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_PERSONA" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_OP" HeaderText="Tipo de operación">
                                    <ItemStyle Width="20%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SUC" HeaderText="Sucursal">
                                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="USUARIO" HeaderText="Usuario">
                                        
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="AUTO" Text="Dictaminar">
                                    <ItemStyle Width="5%"  ForeColor="#054B66" HorizontalAlign="Center" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                </div>

                <div class="module_subsec">

                    <div class="overflow_x shadow" style="flex:1"  > 
                        <asp:DataGrid ID="dag_cheques_aut" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="95%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="BANCO" HeaderText="Banco">
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CHEQUE" HeaderText="Núm. Cheque">
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                            
                </div>

                <asp:Panel ID="pnl_Dictamen" runat="server" Visible="false">

                    <div style="margin-left:20px">
                        <asp:Label ID="lbl_NumAuto" runat="server" class="subtitulos" Text="" /><br />
                        <asp:Label ID="lbl_Persona" runat="server" class="texto" Text="" /><br />
                        <asp:Label ID="lbl_Folio" runat="server" class="texto" Text="" /><br />
                        <asp:Label ID="lbl_Usuario2" runat="server" class="texto" Text="" /><br />
                        <asp:Label ID="lbl_Sucursal" runat="server" class="texto" Text="" /><br />
                        <asp:Label ID="lbl_TipoOpe" runat="server" class="texto" Text="" /><br /><br />
                    </div>

                    <asp:Label ID="lbl_subtitulo" runat="server"></asp:Label>

                    <div class= "module_subsec low_m columned three_columns">
	                    <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_Usr" runat="server" class="text_input_nice_input" MaxLength="15"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_Usr" runat="server" Text="Usuario:" class="text_input_nice_label"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Usr" runat="server" ControlToValidate="txt_Usr"
                                        CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Autorizar">
                                        </asp:RequiredFieldValidator>
                                </div>
		                    </div>
	                    </div>

	                    <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_Pwd" runat="server" class="text_input_nice_input" MaxLength="15" TextMode="Password"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_Pwd" runat="server" class="text_input_nice_label" Text="Contraseña:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Pwd" runat="server" ControlToValidate="txt_Pwd"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Autorizar">
                                </asp:RequiredFieldValidator>
                                </div>
		                    </div>
	                    </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements"> 
		                    <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_Acc" runat="server" class="btn btn-primary2 dropdown_label">
                                    <asp:ListItem Value="-1">ELIJA</asp:ListItem>
                                    <asp:ListItem Value="CANCELADO">CANCELAR</asp:ListItem>
                                    <asp:ListItem Value="RECHAZADO">RECHAZAR</asp:ListItem>
                                    <asp:ListItem Value="AUTORIZADO">AUTORIZAR</asp:ListItem>
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label ID="lbl_Acc" runat="server" class="texto" Text="Acción:"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Acc" CssClass="textogris"
                                    ControlToValidate="cmb_Acc" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                    ValidationGroup="val_Autorizar" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class= "module_subsec low_m columned three_columns flex_start">
                        <div class="module_subsec_elements" style="flex:1;">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_Mtv" runat="server" class="text_input_nice_textarea" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                        <asp:Label ID="lbl_Mtv" runat="server" class="texto" Text="Motivo:"></asp:Label>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Mtv" runat="server" Enabled="True" TargetControlID="txt_Mtv" 
                                        FilterType="Custom, UppercaseLetters, LowercaseLetters,numbers" ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,.">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Mtv" runat="server" ControlToValidate="txt_Mtv"
                                    CssClass="textogris" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_Autorizar">
                                </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="module_subsec_elements"> 
	                        <div class="text_input_nice_div module_subsec_elements_content">

                            </div>
                        </div>
                    </div>
                            
                    <br />

                    <div align="center">
                        <asp:Label ID="lbl_InfoAutoriza" runat="server" CssClass="textogris" ForeColor="Red"></asp:Label><br />
                        <asp:Button ID="btn_AutorPLD_AUTO" runat="server" class="btn btn-primary" Text="Aplicar" ValidationGroup="val_Autorizar" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_AutorPLD_CAN" runat="server" class="btn btn-primary" Text="Cancelar" />
                    </div>

                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

    </section>
        
</asp:Content>
