Public Class frmConfig
    'サーバ用JARファイルの指定
    Private Sub btnServerFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerFileOpen.Click
        'デフォルトファイル名指定
        If rbOfficial.Checked = True Then
            ofdFile.FileName = "minecraft_server*.jar"
        ElseIf rbBukkit.Checked = True Then
            ofdFile.FileName = "craftbukkit*.jar"
        End If
        If txtJarPath.Text = "" OrElse System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtJarPath.Text)) = False Then
            '現在指定されているパスのフォルダが見つからない場合、MCSCのディレクトリを指定
            ofdFile.InitialDirectory = Application.StartupPath
        Else
            ofdFile.InitialDirectory = System.IO.Path.GetDirectoryName(txtJarPath.Text)
        End If

        ofdFile.Filter = "JARファイル(*.jar)|*.jar|実行ファイル(*.exe)|*.exe|全てのファイル(*.*)|*.*"
        ofdFile.FilterIndex = 1 'JARファイルをデフォルト選択

        If ofdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'OKを押されたときのみ処理する
            txtJarPath.Text = ofdFile.FileName

            If txtJarPath.Text.IndexOf("bukkit") >= 0 Then
                'ファイル名にbukkitの記載があればラジオボタンをbukkitに変更(強引)
                rbBukkit.Checked = True
                txtJarFileAugment.Text = "--nojline" '2013/07/28 Win8/WS2012で2バイト文字が化ける問題対応
            Else
                rbOfficial.Checked = True
                txtJarFileAugment.Text = "nogui"

                '最近はデフォルトのファイル名にバージョンが入るので、一応ラジオボタンの選択を自動化
                If txtJarPath.Text.IndexOf("minecraft_server.1.6") >= 0 Then
                    rbV14.Checked = True
                ElseIf txtJarPath.Text.IndexOf("minecraft_server.1.7") >= 0 Then
                    rbV17.Checked = True
                End If
            End If
        End If

    End Sub

    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '設定の反映
        With Settings.Instance
            txtJarPath.Text = .JarPath                  'JARパス
            txtJavaExe.Text = .JavaPath                 'Javaパス
            txtAugment.Text = .Augment                  'Java起動引数
            txtJarFileAugment.Text = .JarFileAugment    'サーバ引数
            chkAutoStart.Checked = .AutoStart           '自動起動
            chkAutoRecovery.Checked = .AutoRecovery     '自動リカバリ
            chkShowConsole.Checked = .ShowConsole       'ウィンドウ非表示の無効
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

        'java.exeのパスを簡易検索
        If System.IO.File.Exists(txtJavaExe.Text) = False Then
            txtJavaExe.Text = GetDefaultJavaPath()
        End If

        'MCServerのJarファイルを簡易検索
        If System.IO.File.Exists(txtJarPath.Text) = False Then
            Dim strJarFiles As String() = System.IO.Directory.GetFiles(Application.StartupPath, "*.jar", System.IO.SearchOption.TopDirectoryOnly)
            If strJarFiles.Length > 0 Then
                txtJarPath.Text = strJarFiles(0) 'どれかまでは判断出来ないので最初に見つかった物を設定
                If strJarFiles(0).IndexOf("bukkit") >= 0 Then
                    'ファイル名にbukkitの記載があればラジオボタンをbukkitに変更(強引)
                    rbBukkit.Checked = True
                    txtJarFileAugment.Text = "--nojline" '2013/07/28 Win8/WS2012で2バイト文字が化ける問題対応
                Else
                    rbOfficial.Checked = True
                    txtJarFileAugment.Text = "nogui"

                    '最近はデフォルトのファイル名にバージョンが入るので、一応ラジオボタンの選択を自動化
                    If strJarFiles(0).IndexOf("minecraft_server.1.6") >= 0 Then
                        rbV14.AutoCheck = True
                    ElseIf strJarFiles(0).IndexOf("minecraft_server.1.7") >= 0 Then
                        rbV17.Checked = True
                    End If
                End If

            End If

            '引数もデフォルトに設定する
            txtAugment.Text = "-Xms1024M -Xmx1024M"

        End If
    End Sub

    Private Sub btnJavaExeOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJavaExeOpen.Click
        ofdFile.FileName = "java.exe"
        If txtJavaExe.Text = "" OrElse System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtJavaExe.Text)) = False Then
            '現在指定されているパスのフォルダが見つからない場合、MCSCのディレクトリを指定
            ofdFile.InitialDirectory = Application.StartupPath
        Else
            ofdFile.InitialDirectory = System.IO.Path.GetDirectoryName(txtJavaExe.Text)
        End If
        ofdFile.Filter = "実行ファイル(*.exe)|*.exe|全てのファイル(*.*)|*.*"
        ofdFile.FilterIndex = 1 'EXEファイルをデフォルト選択

        If ofdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'OKを押されたときのみ処理する
            txtJavaExe.Text = ofdFile.FileName
        End If
    End Sub

    Private Function GetDefaultJavaPath() As String

        'Java.exeがありそうな場所の一覧
        '(優先順位が高い物を上に記述すること)
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
                '先に見つけた物を適用する
                Return strPath
            End If
        Next

        '見つからなかったらとりあえずこの実行ファイルのある場所
        Return Application.StartupPath

    End Function

    'デフォルトの起動オプションに戻します
    Private Sub btnAugmentDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAugmentDefault.Click
        If txtAugment.Text <> "" Then
            If MessageBox.Show("起動オプションをデフォルトに戻しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If
        txtAugment.Text = "-Xms1024M -Xmx1024M"
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        '設定を保存して閉じる
        With Settings.Instance
            .JarPath = txtJarPath.Text                  'JARパス
            .JavaPath = txtJavaExe.Text                 'Javaパス
            .Augment = txtAugment.Text                  'Java起動引数
            .JarFileAugment = txtJarFileAugment.Text    'サーバ引数
            .AutoStart = chkAutoStart.Checked           '自動起動
            .AutoRecovery = chkAutoRecovery.Checked     '自動リカバリ
            .ShowConsole = chkShowConsole.Checked       'ウィンドウ非表示の無効
            If rbOfficial.Checked = True Then             '使用サーバ
                .ServerMode = 0
            ElseIf rbBukkit.Checked = True Then
                .ServerMode = 1
            End If
            If rbV125.Checked = True Then               '使用サーババージョン
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
            epInput.SetError(txtJarPath, "jarファイルが見つかりません")
        Else
            epInput.SetError(txtJarPath, "")
        End If
    End Sub

    Private Sub txtJavaExe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJavaExe.TextChanged
        If System.IO.File.Exists(txtJavaExe.Text) = False Then
            epInput.SetError(txtJavaExe, "java.exeが見つかりません")
        Else
            epInput.SetError(txtJavaExe, "")
        End If
    End Sub

    Private Sub btnJarFileAugment_Click(sender As Object, e As EventArgs) Handles btnJarFileAugment.Click
        If MessageBox.Show("Jar File 起動オプションをデフォルトに戻しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If rbOfficial.Checked = True Then
            txtJarFileAugment.Text = "nogui"
        Else
            txtJarFileAugment.Text = "--nojline"
        End If
    End Sub

    'サーバの種類が変更されたとき
    Private Sub ServerType_CheckedChanged(sender As Object, e As EventArgs) Handles rbBukkit.CheckedChanged, rbOfficial.CheckedChanged
        If rbOfficial.Checked = True Then
            txtJarFileAugment.Text = "nogui"
        Else
            txtJarFileAugment.Text = "--nojline"
        End If
    End Sub

End Class