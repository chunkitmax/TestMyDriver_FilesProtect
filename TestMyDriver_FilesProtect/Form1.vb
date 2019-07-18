Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Win32.SafeHandles
Imports FilesProtect_Dll.FileProtect

Public Class Form1

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SSDT_Hook_Off()
        'MsgBox("Call StopSys, Return : " & StopSys("MyDriver"))
        'MsgBox("Call UnInstallSys, Return : " & UnInstallSys("MyDriver"))
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim Path As String = "C:\DbgView\MyDriver.sys"
        Dim Path As String = My.Application.Info.DirectoryPath & "\Resources\MyDriver.sys"
        MsgBox(Path)
        Dim InstallRet As Boolean = InstallSys("MyDriver", "MyDriver", Path)
        Dim StartRet As Boolean = StartSys("MyDriver")
        If InstallRet AndAlso StartRet Then SSDT_Hook_Init()
        MsgBox("Turn SSDT_Hook On! Return : " & SSDT_Hook_On())
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim show As DialogResult = OpenFileDialog1.ShowDialog
        If show = Windows.Forms.DialogResult.OK Then
            Dim b = OpenFileDialog1.FileName
            MsgBox(b.Substring(0, b.IndexOf(":") + 1))
            MsgBox(b.Substring(b.IndexOf(":") + 1))
            添加防刪文件(b.Substring(0, b.IndexOf(":") + 1), b.Substring(b.IndexOf(":") + 1))
            ListBox1.Items.Add(b)
            OpenFileDialog1.Reset()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim show As DialogResult = FolderBrowserDialog1.ShowDialog
        If show = Windows.Forms.DialogResult.OK Then
            Dim b = FolderBrowserDialog1.SelectedPath
            MsgBox(b.Substring(0, b.IndexOf(":") + 1))
            MsgBox(b.Substring(b.IndexOf(":") + 1))
            添加防刪資料夾(b.Substring(0, b.IndexOf(":") + 1), b.Substring(b.IndexOf(":") + 1))
            ListBox1.Items.Add(b)
            FolderBrowserDialog1.Reset()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex >= 0 Then
            用文件路徑移除防刪文件(CType(ListBox1.SelectedItem, String).Substring(CType(ListBox1.SelectedItem, String).IndexOf(":") + 1))
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        Else
            MsgBox("沒有選取目標")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex >= 0 Then
            移除防刪資料夾(CType(ListBox1.SelectedItem, String).Substring(CType(ListBox1.SelectedItem, String).IndexOf(":") + 1))
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        Else
            MsgBox("沒有選取目標")
        End If
    End Sub

End Class
