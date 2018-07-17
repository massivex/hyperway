namespace Mx.Mime
{
    using System;

    /// <summary>
	/// 
	/// </summary>
	public class MimeMessage : MimeBody
	{
		public MimeMessage() { }

		// set/get RFC 822 message header fields
		public void SetFrom(string from, string charset)
		{
			this.SetFieldValue("From", from, charset);
		}
		public string GetFrom()
		{
			return this.GetFieldValue("From");
		}

		public void SetTo(string to, string charset)
		{
			this.SetFieldValue("To", to, charset);
		}
		public string GetTo()
		{
			return this.GetFieldValue("To");
		}

		public void SetCC(string cc, string charset)
		{
			this.SetFieldValue("CC", cc, charset);
		}
		public string GetCC()
		{
			return this.GetFieldValue("CC");
		}

		public void SetBCC(string bcc, string charset)
		{
			this.SetFieldValue("BCC", bcc, charset);
		}
		public string GetBCC()
		{
			return this.GetFieldValue("BCC");
		}

		public void SetSubject(string subject, string charset)
		{
			this.SetFieldValue("Subject", subject, charset);
		}
		public string GetSubject()
		{
			return this.GetFieldValue("Subject");
		}

		public void SetDate(string date, string charset)
		{
			this.SetFieldValue("Date", date, charset);
		}
		public void SetDate()
		{
			string dt = DateTime.Now.ToString("r",System.Globalization.DateTimeFormatInfo.InvariantInfo);
			dt = dt.Replace("GMT",DateTime.Now.ToString("zz",System.Globalization.DateTimeFormatInfo.InvariantInfo)+"00");
			this.SetFieldValue("Date",dt , null);
		}
		public string GetDate()
		{
			return this.GetFieldValue("Date");
		}

		public void Setversion()
		{
			this.SetFieldValue(MimeConst.MimeVersion, "1.0", null);
		}

	}
}
