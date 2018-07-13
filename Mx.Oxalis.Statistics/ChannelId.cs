using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Statistics
{
    public class ChannelId
    {

        String value;

        public ChannelId(String channelId)
        {
            if (channelId == null)
            {
                value = "";
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
            return value;
        }
    }
}
