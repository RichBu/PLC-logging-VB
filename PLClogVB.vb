Public Class PLClogVB
    Dim ReadPLCnum As Integer = 0   'to replace PLCTargetNumericUpDown.Value
    Const NumMach = 9
    Dim MachStatCode(NumMach) As Int16 'all the machine codes

    'runs when program opens
    Private Sub DL06Windows_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim i As Int16


        'initializes the PLC network connections
        InitializeNetwork()

        ReadVMemsLabel.Text = ""
        lblReadStatus.Visible = False

        'initialize state of PLC's to disconnected
        For i = 0 To MAXDEVICES - 1
            DeviceConnected(i) = False
        Next

        'initialize plc 0 to IP address of 192.168.1.101
        DeviceAddress(0, 0) = 10
        DeviceAddress(0, 1) = 10
        DeviceAddress(0, 2) = 10
        DeviceAddress(0, 3) = 22
        'initialize the number of devices to 1
        NumberOfDevices = 1

        MachStatClearAll()
        tmrUpdate.Enabled = True
    End Sub

    'runs when program closes
    Private Sub DL06Windows_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Save()
        tmrUpdate.Enabled = False

        Rc = HEICloseTransport(pTransportStructure)
        Rc = HEIClose()
    End Sub

    'attempts to connect (or reconnect) with PLC at IP address 192.168.1.101, named 'talk to me'
    'read or write subroutines that fail will note the target PLC as disconnected
    Private Sub ConnectDevices()
        'for/next from 0 to NumberofDevices - 1, attempting to connect each device
        Dim i As Int16
        For i = 0 To NumberOfDevices - 1
            'attempt to connect to device i, show error message if unable
            If DeviceConnected(i) = False Then 'test to see if device is already connected
                If ConnectDevice(i) = False Then 'call ConnectDevice(0), which returns true if the PLC connects or false if it doesn't
                    'note device 0 as disconnected
                    DeviceConnected(i) = False
                Else
                    'note device 1 as connected
                    DeviceConnected(i) = True
                End If
            End If
        Next
    End Sub

    'button handlers

    'Connect button allows user to try to reconnect to a disconnected PLC
    Private Sub ConnectButton_Click(sender As System.Object, e As System.EventArgs) Handles ConnectButton.Click
        'ConnectDevices() attempts to establish a connection with all disconnected devices
        'This can potentially cause the program to temporarily stall for large numbers of devices
        ConnectDevices()


        'update labels
        If DeviceConnected(ReadPLCnum) = True Then  'PLCTargetNumericUpDown.Value
            ConnectionStatusLabel.Text = "connected"
        End If
    End Sub

    'menu strip handlers
    Private Sub ContactInfoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ContactInfoToolStripMenuItem.Click
        ContactInfo.Show()
    End Sub

    Private Sub AddressListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddressListToolStripMenuItem.Click
        PLC_Addresses.Show()
    End Sub

    Private Sub PLCTargetNumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs)
        If DeviceConnected(ReadPLCnum) Then
            ConnectionStatusLabel.Text = "connected"
        Else
            ConnectionStatusLabel.Text = "disconnected"
        End If
    End Sub

    Private Sub MachStatClearAll()
        'clear out all the machine statuses
        For iMachNum = 0 To NumMach
            MachStatCode(iMachNum) = 0
        Next
    End Sub

    Private Sub UpdateStatus()
        lblReadStatus.Visible = True
        Me.Refresh()
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Application.DoEvents()

        ConnectDevices()

        'update labels
        If DeviceConnected(ReadPLCnum) = True Then
            ConnectionStatusLabel.Text = "connected"
        End If

        MachStatClearAll()
        ReadVMemsLabel.Text = ReadStatus("", 1, ReadPLCnum, MachStatCode)
        lblReadStatus.Visible = False
        Me.Refresh()
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Application.DoEvents()

        For i = 0 To NumMach
            Debug.Print("#" + Str(i) + " " + Str(MachStatCode(i)))
        Next i
    End Sub

    Private Sub ReadVMemsButton_Click(sender As System.Object, e As System.EventArgs) Handles ReadVMemsButton.Click
        UpdateStatus()
        ' close the connection so screen and others can connect
        'Rc = HEICloseTransport(pTransportStructure)
        'Rc = HEIClose()
        'NetworkOK = False
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        UpdateStatus()
    End Sub
End Class
