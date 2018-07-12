using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    public class Header
    {

        private static readonly long serialVersionUID = -7517561747468194479L;

        private ParticipantIdentifier _sender;

        private ParticipantIdentifier _receiver;

        private ProcessIdentifier _process;

        private DocumentTypeIdentifier _documentType;

        private InstanceIdentifier _identifier;

        private InstanceType _instanceType;

        private DateTime? _creationTimestamp;

        public static Header newInstance()
        {
            return new Header();
        }

        public static Header of(
            ParticipantIdentifier sender,
            ParticipantIdentifier receiver,
            ProcessIdentifier process,
            DocumentTypeIdentifier documentType,
            InstanceIdentifier identifier,
            InstanceType instanceType,
            DateTime? creationTimestamp)
        {
            return new Header(sender, receiver, process, documentType, identifier, instanceType, creationTimestamp);
        }

        public static Header of(
            ParticipantIdentifier sender,
            ParticipantIdentifier receiver,
            ProcessIdentifier process,
            DocumentTypeIdentifier documentType)
        {
            return new Header(sender, receiver, process, documentType, null, null, null);
        }

        private Header()
        {
            // No action.
        }

        private Header(
            ParticipantIdentifier sender,
            ParticipantIdentifier receiver,
            ProcessIdentifier process,
            DocumentTypeIdentifier documentType,
            InstanceIdentifier identifier,
            InstanceType instanceType,
            DateTime? creationTimestamp)
        {
            this._sender = sender;
            this._receiver = receiver;
            this._process = process;
            this._documentType = documentType;
            this._identifier = identifier;
            this._instanceType = instanceType;
            this._creationTimestamp = creationTimestamp;
        }

        public ParticipantIdentifier getSender()
        {
            return this._sender;
        }

        public Header sender(ParticipantIdentifier sender)
        {
            Header header = this.copy();
            header._sender = sender;
            return header;
        }

        public ParticipantIdentifier getReceiver()
        {
            return this._receiver;
        }

        public Header receiver(ParticipantIdentifier receiver)
        {
            Header header = this.copy();
            header._receiver = receiver;
            return header;
        }

        public ProcessIdentifier getProcess()
        {
            return this._process;
        }

        public Header process(ProcessIdentifier process)
        {
            Header header = this.copy();
            header._process = process;
            return header;
        }

        public DocumentTypeIdentifier getDocumentType()
        {
            return this._documentType;
        }

        public Header documentType(DocumentTypeIdentifier documentType)
        {
            Header header = this.copy();
            header._documentType = documentType;
            return header;
        }

        public InstanceIdentifier getIdentifier()
        {
            return this._identifier;
        }

        public Header identifier(InstanceIdentifier identifier)
        {
            Header header = this.copy();
            header._identifier = identifier;
            return header;
        }

        public InstanceType getInstanceType()
        {
            return this._instanceType;
        }

        public Header instanceType(InstanceType instanceType)
        {
            Header header = this.copy();
            header._instanceType = instanceType;
            return header;
        }

        public DateTime? getCreationTimestamp()
        {
            return this._creationTimestamp;
        }

        public Header creationTimestamp(DateTime creationTimestamp)
        {
            Header header = this.copy();
            header._creationTimestamp = creationTimestamp;
            return header;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Header)) return false;

            Header header = (Header)o;

            if (!this._sender.Equals(header._sender)) return false;
            if (!this._receiver.Equals(header._receiver)) return false;
            if (!this._process.Equals(header._process)) return false;
            if (!this._documentType.Equals(header._documentType)) return false;
            if (this._identifier != null
                    ? !this._identifier.Equals(header._identifier)
                    : header._identifier != null) return false;
            if (this._instanceType != null
                    ? !this._instanceType.Equals(header._instanceType)
                    : header._instanceType != null)
                return false;
            return !(this._creationTimestamp != null
                         ? !this._creationTimestamp.Equals(header._creationTimestamp)
                         : header._creationTimestamp != null);
        }

        public override int GetHashCode()
        {
            int result = this._sender.GetHashCode();
            result = 31 * result + this._receiver.GetHashCode();
            result = 31 * result + this._process.GetHashCode();
            result = 31 * result + this._documentType.GetHashCode();
            result = 31 * result + (this._identifier != null ? this._identifier.GetHashCode() : 0);
            result = 31 * result + (this._instanceType != null ? this._instanceType.GetHashCode() : 0);
            result = 31 * result + (this._creationTimestamp != null ? this._creationTimestamp.GetHashCode() : 0);
            return result;
        }

        public override String ToString()
        {
            return "Header{" + "sender=" + this._sender + ", receiver=" + this._receiver + ", process=" + this._process
                   + ", documentType=" + this._documentType + ", identifier=" + this._identifier + ", instanceType=" + this._instanceType
                   + ", creationTimestamp=" + this._creationTimestamp + '}';
        }

        private Header copy()
        {
            return new Header(this._sender, this._receiver, this._process, this._documentType, this._identifier, this._instanceType, this._creationTimestamp);
        }
    }
}
