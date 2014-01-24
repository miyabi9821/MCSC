Public Class frmPermission
    Private Sub frmPermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '設定を画面に反映
        With PermissionSettings.Instance
            chkPermissionEnabled.Checked = .Enabled
            txtPrefixChar.Text = .PrefixChar
            'tp
            chkTp.Checked = .TpEnabled
            Select Case .TpMode
                Case 0
                    rbTpEveryone.Checked = True
                Case 1
                    rbTpSelected.Checked = True
                Case Else
            End Select
            txtTpSelectedUsers.Text = .TpSelectedUsers
            'give
            chkGive.Checked = .GiveEnabled
            Select Case .GiveMode
                Case 0
                    rbGiveEveryone.Checked = True
                Case 1
                    rbGiveSelected.Checked = True
                Case Else
            End Select
            txtGiveSelectedUsers.Text = .GiveSelectedUsers
            'time
            chkTime.Checked = .TimeEnabled
            Select Case .TimeMode
                Case 0
                    rbTimeEveryone.Checked = True
                Case 1
                    rbTimeSelected.Checked = True
                Case Else
            End Select
            txtTimeSelectedUsers.Text = .TimeSelectedUsers
            'xp
            chkXp.Checked = .XpEnabled
            Select Case .XpMode
                Case 0
                    rbXpEveryone.Checked = True
                Case 1
                    rbXpSelected.Checked = True
                Case Else
            End Select
            txtXpSelectedUsers.Text = .XpSelectedUsers
            'gamemode
            chkGamemode.Checked = .GamemodeEnabled
            Select Case .GamemodeMode
                Case 0
                    rbGamemodeEveryone.Checked = True
                Case 1
                    rbGamemodeSelected.Checked = True
                Case Else
            End Select
            txtGamemodeSelectedUsers.Text = .GamemodeSelectedUsers
            'kick
            chkKick.Checked = .KickEnabled
            Select Case .KickMode
                Case 0
                    rbKickEveryone.Checked = True
                Case 1
                    rbKickSelected.Checked = True
                Case Else
            End Select
            txtKickSelectedUsers.Text = .KickSelectedUsers
            'ban
            chkBan.Checked = .BanEnabled
            Select Case .BanMode
                Case 0
                    rbBanEveryone.Checked = True
                Case 1
                    rbBanSelected.Checked = True
                Case Else
            End Select
            txtBanSelectedUsers.Text = .BanSelectedUsers
            'pardon
            chkPardon.Checked = .PardonEnabled
            Select Case .PardonMode
                Case 0
                    rbPardonEveryone.Checked = True
                Case 1
                    rbPardonSelected.Checked = True
                Case Else
            End Select
            txtPardonSelectedUsers.Text = .PardonSelectedUsers
            'whitelist
            chkWhitelist.Checked = .WhitelistEnabled
            Select Case .WhitelistMode
                Case 0
                    rbWhitelistEveryone.Checked = True
                Case 1
                    rbWhitelistSelected.Checked = True
                Case Else
            End Select
            txtWhitelistSelectedUsers.Text = .WhitelistSelectedUsers

            'spawnpoint
            chkSpawnpoint.Checked = .SpawnpointEnabled
            Select Case .SpawnpointMode
                Case 0
                    rbSpawnpointEveryone.Checked = True
                Case 1
                    rbSpawnpointSelected.Checked = True
                Case Else
            End Select
            txtSpawnpointSelectedUsers.Text = .SpawnpointSelectedUsers
            'weather
            chkWeather.Checked = .WeatherEnabled
            Select Case .WeatherMode
                Case 0
                    rbWeatherEveryone.Checked = True
                Case 1
                    rbWeatherSelected.Checked = True
                Case Else
            End Select
            txtWeatherSelectedUsers.Text = .WeatherSelectedUsers

        End With

    End Sub

    Private Sub txtPrefixChar_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrefixChar.Leave
        'Permission機能を有効にしているとき、Prefix Characterを指定してない場合はエラーを出す
        If chkPermissionEnabled.Checked = True And txtPrefixChar.Text.Length = 0 Then
            epInput.SetError(txtPrefixChar, "Prefix Characterを指定して下さい")
        Else
            epInput.SetError(txtPrefixChar, Nothing)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            '画面を設定に反映
            With PermissionSettings.Instance
                .Enabled = chkPermissionEnabled.Checked
                .PrefixChar = txtPrefixChar.Text
                'tp
                .TpEnabled = chkTp.Checked
                If rbTpEveryone.Checked Then
                    .TpMode = 0
                Else
                    .TpMode = 1
                End If
                .TpSelectedUsers = txtTpSelectedUsers.Text
                'give
                .GiveEnabled = chkGive.Checked
                If rbGiveEveryone.Checked Then
                    .GiveMode = 0
                Else
                    .GiveMode = 1
                End If
                .GiveSelectedUsers = txtGiveSelectedUsers.Text
                'time
                .TimeEnabled = chkTime.Checked
                If rbTimeEveryone.Checked Then
                    .TimeMode = 0
                Else
                    .TimeMode = 1
                End If
                .TimeSelectedUsers = txtTimeSelectedUsers.Text
                'xp
                .XpEnabled = chkXp.Checked
                If rbXpEveryone.Checked Then
                    .XpMode = 0
                Else
                    .XpMode = 1
                End If
                .XpSelectedUsers = txtXpSelectedUsers.Text
                'gamemode
                .GamemodeEnabled = chkGamemode.Checked
                If rbGamemodeEveryone.Checked Then
                    .GamemodeMode = 0
                Else
                    .GamemodeMode = 1
                End If
                .GamemodeSelectedUsers = txtGamemodeSelectedUsers.Text
                'kick
                .KickEnabled = chkKick.Checked
                If rbKickEveryone.Checked Then
                    .KickMode = 0
                Else
                    .KickMode = 1
                End If
                .KickSelectedUsers = txtKickSelectedUsers.Text
                'ban
                .BanEnabled = chkBan.Checked
                If rbBanEveryone.Checked Then
                    .BanMode = 0
                Else
                    .BanMode = 1
                End If
                .BanSelectedUsers = txtBanSelectedUsers.Text
                'pardon
                .PardonEnabled = chkPardon.Checked
                If rbPardonEveryone.Checked Then
                    .PardonMode = 0
                Else
                    .PardonMode = 1
                End If
                .PardonSelectedUsers = txtPardonSelectedUsers.Text
                'whitelist
                .WhitelistEnabled = chkWhitelist.Checked
                If rbWhitelistEveryone.Checked Then
                    .WhitelistMode = 0
                Else
                    .WhitelistMode = 1
                End If
                .WhitelistSelectedUsers = txtWhitelistSelectedUsers.Text

                'spawnpoint
                .SpawnpointEnabled = chkSpawnpoint.Checked
                If rbSpawnpointEveryone.Checked Then
                    .SpawnpointMode = 0
                Else
                    .SpawnpointMode = 1
                End If
                .SpawnpointSelectedUsers = txtSpawnpointSelectedUsers.Text
                'weather
                .WeatherEnabled = chkWeather.Checked
                If rbWeatherEveryone.Checked Then
                    .WeatherMode = 0
                Else
                    .WeatherMode = 1
                End If
                .WeatherSelectedUsers = txtWeatherSelectedUsers.Text
            End With
            '設定を保存
            PermissionSettings.SaveToXmlFile()
        Catch ex As Exception
            MessageBox.Show(GSTR_PERMISSION_FILE & " save error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class