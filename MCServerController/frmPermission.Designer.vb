<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPermission
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
        Me.chkPermissionEnabled = New System.Windows.Forms.CheckBox()
        Me.lblEnabled = New System.Windows.Forms.Label()
        Me.txtPrefixChar = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbUsefulCommands = New System.Windows.Forms.GroupBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.rbWeatherSelected = New System.Windows.Forms.RadioButton()
        Me.rbWeatherEveryone = New System.Windows.Forms.RadioButton()
        Me.txtWeatherSelectedUsers = New System.Windows.Forms.TextBox()
        Me.chkWeather = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.rbSpawnpointSelected = New System.Windows.Forms.RadioButton()
        Me.rbSpawnpointEveryone = New System.Windows.Forms.RadioButton()
        Me.txtSpawnpointSelectedUsers = New System.Windows.Forms.TextBox()
        Me.chkSpawnpoint = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rbGamemodeSelected = New System.Windows.Forms.RadioButton()
        Me.rbGamemodeEveryone = New System.Windows.Forms.RadioButton()
        Me.txtGamemodeSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbXpSelected = New System.Windows.Forms.RadioButton()
        Me.rbXpEveryone = New System.Windows.Forms.RadioButton()
        Me.txtXpSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbTimeSelected = New System.Windows.Forms.RadioButton()
        Me.rbTimeEveryone = New System.Windows.Forms.RadioButton()
        Me.txtTimeSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbGiveSelected = New System.Windows.Forms.RadioButton()
        Me.rbGiveEveryone = New System.Windows.Forms.RadioButton()
        Me.txtGiveSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbTpSelected = New System.Windows.Forms.RadioButton()
        Me.rbTpEveryone = New System.Windows.Forms.RadioButton()
        Me.txtTpSelectedUsers = New System.Windows.Forms.TextBox()
        Me.chkGamemode = New System.Windows.Forms.CheckBox()
        Me.chkXp = New System.Windows.Forms.CheckBox()
        Me.chkTime = New System.Windows.Forms.CheckBox()
        Me.chkGive = New System.Windows.Forms.CheckBox()
        Me.chkTp = New System.Windows.Forms.CheckBox()
        Me.gbAdminCommands = New System.Windows.Forms.GroupBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.rbWhitelistSelected = New System.Windows.Forms.RadioButton()
        Me.rbWhitelistEveryone = New System.Windows.Forms.RadioButton()
        Me.txtWhitelistSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.rbPardonSelected = New System.Windows.Forms.RadioButton()
        Me.rbPardonEveryone = New System.Windows.Forms.RadioButton()
        Me.txtPardonSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rbBanSelected = New System.Windows.Forms.RadioButton()
        Me.rbBanEveryone = New System.Windows.Forms.RadioButton()
        Me.txtBanSelectedUsers = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rbKickSelected = New System.Windows.Forms.RadioButton()
        Me.rbKickEveryone = New System.Windows.Forms.RadioButton()
        Me.txtKickSelectedUsers = New System.Windows.Forms.TextBox()
        Me.chkWhitelist = New System.Windows.Forms.CheckBox()
        Me.chkPardon = New System.Windows.Forms.CheckBox()
        Me.chkKick = New System.Windows.Forms.CheckBox()
        Me.chkBan = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.epInput = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.gbAdditionalCommands = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.rbRespawnSelected = New System.Windows.Forms.RadioButton()
        Me.rbRespawnEveryone = New System.Windows.Forms.RadioButton()
        Me.txtRespawnSelectedUsers = New System.Windows.Forms.TextBox()
        Me.chkRespawn = New System.Windows.Forms.CheckBox()
        Me.gbUsefulCommands.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gbAdminCommands.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAdditionalCommands.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkPermissionEnabled
        '
        Me.chkPermissionEnabled.AutoSize = True
        Me.chkPermissionEnabled.Location = New System.Drawing.Point(107, 8)
        Me.chkPermissionEnabled.Name = "chkPermissionEnabled"
        Me.chkPermissionEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkPermissionEnabled.TabIndex = 1
        Me.chkPermissionEnabled.UseVisualStyleBackColor = True
        '
        'lblEnabled
        '
        Me.lblEnabled.AutoSize = True
        Me.lblEnabled.Location = New System.Drawing.Point(12, 9)
        Me.lblEnabled.Name = "lblEnabled"
        Me.lblEnabled.Size = New System.Drawing.Size(39, 12)
        Me.lblEnabled.TabIndex = 17
        Me.lblEnabled.Text = "Enable"
        '
        'txtPrefixChar
        '
        Me.txtPrefixChar.Location = New System.Drawing.Point(107, 28)
        Me.txtPrefixChar.MaxLength = 1
        Me.txtPrefixChar.Name = "txtPrefixChar"
        Me.txtPrefixChar.Size = New System.Drawing.Size(37, 19)
        Me.txtPrefixChar.TabIndex = 2
        Me.txtPrefixChar.Text = "@"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Prefix Character"
        '
        'gbUsefulCommands
        '
        Me.gbUsefulCommands.Controls.Add(Me.Panel11)
        Me.gbUsefulCommands.Controls.Add(Me.txtWeatherSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.chkWeather)
        Me.gbUsefulCommands.Controls.Add(Me.Label2)
        Me.gbUsefulCommands.Controls.Add(Me.Panel10)
        Me.gbUsefulCommands.Controls.Add(Me.txtSpawnpointSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.chkSpawnpoint)
        Me.gbUsefulCommands.Controls.Add(Me.Panel5)
        Me.gbUsefulCommands.Controls.Add(Me.txtGamemodeSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.Panel4)
        Me.gbUsefulCommands.Controls.Add(Me.txtXpSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.Panel3)
        Me.gbUsefulCommands.Controls.Add(Me.txtTimeSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.Panel2)
        Me.gbUsefulCommands.Controls.Add(Me.txtGiveSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.Panel1)
        Me.gbUsefulCommands.Controls.Add(Me.txtTpSelectedUsers)
        Me.gbUsefulCommands.Controls.Add(Me.chkGamemode)
        Me.gbUsefulCommands.Controls.Add(Me.chkXp)
        Me.gbUsefulCommands.Controls.Add(Me.chkTime)
        Me.gbUsefulCommands.Controls.Add(Me.chkGive)
        Me.gbUsefulCommands.Controls.Add(Me.chkTp)
        Me.gbUsefulCommands.Location = New System.Drawing.Point(14, 53)
        Me.gbUsefulCommands.Name = "gbUsefulCommands"
        Me.gbUsefulCommands.Size = New System.Drawing.Size(740, 197)
        Me.gbUsefulCommands.TabIndex = 3
        Me.gbUsefulCommands.TabStop = False
        Me.gbUsefulCommands.Text = "Useful Commands"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.rbWeatherSelected)
        Me.Panel11.Controls.Add(Me.rbWeatherEveryone)
        Me.Panel11.Location = New System.Drawing.Point(206, 168)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(154, 21)
        Me.Panel11.TabIndex = 21
        '
        'rbWeatherSelected
        '
        Me.rbWeatherSelected.AutoSize = True
        Me.rbWeatherSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbWeatherSelected.Name = "rbWeatherSelected"
        Me.rbWeatherSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbWeatherSelected.TabIndex = 25
        Me.rbWeatherSelected.TabStop = True
        Me.rbWeatherSelected.Text = "Selected"
        Me.rbWeatherSelected.UseVisualStyleBackColor = True
        '
        'rbWeatherEveryone
        '
        Me.rbWeatherEveryone.AutoSize = True
        Me.rbWeatherEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbWeatherEveryone.Name = "rbWeatherEveryone"
        Me.rbWeatherEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbWeatherEveryone.TabIndex = 24
        Me.rbWeatherEveryone.TabStop = True
        Me.rbWeatherEveryone.Text = "Everyone"
        Me.rbWeatherEveryone.UseVisualStyleBackColor = True
        '
        'txtWeatherSelectedUsers
        '
        Me.txtWeatherSelectedUsers.Location = New System.Drawing.Point(366, 170)
        Me.txtWeatherSelectedUsers.Name = "txtWeatherSelectedUsers"
        Me.txtWeatherSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtWeatherSelectedUsers.TabIndex = 22
        '
        'chkWeather
        '
        Me.chkWeather.AutoSize = True
        Me.chkWeather.Location = New System.Drawing.Point(6, 173)
        Me.chkWeather.Name = "chkWeather"
        Me.chkWeather.Size = New System.Drawing.Size(64, 16)
        Me.chkWeather.TabIndex = 20
        Me.chkWeather.Text = "weather"
        Me.chkWeather.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 12)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "for MC1.4 or later"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.rbSpawnpointSelected)
        Me.Panel10.Controls.Add(Me.rbSpawnpointEveryone)
        Me.Panel10.Location = New System.Drawing.Point(206, 146)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(154, 21)
        Me.Panel10.TabIndex = 17
        '
        'rbSpawnpointSelected
        '
        Me.rbSpawnpointSelected.AutoSize = True
        Me.rbSpawnpointSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbSpawnpointSelected.Name = "rbSpawnpointSelected"
        Me.rbSpawnpointSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbSpawnpointSelected.TabIndex = 25
        Me.rbSpawnpointSelected.TabStop = True
        Me.rbSpawnpointSelected.Text = "Selected"
        Me.rbSpawnpointSelected.UseVisualStyleBackColor = True
        '
        'rbSpawnpointEveryone
        '
        Me.rbSpawnpointEveryone.AutoSize = True
        Me.rbSpawnpointEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbSpawnpointEveryone.Name = "rbSpawnpointEveryone"
        Me.rbSpawnpointEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbSpawnpointEveryone.TabIndex = 24
        Me.rbSpawnpointEveryone.TabStop = True
        Me.rbSpawnpointEveryone.Text = "Everyone"
        Me.rbSpawnpointEveryone.UseVisualStyleBackColor = True
        '
        'txtSpawnpointSelectedUsers
        '
        Me.txtSpawnpointSelectedUsers.Location = New System.Drawing.Point(366, 148)
        Me.txtSpawnpointSelectedUsers.Name = "txtSpawnpointSelectedUsers"
        Me.txtSpawnpointSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtSpawnpointSelectedUsers.TabIndex = 18
        '
        'chkSpawnpoint
        '
        Me.chkSpawnpoint.AutoSize = True
        Me.chkSpawnpoint.Location = New System.Drawing.Point(6, 151)
        Me.chkSpawnpoint.Name = "chkSpawnpoint"
        Me.chkSpawnpoint.Size = New System.Drawing.Size(81, 16)
        Me.chkSpawnpoint.TabIndex = 16
        Me.chkSpawnpoint.Text = "spawnpoint"
        Me.chkSpawnpoint.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rbGamemodeSelected)
        Me.Panel5.Controls.Add(Me.rbGamemodeEveryone)
        Me.Panel5.Location = New System.Drawing.Point(206, 101)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(154, 21)
        Me.Panel5.TabIndex = 14
        '
        'rbGamemodeSelected
        '
        Me.rbGamemodeSelected.AutoSize = True
        Me.rbGamemodeSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbGamemodeSelected.Name = "rbGamemodeSelected"
        Me.rbGamemodeSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbGamemodeSelected.TabIndex = 25
        Me.rbGamemodeSelected.TabStop = True
        Me.rbGamemodeSelected.Text = "Selected"
        Me.rbGamemodeSelected.UseVisualStyleBackColor = True
        '
        'rbGamemodeEveryone
        '
        Me.rbGamemodeEveryone.AutoSize = True
        Me.rbGamemodeEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbGamemodeEveryone.Name = "rbGamemodeEveryone"
        Me.rbGamemodeEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbGamemodeEveryone.TabIndex = 24
        Me.rbGamemodeEveryone.TabStop = True
        Me.rbGamemodeEveryone.Text = "Everyone"
        Me.rbGamemodeEveryone.UseVisualStyleBackColor = True
        '
        'txtGamemodeSelectedUsers
        '
        Me.txtGamemodeSelectedUsers.Location = New System.Drawing.Point(366, 103)
        Me.txtGamemodeSelectedUsers.Name = "txtGamemodeSelectedUsers"
        Me.txtGamemodeSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtGamemodeSelectedUsers.TabIndex = 15
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbXpSelected)
        Me.Panel4.Controls.Add(Me.rbXpEveryone)
        Me.Panel4.Location = New System.Drawing.Point(206, 79)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(154, 21)
        Me.Panel4.TabIndex = 11
        '
        'rbXpSelected
        '
        Me.rbXpSelected.AutoSize = True
        Me.rbXpSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbXpSelected.Name = "rbXpSelected"
        Me.rbXpSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbXpSelected.TabIndex = 21
        Me.rbXpSelected.TabStop = True
        Me.rbXpSelected.Text = "Selected"
        Me.rbXpSelected.UseVisualStyleBackColor = True
        '
        'rbXpEveryone
        '
        Me.rbXpEveryone.AutoSize = True
        Me.rbXpEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbXpEveryone.Name = "rbXpEveryone"
        Me.rbXpEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbXpEveryone.TabIndex = 20
        Me.rbXpEveryone.TabStop = True
        Me.rbXpEveryone.Text = "Everyone"
        Me.rbXpEveryone.UseVisualStyleBackColor = True
        '
        'txtXpSelectedUsers
        '
        Me.txtXpSelectedUsers.Location = New System.Drawing.Point(366, 81)
        Me.txtXpSelectedUsers.Name = "txtXpSelectedUsers"
        Me.txtXpSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtXpSelectedUsers.TabIndex = 12
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbTimeSelected)
        Me.Panel3.Controls.Add(Me.rbTimeEveryone)
        Me.Panel3.Location = New System.Drawing.Point(206, 57)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(154, 21)
        Me.Panel3.TabIndex = 8
        '
        'rbTimeSelected
        '
        Me.rbTimeSelected.AutoSize = True
        Me.rbTimeSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbTimeSelected.Name = "rbTimeSelected"
        Me.rbTimeSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbTimeSelected.TabIndex = 2
        Me.rbTimeSelected.TabStop = True
        Me.rbTimeSelected.Text = "Selected"
        Me.rbTimeSelected.UseVisualStyleBackColor = True
        '
        'rbTimeEveryone
        '
        Me.rbTimeEveryone.AutoSize = True
        Me.rbTimeEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbTimeEveryone.Name = "rbTimeEveryone"
        Me.rbTimeEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbTimeEveryone.TabIndex = 1
        Me.rbTimeEveryone.TabStop = True
        Me.rbTimeEveryone.Text = "Everyone"
        Me.rbTimeEveryone.UseVisualStyleBackColor = True
        '
        'txtTimeSelectedUsers
        '
        Me.txtTimeSelectedUsers.Location = New System.Drawing.Point(366, 59)
        Me.txtTimeSelectedUsers.Name = "txtTimeSelectedUsers"
        Me.txtTimeSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtTimeSelectedUsers.TabIndex = 9
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbGiveSelected)
        Me.Panel2.Controls.Add(Me.rbGiveEveryone)
        Me.Panel2.Location = New System.Drawing.Point(206, 35)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(154, 21)
        Me.Panel2.TabIndex = 5
        '
        'rbGiveSelected
        '
        Me.rbGiveSelected.AutoSize = True
        Me.rbGiveSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbGiveSelected.Name = "rbGiveSelected"
        Me.rbGiveSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbGiveSelected.TabIndex = 15
        Me.rbGiveSelected.TabStop = True
        Me.rbGiveSelected.Text = "Selected"
        Me.rbGiveSelected.UseVisualStyleBackColor = True
        '
        'rbGiveEveryone
        '
        Me.rbGiveEveryone.AutoSize = True
        Me.rbGiveEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbGiveEveryone.Name = "rbGiveEveryone"
        Me.rbGiveEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbGiveEveryone.TabIndex = 14
        Me.rbGiveEveryone.TabStop = True
        Me.rbGiveEveryone.Text = "Everyone"
        Me.rbGiveEveryone.UseVisualStyleBackColor = True
        '
        'txtGiveSelectedUsers
        '
        Me.txtGiveSelectedUsers.Location = New System.Drawing.Point(366, 37)
        Me.txtGiveSelectedUsers.Name = "txtGiveSelectedUsers"
        Me.txtGiveSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtGiveSelectedUsers.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbTpSelected)
        Me.Panel1.Controls.Add(Me.rbTpEveryone)
        Me.Panel1.Location = New System.Drawing.Point(206, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(154, 21)
        Me.Panel1.TabIndex = 2
        '
        'rbTpSelected
        '
        Me.rbTpSelected.AutoSize = True
        Me.rbTpSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbTpSelected.Name = "rbTpSelected"
        Me.rbTpSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbTpSelected.TabIndex = 10
        Me.rbTpSelected.TabStop = True
        Me.rbTpSelected.Text = "Selected"
        Me.rbTpSelected.UseVisualStyleBackColor = True
        '
        'rbTpEveryone
        '
        Me.rbTpEveryone.AutoSize = True
        Me.rbTpEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbTpEveryone.Name = "rbTpEveryone"
        Me.rbTpEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbTpEveryone.TabIndex = 9
        Me.rbTpEveryone.TabStop = True
        Me.rbTpEveryone.Text = "Everyone"
        Me.rbTpEveryone.UseVisualStyleBackColor = True
        '
        'txtTpSelectedUsers
        '
        Me.txtTpSelectedUsers.Location = New System.Drawing.Point(366, 15)
        Me.txtTpSelectedUsers.Name = "txtTpSelectedUsers"
        Me.txtTpSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtTpSelectedUsers.TabIndex = 3
        '
        'chkGamemode
        '
        Me.chkGamemode.AutoSize = True
        Me.chkGamemode.Location = New System.Drawing.Point(6, 106)
        Me.chkGamemode.Name = "chkGamemode"
        Me.chkGamemode.Size = New System.Drawing.Size(124, 16)
        Me.chkGamemode.TabIndex = 13
        Me.chkGamemode.Text = "gamemode <0/1/2>"
        Me.chkGamemode.UseVisualStyleBackColor = True
        '
        'chkXp
        '
        Me.chkXp.AutoSize = True
        Me.chkXp.Location = New System.Drawing.Point(6, 84)
        Me.chkXp.Name = "chkXp"
        Me.chkXp.Size = New System.Drawing.Size(88, 16)
        Me.chkXp.TabIndex = 10
        Me.chkXp.Text = "xp <0-5000>"
        Me.chkXp.UseVisualStyleBackColor = True
        '
        'chkTime
        '
        Me.chkTime.AutoSize = True
        Me.chkTime.Location = New System.Drawing.Point(6, 62)
        Me.chkTime.Name = "chkTime"
        Me.chkTime.Size = New System.Drawing.Size(160, 16)
        Me.chkTime.TabIndex = 7
        Me.chkTime.Text = "time <add/set> <0-24000>"
        Me.chkTime.UseVisualStyleBackColor = True
        '
        'chkGive
        '
        Me.chkGive.AutoSize = True
        Me.chkGive.Location = New System.Drawing.Point(6, 40)
        Me.chkGive.Name = "chkGive"
        Me.chkGive.Size = New System.Drawing.Size(186, 16)
        Me.chkGive.TabIndex = 4
        Me.chkGive.Text = "give <ItemID> <num> <damage>"
        Me.chkGive.UseVisualStyleBackColor = True
        '
        'chkTp
        '
        Me.chkTp.AutoSize = True
        Me.chkTp.Location = New System.Drawing.Point(6, 18)
        Me.chkTp.Name = "chkTp"
        Me.chkTp.Size = New System.Drawing.Size(82, 16)
        Me.chkTp.TabIndex = 1
        Me.chkTp.Text = "tp <Player>"
        Me.chkTp.UseVisualStyleBackColor = True
        '
        'gbAdminCommands
        '
        Me.gbAdminCommands.Controls.Add(Me.Panel9)
        Me.gbAdminCommands.Controls.Add(Me.txtWhitelistSelectedUsers)
        Me.gbAdminCommands.Controls.Add(Me.Panel8)
        Me.gbAdminCommands.Controls.Add(Me.txtPardonSelectedUsers)
        Me.gbAdminCommands.Controls.Add(Me.Panel7)
        Me.gbAdminCommands.Controls.Add(Me.txtBanSelectedUsers)
        Me.gbAdminCommands.Controls.Add(Me.Panel6)
        Me.gbAdminCommands.Controls.Add(Me.txtKickSelectedUsers)
        Me.gbAdminCommands.Controls.Add(Me.chkWhitelist)
        Me.gbAdminCommands.Controls.Add(Me.chkPardon)
        Me.gbAdminCommands.Controls.Add(Me.chkKick)
        Me.gbAdminCommands.Controls.Add(Me.chkBan)
        Me.gbAdminCommands.Location = New System.Drawing.Point(14, 256)
        Me.gbAdminCommands.Name = "gbAdminCommands"
        Me.gbAdminCommands.Size = New System.Drawing.Size(740, 125)
        Me.gbAdminCommands.TabIndex = 4
        Me.gbAdminCommands.TabStop = False
        Me.gbAdminCommands.Text = "Admin Commands"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.rbWhitelistSelected)
        Me.Panel9.Controls.Add(Me.rbWhitelistEveryone)
        Me.Panel9.Location = New System.Drawing.Point(206, 97)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(154, 21)
        Me.Panel9.TabIndex = 11
        '
        'rbWhitelistSelected
        '
        Me.rbWhitelistSelected.AutoSize = True
        Me.rbWhitelistSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbWhitelistSelected.Name = "rbWhitelistSelected"
        Me.rbWhitelistSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbWhitelistSelected.TabIndex = 37
        Me.rbWhitelistSelected.TabStop = True
        Me.rbWhitelistSelected.Text = "Selected"
        Me.rbWhitelistSelected.UseVisualStyleBackColor = True
        '
        'rbWhitelistEveryone
        '
        Me.rbWhitelistEveryone.AutoSize = True
        Me.rbWhitelistEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbWhitelistEveryone.Name = "rbWhitelistEveryone"
        Me.rbWhitelistEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbWhitelistEveryone.TabIndex = 36
        Me.rbWhitelistEveryone.TabStop = True
        Me.rbWhitelistEveryone.Text = "Everyone"
        Me.rbWhitelistEveryone.UseVisualStyleBackColor = True
        '
        'txtWhitelistSelectedUsers
        '
        Me.txtWhitelistSelectedUsers.Location = New System.Drawing.Point(366, 99)
        Me.txtWhitelistSelectedUsers.Name = "txtWhitelistSelectedUsers"
        Me.txtWhitelistSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtWhitelistSelectedUsers.TabIndex = 12
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.rbPardonSelected)
        Me.Panel8.Controls.Add(Me.rbPardonEveryone)
        Me.Panel8.Location = New System.Drawing.Point(206, 57)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(154, 21)
        Me.Panel8.TabIndex = 8
        '
        'rbPardonSelected
        '
        Me.rbPardonSelected.AutoSize = True
        Me.rbPardonSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbPardonSelected.Name = "rbPardonSelected"
        Me.rbPardonSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbPardonSelected.TabIndex = 11
        Me.rbPardonSelected.TabStop = True
        Me.rbPardonSelected.Text = "Selected"
        Me.rbPardonSelected.UseVisualStyleBackColor = True
        '
        'rbPardonEveryone
        '
        Me.rbPardonEveryone.AutoSize = True
        Me.rbPardonEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbPardonEveryone.Name = "rbPardonEveryone"
        Me.rbPardonEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbPardonEveryone.TabIndex = 10
        Me.rbPardonEveryone.TabStop = True
        Me.rbPardonEveryone.Text = "Everyone"
        Me.rbPardonEveryone.UseVisualStyleBackColor = True
        '
        'txtPardonSelectedUsers
        '
        Me.txtPardonSelectedUsers.Location = New System.Drawing.Point(366, 59)
        Me.txtPardonSelectedUsers.Name = "txtPardonSelectedUsers"
        Me.txtPardonSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtPardonSelectedUsers.TabIndex = 9
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbBanSelected)
        Me.Panel7.Controls.Add(Me.rbBanEveryone)
        Me.Panel7.Location = New System.Drawing.Point(206, 35)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(154, 21)
        Me.Panel7.TabIndex = 5
        '
        'rbBanSelected
        '
        Me.rbBanSelected.AutoSize = True
        Me.rbBanSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbBanSelected.Name = "rbBanSelected"
        Me.rbBanSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbBanSelected.TabIndex = 33
        Me.rbBanSelected.TabStop = True
        Me.rbBanSelected.Text = "Selected"
        Me.rbBanSelected.UseVisualStyleBackColor = True
        '
        'rbBanEveryone
        '
        Me.rbBanEveryone.AutoSize = True
        Me.rbBanEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbBanEveryone.Name = "rbBanEveryone"
        Me.rbBanEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbBanEveryone.TabIndex = 32
        Me.rbBanEveryone.TabStop = True
        Me.rbBanEveryone.Text = "Everyone"
        Me.rbBanEveryone.UseVisualStyleBackColor = True
        '
        'txtBanSelectedUsers
        '
        Me.txtBanSelectedUsers.Location = New System.Drawing.Point(366, 37)
        Me.txtBanSelectedUsers.Name = "txtBanSelectedUsers"
        Me.txtBanSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtBanSelectedUsers.TabIndex = 6
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rbKickSelected)
        Me.Panel6.Controls.Add(Me.rbKickEveryone)
        Me.Panel6.Location = New System.Drawing.Point(206, 13)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(154, 21)
        Me.Panel6.TabIndex = 2
        '
        'rbKickSelected
        '
        Me.rbKickSelected.AutoSize = True
        Me.rbKickSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbKickSelected.Name = "rbKickSelected"
        Me.rbKickSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbKickSelected.TabIndex = 29
        Me.rbKickSelected.TabStop = True
        Me.rbKickSelected.Text = "Selected"
        Me.rbKickSelected.UseVisualStyleBackColor = True
        '
        'rbKickEveryone
        '
        Me.rbKickEveryone.AutoSize = True
        Me.rbKickEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbKickEveryone.Name = "rbKickEveryone"
        Me.rbKickEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbKickEveryone.TabIndex = 28
        Me.rbKickEveryone.TabStop = True
        Me.rbKickEveryone.Text = "Everyone"
        Me.rbKickEveryone.UseVisualStyleBackColor = True
        '
        'txtKickSelectedUsers
        '
        Me.txtKickSelectedUsers.Location = New System.Drawing.Point(366, 15)
        Me.txtKickSelectedUsers.Name = "txtKickSelectedUsers"
        Me.txtKickSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtKickSelectedUsers.TabIndex = 3
        '
        'chkWhitelist
        '
        Me.chkWhitelist.AutoSize = True
        Me.chkWhitelist.Location = New System.Drawing.Point(6, 84)
        Me.chkWhitelist.Name = "chkWhitelist"
        Me.chkWhitelist.Size = New System.Drawing.Size(383, 16)
        Me.chkWhitelist.TabIndex = 10
        Me.chkWhitelist.Text = "whitelist <on/off/list/add/remove/reload> <Player(add/remove only)>"
        Me.chkWhitelist.UseVisualStyleBackColor = True
        '
        'chkPardon
        '
        Me.chkPardon.AutoSize = True
        Me.chkPardon.Location = New System.Drawing.Point(6, 62)
        Me.chkPardon.Name = "chkPardon"
        Me.chkPardon.Size = New System.Drawing.Size(106, 16)
        Me.chkPardon.TabIndex = 7
        Me.chkPardon.Text = "pardon <Player>"
        Me.chkPardon.UseVisualStyleBackColor = True
        '
        'chkKick
        '
        Me.chkKick.AutoSize = True
        Me.chkKick.Location = New System.Drawing.Point(6, 18)
        Me.chkKick.Name = "chkKick"
        Me.chkKick.Size = New System.Drawing.Size(93, 16)
        Me.chkKick.TabIndex = 1
        Me.chkKick.Text = "kick <Player>"
        Me.chkKick.UseVisualStyleBackColor = True
        '
        'chkBan
        '
        Me.chkBan.AutoSize = True
        Me.chkBan.Location = New System.Drawing.Point(6, 40)
        Me.chkBan.Name = "chkBan"
        Me.chkBan.Size = New System.Drawing.Size(90, 16)
        Me.chkBan.TabIndex = 4
        Me.chkBan.Text = "ban <Player>"
        Me.chkBan.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(81, 493)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 23)
        Me.btnCancel.TabIndex = 99
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(14, 493)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(61, 23)
        Me.btnApply.TabIndex = 98
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'epInput
        '
        Me.epInput.ContainerControl = Me
        '
        'gbAdditionalCommands
        '
        Me.gbAdditionalCommands.Controls.Add(Me.Label3)
        Me.gbAdditionalCommands.Controls.Add(Me.Panel12)
        Me.gbAdditionalCommands.Controls.Add(Me.txtRespawnSelectedUsers)
        Me.gbAdditionalCommands.Controls.Add(Me.chkRespawn)
        Me.gbAdditionalCommands.Location = New System.Drawing.Point(14, 387)
        Me.gbAdditionalCommands.Name = "gbAdditionalCommands"
        Me.gbAdditionalCommands.Size = New System.Drawing.Size(740, 100)
        Me.gbAdditionalCommands.TabIndex = 100
        Me.gbAdditionalCommands.TabStop = False
        Me.gbAdditionalCommands.Text = "Additional Commands"
        Me.gbAdditionalCommands.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 12)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "for MC1.4 or later"
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.rbRespawnSelected)
        Me.Panel12.Controls.Add(Me.rbRespawnEveryone)
        Me.Panel12.Location = New System.Drawing.Point(206, 29)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(154, 21)
        Me.Panel12.TabIndex = 20
        '
        'rbRespawnSelected
        '
        Me.rbRespawnSelected.AutoSize = True
        Me.rbRespawnSelected.Location = New System.Drawing.Point(82, 4)
        Me.rbRespawnSelected.Name = "rbRespawnSelected"
        Me.rbRespawnSelected.Size = New System.Drawing.Size(67, 16)
        Me.rbRespawnSelected.TabIndex = 25
        Me.rbRespawnSelected.TabStop = True
        Me.rbRespawnSelected.Text = "Selected"
        Me.rbRespawnSelected.UseVisualStyleBackColor = True
        '
        'rbRespawnEveryone
        '
        Me.rbRespawnEveryone.AutoSize = True
        Me.rbRespawnEveryone.Location = New System.Drawing.Point(6, 4)
        Me.rbRespawnEveryone.Name = "rbRespawnEveryone"
        Me.rbRespawnEveryone.Size = New System.Drawing.Size(70, 16)
        Me.rbRespawnEveryone.TabIndex = 24
        Me.rbRespawnEveryone.TabStop = True
        Me.rbRespawnEveryone.Text = "Everyone"
        Me.rbRespawnEveryone.UseVisualStyleBackColor = True
        '
        'txtRespawnSelectedUsers
        '
        Me.txtRespawnSelectedUsers.Location = New System.Drawing.Point(366, 31)
        Me.txtRespawnSelectedUsers.Name = "txtRespawnSelectedUsers"
        Me.txtRespawnSelectedUsers.Size = New System.Drawing.Size(368, 19)
        Me.txtRespawnSelectedUsers.TabIndex = 21
        '
        'chkRespawn
        '
        Me.chkRespawn.AutoSize = True
        Me.chkRespawn.Location = New System.Drawing.Point(6, 34)
        Me.chkRespawn.Name = "chkRespawn"
        Me.chkRespawn.Size = New System.Drawing.Size(66, 16)
        Me.chkRespawn.TabIndex = 19
        Me.chkRespawn.Text = "respawn"
        Me.chkRespawn.UseVisualStyleBackColor = True
        '
        'frmPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 526)
        Me.Controls.Add(Me.gbAdditionalCommands)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.gbAdminCommands)
        Me.Controls.Add(Me.gbUsefulCommands)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPrefixChar)
        Me.Controls.Add(Me.chkPermissionEnabled)
        Me.Controls.Add(Me.lblEnabled)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmPermission"
        Me.Text = "Permission Configuration"
        Me.gbUsefulCommands.ResumeLayout(False)
        Me.gbUsefulCommands.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbAdminCommands.ResumeLayout(False)
        Me.gbAdminCommands.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAdditionalCommands.ResumeLayout(False)
        Me.gbAdditionalCommands.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkPermissionEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblEnabled As System.Windows.Forms.Label
    Friend WithEvents txtPrefixChar As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbUsefulCommands As System.Windows.Forms.GroupBox
    Friend WithEvents chkGive As System.Windows.Forms.CheckBox
    Friend WithEvents chkTp As System.Windows.Forms.CheckBox
    Friend WithEvents chkXp As System.Windows.Forms.CheckBox
    Friend WithEvents chkTime As System.Windows.Forms.CheckBox
    Friend WithEvents gbAdminCommands As System.Windows.Forms.GroupBox
    Friend WithEvents chkPardon As System.Windows.Forms.CheckBox
    Friend WithEvents chkKick As System.Windows.Forms.CheckBox
    Friend WithEvents chkBan As System.Windows.Forms.CheckBox
    Friend WithEvents chkGamemode As System.Windows.Forms.CheckBox
    Friend WithEvents chkWhitelist As System.Windows.Forms.CheckBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rbGamemodeSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbGamemodeEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtGamemodeSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbXpSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbXpEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtXpSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbTimeSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbTimeEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtTimeSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbGiveSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbGiveEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtGiveSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbTpSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbTpEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtTpSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents rbPardonSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbPardonEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtPardonSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbBanSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbBanEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtBanSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rbKickSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbKickEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtKickSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents rbWhitelistSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbWhitelistEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtWhitelistSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents epInput As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents rbSpawnpointSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbSpawnpointEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtSpawnpointSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents chkSpawnpoint As System.Windows.Forms.CheckBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents rbWeatherSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbWeatherEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtWeatherSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents chkWeather As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gbAdditionalCommands As System.Windows.Forms.GroupBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents rbRespawnSelected As System.Windows.Forms.RadioButton
    Friend WithEvents rbRespawnEveryone As System.Windows.Forms.RadioButton
    Friend WithEvents txtRespawnSelectedUsers As System.Windows.Forms.TextBox
    Friend WithEvents chkRespawn As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
