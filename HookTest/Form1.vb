Public Class Form1
    Private Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" (ByVal lpLibFileName As String) As Long

    Declare Function MessageBox Lib "user32" Alias "MessageBoxA" (ByVal hwnd As Integer, ByVal lpText As String, ByVal lpCaption As String, ByVal wType As Integer) As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'MessageBox.Show("原始")
        MessageBox(IntPtr.Zero, "Text", "Caption", 0)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLibrary(Application.StartupPath & "\iDll.dll")
    End Sub
End Class
