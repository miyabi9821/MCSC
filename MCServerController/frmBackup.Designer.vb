<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackup
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
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.epInput = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.clbBackupTarget = New System.Windows.Forms.CheckedListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnBrowsTargetFile = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBrowsOutput = New System.Windows.Forms.Button
        Me.txtInterval = New System.Windows.Forms.TextBox
        Me.txtBackupOutput = New System.Windows.Forms.TextBox
        Me.txtBackupTarget = New System.Windows.Forms.TextBox
        Me.lblEnabled = New System.Windows.Forms.Label
        Me.chkBackupEnabled = New System.Windows.Forms.CheckBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnBrowsTargetDir = New System.Windows.Forms.Button
        Me.fbdTarget = New System.Windows.Forms.FolderBrowserDialog
        Me.ofdTarget = New System.Windows.Forms.OpenFileDialog
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtKeepDays = New System.Windows.Forms.TextBox
        Me.gbCompressOption = New System.Windows.Forms.GroupBox
        Me.chkCompressEnabled = New System.Windows.Forms.CheckBox
        Me.chkBackupBeforeRun = New System.Windows.Forms.CheckBox
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCompressOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Interval (minute)"
        '
        'epInput
        '
        Me.epInput.ContainerControl = Me
        '
        'clbBackupTarget
        '
        Me.clbBackupTarget.AllowDrop = True
        Me.clbBackupTarget.FormattingEnabled = True
        Me.clbBackupTarget.Location = New System.Drawing.Point(11, 201)
        Me.clbBackupTarget.Name = "clbBackupTarget"
        Me.clbBackupTarget.Size = New System.Drawing.Size(610, 116)
        Me.clbBackupTarget.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(171, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Backup Target Files/Directories"
        '
        'btnBrowsTargetFile
        '
        Me.btnBrowsTargetFile.Location = New System.Drawing.Point(12, 145)
        Me.btnBrowsTargetFile.Name = "btnBrowsTargetFile"
        Me.btnBrowsTargetFile.Size = New System.Drawing.Size(77, 23)
        Me.btnBrowsTargetFile.TabIndex = 5
        Me.btnBrowsTargetFile.Text = "Select File"
        Me.btnBrowsTargetFile.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Backup Output Directory"
        '
        'btnBrowsOutput
        '
        Me.btnBrowsOutput.Location = New System.Drawing.Point(14, 94)
        Me.btnBrowsOutput.Name = "btnBrowsOutput"
        Me.btnBrowsOutput.Size = New System.Drawing.Size(47, 23)
        Me.btnBrowsOutput.TabIndex = 7
        Me.btnBrowsOutput.Text = "参照"
        Me.btnBrowsOutput.UseVisualStyleBackColor = True
        '
        'txtInterval
        '
        Me.txtInterval.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtInterval.Location = New System.Drawing.Point(107, 27)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(76, 19)
        Me.txtInterval.TabIndex = 2
        '
        'txtBackupOutput
        '
        Me.txtBackupOutput.Location = New System.Drawing.Point(68, 96)
        Me.txtBackupOutput.Name = "txtBackupOutput"
        Me.txtBackupOutput.Size = New System.Drawing.Size(554, 19)
        Me.txtBackupOutput.TabIndex = 4
        '
        'txtBackupTarget
        '
        Me.txtBackupTarget.Location = New System.Drawing.Point(95, 147)
        Me.txtBackupTarget.Name = "txtBackupTarget"
        Me.txtBackupTarget.Size = New System.Drawing.Size(526, 19)
        Me.txtBackupTarget.TabIndex = 5
        '
        'lblEnabled
        '
        Me.lblEnabled.AutoSize = True
        Me.lblEnabled.Location = New System.Drawing.Point(12, 9)
        Me.lblEnabled.Name = "lblEnabled"
        Me.lblEnabled.Size = New System.Drawing.Size(39, 12)
        Me.lblEnabled.TabIndex = 11
        Me.lblEnabled.Text = "Enable"
        '
        'chkBackupEnabled
        '
        Me.chkBackupEnabled.AutoSize = True
        Me.chkBackupEnabled.Location = New System.Drawing.Point(107, 8)
        Me.chkBackupEnabled.Name = "chkBackupEnabled"
        Me.chkBackupEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkBackupEnabled.TabIndex = 1
        Me.chkBackupEnabled.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(200, 172)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(62, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add↓"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(268, 172)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(62, 23)
        Me.btnCopy.TabIndex = 8
        Me.btnCopy.Text = "Copy↑"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(336, 172)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(62, 23)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(14, 418)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(62, 23)
        Me.btnApply.TabIndex = 10
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(82, 418)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(62, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(397, 320)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(224, 12)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "You can add file/folder with drag and drop."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowsTargetDir
        '
        Me.btnBrowsTargetDir.Location = New System.Drawing.Point(12, 168)
        Me.btnBrowsTargetDir.Name = "btnBrowsTargetDir"
        Me.btnBrowsTargetDir.Size = New System.Drawing.Size(77, 23)
        Me.btnBrowsTargetDir.TabIndex = 19
        Me.btnBrowsTargetDir.Text = "Select Dir"
        Me.btnBrowsTargetDir.UseVisualStyleBackColor = True
        '
        'fbdTarget
        '
        Me.fbdTarget.ShowNewFolderButton = False
        '
        'ofdTarget
        '
        Me.ofdTarget.Title = "対象ファイル指定"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(266, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 12)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Backup Keep Days"
        '
        'txtKeepDays
        '
        Me.txtKeepDays.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKeepDays.Location = New System.Drawing.Point(374, 27)
        Me.txtKeepDays.Name = "txtKeepDays"
        Me.txtKeepDays.Size = New System.Drawing.Size(76, 19)
        Me.txtKeepDays.TabIndex = 3
        '
        'gbCompressOption
        '
        Me.gbCompressOption.Controls.Add(Me.chkCompressEnabled)
        Me.gbCompressOption.Location = New System.Drawing.Point(12, 332)
        Me.gbCompressOption.Name = "gbCompressOption"
        Me.gbCompressOption.Size = New System.Drawing.Size(610, 80)
        Me.gbCompressOption.TabIndex = 22
        Me.gbCompressOption.TabStop = False
        Me.gbCompressOption.Text = "Compress Option"
        '
        'chkCompressEnabled
        '
        Me.chkCompressEnabled.AutoSize = True
        Me.chkCompressEnabled.Enabled = False
        Me.chkCompressEnabled.Location = New System.Drawing.Point(6, 18)
        Me.chkCompressEnabled.Name = "chkCompressEnabled"
        Me.chkCompressEnabled.Size = New System.Drawing.Size(119, 16)
        Me.chkCompressEnabled.TabIndex = 23
        Me.chkCompressEnabled.Text = "Compress Enabled"
        Me.chkCompressEnabled.UseVisualStyleBackColor = True
        '
        'chkBackupBeforeRun
        '
        Me.chkBackupBeforeRun.AutoSize = True
        Me.chkBackupBeforeRun.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBackupBeforeRun.Location = New System.Drawing.Point(12, 52)
        Me.chkBackupBeforeRun.Name = "chkBackupBeforeRun"
        Me.chkBackupBeforeRun.Size = New System.Drawing.Size(159, 16)
        Me.chkBackupBeforeRun.TabIndex = 23
        Me.chkBackupBeforeRun.Text = "Backup before Server Run"
        Me.chkBackupBeforeRun.UseVisualStyleBackColor = True
        '
        'frmBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 453)
        Me.Controls.Add(Me.chkBackupBeforeRun)
        Me.Controls.Add(Me.gbCompressOption)
        Me.Controls.Add(Me.txtKeepDays)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnBrowsTargetDir)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.chkBackupEnabled)
        Me.Controls.Add(Me.lblEnabled)
        Me.Controls.Add(Me.txtBackupTarget)
        Me.Controls.Add(Me.txtBackupOutput)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.btnBrowsOutput)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnBrowsTargetFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.clbBackupTarget)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmBackup"
        Me.Text = "Backup Configuration"
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCompressOption.ResumeLayout(False)
        Me.gbCompressOption.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents epInput As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnBrowsTargetFile As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents clbBackupTarget As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBrowsOutput As System.Windows.Forms.Button
    Friend WithEvents txtBackupTarget As System.Windows.Forms.TextBox
    Friend WithEvents txtBackupOutput As System.Windows.Forms.TextBox
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents chkBackupEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblEnabled As System.Windows.Forms.Label
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnBrowsTargetDir As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents fbdTarget As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ofdTarget As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtKeepDays As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents gbCompressOption As System.Windows.Forms.GroupBox
    Friend WithEvents chkCompressEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkBackupBeforeRun As System.Windows.Forms.CheckBox
End Class
