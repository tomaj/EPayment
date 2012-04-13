//  Copyright 2009 MONOGRAM Technologies
//  
//  This file is part of MONOGRAM EPayment libraries
//  
//  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Monogram.EPayment.Merchant;

namespace Monogram.EPayment.Merchant.SLSP.SporoPay
{
  /// <summary>
  /// Trieda pre prichádzajúce aj odchádzajúce SporoPay platby.
  /// Je v nej definovaný spôsob generovania digitálneho podpisu.
  /// </summary>
  public abstract class SporoPayMessage : EPaymentMessage
  {
    /// <summary>
    /// Metóda podpisujúca správu z jej odtlaèku
    /// </summary>
    /// <param name="sharedSecret"></param>
    public override void SignMessage(string sharedSecret)
    {
      signature = get3DesSignature(SignatureBase, sharedSecret);
    }

    /// <summary>
    /// Metóda generujúca podpis z textových hodnôt odtlaèku a k¾úèa
    /// </summary>
    /// <param name="data">Odtlaèok správy</param>
    /// <param name="sharedSecret">K¾úè na generovanie podpisu, 16 bajtov zakódovaných Base64 encodingom.</param>
    /// <returns>Podpis správy ako string, 24 bajtov zaenkódovaných Base64 encodingom.</returns>
    protected static string get3DesSignature(string data, string sharedSecret)
    {
      return Convert.ToBase64String(get3DesSignatureInBytes(Encoding.ASCII.GetBytes(data), Convert.FromBase64String(sharedSecret)));
    }
    
    /// <summary>
    /// Metóda generujúca podpis správy
    /// </summary>
    /// <param name="data">Odtlaèok správy ako byte array</param>
    /// <param name="sharedSecret">K¾úè na generovanie podpisu, byte array o dåžke 16</param>
    /// <returns>Podpis správy ako byte array o dåžke 24 bajtov</returns>
    protected static byte[] get3DesSignatureInBytes(byte[] data, byte[] sharedSecret)
    {
      HashAlgorithm hash = new SHA1Managed();
      byte[] hashedData = hash.ComputeHash(data);

      byte[] decryptedSign = new byte[24];
      for (int i = 0; i < hashedData.Length; i++)
      { decryptedSign[i] = hashedData[i]; }
      for (int i = 20; i < decryptedSign.Length; i++)
      { decryptedSign[i] = (byte)0xFF; }

      TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
      tdes.Key = sharedSecret;
      byte[] iv = new byte[8];
      for (int i = 0; i < iv.Length; i++)
      { iv[i] = (byte)0x00; }
      tdes.IV = iv;
      tdes.Mode = CipherMode.CBC;

      ICryptoTransform transform = tdes.CreateEncryptor();
      byte[] sign = new byte[24];
      transform.TransformBlock(decryptedSign, 0, decryptedSign.Length, sign, 0);

      return sign;
    }
  }
}
