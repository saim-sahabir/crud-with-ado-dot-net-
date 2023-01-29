

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
    
    
    
    
    
    
    
    
 /////   defrent way 
    
    
        public BrandsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public ActionResult ExportToExcel()
    {
        List<Brand> brands = _context.Brands.ToList();
        byte[] result = CreateExcelFile(brands);
        return File(result, "application/ms-excel", "Brands.xlsx");
    }

    private byte[] CreateExcelFile(List<Brand> brands)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Brands");
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Entry Date";
            int row = 2;
            foreach (var brand in brands)
            {
                worksheet.Cells[row, 1].Value = brand.Id;
                worksheet.Cells[row, 2].Value = brand.Name;
                worksheet.Cells[row, 3].Value = brand.EntryDate;
                row++;
            }
            return package.GetAsByteArray();
        }
    }
