﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PLClogVB
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddressListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContactInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectButton = New System.Windows.Forms.Button()
        Me.ConnectionStatusLabel = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ReadVMemsLabel = New System.Windows.Forms.Label()
        Me.ReadVMemsButton = New System.Windows.Forms.Button()
        Me.lblReadStatus = New System.Windows.Forms.Label()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.Mach1 = New System.Windows.Forms.PictureBox()
        Me.Mach2 = New System.Windows.Forms.PictureBox()
        Me.Mach3 = New System.Windows.Forms.PictureBox()
        Me.Mach4 = New System.Windows.Forms.PictureBox()
        Me.Mach5 = New System.Windows.Forms.PictureBox()
        Me.Mach6 = New System.Windows.Forms.PictureBox()
        Me.Mach7 = New System.Windows.Forms.PictureBox()
        Me.Mach8 = New System.Windows.Forms.PictureBox()
        Me.Mach9 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.bttnPost = New System.Windows.Forms.Button()
        Me.tbTmrInterval = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbAutoPost = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbTmrPost = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tmrPost = New System.Windows.Forms.Timer(Me.components)
        Me.lblWarn_change = New System.Windows.Forms.Label()
        Me.lblWarn_Post = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Mach1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Mach9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(634, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddressListToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'AddressListToolStripMenuItem
        '
        Me.AddressListToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark
        Me.AddressListToolStripMenuItem.Name = "AddressListToolStripMenuItem"
        Me.AddressListToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AddressListToolStripMenuItem.Text = "PLC List"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContactInfoToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ContactInfoToolStripMenuItem
        '
        Me.ContactInfoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ContactInfoToolStripMenuItem.Name = "ContactInfoToolStripMenuItem"
        Me.ContactInfoToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ContactInfoToolStripMenuItem.Text = "Contact Info"
        '
        'ConnectButton
        '
        Me.ConnectButton.Location = New System.Drawing.Point(12, 30)
        Me.ConnectButton.Name = "ConnectButton"
        Me.ConnectButton.Size = New System.Drawing.Size(95, 23)
        Me.ConnectButton.TabIndex = 1
        Me.ConnectButton.Text = "Connect PLC's"
        Me.ConnectButton.UseVisualStyleBackColor = True
        '
        'ConnectionStatusLabel
        '
        Me.ConnectionStatusLabel.AutoSize = True
        Me.ConnectionStatusLabel.Location = New System.Drawing.Point(149, 35)
        Me.ConnectionStatusLabel.Name = "ConnectionStatusLabel"
        Me.ConnectionStatusLabel.Size = New System.Drawing.Size(71, 13)
        Me.ConnectionStatusLabel.TabIndex = 19
        Me.ConnectionStatusLabel.Text = "disconnected"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(12, 88)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(84, 13)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Returned Value:"
        '
        'ReadVMemsLabel
        '
        Me.ReadVMemsLabel.AutoSize = True
        Me.ReadVMemsLabel.Location = New System.Drawing.Point(102, 88)
        Me.ReadVMemsLabel.Name = "ReadVMemsLabel"
        Me.ReadVMemsLabel.Size = New System.Drawing.Size(31, 13)
        Me.ReadVMemsLabel.TabIndex = 39
        Me.ReadVMemsLabel.Text = "9999"
        '
        'ReadVMemsButton
        '
        Me.ReadVMemsButton.Location = New System.Drawing.Point(13, 11)
        Me.ReadVMemsButton.Name = "ReadVMemsButton"
        Me.ReadVMemsButton.Size = New System.Drawing.Size(75, 23)
        Me.ReadVMemsButton.TabIndex = 38
        Me.ReadVMemsButton.Text = "Update"
        Me.ReadVMemsButton.UseVisualStyleBackColor = True
        '
        'lblReadStatus
        '
        Me.lblReadStatus.AutoSize = True
        Me.lblReadStatus.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblReadStatus.Location = New System.Drawing.Point(108, 16)
        Me.lblReadStatus.Name = "lblReadStatus"
        Me.lblReadStatus.Size = New System.Drawing.Size(72, 13)
        Me.lblReadStatus.TabIndex = 42
        Me.lblReadStatus.Text = "reading PLC's"
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 5000
        '
        'Mach1
        '
        Me.Mach1.Location = New System.Drawing.Point(19, 225)
        Me.Mach1.Name = "Mach1"
        Me.Mach1.Size = New System.Drawing.Size(50, 50)
        Me.Mach1.TabIndex = 43
        Me.Mach1.TabStop = False
        '
        'Mach2
        '
        Me.Mach2.Location = New System.Drawing.Point(115, 225)
        Me.Mach2.Name = "Mach2"
        Me.Mach2.Size = New System.Drawing.Size(50, 50)
        Me.Mach2.TabIndex = 44
        Me.Mach2.TabStop = False
        '
        'Mach3
        '
        Me.Mach3.Location = New System.Drawing.Point(204, 225)
        Me.Mach3.Name = "Mach3"
        Me.Mach3.Size = New System.Drawing.Size(50, 50)
        Me.Mach3.TabIndex = 45
        Me.Mach3.TabStop = False
        '
        'Mach4
        '
        Me.Mach4.Location = New System.Drawing.Point(292, 225)
        Me.Mach4.Name = "Mach4"
        Me.Mach4.Size = New System.Drawing.Size(50, 50)
        Me.Mach4.TabIndex = 46
        Me.Mach4.TabStop = False
        '
        'Mach5
        '
        Me.Mach5.Location = New System.Drawing.Point(19, 313)
        Me.Mach5.Name = "Mach5"
        Me.Mach5.Size = New System.Drawing.Size(50, 50)
        Me.Mach5.TabIndex = 47
        Me.Mach5.TabStop = False
        '
        'Mach6
        '
        Me.Mach6.Location = New System.Drawing.Point(115, 313)
        Me.Mach6.Name = "Mach6"
        Me.Mach6.Size = New System.Drawing.Size(50, 50)
        Me.Mach6.TabIndex = 48
        Me.Mach6.TabStop = False
        '
        'Mach7
        '
        Me.Mach7.Location = New System.Drawing.Point(204, 313)
        Me.Mach7.Name = "Mach7"
        Me.Mach7.Size = New System.Drawing.Size(50, 50)
        Me.Mach7.TabIndex = 49
        Me.Mach7.TabStop = False
        '
        'Mach8
        '
        Me.Mach8.Location = New System.Drawing.Point(292, 313)
        Me.Mach8.Name = "Mach8"
        Me.Mach8.Size = New System.Drawing.Size(50, 50)
        Me.Mach8.TabIndex = 50
        Me.Mach8.TabStop = False
        '
        'Mach9
        '
        Me.Mach9.Location = New System.Drawing.Point(19, 398)
        Me.Mach9.Name = "Mach9"
        Me.Mach9.Size = New System.Drawing.Size(50, 50)
        Me.Mach9.TabIndex = 51
        Me.Mach9.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 206)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 16)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(133, 206)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 16)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(223, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 16)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "3"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(310, 206)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 16)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(310, 294)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "8"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(223, 294)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 16)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "7"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(133, 294)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 16)
        Me.Label7.TabIndex = 57
        Me.Label7.Text = "6"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(37, 294)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(16, 16)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "5"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(37, 379)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(16, 16)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "9"
        '
        'bttnPost
        '
        Me.bttnPost.Location = New System.Drawing.Point(6, 11)
        Me.bttnPost.Name = "bttnPost"
        Me.bttnPost.Size = New System.Drawing.Size(75, 23)
        Me.bttnPost.TabIndex = 61
        Me.bttnPost.Text = "Post"
        Me.bttnPost.UseVisualStyleBackColor = True
        '
        'tbTmrInterval
        '
        Me.tbTmrInterval.Location = New System.Drawing.Point(13, 44)
        Me.tbTmrInterval.Name = "tbTmrInterval"
        Me.tbTmrInterval.Size = New System.Drawing.Size(74, 20)
        Me.tbTmrInterval.TabIndex = 62
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(93, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 13)
        Me.Label10.TabIndex = 63
        Me.Label10.Text = "Update Time (secs)"
        '
        'cbAutoPost
        '
        Me.cbAutoPost.AutoSize = True
        Me.cbAutoPost.Location = New System.Drawing.Point(9, 47)
        Me.cbAutoPost.Name = "cbAutoPost"
        Me.cbAutoPost.Size = New System.Drawing.Size(72, 17)
        Me.cbAutoPost.TabIndex = 64
        Me.cbAutoPost.Text = "Auto Post"
        Me.cbAutoPost.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.tbTmrPost)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.tbTmrInterval)
        Me.GroupBox1.Controls.Add(Me.lblReadStatus)
        Me.GroupBox1.Controls.Add(Me.ReadVMemsButton)
        Me.GroupBox1.Location = New System.Drawing.Point(187, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(212, 106)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(93, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 13)
        Me.Label11.TabIndex = 68
        Me.Label11.Text = "Post Time (secs)"
        '
        'tbTmrPost
        '
        Me.tbTmrPost.Location = New System.Drawing.Point(13, 71)
        Me.tbTmrPost.Name = "tbTmrPost"
        Me.tbTmrPost.Size = New System.Drawing.Size(74, 20)
        Me.tbTmrPost.TabIndex = 67
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbAutoPost)
        Me.GroupBox2.Controls.Add(Me.bttnPost)
        Me.GroupBox2.Location = New System.Drawing.Point(427, 78)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(111, 73)
        Me.GroupBox2.TabIndex = 66
        Me.GroupBox2.TabStop = False
        '
        'tmrPost
        '
        Me.tmrPost.Interval = 20000
        '
        'lblWarn_change
        '
        Me.lblWarn_change.AutoSize = True
        Me.lblWarn_change.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarn_change.ForeColor = System.Drawing.Color.Red
        Me.lblWarn_change.Location = New System.Drawing.Point(411, 186)
        Me.lblWarn_change.Name = "lblWarn_change"
        Me.lblWarn_change.Size = New System.Drawing.Size(155, 16)
        Me.lblWarn_change.TabIndex = 67
        Me.lblWarn_change.Text = "Status Change Found"
        '
        'lblWarn_Post
        '
        Me.lblWarn_Post.AutoSize = True
        Me.lblWarn_Post.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarn_Post.ForeColor = System.Drawing.Color.Red
        Me.lblWarn_Post.Location = New System.Drawing.Point(411, 215)
        Me.lblWarn_Post.Name = "lblWarn_Post"
        Me.lblWarn_Post.Size = New System.Drawing.Size(91, 16)
        Me.lblWarn_Post.TabIndex = 68
        Me.lblWarn_Post.Text = "Post started"
        '
        'PLClogVB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 562)
        Me.Controls.Add(Me.lblWarn_Post)
        Me.Controls.Add(Me.lblWarn_change)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Mach9)
        Me.Controls.Add(Me.Mach8)
        Me.Controls.Add(Me.Mach7)
        Me.Controls.Add(Me.Mach6)
        Me.Controls.Add(Me.Mach5)
        Me.Controls.Add(Me.Mach4)
        Me.Controls.Add(Me.Mach3)
        Me.Controls.Add(Me.Mach2)
        Me.Controls.Add(Me.Mach1)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.ReadVMemsLabel)
        Me.Controls.Add(Me.ConnectionStatusLabel)
        Me.Controls.Add(Me.ConnectButton)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "PLClogVB"
        Me.Text = "PLC log / monitor VB"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Mach1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Mach9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddressListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContactInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectButton As System.Windows.Forms.Button
    Friend WithEvents ConnectionStatusLabel As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ReadVMemsLabel As System.Windows.Forms.Label
    Friend WithEvents ReadVMemsButton As System.Windows.Forms.Button
    Friend WithEvents lblReadStatus As Label
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents Mach1 As PictureBox
    Friend WithEvents Mach2 As PictureBox
    Friend WithEvents Mach3 As PictureBox
    Friend WithEvents Mach4 As PictureBox
    Friend WithEvents Mach5 As PictureBox
    Friend WithEvents Mach6 As PictureBox
    Friend WithEvents Mach7 As PictureBox
    Friend WithEvents Mach8 As PictureBox
    Friend WithEvents Mach9 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents bttnPost As Button
    Friend WithEvents tbTmrInterval As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cbAutoPost As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents tbTmrPost As TextBox
    Friend WithEvents tmrPost As Timer
    Friend WithEvents lblWarn_change As Label
    Friend WithEvents lblWarn_Post As Label
End Class
