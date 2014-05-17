<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInformation
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
        Me.lblAppName = New System.Windows.Forms.Label
        Me.lblAppVersion = New System.Windows.Forms.Label
        Me.lblDevel = New System.Windows.Forms.Label
        Me.lnklblDevel = New System.Windows.Forms.LinkLabel
        Me.lblWebSite = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lnklblWebSite = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblAppName
        '
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Location = New System.Drawing.Point(12, 9)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(188, 12)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "Minecraft Server Controller (MCSC)"
        '
        'lblAppVersion
        '
        Me.lblAppVersion.AutoSize = True
        Me.lblAppVersion.Location = New System.Drawing.Point(206, 9)
        Me.lblAppVersion.Name = "lblAppVersion"
        Me.lblAppVersion.Size = New System.Drawing.Size(44, 12)
        Me.lblAppVersion.TabIndex = 1
        Me.lblAppVersion.Text = "Version"
        '
        'lblDevel
        '
        Me.lblDevel.AutoSize = True
        Me.lblDevel.Location = New System.Drawing.Point(12, 21)
        Me.lblDevel.Name = "lblDevel"
        Me.lblDevel.Size = New System.Drawing.Size(78, 12)
        Me.lblDevel.TabIndex = 2
        Me.lblDevel.Text = "Developed by "
        '
        'lnklblDevel
        '
        Me.lnklblDevel.AutoSize = True
        Me.lnklblDevel.Location = New System.Drawing.Point(96, 21)
        Me.lnklblDevel.Name = "lnklblDevel"
        Me.lnklblDevel.Size = New System.Drawing.Size(120, 12)
        Me.lnklblDevel.TabIndex = 3
        Me.lnklblDevel.TabStop = True
        Me.lnklblDevel.Text = "雪乃雅(Yukino Miyabi)"
        '
        'lblWebSite
        '
        Me.lblWebSite.AutoSize = True
        Me.lblWebSite.Location = New System.Drawing.Point(12, 45)
        Me.lblWebSite.Name = "lblWebSite"
        Me.lblWebSite.Size = New System.Drawing.Size(48, 12)
        Me.lblWebSite.TabIndex = 5
        Me.lblWebSite.Text = "WebSite:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(219, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Special Thanks: alice氏、suei氏、syamn氏"
        '
        'lnklblWebSite
        '
        Me.lnklblWebSite.AutoSize = True
        Me.lnklblWebSite.Location = New System.Drawing.Point(66, 45)
        Me.lnklblWebSite.Name = "lnklblWebSite"
        Me.lnklblWebSite.Size = New System.Drawing.Size(161, 12)
        Me.lnklblWebSite.TabIndex = 7
        Me.lnklblWebSite.TabStop = True
        Me.lnklblWebSite.Text = "http://minecraftjp.info/MCSC/"
        '
        'frmInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 100)
        Me.Controls.Add(Me.lnklblWebSite)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblWebSite)
        Me.Controls.Add(Me.lnklblDevel)
        Me.Controls.Add(Me.lblDevel)
        Me.Controls.Add(Me.lblAppVersion)
        Me.Controls.Add(Me.lblAppName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmInformation"
        Me.Text = "Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblAppName As System.Windows.Forms.Label
    Friend WithEvents lblAppVersion As System.Windows.Forms.Label
    Friend WithEvents lblDevel As System.Windows.Forms.Label
    Friend WithEvents lnklblDevel As System.Windows.Forms.LinkLabel
    Friend WithEvents lblWebSite As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lnklblWebSite As System.Windows.Forms.LinkLabel
End Class
