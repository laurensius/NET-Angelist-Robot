Imports System.Windows.Forms
Imports System.IO.Ports

Public Class Form_Kendali_Robot

    Dim port_name As String()

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = Keys.Up Then
            Me.lblInstruction.Text = "Maju"
            serialPort.Write("1")
            Return True
        End If
        If keyData = Keys.Down Then
            Me.lblInstruction.Text = "Mundur"
            serialPort.Write("3")
            Return True
        End If
        If keyData = Keys.Left Then
            Me.lblInstruction.Text = "Kiri"
            serialPort.Write("4")
            Return True
        End If
        If keyData = Keys.Right Then
            Me.lblInstruction.Text = "Kanan"
            serialPort.Write("2")
            Return True
        End If
        If keyData = Keys.Space Then
            Me.lblInstruction.Text = "Stop"
            serialPort.Write("5")
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function


    Private Sub Form_Kendali_Robot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        inisialisasi()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles timerSerial.Tick
        webBrowserCamera.Navigate(txtIPCam.Text.ToString)
    End Sub

    Private Sub inisialisasi()
        If serialPort.IsOpen Then
            serialPort.Close()
        End If
        timerSerial.Stop()
        webBrowserCamera.Stop()
        cmbComPort.Items.Clear()
        cmbComPort.Enabled = True
        txtIPCam.Enabled = True
        lblInstruction.Text = "Unavailable"
        lblResponse.Text = "Unavailable"
        btnConnect.Text = "Connect"
        Load_COM_List()
    End Sub

    Private Sub onConnected()
        timerSerial.Start()
        cmbComPort.Enabled = False
        txtIPCam.Enabled = False
        btnConnect.Text = "Disconnect"
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If (btnConnect.Text.ToString = "Connect") Then
            Buka_COM()

        Else
            inisialisasi()
        End If
    End Sub

    Private Sub Load_COM_List()
        port_name = IO.Ports.SerialPort.GetPortNames()
        For Each comp1 In port_name
            cmbComPort.Items.Add(comp1)
        Next
        cmbComPort.SelectedIndex = 0
    End Sub

    Private Sub Buka_COM()
        Try
            serialPort.PortName = cmbComPort.Text
            serialPort.BaudRate = 9600
            serialPort.StopBits = IO.Ports.StopBits.One
            serialPort.Parity = IO.Ports.Parity.None
            serialPort.DataBits = 8
            serialPort.Open()
            onConnected()
        Catch ex As Exception
            MsgBox("Terjadi kedala saat menghubungkan ke" & cmbComPort.Text)
            inisialisasi()
        End Try    
    End Sub



End Class
