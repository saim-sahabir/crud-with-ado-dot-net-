


public static class DateHelper
{
    public static string FormatDate(string date)
    {
        string[] formats = {"dd-MM-yyyy", "dd/MM/yyyy", "dd.MM.yyyy", "dd MMM yyyy"};
        DateTime result;
        if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            return result.ToString("dd-MM-yyyy");
        }
        return string.Empty;
    }
}

///////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


@using YourNamespace.DateHelper

<td>@DateHelper.FormatDate(item.DateColumn)</td>
