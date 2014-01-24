'�Q�l�Fhttp://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class MailSettings
    '�ݒ��ۑ�����t�B�[���h
    Private _blnEnabled As Boolean
    Private _strServer As String
    Private _strPort As Integer
    Private _strUser As String
    Private _strPass As String
    Private _strSendFrom As String
    Private _dtSendTo As DataTable

    '�ݒ�̃v���p�e�B

    Public Property Enabled() As Boolean
        Get
            Return _blnEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnEnabled = Value
        End Set
    End Property
    Public Property Server() As String
        Get
            Return _strServer
        End Get
        Set(ByVal Value As String)
            _strServer = Value
        End Set
    End Property
    Public Property Port() As Integer
        Get
            Return _strPort
        End Get
        Set(ByVal Value As Integer)
            _strPort = Value
        End Set
    End Property
    Public Property User() As String
        Get
            Return _strUser
        End Get
        Set(ByVal Value As String)
            _strUser = Value
        End Set
    End Property
    Public Property Pass() As String
        Get
            Return _strPass
        End Get
        Set(ByVal Value As String)
            _strPass = Value
        End Set
    End Property
    Public Property SendTo() As DataTable
        Get
            Return _dtSendTo
        End Get
        Set(ByVal Value As DataTable)
            _dtSendTo = Value
        End Set
    End Property
    Public Property SendFrom() As String
        Get
            Return _strSendFrom
        End Get
        Set(ByVal Value As String)
            _strSendFrom = Value
        End Set
    End Property

    '�R���X�g���N�^
    Public Sub New()
        _blnEnabled = False
        _strServer = ""
        _strPort = 25
        _strUser = ""
        _strPass = ""
        _strSendFrom = ""
        _dtSendTo = New DataTable("SendTo")
    End Sub

    'MailSettings�N���X�̂�����̃C���X�^���X
    <NonSerialized()> _
    Private Shared _instance As MailSettings
    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As MailSettings
        Get
            If _instance Is Nothing Then
                _instance = New MailSettings
            End If
            Return _instance
        End Get
        Set(ByVal Value As MailSettings)
            _instance = Value
        End Set
    End Property

    '�ݒ��XML�t�@�C������ǂݍ��ݕ�������
    Public Shared Sub LoadFromXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Open, FileAccess.Read)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(Settings))
        '�ǂݍ���ŋt�V���A��������
        Dim obj As Object = xs.Deserialize(fs)
        fs.Close()

        Instance = CType(obj, MailSettings)
    End Sub

    '���݂̐ݒ��XML�t�@�C���ɕۑ�����
    Public Shared Sub SaveToXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Create, FileAccess.Write)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(Settings))
        '�V���A�������ď�������
        xs.Serialize(fs, Instance)
        fs.Close()
    End Sub

    Private Shared Function GetSettingPath() As String
        Dim p As String = Path.Combine(gfGetConfigDir, GSTR_SENDMAIL_FILE)
        Return p
    End Function
End Class
