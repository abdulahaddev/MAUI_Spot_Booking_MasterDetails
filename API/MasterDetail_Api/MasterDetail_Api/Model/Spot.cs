namespace MasterDetail_Api.Model;

public class Spot
{
    public int SpotId { get; set; }
    public string? SpotName { get; set; }
    public virtual ICollection<BookingEntry>? bookingEntries { get; set; } = new List<BookingEntry>();
}
