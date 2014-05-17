'�Q�l�Fhttp://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class SchedulerSettings
    '�ݒ��ۑ�����t�B�[���h
    Private _blnEnabled As Boolean
    Private _dtSchedule As DataTable

    '�ݒ�̃v���p�e�B
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

    '�R���X�g���N�^
    Public Sub New()
        _blnEnabled = False
        _dtSchedule = New DataTable("Schedule")
    End Sub

    'MailSettings�N���X�̂�����̃C���X�^���X
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

        Instance = CType(obj, SchedulerSettings)
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
