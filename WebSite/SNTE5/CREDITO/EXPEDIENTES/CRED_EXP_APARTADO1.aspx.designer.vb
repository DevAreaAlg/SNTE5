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


Partial Public Class CRED_EXP_APARTADO1

    '''<summary>
    '''Control lbl_Folio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Folio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbl_Prospecto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Prospecto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lbl_Producto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Producto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txt_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_monto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_monto As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control lbl_MinReest.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_MinReest As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbl_NotaReest.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_NotaReest As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbl_rango.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_rango As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_monto As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control RegularExpressionValidator_monto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RegularExpressionValidator_monto As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control txt_plazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_plazo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_plazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_plazo As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control lbl_rango_plazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_rango_plazo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator_plazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_plazo As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control cmb_objetivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_objetivo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control rfv_objetivo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfv_objetivo As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control txt_notas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_notas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_notas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_notas As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control lbl_status.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btn_guardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control UpdatePanelFondeo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelFondeo As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control cmb_fondeo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmb_fondeo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control RequiredFieldValidator1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator1 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control txt_porcentaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_porcentaje As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_porcent.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_porcent As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control RequiredFieldValidator_porcent.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_porcent As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control btn_asignar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_asignar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lst_fond.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lst_fond As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Control btn_remover.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_remover As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lbl_statusfondeo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_statusfondeo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanelGracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelGracia As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txt_diasgracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_diasgracia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_diasgracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_diasgracia As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control lbl_DiasGracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_DiasGracia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator_diasgracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_diasgracia As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control txt_diasgraciamora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_diasgraciamora As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control FilteredTextBoxExtender_diasgraciamora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FilteredTextBoxExtender_diasgraciamora As Global.AjaxControlToolkit.FilteredTextBoxExtender

    '''<summary>
    '''Control lbl_DiasGraciaMora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_DiasGraciaMora As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator_diasgraciamora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator_diasgraciamora As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control btn_guardardiasdegracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardardiasdegracia As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lbl_statusgracia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_statusgracia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control pnl_Modal_Confirmar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_Modal_Confirmar As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lbl_alerta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_alerta As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbl_mensaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_mensaje As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btn_confirmar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_confirmar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btn_cancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_cancelar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control modal_Confirmar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents modal_Confirmar As Global.AjaxControlToolkit.ModalPopupExtender

    '''<summary>
    '''Control panel_comisiones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panel_comisiones As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control upd_pnl_AsignarCom.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents upd_pnl_AsignarCom As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control dag_comisiones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_comisiones As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control lbl_asigFuentesEstatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_asigFuentesEstatus As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btn_guardar_c.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_guardar_c As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control lnk_RegresarTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnk_RegresarTotal As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control dag_atributos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_atributos As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Control pnl_APE_CRED.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnl_APE_CRED As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control dag_APE_CRED.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dag_APE_CRED As Global.System.Web.UI.WebControls.DataGrid

    '''<summary>
    '''Control lbl_titulo_atributo_ape.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_titulo_atributo_ape As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txt_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txt_Valor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lbl_Valor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_Valor As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbl_ValorMinMax.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_ValorMinMax As Global.System.Web.UI.WebControls.Label

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
    '''Control lbl_status_atrib.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbl_status_atrib As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btn_APE_CRED.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btn_APE_CRED As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control hdn_alertas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdn_alertas As Global.System.Web.UI.HtmlControls.HtmlInputHidden
End Class
