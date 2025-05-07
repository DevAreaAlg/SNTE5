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


Partial Public Class CORE_OPE_DIVISAS
    
    '''<summary>
    '''Control lbl_mensaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_mensaje As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control cmb_Divisas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_Divisas As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_Divisas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Divisas As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_Divisas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Divisas As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_ValorMXP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_ValorMXP As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_ValorMXP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ValorMXP As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_ValorPeso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ValorPeso As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_ValorMXP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_ValorMXP As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_ValorMXP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_ValorMXP As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_ValorMXP.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_ValorMXP As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control btn_Asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_Asignar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_EXCEL.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_EXCEL As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control dag_divisas0.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_divisas0 As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control txt_FechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_FechaFiltro As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_fecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fecha As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_FechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_FechaFiltro As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control MaskedEditExtender_FechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditExtender_FechaFiltro As Global.AjaxControlToolkit.MaskedEditExtender
    
    '''<summary>
    '''Control MaskedEditValidator_FechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents MaskedEditValidator_FechaFiltro As Global.AjaxControlToolkit.MaskedEditValidator
    
    '''<summary>
    '''Control CalendarExtender1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents CalendarExtender1 As Global.AjaxControlToolkit.CalendarExtender
    
    '''<summary>
    '''Control lnk_FechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_FechaFiltro As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control lnk_CancelaFechaFiltro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_CancelaFechaFiltro As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control dag_divisas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_divisas As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control hdn_nombrebusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_nombrebusqueda As Global.System.Web.UI.HtmlControls.HtmlInputHidden
End Class
