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

namespace Monogram.EPayment.Merchant
{
  /// <summary>
  /// Trieda pre platobné systémy pouívajúce pri podpise DES šifrovanie.
  /// Tento spôsob vıroby podpisu pouívajú napríklad TatraPay, CardPay a donedávna aj EPlatba
  /// </summary>
  public abstract class EPaymentHmacSignedMessage : EPaymentMessage
  {
    /// <summary>
    /// Definovanı spôsob podpisovania správ - pomocou DES šifrovania metódami zadefinovanımi tie v tejto triede
    /// </summary>
    /// <param name="sharedSecret">Tajnı k¾úè pouívanı na podpis správy. Je to 8 znakovı string zloenı z vidite¾nıch ASCII znakov.</param>
    public override void SignMessage(string sharedSecret)
    {
      signature = GetHmacSignature(SignatureBase, sharedSecret);
    }

    /// <summary>
    /// Metóda ohodnocuje cifru hexadecimálneho zápisu
    /// </summary>
    /// <param name="ch">cifra</param>
    /// <returns>hodnota cifry</returns>
    protected static byte getHexDigitValue(char ch)
    {
      switch (ch)
      {
        case '0': return 0;
        case '1': return 1;
        case '2': return 2;
        case '3': return 3;
        case '4': return 4;
        case '5': return 5;
        case '6': return 6;
        case '7': return 7;
        case '8': return 8;
        case '9': return 9;
        case 'A': case 'a': return 10;
        case 'B': case 'b': return 11;
        case 'C': case 'c': return 12;
        case 'D': case 'd': return 13;
        case 'E': case 'e': return 14;
        case 'F': case 'f': return 15;
        default:
          throw new EPaymentException("Invalid HEX digit.");
      }
    }

    /// <summary>
    /// Zaoba¾ujúca metóda pre podpis správy, dovo¾uje podáva parametre ako stringy, je volaná metódou SignMessage().
    /// </summary>
    /// <param name="data">Odtlaèok správy</param>
    /// <param name="sharedSecret">Tajnı k¾úè pouívanı na podpis správy. Je to 64 znakovı string obsahujúci k¾úè alebo jeho 128 znakovı hexadecimálny zápis.</param>
    /// <returns>Podpis správy</returns>
    protected static string GetHmacSignature(string data, string sharedSecret)
    {
      byte[] rawSharedSecret = null;
      if (sharedSecret.Length == 128)
      {
        rawSharedSecret = new byte[64];
        for (int i = 0; i < 64; i++)
        {
          rawSharedSecret[i] = (byte)(getHexDigitValue(sharedSecret[2*i])*16 + getHexDigitValue(sharedSecret[2*i+1]));
        }
      }
      else if (sharedSecret.Length == 64)
      {
        rawSharedSecret = Encoding.ASCII.GetBytes(sharedSecret);
      }
      else
      {
        throw new EPaymentException("Unsupported input format of shared secret.");
      }

      return BitConverter.ToString(GetHmacSignatureInBytes(Encoding.ASCII.GetBytes(data), rawSharedSecret)).Replace("-", "");
    }

    /// <summary>
    /// Metóda vygeneruje podpis správy
    /// </summary>
    /// <param name="data">Odtlaèok správy ako byte array, kde kadı bajt znamená jeden ASCII znak odtlaèku</param>
    /// <param name="sharedSecret">Tajnı k¾úè pouivanı na podpis správy - byte array dåky 8</param>
    /// <returns>Podpis správy v byte array-i</returns>
    protected static byte[] GetHmacSignatureInBytes(byte[] data, byte[] sharedSecret)
    {
      try
      {
        HMACSHA256 hash = new HMACSHA256();
        hash.Key = sharedSecret;
        byte[] hashedData = hash.ComputeHash(data);
        return hashedData;
      }
      catch (Exception e)
      {
        throw new EPaymentException("Pri generovaní podpisu správy došlo k chybe.", e);
      }
    }
  }
}
