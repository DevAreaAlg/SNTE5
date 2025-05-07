<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="UNI_RED_SOCIAL.aspx.vb" Inherits="SNTE5.UNI_RED_SOCIAL" MaintainScrollPositionOnPostback ="true" %>

<%@ MasterType  virtualPath="~/MasterMascore.master"%>
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
    </script>

    <asp:Panel ID="pnl_cliente" class="panel" runat="server" >
        <header class="panel-heading">
            <span>Datos Afiliado</span>
        </header>
        <div class="panel-body">
                               
            <div class="module_subsec columned   three_columns">
                <div class="module_subsec_elements w_50">
                    <div class="text_input_nice_div module_subsec_elements_content">
                        <asp:TextBox ID="txt_IdCliente1" runat="server" MaxLength="10" class="text_input_nice_input"></asp:TextBox>
                         <asp:TextBox ID="txt_IdCliente" runat="server" MaxLength="10" class="text_input_nice_input" Visible="false"></asp:TextBox>
                        <div class="text_input_nice_labels">
                            <asp:Label ID="lbl_BusquedaPersona0" runat="server" CssClass="text_input_nice_label" Text="Ingrese el número de afiliado:"></asp:Label>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender__idcliente" runat="server"
                                TargetControlID="txt_idCliente" FilterType="Numbers" Enabled="True" />
                        </div>
                    </div>
                </div>
                <div class="flex_1 module_subsec module_subsec_elements no_m no_column" style="margin-bottom: 10px;">
                    <asp:LinkButton ID="lnk_Continuar" CssClass="btn btn-primary module_subsec_elements no_tbm " runat="server" Text="Continuar"  />
                    <asp:Button ID="lnk_BusquedaPersona" CssClass="btn btn-primary module_subsec_elements no_tbm" runat="server" Text="Buscar Afiliado" />
                </div>
            </div>

            <div class="module_subsec columned low_m align_items_flex_center">
                <span class="module_subsec_medium-elements module_subsec_elements no_m">Nombre del afiliado: </span>
                <asp:Label ID="lbl_NombrePersonaBusqueda" runat="server" Enabled="false"></asp:Label>                                    
            </div>                           
        </div>
    </asp:Panel>

     <asp:Label ID="lbl_statusc" runat="server" CssClass="module_subsec low_m flex_center alerta"></asp:Label>

    <asp:Panel class="panel" id="pnl_redsocial" runat="server">
        <header class="panel-heading">
            <span>Red social</span>
        </header>
            <div class="panel-body">

                <div class="module_subsec flex_start">
                    <asp:LinkButton ID="lnk_regorig" runat="server" class="texto" Text="Regresar afiliado origen"></asp:LinkButton> &nbsp;&nbsp;                       
                    <asp:LinkButton ID="lnk_regniv" runat="server" class="texto" Text="Regresar un nivel"></asp:LinkButton>
                </div>
                    <div runat="server" id="pnl_informacion">                    
                <!--DATOS CONYUGE-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_conyuge" runat="server" TargetControlID="ContentPanel_conyuge" 
                    ExpandControlID="HeaderPanel_conyuge" CollapseControlID="HeaderPanel_conyuge" Collapsed="True" ImageControlID="ToggleImage_conyuge"
                    Enabled="True" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg"
                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_conyuge" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_conyuge" runat="server" />
                        Cónyuge
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_conyuge" runat="server" Style="overflow: hidden;" >
                    <div class="module_subsec">
                        <asp:Label ID="lbl_conyuges" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                    <asp:DataGrid ID="dag_conyuges" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                        GridLines="None" Width="100%">
                        <HeaderStyle CssClass="table_header" />
                        <Columns>
                            <asp:BoundColumn DataField="idcon" HeaderText="Núm. afiliado">
                                <ItemStyle Width="10%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="nombre" HeaderText="Cónyuge">
                                <ItemStyle Width="35%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="regimen" HeaderText="Régimen">
                                <ItemStyle Width="20%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="dependientes" HeaderText="Dependientes">
                                <ItemStyle Width="10%"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                <ItemStyle Width="15%" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                <ItemStyle Width="10%" />
                            </asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                    </div>
                        
                    <div class="module_subsec">
                    <asp:Label ID="lbl_conyugespor" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" >  
                        <asp:DataGrid ID="dag_conyugespor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="idpersona" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Cónyuge">
                                    <ItemStyle Width="35%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="regimen" HeaderText="Régimen">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="dependientes" HeaderText="Dependientes">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                            
                <!--DATOS AVALES-->
                <ajaxToolkit:CollapsiblePanelExtender ID="avalesProperties" runat="server" TargetControlID="ContentPanel_avales"
                    ExpandControlID="HeaderPanel_avales" CollapseControlID="HeaderPanel_avales" Collapsed="True"
                    ImageControlID="ToggleImage_avales" Enabled="True" ExpandedImage="~/img/collapse.jpg"
                    CollapsedImage="~/img/expand.jpg" ExpandedText="Contraer" CollapsedText="Expandir"
                    SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_avales" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_avales" runat="server" />
                        Avales
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_avales" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_avales" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_avales" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None"  Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idaval" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Aval">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                          
                    <div class="module_subsec">
                        <asp:Label ID="lbl_avalespor" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_avalespor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idpersona" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Aval">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                            
                <!--DATOS CODEUDORES-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_codeudor" runat="server"
                    TargetControlID="ContentPanel_codeudor" ExpandControlID="HeaderPanel_codeudor"
                    CollapseControlID="HeaderPanel_codeudor" Collapsed="True" ImageControlID="ToggleImage_codeudor"
                    Enabled="True" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg"
                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_codeudor" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_codeudor" runat="server" />
                        Codeudores
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_codeudor" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_codeudores" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_codeudores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idcodeudor" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Codeudor">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec">
                        <asp:Label ID="lbl_codeudorespor" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_codeudorespor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle  Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idpersona" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Codeudor">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                            
                <!--DATOS REFERENCIAS-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_referencias" runat="server"
                    TargetControlID="ContentPanel_referencia" ExpandControlID="HeaderPanel_referencia"
                    CollapseControlID="HeaderPanel_referencia" Collapsed="True" ImageControlID="ToggleImage_referencia"
                    Enabled="True" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg"
                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_referencia" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_referencia" runat="server" />
                        Referencias
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_referencia" runat="server" Style="overflow: hidden;;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_referencias" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_referencias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="cverefe" HeaderText="Referencia">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idrefe" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Referencia">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="tiempo" HeaderText="Años conocer">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec">
                        <asp:Label ID="lbl_referenciaspor" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_referenciaspor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="cverefe" HeaderText="Referencia">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idper" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Referencia">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="tiempo" HeaderText="Años conocer">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="folio" HeaderText="Folio">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fechainicio" HeaderText="Fecha exp">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                         
                <!--DATOS DEPENDIENTES ECONOMICOS-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_depeco" runat="server"
                    TargetControlID="ContentPanel_depeco" ExpandControlID="HeaderPanel_depeco" CollapseControlID="HeaderPanel_depeco"
                    Collapsed="True" ImageControlID="ToggleImage_depeco" Enabled="True" ExpandedImage="~/img/collapse.jpg"
                    CollapsedImage="~/img/expand.jpg" ExpandedText="Contraer" CollapsedText="Expandir"
                    SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_depeco" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_depeco" runat="server" />
                        Dependientes Económicos
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_depeco" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_depeco" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_depeco" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="iddepeco" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="50px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="100px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                    <ItemStyle Width="100px" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="40px" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec">
                        <asp:Label ID="lbl_depecopor" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_depecopor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idpersona" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="100px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                    <ItemStyle Width="100px" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="CONSULTAR" HeaderText="" Text="Consultar">
                                    <ItemStyle Width="40px" />
                                </asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                           
                <!--DATOS MIEMBROS DEL CONSEJO-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_consejo" runat="server"
                    TargetControlID="ContentPanel_consejo" ExpandControlID="HeaderPanel_consejo"
                    CollapseControlID="HeaderPanel_consejo" Collapsed="True" ImageControlID="ToggleImage_consejo"
                    Enabled="True" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg"
                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_consejo" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_consejo" runat="server" />
                        Consejo de Administración
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_consejo" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_consejo" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="shadow module_subsec" > 
                        <asp:DataGrid ID="dag_consejo" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idmconsejo" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle Width="25%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="estatus" HeaderText="Estatus">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                           
                <!--DATOS PERSONAS POLITICAMENTE EXPUESTAS-->
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_expuesta" runat="server"
                    TargetControlID="ContentPanel_expuesta" ExpandControlID="HeaderPanel_expuesta"
                    CollapseControlID="HeaderPanel_expuesta" Collapsed="True" ImageControlID="ToggleImage_expuesta"
                    Enabled="True" ExpandedImage="~/img/collapse.jpg" CollapsedImage="~/img/expand.jpg"
                    ExpandedText="Contraer" CollapsedText="Expandir" SuppressPostBack="True" />
                <asp:Panel ID="HeaderPanel_expuesta" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ToggleImage_expuesta" runat="server" />
                        Personas Políticamente Expuestas
                    </div>
                </asp:Panel>
                <asp:Panel ID="ContentPanel_expuesta" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_expuesta" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_expuesta" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="alerta">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="semaforo" Visible="false">
                                </asp:BoundColumn>--%>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image ID="Semaforoimg" runat="server" AlternateText="BLANK" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="motivo" HeaderText="Alerta">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="idexpuesta" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="5%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="relacion" HeaderText="Relación">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="puesto" HeaderText="Estatus">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="fecha" HeaderText="Fecha asignación">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </asp:Panel>
                            
                <!--CREDITOS EN RIESGO COMUN-->
                <ajaxToolkit:CollapsiblePanelExtender ID="cpe_cred_rel" runat="server" TargetControlID="cp_cred_rel"
                    ExpandControlID="hp_cred_rel" CollapseControlID="hp_cred_rel" Collapsed="True"
                    ImageControlID="ti_cred_rel" Enabled="True" ExpandedImage="~/img/collapse.jpg"
                    CollapsedImage="~/img/expand.jpg" ExpandedText="Contraer" CollapsedText="Expandir"
                    SuppressPostBack="True" />
                <asp:Panel ID="hp_cred_rel" runat="server" class="collapsable">
                    <div class="texto">
                        <asp:ImageButton ID="ti_cred_rel" runat="server" />
                        Préstamos en Riesgo Común
                    </div>
                </asp:Panel>
                <asp:Panel ID="cp_cred_rel" runat="server" Style="overflow: hidden;">
                    <div class="module_subsec">
                        <asp:Label ID="lbl_cred_rel_dep" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_cred_rel_dep" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="50px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DIR" HeaderText="Dirección">
                                    <ItemStyle Width="230px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PAR" HeaderText="Parentesco">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec">
                        <asp:Label ID="lbl_cred_rel_dep_de" runat="server" CssClass="resalte_azul"></asp:Label>
                    </div>

                    <div class="overflow_x shadow module_subsec" > 
                        <asp:DataGrid ID="dag_cred_rel_dep_de" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="table_header" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_PERSONA" HeaderText="Núm. afiliado">
                                    <ItemStyle Width="10%"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMBRE" HeaderText="Nombre">
                                    <ItemStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DIR" HeaderText="Dirección">
                                    <ItemStyle Width="230px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PAR" HeaderText="Parentesco">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Folio">
                                    <ItemStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ESTATUS" HeaderText="Estatus">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MONTO" HeaderText="Monto">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle Width="80px" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>

                    <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                        <div style="display:flex;align-items:center;"> 
                            <asp:LinkButton runat="server" ValidationGroup="generar_reporte_param" style="margin-right:25px;font-size:18px;" ID="exp_calc">
                                Exportar a Excel
                                <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
                        </div> 
            </div>
    </asp:Panel>

            <input type="hidden" name="hdn_nombrebusqueda" id="hdn_nombrebusqueda" value="" runat="server" />
       
</asp:Content>

