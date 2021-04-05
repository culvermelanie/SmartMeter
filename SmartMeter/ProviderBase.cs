using Gurux.DLMS;
using Gurux.DLMS.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace culsoft.smartmeter
{
   /// <summary>
   /// Base class to be used for every provider
   /// </summary>
    public abstract class ProviderBase
    {
        private string _DecryptionKey;

        /// <summary>
        /// Key to decrypt the message package
        /// </summary>
        public string DecryptionKey
        {
            get { return _DecryptionKey; }
            set { 
                _DecryptionKey = value.Replace("-", "").Replace(" ", ""); //remove any - or blanks
                }
        }

        /// <summary>
        /// Base ctor for any provider
        /// </summary>
        /// <param name="DecryptionKey"></param>
        public ProviderBase(string DecryptionKey)
        {
            this.DecryptionKey = DecryptionKey;
        }

        /// <summary>
        /// Actual processor of the Hex Message from the smart meter
        /// </summary>
        /// <param name="DataRow">HEX message</param>
        /// <returns></returns>
        public virtual DataRow ProcessDataRow(string DataRow)
        {
            var aes = new AesGcmEncryption(this.DecryptionKey);
            GXDLMSTranslator translator = new GXDLMSTranslator(TranslatorOutputType.SimpleXml);
            translator.Comments = true;
            translator.Hex = false;
            translator.ShowStringAsHex = false;

            //We have to call encrypt and not decrypt - this is how the EVN integration works at least :/
            string hexStringDecrypted = aes.Encrypt(DataRow);
            string xml = translator.PduToXml(hexStringDecrypted);

            return this.ParseXML(xml); 
        }

        /// <summary>
        /// Process multiple HEX messages
        /// </summary>
        /// <param name="DataRows"></param>
        /// <returns></returns>
        public virtual List<DataRow> ProcessDataRows(List<string> DataRows)
        {
            List<DataRow> result = new List<DataRow>();
            foreach (string dr in DataRows)
            {
                result.Add(this.ProcessDataRow(dr));
            }

            return result;
        }

        /// <summary>
        /// This methond should be overriden in each derived provider class, because the implemenation for each provider will be different
        /// </summary>
        /// <param name="XML"></param>
        /// <returns></returns>
        internal virtual DataRow ParseXML(string XML)
        {
            return new DataRow();
        }
    }
}
