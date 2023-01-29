using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.IO;

public class BrandsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult GenerateCsvFile()
    {
        var brands = db.Brands.ToList();

        var csv = new StringBuilder();
        csv.AppendLine("ID,Name,Entry Date");

        foreach (var brand in brands)
        {
            csv.AppendLine($"{brand.Id},{brand.Name},{brand.EntryDate}");
        }

        var fileBytes = Encoding.UTF8.GetBytes(csv.ToString());
        return File(fileBytes, "text/csv", "Brands.csv");
    }
}





using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Text;

public class BrandsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult GenerateAndSaveCsvFile()
    {
        var brands = db.Brands.ToList();
        var sb = new StringBuilder();
        sb.AppendLine("ID,Name,Entry Date");
        foreach (var brand in brands)
        {
            sb.AppendLine($"{brand.Id},{brand.Name},{brand.EntryDate}");
        }
        var fileName = "Brands.csv";
        var filePath = Path.Combine(Server.MapPath("~/Reports"), fileName);
        System.IO.File.WriteAllText(filePath, sb.ToString());

        return File(filePath, "text/csv", fileName);
    }
}

