<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.MenuStrip1.SuspendLayout()
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
        Me.Label21.Location = New System.Drawing.Point(12, 102)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(84, 13)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Returned Value:"
        '
        'ReadVMemsLabel
        '
        Me.ReadVMemsLabel.AutoSize = True
        Me.ReadVMemsLabel.Location = New System.Drawing.Point(102, 102)
        Me.ReadVMemsLabel.Name = "ReadVMemsLabel"
        Me.ReadVMemsLabel.Size = New System.Drawing.Size(31, 13)
        Me.ReadVMemsLabel.TabIndex = 39
        Me.ReadVMemsLabel.Text = "9999"
        '
        'ReadVMemsButton
        '
        Me.ReadVMemsButton.Location = New System.Drawing.Point(380, 75)
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
        Me.lblReadStatus.Location = New System.Drawing.Point(475, 80)
        Me.lblReadStatus.Name = "lblReadStatus"
        Me.lblReadStatus.Size = New System.Drawing.Size(72, 13)
        Me.lblReadStatus.TabIndex = 42
        Me.lblReadStatus.Text = "reading PLC's"
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Interval = 2000
        '
        'PLClogVB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 562)
        Me.Controls.Add(Me.lblReadStatus)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.ReadVMemsLabel)
        Me.Controls.Add(Me.ReadVMemsButton)
        Me.Controls.Add(Me.ConnectionStatusLabel)
        Me.Controls.Add(Me.ConnectButton)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "PLClogVB"
        Me.Text = "PLC log / monitor VB"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
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
End Class
