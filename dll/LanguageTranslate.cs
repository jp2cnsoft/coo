using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Collections;

namespace Seika.Util.Translate
{
    /// <summary>
    /// Summary description for LanguageDict
    /// </summary>
    public class LanguageTranslate
    {

        /// <summary> 
        /// 使用WebRequest获取Google翻译后的内容 
        /// </summary> 
        /// <param name="strTranslateString">需要翻译的内容</param> 
        /// <param name="strRequestLanguage">原文语种</param> 
        /// <param name="strResultLanguage">译文语种</param> 
        /// <returns></returns> 
        static public string GetTranslateString(
            string strTranslateString, string strRequestLanguage, string strResultLanguage)
        {
            string wsUrlTmp = "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}%7C{2}";

            
            WebRequest request = null;
            HttpWebResponse response = null;
            Stream dataStream = null;
            StreamReader reader = null;
            MemoryStream ms = null;

            try
            {
                ArrayList ar = new ArrayList();
                StringBuilder sb = new StringBuilder();

                int i = 0;

                while (i < strTranslateString.Length) 
                {
                    if (i + 1024 > strTranslateString.Length)
                    {
                        ar.Add(strTranslateString.Substring(i));
                    }
                    else
                    {
                        ar.Add(strTranslateString.Substring(i, 1024));
                    }
                    i += 1024;
                }

                for (int t = 0; t < ar.Count; t++)
                {

                    string wsUtl = String.Format(
                        wsUrlTmp, (string)ar[t], strRequestLanguage, strResultLanguage);

                    request = HttpWebRequest.Create(wsUtl);
                    request.Credentials = CredentialCache.DefaultCredentials;

                    response = (HttpWebResponse)request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);

                    String jsonResponse = reader.ReadToEnd();

                    DataContractJsonSerializer ser =
                        new DataContractJsonSerializer(typeof(TranslatedText));

                    ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonResponse));

                    TranslatedText s = (TranslatedText)ser.ReadObject(ms);

                    if (s.responseData != null && s.responseData.translatedText != null)
                    {
                        sb.Append(s.responseData.translatedText);
                    }
                }

                return sb.ToString();
            }
            finally
            {
                if (ms != null) ms.Close();
                if (reader != null) reader.Close();
                if (dataStream != null) dataStream.Close();
                if (response != null) response.Close();
            }
        }
    }

    [DataContract]
    class TranslatedText
    {
        [DataMember]
        public ResponseData responseData { get; set; }
        [DataMember]
        public string responseDetails { get; set; }
        [DataMember]
        public string responseStatus { get; set; }
    }

    [DataContract]
    class ResponseData
    {
        [DataMember]
        public string translatedText { get; set; }
    }
}
