namespace MAUI_MasterDetails.Model;

public class BookingEntry
{
    public int BookingEntryId { get; set; }
    public int ClientId { get; set; }
    public int SpotId { get; set; }

    public Client Client { get; set; }
    public Spot Spot { get; set; }
}
