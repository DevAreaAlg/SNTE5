<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_TES_ENVIO_EFECTIVO.aspx.vb" Inherits="SNTE5.CRED_TES_ENVIO_EFECTIVO" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function cons() {
            window.open("CRED_TES_REGISTRO_ENVIO.aspx", "RP", "width=1250px;");
        }        
    </script>

    <section class="panel">
            <header class="panel-heading">
                <span>Envío de Cheques/Efectivo</span>
            </header>
            <div class="panel-body">                     
                    <div class="module_subsec flex_end">                               
                        <asp:LinkButton ID="lnk_constancias" runat="server" class="btntextoazul" Text="Envíos Pendientes"></asp:LinkButton>                               
                    </div>

                    <div class="module_subsec low_m columned four_columns">
                        <div class="module_subsec_elements flex-end"></div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <div class="text_input_nice_labels">
                                    <asp:RadioButton ID="rad_sucursal_ori" runat="server" CssClass="texto" Text="Sucursal"
                                        GroupName="origen_envio" AutoPostBack="True" />
                                        &nbsp;
                                    <asp:RadioButton ID="rad_banco_ori" runat="server" CssClass="texto" Text="Banco"
                                        GroupName="origen_envio" AutoPostBack="True" />
                                </div>
                                <asp:Label runat="server" class="texto" ID="lbl_ins3" Text="Indique el origen del envío:" />
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">                                   
                                <div class="text_input_nice_labels">
                                    <asp:RadioButton ID="rad_sucursal" runat="server" CssClass="texto" Text="Sucursal"
                                        GroupName="destino_envio" AutoPostBack="True" />
                                        &nbsp;
                                    <asp:RadioButton ID="rad_banco" runat="server" CssClass="texto" Text="Banco" GroupName="destino_envio"
                                        AutoPostBack="True" />
                                </div>
                                <asp:Label runat="server" class="texto" ID="lbl_ins2" Text="Indique el destino del envío:" />
                            </div>
                        </div>
                    </div>


                    <div align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lbl_pasig" runat="server" CssClass="texto" Text="Cheques en sucursal"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pasig_info" runat="server" CssClass="texto" Text="Banco - Num. Cta - Num. Cheque - Monto - Fecha recepción"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="Lst_en_sucursal" SelectionMode="Multiple" CssClass="textocajas"
                                        runat="server" Height="184px" Width="570px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton runat="server" ImageUrl="~/img/expand.jpg" ID="btn_add"></asp:ImageButton>
                                    &nbsp;
                                <asp:ImageButton runat="server" ImageUrl="~/img/collapse.jpg" ID="btn_rem"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lbl_pdisp" runat="server" CssClass="texto" Text="Cheques a enviar"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_pdisp_info" runat="server" CssClass="texto" Text="Banco - Num. Cta - Num. Cheque - Monto - Fecha recepción"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="Lst_empaquetados" SelectionMode="Multiple" CssClass="textocajas"
                                        runat="server" Height="184px" Width="570px"></asp:ListBox>
                                </td>
                            </tr>
                        </table>

                         <br />
                        <asp:Label runat="server" CssClass="textonegritas" ID="Label1" Text="Envío efectivo" />
                       
                        <div class="module_subsec low_m columned three_columns">
                            <div class="module_subsec_elements"></div>

                            <div class="module_subsec_elements">
                                <div class="text_input_nice_div module_subsec_elements_content">
                                    <asp:TextBox ID="txt_monto" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                                    <div class="text_input_nice_labels">
                                        <asp:Label runat="server" CssClass="texto" ID="lbl_monto" Text="Monto($):" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_monto" runat="server"
                                            Enabled="True" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txt_monto">
                                        </ajaxToolkit:FilteredTextBoxExtender><br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_monto" runat="server"
                                            class="textogris" ControlToValidate="txt_monto" ErrorMessage=" Monto incorrecto!"
                                            ValidationExpression="^[0-9]*(\.[0-9]{1}[0-9]?)?$" Display="Dynamic" ValidationGroup="val_operacion"></asp:RegularExpressionValidator>
                                     </div>
                                </div>
                            </div>
                        </div>

                    <div class="module_subsec low_m columned three_columns">
                        
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:DropDownList runat="server" ID="cmb_ori" class="btn btn-primary2 dropdown_label" Visible="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                     <asp:Label runat="server" class="texto" ID="lbl_suc_ori" Text="Origen:" Visible="False" />
                                    <asp:RequiredFieldValidator runat="server" ID="req_ori" CssClass="alertaValidator" 
                                                ControlToValidate="cmb_ori" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_envio" InitialValue="-1" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:DropDownList runat="server" ID="cmb_des" class="btn btn-primary2 dropdown_label" Visible="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" class="texto" ID="lbl_suc_des" Text="Destino:" Visible="False" />
                                    <asp:RequiredFieldValidator runat="server" ID="req_des" CssClass="alertaValidator" 
                                                ControlToValidate="cmb_des" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_envio" InitialValue="-1" />
                                </div>
                            </div>
                        </div>
                    </div>                        

                    <div class="module_subsec low_m columned three_columns">
                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:DropDownList runat="server" ID="cmb_envia" class="btn btn-primary2 dropdown_label" Visible="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" class="texto" ID="lbl_envia" Text="Envia:" Visible="False" />
                                    <asp:RequiredFieldValidator runat="server" ID="req_envia" CssClass="alertaValidator" 
                                                ControlToValidate="cmb_envia" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_envio" InitialValue="-1" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                <asp:DropDownList runat="server" ID="cmb_transporta" class="btn btn-primary2 dropdown_label" Visible="False">
                                </asp:DropDownList>                                
                                <div class="text_input_nice_labels">
                                     <asp:Label runat="server" class="texto" ID="lbl_transporta" Text="Transporta:" Visible="False" />
                                     <asp:RequiredFieldValidator runat="server" ID="req_transporta" CssClass="alertaValidator" 
                                                ControlToValidate="cmb_transporta" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_envio" InitialValue="-1" />
                                </div>
                            </div>
                        </div>

                        <div class="module_subsec_elements">
                            <div class="text_input_nice_div module_subsec_elements_content">
                                 <asp:DropDownList runat="server" ID="cmb_recibe" class="btn btn-primary2 dropdown_label" Visible="False">
                                </asp:DropDownList>
                                <div class="text_input_nice_labels">
                                    <asp:Label runat="server" class="texto" ID="lbl_recibe" Text="Recibe:" Visible="False" />
                                    <asp:RequiredFieldValidator runat="server" ID="req_recibe" CssClass="alertaValidator" 
                                                ControlToValidate="cmb_recibe" Display="Dynamic" ErrorMessage=" Falta Dato!"
                                                ValidationGroup="val_envio" InitialValue="-1" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div align="center">
                        <asp:Button ID="btn_enviar" runat="server"  class="btn btn-primary" Text="Enviar"
                            ValidationGroup="val_envio" Visible="False" UseSubmitBehavior="False" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_registro" runat="server" class="btn btn-primary" Text="Imprimir Constancia"
                            Visible="False" Enabled="False" />
                            <br />
                        <asp:Label runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
                            ForeColor="Red" ID="lbl_status"></asp:Label>
                    </div>
                    
                
    </div>
            </div>
    </section>

</asp:Content>
