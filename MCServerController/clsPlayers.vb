Imports System.Collections
Imports System.Windows.Forms

'ListViewItemComparerNX๐tH[R[hษวม
Class ListViewItemComparer
    Implements IComparer

    Private col As Integer
    Private sort As Integer

    Public Sub New()
        col = 0
        sort = 0
    End Sub

    Public Sub New(ByVal column As Integer, ByVal sortflg As Integer)
        'column  : ๑ิ
        'sortflg : \[g(0=ธ,1=~)
        '--------------------------------
        col = column
        sort = sortflg
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare
        If sort = 0 Then
            'ธ
            Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                    CType(y, ListViewItem).SubItems(col).Text)
        Else
            '~
            Return -[String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                     CType(y, ListViewItem).SubItems(col).Text)
        End If
    End Function
End Class
