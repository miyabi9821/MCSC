<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlayerInfo
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
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.lblIP = New System.Windows.Forms.Label()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtLastLogin = New System.Windows.Forms.TextBox()
        Me.lblLastLogin = New System.Windows.Forms.Label()
        Me.txtLastLogout = New System.Windows.Forms.TextBox()
        Me.lblLastLogout = New System.Windows.Forms.Label()
        Me.txtBadCount = New System.Windows.Forms.TextBox()
        Me.lblBadCount = New System.Windows.Forms.Label()
        Me.btnGetHost = New System.Windows.Forms.Button()
        Me.btnBadCountApply = New System.Windows.Forms.Button()
        Me.txtSelectedIndex = New System.Windows.Forms.TextBox()
        Me.lblSelectedIndex = New System.Windows.Forms.Label()
        Me.txtSpawnPoint = New System.Windows.Forms.TextBox()
        Me.lblSpawnPoint = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(12, 9)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(68, 12)
        Me.lblID.TabIndex = 0
        Me.lblID.Text = "Minecraft ID"
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(86, 6)
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(134, 19)
        Me.txtID.TabIndex = 1
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(86, 31)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.ReadOnly = True
        Me.txtIP.Size = New System.Drawing.Size(134, 19)
        Me.txtIP.TabIndex = 3
        '
        'lblIP
        '
        Me.lblIP.AutoSize = True
        Me.lblIP.Location = New System.Drawing.Point(12, 34)
        Me.lblIP.Name = "lblIP"
        Me.lblIP.Size = New System.Drawing.Size(61, 12)
        Me.lblIP.TabIndex = 2
        Me.lblIP.Text = "IP Address"
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(86, 56)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.ReadOnly = True
        Me.txtHost.Size = New System.Drawing.Size(271, 19)
        Me.txtHost.TabIndex = 5
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(12, 59)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(58, 12)
        Me.lblHost.TabIndex = 4
        Me.lblHost.Text = "HostName"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(86, 81)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(134, 19)
        Me.txtStatus.TabIndex = 7
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 84)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(38, 12)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "Status"
        '
        'txtLastLogin
        '
        Me.txtLastLogin.Location = New System.Drawing.Point(86, 106)
        Me.txtLastLogin.Name = "txtLastLogin"
        Me.txtLastLogin.ReadOnly = True
        Me.txtLastLogin.Size = New System.Drawing.Size(134, 19)
        Me.txtLastLogin.TabIndex = 9
        '
        'lblLastLogin
        '
        Me.lblLastLogin.AutoSize = True
        Me.lblLastLogin.Location = New System.Drawing.Point(12, 109)
        Me.lblLastLogin.Name = "lblLastLogin"
        Me.lblLastLogin.Size = New System.Drawing.Size(58, 12)
        Me.lblLastLogin.TabIndex = 8
        Me.lblLastLogin.Text = "Last Login"
        '
        'txtLastLogout
        '
        Me.txtLastLogout.Location = New System.Drawing.Point(86, 131)
        Me.txtLastLogout.Name = "txtLastLogout"
        Me.txtLastLogout.ReadOnly = True
        Me.txtLastLogout.Size = New System.Drawing.Size(134, 19)
        Me.txtLastLogout.TabIndex = 11
        '
        'lblLastLogout
        '
        Me.lblLastLogout.AutoSize = True
        Me.lblLastLogout.Location = New System.Drawing.Point(12, 134)
        Me.lblLastLogout.Name = "lblLastLogout"
        Me.lblLastLogout.Size = New System.Drawing.Size(65, 12)
        Me.lblLastLogout.TabIndex = 10
        Me.lblLastLogout.Text = "Last Logout"
        '
        'txtBadCount
        '
        Me.txtBadCount.Location = New System.Drawing.Point(86, 181)
        Me.txtBadCount.Name = "txtBadCount"
        Me.txtBadCount.Size = New System.Drawing.Size(53, 19)
        Me.txtBadCount.TabIndex = 13
        '
        'lblBadCount
        '
        Me.lblBadCount.AutoSize = True
        Me.lblBadCount.Location = New System.Drawing.Point(12, 184)
        Me.lblBadCount.Name = "lblBadCount"
        Me.lblBadCount.Size = New System.Drawing.Size(59, 12)
        Me.lblBadCount.TabIndex = 12
        Me.lblBadCount.Text = "Bad Count"
        '
        'btnGetHost
        '
        Me.btnGetHost.Location = New System.Drawing.Point(363, 54)
        Me.btnGetHost.Name = "btnGetHost"
        Me.btnGetHost.Size = New System.Drawing.Size(62, 23)
        Me.btnGetHost.TabIndex = 14
        Me.btnGetHost.Text = "Get Host"
        Me.btnGetHost.UseVisualStyleBackColor = True
        '
        'btnBadCountApply
        '
        Me.btnBadCountApply.Location = New System.Drawing.Point(145, 179)
        Me.btnBadCountApply.Name = "btnBadCountApply"
        Me.btnBadCountApply.Size = New System.Drawing.Size(62, 23)
        Me.btnBadCountApply.TabIndex = 15
        Me.btnBadCountApply.Text = "Apply"
        Me.btnBadCountApply.UseVisualStyleBackColor = True
        '
        'txtSelectedIndex
        '
        Me.txtSelectedIndex.Location = New System.Drawing.Point(384, 181)
        Me.txtSelectedIndex.Name = "txtSelectedIndex"
        Me.txtSelectedIndex.Size = New System.Drawing.Size(53, 19)
        Me.txtSelectedIndex.TabIndex = 17
        Me.txtSelectedIndex.Visible = False
        '
        'lblSelectedIndex
        '
        Me.lblSelectedIndex.AutoSize = True
        Me.lblSelectedIndex.Location = New System.Drawing.Point(302, 184)
        Me.lblSelectedIndex.Name = "lblSelectedIndex"
        Me.lblSelectedIndex.Size = New System.Drawing.Size(76, 12)
        Me.lblSelectedIndex.TabIndex = 16
        Me.lblSelectedIndex.Text = "SelectedIndex"
        Me.lblSelectedIndex.Visible = False
        '
        'txtSpawnPoint
        '
        Me.txtSpawnPoint.Location = New System.Drawing.Point(86, 156)
        Me.txtSpawnPoint.Name = "txtSpawnPoint"
        Me.txtSpawnPoint.ReadOnly = True
        Me.txtSpawnPoint.Size = New System.Drawing.Size(134, 19)
        Me.txtSpawnPoint.TabIndex = 19
        '
        'lblSpawnPoint
        '
        Me.lblSpawnPoint.AutoSize = True
        Me.lblSpawnPoint.Location = New System.Drawing.Point(12, 159)
        Me.lblSpawnPoint.Name = "lblSpawnPoint"
        Me.lblSpawnPoint.Size = New System.Drawing.Size(68, 12)
        Me.lblSpawnPoint.TabIndex = 18
        Me.lblSpawnPoint.Text = "Spawn Point"
        '
        'frmPlayerInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 210)
        Me.Controls.Add(Me.txtSpawnPoint)
        Me.Controls.Add(Me.lblSpawnPoint)
        Me.Controls.Add(Me.txtSelectedIndex)
        Me.Controls.Add(Me.lblSelectedIndex)
        Me.Controls.Add(Me.btnBadCountApply)
        Me.Controls.Add(Me.btnGetHost)
        Me.Controls.Add(Me.txtBadCount)
        Me.Controls.Add(Me.lblBadCount)
        Me.Controls.Add(Me.txtLastLogout)
        Me.Controls.Add(Me.lblLastLogout)
        Me.Controls.Add(Me.txtLastLogin)
        Me.Controls.Add(Me.lblLastLogin)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.txtHost)
        Me.Controls.Add(Me.lblHost)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.lblIP)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.lblID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmPlayerInfo"
        Me.Text = "Player Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents lblIP As System.Windows.Forms.Label
    Friend WithEvents txtHost As System.Windows.Forms.TextBox
    Friend WithEvents lblHost As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtLastLogin As System.Windows.Forms.TextBox
    Friend WithEvents lblLastLogin As System.Windows.Forms.Label
    Friend WithEvents txtLastLogout As System.Windows.Forms.TextBox
    Friend WithEvents lblLastLogout As System.Windows.Forms.Label
    Friend WithEvents txtBadCount As System.Windows.Forms.TextBox
    Friend WithEvents lblBadCount As System.Windows.Forms.Label
    Friend WithEvents btnGetHost As System.Windows.Forms.Button
    Friend WithEvents btnBadCountApply As System.Windows.Forms.Button
    Friend WithEvents txtSelectedIndex As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectedIndex As System.Windows.Forms.Label
    Friend WithEvents txtSpawnPoint As System.Windows.Forms.TextBox
    Friend WithEvents lblSpawnPoint As System.Windows.Forms.Label
End Class
