'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class CRED_VEN_AMORTIZACION
    
    '''<summary>
    '''Control lbl_info.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_info As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lnk_ProvRec.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_ProvRec As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control ddl_Instituciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddl_Instituciones As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control RequiredFieldValidator_NumCtrl.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_NumCtrl As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_cliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_cliente As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_numcliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_numcliente As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_cliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_cliente As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RequiredFieldValidator_cliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_cliente As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control btn_seleccionar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_seleccionar As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control btn_buscapersona.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_buscapersona As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control cmb_folio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_folio As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_folio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_folio As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_accion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_accion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control rad_cliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_cliente As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control rad_usuario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_usuario As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control lbl_cliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_cliente As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control pnl_usuario_pf.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_usuario_pf As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control CollapsiblePanelExtender1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents CollapsiblePanelExtender1 As Global.AjaxControlToolkit.CollapsiblePanelExtender
    
    '''<summary>
    '''Control HeaderPanel_busqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HeaderPanel_busqueda As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control ToggleImage_busqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ToggleImage_busqueda As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control Label5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label5 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control pnl_busqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_busqueda As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control lbl_mensaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_mensaje As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_IdCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_IdCliente As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_numaval.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_numaval As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender__idcliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender__idcliente As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control btn_buscapersona_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_buscapersona_ori As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control btn_seleccionar_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_seleccionar_ori As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control lbl_nombre_cliente_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre_cliente_ori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control CollapsiblePanelExtender2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents CollapsiblePanelExtender2 As Global.AjaxControlToolkit.CollapsiblePanelExtender
    
    '''<summary>
    '''Control HeaderPanel_nuevabusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HeaderPanel_nuevabusqueda As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control ToggleImage_nuevabusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ToggleImage_nuevabusqueda As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control lbl_new_persona.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_new_persona As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control pnl_nuevabusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_nuevabusqueda As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control txt_nombre1_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre1_u As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_nombre1_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre1_u As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender4 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_nombre2_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre2_u As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_nombre2_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre2_u As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender5 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_paterno_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_paterno_u As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_paterno_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_paterno_u As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender6 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_materno_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_materno_u As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_materno_u.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_materno_u As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender7 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_sexo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_sexo As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control rad_sexo0.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_sexo0 As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control rad_sexo1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_sexo1 As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control cmb_pais.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_pais As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_nac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_nac As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_nac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nac As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_fechanac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_fechanac As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fechanac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechanac As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control MaskedEditExtender_fechanacperf.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditExtender_fechanacperf As Global.AjaxControlToolkit.MaskedEditExtender
    
    '''<summary>
    '''Control MaskedEditValidator_fechanacperf.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditValidator_fechanacperf As Global.AjaxControlToolkit.MaskedEditValidator
    
    '''<summary>
    '''Control CalendarExtender1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents CalendarExtender1 As Global.AjaxControlToolkit.CalendarExtender
    
    '''<summary>
    '''Control txt_id_oficial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_id_oficial As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_id_oficial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_id_oficial As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_id_oficial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_id_oficial As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_cp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_cp As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_cp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_cp As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_cp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_cp As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control btn_buscadat.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_buscadat As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control lnk_BusquedaCP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_BusquedaCP As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control cmb_estado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_estado As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_municipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_municipio As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_localidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_localidad As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_colonia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_colonia As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_tipo_via.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_tipo_via As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txt_calle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_calle As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_calle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_calle As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_num_ext.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_num_ext As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control FilteredTextBoxExtender_num_ext.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_num_ext As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_num_int.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_num_int As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control FilteredTextBoxExtender_num_int.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_num_int As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_ife.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ife As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_info_ide.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_info_ide As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_info_disp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_info_disp As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control up1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents up1 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control lbl_montocredito.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_montocredito As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_montocreditotxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_montocreditotxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_saldocredito.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_saldocredito As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_saldocreditotxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_saldocreditotxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fechaliberacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechaliberacion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_fechaliberaciontxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechaliberaciontxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fechaultimopago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechaultimopago As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_fechaultimopagotxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechaultimopagotxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_intnor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_intnor As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_intnortxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_intnortxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_intmor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_intmor As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_intmortxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_intmortxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_diasultimopago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_diasultimopago As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_diasultimopagotxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_diasultimopagotxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_vencidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_vencidos As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_vencidostxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_vencidostxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fechavenc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechavenc As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_fechavenctxt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechavenctxt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fechacorte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fechacorte As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_fechacorte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_fechacorte As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control MaskedEditExtender_fechacorte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditExtender_fechacorte As Global.AjaxControlToolkit.MaskedEditExtender
    
    '''<summary>
    '''Control MaskedEditValidator_fechacorte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditValidator_fechacorte As Global.AjaxControlToolkit.MaskedEditValidator
    
    '''<summary>
    '''Control defaultCalendarExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents defaultCalendarExtender As Global.AjaxControlToolkit.CalendarExtender
    
    '''<summary>
    '''Control lbl_monto_liq.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_monto_liq As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_monto_liq_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_monto_liq_txt As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_info_fecha_corte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_info_fecha_corte As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control grd_pago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grd_pago As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control txt_cajaori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_cajaori As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_cajaori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_cajaori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_cajaori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_cajaori As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_cajaori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_cajaori As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control cmb_bancoori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_bancoori As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_bancoori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_bancoori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_cmbbancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_cmbbancosori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_montobancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_montobancosori As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_montobancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_montobancosori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_montobancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_montobancosori As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_montobancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_montobancosori As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control RequiredFieldValidator_txtmontobancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_txtmontobancosori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_origen_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_origen_dep As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_tit_origen_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_tit_origen_dep As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_tipo_origen_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_tipo_origen_dep As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control pnl_forma_transferencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_forma_transferencia As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control cmb_banco_des_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_banco_des_dep As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator2 As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_cta_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_cta_dep As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_tit_dep_origen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_tit_dep_origen As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender3 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RequiredFieldValidator_txt_cta_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_txt_cta_dep As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_titular_cta_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_titular_cta_dep As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control Label4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label4 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_titular_cta_dep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_titular_cta_dep As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_infobanco.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_infobanco As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_agregarbancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_agregarbancosori As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control dag_bancosori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_bancosori As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control div_pnl_ctascapori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_pnl_ctascapori As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control lbl_ctascapori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ctascapori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control pnl_ctascapori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_ctascapori As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control cmb_bancochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_bancochequesori As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_bancochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_bancochequesori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_cmbbancochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_cmbbancochequesori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_ctachequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_ctachequesori As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_ctachequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ctachequesori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_ctachequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_ctachequesori As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RequiredFieldValidator_ctachequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_ctachequesori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_numchequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_numchequesori As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_numchequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_numchequesori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_numchequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_numchequesori As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RequiredFieldValidator_numchequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_numchequesori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_montochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_montochequesori As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_montochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_montochequesori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_montochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_montochequesori As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_montochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_montochequesori As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control RequiredFieldValidator_montochequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_montochequesori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_modo_rec.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_modo_rec As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_modo_rec.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_modo_rec As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control rfv_modo_rec.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfv_modo_rec As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_infocheques.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_infocheques As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_agregarchequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_agregarchequesori As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control dag_chequesori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_chequesori As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control txt_nombre1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre1 As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_nombre1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre1 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_nombres1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_nombres1 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_nombre2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre2 As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_nombre2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_nombre2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_nombre2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_nombre2 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_paterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_paterno As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_paterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_paterno As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_paterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_paterno As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_materno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_materno As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_materno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_materno As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender1 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_notas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_notas As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_notas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_notas As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_notas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_notas As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_estatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_estatus As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_pagar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_pagar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control pnl_ide.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_ide As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control pnl_Titulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_Titulo As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control lbl_tit.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_tit As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_aviso_ide.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_aviso_ide As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_ok.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_ok As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_cancel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_cancel As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control ModalPopupExtender1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ModalPopupExtender1 As Global.AjaxControlToolkit.ModalPopupExtender
    
    '''<summary>
    '''Control pnl_AutorPLD.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_AutorPLD As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control pnl_AutorPLD_Titulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_AutorPLD_Titulo As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control lbl_AutorPLD_Titulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_AutorPLD_Titulo As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_AutorPLD.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_AutorPLD As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lnk_ACT.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_ACT As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control cpe_lst.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cpe_lst As Global.AjaxControlToolkit.CollapsiblePanelExtender
    
    '''<summary>
    '''Control hp_lst.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hp_lst As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control ToggleImage_dirfis.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ToggleImage_dirfis As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control cp_lst.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cp_lst As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control dag_lst.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_lst As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control lbl_IDAutor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_IDAutor As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_IDAutor2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_IDAutor2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control dag_cheques_aut.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_cheques_aut As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control lbl_Usr.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Usr As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_Usr.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Usr As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_Usr.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Usr As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_Pwd.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Pwd As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_Pwd.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Pwd As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_Pwd.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Pwd As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_Acc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Acc As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control cmb_Acc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_Acc As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control RequiredFieldValidator_Acc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Acc As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_Mtv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Mtv As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_Mtv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Mtv As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control FilteredTextBoxExtender_Mtv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_Mtv As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RequiredFieldValidator_Mtv.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Mtv As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_InfoAutoriza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_InfoAutoriza As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_AutorPLD_AUTO.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_AutorPLD_AUTO As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_AutorPLD_CAN.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_AutorPLD_CAN As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_AUTOCerrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_AUTOCerrar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control ModalPopupExtender2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ModalPopupExtender2 As Global.AjaxControlToolkit.ModalPopupExtender
    
    '''<summary>
    '''Control pnl_recalculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_recalculo As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control pnl_avisa_recalculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_avisa_recalculo As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control lbl_avisa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_avisa As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_avisa_2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_avisa_2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_avisa_3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_avisa_3 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_avisa_4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_avisa_4 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_aceptar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_aceptar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control ModalPopupExtender_recalculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ModalPopupExtender_recalculo As Global.AjaxControlToolkit.ModalPopupExtender
    
    '''<summary>
    '''Control HiddenPrinterName.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HiddenPrinterName As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control HiddenRawData.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HiddenRawData As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdn_ide.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_ide As Global.System.Web.UI.HtmlControls.HtmlInputHidden
    
    '''<summary>
    '''Control hdn_AutorPLD.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_AutorPLD As Global.System.Web.UI.HtmlControls.HtmlInputHidden
    
    '''<summary>
    '''Control hdn_nombrebusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_nombrebusqueda As Global.System.Web.UI.HtmlControls.HtmlInputHidden
    
    '''<summary>
    '''Control hdn_recalculo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_recalculo As Global.System.Web.UI.HtmlControls.HtmlInputHidden
End Class
