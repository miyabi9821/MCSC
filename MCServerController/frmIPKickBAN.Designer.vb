<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIPKickBAN
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblSlash = New System.Windows.Forms.Label
        Me.txtIPv4_4 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDot2 = New System.Windows.Forms.Label
        Me.txtIPv4_3 = New System.Windows.Forms.TextBox
        Me.txtIPv4_2 = New System.Windows.Forms.TextBox
        Me.lblDot1 = New System.Windows.Forms.Label
        Me.txtIPv4_1 = New System.Windows.Forms.TextBox
        Me.cmbMask = New System.Windows.Forms.ComboBox
        Me.dgvIPAddress = New System.Windows.Forms.DataGridView
        Me.chEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.chIPAddress = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chMask = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chIDBAN = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.chkIDBAN = New System.Windows.Forms.CheckBox
        Me.txtCopyArea = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnAddFromList = New System.Windows.Forms.Button
        CType(Me.dgvIPAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(176, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 12)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Mask"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 12)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "IPv4"
        '
        'lblSlash
        '
        Me.lblSlash.AutoSize = True
        Me.lblSlash.Location = New System.Drawing.Point(164, 31)
        Me.lblSlash.Name = "lblSlash"
        Me.lblSlash.Size = New System.Drawing.Size(11, 12)
        Me.lblSlash.TabIndex = 39
        Me.lblSlash.Text = "/"
        '
        'txtIPv4_4
        '
        Me.txtIPv4_4.Location = New System.Drawing.Point(133, 24)
        Me.txtIPv4_4.Name = "txtIPv4_4"
        Me.txtIPv4_4.Size = New System.Drawing.Size(28, 19)
        Me.txtIPv4_4.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(123, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(7, 12)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "."
        '
        'lblDot2
        '
        Me.lblDot2.AutoSize = True
        Me.lblDot2.Location = New System.Drawing.Point(82, 31)
        Me.lblDot2.Name = "lblDot2"
        Me.lblDot2.Size = New System.Drawing.Size(7, 12)
        Me.lblDot2.TabIndex = 37
        Me.lblDot2.Text = "."
        '
        'txtIPv4_3
        '
        Me.txtIPv4_3.Location = New System.Drawing.Point(92, 24)
        Me.txtIPv4_3.Name = "txtIPv4_3"
        Me.txtIPv4_3.Size = New System.Drawing.Size(28, 19)
        Me.txtIPv4_3.TabIndex = 3
        '
        'txtIPv4_2
        '
        Me.txtIPv4_2.Location = New System.Drawing.Point(52, 24)
        Me.txtIPv4_2.Name = "txtIPv4_2"
        Me.txtIPv4_2.Size = New System.Drawing.Size(28, 19)
        Me.txtIPv4_2.TabIndex = 2
        '
        'lblDot1
        '
        Me.lblDot1.AutoSize = True
        Me.lblDot1.Location = New System.Drawing.Point(42, 31)
        Me.lblDot1.Name = "lblDot1"
        Me.lblDot1.Size = New System.Drawing.Size(7, 12)
        Me.lblDot1.TabIndex = 35
        Me.lblDot1.Text = "."
        '
        'txtIPv4_1
        '
        Me.txtIPv4_1.Location = New System.Drawing.Point(12, 24)
        Me.txtIPv4_1.Name = "txtIPv4_1"
        Me.txtIPv4_1.Size = New System.Drawing.Size(28, 19)
        Me.txtIPv4_1.TabIndex = 1
        '
        'cmbMask
        '
        Me.cmbMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMask.FormattingEnabled = True
        Me.cmbMask.ItemHeight = 12
        Me.cmbMask.Location = New System.Drawing.Point(178, 23)
        Me.cmbMask.Name = "cmbMask"
        Me.cmbMask.Size = New System.Drawing.Size(128, 20)
        Me.cmbMask.TabIndex = 5
        '
        'dgvIPAddress
        '
        Me.dgvIPAddress.AllowUserToAddRows = False
        Me.dgvIPAddress.AllowUserToDeleteRows = False
        Me.dgvIPAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvIPAddress.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chEnabled, Me.chIPAddress, Me.chMask, Me.chIDBAN})
        Me.dgvIPAddress.Location = New System.Drawing.Point(12, 96)
        Me.dgvIPAddress.Name = "dgvIPAddress"
        Me.dgvIPAddress.RowTemplate.Height = 21
        Me.dgvIPAddress.Size = New System.Drawing.Size(352, 345)
        Me.dgvIPAddress.TabIndex = 7
        '
        'chEnabled
        '
        Me.chEnabled.Frozen = True
        Me.chEnabled.HeaderText = "Enabled"
        Me.chEnabled.Name = "chEnabled"
        Me.chEnabled.Width = 50
        '
        'chIPAddress
        '
        Me.chIPAddress.Frozen = True
        Me.chIPAddress.HeaderText = "IP Address"
        Me.chIPAddress.Name = "chIPAddress"
        Me.chIPAddress.Width = 95
        '
        'chMask
        '
        Me.chMask.HeaderText = "Mask"
        Me.chMask.Name = "chMask"
        Me.chMask.Width = 95
        '
        'chIDBAN
        '
        Me.chIDBAN.HeaderText = "ID BAN"
        Me.chIDBAN.Name = "chIDBAN"
        Me.chIDBAN.Width = 50
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(14, 55)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(46, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(66, 55)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(46, 23)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Del"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.ForeColor = System.Drawing.Color.Red
        Me.btnClear.Location = New System.Drawing.Point(289, 55)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 42
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'chkIDBAN
        '
        Me.chkIDBAN.AutoSize = True
        Me.chkIDBAN.Location = New System.Drawing.Point(312, 25)
        Me.chkIDBAN.Name = "chkIDBAN"
        Me.chkIDBAN.Size = New System.Drawing.Size(63, 16)
        Me.chkIDBAN.TabIndex = 44
        Me.chkIDBAN.Text = "ID BAN"
        Me.chkIDBAN.UseVisualStyleBackColor = True
        '
        'txtCopyArea
        '
        Me.txtCopyArea.Location = New System.Drawing.Point(380, 31)
        Me.txtCopyArea.MaxLength = 999999
        Me.txtCopyArea.Multiline = True
        Me.txtCopyArea.Name = "txtCopyArea"
        Me.txtCopyArea.Size = New System.Drawing.Size(242, 410)
        Me.txtCopyArea.TabIndex = 45
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(378, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 12)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "IP List Paste Area"
        '
        'btnAddFromList
        '
        Me.btnAddFromList.Location = New System.Drawing.Point(151, 55)
        Me.btnAddFromList.Name = "btnAddFromList"
        Me.btnAddFromList.Size = New System.Drawing.Size(86, 23)
        Me.btnAddFromList.TabIndex = 47
        Me.btnAddFromList.Text = "Add from List"
        Me.btnAddFromList.UseVisualStyleBackColor = True
        '
        'frmIPKickBAN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 453)
        Me.Controls.Add(Me.btnAddFromList)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCopyArea)
        Me.Controls.Add(Me.chkIDBAN)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.dgvIPAddress)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSlash)
        Me.Controls.Add(Me.txtIPv4_4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDot2)
        Me.Controls.Add(Me.txtIPv4_3)
        Me.Controls.Add(Me.txtIPv4_2)
        Me.Controls.Add(Me.lblDot1)
        Me.Controls.Add(Me.txtIPv4_1)
        Me.Controls.Add(Me.cmbMask)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmIPKickBAN"
        Me.Text = "IP Kick/BAN Configuration"
        CType(Me.dgvIPAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSlash As System.Windows.Forms.Label
    Friend WithEvents txtIPv4_4 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDot2 As System.Windows.Forms.Label
    Friend WithEvents txtIPv4_3 As System.Windows.Forms.TextBox
    Friend WithEvents txtIPv4_2 As System.Windows.Forms.TextBox
    Friend WithEvents lblDot1 As System.Windows.Forms.Label
    Friend WithEvents txtIPv4_1 As System.Windows.Forms.TextBox
    Friend WithEvents cmbMask As System.Windows.Forms.ComboBox
    Friend WithEvents dgvIPAddress As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents chkIDBAN As System.Windows.Forms.CheckBox
    Friend WithEvents chEnabled As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chIPAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chMask As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chIDBAN As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtCopyArea As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnAddFromList As System.Windows.Forms.Button
End Class
