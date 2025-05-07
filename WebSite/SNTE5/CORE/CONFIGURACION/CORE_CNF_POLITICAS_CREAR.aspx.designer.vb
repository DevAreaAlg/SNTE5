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


Partial Public Class CORE_CNF_POLITICAS_CREAR
    
    '''<summary>
    '''Control txt_id_Politica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_id_Politica As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control ckb_Activo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ckb_Activo As Global.System.Web.UI.WebControls.CheckBox
    
    '''<summary>
    '''Control txt_nombre_politica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre_politica As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_nombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_nombre As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_nombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_nombre As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_tiempo_inactividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_tiempo_inactividad As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_inactividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_inactividad As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_inactividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_inactividad As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_intentos_fallidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_intentos_fallidos As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_fallidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_fallidos As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_fallidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_fallidos As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control timepicker1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents timepicker1 As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_fechaini.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_fechaini As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_fechaini.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_fechaini As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control timepicker2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents timepicker2 As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_fechafin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_fechafin As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_fechafin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_fechafin As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_dias_expiracion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_dias_expiracion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_expira.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_expira As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_expira.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_expira As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_mem_contrasena.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_mem_contrasena As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_memcontra.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_memcontra As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_memcontra.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_memcontra As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control txt_tpo_bloqueo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_tpo_bloqueo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_tpo_bloqueo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_tpo_bloqueo As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_tpo_bloqueo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_tpo_bloqueo As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control lbl_statupol.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_statupol As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_guardar_politica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardar_politica As Global.System.Web.UI.HtmlControls.HtmlButton
End Class
