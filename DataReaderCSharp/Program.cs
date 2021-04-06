using System;

namespace culsoft.smartmeter.datareader
{
    class Program
    {
        static void Main(string[] args)
        {
            //collect the needed input from user
            Console.WriteLine("Provide decryption Key:");
            string decryptionKey = Console.ReadLine();

            Console.WriteLine("Provide input file path:");
            string filename = Console.ReadLine();

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
                Console.WriteLine("Wirkenergie zugeführt: " + dr.ActiveEnergyPositive);
                Console.WriteLine("Wirkenergie eingespeist: " + dr.ActiveEnergyNegative);
                Console.WriteLine("Momentanleistung zugeführt: " + dr.ActivePowerPositive);
                Console.WriteLine("Momentanleistung eingespeist: " + dr.ActivePowerNegative);
                Console.WriteLine("Spannung L1: " + dr.Voltage1);
                Console.WriteLine("Strom L1: " + dr.Current1);
                Console.WriteLine("Spannung L2: " + dr.Voltage2);
                Console.WriteLine("Strom L2: " + dr.Current2);
                Console.WriteLine("Spannung L3: " + dr.Voltage3);
                Console.WriteLine("Strom L3: " + dr.Current3);
                counter++;
            }

            Console.Read();
        }
    }
}
