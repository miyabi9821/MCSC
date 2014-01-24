Public Class frmNGWords

    Private Sub frmNGWords_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'データを全てクリア
            dgvNGWords.Rows.Clear()

            chkNGWordsEnabled.Checked = NGWSettings.Instance.NGWordsEnabled
            'DataTable読込
            With NGWSettings.Instance.NGWords
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim dgvNewRow As New DataGridViewRow
                        dgvNewRow.CreateCells(dgvNGWords)
                        dgvNewRow.Cells(0).Value = .Rows(i)(0)
                        dgvNewRow.Cells(1).Value = .Rows(i)(1)
                        dgvNewRow.Cells(2).Value = .Rows(i)(2)
                        dgvNGWords.Rows.Add(dgvNewRow)
                    Next
                End If
            End With

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click

        NGWSettings.Instance.NGWordsEnabled = chkNGWordsEnabled.Checked
        'DataTable設定
        Dim dtNew As New DataTable("NGWords")
        dtNew.Columns.Add(dgvNGWords.Columns(0).HeaderText)
        dtNew.Columns.Add(dgvNGWords.Columns(1).HeaderText)
        dtNew.Columns.Add(dgvNGWords.Columns(2).HeaderText)
        With dgvNGWords
            For i As Integer = 0 To .Rows.Count - 1
                If .Rows(i).Cells(1).Value <> "" Then
                    Dim newRow As DataRow = dtNew.NewRow
                    newRow(dgvNGWords.Columns(0).HeaderText) = .Rows(i).Cells(0).Value
                    newRow(dgvNGWords.Columns(1).HeaderText) = .Rows(i).Cells(1).Value
                    newRow(dgvNGWords.Columns(2).HeaderText) = .Rows(i).Cells(2).Value
                    dtNew.Rows.Add(newRow)
                End If
            Next
        End With
        NGWSettings.Instance.NGWords = dtNew
        NGWSettings.SaveToXmlFile()

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    '行追加時の初期値
    Private Sub dgvNGWords_DefaultValuesNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvNGWords.DefaultValuesNeeded
        e.Row.Cells("colEnabled").Value = CBool("True")
        e.Row.Cells("colBadCountUp").Value = 1
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        If MessageBox.Show("全NG Word定義を削除して宜しいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        dgvNGWords.Rows.Clear()
    End Sub
End Class