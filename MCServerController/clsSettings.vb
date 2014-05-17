'参考：http://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class Settings
    '設定を保存するフィールド
    Private _ptWindowPos As Point
    Private _szWindowSize As Size
    Private _blnWindowMaximize As Boolean
    Private _strJarPath As String
    Private _strJavaPath As String
    Private _strAugment As String
    Private _strJarFileAugment As String
    Private _blnAutoStart As Boolean
    Private _blnAutoRecovery As Boolean
    Private _blnShowConsole As Boolean
    Private _intServerVersion As Integer
    Private _intServerMode As Integer
    Private _lstCommandRecent As List(Of String)
    Private _intHeartBeatInterval As Integer
    Private _intHeartBeatStopCount As Integer
    Private _intHeartBeatKillCount As Integer
    Private _intHeartBeatUse0xFE As Boolean
    Private _blnBackupEnabled As Boolean
    Private _intBackupInterval As Integer
    Private _intBackupKeepDays As Integer
    Private _intBackupBeforeServerRun As Boolean
    Private _strBackupOutput As String
    Private _blnCompressEnabled As Boolean
    Private _blnMCBansEnabled As Boolean
    Private _strMCBansAPIKey As String
    Private _dblMCBansMinRep As Double
    Private _blnMCBansFailSafe As Boolean
    Private _blnExtendedPlayersListEnabled As Boolean
    Private _dtBackupTarget As DataTable

    '設定のプロパティ
    Public Property WindowPos() As Point
        Get
            Return _ptWindowPos
        End Get
        Set(ByVal Value As Point)
            _ptWindowPos = Value
        End Set
    End Property
    Public Property WindowSize() As Size
        Get
            Return _szWindowSize
        End Get
        Set(ByVal Value As Size)
            _szWindowSize = Value
        End Set
    End Property
    Public Property WindowMaximize() As Boolean
        Get
            Return _blnWindowMaximize
        End Get
        Set(ByVal Value As Boolean)
            _blnWindowMaximize = Value
        End Set
    End Property
    Public Property JarPath() As String
        Get
            Return _strJarPath
        End Get
        Set(ByVal Value As String)
            _strJarPath = Value
        End Set
    End Property
    Public Property JavaPath() As String
        Get
            Return _strJavaPath
        End Get
        Set(ByVal Value As String)
            _strJavaPath = Value
        End Set
    End Property
    Public Property Augment() As String
        Get
            Return _strAugment
        End Get
        Set(ByVal Value As String)
            _strAugment = Value
        End Set
    End Property
    Public Property JarFileAugment() As String
        Get
            Return _strJarFileAugment
        End Get
        Set(ByVal Value As String)
            _strJarFileAugment = Value
        End Set
    End Property
    Public Property AutoStart() As Boolean
        Get
            Return _blnAutoStart
        End Get
        Set(ByVal Value As Boolean)
            _blnAutoStart = Value
        End Set
    End Property
    Public Property AutoRecovery() As Boolean
        Get
            Return _blnAutoRecovery
        End Get
        Set(ByVal Value As Boolean)
            _blnAutoRecovery = Value
        End Set
    End Property
    Public Property ShowConsole() As Boolean
        Get
            Return _blnShowConsole
        End Get
        Set(ByVal Value As Boolean)
            _blnShowConsole = Value
        End Set
    End Property
    Public Property ServerVersion() As Integer
        Get
            Return _intServerVersion
        End Get
        Set(ByVal Value As Integer)
            _intServerVersion = Value
        End Set
    End Property
    Public Property ServerMode() As Integer
        Get
            Return _intServerMode
        End Get
        Set(ByVal Value As Integer)
            _intServerMode = Value
        End Set
    End Property
    Public Property CommandRecent() As List(Of String)
        Get
            Return _lstCommandRecent
        End Get
        Set(ByVal Value As List(Of String))
            _lstCommandRecent = Value
        End Set
    End Property
    Public Property HeartBeatInterval() As Integer
        Get
            Return _intHeartBeatInterval
        End Get
        Set(ByVal Value As Integer)
            _intHeartBeatInterval = Value
        End Set
    End Property
    Public Property HeartBeatStopCount() As Integer
        Get
            Return _intHeartBeatStopCount
        End Get
        Set(ByVal Value As Integer)
            _intHeartBeatStopCount = Value
        End Set
    End Property
    Public Property HeartBeatKillCount() As Integer
        Get
            Return _intHeartBeatKillCount
        End Get
        Set(ByVal Value As Integer)
            _intHeartBeatKillCount = Value
        End Set
    End Property
    Public Property HeartBeatUse0xFE() As Boolean
        Get
            Return _intHeartBeatUse0xFE
        End Get
        Set(ByVal Value As Boolean)
            _intHeartBeatUse0xFE = Value
        End Set
    End Property
    Public Property BackupEnabled() As Boolean
        Get
            Return _blnBackupEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnBackupEnabled = Value
        End Set
    End Property
    Public Property BackupInterval() As Integer
        Get
            Return _intBackupInterval
        End Get
        Set(ByVal Value As Integer)
            _intBackupInterval = Value
        End Set
    End Property
    Public Property BackupKeepDays() As Integer
        Get
            Return _intBackupKeepDays
        End Get
        Set(ByVal Value As Integer)
            _intBackupKeepDays = Value
        End Set
    End Property
    Public Property BackupBeforeServerRun() As Boolean
        Get
            Return _intBackupBeforeServerRun
        End Get
        Set(ByVal Value As Boolean)
            _intBackupBeforeServerRun = Value
        End Set
    End Property
    Public Property BackupOutput() As String
        Get
            Return _strBackupOutput
        End Get
        Set(ByVal Value As String)
            _strBackupOutput = Value
        End Set
    End Property
    Public Property CompressEnabled() As Boolean
        Get
            Return _blnCompressEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnCompressEnabled = Value
        End Set
    End Property
    Public Property MCBansEnabled() As Boolean
        Get
            Return _blnMCBansEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnMCBansEnabled = Value
        End Set
    End Property
    Public Property MCBansAPIKey() As String
        Get
            Return _strMCBansAPIKey
        End Get
        Set(ByVal Value As String)
            _strMCBansAPIKey = Value
        End Set
    End Property
    Public Property MCBansMinRep() As Double
        Get
            Return _dblMCBansMinRep
        End Get
        Set(ByVal Value As Double)
            _dblMCBansMinRep = Value
        End Set
    End Property
    Public Property MCBansFailSafe() As Boolean
        Get
            Return _blnMCBansFailSafe
        End Get
        Set(ByVal Value As Boolean)
            _blnMCBansFailSafe = Value
        End Set
    End Property
    Public Property ExtendedPlayersListEnabled() As Boolean
        Get
            Return _blnExtendedPlayersListEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnExtendedPlayersListEnabled = Value
        End Set
    End Property
    Public Property BackupTarget() As DataTable
        Get
            Return _dtBackupTarget
        End Get
        Set(ByVal Value As DataTable)
            _dtBackupTarget = Value
        End Set
    End Property

    'コンストラクタ
    Public Sub New()
        _ptWindowPos = New Point(0, 0)
        _szWindowSize = New Size(800, 600)
        _blnWindowMaximize = False
        _strJarPath = ""
        _strJavaPath = ""
        _strAugment = ""
        _strJarFileAugment = ""
        _blnAutoStart = False
        _blnAutoRecovery = False
        _blnShowConsole = False
        _intServerMode = 0
        _intServerVersion = 2
        _intHeartBeatInterval = 60
        _intHeartBeatStopCount = 3
        _intHeartBeatKillCount = 6
        _intHeartBeatUse0xFE = False
        _lstCommandRecent = New List(Of String)
        _blnBackupEnabled = False
        _intBackupInterval = 60
        _intBackupKeepDays = 7
        _intBackupBeforeServerRun = False
        _strBackupOutput = ""
        _blnCompressEnabled = False
        _blnMCBansEnabled = False
        _strMCBansAPIKey = ""
        _dblMCBansMinRep = 3
        _blnExtendedPlayersListEnabled = False
        _dtBackupTarget = New DataTable("BackupTarget")
    End Sub

    'Settingsクラスのただ一つのインスタンス
    <NonSerialized()> _
    Private Shared _instance As Settings
    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As Settings
        Get
            If _instance Is Nothing Then
                _instance = New Settings
            End If
            Return _instance
        End Get
        Set(ByVal Value As Settings)
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

        Instance = CType(obj, Settings)
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
        Dim p As String = Path.Combine(gfGetConfigDir, GSTR_CONFIG_FILE)
        Return p
    End Function
End Class