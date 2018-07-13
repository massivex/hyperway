using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Statistics
{
    using Mx.Tools;

    public class StatisticsGranularity
    {
        public static readonly StatisticsGranularity YEAR = new StatisticsGranularity("Y");

        public static readonly StatisticsGranularity MONTH = new StatisticsGranularity("M");

        public static readonly StatisticsGranularity DAY = new StatisticsGranularity("D");

        public static readonly StatisticsGranularity HOUR = new StatisticsGranularity("H");

        private readonly String abbreviation;

        StatisticsGranularity(String abbreviation)
        {
            this.abbreviation = abbreviation;
        }

        public String getAbbreviation()
        {
            return abbreviation;
        }

        public static StatisticsGranularity valueForAbbreviation(String abbreviation)
        {
            if (abbreviation == null)
            {
                throw new InvalidOperationException(
                    "null string is an invalid abbreviation for statistics granularity");
            }

            foreach (StatisticsGranularity granularity in values())
            {
                if (granularity.abbreviation.EqualsIgnoreCase(abbreviation))
                {
                    return granularity;
                }
            }

            throw new ArgumentException("Invalid abbreviation for statistics granularity: " + abbreviation);
        }

        private static StatisticsGranularity[] values()
        {
            return new[] { YEAR, MONTH, DAY, HOUR };
        }
    }

}
