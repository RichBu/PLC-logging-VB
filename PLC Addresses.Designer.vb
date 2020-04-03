<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PLC_Addresses
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
        Me.NumberOfDevicesUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Octet1NumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Octet2NumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Octet4NumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Octet3NumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.PLCToChangeNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.NumberOfDevicesUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Octet1NumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Octet2NumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Octet4NumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Octet3NumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PLCToChangeNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumberOfDevicesUpDown
        '
        Me.NumberOfDevicesUpDown.Location = New System.Drawing.Point(9, 41)
        Me.NumberOfDevicesUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumberOfDevicesUpDown.Name = "NumberOfDevicesUpDown"
        Me.NumberOfDevicesUpDown.Size = New System.Drawing.Size(59, 20)
        Me.NumberOfDevicesUpDown.TabIndex = 0
        Me.NumberOfDevicesUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(77, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(186, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Number of PLC's to communicate with"
        '
        'Octet1NumericUpDown
        '
        Me.Octet1NumericUpDown.Location = New System.Drawing.Point(9, 133)
        Me.Octet1NumericUpDown.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Octet1NumericUpDown.Name = "Octet1NumericUpDown"
        Me.Octet1NumericUpDown.Size = New System.Drawing.Size(59, 20)
        Me.Octet1NumericUpDown.TabIndex = 2
        '
        'Octet2NumericUpDown
        '
        Me.Octet2NumericUpDown.Location = New System.Drawing.Point(74, 133)
        Me.Octet2NumericUpDown.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Octet2NumericUpDown.Name = "Octet2NumericUpDown"
        Me.Octet2NumericUpDown.Size = New System.Drawing.Size(59, 20)
        Me.Octet2NumericUpDown.TabIndex = 3
        '
        'Octet4NumericUpDown
        '
        Me.Octet4NumericUpDown.Location = New System.Drawing.Point(204, 133)
        Me.Octet4NumericUpDown.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Octet4NumericUpDown.Name = "Octet4NumericUpDown"
        Me.Octet4NumericUpDown.Size = New System.Drawing.Size(59, 20)
        Me.Octet4NumericUpDown.TabIndex = 5
        '
        'Octet3NumericUpDown
        '
        Me.Octet3NumericUpDown.Location = New System.Drawing.Point(139, 133)
        Me.Octet3NumericUpDown.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.Octet3NumericUpDown.Name = "Octet3NumericUpDown"
        Me.Octet3NumericUpDown.Size = New System.Drawing.Size(59, 20)
        Me.Octet3NumericUpDown.TabIndex = 4
        '
        'PLCToChangeNumericUpDown
        '
        Me.PLCToChangeNumericUpDown.Location = New System.Drawing.Point(9, 86)
        Me.PLCToChangeNumericUpDown.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.PLCToChangeNumericUpDown.Name = "PLCToChangeNumericUpDown"
        Me.PLCToChangeNumericUpDown.Size = New System.Drawing.Size(59, 20)
        Me.PLCToChangeNumericUpDown.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(74, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "PLC to change - 0 based"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "IP Address"
        '
        'PLC_Addresses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PLCToChangeNumericUpDown)
        Me.Controls.Add(Me.Octet4NumericUpDown)
        Me.Controls.Add(Me.Octet3NumericUpDown)
        Me.Controls.Add(Me.Octet2NumericUpDown)
        Me.Controls.Add(Me.Octet1NumericUpDown)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NumberOfDevicesUpDown)
        Me.Name = "PLC_Addresses"
        Me.Text = "PLC_Addresses"
        CType(Me.NumberOfDevicesUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Octet1NumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Octet2NumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Octet4NumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Octet3NumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PLCToChangeNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NumberOfDevicesUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Octet1NumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Octet2NumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Octet4NumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Octet3NumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents PLCToChangeNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
