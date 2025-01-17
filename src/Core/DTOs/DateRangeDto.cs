using System.ComponentModel.DataAnnotations;

[ValidDateRange]
public class DateRangeDto
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTimeOffset? StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTimeOffset? EndDate { get; set; }
}
