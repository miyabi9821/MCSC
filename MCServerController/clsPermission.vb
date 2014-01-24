'参考：http://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class PermissionSettings
    '設定を保存するフィールド
    Private _blnEnabled As Boolean
    Private _strPrefixChar As String
    Private _blnTpEnabled As Boolean
    Private _intTpMode As Integer
    Private _strTpSelectedUsers As String
    Private _blnGiveEnabled As Boolean
    Private _intGiveMode As Integer
    Private _strGiveSelectedUsers As String
    Private _blnTimeEnabled As Boolean
    Private _intTimeMode As Integer
    Private _strTimeSelectedUsers As String
    Private _blnXpEnabled As Boolean
    Private _intXpMode As Integer
    Private _strXpSelectedUsers As String
    Private _blnGamemodeEnabled As Boolean
    Private _intGamemodeMode As Integer
    Private _strGamemodeSelectedUsers As String
    Private _blnKickEnabled As Boolean
    Private _intKickMode As Integer
    Private _strKickSelectedUsers As String
    Private _blnBanEnabled As Boolean
    Private _intBanMode As Integer
    Private _strBanSelectedUsers As String
    Private _blnPardonEnabled As Boolean
    Private _intPardonMode As Integer
    Private _strPardonSelectedUsers As String
    Private _blnWhitelistEnabled As Boolean
    Private _intWhitelistMode As Integer
    Private _strWhitelistSelectedUsers As String
    '2013/06/08 0.3.4
    Private _blnSpawnpointEnabled As Boolean
    Private _intSpawnpointMode As Integer
    Private _strSpawnpointSelectedUsers As String
    Private _blnWeatherEnabled As Boolean
    Private _intWeatherMode As Integer
    Private _strWeatherSelectedUsers As String

    '設定のプロパティ
    Public Property Enabled() As Boolean
        Get
            Return _blnEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnEnabled = Value
        End Set
    End Property
    Public Property PrefixChar() As String
        Get
            Return _strPrefixChar
        End Get
        Set(ByVal Value As String)
            _strPrefixChar = Value
        End Set
    End Property
    Public Property TpEnabled() As Boolean
        Get
            Return _blnTpEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnTpEnabled = Value
        End Set
    End Property
    Public Property TpMode() As Integer
        Get
            Return _intTpMode
        End Get
        Set(ByVal Value As Integer)
            _intTpMode = Value
        End Set
    End Property
    Public Property TpSelectedUsers() As String
        Get
            Return _strTpSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strTpSelectedUsers = Value
        End Set
    End Property
    Public Property GiveEnabled() As Boolean
        Get
            Return _blnGiveEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnGiveEnabled = Value
        End Set
    End Property
    Public Property GiveMode() As Integer
        Get
            Return _intGiveMode
        End Get
        Set(ByVal Value As Integer)
            _intGiveMode = Value
        End Set
    End Property
    Public Property GiveSelectedUsers() As String
        Get
            Return _strGiveSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strGiveSelectedUsers = Value
        End Set
    End Property
    Public Property TimeEnabled() As Boolean
        Get
            Return _blnTimeEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnTimeEnabled = Value
        End Set
    End Property
    Public Property TimeMode() As Integer
        Get
            Return _intTimeMode
        End Get
        Set(ByVal Value As Integer)
            _intTimeMode = Value
        End Set
    End Property
    Public Property TimeSelectedUsers() As String
        Get
            Return _strTimeSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strTimeSelectedUsers = Value
        End Set
    End Property
    Public Property XpEnabled() As Boolean
        Get
            Return _blnXpEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnXpEnabled = Value
        End Set
    End Property
    Public Property XpMode() As Integer
        Get
            Return _intXpMode
        End Get
        Set(ByVal Value As Integer)
            _intXpMode = Value
        End Set
    End Property
    Public Property XpSelectedUsers() As String
        Get
            Return _strXpSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strXpSelectedUsers = Value
        End Set
    End Property
    Public Property GamemodeEnabled() As Boolean
        Get
            Return _blnGamemodeEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnGamemodeEnabled = Value
        End Set
    End Property
    Public Property GamemodeMode() As Integer
        Get
            Return _intGamemodeMode
        End Get
        Set(ByVal Value As Integer)
            _intGamemodeMode = Value
        End Set
    End Property
    Public Property GamemodeSelectedUsers() As String
        Get
            Return _strGamemodeSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strGamemodeSelectedUsers = Value
        End Set
    End Property
    Public Property KickEnabled() As Boolean
        Get
            Return _blnKickEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnKickEnabled = Value
        End Set
    End Property
    Public Property KickMode() As Integer
        Get
            Return _intKickMode
        End Get
        Set(ByVal Value As Integer)
            _intKickMode = Value
        End Set
    End Property
    Public Property KickSelectedUsers() As String
        Get
            Return _strKickSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strKickSelectedUsers = Value
        End Set
    End Property
    Public Property BanEnabled() As Boolean
        Get
            Return _blnBanEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnBanEnabled = Value
        End Set
    End Property
    Public Property BanMode() As Integer
        Get
            Return _intBanMode
        End Get
        Set(ByVal Value As Integer)
            _intBanMode = Value
        End Set
    End Property
    Public Property BanSelectedUsers() As String
        Get
            Return _strBanSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strBanSelectedUsers = Value
        End Set
    End Property
    Public Property PardonEnabled() As Boolean
        Get
            Return _blnPardonEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnPardonEnabled = Value
        End Set
    End Property
    Public Property PardonMode() As Integer
        Get
            Return _intPardonMode
        End Get
        Set(ByVal Value As Integer)
            _intPardonMode = Value
        End Set
    End Property
    Public Property PardonSelectedUsers() As String
        Get
            Return _strPardonSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strPardonSelectedUsers = Value
        End Set
    End Property
    Public Property WhitelistEnabled() As Boolean
        Get
            Return _blnWhitelistEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnWhitelistEnabled = Value
        End Set
    End Property
    Public Property WhitelistMode() As Integer
        Get
            Return _intWhitelistMode
        End Get
        Set(ByVal Value As Integer)
            _intWhitelistMode = Value
        End Set
    End Property
    Public Property WhitelistSelectedUsers() As String
        Get
            Return _strWhitelistSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strWhitelistSelectedUsers = Value
        End Set
    End Property
    Public Property SpawnpointEnabled() As Boolean
        Get
            Return _blnSpawnpointEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnSpawnpointEnabled = Value
        End Set
    End Property
    Public Property SpawnpointMode() As Integer
        Get
            Return _intSpawnpointMode
        End Get
        Set(ByVal Value As Integer)
            _intSpawnpointMode = Value
        End Set
    End Property
    Public Property SpawnpointSelectedUsers() As String
        Get
            Return _strSpawnpointSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strSpawnpointSelectedUsers = Value
        End Set
    End Property
    Public Property WeatherEnabled() As Boolean
        Get
            Return _blnWeatherEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnWeatherEnabled = Value
        End Set
    End Property
    Public Property WeatherMode() As Integer
        Get
            Return _intWeatherMode
        End Get
        Set(ByVal Value As Integer)
            _intWeatherMode = Value
        End Set
    End Property
    Public Property WeatherSelectedUsers() As String
        Get
            Return _strWeatherSelectedUsers
        End Get
        Set(ByVal Value As String)
            _strWeatherSelectedUsers = Value
        End Set
    End Property

    'コンストラクタ
    Public Sub New()
        _blnEnabled = False
        _strPrefixChar = "@"
        _blnTpEnabled = False
        _intTpMode = 1
        _strTpSelectedUsers = ""
        _blnGiveEnabled = False
        _intGiveMode = 1
        _strGiveSelectedUsers = ""
        _blnTimeEnabled = False
        _intTimeMode = 1
        _strTimeSelectedUsers = ""
        _blnXpEnabled = False
        _intXpMode = 1
        _strXpSelectedUsers = ""
        _blnGamemodeEnabled = False
        _intGamemodeMode = 1
        _strGamemodeSelectedUsers = ""
        _blnKickEnabled = False
        _intKickMode = 1
        _strKickSelectedUsers = ""
        _blnBanEnabled = False
        _intBanMode = 1
        _strBanSelectedUsers = ""
        _blnPardonEnabled = False
        _intPardonMode = 1
        _strPardonSelectedUsers = ""
        _blnWhitelistEnabled = False
        _intWhitelistMode = 1
        _strWhitelistSelectedUsers = ""
        _blnSpawnpointEnabled = False
        _intSpawnpointMode = 1
        _strSpawnpointSelectedUsers = ""
        _blnWeatherEnabled = False
        _intWeatherMode = 1
        _strWeatherSelectedUsers = ""

    End Sub

    'PermissionSettingsクラスのただ一つのインスタンス
    <NonSerialized()> _
    Private Shared _instance As PermissionSettings
    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As PermissionSettings
        Get
            If _instance Is Nothing Then
                _instance = New PermissionSettings
            End If
            Return _instance
        End Get
        Set(ByVal Value As PermissionSettings)
            _instance = Value
        End Set
    End Property

    '設定をXMLファイルから読み込み復元する
    Public Shared Sub LoadFromXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Open, FileAccess.Read)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(PermissionSettings))
        '読み込んで逆シリアル化する
        Dim obj As Object = xs.Deserialize(fs)
        fs.Close()

        Instance = CType(obj, PermissionSettings)
    End Sub

    '現在の設定をXMLファイルに保存する
    Public Shared Sub SaveToXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Create, FileAccess.Write)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(PermissionSettings))
        'シリアル化して書き込む
        xs.Serialize(fs, Instance)
        fs.Close()
    End Sub

    Private Shared Function GetSettingPath() As String
        Dim p As String = Path.Combine(gfGetConfigDir, GSTR_PERMISSION_FILE)
        Return p
    End Function
End Class
