using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parser.Models;

public class Meta
{
    [Key] public int Id { get; set; }
    public string? Institution { get; set; }
    [Column("file_id")] 
    public string? FileId { get; set; }

    [Column("date")] 
    public DateTime Date { get; set; }

    [Column("transaction_currency")]
    public string? TransactionCurrency { get; set; }

    [Column("reconciliation_currency")]
    public string? ReconciliationCurrency { get; set; }

    public ICollection<Report>? Reports { get; set; }
}