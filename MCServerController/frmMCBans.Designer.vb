<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMCBans
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAPIKey = New System.Windows.Forms.TextBox()
        Me.gbUsage = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.llGlobalBanRules = New System.Windows.Forms.LinkLabel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.llMCBansWeb = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtGlobalIP = New System.Windows.Forms.TextBox()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMinRep = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.epMCBans = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label18 = New System.Windows.Forms.Label()
        Me.chkFailSafe = New System.Windows.Forms.CheckBox()
        Me.gbUsage.SuspendLayout()
        CType(Me.epMCBans, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 293)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "API Key"
        '
        'txtAPIKey
        '
        Me.txtAPIKey.Location = New System.Drawing.Point(87, 290)
        Me.txtAPIKey.Name = "txtAPIKey"
        Me.txtAPIKey.Size = New System.Drawing.Size(286, 19)
        Me.txtAPIKey.TabIndex = 1
        '
        'gbUsage
        '
        Me.gbUsage.Controls.Add(Me.Label17)
        Me.gbUsage.Controls.Add(Me.llGlobalBanRules)
        Me.gbUsage.Controls.Add(Me.Label16)
        Me.gbUsage.Controls.Add(Me.llMCBansWeb)
        Me.gbUsage.Controls.Add(Me.Label7)
        Me.gbUsage.Controls.Add(Me.txtGlobalIP)
        Me.gbUsage.Controls.Add(Me.chkEnabled)
        Me.gbUsage.Controls.Add(Me.Label13)
        Me.gbUsage.Controls.Add(Me.Label12)
        Me.gbUsage.Controls.Add(Me.Label11)
        Me.gbUsage.Controls.Add(Me.Label10)
        Me.gbUsage.Controls.Add(Me.Label9)
        Me.gbUsage.Controls.Add(Me.Label8)
        Me.gbUsage.Controls.Add(Me.Label6)
        Me.gbUsage.Controls.Add(Me.Label5)
        Me.gbUsage.Controls.Add(Me.Label4)
        Me.gbUsage.Controls.Add(Me.Label3)
        Me.gbUsage.Controls.Add(Me.Label2)
        Me.gbUsage.Location = New System.Drawing.Point(11, 12)
        Me.gbUsage.Name = "gbUsage"
        Me.gbUsage.Size = New System.Drawing.Size(571, 272)
        Me.gbUsage.TabIndex = 0
        Me.gbUsage.TabStop = False
        Me.gbUsage.Text = "利用方法について"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(178, 220)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(253, 12)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "に4.Hamachi Server Policyとして記載されています。"
        '
        'llGlobalBanRules
        '
        Me.llGlobalBanRules.AutoSize = True
        Me.llGlobalBanRules.Location = New System.Drawing.Point(31, 220)
        Me.llGlobalBanRules.Name = "llGlobalBanRules"
        Me.llGlobalBanRules.Size = New System.Drawing.Size(141, 12)
        Me.llGlobalBanRules.TabIndex = 16
        Me.llGlobalBanRules.TabStop = True
        Me.llGlobalBanRules.Text = "MCBans Global Ban Rules"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(10, 206)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(235, 12)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "・hamach等のVPN環境では利用しないでください"
        '
        'llMCBansWeb
        '
        Me.llMCBansWeb.AutoSize = True
        Me.llMCBansWeb.Location = New System.Drawing.Point(347, 47)
        Me.llMCBansWeb.Name = "llMCBansWeb"
        Me.llMCBansWeb.Size = New System.Drawing.Size(92, 12)
        Me.llMCBansWeb.TabIndex = 1
        Me.llMCBansWeb.TabStop = True
        Me.llMCBansWeb.Text = "MCBans Website"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(48, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 12)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "グローバルIPアドレス"
        '
        'txtGlobalIP
        '
        Me.txtGlobalIP.Location = New System.Drawing.Point(153, 88)
        Me.txtGlobalIP.Name = "txtGlobalIP"
        Me.txtGlobalIP.ReadOnly = True
        Me.txtGlobalIP.Size = New System.Drawing.Size(100, 19)
        Me.txtGlobalIP.TabIndex = 13
        Me.txtGlobalIP.TabStop = False
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Enabled = False
        Me.chkEnabled.Location = New System.Drawing.Point(12, 247)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(241, 16)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "上記に同意し、MCBans連携機能を利用する"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(31, 187)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(304, 12)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "システム全体に影響のあるグローバルBANは行わないでください。"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(31, 173)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(309, 12)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "MCSCはMCBansのデータを二次利用させて頂いているだけです。"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(31, 141)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(362, 12)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "そのため、オフラインでは連携機能を有効にしていても、連携は行われません。"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(31, 127)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(420, 12)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "オフラインだとユーザーIDの詐称により、不正なデータが登録される可能性があるためです。"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(10, 159)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(335, 12)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "・MCBansのWeb DashboardからグローバルBANを行わないでください。"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(10, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(423, 12)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "・連携機能を利用するためにはサーバがオンライン（online-mode=true）の必要があります。"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(441, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "３．My Serversに２で登録したサーバがあるので、利用するグローバルIPアドレスを登録します。"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(339, 12)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "２．Register Serverから自分のサーバを登録し、APIKeyを取得します。"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(310, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "１．MCBansにユーザー登録を行い、アカウントを有効化させます。"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(10, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(284, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "・MCBansにユーザ登録し、サーバ情報の登録が必要です。"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(382, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "MCBans連携機能を利用するためには、いくつかの条件を満たす必要があります。"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(78, 368)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(11, 368)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(61, 23)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 318)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(29, 12)
        Me.Label14.TabIndex = 102
        Me.Label14.Text = "閾値"
        '
        'txtMinRep
        '
        Me.txtMinRep.Location = New System.Drawing.Point(87, 315)
        Me.txtMinRep.MaxLength = 4
        Me.txtMinRep.Name = "txtMinRep"
        Me.txtMinRep.Size = New System.Drawing.Size(47, 19)
        Me.txtMinRep.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(148, 318)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(351, 12)
        Me.Label15.TabIndex = 104
        Me.Label15.Text = "Reputationの値がこれ以下だと接続を拒否します（推奨値：3　-1で無効）"
        '
        'epMCBans
        '
        Me.epMCBans.ContainerControl = Me
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(13, 343)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 12)
        Me.Label18.TabIndex = 106
        Me.Label18.Text = "フェイルセーフ"
        '
        'chkFailSafe
        '
        Me.chkFailSafe.AutoSize = True
        Me.chkFailSafe.Enabled = False
        Me.chkFailSafe.Location = New System.Drawing.Point(87, 342)
        Me.chkFailSafe.Name = "chkFailSafe"
        Me.chkFailSafe.Size = New System.Drawing.Size(366, 16)
        Me.chkFailSafe.TabIndex = 18
        Me.chkFailSafe.Text = "有効の場合、MCBansサーバと連携出来ない場合は接続を拒否します。"
        Me.chkFailSafe.UseVisualStyleBackColor = True
        '
        'frmMCBans
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 405)
        Me.Controls.Add(Me.chkFailSafe)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtMinRep)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.gbUsage)
        Me.Controls.Add(Me.txtAPIKey)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMCBans"
        Me.Text = "MCBans Configuration"
        Me.gbUsage.ResumeLayout(False)
        Me.gbUsage.PerformLayout()
        CType(Me.epMCBans, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAPIKey As System.Windows.Forms.TextBox
    Friend WithEvents gbUsage As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtGlobalIP As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents llMCBansWeb As System.Windows.Forms.LinkLabel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMinRep As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents epMCBans As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents llGlobalBanRules As System.Windows.Forms.LinkLabel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents chkFailSafe As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
End Class
