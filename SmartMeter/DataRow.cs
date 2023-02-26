using System;

namespace culsoft.smartmeter
{
    /// <summary>
    /// Data object representing one network provider message.
    /// Currently based on the EVN message package.
    /// </summary>
    public struct Dataset
    {
        public UInt16 Value { get; set; }
        public sbyte Scale { get; set; } // Int8
    }
    public class DataRow
    {
        /// <summary>
        /// Time of the recorded data
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Number of the smart meter - for EVN currently corrupted value
        /// </summary>
        public String MeterNumber { get; set; }

        /// <summary>
        /// Wirkenergie +
        /// Keine Skalierung Wh
        /// </summary>
        public UInt32 ActiveEnergyPositive { get; set; }//Gurux.DLMS.Enums.Unit.ActiveEnergy

        /// <summary>
        /// Wirkenergie -
        /// Keine Skalierung Wh
        /// </summary>
        public UInt32 ActiveEnergyNegative { get; set; }//Gurux.DLMS.Enums.Unit.ActiveEnergy
        
        /// <summary>
        /// Momentanleistung P+
        /// Keine Skalierung W
        /// </summary>
        public UInt32 ActivePowerPositive { get; set; } ////Gurux.DLMS.Enums.Unit.ActivePower
        
        /// <summary>
        /// Momentanleistung P-
        /// Keine Skalierung W
        /// </summary>
        public UInt32 ActivePowerNegative { get; set; } ////Gurux.DLMS.Enums.Unit.ActivePower

        /// <summary>
        /// Spannung L1
        /// Wert x10^-1
        /// V
        /// </summary>
        public Dataset Voltage1; //Gurux.DLMS.Enums.Unit.Voltage
        /// <summary>
        /// Spannung L2
        /// Wert x10^-1
        /// V
        /// </summary>
        public Dataset Voltage2; //Gurux.DLMS.Enums.Unit.Voltage
        /// <summary>
        /// Spannung L3
        /// Wert x10^-1
        /// V
        /// </summary>
        public Dataset Voltage3; //Gurux.DLMS.Enums.Unit.Voltage

        /// <summary>
        /// Strom L1
        /// Wert x10^-2
        /// A
        /// </summary>
        public Dataset Current1; //Gurux.DLMS.Enums.Unit.Current
        /// <summary>
        /// Strom L2
        /// Wert x10^-2
        /// A
        /// </summary>
        public Dataset Current2; //Gurux.DLMS.Enums.Unit.Current
        /// <summary>
        /// Strom L3
        /// Wert x10^-2
        /// A
        /// </summary>        
        public Dataset Current3; //Gurux.DLMS.Enums.Unit.Current

        /// <summary>
        /// Leistungsfaktor
        /// 1
        /// Wert x10-3
        /// </summary>
        public Dataset PowerFactor;

        /// <summary>
        /// The original XML from the Gurux library.
        /// </summary>
        public String XML { get; set; }

    }
}
