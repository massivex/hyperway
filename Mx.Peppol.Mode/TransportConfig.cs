namespace Mx.Peppol.Mode
{
    public class TransportConfig
    {
        public TransportConfig()
        {
            // Default behaviour
            this.Enabled = true;
        }
        public string Profile { get; set; }
        public string Sender { get; set; }
        public int Weight { get; set; }
        public bool Enabled { get; set; }
    }
}