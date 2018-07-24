namespace Mx.Hyperway.Api.Timestamp
{
    using System;

    using Mx.Peppol.Common.Model;

    public class Timestamp
    {


        /**
         * Timestamp to be presented.
         */
        private readonly DateTime date;

        /**
         * Receipt to be presented
         */
        private readonly Receipt receipt;

        /**
         * Constructor accepting a timestamp and potentially a receipt.
         *
         * @param date    Timestamp to be available.
         * @param receipt Receipt to be available.
         */
        public Timestamp(DateTime date, Receipt receipt)
        {
            this.date = date;
            this.receipt = receipt;
        }

        /**
         * Fetch timestamp.
         *
         * @return Timestamp.
         */
        public DateTime getDate()
        {
            return this.date;
        }

        /**
         * Fetch receipt.
         *
         * @return Optional receipt.
         */
        public Receipt getReceipt()
        {
            return this.receipt;
        }
    }
}
