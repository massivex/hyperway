namespace Mx.Hyperway.Statistics
{
    using System;

    using Mx.Tools;

    public class StatisticsGranularity
    {
        public static readonly StatisticsGranularity Year = new StatisticsGranularity("Y");

        public static readonly StatisticsGranularity Month = new StatisticsGranularity("M");

        public static readonly StatisticsGranularity Day = new StatisticsGranularity("D");

        public static readonly StatisticsGranularity Hour = new StatisticsGranularity("H");

        private readonly string abbreviation;

        StatisticsGranularity(string abbreviation)
        {
            this.abbreviation = abbreviation;
        }

        public string GetAbbreviation()
        {
            return this.abbreviation;
        }

        public static StatisticsGranularity ValueForAbbreviation(string abbreviation)
        {
            if (abbreviation == null)
            {
                throw new InvalidOperationException(
                    "null string is an invalid abbreviation for statistics granularity");
            }

            foreach (StatisticsGranularity granularity in Values())
            {
                if (granularity.abbreviation.EqualsIgnoreCase(abbreviation))
                {
                    return granularity;
                }
            }

            throw new ArgumentException("Invalid abbreviation for statistics granularity: " + abbreviation);
        }

        private static StatisticsGranularity[] Values()
        {
            return new[] { Year, Month, Day, Hour };
        }
    }

}
