<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="CRED_EXP_INVESTIGACIONES.aspx.vb" Inherits="SNTE5.CRED_EXP_INVESTIGACIONES" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="panel">
        <header class="panel-heading">
            <span><asp:Label ID="lbl_Folio" runat="server" Text="Datos del Expediente: " Enabled="false" ></asp:Label></span>
        </header>
        <div class="panel-body">
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Afiliado:</h5>
                <asp:Textbox ID="lbl_Prospecto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div>
            <div class="module_subsec columned low_m align_items_flex_center">
                <h5 class="module_subsec_elements module_subsec_small-elements">Producto:</h5>
                <asp:Textbox ID="lbl_Producto" runat="server" Enabled="false" CssClass="module_subsec_elements text_input_nice_input flex_1"></asp:Textbox>
            </div> 
        </div>
    </section>

    <section class="panel" >
        <header class="panel-heading">
            <span>Comparación de ingresos y egresos</span>
        </header> 
        <div class="panel-body">

                <asp:Panel ID="pnl_ingresos" runat="server" Height="264px"  CssClass="texto" Font-Bold="True">
                    <div class="center">
                        <asp:Label ID="lbl_fechaact" runat="server" Cssclass="textogris" 
                            Font-Bold="True" Font-Italic="False" Height="16px"></asp:Label><br />
                        <asp:Label ID="Label6" runat="server" CssClass="texto" Font-Bold="True" Text="INGRESOS MENSUALES" Width="205px"></asp:Label>
                    </div>
                                           
                        <br />
                    <table border="0" runat="server" align="center">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbl_tit_cliente" runat="server" CssClass="texto" Font-Bold="True" 
                                Font-Italic="False" Text="AFILIADO" Width="208px" Visible="False"></asp:Label>
                            </td>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbl_ingconyuge" runat="server" CssClass="texto" Font-Bold="True" 
                                Font-Italic="False" Text="CODEUDOR(ES)" Width="208px" Visible="False"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <asp:Label ID="lbl_empleo" runat="server" CssClass="texto" Text="EMPLEO:" Width="124px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_montoempleo" runat="server" CssClass="texto" Width="125px" Text="-"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_empleocony" runat="server" CssClass="texto" 
                                Font-Bold="False" Font-Italic="False" Text="EMPLEO:" Width="124px" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_mempleocony" runat="server" CssClass="texto" Width="125px" Visible="False">-</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_adicional" runat="server" CssClass="texto" Text="ADICIONAL:" Width="124px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_montoadicional" runat="server" CssClass="texto" Width="125px">-</asp:Label>
                            </td>
                            <td>                           
                                <asp:Label ID="lbl_adicionalcony" runat="server" CssClass="texto" 
                                Font-Bold="False" Font-Italic="False" Text="ADICIONAL:" Width="124px" Visible="False"></asp:Label>
                            </td>
                            <td>                                                          
                                <asp:Label ID="lbl_montoadicionalcony" runat="server" CssClass="texto" Width="125px" Visible="False">-</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_otros" runat="server" CssClass="texto" Text="OTROS:" Width="124px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_montootros" runat="server" CssClass="texto" Width="125px">-</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_otroscony" runat="server" CssClass="texto" Font-Bold="False" 
                                Font-Italic="False" Text="OTROS:" Width="124px" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_montootroscony" runat="server" CssClass="texto" Width="125px" Visible="False">-</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_subtotal" runat="server" CssClass="texto" Font-Bold="True" 
                                Font-Italic="True" Text="SUB-TOTAL:" Width="124px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_montosub" runat="server" CssClass="texto" Width="125px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_subtotalconyuge" runat="server" CssClass="texto" 
                                Font-Bold="True" Font-Italic="True" Text="SUB-TOTAL:" Width="124px" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbl_subcony" runat="server" CssClass="texto"  Width="125px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lbl_total" runat="server" Cssclass="textogris" Font-Bold="True" 
                                Font-Italic="False" Text="TOTAL INGRESOS:" Width="124px" Height="16px"></asp:Label>
                                <asp:Label ID="lbl_montototal" runat="server" CssClass="textocajas" Font-Bold="True" Height="16px" Width="191px"></asp:Label>
                            </td>                         
                        </tr>
                    </table>
                     
                </asp:Panel>

                <asp:Panel ID="pnl_invSocio" runat="server" >

                    <table border="0" align="center">
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="INVESTIGACION DE CAMPO" CssClass="texto" 
                                Width="205px" ID="Label3" Font-Bold="True"></asp:Label> 
                            </td>
                            <td align="center">
                                <asp:Label runat="server" Text="ESTUDIO SOCIOECONÓMICO" CssClass="texto" 
                                Width="205px" ID="Label4" Font-Bold="True"></asp:Label>
                            </td>
                            <td colspan="1" align="center" Width="205px"></td>
                        </tr>
                    </table>

                    <table border="0" align="center">
                            
                        <tr valign="top">
                            <td class="style4">
                                <asp:Panel ID="pnl_investigacion" runat="server" CssClass="texto" Font-Bold="True" Width="233px">
                                    <asp:Label ID="lbl_statusic" runat="server" Cssclass="textogris" Font-Bold="True" Font-Italic="False" Height="16px"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_titic" runat="server" CssClass="texto" Font-Bold="True" >GASTOS</asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbl_titcantidad" runat="server" CssClass="texto" Font-Bold="True" Text="CANTIDAD" Width="91px"></asp:Label>
                                    <br /><br />          
                                </asp:Panel>
                                <br />
                            </td>
                            <td>
                                <asp:Panel ID="pnl_socioeconomico" runat="server" CssClass="texto" Width="282px">
                                    <asp:Label ID="lbl_statuses" runat="server" Cssclass="textogris" Font-Bold="True" Font-Italic="False" Height="16px"></asp:Label>
                                    <br /> &nbsp;
                                    <asp:Label runat="server" CssClass="texto" ID="lbl_titsocioeconomico" 
                                        Font-Bold="True" EnableTheming="False">GASTOS</asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lbl_titcantidadeg" runat="server" CssClass="texto" Font-Bold="True" Text="CANTIDAD" Width="105px"></asp:Label>
                                        <br /><br />
                                </asp:Panel>
                                <br />
                            </td>
                            <td>
                                <asp:Panel ID="pnl_diferencia" runat="server" CssClass="texto" Width="187px">
                                    <br />
                                    <asp:Label ID="lbl_cantidad" runat="server" CssClass="texto" Font-Bold="True" Text="DIFERENCIA" ></asp:Label>
                                    <br /><br />                               
                                </asp:Panel>
                                <br />
                            </td>
                        </tr>
                                          
                    </table> 

                </asp:Panel>
                  
                <p align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbl_simbolo" runat="server" Cssclass="textogris" Font-Bold="True" Font-Italic="False"
                        Text="-------------------------------" Width="211px" Height="16px"></asp:Label>
                </p>
                                    
            </div>
        </section>

</asp:Content>
