Imports Microsoft.VisualBasic

Public Class Categ_Ctrl
    Inherits System.Web.UI.WebControls.Panel

    'Declaramos las propiedades del conrol
    Private Property DTe As New System.Data.DataTable
    Public Event SelectedCat As EventHandler

#Region "Constructor"

    'Especificamos el contructor
    Public Sub New()
        'Especificamos las propiedades base del control
        Me.CssClass = "tree_outCont"
        'Declaramos la tabla que contengdra los datos de las categorias para construir el arbol
        Dim custDA As New System.Data.OleDb.OleDbDataAdapter()
        'Ejecutamos el procedimiento que trae las categorias
        Dim Con = CreateObject("ADODB.Connection")
        Con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ToString
        Con.ConnectionTimeout = 240
        Dim cmd = New ADODB.Command()
        Con.Open()
        cmd.ActiveConnection = Con
        cmd.CommandType = System.Data.CommandType.StoredProcedure
        cmd.CommandText = "SEL_CATEGSYS"
        Dim rs As ADODB.Recordset = cmd.Execute()
        custDA.Fill(DTe, rs)
        'Checamos cuales son las categorias en el nivel superior
        Dim fRows() As Data.DataRow = DTe.Select("PADRE is null")
        For Each row As Data.DataRow In fRows
            'Declaramos variables que contendran la categoria en la interfaz
            Dim lCat As New HtmlGenericControl
            Dim nCat As New LinkButton
            'EStablecemos las propiedades de las varibles
            lCat.TagName = "div"
            lCat.Attributes("class") = "lCat"
            AddHandler nCat.Click, AddressOf OnSelectedCat
            nCat.Text = row("NOMBRE")
            nCat.Attributes.Add("valor", row("ID"))
            'agregamos controles
            lCat.Controls.Add(nCat)
            Me.Controls.Add(lCat)
            'buscamos subcategorias
            buscaSubC(Me, lCat, row("ID"))
        Next


    End Sub

#End Region

#Region "Busca subcategorias"

    Private Sub buscaSubC(ByRef ctrlP As Panel, ByRef lPadre As HtmlGenericControl, ByVal padre As Integer)
        Dim sRows() As Data.DataRow = DTe.Select("PADRE = " & padre.ToString)

        If sRows.Count > 0 Then
            Dim nodeCross As HtmlGenericControl = createNodeCross("15px", "15px")
            lPadre.Controls.Add(nodeCross)

            Dim subDiv As New Panel
            subDiv.CssClass = "subDiv"

            For Each row As Data.DataRow In sRows
                Dim lCat As New HtmlGenericControl
                Dim nCat As New LinkButton
                lCat.TagName = "div"
                lCat.Attributes("class") = "lCat"
                nCat.Text = row("NOMBRE")
                AddHandler nCat.Click, AddressOf OnSelectedCat
                nCat.Attributes.Add("valor", row("ID"))
                lCat.Controls.Add(nCat)
                subDiv.Controls.Add(lCat)
                buscaSubC(subDiv, lCat, row("ID"))
            Next
            ctrlP.Controls.Add(subDiv)
        Else
            lPadre.Attributes.CssStyle.Add("padding-left", "25px")
        End If

    End Sub
#End Region

#Region "Crea cruzes para nodos de arboles"

    Function createNodeCross(ByVal Width As String, ByVal Height As String) As HtmlGenericControl

        Dim out_cross As New HtmlGenericControl
        out_cross.TagName = "div"
        out_cross.Attributes("style") = " width: " & Width & " ; " & " height: " & Height & " ;"
        out_cross.Attributes("class") = "nodeCross"

        Return out_cross

    End Function

#End Region

#Region "evento catClick"

    Protected Overridable Sub OnSelectedCat(ByVal sender As LinkButton, ByVal e As System.EventArgs)
        RaiseEvent SelectedCat(sender, e)
    End Sub

#End Region

End Class
