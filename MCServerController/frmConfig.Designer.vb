<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
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
        Me.txtJarPath = New System.Windows.Forms.TextBox()
        Me.rbBukkit = New System.Windows.Forms.RadioButton()
        Me.btnServerFileOpen = New System.Windows.Forms.Button()
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog()
        Me.lblJavaFile = New System.Windows.Forms.Label()
        Me.btnJavaExeOpen = New System.Windows.Forms.Button()
        Me.txtJavaExe = New System.Windows.Forms.TextBox()
        Me.lblAugment = New System.Windows.Forms.Label()
        Me.btnAugmentDefault = New System.Windows.Forms.Button()
        Me.txtAugment = New System.Windows.Forms.TextBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkAutoStart = New System.Windows.Forms.CheckBox()
        Me.chkAutoRecovery = New System.Windows.Forms.CheckBox()
        Me.chkShowConsole = New System.Windows.Forms.CheckBox()
        Me.epInput = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.panelServerType = New System.Windows.Forms.Panel()
        Me.rbOfficial = New System.Windows.Forms.RadioButton()
        Me.panelServerVersion = New System.Windows.Forms.Panel()
        Me.rbV17 = New System.Windows.Forms.RadioButton()
        Me.rbV14 = New System.Windows.Forms.RadioButton()
        Me.rbV131 = New System.Windows.Forms.RadioButton()
        Me.rbV125 = New System.Windows.Forms.RadioButton()
        Me.gbHeartBeat = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtHeartBeatKillCount = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtHeartBeatStopCount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkHeartBeatUse0xFE = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtHeartBeatInterval = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtJarFileAugment = New System.Windows.Forms.TextBox()
        Me.btnJarFileAugment = New System.Windows.Forms.Button()
        Me.lblJarFileAugment = New System.Windows.Forms.Label()
        Me.ttipHelp = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelServerType.SuspendLayout()
        Me.panelServerVersion.SuspendLayout()
        Me.gbHeartBeat.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "サーバファイルの場所"
        '
        'txtJarPath
        '
        Me.txtJarPath.Location = New System.Drawing.Point(79, 69)
        Me.txtJarPath.Name = "txtJarPath"
        Me.txtJarPath.Size = New System.Drawing.Size(543, 19)
        Me.txtJarPath.TabIndex = 1
        '
        'rbBukkit
        '
        Me.rbBukkit.AutoSize = True
        Me.rbBukkit.Location = New System.Drawing.Point(65, 3)
        Me.rbBukkit.Name = "rbBukkit"
        Me.rbBukkit.Size = New System.Drawing.Size(56, 16)
        Me.rbBukkit.TabIndex = 3
        Me.rbBukkit.TabStop = True
        Me.rbBukkit.Text = "Bukkit"
        Me.rbBukkit.UseVisualStyleBackColor = True
        '
        'btnServerFileOpen
        '
        Me.btnServerFileOpen.Location = New System.Drawing.Point(12, 67)
        Me.btnServerFileOpen.Name = "btnServerFileOpen"
        Me.btnServerFileOpen.Size = New System.Drawing.Size(61, 23)
        Me.btnServerFileOpen.TabIndex = 4
        Me.btnServerFileOpen.Text = "開く"
        Me.btnServerFileOpen.UseVisualStyleBackColor = True
        '
        'ofdFile
        '
        Me.ofdFile.FileName = "minecraft_server.jar"
        '
        'lblJavaFile
        '
        Me.lblJavaFile.AutoSize = True
        Me.lblJavaFile.Location = New System.Drawing.Point(12, 102)
        Me.lblJavaFile.Name = "lblJavaFile"
        Me.lblJavaFile.Size = New System.Drawing.Size(80, 12)
        Me.lblJavaFile.TabIndex = 5
        Me.lblJavaFile.Text = "java.exeの場所"
        '
        'btnJavaExeOpen
        '
        Me.btnJavaExeOpen.Location = New System.Drawing.Point(12, 117)
        Me.btnJavaExeOpen.Name = "btnJavaExeOpen"
        Me.btnJavaExeOpen.Size = New System.Drawing.Size(61, 23)
        Me.btnJavaExeOpen.TabIndex = 6
        Me.btnJavaExeOpen.Text = "開く"
        Me.btnJavaExeOpen.UseVisualStyleBackColor = True
        '
        'txtJavaExe
        '
        Me.txtJavaExe.Location = New System.Drawing.Point(79, 119)
        Me.txtJavaExe.Name = "txtJavaExe"
        Me.txtJavaExe.Size = New System.Drawing.Size(543, 19)
        Me.txtJavaExe.TabIndex = 7
        '
        'lblAugment
        '
        Me.lblAugment.AutoSize = True
        Me.lblAugment.Location = New System.Drawing.Point(10, 152)
        Me.lblAugment.Name = "lblAugment"
        Me.lblAugment.Size = New System.Drawing.Size(88, 12)
        Me.lblAugment.TabIndex = 8
        Me.lblAugment.Text = "Javaの起動引数"
        '
        'btnAugmentDefault
        '
        Me.btnAugmentDefault.Location = New System.Drawing.Point(10, 167)
        Me.btnAugmentDefault.Name = "btnAugmentDefault"
        Me.btnAugmentDefault.Size = New System.Drawing.Size(61, 23)
        Me.btnAugmentDefault.TabIndex = 9
        Me.btnAugmentDefault.Text = "開く"
        Me.btnAugmentDefault.UseVisualStyleBackColor = True
        '
        'txtAugment
        '
        Me.txtAugment.Location = New System.Drawing.Point(77, 169)
        Me.txtAugment.Name = "txtAugment"
        Me.txtAugment.Size = New System.Drawing.Size(543, 19)
        Me.txtAugment.TabIndex = 10
        Me.ttipHelp.SetToolTip(Me.txtAugment, "Set Minimum Memory : -Xms1024M" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Set Maximum Memory : -Xmx1024M" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Set Logfile Encod" & _
        "ing UTF-8 : -Dfile.encoding=utf-8")
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(12, 346)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(61, 23)
        Me.btnApply.TabIndex = 11
        Me.btnApply.Text = "適用"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(79, 346)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkAutoStart
        '
        Me.chkAutoStart.AutoSize = True
        Me.chkAutoStart.Location = New System.Drawing.Point(12, 266)
        Me.chkAutoStart.Name = "chkAutoStart"
        Me.chkAutoStart.Size = New System.Drawing.Size(183, 16)
        Me.chkAutoStart.TabIndex = 13
        Me.chkAutoStart.Text = "MCSC起動時にサーバも起動する"
        Me.chkAutoStart.UseVisualStyleBackColor = True
        '
        'chkAutoRecovery
        '
        Me.chkAutoRecovery.AutoSize = True
        Me.chkAutoRecovery.Location = New System.Drawing.Point(12, 288)
        Me.chkAutoRecovery.Name = "chkAutoRecovery"
        Me.chkAutoRecovery.Size = New System.Drawing.Size(157, 16)
        Me.chkAutoRecovery.TabIndex = 14
        Me.chkAutoRecovery.Text = "自動復旧機能を有効にする"
        Me.chkAutoRecovery.UseVisualStyleBackColor = True
        '
        'chkShowConsole
        '
        Me.chkShowConsole.AutoSize = True
        Me.chkShowConsole.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowConsole.Location = New System.Drawing.Point(12, 310)
        Me.chkShowConsole.Name = "chkShowConsole"
        Me.chkShowConsole.Size = New System.Drawing.Size(156, 16)
        Me.chkShowConsole.TabIndex = 15
        Me.chkShowConsole.Text = "標準のコンソールも表示する"
        Me.chkShowConsole.UseVisualStyleBackColor = True
        '
        'epInput
        '
        Me.epInput.ContainerControl = Me
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "サーバ種別"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 12)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "バージョン"
        '
        'panelServerType
        '
        Me.panelServerType.Controls.Add(Me.rbOfficial)
        Me.panelServerType.Controls.Add(Me.rbBukkit)
        Me.panelServerType.Location = New System.Drawing.Point(97, 2)
        Me.panelServerType.Name = "panelServerType"
        Me.panelServerType.Size = New System.Drawing.Size(523, 21)
        Me.panelServerType.TabIndex = 19
        '
        'rbOfficial
        '
        Me.rbOfficial.AutoSize = True
        Me.rbOfficial.Location = New System.Drawing.Point(3, 3)
        Me.rbOfficial.Name = "rbOfficial"
        Me.rbOfficial.Size = New System.Drawing.Size(60, 16)
        Me.rbOfficial.TabIndex = 4
        Me.rbOfficial.TabStop = True
        Me.rbOfficial.Text = "Official"
        Me.rbOfficial.UseVisualStyleBackColor = True
        '
        'panelServerVersion
        '
        Me.panelServerVersion.Controls.Add(Me.rbV17)
        Me.panelServerVersion.Controls.Add(Me.rbV14)
        Me.panelServerVersion.Controls.Add(Me.rbV131)
        Me.panelServerVersion.Controls.Add(Me.rbV125)
        Me.panelServerVersion.Location = New System.Drawing.Point(97, 23)
        Me.panelServerVersion.Name = "panelServerVersion"
        Me.panelServerVersion.Size = New System.Drawing.Size(523, 21)
        Me.panelServerVersion.TabIndex = 20
        '
        'rbV17
        '
        Me.rbV17.AutoSize = True
        Me.rbV17.Location = New System.Drawing.Point(215, 2)
        Me.rbV17.Name = "rbV17"
        Me.rbV17.Size = New System.Drawing.Size(43, 16)
        Me.rbV17.TabIndex = 20
        Me.rbV17.TabStop = True
        Me.rbV17.Text = "1.7-"
        Me.rbV17.UseVisualStyleBackColor = True
        '
        'rbV14
        '
        Me.rbV14.AutoSize = True
        Me.rbV14.Location = New System.Drawing.Point(144, 2)
        Me.rbV14.Name = "rbV14"
        Me.rbV14.Size = New System.Drawing.Size(65, 16)
        Me.rbV14.TabIndex = 19
        Me.rbV14.TabStop = True
        Me.rbV14.Text = "1.4-1.6.4"
        Me.rbV14.UseVisualStyleBackColor = True
        '
        'rbV131
        '
        Me.rbV131.AutoSize = True
        Me.rbV131.Location = New System.Drawing.Point(65, 2)
        Me.rbV131.Name = "rbV131"
        Me.rbV131.Size = New System.Drawing.Size(73, 16)
        Me.rbV131.TabIndex = 18
        Me.rbV131.TabStop = True
        Me.rbV131.Text = "1.3.1-1.3.2"
        Me.rbV131.UseVisualStyleBackColor = True
        '
        'rbV125
        '
        Me.rbV125.AutoSize = True
        Me.rbV125.Location = New System.Drawing.Point(3, 2)
        Me.rbV125.Name = "rbV125"
        Me.rbV125.Size = New System.Drawing.Size(51, 16)
        Me.rbV125.TabIndex = 17
        Me.rbV125.TabStop = True
        Me.rbV125.Text = "-1.2.5"
        Me.rbV125.UseVisualStyleBackColor = True
        '
        'gbHeartBeat
        '
        Me.gbHeartBeat.Controls.Add(Me.Label9)
        Me.gbHeartBeat.Controls.Add(Me.txtHeartBeatKillCount)
        Me.gbHeartBeat.Controls.Add(Me.Label10)
        Me.gbHeartBeat.Controls.Add(Me.Label7)
        Me.gbHeartBeat.Controls.Add(Me.txtHeartBeatStopCount)
        Me.gbHeartBeat.Controls.Add(Me.Label8)
        Me.gbHeartBeat.Controls.Add(Me.Label6)
        Me.gbHeartBeat.Controls.Add(Me.chkHeartBeatUse0xFE)
        Me.gbHeartBeat.Controls.Add(Me.Label5)
        Me.gbHeartBeat.Controls.Add(Me.txtHeartBeatInterval)
        Me.gbHeartBeat.Controls.Add(Me.Label4)
        Me.gbHeartBeat.Location = New System.Drawing.Point(316, 256)
        Me.gbHeartBeat.Name = "gbHeartBeat"
        Me.gbHeartBeat.Size = New System.Drawing.Size(306, 113)
        Me.gbHeartBeat.TabIndex = 26
        Me.gbHeartBeat.TabStop = False
        Me.gbHeartBeat.Text = "HeartBeat Option"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(164, 65)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 12)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "times"
        '
        'txtHeartBeatKillCount
        '
        Me.txtHeartBeatKillCount.Location = New System.Drawing.Point(113, 62)
        Me.txtHeartBeatKillCount.Name = "txtHeartBeatKillCount"
        Me.txtHeartBeatKillCount.Size = New System.Drawing.Size(41, 19)
        Me.txtHeartBeatKillCount.TabIndex = 33
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 65)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 12)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "Server Kill Count"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(164, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 12)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "times"
        '
        'txtHeartBeatStopCount
        '
        Me.txtHeartBeatStopCount.Location = New System.Drawing.Point(113, 37)
        Me.txtHeartBeatStopCount.Name = "txtHeartBeatStopCount"
        Me.txtHeartBeatStopCount.Size = New System.Drawing.Size(41, 19)
        Me.txtHeartBeatStopCount.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 12)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Server Stop Count"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(217, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 12)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "(SLOW!)"
        '
        'chkHeartBeatUse0xFE
        '
        Me.chkHeartBeatUse0xFE.AutoSize = True
        Me.chkHeartBeatUse0xFE.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHeartBeatUse0xFE.Location = New System.Drawing.Point(8, 87)
        Me.chkHeartBeatUse0xFE.Name = "chkHeartBeatUse0xFE"
        Me.chkHeartBeatUse0xFE.Size = New System.Drawing.Size(203, 16)
        Me.chkHeartBeatUse0xFE.TabIndex = 27
        Me.chkHeartBeatUse0xFE.Text = "Use Server List Ping(0xFE) Packet"
        Me.chkHeartBeatUse0xFE.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(164, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 12)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "sec"
        '
        'txtHeartBeatInterval
        '
        Me.txtHeartBeatInterval.Location = New System.Drawing.Point(113, 12)
        Me.txtHeartBeatInterval.Name = "txtHeartBeatInterval"
        Me.txtHeartBeatInterval.Size = New System.Drawing.Size(41, 19)
        Me.txtHeartBeatInterval.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 12)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "HeartBeat Interval"
        '
        'txtJarFileAugment
        '
        Me.txtJarFileAugment.Location = New System.Drawing.Point(77, 219)
        Me.txtJarFileAugment.Name = "txtJarFileAugment"
        Me.txtJarFileAugment.Size = New System.Drawing.Size(543, 19)
        Me.txtJarFileAugment.TabIndex = 29
        Me.ttipHelp.SetToolTip(Me.txtJarFileAugment, "Show GUI Window(for Official Server) : nogui" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Set nojline(for CraftBukkit Server)" & _
        " : --nojline")
        '
        'btnJarFileAugment
        '
        Me.btnJarFileAugment.Location = New System.Drawing.Point(10, 217)
        Me.btnJarFileAugment.Name = "btnJarFileAugment"
        Me.btnJarFileAugment.Size = New System.Drawing.Size(61, 23)
        Me.btnJarFileAugment.TabIndex = 28
        Me.btnJarFileAugment.Text = "開く"
        Me.btnJarFileAugment.UseVisualStyleBackColor = True
        '
        'lblJarFileAugment
        '
        Me.lblJarFileAugment.AutoSize = True
        Me.lblJarFileAugment.Location = New System.Drawing.Point(10, 202)
        Me.lblJarFileAugment.Name = "lblJarFileAugment"
        Me.lblJarFileAugment.Size = New System.Drawing.Size(93, 12)
        Me.lblJarFileAugment.TabIndex = 27
        Me.lblJarFileAugment.Text = "サーバの起動引数"
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 381)
        Me.Controls.Add(Me.txtJarFileAugment)
        Me.Controls.Add(Me.btnJarFileAugment)
        Me.Controls.Add(Me.lblJarFileAugment)
        Me.Controls.Add(Me.gbHeartBeat)
        Me.Controls.Add(Me.panelServerVersion)
        Me.Controls.Add(Me.panelServerType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkShowConsole)
        Me.Controls.Add(Me.chkAutoRecovery)
        Me.Controls.Add(Me.chkAutoStart)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.txtAugment)
        Me.Controls.Add(Me.btnAugmentDefault)
        Me.Controls.Add(Me.lblAugment)
        Me.Controls.Add(Me.txtJavaExe)
        Me.Controls.Add(Me.btnJavaExeOpen)
        Me.Controls.Add(Me.lblJavaFile)
        Me.Controls.Add(Me.btnServerFileOpen)
        Me.Controls.Add(Me.txtJarPath)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmConfig"
        Me.Text = "Configuration"
        CType(Me.epInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelServerType.ResumeLayout(False)
        Me.panelServerType.PerformLayout()
        Me.panelServerVersion.ResumeLayout(False)
        Me.panelServerVersion.PerformLayout()
        Me.gbHeartBeat.ResumeLayout(False)
        Me.gbHeartBeat.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtJarPath As System.Windows.Forms.TextBox
    Friend WithEvents rbBukkit As System.Windows.Forms.RadioButton
    Friend WithEvents btnServerFileOpen As System.Windows.Forms.Button
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblJavaFile As System.Windows.Forms.Label
    Friend WithEvents btnJavaExeOpen As System.Windows.Forms.Button
    Friend WithEvents txtJavaExe As System.Windows.Forms.TextBox
    Friend WithEvents lblAugment As System.Windows.Forms.Label
    Friend WithEvents btnAugmentDefault As System.Windows.Forms.Button
    Friend WithEvents txtAugment As System.Windows.Forms.TextBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkAutoStart As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoRecovery As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowConsole As System.Windows.Forms.CheckBox
    Friend WithEvents epInput As System.Windows.Forms.ErrorProvider
    Friend WithEvents panelServerType As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbOfficial As System.Windows.Forms.RadioButton
    Friend WithEvents panelServerVersion As System.Windows.Forms.Panel
    Friend WithEvents rbV131 As System.Windows.Forms.RadioButton
    Friend WithEvents rbV125 As System.Windows.Forms.RadioButton
    Friend WithEvents gbHeartBeat As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkHeartBeatUse0xFE As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHeartBeatInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtHeartBeatKillCount As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtHeartBeatStopCount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbV14 As System.Windows.Forms.RadioButton
    Friend WithEvents txtJarFileAugment As System.Windows.Forms.TextBox
    Friend WithEvents btnJarFileAugment As System.Windows.Forms.Button
    Friend WithEvents lblJarFileAugment As System.Windows.Forms.Label
    Friend WithEvents rbV17 As System.Windows.Forms.RadioButton
    Friend WithEvents ttipHelp As System.Windows.Forms.ToolTip
End Class
