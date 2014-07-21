Public Class frmMain
    Private pstrCmsIPCalledObject As String 'cmsIP���Ăяo�����I�u�W�F�N�g��
    Private pstrPort As String = "" '�ǂݍ��񂾐ݒ�t�@�C���̃|�[�g�ԍ�
    Public chatting As Boolean = False '�`���b�g���O�E�B���h�E�\�������ǂ���
#Region "�萔"
    'WM_QUERYENDSESSION���b�Z�[�W
    Private Const WM_QUERYENDSESSION As Integer = &H11

    'WM_POWERBROADCAST���b�Z�[�W
    Public Const WM_POWERBROADCAST = &H218
    Public Const PBT_APMQUERYSUSPEND = &H0
    Public Const PBT_APMQUERYSTANDBY = &H1
    Public Const PBT_APMQUERYSUSPENDFAILED = &H2
    Public Const PBT_APMQUERYSTANDBYFAILED = &H3
    Public Const PBT_APMSUSPEND = &H4
    Public Const PBT_APMSTANDBY = &H5
    Public Const PBT_APMRESUMECRITICAL = &H6
    Public Const PBT_APMRESUMESUSPEND = &H7
    Public Const PBT_APMRESUMESTANDBY = &H8
#End Region

#Region "�I�[�o�[���C�h���\�b�h"
    Protected Overrides Sub WndProc( _
     ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_QUERYENDSESSION Then
            'WM_QUERYENDSESSION(�V���b�g�_�E��)
            pfWriteSystemLog("Shutdown Event detected.", Color.Blue)

            If btnRun.Enabled = False Then
                Try
                    gblnExitFlg = True '����I���t���O
                    gsSendCommand("stop")
                Catch ex As Exception
                    pfWriteSystemLog("Command Send Error.", Color.Red)
                End Try
            End If

        ElseIf m.Msg = WM_POWERBROADCAST Then
            'WM_POWERBROADCAST(�p���[��ԕύX)
            pfWriteSystemLog("PowerMode Change Event detected.", Color.Blue)

            If m.WParam.ToInt32 = PBT_APMSUSPEND Or m.WParam.ToInt32 = PBT_APMSTANDBY Then
                '�T�X�y���h�A�x�~��Ԃɓ���ꍇ�̓T�[�o��~
                If btnRun.Enabled = False Then
                    Try
                        gblnExitFlg = True '����I���t���O
                        gblnResumeFlg = True '�����t���O
                        gsSendCommand("stop")
                    Catch ex As Exception
                        pfWriteSystemLog("Command Send Error.", Color.Red)
                    End Try
                End If

            ElseIf m.WParam.ToInt32 = PBT_APMRESUMESUSPEND Or m.WParam.ToInt32 = PBT_APMRESUMESTANDBY Then
                '�T�X�y���h�A�x�~��Ԃ��畜�������ꍇ�́A��~�O�ɃT�[�o���N�����Ă��ꍇ�Ɍ���N��
                If gblnResumeFlg = True Then
                    If pfServerStart() = True Then
                        pfBackupTimerSet() '����ɋN���o������A�o�b�N�A�b�v�^�C�}�[�̏�Ԃ�ݒ�
                        If Settings.Instance.HeartBeatInterval >= 1 Then
                            'HB�Ԋu��1�ȏ�Ȃ�HB�L��
                            timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                            timHB.Start() 'HB�J�n
                        End If
                    End If
                    gblnResumeFlg = False '�����t���O�����ɖ߂�
                End If
            End If
        End If

        MyBase.WndProc(m)
    End Sub
#End Region

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerToolStripMenuItem.Click
        If frmConfig.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        With Settings.Instance
            'Start�{�^���̏��
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("�T�[�o�ݒ�̃t�@�C���w�肪����������܂���B" & vbCrLf & "�ݒ��ʂ��J���A�p�X���m�F���ĉ������B", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("�T�[�o�ݒ�̃t�@�C���w�肪����������܂���B" & vbCrLf & "�ݒ��ʂ��J���A�p�X���m�F���ĉ������B", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    If lblServer.Text <> "Running" Then
                        btnRun.Enabled = True
                    End If
                End If
            End If

            'Connected�\���̏��
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

    End Sub

    Private Sub NGWordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NGWordsToolStripMenuItem.Click
        'NG���[�h�ݒ��ʂ��J��
        frmNGWords.Show()
    End Sub

    Private Sub KickListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        '�����L�b�N�ݒ��ʂ��J��
        frmIPKickBAN.Show()
    End Sub

    '�I������
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If mcsProc.HasExited = False Then
                MessageBox.Show("�I������ꍇ�͐�ɃT�[�o���~���ĉ�����", "�I���ł��܂���", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        Try
            '�ő剻��Ԃ̊m��
            If Me.WindowState = FormWindowState.Maximized Then
                Settings.Instance.WindowMaximize = True
            Else
                Settings.Instance.WindowMaximize = False
            End If

            'Extended Players List Area�̗L�����
            Settings.Instance.ExtendedPlayersListEnabled = ExtendPlayersListAreaToolStripMenuItem.Checked

            '�R�}���h���X�g�̕ۑ�
            Settings.Instance.CommandRecent = New List(Of String)
            For i As Integer = 0 To cmbCommand.Items.Count - 1
                Settings.Instance.CommandRecent.Add(cmbCommand.Items(i))
            Next

            '�ݒ�̕ۑ�
            Settings.SaveToXmlFile()

            '�v���C���[�ꗗ�̕ۑ�
            SaveObjectProperties()
        Catch ex As Exception
            MessageBox.Show("�ݒ�t�@�C���̕ۑ��Ɏ��s���܂���", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '���ݒ�t�@�C���̃��J�o�����u(1.0������ō폜�\��)
        gfMoveOldConfig()

        '�ݒ�ǂݍ���
        Try
            Settings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'server.properties�Ǎ�
        If gfLoadServerProp() = False Then
            pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
        End If

        '�v���C���[�ꗗ�Ǎ�
        Try
            ReloadListviewFromXML()
        Catch ex As Exception
        End Try

        'NGWords�Ǎ�
        Try
            'NGWSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'Permission�Ǎ�
        Try
            PermissionSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        '�ʃX���b�h����̑����������
        Control.CheckForIllegalCrossThreadCalls = False

        '�R�}���h���X�g�̕���
        For i As Integer = 0 To Settings.Instance.CommandRecent.Count - 1
            cmbCommand.Items.Add(Settings.Instance.CommandRecent.Item(i))
        Next

        With Settings.Instance
            'Start�{�^���̏��
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("�T�[�o�ݒ�̃t�@�C���w�肪����������܂���B" & vbCrLf & "�ݒ��ʂ��J���A�p�X���m�F���ĉ������B", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("�T�[�o�ݒ�̃t�@�C���w�肪����������܂���B" & vbCrLf & "�ݒ��ʂ��J���A�p�X���m�F���ĉ������B", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    btnRun.Enabled = True
                End If
            End If

            'Extended Players List�̗L����ԕ���
            ExtendPlayersListAreaToolStripMenuItem.Checked = .ExtendedPlayersListEnabled

            'Connected�\���̏��
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

        '�E�B���h�E�ʒu�̕���
        Me.DesktopLocation = Settings.Instance.WindowPos
        If Me.DesktopLocation = New Point(-32000, -32000) Then
            '�ŏ�����Ԃ̍��W�ŕۑ�����Ă��Ƃ��͋����I��0,0�ɖ߂�
            Me.DesktopLocation = New Point(0, 0)
        End If

        '�E�B���h�E�T�C�Y�̕���
        Me.Size = Settings.Instance.WindowSize
        '�ő剻��Ԃ̕���
        If Settings.Instance.WindowMaximize = True Then
            Me.WindowState = FormWindowState.Maximized
        End If

        '�O���[�o��IP�A�h���X�擾
        lblGlobalIP.Text = gfGetGlobalIP()
        '�v���C�x�[�gIP�A�h���X�擾
        lblPrivateIP.Text = gfGetPrivateIP()
        '�����o�b�N�A�b�v�\��
        If Settings.Instance.BackupEnabled = True Then
            lblDataBackup.Text = "Enabled (stop)"
        End If

        '���O�\��
        pfWriteSystemLog("MCServerController " & GSTR_APP_VERSION & " Startup.", Color.Blue)

        '�o�[�W�����`�F�b�N
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black
        gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)

        '�����T�[�o�N��
        If Settings.Instance.AutoStart = True Then
            '�I�v�V�������L����������A�����I��MC�T�[�o�𗧂��グ��
            '2012/11/07 �����N���̍ہA�����o�b�N�A�b�v��L���ɂ���̂�Y��Ă����o�O���C��
            If pfServerStart() = True Then
                pfBackupTimerSet() '����ɋN���o������A�o�b�N�A�b�v�^�C�}�[�̏�Ԃ�ݒ�
                If Settings.Instance.HeartBeatInterval >= 1 Then
                    'HB�Ԋu��1�ȏ�Ȃ�HB�L��
                    timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                    timHB.Start() 'HB�J�n
                End If
            End If
        End If
    End Sub

    Private Function pfWriteSystemLog(ByVal msg As String, ByVal msgColor As Color) As Boolean
        Try
            rtbSystemLog.SelectionStart = rtbSystemLog.TextLength
            rtbSystemLog.SelectionColor = msgColor
            rtbSystemLog.SelectedText = "[" & System.DateTime.Now.ToString & "] " & msg & vbCrLf
            rtbSystemLog.SelectionStart = rtbSystemLog.TextLength
            rtbSystemLog.ScrollToCaret()
        Catch ex As Exception

        End Try
    End Function

    Private Function pfWriteServerLog(ByRef msg As String) As Boolean
        Try
            If msg = "" Then
                '���b�Z�[�W����Ȃ珈�����s��Ȃ�
                Return True
            End If

            If pfColorCodeTrim(msg) = False Then
                Return False
            End If

            '"[INFO] /127.0.0.1:xxxxx lost connection"�̃��O���o�͂��Ȃ�(2012/11/03 HB��0xFE���g�p���Ă��鎞�͏������Ȃ��悤�ύX)
            If Settings.Instance.HeartBeatUse0xFE = False AndAlso CheckHeartBeatLog(msg) = True Then
                Return True
            End If

            '���[�U�̃��O�C���^���O�A�E�g�`�F�b�N
            pfPlayerInfoUpdate(msg)

            '�`���b�g�`�F�b�N����
            pfPlayerChatCheck(msg)

            '�`���b�g���O�E�B���h�E�ɏo��
            ExpertChatLog(msg)

            'spawnpoint�R�}���h���ʏ���
            pfPlayerSpawnPointCheck(msg)

            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            '�f�t�H���g�̐F�ύX����
            If msg.IndexOf("[WARNING]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Orange
            ElseIf msg.IndexOf("[ERROR]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Red
            Else
                rtbServerLog.SelectionColor = Color.Black
            End If

            '�J�X�^���A�N�V�������s

            '���O�o��
            'rtbServerLog.SelectedText = "[" & System.DateTime.Now.ToString & "] " & msg & vbCrLf
            rtbServerLog.SelectedText = msg & vbCrLf
            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            rtbServerLog.ScrollToCaret()
        Catch ex As Exception

        End Try

    End Function

    '���O�ɏo�͂����J���[�R�[�h���폜����(�擪�ɓ��ꕶ��������)
    ' [34;22m [32;22m [36;22m [31;22m [35;22m [33;22m [37;22m [30;1m [34;1m [30;22m [32;1m [36;1m [31;1m [35;1m [33;1m [37;1m [m
    Private Function pfColorCodeTrim(ByRef msg As String) As Boolean
        Try
            msg = System.Text.RegularExpressions.Regex.Replace(msg, ChrW(27) & "\[(\d\d;(1|22))?m", "")

            Return True
        Catch ex As Exception
            pfWriteSystemLog("System error in pfColorCodeTrim." & vbCrLf & ex.Message, Color.Red)
            Return False
        End Try
    End Function

    '�T�[�o�̋N������
    '�Q�l(�v���Z�X�쐬)�Fhttp://blogs.wankuma.com/naoko/archive/2007/03/09/65823.aspx
    '�Q�l(�t�@�C���Ď�)�Fhttp://dobon.net/vb/dotnet/file/filesystemwatcher.html
    Private Function pfServerStart() As Boolean
        Try
            '2014/07/21 eula.txt�Ή�
            If gfGetEula() = False Then
                pfWriteSystemLog("You must agree to the EULA(eula.txt).", Color.Red)
                gblnExitFlg = True
                Return False
            End If

            '2012/11/03 �T�[�o�N���O�Ƀo�b�N�A�b�v���擾����I�v�V�����ǉ�(�����o�b�N�A�b�v�L�����̂�)
            '2012/11/03 Run�{�^�����L���̎��̂ݎ��s����悤�ɕύX�i�������J�o���������̓o�b�N�A�b�v���Ȃ��j
            If btnRun.Enabled = True AndAlso Settings.Instance.BackupEnabled = True AndAlso Settings.Instance.BackupBeforeServerRun = True Then
                If Settings.Instance.BackupTarget.Rows.Count = 0 Then
                    pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
                End If

                pfWriteSystemLog("Auto-Backup Started.", Color.Black)

                '�o�b�N�A�b�v���s
                Dim strRetMsg As String = "" '�o�b�N�A�b�v��������߂���郁�b�Z�[�W
                If gfBackup(strRetMsg) = True Then
                    pfWriteSystemLog(strRetMsg, Color.Blue)
                Else
                    pfWriteSystemLog(strRetMsg, Color.Red)
                End If
            End If

            'server.properties�ēǍ�
            If gfLoadServerProp() = False Then
                pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
            End If

            pfWriteSystemLog("Minecraft Server Starting...", Color.Black)

            Dim strWorkingPath = _
                System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)   '��ƃf�B���N�g���擾
            System.IO.Directory.SetCurrentDirectory(strWorkingPath)             '�J�����g�f�B���N�g���ړ�

            If Settings.Instance.ServerVersion >= 3 Then
                '1.7�ȍ~��.\logs\latest.log���Q�Ɓ����O�̑Ҕ��̓T�[�o�A�v���������ōs��
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "logs\latest.log")

            ElseIf Settings.Instance.ServerVersion <= 2 Then
                '1.7�ȑO��.\server.log���Q�Ɓ����O�̑Ҕ���MCSC�ōs��
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "server.log")

                '������server.log��log�t�H���_�Ɉړ�(+���l�[��)
                If System.IO.File.Exists("server.log") = True Then
                    If System.IO.Directory.Exists("log") = False Then
                        '���O�o�b�N�A�b�v�t�H���_�쐬
                        System.IO.Directory.CreateDirectory("log")
                    End If

                    'server.log�̈ړ�(yyyyMMdd-HHmmss���t�@�C�����ɕt��)
                    System.IO.File.Move("server.log", "log\server.log." & DateTime.Now.ToString("yyyyMMdd-HHmmss"))
                End If
            End If

            '�쐬����T�[�o�v���Z�X�̏ڍׂ��w��
            Dim mcsprocPsInfo As ProcessStartInfo = New ProcessStartInfo
            With mcsprocPsInfo
                '��ƃf�B���N�g��(jar�Ɠ����ꏊ)
                .WorkingDirectory = strWorkingPath
                '�N������Java.exe�̃p�X
                .FileName = Settings.Instance.JavaPath
                '���� 2012/10/17 jar�̃p�X��""�ł�����悤�ύX, 2013/07/28 �T�[�o�̋N�������w��ɑΉ�
                .Arguments = Settings.Instance.Augment & " -jar " _
                            & """" & Settings.Instance.JarPath & """" & " " & Settings.Instance.JarFileAugment

                If Settings.Instance.ShowConsole = True Then
                    ' �R���\�[����\������
                    .CreateNoWindow = False
                    .WindowStyle = ProcessWindowStyle.Normal
                Else
                    ' �V�����E�B���h�E�͍��Ȃ�
                    .CreateNoWindow = True
                    .WindowStyle = ProcessWindowStyle.Hidden
                End If
            End With

            '�v���Z�X�I�u�W�F�N�g�̏�����
            mcsProc = New System.Diagnostics.Process

            With Me.mcsProc
                ' ProcessStartInfo ���֘A�t��
                .StartInfo = mcsprocPsInfo
                ' Process �I������ Exited �C�x���g�𔭐������邩�ۂ�(����FFalse)
                ' OnExited ���\�b�h���g���ƃv���O�������� Exited �C�x���g�̔������\
                .EnableRaisingEvents = True
                AddHandler .Exited, AddressOf mcsOnProcessExited

                ' �J�n
                .Start()
            End With

            'PerformanceCounter�̃C���X�^���X�ݒ�
            pcCPU.InstanceName = mcsProc.ProcessName
            pcMem.InstanceName = mcsProc.ProcessName
            '����Vista/7��Private set -private-���Q�Ƃ���Ȃ�A
            '������OS���肵�Đ؂�ւ���΂悢���낤���B


            pfWriteSystemLog("Minecraft Server Startup Success.", Color.Blue)

            '�����܂Ő���Ȃ�^�C�}�[���J�n���A�{�^���̏�Ԃ�ύX����
            Me.timTick.Interval = 10000 '����̂݃��O��Ǎ��n�߂�܂�10�b�҂�(1.7�Ή�)
            Me.timTick.Start()
            btnRun.Enabled = False
            btnStop.Enabled = True
            btnKill.Enabled = True
            cmbCommand.Enabled = True
            UpdatePlayerListToolStripMenuItem.Enabled = True

            '�|�[�g�ԍ����Z�b�g
            pstrPort = ""

            '�I���t���O��False�ɕύX
            gblnExitFlg = False

            Return True

        Catch ex As Exception
            pfWriteSystemLog("Minecraft Server Startup Failure.", Color.Red)
            btnRun.Enabled = True
            btnStop.Enabled = False
            btnKill.Enabled = False
            cmbCommand.Enabled = False
            UpdatePlayerListToolStripMenuItem.Enabled = False
            Return False
        End Try
    End Function

    '�T�[�o�̋N���{�^��
    Private Sub btnServerRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        '�T�[�o�N��
        If pfServerStart() = True Then
            pfBackupTimerSet() '����ɋN���o������A�o�b�N�A�b�v�^�C�}�[�̏�Ԃ�ݒ�
            If Settings.Instance.HeartBeatInterval >= 1 Then
                'HB���o��1�ȏ�Ȃ�HB�L��
                timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                timHB.Start() 'HB�J�n
            End If
        End If
    End Sub

    '�v���Z�X���I�������Ƃ��Ɏ��s�����
    Private Sub mcsOnProcessExited(ByVal sender As Object, ByVal e As EventArgs)
        Try
            '�I�����server.log�ǎ��
            pfLogRead()

            '�v���C���[�̃I�����C���X�e�[�^�X��S���I�t���C���ɍX�V
            pfPlayerLogout("", True)

            '���O�o��
            pfWriteSystemLog("Minecraft Server Shutdown.", Color.Red)

            '�������J�o��
            If Settings.Instance.AutoRecovery = True Then
                '�������J�o���L����
                If gblnExitFlg = False Then
                    '���K�̏I���ł͂Ȃ��̂ŁA�T�[�o���ċN������
                    pfWriteSystemLog("Minecraft Server Auto-Recovery Execute.", Color.Red)
                    pfServerStart()

                Else
                    '�{�^���̏�ԕύX
                    btnRun.Enabled = True
                    btnStop.Enabled = False
                    btnKill.Enabled = False
                    cmbCommand.Enabled = False
                    UpdatePlayerListToolStripMenuItem.Enabled = False

                    '�o�b�N�A�b�v�p�^�C�}�̐ݒ�
                    pfBackupTimerSet()

                    'HB��~
                    timHB.Stop()
                End If

            Else
                '�������J�o��������
                gblnExitFlg = True '�I���t���O��L���ɂ���

                '�{�^���̏�ԕύX
                btnRun.Enabled = True
                btnStop.Enabled = False
                btnKill.Enabled = False
                cmbCommand.Enabled = False
                UpdatePlayerListToolStripMenuItem.Enabled = False

                '�o�b�N�A�b�v�p�^�C�}�̐ݒ�
                pfBackupTimerSet()

                'HB��~
                timHB.Stop()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function pfLogRead() As Boolean
        Static intFileSize As Integer '�ȑO�Ƀ`�F�b�N�����t�@�C���T�C�Y���i�[

        '�t�@�C���`�F�b�N
        If System.IO.File.Exists(gstrLogFilePath) = False Then
            Return False
        End If

        '�t�@�C���T�C�Y�`�F�b�N
        Dim fiLog As New System.IO.FileInfo(gstrLogFilePath)
        Try
            If intFileSize = fiLog.Length Then
                '�t�@�C���T�C�Y�ɕω����Ȃ���Ώ������Ȃ�
                Exit Function
            ElseIf intFileSize > fiLog.Length Then
                '�L�^����Ă郍�O�T�C�Y�̕����傫���ꍇ�̓��O�t�@�C�����폜���ꂽ�Ƃ���
                intFileSize = 0
            End If
        Catch ex As Exception
            intFileSize = 0
        End Try

        Try
            '���O�t�@�C�����J��
            Dim fsLog As New System.IO.FileStream(gstrLogFilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

            '���O�t�@�C���̕����R�[�h���� 2013/12/08
            Dim bsLog(fsLog.Length - 1) As Byte
            fsLog.Read(bsLog, 0, bsLog.Length)
            Dim enc As System.Text.Encoding = GetCode(bsLog)

            Dim srLog As New System.IO.StreamReader(fsLog, enc)
            '���ɓǂݍ��ݍς݂̏��܂ŃV�[�N
            srLog.BaseStream.Seek(intFileSize, IO.SeekOrigin.Begin)

            '�ǉ����ꂽ���b�Z�[�W��1�s���ǂݎ�菈�����s
            '2013/06/11 list�R�}���h�̏����̂��߁A�s�̌��������ǉ�
            Dim strReadBuf As String = String.Empty
            While (srLog.Peek() >= 0)
                strReadBuf = srLog.ReadLine
                If pfListComanndCheck(strReadBuf) = True Then
                    strReadBuf = strReadBuf & " " & srLog.ReadLine
                End If
                pfWriteServerLog(strReadBuf)
                Application.DoEvents()
            End While

            '���O�t�@�C�������
            srLog.Close()
            fsLog.Close()

            '����ǎ�p�I�t�Z�b�g�ʒu���L��
            intFileSize = fiLog.Length
        Catch ex As Exception
            pfWriteSystemLog("LogFile Read Failure.", Color.Red)
        End Try
    End Function

    '1.3.1�ȍ~��list�R�}���h���s����"There are 0/0 players online:"�����o
    Private Function pfListComanndCheck(msg) As Boolean
        Try
            Dim strSplitMsg As String() = msg.Split(" ")
            If strSplitMsg.Length = 8 Then
                If strSplitMsg(3) = "There" _
                   AndAlso strSplitMsg(4) = "are" _
                   AndAlso strSplitMsg(6) = "players" _
                   AndAlso strSplitMsg(7) = "online:" Then
                    Return True
                End If

            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub timTick_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timTick.Tick
        Try
            If timTick.Interval = 10000 Then '10�b��1�b�ɕύX����
                timTick.Interval = 1000
            End If

            '�v���Z�X���I�����Ă�����^�C�}�[��~
            If Me.mcsProc.HasExited = True Then
                Me.timTick.Stop()
                '���N���A
                lblServer.Text = "Not Running"
                lblUptime.Text = "-"
                lblCPUUsage.Text = "-"
                lblMemUsage.Text = "-"
                'lblConnected.Text = "-"
                Exit Sub
            End If

            '���O�ǂݍ���
            timTick.Enabled = False '���O�ǂݎ�肪�I���܂Ń^�C�}�[��~
            pfLogRead()

            '���X�V
            With mcsProc
                '�T�[�o��
                lblServer.Text = "Running"
                '�T�[�o�N������
                'lblUptime.Text = New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                Dim tsInterval As TimeSpan = DateTime.Now.Subtract(.StartTime)
                lblUptime.Text = tsInterval.Days & "d " & New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                'CPU�g�p��
                lblCPUUsage.Text = Math.Round(pcCPU.NextValue / System.Environment.ProcessorCount, 1, MidpointRounding.AwayFromZero).ToString("0.0") & " %"
                '�����������g�p��
                lblMemUsage.Text = (pcMem.NextValue / 1024 / 1024).ToString("0.0") & " MB"
            End With

            '�X�P�W���[������

        Catch ex As Exception


        Finally
            '�^�C�}�[�ĊJ�i�T�[�o�N�����Ɍ���j
            If timTick.Enabled = False And btnRun.Enabled = False Then
                timTick.Enabled = True
            End If
        End Try

    End Sub

    Private Sub cmbCommand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCommand.KeyPress
        Try
            'Enter��������A���e�L�X�g����łȂ���Ώ������s
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) AndAlso Me.cmbCommand.Text <> "" Then
                '����̓��͂̏���
                Select Case Char.ToLower(cmbCommand.Text)
                    Case "stop"
                        'STOP�����͂��ꂽ�ꍇ�͐���I���J�n�Ƃ���
                        gblnExitFlg = True
                End Select

                '�R�}���h���s
                gsSendCommand(cmbCommand.Text)

                '���s�����R�}���h�����X�g�ɕۑ�
                If pfSetCommandList(Me.cmbCommand.Text) = False Then
                    '���X�g�ǉ��G���[�̃��O�\��
                    pfWriteSystemLog("Command List Edit Error", Color.Red)
                End If

                pfWriteSystemLog("Command Send : " & cmbCommand.Text, Color.Black)

                cmbCommand.Text = ""
            End If
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
            '���s�����R�}���h�����X�g�ɕۑ�
            If pfSetCommandList(Me.cmbCommand.Text) = False Then
                '���X�g�ǉ��G���[�̃��O�\��
                pfWriteSystemLog("Command List Edit Error.", Color.Red)
            End If
            cmbCommand.Text = ""
        End Try

    End Sub

    Private Function pfSetCommandList(ByVal cmd As String) As Boolean
        Try
            If Me.cmbCommand.Items.Count = 0 Then
                '�A�C�e�������݂��Ȃ��ꍇ�͒ǉ����đ��I��
                Me.cmbCommand.Items.Add(cmd)
                Return True
            End If

            '���͂����R�}���h�����X�g�Ɋ��ɑ��݂��Ȃ����`�F�b�N
            For i As Integer = 0 To Me.cmbCommand.Items.Count - 1
                If Me.cmbCommand.Items(i) = cmd Then
                    '��v����Item�����݂����ꍇ�́A�폜
                    Me.cmbCommand.Items.RemoveAt(i)
                    Exit For
                End If
            Next

            '�R�}���h�����X�g�ɒǉ�
            Me.cmbCommand.Items.Insert(0, cmd)

            '�R�}���h������20�𒴂����ꍇ�͌Â������폜
            If Me.cmbCommand.Items.Count > 20 Then
                For i As Integer = 20 To Me.cmbCommand.Items.Count - 1
                    Me.cmbCommand.Items.RemoveAt(i)
                Next
            End If

            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    'stop�{�^�����������Ƃ��́Astop�R�}���h�𑗐M����
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Try
            gblnExitFlg = True '����I���t���O
            gsSendCommand("stop")
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
        End Try
    End Sub

    Private Sub InformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformationToolStripMenuItem.Click
        '�C���t�H���[�V�����\��
        frmInformation.Show()
        frmInformation.Activate()
    End Sub

    '�T�[�o���b�Z�[�W�����āAPlayer�̈ꗗ���X�V����
    Private Function pfPlayerInfoUpdate(ByVal msg As String) As Boolean
        Try
            'MCBans�A�g�@�\�������I�ɖ����� # FOR_DEBUG #
            Settings.Instance.MCBansEnabled = False

            '�ʓ|�ȃX�y�[�X��u������
            Dim strEditMsg As String = msg
            If Settings.Instance.ServerVersion >= 3 Then
                If strEditMsg.IndexOf("[Server thread/INFO]:") >= 0 Then
                    strEditMsg = strEditMsg.Replace("[Server thread/INFO]:", "[Server%20thread/INFO]:")
                End If
                If strEditMsg.IndexOf("[User Authenticator #1/INFO]:") >= 0 Then
                    strEditMsg = strEditMsg.Replace("[User Authenticator #1/INFO]:", "[User%20Authenticator%20#1/INFO]:")
                End If
                If strEditMsg.IndexOf("[Server Shutdown Thread/INFO]:") >= 0 Then
                    strEditMsg = strEditMsg.Replace("[Server Shutdown Thread/INFO]:", "[Server%20Shutdown%20Thread/INFO]:")
                End If
            End If

            Dim strSplitMsg As String() = strEditMsg.Split(" ")

            '�u�������X�y�[�X��߂�
            If Settings.Instance.ServerVersion >= 3 Then
                For i As Integer = 0 To strSplitMsg.Length - 1
                    strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                Next
            End If

            If Settings.Instance.ServerVersion >= 3 Then
                '�V�������O�`��
                Select Case strSplitMsg(1)
                    Case "[Server thread/INFO]:"
                        '2012/11/03 ���b�Z�[�W�̓��e����̏ꍇ���I������
                        If strSplitMsg.Length >= 3 AndAlso strSplitMsg(2).Length = 0 Then
                            Exit Function
                        End If

                        '�`���b�g���b�Z�[�W�̏ꍇ�͑��I������
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(2)
                            Case "Disconnecting" '���̎��_�łقڊm����BAN�̃��b�Z�[�W
                                If strSplitMsg(5) = "You" _
                                            AndAlso strSplitMsg(6) = "are" _
                                            AndAlso strSplitMsg(7) = "not" _
                                            AndAlso strSplitMsg(8) = "white-listed" _
                                            AndAlso strSplitMsg(9) = "on" _
                                            AndAlso strSplitMsg(10) = "this" _
                                            AndAlso strSplitMsg(11) = "server!" Then
                                    'white-list�ɒǉ�����Ă��Ȃ�
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(3)
                                    Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_WHITE) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If

                                ElseIf strSplitMsg(5) = "You" _
                                        AndAlso strSplitMsg(6) = "are" _
                                        AndAlso strSplitMsg(7) = "banned" _
                                        AndAlso strSplitMsg(8) = "from" _
                                        AndAlso strSplitMsg(9) = "this" _
                                        AndAlso strSplitMsg(10) = "server!" Then
                                    'Player BAN����Ă��鎞
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(3)
                                    Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If

                                ElseIf strSplitMsg(5) = "Your" _
                                        AndAlso strSplitMsg(6) = "IP" _
                                        AndAlso strSplitMsg(7) = "address" _
                                        AndAlso strSplitMsg(8) = "is" _
                                        AndAlso strSplitMsg(9) = "banned" _
                                        AndAlso strSplitMsg(10) = "from" _
                                        AndAlso strSplitMsg(11) = "this" _
                                        AndAlso strSplitMsg(12) = "server!" Then
                                    'IP BAN����Ă��鎞
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(3)
                                    Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(3) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'list�R�}���h�����s���ꂽ�Ƃ��i����1.2.5�܂Łj
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(3) = "are" _
                                   AndAlso strSplitMsg(5) = "players" _
                                   AndAlso strSplitMsg(6) = "online:" Then
                                    '2013/06/11 1.3.1�ȍ~��list�R�}���h���ʏ����Ή�
                                    '�����T�[�o�ł�2�s�������������ʁA���� [Server thread/INFO]: There are 0/0 players online: ���� [Server thread/INFO]: Player1, Player2,...�ƂȂ�̂ŋ������u
                                    'CraftBukkit�ł͂��̂܂܌������ď������Ă��܂��Ζ�薳��
                                    If strSplitMsg.Length >= 10 AndAlso strSplitMsg(8) = "[Server thread/INFO]:" Then '�����T�[�o
                                        If pfPlayerUpdateFromList(strSplitMsg, 9) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkit�T�[�o
                                        If pfPlayerUpdateFromList(strSplitMsg, 8) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    End If

                                End If


                            Case Else
                                If strSplitMsg.Length <= 3 Then
                                    Exit Function
                                End If

                                Select Case strSplitMsg(3)
                                    Case "lost"
                                        If strSplitMsg(4) = "connection:" Then
                                            If pfPlayerLogout(strSplitMsg(2)) = False Then
                                                pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                            End If
                                        End If

                                    Case Else
                                        Select Case strSplitMsg(3)
                                            Case "logged"
                                                If strSplitMsg(4) = "in" AndAlso strSplitMsg(5) = "with" AndAlso strSplitMsg(6) = "entity" Then
                                                    '���O�C���̃��b�Z�[�W
                                                    'Minecraft ID��IP�𔲂��o��
                                                    Dim strSplit() As String = strSplitMsg(2).Replace("[", "").Replace("]", "").Split("/")
                                                    Dim strUser = strSplit(0)
                                                    Dim strSplit2() As String = strSplit(1).Split(":")
                                                    Dim strIP = strSplit2(0)

                                                    If Settings.Instance.MCBansEnabled = True Then
                                                        Dim blnStat As Boolean = False
                                                        Dim strReason As String = String.Empty
                                                        Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                            Case -1 '�����s���̃G���[
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 0
                                                                If blnStat = False Then '�ڑ�NG
                                                                    pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                    gsSendCommand("kick " & strUser) 'kick
                                                                    gsSendCommand("ban " & strUser) 'ban
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                Else '�ڑ�OK
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                End If
                                                            Case 1 'online-mode�̒l���擾�o���Ȃ�
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 2 'online-mode��false������
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 3 'MCBans�T�[�o�ɂ��琳�����f�[�^���擾�o���Ȃ�����
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 4 'MCBans�A�g�ɕK�v�Ȑڑ���IP�A�h���X���������擾�o���Ȃ�����
                                                                pfWriteSystemLog("MCSC can't get IP Adress from Log in User:" & strUser & "", Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                        End Select
                                                    Else
                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                        End If
                                                    End If
                                                End If
                                        End Select
                                End Select
                        End Select
                End Select

            Else
                '�]���ʂ�̃��O�`��
                Select Case strSplitMsg(2) '[INFO]���̕���
                    Case "[INFO]" '1.6.4�܂ł̌`��
                        '2012/11/03 ���b�Z�[�W�̓��e����̏ꍇ���I������
                        If strSplitMsg.Length >= 4 AndAlso strSplitMsg(3).Length = 0 Then
                            Exit Function
                        End If

                        '�`���b�g���b�Z�[�W�̏ꍇ�͑��I������
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(3)
                            Case "Disconnecting" '���̎��_�łقڊm����BAN�̃��b�Z�[�W
                                If strSplitMsg(6) = "You" _
                                            AndAlso strSplitMsg(7) = "are" _
                                            AndAlso strSplitMsg(8) = "not" _
                                            AndAlso strSplitMsg(9) = "white-listed" _
                                            AndAlso strSplitMsg(10) = "on" _
                                            AndAlso strSplitMsg(11) = "this" _
                                            AndAlso strSplitMsg(12) = "server!" Then
                                    'white-list�ɒǉ�����Ă��Ȃ�
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(4)
                                    Dim strSplit() As String = strSplitMsg(5).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_WHITE) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If

                                ElseIf strSplitMsg(6) = "You" _
                                        AndAlso strSplitMsg(7) = "are" _
                                        AndAlso strSplitMsg(8) = "banned" _
                                        AndAlso strSplitMsg(9) = "from" _
                                        AndAlso strSplitMsg(10) = "this" _
                                        AndAlso strSplitMsg(11) = "server!" Then
                                    'Player BAN����Ă��鎞
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(4)
                                    Dim strSplit() As String = strSplitMsg(5).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If

                                ElseIf strSplitMsg(6) = "Your" _
                                        AndAlso strSplitMsg(7) = "IP" _
                                        AndAlso strSplitMsg(8) = "address" _
                                        AndAlso strSplitMsg(9) = "is" _
                                        AndAlso strSplitMsg(10) = "banned" _
                                        AndAlso strSplitMsg(11) = "from" _
                                        AndAlso strSplitMsg(12) = "this" _
                                        AndAlso strSplitMsg(13) = "server!" Then
                                    'IP BAN����Ă��鎞
                                    'Minecraft ID��IP�𔲂��o��
                                    Dim strUser = strSplitMsg(4)
                                    Dim strSplit() As String = strSplitMsg(5).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(4) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'list�R�}���h�����s���ꂽ�Ƃ��i����1.2.5�܂Łj
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(4) = "are" _
                                   AndAlso strSplitMsg(6) = "players" _
                                   AndAlso strSplitMsg(7) = "online:" Then
                                    '2013/06/11 1.3.1�ȍ~��list�R�}���h���ʏ����Ή�
                                    '�����T�[�o�ł�2�s�������������ʁA���t ���� [INFO] There are 0/0 players online: ���t ���� [INFO] Player1, Player2,...�ƂȂ�̂ŋ������u
                                    'CraftBukkit�ł͂��̂܂܌������ď������Ă��܂��Ζ�薳��
                                    If strSplitMsg.Length >= 11 AndAlso strSplitMsg(10) = "[INFO]" Then '�����T�[�o
                                        If pfPlayerUpdateFromList(strSplitMsg, 11) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkit�T�[�o
                                        If pfPlayerUpdateFromList(strSplitMsg, 8) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    End If

                                End If


                            Case Else
                                '�z��3�܂ł����Ȃ��ꍇ�ɗ�O�G���[��f���̂ő΍� 2013/07/20
                                If strSplitMsg.Length <= 4 Then
                                    Exit Function
                                End If

                                Select Case strSplitMsg(4)
                                    Case "lost"
                                        If strSplitMsg(5) = "connection:" Then
                                            If strSplitMsg(6).IndexOf("disconnect") >= 0 Then
                                                '���O�A�E�g�̃��b�Z�[�W
                                                'disconnect.quitting��disconnect.genericReason�Adisconnect.endOfStream�������݂���̂�
                                                'disconnect���܂܂�Ă���ΓK�p�Ƃ���
                                                If pfPlayerLogout(strSplitMsg(3)) = False Then
                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                End If
                                            End If
                                        End If

                                    Case Else
                                        If Settings.Instance.ServerVersion >= 1 Then '1.3����̐V�`��
                                            Select Case strSplitMsg(4)
                                                Case "logged"
                                                    If strSplitMsg(5) = "in" AndAlso strSplitMsg(6) = "with" AndAlso strSplitMsg(7) = "entity" Then
                                                        '���O�C���̃��b�Z�[�W
                                                        'Minecraft ID��IP�𔲂��o��

                                                        '1.3pre�ł�UserID�݂̂�����
                                                        'Dim strUser = strSplitMsg(3)
                                                        'Dim strIP = "0.0.0.0"
                                                        'If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                        '    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                        'End If

                                                        '1.3.1�ł�UserID[/IP:Port]�Ə]���̌`������X�y�[�X�������Ȃ���
                                                        Dim strSplit() As String = strSplitMsg(3).Replace("[", "").Replace("]", "").Split("/")
                                                        Dim strUser = strSplit(0)
                                                        Dim strSplit2() As String = strSplit(1).Split(":")
                                                        Dim strIP = strSplit2(0)

                                                        '2012/11/11 MCBans�A�g�ǉ�
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 '�����s���̃G���[
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then '�ڑ�NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else '�ڑ�OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-mode�̒l���擾�o���Ȃ�
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-mode��false������
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBans�T�[�o�ɂ��琳�����f�[�^���擾�o���Ȃ�����
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans�A�g�ɕK�v�Ȑڑ���IP�A�h���X���������擾�o���Ȃ�����
                                                                    pfWriteSystemLog("MCSC can't get IP Adress from Log in User:" & strUser & "", Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                            End Select
                                                        Else
                                                            If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                            End If
                                                        End If
                                                    End If
                                            End Select
                                        Else '1.2.5�ȑO�̌`��
                                            Select Case strSplitMsg(5)
                                                Case "logged"
                                                    If strSplitMsg(6) = "in" AndAlso strSplitMsg(7) = "with" AndAlso strSplitMsg(8) = "entity" Then
                                                        '���O�C���̃��b�Z�[�W
                                                        'Minecraft ID��IP�𔲂��o��
                                                        Dim strUser = strSplitMsg(3)
                                                        Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                                        Dim strIP = strSplit(0)

                                                        '2012/11/11 MCBans�A�g�ǉ�
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 '�����s���̃G���[
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then '�ڑ�NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else '�ڑ�OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-mode�̒l���擾�o���Ȃ�
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-mode��false������
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBans�T�[�o�ɂ��琳�����f�[�^���擾�o���Ȃ�����
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans�A�g�ɕK�v�Ȑڑ���IP�A�h���X���������擾�o���Ȃ�����
                                                                    pfWriteSystemLog("MCSC can't get IP Adress from Log in User:" & strUser & "", Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                            End Select
                                                        Else
                                                            If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                            End If
                                                        End If

                                                    End If
                                            End Select
                                        End If
                                End Select
                        End Select
                End Select
            End If

        Catch ex As Exception
            pfWriteSystemLog("unknown error in pfPlayerInfoUpdate." & vbCrLf & ex.Message, Color.Red)
        End Try

    End Function

    '�v���C���[���O�C�����ɁA�v���C���[�����X�V����
    Private Function pfPlayerLogin(ByVal user As String, ByVal IP As String, ByVal online As String) As Boolean
        Try
            Dim item() As String = {user, IP, online, DateTime.Now, "", "", 0} '2013/06/08 SpawnPoint�ǉ��ɂ��C��

            If lvPlayers.Items.Count = 0 Then '1�l�ڂȂ瑦�ǉ�
                lvPlayers.Items.Add(New ListViewItem(item))
                Return True
            End If

            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    '���Ƀ��X�g�ɑ��݂���ꍇ�AIP��Online���X�V
                    lvPlayers.Items(i).SubItems(1).Text = IP
                    lvPlayers.Items(i).SubItems(2).Text = online
                    If online = GSTR_ONLINE_TRUE Then '�I�����C���ɂȂ����Ƃ��̂݃��O�C�����ԍX�V
                        lvPlayers.Items(i).SubItems(3).Text = DateTime.Now '���O�C������
                    End If
                    Return True
                End If
            Next

            '���X�g�ɑ��݂��Ȃ���Βǉ�
            lvPlayers.Items.Add(New ListViewItem(item))
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    '�v���C���[���O�A�E�g���ɁA�v���C���[�����X�V����
    '2013/06/09 �S�����I�t���C���ɍX�V���郂�[�h�ǉ�(���[�U�[���͋󗓂ŗǂ�)
    Private Function pfPlayerLogout(ByVal user As String, Optional ByVal allOfflineMode As Boolean = False) As Boolean
        If lvPlayers.Items.Count = 0 Then '���X�g����Ȃ珈�����Ȃ�
            Return True
        End If

        If allOfflineMode = True Then '�S���I�t���C����ԂɕύX
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                '2014/05/17 ���O�C�����ĂȂ��l�܂Ń��O�A�E�g���Ԃ��X�V���Ă����̂ŏC��
                If lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now '���O�A�E�g����
                End If
            Next

        Else '����̃��[�U�[�̏�Ԃ��X�V
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now '���O�A�E�g����
                    Return True
                End If
            Next
        End If

    End Function

    'list�R�}���h�̌��ʂ���v���C���[�̃X�e�[�^�X���X�V����
    '2012/11/03 1.3����list�̎��s���ʕ\�����ς�������߁AstartIndex���w��ł���悤�ɕύX
    Private Function pfPlayerUpdateFromList(ByVal splitmsg() As String, Optional ByVal startIndex As Integer = 5) As Boolean
        '���O�A�E�g�`�F�b�N�i���O�A�E�g�������o���ĂȂ����[�U�̃I�����C���X�e�[�^�X���I�t���C���ɐݒ�j
        Dim blnOnline As Boolean = False
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                'Online�̃��[�U�݂̂�ΏۂɌ���

                blnOnline = False
                For iPlayer As Integer = startIndex To splitmsg.Length - 1
                    If lvPlayers.Items(iList).SubItems(0).Text = splitmsg(iPlayer).Trim(",") Then
                        blnOnline = True
                        Exit For
                    End If
                Next

                '�I�����C���̃v���C���[�ꗗ�ɑ��݂��Ȃ���ΐؒf�ς݂Ƃ���
                If blnOnline = False Then
                    pfPlayerLogout(lvPlayers.Items(iList).SubItems(0).Text)
                End If
            End If
        Next

        '���O�C���`�F�b�N�i���O�C���������o���ĂȂ����[�U�̃I�����C���X�e�[�^�X���I�����C���ɐݒ�j
        Dim blnListed As Boolean = False
        Dim blnUpdate As Boolean = False
        For iPlayer As Integer = startIndex To splitmsg.Length - 1

            blnListed = False
            blnUpdate = False

            For iList As Integer = 0 To lvPlayers.Items.Count - 1
                '�󕶎��Ȃ瑦�X�L�b�v
                If splitmsg(iPlayer).Trim(",") = "" Then
                    Continue For
                End If

                If splitmsg(iPlayer).Trim(",") = lvPlayers.Items(iList).SubItems(0).Text Then
                    'ListView�Ƀ��[�U�[��������
                    If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                        '����Online�̃��[�U�Ȃ牽�����Ȃ�
                        blnListed = True
                        blnUpdate = False

                    Else
                        'Online�ȊO�Ȃ�X�e�[�^�X�X�V
                        blnListed = True
                        blnUpdate = True

                    End If
                    Continue For
                End If

            Next

            '�X�V����
            Select Case blnListed
                Case True
                    If blnUpdate = True Then
                        pfPlayerLogin(splitmsg(iPlayer).Trim(","), "unknown", GSTR_ONLINE_TRUE)
                    End If

                Case False
                    If splitmsg(iPlayer).Trim(",") <> "" Then
                        pfPlayerLogin(splitmsg(iPlayer).Trim(","), "unknown", GSTR_ONLINE_TRUE)
                    End If

            End Select
        Next

        Return True
    End Function

    '�`���b�g���b�Z�[�W�̊Ď�
    'NGWords�Ŏw�肳�ꂽ���������������bad count�̏㏸/kick/ban�����s��
    '2012-05-25 08:21:29 [INFO] <USERNAME> Chat Message �`�����Ď�����
    Private Function pfPlayerChatCheck(ByVal msg As String) As Boolean
        Try
            Dim strSplitMsg As String()
            Dim strPlayerID As String = String.Empty
            Dim strChat As String = String.Empty
            Dim intCommandOffset As Integer = 0

            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4�ȑO
                '2014-05-17 16:21:55 [INFO] <miyabi9821> test

                intCommandOffset = 4

                strSplitMsg = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '�v�f����4�ȉ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3�߂̗v�f��[INFO]����Ȃ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    '���b�Z�[�W��\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)����Ȃ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                'PlayerID�擾
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?<(.+?)> (.*)", "$2")
                '�`���b�g���b�Z�[�W�擾
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)", "$3")

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7�ȍ~
                '[16:05:19] [Server thread/INFO]: <miyabi9821> test

                intCommandOffset = 3

                '�ʓ|�ȃX�y�[�X��u������
                Dim strEditMsg As String = msg
                If Settings.Instance.ServerVersion >= 3 Then
                    If strEditMsg.IndexOf("[Server thread/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[Server thread/INFO]:", "[Server%20thread/INFO]:")
                    End If
                    If strEditMsg.IndexOf("[User Authenticator #1/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[User Authenticator #1/INFO]:", "[User%20Authenticator%20#1/INFO]:")
                    End If
                    If strEditMsg.IndexOf("[Server Shutdown Thread/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[Server Shutdown Thread/INFO]:", "[Server%20Shutdown%20Thread/INFO]:")
                    End If
                End If

                strSplitMsg = strEditMsg.Split(" ")

                '�u�������X�y�[�X��߂�
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '�v�f����3�ȉ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2�߂̗v�f��[Server thread/INFO]:����Ȃ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                    '���b�Z�[�W������Ȃ�
                    Exit Function
                End If

                'PlayerID�擾
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)", "$2")
                '�`���b�g���b�Z�[�W�擾
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?(<.+?>) (.*)", "$3")

            End If

            ''*** NG Words���� ***
            'With NGWSettings.Instance
            '    If .NGWordsEnabled = True Then
            '        Dim intBadCount As Integer = 0
            '        With NGWSettings.Instance.NGWords
            '            For i As Integer = 0 To .Rows.Count - 1
            '                If CBool(.Rows(i)(0)) = True Then
            '                    If strChat.IndexOf(.Rows(i)(1)) >= 0 Then
            '                        'NGWords���`���b�g������Ɍ��������ꍇ�ABadCount���Z
            '                        intBadCount += CInt(.Rows(i)(2))
            '                    End If
            '                End If
            '            Next
            '        End With

            '        'BadCount��0���傫�����
            '        If intBadCount > 0 Then
            '            '�v���C���[�ꗗ���猟��
            '            With lvPlayers
            '                For i As Integer = 0 To lvPlayers.Items.Count - 1
            '                    If .Items(i).SubItems(0).Text = strPlayerID Then
            '                        'ID��������BadCount���Z
            '                        intBadCount += intBadCount + CInt(.Items(i).SubItems(5).Text)
            '                        .Items(i).SubItems(0).Text = intBadCount.ToString

            '                        'say�R�}���h�Ōx��
            '                        gsSendCommand("say " & strPlayerID)

            '                        'BadCount��臒l�𒴂�����BAN�R�}���h���s
            '                        If intBadCount > 10 Then
            '                            gsSendCommand("ban " & strPlayerID)
            '                        End If
            '                    End If
            '                Next
            '            End With
            '        End If
            '    End If
            'End With

            '*** Permission ���� ***
            With PermissionSettings.Instance
                If .Enabled = True Then
                    If .PrefixChar.Length = 1 Then
                        '�`���b�g���b�Z�[�W��1�����ڂ��w�肳�ꂽPrefix Character�Ȃ珈���J�n
                        If strChat.Substring(0, 1) = .PrefixChar Then
                            'strSplitMsg(4)���R�}���h�ɂȂ��Ă�̂ŁAPrefix Character�𔲂����R�}���h��؂�o��
                            '1.7�ȍ~��(3)�ɂȂ��Ă��܂����̂ŁA�Œ�l�ł͂Ȃ��ϐ���
                            Dim strCommand As String = strSplitMsg(intCommandOffset).Substring(1, strSplitMsg(intCommandOffset).Length - 1)

                            '���s���[�U�[���R�}���h�̎��s�p�[�~�b�V�����������Ă��邩
                            Select Case pfGetPermission(strCommand, strPlayerID)
                                Case 1 '�p�[�~�b�V��������
                                    '�R�}���h���s����
                                    Select Case strCommand
                                        Case "tp"
                                            'strSplitMsg 4:Command 5:PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                '�w�肳�ꂽ�v���C���[�����邩�H
                                                If pfIsPlayerOnline(strSplitMsg(intCommandOffset + 1)) = True Then
                                                    'tp <from PlayerID> <to PlayerID>
                                                    gsSendCommand("tp " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1))
                                                Else
                                                    gsSendCommand("tell " & strPlayerID & " Target Player is not Online.")
                                                End If
                                            Else
                                                '�R�}���h�̎g�p���@�\��
                                                gsSendCommand("tell " & strPlayerID & " tp comand usage : " & .PrefixChar & "tp <Player>")
                                            End If
                                        Case "give"
                                            'strSplitMsg 4:Command 5:ItemID 6:Num 7:Damage
                                            Select Case strSplitMsg.Length
                                                Case 6
                                                    'give <PlayerID> <ItemID>
                                                    gsSendCommand("give " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1))
                                                Case 7
                                                    'give <PlayerID> <ItemID> <Num>
                                                    gsSendCommand("give " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                Case 8
                                                    Select Case Settings.Instance.ServerMode
                                                        Case 0 '�����T�[�o
                                                            'give <PlayerID> <ItemID> <Num> <Damage>
                                                            gsSendCommand("give " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                        Case Else '����ȊO
                                                            '��Bukkit�ɂ�Damage�w��Ȃ�
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num>")
                                                    End Select
                                                Case Else
                                                    Select Case Settings.Instance.ServerMode
                                                        Case 0 '�����T�[�o
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num> <Damage>")
                                                        Case Else '����ȊO
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num>")
                                                    End Select
                                            End Select
                                        Case "time"
                                            'strSplitMsg 4:Command 5:set/add 6:amount
                                            If strSplitMsg.Length = intCommandOffset + 3 Then
                                                If strSplitMsg(intCommandOffset + 1) = "set" OrElse strSplitMsg(intCommandOffset + 1) = "add" Then
                                                    'time add/set <amount>
                                                    gsSendCommand("time " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                Else
                                                    gsSendCommand("tell " & strPlayerID & " time command option is add/set.")
                                                End If
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " time comand usage : " & .PrefixChar & "time <add/set> <0-24000>")
                                            End If
                                        Case "xp"
                                            'strSplitMsg 4:Command 5:amount
                                            If strSplitMsg.Length = 6 Then
                                                'xp <amount> <PlayerID>
                                                gsSendCommand("xp " & strSplitMsg(intCommandOffset + 1) & " " & strPlayerID)
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " xp comand usage : " & .PrefixChar & "xp <0-5000>")
                                            End If
                                        Case "gamemode"
                                            'strSplitMsg 4:Command 5:0/1/2
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'gamemode 0/1/2 <PlayerID>
                                                gsSendCommand("gamemode " & strSplitMsg(intCommandOffset + 1) & " " & strPlayerID)
                                                Select Case strSplitMsg(intCommandOffset + 1)
                                                    Case 0
                                                        gsSendCommand("tell " & strPlayerID & " gamemode changed to 0=Servival")
                                                    Case 1
                                                        gsSendCommand("tell " & strPlayerID & " gamemode changed to 1=Creative")
                                                    Case 2
                                                        gsSendCommand("tell " & strPlayerID & " gamemode changed to 2=Adventure")
                                                    Case Else
                                                        gsSendCommand("tell " & strPlayerID & " gamemode comand usage : " & .PrefixChar & "gamemode <0/1/2>")
                                                End Select
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " gamemode comand usage : " & .PrefixChar & "gamemode <0/1/2>")
                                            End If
                                        Case "kick"
                                            'strSplitMsg 4:Command 5:Target PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                If pfIsPlayerOnline(strSplitMsg(intCommandOffset + 1)) = True Then
                                                    'kick <PlayerID>
                                                    gsSendCommand("kick " & strSplitMsg(intCommandOffset + 1))
                                                Else
                                                    gsSendCommand("tell " & strPlayerID & " Target Player is not Online.")
                                                End If
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " kick comand usage : " & .PrefixChar & "kick <PlayerID>")
                                            End If
                                        Case "ban"
                                            'strSplitMsg 4:Command 5:Target PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'banned-players.txt�ɑ��݂��邩�`�F�b�N(����\��)

                                                'ban <PlayerID>
                                                gsSendCommand("ban " & strSplitMsg(intCommandOffset + 1))
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " ban comand usage : " & .PrefixChar & "ban <PlayerID>")
                                            End If
                                        Case "pardon"
                                            'strSplitMsg 4:Command 5:Target PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'banned-players.txt�ɑ��݂��邩�`�F�b�N(����\��)

                                                'pardon <PlayerID>
                                                gsSendCommand("pardon " & strSplitMsg(intCommandOffset + 1))
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " pardon comand usage : " & .PrefixChar & "pardon <PlayerID>")
                                            End If
                                        Case "whitelist"
                                            'strSplitMsg 4:Command 5:Command Mode 6:PlayerID(add/remove)
                                            Select Case strSplitMsg.Length
                                                Case intCommandOffset + 2
                                                    Select Case strSplitMsg(intCommandOffset + 1)
                                                        Case "on"
                                                            'whitelist on
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1))
                                                            gsSendCommand("tell " & strPlayerID & " Whitelist Enabled.")
                                                        Case "off"
                                                            'whitelist off
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1))
                                                            gsSendCommand("tell " & strPlayerID & " Whitelist Disabled.")
                                                        Case "reload"
                                                            'whitelist reload
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1))
                                                            gsSendCommand("tell " & strPlayerID & " whitelist reloaded.")
                                                        Case "list"
                                                            '�T�[�o���Ŏ��s����ƃ��[�U�ɒʒm����Ȃ��̂ŁAwhite-list.txt��ǂ�Œʒm����
                                                            Dim strWhitelist As String = ""
                                                            '���O�t�@�C�����J��
                                                            Dim fsWL As New System.IO.FileStream(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), GSTR_WHITE_LIST), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                                                            Dim srWL As New System.IO.StreamReader(fsWL, System.Text.Encoding.Default)
                                                            While (srWL.Peek() >= 0)
                                                                strWhitelist = strWhitelist & srWL.ReadLine & ","
                                                            End While
                                                            srWL.Close()
                                                            srWL.Dispose()
                                                            fsWL.Close()
                                                            fsWL.Dispose()

                                                            strWhitelist = strWhitelist.Substring(0, strWhitelist.Length - 1)
                                                            If strWhitelist.Length > 0 Then
                                                                gsSendCommand("tell " & strPlayerID & " List Whitelist")
                                                                gsSendCommand("tell " & strPlayerID & " " & strWhitelist)
                                                            Else
                                                                gsSendCommand("tell " & strPlayerID & " Any User is not registered in the whitelist.")
                                                            End If
                                                        Case Else
                                                            gsSendCommand("tell " & strPlayerID & " whitelist comand usage : " & .PrefixChar & "whitelist <on/off/list/reload>")
                                                            gsSendCommand("tell " & strPlayerID & " whitelist comand usage : " & .PrefixChar & "whitelist <add/remove> <PlayerID>")
                                                    End Select
                                                Case intCommandOffset + 3
                                                    Select Case strSplitMsg(intCommandOffset + 1)
                                                        Case "add"
                                                            'white-list.txt�ɑ��݂��邩�`�F�b�N(����\��)

                                                            'whitelist add <PlayerID>
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                            gsSendCommand("tell " & strPlayerID & " " & strSplitMsg(intCommandOffset + 2) & " has been registered to the white list.")
                                                        Case "remove"
                                                            'white-list.txt�ɑ��݂��邩�`�F�b�N(����\��)

                                                            'whitelist remove <PlayerID>
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                            gsSendCommand("tell " & strPlayerID & " " & strSplitMsg(6) & " has been unregistered from the whitelist.")
                                                        Case Else
                                                            gsSendCommand("tell " & strPlayerID & " whitelist comand usage : @whitelist <on/off/list/reload>")
                                                            gsSendCommand("tell " & strPlayerID & " whitelist comand usage : @whitelist <add/remove> <PlayerID>")
                                                    End Select
                                                Case Else
                                                    gsSendCommand("tell " & strPlayerID & " whitelist comand usage : @whitelist <on/off/list/reload>")
                                                    gsSendCommand("tell " & strPlayerID & " whitelist comand usage : @whitelist <add/remove> <PlayerID>")
                                            End Select
                                        Case "spawnpoint"
                                            'strSplitMsg 4:Command 5:x(option) 6:y(option) 7:z(option)
                                            Select Case strSplitMsg.Length
                                                Case intCommandOffset + 1 'xyz���w�肳��Ȃ��ꍇ
                                                    'spawnpoint <PlayerID>
                                                    gsSendCommand("spawnpoint " & strPlayerID)
                                                    gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to current point.")
                                                Case intCommandOffset + 4 'xyz���w�肳��Ă���ꍇ
                                                    'spawnpoint <PlayerID> <x> <y> <z>
                                                    Select Case pfCheckXYZ(strSplitMsg(intCommandOffset + 1), strSplitMsg(intCommandOffset + 2), strSplitMsg(intCommandOffset + 3))
                                                        Case 0
                                                            '����Ȃ̂ŃR�}���h���s
                                                            gsSendCommand("spawnpoint " & strPlayerID & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                            gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to x:" & strSplitMsg(intCommandOffset + 1) & " y:" & strSplitMsg(intCommandOffset + 2) & " z:" & strSplitMsg(intCommandOffset + 3) & ".")
                                                        Case 1 'x�������Ŗ���
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " x point is not invalid.")
                                                        Case 2 'y�������Ŗ���
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " y point is not invalid.")
                                                        Case 3 'z�������Ŗ���
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " z point is not invalid.")
                                                        Case Else '�^�`�F�b�N���ɃG���[
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                    End Select
                                                Case Else
                                                    gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                            End Select
                                        Case "weather"
                                            'strSplitMsg 4:Command 5:Command Mode(clear/rain/thunder) 6:sec(option)
                                            Select Case strSplitMsg.Length
                                                Case intCommandOffset + 2
                                                    If strSplitMsg(5) = "clear" Or strSplitMsg(intCommandOffset + 1) = "rain" Or strSplitMsg(intCommandOffset + 1) = "thunder" Then
                                                        '�V�C���[�h��clear/rain/thunder�̎��̂ݎ��s��ʂ�
                                                        gsSendCommand("weather " & strSplitMsg(intCommandOffset + 1))
                                                        gsSendCommand("tell " & strPlayerID & " Changing to " & strSplitMsg(intCommandOffset + 1) & " weather")
                                                    Else
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    End If
                                                Case intCommandOffset + 3
                                                    Dim t As Integer = 0
                                                    If Integer.TryParse(strSplitMsg(intCommandOffset + 2), t) = False Then '�b�w�肪������
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    Else
                                                        If t >= 1 And t <= 1000000 Then '�b�w�肪�͈͓���
                                                            gsSendCommand("weather " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                            gsSendCommand("tell " & strPlayerID & " Changing to " & strSplitMsg(intCommandOffset + 1) & " weather only " & strSplitMsg(intCommandOffset + 2) & " sec.")
                                                        Else
                                                            gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                        End If
                                                    End If
                                                Case Else
                                                    gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                            End Select

                                        Case Else
                                            '�\�߃R�}���h�̑��ݔ����pfGetPermission�ōs���Ă�̂ł����ɂ͗��Ȃ�

                                    End Select

                                Case 0 '�p�[�~�b�V�����Ȃ�
                                    gsSendCommand("tell " & strPlayerID & " You don't have " & strCommand & " Command Permission.")
                                    pfWriteSystemLog(strPlayerID & " don't have " & strCommand & " Command Permission.", Color.Red)

                                Case -1 '�R�}���h�����݂��Ȃ�
                                    gsSendCommand("tell " & strPlayerID & " " & strCommand & " Command Not Found.")
                                    pfWriteSystemLog(strCommand & "Command Not Found.", Color.Red)
                            End Select
                        End If
                    Else
                        'Prefix Character�̎w��s��
                        pfWriteSystemLog("Prefix Character is Invalid.", Color.Red)
                    End If

                End If
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'x:y:z�����������W���`�F�b�N����
    Private Function pfCheckXYZ(x As String, y As String, z As String) As Integer
        Try
            Dim t As Integer = 0
            If Integer.TryParse(x, t) = False Then
                Return 1 'x�������l�Ŗ���
            End If
            If Integer.TryParse(y, t) = False Then
                Return 2 'y�������l�Ŗ���
            End If
            If Integer.TryParse(z, t) = False Then
                Return 3 'z�������l�Ŗ���
            End If
            Return 0 '�S�������l

        Catch ex As Exception
            Return -1 '�s���ȃG���[
        End Try

    End Function

    'SpawnPoint�̌��ʃ��O����A�Z�b�g���ꂽ���W���擾�A�ۑ�����
    Private Function pfPlayerSpawnPointCheck(ByVal msg As String) As Boolean
        '���[�U�[���R�}���h���s���������Aspawnpoint�R�}���h�����s��������
        '2013-01-13 15:10:56 [INFO] [miyabi9821: Set miyabi9821's spawn point to (-240, 63, -14)]
        '0          1        2      3            4   5            6     7     8   9     10  11

        '�T�[�o�R���\�[���������̃��[�U�[�ɑ΂��Aspawnpoint�����s�������ʁiMCSC��Permission�@�\�ł�������j
        '2013-06-09 05:23:21 [INFO] Set miyabi9821's spawn point to (-127, 71, 291)
        '0          1        2      3   4            5     6     7   8     9  10
        Try
            Dim strSplitMsg As String() = msg.Split(" ")
            If strSplitMsg.Length < 11 Or strSplitMsg.Length > 12 Then
                '�v�f����11��菬�������A12���傫���ꍇ�͊Y�����O�ł͖���
                Exit Function
            End If

            If strSplitMsg(2) <> "[INFO]" Then
                '3�߂̗v�f��[INFO]����Ȃ��Ȃ�Y�����O�ł͖���
                Exit Function
            End If

            'Set xxx's spawn point���b�Z�[�W�Ȃ�spawnpoint�̎��s���ʂƂ��č��W�擾�i������Ƃ���������j
            If strSplitMsg(4) = "Set" And strSplitMsg(6) = "spawn" And strSplitMsg(7) = "point" Then
                '���[�U�[��spawnpoint�R�}���h�����s�����ꍇ
                Dim strPlayerID() As String = strSplitMsg(5).Split("'")
                Dim strX As String = strSplitMsg(9).Substring(1, strSplitMsg(9).Length - 2)
                Dim strY As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 1)
                Dim strZ As String = strSplitMsg(11).Substring(0, strSplitMsg(11).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    '�擾���悤�Ƃ���XYZ���W���ُ�
                    pfWriteSystemLog("Cannot get spawnpoint. PlayerID:" & strPlayerID(0) & " x:" & strX & " y:" & strY & " z:" & strZ, Color.Red)
                Else
                    If pfUpdatePlayerSpawnPoint(strPlayerID(0), strX & "," & strY & "," & strZ) = False Then
                        pfWriteSystemLog("Player Information(spawn point) Update Failure.", Color.Red)
                    End If
                End If

            ElseIf strSplitMsg(3) = "Set" And strSplitMsg(5) = "spawn" And strSplitMsg(6) = "point" Then
                '�T�[�o�R���\�[�����烆�[�U�[�w���spawnpoint�R�}���h�����s�����ꍇ
                Dim strPlayerID() As String = strSplitMsg(4).Split("'")
                Dim strX As String = strSplitMsg(8).Substring(1, strSplitMsg(8).Length - 2)
                Dim strY As String = strSplitMsg(9).Substring(0, strSplitMsg(9).Length - 1)
                Dim strZ As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    '�擾���悤�Ƃ���XYZ���W���ُ�
                    pfWriteSystemLog("Cannot get spawnpoint. PlayerID:" & strPlayerID(0) & " x:" & strX & " y:" & strY & " z:" & strZ, Color.Red)
                Else
                    If pfUpdatePlayerSpawnPoint(strPlayerID(0), strX & "," & strY & "," & strZ) = False Then
                        pfWriteSystemLog("Player Information(spawn point) Update Failure.", Color.Red)
                    End If
                End If

            End If

            Return True
        Catch ex As Exception
            pfWriteSystemLog("System Error in pfPlayerSpawnPointCheck.", Color.Red)
            Return False
        End Try

    End Function

    '�R�}���h��PlayerID���w�肷��ƁA�p�[�~�b�V�����������Ă��邩�𔻒肷��
    '�߂�l�@1:�p�[�~�b�V��������A0:�p�[�~�b�V���������A-1:�R�}���h�����݂��Ȃ�
    Private Function pfGetPermission(ByVal cmd As String, ByVal id As String) As Integer
        With PermissionSettings.Instance
            Select Case cmd
                Case "tp"
                    If .TpEnabled = True Then 'Permission�L��
                        If .TpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "give"
                    If .GiveEnabled = True Then 'Permission�L��
                        If .GiveMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GiveSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "time"
                    If .TimeEnabled = True Then 'Permission�L��
                        If .TimeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TimeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "xp"
                    If .XpEnabled = True Then 'Permission�L��
                        If .XpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .XpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "gamemode"
                    If .GamemodeEnabled = True Then 'Permission�L��
                        If .GamemodeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GamemodeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "kick"
                    If .KickEnabled = True Then 'Permission�L��
                        If .KickMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .KickSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "ban"
                    If .BanEnabled = True Then 'Permission�L��
                        If .BanMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .BanSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "pardon"
                    If .PardonEnabled = True Then 'Permission�L��
                        If .PardonMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .PardonSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "whitelist"
                    If .WhitelistEnabled = True Then 'Permission�L��
                        If .WhitelistMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WhitelistSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "spawnpoint"
                    If .SpawnpointEnabled = True Then 'Permission�L��
                        If .SpawnpointMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .SpawnpointSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case "weather"
                    If .WeatherEnabled = True Then 'Permission�L��
                        If .WeatherMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WeatherSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '�w�胆�[�U�[�ɑ��݂���
                                End If
                            Next
                            Return 0 '�w�胆�[�U�[�ɑ��݂��Ȃ�
                        End If
                    Else 'Permission����
                        Return 0
                    End If

                Case Else
                    Return -1
            End Select

        End With
    End Function

    '�v���C���[���I�����C�����`�F�b�N
    Private Function pfIsPlayerOnline(ByVal id As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                '�v���C���[�ꗗ�̒��Ƀv���C���[�𔭌�
                If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    Return True '�I�����C�����
                Else
                    Return False '�I�t���C�����
                End If
            End If
        Next

        Return False '�v���C���[�ꗗ�ɑ��݂��Ȃ�(���O�C���������Ƃ��Ȃ�)
    End Function

    'HeartBeat���O���o�͂��Ȃ�
    '1.6����end of stream�̏o�͂��o�邽�߁A������}������
    '2012-07-27 20:29:49 [INFO] /127.0.0.1:xxxxx lost connection
    '2013-07-28 08:37:28 [SEVERE] Reached end of stream for /127.0.0.1
    Private Function CheckHeartBeatLog(ByVal msg As String) As Boolean
        If Settings.Instance.ServerVersion >= 3 Then
            '1.7�ȍ~�͂��̃��O���o�Ȃ��悤�Ȃ̂ő�����
            Return False
        End If

        Dim strSplitMsg As String() = msg.Split(" ")
        Dim strSplit() As String = strSplitMsg(3).Replace("/", "").Split(":")
        If strSplitMsg(2) = "[INFO]" AndAlso strSplit(0) = "127.0.0.1" AndAlso strSplitMsg(4) = "lost" AndAlso strSplitMsg(5) = "connection" Then
            Return True
        ElseIf strSplitMsg(2) = "[SEVERE]" AndAlso msg.IndexOf("Reached end of stream for /127.0.0.1") >= 0 Then
            Return True
        End If
        Return False
    End Function

    '�E�B���h�E�ʒu��ۑ�
    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        Settings.Instance.WindowPos = Me.Location
    End Sub


    '�E�B���h�E�T�C�Y�ύX
    Private Sub frmMain_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        '�I�u�W�F�N�g�T�C�Y�ύX
        psObjectSizeChange()

        '�E�B���h�E�T�C�Y�̕ۑ�
        If Me.WindowState = FormWindowState.Normal Then
            '�m�[�}���̎��̂ݕۑ��i�ő剻�A�ŏ����̎��͕ۑ����Ȃ��j
            Settings.Instance.WindowSize = New Point(Me.Width, Me.Height)
        End If

    End Sub

    '�E�B���h�E�T�C�Y���ύX���ꂽ�Ƃ��ȂǁA��ʃI�u�W�F�N�g�̃T�C�Y��ύX����
    Private Sub psObjectSizeChange()
        '�E�B���h�E�T�C�Y�̊�{��800x600�Ƃ���
        Dim AddWidth As Integer = Me.Width - 800
        Dim AddHeight As Integer = Me.Height - 600

        '�c�|�W�V�����ύX
        gbSVController.Top = 420 + AddHeight

        If ExtendPlayersListAreaToolStripMenuItem.Checked = True Then
            'Extend Players List Area�Ƀ`�F�b�N�������Ă���A�v���C���[�ꗗ�̉�����傫�����

            '���T�C�Y�ύX
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297 + 457 + AddWidth
            lvPlayers.Width = 285 + 457 + AddWidth

            '�c�T�C�Y�ύX
            gbLog.Height = 385 - 229
            rtbServerLog.Height = 361 - 229
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayers�̃J������
            lvPlayers.Columns(0).Width = 150     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 50     'Online
            lvPlayers.Columns(3).Width = 120    'LastLogin
            lvPlayers.Columns(4).Width = 120    'LastLogout
            lvPlayers.Columns(5).Width = 80     'Spawn
            lvPlayers.Columns(6).Width = 80     'BadCount

        Else
            '�]���ʂ�

            '���T�C�Y�ύX
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297
            lvPlayers.Width = 285

            '�c�T�C�Y�ύX
            gbLog.Height = 385 + AddHeight
            rtbServerLog.Height = 361 + AddHeight
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayers�̃J������
            lvPlayers.Columns(0).Width = 100     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 30     'Online
            lvPlayers.Columns(3).Width = 5      'LastLogin
            lvPlayers.Columns(4).Width = 5      'LastLogout
            lvPlayers.Columns(5).Width = 5      'Spawn
            lvPlayers.Columns(6).Width = 5      'BadCount

        End If

        '�T�[�o���O�̃J�[�\���ʒu���Ō�Ɉړ�
        rtbServerLog.SelectionStart = rtbServerLog.Text.Length
        rtbServerLog.Focus()
        rtbServerLog.ScrollToCaret()

    End Sub

    '�v���Z�X�̋����I��
    Private Sub btnKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKill.Click
        Try
            If MessageBox.Show("�T�[�o�������I�������܂��B" & vbCrLf & _
                               "�f�[�^���j������\��������A�ʏ�̒�~�����Ŏ~�܂�Ȃ��Ƃ��̂ݎ��s���ĉ������B" & vbCrLf & _
                               "���s���܂����H", "�x��", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            pfWriteSystemLog("Minecraft Server Process Kill Execute.", Color.Red)
            gblnExitFlg = True '�I���t���O��ݒ�
            mcsProc.Kill()
        Catch ex As Exception

        End Try

    End Sub

    '���[�U�[�̏ڍׂ�\������
    Private Sub PropertyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertyToolStripMenuItem.Click
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        My.Forms.frmPlayerInfo.txtSelectedIndex.Text = idx.ToString
        My.Forms.frmPlayerInfo.txtID.Text = lvPlayers.Items(idx).SubItems(0).Text
        My.Forms.frmPlayerInfo.txtIP.Text = lvPlayers.Items(idx).SubItems(1).Text
        My.Forms.frmPlayerInfo.txtStatus.Text = lvPlayers.Items(idx).SubItems(2).Text
        My.Forms.frmPlayerInfo.txtLastLogin.Text = lvPlayers.Items(idx).SubItems(3).Text
        My.Forms.frmPlayerInfo.txtLastLogout.Text = lvPlayers.Items(idx).SubItems(4).Text
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint�ǉ�
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint�ǉ��ɂ�����Ɉړ�

        My.Forms.frmPlayerInfo.Show()
    End Sub

    '���[�U�[�������I�ɐؒf����R�}���h�𔭍s����
    Private Sub KickPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        'kick [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("kick " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    '���[�U���z���C�g���X�g�ɒǉ�����R�}���h�𔭍s����
    Private Sub AddWhiteListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWhiteListToolStripMenuItem.Click
        'whitelist add [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("whitelist add " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    '���[�U�[��Player BAN���X�g�ɒǉ�����R�}���h�𔭍s����
    Private Sub AddPlayerBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPlayerBANListToolStripMenuItem.Click
        'ban [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("ban " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    '���[�U�[��IP��IP BAN���X�g�ɒǉ�����R�}���h�𔭍s����
    Private Sub AddIPBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddIPBANListToolStripMenuItem.Click
        'ban-ip [IP]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("ban-ip " & lvPlayers.Items(idx).SubItems(1).Text)
    End Sub

    Private Sub DeleteWhiteListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteWhiteListToolStripMenuItem.Click
        'whitelist remove [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("whitelist remove " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    Private Sub DeletePlayerBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePlayerBANListToolStripMenuItem.Click
        'pardon [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("pardon " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    Private Sub DeleteIPBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteIPBANListToolStripMenuItem.Click
        'pardon-ip [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("pardon-ip " & lvPlayers.Items(idx).SubItems(1).Text)
    End Sub

    'IP�p�R���e�L�X�g���j���[���J�����Ƃ�
    Private Sub cmsIP_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsIP.Opening
        Dim menu As ContextMenuStrip = CType(sender, ContextMenuStrip)
        pstrCmsIPCalledObject = menu.SourceControl.Name
    End Sub

    Private Sub CopyToClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToClipboardToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                '�O���[�o��IP�A�h���X���N���b�v�{�[�h�ɃR�s�[
                System.Windows.Forms.Clipboard.SetText(lblGlobalIP.Text)
            Case Me.lblPrivateIP.Name
                '�v���C�x�[�gIP�A�h���X���N���b�v�{�[�h�ɃR�s�[
                System.Windows.Forms.Clipboard.SetText(lblPrivateIP.Text)
        End Select

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                '�O���[�o��IP�A�h���X�Ď擾
                lblGlobalIP.Text = gfGetGlobalIP()
            Case Me.lblPrivateIP.Name
                '�v���C�x�[�gIP�A�h���X�Ď擾
                lblPrivateIP.Text = gfGetPrivateIP()
        End Select
    End Sub


    Private Sub cmsPlayers_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsPlayers.Opening
        '�I������Ă��Ȃ�������L�����Z��
        If lvPlayers.SelectedItems.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If btnRun.Enabled = True Then
            '�T�[�o���N������Ă��Ȃ��ꍇ�A�ڍ׈ȊO�g�p�s��
            KickPlayerToolStripMenuItem.Enabled = False
            AddWhiteListToolStripMenuItem.Enabled = False
            AddPlayerBANListToolStripMenuItem.Enabled = False
            AddIPBANListToolStripMenuItem.Enabled = False
            DeleteWhiteListToolStripMenuItem.Enabled = False
            DeletePlayerBANListToolStripMenuItem.Enabled = False
            DeleteIPBANListToolStripMenuItem.Enabled = False
            DeletePlayerBANListToolStripMenuItem.Enabled = False
        Else
            KickPlayerToolStripMenuItem.Enabled = True
            AddWhiteListToolStripMenuItem.Enabled = True
            AddPlayerBANListToolStripMenuItem.Enabled = True
            AddIPBANListToolStripMenuItem.Enabled = True
            DeleteWhiteListToolStripMenuItem.Enabled = True
            DeletePlayerBANListToolStripMenuItem.Enabled = True
            DeleteIPBANListToolStripMenuItem.Enabled = True
            DeletePlayerBANListToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub SchedulerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchedulerToolStripMenuItem.Click
        '�X�P�W���[���N��
        frmScheduler.Show()
    End Sub

    '�v���C���[�ꗗ�̕��ёւ�
    '�Q�l�Fhttp://natchan-develop.seesaa.net/article/141920783.html
    Private Sub lvPlayers_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvPlayers.ColumnClick
        Static sIntColNo(lvPlayers.Columns.Count - 1) As Integer  '��̃\�[�g��ԕێ��p
        If sIntColNo(e.Column) = 0 Then
            '����܂��͏���
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 0)
            sIntColNo(e.Column) = 1    '����͍~��
        Else
            '�~��
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 1)
            sIntColNo(e.Column) = 0    '����͏���
        End If

    End Sub

    '�_�u���N���b�N�őI���v���C���[�̏ڍו\��
    Private Sub lvPlayers_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvPlayers.DoubleClick
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        My.Forms.frmPlayerInfo.txtSelectedIndex.Text = idx.ToString
        My.Forms.frmPlayerInfo.txtID.Text = lvPlayers.Items(idx).SubItems(0).Text
        My.Forms.frmPlayerInfo.txtIP.Text = lvPlayers.Items(idx).SubItems(1).Text
        My.Forms.frmPlayerInfo.txtStatus.Text = lvPlayers.Items(idx).SubItems(2).Text
        My.Forms.frmPlayerInfo.txtLastLogin.Text = lvPlayers.Items(idx).SubItems(3).Text
        My.Forms.frmPlayerInfo.txtLastLogout.Text = lvPlayers.Items(idx).SubItems(4).Text
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint�ǉ�
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint�ǉ��ɂ�����Ɉړ�

        My.Forms.frmPlayerInfo.Show()
        My.Forms.frmPlayerInfo.Activate()
    End Sub


    'lvPlayers�̃f�[�^��ۑ��A�Ǎ�����
    '�Q�l�Fhttp://www.knowdotnet.com/articles/serializationoflistviewtoxml.html
    Public Sub SaveObjectProperties()
        Dim ds As New DataSet
        Dim i As Integer

        Try
            ' create a datatable in a dataset.  the dataset will 
            ' automatically create the xml
            Dim dt As DataTable = ds.Tables.Add("UserList")

            For i = 0 To Me.lvPlayers.Columns.Count - 1
                dt.Columns.Add("Col" & i.ToString, Type.GetType("System.String"))
            Next

            For i = 0 To Me.lvPlayers.Items.Count - 1
                With Me.lvPlayers.Items(i)
                    '�I������̂ŃI�����C���X�e�[�^�X���~�ɕύX����
                    If .SubItems(2).Text = GSTR_ONLINE_TRUE Then
                        .SubItems(2).Text = GSTR_ONLINE_FALSE
                    End If

                    Dim o() As Object = {.SubItems(0).Text, _
                                       .SubItems(1).Text, _
                                       .SubItems(2).Text, _
                                       .SubItems(3).Text, _
                                       .SubItems(4).Text, _
                                       .SubItems(5).Text, _
                                       .SubItems(6).Text}
                    AddRowToTable(dt, o)
                End With
            Next i
            ds.WriteXml(System.IO.Path.Combine(gfGetConfigDir, GSTR_PLAYERLIST_FILE))
        Catch ex As System.Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function AddRowToTable(ByRef dt As DataTable, _
       ByVal ParamArray DRows() As Object) As Boolean
        Dim i As Short
        Try
            Dim newRow As DataRow = dt.NewRow
            For i = 0 To UBound(DRows)
                ' add a row to the passed dtList
                newRow(i) = DRows(i)
            Next
            dt.Rows.Add(newRow)
            Return True
        Catch ex As System.Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function

    Public Sub ReloadListviewFromXML()
        Dim ds As New DataSet

        Try
            ds.ReadXml(System.IO.Path.Combine(gfGetConfigDir, GSTR_PLAYERLIST_FILE))
            Dim dt As DataTable = ds.Tables("UserList")
            Dim i As Integer
            Dim j As Integer
            Me.lvPlayers.Items.Clear()

            For i = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                With Me.lvPlayers
                    .Items.Add(dr(0))
                    For j = 1 To dt.Columns.Count - 1
                        .Items(i).SubItems.Add(dr(j))
                    Next
                End With
            Next
        Catch ex As System.Exception
            Throw
        End Try
    End Sub


    Private Sub BackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupToolStripMenuItem.Click
        frmBackup.ShowDialog()
        pfBackupTimerSet()
    End Sub

    Private Function pfBackupTimerSet() As Boolean
        Try
            If Settings.Instance.BackupEnabled = True Then
                '���͂����Ȃ̂ŁA�~���b�w��Őݒ�
                timBackup.Interval = Settings.Instance.BackupInterval * 60 * 1000

                '�T�[�o�N�����̂݃^�C�}�[��L���ɂ���
                If btnRun.Enabled = False Then
                    timBackup.Start()
                    lblDataBackup.Text = "Enabled"
                    lblNextTime.Text = DateAdd(DateInterval.Minute, _
                                            Settings.Instance.BackupInterval, _
                                            DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    timBackup.Stop()
                    lblDataBackup.Text = "Enabled (stop)"
                    lblNextTime.Text = "-"
                End If
            Else
                timBackup.Stop()
                lblDataBackup.Text = "Disabled"
                lblNextTime.Text = "-"
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    '�^�C�}�[�ɂ�����o�b�N�A�b�v�����s
    Private Sub timBackup_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBackup.Tick
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
            Exit Sub
        End If

        pfWriteSystemLog("Auto-Backup Started.", Color.Black)

        '�o�b�N�A�b�v���s
        Dim strRetMsg As String = "" '�o�b�N�A�b�v��������߂���郁�b�Z�[�W
        If gfBackup(strRetMsg) = True Then
            pfWriteSystemLog(strRetMsg, Color.Blue)
        Else
            pfWriteSystemLog(strRetMsg, Color.Red)
        End If

        '����o�b�N�A�b�v���Ԃ̕\���X�V
        lblNextTime.Text = DateAdd(DateInterval.Minute, _
                        Settings.Instance.BackupInterval, _
                        DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")

    End Sub

    '�蓮�o�b�N�A�b�v
    Private Sub DataBackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataBackupToolStripMenuItem.Click
        '�o�b�N�A�b�v�̐ݒ�m�F����
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            MessageBox.Show("�o�b�N�A�b�v�Ώۃt�H���_���\������Ă��܂���B" & vbCrLf & "�ݒ���m�F���ĉ������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If System.IO.Directory.Exists(Settings.Instance.BackupOutput) = False Then
            MessageBox.Show("�w�肳�ꂽ�o�͐悪���݂��܂���B" & vbCrLf & "�ݒ���m�F���ĉ������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If

        '���s�m�F
        If MessageBox.Show("�蓮�o�b�N�A�b�v�����s���܂����H", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        pfWriteSystemLog("Manual-Backup Started.", Color.Black)

        '�o�b�N�A�b�v���s
        Dim strRetMsg As String = ""
        If gfBackup(strRetMsg, True) = False Then
            pfWriteSystemLog(strRetMsg, Color.Red)
            Exit Sub
        End If

        pfWriteSystemLog(strRetMsg, Color.Blue)

    End Sub

    '�T�[�o�|�[�g�ɑ΂��AHeartBeat�m�F���s��
    Private Sub timHB_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timHB.Tick
        '�|�[�g�ԍ����i�[�����ϐ�
        Static intErrCnt As Integer

        If pstrPort = "" Then
            'server.properties�̓Ǎ��m�F
            If ghtServerProperties Is Nothing Then
                If gfLoadServerProp() = False Then
                    pfWriteSystemLog("Cannot Read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
                    Exit Sub
                End If
            End If

            '�|�[�g�ԍ��擾
            If gfGetServerPropValue("server-port", pstrPort) = False Then
                pfWriteSystemLog("Server port number unknown.", Color.Red)
                Exit Sub
            End If
        End If

        'Socket�ڑ�
        Dim IPEPLocal As New System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), CInt(pstrPort))
        Dim sockLocal As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
        Try
            sockLocal.ReceiveTimeout = 5000 '�ő�5�b
            sockLocal.Connect(IPEPLocal)
            If sockLocal.Connected Then
                If Settings.Instance.HeartBeatUse0xFE = True Then
                    '2012/10/28 �ڑ��l���Ȃǂ��擾����悤�ύX
                    '2012/11/03 2�b�قǃA�v�����ł܂�̂ŁA�I�v�V�����őI�ׂ�悤�ɕύX

                    '���M(0xFE)
                    Dim btSendMsg(1) As Byte
                    btSendMsg(0) = &HFE
                    Try
                        sockLocal.Send(btSendMsg, btSendMsg.Length, Net.Sockets.SocketFlags.None)

                        '��M
                        Dim resBytes(1023) As Byte
                        Dim mem As New System.IO.MemoryStream
                        While True
                            Dim resSize As Integer = _
                                sockLocal.Receive(resBytes, resBytes.Length, _
                                    System.Net.Sockets.SocketFlags.None)
                            If resSize = 0 Then
                                Exit While
                            End If
                            mem.Write(resBytes, 0, resSize)
                        End While

                        '2�o�C�g�ڈȍ~��؂�o���AUTF-16BE�ɕϊ�
                        Dim resMsg As String = _
                            System.Text.Encoding.GetEncoding("utf-16be").GetString(mem.GetBuffer(), 3, CInt(mem.Length) - 3)
                        mem.Close()

                        '0xA7�ŕ�������imotd�A�ڑ��v���C���[���A�ő�v���C���[���j
                        Dim resInfo As String() = resMsg.Split("��")
                        lblConnected.Text = resInfo(1) & " / " & resInfo(2)

                    Catch ex As Exception
                        pfWriteSystemLog("HeartBeat(0xFE) Failure. Error Count:" & intErrCnt.ToString, Color.Red)
                        lblConnected.Text = "Get Error."

                    End Try

                End If

                '����ɐڑ��o�����̂ŃG���[�J�E���g��0�ɂ��ă\�P�b�g�����
                intErrCnt = 0
                sockLocal.Close()
            Else
                intErrCnt = intErrCnt + 1
                pfWriteSystemLog("HeartBeat Failure. Error Count:" & intErrCnt.ToString, Color.Red)
            End If
        Catch ex As Exception
            pfWriteSystemLog("HeartBeat Failure. Error Count:" & intErrCnt.ToString, Color.Red)
            intErrCnt = intErrCnt + 1
        End Try

        '�I�[�g���J�o�������Ȃ�I���
        If Settings.Instance.AutoRecovery = False Then
            Exit Sub
        End If

        '�K��̃G���[臒l�𒴂�����
        If intErrCnt >= Settings.Instance.HeartBeatStopCount And intErrCnt < Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Auto-Recovery Start.", Color.Red)
            '�T�[�o�ċN������
            gsSendCommand("stop")
        ElseIf intErrCnt >= Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Force Auto-Recovery Start.", Color.Red)
            '�T�[�o�v���Z�X�����I��
            mcsProc.Kill()
        End If

    End Sub

    '���[���ʒm�ݒ��ʂ��J��
    Private Sub SendMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMailToolStripMenuItem.Click
        frmMail.Show()
    End Sub

    '�w�肳�ꂽ�v���C���[��BadCount������������
    Public Function gfRewriteBadCount(ByVal idx As Integer, ByVal newcnt As String) As Boolean
        lvPlayers.Items(idx).SubItems(6).Text = newcnt
    End Function

    Private Sub DeletePlayerFromListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePlayerFromListToolStripMenuItem.Click
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index
        lvPlayers.Items.RemoveAt(idx)
    End Sub

    'list�R�}���h�𔭍s���A�v���C���[�ꗗ�̃X�e�[�^�X���X�V����
    Private Sub UpdatePlayerListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdatePlayerListToolStripMenuItem.Click
        gsSendCommand("list")
    End Sub

    '�ŐV�o�[�W�������m�F���A���b�Z�[�W���o�͂���
    Private Sub CheckLatestVersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckLatestVersionToolStripMenuItem.Click
        '�o�[�W�����`�F�b�N
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black

        Dim blnRet As Boolean = gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)
        If blnRet = True Then
            If MessageBox.Show("���ݎg�p���̃o�[�W�������V�����o�[�W�����������[�X����Ă��܂��B" & vbCrLf & "Web�T�C�g��\�����m�F���܂����H", _
                                "�V�o�[�W�������o", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '����̃u���E�U��Web�T�C�g���J��
            System.Diagnostics.Process.Start("http://minecraftjp.info/MCSC/")
        End If
    End Sub

    Private Sub IPBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IPBANListToolStripMenuItem.Click

    End Sub

    Private Sub IPKickBANToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IPKickBANToolStripMenuItem.Click
        frmIPKickBAN.Show()
    End Sub

    Private Sub PermissionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PermissionToolStripMenuItem.Click
        frmPermission.Show()
    End Sub

    Private Sub ExtendPlayersLitAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtendPlayersListAreaToolStripMenuItem.Click
        If ExtendPlayersListAreaToolStripMenuItem.Checked = False Then
            '�`�F�b�N��t���āA�v���C���[�ꗗ�\���G���A���g������
            ExtendPlayersListAreaToolStripMenuItem.Checked = True
            psObjectSizeChange()
        Else
            '�`�F�b�N���O���āA�f�t�H���g��Ԃɖ߂�
            ExtendPlayersListAreaToolStripMenuItem.Checked = False
            psObjectSizeChange()
        End If

    End Sub

    Private Sub ClearServerLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearServerLogToolStripMenuItem.Click
        rtbServerLog.Clear()
    End Sub

    Private Sub ServerpropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServerpropertiesToolStripMenuItem.Click
        frmPropEditor.Show()
    End Sub

    Private Sub MCBansToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCBansToolStripMenuItem.Click
        frmMCBans.Show()
    End Sub

    '�v���C���[�̃X�|�[���|�C���g�\�����X�V
    Private Function pfUpdatePlayerSpawnPoint(ByVal id As String, xyz As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                '�v���C���[�ꗗ�̒��Ƀv���C���[�𔭌�
                lvPlayers.Items(iList).SubItems(5).Text = xyz
                Return True 'Spawn Point���X�V���ďI��
            End If
        Next

        Return False '�v���C���[�ꗗ�ɑ��݂��Ȃ��i���肦�Ȃ��H�j
    End Function

    '�v���C���[�ꗗ�̃N���A
    Private Sub ClearPlayerListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearPlayerListToolStripMenuItem.Click
        If MessageBox.Show("Clear Player List?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        '�N���A����
        lvPlayers.Items.Clear()
    End Sub


    '*** frmChatLog�Ƀ`���b�g���O�𑗐M ***
    Private Function ExpertChatLog(ByVal msg As String) As Boolean
        Try
            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4�ȑO

                Dim strSplitMsg As String() = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '�v�f����4�ȉ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3�߂̗v�f��[INFO]����Ȃ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    '4�߂̗v�f��(\[.*?\])?<.*?>����Ȃ��Ȃ�`���b�g����Ȃ�
                    If strSplitMsg(3).Contains("[Server]") = True Then
                        '[Server]���܂ނȂ�T�[�o�[����̃`���b�g
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* �Ȃ烍�O�C�����b�Z�[�W
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " ���񂪃��O�C�����܂����B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* �Ȃ烍�O�A�E�g���b�Z�[�W
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " ���񂪃��O�A�E�g���܂����B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) �Ȃ�R�}���h�g�p���O
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 ���� $3 �R�}���h���g�p���܂����B") & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    Exit Function
                End If

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7
                Dim strSplitMsg As String() = msg.Split(" ")

                '�ʓ|�ȃX�y�[�X��u������
                Dim strEditMsg As String = msg
                If Settings.Instance.ServerVersion >= 3 Then
                    If strEditMsg.IndexOf("[Server thread/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[Server thread/INFO]:", "[Server%20thread/INFO]:")
                    End If
                    If strEditMsg.IndexOf("[User Authenticator #1/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[User Authenticator #1/INFO]:", "[User%20Authenticator%20#1/INFO]:")
                    End If
                    If strEditMsg.IndexOf("[Server Shutdown Thread/INFO]:") >= 0 Then
                        strEditMsg = strEditMsg.Replace("[Server Shutdown Thread/INFO]:", "[Server%20Shutdown%20Thread/INFO]:")
                    End If
                End If

                strSplitMsg = strEditMsg.Split(" ")

                '�u�������X�y�[�X��߂�
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '�v�f����3�ȉ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2�߂̗v�f��[Server thread/INFO]:����Ȃ��Ȃ�`���b�g����Ȃ�
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (\[.+?\])?(<.+?>) (.*)") Then
                    '4�߂̗v�f��(\[.*?\])?<.*?>����Ȃ��Ȃ�`���b�g����Ȃ�
                    If strSplitMsg(2).Contains("[Server]") = True Then
                        '[Server]���܂ނȂ�T�[�o�[����̃`���b�g
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* �Ȃ烍�O�C�����b�Z�[�W
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " ���񂪃��O�C�����܂����B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* �Ȃ烍�O�A�E�g���b�Z�[�W
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " ���񂪃��O�A�E�g���܂����B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) �Ȃ�R�}���h�g�p���O
                        '�`���b�g���O�ɏo��
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 ���� $3 �R�}���h���g�p���܂����B") & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If

                    Exit Function
                End If
            End If

            Return True
        Catch ex As Exception
            pfWriteSystemLog(ex.Message, Color.Red)
            Return False
        End Try
    End Function

    Private Sub ChatLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChatLogToolStripMenuItem.Click
        frmChatlog.Show()
    End Sub
End Class
