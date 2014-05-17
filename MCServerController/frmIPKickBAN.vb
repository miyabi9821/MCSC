Public Class frmIPKickBAN
    Private pStrMaskData(25) As String
    Private ReadOnly pStrIPMask() As String = {"完全一致", _
                                               "255.0.0.0", _
                                               "255.128.0.0", _
                                               "255.192.0.0", _
                                               "255.224.0.0", _
                                               "255.240.0.0", _
                                               "255.248.0.0", _
                                               "255.252.0.0", _
                                               "255.254.0.0", _
                                               "255.255.0.0", _
                                               "255.255.128.0", _
                                               "255.255.192.0", _
                                               "255.255.224.0", _
                                               "255.255.240.0", _
                                               "255.255.248.0", _
                                               "255.255.252.0", _
                                               "255.255.254.0", _
                                               "255.255.255.0", _
                                               "255.255.255.128", _
                                               "255.255.255.192", _
                                               "255.255.255.224", _
                                               "255.255.255.240", _
                                               "255.255.255.248", _
                                               "255.255.255.252", _
                                               "255.255.255.254", _
                                               "255.255.255.255"}

    Private Sub frmIPKickBAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        psSetCmdMask()

    End Sub

    'マスク選択コンボボックスの内容を設定する
    Private Sub psSetCmdMask()
        cmbMask.Items.Clear()

        cmbMask.Items.Add("完全一致")               'Index = 0
        cmbMask.Items.Add(" 8 (255.0.0.0)")         'Index = 1
        cmbMask.Items.Add(" 9 (255.128.0.0)")       'Index = 2
        cmbMask.Items.Add("10 (255.192.0.0)")       'Index = 3
        cmbMask.Items.Add("11 (255.224.0.0)")       'Index = 4
        cmbMask.Items.Add("12 (255.240.0.0)")       'Index = 5
        cmbMask.Items.Add("13 (255.248.0.0)")       'Index = 6
        cmbMask.Items.Add("14 (255.252.0.0)")       'Index = 7
        cmbMask.Items.Add("15 (255.254.0.0)")       'Index = 8
        cmbMask.Items.Add("16 (255.255.0.0)")       'Index = 9
        cmbMask.Items.Add("17 (255.255.128.0)")     'Index = 10
        cmbMask.Items.Add("18 (255.255.192.0)")     'Index = 11
        cmbMask.Items.Add("19 (255.255.224.0)")     'Index = 12
        cmbMask.Items.Add("20 (255.255.240.0)")     'Index = 13
        cmbMask.Items.Add("21 (255.255.248.0)")     'Index = 14
        cmbMask.Items.Add("22 (255.255.252.0)")     'Index = 15
        cmbMask.Items.Add("23 (255.255.254.0)")     'Index = 16
        cmbMask.Items.Add("24 (255.255.255.0)")     'Index = 17
        cmbMask.Items.Add("25 (255.255.255.128)")   'Index = 18
        cmbMask.Items.Add("26 (255.255.255.192)")   'Index = 19
        cmbMask.Items.Add("27 (255.255.255.224)")   'Index = 20
        cmbMask.Items.Add("28 (255.255.255.240)")   'Index = 21
        cmbMask.Items.Add("29 (255.255.255.248)")   'Index = 22
        cmbMask.Items.Add("30 (255.255.255.252)")   'Index = 23
        cmbMask.Items.Add("31 (255.255.255.254)")   'Index = 24
        cmbMask.Items.Add("32 (255.255.255.255)")   'Index = 25

        cmbMask.SelectedIndex = 0
    End Sub

    '.入力で次の入力欄に移動
    Private Sub txtIPv4_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPv4_1.KeyPress
        If e.KeyChar = "."c Then
            txtIPv4_2.Focus()
        End If

        If e.KeyChar = vbBack And txtIPv4_1.Text = "" Then

        End If

        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If

        If txtIPv4_1.Text.Length >= 3 And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIPv4_2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPv4_2.KeyPress
        If e.KeyChar = "."c Then
            txtIPv4_3.Focus()
        End If

        If e.KeyChar = vbBack And txtIPv4_2.Text = "" Then
            txtIPv4_1.Focus()
        End If

        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIPv4_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPv4_3.KeyPress
        If e.KeyChar = "."c Then
            txtIPv4_4.Focus()
        End If

        If e.KeyChar = vbBack And txtIPv4_3.Text = "" Then
            txtIPv4_2.Focus()
        End If

        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIPv4_4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPv4_4.KeyPress
        If e.KeyChar = "."c Then

        End If

        If e.KeyChar = vbBack And txtIPv4_4.Text = "" Then
            txtIPv4_3.Focus()
        End If

        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub

    '入力チェック
    Private Sub txtIPv4_1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPv4_1.Leave
        If pfCheckIPAddress(txtIPv4_1.Text) = True Then
            txtIPv4_1.BackColor = Color.White
        Else
            txtIPv4_1.BackColor = Color.Pink
        End If
    End Sub
    Private Sub txtIPv4_2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPv4_2.Leave
        If pfCheckIPAddress(txtIPv4_2.Text) = True Then
            txtIPv4_2.BackColor = Color.White
        Else
            txtIPv4_2.BackColor = Color.Pink
        End If
    End Sub
    Private Sub txtIPv4_3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPv4_3.Leave
        If pfCheckIPAddress(txtIPv4_3.Text) = True Then
            txtIPv4_3.BackColor = Color.White
        Else
            txtIPv4_3.BackColor = Color.Pink
        End If
    End Sub
    Private Sub txtIPv4_4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPv4_4.Leave
        If pfCheckIPAddress(txtIPv4_4.Text) = True Then
            txtIPv4_4.BackColor = Color.White
        Else
            txtIPv4_4.BackColor = Color.Pink
        End If
    End Sub


    '入力値が0～255かを判定する
    Private Function pfCheckIPAddress(ByVal IP As String) As Boolean
        Try
            Dim intTmp As Integer = 0
            If Integer.TryParse(IP, intTmp) = False Then
                Return False
            End If

            If intTmp >= 0 And intTmp <= 255 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            '入力値がエラーの場合はそこにフォーカスを移す
            If txtIPv4_1.BackColor = Color.Pink Then
                txtIPv4_1.Focus()
                txtIPv4_1.SelectAll()
                Exit Sub
            End If
            If txtIPv4_2.BackColor = Color.Pink Then
                txtIPv4_2.Focus()
                txtIPv4_2.SelectAll()
                Exit Sub
            End If
            If txtIPv4_3.BackColor = Color.Pink Then
                txtIPv4_3.Focus()
                txtIPv4_3.SelectAll()
                Exit Sub
            End If
            If txtIPv4_4.BackColor = Color.Pink Then
                txtIPv4_4.Focus()
                txtIPv4_4.SelectAll()
                Exit Sub
            End If

            '追加処理
            dgvIPAddress.Rows.Add()
            Dim intRow As Integer = dgvIPAddress.Rows.Count - 1
            dgvIPAddress.Rows(intRow).Cells("chEnabled").Value = True
            dgvIPAddress.Rows(intRow).Cells("chIPAddress").Value = txtIPv4_1.Text & "." & txtIPv4_2.Text & "." & txtIPv4_3.Text & "." & txtIPv4_4.Text
            dgvIPAddress.Rows(intRow).Cells("chMask").Value = pStrIPMask(cmbMask.SelectedIndex)
            dgvIPAddress.Rows(intRow).Cells("chIDBAN").Value = chkIDBAN.Checked



        Catch ex As Exception
            MessageBox.Show("予期せぬエラーが発生しました")
        End Try
    End Sub

    'IPアドレスのリストをクリアする
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If MessageBox.Show("登録済みのIPを全てクリアします。宜しいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        dgvIPAddress.Rows.Clear()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub txtIPv4_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPv4_1.TextChanged

    End Sub

    'テキストから一気にIPアドレスを登録する
    Private Sub btnAddFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFromList.Click
        Try
            Dim strIPAddr As String = String.Empty
            Dim strMask As String = String.Empty

            For l As Integer = 0 To txtCopyArea.Lines.Length
                Dim strLineData() As String = txtCopyArea.Lines(l).Split("/")

                'IPアドレスかの判定
                Dim ipaddr As New System.Net.IPAddress(0)
                If System.Net.IPAddress.TryParse(strLineData(0), ipaddr) = False Then
                    'IPアドレスで無ければ次の行に進む
                    Continue For
                End If

                'サブネットマスクが指定されているか
                If strLineData.Length = 1 Then
                    'IPアドレスのみ指定
                    strIPAddr = strLineData(0)
                    strMask = "完全一致"

                Else
                    'サブネットマスクも指定


                End If




            Next

        Catch ex As Exception

        End Try
    End Sub
End Class