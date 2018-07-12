

/**
 * Indicates whether the message sent was inbound or outbound with respect to the PEPPOL network.
 * I.e. an outbound message is sent from this access point into the PEPPOL network, while an inbound
 * message is received from the PEPPOL network by this access point.
 *
* @author steinar
*         Date: 25.03.13
*         Time: 14:44
*/
namespace Mx.Oxalis.Api.Model
{
    public enum Direction
    {
        IN,
        OUT
    }
}
