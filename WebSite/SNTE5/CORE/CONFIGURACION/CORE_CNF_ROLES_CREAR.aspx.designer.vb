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


Partial Public Class CORE_CNF_ROLES_CREAR
    
    '''<summary>
    '''Control lbl_RolNumero.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_RolNumero As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_id.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_id As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control UpdatePanelDatos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelDatos As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control panel_datos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_datos As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control chk_estatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chk_estatus As Global.System.Web.UI.WebControls.CheckBox
    
    '''<summary>
    '''Control txt_nombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_nombre As Global.System.Web.UI.WebControls.TextBox
    
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
    '''Control txt_descripcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_descripcion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_guardado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_guardado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_guardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control panel_roles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_roles As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control upd_pnl_AsignarModulos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents upd_pnl_AsignarModulos As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control dag_mod_si.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_mod_si As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control lbl_permisos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_permisos As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_asignar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control pnl_reportes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_reportes As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control out_catg_ctrl.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents out_catg_ctrl As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control grdVw_reportesAsig.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdVw_reportesAsig As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control Label2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_repEstatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_repEstatus As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btn_guardarRep.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardarRep As Global.System.Web.UI.WebControls.Button
End Class
