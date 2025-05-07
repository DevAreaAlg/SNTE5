<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="COB_DESP_EXP.aspx.vb" Inherits="SNTE5.COB_DESP_EXP" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="panel">
        <header class="panel-heading">
            Búsqueda de expediente
        </header>

        <div class="panel-body">         
        
            <div class="module_subsec">
                Puede filtrar los préstamos: por sucursal, días de mora y/o abogado/despacho ó simplemente dar clic en consultar para ver todos los préstamos entregados.
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:DropDownList ID="cmb_sucursal" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>                                                       
                    <span class="text_input_nice_label">Sucursal:</span>                                          
                </div>
                  
                <div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:DropDownList ID="cmb_despacho" runat="server" CssClass="btn btn-primary2 dropdown_label"></asp:DropDownList>
                    <span class="text_input_nice_label">Abogado/Despacho:</span>
                </div>

				<div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:TextBox ID="txtfolio" class="text_input_nice_input" runat="server" MaxLength="10"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Folio:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filterfolio" runat="server"
                            Enabled="True" FilterType="Numbers" TargetControlID="txtfolio">
                        </ajaxToolkit:FilteredTextBoxExtender>                            
                    </div>
                </div>
            </div>                
                        
            <div class="module_subsec columned low_m no_rm three_columns">
				<div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:TextBox ID="txt_minimo" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Días de mora (mínimo):</span>                             
                        <ajaxToolkit:FilteredTextBoxExtender ID="filterminimo" runat="server"
                            Enabled="True" FilterType="Numbers" TargetControlID="txt_minimo">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>
					
				<div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:TextBox ID="txtmaximo" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Días de mora (máximo):</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filtermaximo" runat="server"
                            Enabled="True" FilterType="Numbers" TargetControlID="txtmaximo">
                        </ajaxToolkit:FilteredTextBoxExtender>                        
                    
            </div>
            </div>                  
                  
                <div class="module_subsec_elements text_input_nice_div flex_1">
                    <asp:TextBox ID="txtnumcliente" class="text_input_nice_input" runat="server" MaxLength="9"></asp:TextBox>
                    <div class="text_input_nice_labels">
                        <span class="text_input_nice_label">Número de afiliado:</span>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filternumcliente" runat="server"
                            Enabled="True" FilterType="Numbers" TargetControlID="txtnumcliente">
                        </ajaxToolkit:FilteredTextBoxExtender>                                     
                    </div>
                </div>					  
            </div>
                        
            <div class="module_subsec flex_end">
                <asp:Button ID="btn_consultar" runat="server" Text="Buscar" OnClick="btn_consultar_Click" class="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_eliminar" runat="server" Text="Eliminar" onclick="btneliminar_Click" class="btn btn-primary"/>
            </div>
               
            <div class="module_subsec flex_center">
                <asp:Label runat="server" ID="lbl_estatus" CssClass="alerta"></asp:Label>
            </div>

            <section class="panel">
                <div class="shadow module_subsec">           
                    <asp:DataGrid ID="dag_expedientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn ItemStyle-Width="5%" DataField="FOLIO" HeaderText="Folio"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="5%" DataField="IDPERSONA" HeaderText="Núm. afiliado"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="20%" DataField="NOMBRE" HeaderText="Nombre"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="15%" DataField="SUCURSAL" HeaderText="Sucursal"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="10%" DataField="FECHAULTPAGO" HeaderText="Fecha última amortización"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="15%" DataField="MORACAPITAL" HeaderText="Días mora capital"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="15%" DataField="MORAINTERES" HeaderText="Días mora interés"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="15%" DataField="TIPOCARTERA" HeaderText="Tipo cartera"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="20%" DataField="IDDESPACHO" HeaderText="Id"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-Width="20%" DataField="DESPACHO" HeaderText="Abogado/Despacho"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="EDITAR" HeaderText="" Text="Editar">
                                <ItemStyle Width="15%" />
                            </asp:ButtonColumn>                   
                        </Columns>
                    </asp:DataGrid>
                </div>
            </section>
            <%--MODAL--%>
            <asp:Panel ID="pnl_evalx" runat="server" CssClass="modalPopup">
            <%--    <asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
                <asp:Panel ID="pnl_tit_asignacion" runat="server" CssClass="modalHeader">
                    <asp:Label ID="LBL_MODport" runat="server" class="modalTitle" Text="Abogado/Despacho"></asp:Label>
                </asp:Panel>
         
                    <div class="module_subsec low_m columned two_columns">                                    
                        <div class=" module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_fase" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                    AutoPostBack="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Fase:</span>
                                    <asp:RequiredFieldValidator runat="server" ID="req_asentamiento" CssClass="alertaValidator"
                                        ControlToValidate="cmb_fase" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                        ValidationGroup="val_indic" InitialValue="0" />
                                </div>
                            </div>
                        </div>

                        <div class=" module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList ID="cmb_despacho_asignar" CssClass="btn btn-primary2 dropdown_label" runat="server"
                                    AutoPostBack="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">*Abogado/Despacho:</span>
                                </div>
                            </div>
                        </div>

                        <div class=" module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:TextBox ID="txt_valor" class="text_input_nice_input" runat="server" MaxLength="5"></asp:TextBox>                                          
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Valor:</span>
                                    <asp:RequiredFieldValidator ID="req_valor" runat="server" ControlToValidate="txt_valor"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_indic">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" Enabled="True"
                                          FilterType="Numbers, Custom" ValidChars="." TargetControlID="txt_valor">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" Display="Dynamic"
                                    runat="server" ControlToValidate="txt_valor" CssClass="textogris" ErrorMessage=" Error:Monto incorrecto"
                                    ValidationExpression="^[0-9]+(\.[0-9]{1}[0-9]?)?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div> 
                    </div>
                         
                    <div class="module_subsec low_m columned two_columns">
                        <div class="module_subsec_elements flex_1">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:TextBox ID="txt_Objetivo" runat="server" class="text_input_nice_textarea" MaxLength="3000"
                                        ValidationGroup="val_conf" TextMode="MultiLine" onKeyUp="javascript:Check(this, 3000);"
                                        onChange="javascript:Check(this, 3000);"></asp:TextBox>
                                <div class="text_input_nice_labels">
                                    <span class="text_input_nice_label">Motivo:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Objetivo"
                                        CssClass="alertaValidator bold" Display="Dynamic" ErrorMessage="Falta Dato!" ValidationGroup="val_indic">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_objetivo" runat="server" Enabled="True" 
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txt_Objetivo" 
                                        ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ,."></ajaxToolkit:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>                            
                    </div>
                             
                    <div class="module_subsec flex_center">
                        <asp:Label ID="lbl_status_modal" runat="server" CssClass="alerta"></asp:Label>
                    </div>

                    <div class="module_subsec low_m columned two_columns">
                        <div class="module_subsec_elements flex_end">                    
                            <asp:Button ID="btn_guarda_modal" runat="server" class="btn btn-primary" ValidationGroup="val_indic" Text="Guardar" Width="90%"/>                    
                        </div>                
                        <div class="module_subsec_elements flex_start">
                            <asp:Button ID="btn_evalxcerrar" runat="server" class="btn btn-primary" Text="Cancelar"  Width="90%"/>
                        </div>
                    </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>
            <asp:HiddenField ID="hdn_ctrl" runat="server" />

            <ajaxToolkit:ModalPopupExtender ID="modal_port" runat="server" BackgroundCssClass="modalBackground"
            DropShadow="True" Enabled="True" PopupControlID="pnl_evalx" PopupDragHandleControlID="pnl_subevalx"
            TargetControlID="hdn_ctrl" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>

            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Label runat="server" ID="lbl_AlertaDesExpedienteDig" Cssclass="alerta"></asp:Label>
            </div>

            <div class="module_subsec flex_center low_m align_items_flex_center">
                <asp:Label ID="lbl_status" runat="server" Cssclass="alerta"></asp:Label> 
            </div>

            <div class="center">
                        <%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_asignacion" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" DynamicServicePath="" Enabled="True" PopupControlID="pnl_asignacion"
                            PopupDragHandleControlID="pnl_tit_asignacion" TargetControlID="hdn_asignacion">
                        </ajaxToolkit:ModalPopupExtender>--%> 
            </div>
            
        </div>        
    </section>       
    
</asp:Content>
