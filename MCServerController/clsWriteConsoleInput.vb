Public Class clsWriteConsoleInput
    Private Declare Function AttachConsole Lib "kernel32.dll" (ByVal dwProcessId As Int32) As Boolean
    Private Declare Function FreeConsole Lib "kernel32.dll" () As Boolean

    Private Structure KEY_EVENT_RECORD
        Dim bKeyDown As Integer
        Dim wRepeatCount As Short
        Dim wVirtualKeyCode As Short
        Dim wVirtualScanCode As Short
        Dim UnicodeChar As UShort
        Dim dwControlKeyState As Integer
    End Structure

    Private Structure INPUT_RECORD
        Dim EventType As Short
        Dim KeyEvent As KEY_EVENT_RECORD
    End Structure

    Private Declare Function GetStdHandle Lib "kernel32" (ByVal nStdHandle As Integer) As Integer
    Private Declare Function WriteConsoleInput Lib "kernel32" Alias "WriteConsoleInputW" (ByVal hConsoleInput As Integer, ByVal lpBuffer() As INPUT_RECORD, ByVal nLength As Integer, ByRef lpNumberOfEventsWritten As Integer) As Integer

    Private Const KEY_EVENT As Integer = 1S
    Private Const STD_INPUT_HANDLE As Integer = -10

    Public Sub KeyIn(ByVal s As String, ByVal pid As Integer)
        AttachConsole(pid)

        Dim lpBuffer() As INPUT_RECORD
        Dim lpNumberOfEventsWritten As Integer
        Dim hConsoleInput As Integer = GetStdHandle(STD_INPUT_HANDLE)
        ReDim lpBuffer(Len(s) * 2 - 1)
        For k As Integer = 0 To UBound(lpBuffer)
            lpBuffer(k).EventType = KEY_EVENT
            lpBuffer(k).KeyEvent.bKeyDown = (k + 1) Mod 2
            lpBuffer(k).KeyEvent.wRepeatCount = 0
            lpBuffer(k).KeyEvent.wVirtualScanCode = 0
            lpBuffer(k).KeyEvent.wVirtualKeyCode = 0
            lpBuffer(k).KeyEvent.UnicodeChar = AscW(Mid(s, 1 + (k \ 2), 1))
            lpBuffer(k).KeyEvent.dwControlKeyState = 0
        Next
        WriteConsoleInput(hConsoleInput, lpBuffer, UBound(lpBuffer) + 1, lpNumberOfEventsWritten)

        FreeConsole()
    End Sub

End Class
