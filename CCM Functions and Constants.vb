Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO
Imports System.Collections.Generic

Module CCMFunctions
    ' maximum number of devices you want to allow
    Public Const MAXDEVICES As Int16 = 100

    ' return code from the SDK API calls
    Public Rc As Integer

    ' this is the device the user selected from the list
    Public tDevice As Int16

    ' this is the type of device the user selcted
    Public tDeviceType As String

    ' Ethernet protocol transport
    Public TP As New HEITransport

    'pointer to HEITransport structure
    'this structure will be located in the unmanaged heap
    Public pTransportStructure As IntPtr

    ' true if the network interface can be initialized using the selected protocol
    Public NetworkOK As Boolean

    ' array of Host Ethernet devices
    Public aDevices(MAXDEVICES) As HEIDevice

    ' number of Host Ethernet devices found on the network
    Public DeviceCount As Integer

    ' set to true if any Host Ethernet device is already open
    Public DeviceOpen As Boolean

    ' detail line that gets displayed in the listbox
    'used as variable for read/write return strings
    Public DetailLine As String

    Public ascii As New ASCIIEncoding

    'DirectLogic DL6 Upper and Lower Limits
    'Values are in decimal, not octal

    'Global Inputs
    Public Const GXLowerLimit As Int16 = 0
    Public Const GXUpperLimit As Int16 = 2047
    'Real World Inputs
    Public Const XLowerLimit As Int16 = 0
    Public Const XUpperLimit As Int16 = 511
    'Special Purpose Relays
    Public Const SPLowerLimit As Int16 = 0
    Public Const SPUpperLimit As Int16 = 511
    'Global Outputs
    Public Const GYLowerLimit As Int16 = 0
    Public Const GYUpperLimit As Int16 = 2047
    'Real World Outputs
    Public Const YLowerLimit As Int16 = 0
    Public Const YUpperLimit As Int16 = 511
    'Control Relays
    Public Const CLowerLimit As Int16 = 0
    Public Const CUpperLimit As Int16 = 1023
    'Stage Status Bits
    Public Const SLowerLimit As Int16 = 0
    Public Const SUpperLimit As Int16 = 1023
    'Timer Status Bits
    Public Const TLowerLimit As Int16 = 0
    Public Const TUpperLimit As Int16 = 255
    'Counter Status Bits
    Public Const CTLowerLimit As Int16 = 0
    Public Const CTUpperLimit As Int16 = 127

    'V Memory Range currently Ox0000 - Ox17777
    '"System Status" ranges are from 700-777, 7600-7777, 36000-37777 and should not be used
    'Also, V0 - V377 are Timer Accumulators and V1000 - V1177 are Counter Accumulators
    Public Const VMemLowerLimit As Int16 = 0
    Public Const VMemUpperLimit As Int16 = 8191
    Public Const VMemDisallowed1On As Boolean = True
    Public Const VMemDisallowedLowerLimit1 As Int16 = 448
    Public Const VMemDisallowedUpperLimit1 As Int16 = 511
    Public Const VMemDisallowed2On As Boolean = True
    Public Const VMemDisallowedLowerLimit2 As Int16 = 3968
    Public Const VMemDisallowedUpperLimit2 As Int16 = 4095

    'CCM Range (Hex) offsets
    Public Const GXCCMRange As Int16 = 1
    Public Const XCCMRange As Int16 = 257
    Public Const SPCCMRange As Int16 = 385
    Public Const GYCCMRange As Int16 = 1
    Public Const YCCMRange As Int16 = 257
    Public Const CCCMRange As Int16 = 385
    Public Const SCCMRange As Int16 = 641
    Public Const TCCMRange As Int16 = 769
    Public Const CTCCMRange As Int16 = 801
    Public Const VMemCCMRange As Int16 = 1

    Public Const InvalidAddress As String = "Invalid Address"
    Public Const InvalidDataType As String = "Invalid DataType"

    Public DeviceConnected(MAXDEVICES) As Boolean
    Public NumberOfDevices As Int16
    Public Reconnecting As Boolean

    'IP Address for PLC's
    Public DeviceAddress(MAXDEVICES, 4) As Int16 'ip addresses of all PLC's

    'Optional name for PLC's
    Public DeviceName(MAXDEVICES) As String 'names of all PLC's

    'initializes HEIT network protocols
    Public Sub InitializeNetwork()
        ' if the network interface has already been opened, close it
        '
        If NetworkOK = True Then
            Rc = HEICloseTransport(pTransportStructure)
            Rc = HEIClose()

        Else
            TP.Initialize()

            Dim x As Integer

            For x = 0 To MAXDEVICES - 1
                aDevices(x).Initialize()
            Next
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Initialize the Ethernet Driver
        '
        Rc = HEIOpen(HEIAPIVersion)
        If Rc <> 0 Then
            'DisplayList.Items.Add("Error " & Hex(Rc) & " trying to initialize the Ethernet driver")

        Else
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Initiaizize the Winsock protocol transport
            '
            TP.Transport = HEIT_WINSOCK

            'If UseIP.Checked = True Then
            TP.Protocol = HEIP_IP
            'Else
            'TP.Protocol = HEIP_IPX
            'End If

            ' The HEITransport structure has now been configured with the transport (HEIT_WINSOCK) and protocol (HEIP_IP or HEIP_IPX).
            ' The HEITransport structure must now be moved to the unmanaged heap, which is where the HEI32_3 dll resides.
            ' If the HEITransport structure is left in the managed heap, it will be moved as the .net garbage collector destroys unneeded variables.
            ' The DLL maintains pointers to the HEITransport structure in the aDevices array.  
            ' If the HEITransport structure is left in the managed heap, when the structure is relocated, the DLL pointers will no longer point to 
            ' the structure and the DLL will work improperly.  Note that any pointers maintained by managed code will be automatically readjusted by
            ' the garbage collector as variables are moved.  The garbage collector cannot adjust pointers in unmanaged memory.  
            ' In order to prevent the garbage collector from moving the structure, the structure will be moved to the unmanaged heap.   

            'Creat a memory buffer in the unmanaged heap and obtain a pointer to it.  
            pTransportStructure = Marshal.AllocHGlobal(Marshal.SizeOf(TP))

            'Copy the TP structure to the buffer and destroy the original structure in managed memory
            Marshal.StructureToPtr(TP, pTransportStructure, True)

            Rc = HEIOpenTransport(pTransportStructure, HEIAPIVersion, 0)

            If Rc <> 0 Then
                'DisplayList.Items.Add("Error " & Hex(Rc) & " trying to initialize the Winsock transport")

            Else
                'If UseIP.Checked = True Then
                'DisplayList.Items.Add("Initialized Ok, using IP protocol")
                'Else
                'DisplayList.Items.Add("Initialized Ok, using IPX protocol")
                'End If

                NetworkOK = True
            End If
        End If
    End Sub

    'closes connection to a device
    Public Sub Disconnect()
        ' Close the open device
        '
        Rc = HEICloseDevice(aDevices(tDevice))
        If Rc <> 0 Then
            'DisplayList.Items.Add("Error " & Hex(Rc) & " trying to close device" & Str(tDevice))

        Else
            ' reset the screen
            '
            DeviceOpen = False

            'NetworkInit.Enabled = True
            'ScanNetwork.Enabled = True
            'Connect.Enabled = False
            'Disconnect.Enabled = False


            NetworkOK = False
            'UseIP.Enabled = True
            NetworkOK = False
            'UseIPX.Enabled = True
            'ProtocolFrame.Enabled = True

            'DisplayDevicesFrame.Enabled = True
            'DisplayOnlyECOMs.Enabled = True
            'DisplayAllDevices.Enabled = True

            'DisableOnDisconnect()

            tDeviceType = ""
            tDevice = 0

            'DisplayList.Items.Clear()
            'DisplayList.Items.Add("Disconnected")

        End If

    End Sub

    'parts of initial HEIT program
    Public Function BuildTypeString(ByRef Device As HEIDevice, ByRef DDError As Boolean) As String

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' First
        ' Read the Device Definition Data
        '
        Dim DDBuffer As New DeviceDef
        DDBuffer.Initialize()

        Dim DDSize As Integer
        Dim t As String = ""
        Dim eRc As Integer


        DDSize = 42

        eRc = HEIReadDeviceDef(Device, DDBuffer.device(0), DDSize)
        If eRc <> 0 Then
            DDError = True

        Else
            ' device byte 0 determines the Module Family
            '
            ' device byte 2 determines the Module Type, MSB determines media type
            '
            Select Case DDBuffer.device(2)
                Case MT_EBC
                    Select Case DDBuffer.device(0)
                        Case MF_005
                            t = "H0-"
                        Case MF_205
                            t = "H2-"
                        Case MF_305
                            t = "H3-"
                        Case MF_405
                            t = "H4-"
                        Case MF_TERM
                            t = "T1H-"
                    End Select

                    If (DDBuffer.device(2) And &H80) = &H80 Then
                        t = t + "EBC-F"
                    Else
                        t = t + "EBC"
                    End If

                Case MT_ECOM
                    Select Case DDBuffer.device(0)
                        Case MF_005
                            t = "H0-"
                        Case MF_205
                            t = "H2-"
                        Case MF_305
                            t = "H3-"
                        Case MF_405
                            t = "H4-"
                    End Select

                    If (DDBuffer.device(2) And &H81) = &H81 Then
                        t = t + "ECOM-F"
                    Else
                        t = t + "ECOM"
                    End If

                Case MT_WPLC
                    Select Case DDBuffer.device(0)
                        Case MF_005
                            t = "H0-"
                        Case MF_205
                            t = "H2-"
                        Case MF_305
                            t = "H3-"
                        Case MF_405
                            t = "H4-"
                    End Select

                    t = t + "WPLC"

                Case MT_DRIVE
                    Select Case DDBuffer.device(0)
                        Case MF_100_SERIES
                            t = "HA-EDRV2"
                        Case MF_J300
                            t = "HA-EDRV3"
                        Case MF_300_Series
                            t = "HA-EDRV"
                        Case MF_GS
                            t = "GS-EDRV"
                    End Select

                Case MT_ERMA
                    Select Case DDBuffer.device(0)
                        Case MF_005
                            t = "H0-"
                        Case MF_205
                            t = "H2-"
                        Case MF_305
                            t = "H3-"
                        Case MF_405
                            t = "H4-"
                    End Select

                    If (DDBuffer.device(2) And &H84) = &H84 Then
                        t = t + "ERM-F"
                    Else
                        t = t + "ERM"
                    End If

                Case MT_CTRIO
                    t = t + "CTRIO"

                Case MT_AVG_DISP
                    t = t + "EZTOUCH"

                Case MT_PBC
                    t = t + "PBC"

                Case MT_PBCC
                    t = t + "PBCC"

                Case MT_UNK
                    t = t + "UNKNOWN"
            End Select

        End If

        If DDError = True Then
            BuildTypeString = " ??????????? "
        Else
            BuildTypeString = t
        End If

    End Function
    Public Function GetMACFromBuffer(ByRef Buffer() As Byte) As String

        Dim y As Int16, t As String
        t = ""

        For y = 30 To 35
            If Buffer(y) = 0 Then
                t = t + "00 "
            Else
                'make sure each piece of the address has two digits by adding a leading 0 to
                'single digit values
                If Buffer(y) < 16 Then
                    t = t + "0" + Hex(Buffer(y)) + " "
                Else
                    t = t + Hex(Buffer(y)) + " "
                End If
            End If
        Next y

        GetMACFromBuffer = t

    End Function
    '' This function will format the IP address retrieved from DT_IP_ADDRESS
    ''
    'Private Function GetIPFromBuffer(ByRef Buffer() As Byte) As String

    '    Dim y As Int16, t As String

    '    For y = 0 To 3

    '        'make a printable version of each octet, add leading zeros to make all the octest three digits in length
    '        t = t + VB6.Format(StrConv(CStr(Buffer(y)), 1, Len(Buffer(y))), "000")

    '        ' place a decimal point between the octets
    '        If y < 3 Then
    '            t = t + "."
    '        Else
    '            t = t + " "
    '        End If

    '    Next y

    '    GetIPFromBuffer = t

    'End Function

    '' This function will format the Node Number retrieved from DT_NODE_NUMBER
    ''
    'Private Function GetNodeNumberFromBuffer(ByRef Buffer() As Byte) As Integer

    '    Dim tNN As Integer

    '    ' the Node Number can be a 32bit number ( 4 bytes)
    '    '
    '    tNN = Buffer(0) + (Buffer(1) * 256) + (Buffer(2) * 65536.0#) + (Buffer(3) * 16777216.0#)

    '    GetNodeNumberFromBuffer = tNN

    'End Function

    ' This function will format the Node Name retrieved from DT_NODE_NAME
    '
    ''UPGRADE_NOTE: Size was upgraded to Size_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'Private Function GetNodeNameFromBuffer(ByRef Buffer() As Byte, ByRef Size_Renamed As Int16) As String

    '    Dim i As Int16
    '    Dim NameText As String

    '    For i = 0 To Size_Renamed - 1
    '        NameText = NameText + Chr(Buffer(i))
    '    Next

    '    GetNodeNameFromBuffer = NameText

    'End Function

    '' This function will format the Node Description retrieved from DT_NODE_DESCRIPTION
    ''
    ''UPGRADE_NOTE: Size was upgraded to Size_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'Private Function GetNodeDescFromBuffer(ByRef Buffer() As Byte, ByRef Size_Renamed As Int16) As String

    '    Dim i As Int16, DescText As String

    '    For i = 0 To Size_Renamed - 1

    '        DescText = DescText + Chr(Buffer(i))
    '    Next

    '    GetNodeDescFromBuffer = DescText

    'End Function

    ' This function will display some text for a given error code
    '
    Public Function ShowHEIErrorText(ByRef ErrorCode As Integer) As String

        Select Case ErrorCode
            Case 32769
                ShowHEIErrorText = "HEI Version Mismatch"

            Case 32771
                ShowHEIErrorText = "Invalid Device"

            Case 32772
                ShowHEIErrorText = "Data Buffer Too Small"

            Case 32774
                ShowHEIErrorText = "Timeout Error"

            Case 32775
                ShowHEIErrorText = "Unsupported Protocol"

            Case 32776
                ShowHEIErrorText = "IP Address not Initialized"

            Case 32778
                ShowHEIErrorText = "IPX Transport Not Initialized"

            Case 32779
                ShowHEIErrorText = "Error Opening IPX Socket"

            Case 32784
                ShowHEIErrorText = "Invalid Request"

            Case 32787
                ShowHEIErrorText = "Data Too Large"

            Case 41079
                ShowHEIErrorText = "Invalid Data"

            Case Else
                ShowHEIErrorText = "unknown"

        End Select

    End Function
    '***********************************************************************
    ' Since the HEIxxxx calls require a byte buffer, convert the user
    ' entered strings to byte arrays
    '
    'Function StringToByteArray(ByVal inString As String, ByRef Buffer() As Byte) As Int16
    '    Dim i As Int16
    '    Dim u() As Byte

    '    'Make sure all alpha characters are uppercase
    '    'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1059"'
    '    u = System.Text.UnicodeEncoding.Unicode.GetBytes(StrConv(inString, VbStrConv.UpperCase))

    '    'skip over the Unicode byte
    '    For i = 0 To (Len(inString) - 1)
    '        Buffer(i) = u(i * 2)
    '    Next i

    '    StringToByteArray = i

    'End Function

    '***********************************************************************
    ' Swap successive entries in a byte array
    '
    'Function ByteSwap(ByRef Buffer() As Byte, ByRef Count As Int16) As Int16
    '    Dim i As Int16
    '    Dim Temp As Byte

    '    For i = 0 To Count - 1 Step 2
    '        Temp = Buffer(i)

    '        Buffer(i) = Buffer(i + 1)

    '        Buffer(i + 1) = Temp
    '    Next i

    '    ByteSwap = i

    'End Function

    '************************************************************************
    ' Convert a byte array of character codes to a packed array of characters
    '
    'Function HexConvert(ByRef Buffer() As Byte, ByRef Count As Int16) As Int16
    '    Dim i As Int16

    '    'convert each character code
    '    For i = 0 To (Count * 2) - 1

    '        'have to manually process HEX character digits

    '        If (Buffer(i) > 64) And (Buffer(i) < 71) Then

    '            Select Case Buffer(i)
    '                Case 65 'A
    '                    Buffer(i) = 10
    '                Case 66 'B
    '                    Buffer(i) = 11
    '                Case 67 'C
    '                    Buffer(i) = 12
    '                Case 68 'D
    '                    Buffer(i) = 13
    '                Case 69 'E
    '                    Buffer(i) = 14
    '                Case 70 'F
    '                    Buffer(i) = 15
    '            End Select

    '        Else
    '            'numeric digits are much easier
    '            Buffer(i) = CByte(Chr(Buffer(i)))

    '        End If

    '    Next i

    '    'Now pack two HEX characters into a byte
    '    Dim Z As Int16

    '    Z = 0
    '    For i = 0 To (Count * 2) - 1 Step 2
    '        Buffer(Z) = (Buffer(i) * 16) + Buffer(i + 1)
    '        Z = Z + 1
    '    Next i

    '    'Now clear the remainder of the byte array - just to be neat and complete
    '    For i = Z To (Count * 2) - 1
    '        Buffer(i) = 0
    '    Next i
    '    HexConvert = Z

    'End Function

    ''***********************************************************************
    '' Brute force method of converting a 4 character string to a HEX number
    ''
    'Function StringToHexInt(ByRef inData As String) As Int16

    '    Dim i As Int16
    '    Dim t(4) As Byte

    '    i = StringToByteArray(inData, t)

    '    'convert each character code
    '    For i = 0 To (Len(inData) - 1)

    '        'have to manually process HEX characters digits
    '        If (t(i) > 64) And (t(i) < 71) Then
    '            Select Case t(i)
    '                Case 65 'A
    '                    t(i) = 10
    '                Case 66 'B
    '                    t(i) = 11
    '                Case 67 'C
    '                    t(i) = 12
    '                Case 68 'D
    '                    t(i) = 13
    '                Case 69 'E
    '                    t(i) = 14
    '                Case 70 'F
    '                    t(i) = 15
    '            End Select

    '        Else
    '            'numeric digits are much easier
    '            t(i) = CByte(Chr(t(i)))

    '        End If
    '    Next i

    '    Select Case Len(inData)
    '        Case 0
    '            StringToHexInt = 0
    '        Case 1
    '            StringToHexInt = t(0)
    '        Case 2
    '            StringToHexInt = (t(0) * 16) + t(1)
    '        Case 3
    '            StringToHexInt = (t(0) * 256) + (t(1) * 16) + t(2)
    '        Case 4
    '            StringToHexInt = (t(0) * 4096) + (t(1) * 256) + (t(2) * 16) + t(3)
    '    End Select

    'End Function

    'currently set to use IP addresses
    Public Function ConnectDevice(ByVal DeviceNumber As Int16) As Boolean
        InitializeDevice(DeviceNumber)
        tDevice = DeviceNumber

        ' Open the device
        '
        Rc = HEIOpenDevice(pTransportStructure, aDevices(tDevice), HEIAPIVersion, DefDevTimeout, DefDevRetrys, False)
        Dim RSDBuffer(31) As Byte
        Dim RSDSize As Int16
        Dim RSDError As Boolean
        Dim VISize As Int16
        Dim tBuffer(255) As Byte
        Dim RSize As Int16

        Dim VIBuffer As New VersionInfoDef
        VIBuffer.Initialize()
        Dim pVIBuffer As IntPtr

        If Rc <> 0 Then
            Return False

        Else
            DeviceOpen = True
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Display the Device Type
            '

            System.Array.Clear(RSDBuffer, 0, RSDBuffer.Length)
            RSDSize = 32

            Rc = HEIReadSetupData(aDevices(tDevice), DT_TYPE_STRING, RSDBuffer(0), RSDSize)
            If Rc = 0 Then
            Else
                RSDError = False
                tDeviceType = BuildTypeString(aDevices(tDevice), RSDError)
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Display the boot loader version and the firmware version
            '
            VISize = 50

            'Creat a memory buffer in the unmanaged heap and obtain a pointer to it.  
            pVIBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(VIBuffer))

            'Copy the VIBuffer structure to the buffer 
            Marshal.StructureToPtr(VIBuffer, pVIBuffer, True)

            'pass the pointer to the function to obtain version information
            Rc = HEIReadVersionInfo(aDevices(tDevice), pVIBuffer, VISize)

            'copy the memory buffer back to the VIBuffer structure located in managed heap
            VIBuffer = Marshal.PtrToStructure(pVIBuffer, GetType(VersionInfoDef))

            'free the memory buffer in the unmanaged heap
            Marshal.FreeHGlobal(pVIBuffer)

            If Rc <> 0 Then
                Return False
            Else
                If VIBuffer.SizeofVersionInfo <> 0 Then
                    'DisplayList.Items.Add("Booter : " & _
                    'Format(VIBuffer.BootVersion.MajorVersion, "0") & "." & _
                    'Format(VIBuffer.BootVersion.MinorVersion, "0") & "." & _
                    'Format(VIBuffer.BootVersion.BuildVersion, "000"))
                    'DisplayList.Items.Add("Version: " & _
                    'Format(VIBuffer.OSVersion.MajorVersion, "0") & "." & _
                    'Format(VIBuffer.OSVersion.MinorVersion, "0") & "." & _
                    'Format(VIBuffer.OSVersion.BuildVersion, "000"))
                End If
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Display the Module ID (Node Number)
            '
            System.Array.Clear(tBuffer, 0, tBuffer.Length)
            RSize = 4

            Rc = HEIReadSetupData(aDevices(tDevice), DT_NODE_NUMBER, tBuffer(0), RSize)
            If Rc <> 0 Then
                'DisplayList.Items.Add("Error reading Module ID:")

            Else
                'DisplayList.Items.Add("ID     : " & BitConverter.ToInt32(tBuffer, 0).ToString)

            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Display the Node Name
            '
            System.Array.Clear(tBuffer, 0, tBuffer.Length)
            RSize = 256

            Rc = HEIReadSetupData(aDevices(tDevice), DT_NODE_NAME, tBuffer(0), RSize)
            If Rc <> 0 Then
                Return False
                'DisplayList.Items.Add("Error reading Module Name")

            Else
                'DisplayList.Items.Add("Name   : " & ascii.GetString(tBuffer, 0, Array.IndexOf(tBuffer, Byte.Parse("0"))))

            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Display the Module Description
            '
            System.Array.Clear(tBuffer, 0, tBuffer.Length)
            RSize = 256

            Rc = HEIReadSetupData(aDevices(tDevice), DT_DESCRIPTION, tBuffer(0), RSize)
            If Rc <> 0 Then
                Return False
                'DisplayList.Items.Add("Error reading Module Description")

            Else
                'DisplayList.Items.Add("Desc   : " & ascii.GetString(tBuffer, 0, Array.IndexOf(tBuffer, Byte.Parse("0"))))

            End If

            'DisplayList.Items.Add("--------------------------------------------------------")

        End If
        '---------------------------------------
        Return True
    End Function
    Public Sub InitializeDevice(ByVal DeviceNumber As Int16)
        aDevices(DeviceNumber).Address(0) = 2
        aDevices(DeviceNumber).Address(1) = 0

        aDevices(DeviceNumber).Address(2) = &H70
        aDevices(DeviceNumber).Address(3) = &H70

        aDevices(DeviceNumber).Address(4) = DeviceAddress(DeviceNumber, 0)
        aDevices(DeviceNumber).Address(5) = DeviceAddress(DeviceNumber, 1)
        aDevices(DeviceNumber).Address(6) = DeviceAddress(DeviceNumber, 2)
        aDevices(DeviceNumber).Address(7) = DeviceAddress(DeviceNumber, 3)
    End Sub

    'read and write subroutines build from Host Engineering base code, repurposed to differentiate between memory types

    'subroutines for finding the value of a x,y,gx,gy,c, or t coil, or a vmemory address
    'returns either "on" or "off" for a coil, or the value of a vmem
    'to use call ReadValue(), which will determine what you are looking for and call the other subroutines.
    'to find X15, use ReadValue("x15"), c24 use ReadValue("c24"), v432 use ReadValue("v423")
    'x,y,c,v, etc can be either upper or lowercase


    'ReadValue subroutines are used to generate a string corresponding to the value of a coil or memory address
    'Discrete coils return either "on" or 'off"
    'V-memories return the value of the v-memory address

    'RelayAddress needs to be the string value of the address to be read from - "X0", "Y12", "CT25", "V2021", etc.
    'an invalid address will return the InvalidAddress string

    'returns the value of the address in RelayAddress from the PLC at DeviceAddresses(0, x)
 
    'returns the value of the address in RelayAddress from the PLC at DeviceAddresses(TargetAddress, x)
    'allows the user to read from more than just the first PLC
    Public Function ReadValue(ByVal RelayAddress As String, ByVal TargetDevice As Int16) As String

        tDevice = TargetDevice

        Dim VMemAddress As String = ""

        If RelayAddress = "" Then
            Return InvalidAddress
            Exit Function
        End If

        Dim AddressType As String

        'determine if first character is x, y, t, c, g, or s
        Select Case RelayAddress(0)
            Case "x", "X"
                AddressType = "x"
                RelayAddress = RelayAddress.Substring(1)
            Case "y", "Y"
                AddressType = "y"
                RelayAddress = RelayAddress.Substring(1)
            Case "t", "T"
                AddressType = "t"
                RelayAddress = RelayAddress.Substring(1)
            Case "c", "C"
                AddressType = "c"
                RelayAddress = RelayAddress.Substring(1)
            Case "g", "G"
                AddressType = "g"
                RelayAddress = RelayAddress.Substring(1)
            Case "s", "S"
                AddressType = "s"
                RelayAddress = RelayAddress.Substring(1)
            Case "v", "V"
                AddressType = "v"
                VMemAddress = RelayAddress.Substring(1)
            Case Else
                Return InvalidAddress
                Exit Function
        End Select
        'if first char was c, g, or s, and at least 1 char left, then test for ct, gx/gy, and sp
        If Len(RelayAddress) > 0 Then
            Select Case AddressType(0)
                Case "c"
                    Select Case RelayAddress(0)
                        Case "t"
                            AddressType = "ct"
                            RelayAddress = RelayAddress.Substring(1)
                    End Select
                Case "g"
                    Select Case RelayAddress(0)
                        Case "x"
                            AddressType = "gx"
                            RelayAddress = RelayAddress.Substring(1)
                        Case "y"
                            AddressType = "gy"
                            RelayAddress = RelayAddress.Substring(1)
                        Case Else
                            Return InvalidAddress
                            Exit Function
                    End Select
                Case "s"
                    Select Case RelayAddress(0)
                        Case "p"
                            AddressType = "sp"
                            RelayAddress = RelayAddress.Substring(1)
                    End Select
            End Select
        End If

        If Len(RelayAddress) = 0 Then
            Return InvalidAddress
            Exit Function
        End If

        Dim Address As Int16 = 0
        Dim BitAddress As Int16 = 0

        Select Case AddressType
            Case "x"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + XCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H32S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "y"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + YCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "c"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + CCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "s"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + SCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "t"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + TCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "ct"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + CTCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "gx"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + GXCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H32S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "gy"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + GYCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H33S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "sp"
                If CheckOctal(RelayAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                Else
                    Address = (ConvertOctalToDecimal(RelayAddress) \ 8) + SPCCMRange
                    BitAddress = ConvertOctalToDecimal(RelayAddress) Mod 8
                    Return ReadValue(&H32S, Address, BitAddress, VMemAddress)
                    Exit Function
                End If
            Case "v"

                If CheckOctal(VMemAddress) = False Then
                    Return InvalidAddress
                    Exit Function
                End If

                Dim i As Int16 = ConvertOctalToDecimal(VMemAddress)

                If VMemDisallowed1On = True Then
                    If (i >= VMemDisallowedLowerLimit1) And (i <= VMemDisallowedUpperLimit1) Then
                        Return InvalidAddress
                        Exit Function
                    End If
                End If

                If VMemDisallowed2On = True Then
                    If (i >= VMemDisallowedLowerLimit2) And (i <= VMemDisallowedUpperLimit2) Then
                        Return InvalidAddress
                        Exit Function
                    End If
                End If

                VMemAddress = Hex(Val("&O" & VMemAddress) + 1)
                Return ReadValue(&H31S, Address, 0, VMemAddress)
                Exit Function
        End Select

        Return "end of function"
    End Function
    'generic read function
    Public Function ReadValue(ByVal DataType As Byte, ByVal RelayAddress As Int16, ByVal BitAddress As Int16, ByVal VMemAddress As String) As String
        'if the device is disconnected, attempt to reconnect
        If DeviceConnected(tDevice) = False Then
            ConnectDevice(tDevice)
        End If

        'Reading from an IP address of 0.0.0.0 throws an error in the RC = line 
        If (DeviceAddress(tDevice, 0) = 0) And (DeviceAddress(tDevice, 1) = 0) And (DeviceAddress(tDevice, 2) = 0) And (DeviceAddress(tDevice, 3) = 0) Then
            DeviceConnected(tDevice) = False
            Return "Error"
        End If


        If (DataType <> &H31S) And (DataType <> &H32S) And (DataType <> &H33S) Then
            Return InvalidDataType
            Exit Function
        End If


        Dim DataAddress As Int16
        Dim DataLength As Int16

        If DataType = &H31S Then
            DataLength = 2
            DataAddress = "&h" & VMemAddress
        ElseIf DataType = &H32S Then
            DataLength = 1
            DataAddress = RelayAddress
        ElseIf DataType = &H33S Then
            DataLength = 1
            DataAddress = RelayAddress
        End If

        Dim bWrite As Int16
        bWrite = False

        Dim ByteBuffer(255) As Byte
        Dim i As Int16

        System.Array.Clear(ByteBuffer, 0, ByteBuffer.Length)

        Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))
        Dim l, h As String

        If Rc <> 0 Then
            'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
        Else
            DetailLine = ""

            If DataType = &H31S Then
                For i = 0 To DataLength - 1

                    l = Hex(ByteBuffer(i)).PadLeft(2, "0")
                    h = Hex(ByteBuffer(i + 1)).PadLeft(2, "0")
                    DetailLine = DetailLine & h & l & " "
                    i += 1
                Next
                Return DetailLine
                Exit Function
            Else
                Dim YValues(8) As Boolean
                Dim j As Int16
                For j = 0 To 7
                    YValues(j) = ByteBuffer(0) And (2 ^ j)
                Next
                If YValues(BitAddress) = True Then
                    Return "on"
                    Exit Function
                Else
                    Return "off"
                    Exit Function
                End If
            End If
        End If

        DeviceConnected(tDevice) = False
        Return "Error"
    End Function

    'reads in a range of vmemory locations at once, up to a maximum of 128
    'Use this function to read multiple adjacent vmemory locations quicker than reading in each individually
    'calling syntax is ReadVMems(first address, number of vmems, target device)
    'so to find v100 - v123 of device 0, use ReadVMems("V100", 24, 0)
    'returns a single string consisting of 4 characters followed by space for each vmem
    'so 4 vmems might read as this "1234 abce 2321 aadd "
    Public Function ReadVMems(ByVal FirstVMem As String, ByVal NumberOfVMems As Int16, ByVal TargetDevice As Int16) As String
        If (NumberOfVMems <= 128) Then
            tDevice = TargetDevice
            Dim DataType As Byte = &H31S

            Dim DataAddress As Int16
            Dim DataLength As Int16
            DataLength = 2 * NumberOfVMems
            DataAddress = "&h" & Hex(Val("&O" & FirstVMem) + 1)

            Dim bWrite As Int16
            bWrite = False

            Dim ByteBuffer(255) As Byte
            Dim i As Int16

            System.Array.Clear(ByteBuffer, 0, ByteBuffer.Length)

            Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))
            Dim l, h As String

            If Rc <> 0 Then
                Return ""
            Else
                DetailLine = ""

                For i = 0 To DataLength - 1

                    l = Hex(ByteBuffer(i)).PadLeft(2, "0")
                    h = Hex(ByteBuffer(i + 1)).PadLeft(2, "0")
                    DetailLine = DetailLine & h & l & " "
                    i += 1
                Next
                Return DetailLine
                Exit Function

            End If
            Return "Error"
        Else
            Return "Error"
        End If

    End Function

    Public Function ReadStatus(ByVal FirstVMem As String, ByVal NumberOfVMems As Int16, ByVal TargetDevice As Int16,
                               _MachStatCode() As Int16) As String
        'go thru all of the addresses and read the first byte
        Dim PLCaddr = New String() {"2000", "2040", "2100", "2140", "2200", "2240", "2300", "2340", "2400"}
        Dim outVal As String = ""

        Dim DataType As Byte = &H31S

        Dim DataAddress As Int16
        Dim DataLength As Int16
        Dim bWrite As Int16
        Dim ByteBuffer(255) As Byte
        Dim i As Int16
        Dim l, h As String

        For addrLoop = 0 To 8
            tDevice = TargetDevice

            DataLength = 2 * NumberOfVMems
            DataAddress = "&h" & Hex(Val("&O" & PLCaddr(addrLoop) + 1))
            bWrite = False


            System.Array.Clear(ByteBuffer, 0, ByteBuffer.Length)

            Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))
            If Rc <> 0 Then
                Return ""
            Else
                DetailLine = ""

                For i = 0 To DataLength - 1
                    l = Hex(ByteBuffer(i)).PadLeft(2, "0")
                    h = Hex(ByteBuffer(i + 1)).PadLeft(2, "0")
                    DetailLine = DetailLine & h & l & " "
                    i += 1
                Next
                If DetailLine = "0203 " Then
                    'status is run
                    _MachStatCode(addrLoop) = 3
                End If
                If DetailLine = "0001 " Then
                    'status is stopped
                    _MachStatCode(addrLoop) = 1
                End If
                outVal = outVal + DetailLine
                'Return DetailLine
                'Exit Function
            End If
        Next addrLoop

        Return outVal

    End Function



    'writes vmemories assuming no spaces between each vmemory string - useful if they were stored line-by-line in text file
    Public Sub WriteVMems(ByVal FirstVMem As String, ByVal NumberOfVMems As Int16, ByVal TargetDevice As Int16, ByVal Value As String)
        tDevice = TargetDevice
        Dim DataType As Byte = &H31S

        Dim DataLength As Int16
        DataLength = 2 * NumberOfVMems

        Dim DataAddress As Int16
        DataAddress = "&h" & Hex(Val("&O" & FirstVMem) + 1)

        Dim bWrite As Int16
        bWrite = True

        Dim ByteBuffer(255) As Byte
        Dim i As Int16

        For i = 0 To DataLength / 2 - 1
            ByteBuffer(i * 2) = "&h" & Value.Substring(i * 4 + 2, 2) ' low byte
            ByteBuffer(i * 2 + 1) = "&h" & Value.Substring(i * 4, 2) ' high byte
        Next

        Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))

        If Rc <> 0 Then
            'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
        End If
    End Sub
    'writes vmemories with spaces between each vmemory string, - useful if still in format saved from elsewhere in PLC
    Public Sub WriteVMemsWithSpaces(ByVal FirstVMem As String, ByVal NumberOfVMems As Int16, ByVal TargetDevice As Int16, ByVal Value As String)
        tDevice = TargetDevice
        Dim DataType As Byte = &H31S

        Dim DataLength As Int16
        DataLength = 2 * NumberOfVMems

        Dim DataAddress As Int16
        DataAddress = "&h" & Hex(Val("&O" & FirstVMem) + 1)

        Dim bWrite As Int16
        bWrite = True

        Dim ByteBuffer(255) As Byte
        Dim i As Int16

        For i = 0 To DataLength / 2 - 1
            ByteBuffer(i * 2) = "&h" & Value.Substring(i * 5 + 2, 2) ' low byte
            ByteBuffer(i * 2 + 1) = "&h" & Value.Substring(i * 5, 2) ' high byte
        Next

        Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))

        If Rc <> 0 Then
            'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
        End If
    End Sub

    'can use on/1 to turn on, off/0 to turn off
    Public Sub WriteValue(ByVal Address As String, ByVal Value As String, ByVal TargetDevice As Int16)
        Dim VMemAddress As String = ""

        If Address = "" Then
            Exit Sub
        End If

        Dim AddressType As String

        'determine if first character is x, y, t, c, g, or s
        Select Case Address(0)
            Case "x", "X"
                AddressType = "x"
                Address = Address.Substring(1)
            Case "y", "Y"
                AddressType = "y"
                Address = Address.Substring(1)
            Case "t", "T"
                AddressType = "t"
                Address = Address.Substring(1)
            Case "c", "C"
                AddressType = "c"
                Address = Address.Substring(1)
            Case "g", "G"
                AddressType = "g"
                Address = Address.Substring(1)
            Case "s", "S"
                AddressType = "s"
                Address = Address.Substring(1)
            Case "v", "V"
                AddressType = "v"
                VMemAddress = Address.Substring(1)
            Case Else
                Exit Sub
        End Select

        'if first char was c, g, or s, and at least 1 char left, then test for ct, gx/gy, and sp
        If Len(Address) > 0 Then
            Select Case AddressType(0)
                Case "c"
                    Select Case Address(0)
                        Case "t"
                            AddressType = "ct"
                            Address = Address.Substring(1)
                    End Select
                Case "g"
                    Select Case Address(0)
                        Case "x"
                            AddressType = "gx"
                            Address = Address.Substring(1)
                        Case "y"
                            AddressType = "gy"
                            Address = Address.Substring(1)
                        Case Else
                            Exit Sub
                    End Select
                Case "s"
                    Select Case Address(0)
                        Case "p"
                            AddressType = "sp"
                            Address = Address.Substring(1)
                    End Select
            End Select
        End If

        'now seperated into address type and address
        If Len(Address) = 0 Then
            Exit Sub
        End If

        Dim BitAddress As Int16 = 0
        Dim DataAddress As Int16 = 0
        '----------
        'Dim BitAddress As Int16 = 0

        Select Case AddressType
            Case "x"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + XCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H32S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "y"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + YCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "c"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + CCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "s"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + SCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "t"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + TCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "ct"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + CTCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "gx"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + GXCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H32S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "gy"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + GYCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H33S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "sp"
                If CheckOctal(Address) = False Then
                    Exit Sub
                Else
                    DataAddress = (ConvertOctalToDecimal(Address) \ 8) + SPCCMRange
                    BitAddress = ConvertOctalToDecimal(Address) Mod 8
                    WriteValue(&H32S, DataAddress, BitAddress, VMemAddress, Value, TargetDevice)
                    Exit Sub
                End If
            Case "v"
                If CheckOctal(VMemAddress) = False Then
                    Exit Sub
                End If

                Dim i As Int16 = ConvertOctalToDecimal(VMemAddress)

                If VMemDisallowed1On = True Then
                    If (i >= VMemDisallowedLowerLimit1) And (i <= VMemDisallowedUpperLimit1) Then
                        Exit Sub
                    End If
                End If

                If VMemDisallowed2On = True Then
                    If (i >= VMemDisallowedLowerLimit2) And (i <= VMemDisallowedUpperLimit2) Then
                        Exit Sub
                    End If
                End If

                VMemAddress = Hex(Val("&O" & VMemAddress) + 1)
                WriteValue(&H31S, DataAddress, 0, VMemAddress, Value, TargetDevice)
                Exit Sub
        End Select




    End Sub

    Public Sub WriteValue(ByVal DataType As Byte, ByVal Address As Int16, ByVal BitAddress As Int16, ByVal VMemAddress As String, ByVal Value As String, ByVal TargetDevice As Int16)
        tDevice = TargetDevice

        If (DataType <> &H31S) And (DataType <> &H32S) And (DataType <> &H33S) Then
            'DisplayList.Items.Add("Invalid DataType entered")
            Exit Sub
        End If

        Dim DataLength As Int16
        Dim DataAddress As Int16
        Dim bWrite As Int16
        bWrite = False
        Dim ByteBuffer(255) As Byte
        Dim i As Int16
        Dim RelayValues(8) As Boolean
        Dim BufferValue As Int16 = 0

        If DataType = &H31S Then
            bWrite = True
            DataLength = 2
            DataAddress = "&h" & VMemAddress


            If ((CheckHex(Value) <> True) Or (Len(Value) > 4)) Then
                'DisplayList.Items.Add("Invalid value entered.")
                Exit Sub
            End If

            ByteBuffer(i * 2) = "&h" & Value.Substring(i * 4 + 2, 2) ' low byte
            ByteBuffer(i * 2 + 1) = "&h" & Value.Substring(i * 4, 2) ' high byte

            Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))

            If Rc <> 0 Then
                'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
            End If

        Else
            DataLength = 1
            DataAddress = Address

            Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))

            If Rc <> 0 Then
                'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
            Else
                For i = 0 To 7
                    RelayValues(i) = ByteBuffer(0) And (2 ^ i)
                Next
                Select Case Value
                    Case "off", "0"
                        RelayValues(BitAddress) = False
                    Case "on", "1"
                        RelayValues(BitAddress) = True
                    Case Else
                        'DisplayList.Items.Add("Invalid Value")
                        Exit Sub
                End Select

                For i = 0 To 7
                    If RelayValues(i) = True Then
                        BufferValue = BufferValue + (2 ^ i)
                    End If
                Next
                ByteBuffer(0) = "&h" & Hex(BufferValue)

                bWrite = True
                Rc = HEICCMRequest(aDevices(tDevice), bWrite, DataType, DataAddress, DataLength, ByteBuffer(0))

                If Rc <> 0 Then
                    'DisplayList.Items.Add("Error " & Hex(Rc) & " (" & ShowHEIErrorText(Rc) & ") performing CCM Request")
                End If
            End If
        End If
    End Sub

    'given a string, returns true if the string would be an octal number, false if not (or decimal, or hex)
    'could possibly be replace with hardcoded commands which I couldn't find
    'however left in under the 'ain't broke, don't fix' rule
    Public Function CheckOctal(ByVal TextToCheck As String) As Boolean
        Dim i As Int16
        If Len(TextToCheck) = 0 Then
            CheckOctal = False
            Exit Function
        End If
        For i = 0 To Len(TextToCheck) - 1
            Select Case TextToCheck(i)
                Case "0" To "7"
                Case Else
                    CheckOctal = False
                    Exit Function
            End Select
        Next
        CheckOctal = True
    End Function
    Public Function CheckDecimal(ByVal TextToCheck As String) As Boolean
        Dim i As Int16
        For i = 0 To Len(TextToCheck) - 1
            Select Case TextToCheck(i)
                Case "0" To "9"
                Case Else
                    CheckDecimal = False
                    Exit Function
            End Select
        Next
        CheckDecimal = True
    End Function
    Public Function CheckHex(ByVal TextToCheck As String) As Boolean
        Dim i As Int16
        For i = 0 To Len(TextToCheck) - 1
            Select Case TextToCheck(i)
                Case "0" To "9"
                Case "a" To "f"
                Case "A" To "F"
                Case Else
                    CheckHex = False
                    Exit Function
            End Select
        Next
        CheckHex = True
    End Function
    'converts an octal number string to a decimal int16 number
    Public Function ConvertOctalToDecimal(ByVal TextToConvert As String) As Int16
        Dim DecimalValue As Int32
        DecimalValue = 0

        Dim PlaceValue As Int32
        PlaceValue = 1

        Dim i As Int16
        If Len(TextToConvert) <> 0 Then
            For i = Len(TextToConvert) - 1 To 0 Step -1
                Select Case TextToConvert(i)
                    Case "0"
                        DecimalValue = DecimalValue + 0 * PlaceValue
                    Case "1"
                        DecimalValue = DecimalValue + 1 * PlaceValue
                    Case "2"
                        DecimalValue = DecimalValue + 2 * PlaceValue
                    Case "3"
                        DecimalValue = DecimalValue + 3 * PlaceValue
                    Case "4"
                        DecimalValue = DecimalValue + 4 * PlaceValue
                    Case "5"
                        DecimalValue = DecimalValue + 5 * PlaceValue
                    Case "6"
                        DecimalValue = DecimalValue + 6 * PlaceValue
                    Case "7"
                        DecimalValue = DecimalValue + 7 * PlaceValue
                End Select
                PlaceValue = PlaceValue * 8
            Next
            Return DecimalValue
        Else
            Return 0
        End If
    End Function
End Module
