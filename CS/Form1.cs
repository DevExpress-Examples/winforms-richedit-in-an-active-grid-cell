using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraRichEdit;

namespace RichAccessInGridCell {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            InitGridData();
            AddRepositoryItemToGrid();
        }

        private void gridView1_ShownEditor(object sender, EventArgs e) {
            ColumnView columnView = sender as ColumnView;
            
            if (columnView != null) {
                RichTextEdit activeEditor = columnView.ActiveEditor as RichTextEdit;
                
                if (activeEditor != null) {
                    RichEditControl richEditControl = (RichEditControl)activeEditor.Controls[0];
                    
                    // TODO: Use any RichEditControl API
                    richEditControl.ActiveViewType = RichEditViewType.PrintLayout;
                    richEditControl.ActiveView.ZoomFactor = 2f;
                    richEditControl.Document.Sections[0].Margins.Left = 50;
                    richEditControl.Document.Sections[0].Margins.Top = 50;
                }
            }
        }

        #region Helper Methods
        private void InitGridData() {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyDataTable");

            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("MyData(RichEdit)", typeof(string));
            dt.Constraints.Add("IDPK", dt.Columns["ID"], true);

            dt.Rows.Add(new object[] { 0, "Row A" });
            dt.Rows.Add(new object[] { 1, "Row B" });

            ds.Tables.Add(dt);

            gridControl1.DataSource = ds;
            gridControl1.DataMember = ds.Tables[0].TableName;
        }

        private void AddRepositoryItemToGrid() {
            RepositoryItemRichTextEdit repositoryItemRichTextEdit = new RepositoryItemRichTextEdit();

            gridControl1.RepositoryItems.Add(repositoryItemRichTextEdit);
            gridView1.Columns["MyData(RichEdit)"].ColumnEdit = repositoryItemRichTextEdit;

            gridView1.RowHeight = 120;
        }
        #endregion
    }
}
