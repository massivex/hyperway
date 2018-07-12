using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Sbdh
{
    using System.Globalization;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Xml.Sbdh;

    class SbdhHelper
    {

        //        //        // public static JAXBContext JAXB_CONTEXT;

        //        //        public static readonly ObjectFactory OBJECT_FACTORY = new ObjectFactory();

        //        //        public static XMLInputFactory XML_INPUT_FACTORY;

        //        //        public static XMLOutputFactory XML_OUTPUT_FACTORY;

        //        //        public static DatatypeFactory DATATYPE_FACTORY;

        //        //        static {
        //        //        ExceptionUtil.perform(PeppolRuntimeException.class, new PerformAction()
        //        //        {
        //        //            @Override
        //        //            public void action() throws Exception {
        //        //                JAXB_CONTEXT =
        //        //                        JAXBContext.newInstance(StandardBusinessDocument.class, StandardBusinessDocumentHeader.class);

        //        //                XML_INPUT_FACTORY = XMLInputFactory.newFactory();

        //        //                XML_OUTPUT_FACTORY = XMLOutputFactory.newFactory();

        //        //                DATATYPE_FACTORY = DatatypeFactory.newInstance();
        //        //            }
        //        //});
        //        //    }

        //        //    SbdhHelper()
        //        //{

        //        //}

        public static Partner createPartner(ParticipantIdentifier participant)
        {
            PartnerIdentification partnerIdentification = new PartnerIdentification();
            partnerIdentification.Authority = participant.getScheme().getIdentifier();
            partnerIdentification.Value = participant.getIdentifier();

            Partner partner = new Partner();
            partner.Identifier = partnerIdentification;
            return partner;
        }

        public static Scope createScope(ProcessIdentifier processIdentifier)
        {
            Scope scope = new Scope();
            scope.Type = "PROCESSID";
            scope.InstanceIdentifier = processIdentifier.getIdentifier();
            if (!processIdentifier.getScheme().Equals(ProcessIdentifier.DEFAULT_SCHEME))
            {
                scope.Identifier = processIdentifier.getScheme().getIdentifier();
            }

            return scope;
        }

        public static Scope createScope(DocumentTypeIdentifier documentTypeIdentifier)
        {
            Scope scope = new Scope();
            scope.Type = "DOCUMENTID";
            scope.InstanceIdentifier = documentTypeIdentifier.getIdentifier();
            if (!documentTypeIdentifier.getScheme().Equals(DocumentTypeIdentifier.DEFAULT_SCHEME))
            {
                scope.Identifier = documentTypeIdentifier.getScheme().getIdentifier();
            }

            return scope;
        }

        //        public static XMLGregorianCalendar toXmlGregorianCalendar(Date date) // throws SbdhException
        //        {
        //            GregorianCalendar c = new GregorianCalendar();
        //            c.setTime(date);
        //            return DATATYPE_FACTORY.newXMLGregorianCalendar(c);
        //        }

        //        public static Date fromXMLGregorianCalendar(XMLGregorianCalendar calendar)
        //        {
        //            return calendar.toGregorianCalendar().getTime();
        //        }
        //    }


    }
}
