Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraRichEdit

Namespace RichAccessInGridCell
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()

			InitGridData()
			AddRepositoryItemToGrid()
		End Sub

		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.ShownEditor
			Dim columnView As ColumnView = TryCast(sender, ColumnView)

			If columnView IsNot Nothing Then
				Dim activeEditor As RichTextEdit = TryCast(columnView.ActiveEditor, RichTextEdit)

				If activeEditor IsNot Nothing Then
					Dim richEditControl As RichEditControl = CType(activeEditor.Controls(0), RichEditControl)

					' TODO: Use any RichEditControl API
					richEditControl.ActiveViewType = RichEditViewType.PrintLayout
					richEditControl.ActiveView.ZoomFactor = 2f
					richEditControl.Document.Sections(0).Margins.Left = 50
					richEditControl.Document.Sections(0).Margins.Top = 50
				End If
			End If
		End Sub

		#Region "Helper Methods"
		Private Sub InitGridData()
			Dim ds As New DataSet()
			Dim dt As New DataTable("MyDataTable")

			dt.Columns.Add("ID", GetType(Int32))
			dt.Columns.Add("MyData(RichEdit)", GetType(String))
			dt.Constraints.Add("IDPK", dt.Columns("ID"), True)

			dt.Rows.Add(New Object() { 0, "Row A" })
			dt.Rows.Add(New Object() { 1, "Row B" })

			ds.Tables.Add(dt)

			gridControl1.DataSource = ds
			gridControl1.DataMember = ds.Tables(0).TableName
		End Sub

		Private Sub AddRepositoryItemToGrid()
			Dim repositoryItemRichTextEdit As New RepositoryItemRichTextEdit()

			gridControl1.RepositoryItems.Add(repositoryItemRichTextEdit)
			gridView1.Columns("MyData(RichEdit)").ColumnEdit = repositoryItemRichTextEdit

			gridView1.RowHeight = 120
		End Sub
		#End Region
	End Class
End Namespace
