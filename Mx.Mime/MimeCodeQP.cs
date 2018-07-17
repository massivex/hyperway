namespace Mx.Mime
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
	/// 
	/// </summary>
	public class MimeCodeQP : MimeCode
	{
		public MimeCodeQP()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		public override byte[] DecodeToBytes(string s)
		{
			if(s == null)
				throw new ArgumentNullException();

			string line;
			MemoryStream ms = new MemoryStream();
			StringReader sr = new StringReader(s);
			try
			{
				while((line=sr.ReadLine())!=null)
				{
					this.DecodeOneLine(ms, line);
				}
				return ms.ToArray();
			}
			finally
			{
				ms.Close();
				sr.Close();
				ms = null;
				sr = null;
			}
		}

		void DecodeOneLine(MemoryStream ms, string line)
		{
			if(ms == null || line == null)
				throw new ArgumentNullException();

			for(int i=0,j=0; i<line.Length; i++,j++)
			{
				byte b;
				if(line[i] == '=')
				{
					if(i+2 > line.Length) break;//bad encode or endof line
					if(this.IsHex(line[i+1]) && this.IsHex(line[i+2]))
					{
						string hex = line.Substring(i+1, 2);
						b = Convert.ToByte(hex, 16);
						i += 2;
					}
					else
					{
						b = Convert.ToByte(line[++i]);// invalid endcoding, let it go
					}

				}
				else
				{
					b = Convert.ToByte(line[i]);
				}
				ms.WriteByte(b);
			}

			if(!line.EndsWith("="))
			{
				ms.WriteByte(0x0D);
				ms.WriteByte(0x0A);
			}
		}

		bool IsHex(char ch)
		{
			if((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f'))
				return true;
			else
				return false;
		}

		public override string EncodeFromBytes(byte[] inArray, int offset, int length)
		{
			if(inArray == null)
				throw new ArgumentNullException();

			StringBuilder enc = new StringBuilder();
			for(int i=0; i<length; i++)
			{
				int index = offset + i;
				byte b = inArray[index];
				if((b < 33 || b > 126 || b == 0x3D) && b != 0x09 && b != 0x20 && b != 0x0D && b != 0x0A)
				{
					int code = (int)b;
					enc.AppendFormat("={0}", code.ToString("X2"));
					//i++;
				}/*
				else if((b == 0x20 || b == 0x09) && (inArray[index+1] == 0x0D || index == offset+length-1))
				{//space or tab in line end
					int code = (int)b;
					enc.AppendFormat("={0}", code.ToString("X2"));
					i++;
				}
				else if(b == 0x0D && index+4<length && inArray[index+1] == 0x0A && inArray[index+2] == 0x2E && inArray[index+3] == 0x0D && inArray[index+4] == 0x0A)
				{//avoid smtp end sig
					int code = (int)b;
					enc.AppendFormat("={0}", code.ToString("X2"));
				}*/
				else
				{
					enc.Append(System.Convert.ToChar(b));
					//i++;
				}
			}
			enc.Replace(" \r","=20\r",0,enc.Length);
			enc.Replace("\t\r","=09\r",0,enc.Length);
			enc.Replace("\r\n.\r\n","\r\n=2E\r\n",0,enc.Length);
			enc.Replace(" ","=20",enc.Length-1,1);
			return this.FormatEncodedString(enc.ToString());
		}

		string FormatEncodedString(String s)
		{
			const int MAX_CHAR_LEN = 75;
			string line;
			StringReader sr = new StringReader(s);
			StringBuilder sb = new StringBuilder();
			try
			{
				while((line=sr.ReadLine()) != null)
				{
					int index=MAX_CHAR_LEN;
					int lastindex = 0;
					while(index < line.Length)
					{
						if(this.IsHex(line[index]) && this.IsHex(line[index-1]) && line[index-2] == '=')
						{
							index -= 2;
						}
						sb.Append(line.Substring(lastindex, index - lastindex));
						sb.Append("=\r\n");
						lastindex = index;
						index += MAX_CHAR_LEN;
					}
					sb.Append(line.Substring(lastindex, line.Length - lastindex));
					sb.Append("\r\n");
				}
				return sb.ToString();
			}
			finally
			{
				sr.Close();
				sr=null;
				sb=null;
			}
		}

	}
}
