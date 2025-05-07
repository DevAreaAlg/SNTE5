<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CORE_OPE_CONTABILIDAD.aspx.vb" Inherits="SNTE5.CORE_OPE_CONTABILIDAD" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function BtnClick() {
             document.getElementById("Loading").innerHTML = document.getElementById("Loading").innerHTML;
             var lLoadingMessage = document.getElementById('<%=lblLoadingMessage.ClientID %>');
            var dvLoading = document.getElementById('<%=dvLoading.ClientID %>');
             var dvMain = document.getElementById('<%=dvMain.ClientID %>');
             var cmb1 = document.getElementById('<%=cmb_Qna.ClientID %>')
             var cmb2 = document.getElementById('<%=cmb_Tipo.ClientID %>')
             if (cmb1.value != "ELIJA" && cmb2.value != "ELIJA") { 
            if (dvMain != null) dvMain.style.display = 'none';
            if (dvLoading != null) dvLoading.style.display = '';            
                 return true;
             };
         }
     </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="dvLoading" runat="server" class="loadingMessageFrame" style="display: none;">
        <div class="module_subsec flex_center">
            <asp:Label ID="lblLoadingMessage" runat="server" Text="Procesando..." CssClass="Loading_Message"> </asp:Label>
        </div>
        <div class="module_subsec flex_center" id="Loading">
            <asp:Image ID="lblLoadingMessageGif" runat="server" ImageUrl="~/img/Loading.gif" />
        </div>
    </div>
    <div id="dvMain" runat="server">
        <section class="panel" >
            <header class="panel-heading">
                <span>Generar asientos contables</span>
            </header>

            <div class= "module_subsec low_m columned three_columns flex_end">
	            <div class="module_subsec_elements" style="flex:1;"> 
		            <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" ID="cmb_Tipo" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_Tipo" runat="server" CssClass="text_input_nice_label">Tipo Póliza:</asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_Divisas" Cssclass="textogris" ControlToValidate="cmb_Tipo" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!"  ValidationGroup="val_Poliza" initialvalue="ELIJA"/>
                        </div>
		            </div>
	            </div>

                <div class="module_subsec_elements" style="flex:1;"> 
		            <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:DropDownList runat="server" ID="cmb_Qna" class="btn btn-primary2 dropdown_label"></asp:DropDownList>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_Qna" runat="server" CssClass="text_input_nice_label">Periodo:</asp:Label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Cssclass="textogris" ControlToValidate="cmb_Qna" 
                                Display="Dynamic" ErrorMessage=" Falta Dato!"  ValidationGroup="val_Poliza" initialvalue="ELIJA"/>
                        </div>
		            </div>
	            </div>

                <div class="module_subsec_elements" style="flex:1;">
                    <div class="text_input_nice_div flex_end">
                            <asp:Button ID="btn_Generar" runat="server" ValidationGroup="val_Poliza" class="btn btn-primary"  Text="Generar" OnClientClick="BtnClick()"/>
                    </div>
                </div>
            </div>

            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox runat="server" ID="txt_nombre" CssClass="text_input_nice_input" MaxLength="100" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Nombre Póliza:" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_nombre" Display="Dynamic"
                                CssClass="alertaValidator bold" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_nombre" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="module_subsec low_m columned three_columns">
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox runat="server" ID="txt_num" CssClass="text_input_nice_input" MaxLength="10" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Número Póliza:" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_num" Display="Dynamic"
                                CssClass="alertaValidator bold" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txt_num" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox runat="server" ID="txt_concepto" CssClass="text_input_nice_input" MaxLength="100" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Concepto:" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_concepto" Display="Dynamic"
                                CssClass="alertaValidator bold" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters, Custom, numbers"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_concepto" />
                        </div>
                    </div>
                </div>
                <div class="module_subsec_elements">
                   <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox runat="server" ID="txt_referencia" CssClass="text_input_nice_input" MaxLength="31" />
                        <div class="text_input_nice_labels">
                            <asp:Label runat="server" CssClass="text_input_nice_label" Text="*Referencia:" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_referencia" Display="Dynamic"
                                CssClass="alertaValidator bold" ErrorMessage="Falta Dato!" ValidationGroup="val_Persona" />
                            <ajaxToolkit:FilteredTextBoxExtender runat="server" Enabled="True"
                                FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=" ,Á,É,Í,Ó,Ú,Ñ,á,é,í,ó,ú,ñ" TargetControlID="txt_referencia" />
                        </div>
                    </div>
                </div>
            </div>
                        
	        <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                <div style="display:flex;align-items:center;"> 
                    <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" style="margin-right:25px;font-size:18px;" ID="btn_EXCEL">
                        Descargar Póliza Excel
                        <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                    </asp:LinkButton>
            
                    <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" style="margin-right:25px;font-size:18px;" ID="btn_txt">
                        Descargar Póliza txt
                        <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                    </asp:LinkButton>
                </div>
            </div>

            <p align="center">
                <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>  
            </p>

            <div class="overflow_x shadow module_subsec">
                <asp:DataGrid ID="dag_divisas0" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    GridLines="None" HorizontalAlign="Center" TabIndex ="17" Width="100%">
                    <HeaderStyle CssClass="table_header" />
                    <Columns>
                        <asp:BoundColumn DataField="DIVISA" HeaderText="Divisa">
                            <ItemStyle Width="200px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VALOR" HeaderText="Valor">
                            <ItemStyle Width="100px" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>                        

        </section>        
           
        <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />        
   </div>
</asp:Content>

