'�Q�l�Fhttp://dobon.net/vb/dotnet/programing/storeappsettings.html
Imports System
Imports System.IO

<Serializable()> _
Public Class NGWSettings
    '�ݒ��ۑ�����t�B�[���h
    Private _blnNGWordsEnabled As Boolean
    Private _dtNGWords As DataTable

    '�ݒ�̃v���p�e�B

    Public Property NGWordsEnabled() As Boolean
        Get
            Return _blnNGWordsEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnNGWordsEnabled = Value
        End Set
    End Property
    Public Property NGWords() As DataTable
        Get
            Return _dtNGWords
        End Get
        Set(ByVal Value As DataTable)
            _dtNGWords = Value
        End Set
    End Property


    '�R���X�g���N�^
    Public Sub New()
        _blnNGWordsEnabled = False
        _dtNGWords = New DataTable("NGWords")
    End Sub

    'NGWSettings�N���X�̂�����̃C���X�^���X
    <NonSerialized()> _
    Private Shared _instance As NGWSettings
    <System.Xml.Serialization.XmlIgnore()> _
    Public Shared Property Instance() As NGWSettings
        Get
            If _instance Is Nothing Then
                _instance = New NGWSettings
            End If
            Return _instance
        End Get
        Set(ByVal Value As NGWSettings)
            _instance = Value
        End Set
    End Property

    '�ݒ��XML�t�@�C������ǂݍ��ݕ�������
    Public Shared Sub LoadFromXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Open, FileAccess.Read)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(NGWSettings))
        '�ǂݍ���ŋt�V���A��������
        Dim obj As Object = xs.Deserialize(fs)
        fs.Close()

        Instance = CType(obj, NGWSettings)
    End Sub

    '���݂̐ݒ��XML�t�@�C���ɕۑ�����
    Public Shared Sub SaveToXmlFile()
        Dim p As String = GetSettingPath()

        Dim fs As New FileStream( _
            p, FileMode.Create, FileAccess.Write)
        Dim xs As New System.Xml.Serialization.XmlSerializer( _
            GetType(NGWSettings))
        '�V���A�������ď�������
        xs.Serialize(fs, Instance)
        fs.Close()
    End Sub

    Private Shared Function GetSettingPath() As String
        Dim p As String = Path.Combine(gfGetConfigDir, GSTR_NGWORDS_FILE)
        Return p
    End Function
End Class
