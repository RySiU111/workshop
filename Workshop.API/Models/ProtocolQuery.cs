namespace Workshop.API.Models
{
    public class ProtocolQuery
    {
        public string VIN { get; set; }
        public string ProtocolNumber { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(VIN) && !string.IsNullOrEmpty(ProtocolNumber);
        }
    }
}