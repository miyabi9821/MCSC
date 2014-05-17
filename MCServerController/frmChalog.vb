Imports System.Text.RegularExpressions

Public Class frmChatlog
    Public log As String

    Sub chatRefresh()
        If log = Nothing Then
            Exit Sub
        End If
        '「log」をrtbChatLogに表示し、[INFO]削除
        rtbChatlog.Text = log.Replace("[INFO]", "")
        'ログイン・ログアウトを表示するか
        If cbShenter.Checked = False Then
            '2013-07-12 13:28:58  nullp[/127.0.0.1:58935] さんがログインしました。
            rtbChatlog.Text = Regex.Replace(rtbChatlog.Text, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d  .+?\[.+?\] さんがログインしました。", "")
            rtbChatlog.Text = Regex.Replace(rtbChatlog.Text, "\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d  .+? さんがログアウトしました。", "")
        End If

        '自然発生する無駄改行削除
        rtbChatlog.Text = Regex.Replace(rtbChatlog.Text, "(" & vbLf & "){1,}", vbLf)
        rtbChatlog.Text = Regex.Replace(rtbChatlog.Text, "^\n", "", RegexOptions.Multiline)

        'Dim pos, cls As String()
        'Dim counting As Long = 0
        'Dim logg As String = rtbChatlog.Text
        'Dim r As New System.Text.RegularExpressions.Regex("\[(.*?)m")
        'Dim m As System.Text.RegularExpressions.Match = r.Match(logg)
        'If m.Success = True Then
        '    For i As Long = 0 To m.Captures.Count - 1
        '        cls(i) = m.Value
        '        pos(i) = m.Index.ToString
        '    Next
        'End If

        'While m.Success
        '    cls(counting) = m.Value
        '    'pos(counting)
        '    counting = counting + 1
        '    m = m.NextMatch()
        'End While

        'カラーコード削除
        rtbChatlog.Text = Regex.Replace(rtbChatlog.Text, ChrW(27) & "\[(\d\d;(1|22))?m", "")
        '下までスクロール
        rtbChatlog.SelectionStart = rtbChatlog.TextLength
        rtbChatlog.ScrollToCaret()

    End Sub



    Private Sub frmChatlog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmMain.chatting = False
    End Sub

    Private Sub frmChatlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmMain.chatting = True
        chatRefresh()
    End Sub

    Private Sub tbSay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbSay.KeyPress
        Try
            'Enterが押され、かつテキストが空でなければ処理実行
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) AndAlso Me.tbSay.Text <> "" Then
                'コマンド実行
                gsSendCommand("say " & tbSay.Text)
                tbSay.Text = ""
            End If
        Catch ex As Exception
            tbSay.Text = ""
        End Try
    End Sub

End Class