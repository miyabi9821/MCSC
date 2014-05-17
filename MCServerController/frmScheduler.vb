Public Class frmScheduler

#Region "Initialize"
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '                 Initialize  &  Settings
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '----------------------------------------------------------
    '���\�b�h���FSetupDataGridView
    '   �T�v�FGridView�S�ʂ̐ݒ�A�J�����̐ݒ���s��
    '  �߂�l�F�Ȃ�
    '   ���l�F�Ȃ�
    ' �X�V���F2013/06/30
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
    '���\�b�h���FSetupDataGridView
    '   �T�v�FGridView�S�ʂ̐ݒ�A�J�����̐ݒ���s��
    '  �߂�l�F�Ȃ�
    '   ���l�F�Ȃ�
    ' �X�V���F2013/06/30
    '----------------------------------------------------------
    Private Sub SetupDataGridView()
        'GridView�̃Z���X�^�C����ݒ�
        With dgvSchedule.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .Font = New Font(dgvSchedule.Font, FontStyle.Bold)
        End With

        'GridView�̊O�ς�ݒ�
        With dgvSchedule
            .Name = "dgvSchedule"
            .AutoSizeRowsMode = _
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
            .CellBorderStyle = DataGridViewCellBorderStyle.Single
            '�F��ݒ�
            .GridColor = Color.Black
            '�s�̃w�b�_�[��\��
            .RowHeadersVisible = False
            '�s�I�����ɁA�S�ẴJ������I������悤�ݒ�
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '�����s�I���𖳌���
            .MultiSelect = False


            '�L�������f�[�^�ݒ�
            Dim colEnable As New DataGridViewCheckBoxColumn
            colEnable.HeaderText = ""
            colEnable.Name = "chEnable"
            colEnable.Width = 30
            .Columns.Add(colEnable)

            '�o�C���h�f�[�^��ݒ�
            Dim bindingMode As New BindingSource
            bindingMode.Add("hourly")
            bindingMode.Add("daily")
            bindingMode.Add("weekly")
            bindingMode.Add("monthly")
            bindingMode.Add("Specified date")

            '���[�h���ǉ�
            Dim colMode As New DataGridViewComboBoxColumn
            colMode.HeaderText = "Mode"
            colMode.Name = "chMode"
            colMode.DataSource = bindingMode
            .Columns.Add(colMode)

            '���t���ǉ�
            Dim colDate As New DataGridViewTextBoxColumn
            colDate.Name = "chDate"
            colDate.HeaderText = "Date"
            .Columns.Add(colDate)

            '�T���ǉ�
            Dim colWeekday As New DataGridViewTextBoxColumn
            colWeekday.Name = "chWeekday"
            colWeekday.HeaderText = "Weekday"
            .Columns.Add(colWeekday)

            '�T���ǉ�
            Dim colTime As New DataGridViewTextBoxColumn
            colTime.Name = "chTime"
            colTime.HeaderText = "Time"
            .Columns.Add(colTime)

            '�o�C���h�f�[�^��ݒ�
            Dim bindingTask As New BindingSource
            bindingTask.Add("Server Startup")
            bindingTask.Add("Server Shutdown")
            bindingTask.Add("Data Backup")
            bindingTask.Add("Send Command")
            bindingTask.Add("Run Other Apps")
            '�T���ǉ�
            Dim colTask As New DataGridViewComboBoxColumn
            colTask.Name = "chTask"
            colTask.HeaderText = "Task"
            colTask.DataSource = bindingTask
            .Columns.Add(colTask)

            '�T���ǉ�
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
        '���[�h�ݒ�
        psSetMode()

        '�^�X�N�ݒ�
        psSetTask()

        'GridView�ݒ�
        SetupDataGridView()
    End Sub

#End Region

#Region "Form Event Method"
    '----------------------------------------------------------
    '���\�b�h���FbtnAdd_Click
    '   �T�v�F�ǉ��{�^�������ōs��ǉ�����
    '  �߂�l�F�Ȃ�
    '   ���l�F�Ȃ�
    '�X�V���F2013/06/30
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
    '���\�b�h���FbtnDelete_Click
    '   �T�v�F�폜�{�^�������ōs���폜����
    '  �߂�l�F�Ȃ�
    '   ���l�F�Ȃ�
    '�X�V���F2013/06/30
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
    '���\�b�h���FcmbMode_SelectedIndexChanged
    '   �T�v�FcmbMode�ύX���Ƀp�����^�̗L��������؂�ւ���
    '  �߂�l�F�Ȃ�
    '   ���l�F�Ȃ�
    '�X�V���F2013/06/30
    '----------------------------------------------------------
    Private Sub cmbMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMode.SelectedIndexChanged
        Select Case cmbMode.SelectedIndex
            Case 0 '����
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 1 '����
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 2 '���T
                dtpTargetDate.Enabled = False
                pnlWeekday.Enabled = True
                dtpTargetTime.Enabled = True
            Case 3 '����
                dtpTargetDate.Enabled = True
                pnlWeekday.Enabled = False
                dtpTargetTime.Enabled = True
            Case 4 '���t�w��
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
    '���\�b�h���FdgvSchedule_CellEnter
    '   �T�v�FComboBox�����̃N���b�N�ŕҏW���[�h�Ɉڍs����悤�ɂ���
    '  �X�V���F2013/06/30
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
    '���\�b�h���FdgvSchedule_CellFormatting
    '�X�V���F2013/06/30
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