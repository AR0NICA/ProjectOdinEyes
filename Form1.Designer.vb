<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        SplitContainer1 = New SplitContainer()
        SplitContainer2 = New SplitContainer()
        Panel1 = New Panel()
        txtSearch = New TextBox()
        btnSearch = New Button()
        tvNotes = New TreeView()
        ctxTreeView = New ContextMenuStrip(components)
        NewNoteToolStripMenuItem1 = New ToolStripMenuItem()
        NewFolderToolStripMenuItem = New ToolStripMenuItem()
        DeleteToolStripMenuItem = New ToolStripMenuItem()
        RenameToolStripMenuItem = New ToolStripMenuItem()
        ImageList1 = New ImageList(components)
        TabControl1 = New TabControl()
        tabEditor = New TabPage()
        txtEditor = New RichTextBox()
        toolbarEditor = New ToolStrip()
        btnBold = New ToolStripButton()
        btnItalic = New ToolStripButton()
        btnHeader = New ToolStripButton()
        btnLink = New ToolStripButton()
        btnList = New ToolStripButton()
        tabPreview = New TabPage()
        webPreview = New WebBrowser()
        tabBacklinks = New TabPage()
        lstBacklinks = New ListView()
        colTitle = New ColumnHeader()
        colPath = New ColumnHeader()
        Panel2 = New Panel()
        lblTags = New Label()
        txtTags = New TextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        NewNoteToolStripMenuItem = New ToolStripMenuItem()
        OpenToolStripMenuItem = New ToolStripMenuItem()
        SaveToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        EditToolStripMenuItem = New ToolStripMenuItem()
        ViewToolStripMenuItem = New ToolStripMenuItem()
        ToolsToolStripMenuItem = New ToolStripMenuItem()
        HelpToolStripMenuItem = New ToolStripMenuItem()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        Panel1.SuspendLayout()
        ctxTreeView.SuspendLayout()
        TabControl1.SuspendLayout()
        tabEditor.SuspendLayout()
        toolbarEditor.SuspendLayout()
        tabPreview.SuspendLayout()
        tabBacklinks.SuspendLayout()
        Panel2.SuspendLayout()
        StatusStrip1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 24)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(SplitContainer2)
        SplitContainer1.Panel1MinSize = 200
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(TabControl1)
        SplitContainer1.Panel2.Controls.Add(Panel2)
        SplitContainer1.Size = New Size(984, 715)
        SplitContainer1.SplitterDistance = 250
        SplitContainer1.TabIndex = 0
        ' 
        ' SplitContainer2
        ' 
        SplitContainer2.Dock = DockStyle.Fill
        SplitContainer2.Location = New Point(0, 0)
        SplitContainer2.Name = "SplitContainer2"
        SplitContainer2.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer2.Panel1
        ' 
        SplitContainer2.Panel1.Controls.Add(Panel1)
        ' 
        ' SplitContainer2.Panel2
        ' 
        SplitContainer2.Panel2.Controls.Add(tvNotes)
        SplitContainer2.Size = New Size(250, 715)
        SplitContainer2.SplitterDistance = 357
        SplitContainer2.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(txtSearch)
        Panel1.Controls.Add(btnSearch)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(250, 357)
        Panel1.TabIndex = 0
        ' 
        ' txtSearch
        ' 
        txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtSearch.Location = New Point(12, 14)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(186, 21)
        txtSearch.TabIndex = 0
        ' 
        ' btnSearch
        ' 
        btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSearch.Location = New Point(204, 14)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(34, 23)
        btnSearch.TabIndex = 1
        btnSearch.Text = "🔍"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' tvNotes
        ' 
        tvNotes.ContextMenuStrip = ctxTreeView
        tvNotes.Dock = DockStyle.Fill
        tvNotes.ImageIndex = 0
        tvNotes.ImageList = ImageList1
        tvNotes.Location = New Point(0, 0)
        tvNotes.Name = "tvNotes"
        tvNotes.SelectedImageIndex = 0
        tvNotes.Size = New Size(250, 354)
        tvNotes.TabIndex = 0
        ' 
        ' ctxTreeView
        ' 
        ctxTreeView.Items.AddRange(New ToolStripItem() {NewNoteToolStripMenuItem1, NewFolderToolStripMenuItem, DeleteToolStripMenuItem, RenameToolStripMenuItem})
        ctxTreeView.Name = "ctxTreeView"
        ctxTreeView.Size = New Size(127, 92)
        ' 
        ' NewNoteToolStripMenuItem1
        ' 
        NewNoteToolStripMenuItem1.Name = "NewNoteToolStripMenuItem1"
        NewNoteToolStripMenuItem1.Size = New Size(126, 22)
        NewNoteToolStripMenuItem1.Text = "새 노트"
        ' 
        ' NewFolderToolStripMenuItem
        ' 
        NewFolderToolStripMenuItem.Name = "NewFolderToolStripMenuItem"
        NewFolderToolStripMenuItem.Size = New Size(126, 22)
        NewFolderToolStripMenuItem.Text = "새 폴더"
        ' 
        ' DeleteToolStripMenuItem
        ' 
        DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        DeleteToolStripMenuItem.Size = New Size(126, 22)
        DeleteToolStripMenuItem.Text = "삭제"
        ' 
        ' RenameToolStripMenuItem
        ' 
        RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        RenameToolStripMenuItem.Size = New Size(126, 22)
        RenameToolStripMenuItem.Text = "이름 변경"
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth8Bit
        ImageList1.ImageSize = New Size(16, 16)
        ImageList1.TransparentColor = Color.Transparent
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(tabEditor)
        TabControl1.Controls.Add(tabPreview)
        TabControl1.Controls.Add(tabBacklinks)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(730, 685)
        TabControl1.TabIndex = 0
        ' 
        ' tabEditor
        ' 
        tabEditor.Controls.Add(txtEditor)
        tabEditor.Controls.Add(toolbarEditor)
        tabEditor.Location = New Point(4, 23)
        tabEditor.Name = "tabEditor"
        tabEditor.Padding = New Padding(3)
        tabEditor.Size = New Size(722, 658)
        tabEditor.TabIndex = 0
        tabEditor.Text = "편집기"
        tabEditor.UseVisualStyleBackColor = True
        ' 
        ' txtEditor
        ' 
        txtEditor.Dock = DockStyle.Fill
        txtEditor.Location = New Point(3, 28)
        txtEditor.Name = "txtEditor"
        txtEditor.Size = New Size(716, 627)
        txtEditor.TabIndex = 0
        txtEditor.Text = ""
        ' 
        ' toolbarEditor
        ' 
        ' 버튼들 초기화
        btnUndo = New ToolStripButton()
        btnRedo = New ToolStripButton()
        separatorEdit = New ToolStripSeparator()
        btnClearFormat = New ToolStripButton()
        btnStrikethrough = New ToolStripButton()
        btnUnderline = New ToolStripButton()
        btnHighlight = New ToolStripButton()
        separatorFormat = New ToolStripSeparator()
        btnTable = New ToolStripButton()
        btnListType = New ToolStripDropDownButton()
        miCheckList = New ToolStripMenuItem()
        miNumberedList = New ToolStripMenuItem()
        miBulletList = New ToolStripMenuItem()
        miIndent = New ToolStripMenuItem()
        miUnindent = New ToolStripMenuItem()
        separatorAlign = New ToolStripSeparator()
        btnTextAlign = New ToolStripDropDownButton()
        miAlignLeft = New ToolStripMenuItem()
        miAlignCenter = New ToolStripMenuItem()
        miAlignRight = New ToolStripMenuItem()
        miAlignJustify = New ToolStripMenuItem()
        separatorColor = New ToolStripSeparator()
        btnTextColor = New ToolStripButton()
        btnBackColor = New ToolStripButton()

        ' toolbarEditor
        toolbarEditor.Items.AddRange(New ToolStripItem() {
    btnUndo, btnRedo, separatorEdit,
    btnBold, btnItalic, btnStrikethrough, btnUnderline, btnHighlight, btnClearFormat, separatorFormat,
    btnHeader, btnLink, btnTable, btnList, btnListType, separatorAlign,
    btnTextAlign, separatorColor, btnTextColor, btnBackColor})

        ' 각 버튼 설정
        btnUndo.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUndo.Text = "↩"
        btnUndo.ToolTipText = "실행 취소"

        btnRedo.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnRedo.Text = "↪"
        btnRedo.ToolTipText = "다시 실행"

        btnClearFormat.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnClearFormat.Text = "C"
        btnClearFormat.ToolTipText = "서식 지우기"

        btnStrikethrough.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnStrikethrough.Font = New Font("Segoe UI", 9.0F, FontStyle.Strikeout)
        btnStrikethrough.Text = "S"
        btnStrikethrough.ToolTipText = "취소선"

        btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUnderline.Font = New Font("Segoe UI", 9.0F, FontStyle.Underline)
        btnUnderline.Text = "U"
        btnUnderline.ToolTipText = "밑줄"

        btnHighlight.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnHighlight.Text = "="
        btnHighlight.ToolTipText = "형광펜"

        btnTable.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTable.Text = "▦"
        btnTable.ToolTipText = "표 삽입"

        btnListType.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnListType.Text = "▤"
        btnListType.ToolTipText = "목록 유형"
        btnListType.DropDownItems.AddRange(New ToolStripItem() {miCheckList, miNumberedList, miBulletList, miIndent, miUnindent})

        miCheckList.Text = "체크리스트"
        miNumberedList.Text = "번호 매기기"
        miBulletList.Text = "글머리 기호"
        miIndent.Text = "들여쓰기"
        miUnindent.Text = "내어쓰기"

        btnTextAlign.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTextAlign.Text = "≡"
        btnTextAlign.ToolTipText = "텍스트 정렬"
        btnTextAlign.DropDownItems.AddRange(New ToolStripItem() {miAlignLeft, miAlignCenter, miAlignRight, miAlignJustify})

        miAlignLeft.Text = "왼쪽 정렬"
        miAlignCenter.Text = "가운데 정렬"
        miAlignRight.Text = "오른쪽 정렬"
        miAlignJustify.Text = "양쪽 정렬"

        btnTextColor.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTextColor.Text = "A"
        btnTextColor.ForeColor = Color.Red
        btnTextColor.ToolTipText = "텍스트 색상"

        btnBackColor.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBackColor.Text = "A"
        btnBackColor.BackColor = Color.Yellow
        btnBackColor.ToolTipText = "배경 색상"

        ' separatorEdit
        separatorEdit.Name = "separatorEdit"
        separatorEdit.Size = New Size(6, 25)
        ' 
        ' btnBold
        ' 
        btnBold.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBold.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnBold.ImageTransparentColor = Color.Magenta
        btnBold.Name = "btnBold"
        btnBold.Size = New Size(23, 22)
        btnBold.Text = "B"
        btnBold.ToolTipText = "굵게"
        ' 
        ' btnItalic
        ' 
        btnItalic.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnItalic.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic)
        btnItalic.ImageTransparentColor = Color.Magenta
        btnItalic.Name = "btnItalic"
        btnItalic.Size = New Size(23, 22)
        btnItalic.Text = "I"
        btnItalic.ToolTipText = "기울임"

        ' btnStrikethrough
        btnStrikethrough.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnStrikethrough.Font = New Font("Segoe UI", 9.0F, FontStyle.Strikeout)
        btnStrikethrough.Name = "btnStrikethrough"
        btnStrikethrough.Size = New Size(23, 22)
        btnStrikethrough.Text = "S"
        btnStrikethrough.ToolTipText = "취소선"

        ' btnUnderline
        btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnUnderline.Font = New Font("Segoe UI", 9.0F, FontStyle.Underline)
        btnUnderline.Name = "btnUnderline"
        btnUnderline.Size = New Size(23, 22)
        btnUnderline.Text = "U"
        btnUnderline.ToolTipText = "밑줄"

        ' btnHighlight
        btnHighlight.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnHighlight.Name = "btnHighlight"
        btnHighlight.Size = New Size(23, 22)
        btnHighlight.Text = "="
        btnHighlight.ToolTipText = "형광펜 (==텍스트==)"

        ' btnClearFormat
        btnClearFormat.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnClearFormat.Name = "btnClearFormat"
        btnClearFormat.Size = New Size(23, 22)
        btnClearFormat.Text = "C"
        btnClearFormat.ToolTipText = "서식 지우기"

        ' separatorFormat
        separatorFormat.Name = "separatorFormat"
        separatorFormat.Size = New Size(6, 25)

        ' 
        ' btnHeader
        ' 
        btnHeader.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnHeader.ImageTransparentColor = Color.Magenta
        btnHeader.Name = "btnHeader"
        btnHeader.Size = New Size(23, 22)
        btnHeader.Text = "H"
        btnHeader.ToolTipText = "제목"
        ' 
        ' btnLink
        ' 
        btnLink.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnLink.ImageTransparentColor = Color.Magenta
        btnLink.Name = "btnLink"
        btnLink.Size = New Size(23, 22)
        btnLink.Text = "🔗"
        btnLink.ToolTipText = "링크"

        ' btnTable
        btnTable.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTable.Name = "btnTable"
        btnTable.Size = New Size(23, 22)
        btnTable.Text = "▦"
        btnTable.ToolTipText = "표 삽입"
        ' 
        ' btnList
        ' 
        btnList.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnList.ImageTransparentColor = Color.Magenta
        btnList.Name = "btnList"
        btnList.Size = New Size(23, 22)
        btnList.Text = "•"
        btnList.ToolTipText = "목록"

        ' btnListType
        btnListType.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnListType.DropDownItems.AddRange(New ToolStripItem() {miCheckList, miNumberedList, miBulletList, miIndent, miUnindent})
        btnListType.Name = "btnListType"
        btnListType.Size = New Size(29, 22)
        btnListType.Text = "▤"
        btnListType.ToolTipText = "목록 유형"

        ' miCheckList
        miCheckList.Name = "miCheckList"
        miCheckList.Size = New Size(180, 22)
        miCheckList.Text = "체크리스트"

        ' miNumberedList
        miNumberedList.Name = "miNumberedList"
        miNumberedList.Size = New Size(180, 22)
        miNumberedList.Text = "번호 매기기"

        ' miBulletList
        miBulletList.Name = "miBulletList"
        miBulletList.Size = New Size(180, 22)
        miBulletList.Text = "글머리 기호"

        ' miIndent
        miIndent.Name = "miIndent"
        miIndent.Size = New Size(180, 22)
        miIndent.Text = "들여쓰기"

        ' miUnindent
        miUnindent.Name = "miUnindent"
        miUnindent.Size = New Size(180, 22)
        miUnindent.Text = "내어쓰기"

        ' separatorAlign
        separatorAlign.Name = "separatorAlign"
        separatorAlign.Size = New Size(6, 25)

        ' btnTextAlign
        btnTextAlign.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTextAlign.DropDownItems.AddRange(New ToolStripItem() {miAlignLeft, miAlignCenter, miAlignRight, miAlignJustify})
        btnTextAlign.Name = "btnTextAlign"
        btnTextAlign.Size = New Size(29, 22)
        btnTextAlign.Text = "≡"
        btnTextAlign.ToolTipText = "텍스트 정렬"

        ' miAlignLeft
        miAlignLeft.Name = "miAlignLeft"
        miAlignLeft.Size = New Size(180, 22)
        miAlignLeft.Text = "왼쪽 정렬"

        ' miAlignCenter
        miAlignCenter.Name = "miAlignCenter"
        miAlignCenter.Size = New Size(180, 22)
        miAlignCenter.Text = "가운데 정렬"

        ' miAlignRight
        miAlignRight.Name = "miAlignRight"
        miAlignRight.Size = New Size(180, 22)
        miAlignRight.Text = "오른쪽 정렬"

        ' miAlignJustify
        miAlignJustify.Name = "miAlignJustify"
        miAlignJustify.Size = New Size(180, 22)
        miAlignJustify.Text = "양쪽 정렬"

        ' separatorColor
        separatorColor.Name = "separatorColor"
        separatorColor.Size = New Size(6, 25)

        ' btnTextColor
        btnTextColor.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnTextColor.Name = "btnTextColor"
        btnTextColor.Size = New Size(23, 22)
        btnTextColor.Text = "A"
        btnTextColor.ForeColor = Color.Red
        btnTextColor.ToolTipText = "텍스트 색상"

        ' btnBackColor
        btnBackColor.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnBackColor.Name = "btnBackColor"
        btnBackColor.Size = New Size(23, 22)
        btnBackColor.Text = "A"
        btnBackColor.BackColor = Color.Yellow
        btnBackColor.ToolTipText = "배경 색상"
        ' 
        ' tabPreview
        ' 
        tabPreview.Controls.Add(webPreview)
        tabPreview.Location = New Point(4, 24)
        tabPreview.Name = "tabPreview"
        tabPreview.Padding = New Padding(3)
        tabPreview.Size = New Size(722, 657)
        tabPreview.TabIndex = 1
        tabPreview.Text = "미리보기"
        tabPreview.UseVisualStyleBackColor = True
        ' 
        ' webPreview
        ' 
        webPreview.Dock = DockStyle.Fill
        webPreview.Location = New Point(3, 3)
        webPreview.MinimumSize = New Size(20, 20)
        webPreview.Name = "webPreview"
        webPreview.Size = New Size(716, 651)
        webPreview.TabIndex = 0
        ' 
        ' tabBacklinks
        ' 
        tabBacklinks.Controls.Add(lstBacklinks)
        tabBacklinks.Location = New Point(4, 24)
        tabBacklinks.Name = "tabBacklinks"
        tabBacklinks.Size = New Size(722, 657)
        tabBacklinks.TabIndex = 2
        tabBacklinks.Text = "백링크"
        tabBacklinks.UseVisualStyleBackColor = True
        ' 
        ' lstBacklinks
        ' 
        lstBacklinks.Columns.AddRange(New ColumnHeader() {colTitle, colPath})
        lstBacklinks.Dock = DockStyle.Fill
        lstBacklinks.FullRowSelect = True
        lstBacklinks.Location = New Point(0, 0)
        lstBacklinks.Name = "lstBacklinks"
        lstBacklinks.Size = New Size(722, 657)
        lstBacklinks.TabIndex = 0
        lstBacklinks.UseCompatibleStateImageBehavior = False
        lstBacklinks.View = View.Details
        ' 
        ' colTitle
        ' 
        colTitle.Text = "제목"
        colTitle.Width = 200
        ' 
        ' colPath
        ' 
        colPath.Text = "경로"
        colPath.Width = 400
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(lblTags)
        Panel2.Controls.Add(txtTags)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(0, 685)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(730, 30)
        Panel2.TabIndex = 1
        ' 
        ' lblTags
        ' 
        lblTags.Location = New Point(7, 9)
        lblTags.Name = "lblTags"
        lblTags.Size = New Size(34, 13)
        lblTags.TabIndex = 0
        lblTags.Text = "태그:"
        ' 
        ' txtTags
        ' 
        txtTags.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtTags.Location = New Point(47, 6)
        txtTags.Name = "txtTags"
        txtTags.Size = New Size(676, 21)
        txtTags.TabIndex = 1
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus})
        StatusStrip1.Location = New Point(0, 739)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(984, 22)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(31, 17)
        lblStatus.Text = "준비"
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, EditToolStripMenuItem, ViewToolStripMenuItem, ToolsToolStripMenuItem, HelpToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(984, 24)
        MenuStrip1.TabIndex = 2
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {NewNoteToolStripMenuItem, OpenToolStripMenuItem, SaveToolStripMenuItem, ToolStripSeparator1, ExitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(57, 20)
        FileToolStripMenuItem.Text = "파일(&F)"
        ' 
        ' NewNoteToolStripMenuItem
        ' 
        NewNoteToolStripMenuItem.Name = "NewNoteToolStripMenuItem"
        NewNoteToolStripMenuItem.Size = New Size(131, 22)
        NewNoteToolStripMenuItem.Text = "새 노트(&N)"
        ' 
        ' OpenToolStripMenuItem
        ' 
        OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        OpenToolStripMenuItem.Size = New Size(131, 22)
        OpenToolStripMenuItem.Text = "열기(&O)"
        ' 
        ' SaveToolStripMenuItem
        ' 
        SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        SaveToolStripMenuItem.Size = New Size(131, 22)
        SaveToolStripMenuItem.Text = "저장(&S)"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(128, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(131, 22)
        ExitToolStripMenuItem.Text = "종료(&X)"
        ' 
        ' EditToolStripMenuItem
        ' 
        EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        EditToolStripMenuItem.Size = New Size(57, 20)
        EditToolStripMenuItem.Text = "편집(&E)"
        ' 
        ' ViewToolStripMenuItem
        ' 
        ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        ViewToolStripMenuItem.Size = New Size(59, 20)
        ViewToolStripMenuItem.Text = "보기(&V)"
        ' 
        ' ToolsToolStripMenuItem
        ' 
        ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        ToolsToolStripMenuItem.Size = New Size(57, 20)
        ToolsToolStripMenuItem.Text = "도구(&T)"
        ' 
        ' HelpToolStripMenuItem
        ' 
        HelpToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AboutToolStripMenuItem})
        HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        HelpToolStripMenuItem.Size = New Size(72, 20)
        HelpToolStripMenuItem.Text = "도움말(&H)"
        ' 
        ' AboutToolStripMenuItem
        ' 
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(123, 22)
        AboutToolStripMenuItem.Text = "정보(&A)..."
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 14.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(984, 761)
        Controls.Add(SplitContainer1)
        Controls.Add(StatusStrip1)
        Controls.Add(MenuStrip1)
        Font = New Font("나눔고딕", 9.0F)
        MainMenuStrip = MenuStrip1
        Name = "frmMain"
        Text = "OdinEyes - 개인 지식 관리 시스템"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ctxTreeView.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        tabEditor.ResumeLayout(False)
        tabEditor.PerformLayout()
        toolbarEditor.ResumeLayout(False)
        toolbarEditor.PerformLayout()
        tabPreview.ResumeLayout(False)
        tabBacklinks.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tvNotes As System.Windows.Forms.TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabEditor As System.Windows.Forms.TabPage
    Friend WithEvents txtEditor As System.Windows.Forms.RichTextBox
    Friend WithEvents toolbarEditor As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnItalic As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHeader As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLink As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnList As System.Windows.Forms.ToolStripButton
    Friend WithEvents tabPreview As System.Windows.Forms.TabPage
    Friend WithEvents tabBacklinks As System.Windows.Forms.TabPage
    Friend WithEvents webPreview As System.Windows.Forms.WebBrowser
    Friend WithEvents lstBacklinks As System.Windows.Forms.ListView
    Friend WithEvents colTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTags As System.Windows.Forms.Label
    Friend WithEvents txtTags As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewNoteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ctxTreeView As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NewNoteToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    ' 새 버튼 및 컨트롤들
    Friend WithEvents btnUndo As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRedo As System.Windows.Forms.ToolStripButton
    Friend WithEvents separatorEdit As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnClearFormat As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnStrikethrough As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUnderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHighlight As System.Windows.Forms.ToolStripButton
    Friend WithEvents separatorFormat As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnTable As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnListType As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents miCheckList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miNumberedList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miBulletList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miIndent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miUnindent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents separatorAlign As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnTextAlign As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents miAlignLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miAlignCenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miAlignRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents miAlignJustify As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents separatorColor As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnTextColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnBackColor As System.Windows.Forms.ToolStripButton
End Class
