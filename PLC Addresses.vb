Public Class PLC_Addresses
    Private Sub PLC_Addresses_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Update NumberofDevicesNumericUpDown
        NumberOfDevicesUpDown.Value = NumberOfDevices

        'Update IP and name
        Octet1NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 0)
        Octet2NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 1)
        Octet3NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 2)
        Octet4NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 3)
    End Sub


    Private Sub NumberOfDevicesUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumberOfDevicesUpDown.ValueChanged
        NumberOfDevices = NumberOfDevicesUpDown.Value
    End Sub

    Private Sub PLCToChangeNumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles PLCToChangeNumericUpDown.ValueChanged
        'When the value of PLCtoChangeNumericUpDown changes, update the shown address and name
        Octet1NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 0)
        Octet2NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 1)
        Octet3NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 2)
        Octet4NumericUpDown.Value = DeviceAddress(PLCToChangeNumericUpDown.Value, 3)
    End Sub

    'When the address values or name are changed, update the address and name arrays
    Private Sub Octet1NumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles Octet1NumericUpDown.ValueChanged
        DeviceAddress(PLCToChangeNumericUpDown.Value, 0) = Octet1NumericUpDown.Value
    End Sub
    Private Sub Octet2NumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles Octet2NumericUpDown.ValueChanged
        DeviceAddress(PLCToChangeNumericUpDown.Value, 1) = Octet2NumericUpDown.Value
    End Sub
    Private Sub Octet3NumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles Octet3NumericUpDown.ValueChanged
        DeviceAddress(PLCToChangeNumericUpDown.Value, 2) = Octet3NumericUpDown.Value
    End Sub
    Private Sub Octet4NumericUpDown_ValueChanged(sender As System.Object, e As System.EventArgs) Handles Octet4NumericUpDown.ValueChanged
        DeviceAddress(PLCToChangeNumericUpDown.Value, 3) = Octet4NumericUpDown.Value
    End Sub
End Class