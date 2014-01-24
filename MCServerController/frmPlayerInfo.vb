Public Class frmPlayerInfo

    Private Sub btnGetHost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetHost.Click
        Try
            Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(txtIP.Text)
            txtHost.Text = iphe.HostName
        Catch ex As Exception
            txtHost.Text = "Can't Get Hostname"
        End Try

    End Sub

    Private Sub btnBadCountApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBadCountApply.Click
        Try
            Dim intTmp As Integer = txtBadCount.Text
        Catch ex As Exception
            MessageBox.Show("���͂��ꂽ�l�������l�ł͂���܂���", "���̓G���[", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        End Try

        'BadCount��������
        frmMain.gfRewriteBadCount(CInt(Me.txtSelectedIndex.Text), txtBadCount.Text)

    End Sub
End Class