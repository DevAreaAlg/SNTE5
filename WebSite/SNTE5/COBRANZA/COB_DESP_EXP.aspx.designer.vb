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


Partial Public Class COB_DESP_EXP
    
    '''<summary>
    '''Control cmb_sucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_sucursal As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control cmb_despacho.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_despacho As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txtfolio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtfolio As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control filterfolio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents filterfolio As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_minimo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_minimo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control filterminimo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents filterminimo As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txtmaximo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtmaximo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control filtermaximo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents filtermaximo As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txtnumcliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtnumcliente As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control filternumcliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents filternumcliente As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control btn_consultar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_consultar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_eliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_eliminar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lbl_estatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_estatus As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control dag_expedientes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_expedientes As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control pnl_evalx.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_evalx As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control pnl_tit_asignacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_tit_asignacion As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control LBL_MODport.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LBL_MODport As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control cmb_fase.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_fase As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control req_asentamiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_asentamiento As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_despacho_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_despacho_asignar As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txt_valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_valor As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control req_valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_valor As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender29.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender29 As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_monto As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control txt_Objetivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Objetivo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator2 As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_objetivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_objetivo As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_status_modal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status_modal As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_guarda_modal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guarda_modal As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_evalxcerrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_evalxcerrar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control hdn_ctrl.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_ctrl As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control modal_port.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents modal_port As Global.AjaxControlToolkit.ModalPopupExtender
    
    '''<summary>
    '''Control lbl_AlertaDesExpedienteDig.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_AlertaDesExpedienteDig As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_status.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status As Global.System.Web.UI.WebControls.Label
End Class
