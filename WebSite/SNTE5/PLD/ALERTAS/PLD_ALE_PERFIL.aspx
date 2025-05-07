<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_ALE_PERFIL.aspx.vb" Inherits="SNTE5.PLD_ALE_PERFIL" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function busquedapersonafisica(ctrl) {
            var wbusf = window.showModalDialog("UNI_BUSQUEDA_PERSONA.aspx?tipop=f&origen=INSIDE", wbusf, "center:yes;resizable:no;dialogHeight:600px;dialogWidth:650px");
            if (wbusf != null) {
                __doPostBack(ctrl, '');
            }
        }

        function onCancel() { }

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

        function his_PPE() {
            window.open("DetallePPE.aspx", "DR", "width=800px,height=650px,Scrollbars=YES");
        }
    </script>
    <section class="wrapper">
        
        <div class="row">
	        <div class="col-lg-12">
		        <h3 class="page-header"><i class="fa fa-file-text-o"></i> CONSULTA DE PERFIL</h3>
		        <ol class="breadcrumb">
                    <li><i class="fa fa-home"></i> <asp:LinkButton ID="lnk_Regresar" runat="server" class="texto_pnli">Inicio</asp:LinkButton></li>
                    <li><i class="icon_document_alt"></i><asp:Label ID="Label1" runat="server" CssClass="page-header ">PERFIL DE AFILIADOS</asp:Label></li>
                </ol>
	        </div>
        </div>

        <div class="tamano-cuerpo">

             <section class="panel" >
                <header class="panel-heading">
                    <span>AFILIADO</span>
                </header>
                <div class="panel-body">
                    <div class="module_subsec columned low_m align_items_flex_center" >
                        <asp:Label runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" ID="lbl_numcliente">Número de Cliente: </asp:Label>
                        <div class="module_subsec_elements module_subsec_medium-elements">
                            <asp:TextBox ID="txt_cliente" runat="server" CssClass="text_input_nice_input"></asp:TextBox>
                            &nbsp;
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_cliente" runat="server" Enabled="True" 
                                 FilterType="Numbers" TargetControlID="txt_cliente"></ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_cliente" Enabled="true" CssClass="textogris" 
                                ControlToValidate="txt_cliente" Display="Dynamic" ErrorMessage=" Falta Dato!" ValidationGroup="val_cliente" />
                        </div>
                        <div class="module_subsec_elements module_subsec_small-elements">
                            <asp:ImageButton runat="server" ImageUrl="~/img/img/glass.png" ID="btn_buscapersona" Style="height: 16px; width: 14px;"></asp:ImageButton>
                            &nbsp;
                            <asp:LinkButton ID="lnk_seleccionar" runat="server" class="btntextoazul" Text="Seleccionar" ValidationGroup="val_cliente" />
                        </div>
                    </div>
                </div>
            </section>
         
            <section class="panel" >
                <header class="panel-heading">
                    <span>DATOS ACTUALES</span>
                </header>
                <div class="panel-body">

                    <table width="100%">
                        <tr valign="top">
                            <td width="65%">
                                <div class="module_subsec columned low_m align_items_flex_center" >
                                    <asp:Label ID="lbl_clienteA" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag">Cliente:</asp:Label>
                                    <asp:Label ID="lbl_clienteB" runat="server" class="module_subsec_elements module_subsec_big-elements title_tag"></asp:Label>
                                </div>
                                <div class="module_subsec columned low_m align_items_flex_center" >
                                    <asp:Label ID="lbl_TipoPerfil" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" Text="Tipo Perfil de Persona:"></asp:Label>            
                                    <asp:Label ID="lbl_TipoPerfilM" runat="server" class="module_subsec_elements module_subsec_big-elements title_tag"></asp:Label>
                                </div>
                                <div class="module_subsec columned low_m align_items_flex_center" >
                                    <asp:Label ID="lbl_PerfilPersona" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" Text="Perfil de Persona:"></asp:Label>
                                    <asp:Label ID="lbl_PerfilPersonaM" runat="server" class="module_subsec_elements module_subsec_big-elements title_tag"></asp:Label>
                                </div>
                                <div class="module_subsec columned low_m align_items_flex_center" >
                                    <asp:Label ID="lbl_Puntaje" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" Text="Calificación:"></asp:Label>
                                    <asp:Label ID="lbl_PuntajeM" runat="server" class="module_subsec_elements module_subsec_big-elements title_tag"></asp:Label>
                                </div>
                                <div class="module_subsec columned low_m align_items_flex_center" >
                                    <asp:Label ID="lbl_GradoR" runat="server" class="module_subsec_elements module_subsec_medium-elements title_tag" Text="Grado de Riesgo:"></asp:Label>
                                    <asp:Label ID="lbl_GradoRM" runat="server" class="module_subsec_elements module_subsec_big-elements title_tag"></asp:Label>
                                </div>
                            </td>
                            <td width="35%">
                                <br />
                                <asp:LinkButton ID="lnk_HistorialAlertas" runat="server" CssClass="textogris" Enabled="False" Text="Historial Alertas"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="lnk_PersonaPolitica" runat="server" CssClass="textogris" Enabled="False" Text="Persona Politica"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="lnk_HistorialPerfiles" runat="server" CssClass="textogris" Enabled="False" Text="Historial Perfil"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </section>

            <section class="panel" >
                <header class="panel-heading">
                    <span>CALIFICACIÓN</span>
                </header>
                <div class="panel-body">
                    
                    <table width="100%">
                        <tr>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" width="200px" align="center">
                                <asp:Label ID="lbl_TopFactor" runat="server" class="texto" Text="Factor"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" width="90px" align="center">
                                <asp:Label ID="lbl_TopPond" runat="server" class="texto" Text="Ponderación"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" width="300px" align="center">
                                <asp:Label ID="lbl_TopFactPer" runat="server" class="texto" Text="Factor Persona"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" width="90px" align="center">
                                <asp:Label ID="lbl_TopPunt" runat="server" class="texto" Text="Puntaje"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" width="90px" align="center">
                                <asp:Label ID="lbl_TopPuntPond" runat="server" class="texto" Text="Calificación"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorActeco" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondActeco" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerActeco" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntActeco" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondActeco" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorPEP" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondPEP" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerPEP" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPEP" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondPEP" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorPais" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondPais" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerPais" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPais" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondPais" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorFormaJur" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondFormaJur" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerFormaJur" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntFormaJur" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondFormaJur" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorServicio" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondServicio" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerServicio" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntServicio" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 1px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondServicio" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactorTotal" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PondTotal" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" align="left">
                                <asp:Label ID="lbl_FactPerTotal" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntTotal" runat="server" class="texto"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-width: 2px; border-color: #054B66;" align="center">
                                <asp:Label ID="lbl_PuntPondTotal" runat="server" class="texto"></asp:Label>
                            </td>
                        </tr>
                    </table>
                               
                </div>
            </section>

            <p align="center">
                <asp:Label ID="lbl_info" runat="server" CssClass="textogris" ForeColor="Red" Text="" Visible="True"></asp:Label>
            </p>           
           
            <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
       
    </section>
</asp:Content>
