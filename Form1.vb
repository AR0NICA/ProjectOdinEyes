Imports System.DirectoryServices
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Linq
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Text

Public Class frmMain
    ' 기본 노트 저장 경로
    Private ReadOnly NotesDirectory As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OdinEyes")

    ' 현재 열린 노트의 파일 경로
    Private CurrentNotePath As String = ""

    ' 노트 메타데이터 저장 (태그, 백링크 등)
    Private Structure NoteMetadata
        Public Tags As List(Of String)
        Public Backlinks As List(Of String)
    End Structure

    Private NoteMetadataDict As New Dictionary(Of String, NoteMetadata)

    ' 마크다운 패턴
    Private ReadOnly LinkPattern As New Regex("\[\[([^\]]+)\]\]", RegexOptions.Compiled)

    Public Sub New()
        ' 디자이너 생성 코드
        InitializeComponent()

        ' 폴더가 없으면 생성
        If Not Directory.Exists(NotesDirectory) Then
            Directory.CreateDirectory(NotesDirectory)
        End If

        ' 이미지 리스트 설정
        SetupImageList()

        ' 이벤트 핸들러 연결
        AddEventHandlers()

        ' 트리뷰 초기화
        LoadNotesTree()
    End Sub

    Private Sub SetupImageList()
        ' 이미지 리스트에 아이콘 추가 (폴더와 파일 아이콘)
        Dim folderIconPath As String = Path.Combine(Application.StartupPath, "Resources", "folder.ico")
        Dim fileIconPath As String = Path.Combine(Application.StartupPath, "Resources", "file.ico")

        ' 리소스에서 아이콘 로드하거나 기본 아이콘 사용
        Try
            If File.Exists(folderIconPath) Then
                ImageList1.Images.Add("folder", Image.FromFile(folderIconPath))
            Else
                ImageList1.Images.Add("folder", GetFileIcon("C:\", False).ToBitmap())
            End If

            If File.Exists(fileIconPath) Then
                ImageList1.Images.Add("file", Image.FromFile(fileIconPath))
            Else
                ImageList1.Images.Add("file", GetFileIcon("*.txt", True).ToBitmap())
            End If
        Catch ex As Exception
            MessageBox.Show("아이콘 로드 중 오류 발생: " & ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' 기본 아이콘 사용
            Try
                ' 대체 방법으로 아이콘 가져오기
                Dim folderIcon As Icon = GetFileIcon("C:\", False)
                Dim fileIcon As Icon = GetFileIcon("*.txt", True)

                ImageList1.Images.Add("folder", folderIcon.ToBitmap())
                ImageList1.Images.Add("file", fileIcon.ToBitmap())
            Catch innerEx As Exception
                ' 마지막 대안으로 기본 아이콘 생성
                Using blankIcon As New Bitmap(16, 16)
                    Using g As Graphics = Graphics.FromImage(blankIcon)
                        g.Clear(Color.LightYellow) ' 폴더 색상
                    End Using
                    ImageList1.Images.Add("folder", blankIcon)

                    Using blankFileIcon As New Bitmap(16, 16)
                        Using g As Graphics = Graphics.FromImage(blankFileIcon)
                            g.Clear(Color.White) ' 파일 색상
                        End Using
                        ImageList1.Images.Add("file", blankFileIcon)
                    End Using
                End Using
            End Try
        End Try
    End Sub

    ' 파일이나 폴더의 아이콘을 가져오는 함수
    Private Function GetFileIcon(Path As String, isFile As Boolean) As Icon
        Dim shinfo As New Shell32.SHFILEINFO()
        Dim flags As Shell32.SHGFI

        If isFile Then
            flags = Shell32.SHGFI.Icon Or Shell32.SHGFI.SmallIcon Or Shell32.SHGFI.UseFileAttributes
        Else
            flags = Shell32.SHGFI.Icon Or Shell32.SHGFI.SmallIcon Or Shell32.SHGFI.UseFileAttributes Or Shell32.SHGFI.FolderMask
        End If

        Shell32.SHGetFileInfo(Path, If(isFile, Shell32.FILE_ATTRIBUTE_NORMAL, Shell32.FILE_ATTRIBUTE_DIRECTORY),
                         shinfo, Marshal.SizeOf(shinfo), flags)

        If shinfo.hIcon <> IntPtr.Zero Then
            Dim icon As Icon = Icon.FromHandle(shinfo.hIcon).Clone()
            User32.DestroyIcon(shinfo.hIcon)
            Return icon
        End If

        ' 아이콘을 가져오지 못한 경우 기본 아이콘 반환
        Return If(isFile, SystemIcons.WinLogo, SystemIcons.Application)
    End Function

    Private Sub AddEventHandlers()
        ' 트리뷰 이벤트
        AddHandler tvNotes.NodeMouseDoubleClick, AddressOf tvNotes_NodeMouseDoubleClick
        AddHandler tvNotes.AfterSelect, AddressOf tvNotes_AfterSelect

        ' 편집기 이벤트
        AddHandler txtEditor.TextChanged, AddressOf txtEditor_TextChanged

        ' 태그 이벤트
        AddHandler txtTags.Leave, AddressOf txtTags_Leave
        AddHandler txtTags.KeyDown, AddressOf txtTags_KeyDown

        ' 메뉴 이벤트
        AddHandler NewNoteToolStripMenuItem.Click, AddressOf NewNote_Click
        AddHandler OpenToolStripMenuItem.Click, AddressOf OpenNote_Click
        AddHandler SaveToolStripMenuItem.Click, AddressOf SaveNote_Click
        AddHandler ExitToolStripMenuItem.Click, AddressOf Exit_Click

        ' 컨텍스트 메뉴 이벤트
        AddHandler NewNoteToolStripMenuItem1.Click, AddressOf NewNote_Click
        AddHandler NewFolderToolStripMenuItem.Click, AddressOf NewFolder_Click
        AddHandler DeleteToolStripMenuItem.Click, AddressOf Delete_Click
        AddHandler RenameToolStripMenuItem.Click, AddressOf Rename_Click

        ' 도구 모음 이벤트
        AddHandler btnBold.Click, AddressOf btnBold_Click
        AddHandler btnItalic.Click, AddressOf btnItalic_Click
        AddHandler btnHeader.Click, AddressOf btnHeader_Click
        AddHandler btnLink.Click, AddressOf btnLink_Click
        AddHandler btnList.Click, AddressOf btnList_Click

        ' 탭 이벤트
        AddHandler TabControl1.SelectedIndexChanged, AddressOf TabControl1_SelectedIndexChanged

        ' 검색 이벤트
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        AddHandler txtSearch.KeyDown, AddressOf txtSearch_KeyDown

        ' 툴바 이벤트 핸들러 추가
        AddHandler btnUndo.Click, AddressOf btnUndo_Click
        AddHandler btnRedo.Click, AddressOf btnRedo_Click
        AddHandler btnClearFormat.Click, AddressOf btnClearFormat_Click
        AddHandler btnStrikethrough.Click, AddressOf btnStrikethrough_Click
        AddHandler btnUnderline.Click, AddressOf btnUnderline_Click
        AddHandler btnHighlight.Click, AddressOf btnHighlight_Click
        AddHandler btnTable.Click, AddressOf btnTable_Click

        ' 드롭다운 메뉴 이벤트 핸들러
        AddHandler miCheckList.Click, AddressOf miCheckList_Click
        AddHandler miNumberedList.Click, AddressOf miNumberedList_Click
        AddHandler miBulletList.Click, AddressOf miBulletList_Click
        AddHandler miIndent.Click, AddressOf miIndent_Click
        AddHandler miUnindent.Click, AddressOf miUnindent_Click

        AddHandler miAlignLeft.Click, AddressOf miAlignLeft_Click
        AddHandler miAlignCenter.Click, AddressOf miAlignCenter_Click
        AddHandler miAlignRight.Click, AddressOf miAlignRight_Click
        AddHandler miAlignJustify.Click, AddressOf miAlignJustify_Click

        AddHandler btnTextColor.Click, AddressOf btnTextColor_Click
        AddHandler btnBackColor.Click, AddressOf btnBackColor_Click

    End Sub

    Private Sub LoadNotesTree()
        ' 트리뷰 초기화
        tvNotes.Nodes.Clear()

        ' 루트 노드 생성
        Dim rootNode As New TreeNode("노트") With {.Tag = NotesDirectory, .ImageKey = "folder", .SelectedImageKey = "folder"}
        tvNotes.Nodes.Add(rootNode)

        ' 디렉토리에서 파일 및 폴더 로드
        LoadDirectoryNodes(rootNode, NotesDirectory)

        ' 노트 메타데이터 로드 (태그, 백링크 등)
        LoadAllNoteMetadata()

        ' 루트 노드 확장
        rootNode.Expand()
    End Sub

    Private Sub LoadDirectoryNodes(ByVal parentNode As TreeNode, ByVal directoryPath As String)
        ' 하위 디렉토리 로드
        For Each subDirectory As String In System.IO.Directory.GetDirectories(directoryPath)
            Dim dirInfo As New DirectoryInfo(subDirectory)
            Dim dirNode As New TreeNode(dirInfo.Name) With {.Tag = subDirectory, .ImageKey = "folder", .SelectedImageKey = "folder"}
            parentNode.Nodes.Add(dirNode)
            LoadDirectoryNodes(dirNode, subDirectory)
        Next

        ' 노트 파일 로드 (.md 확장자만)
        For Each noteFile As String In System.IO.Directory.GetFiles(directoryPath, "*.md")
            Dim fileInfo As New FileInfo(noteFile)
            Dim fileNode As New TreeNode(Path.GetFileNameWithoutExtension(fileInfo.Name)) With {.Tag = noteFile, .ImageKey = "file", .SelectedImageKey = "file"}
            parentNode.Nodes.Add(fileNode)
        Next
    End Sub

    Private Sub LoadAllNoteMetadata()
        NoteMetadataDict.Clear()

        ' 모든 .md 파일 스캔
        For Each noteFile As String In System.IO.Directory.GetFiles(NotesDirectory, "*.md", SearchOption.AllDirectories)
            Dim content As String = System.IO.File.ReadAllText(noteFile)

            ' 태그 추출 (YAML 프론트매터 또는 태그 형식)
            Dim tags As New List(Of String)
            Dim tagMatch As Match = Regex.Match(content, "tags:\s*(.*?)(?:\r?\n|\r|$)", RegexOptions.IgnoreCase)

            If tagMatch.Success Then
                ' YAML 형식의 태그 추출
                Dim tagString As String = tagMatch.Groups(1).Value.Trim()
                For Each tag As String In tagString.Split(","c)
                    tags.Add(tag.Trim())
                Next
            End If

            ' 해시태그 형식 추출 (#태그)
            Dim hashTagMatches As MatchCollection = Regex.Matches(content, "#(\w+)")
            For Each m As Match In hashTagMatches
                tags.Add(m.Groups(1).Value)
            Next

            ' 백링크 추출 ([[링크명]])
            Dim backlinks As New List(Of String)
            Dim linkMatches As MatchCollection = LinkPattern.Matches(content)
            For Each m As Match In linkMatches
                backlinks.Add(m.Groups(1).Value)
            Next

            ' 메타데이터 저장
            Dim metadata As New NoteMetadata With {
                .Tags = tags.Distinct().ToList(),
                .Backlinks = backlinks
            }

            NoteMetadataDict(noteFile) = metadata
        Next

        ' 백링크 관계 업데이트
        UpdateBacklinks()
    End Sub

    Private Sub UpdateBacklinks()
        ' 모든 파일을 스캔하여 백링크 관계 업데이트
        For Each sourceFile As String In NoteMetadataDict.Keys
            Dim sourceFileName As String = Path.GetFileNameWithoutExtension(sourceFile)

            ' 현재 파일이 링크하는 모든 대상 파일 찾기
            For Each targetFile As String In NoteMetadataDict.Keys
                Dim targetFileName As String = Path.GetFileNameWithoutExtension(targetFile)

                ' 소스 파일의 백링크 목록에 대상 파일 이름이 있는지 확인
                If NoteMetadataDict(sourceFile).Backlinks.Contains(targetFileName) Then
                    ' 대상 파일의 백링크에 소스 파일 추가
                    If NoteMetadataDict.ContainsKey(targetFile) Then
                        Dim targetMetadata As NoteMetadata = NoteMetadataDict(targetFile)
                        If Not targetMetadata.Backlinks.Contains(sourceFileName) Then
                            targetMetadata.Backlinks.Add(sourceFileName)
                            NoteMetadataDict(targetFile) = targetMetadata
                        End If
                    End If
                End If
            Next
        Next
    End Sub


#Region "이벤트 핸들러"

    Private Sub tvNotes_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)
        If e.Node IsNot Nothing AndAlso TypeOf e.Node.Tag Is String Then
            Dim filePath As String = DirectCast(e.Node.Tag, String)

            If System.IO.File.Exists(filePath) AndAlso Path.GetExtension(filePath).ToLower() = ".md" Then
                OpenNoteFile(filePath)
            End If
        End If
    End Sub

    Private Sub tvNotes_AfterSelect(sender As Object, e As TreeViewEventArgs)
        If e.Node IsNot Nothing AndAlso TypeOf e.Node.Tag Is String Then
            Dim filePath As String = DirectCast(e.Node.Tag, String)

            If System.IO.File.Exists(filePath) AndAlso Path.GetExtension(filePath).ToLower() = ".md" Then
                OpenNoteFile(filePath)
            End If
        End If
    End Sub

    Private Sub txtEditor_TextChanged(sender As Object, e As EventArgs)
        UpdatePreview()

        ' 저장 상태 표시
        lblStatus.Text = "수정됨"
    End Sub

    Private Sub txtTags_Leave(sender As Object, e As EventArgs)
        SaveTags()
    End Sub

    Private Sub txtTags_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SaveTags()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' 탭 변경 시 처리
        If TabControl1.SelectedTab Is tabPreview Then
            UpdatePreview()
        ElseIf TabControl1.SelectedTab Is tabBacklinks Then
            UpdateBacklinksView()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        SearchNotes(txtSearch.Text)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SearchNotes(txtSearch.Text)
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
#End Region

#Region "도구 모음 이벤트 핸들러"

    Private Sub btnBold_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("**", "**", "굵은 텍스트")
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("*", "*", "기울임 텍스트")
    End Sub

    Private Sub btnHeader_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("# ", "", "제목")
    End Sub

    Private Sub btnLink_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("[[", "]]", "링크명")
    End Sub

    Private Sub btnList_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("- ", "", "목록 항목")
    End Sub
    Private Sub btnUndo_Click(sender As Object, e As EventArgs)
        txtEditor.Undo()
    End Sub

    Private Sub btnRedo_Click(sender As Object, e As EventArgs)
        txtEditor.Redo()
    End Sub

    Private Sub btnClearFormat_Click(sender As Object, e As EventArgs)
        ' 선택된 텍스트에서 마크다운 서식 제거
        Dim selText As String = txtEditor.SelectedText

        ' 볼드체 제거
        selText = Regex.Replace(selText, "\*\*(.*?)\*\*", "$1")

        ' 이탤릭체 제거
        selText = Regex.Replace(selText, "\*(.*?)\*", "$1")

        ' 취소선 제거
        selText = Regex.Replace(selText, "~~(.*?)~~", "$1")

        ' 밑줄 제거
        selText = Regex.Replace(selText, "__(.*?)__", "$1")

        ' 강조 제거
        selText = Regex.Replace(selText, "==(.*?)==", "$1")

        ' 헤더 제거
        selText = Regex.Replace(selText, "^# (.*?)$", "$1", RegexOptions.Multiline)

        ' 수정된 텍스트로 교체
        txtEditor.SelectedText = selText
    End Sub

    Private Sub btnStrikethrough_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("~~", "~~", "취소선 텍스트")
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("__", "__", "밑줄 텍스트")
    End Sub

    Private Sub btnHighlight_Click(sender As Object, e As EventArgs)
        InsertMarkdownSyntax("==", "==", "강조 텍스트")
    End Sub

    Private Sub btnTable_Click(sender As Object, e As EventArgs)
        ' 기본 3x3 테이블 삽입
        Dim tableMarkdown As String =
            "| 제목1 | 제목2 | 제목3 |" & Environment.NewLine &
            "| --- | --- | --- |" & Environment.NewLine &
            "| 내용1 | 내용2 | 내용3 |" & Environment.NewLine &
            "| 내용4 | 내용5 | 내용6 |" & Environment.NewLine

        ' 현재 커서 위치에 삽입
        txtEditor.SelectedText = tableMarkdown
    End Sub

    Private Sub miCheckList_Click(sender As Object, e As EventArgs)
        ApplyListFormat("- [ ] ", "체크리스트 항목")
    End Sub

    Private Sub miNumberedList_Click(sender As Object, e As EventArgs)
        ApplyListFormat("1. ", "번호 항목")
    End Sub

    Private Sub miBulletList_Click(sender As Object, e As EventArgs)
        ApplyListFormat("- ", "글머리 항목")
    End Sub

    Private Sub miIndent_Click(sender As Object, e As EventArgs)
        ' 선택된 각 줄에 들여쓰기 적용
        IndentText(True)
    End Sub

    Private Sub miUnindent_Click(sender As Object, e As EventArgs)
        ' 선택된 각 줄에 내어쓰기 적용
        IndentText(False)
    End Sub

    Private Sub miAlignLeft_Click(sender As Object, e As EventArgs)
        ApplyAlignment(":---")
    End Sub

    Private Sub miAlignCenter_Click(sender As Object, e As EventArgs)
        ApplyAlignment(":---:")
    End Sub

    Private Sub miAlignRight_Click(sender As Object, e As EventArgs)
        ApplyAlignment("---:")
    End Sub

    Private Sub miAlignJustify_Click(sender As Object, e As EventArgs)
        ' 양쪽 정렬은 마크다운에서 표준 지원이 없으므로 
        ' 여기서는 왼쪽 정렬과 동일하게 처리
        ApplyAlignment(":---")
    End Sub

    Private Sub btnTextColor_Click(sender As Object, e As EventArgs)
        ' 색상 선택 대화상자 표시
        Dim colorDialog As New ColorDialog()
        If colorDialog.ShowDialog() = DialogResult.OK Then
            ' 선택된 색상으로 텍스트 래핑
            Dim hexColor As String = String.Format("#{0:X2}{1:X2}{2:X2}", colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)
            InsertMarkdownSyntax("<span style=""color: " & hexColor & """>", "</span>", "색상 텍스트")

            ' 버튼 색상 업데이트
            btnTextColor.ForeColor = colorDialog.Color
        End If
    End Sub

    Private Sub btnBackColor_Click(sender As Object, e As EventArgs)
        ' 색상 선택 대화상자 표시
        Dim colorDialog As New ColorDialog()
        If colorDialog.ShowDialog() = DialogResult.OK Then
            ' 선택된 색상으로 배경색 래핑
            Dim hexColor As String = String.Format("#{0:X2}{1:X2}{2:X2}", colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)
            InsertMarkdownSyntax("<span style=""background-color: " & hexColor & """>", "</span>", "배경색 텍스트")

            ' 버튼 색상 업데이트
            btnBackColor.BackColor = colorDialog.Color
        End If
    End Sub

    ' 리스트 서식 적용 도우미 메서드
    Private Sub ApplyListFormat(prefix As String, placeholder As String)
        Dim selectionStart As Integer = txtEditor.SelectionStart
        Dim selectionLength As Integer = txtEditor.SelectionLength

        If selectionLength > 0 Then
            ' 선택된 텍스트가 있으면 각 라인에 적용
            Dim selectedText As String = txtEditor.SelectedText
            Dim lines As String() = selectedText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

            For i As Integer = 0 To lines.Length - 1
                If Not String.IsNullOrWhiteSpace(lines(i)) Then
                    ' 번호 목록인 경우 순차적으로 번호 증가
                    If prefix = "1. " AndAlso i > 0 Then
                        lines(i) = (i + 1) & ". " & lines(i)
                    Else
                        lines(i) = prefix & lines(i)
                    End If
                End If
            Next

            txtEditor.SelectedText = String.Join(Environment.NewLine, lines)
        Else
            ' 선택된 텍스트가 없으면 현재 라인에 적용
            txtEditor.SelectedText = prefix & placeholder
        End If
    End Sub

    ' 들여쓰기/내어쓰기 적용 메서드
    Private Sub IndentText(isIndent As Boolean)
        Dim selectionStart As Integer = txtEditor.SelectionStart
        Dim selectionLength As Integer = txtEditor.SelectionLength

        If selectionLength > 0 Then
            Dim selectedText As String = txtEditor.SelectedText
            Dim lines As String() = selectedText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

            For i As Integer = 0 To lines.Length - 1
                If isIndent Then
                    ' 들여쓰기 추가
                    lines(i) = "    " & lines(i)
                Else
                    ' 내어쓰기 (탭 또는 4개 공백 제거)
                    If lines(i).StartsWith(vbTab) Then
                        lines(i) = lines(i).Substring(1)
                    ElseIf lines(i).StartsWith("    ") Then
                        lines(i) = lines(i).Substring(4)
                    Else
                        ' 가능한 공백 제거
                        Dim spaceCount As Integer = 0
                        For j As Integer = 0 To Math.Min(4, lines(i).Length) - 1
                            If lines(i)(j) = " "c Then
                                spaceCount += 1
                            Else
                                Exit For
                            End If
                        Next

                        If spaceCount > 0 Then
                            lines(i) = lines(i).Substring(spaceCount)
                        End If
                    End If
                End If
            Next

            txtEditor.SelectedText = String.Join(Environment.NewLine, lines)
        End If
    End Sub

    ' 정렬 적용 메서드
    Private Sub ApplyAlignment(alignMarker As String)
        ' 마크다운에서 정렬은 주로 테이블에서 사용됨
        ' 현재 커서가 테이블 내에 있는지 확인 필요
        ' 간단한 구현을 위해 여기서는 선택된 텍스트를 HTML로 래핑

        Select Case alignMarker
            Case ":---"  ' 왼쪽 정렬
                InsertMarkdownSyntax("<div style=""text-align: left;"">", "</div>", "왼쪽 정렬 텍스트")
            Case ":---:"  ' 가운데 정렬
                InsertMarkdownSyntax("<div style=""text-align: center;"">", "</div>", "가운데 정렬 텍스트")
            Case "---:"  ' 오른쪽 정렬
                InsertMarkdownSyntax("<div style=""text-align: right;"">", "</div>", "오른쪽 정렬 텍스트")
        End Select
    End Sub


#End Region
#Region "메뉴 이벤트 핸들러"

    Private Sub NewNote_Click(sender As Object, e As EventArgs)
        ' 새 노트 생성
        Dim noteName As String = InputBox("새 노트의 이름을 입력하세요:", "새 노트")

        If String.IsNullOrEmpty(noteName) Then
            Return
        End If

        ' 현재 선택된 노드 확인 (폴더인 경우)
        Dim targetPath As String = NotesDirectory
        Dim selectedNode As TreeNode = tvNotes.SelectedNode

        If selectedNode IsNot Nothing AndAlso TypeOf selectedNode.Tag Is String Then
            Dim nodePath As String = DirectCast(selectedNode.Tag, String)

            If System.IO.Directory.Exists(nodePath) Then
                targetPath = nodePath
            ElseIf System.IO.File.Exists(nodePath) Then
                ' 파일이 선택된 경우 부모 디렉토리 사용
                targetPath = Path.GetDirectoryName(nodePath)
            End If
        End If

        ' 파일 경로 생성
        Dim filePath As String = Path.Combine(targetPath, noteName & ".md")

        ' 파일 이미 존재하는지 확인
        If System.IO.File.Exists(filePath) Then
            MessageBox.Show($"'{noteName}.md' 파일이 이미 존재합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 기본 내용으로 파일 생성
        Dim content As String = $"# {noteName}" & Environment.NewLine & Environment.NewLine & "tags: " & Environment.NewLine & Environment.NewLine
        System.IO.File.WriteAllText(filePath, content)

        ' 트리뷰 업데이트
        Dim parentNode As TreeNode

        If selectedNode IsNot Nothing Then
            If System.IO.Directory.Exists(DirectCast(selectedNode.Tag, String)) Then
                parentNode = selectedNode
            Else
                parentNode = selectedNode.Parent
            End If
        Else
            parentNode = tvNotes.Nodes(0)
        End If

        ' 새 노드 추가
        Dim newNode As New TreeNode(noteName) With {.Tag = filePath, .ImageKey = "file", .SelectedImageKey = "file"}
        parentNode.Nodes.Add(newNode)
        parentNode.Expand()
        tvNotes.SelectedNode = newNode

        ' 메타데이터 추가
        NoteMetadataDict(filePath) = New NoteMetadata With {
            .Tags = New List(Of String),
            .Backlinks = New List(Of String)
        }

        ' 파일 열기
        OpenNoteFile(filePath)
    End Sub

    Private Sub NewFolder_Click(sender As Object, e As EventArgs)
        ' 새 폴더 생성
        Dim folderName As String = InputBox("새 폴더의 이름을 입력하세요:", "새 폴더")

        If String.IsNullOrEmpty(folderName) Then
            Return
        End If

        ' 현재 선택된 노드 확인
        Dim targetPath As String = NotesDirectory
        Dim selectedNode As TreeNode = tvNotes.SelectedNode

        If selectedNode IsNot Nothing AndAlso TypeOf selectedNode.Tag Is String Then
            Dim nodePath As String = DirectCast(selectedNode.Tag, String)
            If System.IO.Directory.Exists(nodePath) Then
                targetPath = nodePath
            ElseIf System.IO.File.Exists(nodePath) Then
                ' 파일이 선택된 경우 부모 디렉토리 사용
                targetPath = Path.GetDirectoryName(nodePath)
            End If
        End If

        ' 폴더 경로 생성
        Dim folderPath As String = Path.Combine(targetPath, folderName)

        ' 폴더 이미 존재하는지 확인
        If System.IO.Directory.Exists(folderPath) Then
            MessageBox.Show($"'{folderName}' 폴더가 이미 존재합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 폴더 생성
        System.IO.Directory.CreateDirectory(folderPath)

        ' 트리뷰 업데이트
        Dim parentNode As TreeNode

        If selectedNode IsNot Nothing Then
            If System.IO.Directory.Exists(DirectCast(selectedNode.Tag, String)) Then
                parentNode = selectedNode
            Else
                parentNode = selectedNode.Parent
            End If
        Else
            parentNode = tvNotes.Nodes(0)
        End If

        ' 새 노드 추가
        Dim newNode As New TreeNode(folderName) With {.Tag = folderPath, .ImageKey = "folder", .SelectedImageKey = "folder"}
        parentNode.Nodes.Add(newNode)
        parentNode.Expand()
        tvNotes.SelectedNode = newNode
    End Sub

    Private Sub OpenNote_Click(sender As Object, e As EventArgs)
        ' 파일 선택 대화상자
        Dim ofd As New OpenFileDialog With {
            .Filter = "마크다운 파일 (*.md)|*.md|모든 파일 (*.*)|*.*",
            .InitialDirectory = NotesDirectory
        }
        If ofd.ShowDialog() = DialogResult.OK Then
            OpenNoteFile(ofd.FileName)
        End If
    End Sub

    Private Sub SaveNote_Click(sender As Object, e As EventArgs)
        SaveCurrentNote()
    End Sub

    Private Sub Exit_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs)
        ' 선택된 노드 삭제
        Dim selectedNode As TreeNode = tvNotes.SelectedNode

        If selectedNode Is Nothing OrElse selectedNode Is tvNotes.Nodes(0) Then
            Return ' 루트 노드는 삭제 불가
        End If

        Dim itemPath As String = DirectCast(selectedNode.Tag, String)
        Dim isDirectory As Boolean = System.IO.Directory.Exists(itemPath)
        Dim itemType As String = If(isDirectory, "폴더", "노트")

        ' 확인 대화상자
        If MessageBox.Show($"선택한 {itemType}를 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If
        Try
            If isDirectory Then
                System.IO.Directory.Delete(itemPath, True) ' 폴더 및 모든 내용 삭제
            Else
                System.IO.File.Delete(itemPath) ' 파일 삭제

                ' 메타데이터 제거
                If NoteMetadataDict.ContainsKey(itemPath) Then
                    NoteMetadataDict.Remove(itemPath)
                End If

                ' 현재 파일이 삭제된 경우 편집기 내용 지우기
                If itemPath = CurrentNotePath Then
                    CurrentNotePath = ""
                    txtEditor.Clear()
                    txtTags.Clear()
                    UpdateBacklinksView()
                End If
            End If

            ' 노드 제거
            tvNotes.Nodes.Remove(selectedNode)
        Catch ex As Exception
            MessageBox.Show($"삭제 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Rename_Click(sender As Object, e As EventArgs)
        ' 선택된 노드 이름 변경
        Dim selectedNode As TreeNode = tvNotes.SelectedNode

        If selectedNode Is Nothing OrElse selectedNode Is tvNotes.Nodes(0) Then
            Return ' 루트 노드는 이름 변경 불가
        End If

        Dim itemPath As String = DirectCast(selectedNode.Tag, String)
        Dim isDirectory As Boolean = System.IO.Directory.Exists(itemPath)
        Dim currentName As String = selectedNode.Text
        Dim itemType As String = If(isDirectory, "폴더", "노트")

        ' 새 이름 입력
        Dim newName As String = InputBox($"{itemType} 이름 변경:", "이름 변경", currentName)

        If String.IsNullOrEmpty(newName) OrElse newName = currentName Then
            Return
        End If
        Try
            Dim parentDirectory As String = If(isDirectory, Path.GetDirectoryName(itemPath), Path.GetDirectoryName(itemPath))
            Dim newPath As String = If(isDirectory, Path.Combine(parentDirectory, newName), Path.Combine(parentDirectory, newName & Path.GetExtension(itemPath)))

            ' 이미 존재하는지 확인
            If (isDirectory AndAlso System.IO.Directory.Exists(newPath)) OrElse (Not isDirectory AndAlso System.IO.File.Exists(newPath)) Then
                MessageBox.Show($"'{newName}'(이)라는 이름의 {itemType}가 이미 존재합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If isDirectory Then
                ' 디렉토리 이름 변경
                System.IO.Directory.Move(itemPath, newPath)
            Else
                ' 파일 이름 변경 전에 저장
                If itemPath = CurrentNotePath Then
                    SaveCurrentNote()
                End If

                ' 파일 이름 변경
                System.IO.File.Move(itemPath, newPath)

                ' 메타데이터 업데이트
                If NoteMetadataDict.ContainsKey(itemPath) Then
                    Dim metadata As NoteMetadata = NoteMetadataDict(itemPath)
                    NoteMetadataDict.Remove(itemPath)
                    NoteMetadataDict(newPath) = metadata
                End If

                ' 현재 파일이 변경된 경우 경로 업데이트
                If itemPath = CurrentNotePath Then
                    CurrentNotePath = newPath
                End If
            End If

            ' 노드 업데이트
            selectedNode.Text = If(isDirectory, newName, Path.GetFileNameWithoutExtension(newPath))
            selectedNode.Tag = newPath

            ' 백링크 업데이트
            If Not isDirectory Then
                LoadAllNoteMetadata()
                UpdateBacklinksView()
            End If
        Catch ex As Exception
            MessageBox.Show($"이름 변경 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "메인 기능"
    Private Sub OpenNoteFile(filePath As String)
        Try
            ' 현재 파일 저장
            If Not String.IsNullOrEmpty(CurrentNotePath) AndAlso System.IO.File.Exists(CurrentNotePath) Then
                SaveCurrentNote()
            End If

            ' 새 파일 열기
            If System.IO.File.Exists(filePath) Then
                Dim content As String = System.IO.File.ReadAllText(filePath)
                txtEditor.Text = content
                CurrentNotePath = filePath

                ' 태그 업데이트
                If NoteMetadataDict.ContainsKey(filePath) Then
                    txtTags.Text = String.Join(", ", NoteMetadataDict(filePath).Tags)
                Else
                    txtTags.Text = ""
                End If

                ' 미리보기 업데이트
                UpdatePreview()

                ' 백링크 업데이트
                UpdateBacklinksView()

                ' 상태 업데이트
                lblStatus.Text = "준비"
            End If
        Catch ex As Exception
            MessageBox.Show($"파일 열기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveCurrentNote()
        If String.IsNullOrEmpty(CurrentNotePath) Then
            Return
        End If

        Try
            ' 파일 저장
            System.IO.File.WriteAllText(CurrentNotePath, txtEditor.Text)

            ' 태그 저장
            SaveTags()

            ' 백링크 업데이트
            LoadAllNoteMetadata()
            UpdateBacklinksView()

            ' 상태 업데이트
            lblStatus.Text = "저장됨"
        Catch ex As Exception
            MessageBox.Show($"파일 저장 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveTags()
        If String.IsNullOrEmpty(CurrentNotePath) Then
            Return
        End If

        ' 태그 파싱
        Dim tagsList As New List(Of String)
        For Each tag As String In txtTags.Text.Split(","c)
            Dim trimmedTag As String = tag.Trim()
            If Not String.IsNullOrEmpty(trimmedTag) Then
                tagsList.Add(trimmedTag)
            End If
        Next

        ' 메타데이터 업데이트
        If Not NoteMetadataDict.ContainsKey(CurrentNotePath) Then
            NoteMetadataDict(CurrentNotePath) = New NoteMetadata With {
                .Tags = tagsList,
                .Backlinks = New List(Of String)
            }
        Else
            ' 기존 구조체를 가져와서 수정
            Dim metadata As NoteMetadata = NoteMetadataDict(CurrentNotePath)
            metadata.Tags = tagsList  ' 태그 목록 업데이트
            NoteMetadataDict(CurrentNotePath) = metadata  ' 수정된 구조체를 Dictionary에 다시 저장
        End If

        ' 파일 내용 업데이트 (YAML 프론트매터)
        Dim content As String = txtEditor.Text
        Dim tagMatch As Match = Regex.Match(content, "tags:\s*(.*?)(?:\r?\n|\r|$)", RegexOptions.IgnoreCase)

        If tagMatch.Success Then
            ' 기존 태그 교체
            content = Regex.Replace(content, "tags:\s*(.*?)(?:\r?\n|\r|$)", "tags: " & String.Join(", ", tagsList) & Environment.NewLine, RegexOptions.IgnoreCase)
        Else
            ' 시작 부분에 태그 추가
            content = "tags: " & String.Join(", ", tagsList) & Environment.NewLine & Environment.NewLine & content
        End If
        txtEditor.Text = content
    End Sub

    Private Sub UpdatePreview()
        ' 마크다운을 HTML로 변환
        Dim html As String = ConvertMarkdownToHtml(txtEditor.Text)

        ' 웹 브라우저에 표시
        webPreview.DocumentText = html
    End Sub

    Private Function ConvertMarkdownToHtml(markdown As String) As String
        ' 마크다운 변환
        Dim html As String = markdown

        ' 스타일 시트
        Dim css As String = "<style>
        body { font-family: Arial, sans-serif; line-height: 1.6; margin: 20px; }
        h1, h2, h3 { color: #333; }
        a { color: #0066cc; }
        code { background-color: #f5f5f5; padding: 2px 4px; border-radius: 3px; }
        blockquote { border-left: 4px solid #ccc; margin: 0; padding-left: 16px; color: #666; }
        table { border-collapse: collapse; width: 70%; margin: 10px 0; }
        th, td { border: 1px solid #ddd; padding: 8px; }
        th { background-color: #f5f5f5; }
        .task-list-item { list-style-type: none; }
        .task-list-item-checkbox { margin-right: 8px; }
        mark { background-color: #ffffcc; padding: 0 2px; }
        p { margin: 8px 0; }
    </style>"

        ' 테이블 변환을 위한 패턴 (전체 테이블 매칭)
        Dim tablePattern As New Regex("\|.*\|[ \t]*\r?\n\|[-:| ]+\|[ \t]*\r?\n(\|.*\|[ \t]*\r?\n)+", RegexOptions.Multiline)
        html = tablePattern.Replace(html, AddressOf ConvertTable)

        ' 제목
        html = Regex.Replace(html, "^# (.*?)$", "<h1>$1</h1>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^## (.*?)$", "<h2>$1</h2>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^### (.*?)$", "<h3>$1</h3>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^#### (.*?)$", "<h4>$1</h4>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^##### (.*?)$", "<h5>$1</h5>", RegexOptions.Multiline)

        ' 단락 처리 (빈 줄로 구분된 텍스트 블록을 <p> 태그로 감싼다)
        html = Regex.Replace(html, "([^\r\n]+)(\r?\n\r?\n|$)", "<p>$1</p>$2", RegexOptions.Multiline)

        ' 굵게 및 기울임
        html = Regex.Replace(html, "\*\*(.*?)\*\*", "<strong>$1</strong>")
        html = Regex.Replace(html, "\*(.*?)\*", "<em>$1</em>")

        ' 취소선
        html = Regex.Replace(html, "~~(.*?)~~", "<del>$1</del>")

        ' 밑줄
        html = Regex.Replace(html, "__(.*?)__", "<u>$1</u>")

        ' 강조 (하이라이트)
        html = Regex.Replace(html, "==(.*?)==", "<mark>$1</mark>")

        ' 링크
        html = LinkPattern.Replace(html, "<a href=""#$1"">$1</a>")

        ' 체크리스트
        html = Regex.Replace(html, "^- \[ \] (.*?)$", "<li class=""task-list-item""><input type=""checkbox"" class=""task-list-item-checkbox"" disabled> $1</li>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^- \[x\] (.*?)$", "<li class=""task-list-item""><input type=""checkbox"" class=""task-list-item-checkbox"" checked disabled> $1</li>", RegexOptions.Multiline)

        ' 일반 목록 및 번호 목록
        html = Regex.Replace(html, "^- (.*?)$", "<li>$1</li>", RegexOptions.Multiline)
        html = Regex.Replace(html, "^(\d+)\. (.*?)$", "<li>$2</li>", RegexOptions.Multiline)

        ' 목록 항목 그룹화
        html = Regex.Replace(html, "(<li>.*?</li>)(\r?\n)(<li>.*?</li>)", "$1$3", RegexOptions.Singleline)
        html = Regex.Replace(html, "(<li>.*?</li>)+", "<ul>$0</ul>", RegexOptions.Singleline)

        ' 코드 블록
        html = Regex.Replace(html, "<pre><code>$1</code></pre>", "", RegexOptions.Singleline)
        ' 들여쓰기된 코드 블록 (4칸 이상)
        html = Regex.Replace(html, "(?m)^(    |\t)(.+)$", "<pre><code>$2</code></pre>")

        ' 7. 태그 라인 숨기기 및 특수 항목 처리
        html = Regex.Replace(html, "tags:.*?(?:\r?\n|\r|$)", "", RegexOptions.IgnoreCase)

        ' 8. 단일 줄바꿈은 <br> 태그로 변환 (단락 내에서만)
        ' 이미 <p> 태그로 처리된 부분은 제외하고, 남은 줄바꿈을 <br> 태그로 변환
        html = html.Replace(Environment.NewLine, "<br>")

        ' <p> 태그 내부의 중복된 <br> 제거
        html = Regex.Replace(html, "<p>(.*?)</p>", Function(m As Match) m.Value.Replace("<br><br>", "<br>"))

        ' HTML 문서 생성
        Return "<!DOCTYPE html><html><head>" & css & "</head><body>" & html & "</body></html>"
    End Function

    ' 테이블 변환을 위한 별도의 메서드
    Private Function ConvertTable(match As Match) As String
        Dim tableContent As String = match.Value
        Dim rows() As String = tableContent.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        If rows.Length < 3 Then
            ' 최소한 헤더 행, 구분 행, 데이터 행이 필요
            Return match.Value
        End If

        ' 테이블 시작
        Dim html As New StringBuilder("<table>")

        ' 헤더 처리
        Dim headerRow As String = rows(0)
        Dim headerCells() As String = headerRow.Split("|"c)
        Dim alignRow As String = rows(1)
        Dim alignCells() As String = alignRow.Split("|"c)

        ' 헤더 행 HTML 생성
        html.Append("<thead><tr>")
        For i As Integer = 1 To headerCells.Length - 2  ' 첫 번째와 마지막 빈 셀 제외
            Dim cellContent As String = headerCells(i).Trim()
            Dim alignStyle As String = "left"  ' 기본 왼쪽 정렬

            ' 정렬 스타일 결정
            If i <= alignCells.Length - 2 Then
                Dim alignMarker As String = alignCells(i).Trim()
                If alignMarker.StartsWith(":") AndAlso alignMarker.EndsWith(":") Then
                    alignStyle = "center"
                ElseIf alignMarker.EndsWith(":") Then
                    alignStyle = "right"
                End If
            End If

            html.AppendFormat("<th style=""text-align: {0}"">{1}</th>", alignStyle, cellContent)
        Next
        html.Append("</tr></thead>")

        ' 데이터 행 처리
        html.Append("<tbody>")
        For i As Integer = 2 To rows.Length - 1  ' 헤더 및 구분 행 이후
            Dim dataRow As String = rows(i)
            Dim dataCells() As String = dataRow.Split("|"c)

            html.Append("<tr>")
            For j As Integer = 1 To dataCells.Length - 2  ' 첫 번째와 마지막 빈 셀 제외
                Dim cellContent As String = dataCells(j).Trim()
                Dim alignStyle As String = "left"  ' 기본 왼쪽 정렬

                ' 정렬 스타일 결정
                If j <= alignCells.Length - 2 Then
                    Dim alignMarker As String = alignCells(j).Trim()
                    If alignMarker.StartsWith(":") AndAlso alignMarker.EndsWith(":") Then
                        alignStyle = "center"
                    ElseIf alignMarker.EndsWith(":") Then
                        alignStyle = "right"
                    End If
                End If

                html.AppendFormat("<td style=""text-align: {0}"">{1}</td>", alignStyle, cellContent)
            Next
            html.Append("</tr>")
        Next

        html.Append("</tbody></table>")
        Return html.ToString()
    End Function

    Private Sub UpdateBacklinksView()
        ' 백링크 목록 초기화
        lstBacklinks.Items.Clear()

        If String.IsNullOrEmpty(CurrentNotePath) OrElse Not NoteMetadataDict.ContainsKey(CurrentNotePath) Then
            Return
        End If

        Dim currentFileName As String = Path.GetFileNameWithoutExtension(CurrentNotePath)

        ' 현재 노트를 참조하는 모든 파일 찾기
        For Each kvp As KeyValuePair(Of String, NoteMetadata) In NoteMetadataDict
            Dim filePath As String = kvp.Key
            Dim metadata As NoteMetadata = kvp.Value

            ' 백링크 목록에 현재 파일 이름이 있는지 확인
            If metadata.Backlinks.Contains(currentFileName) AndAlso filePath <> CurrentNotePath Then
                Dim item As New ListViewItem(Path.GetFileNameWithoutExtension(filePath))
                item.SubItems.Add(filePath)
                item.Tag = filePath
                lstBacklinks.Items.Add(item)
            End If
        Next
    End Sub

    Private Sub SearchNotes(query As String)
        If String.IsNullOrEmpty(query) Then
            Return
        End If

        ' 검색 결과 폼
        Dim searchResults As New List(Of SearchResult)

        ' 모든 노트 검색
        For Each filePath As String In System.IO.Directory.GetFiles(NotesDirectory, "*.md", SearchOption.AllDirectories)
            Dim content As String = System.IO.File.ReadAllText(filePath)

            ' 내용 또는 파일명에서 검색
            If content.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               Path.GetFileNameWithoutExtension(filePath).IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 Then
                Dim result As New SearchResult With {
                    .FilePath = filePath,
                    .Title = Path.GetFileNameWithoutExtension(filePath),
                    .Preview = GetPreviewText(content, query)
                }
                searchResults.Add(result)
            End If

            ' 태그로 검색
            If NoteMetadataDict.ContainsKey(filePath) AndAlso
               NoteMetadataDict(filePath).Tags.Any(Function(tag) tag.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) Then
                If Not searchResults.Any(Function(r) r.FilePath = filePath) Then
                    Dim result As New SearchResult With {
                        .FilePath = filePath,
                        .Title = Path.GetFileNameWithoutExtension(filePath),
                        .Preview = "태그: " & String.Join(", ", NoteMetadataDict(filePath).Tags)
                    }
                    searchResults.Add(result)
                End If
            End If
        Next

        ' 검색 결과 표시
        If searchResults.Count > 0 Then
            Dim resultForm As New SearchResultForm(searchResults)
            If resultForm.ShowDialog() = DialogResult.OK AndAlso resultForm.SelectedResult IsNot Nothing Then
                OpenNoteFile(resultForm.SelectedResult.FilePath)
            End If
        Else
            MessageBox.Show($"검색어 '{query}'에 대한 결과가 없습니다.", "검색 결과", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function GetPreviewText(content As String, query As String) As String
        Dim index As Integer = content.IndexOf(query, StringComparison.OrdinalIgnoreCase)

        If index < 0 Then
            Return content.Substring(0, Math.Min(100, content.Length)) & "..."
        End If

        Dim start As Integer = Math.Max(0, index - 50)
        Dim length As Integer = Math.Min(100, content.Length - start)

        Return If(start > 0, "...", "") & content.Substring(start, length) & "..."
    End Function

    Private Sub InsertMarkdownSyntax(prefix As String, suffix As String, placeholder As String)
        Dim selectionStart As Integer = txtEditor.SelectionStart
        Dim selectionLength As Integer = txtEditor.SelectionLength

        If selectionLength > 0 Then
            ' 선택된 텍스트에 적용
            Dim selectedText As String = txtEditor.SelectedText
            txtEditor.SelectedText = prefix & selectedText & suffix
        Else
            ' 선택된 텍스트 없으면 플레이스홀더 삽입
            txtEditor.SelectedText = prefix & placeholder & suffix
            ' 플레이스홀더 선택
            txtEditor.SelectionStart = selectionStart + prefix.Length
            txtEditor.SelectionLength = placeholder.Length
        End If
    End Sub

#End Region
End Class
