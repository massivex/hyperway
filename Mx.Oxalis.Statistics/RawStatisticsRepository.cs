using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Statistics
{
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
