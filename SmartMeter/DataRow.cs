using System;

namespace culsoft.smartmeter
{
    /// <summary>
    /// Data object representing one network provider message.
    /// Currently based on the EVN message package.
    /// </summary>
    public struct Dataset<T1, T2>
    {
        public T1 Value { get; set; }
        public T2 Scale { get; set; }
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
        public Dataset<UInt16, sbyte> Voltage1; //Gurux.DLMS.Enums.Unit.Voltage
        /// <summary>
        /// Spannung L2
        /// Wert x10^-1
        /// V
        /// </summary>
        public Dataset<UInt16, sbyte> Voltage2; //Gurux.DLMS.Enums.Unit.Voltage
        /// <summary>
        /// Spannung L3
        /// Wert x10^-1
        /// V
        /// </summary>
        public Dataset<UInt16, sbyte> Voltage3; //Gurux.DLMS.Enums.Unit.Voltage

        /// <summary>
        /// Strom L1
        /// Wert x10^-2
        /// A
        /// </summary>
        public Dataset<UInt16, sbyte> Current1; //Gurux.DLMS.Enums.Unit.Current
        /// <summary>
        /// Strom L2
        /// Wert x10^-2
        /// A
        /// </summary>
        public Dataset<UInt16, sbyte> Current2; //Gurux.DLMS.Enums.Unit.Current
        /// <summary>
        /// Strom L3
        /// Wert x10^-2
        /// A
        /// </summary>        
        public Dataset<UInt16, sbyte> Current3; //Gurux.DLMS.Enums.Unit.Current

        /// <summary>
        /// Leistungsfaktor
        /// 1
        /// Wert x10-3
        /// </summary>
        public Dataset<UInt16, sbyte> PowerFactor;

        /// <summary>
        /// The original XML from the Gurux library.
        /// </summary>
        public String XML { get; set; }

    }
}
