' �o�b�N�A�b�v�ݒ���
' 2013/07/02 �T�[�o�t�@�C�����w�肳��ĂȂ���ԂŊe��_�C�A���O�\�����ɃG���[���o�����߁A�S�̓I�ɃG���[�`�F�b�N����

Public Class frmBackup

    Dim blnValid As Boolean = True

    Private Sub txtInterval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInterval.KeyPress
        '���l(��BackSpace)�ȊO�̓��͎͂󂯕t���Ȃ�
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtInterval_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInterval.Leave
        checkInterval(txtInterval.Name)
    End Sub

    Private Sub chkBackupEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBackupEnabled.CheckedChanged
        checkInterval(chkBackupEnabled.Name)
    End Sub

    Private Sub checkInterval(ByVal called As String)
        Try
            If chkBackupEnabled.Checked = False And called = chkBackupEnabled.Name Then
                epInput.Clear()
                blnValid = True
                Exit Sub
            End If

            If txtInterval.Text = "" And chkBackupEnabled.Checked = True Then
                epInput.SetError(Me.txtInterval, "����o�b�N�A�b�v��L���ɂ���ꍇ�͎w�肵�ĉ�����")
                blnValid = False
                Exit Sub
            End If

            If CInt(txtInterval.Text) < 10 Then
                epInput.SetError(Me.txtInterval, "10���ȉ��ɂ͐ݒ�ł��܂���")
                blnValid = False
                Exit Sub
            ElseIf CInt(txtInterval.Text) > 2147484 Then
                epInput.SetError(Me.txtInterval, "2147484���ȏ�ɂ͐ݒ�ł��܂���")
                blnValid = False
                Exit Sub
            End If
            epInput.Clear()
            blnValid = True
        Catch ex As System.OverflowException
            epInput.SetError(Me.txtInterval, "2147484���ȏ�ɂ͐ݒ�ł��܂���")
            blnValid = False
        Catch ex As Exception
            epInput.SetError(Me.txtInterval, "�s���ȕ��������͂���܂���")
            blnValid = False
            txtInterval.Text = ""
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If txtBackupTarget.Text = "" Then
                Exit Sub
            End If

            If System.IO.File.Exists(txtBackupTarget.Text) = False And _
                    System.IO.Directory.Exists(txtBackupTarget.Text) = False Then
                If MessageBox.Show("�w�肳�ꂽ�t�@�C���^�t�H���_�͑��݂��܂���B�ǉ����܂����H", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            clbBackupTarget.Items.Add(txtBackupTarget.Text, True)
            txtBackupTarget.Text = ""
            txtBackupTarget.Focus()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "��O�G���[")
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        clbBackupTarget.Items.Remove(clbBackupTarget.SelectedItem)
    End Sub

    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        txtBackupTarget.Text = clbBackupTarget.SelectedItem.ToString
        txtBackupTarget.Focus()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            If blnValid = False Then
                MessageBox.Show("���͂Ɍ�肪����܂�", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If

            '�ݒ��ۑ�����
            Settings.Instance.BackupEnabled = chkBackupEnabled.Enabled
            Settings.Instance.BackupInterval = txtInterval.Text
            Settings.Instance.BackupKeepDays = txtKeepDays.Text
            Settings.Instance.BackupOutput = txtBackupOutput.Text
            Settings.Instance.CompressEnabled = chkCompressEnabled.Checked
            Settings.Instance.BackupBeforeServerRun = chkBackupBeforeRun.Checked    '�T�[�o�N���O�o�b�N�A�b�v���s����

            Dim dtTarget As New DataTable("BackupTarget") '�Ώۃt�@�C���^�f�B���N�g���̕ۑ�
            dtTarget.Columns.Add("Checked")
            dtTarget.Columns.Add("Path")
            For i As Integer = 0 To clbBackupTarget.Items.Count - 1
                Dim newRow As DataRow = dtTarget.NewRow
                newRow("Checked") = clbBackupTarget.GetItemChecked(i).ToString
                newRow("Path") = clbBackupTarget.Items(i).ToString
                dtTarget.Rows.Add(newRow)
            Next
            Settings.Instance.BackupTarget = dtTarget

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("�ȉ��̗��R�ɂ��ݒ�̕ۑ��͎��s���܂����F" & vbCrLf & Err.Description, "��O�G���[")
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()

        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmBackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkBackupEnabled.Checked = Settings.Instance.BackupEnabled              '�L����ԓǍ�
        txtInterval.Text = Settings.Instance.BackupInterval                     '���s�C���^�[�o���Ǎ�
        txtKeepDays.Text = Settings.Instance.BackupKeepDays                     '���O�ێ������Ǎ�
        txtBackupOutput.Text = Settings.Instance.BackupOutput                   '�o�̓t�H���_�Ǎ�
        chkCompressEnabled.Checked = Settings.Instance.CompressEnabled          '�t�H���_���k�I�v�V����
        chkBackupBeforeRun.Checked = Settings.Instance.BackupBeforeServerRun    '�T�[�o�N���O�o�b�N�A�b�v���s����

        '�Ώۃt�@�C���^�t�H���_�Ǎ�
        clbBackupTarget.Items.Clear()
        Dim dtTarget As DataTable = Settings.Instance.BackupTarget
        Dim blnChecked As Boolean = False
        For i As Integer = 0 To dtTarget.Rows.Count - 1
            Try
                blnChecked = CBool(dtTarget.Rows(i)("Checked"))
            Catch ex As Exception
                blnChecked = False
            End Try
            '���X�g�s�ǉ�
            clbBackupTarget.Items.Add(dtTarget.Rows(i)("Path"), blnChecked)

        Next

        '�C���^�[�o�����󔒂̎��͂Ƃ肠����1���ԂɎw�肷��
        If txtInterval.Text = "" Then
            txtInterval.Text = 60
        End If

        '�o���f�[�V����
        checkInterval(txtInterval.Name)
    End Sub

    '�t�@�C����D&D�����e����
    '�Q�l�jhttp://blog.livedoor.jp/akf0/archives/51252181.html
    Private Sub clbBackupTarget_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles clbBackupTarget.DragEnter
        '�h���b�O���Ă���t�@�C���^�t�H���_�̎擾
        Dim FilePath() As String = _
         CType(e.Data.GetData(DataFormats.FileDrop), String())

        For idx As Integer = 0 To FilePath.Length - 1
            If (Not System.IO.File.Exists(FilePath(idx))) And (Not System.IO.Directory.Exists(FilePath(idx))) Then
                Return
            End If
        Next idx

        '�h���b�v�\�ȏꍇ�́A�G�t�F�N�g��ς���
        e.Effect = DragDropEffects.Copy

    End Sub


    'D&D�Œǉ�����
    Private Sub clbBackupTarget_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles clbBackupTarget.DragDrop
        '�h���b�O���Ă���t�@�C���^�t�H���_�̎擾
        Dim FilePath() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        'D&D���ꂽ�����X�g�ɒǉ�����
        For idx As Integer = 0 To FilePath.Length - 1
            clbBackupTarget.Items.Add(FilePath(idx), True)
        Next


    End Sub

    '�Q�ƃ{�^������A�o�b�N�A�b�v�o�͐�I���_�C�A���O���J��
    '2013/07/02 �����t�H���_�̈ʒu��ύX�i�w��ς݂Ȃ�w��ς݂̏ꏊ�A�w�肪������΋N���p�X�j
    Private Sub btnBrowsOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsOutput.Click
        Try
            If System.IO.Directory.Exists(txtBackupOutput.Text) = True Then
                fbdTarget.SelectedPath = txtBackupOutput.Text
            Else
                fbdTarget.SelectedPath = Application.StartupPath
            End If
            fbdTarget.ShowNewFolderButton = True '�V�����t�H���_�쐬�{�^����\��
            If fbdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupOutput.Text = fbdTarget.SelectedPath

        Catch ex As Exception
            MessageBox.Show(Err.Description, "��O�G���[")
        End Try
    End Sub

    'Target File�{�^������A�o�b�N�A�b�v�Ώۃt�@�C���I���_�C�A���O���J��
    '2013/07/02 �T�[�o�ݒ�ŃT�[�o�t�@�C�����������w�肳��ĂȂ��ꍇ�Ƀn���h������ĂȂ���O�G���[����������s��ɑΉ�
    Private Sub btnBrowsTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsTargetFile.Click
        Try
            If System.IO.File.Exists(Settings.Instance.JarPath) = True Then
                ofdTarget.InitialDirectory = System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)
            Else
                ofdTarget.InitialDirectory = Application.StartupPath
            End If


            ofdTarget.Filter = "�S�Ẵt�@�C��(*.*)|*.*"
            If ofdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupTarget.Text = ofdTarget.FileName
        Catch ex As Exception
            MessageBox.Show(Err.Description, "��O�G���[")
        End Try

    End Sub


    'Target Dir�{�^������A�o�b�N�A�b�v�Ώۃt�H���_�I���_�C�A���O���J��
    '2013/07/02 �T�[�o�ݒ�ŃT�[�o�t�@�C�����������w�肳��ĂȂ��ꍇ�Ƀn���h������ĂȂ���O�G���[����������s��ɑΉ�
    Private Sub btnBrowsTargetDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsTargetDir.Click
        Try
            If System.IO.File.Exists(Settings.Instance.JarPath) = True Then
                fbdTarget.SelectedPath = System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)
            Else
                fbdTarget.SelectedPath = Application.StartupPath
            End If

            fbdTarget.ShowNewFolderButton = False '�V�����t�H���_�쐬�{�^�����\��
            If fbdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupTarget.Text = fbdTarget.SelectedPath
        Catch ex As Exception
            MessageBox.Show(Err.Description, "��O�G���[")
        End Try
    End Sub

    Private Sub txtKeepDays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKeepDays.KeyPress
        '���l(��BackSpace)�ȊO�̓��͎͂󂯕t���Ȃ�
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
End Class