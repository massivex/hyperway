namespace Mx.Mime
{
    using System;
    using System.Text;

    /// <summary>
	/// 
	/// </summary>
	public class MimeCodeBase64 : MimeCode
	{
		public MimeCodeBase64()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		public override byte[] DecodeToBytes(string s)
		{
			if(s == null)
				throw new ArgumentNullException();

			return System.Convert.FromBase64String(s);
		}

		public override string EncodeFromBytes(byte[] inArray, int offset, int length)
		{
			if(inArray == null)
				throw new ArgumentNullException();

			return this.FormatEncodedString(System.Convert.ToBase64String(inArray, offset, length));
		}

		string FormatEncodedString(string s)
		{
			if(s == null)
				throw new ArgumentNullException();

			const int MAX_CHAR_LEN = 76;
			int index = 0;
			StringBuilder sb = new StringBuilder();
			while((index+MAX_CHAR_LEN) < s.Length)
			{
				sb.AppendFormat("{0}\r\n", s.Substring(index, MAX_CHAR_LEN));
				index += MAX_CHAR_LEN;
			}
			sb.AppendFormat("{0}", s.Substring(index, s.Length - index));
			return sb.ToString();
		}

	}
}
