<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="VAL_VALIDACION_ADM.aspx.vb" Inherits="SNTE5.VAL_VALIDACION_ADM" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                
    <section class="panel">
        <header class="panel-heading">
            <span>Cola de validación</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec  flex_end">
                    <asp:Button ID="btn_actualizar" class="btn btn-primary" runat="server" OnClick="btn_actualizar_Click" Text="Actualizar" />
                </div>

                <div class="module_subsec flex_center">
                    <asp:Label ID="lbl_status" runat="server" CssClass="alerta"></asp:Label>    
                </div>

                 <div class="module_subsec  flex_end">
                     <asp:Label runat="server" CssClass="module_subsec_elements module_subsec_medium-elements" Text="Seleccionar todos:" /> &nbsp; &nbsp;
                     <asp:checkbox ID="ckb_todos" AutoPostBack="true" runat="server" OnCheckedchange="ckb_todos_CheckedChanged"  />
                 </div>
                <div class="module_subsec">
                    <div class="overflow_x shadow w_100">
                        <asp:DataGrid ID="DAG_cola1" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped">
                            <HeaderStyle Cssclass="table_header"/>
                            <Columns>
                                <asp:BoundColumn DataField="FOLIO" HeaderText="Folio" Visible="false">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CLAVE" HeaderText="Folio">
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERSONA" HeaderText="Id Persona" Visible ="false">
                                <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INSTI" HeaderText="Institución">
                                <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundColumn>                                
                                <asp:BoundColumn DataField="RFC" HeaderText="RFC">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Prospecto" HeaderText="Prospecto">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>   
                                     <asp:BoundColumn DataField="creadox" HeaderText="Capturista">
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn> 
                                <asp:BoundColumn DataField="Fecha" HeaderText="Fecha">
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="VALIDAR" Text="Validar"> 
                                    <ItemStyle  Width="10%" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="IDDISP">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO">
                                    <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn itemStyle-Width="0%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:label runat="server" ID="FOLIO" Visible="false" Text='<%#Eval("FOLIO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>   
                                <asp:TemplateColumn HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_seleccionado" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid> 
                    </div> 
                </div>   
                
                <div class="module_subsec  flex_end">
                    <asp:Button ID="btn_validar" class="btn btn-primary" OnClick="btn_validar_Click" runat="server" Text="Validar Múltiple" />
                </div>
            </div>
        </div>
    </section>

    <section class="panel">
        <header class="panel-heading">
            <span>Expedientes pendientes</span>
        </header>
        <div class="panel-body">
            <div class="panel-body_content init_show">
                <div class="module_subsec">
                    <div class="overflow_x shadow w_100">
                    <asp:DataGrid ID="DAG_Cola" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-striped">
                        <HeaderStyle Cssclass="table_header"/>    
                        <Columns>
                            <asp:BoundColumn DataField="FOLIO" HeaderText="Folio" Visible="false">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAVE" HeaderText="Folio">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INSTI" HeaderText="Institución">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RFC" HeaderText="RFC">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Prospecto" HeaderText="Prospecto">
                                <ItemStyle Width="30%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="creadox" HeaderText="Capturista">
                                 <ItemStyle Width="15%" />
                             </asp:BoundColumn>
                            <asp:BoundColumn DataField="Fecha" HeaderText="Fecha">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="VALIDAR" Text="Validar">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:ButtonColumn>
                                <asp:BoundColumn DataField="ID">
                                <ItemStyle Width="5%" />
                            </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO">
                                <ItemStyle Width="5%" />
                                </asp:BoundColumn>
                            <%--<asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                               <asp:label runat="server" ID="Label1" Visible="false" Text='<%#Eval("FOLIO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        </Columns>                            
                    </asp:DataGrid>
                </div>
                </div>                
            </div>
        </div>
    </section>
</asp:Content>


