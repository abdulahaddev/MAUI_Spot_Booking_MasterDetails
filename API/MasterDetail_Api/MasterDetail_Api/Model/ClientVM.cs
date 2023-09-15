namespace MasterDetail_Api.Model
{
    public class ClientVM
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = default!;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public int PhoneNo { get; set; }
        public bool MaritalStatus { get; set; }
        public List<Spot> SpotList { get; set; } = new List<Spot>();
    }
}
