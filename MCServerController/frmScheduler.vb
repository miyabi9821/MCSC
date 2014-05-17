Public Class frmScheduler

#Region "Initialize"
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '                 Initialize  &  Settings
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '----------------------------------------------------------
    'メソッド名：SetupDataGridView
    '   概要：GridView全般の設定、カラムの設定を行う
    '  戻り値：なし
    '   備考：なし
    ' 更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub psSetMode()
        cmbMode.Items.Clear()
        cmbMode.Items.Add("hourly")
        cmbMode.Items.Add("daily")
        cmbMode.Items.Add("weekly")
        cmbMode.Items.Add("monthly")
        cmbMode.Items.Add("Specified date")
        cmbMode.SelectedIndex = 0
    End Sub

    Private Sub psSetTask()
        cmbTask.Items.Clear()
        cmbTask.Items.Add("Server Startup")
        cmbTask.Items.Add("Server Shutdown")
        cmbTask.Items.Add("Data Backup")
        cmbTask.Items.Add("Send Command")
        cmbTask.Items.Add("Run Other Apps")
        cmbTask.SelectedIndex = 0
    End Sub


    '----------------------------------------------------------
    'メソッド名：SetupDataGridView
    '   概要：GridView全般の設定、カラムの設定を行う
    '  戻り値：なし
    '   備考：なし
    ' 更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub SetupDataGridView()
        'GridViewのセルスタイルを設定
        With dgvSchedule.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font(dgvSchedule.Font, FontStyle.Bold)
        End With

        'GridViewの外観を設定
        With dgvSchedule
            .Name = "dgvSchedule"
            .AutoSizeRowsMode = _
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            .CellBorderStyle = DataGridViewCellBorderStyle.Single
            '色を設定
            .GridColor = Color.Black
            '行のヘッダーを表示
            .RowHeadersVisible = False
            '行選択時に、全てのカラムを選択するよう設定
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '複数行選択を無効化
            .MultiSelect = False


            '有効無効データ設定
            Dim colEnable As New DataGridViewCheckBoxColumn
            colEnable.HeaderText = ""
            colEnable.Name = "chEnable"
            colEnable.Width = 30
            .Columns.Add(colEnable)

            'バインドデータを設定
            Dim bindingMode As New BindingSource
            bindingMode.Add("hourly")
            bindingMode.Add("daily")
            bindingMode.Add("weekly")
            bindingMode.Add("monthly")
            bindingMode.Add("Specified date")

            'モード列を追加
            Dim colMode As New DataGridViewComboBoxColumn
            colMode.HeaderText = "Mode"
            colMode.Name = "chMode"
            colMode.DataSource = bindingMode
            .Columns.Add(colMode)

            '日付列を追加
            Dim colDate As New DataGridViewTextBoxColumn
            colDate.Name = "chDate"
            colDate.HeaderText = "Date"
            .Columns.Add(colDate)

            '週列を追加
            Dim colWeekday As New DataGridViewTextBoxColumn
            colWeekday.Name = "chWeekday"
            colWeekday.HeaderText = "Weekday"
            .Columns.Add(colWeekday)

            '週列を追加
            Dim colTime As New DataGridViewTextBoxColumn
            colTime.Name = "chTime"
            colTime.HeaderText = "Time"
            .Columns.Add(colTime)

            'バインドデータを設定
            Dim bindingTask As New BindingSource
            bindingTask.Add("Server Startup")
            bindingTask.Add("Server Shutdown")
            bindingTask.Add("Data Backup")
            bindingTask.Add("Send Command")
            bindingTask.Add("Run Other Apps")
            '週列を追加
            Dim colTask As New DataGridViewComboBoxColumn
            colTask.Name = "chTask"
            colTask.HeaderText = "Task"
            colTask.DataSource = bindingTask
            .Columns.Add(colTask)

            '週列を追加
            Dim colCommand As New DataGridViewTextBoxColumn
            colCommand.Name = "chCommand"
            colCommand.HeaderText = "Command"
            .Columns.Add(colCommand)


        End With
    End Sub

#End Region

#Region "Form MainMethod"
    Private Sub Form1_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load
        'モード設定
        psSetMode()

        'タスク設定
        psSetTask()

        'GridView設定
        SetupDataGridView()
    End Sub

#End Region

#Region "Form Event Method"
    '----------------------------------------------------------
    'メソッド名：btnAdd_Click
    '   概要：追加ボタン押下で行を追加する
    '  戻り値：なし
    '   備考：なし
    '更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub btnAdd_Click(ByVal sender As Object, _
        ByVal e As EventArgs) Handles btnAdd.Click

        With Me.dgvSchedule
            .Rows.Add()
            Dim intRow As Integer = dgvSchedule.Rows.Count - 1
            .Rows(intRow).Cells("chEnable").Value = True
            .Rows(intRow).Cells("chMode").Value = cmbMode.SelectedItem
            .Rows(intRow).Cells("chDate").Value = dtpTargetDate.Value
            .Rows(intRow).Cells("chWeekday").Value = pnlWeekday
            .Rows(intRow).Cells("chTime").Value = dtpTargetTime.Value
            .Rows(intRow).Cells("chTask").Value = cmbTask.SelectedItem
            .Rows(intRow).Cells("chCommand").Value = txtCommand.Text
        End With
    End Sub

    '----------------------------------------------------------
    'メソッド名：btnDelete_Click
    '   概要：削除ボタン押下で行を削除する
    '  戻り値：なし
    '   備考：なし
    '更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub btnDelete_Click(ByVal sender As Object, _
        ByVal e As EventArgs) Handles btnDelete.Click

        If Me.dgvSchedule.SelectedRows.Count > 0 AndAlso _
           Not Me.dgvSchedule.SelectedRows(0).Index = _
              Me.dgvSchedule.Rows.Count - 1 Then

            Me.dgvSchedule.Rows.RemoveAt( _
               Me.dgvSchedule.SelectedRows(0).Index)

        End If
    End Sub


    '----------------------------------------------------------
    'メソッド名：cmbMode_SelectedIndexChanged
    '   概要：cmbMode変更時にパラメタの有効無効を切り替える
    '  戻り値：なし
    '   備考：なし
    '更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub cmbMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMode.SelectedIndexChanged
        Select Case cmbMode.SelectedIndex
            Case 0 '毎時
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 1 '毎日
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 2 '毎週
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = True
                dtpTargetTime.Enabled = True
            Case 3 '毎月
                dtpTargetDate.Enabled = True
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 4 '日付指定
                dtpTargetDate.Enabled = True
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case Else
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = False
        End Select
    End Sub

#End Region

#Region "Option Method"

    '----------------------------------------------------------
    'メソッド名：dgvSchedule_CellEnter
    '   概要：ComboBoxが一回のクリックで編集モードに移行するようにする
    '  更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub dgvSchedule_CellEnter(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles dgvSchedule.CellEnter
        Dim dgv As DataGridView = CType(sender, DataGridView)

        If dgv.Columns(e.ColumnIndex).Name = "chMode" AndAlso _
            TypeOf dgv.Columns(e.ColumnIndex) Is DataGridViewComboBoxColumn Then
            SendKeys.Send("{F4}")
        End If
    End Sub


    '----------------------------------------------------------
    'メソッド名：dgvSchedule_CellFormatting
    '更新日：2013/06/30
    '----------------------------------------------------------
    Private Sub dgvSchedule_CellFormatting(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) _
        Handles dgvSchedule.CellFormatting

        If e IsNot Nothing Then

            If Me.dgvSchedule.Columns(e.ColumnIndex).Name = _
            "Release Date" Then
                If e.Value IsNot Nothing Then
                    Try
                        e.Value = DateTime.Parse(e.Value.ToString()) _
                            .ToLongDateString()
                        e.FormattingApplied = True
                    Catch ex As FormatException
                        Console.WriteLine("{0} is not a valid date.", e.Value.ToString())
                    End Try
                End If
            End If

        End If

    End Sub
#End Region

End Class