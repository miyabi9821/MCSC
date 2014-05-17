Public Class frmConfig
    '�T�[�o�pJAR�t�@�C���̎w��
    Private Sub btnServerFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerFileOpen.Click
        '�f�t�H���g�t�@�C�����w��
        If rbOfficial.Checked = True Then
            ofdFile.FileName = "minecraft_server*.jar"
        ElseIf rbBukkit.Checked = True Then
            ofdFile.FileName = "craftbukkit*.jar"
        End If
        If txtJarPath.Text = "" OrElse System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtJarPath.Text)) = False Then
            '���ݎw�肳��Ă���p�X�̃t�H���_��������Ȃ��ꍇ�AMCSC�̃f�B���N�g�����w��
            ofdFile.InitialDirectory = Application.StartupPath
        Else
            ofdFile.InitialDirectory = System.IO.Path.GetDirectoryName(txtJarPath.Text)
        End If

        ofdFile.Filter = "JAR�t�@�C��(*.jar)|*.jar|���s�t�@�C��(*.exe)|*.exe|�S�Ẵt�@�C��(*.*)|*.*"
        ofdFile.FilterIndex = 1 'JAR�t�@�C�����f�t�H���g�I��

        If ofdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'OK�������ꂽ�Ƃ��̂ݏ�������
            txtJarPath.Text = ofdFile.FileName

            If txtJarPath.Text.IndexOf("bukkit") >= 0 Then
                '�t�@�C������bukkit�̋L�ڂ�����΃��W�I�{�^����bukkit�ɕύX(����)
                rbBukkit.Checked = True
                txtJarFileAugment.Text = "--nojline" '2013/07/28 Win8/WS2012��2�o�C�g��������������Ή�
            Else
                rbOfficial.Checked = True
                txtJarFileAugment.Text = "nogui"

                '�ŋ߂̓f�t�H���g�̃t�@�C�����Ƀo�[�W����������̂ŁA�ꉞ���W�I�{�^���̑I����������
                If txtJarPath.Text.IndexOf("minecraft_server.1.6") >= 0 Then
                    rbV14.Checked = True
                ElseIf txtJarPath.Text.IndexOf("minecraft_server.1.7") >= 0 Then
                    rbV17.Checked = True
                End If
            End If
        End If

    End Sub

    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�ݒ�̔��f
        With Settings.Instance
            txtJarPath.Text = .JarPath                  'JAR�p�X
            txtJavaExe.Text = .JavaPath                 'Java�p�X
            txtAugment.Text = .Augment                  'Java�N������
            txtJarFileAugment.Text = .JarFileAugment    '�T�[�o����
            chkAutoStart.Checked = .AutoStart           '�����N��
            chkAutoRecovery.Checked = .AutoRecovery     '�������J�o��
            chkShowConsole.Checked = .ShowConsole       '�E�B���h�E��\���̖���
            Select Case .ServerMode                     'Official/Bukkit
                Case 0
                    rbOfficial.Checked = True
                Case 1
                    rbBukkit.Checked = True
                Case Else
                    rbOfficial.Checked = True
            End Select
            Select Case .ServerVersion                  '-1.2.5 / 1.3.1-1.3.2 / 1.4-1.6.4 / 1.7-
                Case 0  '-1.2.5
                    rbV125.Checked = True
                Case 1 '1.3.1-1.3.2
                    rbV131.Checked = True
                Case 2 '1.4-1.6.4
                    rbV14.Checked = True
                Case 3 '1.7-
                    rbV17.Checked = True
                Case Else
                    rbV17.Checked = True
            End Select
            txtHeartBeatInterval.Text = .HeartBeatInterval
            txtHeartBeatStopCount.Text = .HeartBeatStopCount
            txtHeartBeatKillCount.Text = .HeartBeatKillCount
            chkHeartBeatUse0xFE.Checked = .HeartBeatUse0xFE
        End With

        'java.exe�̃p�X���ȈՌ���
        If System.IO.File.Exists(txtJavaExe.Text) = False Then
            txtJavaExe.Text = GetDefaultJavaPath()
        End If

        'MCServer��Jar�t�@�C�����ȈՌ���
        If System.IO.File.Exists(txtJarPath.Text) = False Then
            Dim strJarFiles As String() = System.IO.Directory.GetFiles(Application.StartupPath, "*.jar", System.IO.SearchOption.TopDirectoryOnly)
            If strJarFiles.Length > 0 Then
                txtJarPath.Text = strJarFiles(0) '�ǂꂩ�܂ł͔��f�o���Ȃ��̂ōŏ��Ɍ�����������ݒ�
                If strJarFiles(0).IndexOf("bukkit") >= 0 Then
                    '�t�@�C������bukkit�̋L�ڂ�����΃��W�I�{�^����bukkit�ɕύX(����)
                    rbBukkit.Checked = True
                    txtJarFileAugment.Text = "--nojline" '2013/07/28 Win8/WS2012��2�o�C�g��������������Ή�
                Else
                    rbOfficial.Checked = True
                    txtJarFileAugment.Text = "nogui"

                    '�ŋ߂̓f�t�H���g�̃t�@�C�����Ƀo�[�W����������̂ŁA�ꉞ���W�I�{�^���̑I����������
                    If strJarFiles(0).IndexOf("minecraft_server.1.6") >= 0 Then
                        rbV14.AutoCheck = True
                    ElseIf strJarFiles(0).IndexOf("minecraft_server.1.7") >= 0 Then
                        rbV17.Checked = True
                    End If
                End If

            End If

            '�������f�t�H���g�ɐݒ肷��
            txtAugment.Text = "-Xms1024M -Xmx1024M"

        End If
    End Sub

    Private Sub btnJavaExeOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJavaExeOpen.Click
        ofdFile.FileName = "java.exe"
        If txtJavaExe.Text = "" OrElse System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtJavaExe.Text)) = False Then
            '���ݎw�肳��Ă���p�X�̃t�H���_��������Ȃ��ꍇ�AMCSC�̃f�B���N�g�����w��
            ofdFile.InitialDirectory = Application.StartupPath
        Else
            ofdFile.InitialDirectory = System.IO.Path.GetDirectoryName(txtJavaExe.Text)
        End If
        ofdFile.Filter = "���s�t�@�C��(*.exe)|*.exe|�S�Ẵt�@�C��(*.*)|*.*"
        ofdFile.FilterIndex = 1 'EXE�t�@�C�����f�t�H���g�I��

        If ofdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'OK�������ꂽ�Ƃ��̂ݏ�������
            txtJavaExe.Text = ofdFile.FileName
        End If
    End Sub

    Private Function GetDefaultJavaPath() As String

        'Java.exe�����肻���ȏꏊ�̈ꗗ
        '(�D�揇�ʂ�����������ɋL�q���邱��)
        Dim strProgramFiles As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        Dim arDefaultJavaExePath As New ArrayList
        arDefaultJavaExePath.Add(strProgramFiles & "\java\jre7\bin\java.exe")
        arDefaultJavaExePath.Add(strProgramFiles & "\java\jre8\bin\java.exe")
        arDefaultJavaExePath.Add(strProgramFiles & "\java\jre6\bin\java.exe")
        arDefaultJavaExePath.Add(strProgramFiles & " (x86)\java\jre7\bin\java.exe")
        arDefaultJavaExePath.Add(strProgramFiles & " (x86)\java\jre8\bin\java.exe")
        arDefaultJavaExePath.Add(strProgramFiles & " (x86)\java\jre6\bin\java.exe")

        Dim strPath As String
        For Each strPath In arDefaultJavaExePath
            If System.IO.File.Exists(strPath) = True Then
                '��Ɍ���������K�p����
                Return strPath
            End If
        Next

        '������Ȃ�������Ƃ肠�������̎��s�t�@�C���̂���ꏊ
        Return Application.StartupPath

    End Function

    '�f�t�H���g�̋N���I�v�V�����ɖ߂��܂�
    Private Sub btnAugmentDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAugmentDefault.Click
        If txtAugment.Text <> "" Then
            If MessageBox.Show("�N���I�v�V�������f�t�H���g�ɖ߂��܂����H", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If
        txtAugment.Text = "-Xms1024M -Xmx1024M"
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        '�ݒ��ۑ����ĕ���
        With Settings.Instance
            .JarPath = txtJarPath.Text                  'JAR�p�X
            .JavaPath = txtJavaExe.Text                 'Java�p�X
            .Augment = txtAugment.Text                  'Java�N������
            .JarFileAugment = txtJarFileAugment.Text    '�T�[�o����
            .AutoStart = chkAutoStart.Checked           '�����N��
            .AutoRecovery = chkAutoRecovery.Checked     '�������J�o��
            .ShowConsole = chkShowConsole.Checked       '�E�B���h�E��\���̖���
            If rbOfficial.Checked = True Then             '�g�p�T�[�o
                .ServerMode = 0
            ElseIf rbBukkit.Checked = True Then
                .ServerMode = 1
            End If
            If rbV125.Checked = True Then               '�g�p�T�[�o�o�[�W����
                .ServerVersion = 0
            ElseIf rbV131.Checked = True Then
                .ServerVersion = 1
            ElseIf rbV14.Checked = True Then
                .ServerVersion = 2
            ElseIf rbV17.Checked = True Then
                .ServerVersion = 3
            End If
            .HeartBeatInterval = txtHeartBeatInterval.Text
            .HeartBeatStopCount = txtHeartBeatStopCount.Text
            .HeartBeatKillCount = txtHeartBeatKillCount.Text
            .HeartBeatUse0xFE = chkHeartBeatUse0xFE.Checked
        End With

        Settings.SaveToXmlFile()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtJarPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJarPath.TextChanged
        If System.IO.File.Exists(txtJarPath.Text) = False Then
            epInput.SetError(txtJarPath, "jar�t�@�C����������܂���")
        Else
            epInput.SetError(txtJarPath, "")
        End If
    End Sub

    Private Sub txtJavaExe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJavaExe.TextChanged
        If System.IO.File.Exists(txtJavaExe.Text) = False Then
            epInput.SetError(txtJavaExe, "java.exe��������܂���")
        Else
            epInput.SetError(txtJavaExe, "")
        End If
    End Sub

    Private Sub btnJarFileAugment_Click(sender As Object, e As EventArgs) Handles btnJarFileAugment.Click
        If MessageBox.Show("Jar File �N���I�v�V�������f�t�H���g�ɖ߂��܂����H", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If rbOfficial.Checked = True Then
            txtJarFileAugment.Text = "nogui"
        Else
            txtJarFileAugment.Text = "--nojline"
        End If
    End Sub

    '�T�[�o�̎�ނ��ύX���ꂽ�Ƃ�
    Private Sub ServerType_CheckedChanged(sender As Object, e As EventArgs) Handles rbBukkit.CheckedChanged, rbOfficial.CheckedChanged
        If rbOfficial.Checked = True Then
            txtJarFileAugment.Text = "nogui"
        Else
            txtJarFileAugment.Text = "--nojline"
        End If
    End Sub

End Class