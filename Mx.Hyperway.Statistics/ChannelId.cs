namespace Mx.Hyperway.Statistics
{
    public class ChannelId
    {

        string value;

        public ChannelId(string channelId)
        {
            if (channelId == null)
            {
                this.value = "";
            }
            else
                this.value = channelId;
        }

        public string StringValue()
        {
            return this.ToString();
        }


        public override string ToString()
        {
            return this.value;
        }
    }
}
