Public Class frmMain
    Private pstrCmsIPCalledObject As String 'cmsIPを呼び出したオブジェクト名
    Private pstrPort As String = "" '読み込んだ設定ファイルのポート番号
    Public chatting As Boolean = False 'チャットログウィンドウ表示中かどうか
#Region "定数"
    'WM_QUERYENDSESSIONメッセージ
    Private Const WM_QUERYENDSESSION As Integer = &H11

    'WM_POWERBROADCASTメッセージ
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

#Region "オーバーライドメソッド"
    Protected Overrides Sub WndProc( _
     ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_QUERYENDSESSION Then
            'WM_QUERYENDSESSION(シャットダウン)
            pfWriteSystemLog("Shutdown Event detected.", Color.Blue)

            If btnRun.Enabled = False Then
                Try
                    gblnExitFlg = True '正常終了フラグ
                    gsSendCommand("stop")
                Catch ex As Exception
                    pfWriteSystemLog("Command Send Error.", Color.Red)
                End Try
            End If

        ElseIf m.Msg = WM_POWERBROADCAST Then
            'WM_POWERBROADCAST(パワー状態変更)
            pfWriteSystemLog("PowerMode Change Event detected.", Color.Blue)

            If m.WParam.ToInt32 = PBT_APMSUSPEND Or m.WParam.ToInt32 = PBT_APMSTANDBY Then
                'サスペンド、休止状態に入る場合はサーバ停止
                If btnRun.Enabled = False Then
                    Try
                        gblnExitFlg = True '正常終了フラグ
                        gblnResumeFlg = True '復旧フラグ
                        gsSendCommand("stop")
                    Catch ex As Exception
                        pfWriteSystemLog("Command Send Error.", Color.Red)
                    End Try
                End If

            ElseIf m.WParam.ToInt32 = PBT_APMRESUMESUSPEND Or m.WParam.ToInt32 = PBT_APMRESUMESTANDBY Then
                'サスペンド、休止状態から復旧した場合は、停止前にサーバが起動してた場合に限り起動
                If gblnResumeFlg = True Then
                    If pfServerStart() = True Then
                        pfBackupTimerSet() '正常に起動出来たら、バックアップタイマーの状態を設定
                        If Settings.Instance.HeartBeatInterval >= 1 Then
                            'HB間隔が1以上ならHB有効
                            timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                            timHB.Start() 'HB開始
                        End If
                    End If
                    gblnResumeFlg = False '復旧フラグを元に戻す
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
            'Startボタンの状態
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("サーバ設定のファイル指定が正しくありません。" & vbCrLf & "設定画面を開き、パスを確認して下さい。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("サーバ設定のファイル指定が正しくありません。" & vbCrLf & "設定画面を開き、パスを確認して下さい。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    If lblServer.Text <> "Running" Then
                        btnRun.Enabled = True
                    End If
                End If
            End If

            'Connected表示の状態
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

    End Sub

    Private Sub NGWordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NGWordsToolStripMenuItem.Click
        'NGワード設定画面を開く
        frmNGWords.Show()
    End Sub

    Private Sub KickListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        '自動キック設定画面を開く
        frmIPKickBAN.Show()
    End Sub

    '終了処理
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If mcsProc.HasExited = False Then
                MessageBox.Show("終了する場合は先にサーバを停止して下さい", "終了できません", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception

        End Try

        Try
            '最大化状態の確定
            If Me.WindowState = FormWindowState.Maximized Then
                Settings.Instance.WindowMaximize = True
            Else
                Settings.Instance.WindowMaximize = False
            End If

            'Extended Players List Areaの有効状態
            Settings.Instance.ExtendedPlayersListEnabled = ExtendPlayersListAreaToolStripMenuItem.Checked

            'コマンドリストの保存
            Settings.Instance.CommandRecent = New List(Of String)
            For i As Integer = 0 To cmbCommand.Items.Count - 1
                Settings.Instance.CommandRecent.Add(cmbCommand.Items(i))
            Next

            '設定の保存
            Settings.SaveToXmlFile()

            'プレイヤー一覧の保存
            SaveObjectProperties()
        Catch ex As Exception
            MessageBox.Show("設定ファイルの保存に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '旧設定ファイルのリカバリ処置(1.0あたりで削除予定)
        gfMoveOldConfig()

        '設定読み込み
        Try
            Settings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'server.properties読込
        If gfLoadServerProp() = False Then
            pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
        End If

        'プレイヤー一覧読込
        Try
            ReloadListviewFromXML()
        Catch ex As Exception
        End Try

        'NGWords読込
        Try
            'NGWSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        'Permission読込
        Try
            PermissionSettings.LoadFromXmlFile()
        Catch ex As Exception
        End Try

        '別スレッドからの操作を許可する
        Control.CheckForIllegalCrossThreadCalls = False

        'コマンドリストの復元
        For i As Integer = 0 To Settings.Instance.CommandRecent.Count - 1
            cmbCommand.Items.Add(Settings.Instance.CommandRecent.Item(i))
        Next

        With Settings.Instance
            'Startボタンの状態
            If .JarPath = "" OrElse System.IO.File.Exists(.JarPath) = False Then
                MessageBox.Show("サーバ設定のファイル指定が正しくありません。" & vbCrLf & "設定画面を開き、パスを確認して下さい。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnRun.Enabled = False
            Else
                If .JavaPath = "" OrElse System.IO.File.Exists(.JavaPath) = False Then
                    MessageBox.Show("サーバ設定のファイル指定が正しくありません。" & vbCrLf & "設定画面を開き、パスを確認して下さい。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnRun.Enabled = False
                Else
                    btnRun.Enabled = True
                End If
            End If

            'Extended Players Listの有効状態復元
            ExtendPlayersListAreaToolStripMenuItem.Checked = .ExtendedPlayersListEnabled

            'Connected表示の状態
            lblConnectedName.Visible = .HeartBeatUse0xFE
            lblConnected.Visible = .HeartBeatUse0xFE
        End With

        'ウィンドウ位置の復元
        Me.DesktopLocation = Settings.Instance.WindowPos
        If Me.DesktopLocation = New Point(-32000, -32000) Then
            '最小化状態の座標で保存されてたときは強制的に0,0に戻す
            Me.DesktopLocation = New Point(0, 0)
        End If

        'ウィンドウサイズの復元
        Me.Size = Settings.Instance.WindowSize
        '最大化状態の復元
        If Settings.Instance.WindowMaximize = True Then
            Me.WindowState = FormWindowState.Maximized
        End If

        'グローバルIPアドレス取得
        lblGlobalIP.Text = gfGetGlobalIP()
        'プライベートIPアドレス取得
        lblPrivateIP.Text = gfGetPrivateIP()
        '自動バックアップ表示
        If Settings.Instance.BackupEnabled = True Then
            lblDataBackup.Text = "Enabled (stop)"
        End If

        'ログ表示
        pfWriteSystemLog("MCServerController " & GSTR_APP_VERSION & " Startup.", Color.Blue)

        'バージョンチェック
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black
        gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)

        '自動サーバ起動
        If Settings.Instance.AutoStart = True Then
            'オプションが有効だったら、自動的にMCサーバを立ち上げる
            '2012/11/07 自動起動の際、自動バックアップを有効にするのを忘れていたバグを修正
            If pfServerStart() = True Then
                pfBackupTimerSet() '正常に起動出来たら、バックアップタイマーの状態を設定
                If Settings.Instance.HeartBeatInterval >= 1 Then
                    'HB間隔が1以上ならHB有効
                    timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                    timHB.Start() 'HB開始
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
                'メッセージが空なら処理を行わない
                Return True
            End If

            If pfColorCodeTrim(msg) = False Then
                Return False
            End If

            '"[INFO] /127.0.0.1:xxxxx lost connection"のログを出力しない(2012/11/03 HBに0xFEを使用している時は処理しないよう変更)
            If Settings.Instance.HeartBeatUse0xFE = False AndAlso CheckHeartBeatLog(msg) = True Then
                Return True
            End If

            'ユーザのログイン／ログアウトチェック
            pfPlayerInfoUpdate(msg)

            'チャットチェック処理
            pfPlayerChatCheck(msg)

            'チャットログウィンドウに出力
            ExpertChatLog(msg)

            'spawnpointコマンド結果処理
            pfPlayerSpawnPointCheck(msg)

            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            'デフォルトの色変更処理
            If msg.IndexOf("[WARNING]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Orange
            ElseIf msg.IndexOf("[ERROR]") >= 0 Then
                rtbServerLog.SelectionColor = Color.Red
            Else
                rtbServerLog.SelectionColor = Color.Black
            End If

            'カスタムアクション実行

            'ログ出力
            'rtbServerLog.SelectedText = "[" & System.DateTime.Now.ToString & "] " & msg & vbCrLf
            rtbServerLog.SelectedText = msg & vbCrLf
            rtbServerLog.SelectionStart = rtbServerLog.TextLength
            rtbServerLog.ScrollToCaret()
        Catch ex As Exception

        End Try

    End Function

    'ログに出力されるカラーコードを削除する(先頭に特殊文字がある)
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

    'サーバの起動処理
    '参考(プロセス作成)：http://blogs.wankuma.com/naoko/archive/2007/03/09/65823.aspx
    '参考(ファイル監視)：http://dobon.net/vb/dotnet/file/filesystemwatcher.html
    Private Function pfServerStart() As Boolean
        Try
            '2014/07/21 eula.txt対応
            If gfGetEula() = False Then
                pfWriteSystemLog("You must agree to the EULA(eula.txt).", Color.Red)
                gblnExitFlg = True
                Return False
            End If

            '2012/11/03 サーバ起動前にバックアップを取得するオプション追加(自動バックアップ有効時のみ)
            '2012/11/03 Runボタンが有効の時のみ実行するように変更（自動リカバリ処理中はバックアップしない）
            If btnRun.Enabled = True AndAlso Settings.Instance.BackupEnabled = True AndAlso Settings.Instance.BackupBeforeServerRun = True Then
                If Settings.Instance.BackupTarget.Rows.Count = 0 Then
                    pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
                End If

                pfWriteSystemLog("Auto-Backup Started.", Color.Black)

                'バックアップ実行
                Dim strRetMsg As String = "" 'バックアップ処理から戻されるメッセージ
                If gfBackup(strRetMsg) = True Then
                    pfWriteSystemLog(strRetMsg, Color.Blue)
                Else
                    pfWriteSystemLog(strRetMsg, Color.Red)
                End If
            End If

            'server.properties再読込
            If gfLoadServerProp() = False Then
                pfWriteSystemLog("Cannot read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
            End If

            pfWriteSystemLog("Minecraft Server Starting...", Color.Black)

            Dim strWorkingPath = _
                System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)   '作業ディレクトリ取得
            System.IO.Directory.SetCurrentDirectory(strWorkingPath)             'カレントディレクトリ移動

            If Settings.Instance.ServerVersion >= 3 Then
                '1.7以降は.\logs\latest.logを参照＆ログの待避はサーバアプリが自動で行う
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "logs\latest.log")

            ElseIf Settings.Instance.ServerVersion <= 2 Then
                '1.7以前は.\server.logを参照＆ログの待避をMCSCで行う
                gstrLogFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                                         "server.log")

                '今あるserver.logをlogフォルダに移動(+リネーム)
                If System.IO.File.Exists("server.log") = True Then
                    If System.IO.Directory.Exists("log") = False Then
                        'ログバックアップフォルダ作成
                        System.IO.Directory.CreateDirectory("log")
                    End If

                    'server.logの移動(yyyyMMdd-HHmmssをファイル名に付加)
                    System.IO.File.Move("server.log", "log\server.log." & DateTime.Now.ToString("yyyyMMdd-HHmmss"))
                End If
            End If

            '作成するサーバプロセスの詳細を指定
            Dim mcsprocPsInfo As ProcessStartInfo = New ProcessStartInfo
            With mcsprocPsInfo
                '作業ディレクトリ(jarと同じ場所)
                .WorkingDirectory = strWorkingPath
                '起動するJava.exeのパス
                .FileName = Settings.Instance.JavaPath
                '引数 2012/10/17 jarのパスを""でくくるよう変更, 2013/07/28 サーバの起動引数指定に対応
                .Arguments = Settings.Instance.Augment & " -jar " _
                            & """" & Settings.Instance.JarPath & """" & " " & Settings.Instance.JarFileAugment

                If Settings.Instance.ShowConsole = True Then
                    ' コンソールを表示する
                    .CreateNoWindow = False
                    .WindowStyle = ProcessWindowStyle.Normal
                Else
                    ' 新しいウィンドウは作らない
                    .CreateNoWindow = True
                    .WindowStyle = ProcessWindowStyle.Hidden
                End If
            End With

            'プロセスオブジェクトの初期化
            mcsProc = New System.Diagnostics.Process

            With Me.mcsProc
                ' ProcessStartInfo を関連付け
                .StartInfo = mcsprocPsInfo
                ' Process 終了時に Exited イベントを発生させるか否か(既定：False)
                ' OnExited メソッドを使うとプログラムから Exited イベントの発生が可能
                .EnableRaisingEvents = True
                AddHandler .Exited, AddressOf mcsOnProcessExited

                ' 開始
                .Start()
            End With

            'PerformanceCounterのインスタンス設定
            pcCPU.InstanceName = mcsProc.ProcessName
            pcMem.InstanceName = mcsProc.ProcessName
            'もしVista/7でPrivate set -private-を参照するなら、
            'ここでOS判定して切り替えればよいだろうか。


            pfWriteSystemLog("Minecraft Server Startup Success.", Color.Blue)

            'ここまで正常ならタイマーを開始し、ボタンの状態を変更する
            Me.timTick.Interval = 10000 '初回のみログを読込始めるまで10秒待つ(1.7対応)
            Me.timTick.Start()
            btnRun.Enabled = False
            btnStop.Enabled = True
            btnKill.Enabled = True
            cmbCommand.Enabled = True
            UpdatePlayerListToolStripMenuItem.Enabled = True

            'ポート番号リセット
            pstrPort = ""

            '終了フラグをFalseに変更
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

    'サーバの起動ボタン
    Private Sub btnServerRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        'サーバ起動
        If pfServerStart() = True Then
            pfBackupTimerSet() '正常に起動出来たら、バックアップタイマーの状態を設定
            If Settings.Instance.HeartBeatInterval >= 1 Then
                'HB感覚が1以上ならHB有効
                timHB.Interval = Settings.Instance.HeartBeatInterval * 1000
                timHB.Start() 'HB開始
            End If
        End If
    End Sub

    'プロセスが終了したときに実行される
    Private Sub mcsOnProcessExited(ByVal sender As Object, ByVal e As EventArgs)
        Try
            '終了後のserver.log読取り
            pfLogRead()

            'プレイヤーのオンラインステータスを全員オフラインに更新
            pfPlayerLogout("", True)

            'ログ出力
            pfWriteSystemLog("Minecraft Server Shutdown.", Color.Red)

            '自動リカバリ
            If Settings.Instance.AutoRecovery = True Then
                '自動リカバリ有効時
                If gblnExitFlg = False Then
                    '正規の終了ではないので、サーバを再起動する
                    pfWriteSystemLog("Minecraft Server Auto-Recovery Execute.", Color.Red)
                    pfServerStart()

                Else
                    'ボタンの状態変更
                    btnRun.Enabled = True
                    btnStop.Enabled = False
                    btnKill.Enabled = False
                    cmbCommand.Enabled = False
                    UpdatePlayerListToolStripMenuItem.Enabled = False

                    'バックアップ用タイマの設定
                    pfBackupTimerSet()

                    'HB停止
                    timHB.Stop()
                End If

            Else
                '自動リカバリ無効時
                gblnExitFlg = True '終了フラグを有効にする

                'ボタンの状態変更
                btnRun.Enabled = True
                btnStop.Enabled = False
                btnKill.Enabled = False
                cmbCommand.Enabled = False
                UpdatePlayerListToolStripMenuItem.Enabled = False

                'バックアップ用タイマの設定
                pfBackupTimerSet()

                'HB停止
                timHB.Stop()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function pfLogRead() As Boolean
        Static intFileSize As Integer '以前にチェックしたファイルサイズを格納

        'ファイルチェック
        If System.IO.File.Exists(gstrLogFilePath) = False Then
            Return False
        End If

        'ファイルサイズチェック
        Dim fiLog As New System.IO.FileInfo(gstrLogFilePath)
        Try
            If intFileSize = fiLog.Length Then
                'ファイルサイズに変化がなければ処理しない
                Exit Function
            ElseIf intFileSize > fiLog.Length Then
                '記録されてるログサイズの方が大きい場合はログファイルが削除されたとする
                intFileSize = 0
            End If
        Catch ex As Exception
            intFileSize = 0
        End Try

        Try
            'ログファイルを開く
            Dim fsLog As New System.IO.FileStream(gstrLogFilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

            'ログファイルの文字コード判定 2013/12/08
            Dim bsLog(fsLog.Length - 1) As Byte
            fsLog.Read(bsLog, 0, bsLog.Length)
            Dim enc As System.Text.Encoding = GetCode(bsLog)

            Dim srLog As New System.IO.StreamReader(fsLog, enc)
            '既に読み込み済みの所までシーク
            srLog.BaseStream.Seek(intFileSize, IO.SeekOrigin.Begin)

            '追加されたメッセージを1行ずつ読み取り処理実行
            '2013/06/11 listコマンドの処理のため、行の結合処理追加
            Dim strReadBuf As String = String.Empty
            While (srLog.Peek() >= 0)
                strReadBuf = srLog.ReadLine
                If pfListComanndCheck(strReadBuf) = True Then
                    strReadBuf = strReadBuf & " " & srLog.ReadLine
                End If
                pfWriteServerLog(strReadBuf)
                Application.DoEvents()
            End While

            'ログファイルを閉じる
            srLog.Close()
            fsLog.Close()

            '次回読取用オフセット位置を記憶
            intFileSize = fiLog.Length
        Catch ex As Exception
            pfWriteSystemLog("LogFile Read Failure.", Color.Red)
        End Try
    End Function

    '1.3.1以降のlistコマンド実行結果"There are 0/0 players online:"を検出
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
            If timTick.Interval = 10000 Then '10秒を1秒に変更する
                timTick.Interval = 1000
            End If

            'プロセスが終了していたらタイマー停止
            If Me.mcsProc.HasExited = True Then
                Me.timTick.Stop()
                '情報クリア
                lblServer.Text = "Not Running"
                lblUptime.Text = "-"
                lblCPUUsage.Text = "-"
                lblMemUsage.Text = "-"
                'lblConnected.Text = "-"
                Exit Sub
            End If

            'ログ読み込み
            timTick.Enabled = False 'ログ読み取りが終わるまでタイマー停止
            pfLogRead()

            '情報更新
            With mcsProc
                'サーバ状況
                lblServer.Text = "Running"
                'サーバ起動時間
                'lblUptime.Text = New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                Dim tsInterval As TimeSpan = DateTime.Now.Subtract(.StartTime)
                lblUptime.Text = tsInterval.Days & "d " & New DateTime(0).Add(System.DateTime.Now - .StartTime).ToString("HH:mm:ss")
                'CPU使用率
                lblCPUUsage.Text = Math.Round(pcCPU.NextValue / System.Environment.ProcessorCount, 1, MidpointRounding.AwayFromZero).ToString("0.0") & " %"
                '物理メモリ使用量
                lblMemUsage.Text = (pcMem.NextValue / 1024 / 1024).ToString("0.0") & " MB"
            End With

            'スケジューラ処理

        Catch ex As Exception


        Finally
            'タイマー再開（サーバ起動中に限る）
            If timTick.Enabled = False And btnRun.Enabled = False Then
                timTick.Enabled = True
            End If
        End Try

    End Sub

    Private Sub cmbCommand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbCommand.KeyPress
        Try
            'Enterが押され、かつテキストが空でなければ処理実行
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) AndAlso Me.cmbCommand.Text <> "" Then
                '特定の入力の処理
                Select Case Char.ToLower(cmbCommand.Text)
                    Case "stop"
                        'STOPが入力された場合は正常終了開始とする
                        gblnExitFlg = True
                End Select

                'コマンド実行
                gsSendCommand(cmbCommand.Text)

                '実行したコマンドをリストに保存
                If pfSetCommandList(Me.cmbCommand.Text) = False Then
                    'リスト追加エラーのログ表示
                    pfWriteSystemLog("Command List Edit Error", Color.Red)
                End If

                pfWriteSystemLog("Command Send : " & cmbCommand.Text, Color.Black)

                cmbCommand.Text = ""
            End If
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
            '実行したコマンドをリストに保存
            If pfSetCommandList(Me.cmbCommand.Text) = False Then
                'リスト追加エラーのログ表示
                pfWriteSystemLog("Command List Edit Error.", Color.Red)
            End If
            cmbCommand.Text = ""
        End Try

    End Sub

    Private Function pfSetCommandList(ByVal cmd As String) As Boolean
        Try
            If Me.cmbCommand.Items.Count = 0 Then
                'アイテムが存在しない場合は追加して即終了
                Me.cmbCommand.Items.Add(cmd)
                Return True
            End If

            '入力したコマンドがリストに既に存在しないかチェック
            For i As Integer = 0 To Me.cmbCommand.Items.Count - 1
                If Me.cmbCommand.Items(i) = cmd Then
                    '一致するItemが存在した場合は、削除
                    Me.cmbCommand.Items.RemoveAt(i)
                    Exit For
                End If
            Next

            'コマンドをリストに追加
            Me.cmbCommand.Items.Insert(0, cmd)

            'コマンド履歴が20を超えた場合は古い物を削除
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

    'stopボタンを押したときは、stopコマンドを送信する
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Try
            gblnExitFlg = True '正常終了フラグ
            gsSendCommand("stop")
        Catch ex As Exception
            pfWriteSystemLog("Command Send Error.", Color.Red)
        End Try
    End Sub

    Private Sub InformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformationToolStripMenuItem.Click
        'インフォメーション表示
        frmInformation.Show()
        frmInformation.Activate()
    End Sub

    'サーバメッセージを見て、Playerの一覧を更新する
    Private Function pfPlayerInfoUpdate(ByVal msg As String) As Boolean
        Try
            'MCBans連携機能を強制的に無効化 # FOR_DEBUG #
            Settings.Instance.MCBansEnabled = False

            '面倒なスペースを置換処理
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

            '置換したスペースを戻す
            If Settings.Instance.ServerVersion >= 3 Then
                For i As Integer = 0 To strSplitMsg.Length - 1
                    strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                Next
            End If

            If Settings.Instance.ServerVersion >= 3 Then
                '新しいログ形式
                Select Case strSplitMsg(1)
                    Case "[Server thread/INFO]:"
                        '2012/11/03 メッセージの内容が空の場合も終了する
                        If strSplitMsg.Length >= 3 AndAlso strSplitMsg(2).Length = 0 Then
                            Exit Function
                        End If

                        'チャットメッセージの場合は即終了する
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(2)
                            Case "Disconnecting" 'この時点でほぼ確実にBANのメッセージ
                                If strSplitMsg(5) = "You" _
                                            AndAlso strSplitMsg(6) = "are" _
                                            AndAlso strSplitMsg(7) = "not" _
                                            AndAlso strSplitMsg(8) = "white-listed" _
                                            AndAlso strSplitMsg(9) = "on" _
                                            AndAlso strSplitMsg(10) = "this" _
                                            AndAlso strSplitMsg(11) = "server!" Then
                                    'white-listに追加されていない
                                    'Minecraft IDとIPを抜き出す
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
                                    'Player BANされている時
                                    'Minecraft IDとIPを抜き出す
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
                                    'IP BANされている時
                                    'Minecraft IDとIPを抜き出す
                                    Dim strUser = strSplitMsg(3)
                                    Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(3) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'listコマンドが実行されたとき（多分1.2.5まで）
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(3) = "are" _
                                   AndAlso strSplitMsg(5) = "players" _
                                   AndAlso strSplitMsg(6) = "online:" Then
                                    '2013/06/11 1.3.1以降のlistコマンド結果処理対応
                                    '公式サーバでは2行を結合した結果、時刻 [Server thread/INFO]: There are 0/0 players online: 時刻 [Server thread/INFO]: Player1, Player2,...となるので強制処置
                                    'CraftBukkitではそのまま結合して処理してしまえば問題無い
                                    If strSplitMsg.Length >= 10 AndAlso strSplitMsg(8) = "[Server thread/INFO]:" Then '公式サーバ
                                        If pfPlayerUpdateFromList(strSplitMsg, 9) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkitサーバ
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
                                                    'ログインのメッセージ
                                                    'Minecraft IDとIPを抜き出す
                                                    Dim strSplit() As String = strSplitMsg(2).Replace("[", "").Replace("]", "").Split("/")
                                                    Dim strUser = strSplit(0)
                                                    Dim strSplit2() As String = strSplit(1).Split(":")
                                                    Dim strIP = strSplit2(0)

                                                    If Settings.Instance.MCBansEnabled = True Then
                                                        Dim blnStat As Boolean = False
                                                        Dim strReason As String = String.Empty
                                                        Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                            Case -1 '原因不明のエラー
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 0
                                                                If blnStat = False Then '接続NG
                                                                    pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                    gsSendCommand("kick " & strUser) 'kick
                                                                    gsSendCommand("ban " & strUser) 'ban
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                Else '接続OK
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                End If
                                                            Case 1 'online-modeの値が取得出来ない
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 2 'online-modeがfalseだった
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If
                                                            Case 3 'MCBansサーバにから正しくデータが取得出来なかった
                                                                pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                End If

                                                                'failsafe
                                                                If Settings.Instance.MCBansFailSafe = True Then
                                                                    gsSendCommand("kick " & strUser)
                                                                End If
                                                            Case 4 'MCBans連携に必要な接続元IPアドレスが正しく取得出来なかった
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
                '従来通りのログ形式
                Select Case strSplitMsg(2) '[INFO]等の部分
                    Case "[INFO]" '1.6.4までの形式
                        '2012/11/03 メッセージの内容が空の場合も終了する
                        If strSplitMsg.Length >= 4 AndAlso strSplitMsg(3).Length = 0 Then
                            Exit Function
                        End If

                        'チャットメッセージの場合は即終了する
                        If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                            Exit Function
                        End If

                        Select Case strSplitMsg(3)
                            Case "Disconnecting" 'この時点でほぼ確実にBANのメッセージ
                                If strSplitMsg(6) = "You" _
                                            AndAlso strSplitMsg(7) = "are" _
                                            AndAlso strSplitMsg(8) = "not" _
                                            AndAlso strSplitMsg(9) = "white-listed" _
                                            AndAlso strSplitMsg(10) = "on" _
                                            AndAlso strSplitMsg(11) = "this" _
                                            AndAlso strSplitMsg(12) = "server!" Then
                                    'white-listに追加されていない
                                    'Minecraft IDとIPを抜き出す
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
                                    'Player BANされている時
                                    'Minecraft IDとIPを抜き出す
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
                                    'IP BANされている時
                                    'Minecraft IDとIPを抜き出す
                                    Dim strUser = strSplitMsg(4)
                                    Dim strSplit() As String = strSplitMsg(5).Replace("[/", "").Replace("]", "").Split(":")
                                    Dim strIP = strSplit(0)
                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If

                            Case "Connected"
                                If strSplitMsg(4) = "players:" AndAlso strSplitMsg.Length >= 6 Then
                                    'listコマンドが実行されたとき（多分1.2.5まで）
                                    If pfPlayerUpdateFromList(strSplitMsg) = False Then
                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                    End If
                                End If
                            Case "There"
                                If strSplitMsg(4) = "are" _
                                   AndAlso strSplitMsg(6) = "players" _
                                   AndAlso strSplitMsg(7) = "online:" Then
                                    '2013/06/11 1.3.1以降のlistコマンド結果処理対応
                                    '公式サーバでは2行を結合した結果、日付 時刻 [INFO] There are 0/0 players online: 日付 時刻 [INFO] Player1, Player2,...となるので強制処置
                                    'CraftBukkitではそのまま結合して処理してしまえば問題無い
                                    If strSplitMsg.Length >= 11 AndAlso strSplitMsg(10) = "[INFO]" Then '公式サーバ
                                        If pfPlayerUpdateFromList(strSplitMsg, 11) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    Else 'CraftBukkitサーバ
                                        If pfPlayerUpdateFromList(strSplitMsg, 8) = False Then
                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                        End If
                                    End If

                                End If


                            Case Else
                                '配列が3までしかない場合に例外エラーを吐くので対策 2013/07/20
                                If strSplitMsg.Length <= 4 Then
                                    Exit Function
                                End If

                                Select Case strSplitMsg(4)
                                    Case "lost"
                                        If strSplitMsg(5) = "connection:" Then
                                            If strSplitMsg(6).IndexOf("disconnect") >= 0 Then
                                                'ログアウトのメッセージ
                                                'disconnect.quittingやdisconnect.genericReason、disconnect.endOfStream等が存在するので
                                                'disconnectが含まれていれば適用とする
                                                If pfPlayerLogout(strSplitMsg(3)) = False Then
                                                    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                End If
                                            End If
                                        End If

                                    Case Else
                                        If Settings.Instance.ServerVersion >= 1 Then '1.3からの新形式
                                            Select Case strSplitMsg(4)
                                                Case "logged"
                                                    If strSplitMsg(5) = "in" AndAlso strSplitMsg(6) = "with" AndAlso strSplitMsg(7) = "entity" Then
                                                        'ログインのメッセージ
                                                        'Minecraft IDとIPを抜き出す

                                                        '1.3preではUserIDのみだった
                                                        'Dim strUser = strSplitMsg(3)
                                                        'Dim strIP = "0.0.0.0"
                                                        'If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                        '    pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                        'End If

                                                        '1.3.1ではUserID[/IP:Port]と従来の形式からスペースが無くなった
                                                        Dim strSplit() As String = strSplitMsg(3).Replace("[", "").Replace("]", "").Split("/")
                                                        Dim strUser = strSplit(0)
                                                        Dim strSplit2() As String = strSplit(1).Split(":")
                                                        Dim strIP = strSplit2(0)

                                                        '2012/11/11 MCBans連携追加
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 '原因不明のエラー
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then '接続NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else '接続OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-modeの値が取得出来ない
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-modeがfalseだった
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBansサーバにから正しくデータが取得出来なかった
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans連携に必要な接続元IPアドレスが正しく取得出来なかった
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
                                        Else '1.2.5以前の形式
                                            Select Case strSplitMsg(5)
                                                Case "logged"
                                                    If strSplitMsg(6) = "in" AndAlso strSplitMsg(7) = "with" AndAlso strSplitMsg(8) = "entity" Then
                                                        'ログインのメッセージ
                                                        'Minecraft IDとIPを抜き出す
                                                        Dim strUser = strSplitMsg(3)
                                                        Dim strSplit() As String = strSplitMsg(4).Replace("[/", "").Replace("]", "").Split(":")
                                                        Dim strIP = strSplit(0)

                                                        '2012/11/11 MCBans連携追加
                                                        If Settings.Instance.MCBansEnabled = True Then
                                                            Dim blnStat As Boolean = False
                                                            Dim strReason As String = String.Empty
                                                            Select Case gfGetUserStat(blnStat, Settings.Instance.MCBansAPIKey, strUser, strIP, strReason)
                                                                Case -1 '原因不明のエラー
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Unknown Error." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 0
                                                                    If blnStat = False Then '接続NG
                                                                        pfWriteSystemLog(strUser & " is Rejected with MCBans. Reason:" & strReason, Color.Red)
                                                                        gsSendCommand("kick " & strUser) 'kick
                                                                        gsSendCommand("ban " & strUser) 'ban
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_FALUSE_BAN) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If

                                                                    Else '接続OK
                                                                        If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                            pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                        End If
                                                                    End If
                                                                Case 1 'online-modeの値が取得出来ない
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is unknown." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 2 'online-modeがfalseだった
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. Online-Mode is False." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If
                                                                Case 3 'MCBansサーバにから正しくデータが取得出来なかった
                                                                    pfWriteSystemLog(strUser & " can't Valid with MCBans. MCBans response is invalid." & strReason, Color.Red)
                                                                    If pfPlayerLogin(strUser, strIP, GSTR_ONLINE_TRUE) = False Then
                                                                        pfWriteSystemLog("Player Information Update Failure.", Color.Red)
                                                                    End If

                                                                    'failsafe
                                                                    If Settings.Instance.MCBansFailSafe = True Then
                                                                        gsSendCommand("kick " & strUser)
                                                                    End If
                                                                Case 4 'MCBans連携に必要な接続元IPアドレスが正しく取得出来なかった
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

    'プレイヤーログイン時に、プレイヤー情報を更新する
    Private Function pfPlayerLogin(ByVal user As String, ByVal IP As String, ByVal online As String) As Boolean
        Try
            Dim item() As String = {user, IP, online, DateTime.Now, "", "", 0} '2013/06/08 SpawnPoint追加につき修正

            If lvPlayers.Items.Count = 0 Then '1人目なら即追加
                lvPlayers.Items.Add(New ListViewItem(item))
                Return True
            End If

            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    '既にリストに存在する場合、IPとOnlineを更新
                    lvPlayers.Items(i).SubItems(1).Text = IP
                    lvPlayers.Items(i).SubItems(2).Text = online
                    If online = GSTR_ONLINE_TRUE Then 'オンラインになったときのみログイン時間更新
                        lvPlayers.Items(i).SubItems(3).Text = DateTime.Now 'ログイン時間
                    End If
                    Return True
                End If
            Next

            'リストに存在しなければ追加
            lvPlayers.Items.Add(New ListViewItem(item))
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    'プレイヤーログアウト時に、プレイヤー情報を更新する
    '2013/06/09 全員をオフラインに更新するモード追加(ユーザー名は空欄で良い)
    Private Function pfPlayerLogout(ByVal user As String, Optional ByVal allOfflineMode As Boolean = False) As Boolean
        If lvPlayers.Items.Count = 0 Then 'リストが空なら処理しない
            Return True
        End If

        If allOfflineMode = True Then '全員オフライン状態に変更
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                '2014/05/17 ログインしてない人までログアウト時間を更新していたので修正
                If lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now 'ログアウト時間
                End If
            Next

        Else '特定のユーザーの状態を更新
            For i As Integer = 0 To lvPlayers.Items.Count - 1
                If lvPlayers.Items(i).SubItems(0).Text = user Then
                    lvPlayers.Items(i).SubItems(2).Text = GSTR_ONLINE_FALSE
                    lvPlayers.Items(i).SubItems(4).Text = DateTime.Now 'ログアウト時間
                    Return True
                End If
            Next
        End If

    End Function

    'listコマンドの結果からプレイヤーのステータスを更新する
    '2012/11/03 1.3からlistの実行結果表示が変わったため、startIndexを指定できるように変更
    Private Function pfPlayerUpdateFromList(ByVal splitmsg() As String, Optional ByVal startIndex As Integer = 5) As Boolean
        'ログアウトチェック（ログアウト処理が出来てないユーザのオンラインステータスをオフラインに設定）
        Dim blnOnline As Boolean = False
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                'Onlineのユーザのみを対象に検索

                blnOnline = False
                For iPlayer As Integer = startIndex To splitmsg.Length - 1
                    If lvPlayers.Items(iList).SubItems(0).Text = splitmsg(iPlayer).Trim(",") Then
                        blnOnline = True
                        Exit For
                    End If
                Next

                'オンラインのプレイヤー一覧に存在しなければ切断済みとする
                If blnOnline = False Then
                    pfPlayerLogout(lvPlayers.Items(iList).SubItems(0).Text)
                End If
            End If
        Next

        'ログインチェック（ログイン処理が出来てないユーザのオンラインステータスをオンラインに設定）
        Dim blnListed As Boolean = False
        Dim blnUpdate As Boolean = False
        For iPlayer As Integer = startIndex To splitmsg.Length - 1

            blnListed = False
            blnUpdate = False

            For iList As Integer = 0 To lvPlayers.Items.Count - 1
                '空文字なら即スキップ
                If splitmsg(iPlayer).Trim(",") = "" Then
                    Continue For
                End If

                If splitmsg(iPlayer).Trim(",") = lvPlayers.Items(iList).SubItems(0).Text Then
                    'ListViewにユーザーを見つけた
                    If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                        '既にOnlineのユーザなら何もしない
                        blnListed = True
                        blnUpdate = False

                    Else
                        'Online以外ならステータス更新
                        blnListed = True
                        blnUpdate = True

                    End If
                    Continue For
                End If

            Next

            '更新判定
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

    'チャットメッセージの監視
    'NGWordsで指定された文字列を見つけたらbad countの上昇/kick/ban等を行う
    '2012-05-25 08:21:29 [INFO] <USERNAME> Chat Message 形式を監視する
    Private Function pfPlayerChatCheck(ByVal msg As String) As Boolean
        Try
            Dim strSplitMsg As String()
            Dim strPlayerID As String = String.Empty
            Dim strChat As String = String.Empty
            Dim intCommandOffset As Integer = 0

            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4以前
                '2014-05-17 16:21:55 [INFO] <miyabi9821> test

                intCommandOffset = 4

                strSplitMsg = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '要素数が4以下ならチャットじゃない
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3つめの要素が[INFO]じゃないならチャットじゃない
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    'メッセージが\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)じゃないならチャットじゃない
                    Exit Function
                End If

                'PlayerID取得
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?<(.+?)> (.*)", "$2")
                'チャットメッセージ取得
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)", "$3")

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7以降
                '[16:05:19] [Server thread/INFO]: <miyabi9821> test

                intCommandOffset = 3

                '面倒なスペースを置換処理
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

                '置換したスペースを戻す
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '要素数が3以下ならチャットじゃない
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2つめの要素が[Server thread/INFO]:じゃないならチャットじゃない
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)") Then
                    'メッセージがじゃない
                    Exit Function
                End If

                'PlayerID取得
                strPlayerID = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?<(.+?)> (.*)", "$2")
                'チャットメッセージ取得
                strChat = System.Text.RegularExpressions.Regex.Replace(msg, "\[\d\d:\d\d:\d\d\] \[Server thread/INFO\]: (\[.+?\])?(<.+?>) (.*)", "$3")

            End If

            ''*** NG Words処理 ***
            'With NGWSettings.Instance
            '    If .NGWordsEnabled = True Then
            '        Dim intBadCount As Integer = 0
            '        With NGWSettings.Instance.NGWords
            '            For i As Integer = 0 To .Rows.Count - 1
            '                If CBool(.Rows(i)(0)) = True Then
            '                    If strChat.IndexOf(.Rows(i)(1)) >= 0 Then
            '                        'NGWordsがチャット文字列に見つかった場合、BadCount加算
            '                        intBadCount += CInt(.Rows(i)(2))
            '                    End If
            '                End If
            '            Next
            '        End With

            '        'BadCountが0より大きければ
            '        If intBadCount > 0 Then
            '            'プレイヤー一覧から検索
            '            With lvPlayers
            '                For i As Integer = 0 To lvPlayers.Items.Count - 1
            '                    If .Items(i).SubItems(0).Text = strPlayerID Then
            '                        'ID見つけたらBadCount加算
            '                        intBadCount += intBadCount + CInt(.Items(i).SubItems(5).Text)
            '                        .Items(i).SubItems(0).Text = intBadCount.ToString

            '                        'sayコマンドで警告
            '                        gsSendCommand("say " & strPlayerID)

            '                        'BadCountが閾値を超えたらBANコマンド発行
            '                        If intBadCount > 10 Then
            '                            gsSendCommand("ban " & strPlayerID)
            '                        End If
            '                    End If
            '                Next
            '            End With
            '        End If
            '    End If
            'End With

            '*** Permission 処理 ***
            With PermissionSettings.Instance
                If .Enabled = True Then
                    If .PrefixChar.Length = 1 Then
                        'チャットメッセージの1文字目が指定されたPrefix Characterなら処理開始
                        If strChat.Substring(0, 1) = .PrefixChar Then
                            'strSplitMsg(4)がコマンドになってるので、Prefix Characterを抜いたコマンドを切り出す
                            '1.7以降は(3)になってしまったので、固定値ではなく変数化
                            Dim strCommand As String = strSplitMsg(intCommandOffset).Substring(1, strSplitMsg(intCommandOffset).Length - 1)

                            '実行ユーザーがコマンドの実行パーミッションを持っているか
                            Select Case pfGetPermission(strCommand, strPlayerID)
                                Case 1 'パーミッションあり
                                    'コマンド実行処理
                                    Select Case strCommand
                                        Case "tp"
                                            'strSplitMsg 4:Command 5:PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                '指定されたプレイヤーがいるか？
                                                If pfIsPlayerOnline(strSplitMsg(intCommandOffset + 1)) = True Then
                                                    'tp <from PlayerID> <to PlayerID>
                                                    gsSendCommand("tp " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1))
                                                Else
                                                    gsSendCommand("tell " & strPlayerID & " Target Player is not Online.")
                                                End If
                                            Else
                                                'コマンドの使用方法表示
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
                                                        Case 0 '公式サーバ
                                                            'give <PlayerID> <ItemID> <Num> <Damage>
                                                            gsSendCommand("give " & strPlayerID & " " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                        Case Else 'それ以外
                                                            '※BukkitにはDamage指定なし
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num>")
                                                    End Select
                                                Case Else
                                                    Select Case Settings.Instance.ServerMode
                                                        Case 0 '公式サーバ
                                                            gsSendCommand("tell " & strPlayerID & " give comand usage : " & .PrefixChar & "give <ItemID> <Num> <Damage>")
                                                        Case Else 'それ以外
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
                                                'banned-players.txtに存在するかチェック(する予定)

                                                'ban <PlayerID>
                                                gsSendCommand("ban " & strSplitMsg(intCommandOffset + 1))
                                            Else
                                                gsSendCommand("tell " & strPlayerID & " ban comand usage : " & .PrefixChar & "ban <PlayerID>")
                                            End If
                                        Case "pardon"
                                            'strSplitMsg 4:Command 5:Target PlayerID
                                            If strSplitMsg.Length = intCommandOffset + 2 Then
                                                'banned-players.txtに存在するかチェック(する予定)

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
                                                            'サーバ側で実行するとユーザに通知されないので、white-list.txtを読んで通知する
                                                            Dim strWhitelist As String = ""
                                                            'ログファイルを開く
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
                                                            'white-list.txtに存在するかチェック(する予定)

                                                            'whitelist add <PlayerID>
                                                            gsSendCommand("whitelist " & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2))
                                                            gsSendCommand("tell " & strPlayerID & " " & strSplitMsg(intCommandOffset + 2) & " has been registered to the white list.")
                                                        Case "remove"
                                                            'white-list.txtに存在するかチェック(する予定)

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
                                                Case intCommandOffset + 1 'xyzが指定されない場合
                                                    'spawnpoint <PlayerID>
                                                    gsSendCommand("spawnpoint " & strPlayerID)
                                                    gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to current point.")
                                                Case intCommandOffset + 4 'xyzが指定されている場合
                                                    'spawnpoint <PlayerID> <x> <y> <z>
                                                    Select Case pfCheckXYZ(strSplitMsg(intCommandOffset + 1), strSplitMsg(intCommandOffset + 2), strSplitMsg(intCommandOffset + 3))
                                                        Case 0
                                                            '正常なのでコマンド実行
                                                            gsSendCommand("spawnpoint " & strPlayerID & strSplitMsg(intCommandOffset + 1) & " " & strSplitMsg(intCommandOffset + 2) & " " & strSplitMsg(intCommandOffset + 3))
                                                            gsSendCommand("tell " & strPlayerID & " " & "Your spawn point has been set to x:" & strSplitMsg(intCommandOffset + 1) & " y:" & strSplitMsg(intCommandOffset + 2) & " z:" & strSplitMsg(intCommandOffset + 3) & ".")
                                                        Case 1 'xが整数で無い
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " x point is not invalid.")
                                                        Case 2 'yが整数で無い
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " y point is not invalid.")
                                                        Case 3 'zが整数で無い
                                                            gsSendCommand("tell " & strPlayerID & " spawnpoint comand usage : " & .PrefixChar & "spawnpoint <x> <y> <z>")
                                                            gsSendCommand("tell " & strPlayerID & " z point is not invalid.")
                                                        Case Else '型チェック時にエラー
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
                                                        '天気モードがclear/rain/thunderの時のみ実行を通す
                                                        gsSendCommand("weather " & strSplitMsg(intCommandOffset + 1))
                                                        gsSendCommand("tell " & strPlayerID & " Changing to " & strSplitMsg(intCommandOffset + 1) & " weather")
                                                    Else
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    End If
                                                Case intCommandOffset + 3
                                                    Dim t As Integer = 0
                                                    If Integer.TryParse(strSplitMsg(intCommandOffset + 2), t) = False Then '秒指定が整数か
                                                        gsSendCommand("tell " & strPlayerID & " weather comand usage : " & .PrefixChar & "spawnpoint <clear/rain/thunder> <sec.(1-1000000)>")
                                                    Else
                                                        If t >= 1 And t <= 1000000 Then '秒指定が範囲内か
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
                                            '予めコマンドの存在判定をpfGetPermissionで行ってるのでここには来ない

                                    End Select

                                Case 0 'パーミッションなし
                                    gsSendCommand("tell " & strPlayerID & " You don't have " & strCommand & " Command Permission.")
                                    pfWriteSystemLog(strPlayerID & " don't have " & strCommand & " Command Permission.", Color.Red)

                                Case -1 'コマンドが存在しない
                                    gsSendCommand("tell " & strPlayerID & " " & strCommand & " Command Not Found.")
                                    pfWriteSystemLog(strCommand & "Command Not Found.", Color.Red)
                            End Select
                        End If
                    Else
                        'Prefix Characterの指定不備
                        pfWriteSystemLog("Prefix Character is Invalid.", Color.Red)
                    End If

                End If
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'x:y:zが正しい座標かチェックする
    Private Function pfCheckXYZ(x As String, y As String, z As String) As Integer
        Try
            Dim t As Integer = 0
            If Integer.TryParse(x, t) = False Then
                Return 1 'xが整数値で無い
            End If
            If Integer.TryParse(y, t) = False Then
                Return 2 'yが整数値で無い
            End If
            If Integer.TryParse(z, t) = False Then
                Return 3 'zが整数値で無い
            End If
            Return 0 '全部整数値

        Catch ex As Exception
            Return -1 '不明なエラー
        End Try

    End Function

    'SpawnPointの結果ログから、セットされた座標を取得、保存する
    Private Function pfPlayerSpawnPointCheck(ByVal msg As String) As Boolean
        'ユーザーがコマンド実行件を持ち、spawnpointコマンドを実行した結果
        '2013-01-13 15:10:56 [INFO] [miyabi9821: Set miyabi9821's spawn point to (-240, 63, -14)]
        '0          1        2      3            4   5            6     7     8   9     10  11

        'サーバコンソールから特定のユーザーに対し、spawnpointを実行した結果（MCSCのPermission機能でもこちら）
        '2013-06-09 05:23:21 [INFO] Set miyabi9821's spawn point to (-127, 71, 291)
        '0          1        2      3   4            5     6     7   8     9  10
        Try
            Dim strSplitMsg As String() = msg.Split(" ")
            If strSplitMsg.Length < 11 Or strSplitMsg.Length > 12 Then
                '要素数が11より小さいか、12より大きい場合は該当ログでは無い
                Exit Function
            End If

            If strSplitMsg(2) <> "[INFO]" Then
                '3つめの要素が[INFO]じゃないなら該当ログでは無い
                Exit Function
            End If

            'Set xxx's spawn pointメッセージならspawnpointの実行結果として座標取得（ちょっとやっつけ実装）
            If strSplitMsg(4) = "Set" And strSplitMsg(6) = "spawn" And strSplitMsg(7) = "point" Then
                'ユーザーがspawnpointコマンドを実行した場合
                Dim strPlayerID() As String = strSplitMsg(5).Split("'")
                Dim strX As String = strSplitMsg(9).Substring(1, strSplitMsg(9).Length - 2)
                Dim strY As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 1)
                Dim strZ As String = strSplitMsg(11).Substring(0, strSplitMsg(11).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    '取得しようとしたXYZ座標が異常
                    pfWriteSystemLog("Cannot get spawnpoint. PlayerID:" & strPlayerID(0) & " x:" & strX & " y:" & strY & " z:" & strZ, Color.Red)
                Else
                    If pfUpdatePlayerSpawnPoint(strPlayerID(0), strX & "," & strY & "," & strZ) = False Then
                        pfWriteSystemLog("Player Information(spawn point) Update Failure.", Color.Red)
                    End If
                End If

            ElseIf strSplitMsg(3) = "Set" And strSplitMsg(5) = "spawn" And strSplitMsg(6) = "point" Then
                'サーバコンソールからユーザー指定でspawnpointコマンドを実行した場合
                Dim strPlayerID() As String = strSplitMsg(4).Split("'")
                Dim strX As String = strSplitMsg(8).Substring(1, strSplitMsg(8).Length - 2)
                Dim strY As String = strSplitMsg(9).Substring(0, strSplitMsg(9).Length - 1)
                Dim strZ As String = strSplitMsg(10).Substring(0, strSplitMsg(10).Length - 2)
                If pfCheckXYZ(strX, strY, strZ) <> False Then
                    '取得しようとしたXYZ座標が異常
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

    'コマンドとPlayerIDを指定すると、パーミッションを持っているかを判定する
    '戻り値　1:パーミッションあり、0:パーミッション無し、-1:コマンドが存在しない
    Private Function pfGetPermission(ByVal cmd As String, ByVal id As String) As Integer
        With PermissionSettings.Instance
            Select Case cmd
                Case "tp"
                    If .TpEnabled = True Then 'Permission有効
                        If .TpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "give"
                    If .GiveEnabled = True Then 'Permission有効
                        If .GiveMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GiveSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "time"
                    If .TimeEnabled = True Then 'Permission有効
                        If .TimeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .TimeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "xp"
                    If .XpEnabled = True Then 'Permission有効
                        If .XpMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .XpSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "gamemode"
                    If .GamemodeEnabled = True Then 'Permission有効
                        If .GamemodeMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .GamemodeSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "kick"
                    If .KickEnabled = True Then 'Permission有効
                        If .KickMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .KickSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "ban"
                    If .BanEnabled = True Then 'Permission有効
                        If .BanMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .BanSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "pardon"
                    If .PardonEnabled = True Then 'Permission有効
                        If .PardonMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .PardonSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "whitelist"
                    If .WhitelistEnabled = True Then 'Permission有効
                        If .WhitelistMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WhitelistSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "spawnpoint"
                    If .SpawnpointEnabled = True Then 'Permission有効
                        If .SpawnpointMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .SpawnpointSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case "weather"
                    If .WeatherEnabled = True Then 'Permission有効
                        If .WeatherMode = 0 Then 'Everyone
                            Return 1
                        Else 'Selected
                            Dim strUsers As String() = .WeatherSelectedUsers.Split(",")
                            For Each strUserID As String In strUsers
                                If strUserID = id Then
                                    Return 1 '指定ユーザーに存在した
                                End If
                            Next
                            Return 0 '指定ユーザーに存在しない
                        End If
                    Else 'Permission無効
                        Return 0
                    End If

                Case Else
                    Return -1
            End Select

        End With
    End Function

    'プレイヤーがオンラインかチェック
    Private Function pfIsPlayerOnline(ByVal id As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                'プレイヤー一覧の中にプレイヤーを発見
                If lvPlayers.Items(iList).SubItems(2).Text = GSTR_ONLINE_TRUE Then
                    Return True 'オンライン状態
                Else
                    Return False 'オフライン状態
                End If
            End If
        Next

        Return False 'プレイヤー一覧に存在しない(ログインしたことがない)
    End Function

    'HeartBeatログを出力しない
    '1.6からend of streamの出力も出るため、これも抑制する
    '2012-07-27 20:29:49 [INFO] /127.0.0.1:xxxxx lost connection
    '2013-07-28 08:37:28 [SEVERE] Reached end of stream for /127.0.0.1
    Private Function CheckHeartBeatLog(ByVal msg As String) As Boolean
        If Settings.Instance.ServerVersion >= 3 Then
            '1.7以降はこのログが出ないようなので即抜け
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

    'ウィンドウ位置を保存
    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        Settings.Instance.WindowPos = Me.Location
    End Sub


    'ウィンドウサイズ変更
    Private Sub frmMain_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        'オブジェクトサイズ変更
        psObjectSizeChange()

        'ウィンドウサイズの保存
        If Me.WindowState = FormWindowState.Normal Then
            'ノーマルの時のみ保存（最大化、最小化の時は保存しない）
            Settings.Instance.WindowSize = New Point(Me.Width, Me.Height)
        End If

    End Sub

    'ウィンドウサイズが変更されたときなど、画面オブジェクトのサイズを変更する
    Private Sub psObjectSizeChange()
        'ウィンドウサイズの基本は800x600とする
        Dim AddWidth As Integer = Me.Width - 800
        Dim AddHeight As Integer = Me.Height - 600

        '縦ポジション変更
        gbSVController.Top = 420 + AddHeight

        If ExtendPlayersListAreaToolStripMenuItem.Checked = True Then
            'Extend Players List Areaにチェックが入ってたら、プレイヤー一覧の横幅を大きく取る

            '横サイズ変更
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297 + 457 + AddWidth
            lvPlayers.Width = 285 + 457 + AddWidth

            '縦サイズ変更
            gbLog.Height = 385 - 229
            rtbServerLog.Height = 361 - 229
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayersのカラム幅
            lvPlayers.Columns(0).Width = 150     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 50     'Online
            lvPlayers.Columns(3).Width = 120    'LastLogin
            lvPlayers.Columns(4).Width = 120    'LastLogout
            lvPlayers.Columns(5).Width = 80     'Spawn
            lvPlayers.Columns(6).Width = 80     'BadCount

        Else
            '従来通り

            '横サイズ変更
            gbLog.Width = 457 + AddWidth
            rtbServerLog.Width = 445 + AddWidth
            gbSVController.Width = 760 + AddWidth
            rtbSystemLog.Width = 667 + AddWidth
            cmbCommand.Width = 687 + AddWidth
            gbPlayers.Width = 297
            lvPlayers.Width = 285

            '縦サイズ変更
            gbLog.Height = 385 + AddHeight
            rtbServerLog.Height = 361 + AddHeight
            gbPlayers.Height = 229 + AddHeight
            lvPlayers.Height = 205 + AddHeight

            'lvPlayersのカラム幅
            lvPlayers.Columns(0).Width = 100     'Player
            lvPlayers.Columns(1).Width = 100    'IP
            lvPlayers.Columns(2).Width = 30     'Online
            lvPlayers.Columns(3).Width = 5      'LastLogin
            lvPlayers.Columns(4).Width = 5      'LastLogout
            lvPlayers.Columns(5).Width = 5      'Spawn
            lvPlayers.Columns(6).Width = 5      'BadCount

        End If

        'サーバログのカーソル位置を最後に移動
        rtbServerLog.SelectionStart = rtbServerLog.Text.Length
        rtbServerLog.Focus()
        rtbServerLog.ScrollToCaret()

    End Sub

    'プロセスの強制終了
    Private Sub btnKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKill.Click
        Try
            If MessageBox.Show("サーバを強制終了させます。" & vbCrLf & _
                               "データが破損する可能性があり、通常の停止処理で止まらないときのみ実行して下さい。" & vbCrLf & _
                               "続行しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            pfWriteSystemLog("Minecraft Server Process Kill Execute.", Color.Red)
            gblnExitFlg = True '終了フラグを設定
            mcsProc.Kill()
        Catch ex As Exception

        End Try

    End Sub

    'ユーザーの詳細を表示する
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
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint追加
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint追加につき一個後ろに移動

        My.Forms.frmPlayerInfo.Show()
    End Sub

    'ユーザーを強制的に切断するコマンドを発行する
    Private Sub KickPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickPlayerToolStripMenuItem.Click
        'kick [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("kick " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ユーザをホワイトリストに追加するコマンドを発行する
    Private Sub AddWhiteListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWhiteListToolStripMenuItem.Click
        'whitelist add [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("whitelist add " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ユーザーをPlayer BANリストに追加するコマンドを発行する
    Private Sub AddPlayerBANListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPlayerBANListToolStripMenuItem.Click
        'ban [USER]
        If lvPlayers.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        Dim idx As Integer = lvPlayers.SelectedItems(0).Index

        gsSendCommand("ban " & lvPlayers.Items(idx).SubItems(0).Text)
    End Sub

    'ユーザーのIPをIP BANリストに追加するコマンドを発行する
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

    'IP用コンテキストメニューが開いたとき
    Private Sub cmsIP_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsIP.Opening
        Dim menu As ContextMenuStrip = CType(sender, ContextMenuStrip)
        pstrCmsIPCalledObject = menu.SourceControl.Name
    End Sub

    Private Sub CopyToClipboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToClipboardToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                'グローバルIPアドレスをクリップボードにコピー
                System.Windows.Forms.Clipboard.SetText(lblGlobalIP.Text)
            Case Me.lblPrivateIP.Name
                'プライベートIPアドレスをクリップボードにコピー
                System.Windows.Forms.Clipboard.SetText(lblPrivateIP.Text)
        End Select

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        Select Case pstrCmsIPCalledObject
            Case Me.lblGlobalIP.Name
                'グローバルIPアドレス再取得
                lblGlobalIP.Text = gfGetGlobalIP()
            Case Me.lblPrivateIP.Name
                'プライベートIPアドレス再取得
                lblPrivateIP.Text = gfGetPrivateIP()
        End Select
    End Sub


    Private Sub cmsPlayers_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsPlayers.Opening
        '選択されていなかったらキャンセル
        If lvPlayers.SelectedItems.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        If btnRun.Enabled = True Then
            'サーバが起動されていない場合、詳細以外使用不可
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
        'スケジューラ起動
        frmScheduler.Show()
    End Sub

    'プレイヤー一覧の並び替え
    '参考：http://natchan-develop.seesaa.net/article/141920783.html
    Private Sub lvPlayers_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvPlayers.ColumnClick
        Static sIntColNo(lvPlayers.Columns.Count - 1) As Integer  '列のソート状態保持用
        If sIntColNo(e.Column) = 0 Then
            '初回または昇順
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 0)
            sIntColNo(e.Column) = 1    '次回は降順
        Else
            '降順
            Me.lvPlayers.ListViewItemSorter = New ListViewItemComparer(e.Column, 1)
            sIntColNo(e.Column) = 0    '次回は昇順
        End If

    End Sub

    'ダブルクリックで選択プレイヤーの詳細表示
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
        My.Forms.frmPlayerInfo.txtSpawnPoint.Text = lvPlayers.Items(idx).SubItems(5).Text '2013/06/08 SpawnPoint追加
        My.Forms.frmPlayerInfo.txtBadCount.Text = lvPlayers.Items(idx).SubItems(6).Text '2013/06/08 SpawnPoint追加につき一個後ろに移動

        My.Forms.frmPlayerInfo.Show()
        My.Forms.frmPlayerInfo.Activate()
    End Sub


    'lvPlayersのデータを保存、読込する
    '参考：http://www.knowdotnet.com/articles/serializationoflistviewtoxml.html
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
                    '終了するのでオンラインステータスを×に変更する
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
                '入力が分なので、ミリ秒指定で設定
                timBackup.Interval = Settings.Instance.BackupInterval * 60 * 1000

                'サーバ起動中のみタイマーを有効にする
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

    'タイマーにより定期バックアップを実行
    Private Sub timBackup_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBackup.Tick
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            pfWriteSystemLog("Auto-Backup Canceled. (Target file/folder is not configured.)", Color.Black)
            Exit Sub
        End If

        pfWriteSystemLog("Auto-Backup Started.", Color.Black)

        'バックアップ実行
        Dim strRetMsg As String = "" 'バックアップ処理から戻されるメッセージ
        If gfBackup(strRetMsg) = True Then
            pfWriteSystemLog(strRetMsg, Color.Blue)
        Else
            pfWriteSystemLog(strRetMsg, Color.Red)
        End If

        '次回バックアップ時間の表示更新
        lblNextTime.Text = DateAdd(DateInterval.Minute, _
                        Settings.Instance.BackupInterval, _
                        DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")

    End Sub

    '手動バックアップ
    Private Sub DataBackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataBackupToolStripMenuItem.Click
        'バックアップの設定確認処理
        If Settings.Instance.BackupTarget.Rows.Count = 0 Then
            MessageBox.Show("バックアップ対象フォルダが構成されていません。" & vbCrLf & "設定を確認して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If
        If System.IO.Directory.Exists(Settings.Instance.BackupOutput) = False Then
            MessageBox.Show("指定された出力先が存在しません。" & vbCrLf & "設定を確認して下さい。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If

        '実行確認
        If MessageBox.Show("手動バックアップを実行しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        pfWriteSystemLog("Manual-Backup Started.", Color.Black)

        'バックアップ実行
        Dim strRetMsg As String = ""
        If gfBackup(strRetMsg, True) = False Then
            pfWriteSystemLog(strRetMsg, Color.Red)
            Exit Sub
        End If

        pfWriteSystemLog(strRetMsg, Color.Blue)

    End Sub

    'サーバポートに対し、HeartBeat確認を行う
    Private Sub timHB_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timHB.Tick
        'ポート番号が格納される変数
        Static intErrCnt As Integer

        If pstrPort = "" Then
            'server.propertiesの読込確認
            If ghtServerProperties Is Nothing Then
                If gfLoadServerProp() = False Then
                    pfWriteSystemLog("Cannot Read " & GSTR_SERVER_PROPERTIES & ".", Color.Red)
                    Exit Sub
                End If
            End If

            'ポート番号取得
            If gfGetServerPropValue("server-port", pstrPort) = False Then
                pfWriteSystemLog("Server port number unknown.", Color.Red)
                Exit Sub
            End If
        End If

        'Socket接続
        Dim IPEPLocal As New System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), CInt(pstrPort))
        Dim sockLocal As New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp)
        Try
            sockLocal.ReceiveTimeout = 5000 '最大5秒
            sockLocal.Connect(IPEPLocal)
            If sockLocal.Connected Then
                If Settings.Instance.HeartBeatUse0xFE = True Then
                    '2012/10/28 接続人数などを取得するよう変更
                    '2012/11/03 2秒ほどアプリが固まるので、オプションで選べるように変更

                    '送信(0xFE)
                    Dim btSendMsg(1) As Byte
                    btSendMsg(0) = &HFE
                    Try
                        sockLocal.Send(btSendMsg, btSendMsg.Length, Net.Sockets.SocketFlags.None)

                        '受信
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

                        '2バイト目以降を切り出し、UTF-16BEに変換
                        Dim resMsg As String = _
                            System.Text.Encoding.GetEncoding("utf-16be").GetString(mem.GetBuffer(), 3, CInt(mem.Length) - 3)
                        mem.Close()

                        '0xA7で分割する（motd、接続プレイヤー数、最大プレイヤー数）
                        Dim resInfo As String() = resMsg.Split("§")
                        lblConnected.Text = resInfo(1) & " / " & resInfo(2)

                    Catch ex As Exception
                        pfWriteSystemLog("HeartBeat(0xFE) Failure. Error Count:" & intErrCnt.ToString, Color.Red)
                        lblConnected.Text = "Get Error."

                    End Try

                End If

                '正常に接続出来たのでエラーカウントを0にしてソケットを閉じる
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

        'オートリカバリ無効なら終わり
        If Settings.Instance.AutoRecovery = False Then
            Exit Sub
        End If

        '規定のエラー閾値を超えたら
        If intErrCnt >= Settings.Instance.HeartBeatStopCount And intErrCnt < Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Auto-Recovery Start.", Color.Red)
            'サーバ再起動処理
            gsSendCommand("stop")
        ElseIf intErrCnt >= Settings.Instance.HeartBeatKillCount Then
            pfWriteSystemLog("HeartBeat failure. Force Auto-Recovery Start.", Color.Red)
            'サーバプロセス強制終了
            mcsProc.Kill()
        End If

    End Sub

    'メール通知設定画面を開く
    Private Sub SendMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMailToolStripMenuItem.Click
        frmMail.Show()
    End Sub

    '指定されたプレイヤーのBadCountを書き換える
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

    'listコマンドを発行し、プレイヤー一覧のステータスを更新する
    Private Sub UpdatePlayerListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdatePlayerListToolStripMenuItem.Click
        gsSendCommand("list")
    End Sub

    '最新バージョンを確認し、メッセージを出力する
    Private Sub CheckLatestVersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckLatestVersionToolStripMenuItem.Click
        'バージョンチェック
        Dim strMsg As String = ""
        Dim colorMsg As Color = Color.Black

        Dim blnRet As Boolean = gfCheckLatestVersion(strMsg, colorMsg)
        pfWriteSystemLog(strMsg, colorMsg)
        If blnRet = True Then
            If MessageBox.Show("現在使用中のバージョンより新しいバージョンがリリースされています。" & vbCrLf & "Webサイトを表示し確認しますか？", _
                                "新バージョン検出", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '既定のブラウザでWebサイトを開く
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
            'チェックを付けて、プレイヤー一覧表示エリアを拡張する
            ExtendPlayersListAreaToolStripMenuItem.Checked = True
            psObjectSizeChange()
        Else
            'チェックを外して、デフォルト状態に戻す
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

    'プレイヤーのスポーンポイント表示を更新
    Private Function pfUpdatePlayerSpawnPoint(ByVal id As String, xyz As String) As Boolean
        For iList As Integer = 0 To lvPlayers.Items.Count - 1
            If lvPlayers.Items(iList).SubItems(0).Text = id Then
                'プレイヤー一覧の中にプレイヤーを発見
                lvPlayers.Items(iList).SubItems(5).Text = xyz
                Return True 'Spawn Pointを更新して終了
            End If
        Next

        Return False 'プレイヤー一覧に存在しない（ありえない？）
    End Function

    'プレイヤー一覧のクリア
    Private Sub ClearPlayerListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearPlayerListToolStripMenuItem.Click
        If MessageBox.Show("Clear Player List?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        'クリア処理
        lvPlayers.Items.Clear()
    End Sub


    '*** frmChatLogにチャットログを送信 ***
    Private Function ExpertChatLog(ByVal msg As String) As Boolean
        Try
            If Settings.Instance.ServerVersion <= 2 Then
                '1.6.4以前

                Dim strSplitMsg As String() = msg.Split(" ")
                If strSplitMsg.Length <= 4 Then
                    '要素数が4以下ならチャットじゃない
                    Exit Function
                End If

                If strSplitMsg(2) <> "[INFO]" Then
                    '3つめの要素が[INFO]じゃないならチャットじゃない
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (\[.+?\])?(<.+?>) (.*)") Then
                    '4つめの要素が(\[.*?\])?<.*?>じゃないならチャットじゃない
                    If strSplitMsg(3).Contains("[Server]") = True Then
                        '[Server]を含むならサーバーからのチャット
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* ならログインメッセージ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " さんがログインしました。" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* ならログアウトメッセージ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " " & strSplitMsg(3) & " さんがログアウトしました。" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) ならコマンド使用ログ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 さんが $3 コマンドを使用しました。") & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    Exit Function
                End If

            ElseIf Settings.Instance.ServerVersion = 3 Then
                '1.7
                Dim strSplitMsg As String() = msg.Split(" ")

                '面倒なスペースを置換処理
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

                '置換したスペースを戻す
                If Settings.Instance.ServerVersion >= 3 Then
                    For i As Integer = 0 To strSplitMsg.Length - 1
                        strSplitMsg(i) = strSplitMsg(i).Replace("%20", " ")
                    Next
                End If

                If strSplitMsg.Length <= 3 Then
                    '要素数が3以下ならチャットじゃない
                    Exit Function
                End If

                If strSplitMsg(1) <> "[Server thread/INFO]:" Then
                    '2つめの要素が[Server thread/INFO]:じゃないならチャットじゃない
                    Exit Function
                End If

                If Not System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (\[.+?\])?(<.+?>) (.*)") Then
                    '4つめの要素が(\[.*?\])?<.*?>じゃないならチャットじゃない
                    If strSplitMsg(2).Contains("[Server]") = True Then
                        '[Server]を含むならサーバーからのチャット
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & msg & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*?\[/.*?\] logged in with entity id.*") Then
                        '(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?\)[/(.*?\)] logged in with entity id.* ならログインメッセージ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " さんがログインしました。" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: .*? lost connection:.*") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] .*? lost connection:.* ならログアウトメッセージ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & strSplitMsg(0) & " " & strSplitMsg(1) & " " & strSplitMsg(2) & " さんがログアウトしました。" & vbCrLf
                        If chatting = True Then
                            frmChatlog.chatRefresh()
                        End If
                    End If
                    If System.Text.RegularExpressions.Regex.IsMatch(msg, "\[\d\d:\d\d:\d\d\] \[Server Thread/INFO\]: (.*?) issued server command: (/.*)") Then
                        '\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] (.*?) issued server command: (/.*) ならコマンド使用ログ
                        'チャットログに出力
                        frmChatlog.log = frmChatlog.log & System.Text.RegularExpressions.Regex.Replace(msg, "(\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d \[INFO\] )(.*?) issued server command: (/.*)", "[36;1m$1$2 さんが $3 コマンドを使用しました。") & vbCrLf
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
