namespace Mx.Oxalis.Statistics.Model
{
    public class DefaultRawStatistics : AbstractStatistics
    {
        internal DefaultRawStatistics(RawStatisticsBuilder builder): base(builder)
        {

            this.Sender = builder.GetSender();
            this.Receiver = builder.GetReceiver();
        }
    }
}