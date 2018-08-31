namespace Mx.Hyperway.Statistics
{
    using System;

    public interface IStatisticsTransformer
    {
        void StartStatistics(DateTime start, DateTime end);

        /// <summary>
        /// Invoked by the transformer upon the start of a new entry (row, line, etc.) of statistical data
        /// </summary>
        void StartEntry();

        void WriteAccessPointIdentifier(string accessPointIdentifier);

        void WritePeriod(string period);

        void WriteDirection(string direction);

        void WriteParticipantIdentifier(string participantId);

        void WriteDocumentType(string documentType);

        void WriteProfileId(string profileId);

        void WriteChannel(string channel);

        void WriteCount(int count);

        /// <summary>
        /// Completes the current statistics entry 
        /// </summary>
        void EndEntry();

        void EndStatistics();
    }
}
