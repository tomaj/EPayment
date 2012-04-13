//  Copyright 2009 MONOGRAM Technologies
// 
//  This file is part of MONOGRAM EPayment libraries
//  
//  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
  #region 3DES (SporoPay)
  public static string Get3DesSignatureInBase64(string data, string sharedSecret_Base64)
  {
    return Convert.ToBase64String(Get3DesSignatureInBytes(Encoding.ASCII.GetBytes(data), Convert.FromBase64String(sharedSecret_Base64)));
  }

  public static byte[] Get3DesSignatureInBytes(byte[] data, byte[] sharedSecret)
  {
    HashAlgorithm hash = new SHA1Managed();
    byte[] hashedData = hash.ComputeHash(data);

    byte[] decryptedSignature = new byte[24];
    for (int i = 0; i < hashedData.Length; i++)
    {
      decryptedSignature[i] = hashedData[i];
    }

    for (int i = 20; i < decryptedSignature.Length; i++)
    {
      decryptedSignature[i] = (byte)0xFF;
    }

    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
    tdes.Key = sharedSecret;
    byte[] iv = new byte[8];
    for (int i = 0; i < iv.Length; i++)
    {
      iv[i] = (byte)0x00;
    }

    tdes.IV = iv;
    tdes.Mode = CipherMode.CBC;

    ICryptoTransform transform = tdes.CreateEncryptor();
    byte[] signature = new byte[24];
    transform.TransformBlock(decryptedSignature, 0, decryptedSignature.Length, signature, 0);

    return signature;
  }
  #endregion

  #region DES
  public static string GetDesSignatureInHexdec(string data, string sharedSecret)
  {
    return BitConverter.ToString(GetDesSignatureInBytes(Encoding.ASCII.GetBytes(data), Encoding.ASCII.GetBytes(sharedSecret))).Replace("-", "");
  }

  public static byte[] GetDesSignatureInBytes(byte[] data, byte[] sharedSecret)
  {
    HashAlgorithm hash = new SHA1Managed();
    byte[] hashedData = hash.ComputeHash(data);

    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
    des.Key = sharedSecret;
    des.Mode = CipherMode.ECB;

    ICryptoTransform transform = des.CreateEncryptor();
    byte[] signature = new byte[8];
    transform.TransformBlock(hashedData, 0, signature.Length, signature, 0);

    return signature;
  }
  #endregion

  #region HMAC-SHA256 (VUB)
  public static string GetHmacSignatureInHexdec(string data, string sharedSecret)
  {
    byte[] rawHash = GetHmacSignatureInBytes(Encoding.ASCII.GetBytes(data), Encoding.ASCII.GetBytes(sharedSecret));
    return BitConverter.ToString(rawHash).Replace("-", "");
  }

  public static byte[] GetHmacSignatureInBytes(byte[] data, byte[] sharedSecret)
  {
    HMACSHA256 hash = new HMACSHA256();
    hash.Key = sharedSecret;
    byte[] hashedValue = hash.ComputeHash(data);
    return hashedValue;
  }
  #endregion
}
