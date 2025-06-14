Public Class SearchResultForm
    Inherits Form

    Private WithEvents lstResults As New ListView
    Private WithEvents btnOpen As New Button
    Private WithEvents btnCancel As New Button
    Public Property SelectedResult As SearchResult

    Public Sub New(results As List(Of SearchResult))
        ' 폼 설정
        Text = "검색 결과"
        Size = New Size(600, 400)
        StartPosition = FormStartPosition.CenterParent

        ' 리스트뷰 설정
        lstResults.Dock = DockStyle.Fill
        lstResults.View = View.Details
        lstResults.FullRowSelect = True
        lstResults.HideSelection = False
        lstResults.Columns.Add("제목", 150)
        lstResults.Columns.Add("미리보기", 400)

        ' 버튼 패널
        Dim panel As New Panel With {
                .Dock = DockStyle.Bottom,
                .Height = 40
            }

        btnOpen.Text = "열기"
        btnOpen.DialogResult = DialogResult.OK
        btnOpen.Enabled = False
        btnOpen.Location = New Point(panel.Width - 175, 10)
        btnOpen.Size = New Size(75, 23)
        btnCancel.Text = "취소"
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.Location = New Point(panel.Width - 90, 10)
        btnCancel.Size = New Size(75, 23)
        panel.Controls.Add(btnOpen)
        panel.Controls.Add(btnCancel)

        ' 결과 추가
        For Each result As SearchResult In results
            Dim item As New ListViewItem(result.Title)
            item.SubItems.Add(result.Preview)
            item.Tag = result
            lstResults.Items.Add(item)
        Next

        ' 리사이즈 이벤트 처리
        AddHandler Me.Resize, Sub(s, e)
                                  btnOpen.Left = panel.Width - 175
                                  btnCancel.Left = panel.Width - 90
                              End Sub

        ' 항목 선택 이벤트
        AddHandler lstResults.SelectedIndexChanged, Sub(s, e)
                                                        btnOpen.Enabled = lstResults.SelectedItems.Count > 0
                                                    End Sub

        ' 더블 클릭 이벤트
        AddHandler lstResults.MouseDoubleClick, Sub(s, e)
                                                    If lstResults.SelectedItems.Count > 0 Then
                                                        SelectedResult = DirectCast(lstResults.SelectedItems(0).Tag, SearchResult)
                                                        DialogResult = DialogResult.OK
                                                        Close()
                                                    End If
                                                End Sub

        ' 확인 버튼 클릭 이벤트
        AddHandler btnOpen.Click, Sub(s, e)
                                      If lstResults.SelectedItems.Count > 0 Then
                                          SelectedResult = DirectCast(lstResults.SelectedItems(0).Tag, SearchResult)
                                      End If
                                  End Sub

        ' 폼에 컨트롤 추가
        Controls.Add(lstResults)
        Controls.Add(panel)
        AcceptButton = btnOpen
        CancelButton = btnCancel
    End Sub
End Class
