<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIgnoreLogs
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.datvIgnoringLogs = New System.Windows.Forms.DataGridView()
        Me.clmPattern = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clmRegex = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbRegex = New System.Windows.Forms.CheckBox()
        Me.btnMod = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.btnADD = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.datvIgnoringLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'datvIgnoringLogs
        '
        Me.datvIgnoringLogs.AllowUserToOrderColumns = True
        Me.datvIgnoringLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.datvIgnoringLogs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmPattern, Me.clmRegex})
        Me.datvIgnoringLogs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.datvIgnoringLogs.Location = New System.Drawing.Point(3, 43)
        Me.datvIgnoringLogs.Name = "datvIgnoringLogs"
        Me.datvIgnoringLogs.RowTemplate.Height = 21
        Me.datvIgnoringLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.datvIgnoringLogs.Size = New System.Drawing.Size(640, 348)
        Me.datvIgnoringLogs.TabIndex = 0
        '
        'clmPattern
        '
        Me.clmPattern.HeaderText = "Pattern"
        Me.clmPattern.Name = "clmPattern"
        Me.clmPattern.Width = 400
        '
        'clmRegex
        '
        Me.clmRegex.HeaderText = "Regex"
        Me.clmRegex.Name = "clmRegex"
        Me.clmRegex.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clmRegex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.clmRegex.Width = 45
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.datvIgnoringLogs, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(646, 394)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbRegex)
        Me.Panel1.Controls.Add(Me.btnMod)
        Me.Panel1.Controls.Add(Me.btnDel)
        Me.Panel1.Controls.Add(Me.btnADD)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 34)
        Me.Panel1.TabIndex = 1
        '
        'cbRegex
        '
        Me.cbRegex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRegex.AutoSize = True
        Me.cbRegex.Location = New System.Drawing.Point(389, 10)
        Me.cbRegex.Name = "cbRegex"
        Me.cbRegex.Size = New System.Drawing.Size(56, 16)
        Me.cbRegex.TabIndex = 4
        Me.cbRegex.Text = "Regex"
        Me.cbRegex.UseVisualStyleBackColor = True
        '
        'btnMod
        '
        Me.btnMod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMod.Location = New System.Drawing.Point(558, 6)
        Me.btnMod.Name = "btnMod"
        Me.btnMod.Size = New System.Drawing.Size(75, 23)
        Me.btnMod.TabIndex = 3
        Me.btnMod.Text = "Modify"
        Me.btnMod.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.Location = New System.Drawing.Point(477, 6)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(75, 23)
        Me.btnDel.TabIndex = 2
        Me.btnDel.Text = "Delete"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnADD
        '
        Me.btnADD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnADD.Location = New System.Drawing.Point(308, 6)
        Me.btnADD.Name = "btnADD"
        Me.btnADD.Size = New System.Drawing.Size(75, 23)
        Me.btnADD.TabIndex = 1
        Me.btnADD.Text = "add"
        Me.btnADD.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(9, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(293, 19)
        Me.TextBox1.TabIndex = 0
        '
        'frmIgnoreLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 394)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmIgnoreLogs"
        Me.Text = "IgnoreLogs"
        CType(Me.datvIgnoringLogs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents datvIgnoringLogs As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbRegex As System.Windows.Forms.CheckBox
    Friend WithEvents btnMod As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnADD As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents clmPattern As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmRegex As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
