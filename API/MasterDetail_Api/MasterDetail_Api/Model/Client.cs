using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetail_Api.Model;

public class Client
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public DateTime BirthDate { get; set; }
    public int PhoneNo { get; set; }
    public string Picture { get; set; }
    public bool MaritalStatus { get; set; }

    public virtual ICollection<BookingEntry> bookingEntries { get; set; } = new List<BookingEntry>();

}
