'参考：http://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class SchedulerSettings
    '設定を保存するフィールド
    Private _blnEnabled As Boolean
    Private _dtSchedule As DataTable

    '設定のプロパティ
    Public Property Enabled() As Boolean
        Get
            Return _blnEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnEnabled = Value
        End Set
    End Property
    Public Property Schedule() As DataTable
        Get
            Return _dtSchedule
        End Get
        Set(ByVal Value As DataTable)
            _dtSchedule = Value
        End Set
    End Property

    'コンストラクタ
    Public Sub New()
        _blnEnabled = False
        _dtSchedule = New DataTable("Schedule")
    End Sub

    'MailSettingsクラスのただ一つのインスタンス
    <NonSerialized()> _
    Private Shared _instance As SchedulerSettings
    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As SchedulerSettings
        Get
            If _instance Is Nothing Then
                _instance = New SchedulerSettings
            End If
            Return _instance
        End Get
        Set(ByVal Value As SchedulerSettings)
            _instance = Value
        End Set
    End Property

    '設定をXMLファイルから読み込み復元する
    Public Shared Sub LoadFromXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Open, FileAccess.Read)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(Settings))
        '読み込んで逆シリアル化する
        Dim obj As Object = xs.Deserialize(fs)
        fs.Close()

        Instance = CType(obj, SchedulerSettings)
    End Sub

    '現在の設定をXMLファイルに保存する
    Public Shared Sub SaveToXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Create, FileAccess.Write)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(Settings))
        'シリアル化して書き込む
        xs.Serialize(fs, Instance)
        fs.Close()
    End Sub

    Private Shared Function GetSettingPath() As String
        Dim p As String = Path.Combine(gfGetConfigDir, GSTR_SENDMAIL_FILE)
        Return p
    End Function

End Class
