Imports System

Namespace culsoft.smartmeter.datareader
    Class Program
        Public Shared Sub Main(ByVal args As String())

            'collect the needed input from user
            Console.WriteLine("Provide decryption Key:")
            'Dim decryptionKey As String = Console.ReadLine()
            Dim decryptionKey = "36C66639E48A8CA4D6BC8B282A793BBB"
            Console.WriteLine("Provide input file path:")
            'Dim filename As String = Console.ReadLine()
            Dim filename = "../../../../test_data.txt"

            'main processing class for EVN
            Dim evn As EVN = New EVN(decryptionKey)

            'helpers
            Dim counter As Integer = 1
            Dim line As String
            'grap the data rows to convert from a file at the moment, but you could also just pass the encrypted HEX directly instead of using a file input
            Dim file As System.IO.StreamReader = New System.IO.StreamReader(filename)

            While (file.Peek() <> -1)
                line = file.ReadLine()
                If (String.IsNullOrEmpty(line)) Then Continue While
                Console.WriteLine("")
                Console.WriteLine($"Processing row {counter}")

                Dim datarow As String() = line.Split(vbTab) 'we know the input file has a tab as seperator for timestamp and encrypted data

                Dim dr As DataRow = evn.ProcessDataRow(datarow(1)) 'grab the encrpyted data block and get it converted to an actual object

                'now you can use any of the properties from the converted data row
                Console.WriteLine(dr.TimeStamp)
                Console.WriteLine("Wirkenergie zugef�hrt: " & dr.ActiveEnergyPositive)
                Console.WriteLine("Wirkenergie eingespeist: " & dr.ActiveEnergyNegative)
                Console.WriteLine("Momentanleistung zugef�hrt: " & dr.ActivePowerPositive)
                Console.WriteLine("Momentanleistung eingespeist: " & dr.ActivePowerNegative)
                Console.WriteLine("Spannung L1: " & dr.Voltage1.Value / 10)
                Console.WriteLine("Strom L1: " & dr.Current1.Value / 100)

                counter += 1
            End While

            Console.WriteLine("Press any key to continue...")
            Console.Read()
        End Sub

    End Class
End Namespace
