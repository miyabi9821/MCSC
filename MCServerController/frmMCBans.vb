Public Class frmMCBans

    Private Sub frmMCBans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'LinkedLabel��TabStop���f�U�C�i����ύX�ł��Ȃ��̂Ń��[�h���Ɏw��
        llMCBansWeb.TabStop = False

        '�O���[�o��IP�\��
        txtGlobalIP.Text = gfGetGlobalIP()

        '�ݒ�ǂݍ���
        With Settings.Instance
            chkEnabled.Checked = .MCBansEnabled
            txtAPIKey.Text = .MCBansAPIKey
            txtMinRep.Text = .MCBansMinRep
            chkFailSafe.Checked = .MCBansFailSafe
        End With
    End Sub

    Private Sub llMCBansWeb_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llMCBansWeb.LinkClicked
        'MCBans Web�T�C�g�\��
        System.Diagnostics.Process.Start("http://www.mcbans.com/")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        'APIKey�̓��̓`�F�b�N
        If APIKeyIsValid() = False Then
            txtAPIKey.Focus()
            Exit Sub
        End If

        '臒l�̓��̓`�F�b�N
        If MinRepIsValid() = False Then
            txtMinRep.Focus()
            Exit Sub
        End If

        '�ݒ�̕ۑ�
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

    'APIKey�̓��͂��`�F�b�N����
    Private Function APIKeyIsValid() As Boolean
        'Web����R�s�[�����۔��p�X�y�[�X���悭����̂ŏ���
        txtAPIKey.Text = txtAPIKey.Text.Trim

        If chkEnabled.Checked = True And txtAPIKey.Text = String.Empty Then
            epMCBans.SetError(txtAPIKey, "APIKey�����͂���Ă��܂���")
            Return False
        Else
            epMCBans.SetError(txtAPIKey, "")
        End If

        If txtAPIKey.Text <> String.Empty And txtAPIKey.Text.Length <> 40 Then
            epMCBans.SetError(txtAPIKey, "APIKey������������܂���BAPIKey��40�����ł��B")
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

    '臒l�̓��͒l���`�F�b�N����
    Private Function MinRepIsValid() As Boolean
        '�󗓂Ȃ�f�t�H���g��3��ݒ肷��
        If txtMinRep.Text = String.Empty Then
            txtMinRep.Text = "3"
        End If

        '���l�`�F�b�N
        Dim dblTmp As Double = 0
        If Double.TryParse(txtMinRep.Text, dblTmp) = False Then
            epMCBans.SetError(txtMinRep, "臒l���s���ł��B0�`10�A������-1�œ��͂��Ă�������")
            Return False
        Else
            epMCBans.SetError(txtMinRep, "")
        End If

        '���l�͈̓`�F�b�N
        If dblTmp > 10 OrElse dblTmp < 0 Then
            If dblTmp <> -1 Then
                epMCBans.SetError(txtMinRep, "臒l���s���ł��B0�`10�A������-1�œ��͂��Ă�������")
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub llGlobalBanRules_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llGlobalBanRules.LinkClicked
        'MCBans Global Ban Rules�T�C�g�\��
        System.Diagnostics.Process.Start("http://support.mcbans.com/index.php?/Knowledgebase/Article/View/7/0/global-ban-rules")
    End Sub
End Class