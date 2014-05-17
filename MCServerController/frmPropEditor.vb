Public Class frmPropEditor


    Private Sub psCreateDGV()
        Dim dgv As New DataGridView()
        Dim checkCell As New DataGridViewCheckBoxCell
        Dim comboCell As New DataGridViewComboBoxCell
        Dim textCell As New DataGridViewTextBoxCell

        dgv.AllowUserToAddRows = False
        dgv.EditMode = DataGridViewEditMode.EditOnEnter
        dgv.RowCount = 3
        dgv.ColumnCount = 1
        dgv.Dock = DockStyle.Fill
        Controls.Add(dgv)

        checkCell.Value = True
        dgv(0, 0) = checkCell

        comboCell.DisplayMember = "test"
        dgv(0, 1) = comboCell

        textCell.Value = ""
        dgv(0, 2) = textCell

    End Sub

    Private Sub frmPropEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'server.properties設定用DataGridViewオブジェクト作成
        psCreateDGV()
    End Sub
End Class