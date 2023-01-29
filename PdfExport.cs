using System.Data.Entity;
using System.Linq;
using Rotativa;

public class BrandsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult GeneratePdfFile()
    {
        var brands = db.Brands.ToList();
        return new ViewAsPdf("Brands", brands);
    }
}



//////////



@model List<YourNamespace.Brand>

<table>
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th>Entry Date</th>
    </tr>
    @foreach (var brand in Model)
    {
        <tr>
            <td>@brand.Id</td>
            <td>@brand.Name</td>
            <td>@brand.EntryDate</td>
        </tr>
    }
</table>







//////////////////////////////////////////////////////


using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Rotativa;

public class BrandsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult GenerateAndSavePdfFile()
    {
        var brands = db.Brands.ToList();

        var pdf = new ViewAsPdf("BrandsReport", brands);
        var fileStream = new FileStream(Server.MapPath("~/Reports/Brands.pdf"), FileMode.Create);
        pdf.BuildPdf(fileStream);
        fileStream.Close();

        return File(Server.MapPath("~/Reports/Brands.pdf"), "application/pdf", "Brands.pdf");
    }
}


/////////////////////////////////////

@model List<Brand>

<table>
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th>Entry Date</th>
    </tr>
    @foreach (var brand in Model)
    {
        <tr>
            <td>@brand.Id</td>
            <td>@brand.Name</td>
            <td>@brand.EntryDate</td>
        </tr>
    }
</table>







