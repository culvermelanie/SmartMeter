using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using System;
using System.IO;
using System.Xml;

namespace culsoft.smartmeter
{
    /// <summary>
    /// Specific implementation for the EVN smart meter
    /// </summary>
    public class EVN : ProviderBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="DecryptionKey">HEX key for the decryption of the data</param>
        public EVN(string DecryptionKey):base(DecryptionKey)
        {            
        }

        /// <summary>
        /// Convert the XML from the Gurux library to our business object <see cref="DataRow"/>
        /// </summary>
        /// <param name="XML">Gurux XML after parsing the HEX package</param>
        /// <returns></returns>
        internal override DataRow ParseXML(string XML)
        {
            DataRow data = new DataRow();
            data.XML = XML;

            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(XML)))
            {
                // Load the document and set the root element.  
                XmlDocument doc = new XmlDocument();
                doc.Load(ms);
                XmlNode root = doc.DocumentElement;

                //Zeitstempel
                XmlNode node = root.SelectSingleNode("NotificationBody/DataValue/Structure/OctetString[1]");
                string accessDate = node.Attributes.GetNamedItem("Value").InnerText;
                GXDateTime dt = (GXDateTime)GXDLMSClient.ChangeType(GXCommon.HexToBytes(accessDate), DataType.DateTime, true);
                data.TimeStamp = dt.Value.DateTime;

                //Wirkenergie A+
                data.ActiveEnergyPositive = UInt32.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt32[1]").Attributes.GetNamedItem("Value").InnerText);
                //Wirkenergie A-
                data.ActiveEnergyNegative = UInt32.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt32[2]").Attributes.GetNamedItem("Value").InnerText);                

                //Momentanleistung P+
                data.ActivePowerPositive = UInt32.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt32[3]").Attributes.GetNamedItem("Value").InnerText);
                //Momentanleistung P-
                data.ActivePowerNegative = UInt32.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt32[4]").Attributes.GetNamedItem("Value").InnerText);

                //Spannung L1
                data.Voltage1 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[1]").Attributes.GetNamedItem("Value").InnerText);
                //Spannung L2
                data.Voltage1 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[2]").Attributes.GetNamedItem("Value").InnerText);
                //Spannung L3
                data.Voltage1 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[3]").Attributes.GetNamedItem("Value").InnerText);

                //Strom L1
                data.Current1 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[4]").Attributes.GetNamedItem("Value").InnerText);
                //Strom L2
                data.Current2 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[5]").Attributes.GetNamedItem("Value").InnerText);
                //Strom L3
                data.Current3 = UInt16.Parse(root.SelectSingleNode("NotificationBody/DataValue/Structure/UInt16[6]").Attributes.GetNamedItem("Value").InnerText);
                //Leistungsfaktor

                //Zählernummer
                //Todo: there seems to be an issue to extract the right meter number - only looking at the HEX values, the last view characters are far outside of the normal ASCII range :/
                data.MeterNumber = ConvertHexToString(root.SelectSingleNode("NotificationBody/DataValue/Structure/OctetString[13]").Attributes.GetNamedItem("Value").InnerText);
                //data.MeterNumber = (string)GXDLMSClient.ChangeType(GXCommon.HexToBytes(root.SelectSingleNode("NotificationBody/DataValue/Structure/OctetString[13]").Attributes.GetNamedItem("Value").InnerText), DataType.Str//ing);
            }

            return data;
        }
        private string ConvertHexToString(string hex) 
        {
            string value = "";
            foreach (string cc in BitConverter.ToString((byte[])GXCommon.HexToBytes(hex)).Split("-"))
            {
                int n = Convert.ToInt32(cc, 16);
                char c = (char)n;
                value += c.ToString();
            }

            return value;
        }

       
    }
}
