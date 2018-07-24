namespace Mx.Hyperway.Statistics
{
    using System;

    public interface StatisticsTransformer
    {


        void startStatistics(DateTime start, DateTime end);

        /** Invoked by the transformer upon the start of a new entry (row, line, etc.) of statistical data */
        void startEntry();

        void writeAccessPointIdentifier(String accessPointIdentifier);

        void writePeriod(String period);

        void writeDirection(String direction);

        void writeParticipantIdentifier(String participantId);

        void writeDocumentType(String documentType);

        void writeProfileId(String profileId);

        void writeChannel(String channel);

        void writeCount(int count);

        /** Completes the current statistics entry */
        void endEntry();

        void endStatistics();
    }
}
