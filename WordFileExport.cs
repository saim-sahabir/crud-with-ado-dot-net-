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


////ohter way 




using System.Data.Entity;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class BrandsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult GenerateWordDocument()
    {
        var brands = db.Brands.ToList();

        using (var document = WordprocessingDocument.Create("Brands.docx", WordprocessingDocumentType.Document))
        {
            var mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();
            mainPart.Document.AppendChild(new Body());

            var table = new Table();
            var tableProperties = new TableProperties();
            var tableStyle = new TableStyle() { Val = "Table Grid" };
            tableProperties.AppendChild(tableStyle);
            table.AppendChild(tableProperties);

            var tableRow = new TableRow();

            var idCell = new TableCell();
            idCell.AppendChild(new Paragraph(new Run(new Text("ID"))));
            tableRow.AppendChild(idCell);

            var nameCell = new TableCell();
            nameCell.AppendChild(new Paragraph(new Run(new Text("Name"))));
            tableRow.AppendChild(nameCell);

            var entryDateCell = new TableCell();
            entryDateCell.AppendChild(new Paragraph(new Run(new Text("Entry Date"))));
            tableRow.AppendChild(entryDateCell);

            table.AppendChild(tableRow);

            foreach (var brand in brands)
            {
                tableRow = new TableRow();

                idCell = new TableCell();
                idCell.AppendChild(new Paragraph(new Run(new Text(brand.Id.ToString()))));
                tableRow.AppendChild(idCell);

                nameCell = new TableCell();
                nameCell.AppendChild(new Paragraph(new Run(new Text(brand.Name))));
                tableRow.AppendChild(nameCell);

                entryDateCell = new TableCell();
                entryDateCell.AppendChild(new Paragraph(new Run(new Text(brand.EntryDate.ToString()))));
                tableRow.AppendChild(entryDateCell);

                table.AppendChild(tableRow);
            }

            mainPart.Document.Body.AppendChild(table);
            document.Save();
        }

        return File("Brands.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Brands.docx");
    }
}

