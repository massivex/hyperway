namespace Mx.Mime
{
    /// <summary>
	/// 
	/// </summary>
	public class MimeConst
	{
		public MimeConst()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		// field names
		public static string MimeVersion { get{return "MIME-Version";} }
		public static string ContentType { get{ return "Content-Type";} }
		public static string TransferEncoding { get{return "Content-Transfer-Encoding";} }
		public static string ContentID { get{return "Content-ID";} }
		public static string ContentDescription { get{return "Content-Description";} }
		public static string ContentDisposition { get{return "Content-Disposition";} }

		// parameter names
		public static string Charset { get{return "charset";} }
		public static string Name { get{return "name";} }
		public static string Filename { get{return "filename";} }
		public static string Boundary { get{return "boundary";} }

		// parameter values
		public static string Encoding7Bit { get{return "7bit";} }
		public static string Encoding8Bit { get{return "8bit";} }
		public static string EncodingBinary { get{return "binary";} }
		public static string EncodingQP { get{return "quoted-printable";} }
		public static string EncodingBase64 { get{return "base64";} }

		public static string MediaText { get{return "text";} }
		public static string MediaImage { get{return "image";} }
		public static string MediaAudio { get{return "audio";} }
		public static string MediaVideo { get{return "vedio";} }
		public static string MediaApplication { get{return "application";} }
		public static string MediaMultiPart { get{return "multipart";} }
		public static string MediaMessage { get{return "message";} }
	}
}
