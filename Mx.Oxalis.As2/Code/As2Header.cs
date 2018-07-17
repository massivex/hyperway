using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Code
{
    public class As2Header
    {

        public static readonly String AS2_VERSION = "AS2-Version";

        public static readonly String AS2_FROM = "AS2-From";

        public static readonly String AS2_TO = "AS2-To";

        public static readonly String SUBJECT = "Subject";

        public static readonly String MESSAGE_ID = "Message-ID";

        public static readonly String DATE = "Date";

        public static readonly String DISPOSITION_NOTIFICATION_TO = "Disposition-Notification-To";

        public static readonly String DISPOSITION_NOTIFICATION_OPTIONS = "Disposition-Notification-Options";

        public static readonly String RECEIPT_DELIVERY_OPTION = "Receipt-Delivery-Option";

        public static readonly String SERVER = "Server";

        // PEPPOL Transport Infrastructure AS2 Profile specifies AS2 version 1.0
        public static readonly String VERSION = "1.0";

    }

}
