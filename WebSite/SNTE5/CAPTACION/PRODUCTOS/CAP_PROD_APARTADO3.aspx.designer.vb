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


Partial Public Class CAP_PROD_APARTADO3
    
    '''<summary>
    '''Control lbl_Producto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Producto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control dag_ComiAsigandas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_ComiAsigandas As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control lbl_status.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_asignar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lbl_atributos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_atributos As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lnk_RegresarTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_RegresarTotal As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control dag_Comisiones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_Comisiones As Global.System.Web.UI.WebControls.DataGrid
    
    '''<summary>
    '''Control pnl_SaldoMinVista.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_SaldoMinVista As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control rad_SaldoMinVista_Monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_SaldoMinVista_Monto As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control rad_SaldoMinVista_Porcentaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_SaldoMinVista_Porcentaje As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control lbl_SaldoMinVistaTipoCobro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_SaldoMinVistaTipoCobro As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Valor As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_ValorMin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ValorMin As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_TipoMin.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_TipoMin As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RequiredFieldValidator_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_Valor As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_Valor As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_Valor As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control lbl_status_atributos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status_atributos As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_SaldoMinVista.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_SaldoMinVista As Global.System.Web.UI.WebControls.Button
End Class
