Imports System.Runtime.InteropServices

' Shell32 API 임포트
Public Class Shell32
    Public Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
    Public Const FILE_ATTRIBUTE_NORMAL As Integer = &H80

    <StructLayout(LayoutKind.Sequential)>
    Public Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    Public Enum SHGFI
        Icon = &H100
        DisplayName = &H200
        TypeName = &H400
        Attributes = &H800
        IconLocation = &H1000
        ExeType = &H2000
        SysIconIndex = &H4000
        LinkOverlay = &H8000
        Selected = &H10000
        Attr_Specified = &H20000
        LargeIcon = &H0
        SmallIcon = &H1
        OpenIcon = &H2
        ShellIconSize = &H4
        PIDL = &H8
        UseFileAttributes = &H10
        AddOverlays = &H20
        OverlayIndex = &H40
        FolderMask = &H4000
    End Enum

    <DllImport("shell32.dll")>
    Public Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As Integer,
                                       ByRef psfi As SHFILEINFO, cbSizeFileInfo As Integer,
                                       uFlags As SHGFI) As IntPtr
    End Function
End Class

' User32 API 임포트
Public Class User32
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function
End Class
