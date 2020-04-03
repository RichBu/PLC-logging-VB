' HEI Definitions for .Net Version 1.1
' Converted to VB.NET by
'
'      Clark Sann, PE, MCSD
'      Control Solutions
'      csann@earthlink.net
'
' This is a work in progress.
' 
' Please contact me if you find portions that do not work as expected or if you have bug fixes.  
'
'Revision History
'1.0  2/28/05  Initial version 
'1.1  7/27/05  Revised function definition and function call of HEIQueryDevices.  The original definition and call passed 
'              only 1 HEIDevice structure to HEIQueryDevices, rather than an array of structures.  This caused memory to be 
'              overwritten if there was more than 1 device on the network.   

Imports System.Runtime.InteropServices

Module HEI_Definitions

    Const MYDLLSHORT As String = "c:\hei32_3.dll"
    'Const MYDLLSHORT As String = "C:\Documents and Settings\Clark\Desktop\EtherSDK\MyHEI\Debug\MyHEI.DLL"

    Const MYDLLLONG As String = "c:\hei32_3.DLL"
    'Const MYDLLLONG As String = "C:\Documents and Settings\Clark\Desktop\EtherSDK\MyHEI\Debug\MyHEI.DLL"

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' HEI API Defines
    '
    Friend Const HEIAPIVersion As Byte = 3
    Friend Const HEIAPIVersionString As String = "3"
    Friend Const HEIP_HOST As Int16 = 1
    Friend Const HEIP_IPX As Int16 = 2
    Friend Const HEIP_IP As Int16 = 3
    Friend Const HEIP_SERIAL As Int16 = 4

    Friend Const HEIT_HOST As Int16 = 1
    Friend Const HEIT_IPX As Int16 = 2
    Friend Const HEIT_WINSOCK As Int16 = 4
    Friend Const HEIT_OTHER_TRANSPORT As Int16 = 8
    Friend Const HEIT_UNIX As Int16 = 16
    Friend Const HEIT_SERIAL As Int16 = 32

    Friend Const DefDevTimeout As Int16 = 50 ' value in milliseconds
    Friend Const DefDevRetrys As Byte = 3
    Friend Const DefDevUseAddressedBroadcast As Boolean = False
    Friend Const LongDevTimeout As Int16 = DefDevTimeout + 200 ' value in milliseconds
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Error Messages
    '
    Friend Const HEIE_NULL As Int16 = 0 ' No error
    Friend Const HEIE_FIRST_ERROR As Int16 = &H8000S ' Number for first error
    Friend Const HEIE_LAST_ERROR As Int16 = &HFFFFS ' Number for last error
    Friend Const HEIE_NOT_IMPLEMENTED As Int16 = &H8000S ' Function not implemented
    Friend Const HEIE_VER_MISMATCH As Int16 = &H8001S ' Version passed to function is not correct for the library
    Friend Const HEIE_UNSUPPORTED_TRANSPORT As Int16 = &H8002S ' Supplied transport is not supported
    Friend Const HEIE_INVALID_DEVICE As Int16 = &H8003S ' Supplied device is not valid
    Friend Const HEIE_BUFFER_TOO_SMALL As Int16 = &H8004S ' Supplied buffer is too small
    Friend Const HEIE_ZERO_BYTES_RECEIVED As Int16 = &H8005S ' Zero packets returned in the packet
    Friend Const HEIE_TIMEOUT As Int16 = &H8006S ' Timeout error
    Friend Const HEIE_UNSUPPORTED_PROTOCOL As Int16 = &H8007S ' Supplied protocol not supported
    Friend Const HEIE_IP_ADDRESS_NOT_INITIALIZED As Int16 = &H8008S ' The device's IP address has not been assigned.  NOTE: Need to use addressed broadcast to talk to the module (with IP) to setup the IP address. 
    Friend Const HEIE_NULL_TRANSPORT As Int16 = &H8009S ' No Transport specified
    Friend Const HEIE_IPX_NOT_INITIALIZED As Int16 = &H800AS ' IPX Transport not installed
    Friend Const HEIE_IPX_OPEN_SOCKET As Int16 = &H800BS ' Error opening IPX socket
    Friend Const HEIE_NO_PACKET_DRIVER As Int16 = &H800CS ' No packet driver found
    Friend Const HEIE_CRC_MISMATCH As Int16 = &H800DS ' CRC did not match
    Friend Const HEIE_ALLOCATION_ERROR As Int16 = &H800ES ' Memory allocation failed
    Friend Const HEIE_NO_IPX_CACHE As Int16 = &H800FS ' No cache has been allocated for IPX
    Friend Const HEIE_INVALID_REQUEST As Int16 = &H8010S ' Invalid CCM Request
    Friend Const HEIE_NO_RESPONSE As Int16 = &H8011S ' No response was available/requested
    Friend Const HEIE_INVALID_RESPONSE As Int16 = &H8012S ' Invalid format response was received
    Friend Const HEIE_DATA_TOO_LARGE As Int16 = &H8013S ' Given data is too large
    Friend Const HEIE_LOAD_PROC_ERROR As Int16 = &H8014S ' Error loading procedures
    Friend Const HEIE_NOT_LOADED As Int16 = &H8015S ' Attempted command before succesfull OpenTransport()
    Friend Const HEIE_ALIGNMENT_ERROR As Int16 = &H8016S ' Data not aligned on proper boundary 
    Friend Const HEIE_FILE_NOT_OPEN As Int16 = &H8017S ' File not open 

    Friend Const HEIE_LOAD_ERROR As Int16 = &H8100S ' Mask for WinSock load Error see below (from HEIOpenTransport)

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' If HEIOpenTransport()fails for WinSock, it will return one of the following errors:
    '
    ' &h8014    Error getting address from WinSock.DLL
    ' &h8100    System was out of memory, or executable file was corrupt, or sharing or network-protection error
    ' &h8101    Unused
    ' &h8102    file was not found
    ' &h8103    Path was not found
    ' &h8104    Unused
    ' &h8105    Attempt was made to dynamically link to a task, or there was a sharing or network-protection error
    ' &h8106    Library required separate data segments for each task
    ' &h8107    Unused
    ' &h8108    There was insufficient memory to start the application
    ' &h8109    Unused
    ' &h810A    Windows version was incorrect
    ' &h810B    Executable file was invalid, it's either not a Windows application or there was an error in the .EXE image.
    ' &h810C    Application was designed for a different operating system
    ' &h810D    Application was designed for MS-DOS 4.0
    ' &h810E    Type of executable was unknown
    ' &h810F    Attempt was made to load a real-mode application (developed for an earlier version of Windows )
    ' &h8110    Attempt was made to load a second instance of an application file containing multiple data segments that were not marked read-only
    ' &h8113    Attempt was made to load a compressed executable file. The file must be decompressed before it can be loaded
    ' &h8114    Dynamic-link library (DLL) file was invalid. One of the DLLs required to run this application was corrupt
    ' &h8115    Application requires Microsoft Windows 32-bit extensions
    ' &h8116 - &h811F	Unused
    Friend Const HEIE_LAST_LOAD_ERROR As Int16 = &H811FS        ' Last in the range of WinSock load Errors 

    Friend Const HEIE_INVALID_SLOT_NUMBER As Int16 = 118
    Friend Const HEIE_INVALID_DATA As Int16 = 119
    Friend Const HEIE_CHANNEL_FAILURE As Int16 = 121 ' Analog I/O, broken transmitter alarm, for modules with
    '  one error bit for all channels
    Friend Const HEIE_UNUSED_CHANNELS_EXIST As Int16 = 122 ' Analog I/O, module jumpers set to disable some channels
    Friend Const HEIE_INVALID_BASE_NUMBER As Int16 = 135
    Friend Const HEIE_INVALID_MODULE_TYPE As Int16 = 136
    Friend Const HEIE_INVALID_OFFSET As Int16 = 137
    Friend Const HEIE_BROKEN_TRANSMITTER As Int16 = 139
    Friend Const HEIE_INVALID_ADDRESS As Int16 = 140
    Friend Const HEIE_CHANNEL_FAILURE_MULTI As Int16 = 142 ' Analog I/O, broken transmitter alarm, for modules with
    '  an error bit for each channel, e.g. F2-04RTD
    Friend Const HEIE_SERIAL_SETUP_ERROR As Int16 = 143
    Friend Const HEIE_MODULE_ERROR As Int16 = 152
    Friend Const HEIE_MODULE_FAILURE As Int16 = 155
    Friend Const HEIE_PARITY_ERROR As Int16 = 156
    Friend Const HEIE_FRAMING_ERROR As Int16 = 157
    Friend Const HEIE_OVER_RUN_ERROR As Int16 = 158
    Friend Const HEIE_BUFFER_OVERFLOW As Int16 = 159
    Friend Const HEIE_DRIVE_TRIP As Int16 = 162 ' HA-EDRV2, Drive has tripped

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Terminator Hot-Swap Defines, Used in HEIRescanBase()
    '
    Friend Const HEIE_MODULE_NOT_RESPONDING As Int16 = 153 ' one or more module(s) removed
    Friend Const HEIE_BASE_CHANGED As Int16 = 154 ' one or more module(s) added
    Friend Const RESCAN_LEAVE_IMAGE_RAM As Int16 = 0 ' Don't clear the output image RAM after BaseInit
    Friend Const RESCAN_CLEAR_IMAGE_RAM As Int16 = 1 ' Clear the output image RAM after BaseInit


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Warnings
    '
    Friend Const HEIW_FIRST_WARNING As Int16 = &H2000S ' Number for First Warning
    Friend Const HEIW_LAST_WARNING As Int16 = &H2FFFS ' Number for Last Warning
    Friend Const HEIW_RETRY As Int16 = &H2000S ' One or More Retrys have occurred

    ' These are masks for values returned from HEIReadIO and/or HEIWriteIO and indicate that some
    ' error/warning/info/internal condition exists for some module in the base ( it could be an I/O
    ' module or it could be the ethernet module. The function HEIReadModuleStatus can then be used
    ' to retrieve the actual conditions. Note that more than one of the conditions can exist at any time.
    '
    Friend Const MASK_DEVICE_ERROR As Int16 = &H1000S
    Friend Const MASK_DEVICE_WARNING As Int16 = &H2000S
    Friend Const MASK_DEVICE_INFO As Int16 = &H4000S

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Data Types
    '
    Friend Const DT_IP_ADDRESS As Int16 = &H10S ' 4-byte IP Address
    Friend Const DT_NODE_NUMBER As Int16 = &H20S ' 4-byte Node Number
    Friend Const DT_NODE_NAME As Int16 = &H16S ' 256-byte Node Name
    Friend Const DT_DESCRIPTION As Int16 = &H26S ' 256-byte Node Description
    Friend Const DT_SUBNET_MASK As Int16 = &H30S ' 4-byte Subnet Mask
    Friend Const DT_LINK_MONITOR As Int16 = &H8006S ' 256 Byte Link monitor setup (Ram Based) - see link monitor structure below
    Friend Const DT_RXWX_SETTINGS As Int16 = &H15S ' 128 Byte setings for RXWX configuration see HEISettings
    Friend Const DT_SERIAL_SETTINGS As Int16 = &H11S ' 8 Byte Serial Setup(see SerialSetup)
    Friend Const DT_BASE_DEF As Int16 = &H17S ' 512 Byte Base Definition (405 HEIWriteBaseDef)
    Friend Const DT_MODULE_SETUP As Int16 = &H24S ' 64-byte data from FLASH (see ModuleSetup structure)
    Friend Const DT_TYPE_STRING As Int16 = &H33S ' 32 byte string that contains the part number
    Friend Const DT_GATEWAY_ADDRESS As Int16 = &H40S ' Byte Gateway Address 
    Friend Const DT_ERASE_COUNT As Int16 = &H50S     ' 4 Byte used internally 
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Data Formats
    '
    Friend Const DF_BIT_IN As Byte = &H3S
    Friend Const DF_BIT_OUT As Byte = &H4S
    Friend Const DF_WORD_IN As Byte = &H5S
    Friend Const DF_WORD_OUT As Byte = &H6S
    Friend Const DF_DWORD_IN As Byte = &H8S
    Friend Const DF_DWORD_OUT As Byte = &H9S
    Friend Const DF_BYTE_IN As Byte = &H10S
    Friend Const DF_BYTE_OUT As Byte = &H11S
    Friend Const DF_DOUBLE_IN As Byte = &H12S
    Friend Const DF_DOUBLE_OUT As Byte = &H13S
    Friend Const DF_FLOAT_IN As Byte = &H14S
    Friend Const DF_FLOAT_OUT As Byte = &H15S
    Friend Const DF_CONFIG As Byte = &H16S

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Module Types
    '
    Friend Const MT_EBC As Int16 = 0 ' Ethernet Base Controller
    Friend Const MT_ECOM As Int16 = 1 ' Ethernet Communications Module
    Friend Const MT_WPLC As Int16 = 2 ' WinPLC
    Friend Const MT_DRIVE As Int16 = 3 ' Ethernet Drive Interface
    Friend Const MT_ERMA As Int16 = 4 ' Ethernet Remote Master
    Friend Const MT_CTRIO As Int16 = 5 ' Counter Interface
    Friend Const MT_AVG_DISP As Int16 = 6 ' AVG Display Adapter
    Friend Const MT_PBC As Int16 = 7 ' Profibus Slave controller
    Friend Const MT_PBCC As Int16 = 8 ' Profibus Slave Communications Module (PSCM)
    Friend Const MT_UNK As Int16 = &HFFS ' Unknown

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Module Family defines for MT_EBC, MT_ECOM, MT_EDRV, MT_ERMA and MT_WPLC
    '
    Friend Const MF_005 As Int16 = 0 ' DL05 Series
    Friend Const MF_205 As Int16 = 2 ' DL205 Series
    Friend Const MF_305 As Int16 = 3 ' DL305 Series
    Friend Const MF_405 As Int16 = 4 ' DL405 Series
    Friend Const MF_TERM As Int16 = 10 ' Terminator I/O Series

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Module Family defines for MT_DRIVE
    '
    Friend Const MF_100_SERIES As Int16 = 1 ' Hitachi L100 and SJ100
    Friend Const MF_J300 As Int16 = 2 ' Hitachi J300
    Friend Const MF_300_Series As Int16 = 3 ' Hitachi SJ300
    Friend Const MF_GS As Int16 = 4 ' AutomationDirect GSx

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Module Family defines for MT_AVG_DISP
    '
    Friend Const MF_EZ_TOUCH As Int16 = 1 ' AVG EZ-Touch Ethernet Adapter

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Structure Definitions
    '
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure MemRef
        Dim Detail As MemRefDetail
        Dim pBuffer As Byte     'Pointer to Data buffer
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure MemRefDetail
        Dim Direction As Byte
        Dim Type As Int16
        Dim Offset As Int32
        Dim NumBytes As Int16
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure EthernetStats
        Dim SizeOfEthernetStats As Int16
        Dim MissedFrameCount As Integer
        Dim TransmitCollisionCount As Integer
        Dim DiscardedPackets As Integer
        Dim BadCRCCount As Integer
        Dim UnknownTypeCount As Integer
        Dim SendErrorCount As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure SupportDef
        Dim Version As Byte
        Dim Bytes2Follow As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)> _
        Dim UnusedBytes() As Byte
        'bit fields in Byte 1
        'FUN_POLLING As Byte             ' Polling one base
        'FUN_READ_VER_INFO As Byte       ' Version Info
        'FUN_READ_SUPPORT_INFO As Byte   ' Support Info
        'FUN_READ_DEVICE_INFO As Byte    ' Device Info
        'FUN_POLLING_ALL As Byte         ' Polling All bases (returns ethernet address)
        'FUN_WRITE_IO As Byte            ' Write IO Base
        'FUN_READ_IO As Byte             ' Read IO Base
        'FUN_READ_BASE_DEF As Byte       ' Read Base Def

        'bit fields in Byte 2
        'FUN_ENUM_SETUP_DATA As Byte     ' Emunerate the Setup Data
        'FUN_READ_SETUP_DATA As Byte     ' Read Setup Data
        'FUN_WRITE_SETUP_DATA As Byte    ' Write Setup Data
        'FUN_DELETE_SETUP_DATA As Byte   ' Delete Setup Data
        'FUN_READ_ETHERNET_STATS As Byte ' Read Ethernet Statistics
        'FUN_PET_LINK As Byte            ' Used to keep the link sense timer from firing in the absense of REadIO or WriteIO messages
        'FUN_ADDRESSED_BROADCAST As Byte ' Used to broadcast to a particular ethernet address
        'FUN_READ_MODULE_STATUS As Byte  ' Read module status (int16 bytes)

        'bit fields in Byte 3
        'FUN_EXTENDED As Byte            ' Extended function
        'FUN_QUERY_SETUP_DATA As Byte    ' Query for particular data type/value
        'FUN_INIT_BASE_DEF As Byte       ' Initialize base def from backplane
        'FUN_DATA_BROADCAST As Byte      ' Broadcast to a particular data type
        'FUN_CCM_REQUEST As Byte         ' Perform CCM Request
        'FUN_KSEQ_REQUEST As Byte        ' Perform a KSequence Request
        'FUN_BACKPLANE_REQUEST As Byte   ' Perform backplane request
        'FUN_WRITE_BASE_DEF As Byte      ' Write Base Def 

        'bit fields in Byte 4
        'FUN_EXTEND_RESPONSE As Byte	 ' Extends the response packet. 
        'FUN_ACK As Byte			     ' Acknowledge 
        'FUN_NAK As Byte			     ' NOT Acknowledge 
        'FUN_RESPONSE As Byte			 ' Response 
        'FUN_SERIAL_PORT As Byte		 ' Execute serial port function (see below) 
        'FUN_WRITE_MEMORY As Byte		 ' Write a particular memory type 
        'FUN_READ_MEMORY As Byte		 ' Read a particular memory type 
        'FUN_ENUM_MEMORY As Byte		 ' Get list of all memory types 

        'bit fields in Byte 5
        'FUN_READ_SHARED_RAM As Byte	 ' Read shared ram 
        'FUN_WRITE_SHARED_RAM As Byte	 ' Write shared ram 
        'FUN_ACCESS_MEMORY As Byte		 ' Access (read/write) multiple memory types 
        'FUN_COMM_RESPONSE As Byte		 ' Response to PLC generated COMM request 
        'FUN_COMM_REQ_ACK As Byte		 ' Function from PLC generated COMM request 
        'FUN_WRITE_IO_NO_READ As Byte	 ' Write IO base with returned read 
        'FUN_COMM_NO_REQ_ACK As Byte	 ' Function from PLC generated COMM request 
        'FUN_RUN_PROGRAM As Byte		 ' Function to execute a program 

        'bit fields in Byte 6
        'FUN_REMOTE_API	As Byte 	     ' Function to execute a function on remote device 
        'FUN_NOTIFY	As Byte 	         ' Indicates a notification 
        'FUN_COMPLETION As Byte 	     ' Indicates completion of some activity 
        'FUN_SET_OS_LOAD As Byte 	     ' Set Load OS Parm. 
        'FUN_REBOOT As Byte 	         ' Reboot OS			
        'FUN_EXTEND_RESPONSE_FIX As Byte ' Fixed version that extends the response packet. 
        'NEW_STYLE_IO As Byte	         ' This device supports new style I/O requests 
        'HOT_SWAP As Byte	             ' True if this device supports hot swap 

        'bit fields in Byte 7
        'TCP_IP	As Byte                  ' TRUE if this device supports TCP/IP 
        'HTTP As Byte                    ' TRUE if this device supports HTTP 
        'Reserved (next 6 bits)		

        'UnusedBytes[9] as Byte			 ' Unused 
        Public Sub Initialize()
            ReDim UnusedBytes(15)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure SerialSetup
        Dim BaudRate As Int32
        Dim ConfigData As Byte
        'bit fields in Byte 1
        'StopBits 	                    ' 0 == 1 Stop bit;  1 == 2 Stop bits 
        'DataBits	                    ' 0 == 7 Data bits;  1 == 8 Data bits */
        'Parity		                    ' 2 bits; 0 == 1 == None; 2 == Odd;  3 == Even 
        'Mode		                    ' 0 == Slave;  1 == Master/Proxy 
        'UseRTS		                    ' 0 == Don't use RTS line;  1 == Use RTS line 
        'Reserved	                    ' last two bits reserved 
        Dim PreTransmitDelay As Byte    ' If UseRTS == 1 delay this many ms (times 2) before starting transmit 
        Dim PostTransmitDelay As Byte   ' If UseRTS == 1 delay this many ms (times 2) after ending transmit 
        Dim Unused As Byte
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure VersionDef
        Dim MajorVersion As Byte
        Dim MinorVersion As Byte
        Dim BuildVersion As Int16
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure VersionInfoDef
        Dim SizeofVersionInfo As Byte
        Dim BootVersion As VersionDef
        Dim OSVersion As VersionDef
        Dim NumOSExtensions As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=40)> _
        Dim OSExt() As Byte

        Public Sub Initialize()
            ReDim OSExt(39)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure DeviceDef
        'PLCFamily As Byte               ' 1= HA, 2= 205, 3= 305, 4= 405
        'Unused1 As Byte
        'ModuleType As Byte              ' 0= EBC, 1= ECOM, &H80= Fiber, 3= Drive, 4= ERM, 6= AVG
        'StatusCode As Byte
        'EthernetAddress(5) As Byte      ' Hardware Ethernet Address
        'RamSize As Int16                ' in K-byte increments
        'FlashSize As Int16              ' in K-byte increments
        'DipSettings As Byte             ' setting of the 8 element dip switch
        'MediaType As Byte               ' 0= 10Base-T,1= 10Base-F
        'EPFCount As int32               ' Early Power Fail Count - 405 only
        'Status As Byte                  '  Status:1 = Run Relay Status
        '                                '  Status:2 = Battery Low Indicator
        '                                '  Status:6 = unused bits
        'BattRamSize as Int32            ' Size in K-Bytes of battery-backed ram */
        'ExtraDIPS As Byte               ' Extra Dip switches on Terminator EBC's */
        'ModelNumber As Byte             ' 
        'EtherSpeed As Byte              ' 0=10MBit; 1=100MBit */
        'PLDRev[2] As Byte 
        'unused(14) As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=42)> _
        Dim device() As Byte

        Public Sub Initialize()
            ReDim device(41)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure LinkMonitor
        'Timeout As Int32                ' 0= don't use Link Monitor
        'Mode As Byte                    ' 0= Clear Outputs
        '                                ' 1= Set outputs to given I/O data pattern
        'Data(251) as Byte 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=256)> _
        Dim Data() As Byte ' Pattern, same format as used for HEIWriteIO

        Public Sub Initialize()
            ReDim Data(255)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure MemoryTypeDef
        Dim Type As Int16           'Type of memory
        Dim MemSize As Int32        'Size of memory
        Dim UnitSize As Int32       ' 0 = DWORD, 1 = BYTE, 2 = WORD, 4 = DWORD 
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
        Dim unused() As Integer

        Public Sub Initialize()
            ReDim unused(2)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure Encryption
        Dim Algorithm As Byte ' Algorithm to use for encryption: 0= No encryption, 1= private key encryption
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
        Dim Unused1() As Byte ' Reserved
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=60)> _
        Dim Key() As Byte ' Encryption key (null terminated)

        Public Sub Initialize()
            ReDim Unused1(2)
            ReDim Key(59)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure EnetAddress
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)> _
        Dim Address() As Byte

        Public Sub Initialize()
            ReDim Address(19)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure HEITransport
        Dim Transport As Int16
        Dim Protocol As Int16
        Dim Encrypt As Encryption
        'Dim SourceAddress As EnetAddress
        Dim SourceAddress As IntPtr
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=48)> _
        Dim Reserved() As Byte

        Public Sub Initialize()
            Encrypt.Initialize()
            'SourceAddress.Initialize()
            ReDim Reserved(47)
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Friend Structure HEIDevice
        'union
        '{
        '   ' Use this for HEIP_HOST protocol addressing
        '   struct
        '   {
        '       int16 Family;               ' AF_UNSPEC == 0
        '       char Nodenum[6];            ' Ethernet network address
        '       unsigned int16 LANNum;      ' Lana number
        '   }  AddressHost;
        '
        '   ' Use this for HEIP_IPX protocol addressing
        '   struct
        '   {
        '       int16 Family;               ' AF_IPX == 6
        '       char Netnum[4];             ' Network Number
        '       char Nodenum[6];            ' Ethernet network address
        '       unsigned int16 Socket;      ' Socket Number == 0x7070
        '   } AddressIPX;
        '
        '   ' Use this for HEIP_IP protocol addressing
        '   struct
        '   {
        '       int16 Family;               ' AF_INET == 2
        '       unsigned int16 Port;        ' Port number == 0x7070
        '       union
        '       {
        '           struct { unsigned char b1, b2, b3, b4; } bAddr;  ' Byte Addressing
        '           struct { unsigned int16 w1, w2; } wAddr;         ' Word addressing
        '           unsigned long lAddr;                             ' DWord addressing
        '       } AddressingType;
        '       char Zero[8];               ' initialize to zeros
        '   } AddressIP;
        '
        '   struct
        '   {
        '       Byte    Commport;
        '       Byte    ByteSize;
        '       Byte    Parity;
        '       Byte    StopBits;
        '       Long    Baud;
        '       Long    *hLocal;
        '   } AddressSerial;
        '   BYTE Raw[20];
        '}Address
        '

        'Address(77) As Byte             ' 78-byte byte array (VB packs on 4-byte boundaries)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=126)> _
       Dim Address() As Byte ' 122-byte byte array (VB packs on 4-byte boundaries)

        Public Sub Initialize()
            ReDim Address(125)
        End Sub
    End Structure

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Host Ethernet APIs
    '
    <DllImport(MYDLLLONG, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIOpen(ByVal HEIAPIVersion As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIClose() As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIOpenTransport(ByVal pTransport As IntPtr, ByVal HEIAPIVersion As Int16, ByVal EnetAdress As Integer) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEICloseTransport(ByVal pTransport As IntPtr) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIGetQueryTimeout() As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEISetQueryTimeout(ByVal NewTimeout As Integer) As Integer
        'sets new query timeout value and returns old query timeout value
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIQueryDevices(ByVal pTransport As IntPtr, <[In](), Out()> ByVal pDevice() As HEIDevice, ByRef pNumDevices As Integer, ByVal HEIAPIVersion As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIQueryDeviceData(ByVal pTransport As IntPtr, ByRef pDevice As HEIDevice, ByRef pNumDevices As Integer, ByVal HEIAPIVersion As Int16, ByVal DataType As Int16, ByRef pData As Byte, ByVal SizeOFData As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIOpenDevice(ByVal pTransport As IntPtr, ByRef pDevice As HEIDevice, ByVal HEIAPIVersion As Int16, ByVal Timeout As Int16, ByVal Retrys As Int16, <MarshalAs(UnmanagedType.Bool)> ByVal UseAddressedBroadcast As Boolean) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEICloseDevice(ByRef pDevice As HEIDevice) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadSetupData(ByRef pDevice As HEIDevice, ByVal SetupType As Int16, ByRef pData As Byte, ByRef pSizeOfData As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIWriteSetupData(ByRef pDevice As HEIDevice, ByVal SetupType As Int16, ByRef pData As Byte, ByVal SizeOFData As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIDeleteSetupData(ByRef pDevice As HEIDevice, ByVal SetupType As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIEnumSetupData(ByRef pDevice As HEIDevice, ByRef pData As Int16, ByRef pSizeOFData As Int16) As Integer
    End Function

    'Watchdog
    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIPetDevice(ByRef pDevice As HEIDevice) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadEthernetStats(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByRef SizeOfData As Int16, ByVal Clear As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadDeviceDef(ByRef pDevice As HEIDevice, ByRef pModuleDefInfo As Byte, ByVal SizeOfModuleDefInfo As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadIO(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByRef pDataSize As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIWriteIO(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByVal SizeOFData As Int16, ByRef pReturnData As Byte, ByRef pSizeOfReturnData As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIWriteIONoRead(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByVal SizeOFData As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadModuleStatus(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByRef pSizeOfData As Int16, ByVal Clear As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadVersionInfo(ByRef pDevice As HEIDevice, ByVal pVersionInfo As IntPtr, ByVal pVersionInfoSize As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadSupportInfo(ByRef pDevice As HEIDevice, ByRef pSupportInfo As Byte, ByVal pSupportInfoSize As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadBaseDef(ByRef pDevice As HEIDevice, ByRef pBaseDefInfo As Byte, ByRef pBaseDefInfoSize As Int16) As Integer
    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' H4-EBC only
    '
    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIWriteBaseDef(ByRef pDevice As HEIDevice, ByRef pInputBaseDef As Byte, ByVal SizeOfInputBaseDef As Int16, ByRef OutputBaseDef As Byte, ByRef pOutputBaseDefSize As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIInitBaseDef(ByRef pDevice As HEIDevice, ByRef pBaseDefInfo As Byte, ByRef pSizeOfBaseDefInfo As Int16) As Integer
    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Terminator I/O specific APIs
    '
    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIRescanBase(ByRef pDevice As HEIDevice, ByVal RescanFlags As Int32, ByRef pBaseDefInfo As Byte, ByRef pSizeOfBaseDefInfo As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIReadConfigData(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByRef pDataSize As Int16) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIWriteConfigData(ByRef pDevice As HEIDevice, ByRef pData As Byte, ByVal DataSize As Int16, ByRef pReturnData As Byte, ByRef pReturnDataSize As Int16) As Integer
        'NOTE: You can also Use HEIWriteIO to write config data using type DF_CONFIG
    End Function

    'Mutiple Packet Stuff
    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIGetResponse(ByRef pDevice As HEIDevice, ByRef pResponse As Byte, ByRef pResponseSize As Int16, ByVal CheckAppVal As Int16) As Integer
    End Function

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ECOM Specific APIs
    '
    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEICCMRequest(ByRef pDevice As HEIDevice, ByVal Write As Int16, ByVal DataType As Byte, ByVal Address As Int16, ByVal DataLen As Int16, ByRef pData As Byte) As Integer
    End Function

    <DllImport(MYDLLSHORT, _
    CallingConvention:=CallingConvention.Cdecl)> _
    Friend Function _
    HEIKSEQRequest(ByRef pDevice As HEIDevice, ByVal DataLen As Int16, ByRef pData As Byte, ByRef pReturnDataLen As Int16) As Integer
    End Function
End Module
