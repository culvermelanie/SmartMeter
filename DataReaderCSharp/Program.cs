using System;

namespace culsoft.smartmeter.datareader
{
    class Program
    {
        static void Main(string[] args)
        {
            //collect the needed input from user
            Console.WriteLine("Provide decryption Key:");
            //string decryptionKey = Console.ReadLine();
            // Use static test data:
            string decryptionKey = "36C66639E48A8CA4D6BC8B282A793BBB";

            Console.WriteLine("Provide input file path:");
            //string filename = Console.ReadLine();
            // Use static test data:
            string filename = "../../../../test_data.txt";

            //main processing class for EVN
            EVN evn = new EVN(decryptionKey);

            //helpers 
            int counter = 1;
            string line;

            //grap the data rows to convert from a file at the moment, but you could also just pass the encrypted HEX directly instead of using a file input
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(line)) continue;

                Console.WriteLine("");
                Console.WriteLine($"Processing {counter}");

                string[] datarow = line.Split('\t'); //we know the input file has a tab as seperator for timestamp and encrypted data
                DataRow dr = evn.ProcessDataRow(datarow[1]); //grab the encrpyted data block and get it converted to an actual object

                //now you can use any of the properties from the converted data row
                Console.WriteLine(dr.TimeStamp);
                Console.WriteLine("Wirkenergie zugeführt: " + dr.ActiveEnergyPositive + " Wh");
                Console.WriteLine("Wirkenergie eingespeist: " + dr.ActiveEnergyNegative + " Wh");
                Console.WriteLine("Momentanleistung zugeführt: " + dr.ActivePowerPositive + " W");
                Console.WriteLine("Momentanleistung eingespeist: " + dr.ActivePowerNegative + " W");
                Console.WriteLine("Spannung L1: {0:0.0} V", dr.Voltage1.Value * Math.Pow(10, dr.Voltage1.Scale));
                Console.WriteLine("Strom L1: {0:0.0} A", dr.Current1.Value * Math.Pow(10, dr.Current1.Scale));
                Console.WriteLine("Spannung L2: {0:0.0} V", dr.Voltage2.Value * Math.Pow(10, dr.Voltage2.Scale));
                Console.WriteLine("Strom L2: {0:0.0} A", dr.Current2.Value * Math.Pow(10, dr.Current2.Scale));
                Console.WriteLine("Spannung L3: {0:0.0} V", dr.Voltage3.Value * Math.Pow(10,dr.Voltage3.Scale));
                Console.WriteLine("Strom L3: {0:0.0} A", dr.Current3.Value * Math.Pow(10, dr.Current3.Scale));
                Console.WriteLine("Leistungsfaktor: {0:0.00}", dr.PowerFactor.Value * Math.Pow(10, dr.PowerFactor.Scale));
                Console.WriteLine("Zählernummer: " + dr.MeterNumber);
                counter++;
            }

            Console.Write("\nPress any key to continue...");
            Console.Read();
        }
    }
}
