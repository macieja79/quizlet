
Declare Function CoCreateGuid Lib "ole32" (ByRef GUID As Byte) As Long

Const SvgPath = "https://hubblecontent.osi.office.net/ContentSVC/Content/svg/"
Const SvgExtension = ".svg"

Sub CreateTipTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Tool", vbBlack, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateInfoTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Info", vbBlack, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateWarningTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Warning", vbRed, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateLinkTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Link", vbBlack, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateDefinitionTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Definition", vbBlack, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateToolTable()
    Dim table As table
    Set table = CreateTable("QAInfoTable", "Tool", vbBlack, 1, 2)
    table.Columns(2).width = 350
End Sub

Sub CreateQATable()
    
    Dim table As table
    Dim range As range
    
    
    
    
    Set table = CreateTable("QAQuestionTable", "Question", vbBlack, 2, 3)
    
    With table.Rows(1)
        .Cells(2).Shading.BackgroundPatternColor = -738132173
        .Cells(3).Shading.BackgroundPatternColor = -654246093
        .Height = 30
        .Cells(2).range.Font.Size = 12
        .Cells(3).range.Font.Size = 12
    End With
   
    With table.Rows(2)
        .range.Font.ColorIndex = wdGray50
        .Height = 8
        .Borders(wdBorderBottom).LineStyle = wdLineStyleNone
        .Borders(wdBorderLeft).LineStyle = wdLineStyleNone
        .Borders(wdBorderRight).LineStyle = wdLineStyleNone
        .Borders(wdBorderVertical).LineStyle = wdLineStyleNone
        .Borders(wdBorderVertical).LineStyle = wdLineStyleNone
        .Borders(wdBorderTop).LineStyle = wdLineStyleSingle
        .Borders(wdBorderTop).LineWidth = wdLineWidth100pt
        .Borders(wdBorderTop).color = wdColorWhite
        .Cells(2).range = "general"
        .Cells(1).range.Font.Size = 8
        .Cells(2).range.Font.Size = 8
        .Cells(3).range.Font.Size = 8
        
        .Cells(3).range = GetCurrentDateAndGuid
        
    End With
    
    With table.Columns(2).Borders(wdBorderRight)
        .LineStyle = wdLineStyleSingle
        .LineWidth = wdLineWidth600pt
        .color = wdColorWhite
    End With
    
    table.Columns(2).Borders(wdBorderLeft).LineStyle = wdLineStyleNone
    
   
    
    table.Columns(2).width = 180
    table.Columns(3).width = 180
    
    table.Columns(2).Cells(1).Select
    
End Sub

Sub CreateCheatsheetTable()
    
    Dim tb As table

    Dim nRows, nCols As Integer

    nRows = 3
    nCols = 3

    Set tb = ActiveDocument.Tables.Add(range:=Selection.range, NumRows:=nRows, NumColumns:= _
        nCols, DefaultTableBehavior:=wdWord9TableBehavior, AutoFitBehavior:= _
        wdAutoFitFixed)
        
        
        tb.Columns(2).Select
        Selection.range.Font.Name = "Courier New"
      
        tb.Columns(1).Select
        Selection.range.Font.Name = "Calibri"
      
      
        tb.Rows(1).Cells(1).range = ChrW(&H2710)
        tb.Rows(1).Cells(1).range.Font.Name = "MS UI Gothic"
        tb.Rows(1).Cells(1).range.Font.Size = 10
        
        
        tb.Rows(2).Cells(3).range = GetCurrentDateAndGuid
        tb.Rows(2).Cells(3).range.Font.Size = 8
        
        SetColumnWidth tb, 1, 100
        SetColumnWidth tb, 2, 250
        SetColumnWidth tb, 3, 100
       
     With tb.Rows(3)
        .range.Font.ColorIndex = wdGray50
        .Height = 8
        .Borders(wdBorderBottom).LineStyle = wdLineStyleNone
        .Borders(wdBorderLeft).LineStyle = wdLineStyleNone
        .Borders(wdBorderRight).LineStyle = wdLineStyleNone
        .Borders(wdBorderVertical).LineStyle = wdLineStyleNone
        .Borders(wdBorderVertical).LineStyle = wdLineStyleNone
        .Cells(1).range = "cheatsheet|"
        .Cells(1).range.Font.Size = 8
        .Cells(2).range.Font.Size = 8
        .Cells(1).range.Font.Name = "Calibri"
        .Cells(2).range.Font.Name = "Calibri"
    End With
    
    
End Sub

Sub LaunchLearningApplication()

Dim currentDocPath As String
Dim shellCommand As String
Dim learningServiceName As String

learningServiceName = "c:\Users\ezszcma\Google Drive\DevNotes\QuizApp\Quiz.exe"
currentDocPath = ActiveDocument.FullName

shellCommand = """" & learningServiceName & """ """ & currentDocPath & """"

Call Shell(shellCommand, vbNormalFocus)

End Sub

Private Sub SetColumnWidth(table As table, colNumber As Integer, width As Integer)

        For r = 1 To table.Rows.Count
            table.Rows(r).Cells(colNumber).width = width
        Next r

End Sub



Private Function CreateTable(tableTitle As String, icon As String, color As Long, nRows As Integer, nCols As Integer) As table

    Dim iconCell As Cell
    
    Dim path As String
    
    Dim tb As table

    Set tb = ActiveDocument.Tables.Add(range:=Selection.range, NumRows:=nRows, NumColumns:= _
        nCols, DefaultTableBehavior:=wdWord9TableBehavior, AutoFitBehavior:= _
        wdAutoFitFixed)
    
    tb.range.Font.Size = 16
    tb.Title = tableName
    tb.range.Borders.InsideLineStyle = wdLineStyleNone
    tb.range.Borders.OutsideLineStyle = wdLineStyleNone
    
    For r = 1 To nRows
        tb.Rows(r).Cells(1).width = 50
    Next r
   
    If icon = "Info" Then
        tb.Rows(1).Cells(1).range = "i"
        tb.Rows(1).Cells(1).range.Font.Name = "Webdings"
        tb.Rows(1).Cells(1).range.Font.Size = 50
    End If
    
    If icon = "Warning" Then
        tb.Rows(1).Cells(1).range = ChrW(&H26A0)
        tb.Rows(1).Cells(1).range.Font.Name = "Segoe UI Emoji"
        tb.Rows(1).Cells(1).range.Font.Size = 40
    End If
        
    If icon = "Question" Then
        tb.Rows(1).Cells(1).range = ChrW(&H2754)
        tb.Rows(1).Cells(1).range.Font.Name = "Segoe UI Emoji"
        tb.Rows(1).Cells(1).range.Font.Size = 35
    End If
    
    If icon = "Tool" Then
        tb.Rows(1).Cells(1).range = ChrW(&H26CF)
        tb.Rows(1).Cells(1).range.Font.Name = "Segoe UI Emoji"
        tb.Rows(1).Cells(1).range.Font.Size = 40
    End If
    
     If icon = "Definition" Then
        tb.Rows(1).Cells(1).range = ChrW(&H2710)
        tb.Rows(1).Cells(1).range.Font.Name = "Segoe UI Emoji"
        tb.Rows(1).Cells(1).range.Font.Size = 35
    End If
    
  
             
    
    tb.Rows(1).Cells(2).Select
    
    tb.range.NoProofing = True
    tb.range.SpellingChecked = False
        
    Set CreateTable = tb
       

End Function


Private Sub CreateIcon(icon As String, color As Long)

    
    
    

    Dim picture As InlineShape
    Set picture = Selection.InlineShapes.AddPicture(FileName:=icon, LinkToFile:=False, SaveWithDocument:=True)
    picture.width = 40
    picture.Height = 40
    picture.Fill.ForeColor = color
    
End Sub


Private Sub CreateIconAsSymbol(icon As String, color As Long)

 Selection.InsertSymbol Font:="Segoe UI Emoji", CharacterNumber:=10067, Unicode:=True
    Selection.InsertSymbol Font:="Segoe UI Emoji", CharacterNumber:=10068, Unicode:=True
    Selection.InsertSymbol Font:="Segoe UI Emoji", CharacterNumber:=9888, Unicode:=True
    Selection.InsertSymbol Font:="Webdings", CharacterNumber:=-3991, Unicode:=True

End Sub

Sub Macro1()
'
' Macro1 Macro
'
'
    Selection.InlineShapes.AddPicture FileName:= _
        "https://hubblecontent.osi.office.net/ContentSVC/Content/svg/Document.svg" _
        , LinkToFile:=False, SaveWithDocument:=True
    Selection.TypeParagraph
    Selection.InlineShapes.AddPicture FileName:= _
        "https://hubblecontent.osi.office.net/ContentSVC/Content/svg/Hammer.svg", _
         LinkToFile:=False, SaveWithDocument:=True
End Sub
Sub CreateCodeTable()
'
' CreateCodeTable Macro
'
'
    ActiveDocument.Tables.Add range:=Selection.range, NumRows:=1, NumColumns:= _
        1, DefaultTableBehavior:=wdWord9TableBehavior, AutoFitBehavior:= _
        wdAutoFitFixed
    With Selection.Tables(1)
        If .Style <> "Table Grid" Then
            .Style = "Table Grid"
        End If
        .ApplyStyleHeadingRows = True
        .ApplyStyleLastRow = False
        .ApplyStyleFirstColumn = True
        .ApplyStyleLastColumn = False
        .ApplyStyleRowBands = True
        .ApplyStyleColumnBands = False
    End With
    
    Selection.Shading.Texture = wdTextureNone
    Selection.Shading.ForegroundPatternColor = wdColorAutomatic
    Selection.Shading.BackgroundPatternColor = -603917569
    Selection.Font.Name = "Courier New"
    Selection.range.NoProofing = True
    Selection.range.SpellingChecked = False
    
End Sub


Public Sub CreateDateAndGuid()

Selection.range = GetCurrentDateAndGuid

End Sub

Function GetCurrentDateAndGuid() As String

    Dim dt As String
    Dim GUID As String
    Dim dtAndGuid As String
    
    dt = GetCurrentDate
    GUID = GenerateGUID
    dtAndGuid = dt & "|" & GUID
    
    GetCurrentDateAndGuid = dt

End Function




Function GetCurrentDate() As String

    Dim d As String
    Dim t As String
    
    Dim dt As String
        
    d = Format(Date, "yyyy-mm-dd")
    t = Format(Time, "hh:mm:ss")
    
    dt = d & " " & t
    
    GetCurrentDate = dt

End Function

Function GenerateGUID() As String
    Dim ID(0 To 15) As Byte
    Dim N As Long
    Dim GUID As String
    Dim Res As Long
    Res = CoCreateGuid(ID(0))

    For N = 0 To 15
        GUID = GUID & IIf(ID(N) < 16, "0", "") & Hex$(ID(N))
        If Len(GUID) = 8 Or Len(GUID) = 13 Or Len(GUID) = 18 Or Len(GUID) = 23 Then
            GUID = GUID & "-"
        End If
    Next N
    GenerateGUID = GUID
End Function

