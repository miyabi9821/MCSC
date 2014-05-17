<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNGWords
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
        Me.dgvNGWords = New System.Windows.Forms.DataGridView
        Me.colEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colNGWords = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colBadCountUp = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkNGWordsEnabled = New System.Windows.Forms.CheckBox
        Me.lblEnabled = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnClearAll = New System.Windows.Forms.Button
        Me.btnDeleteNotChecked = New System.Windows.Forms.Button
        CType(Me.dgvNGWords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvNGWords
        '
        Me.dgvNGWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNGWords.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEnabled, Me.colNGWords, Me.colBadCountUp})
        Me.dgvNGWords.Location = New System.Drawing.Point(12, 142)
        Me.dgvNGWords.Name = "dgvNGWords"
        Me.dgvNGWords.RowTemplate.Height = 21
        Me.dgvNGWords.Size = New System.Drawing.Size(600, 288)
        Me.dgvNGWords.TabIndex = 0
        '
        'colEnabled
        '
        Me.colEnabled.HeaderText = "Enabled"
        Me.colEnabled.Name = "colEnabled"
        Me.colEnabled.Width = 50
        '
        'colNGWords
        '
        Me.colNGWords.HeaderText = "NGWords"
        Me.colNGWords.Name = "colNGWords"
        Me.colNGWords.Width = 420
        '
        'colBadCountUp
        '
        Me.colBadCountUp.HeaderText = "BadCountUp"
        Me.colBadCountUp.Name = "colBadCountUp"
        Me.colBadCountUp.Width = 80
        '
        'chkNGWordsEnabled
        '
        Me.chkNGWordsEnabled.AutoSize = True
        Me.chkNGWordsEnabled.Location = New System.Drawing.Point(105, 8)
        Me.chkNGWordsEnabled.Name = "chkNGWordsEnabled"
        Me.chkNGWordsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkNGWordsEnabled.TabIndex = 16
        Me.chkNGWordsEnabled.UseVisualStyleBackColor = True
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
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(550, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(62, 23)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(482, 4)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(62, 23)
        Me.btnApply.TabIndex = 18
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(12, 113)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(62, 23)
        Me.btnDelete.TabIndex = 20
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClearAll
        '
        Me.btnClearAll.ForeColor = System.Drawing.Color.Red
        Me.btnClearAll.Location = New System.Drawing.Point(550, 113)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(62, 23)
        Me.btnClearAll.TabIndex = 21
        Me.btnClearAll.Text = "Clear All"
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'btnDeleteNotChecked
        '
        Me.btnDeleteNotChecked.Location = New System.Drawing.Point(80, 113)
        Me.btnDeleteNotChecked.Name = "btnDeleteNotChecked"
        Me.btnDeleteNotChecked.Size = New System.Drawing.Size(146, 23)
        Me.btnDeleteNotChecked.TabIndex = 22
        Me.btnDeleteNotChecked.Text = "Delete(All Not Checked)"
        Me.btnDeleteNotChecked.UseVisualStyleBackColor = True
        '
        'frmNGWords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.btnDeleteNotChecked)
        Me.Controls.Add(Me.btnClearAll)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.chkNGWordsEnabled)
        Me.Controls.Add(Me.lblEnabled)
        Me.Controls.Add(Me.dgvNGWords)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmNGWords"
        Me.Text = "NGWords"
        CType(Me.dgvNGWords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvNGWords As System.Windows.Forms.DataGridView
    Friend WithEvents chkNGWordsEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblEnabled As System.Windows.Forms.Label
    Friend WithEvents colEnabled As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colNGWords As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBadCountUp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClearAll As System.Windows.Forms.Button
    Friend WithEvents btnDeleteNotChecked As System.Windows.Forms.Button
End Class
