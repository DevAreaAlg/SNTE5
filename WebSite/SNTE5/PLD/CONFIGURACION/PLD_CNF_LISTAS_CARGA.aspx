<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterMascore.master" CodeBehind="PLD_CNF_LISTAS_CARGA.aspx.vb" Inherits="SNTE5.PLD_CNF_LISTAS_CARGA" MaintainScrollPositionOnPostback ="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
  <section class="panel">
            <header class=" panel-heading">
                <span>Carga lista PEP</span>
                <span class=" panel_folder_toogle down">&rsaquo;</span>
            </header>
            <div class="panel-body">                  
                     
                            <div class= "module_subsec low_m columned four_columns">
                               <div class="module_subsec_elements"> 
		                         <div class="text_input_nice_div module_subsec_elements_content">
                                        <asp:DropDownList runat="server" ID="cmb_TipoLista" class="btn btn-primary2 dropdown_label" AutoPostBack="True">
                                            <asp:ListItem Value="PEP">PERSONAS POLITICAMENTE EXPUESTAS</asp:ListItem>
                                            <asp:ListItem Value="BLO">PERSONAS BLOQUEADAS</asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="text_input_nice_label title_tag">Tipo de lista: </span>
                                
                                    </div>
                                </div>
                                 <div class="module_subsec_elements"> 
		                    
                                </div>
                                 <div class="module_subsec_elements"> 
		                    
                                 </div>
                                 <div class="module_subsec" style="justify-content:space-between;flex-direction:row-reverse; ">
                                <div style="display:flex;align-items:center;"> 
                                    <asp:LinkButton postback="true" runat="server" style="margin-right:25px;font-size:18px;" ID="btn_ConsultaLista">
                                        Descargar excel
                                        <i class="fa fa-download" aria-hidden="true" style="font-size:20px; margin-left:5px;" ></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                                
                            </div>
                   
                        
                    <div class="module_subsec_elements low_m columned">
                        <div class="module_subsec no_column  align_items_flex_center">
                            <span class="module_subsec_elements">Seleccionar archivo de carga:</span>
                            <asp:FileUpload ID="AsyncFileUpload1" CssClass="module_subsec_elements" runat="server" />
                        </div>
                                     
                     <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_Permit" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                    </div>
                                                  
                     <div class="module_subsec flex_center">
                            <asp:Label ID="lbl_Status_Carga" runat="server" CssClass="alerta" Text="" Visible="True"></asp:Label>
                    </div>
           
            <div class="module_subsec flex_end">
               <asp:Button ID="btn_CargarArch" runat="server" class="btn btn-primary" Text="Cargar"/>
                          
            </div>
            </div>
        </section>
      
</asp:Content>
