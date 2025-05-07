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


Partial Public Class PEN_PROC_SALDOS_TRABAJADOR
    
    '''<summary>
    '''Control div_selCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_selCliente As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control txt_IdCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_IdCliente As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_BusquedaPersona0.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_BusquedaPersona0 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender__idcliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender__idcliente As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control btn_Continuar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_Continuar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_BusquedaPersona.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_BusquedaPersona As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control div_NombrePersonaBusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_NombrePersonaBusqueda As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control lbl_NombrePersonaBusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_NombrePersonaBusqueda As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_statusc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_statusc As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control panel_saldos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_saldos As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control head_panel_saldos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents head_panel_saldos As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control toggle_panel_saldos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents toggle_panel_saldos As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control content_panel_saldos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents content_panel_saldos As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control dag_Cuenta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_Cuenta As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control dag_Saldos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_Saldos As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control panel_mov.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_mov As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control head_panel_mov.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents head_panel_mov As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control toggle_panel_mov.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents toggle_panel_mov As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control content_panel_mov.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents content_panel_mov As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control dag_movimientos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_movimientos As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control btn_EXCEL.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_EXCEL As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control panel_historial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_historial As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control head_panel_historial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents head_panel_historial As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control toggle_panel_historial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents toggle_panel_historial As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control content_panel_historial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents content_panel_historial As Global.System.Web.UI.HtmlControls.HtmlGenericControl
End Class
