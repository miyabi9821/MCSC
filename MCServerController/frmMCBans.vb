Public Class frmMCBans

    Private Sub frmMCBans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LinkedLabelのTabStopがデザイナから変更できないのでロード時に指定
        llMCBansWeb.TabStop = False

        'グローバルIP表示
        txtGlobalIP.Text = gfGetGlobalIP()

        '設定読み込み
        With Settings.Instance
            chkEnabled.Checked = .MCBansEnabled
            txtAPIKey.Text = .MCBansAPIKey
            txtMinRep.Text = .MCBansMinRep
            chkFailSafe.Checked = .MCBansFailSafe
        End With
    End Sub

    Private Sub llMCBansWeb_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llMCBansWeb.LinkClicked
        'MCBans Webサイト表示
        System.Diagnostics.Process.Start("http://www.mcbans.com/")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        'APIKeyの入力チェック
        If APIKeyIsValid() = False Then
            txtAPIKey.Focus()
            Exit Sub
        End If

        '閾値の入力チェック
        If MinRepIsValid() = False Then
            txtMinRep.Focus()
            Exit Sub
        End If

        '設定の保存
        With Settings.Instance
            .MCBansEnabled = chkEnabled.Checked
            .MCBansAPIKey = txtAPIKey.Text
            .MCBansMinRep = txtMinRep.Text
            .MCBansFailSafe = chkFailSafe.Checked
        End With

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtAPIKey_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAPIKey.Enter
        txtAPIKey.SelectAll()
    End Sub

    Private Sub txtAPIKey_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAPIKey.Validating
        APIKeyIsValid()
    End Sub

    'APIKeyの入力をチェックする
    Private Function APIKeyIsValid() As Boolean
        'Webからコピーした際半角スペースがよく入るので除去
        txtAPIKey.Text = txtAPIKey.Text.Trim

        If chkEnabled.Checked = True And txtAPIKey.Text = String.Empty Then
            epMCBans.SetError(txtAPIKey, "APIKeyが入力されていません")
            Return False
        Else
            epMCBans.SetError(txtAPIKey, "")
        End If

        If txtAPIKey.Text <> String.Empty And txtAPIKey.Text.Length <> 40 Then
            epMCBans.SetError(txtAPIKey, "APIKeyが正しくありません。APIKeyは40文字です。")
            Return False
        Else
            epMCBans.SetError(txtAPIKey, "")
        End If

        Return True
    End Function

    Private Sub txtMinRep_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMinRep.Enter
        txtMinRep.SelectAll()
    End Sub

    Private Sub txtMinRep_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMinRep.Validating
        MinRepIsValid()
    End Sub

    '閾値の入力値をチェックする
    Private Function MinRepIsValid() As Boolean
        '空欄ならデフォルトの3を設定する
        If txtMinRep.Text = String.Empty Then
            txtMinRep.Text = "3"
        End If

        '数値チェック
        Dim dblTmp As Double = 0
        If Double.TryParse(txtMinRep.Text, dblTmp) = False Then
            epMCBans.SetError(txtMinRep, "閾値が不正です。0～10、或いは-1で入力してください")
            Return False
        Else
            epMCBans.SetError(txtMinRep, "")
        End If

        '数値範囲チェック
        If dblTmp > 10 OrElse dblTmp < 0 Then
            If dblTmp <> -1 Then
                epMCBans.SetError(txtMinRep, "閾値が不正です。0～10、或いは-1で入力してください")
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub llGlobalBanRules_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llGlobalBanRules.LinkClicked
        'MCBans Global Ban Rulesサイト表示
        System.Diagnostics.Process.Start("http://support.mcbans.com/index.php?/Knowledgebase/Article/View/7/0/global-ban-rules")
    End Sub
End Class