Imports Newtonsoft.Json
Imports System.Net
Imports System.Text

Public Class PLClogVB
    Dim ReadPLCnum As Integer = 0           'to replace PLCTargetNumericUpDown.Value
    Const NumMach = 9
    Dim MachStatCode(NumMach) As Int16      'all the machine codes
    Dim OldMachStatCode(NumMach) As Int16   'the old readings to compare to
    Dim MachStatChanged As Boolean = True   'global to signal there has been a change
    Dim ClicksToForceOutput As Integer = 10 'if there have not been changes, if > than this it will still transmit
    Dim ClicksCurrSame As Integer = 0       'current number of clicks that were the same
    Dim urlToPost As String = ""

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
        tbTmrInterval.Text = "5"  '5sec default
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

    Private Sub MachStatClearAll()
        'clear out all the machine statuses
        For iMachNum = 0 To NumMach
            MachStatCode(iMachNum) = 0
            OldMachStatCode(iMachNum) = 0
        Next
        ClicksCurrSame = 0
        MachStatChanged = True
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

        MachStatChanged = False
        For i = 0 To NumMach
            If (MachStatCode(i) <> OldMachStatCode(i)) Then
                MachStatChanged = True
                OldMachStatCode(i) = MachStatCode(i)
            End If
            Dim PictString = "\images\Off.Bmp"
            If MachStatCode(i) = 0 Then PictString = "images\Off.Bmp"
            If MachStatCode(i) = 1 Then PictString = "images\PwrOn.Bmp"
            If MachStatCode(i) = 3 Then PictString = "images\Run.Bmp"

            Select Case i
                Case 0
                    Mach1.Image = Image.FromFile(PictString)
                Case 1
                    Mach2.Image = Image.FromFile(PictString)
                Case 2
                    Mach3.Image = Image.FromFile(PictString)
                Case 3
                    Mach4.Image = Image.FromFile(PictString)
                Case 4
                    Mach5.Image = Image.FromFile(PictString)
                Case 5
                    Mach6.Image = Image.FromFile(PictString)
                Case 6
                    Mach7.Image = Image.FromFile(PictString)
                Case 7
                    Mach8.Image = Image.FromFile(PictString)
                Case 8
                    Mach9.Image = Image.FromFile(PictString)
            End Select
            Debug.Print("#" + Str(i) + " " + Str(MachStatCode(i)))
        Next i
    End Sub

    Private Sub ReadVMemsButton_Click(sender As System.Object, e As System.EventArgs) Handles ReadVMemsButton.Click
        UpdateStatus()
        ' close the connection so screen and others can connect
        'Rc = HEICloseTransport(pTransportStructure)
        'Rc = HEIClose()
        'NetworkOK = False
        postDataInitiate()
        If Me.cbAutoPost.Checked Then postDataInitiate()
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        Dim forcePost As Boolean = False
        If MachStatChanged Then forcePost = True
        UpdateStatus()
        ClicksCurrSame = ClicksCurrSame + 1
        If ((MachStatChanged = True) Or (forcePost) Or (ClicksCurrSame > ClicksToForceOutput)) Then
            If Me.cbAutoPost.Checked Then postDataInitiate()
        End If
    End Sub

    'routines for the posting
    Public Function postDataOperation(ByVal dictData As Dictionary(Of String, Object)) As Boolean
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            'webClient.Headers.Add("apikey", apikey_favoriot)
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(Me.urlToPost, "post", reqString)
            resString = Encoding.Default.GetString(resByte)
            Console.WriteLine("successful")
            Console.WriteLine(resString)
            webClient.Dispose()
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function


    Public Function postDataInitiate()
        'function to post to url, this one happens to be my own
        urlToPost = "https://wco-prodmon-rt.herokuapp.com/api/update-rt-data"

        Dim dictData As New Dictionary(Of String, Object)
        Dim dataStr As String  'data going to JSON;
        Dim outStr As String
        Dim valStr As String
        Dim keyStr As String
        Dim machNum As Integer

        'reset all of the clicks data
        'do not reset the MachStatChanged  flag, let the update change it
        ClicksCurrSame = 0

        dataStr = "{"
        For i = 0 To NumMach
            valStr = "01"
            valStr = MachStatCode(i).ToString("00")
            machNum = i + 1
            outStr = "M" + Strings.Trim(machNum.ToString("0"))
            keyStr = outStr
            outStr = outStr + " : " + valStr
            'Console.WriteLine(outStr)
            dictData.Add(keyStr, valStr)
        Next
        postDataOperation(dictData)
    End Function

    Private Sub bttnPost_Click(sender As Object, e As EventArgs) Handles bttnPost.Click
        postDataInitiate()
    End Sub


    Private Sub tbTmrInterval_TextChanged(sender As Object, e As EventArgs) Handles tbTmrInterval.TextChanged
    End Sub


    Private Sub tbTmrInterval_Leave(sender As Object, e As EventArgs) Handles tbTmrInterval.Leave
        Dim tmrVal As Single
        Dim tmrValStr As String

        tmrVal = CSng(tbTmrInterval.Text)
        tmrValStr = tmrVal.ToString("00.0")
        tbTmrInterval.Text = tmrValStr
        tmrUpdate.Interval = tmrVal * 1000.0
    End Sub
End Class
