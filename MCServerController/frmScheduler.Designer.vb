<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScheduler
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.chkSendMailEnabled = New System.Windows.Forms.CheckBox()
        Me.lblEnabled = New System.Windows.Forms.Label()
        Me.cmbMode = New System.Windows.Forms.ComboBox()
        Me.pnlWeekday = New System.Windows.Forms.Panel()
        Me.rbSunday = New System.Windows.Forms.RadioButton()
        Me.rbSaturday = New System.Windows.Forms.RadioButton()
        Me.rbFriday = New System.Windows.Forms.RadioButton()
        Me.rbThursday = New System.Windows.Forms.RadioButton()
        Me.rbWednesday = New System.Windows.Forms.RadioButton()
        Me.rbTuesday = New System.Windows.Forms.RadioButton()
        Me.rbMonday = New System.Windows.Forms.RadioButton()
        Me.dtpTargetDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpTargetTime = New System.Windows.Forms.DateTimePicker()
        Me.cmbTask = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCommand = New System.Windows.Forms.TextBox()
        Me.dgvSchedule = New System.Windows.Forms.DataGridView()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pnlWeekday.SuspendLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(79, 418)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 23)
        Me.btnCancel.TabIndex = 99
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(12, 418)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(61, 23)
        Me.btnApply.TabIndex = 98
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'chkSendMailEnabled
        '
        Me.chkSendMailEnabled.AutoSize = True
        Me.chkSendMailEnabled.Location = New System.Drawing.Point(55, 9)
        Me.chkSendMailEnabled.Name = "chkSendMailEnabled"
        Me.chkSendMailEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkSendMailEnabled.TabIndex = 0
        Me.chkSendMailEnabled.UseVisualStyleBackColor = True
        '
        'lblEnabled
        '
        Me.lblEnabled.AutoSize = True
        Me.lblEnabled.Location = New System.Drawing.Point(10, 9)
        Me.lblEnabled.Name = "lblEnabled"
        Me.lblEnabled.Size = New System.Drawing.Size(39, 12)
        Me.lblEnabled.TabIndex = 15
        Me.lblEnabled.Text = "Enable"
        '
        'cmbMode
        '
        Me.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMode.FormattingEnabled = True
        Me.cmbMode.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbMode.Location = New System.Drawing.Point(50, 38)
        Me.cmbMode.Name = "cmbMode"
        Me.cmbMode.Size = New System.Drawing.Size(105, 20)
        Me.cmbMode.TabIndex = 1
        '
        'pnlWeekday
        '
        Me.pnlWeekday.Controls.Add(Me.rbSunday)
        Me.pnlWeekday.Controls.Add(Me.rbSaturday)
        Me.pnlWeekday.Controls.Add(Me.rbFriday)
        Me.pnlWeekday.Controls.Add(Me.rbThursday)
        Me.pnlWeekday.Controls.Add(Me.rbWednesday)
        Me.pnlWeekday.Controls.Add(Me.rbTuesday)
        Me.pnlWeekday.Controls.Add(Me.rbMonday)
        Me.pnlWeekday.Location = New System.Drawing.Point(14, 89)
        Me.pnlWeekday.Name = "pnlWeekday"
        Me.pnlWeekday.Size = New System.Drawing.Size(608, 22)
        Me.pnlWeekday.TabIndex = 3
        '
        'rbSunday
        '
        Me.rbSunday.AutoSize = True
        Me.rbSunday.ForeColor = System.Drawing.Color.Red
        Me.rbSunday.Location = New System.Drawing.Point(518, 3)
        Me.rbSunday.Name = "rbSunday"
        Me.rbSunday.Size = New System.Drawing.Size(60, 16)
        Me.rbSunday.TabIndex = 6
        Me.rbSunday.TabStop = True
        Me.rbSunday.Text = "Sunday"
        Me.rbSunday.UseVisualStyleBackColor = True
        '
        'rbSaturday
        '
        Me.rbSaturday.AutoSize = True
        Me.rbSaturday.ForeColor = System.Drawing.Color.Blue
        Me.rbSaturday.Location = New System.Drawing.Point(432, 3)
        Me.rbSaturday.Name = "rbSaturday"
        Me.rbSaturday.Size = New System.Drawing.Size(68, 16)
        Me.rbSaturday.TabIndex = 5
        Me.rbSaturday.TabStop = True
        Me.rbSaturday.Text = "Saturday"
        Me.rbSaturday.UseVisualStyleBackColor = True
        '
        'rbFriday
        '
        Me.rbFriday.AutoSize = True
        Me.rbFriday.Location = New System.Drawing.Point(346, 3)
        Me.rbFriday.Name = "rbFriday"
        Me.rbFriday.Size = New System.Drawing.Size(55, 16)
        Me.rbFriday.TabIndex = 4
        Me.rbFriday.TabStop = True
        Me.rbFriday.Text = "Friday"
        Me.rbFriday.UseVisualStyleBackColor = True
        '
        'rbThursday
        '
        Me.rbThursday.AutoSize = True
        Me.rbThursday.Location = New System.Drawing.Point(260, 3)
        Me.rbThursday.Name = "rbThursday"
        Me.rbThursday.Size = New System.Drawing.Size(70, 16)
        Me.rbThursday.TabIndex = 3
        Me.rbThursday.TabStop = True
        Me.rbThursday.Text = "Thursday"
        Me.rbThursday.UseVisualStyleBackColor = True
        '
        'rbWednesday
        '
        Me.rbWednesday.AutoSize = True
        Me.rbWednesday.Location = New System.Drawing.Point(174, 3)
        Me.rbWednesday.Name = "rbWednesday"
        Me.rbWednesday.Size = New System.Drawing.Size(80, 16)
        Me.rbWednesday.TabIndex = 2
        Me.rbWednesday.TabStop = True
        Me.rbWednesday.Text = "Wednesday"
        Me.rbWednesday.UseVisualStyleBackColor = True
        '
        'rbTuesday
        '
        Me.rbTuesday.AutoSize = True
        Me.rbTuesday.Location = New System.Drawing.Point(87, 3)
        Me.rbTuesday.Name = "rbTuesday"
        Me.rbTuesday.Size = New System.Drawing.Size(66, 16)
        Me.rbTuesday.TabIndex = 1
        Me.rbTuesday.TabStop = True
        Me.rbTuesday.Text = "Tuesday"
        Me.rbTuesday.UseVisualStyleBackColor = True
        '
        'rbMonday
        '
        Me.rbMonday.AutoSize = True
        Me.rbMonday.Location = New System.Drawing.Point(1, 3)
        Me.rbMonday.Name = "rbMonday"
        Me.rbMonday.Size = New System.Drawing.Size(62, 16)
        Me.rbMonday.TabIndex = 0
        Me.rbMonday.TabStop = True
        Me.rbMonday.Text = "Monday"
        Me.rbMonday.UseVisualStyleBackColor = True
        '
        'dtpTargetDate
        '
        Me.dtpTargetDate.CustomFormat = """yyyy/MM/dd"""
        Me.dtpTargetDate.Location = New System.Drawing.Point(12, 64)
        Me.dtpTargetDate.Name = "dtpTargetDate"
        Me.dtpTargetDate.Size = New System.Drawing.Size(143, 19)
        Me.dtpTargetDate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 12)
        Me.Label1.TabIndex = 101
        Me.Label1.Text = "Mode"
        '
        'dtpTargetTime
        '
        Me.dtpTargetTime.CustomFormat = """HH:mm"""
        Me.dtpTargetTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTargetTime.Location = New System.Drawing.Point(161, 64)
        Me.dtpTargetTime.Name = "dtpTargetTime"
        Me.dtpTargetTime.ShowUpDown = True
        Me.dtpTargetTime.Size = New System.Drawing.Size(82, 19)
        Me.dtpTargetTime.TabIndex = 3
        Me.dtpTargetTime.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
        '
        'cmbTask
        '
        Me.cmbTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTask.FormattingEnabled = True
        Me.cmbTask.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cmbTask.Location = New System.Drawing.Point(48, 132)
        Me.cmbTask.Name = "cmbTask"
        Me.cmbTask.Size = New System.Drawing.Size(149, 20)
        Me.cmbTask.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 12)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Task"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 12)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "Command"
        '
        'txtCommand
        '
        Me.txtCommand.Location = New System.Drawing.Point(73, 158)
        Me.txtCommand.Name = "txtCommand"
        Me.txtCommand.Size = New System.Drawing.Size(549, 19)
        Me.txtCommand.TabIndex = 5
        '
        'dgvSchedule
        '
        Me.dgvSchedule.AllowUserToAddRows = False
        Me.dgvSchedule.AllowUserToDeleteRows = False
        Me.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSchedule.Location = New System.Drawing.Point(12, 210)
        Me.dgvSchedule.Name = "dgvSchedule"
        Me.dgvSchedule.RowTemplate.Height = 21
        Me.dgvSchedule.Size = New System.Drawing.Size(610, 202)
        Me.dgvSchedule.TabIndex = 7
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(201, 181)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(282, 181)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(363, 181)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(547, 181)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 453)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.dgvSchedule)
        Me.Controls.Add(Me.txtCommand)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbTask)
        Me.Controls.Add(Me.dtpTargetTime)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlWeekday)
        Me.Controls.Add(Me.dtpTargetDate)
        Me.Controls.Add(Me.cmbMode)
        Me.Controls.Add(Me.chkSendMailEnabled)
        Me.Controls.Add(Me.lblEnabled)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmScheduler"
        Me.Text = "Scheduler"
        Me.pnlWeekday.ResumeLayout(False)
        Me.pnlWeekday.PerformLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents chkSendMailEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblEnabled As System.Windows.Forms.Label
    Friend WithEvents cmbMode As System.Windows.Forms.ComboBox
    Friend WithEvents pnlWeekday As System.Windows.Forms.Panel
    Friend WithEvents dtpTargetDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpTargetTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbTask As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCommand As System.Windows.Forms.TextBox
    Friend WithEvents dgvSchedule As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents rbSunday As System.Windows.Forms.RadioButton
    Friend WithEvents rbSaturday As System.Windows.Forms.RadioButton
    Friend WithEvents rbFriday As System.Windows.Forms.RadioButton
    Friend WithEvents rbThursday As System.Windows.Forms.RadioButton
    Friend WithEvents rbWednesday As System.Windows.Forms.RadioButton
    Friend WithEvents rbTuesday As System.Windows.Forms.RadioButton
    Friend WithEvents rbMonday As System.Windows.Forms.RadioButton
End Class
