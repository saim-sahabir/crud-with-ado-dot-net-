

/////excel file

public FileResult ExportToExcel()
{
    // Retrieve data from the "brand" table
    var data = _context.Brands.ToList();

    // Create a new instance of SpreadsheetDocument
    using (SpreadsheetDocument document = SpreadsheetDocument.Create("YourFileName.xlsx", SpreadsheetDocumentType.Workbook))
    {
        // Add WorkbookPart to SpreadsheetDocument
        WorkbookPart workbookPart = document.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();

        // Add Sheets to Workbook
        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

        // Add Sheet to Sheets
        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(workbookPart), SheetId = 1, Name = "Sheet1" };
        sheets.Append(sheet);

        // Add Worksheet to WorkbookPart
        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet(new SheetData());

        // Add data to Worksheet
        SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
        Row headerRow = new Row();
        headerRow.Append(new Cell { CellValue = new CellValue("ID"), DataType = CellValues.String });
        headerRow.Append(new Cell { CellValue = new CellValue("Name"), DataType = CellValues.String });
        headerRow.Append(new Cell { CellValue = new CellValue("Entry Date"), DataType = CellValues.String });
        sheetData.Append(headerRow);

        foreach (var item in data)
        {
            Row dataRow = new Row();
            dataRow.Append(new Cell { CellValue = new CellValue(item.Id.ToString()), DataType = CellValues.Number });
            dataRow.Append(new Cell { CellValue = new CellValue(item.Name), DataType = CellValues.String });
            dataRow.Append(new Cell { CellValue = new CellValue(item.EntryDate.ToString("yyyy-MM-dd")), DataType = CellValues.String });
            sheetData.Append(dataRow);
            }
}
