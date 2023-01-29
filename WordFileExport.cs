public class BrandsController : Controller
{
    private readonly YourDbContext _context;

    public BrandsController(YourDbContext context)
    {
        _context = context;
    }

    public IActionResult GenerateWord()
    {
        var brands = _context.Brands.ToList();

        using (MemoryStream mem = new MemoryStream())
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Table table = new Table();

                TableRow headerRow = new TableRow();
                headerRow.AppendChild(CreateCell("ID"));
                headerRow.AppendChild(CreateCell("Name"));
                headerRow.AppendChild(CreateCell("Entry Date"));

                table.AppendChild(headerRow);

                foreach (var brand in brands)
                {
                    TableRow dataRow = new TableRow();
                    dataRow.AppendChild(CreateCell(brand.Id.ToString()));
                    dataRow.AppendChild(CreateCell(brand.Name));
                    dataRow.AppendChild(CreateCell(brand.EntryDate.ToString("yyyy-MM-dd")));

                    table.AppendChild(dataRow);
                }

                body.AppendChild(table);
                wordDoc.MainDocumentPart.Document.Save();
            }

            mem.Seek(0, SeekOrigin.Begin);
            return File(mem, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Brands.docx");
        }
    }

    private TableCell CreateCell(string text)
    {
        TableCell cell = new TableCell();
        Paragraph para = new Paragraph();
        Run run = new Run();
        run.AppendChild(new Text(text));
        para.AppendChild(run);
        cell.AppendChild(para);
        return cell;
    }
}
