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


Partial Public Class CAP_PROD_GENERAL
    
    '''<summary>
    '''Control lnk_ProductoNuevo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_ProductoNuevo As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lnk_ProductoEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_ProductoEditar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control cmb_Productos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_Productos As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lnk_resumen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_resumen As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control Chk_ActivaDesactivar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Chk_ActivaDesactivar As Global.System.Web.UI.WebControls.CheckBox
    
    '''<summary>
    '''Control txt_NombreProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_NombreProducto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_txt_NombreProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_txt_NombreProducto As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control FilteredTextBoxExtender_txt_NombreProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_txt_NombreProducto As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control cmb_tipo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_tipo As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control RequiredFieldValidator_cmb_tipo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_cmb_tipo As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_tipoper.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_tipoper As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control RequiredFieldValidator_cmb_tipoper.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_cmb_tipoper As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control txt_DescripcionProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_DescripcionProducto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control RequiredFieldValidator_txt_DescripcionProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_txt_DescripcionProducto As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control lbl_Alertas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Alertas As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_GenerarProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_GenerarProducto As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_GuardarProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_GuardarProducto As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_Cancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_Cancelar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_eliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_eliminar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control dag_ConfProductos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_ConfProductos As Global.System.Web.UI.WebControls.DataGrid
End Class
