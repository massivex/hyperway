namespace Mx.Hyperway.Statistics
{
    using System;

    public interface RawStatisticsRepository
    {

        /**
         * Persists another raw statistics entry into table {@code raw_stats}
         */
        int persist(IRawStatistics rawStatistics);

        /**
         * Retrieves data from table <code>raw_stats</code> and transforms it into an appropriate XML document
         */
        void fetchAndTransformRawStatistics(StatisticsTransformer transformer, DateTime start, DateTime end,
                                            StatisticsGranularity granularity);

    }

}
