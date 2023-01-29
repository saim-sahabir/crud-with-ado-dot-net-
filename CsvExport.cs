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
