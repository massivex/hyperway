using System;

namespace Mx.Peppol.Common.Model
{
    public class Header
    {
        public static Header NewInstance()
        {
            return new Header();
        }

        public static Header Of(
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

        public static Header Of(
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
            this.Sender = sender;
            this.Receiver = receiver;
            this.Process = process;
            this.DocumentType = documentType;
            this.Identifier = identifier;
            this.InstanceType = instanceType;
            this.CreationTimestamp = creationTimestamp;
        }

        public ParticipantIdentifier Sender { get; private set; }

        public Header SetSender(ParticipantIdentifier value)
        {
            Header header = this.Copy();
            header.Sender = value;
            return header;
        }

        public ParticipantIdentifier Receiver { get; private set; }

        public Header SetReceiver(ParticipantIdentifier value)
        {
            Header header = this.Copy();
            header.Receiver = value;
            return header;
        }

        public ProcessIdentifier Process { get; private set; }

        public Header SetProcess(ProcessIdentifier value)
        {
            Header header = this.Copy();
            header.Process = value;
            return header;
        }

        public DocumentTypeIdentifier DocumentType { get; private set; }

        public Header SetDocumentType(DocumentTypeIdentifier value)
        {
            Header header = this.Copy();
            header.DocumentType = value;
            return header;
        }

        public InstanceIdentifier Identifier { get; private set; }

        public Header SetIdentifier(InstanceIdentifier value)
        {
            Header header = this.Copy();
            header.Identifier = value;
            return header;
        }

        public InstanceType InstanceType { get; private set; }

        public Header SetInstanceType(InstanceType value)
        {
            Header header = this.Copy();
            header.InstanceType = value;
            return header;
        }

        public DateTime? CreationTimestamp { get; private set; }

        public Header SetCreationTimestamp(DateTime value)
        {
            Header header = this.Copy();
            header.CreationTimestamp = value;
            return header;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Header)) return false;

            Header header = (Header)o;

            if (!this.Sender.Equals(header.Sender)) return false;
            if (!this.Receiver.Equals(header.Receiver)) return false;
            if (!this.Process.Equals(header.Process)) return false;
            if (!this.DocumentType.Equals(header.DocumentType)) return false;
            if (!this.Identifier?.Equals(header.Identifier) ?? header.Identifier != null) return false;
            if (!this.InstanceType?.Equals(header.InstanceType) ?? header.InstanceType != null) return false;

            return !(this.CreationTimestamp != null
                         ? !this.CreationTimestamp.Equals(header.CreationTimestamp)
                         : header.CreationTimestamp != null);
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            var result = this.Sender.GetHashCode();
            result = 31 * result + this.Receiver.GetHashCode();
            result = 31 * result + this.Process.GetHashCode();
            result = 31 * result + this.DocumentType.GetHashCode();
            result = 31 * result + (this.Identifier != null ? this.Identifier.GetHashCode() : 0);
            result = 31 * result + (this.InstanceType != null ? this.InstanceType.GetHashCode() : 0);
            result = 31 * result + (this.CreationTimestamp != null ? this.CreationTimestamp.GetHashCode() : 0);
            // ReSharper restore NonReadonlyMemberInGetHashCode
            return result;
        }

        public override string ToString()
        {
            return "Header{" + "sender=" + this.Sender + ", receiver=" + this.Receiver + ", process=" + this.Process
                   + ", documentType=" + this.DocumentType + ", identifier=" + this.Identifier + ", instanceType=" + this.InstanceType
                   + ", creationTimestamp=" + this.CreationTimestamp + '}';
        }

        private Header Copy()
        {
            return new Header(this.Sender, this.Receiver, this.Process, this.DocumentType, this.Identifier, this.InstanceType, this.CreationTimestamp);
        }
    }
}
