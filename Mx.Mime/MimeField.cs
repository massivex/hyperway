namespace Mx.Mime
{
    using System;
    using System.Text;

    /// <summary>
	/// 
	/// </summary>
	public class MimeField
	{
		public MimeField()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		private string m_strName;				// field name
		private string m_strValue;				// field value
		private string m_strCharset;			// charset for non-ascii text

		public void SetName(string pszName) { this.m_strName = pszName; }
		public string GetName() { return this.m_strName; }
		public void SetValue(string pszValue) { this.m_strValue = pszValue; }
		public string GetValue() { return this.m_strValue; }
		public void SetCharset(string pszCharset) { this.m_strCharset = pszCharset; }
		public string GetCharset() { return this.m_strCharset; }

		//set field parameter
		public void SetParameter(string pszAttr, string pszValue)
		{
			string newValue = pszAttr + "=" + pszValue;
			string[] strparams = this.m_strValue.Split(';');
			bool hasAttr = false;
			int i=0;
			if(strparams != null)
			{
				for(; i < strparams.Length ; i++)
				{
					int index = strparams[i].IndexOf(pszAttr, 0);
					if(index != -1)
					{
						hasAttr = true;
						break;
					}
				}
			}
			if(hasAttr)
			{
				this.m_strValue.Replace(strparams[i],newValue);
			}
			else
			{
				this.m_strValue += ";" + newValue;
			}
		}

		//get field parameter
		public string GetParameter(string pszAttr)
		{
			string[] strparams = this.m_strValue.Split(';');
			if(strparams != null)
			{
				for(int i=0; i < strparams.Length ; i++)
				{
					int index = strparams[i].IndexOf(pszAttr, 0);
					if(index != -1)
					{
						int begin = strparams[i].IndexOf('=');
						int end;
						if(strparams[i][begin+1] == '"')
						{
							begin += 2;
							end = strparams[i].IndexOf('"', begin);
						}
						else
						{
							begin += 1;
							end = strparams[i].IndexOf('\r', begin);
							if(end == -1) end = strparams[i].Length;
						}

						return strparams[i].Substring(begin, end-begin);
					}
				}
			}
			return null;
		}

		//clear field content
		public void Clear() { this.m_strName = ""; this.m_strValue = ""; this.m_strCharset = ""; }


		//load a field from a string buffer
		public void LoadField(string strData)
		{
			if(strData == null)
				throw new ArgumentNullException();

			int colonIndex = strData.IndexOf(':');
			if(colonIndex != -1)
			{
				this.m_strName = strData.Substring(0, colonIndex);
			}
			else
			{
				this.m_strName = "";
			}

			colonIndex++;
			this.m_strValue = strData.Substring(colonIndex, strData.Length-colonIndex).Trim();
			//need decode!!! and m_strCharset
			MimeCode aFieldCode = MimeCodeManager.Instance.GetCode(this.GetName());
			if(aFieldCode != null)
			{
				this.m_strValue = aFieldCode.DecodeToString(this.m_strValue);
				this.m_strCharset = aFieldCode.Charset;
			}
		}

		//store a field content to a string buffer
		public void Store(StringBuilder sb)
		{
			if(sb == null)
				throw new ArgumentNullException();

			MimeCode aFieldCode = MimeCodeManager.Instance.GetCode(this.GetName());
			if(aFieldCode != null)
			{
				aFieldCode.Charset = this.m_strCharset;
				sb.AppendFormat("{0}: {1}\r\n", this.m_strName, aFieldCode.EncodeFromString(this.m_strValue));
			}
			else
			{
				sb.AppendFormat("{0}: {1}\r\n", this.m_strName, this.m_strValue);
			}
		}

	}
}
