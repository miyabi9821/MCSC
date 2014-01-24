Module modCommon
    Public gblnExitFlg As Boolean = False '終了フラグ
    Public gblnResumeFlg As Boolean = False '再開フラグ
    Public ghtServerProperties As New Hashtable     'server.propertiesを読み込むハッシュテーブル
    Public gstrLogFilePath As String = String.Empty    'ログファイルのフルパス(1.7で変更されたので追加)

    '共通の固定値
    Public Const GSTR_APP_VERSION As String = "0.3.7 beta (2013/12/08)"
    Public Const GSTR_SERVER_PROPERTIES As String = "server.properties"
    Public Const GSTR_IPBAN_LIST As String = "banned-ips.txt"
    Public Const GSTR_PLBAN_LIST As String = "banned-players.txt"
    Public Const GSTR_WHITE_LIST As String = "white-list.txt"
    'Public Const GSTR_SERVER_LOG As String = "server.log"　'1.7からlatest.logに変わったため、固定値で持つ意味が無いのでコメントアウト
    Public Const GSTR_CONFIG_DIR As String = "MCSConfig"
    Public Const GSTR_CONFIG_FILE As String = "Config.xml"
    Public Const GSTR_PLAYERLIST_FILE As String = "Players.xml"
    Public Const GSTR_NGWORDS_FILE As String = "NGWords.xml"
    Public Const GSTR_SENDMAIL_FILE As String = "Mail.xml"
    Public Const GSTR_SCHEDULE_FILE As String = "Schedule.xml"
    Public Const GSTR_CUSTOMACTION_FILE As String = "CustomAction.xml"
    Public Const GSTR_PERMISSION_FILE As String = "Permission.xml"
    Public Const GSTR_BACKUPFILE_PREFIX As String = "MCSCBackup-"
    Public Const GSTR_RAREXE As String = "Rar.exe"
    Public Const GSTR_ONLINE_TRUE As String = "○" 'オンラインステータスがオンライン
    Public Const GSTR_ONLINE_FALSE As String = "×" 'オンラインステータスがオフライン
    Public Const GSTR_ONLINE_FALUSE_WHITE As String = "×(W)" 'オンラインステータスがオフライン（ホワイトリスト未登録）
    Public Const GSTR_ONLINE_FALUSE_BAN As String = "×(B)" 'オンラインステータスがオフライン（PlayerBAN）

    '***** 共通処理 *****
    'グローバルIPアドレス取得処理
    Public Function gfGetGlobalIP() As String
        Try
            Dim wreqIP As System.Net.HttpWebRequest = _
                    CType(System.Net.HttpWebRequest.Create("http://minecraftjp.info/MCSC/ip.php"), _
                                                            System.Net.HttpWebRequest)
            wreqIP.Proxy = Nothing 'Proxyを使用しない
            wreqIP.Timeout = 5000  'タイムアウトは5000ms (2013/06/08)
            Dim webres As System.Net.HttpWebResponse = _
                CType(wreqIP.GetResponse(), System.Net.HttpWebResponse)
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim st As System.IO.Stream = webres.GetResponseStream()
            Dim sr As New System.IO.StreamReader(st, enc)

            Return sr.ReadToEnd()
        Catch ex As Exception
            Return "Get Failure."
        End Try

    End Function

    'プライベートIPアドレス取得処理
    Public Function gfGetPrivateIP() As String
        Try
            ' ホスト名を取得する
            Dim hostname As String = System.Net.Dns.GetHostName()

            ' ホスト名からIPアドレスを取得する
            Dim adrList As System.Net.IPAddress() = System.Net.Dns.GetHostAddresses(hostname)
            If adrList.Length = 1 Then
                '取得出来たIPアドレスが1つなら、それで確定
                Return adrList(0).ToString
            Else
                '複数取得出来た場合は、IPv4で一番最初に見つけた物を返す（割と暫定対応）
                For Each address As System.Net.IPAddress In adrList
                    If address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                        Return address.ToString
                    End If
                Next
            End If

            'ここまで来たら見つからなかった
            Return "Get Failure."

        Catch ex As Exception
            Return "Get Failure."
        End Try
    End Function

    '最新バージョンチェック処理
    'Booleanの戻り値は、アップデートの必要があるときのみTrueを返す
    Public Function gfCheckLatestVersion(ByRef msg As String, ByRef msgcolor As Color) As Boolean
        Try
            Dim strRes As String = ""

            Dim wreqIP As System.Net.HttpWebRequest = _
                    CType(System.Net.HttpWebRequest.Create("http://minecraftjp.info/MCSC/version.txt"), _
                                                            System.Net.HttpWebRequest)
            wreqIP.Proxy = Nothing 'Proxyを使用しない
            wreqIP.Timeout = 5000  'タイムアウトは5000ms (2013/11/28)
            Dim webres As System.Net.HttpWebResponse = _
                CType(wreqIP.GetResponse(), System.Net.HttpWebResponse)
            Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim st As System.IO.Stream = webres.GetResponseStream()
            Dim sr As New System.IO.StreamReader(st, enc)
            strRes = sr.ReadToEnd()
            sr.Close()
            sr.Dispose()

            If strRes = GSTR_APP_VERSION Then
                msg = "MCSC is latest version."
                msgcolor = Color.Blue
                Return False
            Else
                msg = "MCSC exists a new version. (" & strRes & ")"
                msgcolor = Color.Red
                Return True
            End If
        Catch ex As Exception
            msg = "Latest version info Get Failure."
            msgcolor = Color.Red
            Return False
        End Try

    End Function

    '通知メール送信処理
    Public Function gsSendMail(ByVal title As String, ByVal msg As String) As Boolean
        Try
            'With MailSettings.Instance
            '    'SMTP CLIENT作成
            '    Dim sc As New System.Net.Mail.SmtpClient(.Server, .Port)
            '    'SMTPサーバの認証ユーザ指定
            '    sc.Credentials = New System.Net.NetworkCredential(.User, .Pass)
            '    'メール送信
            '    sc.Send(.SendFrom, .SendTo, title, msg)
            'End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'データバックアップ処理
    '参考）http://wiki.dobon.net/index.php?.NET%A5%D7%A5%ED%A5%B0%A5%E9%A5%DF%A5%F3%A5%B0%B8%A6%B5%E6%2F93
    '参考）http://wiki.dobon.net/index.php?.NET%A5%D7%A5%ED%A5%B0%A5%E9%A5%DF%A5%F3%A5%B0%B8%A6%B5%E6%2F94
    'この処理にはDotNetZip Libraryを使用しています。 http://dotnetzip.codeplex.com/
    Public Function gfBackup(ByRef retMsg As String, Optional ByVal manual As Boolean = False) As Boolean
        Try
            '圧縮を行う場合、圧縮用のexeが無ければエラー終了
            If Settings.Instance.CompressEnabled = True Then
                If System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, GSTR_RAREXE)) = False Then
                    If manual = True Then
                        retMsg = "Manual-Backup Failure. " & GSTR_RAREXE & " is not found."
                        Return False
                    Else
                        retMsg = "Auto-Backup Failure. " & GSTR_RAREXE & " is not found."
                        Return False
                    End If
                End If
            End If

            '出力先フォルダがなければ作成する
            If System.IO.Directory.Exists(Settings.Instance.BackupOutput) = False Then
                Try
                    System.IO.Directory.CreateDirectory(Settings.Instance.BackupOutput)
                Catch ex As Exception
                    retMsg = "Auto-Backup Failure. Can't Create Output Directory."
                    Return False
                End Try
            End If

            'サーバ稼働中は自動保存を停止する
            If frmMain.btnRun.Enabled = False Then
                'save-all実行
                gsSendCommand("save-all")
                System.Threading.Thread.Sleep(1000)

                'save-off実行
                gsSendCommand("save-off")
                System.Threading.Thread.Sleep(1000)
            End If

            'バックアップ処理
            Dim strBkName As String = System.IO.Path.Combine(Settings.Instance.BackupOutput, _
                                    GSTR_BACKUPFILE_PREFIX & DateTime.Now.ToString("yyyyMMdd-HHmmss"))

            '手動バックアップ時はファイル名に分かるような文字を付与
            If manual = True Then
                strBkName = strBkName & "-Manual"
            End If

            If Settings.Instance.CompressEnabled = False Then
                '圧縮を行わない(フォルダ／ファイルのコピー)
                With Settings.Instance.BackupTarget
                    For i As Integer = 0 To .Rows.Count - 1
                        If CBool(.Rows(i)("Checked")) = True Then
                            If System.IO.File.Exists(.Rows(i)("Path")) = True Then
                                'ファイルの場合
                                'System.IO.File.Copy(.Rows(i)("Path"), System.IO.Path.Combine(strBkName, .Rows(i)("Path")), True)
                                Dim fInfo As New System.IO.FileInfo(.Rows(i)("Path"))
                                System.IO.File.Copy(.Rows(i)("Path"), System.IO.Path.Combine(strBkName, fInfo.Name), True)
                            ElseIf System.IO.Directory.Exists(.Rows(i)("Path")) = True Then
                                'フォルダの場合
                                Dim dInfo As New System.IO.DirectoryInfo(.Rows(i)("Path"))
                                CopyDirectory(.Rows(i)("Path"), System.IO.Path.Combine(strBkName, dInfo.Name))
                            Else

                            End If
                        End If
                        Application.DoEvents()
                    Next
                End With

            Else
                '圧縮を行う



            End If

            'サーバ稼働中は停止した自動保存を再開させる
            If frmMain.btnRun.Enabled = False Then
                'save-on実行
                gsSendCommand("save-on")
                System.Threading.Thread.Sleep(1000)

                'save-all実行
                gsSendCommand("save-all")
                System.Threading.Thread.Sleep(1000)
            End If

            'ファイル削除実行
            gfBackupDelete()

            If manual = True Then
                retMsg = "Manual-Backup Success."
            Else
                retMsg = "Auto-Backup Success."
            End If

            Return True
        Catch ex As Exception
            'サーバ稼働中は停止した自動保存を再開させる
            If frmMain.btnRun.Enabled = False Then
                'save-on実行
                gsSendCommand("save-on")
                System.Threading.Thread.Sleep(1000)

                'save-all実行
                gsSendCommand("save-all")
                System.Threading.Thread.Sleep(1000)
            End If

            If manual = True Then
                retMsg = "Manual-Backup Failure."
            Else
                retMsg = "Auto-Backup Failure."
            End If

            Return False
        End Try
    End Function

    '参考：http://dobon.net/vb/dotnet/file/copyfolder.html
    ''' <summary>
    ''' ディレクトリをコピーする
    ''' </summary>
    ''' <param name="sourceDirName">コピーするディレクトリ</param>
    ''' <param name="destDirName">コピー先のディレクトリ</param>
    Private Sub CopyDirectory( _
            ByVal sourceDirName As String, _
            ByVal destDirName As String)
        'コピー先のディレクトリがないときは作る
        If Not System.IO.Directory.Exists(destDirName) Then
            System.IO.Directory.CreateDirectory(destDirName)
            '属性もコピー
            System.IO.File.SetAttributes(destDirName, _
                System.IO.File.GetAttributes(sourceDirName))
        End If

        'コピー先のディレクトリ名の末尾に"\"をつける
        If destDirName.Chars((destDirName.Length - 1)) <> _
                System.IO.Path.DirectorySeparatorChar Then
            destDirName = destDirName + System.IO.Path.DirectorySeparatorChar
        End If

        'コピー元のディレクトリにあるファイルをコピー
        Dim fs As String() = System.IO.Directory.GetFiles(sourceDirName)
        Dim f As String
        For Each f In fs
            System.IO.File.Copy(f, _
                destDirName + System.IO.Path.GetFileName(f), True)
        Next

        'コピー元のディレクトリにあるディレクトリをコピー
        Dim dirs As String() = System.IO.Directory.GetDirectories(sourceDirName)
        Dim dir As String
        For Each dir In dirs
            CopyDirectory(dir, destDirName + System.IO.Path.GetFileName(dir))
        Next
    End Sub

    '保持日数経過済みのバックアップを削除する
    Public Function gfBackupDelete() As Boolean
        Try
            '保持日数が0なら削除しない
            If Settings.Instance.BackupKeepDays = 0 Then
                Return True
            End If

            'ファイル一覧を取得
            Dim strBkFiles As String() = System.IO.Directory.GetFiles(Settings.Instance.BackupOutput, _
                                                                      GSTR_BACKUPFILE_PREFIX & "*", _
                                                                      IO.SearchOption.TopDirectoryOnly)
            'フォルダ一覧を取得
            Dim strBkDirs As String() = System.IO.Directory.GetDirectories(Settings.Instance.BackupOutput, _
                                                                           GSTR_BACKUPFILE_PREFIX & "*", _
                                                                           IO.SearchOption.TopDirectoryOnly)

            ''ファイル数が0なら終了
            'If strBkFiles.Length = 0 Then
            '    Return True
            'End If

            '削除対象となる日付を設定
            Dim dateThreshold As Date = DateAdd(DateInterval.Day, Settings.Instance.BackupKeepDays * -1, DateTime.Now)

            'ファイル削除
            For i As Integer = 0 To strBkFiles.Length - 1
                If System.IO.File.GetCreationTime(strBkFiles(i)) < dateThreshold Then
                    '作成日時＜対象日時ならファイル削除
                    Try
                        If strBkFiles(i).IndexOf("-Manual") < 0 Then
                            '2012/11/02 手動バックアップは対象外
                            System.IO.File.Delete(strBkFiles(i))
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next

            'フォルダ削除
            For i As Integer = 0 To strBkDirs.Length - 1
                If System.IO.Directory.GetCreationTime(strBkDirs(i)) < dateThreshold Then
                    '作成日時＜対象日時ならファイル削除
                    Try
                        If strBkDirs(i).IndexOf("-Manual") < 0 Then
                            '2012/11/02 手動バックアップは対象外に変更
                            System.IO.Directory.Delete(strBkDirs(i), True)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub gsSendCommand(ByVal cmd As String)
        Try
            'サーバにコマンド送信
            Dim WriteConsoleInput As New clsWriteConsoleInput
            Try
                WriteConsoleInput.KeyIn(cmd & vbCr, frmMain.mcsProc.Id)
            Catch ex As Exception
                Throw
            End Try

        Catch ex As Exception
            Throw
        End Try
    End Sub

    'server.propertiesファイルを読み込む
    Public Function gfLoadServerProp() As Boolean
        Try
            Dim strPropFile As String = _
                System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                       GSTR_SERVER_PROPERTIES) 'サーバ設定ファイルのパス
            If System.IO.File.Exists(strPropFile) = False Then
                'ファイルが見つからなかったら終了
                ghtServerProperties = Nothing
                Return False
            End If

            'ファイル読み取り用
            Dim htServerProperties As New Hashtable
            Dim strServerPropertiesComment As String = ""
            Dim strReadBuf As String = ""
            Dim intCommentIdx As Integer = 0
            Dim fsProp As New System.IO.FileStream(strPropFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
            'Dim srProp As System.IO.StreamReader = _
            '    New System.IO.StreamReader(strPropFile, System.Text.Encoding.GetEncoding("Shift_JIS"))
            Dim srProp As New System.IO.StreamReader(fsProp, System.Text.Encoding.Default)
            While (srProp.Peek() >= 0)
                strReadBuf = srProp.ReadLine
                If strReadBuf = "" Then
                    '空行は何もしない
                ElseIf strReadBuf.Substring(0, 1) = "#" Then
                    'コメント行
                    htServerProperties("comment" & intCommentIdx) = strReadBuf
                    intCommentIdx = intCommentIdx + 1
                Else
                    Dim strSplitBuf As String() = strReadBuf.Split("=")
                    If strSplitBuf.Length = 2 Then
                        'option=valueの形式だった場合
                        htServerProperties(strSplitBuf(0)) = strSplitBuf(1)
                    Else
                        'option=空白の形式だった場合
                        htServerProperties(strSplitBuf(0)) = ""
                    End If
                End If
            End While

            '全て読み込んだら共通変数へセット
            ghtServerProperties = htServerProperties
            srProp.Close()
            srProp.Dispose()
            fsProp.Close()
            fsProp.Dispose()
            Return True

        Catch ex As Exception
            ghtServerProperties = Nothing
            Return False

        End Try

    End Function

    'server.propertiesファイルを書き出す
    Public Function gfSaveServerProp() As Boolean
        Try
            Dim strPropFile As String = _
                System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Settings.Instance.JarPath), _
                                       GSTR_SERVER_PROPERTIES) 'サーバ設定ファイルのパス

            'ファイル書き込み用
            Dim srProp As System.IO.StreamWriter = _
                New System.IO.StreamWriter(strPropFile, False, System.Text.Encoding.GetEncoding("Shift_JIS"))


        Catch ex As Exception
            Return False
        End Try

    End Function

    '読み込んだserver.propertiesのvalueを取得する
    Public Function gfGetServerPropValue(ByVal key As String, ByRef value As String) As Boolean
        Try
            'server.propertiesが読み込めていない場合
            If ghtServerProperties Is Nothing Then
                Return False
            End If

            '存在しないkeyが指定された場合
            If ghtServerProperties(key) Is Nothing Then
                value = ""
                Return False
            End If

            'value取得
            value = ghtServerProperties(key).ToString
            Return True
        Catch ex As Exception
            value = ""
            Return False
        End Try
    End Function

    '設定ファイルを保存するパスを返す
    'フォルダの存在チェックと作成も同時に行う
    Public Function gfGetConfigDir() As String
        Try
            Dim strCfgPath As String = System.IO.Path.Combine(Application.StartupPath, GSTR_CONFIG_DIR)
            'パスの存在チェック
            If System.IO.Directory.Exists(strCfgPath) = False Then
                '無ければ当然作る
                System.IO.Directory.CreateDirectory(strCfgPath)
            End If
            Return strCfgPath

        Catch ex As Exception
            Return Application.StartupPath
        End Try
    End Function

    '0.2.0までの設定ファイルを、新しい場所に移動する
    Public Function gfMoveOldConfig() As Boolean
        Try
            Dim strOldPath As String = ""
            Dim strNewPath As String = ""
            'config.xml
            strOldPath = System.IO.Path.Combine(Application.StartupPath, GSTR_CONFIG_FILE)
            strNewPath = System.IO.Path.Combine(gfGetConfigDir, GSTR_CONFIG_FILE)
            If System.IO.File.Exists(strOldPath) = True Then
                System.IO.File.Move(strOldPath, strNewPath)
            End If

            'player.xml
            strOldPath = System.IO.Path.Combine(Application.StartupPath, GSTR_PLAYERLIST_FILE)
            strNewPath = System.IO.Path.Combine(gfGetConfigDir, GSTR_PLAYERLIST_FILE)
            If System.IO.File.Exists(strOldPath) = True Then
                System.IO.File.Move(strOldPath, strNewPath)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    'IPアドレスを対象IP/マスクと一致するか判定
    Public Function gfCheckMaskedIP(ByVal baseIP As String, ByVal mask As String, ByVal targetIP As String) As Boolean
        Dim lngMasked As Long = pfConvIPtoLong(mask) And pfConvIPtoLong(targetIP)
        If pfConvIPtoLong(baseIP) = lngMasked Then
            Return True
        Else
            Return False
        End If
    End Function

    'IPアドレスを二進数に変換
    Private Function pfConvIPtoLong(ByVal IP As String) As Long
        Try
            Dim strIP() As String = IP.Split(".")
            Dim strIPBin As String = ""
            For i As Integer = 0 To strIP.Length - 1
                'strIPBin = strIPBin & pfConv10to2(strIP(i))
                strIPBin = strIPBin & Convert.ToString(CInt(strIP(i)), 2).PadLeft(8, "0")
            Next
            Return Convert.ToInt64(strIPBin, 2)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' 文字コードを判別する
    ''' </summary>
    ''' <remarks>
    ''' Jcode.pmのgetcodeメソッドを移植したものです。
    ''' Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
    ''' Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
    ''' DOBON.NET http://dobon.net/vb/dotnet/string/detectcode.html
    ''' </remarks>
    ''' <param name="bytes">文字コードを調べるデータ</param>
    ''' <returns>適当と思われるEncodingオブジェクト。
    ''' 判断できなかった時はnull。</returns>
    Public Function GetCode(ByVal bytes As Byte()) As System.Text.Encoding
        Const bEscape As Byte = &H1B
        Const bAt As Byte = &H40
        Const bDollar As Byte = &H24
        Const bAnd As Byte = &H26
        Const bOpen As Byte = &H28 ''('
        Const bB As Byte = &H42
        Const bD As Byte = &H44
        Const bJ As Byte = &H4A
        Const bI As Byte = &H49

        Dim len As Integer = bytes.Length
        Dim b1 As Byte, b2 As Byte, b3 As Byte, b4 As Byte

        'Encode::is_utf8 は無視

        Dim isBinary As Boolean = False
        Dim i As Integer
        For i = 0 To len - 1
            b1 = bytes(i)
            If b1 <= &H6 OrElse b1 = &H7F OrElse b1 = &HFF Then
                ''binary'
                isBinary = True
                If b1 = &H0 AndAlso i < len - 1 AndAlso bytes(i + 1) <= &H7F Then
                    'smells like raw unicode
                    Return System.Text.Encoding.Unicode
                End If
            End If
        Next
        If isBinary Then
            Return Nothing
        End If

        'not Japanese
        Dim notJapanese As Boolean = True
        For i = 0 To len - 1
            b1 = bytes(i)
            If b1 = bEscape OrElse &H80 <= b1 Then
                notJapanese = False
                Exit For
            End If
        Next
        If notJapanese Then
            Return System.Text.Encoding.ASCII
        End If

        For i = 0 To len - 3
            b1 = bytes(i)
            b2 = bytes(i + 1)
            b3 = bytes(i + 2)

            If b1 = bEscape Then
                If b2 = bDollar AndAlso b3 = bAt Then
                    'JIS_0208 1978
                    'JIS
                    Return System.Text.Encoding.GetEncoding(50220)
                ElseIf b2 = bDollar AndAlso b3 = bB Then
                    'JIS_0208 1983
                    'JIS
                    Return System.Text.Encoding.GetEncoding(50220)
                ElseIf b2 = bOpen AndAlso (b3 = bB OrElse b3 = bJ) Then
                    'JIS_ASC
                    'JIS
                    Return System.Text.Encoding.GetEncoding(50220)
                ElseIf b2 = bOpen AndAlso b3 = bI Then
                    'JIS_KANA
                    'JIS
                    Return System.Text.Encoding.GetEncoding(50220)
                End If
                If i < len - 3 Then
                    b4 = bytes(i + 3)
                    If b2 = bDollar AndAlso b3 = bOpen AndAlso b4 = bD Then
                        'JIS_0212
                        'JIS
                        Return System.Text.Encoding.GetEncoding(50220)
                    End If
                    If i < len - 5 AndAlso _
                        b2 = bAnd AndAlso b3 = bAt AndAlso b4 = bEscape AndAlso _
                        bytes(i + 4) = bDollar AndAlso bytes(i + 5) = bB Then
                        'JIS_0208 1990
                        'JIS
                        Return System.Text.Encoding.GetEncoding(50220)
                    End If
                End If
            End If
        Next

        'should be euc|sjis|utf8
        'use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
        Dim sjis As Integer = 0
        Dim euc As Integer = 0
        Dim utf8 As Integer = 0
        For i = 0 To len - 2
            b1 = bytes(i)
            b2 = bytes(i + 1)
            If ((&H81 <= b1 AndAlso b1 <= &H9F) OrElse _
                (&HE0 <= b1 AndAlso b1 <= &HFC)) AndAlso _
                ((&H40 <= b2 AndAlso b2 <= &H7E) OrElse _
                 (&H80 <= b2 AndAlso b2 <= &HFC)) Then
                'SJIS_C
                sjis += 2
                i += 1
            End If
        Next
        For i = 0 To len - 2
            b1 = bytes(i)
            b2 = bytes(i + 1)
            If ((&HA1 <= b1 AndAlso b1 <= &HFE) AndAlso _
                (&HA1 <= b2 AndAlso b2 <= &HFE)) OrElse _
                (b1 = &H8E AndAlso (&HA1 <= b2 AndAlso b2 <= &HDF)) Then
                'EUC_C
                'EUC_KANA
                euc += 2
                i += 1
            ElseIf i < len - 2 Then
                b3 = bytes(i + 2)
                If b1 = &H8F AndAlso (&HA1 <= b2 AndAlso b2 <= &HFE) AndAlso _
                    (&HA1 <= b3 AndAlso b3 <= &HFE) Then
                    'EUC_0212
                    euc += 3
                    i += 2
                End If
            End If
        Next
        For i = 0 To len - 2
            b1 = bytes(i)
            b2 = bytes(i + 1)
            If (&HC0 <= b1 AndAlso b1 <= &HDF) AndAlso _
                (&H80 <= b2 AndAlso b2 <= &HBF) Then
                'UTF8
                utf8 += 2
                i += 1
            ElseIf i < len - 2 Then
                b3 = bytes(i + 2)
                If (&HE0 <= b1 AndAlso b1 <= &HEF) AndAlso _
                    (&H80 <= b2 AndAlso b2 <= &HBF) AndAlso _
                    (&H80 <= b3 AndAlso b3 <= &HBF) Then
                    'UTF8
                    utf8 += 3
                    i += 2
                End If
            End If
        Next
        'M. Takahashi's suggestion
        'utf8 += utf8 / 2;

        System.Diagnostics.Debug.WriteLine( _
            String.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8))
        If euc > sjis AndAlso euc > utf8 Then
            'EUC
            Return System.Text.Encoding.GetEncoding(51932)
        ElseIf sjis > euc AndAlso sjis > utf8 Then
            'SJIS
            Return System.Text.Encoding.GetEncoding(932)
        ElseIf utf8 > euc AndAlso utf8 > sjis Then
            'UTF8
            Return System.Text.Encoding.UTF8
        End If

        Return Nothing
    End Function
End Module
