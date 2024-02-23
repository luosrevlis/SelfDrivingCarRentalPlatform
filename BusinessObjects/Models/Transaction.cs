using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models;

public class Transaction
{
    public int Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Contract))]
    public int ContractId { get; set; }
    
    [Required]
    public double InsuranceFee { get; set; }
    
    [Required]
    public double Deposit { get; set; }
    
    [Required]
    public double MortgageFee { get; set; }
    
    public double? DamageFee { get; set; }
    
    public double? LateReturnPenalty { get; set; }
    
    public double? OtherFees { get; set; }
    
    public double TotalPrice { get; set; }
    
    public Contract Contract { get; set; } = null!;
}