Imports System.Collections
Imports System.Windows.Forms

'ListViewItemComparerクラスをフォームコードに追加
Class ListViewItemComparer
    Implements IComparer

    Private col As Integer
    Private sort As Integer

    Public Sub New()
        col = 0
        sort = 0
    End Sub

    Public Sub New(ByVal column As Integer, ByVal sortflg As Integer)
        'column  : 列番号
        'sortflg : ソート(0=昇順,1=降順)
        '--------------------------------
        col = column
        sort = sortflg
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare
        If sort = 0 Then
            '昇順
            Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                    CType(y, ListViewItem).SubItems(col).Text)
        Else
            '降順
            Return -[String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                     CType(y, ListViewItem).SubItems(col).Text)
        End If
    End Function
End Class
