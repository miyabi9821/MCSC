Public Class frmMail
    Private vsBar As VScrollBar

    Private Sub frmMail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'DataGridView�̏c�X�N���[���������I�ɕ\��������
        For Each c As Control In dgvToAddress.Controls
            If TypeOf c Is VScrollBar Then
                vsBar = DirectCast(c, VScrollBar)

                AddHandler vsBar.VisibleChanged, AddressOf vsBar_VisibleChanged
            End If
        Next
        vsBar.Show()

        '�ݒ�̔��f


    End Sub

    Private Sub vsBar_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not vsBar.Visible Then
            '�c�N���[�X�o�[����ɕ\������B
            Dim borderWidth As Integer = 2

            vsBar.Location = New Point(Me.dgvToAddress.ClientRectangle.Width - vsBar.Width, 0)
            vsBar.Size = New Size(vsBar.Width, Me.dgvToAddress.ClientRectangle.Height - borderWidth)
            vsBar.Show()
        End If
    End Sub


End Class