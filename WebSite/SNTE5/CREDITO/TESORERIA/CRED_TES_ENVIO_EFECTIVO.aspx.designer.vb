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


Partial Public Class CRED_TES_ENVIO_EFECTIVO
    
    '''<summary>
    '''Control lnk_constancias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_constancias As Global.System.Web.UI.WebControls.LinkButton
    
    '''<summary>
    '''Control rad_sucursal_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_sucursal_ori As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control rad_banco_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_banco_ori As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control lbl_ins3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ins3 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control rad_sucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_sucursal As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control rad_banco.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rad_banco As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control lbl_ins2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ins2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_pasig.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_pasig As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_pasig_info.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_pasig_info As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control Lst_en_sucursal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lst_en_sucursal As Global.System.Web.UI.WebControls.ListBox
    
    '''<summary>
    '''Control btn_add.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_add As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control btn_rem.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_rem As Global.System.Web.UI.WebControls.ImageButton
    
    '''<summary>
    '''Control lbl_pdisp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_pdisp As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lbl_pdisp_info.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_pdisp_info As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control Lst_empaquetados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lst_empaquetados As Global.System.Web.UI.WebControls.ListBox
    
    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txt_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_monto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control lbl_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_monto As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control FilteredTextBoxExtender_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_monto As Global.AjaxControlToolkit.FilteredTextBoxExtender
    
    '''<summary>
    '''Control RegularExpressionValidator_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_monto As Global.System.Web.UI.WebControls.RegularExpressionValidator
    
    '''<summary>
    '''Control cmb_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_ori As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_suc_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_suc_ori As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control req_ori.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_ori As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_des.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_des As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_suc_des.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_suc_des As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control req_des.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_des As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_envia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_envia As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_envia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_envia As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control req_envia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_envia As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_transporta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_transporta As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_transporta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_transporta As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control req_transporta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_transporta As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control cmb_recibe.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_recibe As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lbl_recibe.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_recibe As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control req_recibe.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents req_recibe As Global.System.Web.UI.WebControls.RequiredFieldValidator
    
    '''<summary>
    '''Control btn_enviar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_enviar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btn_registro.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_registro As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lbl_status.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status As Global.System.Web.UI.WebControls.Label
End Class
