Public Class frmMain
    Private pstrCmsIPCalledObject As String 'cmsIP‚ğŒÄ‚Ño‚µ‚½ƒIƒuƒWƒFƒNƒg–¼
    Private pstrPort As String = "" '“Ç‚İ‚ñ‚¾İ’èƒtƒ@ƒCƒ‹‚Ìƒ|[ƒg”Ô†
    Public chatting As Boolean = False 'ƒ`ƒƒƒbƒgƒƒOƒEƒBƒ“ƒhƒE•\¦’†‚©‚Ç‚¤‚©
#Region "’è”"
    'WM_QUERYENDSESSIONƒƒbƒZ[ƒW
    Private Const WM_QUERYENDSESSION As Integer = &H11

    'WM_POWERBROADCASTƒƒbƒZ[ƒW
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

#Region "ƒI[ƒo[ƒ‰ƒCƒhƒƒ\ƒbƒh"
    Protected Overrides Sub WndProc( _
     ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_QUERYENDSESSION Then
            'WM_QUERYENDSESSION(ƒVƒƒƒbƒgƒ_ƒEƒ“)
            pfWriteSystemLog("Shutdown Event detected.", Color.Blue)

            If btnRun.Enabled = False Then
                Try
                    gblnExitFlg = True '³íI—¹ƒtƒ‰ƒO
                    gsSendCommand("stop")
                Catch ex As Exception
                    pfWriteSystemLog("Command Send Error.", Color.Red)
                End Try
            End If

        ElseIf m.Msg = WM_POWERBROADCAST Then
            'WM_POWERBROADCAST(ƒpƒ[ó‘Ô•ÏX)
            pfWriteSystemLog("PowerMode Change Event detected.", Color.Blue)

            If m.WParam.ToInt32 = PBT_APMSUSPEND Or m.WParam.ToInt32 = PBT_APMSTANDBY Then
                'ƒTƒXƒyƒ“ƒhA‹x~ó‘Ô‚É“ü‚éê‡‚ÍƒT[ƒo’â~
                If btnRun.Enabled = False Then
                    Try
                        gblnExitFlg = True '³íI—¹ƒtƒ‰ƒO
                        gblnResumeFlg = True '•œ‹Œƒtƒ‰ƒO
                        gsSendCommand("stop")
                    Catch ex As Exception
                        pfWriteSystemLog("Command Send Error.", Color.Red)
                    End Try
                End If

            ElseIf m.WParam.ToInt32 = PBT_APMRESUMESUSPEND Or m.WParam.ToInt32 = PBT_APMRESUMESTANDBY Then
                'ƒTƒXƒyƒ“ƒhA‹x~ó‘Ô‚©‚ç•œ‹Œ‚µ‚½ê‡‚ÍA’â~‘O‚ÉƒT[ƒo‚ª‹N“®‚µ‚Ä‚½ê‡‚ÉŒÀ‚è‹N“®
                If gblnResumeFlg = True Then
                    If pfServerStart() = True Then
                        pfBackupTimerSet() '³í‚É‹N“®o—ˆ‚½‚çAƒoƒbƒNƒAƒbƒvƒ^ƒCƒ}[‚Ìó‘Ô‚ğİ’è
                        If Settings.Instance.HeartBeatInterval >= 1 Then
                            'HBŠÔŠu‚ª1ˆÈã‚È‚çHB—LŒø
                            timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                            timHB.Start() 'HBŠJn
                        End If
                    End If
                    gblnResumeFlg = False '•œ‹Œƒtƒ‰ƒO‚ğŒ³‚É–ß‚·
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
            'Startƒ{ƒ^ƒ“‚Ìó‘Ô
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("ƒT[ƒoİ’è‚Ìƒtƒ@ƒCƒ‹w’è‚ª³‚µ‚­‚ ‚è‚Ü‚¹‚ñB" & vbCrLf & "İ’è‰æ–Ê‚ğŠJ‚«AƒpƒX‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "’ˆÓ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("ƒT[ƒoİ’è‚Ìƒtƒ@ƒCƒ‹w’è‚ª³‚µ‚­‚ ‚è‚Ü‚¹‚ñB" & vbCrLf & "İ’è‰æ–Ê‚ğŠJ‚«AƒpƒX‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "’ˆÓ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    If lblServer.Text <> "Running" Then
                        btnRun.Enabled = True
                    End If
                End If
            End If

            'Connected•\¦‚Ìó‘Ô
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

    End Sub

    Private Sub NGWordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NGWordsToolStripMenuItem.Click
        'NGƒ[ƒhİ’è‰æ–Ê‚ğŠJ‚­
        frmNGWords.Show()
    End Sub

    Private Sub KickListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        '©“®ƒLƒbƒNİ’è‰æ–Ê‚ğŠJ‚­
        frmIPKickBAN.Show()
    End Sub

    'I—¹ˆ—
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If mcsProc.HasExited = False Then
                MessageBox.Show("I—¹‚·‚éê‡‚Íæ‚ÉƒT[ƒo‚ğ’â~‚µ‚Ä‰º‚³‚¢", "I—¹‚Å‚«‚Ü‚¹‚ñ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        Try
            'Å‘å‰»ó‘Ô‚ÌŠm’è
            If Me.WindowState = FormWindowState.Maximized Then
                Settings.Instance.WindowMaximize = True
            Else
                Settings.Instance.WindowMaximize = False
            End If

            'Extended Players List Area‚Ì—LŒøó‘Ô
            Settings.Instance.ExtendedPlayersListEnabled = ExtendPlayersListAreaToolStripMenuItem.Checked

            'ƒRƒ}ƒ“ƒhƒŠƒXƒg‚Ì•Û‘¶
            Settings.Instance.CommandRecent = New List(Of String)
            For i As Integer = 0 To cmbCommand.Items.Count - 1
                Settings.Instance.CommandRecent.Add(cmbCommand.Items(i))
            Next

            'İ’è‚Ì•Û‘¶
            Settings.SaveToXmlFile()

            'ƒvƒŒƒCƒ„[ˆê——‚Ì•Û‘¶
            SaveObjectProperties()
        Catch ex As Exception
            MessageBox.Show("İ’èƒtƒ@ƒCƒ‹‚Ì•Û‘¶‚É¸”s‚µ‚Ü‚µ‚½", "ƒGƒ‰[", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '‹Œİ’èƒtƒ@ƒCƒ‹‚ÌƒŠƒJƒoƒŠˆ’u(1.0‚ ‚½‚è‚Åíœ—\’è)
        gfMoveOldConfig()

        'İ’è“Ç‚İ‚İ
        Try
            Settings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'server.properties“Ç
        If gfLoadServerProp() = False Then
            pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
        End If

        'ƒvƒŒƒCƒ„[ˆê——“Ç
        Try
            ReloadListviewFromXML()
        Catch ex As Exception
        End Try

        'NGWords“Ç
        Try
            'NGWSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'Permission“Ç
        Try
            PermissionSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        '•ÊƒXƒŒƒbƒh‚©‚ç‚Ì‘€ì‚ğ‹–‰Â‚·‚é
        Control.CheckForIllegalCrossThreadCalls = False

        'ƒRƒ}ƒ“ƒhƒŠƒXƒg‚Ì•œŒ³
        For i As Integer = 0 To Settings.Instance.CommandRecent.Count - 1
            cmbCommand.Items.Add(Settings.Instance.CommandRecent.Item(i))
        Next

        With Settings.Instance
            'Startƒ{ƒ^ƒ“‚Ìó‘Ô
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("ƒT[ƒoİ’è‚Ìƒtƒ@ƒCƒ‹w’è‚ª³‚µ‚­‚ ‚è‚Ü‚¹‚ñB" & vbCrLf & "İ’è‰æ–Ê‚ğŠJ‚«AƒpƒX‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "’ˆÓ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("ƒT[ƒoİ’è‚Ìƒtƒ@ƒCƒ‹w’è‚ª³‚µ‚­‚ ‚è‚Ü‚¹‚ñB" & vbCrLf & "İ’è‰æ–Ê‚ğŠJ‚«AƒpƒX‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "’ˆÓ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    btnRun.Enabled = True
                End If
            End If

            'Extended Players List‚Ì—LŒøó‘Ô•œŒ³
            ExtendPlayersListAreaToolStripMenuItem.Checked = .ExtendedPlayersListEnabled

            'Connected•\¦‚Ìó‘Ô
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

        'ƒEƒBƒ“ƒhƒEˆÊ’u‚Ì•œŒ³
        Me.DesktopLocation = Settings.Instance.WindowPos
        If Me.DesktopLocation = New Point(-32000, -32000) Then
            'Å¬‰»ó‘Ô‚ÌÀ•W‚Å•Û‘¶‚³‚ê‚Ä‚½‚Æ‚«‚Í‹­§“I‚É0,0‚É–ß‚·
            Me.DesktopLocation = New Point(0, 0)
        End If

        'ƒEƒBƒ“ƒhƒEƒTƒCƒY‚Ì•œŒ³
        Me.Size = Settings.Instance.WindowSize
        'Å‘å‰»ó‘Ô‚Ì•œŒ³
        If Settings.Instance.WindowMaximize = True Then
            Me.WindowState = FormWindowState.Maximized
        End If

        'ƒOƒ[ƒoƒ‹IPƒAƒhƒŒƒXæ“¾
        lblGlobalIP.Text = gfGetGlobalIP()
        'ƒvƒ‰ƒCƒx[ƒgIPƒAƒhƒŒƒXæ“¾
        lblPrivateIP.Text = gfGetPrivateIP()
        '©“®ƒoƒbƒNƒAƒbƒv•\¦
        If Settings.Instance.BackupEnabled = True Then
            lblDataBackup.Text = "Enabled (stop)"
        End If

        'ƒƒO•\¦
        pfWriteSystemLog("MCServerController " & GSTR_APP_VERSION & " Startup.", Color.Blue)

        'ƒo[ƒWƒ‡ƒ“ƒ`ƒFƒbƒN
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black
        gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)

        '©“®ƒT[ƒo‹N“®
        If Settings.Instance.AutoStart = True Then
            'ƒIƒvƒVƒ‡ƒ“‚ª—LŒø‚¾‚Á‚½‚çA©“®“I‚ÉMCƒT[ƒo‚ğ—§‚¿ã‚°‚é
            '2012/11/07 ©“®‹N“®‚ÌÛA©“®ƒoƒbƒNƒAƒbƒv‚ğ—LŒø‚É‚·‚é‚Ì‚ğ–Y‚ê‚Ä‚¢‚½ƒoƒO‚ğC³
            If pfServerStart() = True Then
                pfBackupTimerSet() '³í‚É‹N“®o—ˆ‚½‚çAƒoƒbƒNƒAƒbƒvƒ^ƒCƒ}[‚Ìó‘Ô‚ğİ’è
                If Settings.Instance.HeartBeatInterval >= 1 Then
                    'HBŠÔŠu‚ª1ˆÈã‚È‚çHB—LŒø
                    timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                    timHB.Start() 'HBŠJn
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
                'ƒƒbƒZ[ƒW‚ª‹ó‚È‚çˆ—‚ğs‚í‚È‚¢
                Return True
            End If

            If pfColorCodeTrim(msg) = False Then
                Return False
            End If

            '"[INFO] /127.0.0.1:xxxxx lost connection"‚ÌƒƒO‚ğo—Í‚µ‚È‚¢(2012/11/03 HB‚É0xFE‚ğg—p‚µ‚Ä‚¢‚é‚Íˆ—‚µ‚È‚¢‚æ‚¤•ÏX)
            If Settings.Instance.HeartBeatUse0xFE = False AndAlso CheckHeartBeatLog(msg) = True Then
                Return True
            End If

            'ƒ†[ƒU‚ÌƒƒOƒCƒ“^ƒƒOƒAƒEƒgƒ`ƒFƒbƒN
            pfPlayerInfoUpdate(msg)

            'ƒ`ƒƒƒbƒgƒ`ƒFƒbƒNˆ—
            pfPlayerChatCheck(msg)

            'ƒ`ƒƒƒbƒgƒƒOƒEƒBƒ“ƒhƒE‚Éo—Í
            ExpertChatLog(msg)

            'spawnpointƒRƒ}ƒ“ƒhŒ‹‰Êˆ—
            pfPlayerSpawnPointCheck(msg)

            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            'ƒfƒtƒHƒ‹ƒg‚ÌF•ÏXˆ—
            If msg.IndexOf("[WARNING]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Orange
            ElseIf msg.IndexOf("[ERROR]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Red
            Else
                rtbServerLog.SelectionColor = Color.Black
            End If

            'ƒJƒXƒ^ƒ€ƒAƒNƒVƒ‡ƒ“Às

            'ƒƒOo—Í
            'rtbServerLog.SelectedText = "[" & System.DateTime.Now.ToString & "] " & msg & vbCrLf
            rtbServerLog.SelectedText = msg & vbCrLf
            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            rtbServerLog.ScrollToCaret()
        Catch ex As Exception

        End Try

    End Function

    'ƒƒO‚Éo—Í‚³‚ê‚éƒJƒ‰[ƒR[ƒh‚ğíœ‚·‚é(æ“ª‚É“Áê•¶š‚ª‚ ‚é)
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

    'ƒT[ƒo‚Ì‹N“®ˆ—
    'Ql(ƒvƒƒZƒXì¬)Fhttp://blogs.wankuma.com/naoko/archive/2007/03/09/65823.aspx
    'Ql(ƒtƒ@ƒCƒ‹ŠÄ‹)Fhttp://dobon.net/vb/dotnet/file/filesystemwatcher.html
    Private Function pfServerStart() As Boolean
        Try
            '2014/07/21 eula.txt‘Î‰
            If gfGetEula() = False Then
                pfWriteSystemLog("You must agree to the EULA(eula.txt).", Color.Red)
                gblnExitFlg = True
                Return False
            End If

            '2012/11/03 ƒT[ƒo‹N“®‘O‚ÉƒoƒbƒNƒAƒbƒv‚ğæ“¾‚·‚éƒIƒvƒVƒ‡ƒ“’Ç‰Á(©“®ƒoƒbƒNƒAƒbƒv—LŒø‚Ì‚İ)
            '2012/11/03 Runƒ{ƒ^ƒ“‚ª—LŒø‚Ì‚Ì‚İÀs‚·‚é‚æ‚¤‚É•ÏXi©“®ƒŠƒJƒoƒŠˆ—’†‚ÍƒoƒbƒNƒAƒbƒv‚µ‚È‚¢j
            If btnRun.Enabled = True AndAlso Settings.Instance.BackupEnabled = True AndAlso Settings.Instance.BackupBeforeServerRun = True Then
                If Settings.Instance.BackupTarget.Rows.Count = 0 Then
                    pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
                End If

                pfWriteSystemLog("Auto-Backup Started.", Color.Black)

                'ƒoƒbƒNƒAƒbƒvÀs
                Dim strRetMsg As String = "" 'ƒoƒbƒNƒAƒbƒvˆ—‚©‚ç–ß‚³‚ê‚éƒƒbƒZ[ƒW
                If gfBackup(strRetMsg) = True Then
                    pfWriteSystemLog(strRetMsg, Color.Blue)
                Else
                    pfWriteSystemLog(strRetMsg, Color.Red)
                End If
            End If

            'server.propertiesÄ“Ç
            If gfLoadServerProp() = False Then
                pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
            End If

            pfWriteSystemLog("Minecraft Server Starting...", Color.Black)

            Dim strWorkingPath = _
                System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)   'ì‹ÆƒfƒBƒŒƒNƒgƒŠæ“¾
            System.IO.Directory.SetCurrentDirectory(strWorkingPath)             'ƒJƒŒƒ“ƒgƒfƒBƒŒƒNƒgƒŠˆÚ“®

            If Settings.Instance.ServerVersion >= 3 Then
                '1.7ˆÈ~‚Í.\logs\latest.log‚ğQÆ•ƒƒO‚Ì‘Ò”ğ‚ÍƒT[ƒoƒAƒvƒŠ‚ª©“®‚Ås‚¤
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "logs\latest.log")

            ElseIf Settings.Instance.ServerVersion <= 2 Then
                '1.7ˆÈ‘O‚Í.\server.log‚ğQÆ•ƒƒO‚Ì‘Ò”ğ‚ğMCSC‚Ås‚¤
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "server.log")

                '¡‚ ‚éserver.log‚ğlogƒtƒHƒ‹ƒ_‚ÉˆÚ“®(+ƒŠƒl[ƒ€)
                If System.IO.File.Exists("server.log") = True Then
                    If System.IO.Directory.Exists("log") = False Then
                        'ƒƒOƒoƒbƒNƒAƒbƒvƒtƒHƒ‹ƒ_ì¬
                        System.IO.Directory.CreateDirectory("log")
                    End If

                    'server.log‚ÌˆÚ“®(yyyyMMdd-HHmmss‚ğƒtƒ@ƒCƒ‹–¼‚É•t‰Á)
                    System.IO.File.Move("server.log", "log\server.log." & DateTime.Now.ToString("yyyyMMdd-HHmmss"))
                End If
            End If

            'ì¬‚·‚éƒT[ƒoƒvƒƒZƒX‚ÌÚ×‚ğw’è
            Dim mcsprocPsInfo As ProcessStartInfo = New ProcessStartInfo
            With mcsprocPsInfo
                'ì‹ÆƒfƒBƒŒƒNƒgƒŠ(jar‚Æ“¯‚¶êŠ)
                .WorkingDirectory = strWorkingPath
                '‹N“®‚·‚éJava.exe‚ÌƒpƒX
                .FileName = Settings.Instance.JavaPath
                'ˆø” 2012/10/17 jar‚ÌƒpƒX‚ğ""‚Å‚­‚­‚é‚æ‚¤•ÏX, 2013/07/28 ƒT[ƒo‚Ì‹N“®ˆø”w’è‚É‘Î‰
                .Arguments = Settings.Instance.Augment & " -jar " _
                            & """" & Settings.Instance.JarPath & """" & " " & Settings.Instance.JarFileAugment

                If Settings.Instance.ShowConsole = True Then
                    ' ƒRƒ“ƒ\[ƒ‹‚ğ•\¦‚·‚é
                    .CreateNoWindow = False
                    .WindowStyle = ProcessWindowStyle.Normal
                Else
                    ' V‚µ‚¢ƒEƒBƒ“ƒhƒE‚Íì‚ç‚È‚¢
                    .CreateNoWindow = True
                    .WindowStyle = ProcessWindowStyle.Hidden
                End If
            End With

            'ƒvƒƒZƒXƒIƒuƒWƒFƒNƒg‚Ì‰Šú‰»
            mcsProc = New System.Diagnostics.Process

            With Me.mcsProc
                ' ProcessStartInfo ‚ğŠÖ˜A•t‚¯
                .StartInfo = mcsprocPsInfo
                ' Process I—¹‚É Exited ƒCƒxƒ“ƒg‚ğ”­¶‚³‚¹‚é‚©”Û‚©(Šù’èFFalse)
                ' OnExited ƒƒ\ƒbƒh‚ğg‚¤‚ÆƒvƒƒOƒ‰ƒ€‚©‚ç Exited ƒCƒxƒ“ƒg‚Ì”­¶‚ª‰Â”\
                .EnableRaisingEvents = True
                AddHandler .Exited, AddressOf mcsOnProcessExited

                ' ŠJn
                .Start()
            End With

            'PerformanceCounter‚ÌƒCƒ“ƒXƒ^ƒ“ƒXİ’è
            pcCPU.InstanceName = mcsProc.ProcessName
            pcMem.InstanceName = mcsProc.ProcessName
            '‚à‚µVista/7‚ÅPrivate set -private-‚ğQÆ‚·‚é‚È‚çA
            '‚±‚±‚ÅOS”»’è‚µ‚ÄØ‚è‘Ö‚¦‚ê‚Î‚æ‚¢‚¾‚ë‚¤‚©B


            pfWriteSystemLog("Minecraft Server Startup Success.", Color.Blue)

            '‚±‚±‚Ü‚Å³í‚È‚çƒ^ƒCƒ}[‚ğŠJn‚µAƒ{ƒ^ƒ“‚Ìó‘Ô‚ğ•ÏX‚·‚é
            Me.timTick.Interval = 10000 '‰‰ñ‚Ì‚İƒƒO‚ğ“Çn‚ß‚é‚Ü‚Å10•b‘Ò‚Â(1.7‘Î‰)
            Me.timTick.Start()
            btnRun.Enabled = False
            btnStop.Enabled = True
            btnKill.Enabled = True
            cmbCommand.Enabled = True
            UpdatePlayerListToolStripMenuItem.Enabled = True

            'ƒ|[ƒg”Ô†ƒŠƒZƒbƒg
            pstrPort = ""

            'I—¹ƒtƒ‰ƒO‚ğFalse‚É•ÏX
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

    'ƒT[ƒo‚Ì‹N“®ƒ{ƒ^ƒ“
    Private Sub btnServerRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        'ƒT[ƒo‹N“®
        If pfServerStart() = True Then
            pfBackupTimerSet() '³í‚É‹N“®o—ˆ‚½‚çAƒoƒbƒNƒAƒbƒvƒ^ƒCƒ}[‚Ìó‘Ô‚ğİ’è
            If Settings.Instance.HeartBeatInterval >= 1 Then
                'HBŠ´Šo‚ª1ˆÈã‚È‚çHB—LŒø
                timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                timHB.Start() 'HBŠJn
            End If
        End If
    End Sub

    'ƒvƒƒZƒX‚ªI—¹‚µ‚½‚Æ‚«‚ÉÀs‚³‚ê‚é
    Private Sub mcsOnProcessExited(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'I—¹Œã‚Ìserver.log“Çæ‚è
            pfLogRead()

            'ƒvƒŒƒCƒ„[‚ÌƒIƒ“ƒ‰ƒCƒ“ƒXƒe[ƒ^ƒX‚ğ‘SˆõƒIƒtƒ‰ƒCƒ“‚ÉXV
            pfPlayerLogout("", True)

            'ƒƒOo—Í
            pfWriteSystemLog("Minecraft Server Shutdown.", Color.Red)

            '©“®ƒŠƒJƒoƒŠ
            If Settings.Instance.AutoRecovery = True Then
                '©“®ƒŠƒJƒoƒŠ—LŒø
                If gblnExitFlg = False Then
                    '³‹K‚ÌI—¹‚Å‚Í‚È‚¢‚Ì‚ÅAƒT[ƒo‚ğÄ‹N“®‚·‚é
                    pfWriteSystemLog("Minecraft Server Auto-Recovery Execute.", Color.Red)
                    pfServerStart()

                Else
                    'ƒ{ƒ^ƒ“‚Ìó‘Ô•ÏX
                    btnRun.Enabled = True
                    btnStop.Enabled = False
                    btnKill.Enabled = False
                    cmbCommand.Enabled = False
                    UpdatePlayerListToolStripMenuItem.Enabled = False

                    'ƒoƒbƒNƒAƒbƒv—pƒ^ƒCƒ}‚Ìİ’è
                    pfBackupTimerSet()

                    'HB’â~
                    timHB.Stop()
                End If

            Else
                '©“®ƒŠƒJƒoƒŠ–³Œø
                gblnExitFlg = True 'I—¹ƒtƒ‰ƒO‚ğ—LŒø‚É‚·‚é

                'ƒ{ƒ^ƒ“‚Ìó‘Ô•ÏX
                btnRun.Enabled = True
                btnStop.Enabled = False
                btnKill.Enabled = False
                cmbCommand.Enabled = False
                UpdatePlayerListToolStripMenuItem.Enabled = False

                'ƒoƒbƒNƒAƒbƒv—pƒ^ƒCƒ}‚Ìİ’è
                pfBackupTimerSet()

                'HB’â~
                timHB.Stop()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function pfLogRead() As Boolean
        Static intFileSize As Integer 'ˆÈ‘O‚Éƒ`ƒFƒbƒN‚µ‚½ƒtƒ@ƒCƒ‹ƒTƒCƒY‚ğŠi”[

        'ƒtƒ@ƒCƒ‹ƒ`ƒFƒbƒN
        If System.IO.File.Exists(gstrLogFilePath) = False Then
            Return False
        End If

        'ƒtƒ@ƒCƒ‹ƒTƒCƒYƒ`ƒFƒbƒN
        Dim fiLog As New System.IO.FileInfo(gstrLogFilePath)
        Try
            If intFileSize = fiLog.Length Then
                'ƒtƒ@ƒCƒ‹ƒTƒCƒY‚É•Ï‰»‚ª‚È‚¯‚ê‚Îˆ—‚µ‚È‚¢
                Exit Function
            ElseIf intFileSize > fiLog.Length Then
                '‹L˜^‚³‚ê‚Ä‚éƒƒOƒTƒCƒY‚Ì•û‚ª‘å‚«‚¢ê‡‚ÍƒƒOƒtƒ@ƒCƒ‹‚ªíœ‚³‚ê‚½‚Æ‚·‚é
                intFileSize = 0
            End If
        Catch ex As Exception
            intFileSize = 0
        End Try

        Try
            'ƒƒOƒtƒ@ƒCƒ‹‚ğŠJ‚­
            Dim fsLog As New System.IO.FileStream(gstrLogFilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

            'ƒƒOƒtƒ@ƒCƒ‹‚Ì•¶šƒR[ƒh”»’è 2013/12/08
            Dim bsLog(fsLog.Length - 1) As Byte
            fsLog.Read(bsLog, 0, bsLog.Length)
            Dim enc As System.Text.Encoding = GetCode(bsLog)

            Dim srLog As New System.IO.StreamReader(fsLog, enc)
            'Šù‚É“Ç‚İ‚İÏ‚İ‚ÌŠ‚Ü‚ÅƒV[ƒN
            srLog.BaseStream.Seek(intFileSize, IO.SeekOrigin.Begin)

            '’Ç‰Á‚³‚ê‚½ƒƒbƒZ[ƒW‚ğ1s‚¸‚Â“Ç‚İæ‚èˆ—Às
            '2013/06/11 listƒRƒ}ƒ“ƒh‚Ìˆ—‚Ì‚½‚ßAs‚ÌŒ‹‡ˆ—’Ç‰Á
            Dim strReadBuf As String = String.Empty
            While (srLog.Peek() >= 0)
                strReadBuf = srLog.ReadLine
                If pfListComanndCheck(strReadBuf) = True Then
                    strReadBuf = strReadBuf & " " & srLog.ReadLine
                End If
                pfWriteServerLog(strReadBuf)
                Application.DoEvents()
            End While

            'ƒƒOƒtƒ@ƒCƒ‹‚ğ•Â‚¶‚é
            srLog.Close()
            fsLog.Close()

            'Ÿ‰ñ“Çæ—pƒIƒtƒZƒbƒgˆÊ’u‚ğ‹L‰¯
            intFileSize = fiLog.Length
        Catch ex As Exception
            pfWriteSystemLog("LogFile Read Failure.", Color.Red)
        End Try
    End Function

    '1.3.1ˆÈ~‚ÌlistƒRƒ}ƒ“ƒhÀsŒ‹‰Ê"There are 0/0 players online:"‚ğŒŸo
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
            If timTick.Interval = 10000 Then '10•b‚ğ1•b‚É•ÏX‚·‚é
                timTick.Interval = 1000
            End If

            'ƒvƒƒZƒX‚ªI—¹‚µ‚Ä‚¢‚½‚çƒ^ƒCƒ}[’â~
            If Me.mcsProc.HasExited = True Then
                Me.timTick.Stop()
                'î•ñƒNƒŠƒA
                lblServer.Text = "Not Running"
                lblUptime.Text = "-"
                lblCPUUsage.Text = "-"
                lblMemUsage.Text = "-"
                'lblConnected.Text = "-"
                Exit Sub
            End If

            'ƒƒO“Ç‚İ‚İ
            timTick.Enabled = False 'ƒƒO“Ç‚İæ‚è‚ªI‚í‚é‚Ü‚Åƒ^ƒCƒ}[’â~
            pfLogRead()

            'î•ñXV
            With mcsProc
                'ƒT[ƒoó‹µ
                lblServer.Text = "Running"
                'ƒT[ƒo‹N“®ŠÔ
                'lblUptime.Text = New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                Dim tsInterval As TimeSpan = DateTime.Now.Subtract(.StartTime)
                lblUptime.Text = tsInterval.Days & "d " & New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                'CPUg—p—¦
                lblCPUUsage.Text = Math.Round(pcCPU.NextValue / System.Environment.ProcessorCount, 1, MidpointRounding.AwayFromZero).ToString("0.0") & " %"
                '•¨—ƒƒ‚ƒŠg—p—Ê
                lblMemUsage.Text = (pcMem.NextValue / 1024 / 1024).ToString("0.0") & " MB"
            End With

            'ƒXƒPƒWƒ…[ƒ‰ˆ—

        Catch ex As Exception


        Finally
            'ƒ^ƒCƒ}[ÄŠJiƒT[ƒo‹N“®’†‚ÉŒÀ‚éj
            If timTick.Enabled = False And btnRun.Enabled = False Then
                timTick.Enabled = True
            End If
        End Try

    End Sub

    Private Sub cmbCommand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCommand.KeyPress
        Try
            'Enter‚ª‰Ÿ‚³‚êA‚©‚ÂƒeƒLƒXƒg‚ª‹ó‚Å‚È‚¯‚ê‚Îˆ—Às
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) AndAlso Me.cmbCommand.Text <> "" Then
                '“Á’è‚Ì“ü—Í‚Ìˆ—
                Select Case Char.ToLower(cmbCommand.Text)
                    Case "stop"
                        'STOP‚ª“ü—Í‚³‚ê‚½ê‡‚Í³íI—¹ŠJn‚Æ‚·‚é
                        gblnExitFlg = True
                End Select

                'ƒRƒ}ƒ“ƒhÀs
                gsSendCommand(cmbCommand.Text)

                'Às‚µ‚½ƒRƒ}ƒ“ƒh‚ğƒŠƒXƒg‚É•Û‘¶
                If pfSetCommandList(Me.cmbCommand.Text) = False Then
                    'ƒŠƒXƒg’Ç‰ÁƒGƒ‰[‚ÌƒƒO•\¦
                    pfWriteSystemLog("Command List Edit Error", Color.Red)
                End If

                pfWriteSystemLog("Command Send : " & cmbCommand.Text, Color.Black)

                cmbCommand.Text = ""
            End If
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
            'Às‚µ‚½ƒRƒ}ƒ“ƒh‚ğƒŠƒXƒg‚É•Û‘¶
            If pfSetCommandList(Me.cmbCommand.Text) = False Then
                'ƒŠƒXƒg’Ç‰ÁƒGƒ‰[‚ÌƒƒO•\¦
                pfWriteSystemLog("Command List Edit Error.", Color.Red)
            End If
            cmbCommand.Text = ""
        End Try

    End Sub

    Private Function pfSetCommandList(ByVal cmd As String) As Boolean
        Try
            If Me.cmbCommand.Items.Count = 0 Then
                'ƒAƒCƒeƒ€‚ª‘¶İ‚µ‚È‚¢ê‡‚Í’Ç‰Á‚µ‚Ä‘¦I—¹
                Me.cmbCommand.Items.Add(cmd)
                Return True
            End If

            '“ü—Í‚µ‚½ƒRƒ}ƒ“ƒh‚ªƒŠƒXƒg‚ÉŠù‚É‘¶İ‚µ‚È‚¢‚©ƒ`ƒFƒbƒN
            For i As Integer = 0 To Me.cmbCommand.Items.Count - 1
                If Me.cmbCommand.Items(i) = cmd Then
                    'ˆê’v‚·‚éItem‚ª‘¶İ‚µ‚½ê‡‚ÍAíœ
                    Me.cmbCommand.Items.RemoveAt(i)
                    Exit For
                End If
            Next

            'ƒRƒ}ƒ“ƒh‚ğƒŠƒXƒg‚É’Ç‰Á
            Me.cmbCommand.Items.Insert(0, cmd)

            'ƒRƒ}ƒ“ƒh—š—ğ‚ª20‚ğ’´‚¦‚½ê‡‚ÍŒÃ‚¢•¨‚ğíœ
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

    'stopƒ{ƒ^ƒ“‚ğ‰Ÿ‚µ‚½‚Æ‚«‚ÍAstopƒRƒ}ƒ“ƒh‚ğ‘—M‚·‚é
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Try
            gblnExitFlg = True '³íI—¹ƒtƒ‰ƒO
            gsSendCommand("stop")
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
        End Try
    End Sub

    Private Sub InformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformationToolStripMenuItem.Click
        'ƒCƒ“ƒtƒHƒ[ƒVƒ‡ƒ“•\¦
        frmInformation.Show()
        frmInformation.Activate()
    End Sub

    'ƒT[ƒoƒƒbƒZ[ƒW‚ğŒ©‚ÄAPlayer‚Ìˆê——‚ğXV‚·‚é
    Private Function pfPlayerInfoUpdate(ByVal msg As String) As Boolean
        Try
            'MCBans˜AŒg‹@”\‚ğ‹­§“I‚É–³Œø‰» # FOR_DEBUG #
            Settings.Instance.MCBansEnabled = False

            '–Ê“|‚ÈƒXƒy[ƒX‚ğ’uŠ·ˆ—
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

            '’uŠ·‚µ‚½ƒXƒy[ƒX‚ğ–ß‚·
            If Settings.Instance.ServerVersion >= 3 Then
                For i As Integer = 0 To strSplitMsg.Length - 1
                    strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                Next
            End If

            If Settings.Instance.ServerVersion >= 3 Then
                'V‚µ‚¢ƒƒOŒ`®
                Select Case strSplitMsg(1)
                    Case "[Server thread/INFO]:"
                        '2012/11/03 ƒƒbƒZ[ƒW‚Ì“à—e‚ª‹ó‚Ìê‡‚àI—¹‚·‚é
                        If strSplitMsg.Length >= 3 AndAlso strSplitMsg(2).Length = 0 Then
                            Exit Function
                        End If

                        'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒW‚Ìê‡‚Í‘¦I—¹‚·‚é
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(2)
                            Case "Disconnecting" '‚±‚Ì“_‚Å‚Ù‚ÚŠmÀ‚ÉBAN‚ÌƒƒbƒZ[ƒW
                                If strSplitMsg(5) = "You" _
                                            AndAlso strSplitMsg(6) = "are" _
                                            AndAlso strSplitMsg(7) = "not" _
                                            AndAlso strSplitMsg(8) = "white-listed" _
                                            AndAlso strSplitMsg(9) = "on" _
                                            AndAlso strSplitMsg(10) = "this" _
                                            AndAlso strSplitMsg(11) = "server!" Then
                                    'white-list‚É’Ç‰Á‚³‚ê‚Ä‚¢‚È‚¢
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
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
                                    'Player BAN‚³‚ê‚Ä‚¢‚é
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
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
                                    'IP BAN‚³‚ê‚Ä‚¢‚é
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
                                    Dim strUser = strSplitMsg(3)
                                    Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(3) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'listƒRƒ}ƒ“ƒh‚ªÀs‚³‚ê‚½‚Æ‚«i‘½•ª1.2.5‚Ü‚Åj
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(3) = "are" _
                                   AndAlso strSplitMsg(5) = "players" _
                                   AndAlso strSplitMsg(6) = "online:" Then
                                    '2013/06/11 1.3.1ˆÈ~‚ÌlistƒRƒ}ƒ“ƒhŒ‹‰Êˆ—‘Î‰
                                    'Œö®ƒT[ƒo‚Å‚Í2s‚ğŒ‹‡‚µ‚½Œ‹‰ÊA [Server thread/INFO]: There are 0/0 players online:  [Server thread/INFO]: Player1, Player2,...‚Æ‚È‚é‚Ì‚Å‹­§ˆ’u
                                    'CraftBukkit‚Å‚Í‚»‚Ì‚Ü‚ÜŒ‹‡‚µ‚Äˆ—‚µ‚Ä‚µ‚Ü‚¦‚Î–â‘è–³‚¢
                                    If strSplitMsg.Length >= 10 AndAlso strSplitMsg(8) = "[Server thread/INFO]:" Then 'Œö®ƒT[ƒo
                                        If pfPlayerUpdateFromList(strSplitMsg, 9) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkitƒT[ƒo
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
                                                    'ƒƒOƒCƒ“‚ÌƒƒbƒZ[ƒW
                                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
                                                    Dim strSplit() As String = strSplitMsg(2).Replace("[", "").Replace("]", "").Split("/")
                                                    Dim strUser = strSplit(0)
                                                    Dim strSplit2() As String = strSplit(1).Split(":")
                                                    Dim strIP = strSplit2(0)

                                                    If Settings.Instance.MCBansEnabled = True Then
                                                        Dim blnStat As Boolean = False
                                                        Dim strReason As String = String.Empty
                                                        Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                            Case -1 'Œ´ˆö•s–¾‚ÌƒGƒ‰[
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 0
                                                                If blnStat = False Then 'Ú‘±NG
                                                                    pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                    gsSendCommand("kick " & strUser) 'kick
                                                                    gsSendCommand("ban " & strUser) 'ban
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                Else 'Ú‘±OK
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                End If
                                                            Case 1 'online-mode‚Ì’l‚ªæ“¾o—ˆ‚È‚¢
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 2 'online-mode‚ªfalse‚¾‚Á‚½
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 3 'MCBansƒT[ƒo‚É‚©‚ç³‚µ‚­ƒf[ƒ^‚ªæ“¾o—ˆ‚È‚©‚Á‚½
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 4 'MCBans˜AŒg‚É•K—v‚ÈÚ‘±Œ³IPƒAƒhƒŒƒX‚ª³‚µ‚­æ“¾o—ˆ‚È‚©‚Á‚½
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
                ']—ˆ’Ê‚è‚ÌƒƒOŒ`®
                Select Case strSplitMsg(2) '[INFO]“™‚Ì•”•ª
                    Case "[INFO]" '1.6.4‚Ü‚Å‚ÌŒ`®
                        '2012/11/03 ƒƒbƒZ[ƒW‚Ì“à—e‚ª‹ó‚Ìê‡‚àI—¹‚·‚é
                        If strSplitMsg.Length >= 4 AndAlso strSplitMsg(3).Length = 0 Then
                            Exit Function
                        End If

                        'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒW‚Ìê‡‚Í‘¦I—¹‚·‚é
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(3)
                            Case "Disconnecting" '‚±‚Ì“_‚Å‚Ù‚ÚŠmÀ‚ÉBAN‚ÌƒƒbƒZ[ƒW
                                If strSplitMsg(6) = "You" _
                                            AndAlso strSplitMsg(7) = "are" _
                                            AndAlso strSplitMsg(8) = "not" _
                                            AndAlso strSplitMsg(9) = "white-listed" _
                                            AndAlso strSplitMsg(10) = "on" _
                                            AndAlso strSplitMsg(11) = "this" _
                                            AndAlso strSplitMsg(12) = "server!" Then
                                    'white-list‚É’Ç‰Á‚³‚ê‚Ä‚¢‚È‚¢
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
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
                                    'Player BAN‚³‚ê‚Ä‚¢‚é
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
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
                                    'IP BAN‚³‚ê‚Ä‚¢‚é
                                    'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
                                    Dim strUser = strSplitMsg(4)
                                    Dim strSplit() As String = strSplitMsg(5).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(4) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'listƒRƒ}ƒ“ƒh‚ªÀs‚³‚ê‚½‚Æ‚«i‘½•ª1.2.5‚Ü‚Åj
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(4) = "are" _
                                   AndAlso strSplitMsg(6) = "players" _
                                   AndAlso strSplitMsg(7) = "online:" Then
                                    '2013/06/11 1.3.1ˆÈ~‚ÌlistƒRƒ}ƒ“ƒhŒ‹‰Êˆ—‘Î‰
                                    'Œö®ƒT[ƒo‚Å‚Í2s‚ğŒ‹‡‚µ‚½Œ‹‰ÊA“ú•t  [INFO] There are 0/0 players online: “ú•t  [INFO] Player1, Player2,...‚Æ‚È‚é‚Ì‚Å‹­§ˆ’u
                                    'CraftBukkit‚Å‚Í‚»‚Ì‚Ü‚ÜŒ‹‡‚µ‚Äˆ—‚µ‚Ä‚µ‚Ü‚¦‚Î–â‘è–³‚¢
                                    If strSplitMsg.Length >= 11 AndAlso strSplitMsg(10) = "[INFO]" Then 'Œö®ƒT[ƒo
                                        If pfPlayerUpdateFromList(strSplitMsg, 11) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkitƒT[ƒo
                                        If pfPlayerUpdateFromList(strSplitMsg, 8) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    End If

                                End If


                            Case Else
                                '”z—ñ‚ª3‚Ü‚Å‚µ‚©‚È‚¢ê‡‚É—áŠOƒGƒ‰[‚ğ“f‚­‚Ì‚Å‘Îô 2013/07/20
                                If strSplitMsg.Length <= 4 Then
                                    Exit Function
                                End If

                                Select Case strSplitMsg(4)
                                    Case "lost"
                                        If strSplitMsg(5) = "connection:" Then
                                            If strSplitMsg(6).IndexOf("disconnect") >= 0 Then
                                                'ƒƒOƒAƒEƒg‚ÌƒƒbƒZ[ƒW
                                                'disconnect.quitting‚âdisconnect.genericReasonAdisconnect.endOfStream“™‚ª‘¶İ‚·‚é‚Ì‚Å
                                                'disconnect‚ªŠÜ‚Ü‚ê‚Ä‚¢‚ê‚Î“K—p‚Æ‚·‚é
                                                If pfPlayerLogout(strSplitMsg(3)) = False Then
                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                End If
                                            End If
                                        End If

                                    Case Else
                                        If Settings.Instance.ServerVersion >= 1 Then '1.3‚©‚ç‚ÌVŒ`®
                                            Select Case strSplitMsg(4)
                                                Case "logged"
                                                    If strSplitMsg(5) = "in" AndAlso strSplitMsg(6) = "with" AndAlso strSplitMsg(7) = "entity" Then
                                                        'ƒƒOƒCƒ“‚ÌƒƒbƒZ[ƒW
                                                        'Minecraft ID‚ÆIP‚ğ”²‚«o‚·

                                                        '1.3pre‚Å‚ÍUserID‚Ì‚İ‚¾‚Á‚½
                                                        'Dim strUser = strSplitMsg(3)
                                                        'Dim strIP = "0.0.0.0"
                                                        'If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                        '    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                        'End If

                                                        '1.3.1‚Å‚ÍUserID[/IP:Port]‚Æ]—ˆ‚ÌŒ`®‚©‚çƒXƒy[ƒX‚ª–³‚­‚È‚Á‚½
                                                        Dim strSplit() As String = strSplitMsg(3).Replace("[", "").Replace("]", "").Split("/")
                                                        Dim strUser = strSplit(0)
                                                        Dim strSplit2() As String = strSplit(1).Split(":")
                                                        Dim strIP = strSplit2(0)

                                                        '2012/11/11 MCBans˜AŒg’Ç‰Á
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 'Œ´ˆö•s–¾‚ÌƒGƒ‰[
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then 'Ú‘±NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else 'Ú‘±OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-mode‚Ì’l‚ªæ“¾o—ˆ‚È‚¢
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-mode‚ªfalse‚¾‚Á‚½
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBansƒT[ƒo‚É‚©‚ç³‚µ‚­ƒf[ƒ^‚ªæ“¾o—ˆ‚È‚©‚Á‚½
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans˜AŒg‚É•K—v‚ÈÚ‘±Œ³IPƒAƒhƒŒƒX‚ª³‚µ‚­æ“¾o—ˆ‚È‚©‚Á‚½
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
                                        Else '1.2.5ˆÈ‘O‚ÌŒ`®
                                            Select Case strSplitMsg(5)
                                                Case "logged"
                                                    If strSplitMsg(6) = "in" AndAlso strSplitMsg(7) = "with" AndAlso strSplitMsg(8) = "entity" Then
                                                        'ƒƒOƒCƒ“‚ÌƒƒbƒZ[ƒW
                                                        'Minecraft ID‚ÆIP‚ğ”²‚«o‚·
                                                        Dim strUser = strSplitMsg(3)
                                                        Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                                        Dim strIP = strSplit(0)

                                                        '2012/11/11 MCBans˜AŒg’Ç‰Á
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 'Œ´ˆö•s–¾‚ÌƒGƒ‰[
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then 'Ú‘±NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else 'Ú‘±OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-mode‚Ì’l‚ªæ“¾o—ˆ‚È‚¢
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-mode‚ªfalse‚¾‚Á‚½
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBansƒT[ƒo‚É‚©‚ç³‚µ‚­ƒf[ƒ^‚ªæ“¾o—ˆ‚È‚©‚Á‚½
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans˜AŒg‚É•K—v‚ÈÚ‘±Œ³IPƒAƒhƒŒƒX‚ª³‚µ‚­æ“¾o—ˆ‚È‚©‚Á‚½
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

    'ƒvƒŒƒCƒ„[ƒƒOƒCƒ“‚ÉAƒvƒŒƒCƒ„[î•ñ‚ğXV‚·‚é
    Private Function pfPlayerLogin(ByVal user As String, ByVal IP As String, ByVal online As String) As Boolean
        Try
            Dim item() As String = {user, IP, online, DateTime.Now, "", "", 0} '2013/06/08 SpawnPoint’Ç‰Á‚É‚Â‚«C³

            If lvPlayers.Items.Count = 0 Then '1l–Ú‚È‚ç‘¦’Ç‰Á
                lvPlayers.Items.Add(New ListViewItem(item))
                Return True
            End If

            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    'Šù‚ÉƒŠƒXƒg‚É‘¶İ‚·‚éê‡AIP‚ÆOnline‚ğXV
                    lvPlayers.Items(i).SubItems(1).Text = IP
                    lvPlayers.Items(i).SubItems(2).Text = online
                    If online = GSTR_ONLINE_TRUE Then 'ƒIƒ“ƒ‰ƒCƒ“‚É‚È‚Á‚½‚Æ‚«‚Ì‚İƒƒOƒCƒ“ŠÔXV
                        lvPlayers.Items(i).SubItems(3).Text = DateTime.Now 'ƒƒOƒCƒ“ŠÔ
                    End If
                    Return True
                End If
            Next

            'ƒŠƒXƒg‚É‘¶İ‚µ‚È‚¯‚ê‚Î’Ç‰Á
            lvPlayers.Items.Add(New ListViewItem(item))
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    'ƒvƒŒƒCƒ„[ƒƒOƒAƒEƒg‚ÉAƒvƒŒƒCƒ„[î•ñ‚ğXV‚·‚é
    '2013/06/09 ‘Sˆõ‚ğƒIƒtƒ‰ƒCƒ“‚ÉXV‚·‚éƒ‚[ƒh’Ç‰Á(ƒ†[ƒU[–¼‚Í‹ó—“‚Å—Ç‚¢)
    Private Function pfPlayerLogout(ByVal user As String, Optional ByVal allOfflineMode As Boolean = False) As Boolean
        If lvPlayers.Items.Count = 0 Then 'ƒŠƒXƒg‚ª‹ó‚È‚çˆ—‚µ‚È‚¢
            Return True
        End If

        If allOfflineMode = True Then '‘SˆõƒIƒtƒ‰ƒCƒ“ó‘Ô‚É•ÏX
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                '2014/05/17 ƒƒOƒCƒ“‚µ‚Ä‚È‚¢l‚Ü‚ÅƒƒOƒAƒEƒgŠÔ‚ğXV‚µ‚Ä‚¢‚½‚Ì‚ÅC³
                If lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now 'ƒƒOƒAƒEƒgŠÔ
                End If
            Next

        Else '“Á’è‚Ìƒ†[ƒU[‚Ìó‘Ô‚ğXV
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now 'ƒƒOƒAƒEƒgŠÔ
                    Return True
                End If
            Next
        End If

    End Function

    'listƒRƒ}ƒ“ƒh‚ÌŒ‹‰Ê‚©‚çƒvƒŒƒCƒ„[‚ÌƒXƒe[ƒ^ƒX‚ğXV‚·‚é
    '2012/11/03 1.3‚©‚çlist‚ÌÀsŒ‹‰Ê•\¦‚ª•Ï‚í‚Á‚½‚½‚ßAstartIndex‚ğw’è‚Å‚«‚é‚æ‚¤‚É•ÏX
    Private Function pfPlayerUpdateFromList(ByVal splitmsg() As String, Optional ByVal startIndex As Integer = 5) As Boolean
        'ƒƒOƒAƒEƒgƒ`ƒFƒbƒNiƒƒOƒAƒEƒgˆ—‚ªo—ˆ‚Ä‚È‚¢ƒ†[ƒU‚ÌƒIƒ“ƒ‰ƒCƒ“ƒXƒe[ƒ^ƒX‚ğƒIƒtƒ‰ƒCƒ“‚Éİ’èj
        Dim blnOnline As Boolean = False
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                'Online‚Ìƒ†[ƒU‚Ì‚İ‚ğ‘ÎÛ‚ÉŒŸõ

                blnOnline = False
                For iPlayer As Integer = startIndex To splitmsg.Length - 1
                    If lvPlayers.Items(iList).SubItems(0).Text = splitmsg(iPlayer).Trim(",") Then
                        blnOnline = True
                        Exit For
                    End If
                Next

                'ƒIƒ“ƒ‰ƒCƒ“‚ÌƒvƒŒƒCƒ„[ˆê——‚É‘¶İ‚µ‚È‚¯‚ê‚ÎØ’fÏ‚İ‚Æ‚·‚é
                If blnOnline = False Then
                    pfPlayerLogout(lvPlayers.Items(iList).SubItems(0).Text)
                End If
            End If
        Next

        'ƒƒOƒCƒ“ƒ`ƒFƒbƒNiƒƒOƒCƒ“ˆ—‚ªo—ˆ‚Ä‚È‚¢ƒ†[ƒU‚ÌƒIƒ“ƒ‰ƒCƒ“ƒXƒe[ƒ^ƒX‚ğƒIƒ“ƒ‰ƒCƒ“‚Éİ’èj
        Dim blnListed As Boolean = False
        Dim blnUpdate As Boolean = False
        For iPlayer As Integer = startIndex To splitmsg.Length - 1

            blnListed = False
            blnUpdate = False

            For iList As Integer = 0 To lvPlayers.Items.Count - 1
                '‹ó•¶š‚È‚ç‘¦ƒXƒLƒbƒv
                If splitmsg(iPlayer).Trim(",") = "" Then
                    Continue For
                End If

                If splitmsg(iPlayer).Trim(",") = lvPlayers.Items(iList).SubItems(0).Text Then
                    'ListView‚Éƒ†[ƒU[‚ğŒ©‚Â‚¯‚½
                    If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                        'Šù‚ÉOnline‚Ìƒ†[ƒU‚È‚ç‰½‚à‚µ‚È‚¢
                        blnListed = True
                        blnUpdate = False

                    Else
                        'OnlineˆÈŠO‚È‚çƒXƒe[ƒ^ƒXXV
                        blnListed = True
                        blnUpdate = True

                    End If
                    Continue For
                End If

            Next

            'XV”»’è
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

    'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒW‚ÌŠÄ‹
    'NGWords‚Åw’è‚³‚ê‚½•¶š—ñ‚ğŒ©‚Â‚¯‚½‚çbad count‚Ìã¸/kick/ban“™‚ğs‚¤
    '2012-05-25 08:21:29 [INFO] <USERNAME> Chat Message Œ`®‚ğŠÄ‹‚·‚é
    Private Function pfPlayerChatCheck(ByVal msg As String) As Boolean
        Try
            Dim strSplitMsg As String()
            Dim strPlayerID As String = String.Empty
            Dim strChat As String = String.Empty
            Dim intCommandOffset As Integer = 0

            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4ˆÈ‘O
                '2014-05-17 16:21:55 [INFO] <miyabi9821> test

                intCommandOffset = 4

                strSplitMsg = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '—v‘f”‚ª4ˆÈ‰º‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3‚Â‚ß‚Ì—v‘f‚ª[INFO]‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    'ƒƒbƒZ[ƒW‚ª\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                'PlayerIDæ“¾
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?<(.+?)> (.*)", "$2")
                'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒWæ“¾
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)", "$3")

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7ˆÈ~
                '[16:05:19] [Server thread/INFO]: <miyabi9821> test

                intCommandOffset = 3

                '–Ê“|‚ÈƒXƒy[ƒX‚ğ’uŠ·ˆ—
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

                '’uŠ·‚µ‚½ƒXƒy[ƒX‚ğ–ß‚·
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '—v‘f”‚ª3ˆÈ‰º‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2‚Â‚ß‚Ì—v‘f‚ª[Server thread/INFO]:‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                    'ƒƒbƒZ[ƒW‚ª‚¶‚á‚È‚¢
                    Exit Function
                End If

                'PlayerIDæ“¾
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)", "$2")
                'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒWæ“¾
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?(<.+?>) (.*)", "$3")

            End If

            ''*** NG Wordsˆ— ***
            'With NGWSettings.Instance
            '    If .NGWordsEnabled = True Then
            '        Dim intBadCount As Integer = 0
            '        With NGWSettings.Instance.NGWords
            '            For i As Integer = 0 To .Rows.Count - 1
            '                If CBool(.Rows(i)(0)) = True Then
            '                    If strChat.IndexOf(.Rows(i)(1)) >= 0 Then
            '                        'NGWords‚ªƒ`ƒƒƒbƒg•¶š—ñ‚ÉŒ©‚Â‚©‚Á‚½ê‡ABadCount‰ÁZ
            '                        intBadCount += CInt(.Rows(i)(2))
            '                    End If
            '                End If
            '            Next
            '        End With

            '        'BadCount‚ª0‚æ‚è‘å‚«‚¯‚ê‚Î
            '        If intBadCount > 0 Then
            '            'ƒvƒŒƒCƒ„[ˆê——‚©‚çŒŸõ
            '            With lvPlayers
            '                For i As Integer = 0 To lvPlayers.Items.Count - 1
            '                    If .Items(i).SubItems(0).Text = strPlayerID Then
            '                        'IDŒ©‚Â‚¯‚½‚çBadCount‰ÁZ
            '                        intBadCount += intBadCount + CInt(.Items(i).SubItems(5).Text)
            '                        .Items(i).SubItems(0).Text = intBadCount.ToString

            '                        'sayƒRƒ}ƒ“ƒh‚ÅŒx
            '                        gsSendCommand("say " & strPlayerID)

            '                        'BadCount‚ªè‡’l‚ğ’´‚¦‚½‚çBANƒRƒ}ƒ“ƒh”­s
            '                        If intBadCount > 10 Then
            '                            gsSendCommand("ban " & strPlayerID)
            '                        End If
            '                    End If
            '                Next
            '            End With
            '        End If
            '    End If
            'End With

            '*** Permission ˆ— ***
            With PermissionSettings.Instance
                If .Enabled = True Then
                    If .PrefixChar.Length = 1 Then
                        'ƒ`ƒƒƒbƒgƒƒbƒZ[ƒW‚Ì1•¶š–Ú‚ªw’è‚³‚ê‚½Prefix Character‚È‚çˆ—ŠJn
                        If strChat.Substring(0, 1) = .PrefixChar Then
                            'strSplitMsg(4)‚ªƒRƒ}ƒ“ƒh‚É‚È‚Á‚Ä‚é‚Ì‚ÅAPrefix Character‚ğ”²‚¢‚½ƒRƒ}ƒ“ƒh‚ğØ‚èo‚·
                            '1.7ˆÈ~‚Í(3)‚É‚È‚Á‚Ä‚µ‚Ü‚Á‚½‚Ì‚ÅAŒÅ’è’l‚Å‚Í‚È‚­•Ï”‰»
                            Dim strCommand As String = strSplitMsg(intCommandOffset).Substring(1, strSplitMsg(intCommandOffset).Length - 1)

                            'Àsƒ†[ƒU[‚ªƒRƒ}ƒ“ƒh‚ÌÀsƒp[ƒ~ƒbƒVƒ‡ƒ“‚ğ‚Á‚Ä‚¢‚é‚©
                            Select Case pfGetPermission(strCommand, strPlayerID)
                                Case 1 'ƒp[ƒ~ƒbƒVƒ‡ƒ“‚ ‚è
                                    'ƒRƒ}ƒ“ƒhÀsˆ—
                                    Select Case strCommand
                                        Case "tp"
                                            'strSplitMsg 4:Command 5:PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'w’è‚³‚ê‚½ƒvƒŒƒCƒ„[‚ª‚¢‚é‚©H
                                                If pfIsPlayerOnline(strSplitMsg(intCommandOffset + 1)) = True Then
                                                    'tp <from PlayerID> <to PlayerID>
                                                    gsSendCommand("tp " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1))
                                                Else
                                                    gsSendCommand("tell " & strPlayerID & " Target Player is not Online.")
                                                End If
                                            Else
                                                'ƒRƒ}ƒ“ƒh‚Ìg—p•û–@•\¦
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
                                                        Case 0 'Œö®ƒT[ƒo
                                                            'give <PlayerID> <ItemID> <Num> <Damage>
                                                            gsSendCommand("give " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                        Case Else '‚»‚êˆÈŠO
                                                            '¦Bukkit‚É‚ÍDamagew’è‚È‚µ
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num>")
                                                    End Select
                                                Case Else
                                                    Select Case Settings.Instance.ServerMode
                                                        Case 0 'Œö®ƒT[ƒo
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num> <Damage>")
                                                        Case Else '‚»‚êˆÈŠO
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
                                                'banned-players.txt‚É‘¶İ‚·‚é‚©ƒ`ƒFƒbƒN(‚·‚é—\’è)

                                                'ban <PlayerID>
                                                gsSendCommand("ban " & strSplitMsg(intCommandOffset + 1))
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " ban comand usage : " & .PrefixChar & "ban <PlayerID>")
                                            End If
                                        Case "pardon"
                                            'strSplitMsg 4:Command 5:Target PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'banned-players.txt‚É‘¶İ‚·‚é‚©ƒ`ƒFƒbƒN(‚·‚é—\’è)

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
                                                            'ƒT[ƒo‘¤‚ÅÀs‚·‚é‚Æƒ†[ƒU‚É’Ê’m‚³‚ê‚È‚¢‚Ì‚ÅAwhite-list.txt‚ğ“Ç‚ñ‚Å’Ê’m‚·‚é
                                                            Dim strWhitelist As String = ""
                                                            'ƒƒOƒtƒ@ƒCƒ‹‚ğŠJ‚­
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
                                                            'white-list.txt‚É‘¶İ‚·‚é‚©ƒ`ƒFƒbƒN(‚·‚é—\’è)

                                                            'whitelist add <PlayerID>
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                            gsSendCommand("tell " & strPlayerID & " " & strSplitMsg(intCommandOffset + 2) & " has been registered to the white list.")
                                                        Case "remove"
                                                            'white-list.txt‚É‘¶İ‚·‚é‚©ƒ`ƒFƒbƒN(‚·‚é—\’è)

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
                                                Case intCommandOffset + 1 'xyz‚ªw’è‚³‚ê‚È‚¢ê‡
                                                    'spawnpoint <PlayerID>
                                                    gsSendCommand("spawnpoint " & strPlayerID)
                                                    gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to current point.")
                                                Case intCommandOffset + 4 'xyz‚ªw’è‚³‚ê‚Ä‚¢‚éê‡
                                                    'spawnpoint <PlayerID> <x> <y> <z>
                                                    Select Case pfCheckXYZ(strSplitMsg(intCommandOffset + 1), strSplitMsg(intCommandOffset + 2), strSplitMsg(intCommandOffset + 3))
                                                        Case 0
                                                            '³í‚È‚Ì‚ÅƒRƒ}ƒ“ƒhÀs
                                                            gsSendCommand("spawnpoint " & strPlayerID & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                            gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to x:" & strSplitMsg(intCommandOffset + 1) & " y:" & strSplitMsg(intCommandOffset + 2) & " z:" & strSplitMsg(intCommandOffset + 3) & ".")
                                                        Case 1 'x‚ª®”‚Å–³‚¢
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " x point is not invalid.")
                                                        Case 2 'y‚ª®”‚Å–³‚¢
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " y point is not invalid.")
                                                        Case 3 'z‚ª®”‚Å–³‚¢
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " z point is not invalid.")
                                                        Case Else 'Œ^ƒ`ƒFƒbƒN‚ÉƒGƒ‰[
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
                                                        '“V‹Cƒ‚[ƒh‚ªclear/rain/thunder‚Ì‚Ì‚İÀs‚ğ’Ê‚·
                                                        gsSendCommand("weather " & strSplitMsg(intCommandOffset + 1))
                                                        gsSendCommand("tell " & strPlayerID & " Changing to " & strSplitMsg(intCommandOffset + 1) & " weather")
                                                    Else
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    End If
                                                Case intCommandOffset + 3
                                                    Dim t As Integer = 0
                                                    If Integer.TryParse(strSplitMsg(intCommandOffset + 2), t) = False Then '•bw’è‚ª®”‚©
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    Else
                                                        If t >= 1 And t <= 1000000 Then '•bw’è‚ª”ÍˆÍ“à‚©
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
                                            '—\‚ßƒRƒ}ƒ“ƒh‚Ì‘¶İ”»’è‚ğpfGetPermission‚Ås‚Á‚Ä‚é‚Ì‚Å‚±‚±‚É‚Í—ˆ‚È‚¢

                                    End Select

                                Case 0 'ƒp[ƒ~ƒbƒVƒ‡ƒ“‚È‚µ
                                    gsSendCommand("tell " & strPlayerID & " You don't have " & strCommand & " Command Permission.")
                                    pfWriteSystemLog(strPlayerID & " don't have " & strCommand & " Command Permission.", Color.Red)

                                Case -1 'ƒRƒ}ƒ“ƒh‚ª‘¶İ‚µ‚È‚¢
                                    gsSendCommand("tell " & strPlayerID & " " & strCommand & " Command Not Found.")
                                    pfWriteSystemLog(strCommand & "Command Not Found.", Color.Red)
                            End Select
                        End If
                    Else
                        'Prefix Character‚Ìw’è•s”õ
                        pfWriteSystemLog("Prefix Character is Invalid.", Color.Red)
                    End If

                End If
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'x:y:z‚ª³‚µ‚¢À•W‚©ƒ`ƒFƒbƒN‚·‚é
    Private Function pfCheckXYZ(x As String, y As String, z As String) As Integer
        Try
            Dim t As Integer = 0
            If Integer.TryParse(x, t) = False Then
                Return 1 'x‚ª®”’l‚Å–³‚¢
            End If
            If Integer.TryParse(y, t) = False Then
                Return 2 'y‚ª®”’l‚Å–³‚¢
            End If
            If Integer.TryParse(z, t) = False Then
                Return 3 'z‚ª®”’l‚Å–³‚¢
            End If
            Return 0 '‘S•”®”’l

        Catch ex As Exception
            Return -1 '•s–¾‚ÈƒGƒ‰[
        End Try

    End Function

    'SpawnPoint‚ÌŒ‹‰ÊƒƒO‚©‚çAƒZƒbƒg‚³‚ê‚½À•W‚ğæ“¾A•Û‘¶‚·‚é
    Private Function pfPlayerSpawnPointCheck(ByVal msg As String) As Boolean
        'ƒ†[ƒU[‚ªƒRƒ}ƒ“ƒhÀsŒ‚ğ‚¿AspawnpointƒRƒ}ƒ“ƒh‚ğÀs‚µ‚½Œ‹‰Ê
        '2013-01-13 15:10:56 [INFO] [miyabi9821: Set miyabi9821's spawn point to (-240, 63, -14)]
        '0          1        2      3            4   5            6     7     8   9     10  11

        'ƒT[ƒoƒRƒ“ƒ\[ƒ‹‚©‚ç“Á’è‚Ìƒ†[ƒU[‚É‘Î‚µAspawnpoint‚ğÀs‚µ‚½Œ‹‰ÊiMCSC‚ÌPermission‹@”\‚Å‚à‚±‚¿‚çj
        '2013-06-09 05:23:21 [INFO] Set miyabi9821's spawn point to (-127, 71, 291)
        '0          1        2      3   4            5     6     7   8     9  10
        Try
            Dim strSplitMsg As String() = msg.Split(" ")
            If strSplitMsg.Length < 11 Or strSplitMsg.Length > 12 Then
                '—v‘f”‚ª11‚æ‚è¬‚³‚¢‚©A12‚æ‚è‘å‚«‚¢ê‡‚ÍŠY“–ƒƒO‚Å‚Í–³‚¢
                Exit Function
            End If

            If strSplitMsg(2) <> "[INFO]" Then
                '3‚Â‚ß‚Ì—v‘f‚ª[INFO]‚¶‚á‚È‚¢‚È‚çŠY“–ƒƒO‚Å‚Í–³‚¢
                Exit Function
            End If

            'Set xxx's spawn pointƒƒbƒZ[ƒW‚È‚çspawnpoint‚ÌÀsŒ‹‰Ê‚Æ‚µ‚ÄÀ•Wæ“¾i‚¿‚å‚Á‚Æ‚â‚Á‚Â‚¯À‘•j
            If strSplitMsg(4) = "Set" And strSplitMsg(6) = "spawn" And strSplitMsg(7) = "point" Then
                'ƒ†[ƒU[‚ªspawnpointƒRƒ}ƒ“ƒh‚ğÀs‚µ‚½ê‡
                Dim strPlayerID() As String = strSplitMsg(5).Split("'")
                Dim strX As String = strSplitMsg(9).Substring(1, strSplitMsg(9).Length - 2)
                Dim strY As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 1)
                Dim strZ As String = strSplitMsg(11).Substring(0, strSplitMsg(11).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    'æ“¾‚µ‚æ‚¤‚Æ‚µ‚½XYZÀ•W‚ªˆÙí
                    pfWriteSystemLog("Cannot get spawnpoint. PlayerID:" & strPlayerID(0) & " x:" & strX & " y:" & strY & " z:" & strZ, Color.Red)
                Else
                    If pfUpdatePlayerSpawnPoint(strPlayerID(0), strX & "," & strY & "," & strZ) = False Then
                        pfWriteSystemLog("Player Information(spawn point) Update Failure.", Color.Red)
                    End If
                End If

            ElseIf strSplitMsg(3) = "Set" And strSplitMsg(5) = "spawn" And strSplitMsg(6) = "point" Then
                'ƒT[ƒoƒRƒ“ƒ\[ƒ‹‚©‚çƒ†[ƒU[w’è‚ÅspawnpointƒRƒ}ƒ“ƒh‚ğÀs‚µ‚½ê‡
                Dim strPlayerID() As String = strSplitMsg(4).Split("'")
                Dim strX As String = strSplitMsg(8).Substring(1, strSplitMsg(8).Length - 2)
                Dim strY As String = strSplitMsg(9).Substring(0, strSplitMsg(9).Length - 1)
                Dim strZ As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    'æ“¾‚µ‚æ‚¤‚Æ‚µ‚½XYZÀ•W‚ªˆÙí
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

    'ƒRƒ}ƒ“ƒh‚ÆPlayerID‚ğw’è‚·‚é‚ÆAƒp[ƒ~ƒbƒVƒ‡ƒ“‚ğ‚Á‚Ä‚¢‚é‚©‚ğ”»’è‚·‚é
    '–ß‚è’l@1:ƒp[ƒ~ƒbƒVƒ‡ƒ“‚ ‚èA0:ƒp[ƒ~ƒbƒVƒ‡ƒ“–³‚µA-1:ƒRƒ}ƒ“ƒh‚ª‘¶İ‚µ‚È‚¢
    Private Function pfGetPermission(ByVal cmd As String, ByVal id As String) As Integer
        With PermissionSettings.Instance
            Select Case cmd
                Case "tp"
                    If .TpEnabled = True Then 'Permission—LŒø
                        If .TpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "give"
                    If .GiveEnabled = True Then 'Permission—LŒø
                        If .GiveMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GiveSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "time"
                    If .TimeEnabled = True Then 'Permission—LŒø
                        If .TimeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TimeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "xp"
                    If .XpEnabled = True Then 'Permission—LŒø
                        If .XpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .XpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "gamemode"
                    If .GamemodeEnabled = True Then 'Permission—LŒø
                        If .GamemodeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GamemodeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "kick"
                    If .KickEnabled = True Then 'Permission—LŒø
                        If .KickMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .KickSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "ban"
                    If .BanEnabled = True Then 'Permission—LŒø
                        If .BanMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .BanSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "pardon"
                    If .PardonEnabled = True Then 'Permission—LŒø
                        If .PardonMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .PardonSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "whitelist"
                    If .WhitelistEnabled = True Then 'Permission—LŒø
                        If .WhitelistMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WhitelistSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "spawnpoint"
                    If .SpawnpointEnabled = True Then 'Permission—LŒø
                        If .SpawnpointMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .SpawnpointSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case "weather"
                    If .WeatherEnabled = True Then 'Permission—LŒø
                        If .WeatherMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WeatherSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚½
                                End If
                            Next
                            Return 0 'w’èƒ†[ƒU[‚É‘¶İ‚µ‚È‚¢
                        End If
                    Else 'Permission–³Œø
                        Return 0
                    End If

                Case Else
                    Return -1
            End Select

        End With
    End Function

    'ƒvƒŒƒCƒ„[‚ªƒIƒ“ƒ‰ƒCƒ“‚©ƒ`ƒFƒbƒN
    Private Function pfIsPlayerOnline(ByVal id As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                'ƒvƒŒƒCƒ„[ˆê——‚Ì’†‚ÉƒvƒŒƒCƒ„[‚ğ”­Œ©
                If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    Return True 'ƒIƒ“ƒ‰ƒCƒ“ó‘Ô
                Else
                    Return False 'ƒIƒtƒ‰ƒCƒ“ó‘Ô
                End If
            End If
        Next

        Return False 'ƒvƒŒƒCƒ„[ˆê——‚É‘¶İ‚µ‚È‚¢(ƒƒOƒCƒ“‚µ‚½‚±‚Æ‚ª‚È‚¢)
    End Function

    'HeartBeatƒƒO‚ğo—Í‚µ‚È‚¢
    '1.6‚©‚çend of stream‚Ìo—Í‚ào‚é‚½‚ßA‚±‚ê‚à—}§‚·‚é
    '2012-07-27 20:29:49 [INFO] /127.0.0.1:xxxxx lost connection
    '2013-07-28 08:37:28 [SEVERE] Reached end of stream for /127.0.0.1
    Private Function CheckHeartBeatLog(ByVal msg As String) As Boolean
        If Settings.Instance.ServerVersion >= 3 Then
            '1.7ˆÈ~‚Í‚±‚ÌƒƒO‚ªo‚È‚¢‚æ‚¤‚È‚Ì‚Å‘¦”²‚¯
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

    'ƒEƒBƒ“ƒhƒEˆÊ’u‚ğ•Û‘¶
    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        Settings.Instance.WindowPos = Me.Location
    End Sub


    'ƒEƒBƒ“ƒhƒEƒTƒCƒY•ÏX
    Private Sub frmMain_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'ƒIƒuƒWƒFƒNƒgƒTƒCƒY•ÏX
        psObjectSizeChange()

        'ƒEƒBƒ“ƒhƒEƒTƒCƒY‚Ì•Û‘¶
        If Me.WindowState = FormWindowState.Normal Then
            'ƒm[ƒ}ƒ‹‚Ì‚Ì‚İ•Û‘¶iÅ‘å‰»AÅ¬‰»‚Ì‚Í•Û‘¶‚µ‚È‚¢j
            Settings.Instance.WindowSize = New Point(Me.Width, Me.Height)
        End If

    End Sub

    'ƒEƒBƒ“ƒhƒEƒTƒCƒY‚ª•ÏX‚³‚ê‚½‚Æ‚«‚È‚ÇA‰æ–ÊƒIƒuƒWƒFƒNƒg‚ÌƒTƒCƒY‚ğ•ÏX‚·‚é
    Private Sub psObjectSizeChange()
        'ƒEƒBƒ“ƒhƒEƒTƒCƒY‚ÌŠî–{‚Í800x600‚Æ‚·‚é
        Dim AddWidth As Integer = Me.Width - 800
        Dim AddHeight As Integer = Me.Height - 600

        'cƒ|ƒWƒVƒ‡ƒ“•ÏX
        gbSVController.Top = 420 + AddHeight

        If ExtendPlayersListAreaToolStripMenuItem.Checked = True Then
            'Extend Players List Area‚Éƒ`ƒFƒbƒN‚ª“ü‚Á‚Ä‚½‚çAƒvƒŒƒCƒ„[ˆê——‚Ì‰¡•‚ğ‘å‚«‚­æ‚é

            '‰¡ƒTƒCƒY•ÏX
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297 + 457 + AddWidth
            lvPlayers.Width = 285 + 457 + AddWidth

            'cƒTƒCƒY•ÏX
            gbLog.Height = 385 - 229
            rtbServerLog.Height = 361 - 229
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayers‚ÌƒJƒ‰ƒ€•
            lvPlayers.Columns(0).Width = 150     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 50     'Online
            lvPlayers.Columns(3).Width = 120    'LastLogin
            lvPlayers.Columns(4).Width = 120    'LastLogout
            lvPlayers.Columns(5).Width = 80     'Spawn
            lvPlayers.Columns(6).Width = 80     'BadCount

        Else
            ']—ˆ’Ê‚è

            '‰¡ƒTƒCƒY•ÏX
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297
            lvPlayers.Width = 285

            'cƒTƒCƒY•ÏX
            gbLog.Height = 385 + AddHeight
            rtbServerLog.Height = 361 + AddHeight
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayers‚ÌƒJƒ‰ƒ€•
            lvPlayers.Columns(0).Width = 100     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 30     'Online
            lvPlayers.Columns(3).Width = 5      'LastLogin
            lvPlayers.Columns(4).Width = 5      'LastLogout
            lvPlayers.Columns(5).Width = 5      'Spawn
            lvPlayers.Columns(6).Width = 5      'BadCount

        End If

        'ƒT[ƒoƒƒO‚ÌƒJ[ƒ\ƒ‹ˆÊ’u‚ğÅŒã‚ÉˆÚ“®
        rtbServerLog.SelectionStart = rtbServerLog.Text.Length
        rtbServerLog.Focus()
        rtbServerLog.ScrollToCaret()

    End Sub

    'ƒvƒƒZƒX‚Ì‹­§I—¹
    Private Sub btnKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKill.Click
        Try
            If MessageBox.Show("ƒT[ƒo‚ğ‹­§I—¹‚³‚¹‚Ü‚·B" & vbCrLf & _
                               "ƒf[ƒ^‚ª”j‘¹‚·‚é‰Â”\«‚ª‚ ‚èA’Êí‚Ì’â~ˆ—‚Å~‚Ü‚ç‚È‚¢‚Æ‚«‚Ì‚İÀs‚µ‚Ä‰º‚³‚¢B" & vbCrLf & _
                               "‘±s‚µ‚Ü‚·‚©H", "Œx", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            pfWriteSystemLog("Minecraft Server Process Kill Execute.", Color.Red)
            gblnExitFlg = True 'I—¹ƒtƒ‰ƒO‚ğİ’è
            mcsProc.Kill()
        Catch ex As Exception

        End Try

    End Sub

    'ƒ†[ƒU[‚ÌÚ×‚ğ•\¦‚·‚é
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
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint’Ç‰Á
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint’Ç‰Á‚É‚Â‚«ˆêŒÂŒã‚ë‚ÉˆÚ“®

        My.Forms.frmPlayerInfo.Show()
    End Sub

    'ƒ†[ƒU[‚ğ‹­§“I‚ÉØ’f‚·‚éƒRƒ}ƒ“ƒh‚ğ”­s‚·‚é
    Private Sub KickPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        'kick [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("kick " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ƒ†[ƒU‚ğƒzƒƒCƒgƒŠƒXƒg‚É’Ç‰Á‚·‚éƒRƒ}ƒ“ƒh‚ğ”­s‚·‚é
    Private Sub AddWhiteListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWhiteListToolStripMenuItem.Click
        'whitelist add [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("whitelist add " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ƒ†[ƒU[‚ğPlayer BANƒŠƒXƒg‚É’Ç‰Á‚·‚éƒRƒ}ƒ“ƒh‚ğ”­s‚·‚é
    Private Sub AddPlayerBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPlayerBANListToolStripMenuItem.Click
        'ban [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("ban " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ƒ†[ƒU[‚ÌIP‚ğIP BANƒŠƒXƒg‚É’Ç‰Á‚·‚éƒRƒ}ƒ“ƒh‚ğ”­s‚·‚é
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

    'IP—pƒRƒ“ƒeƒLƒXƒgƒƒjƒ…[‚ªŠJ‚¢‚½‚Æ‚«
    Private Sub cmsIP_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsIP.Opening
        Dim menu As ContextMenuStrip = CType(sender, ContextMenuStrip)
        pstrCmsIPCalledObject = menu.SourceControl.Name
    End Sub

    Private Sub CopyToClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToClipboardToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                'ƒOƒ[ƒoƒ‹IPƒAƒhƒŒƒX‚ğƒNƒŠƒbƒvƒ{[ƒh‚ÉƒRƒs[
                System.Windows.Forms.Clipboard.SetText(lblGlobalIP.Text)
            Case Me.lblPrivateIP.Name
                'ƒvƒ‰ƒCƒx[ƒgIPƒAƒhƒŒƒX‚ğƒNƒŠƒbƒvƒ{[ƒh‚ÉƒRƒs[
                System.Windows.Forms.Clipboard.SetText(lblPrivateIP.Text)
        End Select

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                'ƒOƒ[ƒoƒ‹IPƒAƒhƒŒƒXÄæ“¾
                lblGlobalIP.Text = gfGetGlobalIP()
            Case Me.lblPrivateIP.Name
                'ƒvƒ‰ƒCƒx[ƒgIPƒAƒhƒŒƒXÄæ“¾
                lblPrivateIP.Text = gfGetPrivateIP()
        End Select
    End Sub


    Private Sub cmsPlayers_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsPlayers.Opening
        '‘I‘ğ‚³‚ê‚Ä‚¢‚È‚©‚Á‚½‚çƒLƒƒƒ“ƒZƒ‹
        If lvPlayers.SelectedItems.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If btnRun.Enabled = True Then
            'ƒT[ƒo‚ª‹N“®‚³‚ê‚Ä‚¢‚È‚¢ê‡AÚ×ˆÈŠOg—p•s‰Â
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
        'ƒXƒPƒWƒ…[ƒ‰‹N“®
        frmScheduler.Show()
    End Sub

    'ƒvƒŒƒCƒ„[ˆê——‚Ì•À‚Ñ‘Ö‚¦
    'QlFhttp://natchan-develop.seesaa.net/article/141920783.html
    Private Sub lvPlayers_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvPlayers.ColumnClick
        Static sIntColNo(lvPlayers.Columns.Count - 1) As Integer  '—ñ‚Ìƒ\[ƒgó‘Ô•Û—p
        If sIntColNo(e.Column) = 0 Then
            '‰‰ñ‚Ü‚½‚Í¸‡
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 0)
            sIntColNo(e.Column) = 1    'Ÿ‰ñ‚Í~‡
        Else
            '~‡
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 1)
            sIntColNo(e.Column) = 0    'Ÿ‰ñ‚Í¸‡
        End If

    End Sub

    'ƒ_ƒuƒ‹ƒNƒŠƒbƒN‚Å‘I‘ğƒvƒŒƒCƒ„[‚ÌÚ×•\¦
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
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint’Ç‰Á
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint’Ç‰Á‚É‚Â‚«ˆêŒÂŒã‚ë‚ÉˆÚ“®

        My.Forms.frmPlayerInfo.Show()
        My.Forms.frmPlayerInfo.Activate()
    End Sub


    'lvPlayers‚Ìƒf[ƒ^‚ğ•Û‘¶A“Ç‚·‚é
    'QlFhttp://www.knowdotnet.com/articles/serializationoflistviewtoxml.html
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
                    'I—¹‚·‚é‚Ì‚ÅƒIƒ“ƒ‰ƒCƒ“ƒXƒe[ƒ^ƒX‚ğ~‚É•ÏX‚·‚é
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
                '“ü—Í‚ª•ª‚È‚Ì‚ÅAƒ~ƒŠ•bw’è‚Åİ’è
                timBackup.Interval = Settings.Instance.BackupInterval * 60 * 1000

                'ƒT[ƒo‹N“®’†‚Ì‚İƒ^ƒCƒ}[‚ğ—LŒø‚É‚·‚é
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

    'ƒ^ƒCƒ}[‚É‚æ‚è’èŠúƒoƒbƒNƒAƒbƒv‚ğÀs
    Private Sub timBackup_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBackup.Tick
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
            Exit Sub
        End If

        pfWriteSystemLog("Auto-Backup Started.", Color.Black)

        'ƒoƒbƒNƒAƒbƒvÀs
        Dim strRetMsg As String = "" 'ƒoƒbƒNƒAƒbƒvˆ—‚©‚ç–ß‚³‚ê‚éƒƒbƒZ[ƒW
        If gfBackup(strRetMsg) = True Then
            pfWriteSystemLog(strRetMsg, Color.Blue)
        Else
            pfWriteSystemLog(strRetMsg, Color.Red)
        End If

        'Ÿ‰ñƒoƒbƒNƒAƒbƒvŠÔ‚Ì•\¦XV
        lblNextTime.Text = DateAdd(DateInterval.Minute, _
                        Settings.Instance.BackupInterval, _
                        DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")

    End Sub

    'è“®ƒoƒbƒNƒAƒbƒv
    Private Sub DataBackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataBackupToolStripMenuItem.Click
        'ƒoƒbƒNƒAƒbƒv‚Ìİ’èŠm”Fˆ—
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            MessageBox.Show("ƒoƒbƒNƒAƒbƒv‘ÎÛƒtƒHƒ‹ƒ_‚ª\¬‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB" & vbCrLf & "İ’è‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "ƒGƒ‰[", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If System.IO.Directory.Exists(Settings.Instance.BackupOutput) = False Then
            MessageBox.Show("w’è‚³‚ê‚½o—Íæ‚ª‘¶İ‚µ‚Ü‚¹‚ñB" & vbCrLf & "İ’è‚ğŠm”F‚µ‚Ä‰º‚³‚¢B", "ƒGƒ‰[", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If

        'ÀsŠm”F
        If MessageBox.Show("è“®ƒoƒbƒNƒAƒbƒv‚ğÀs‚µ‚Ü‚·‚©H", "Šm”F", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        pfWriteSystemLog("Manual-Backup Started.", Color.Black)

        'ƒoƒbƒNƒAƒbƒvÀs
        Dim strRetMsg As String = ""
        If gfBackup(strRetMsg, True) = False Then
            pfWriteSystemLog(strRetMsg, Color.Red)
            Exit Sub
        End If

        pfWriteSystemLog(strRetMsg, Color.Blue)

    End Sub

    'ƒT[ƒoƒ|[ƒg‚É‘Î‚µAHeartBeatŠm”F‚ğs‚¤
    Private Sub timHB_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timHB.Tick
        'ƒ|[ƒg”Ô†‚ªŠi”[‚³‚ê‚é•Ï”
        Static intErrCnt As Integer

        If pstrPort = "" Then
            'server.properties‚Ì“ÇŠm”F
            If ghtServerProperties Is Nothing Then
                If gfLoadServerProp() = False Then
                    pfWriteSystemLog("Cannot Read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
                    Exit Sub
                End If
            End If

            'ƒ|[ƒg”Ô†æ“¾
            If gfGetServerPropValue("server-port", pstrPort) = False Then
                pfWriteSystemLog("Server port number unknown.", Color.Red)
                Exit Sub
            End If
        End If

        'SocketÚ‘±
        Dim IPEPLocal As New System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), CInt(pstrPort))
        Dim sockLocal As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
        Try
            sockLocal.ReceiveTimeout = 5000 'Å‘å5•b
            sockLocal.Connect(IPEPLocal)
            If sockLocal.Connected Then
                If Settings.Instance.HeartBeatUse0xFE = True Then
                    '2012/10/28 Ú‘±l”‚È‚Ç‚ğæ“¾‚·‚é‚æ‚¤•ÏX
                    '2012/11/03 2•b‚Ù‚ÇƒAƒvƒŠ‚ªŒÅ‚Ü‚é‚Ì‚ÅAƒIƒvƒVƒ‡ƒ“‚Å‘I‚×‚é‚æ‚¤‚É•ÏX

                    '‘—M(0xFE)
                    Dim btSendMsg(1) As Byte
                    btSendMsg(0) = &HFE
                    Try
                        sockLocal.Send(btSendMsg, btSendMsg.Length, Net.Sockets.SocketFlags.None)

                        'óM
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

                        '2ƒoƒCƒg–ÚˆÈ~‚ğØ‚èo‚µAUTF-16BE‚É•ÏŠ·
                        Dim resMsg As String = _
                            System.Text.Encoding.GetEncoding("utf-16be").GetString(mem.GetBuffer(), 3, CInt(mem.Length) - 3)
                        mem.Close()

                        '0xA7‚Å•ªŠ„‚·‚éimotdAÚ‘±ƒvƒŒƒCƒ„[”AÅ‘åƒvƒŒƒCƒ„[”j
                        Dim resInfo As String() = resMsg.Split("˜")
                        lblConnected.Text = resInfo(1) & " / " & resInfo(2)

                    Catch ex As Exception
                        pfWriteSystemLog("HeartBeat(0xFE) Failure. Error Count:" & intErrCnt.ToString, Color.Red)
                        lblConnected.Text = "Get Error."

                    End Try

                End If

                '³í‚ÉÚ‘±o—ˆ‚½‚Ì‚ÅƒGƒ‰[ƒJƒEƒ“ƒg‚ğ0‚É‚µ‚Äƒ\ƒPƒbƒg‚ğ•Â‚¶‚é
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

        'ƒI[ƒgƒŠƒJƒoƒŠ–³Œø‚È‚çI‚í‚è
        If Settings.Instance.AutoRecovery = False Then
            Exit Sub
        End If

        '‹K’è‚ÌƒGƒ‰[è‡’l‚ğ’´‚¦‚½‚ç
        If intErrCnt >= Settings.Instance.HeartBeatStopCount And intErrCnt < Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Auto-Recovery Start.", Color.Red)
            'ƒT[ƒoÄ‹N“®ˆ—
            gsSendCommand("stop")
        ElseIf intErrCnt >= Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Force Auto-Recovery Start.", Color.Red)
            'ƒT[ƒoƒvƒƒZƒX‹­§I—¹
            mcsProc.Kill()
        End If

    End Sub

    'ƒ[ƒ‹’Ê’mİ’è‰æ–Ê‚ğŠJ‚­
    Private Sub SendMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMailToolStripMenuItem.Click
        frmMail.Show()
    End Sub

    'w’è‚³‚ê‚½ƒvƒŒƒCƒ„[‚ÌBadCount‚ğ‘‚«Š·‚¦‚é
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

    'listƒRƒ}ƒ“ƒh‚ğ”­s‚µAƒvƒŒƒCƒ„[ˆê——‚ÌƒXƒe[ƒ^ƒX‚ğXV‚·‚é
    Private Sub UpdatePlayerListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdatePlayerListToolStripMenuItem.Click
        gsSendCommand("list")
    End Sub

    'ÅVƒo[ƒWƒ‡ƒ“‚ğŠm”F‚µAƒƒbƒZ[ƒW‚ğo—Í‚·‚é
    Private Sub CheckLatestVersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckLatestVersionToolStripMenuItem.Click
        'ƒo[ƒWƒ‡ƒ“ƒ`ƒFƒbƒN
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black

        Dim blnRet As Boolean = gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)
        If blnRet = True Then
            If MessageBox.Show("Œ»İg—p’†‚Ìƒo[ƒWƒ‡ƒ“‚æ‚èV‚µ‚¢ƒo[ƒWƒ‡ƒ“‚ªƒŠƒŠ[ƒX‚³‚ê‚Ä‚¢‚Ü‚·B" & vbCrLf & "WebƒTƒCƒg‚ğ•\¦‚µŠm”F‚µ‚Ü‚·‚©H", _
                                "Vƒo[ƒWƒ‡ƒ“ŒŸo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'Šù’è‚Ìƒuƒ‰ƒEƒU‚ÅWebƒTƒCƒg‚ğŠJ‚­
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
            'ƒ`ƒFƒbƒN‚ğ•t‚¯‚ÄAƒvƒŒƒCƒ„[ˆê——•\¦ƒGƒŠƒA‚ğŠg’£‚·‚é
            ExtendPlayersListAreaToolStripMenuItem.Checked = True
            psObjectSizeChange()
        Else
            'ƒ`ƒFƒbƒN‚ğŠO‚µ‚ÄAƒfƒtƒHƒ‹ƒgó‘Ô‚É–ß‚·
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

    'ƒvƒŒƒCƒ„[‚ÌƒXƒ|[ƒ“ƒ|ƒCƒ“ƒg•\¦‚ğXV
    Private Function pfUpdatePlayerSpawnPoint(ByVal id As String, xyz As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                'ƒvƒŒƒCƒ„[ˆê——‚Ì’†‚ÉƒvƒŒƒCƒ„[‚ğ”­Œ©
                lvPlayers.Items(iList).SubItems(5).Text = xyz
                Return True 'Spawn Point‚ğXV‚µ‚ÄI—¹
            End If
        Next

        Return False 'ƒvƒŒƒCƒ„[ˆê——‚É‘¶İ‚µ‚È‚¢i‚ ‚è‚¦‚È‚¢Hj
    End Function

    'ƒvƒŒƒCƒ„[ˆê——‚ÌƒNƒŠƒA
    Private Sub ClearPlayerListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearPlayerListToolStripMenuItem.Click
        If MessageBox.Show("Clear Player List?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        'ƒNƒŠƒAˆ—
        lvPlayers.Items.Clear()
    End Sub


    '*** frmChatLog‚Éƒ`ƒƒƒbƒgƒƒO‚ğ‘—M ***
    Private Function ExpertChatLog(ByVal msg As String) As Boolean
        Try
            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4ˆÈ‘O

                Dim strSplitMsg As String() = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '—v‘f”‚ª4ˆÈ‰º‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3‚Â‚ß‚Ì—v‘f‚ª[INFO]‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    '4‚Â‚ß‚Ì—v‘f‚ª(\[.*?\])?<.*?>‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    If strSplitMsg(3).Contains("[Server]") = True Then
                        '[Server]‚ğŠÜ‚Ş‚È‚çƒT[ƒo[‚©‚ç‚Ìƒ`ƒƒƒbƒg
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* ‚È‚çƒƒOƒCƒ“ƒƒbƒZ[ƒW
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " ‚³‚ñ‚ªƒƒOƒCƒ“‚µ‚Ü‚µ‚½B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* ‚È‚çƒƒOƒAƒEƒgƒƒbƒZ[ƒW
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " ‚³‚ñ‚ªƒƒOƒAƒEƒg‚µ‚Ü‚µ‚½B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) ‚È‚çƒRƒ}ƒ“ƒhg—pƒƒO
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 ‚³‚ñ‚ª $3 ƒRƒ}ƒ“ƒh‚ğg—p‚µ‚Ü‚µ‚½B") & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    Exit Function
                End If

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7
                Dim strSplitMsg As String() = msg.Split(" ")

                '–Ê“|‚ÈƒXƒy[ƒX‚ğ’uŠ·ˆ—
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

                '’uŠ·‚µ‚½ƒXƒy[ƒX‚ğ–ß‚·
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '—v‘f”‚ª3ˆÈ‰º‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2‚Â‚ß‚Ì—v‘f‚ª[Server thread/INFO]:‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (\[.+?\])?(<.+?>) (.*)") Then
                    '4‚Â‚ß‚Ì—v‘f‚ª(\[.*?\])?<.*?>‚¶‚á‚È‚¢‚È‚çƒ`ƒƒƒbƒg‚¶‚á‚È‚¢
                    If strSplitMsg(2).Contains("[Server]") = True Then
                        '[Server]‚ğŠÜ‚Ş‚È‚çƒT[ƒo[‚©‚ç‚Ìƒ`ƒƒƒbƒg
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* ‚È‚çƒƒOƒCƒ“ƒƒbƒZ[ƒW
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " ‚³‚ñ‚ªƒƒOƒCƒ“‚µ‚Ü‚µ‚½B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* ‚È‚çƒƒOƒAƒEƒgƒƒbƒZ[ƒW
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " ‚³‚ñ‚ªƒƒOƒAƒEƒg‚µ‚Ü‚µ‚½B" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) ‚È‚çƒRƒ}ƒ“ƒhg—pƒƒO
                        'ƒ`ƒƒƒbƒgƒƒO‚Éo—Í
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 ‚³‚ñ‚ª $3 ƒRƒ}ƒ“ƒh‚ğg—p‚µ‚Ü‚µ‚½B") & vbCrLf
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
