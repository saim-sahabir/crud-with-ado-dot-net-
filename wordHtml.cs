using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public ActionResult GenerateWordFile()
{
    MemoryStream ms = new MemoryStream();
    using (WordprocessingDocument doc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
    {
        MainDocumentPart mainPart = doc.AddMainDocumentPart();
        mainPart.Document = new Document();
        Body body = mainPart.Document.AppendChild(new Body());

        string html = "<p>This is an example HTML template for the Word file.</p>";
        HtmlConverter converter = new HtmlConverter(mainPart);
        var elements = converter.Parse(html);
        foreach (var element in elements)
        {
            body.AppendChild(element);
        }

        doc.Save();
    }
    ms.Seek(0, SeekOrigin.Begin);
    return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Example.docx");
}
////

///////////////////////////
using System;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class HomeController : Controller
{
    public ActionResult GenerateWordFromHtml(string html)
    {
        MemoryStream ms = new MemoryStream();
        using (WordprocessingDocument doc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());
            body.Append(new HtmlConverter().ConvertHtml(html, doc.MainDocumentPart));
        }
        ms.Seek(0, SeekOrigin.Begin);
        return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedWord.docx");
    }
}

//////////////////////////////////


using System;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class HomeController : Controller
{
    public ActionResult GenerateWordFromHtml()
    {
        string html = "<h1>Hello OpenXML</h1><p>This is a sample HTML template for Word document</p>";
        MemoryStream ms = new MemoryStream();
        using (WordprocessingDocument doc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
        {
            MainDocumentPart mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = mainPart.Document.AppendChild(new Body());
            var paras = HtmlConverter.ConvertHtmlToParagraphs(html);
            foreach (var p in paras)
            {
                body.Append(p);
            }
        }
        ms.Seek(0, SeekOrigin.Begin);
        return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "GeneratedWordFromHtml.docx");
    }
}

