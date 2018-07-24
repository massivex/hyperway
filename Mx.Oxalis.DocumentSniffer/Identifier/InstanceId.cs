namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;

    using Mx.Peppol.Common.Model;

    /**
     * Represents the Instance Identifier used in the SBDH.
     */
    public class InstanceId
    {


    private readonly String value;

    /** Creates new InstanceId with random UUID */
    public InstanceId()
    {
        this.value = Guid.NewGuid().ToString();
    }

    /** Creates new InstanceId with supplied value */
    public InstanceId(String value)
    {
        this.value = value;
    }


    
    public override String ToString()
    {
        return this.value;
    }

    public InstanceId valueOf(String value)
    {
        return new InstanceId(value);
    }

    public InstanceIdentifier toVefa()
    {
        return InstanceIdentifier.of(this.value);
    }
    }

}
