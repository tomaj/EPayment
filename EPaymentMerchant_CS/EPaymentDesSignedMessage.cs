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
  /// Tento spôsob vıroby podpisu pouívajú napríklad VÚB E-Platba, TatraPay a CardPay
  /// </summary>
  public abstract class EPaymentDesSignedMessage : EPaymentMessage
  {
    /// <summary>
    /// Definovanı spôsob podpisovania správ - pomocou DES šifrovania metódami zadefinovanımi tie v tejto triede
    /// </summary>
    /// <param name="sharedSecret">Tajnı k¾úè pouívanı na podpis správy. Je to 8 znakovı string zloenı z vidite¾nıch ASCII znakov.</param>
    public override void SignMessage(string sharedSecret)
    {
      signature = GetDesSignature(SignatureBase, sharedSecret);
    }

    /// <summary>
    /// Zaoba¾ujúca metóda pre podpis správy, dovo¾uje podáva parametre ako stringy, je volaná metódou SignMessage().
    /// </summary>
    /// <param name="data">Odtlaèok správy</param>
    /// <param name="sharedSecret">Tajnı k¾úè pouívanı na podpis správy. Je to 8 znakovı string zloenı z vidite¾nıch ASCII znakov.</param>
    /// <returns>Podpis správy</returns>
    protected static string GetDesSignature(string data, string sharedSecret)
    {
      return BitConverter.ToString(GetDesSignatureInBytes(Encoding.ASCII.GetBytes(data), Encoding.ASCII.GetBytes(sharedSecret))).Replace("-", "");
    }

    /// <summary>
    /// Metóda vygeneruje podpis správy
    /// </summary>
    /// <param name="data">Odtlaèok správy, ako byte array, kde kadı bajt znamená jeden ASCII znak odtlaèku</param>
    /// <param name="sharedSecret">Tajnı k¾úè pouivanı na podpis správy - byte array dåky 8</param>
    /// <returns>Podpis správy v byte array-i</returns>
    protected static byte[] GetDesSignatureInBytes(byte[] data, byte[] sharedSecret)
    {
      try
      {
        HashAlgorithm hash = new SHA1Managed();
        byte[] hashedData = hash.ComputeHash(data);

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        des.Key = sharedSecret;
        des.Mode = CipherMode.ECB;

        ICryptoTransform transform = des.CreateEncryptor();
        byte[] sign = new byte[8];
        transform.TransformBlock(hashedData, 0, sign.Length, sign, 0);

        return sign;
      }
      catch (Exception e)
      {
        throw new EPaymentException("Pri generovaní podpisu správy došlo k chybe.", e);
      }
    }
  }
}
