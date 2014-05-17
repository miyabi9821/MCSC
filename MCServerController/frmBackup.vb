' バックアップ設定画面
' 2013/07/02 サーバファイルが指定されてない状態で各種ダイアログ表示時にエラーが出たため、全体的にエラーチェック強化

Public Class frmBackup

    Dim blnValid As Boolean = True

    Private Sub txtInterval_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInterval.KeyPress
        '数値(とBackSpace)以外の入力は受け付けない
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtInterval_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInterval.Leave
        checkInterval(txtInterval.Name)
    End Sub

    Private Sub chkBackupEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBackupEnabled.CheckedChanged
        checkInterval(chkBackupEnabled.Name)
    End Sub

    Private Sub checkInterval(ByVal called As String)
        Try
            If chkBackupEnabled.Checked = False And called = chkBackupEnabled.Name Then
                epInput.Clear()
                blnValid = True
                Exit Sub
            End If

            If txtInterval.Text = "" And chkBackupEnabled.Checked = True Then
                epInput.SetError(Me.txtInterval, "定期バックアップを有効にする場合は指定して下さい")
                blnValid = False
                Exit Sub
            End If

            If CInt(txtInterval.Text) < 10 Then
                epInput.SetError(Me.txtInterval, "10分以下には設定できません")
                blnValid = False
                Exit Sub
            ElseIf CInt(txtInterval.Text) > 2147484 Then
                epInput.SetError(Me.txtInterval, "2147484分以上には設定できません")
                blnValid = False
                Exit Sub
            End If
            epInput.Clear()
            blnValid = True
        Catch ex As System.OverflowException
            epInput.SetError(Me.txtInterval, "2147484分以上には設定できません")
            blnValid = False
        Catch ex As Exception
            epInput.SetError(Me.txtInterval, "不正な文字が入力されました")
            blnValid = False
            txtInterval.Text = ""
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If txtBackupTarget.Text = "" Then
                Exit Sub
            End If

            If System.IO.File.Exists(txtBackupTarget.Text) = False And _
                    System.IO.Directory.Exists(txtBackupTarget.Text) = False Then
                If MessageBox.Show("指定されたファイル／フォルダは存在しません。追加しますか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            clbBackupTarget.Items.Add(txtBackupTarget.Text, True)
            txtBackupTarget.Text = ""
            txtBackupTarget.Focus()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "例外エラー")
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        clbBackupTarget.Items.Remove(clbBackupTarget.SelectedItem)
    End Sub

    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        txtBackupTarget.Text = clbBackupTarget.SelectedItem.ToString
        txtBackupTarget.Focus()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            If blnValid = False Then
                MessageBox.Show("入力に誤りがあります", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If

            '設定を保存する
            Settings.Instance.BackupEnabled = chkBackupEnabled.Enabled
            Settings.Instance.BackupInterval = txtInterval.Text
            Settings.Instance.BackupKeepDays = txtKeepDays.Text
            Settings.Instance.BackupOutput = txtBackupOutput.Text
            Settings.Instance.CompressEnabled = chkCompressEnabled.Checked
            Settings.Instance.BackupBeforeServerRun = chkBackupBeforeRun.Checked    'サーバ起動前バックアップを行うか

            Dim dtTarget As New DataTable("BackupTarget") '対象ファイル／ディレクトリの保存
            dtTarget.Columns.Add("Checked")
            dtTarget.Columns.Add("Path")
            For i As Integer = 0 To clbBackupTarget.Items.Count - 1
                Dim newRow As DataRow = dtTarget.NewRow
                newRow("Checked") = clbBackupTarget.GetItemChecked(i).ToString
                newRow("Path") = clbBackupTarget.Items(i).ToString
                dtTarget.Rows.Add(newRow)
            Next
            Settings.Instance.BackupTarget = dtTarget

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("以下の理由により設定の保存は失敗しました：" & vbCrLf & Err.Description, "例外エラー")
            Me.DialogResult = Windows.Forms.DialogResult.Abort
            Me.Close()

        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmBackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkBackupEnabled.Checked = Settings.Instance.BackupEnabled              '有効状態読込
        txtInterval.Text = Settings.Instance.BackupInterval                     '実行インターバル読込
        txtKeepDays.Text = Settings.Instance.BackupKeepDays                     'ログ保持日数読込
        txtBackupOutput.Text = Settings.Instance.BackupOutput                   '出力フォルダ読込
        chkCompressEnabled.Checked = Settings.Instance.CompressEnabled          'フォルダ圧縮オプション
        chkBackupBeforeRun.Checked = Settings.Instance.BackupBeforeServerRun    'サーバ起動前バックアップを行うか

        '対象ファイル／フォルダ読込
        clbBackupTarget.Items.Clear()
        Dim dtTarget As DataTable = Settings.Instance.BackupTarget
        Dim blnChecked As Boolean = False
        For i As Integer = 0 To dtTarget.Rows.Count - 1
            Try
                blnChecked = CBool(dtTarget.Rows(i)("Checked"))
            Catch ex As Exception
                blnChecked = False
            End Try
            'リスト行追加
            clbBackupTarget.Items.Add(dtTarget.Rows(i)("Path"), blnChecked)

        Next

        'インターバルが空白の時はとりあえず1時間に指定する
        If txtInterval.Text = "" Then
            txtInterval.Text = 60
        End If

        'バリデーション
        checkInterval(txtInterval.Name)
    End Sub

    'ファイルのD&Dを許容する
    '参考）http://blog.livedoor.jp/akf0/archives/51252181.html
    Private Sub clbBackupTarget_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles clbBackupTarget.DragEnter
        'ドラッグしているファイル／フォルダの取得
        Dim FilePath() As String = _
         CType(e.Data.GetData(DataFormats.FileDrop), String())

        For idx As Integer = 0 To FilePath.Length - 1
            If (Not System.IO.File.Exists(FilePath(idx))) And (Not System.IO.Directory.Exists(FilePath(idx))) Then
                Return
            End If
        Next idx

        'ドロップ可能な場合は、エフェクトを変える
        e.Effect = DragDropEffects.Copy

    End Sub


    'D&Dで追加する
    Private Sub clbBackupTarget_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles clbBackupTarget.DragDrop
        'ドラッグしているファイル／フォルダの取得
        Dim FilePath() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        'D&Dされた分リストに追加する
        For idx As Integer = 0 To FilePath.Length - 1
            clbBackupTarget.Items.Add(FilePath(idx), True)
        Next


    End Sub

    '参照ボタンから、バックアップ出力先選択ダイアログを開く
    '2013/07/02 初期フォルダの位置を変更（指定済みなら指定済みの場所、指定が無ければ起動パス）
    Private Sub btnBrowsOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsOutput.Click
        Try
            If System.IO.Directory.Exists(txtBackupOutput.Text) = True Then
                fbdTarget.SelectedPath = txtBackupOutput.Text
            Else
                fbdTarget.SelectedPath = Application.StartupPath
            End If
            fbdTarget.ShowNewFolderButton = True '新しいフォルダ作成ボタンを表示
            If fbdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupOutput.Text = fbdTarget.SelectedPath

        Catch ex As Exception
            MessageBox.Show(Err.Description, "例外エラー")
        End Try
    End Sub

    'Target Fileボタンから、バックアップ対象ファイル選択ダイアログを開く
    '2013/07/02 サーバ設定でサーバファイルが正しく指定されてない場合にハンドルされてない例外エラーが発生する不具合に対応
    Private Sub btnBrowsTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsTargetFile.Click
        Try
            If System.IO.File.Exists(Settings.Instance.JarPath) = True Then
                ofdTarget.InitialDirectory = System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)
            Else
                ofdTarget.InitialDirectory = Application.StartupPath
            End If


            ofdTarget.Filter = "全てのファイル(*.*)|*.*"
            If ofdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupTarget.Text = ofdTarget.FileName
        Catch ex As Exception
            MessageBox.Show(Err.Description, "例外エラー")
        End Try

    End Sub


    'Target Dirボタンから、バックアップ対象フォルダ選択ダイアログを開く
    '2013/07/02 サーバ設定でサーバファイルが正しく指定されてない場合にハンドルされてない例外エラーが発生する不具合に対応
    Private Sub btnBrowsTargetDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsTargetDir.Click
        Try
            If System.IO.File.Exists(Settings.Instance.JarPath) = True Then
                fbdTarget.SelectedPath = System.IO.Path.GetDirectoryName(Settings.Instance.JarPath)
            Else
                fbdTarget.SelectedPath = Application.StartupPath
            End If

            fbdTarget.ShowNewFolderButton = False '新しいフォルダ作成ボタンを非表示
            If fbdTarget.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            txtBackupTarget.Text = fbdTarget.SelectedPath
        Catch ex As Exception
            MessageBox.Show(Err.Description, "例外エラー")
        End Try
    End Sub

    Private Sub txtKeepDays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKeepDays.KeyPress
        '数値(とBackSpace)以外の入力は受け付けない
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
End Class