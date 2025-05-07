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


Partial Public Class CRED_TES_RECEPCION_EFECTIVO
    
    '''<summary>
    '''Control grd_operaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grd_operaciones As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control upd_pnl_info_recepcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents upd_pnl_info_recepcion As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control lbl_origen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_origen As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_origen_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_origen_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_destino.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_destino As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_destino_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_destino_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_envia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_envia As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_envia_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_envia_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_transporta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_transporta As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_transporta_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_transporta_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_recibe.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_recibe As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_recibe_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_recibe_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_fecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fecha As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_fecha_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_fecha_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_monto As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_monto_txt.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_monto_txt As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control grd_info_recepcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grd_info_recepcion As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control ae.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ae As Global.AjaxControlToolkit.UpdatePanelAnimationExtender
    
    '''<summary>
    '''Control btn_digit.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_digit As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_recibir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_recibir As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lbl_status.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status As Global.System.Web.UI.WebControls.Label
End Class
