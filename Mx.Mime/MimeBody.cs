namespace Mx.Mime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
	/// 
	/// </summary>
	public class MimeBody : MimeHeader
	{
		protected MimeBody()
		{ }

		private ArrayList childList;
		private string mContent;

		//store all mime part to a string buffer
		public void StoreBody(StringBuilder sb)
		{
			if(sb == null)
				throw new ArgumentNullException();

			this.StoreHead(sb);
			sb.Append(this.mContent);

			if(MimeType.MediaType.MEDIA_MULTIPART == this.GetMediaType())
			{
				string boundary = this.GetBoundary();
				for(int i=0; i<this.childList.Count; i++)
				{
					sb.AppendFormat("--{0}\r\n", boundary);
					MimeBody aMimeBody = (MimeBody)this.childList[i];
					aMimeBody.StoreBody(sb);
				}
				sb.AppendFormat("--{0}--\r\n", boundary);
			}
		}

	    public void LoadBody(byte[] data, Encoding encoding)
	    {
	        var msg = encoding.GetString(data);
            this.LoadBody(msg);
	    }

		//load all mime part from a string buffer
		public void LoadBody(string strData)
		{
			if(strData == null)
				throw new ArgumentNullException();

			int headend = strData.IndexOf("\r\n\r\n", StringComparison.Ordinal);
			this.LoadHead(strData.Substring(0,headend+2));

			int bodystart = headend + 4;
		    if (MimeType.MediaType.MEDIA_MULTIPART == this.GetMediaType())
		    {
		        string boundary = this.GetBoundary();
		        if (null == boundary)
		        {
		            return;
		        }

		        string strBstart = "--" + boundary;
		        string strBend = strBstart + "--";

		        int nBstart = strData.IndexOf(strBstart, bodystart, StringComparison.Ordinal);
		        if (nBstart == -1) return;
		        int nBend = strData.IndexOf(strBend, bodystart, StringComparison.Ordinal);
		        if (nBend == -1) nBend = strData.Length;

		        if (nBstart > bodystart)
		        {
		            this.mContent = strData.Substring(bodystart, nBstart - bodystart);
		        }

		        while (nBstart < nBend)
		        {
		            nBstart = nBstart + strBstart.Length + 2;
		            int nBstart2 = strData.IndexOf(strBstart, nBstart, StringComparison.Ordinal);
		            if (nBstart2 != -1)
		            {
		                MimeBody childBody = this.CreatePart();
		                childBody.LoadBody(strData.Substring(nBstart, nBstart2 - nBstart));
		            }

		            nBstart = nBstart2;
		        }
		    }
		    else
			{
				this.mContent = strData.Substring(bodystart, strData.Length - bodystart);
			}
		}

		//create a child mime part
		public MimeBody CreatePart(MimeBody parent)
		{
			if(this.childList == null) this.childList = new ArrayList();

			MimeBody aMimeBody = new MimeBody();
			if(parent != null)
			{
				int index = this.childList.IndexOf(parent);
				if(index != -1)
				{
					this.childList.Insert(index+1, aMimeBody);
					return aMimeBody;
				}
			}

			this.childList.Add(aMimeBody);
			return aMimeBody;
		}

		public MimeBody CreatePart()
		{
			return this.CreatePart(null);
		}
		
		// get a list of mime part
		public List<MimeBody> GetBodyPartList()
		{
		    var bodyList = new List<MimeBody>();

			if(this.GetMediaType() != MimeType.MediaType.MEDIA_MULTIPART)
			{
				bodyList.Add(this);
			}
			else
			{
				bodyList.Add(this);
				for(int i=0; i<this.childList.Count; i++)
				{
					MimeBody aMimeBody = (MimeBody)this.childList[i];
					bodyList.AddRange(aMimeBody.GetBodyPartList());
				}
			}

		    return bodyList;
		}

		//operation for text or message media
		public bool IsText()
		{
			return this.GetMediaType() == MimeType.MediaType.MEDIA_TEXT;
		}

		public void SetText(string text)
		{
			if(text == null)
				throw new ArgumentNullException();

			string encoding = this.GetTransferEncoding();
			if(encoding == null)
			{
				encoding = MimeConst.Encoding7Bit;
				this.SetTransferEncoding(encoding);
			}
			MimeCode aCode = MimeCodeManager.Instance.GetCode(encoding);
			aCode.Charset = this.GetCharset();
			this.mContent = aCode.EncodeFromString(text) + "\r\n";

			this.SetContentType("text/plain");
			this.SetCharset(aCode.Charset);

		}

		public string GetText()
		{
			string encoding = this.GetTransferEncoding();
			if(encoding == null)
			{
				encoding = MimeConst.Encoding7Bit;
			}
			MimeCode aCode = MimeCodeManager.Instance.GetCode(encoding);
			aCode.Charset = this.GetCharset();
			return aCode.DecodeToString(this.mContent);
		}

		//operations for message media
		public bool IsMessage()
		{
			return this.GetMediaType() == MimeType.MediaType.MEDIA_MESSAGE;
		}

		public void GetMessage(MimeMessage aMimeMessage)
		{
			if(aMimeMessage == null)
				throw new ArgumentNullException();

			aMimeMessage.LoadBody(this.mContent);
		}

		public void SetMessage(MimeMessage aMimeMessage)
		{
			StringBuilder sb = new StringBuilder();
			aMimeMessage.StoreBody(sb);
			this.mContent = sb.ToString();
			this.SetContentType("message/rfc822");
		}

		// operations for 'image/audio/vedio/application' (attachment) media
		public bool IsAttachment()
		{
			return this.GetName()!=null;
		}

		public void ReadFromFile(string filePathName)
		{
			if(filePathName == null)
				throw new ArgumentNullException();

			StreamReader sr = new StreamReader(filePathName);
			Stream bs = sr.BaseStream;

			byte[] b = new byte[bs.Length];
			bs.Read(b,0,(int)bs.Length);
            
			string encoding = this.GetTransferEncoding();
			if(encoding == null)
			{
				encoding = MimeConst.EncodingBase64;
				this.SetTransferEncoding(encoding);
			}

			MimeCode aCode = MimeCodeManager.Instance.GetCode(encoding);
			aCode.Charset = this.GetCharset();
			this.mContent = aCode.EncodeFromBytes(b) + "\r\n";

			string filename;
			int index = filePathName.LastIndexOf('\\');
			if(index != -1)
			{
				filename = filePathName.Substring(index+1, filePathName.Length - index - 1);
			}
			else
			{
				filename = filePathName;
			}
			this.SetName(filename);

			sr.Close();
		}

	    public byte[] GetBuffer()
	    {
	        string encoding = this.GetTransferEncoding();
	        if (encoding == null)
	        {
	            encoding = MimeConst.Encoding7Bit;
	        }

	        MimeCode aCode = MimeCodeManager.Instance.GetCode(encoding);
	        aCode.Charset = this.GetCharset();
	        return aCode.DecodeToBytes(this.mContent);
	    }

	    public void WriteTo(Stream bs)
	    {
	        var b = this.GetBuffer();
	        bs.Write(b, 0, b.Length);
	    }

        public void WriteToFile(string filePathName)
		{
			if(filePathName == null)
				throw new ArgumentNullException();

		    using (StreamWriter sw = new StreamWriter(filePathName))
		    {
		        Stream bs = sw.BaseStream;

		        var b = this.GetBuffer();
		        bs.Write(b, 0, b.Length);
		        sw.Close();
		    }
		}

		public bool IsMultiPart()
		{
			return this.GetMediaType() == MimeType.MediaType.MEDIA_MULTIPART;
		}

		public void DeleteAllPart()
		{
			this.childList.RemoveRange(0, this.childList.Count);
		}

		public void ErasePart(MimeBody childPart)
		{
			this.childList.Remove(childPart);
		}

	}
}
