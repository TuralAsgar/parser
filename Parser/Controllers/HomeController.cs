using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parser.Data;
using Parser.Models;

namespace Parser.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        if (_db.Metas != null)
        {
            var metaList = _db.Metas.Include(meta=>meta.Reports).ToList();
            return View(metaList);
        }
        
        

        return View(new List<Meta>());
    }

    public async Task<IActionResult> Parse(IFormFile file)
    {
        var filePath = "";
        if (file.Length > 0)
        {
            // full path to file in temp location
            filePath = Path.GetTempFileName();
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        var lines = System.IO.File.ReadAllLines(filePath);

        // meta variables
        var institution = "";
        var date = "";
        var fileId = "";
        var transactionCurrency = "";
        var reconciliationCurrency = "";

        var metaId = 0;
        foreach (string line in lines)
        {
            var metaDone = false;
            // clean whitespaces
            var trimmedLine = Regex.Replace(line, "\\s+", " ");

            if (trimmedLine.Contains("Financial Institution"))
            {
                var parts = trimmedLine.Split(':');
                institution = parts[1].Trim();
            }

            if (trimmedLine.Contains("FX Settlement Date"))
            {
                var parts = trimmedLine.Split(':');
                date = parts[1].Trim();
            }

            if (trimmedLine.Contains("Reconciliation File ID"))
            {
                var parts = trimmedLine.Split(':');
                fileId = parts[1].Trim();
            }

            if (trimmedLine.Contains("Transaction Currency"))
            {
                var parts = trimmedLine.Split(':');
                transactionCurrency = parts[1].Trim();
            }

            if (trimmedLine.Contains("Reconciliation Currency"))
            {
                metaDone = true;
                var parts = trimmedLine.Split(':');
                reconciliationCurrency = parts[1].Trim();
            }

            if (metaDone)
            {
                var meta = new Meta
                {
                    Institution = institution,
                    Date = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    FileId = fileId,
                    TransactionCurrency = transactionCurrency,
                    ReconciliationCurrency = reconciliationCurrency
                };
                _db.Metas?.Add(meta);
                await _db.SaveChangesAsync();
                metaId = meta.Id;
            }

            var expression = new Regex(
                @"\s+(?<settlement>[\w\s]+)\s+(?<transactioncredit>[-\s\d]*\.?\d+)\s+(?<reconciliationcredit>[-\s\d]*\.?\d+)\s+(?<feecredit>[-\s\d]*\.?\d+)\s+(?<transactiondebit>[-\s\d]*\.?\d+)\s+(?<reconciliationdebit>[-\s\d]*\.?\d+)\s+(?<feedebit>[-\s\d]*\.?\d+)\s+(?<count>[-\s\d]*\.?\d+)\s+(?<net>[-\s\d]*\.?\d+)");
            var match = expression.Match(trimmedLine);
            if (match.Success)
            {
                var report = new Report
                {
                    MetaId = metaId,

                    Settlement = match.Groups["settlement"].Value,
                    TransactionCredit = Convert.ToDecimal(match.Groups["transactioncredit"].Value),
                    ReconciliationCredit = Convert.ToDecimal(match.Groups["reconciliationcredit"].Value),
                    FeeCredit = Convert.ToDecimal(match.Groups["feecredit"].Value),
                    TransactionDebit = Convert.ToDecimal(match.Groups["transactiondebit"].Value),
                    ReconciliationDebit = Convert.ToDecimal(match.Groups["reconciliationdebit"].Value),
                    FeeDebit = Convert.ToDecimal(match.Groups["feedebit"].Value),
                    Count = Convert.ToDecimal(match.Groups["count"].Value),
                    Net = Convert.ToDecimal(match.Groups["net"].Value),
                };
                _db.Reports?.Add(report);
                await _db.SaveChangesAsync();
            }
        }

        return Ok(new { filePath });
    }
}