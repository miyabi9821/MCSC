'MCBansのデータ取得処理を記述する
'注意事項１：ソースの配布時には抜くこと（MCBansの攻撃に使用されないように）
'注意事項２：online-mode=trueの時のみMCBansサーバに問い合わせる（offlineだとユーザー名詐称し放題のため）

Module modMCBans
    Private Function pfShuffleAPIServer() As String()
        'サーバ情報の配列
        Dim strMCBansAPISV As String() = {"api01.cluster.mcbans.com", "api02.cluster.mcbans.com", "api03.cluster.mcbans.com", "api.mcbans.com"}

        'Fisher-Yatesアルゴリズムでシャッフルする 
        Dim rng As New System.Random()
        Dim n As Integer = strMCBansAPISV.Length
        While n > 1
            n -= 1
            Dim k As Integer = rng.Next(n + 1)
            Dim tmp As String = strMCBansAPISV(k)
            strMCBansAPISV(k) = strMCBansAPISV(n)
            strMCBansAPISV(n) = tmp
        End While

        Return strMCBansAPISV
    End Function

    'MCBansのAPIを利用し、ユーザの接続可否を判定する
    '戻り値は以下の通り
    '-1：不明なエラー
    '0：取得成功
    '1：server.propertiesからonline-modeの値が取得出来なかった
    '2：server.propertiesのonline-modeがfalseだった
    '3：MCBansサーバから正しくデータが受信できなかった
    '4：問い合わせに必要なクライアントIPアドレスが不正
    Public Function gfGetUserStat(ByRef stat As Boolean, ByVal APIKey As String, ByVal UID As String, ByVal IP As String, ByRef reason As String) As Integer
        Try
            Dim strOnline As String = String.Empty
            If gfGetServerPropValue("online-mode", strOnline) = False Then
                'server.propertiesからonline-modeの値が取得出来なかったら終了
                stat = True
                Return 1
            End If
            If CBool(strOnline) = False Then
                'server.propertiesからonline-modeの値がfalse(オフライン)だったら終了
                stat = True
                Return 2
            End If
            Dim IPAddrTemp As New System.Net.IPAddress(0)
            If System.Net.IPAddress.TryParse(IP, IPAddrTemp) = False Then
                'ログから取得したIPアドレスが正しくなければ終了
                stat = True
                Return 4
            End If

            'APIサーバをシャッフルして取得する
            Dim strMCBansAPISV As String() = pfShuffleAPIServer()

            'APIサーバ数の台数で取得出来るまでループ
            For i As Integer = 0 To strMCBansAPISV.Length - 1
                Dim wreqIP As System.Net.HttpWebRequest = _
                        CType(System.Net.HttpWebRequest.Create("http://" & strMCBansAPISV(i) & "/v2/" & APIKey & "/login/" _
                                                                & UID & "/" & IP),  _
                                                                System.Net.HttpWebRequest)
                wreqIP.UserAgent = "MCSC/" & GSTR_APP_VERSION
                wreqIP.Proxy = Nothing 'Proxyを使用しない
                wreqIP.Timeout = 3000 '3秒でタイムアウト

                'リクエスト発行
                Try
                    Dim webres As System.Net.HttpWebResponse = _
                        CType(wreqIP.GetResponse(), System.Net.HttpWebResponse)
                    Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim st As System.IO.Stream = webres.GetResponseStream()
                    Dim sr As New System.IO.StreamReader(st, enc)

                    'レスポンステキスト格納
                    Dim strTmp As String = sr.ReadToEnd()

                    sr.Close()
                    st.Close()

                    'レスポンスからユーザーの状態を判定
                    If strTmp <> String.Empty Then
                        Dim strRes() As String = strTmp.Split(";")
                        If strRes.Length = 6 Then '正常受信
                            If strRes(0) = "l" OrElse strRes(0) = "g" OrElse strRes(0) = "t" OrElse strRes(0) = "s" OrElse strRes(0) = "i" Then
                                'プレイヤーの状態がl,g,t,s,iの場合接続拒否
                                stat = False
                                reason = strRes(1) '接続拒否の理由
                                Return 0
                            ElseIf Settings.Instance.MCBansMinRep > CDbl(strRes(2)) Then
                                'プレイヤーの評判値が閾値を下回る場合接続拒否
                                stat = False
                                reason = "Reputation too low!"
                                Return 0
                            Else
                                '接続許可
                                stat = True
                                Return 0
                            End If
                        Else '項目数不一致
                            'やり直し
                        End If
                    Else 'レスポンス不正
                        'やり直し
                    End If

                Catch ex As Exception
                    'タイムアウト等のエラー
                End Try

                If i = 3 Then '4回失敗したら
                    Return 3
                End If
            Next

        Catch ex As Exception
            Return -1

        End Try
    End Function

End Module
