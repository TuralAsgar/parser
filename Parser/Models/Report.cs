using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parser.Models;

public class Report
{
    [Key]
    public int Id { get; set; }
    [Column("meta_id")]
    public int MetaId { get; set; }
    [Column("settlement")]
    public string? Settlement { get; set; }
    [Column("transaction_credit")]
    public decimal TransactionCredit { get; set; }
    [Column("reconciliation_credit")]
    public decimal ReconciliationCredit { get; set; }
    [Column("fee_credit")]
    public decimal FeeCredit { get; set; }
    [Column("transaction_debit")]
    public decimal TransactionDebit { get; set; }
    [Column("reconciliation_debit")]
    public decimal ReconciliationDebit { get; set; }
    [Column("fee_debit")]
    public decimal FeeDebit { get; set; }
    [Column("count")]
    public decimal Count { get; set; }
    [Column("net")]
    public decimal Net { get; set; }

    public Meta? Meta { get; set; }
}