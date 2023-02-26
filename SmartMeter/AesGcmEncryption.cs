using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Gurux.Common;

/// <summary>
/// Processes the encrypted data and considers any headers and trailing data of the message package which is not encrypted.
/// This could be specific for EVN, but has to be verified with a other provider implementation
/// </summary>
public class AesGcmEncryption
{
    //these are fix values we do not need to decrypt of a message
    const int METER_LENGTH = 22;
    const int SYSTEM_TITLE_LENGTH = 16;
    const int SECURITY_LENGTH = 6;
    const int FRAME_COUNTER_LENGTH = 8;
    const int END_LENGTH = 4;
    private byte[] KEY { get; set; }
    private byte[] TAG { get; set; }

    private int HeaderLength
    {
        get
        {
            return METER_LENGTH + SYSTEM_TITLE_LENGTH + SECURITY_LENGTH + FRAME_COUNTER_LENGTH;
        }
    }

    /// <summary>
    /// Handle the encryption and decrpytion of AesGcm data.
    /// </summary>
    /// <param name="keyHex">HEX decryption/encryption key</param>
    public AesGcmEncryption(string keyHex)
    {
        KEY = HexToByteArray(keyHex);
        TAG = new byte[16];
    }

    /// <summary>
    /// Encrypts the provided message. (For EVN this actually decrypts the message)
    /// </summary>
    /// <param name="HEXMessage">Message to encrypt</param>
    /// <returns></returns>
    public string Encrypt(string HEXMessage)
    {
        string message = HEXMessage.Replace(" ", "");
        byte[] plainText = HexToByteArray(message.Substring(HeaderLength, 512 - HeaderLength - END_LENGTH) + message.Substring(512+18, message.Length - (512+18) - END_LENGTH));
        byte[] ciphertext = new byte[plainText.Length];
        byte[] nonce = HexToByteArray(message.Substring(METER_LENGTH, SYSTEM_TITLE_LENGTH) + message.Substring(METER_LENGTH + SYSTEM_TITLE_LENGTH + SECURITY_LENGTH, FRAME_COUNTER_LENGTH));
        using (AesGcm aesGcm = new AesGcm(KEY))
        {
            aesGcm.Encrypt(
                nonce,
                plainText,
                ciphertext,
                TAG);
        }

        return GXCommon.ToHex(ciphertext);
    }

    /// <summary>
    /// This is not to be used to decrypt messages from EVN and therefore has not been tested!
    /// </summary>
    /// <param name="HEXMessage"></param>
    /// <returns></returns>
    public string Decrypt(string HEXMessage)
    {
        // Notice here -> First get byte from the encoded base64. 
        byte[] cipherText = HexToByteArray(HEXMessage);
        byte[] plainText = new byte[cipherText.Length];
        using (AesGcm aesGcm = new AesGcm(KEY))
        {
            aesGcm.Decrypt(
                null,
                cipherText,
                TAG,
                plainText);
        }

        // Notice here -> then get back the hex from plain text.
        return GXCommon.ToHex(plainText);
    }

    public static byte[] HexToByteArray(string input)
    {
        return GXCommon.HexToBytes(input);
    }

}