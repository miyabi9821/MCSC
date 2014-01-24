<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChatlog
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
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.rtbChatlog = New System.Windows.Forms.RichTextBox()
        Me.tbSay = New System.Windows.Forms.TextBox()
        Me.cbShenter = New System.Windows.Forms.CheckBox()
        Me.rbCo = New System.Windows.Forms.RadioButton()
        Me.rbCh = New System.Windows.Forms.RadioButton()
        Me.rbChCo = New System.Windows.Forms.RadioButton()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Panel_border = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Enabled = False
        Me.CheckBox2.ForeColor = System.Drawing.Color.White
        Me.CheckBox2.Location = New System.Drawing.Point(368, 7)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(77, 16)
        Me.CheckBox2.TabIndex = 19
        Me.CheckBox2.Text = "Show time"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'rtbChatlog
        '
        Me.rtbChatlog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbChatlog.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.rtbChatlog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbChatlog.ForeColor = System.Drawing.Color.White
        Me.rtbChatlog.Location = New System.Drawing.Point(12, 27)
        Me.rtbChatlog.Name = "rtbChatlog"
        Me.rtbChatlog.ReadOnly = True
        Me.rtbChatlog.Size = New System.Drawing.Size(460, 439)
        Me.rtbChatlog.TabIndex = 10
        Me.rtbChatlog.Text = ""
        '
        'tbSay
        '
        Me.tbSay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSay.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.tbSay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbSay.ForeColor = System.Drawing.Color.White
        Me.tbSay.Location = New System.Drawing.Point(12, 475)
        Me.tbSay.Name = "tbSay"
        Me.tbSay.Size = New System.Drawing.Size(460, 19)
        Me.tbSay.TabIndex = 11
        '
        'cbShenter
        '
        Me.cbShenter.AutoSize = True
        Me.cbShenter.Checked = True
        Me.cbShenter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbShenter.Enabled = False
        Me.cbShenter.ForeColor = System.Drawing.Color.White
        Me.cbShenter.Location = New System.Drawing.Point(187, 7)
        Me.cbShenter.Name = "cbShenter"
        Me.cbShenter.Size = New System.Drawing.Size(106, 16)
        Me.cbShenter.TabIndex = 15
        Me.cbShenter.Text = "Show Login/Out"
        Me.cbShenter.UseVisualStyleBackColor = True
        '
        'rbCo
        '
        Me.rbCo.AutoSize = True
        Me.rbCo.Enabled = False
        Me.rbCo.ForeColor = System.Drawing.Color.White
        Me.rbCo.Location = New System.Drawing.Point(137, 7)
        Me.rbCo.Name = "rbCo"
        Me.rbCo.Size = New System.Drawing.Size(52, 16)
        Me.rbCo.TabIndex = 14
        Me.rbCo.TabStop = True
        Me.rbCo.Text = "Cmds"
        Me.rbCo.UseVisualStyleBackColor = True
        '
        'rbCh
        '
        Me.rbCh.AutoSize = True
        Me.rbCh.Enabled = False
        Me.rbCh.ForeColor = System.Drawing.Color.White
        Me.rbCh.Location = New System.Drawing.Point(89, 7)
        Me.rbCh.Name = "rbCh"
        Me.rbCh.Size = New System.Drawing.Size(47, 16)
        Me.rbCh.TabIndex = 13
        Me.rbCh.TabStop = True
        Me.rbCh.Text = "Chat"
        Me.rbCh.UseVisualStyleBackColor = True
        '
        'rbChCo
        '
        Me.rbChCo.AutoSize = True
        Me.rbChCo.Checked = True
        Me.rbChCo.ForeColor = System.Drawing.Color.White
        Me.rbChCo.Location = New System.Drawing.Point(12, 7)
        Me.rbChCo.Name = "rbChCo"
        Me.rbChCo.Size = New System.Drawing.Size(78, 16)
        Me.rbChCo.TabIndex = 12
        Me.rbChCo.TabStop = True
        Me.rbChCo.Text = "Chat,Cmds"
        Me.rbChCo.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.ForeColor = System.Drawing.Color.White
        Me.CheckBox1.Location = New System.Drawing.Point(292, 7)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(77, 16)
        Me.CheckBox1.TabIndex = 18
        Me.CheckBox1.Text = "Show date"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Panel_border
        '
        Me.Panel_border.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_border.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Panel_border.Location = New System.Drawing.Point(10, 25)
        Me.Panel_border.Name = "Panel_border"
        Me.Panel_border.Size = New System.Drawing.Size(464, 443)
        Me.Panel_border.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(10, 473)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 23)
        Me.Panel1.TabIndex = 17
        '
        'frmChatlog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(484, 502)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.rtbChatlog)
        Me.Controls.Add(Me.Panel_border)
        Me.Controls.Add(Me.tbSay)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cbShenter)
        Me.Controls.Add(Me.rbCo)
        Me.Controls.Add(Me.rbCh)
        Me.Controls.Add(Me.rbChCo)
        Me.Controls.Add(Me.CheckBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmChatlog"
        Me.Text = "チャットログ"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents rtbChatlog As System.Windows.Forms.RichTextBox
    Friend WithEvents tbSay As System.Windows.Forms.TextBox
    Friend WithEvents cbShenter As System.Windows.Forms.CheckBox
    Friend WithEvents rbCo As System.Windows.Forms.RadioButton
    Friend WithEvents rbCh As System.Windows.Forms.RadioButton
    Friend WithEvents rbChCo As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Panel_border As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
