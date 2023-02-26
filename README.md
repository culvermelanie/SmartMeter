# Introduction
This project is an implementation for reading data from Austrian smart meters, currently focusing on NÖ Netze/EVN and WienerNetze.

The data provided by the smart meter is encrypted through "DLMS Security Suite 0, HLS5" according to NÖ Netze. In .net language this means you can use the [AesGcm](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aesgcm?view=net-5.0) library for encrypting the data.

# Technology Stack
The base library is developed in C# based on .NET Core 3.1.
There are to sample console applications, on in C# and one in VB.net also both in .NET Core 3.1.

# Pre-Requisites / Dependencies
Two nuget packages are used for the processing of the decrypted data.
- [Gurux.Common](https://www.nuget.org/packages/Gurux.Common)
- [Gurus.DLMS](https://www.nuget.org/packages/Gurux.DLMS)

# General Notes
This code hat not jet been tested with a direct integration to a smart meter, but reads the data provided by the smart meter from a provided file.

Currently there is only an implementation for the NÖ Netze / EVN smart meter. An implementation for the WienerNetze is pending, due to an open ticket with them.

To be able to decrypt the data, AesGcm encrypt has to be used and not decrypt. (don't ask why)

The meter number provided in the message package by EVN is currently not valid. The HEX returned is not within the ACSII range.

# How to use
The solution has three 3 projects.

**SmartMeter**
This project contains the actual logic for processing the encrypted data read from the smart meter. The main class to use is the one for the provider you are using - eg EVN. 
The whole project is basically a wrapper of the Gurux implementation. For easier processing, a DataRow object is retured with all the values returned by the Smart Meter. Presumably different provider will return more or less values, these might need to be extended over time.

**DataReaderCSharp** and **DataReaderVB**
These are sample console applications on how to use the SmartMeter library. 


To test out how things work, start one of the console applications. You will have to provide the decryption you have received from you network provider and the the path to a file containing the smart meter data. 

This is an example format of the file:
```
08:05:00	<HEXValue from the meter>
08:10:00	<HEXValue from the meter>
```
Each line consists of a time stamp, followed by a tabulator and a string of hex values. For NÖ Netze the HEXValue is 564 long.

# Disclaimer
The code is provided as is and not ready to be used in an production environment.
