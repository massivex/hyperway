namespace Mx.Mime
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
	/// 
	/// </summary>
	public class MimeHeader
	{
		protected MimeHeader()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		protected ArrayList m_listFields = new ArrayList();

		//store a head content to a string buffer
		protected virtual void StoreHead(StringBuilder sb)
		{
			if(sb == null)
				throw new ArgumentNullException();

			for(int i=0; i<this.m_listFields.Count; i++)
			{
				MimeField aMimeField = (MimeField)this.m_listFields[i];
				aMimeField.Store(sb);
			}
			sb.Append("\r\n");
		}

		//load a head content from a string buffer
		protected void LoadHead(string strData)
		{
			if(strData == null)
				throw new ArgumentNullException();
			string field="";
			string line;
			StringReader sr = new StringReader(strData);
			try
			{
				line = sr.ReadLine();
				field = line + "\r\n";
				while(line != null)
				{
					line = sr.ReadLine();
					if(line != null && (line[0] == ' ' || line[0] == '\t'))
					{
						field += line + "\r\n";
					}
					else
					{
						MimeField aMimeField = new MimeField();
						aMimeField.LoadField(field);
						this.m_listFields.Add(aMimeField);
						field = line + "\r\n";
					}
				}
			}
			finally
			{
				sr.Close();
				sr = null;
			}
		}

		//find a field according to its field name
		protected MimeField FindField(string pszFieldName)
		{
			for(int i=0; i<this.m_listFields.Count; i++)
			{
				MimeField aMimeField = (MimeField)this.m_listFields[i];
				if(aMimeField.GetName().ToLower() == pszFieldName.ToLower())
				{
					return aMimeField;
				}
			}
			return null;
		}

		public MimeField GetField(string pszFieldName)
		{
			MimeField aMimeField = this.FindField(pszFieldName);
			return aMimeField != null?aMimeField:null;
		}

	    public MimeField[] GetHeaders()
	    {
	        return this.m_listFields.OfType<MimeField>().ToArray();
	    }

		public void SetFieldValue(string pszFieldName, string pszFieldValue, string pszFieldCharset)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			if(aMimeField != null)
			{
				aMimeField.SetValue(pszFieldValue);
				if(pszFieldCharset != null) aMimeField.SetCharset(pszFieldCharset);
			}
			else
			{
				aMimeField = new MimeField();
				aMimeField.SetName(pszFieldName);
				aMimeField.SetValue(pszFieldValue);
				if(pszFieldCharset != null) aMimeField.SetCharset(pszFieldCharset);
				this.m_listFields.Add(aMimeField);
			}
		}

		public string GetFieldValue(string pszFieldName)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			return aMimeField!=null?aMimeField.GetValue():null;
		}

	    public string[] GetAllFieldValue(string fieldName)
	    {
	        var normalizedName = fieldName.ToLowerInvariant();
	        return this.m_listFields.OfType<MimeField>().Where(x => x.GetName().ToLowerInvariant() == normalizedName)
	            .Select(x => x.GetValue()).ToArray();
	    }

		// Content-Type: mediatype/subtype
		public void SetContentType(string pszValue, string pszCharset)
		{
			this.SetFieldValue(MimeConst.ContentType, pszValue, pszCharset);
		}
		public void SetContentType(string pszValue)
		{
			this.SetContentType(pszValue, null);
		}

		public string GetContentType()
		{
			return this.GetFieldValue(MimeConst.ContentType);
		}

		// Content-Type: text/...; charset=...
		public void SetCharset(string pszCharset)
		{
			MimeField aMimeField = this.GetField(MimeConst.ContentType);
			if(aMimeField == null)
			{
				aMimeField = new MimeField();
				aMimeField.SetName(MimeConst.ContentType);
				aMimeField.SetValue("text/plain");
				aMimeField.SetParameter(MimeConst.Charset, "\""+pszCharset+"\"");
				this.m_listFields.Add(aMimeField);
			}
			else
			{
				aMimeField.SetParameter(MimeConst.Charset, "\""+pszCharset+"\"");
			}
		}

		public string GetContentMainType()
		{
			string mainType;
			string contentType = this.GetContentType();
			if(null != contentType)
			{
				int slashIndex = contentType.IndexOf('/', 0);
				if(slashIndex != -1)
				{
					mainType = contentType.Substring(0, slashIndex);
				}
				else
				{
					mainType = contentType;
				}
			}
			else
			{
				mainType = "text";
			}
			return mainType.ToLowerInvariant();
		}

		public string GetContentSubType()
		{
			string subType;
			string contentType = this.GetContentType();
			if(null != contentType)
			{
				int slashIndex = contentType.IndexOf('/', 0);
				if(slashIndex != -1)
				{
					int subTypeEnd = contentType.IndexOf(';', slashIndex+1);
					if(subTypeEnd == -1) subTypeEnd = contentType.IndexOf('\r', slashIndex+1);
					subType = contentType.Substring(slashIndex+1, subTypeEnd-slashIndex-1);
				}
				else
				{
					subType = "";
				}
			}
			else
			{
				subType = "text";
			}
			return subType;
		}

		public MimeType.MediaType GetMediaType()
		{
			string mediaType = this.GetContentMainType();

			int i=0;
			for( ; MimeType.TypeTable[i]!=null; i++)
			{
				if(mediaType.IndexOf(MimeType.TypeTable[i], 0)!=-1)
				{
					return (MimeType.MediaType)i;
				}
			}
			return (MimeType.MediaType)i;
		}

		public string GetCharset()
		{
			return this.GetParameter(MimeConst.ContentType, MimeConst.Charset);
		}

		// Content-Type: image/...; name=...
		public void SetName(string pszName)
		{
			MimeField aMimeField = this.GetField(pszName);
			if(aMimeField == null)
			{
				aMimeField = new MimeField();
				int lastindex = pszName.LastIndexOf('.');
				string strType = "application/octet-stream";
				string ext = pszName.Substring(lastindex + 1, pszName.Length - lastindex - 1);
				int nIndex = 0;
				while(MimeType.TypeCvtTable[nIndex].nMediaType != MimeType.MediaType.MEDIA_UNKNOWN)
				{
					if(MimeType.TypeCvtTable[nIndex].pszFileExt == ext)
					{
						strType = MimeType.TypeTable[(int)MimeType.TypeCvtTable[nIndex].nMediaType];
						strType += '/';
						strType += MimeType.TypeCvtTable[nIndex].pszSubType;
						break;
					}
					nIndex++;
				}
				aMimeField.SetName(MimeConst.ContentType);
				aMimeField.SetValue(strType);
				aMimeField.SetParameter(MimeConst.Name, "\""+pszName+"\"");
				this.m_listFields.Add(aMimeField);
			}
			else
			{
				aMimeField.SetParameter(MimeConst.Name, "\""+pszName+"\"");
			}
		}

		public string GetName()
		{
			return this.GetParameter(MimeConst.ContentType, MimeConst.Name);
		}

		// Content-Type: multipart/...; boundary=...
		public void SetBoundary(string pszBoundary)
		{
			if(pszBoundary == null)
			{
				Random randObj = new Random((int)DateTime.Now.Ticks);
				pszBoundary = "__=_Part_Boundary_"+randObj.Next().ToString()+"_"+randObj.Next().ToString();
			}

			MimeField aMimeField = this.GetField(MimeConst.ContentType);
			if(aMimeField != null)
			{
				if(aMimeField.GetValue().IndexOf("multipart", 0, 9) == -1)
					aMimeField.SetValue("multipart/mixed");
				aMimeField.SetParameter(MimeConst.Boundary, "\""+pszBoundary+"\"");
			}
			else
			{
				aMimeField = new MimeField();
				aMimeField.SetName(MimeConst.ContentType);
				aMimeField.SetValue("multipart/mixed");
				aMimeField.SetParameter(MimeConst.Boundary, "\""+pszBoundary+"\"");
				this.m_listFields.Add(aMimeField);
			}
		}

		public string GetBoundary()
		{
			return this.GetParameter(MimeConst.ContentType, MimeConst.Boundary);
		}

		public bool SetParameter(string pszFieldName, string pszAttr, string pszValue)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			if(aMimeField != null)
			{
				aMimeField.SetParameter(pszAttr, pszValue);
				return true;
			}
			return false;
		}

		public string GetParameter(string pszFieldName, string pszAttr)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			return aMimeField != null?aMimeField.GetParameter(pszAttr):null;
		}

		public void SetFieldCharset(string pszFieldName, string pszFieldCharset)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			if(aMimeField != null)
			{
				aMimeField.SetCharset(pszFieldCharset);
			}
			else
			{
				aMimeField = new MimeField();
				aMimeField.SetCharset(pszFieldCharset);
				this.m_listFields.Add(aMimeField);
			}
		}

		public string GetFieldCharset(string pszFieldName)
		{
			MimeField aMimeField = this.GetField(pszFieldName);
			return aMimeField != null?aMimeField.GetCharset():null;
		}

		// Content-Transfer-Encoding: ...
		public void SetTransferEncoding(string pszValue)
		{
			this.SetFieldValue(MimeConst.TransferEncoding, pszValue, null);
		}

		public string GetTransferEncoding()
		{
			return this.GetFieldValue(MimeConst.TransferEncoding);
		}

		// Content-Disposition: ...
		public void SetDisposition(string pszValue, string pszCharset)
		{
			this.SetFieldValue(MimeConst.ContentDisposition, pszValue, pszCharset);
		}

		public string GetDisposition()
		{
			return this.GetFieldValue(MimeConst.ContentDisposition);
		}

		// Content-Disposition: ...; filename=...
		public string GetFilename()
		{
			return this.GetParameter(MimeConst.ContentDisposition, MimeConst.Filename);
		}

		public void SetDescription(string pszValue, string pszCharset)
		{
			this.SetFieldValue(MimeConst.ContentDescription, pszValue, pszCharset);
		}

		public string GetDiscription()
		{
			return this.GetFieldValue(MimeConst.ContentDescription);
		}
	}
}
