namespace Mx.Hyperway.Statistics
{
    using System;

    public interface IRawStatisticsRepository
    {
        /// <summary>
        /// Persists another raw statistics entry into table {@code raw_stats} 
        /// </summary>
        /// <param name="rawStatistics"></param>
        /// <returns></returns>
        int Persist(IRawStatistics rawStatistics);

        /// <summary>
        /// Retrieves data from table <code>raw_stats</code> and transforms it into an appropriate XML document 
        /// </summary>
        /// <param name="transformer"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="granularity"></param>
        void FetchAndTransformRawStatistics(IStatisticsTransformer transformer, DateTime start, DateTime end,
                                            StatisticsGranularity granularity);

    }

}
