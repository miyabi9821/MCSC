Public Class clsIgnoreLogs
    '非表示ログ一覧保管所
    Public Structure IgnorePtn
        'パターン
        Private strPattern As String
        Public Property Pattern() As String
            Get
                Return strPattern
            End Get
            Set(ByVal Value As String)
                strPattern = Value
            End Set
        End Property

        '正規表現を使うか
        Private bolRegex As String
        Public Property Regex() As String
            Get
                Return bolRegex
            End Get
            Set(ByVal Value As String)
                bolRegex = Value
            End Set
        End Property
    End Structure

    'ファイル読込
    Public Sub loadIgnoreLogs(fl As Boolean)
        Dim dat As String() = System.IO.File.ReadAllLines(System.IO.Path.Combine(gfGetConfigDir(), "IgnoredLogs.txt"), System.Text.Encoding.GetEncoding("UTF-8"))
        For i As Integer = 0 To UBound(dat)
            Dim types As New System.Text.RegularExpressions.Regex("(?<pat>.*),(?<reg>(true|false))")
            Dim find As System.Text.RegularExpressions.MatchCollection = types.Matches(dat(i))
            For Each mat As System.Text.RegularExpressions.Match In find
                Dim pat, reg As String
                pat = mat.Groups("pat").Value
                reg = mat.Groups("reg").Value


            Next
        Next


    End Sub
    'ファイル保存
    Public Sub saveIgnoreLogs(fl As Boolean)
        Dim txt As String = ""


        System.IO.File.WriteAllText(System.IO.Path.Combine(gfGetConfigDir(), "IgnoredLogs.txt"), txt, System.Text.Encoding.GetEncoding("UTF-8"))

    End Sub
End Class
