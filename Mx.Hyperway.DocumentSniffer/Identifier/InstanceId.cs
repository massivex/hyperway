namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;

    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Represents the Instance Identifier used in the SBDH. 
    /// </summary>
    public class InstanceId
    {


        private readonly string value;

        /// <summary>
        /// Creates new InstanceId with random UUID 
        /// </summary>
        public InstanceId()
        {
            this.value = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates new InstanceId with supplied value 
        /// </summary>
        /// <param name="value"></param>
        public InstanceId(string value)
        {
            this.value = value;
        }



        public override string ToString()
        {
            return this.value;
        }

        public InstanceId ValueOf(string newValue)
        {
            return new InstanceId(newValue);
        }

        public InstanceIdentifier ToVefa()
        {
            return InstanceIdentifier.Of(this.value);
        }
    }
}
