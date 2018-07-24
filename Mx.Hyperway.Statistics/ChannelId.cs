namespace Mx.Hyperway.Statistics
{
    using System;

    public class ChannelId
    {

        String value;

        public ChannelId(String channelId)
        {
            if (channelId == null)
            {
                this.value = "";
            }
            else
                this.value = channelId;
        }

        public String stringValue()
        {
            return this.ToString();
        }


        public String ToString()
        {
            return this.value;
        }
    }
}
