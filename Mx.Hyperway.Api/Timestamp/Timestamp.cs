namespace Mx.Hyperway.Api.Timestamp
{
    using System;

    using Mx.Peppol.Common.Model;

    public class Timestamp
    {
        /// <summary>
        /// Timestamp to be presented. 
        /// </summary>
        private readonly DateTime date;

        /// <summary>
        /// Receipt to be presented 
        /// </summary>
        private readonly Receipt receipt;

        /// <summary>
        /// Constructor accepting a timestamp and potentially a receipt. 
        /// </summary>
        /// <param name="date">Timestamp to be available.</param>
        /// <param name="receipt">Receipt to be available.</param>
        public Timestamp(DateTime date, Receipt receipt)
        {
            this.date = date;
            this.receipt = receipt;
        }

        /// <summary>
        /// Fetch timestamp. 
        /// </summary>
        /// <returns>timestamp</returns>
        public DateTime GetDate()
        {
            return this.date;
        }

        /// <summary>
        /// Fetch receipt. 
        /// </summary>
        /// <returns>receipt</returns>
        public Receipt GetReceipt()
        {
            return this.receipt;
        }
    }
}
