using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duster.Models;

[Table("executions")]
public class Execution
{
    [Key]
    public int Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Timestamp { get; set; } // of insertion in database.
    public int Commands { get; set; }
    public int Result { get; set; }
    public double Duration { get; set; } // in seconds
}