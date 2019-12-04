Imports System.Runtime.InteropServices

Namespace HookTest
    Friend Class clsHook
        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function MessageBoxW(ByVal hWnd As Integer, ByVal [Text] As String, ByVal Caption As String, ByVal uType As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True)> _
        Private Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As UIntPtr
        End Function

        <DllImport("kernel32.dll")> _
        Private Shared Function VirtualProtect(ByVal lpAddress As IntPtr, ByVal dwSize As UIntPtr, ByVal flNewProtect As UInt32, <Out> ByRef lpflOldProtect As UInt32) As Boolean
        End Function

        <UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError:=True)> _
        Public Delegate Function MBAH(ByVal hWnd As Integer, ByVal [Text] As String, ByVal Caption As String, ByVal uType As Integer) As Integer

        Private Shared Function InjectHook(ByVal arg As String) As Integer
            Try
                Dim pAddr As Integer = GetProcAddress(GetModuleHandle("user32"), "MessageBoxA")
                Dim functionPointerForDelegate As Integer = CInt(Marshal.GetFunctionPointerForDelegate(New MBAH(AddressOf clsHook.hook)))
                Dim lpflOldProtect As UInt32 = 0
                clsHook.VirtualProtect(pAddr, 6, &H40, lpflOldProtect)
                Dim num3 As Integer = ((functionPointerForDelegate - pAddr) - 5)
                Dim bytes As Byte() = BitConverter.GetBytes(num3)
                Dim source As Byte() = New Byte() {&HE9, bytes(0), bytes(1), bytes(2), bytes(3)}
                Marshal.Copy(source, 0, pAddr, 5)
                Return 1
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function hook(ByVal hWnd As Integer, ByVal [Text] As String, ByVal Caption As String, ByVal uType As Integer) As Integer
            Return clsHook.MessageBoxW(hWnd, ([Text] & " - VB.NET Hook"), "Hook", uType)
        End Function
    End Class
End Namespace


