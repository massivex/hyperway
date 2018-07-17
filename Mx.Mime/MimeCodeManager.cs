namespace Mx.Mime
{
    using System.Collections;

    /// <summary>
	/// 
	/// </summary>
	public class MimeCodeManager
	{
		private MimeCodeManager()
		{
			// 
			// TODO: Add constructor logic here
			//
			this.InitialCode();
		}

		private static Hashtable codeHT = new Hashtable();

		private static readonly MimeCodeManager instance = new MimeCodeManager();

		public static MimeCodeManager Instance
		{
			get{ return instance;}
		}

		private void InitialCode()
		{
			MimeCode aFieldCode = new MimeFieldCodeBase();
			this.SetCode("Subject", aFieldCode);
			this.SetCode("Comments", aFieldCode);
			this.SetCode("Content-Description", aFieldCode);

			aFieldCode = new MimeFieldCodeAddress();
			this.SetCode("From", aFieldCode);
			this.SetCode("To", aFieldCode);
			this.SetCode("Resent-To", aFieldCode);
			this.SetCode("Cc", aFieldCode);
			this.SetCode("Resent-Cc", aFieldCode);
			this.SetCode("Bcc", aFieldCode);
			this.SetCode("Resent-Bcc", aFieldCode);
			this.SetCode("Reply-To", aFieldCode);
			this.SetCode("Resent-Reply-To", aFieldCode);
			
			aFieldCode = new MimeFieldCodeParameter();
			this.SetCode("Content-Type", aFieldCode);
			this.SetCode("Content-Disposition", aFieldCode);

			MimeCode aCode = new MimeCode();
			this.SetCode("7bit", aCode);
			
			this.SetCode("8bit", aCode);

			aCode = new MimeCodeBase64();
			this.SetCode("base64", aCode);

			aCode = new MimeCodeQP();
			this.SetCode("quoted-printable", aCode);

		}

		public void SetCode(string name, MimeCode code)
		{
			codeHT.Add(name.ToLower(), code);
		}

		public MimeCode GetCode(string name)
		{
			return (MimeCode)codeHT[name.ToLower()];
		}
	}
}
