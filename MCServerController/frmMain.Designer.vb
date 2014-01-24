<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PermissionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MCBansToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IPKickBANToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SchedulerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MuteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendMailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NGWordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomActionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServerpropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IDBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IPBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WhiteListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdatePlayerListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtendPlayersListAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearServerLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckLatestVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbStats = New System.Windows.Forms.GroupBox()
        Me.lblConnected = New System.Windows.Forms.Label()
        Me.lblConnectedName = New System.Windows.Forms.Label()
        Me.lblNextTime = New System.Windows.Forms.Label()
        Me.lblNextTimeName = New System.Windows.Forms.Label()
        Me.lblDataBackup = New System.Windows.Forms.Label()
        Me.lblDataBackupName = New System.Windows.Forms.Label()
        Me.lblPrivateIPName = New System.Windows.Forms.Label()
        Me.lblPrivateIP = New System.Windows.Forms.Label()
        Me.cmsIP = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblGlobalIPName = New System.Windows.Forms.Label()
        Me.lblGlobalIP = New System.Windows.Forms.Label()
        Me.lblMemUsage = New System.Windows.Forms.Label()
        Me.lblMemUsageName = New System.Windows.Forms.Label()
        Me.lblCPUUsage = New System.Windows.Forms.Label()
        Me.lblCPUUsageName = New System.Windows.Forms.Label()
        Me.lblUptime = New System.Windows.Forms.Label()
        Me.lblUptimeName = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.cmsPlayers = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PropertyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KickPlayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddWhiteListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddPlayerBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddIPBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteWhiteListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeletePlayerBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteIPBANListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeletePlayerFromListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearPlayerListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbLog = New System.Windows.Forms.GroupBox()
        Me.rtbServerLog = New System.Windows.Forms.RichTextBox()
        Me.gbSVController = New System.Windows.Forms.GroupBox()
        Me.cmbCommand = New System.Windows.Forms.ComboBox()
        Me.lblCommand = New System.Windows.Forms.Label()
        Me.rtbSystemLog = New System.Windows.Forms.RichTextBox()
        Me.btnKill = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.mcsProc = New System.Diagnostics.Process()
        Me.timTick = New System.Windows.Forms.Timer(Me.components)
        Me.pcCPU = New System.Diagnostics.PerformanceCounter()
        Me.pcMem = New System.Diagnostics.PerformanceCounter()
        Me.timBackup = New System.Windows.Forms.Timer(Me.components)
        Me.timHB = New System.Windows.Forms.Timer(Me.components)
        Me.gbPlayers = New System.Windows.Forms.GroupBox()
        Me.lvPlayers = New System.Windows.Forms.ListView()
        Me.chPlayer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chIP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chOnline = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLastLogin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLastLogout = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSpawn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBadCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ChatLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.gbStats.SuspendLayout()
        Me.cmsIP.SuspendLayout()
        Me.cmsPlayers.SuspendLayout()
        Me.gbLog.SuspendLayout()
        Me.gbSVController.SuspendLayout()
        CType(Me.pcCPU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcMem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPlayers.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.ActionToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 26)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurationToolStripMenuItem, Me.EditorToolStripMenuItem, Me.ChatLogToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(51, 22)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'ConfigurationToolStripMenuItem
        '
        Me.ConfigurationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerToolStripMenuItem, Me.BackupToolStripMenuItem, Me.PermissionToolStripMenuItem, Me.MCBansToolStripMenuItem, Me.IPKickBANToolStripMenuItem, Me.SchedulerToolStripMenuItem, Me.MuteToolStripMenuItem, Me.SendMailToolStripMenuItem, Me.NGWordsToolStripMenuItem, Me.CustomActionToolStripMenuItem})
        Me.ConfigurationToolStripMenuItem.Name = "ConfigurationToolStripMenuItem"
        Me.ConfigurationToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ConfigurationToolStripMenuItem.Text = "Configuration"
        '
        'ServerToolStripMenuItem
        '
        Me.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem"
        Me.ServerToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ServerToolStripMenuItem.Text = "Server"
        '
        'BackupToolStripMenuItem
        '
        Me.BackupToolStripMenuItem.Name = "BackupToolStripMenuItem"
        Me.BackupToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.BackupToolStripMenuItem.Text = "Backup"
        '
        'PermissionToolStripMenuItem
        '
        Me.PermissionToolStripMenuItem.Name = "PermissionToolStripMenuItem"
        Me.PermissionToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.PermissionToolStripMenuItem.Text = "Permission"
        '
        'MCBansToolStripMenuItem
        '
        Me.MCBansToolStripMenuItem.Enabled = False
        Me.MCBansToolStripMenuItem.Name = "MCBansToolStripMenuItem"
        Me.MCBansToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.MCBansToolStripMenuItem.Text = "MCBans"
        '
        'IPKickBANToolStripMenuItem
        '
        Me.IPKickBANToolStripMenuItem.Enabled = False
        Me.IPKickBANToolStripMenuItem.Name = "IPKickBANToolStripMenuItem"
        Me.IPKickBANToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.IPKickBANToolStripMenuItem.Text = "IP Kick/BAN"
        '
        'SchedulerToolStripMenuItem
        '
        Me.SchedulerToolStripMenuItem.Enabled = False
        Me.SchedulerToolStripMenuItem.Name = "SchedulerToolStripMenuItem"
        Me.SchedulerToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.SchedulerToolStripMenuItem.Text = "Scheduler"
        '
        'MuteToolStripMenuItem
        '
        Me.MuteToolStripMenuItem.Enabled = False
        Me.MuteToolStripMenuItem.Name = "MuteToolStripMenuItem"
        Me.MuteToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.MuteToolStripMenuItem.Text = "Mute"
        '
        'SendMailToolStripMenuItem
        '
        Me.SendMailToolStripMenuItem.Enabled = False
        Me.SendMailToolStripMenuItem.Name = "SendMailToolStripMenuItem"
        Me.SendMailToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.SendMailToolStripMenuItem.Text = "Send Mail"
        '
        'NGWordsToolStripMenuItem
        '
        Me.NGWordsToolStripMenuItem.Enabled = False
        Me.NGWordsToolStripMenuItem.Name = "NGWordsToolStripMenuItem"
        Me.NGWordsToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.NGWordsToolStripMenuItem.Text = "NG Words"
        '
        'CustomActionToolStripMenuItem
        '
        Me.CustomActionToolStripMenuItem.Enabled = False
        Me.CustomActionToolStripMenuItem.Name = "CustomActionToolStripMenuItem"
        Me.CustomActionToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.CustomActionToolStripMenuItem.Text = "Custom Action"
        '
        'EditorToolStripMenuItem
        '
        Me.EditorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerpropertiesToolStripMenuItem, Me.IDBANListToolStripMenuItem, Me.IPBANListToolStripMenuItem, Me.WhiteListToolStripMenuItem})
        Me.EditorToolStripMenuItem.Name = "EditorToolStripMenuItem"
        Me.EditorToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.EditorToolStripMenuItem.Text = "Editor"
        '
        'ServerpropertiesToolStripMenuItem
        '
        Me.ServerpropertiesToolStripMenuItem.Enabled = False
        Me.ServerpropertiesToolStripMenuItem.Name = "ServerpropertiesToolStripMenuItem"
        Me.ServerpropertiesToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.ServerpropertiesToolStripMenuItem.Text = "server.properties"
        '
        'IDBANListToolStripMenuItem
        '
        Me.IDBANListToolStripMenuItem.Enabled = False
        Me.IDBANListToolStripMenuItem.Name = "IDBANListToolStripMenuItem"
        Me.IDBANListToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.IDBANListToolStripMenuItem.Text = "IDBAN List"
        '
        'IPBANListToolStripMenuItem
        '
        Me.IPBANListToolStripMenuItem.Enabled = False
        Me.IPBANListToolStripMenuItem.Name = "IPBANListToolStripMenuItem"
        Me.IPBANListToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.IPBANListToolStripMenuItem.Text = "IPBAN List"
        '
        'WhiteListToolStripMenuItem
        '
        Me.WhiteListToolStripMenuItem.Enabled = False
        Me.WhiteListToolStripMenuItem.Name = "WhiteListToolStripMenuItem"
        Me.WhiteListToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.WhiteListToolStripMenuItem.Text = "White List"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(167, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ActionToolStripMenuItem
        '
        Me.ActionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdatePlayerListToolStripMenuItem, Me.DataBackupToolStripMenuItem, Me.ExtendPlayersListAreaToolStripMenuItem, Me.ClearServerLogToolStripMenuItem})
        Me.ActionToolStripMenuItem.Name = "ActionToolStripMenuItem"
        Me.ActionToolStripMenuItem.Size = New System.Drawing.Size(56, 22)
        Me.ActionToolStripMenuItem.Text = "Action"
        '
        'UpdatePlayerListToolStripMenuItem
        '
        Me.UpdatePlayerListToolStripMenuItem.Enabled = False
        Me.UpdatePlayerListToolStripMenuItem.Name = "UpdatePlayerListToolStripMenuItem"
        Me.UpdatePlayerListToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.UpdatePlayerListToolStripMenuItem.Text = "Update Player List"
        '
        'DataBackupToolStripMenuItem
        '
        Me.DataBackupToolStripMenuItem.Name = "DataBackupToolStripMenuItem"
        Me.DataBackupToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.DataBackupToolStripMenuItem.Text = "Data Backup"
        '
        'ExtendPlayersListAreaToolStripMenuItem
        '
        Me.ExtendPlayersListAreaToolStripMenuItem.Name = "ExtendPlayersListAreaToolStripMenuItem"
        Me.ExtendPlayersListAreaToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.ExtendPlayersListAreaToolStripMenuItem.Text = "Extend Players List Area"
        '
        'ClearServerLogToolStripMenuItem
        '
        Me.ClearServerLogToolStripMenuItem.Name = "ClearServerLogToolStripMenuItem"
        Me.ClearServerLogToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.ClearServerLogToolStripMenuItem.Text = "Clear Server Log"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InformationToolStripMenuItem, Me.CheckLatestVersionToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(46, 22)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'InformationToolStripMenuItem
        '
        Me.InformationToolStripMenuItem.Name = "InformationToolStripMenuItem"
        Me.InformationToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.InformationToolStripMenuItem.Text = "Information"
        '
        'CheckLatestVersionToolStripMenuItem
        '
        Me.CheckLatestVersionToolStripMenuItem.Name = "CheckLatestVersionToolStripMenuItem"
        Me.CheckLatestVersionToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.CheckLatestVersionToolStripMenuItem.Text = "Check Latest Version"
        '
        'gbStats
        '
        Me.gbStats.Controls.Add(Me.lblConnected)
        Me.gbStats.Controls.Add(Me.lblConnectedName)
        Me.gbStats.Controls.Add(Me.lblNextTime)
        Me.gbStats.Controls.Add(Me.lblNextTimeName)
        Me.gbStats.Controls.Add(Me.lblDataBackup)
        Me.gbStats.Controls.Add(Me.lblDataBackupName)
        Me.gbStats.Controls.Add(Me.lblPrivateIPName)
        Me.gbStats.Controls.Add(Me.lblPrivateIP)
        Me.gbStats.Controls.Add(Me.lblGlobalIPName)
        Me.gbStats.Controls.Add(Me.lblGlobalIP)
        Me.gbStats.Controls.Add(Me.lblMemUsage)
        Me.gbStats.Controls.Add(Me.lblMemUsageName)
        Me.gbStats.Controls.Add(Me.lblCPUUsage)
        Me.gbStats.Controls.Add(Me.lblCPUUsageName)
        Me.gbStats.Controls.Add(Me.lblUptime)
        Me.gbStats.Controls.Add(Me.lblUptimeName)
        Me.gbStats.Controls.Add(Me.lblServer)
        Me.gbStats.Controls.Add(Me.lblServerName)
        Me.gbStats.Location = New System.Drawing.Point(12, 29)
        Me.gbStats.Name = "gbStats"
        Me.gbStats.Size = New System.Drawing.Size(297, 154)
        Me.gbStats.TabIndex = 1
        Me.gbStats.TabStop = False
        Me.gbStats.Text = "Stats"
        '
        'lblConnected
        '
        Me.lblConnected.AutoSize = True
        Me.lblConnected.Location = New System.Drawing.Point(91, 63)
        Me.lblConnected.Name = "lblConnected"
        Me.lblConnected.Size = New System.Drawing.Size(11, 12)
        Me.lblConnected.TabIndex = 21
        Me.lblConnected.Text = "-"
        '
        'lblConnectedName
        '
        Me.lblConnectedName.AutoSize = True
        Me.lblConnectedName.Location = New System.Drawing.Point(10, 63)
        Me.lblConnectedName.Name = "lblConnectedName"
        Me.lblConnectedName.Size = New System.Drawing.Size(61, 12)
        Me.lblConnectedName.TabIndex = 20
        Me.lblConnectedName.Text = "Connected:"
        '
        'lblNextTime
        '
        Me.lblNextTime.AutoSize = True
        Me.lblNextTime.Location = New System.Drawing.Point(91, 134)
        Me.lblNextTime.Name = "lblNextTime"
        Me.lblNextTime.Size = New System.Drawing.Size(11, 12)
        Me.lblNextTime.TabIndex = 19
        Me.lblNextTime.Text = "-"
        '
        'lblNextTimeName
        '
        Me.lblNextTimeName.AutoSize = True
        Me.lblNextTimeName.Location = New System.Drawing.Point(10, 134)
        Me.lblNextTimeName.Name = "lblNextTimeName"
        Me.lblNextTimeName.Size = New System.Drawing.Size(60, 12)
        Me.lblNextTimeName.TabIndex = 18
        Me.lblNextTimeName.Text = "Next Time:"
        '
        'lblDataBackup
        '
        Me.lblDataBackup.AutoSize = True
        Me.lblDataBackup.Location = New System.Drawing.Point(91, 122)
        Me.lblDataBackup.Name = "lblDataBackup"
        Me.lblDataBackup.Size = New System.Drawing.Size(49, 12)
        Me.lblDataBackup.TabIndex = 17
        Me.lblDataBackup.Text = "Disabled"
        '
        'lblDataBackupName
        '
        Me.lblDataBackupName.AutoSize = True
        Me.lblDataBackupName.Location = New System.Drawing.Point(10, 122)
        Me.lblDataBackupName.Name = "lblDataBackupName"
        Me.lblDataBackupName.Size = New System.Drawing.Size(73, 12)
        Me.lblDataBackupName.TabIndex = 16
        Me.lblDataBackupName.Text = "Data Backup:"
        '
        'lblPrivateIPName
        '
        Me.lblPrivateIPName.AutoSize = True
        Me.lblPrivateIPName.Location = New System.Drawing.Point(10, 98)
        Me.lblPrivateIPName.Name = "lblPrivateIPName"
        Me.lblPrivateIPName.Size = New System.Drawing.Size(57, 12)
        Me.lblPrivateIPName.TabIndex = 14
        Me.lblPrivateIPName.Text = "Private IP:"
        '
        'lblPrivateIP
        '
        Me.lblPrivateIP.AutoSize = True
        Me.lblPrivateIP.ContextMenuStrip = Me.cmsIP
        Me.lblPrivateIP.Location = New System.Drawing.Point(91, 98)
        Me.lblPrivateIP.Name = "lblPrivateIP"
        Me.lblPrivateIP.Size = New System.Drawing.Size(92, 12)
        Me.lblPrivateIP.TabIndex = 13
        Me.lblPrivateIP.Text = "Getting PrivateIP"
        '
        'cmsIP
        '
        Me.cmsIP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToClipboardToolStripMenuItem, Me.UpdateToolStripMenuItem})
        Me.cmsIP.Name = "cmsGlobalIP"
        Me.cmsIP.Size = New System.Drawing.Size(180, 48)
        '
        'CopyToClipboardToolStripMenuItem
        '
        Me.CopyToClipboardToolStripMenuItem.Name = "CopyToClipboardToolStripMenuItem"
        Me.CopyToClipboardToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.CopyToClipboardToolStripMenuItem.Text = "Copy to Clipboard"
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.UpdateToolStripMenuItem.Text = "Update"
        '
        'lblGlobalIPName
        '
        Me.lblGlobalIPName.AutoSize = True
        Me.lblGlobalIPName.Location = New System.Drawing.Point(10, 86)
        Me.lblGlobalIPName.Name = "lblGlobalIPName"
        Me.lblGlobalIPName.Size = New System.Drawing.Size(53, 12)
        Me.lblGlobalIPName.TabIndex = 12
        Me.lblGlobalIPName.Text = "Global IP:"
        '
        'lblGlobalIP
        '
        Me.lblGlobalIP.AutoSize = True
        Me.lblGlobalIP.ContextMenuStrip = Me.cmsIP
        Me.lblGlobalIP.Location = New System.Drawing.Point(91, 86)
        Me.lblGlobalIP.Name = "lblGlobalIP"
        Me.lblGlobalIP.Size = New System.Drawing.Size(88, 12)
        Me.lblGlobalIP.TabIndex = 9
        Me.lblGlobalIP.Text = "Getting GlobalIP"
        '
        'lblMemUsage
        '
        Me.lblMemUsage.AutoSize = True
        Me.lblMemUsage.Location = New System.Drawing.Point(91, 51)
        Me.lblMemUsage.Name = "lblMemUsage"
        Me.lblMemUsage.Size = New System.Drawing.Size(11, 12)
        Me.lblMemUsage.TabIndex = 8
        Me.lblMemUsage.Text = "-"
        '
        'lblMemUsageName
        '
        Me.lblMemUsageName.AutoSize = True
        Me.lblMemUsageName.Location = New System.Drawing.Point(10, 51)
        Me.lblMemUsageName.Name = "lblMemUsageName"
        Me.lblMemUsageName.Size = New System.Drawing.Size(68, 12)
        Me.lblMemUsageName.TabIndex = 7
        Me.lblMemUsageName.Text = "MEM Usage:"
        '
        'lblCPUUsage
        '
        Me.lblCPUUsage.AutoSize = True
        Me.lblCPUUsage.Location = New System.Drawing.Point(91, 39)
        Me.lblCPUUsage.Name = "lblCPUUsage"
        Me.lblCPUUsage.Size = New System.Drawing.Size(11, 12)
        Me.lblCPUUsage.TabIndex = 6
        Me.lblCPUUsage.Text = "-"
        '
        'lblCPUUsageName
        '
        Me.lblCPUUsageName.AutoSize = True
        Me.lblCPUUsageName.Location = New System.Drawing.Point(10, 39)
        Me.lblCPUUsageName.Name = "lblCPUUsageName"
        Me.lblCPUUsageName.Size = New System.Drawing.Size(66, 12)
        Me.lblCPUUsageName.TabIndex = 5
        Me.lblCPUUsageName.Text = "CPU Usage:"
        '
        'lblUptime
        '
        Me.lblUptime.AutoSize = True
        Me.lblUptime.Location = New System.Drawing.Point(91, 27)
        Me.lblUptime.Name = "lblUptime"
        Me.lblUptime.Size = New System.Drawing.Size(11, 12)
        Me.lblUptime.TabIndex = 4
        Me.lblUptime.Text = "-"
        '
        'lblUptimeName
        '
        Me.lblUptimeName.AutoSize = True
        Me.lblUptimeName.Location = New System.Drawing.Point(10, 27)
        Me.lblUptimeName.Name = "lblUptimeName"
        Me.lblUptimeName.Size = New System.Drawing.Size(43, 12)
        Me.lblUptimeName.TabIndex = 3
        Me.lblUptimeName.Text = "Uptime:"
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(91, 15)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(68, 12)
        Me.lblServer.TabIndex = 2
        Me.lblServer.Text = "Not Running"
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Location = New System.Drawing.Point(10, 15)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(77, 12)
        Me.lblServerName.TabIndex = 1
        Me.lblServerName.Text = "Server Status:"
        '
        'cmsPlayers
        '
        Me.cmsPlayers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PropertyToolStripMenuItem, Me.KickPlayerToolStripMenuItem, Me.ToolStripSeparator2, Me.AddWhiteListToolStripMenuItem, Me.AddPlayerBANListToolStripMenuItem, Me.AddIPBANListToolStripMenuItem, Me.ToolStripSeparator3, Me.DeleteWhiteListToolStripMenuItem, Me.DeletePlayerBANListToolStripMenuItem, Me.DeleteIPBANListToolStripMenuItem, Me.ToolStripSeparator4, Me.DeletePlayerFromListToolStripMenuItem, Me.ClearPlayerListToolStripMenuItem})
        Me.cmsPlayers.Name = "cmsPlayers"
        Me.cmsPlayers.Size = New System.Drawing.Size(212, 242)
        '
        'PropertyToolStripMenuItem
        '
        Me.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem"
        Me.PropertyToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PropertyToolStripMenuItem.Text = "Property"
        '
        'KickPlayerToolStripMenuItem
        '
        Me.KickPlayerToolStripMenuItem.Name = "KickPlayerToolStripMenuItem"
        Me.KickPlayerToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.KickPlayerToolStripMenuItem.Text = "Kick Player"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(208, 6)
        '
        'AddWhiteListToolStripMenuItem
        '
        Me.AddWhiteListToolStripMenuItem.Name = "AddWhiteListToolStripMenuItem"
        Me.AddWhiteListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.AddWhiteListToolStripMenuItem.Text = "Add White-List"
        '
        'AddPlayerBANListToolStripMenuItem
        '
        Me.AddPlayerBANListToolStripMenuItem.Name = "AddPlayerBANListToolStripMenuItem"
        Me.AddPlayerBANListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.AddPlayerBANListToolStripMenuItem.Text = "Add Player BAN List"
        '
        'AddIPBANListToolStripMenuItem
        '
        Me.AddIPBANListToolStripMenuItem.Name = "AddIPBANListToolStripMenuItem"
        Me.AddIPBANListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.AddIPBANListToolStripMenuItem.Text = "Add IP BAN List"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(208, 6)
        '
        'DeleteWhiteListToolStripMenuItem
        '
        Me.DeleteWhiteListToolStripMenuItem.Name = "DeleteWhiteListToolStripMenuItem"
        Me.DeleteWhiteListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.DeleteWhiteListToolStripMenuItem.Text = "Delete White-List"
        '
        'DeletePlayerBANListToolStripMenuItem
        '
        Me.DeletePlayerBANListToolStripMenuItem.Name = "DeletePlayerBANListToolStripMenuItem"
        Me.DeletePlayerBANListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.DeletePlayerBANListToolStripMenuItem.Text = "Delete Player BAN List"
        '
        'DeleteIPBANListToolStripMenuItem
        '
        Me.DeleteIPBANListToolStripMenuItem.Name = "DeleteIPBANListToolStripMenuItem"
        Me.DeleteIPBANListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.DeleteIPBANListToolStripMenuItem.Text = "Delete IP BAN List"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(208, 6)
        '
        'DeletePlayerFromListToolStripMenuItem
        '
        Me.DeletePlayerFromListToolStripMenuItem.Name = "DeletePlayerFromListToolStripMenuItem"
        Me.DeletePlayerFromListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.DeletePlayerFromListToolStripMenuItem.Text = "Delete Player from List"
        '
        'ClearPlayerListToolStripMenuItem
        '
        Me.ClearPlayerListToolStripMenuItem.ForeColor = System.Drawing.Color.Red
        Me.ClearPlayerListToolStripMenuItem.Name = "ClearPlayerListToolStripMenuItem"
        Me.ClearPlayerListToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.ClearPlayerListToolStripMenuItem.Text = "Clear Player List"
        '
        'gbLog
        '
        Me.gbLog.Controls.Add(Me.rtbServerLog)
        Me.gbLog.Location = New System.Drawing.Point(315, 29)
        Me.gbLog.Name = "gbLog"
        Me.gbLog.Size = New System.Drawing.Size(457, 385)
        Me.gbLog.TabIndex = 2
        Me.gbLog.TabStop = False
        Me.gbLog.Text = "Log and Chat"
        '
        'rtbServerLog
        '
        Me.rtbServerLog.BackColor = System.Drawing.Color.White
        Me.rtbServerLog.Location = New System.Drawing.Point(6, 18)
        Me.rtbServerLog.Name = "rtbServerLog"
        Me.rtbServerLog.ReadOnly = True
        Me.rtbServerLog.Size = New System.Drawing.Size(445, 361)
        Me.rtbServerLog.TabIndex = 0
        Me.rtbServerLog.Text = ""
        '
        'gbSVController
        '
        Me.gbSVController.Controls.Add(Me.cmbCommand)
        Me.gbSVController.Controls.Add(Me.lblCommand)
        Me.gbSVController.Controls.Add(Me.rtbSystemLog)
        Me.gbSVController.Controls.Add(Me.btnKill)
        Me.gbSVController.Controls.Add(Me.btnStop)
        Me.gbSVController.Controls.Add(Me.btnRun)
        Me.gbSVController.Location = New System.Drawing.Point(12, 420)
        Me.gbSVController.Name = "gbSVController"
        Me.gbSVController.Size = New System.Drawing.Size(760, 130)
        Me.gbSVController.TabIndex = 3
        Me.gbSVController.TabStop = False
        Me.gbSVController.Text = "Server Controller"
        '
        'cmbCommand
        '
        Me.cmbCommand.Enabled = False
        Me.cmbCommand.FormattingEnabled = True
        Me.cmbCommand.Location = New System.Drawing.Point(67, 104)
        Me.cmbCommand.Name = "cmbCommand"
        Me.cmbCommand.Size = New System.Drawing.Size(687, 20)
        Me.cmbCommand.TabIndex = 6
        '
        'lblCommand
        '
        Me.lblCommand.AutoSize = True
        Me.lblCommand.Location = New System.Drawing.Point(6, 108)
        Me.lblCommand.Name = "lblCommand"
        Me.lblCommand.Size = New System.Drawing.Size(55, 12)
        Me.lblCommand.TabIndex = 5
        Me.lblCommand.Text = "Command"
        '
        'rtbSystemLog
        '
        Me.rtbSystemLog.BackColor = System.Drawing.Color.White
        Me.rtbSystemLog.Location = New System.Drawing.Point(87, 18)
        Me.rtbSystemLog.Name = "rtbSystemLog"
        Me.rtbSystemLog.ReadOnly = True
        Me.rtbSystemLog.Size = New System.Drawing.Size(667, 81)
        Me.rtbSystemLog.TabIndex = 3
        Me.rtbSystemLog.Text = ""
        '
        'btnKill
        '
        Me.btnKill.Enabled = False
        Me.btnKill.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKill.ForeColor = System.Drawing.Color.Red
        Me.btnKill.Location = New System.Drawing.Point(6, 76)
        Me.btnKill.Name = "btnKill"
        Me.btnKill.Size = New System.Drawing.Size(75, 23)
        Me.btnKill.TabIndex = 2
        Me.btnKill.Text = "Kill"
        Me.btnKill.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(6, 47)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(6, 18)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'mcsProc
        '
        Me.mcsProc.StartInfo.Domain = ""
        Me.mcsProc.StartInfo.LoadUserProfile = False
        Me.mcsProc.StartInfo.Password = Nothing
        Me.mcsProc.StartInfo.StandardErrorEncoding = Nothing
        Me.mcsProc.StartInfo.StandardOutputEncoding = Nothing
        Me.mcsProc.StartInfo.UserName = ""
        Me.mcsProc.SynchronizingObject = Me
        '
        'timTick
        '
        Me.timTick.Interval = 1000
        '
        'pcCPU
        '
        Me.pcCPU.CategoryName = "Process"
        Me.pcCPU.CounterName = "% Processor Time"
        '
        'pcMem
        '
        Me.pcMem.CategoryName = "Process"
        Me.pcMem.CounterName = "Working Set"
        '
        'timBackup
        '
        Me.timBackup.Interval = 3600000
        '
        'timHB
        '
        Me.timHB.Interval = 10000
        '
        'gbPlayers
        '
        Me.gbPlayers.Controls.Add(Me.lvPlayers)
        Me.gbPlayers.Location = New System.Drawing.Point(12, 185)
        Me.gbPlayers.Name = "gbPlayers"
        Me.gbPlayers.Size = New System.Drawing.Size(297, 229)
        Me.gbPlayers.TabIndex = 4
        Me.gbPlayers.TabStop = False
        Me.gbPlayers.Text = "Players"
        '
        'lvPlayers
        '
        Me.lvPlayers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chPlayer, Me.chIP, Me.chOnline, Me.chLastLogin, Me.chLastLogout, Me.chSpawn, Me.chBadCount})
        Me.lvPlayers.ContextMenuStrip = Me.cmsPlayers
        Me.lvPlayers.Location = New System.Drawing.Point(6, 18)
        Me.lvPlayers.MultiSelect = False
        Me.lvPlayers.Name = "lvPlayers"
        Me.lvPlayers.Size = New System.Drawing.Size(285, 205)
        Me.lvPlayers.TabIndex = 0
        Me.lvPlayers.UseCompatibleStateImageBehavior = False
        Me.lvPlayers.View = System.Windows.Forms.View.Details
        '
        'chPlayer
        '
        Me.chPlayer.Text = "Player"
        Me.chPlayer.Width = 80
        '
        'chIP
        '
        Me.chIP.Text = "IP"
        Me.chIP.Width = 120
        '
        'chOnline
        '
        Me.chOnline.Text = "Online"
        '
        'chLastLogin
        '
        Me.chLastLogin.Text = "LastLogin"
        Me.chLastLogin.Width = 5
        '
        'chLastLogout
        '
        Me.chLastLogout.Text = "LastLogout"
        Me.chLastLogout.Width = 5
        '
        'chSpawn
        '
        Me.chSpawn.Text = "SpawnPoint"
        '
        'chBadCount
        '
        Me.chBadCount.Text = "BadCount"
        Me.chBadCount.Width = 5
        '
        'ChatLogToolStripMenuItem
        '
        Me.ChatLogToolStripMenuItem.Name = "ChatLogToolStripMenuItem"
        Me.ChatLogToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ChatLogToolStripMenuItem.Text = "ChatLogWindow"
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.gbSVController)
        Me.Controls.Add(Me.gbLog)
        Me.Controls.Add(Me.gbStats)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.gbPlayers)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmMain"
        Me.Text = "MCServerController画面"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.gbStats.ResumeLayout(False)
        Me.gbStats.PerformLayout()
        Me.cmsIP.ResumeLayout(False)
        Me.cmsPlayers.ResumeLayout(False)
        Me.gbLog.ResumeLayout(False)
        Me.gbSVController.ResumeLayout(False)
        Me.gbSVController.PerformLayout()
        CType(Me.pcCPU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcMem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPlayers.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbStats As System.Windows.Forms.GroupBox
    Friend WithEvents gbLog As System.Windows.Forms.GroupBox
    Friend WithEvents rtbServerLog As System.Windows.Forms.RichTextBox
    Friend WithEvents gbSVController As System.Windows.Forms.GroupBox
    Friend WithEvents ServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NGWordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents rtbSystemLog As System.Windows.Forms.RichTextBox
    Friend WithEvents btnKill As System.Windows.Forms.Button
    Friend WithEvents lblCommand As System.Windows.Forms.Label
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents lblServerName As System.Windows.Forms.Label
    Friend WithEvents lblUptime As System.Windows.Forms.Label
    Friend WithEvents lblUptimeName As System.Windows.Forms.Label
    Friend WithEvents CustomActionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IPBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mcsProc As System.Diagnostics.Process
    Friend WithEvents timTick As System.Windows.Forms.Timer
    Friend WithEvents cmbCommand As System.Windows.Forms.ComboBox
    Friend WithEvents lblMemUsage As System.Windows.Forms.Label
    Friend WithEvents lblMemUsageName As System.Windows.Forms.Label
    Friend WithEvents lblCPUUsage As System.Windows.Forms.Label
    Friend WithEvents lblCPUUsageName As System.Windows.Forms.Label
    Friend WithEvents pcCPU As System.Diagnostics.PerformanceCounter
    Friend WithEvents pcMem As System.Diagnostics.PerformanceCounter
    Friend WithEvents cmsPlayers As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddWhiteListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddPlayerBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddIPBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KickPlayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteWhiteListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeletePlayerBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteIPBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblGlobalIP As System.Windows.Forms.Label
    Friend WithEvents cmsIP As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblPrivateIPName As System.Windows.Forms.Label
    Friend WithEvents lblPrivateIP As System.Windows.Forms.Label
    Friend WithEvents lblGlobalIPName As System.Windows.Forms.Label
    Friend WithEvents CopyToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SchedulerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timBackup As System.Windows.Forms.Timer
    Friend WithEvents BackupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblDataBackup As System.Windows.Forms.Label
    Friend WithEvents lblDataBackupName As System.Windows.Forms.Label
    Friend WithEvents ActionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataBackupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblNextTime As System.Windows.Forms.Label
    Friend WithEvents lblNextTimeName As System.Windows.Forms.Label
    Friend WithEvents timHB As System.Windows.Forms.Timer
    Friend WithEvents SendMailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeletePlayerFromListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdatePlayerListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServerpropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IDBANListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckLatestVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IPKickBANToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PermissionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExtendPlayersListAreaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbPlayers As System.Windows.Forms.GroupBox
    Friend WithEvents lvPlayers As System.Windows.Forms.ListView
    Friend WithEvents chPlayer As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIP As System.Windows.Forms.ColumnHeader
    Friend WithEvents chOnline As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLastLogin As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLastLogout As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBadCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents ClearServerLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WhiteListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblConnected As System.Windows.Forms.Label
    Friend WithEvents lblConnectedName As System.Windows.Forms.Label
    Friend WithEvents MCBansToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chSpawn As System.Windows.Forms.ColumnHeader
    Friend WithEvents MuteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearPlayerListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChatLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
