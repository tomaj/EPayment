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
  /// Trieda pre platobn� syst�my pou��vaj�ce pri podpise DES �ifrovanie.
  /// Tento sp�sob v�roby podpisu pou��vaj� napr�klad V�B E-Platba, TatraPay a CardPay
  /// </summary>
  public abstract class EPaymentDesSignedMessage : EPaymentMessage
  {
    /// <summary>
    /// Definovan� sp�sob podpisovania spr�v - pomocou DES �ifrovania met�dami zadefinovan�mi tie� v tejto triede
    /// </summary>
    /// <param name="sharedSecret">Tajn� k��� pou��van� na podpis spr�vy. Je to 8 znakov� string zlo�en� z vidite�n�ch ASCII znakov.</param>
    public override void SignMessage(string sharedSecret)
    {
      signature = GetDesSignature(SignatureBase, sharedSecret);
    }

    /// <summary>
    /// Zaoba�uj�ca met�da pre podpis spr�vy, dovo�uje pod�va� parametre ako stringy, je volan� met�dou SignMessage().
    /// </summary>
    /// <param name="data">Odtla�ok spr�vy</param>
    /// <param name="sharedSecret">Tajn� k��� pou��van� na podpis spr�vy. Je to 8 znakov� string zlo�en� z vidite�n�ch ASCII znakov.</param>
    /// <returns>Podpis spr�vy</returns>
    protected static string GetDesSignature(string data, string sharedSecret)
    {
      return BitConverter.ToString(GetDesSignatureInBytes(Encoding.ASCII.GetBytes(data), Encoding.ASCII.GetBytes(sharedSecret))).Replace("-", "");
    }

    /// <summary>
    /// Met�da vygeneruje podpis spr�vy
    /// </summary>
    /// <param name="data">Odtla�ok spr�vy, ako byte array, kde ka�d� bajt znamen� jeden ASCII znak odtla�ku</param>
    /// <param name="sharedSecret">Tajn� k��� pou�ivan� na podpis spr�vy - byte array d�ky 8</param>
    /// <returns>Podpis spr�vy v byte array-i</returns>
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
        throw new EPaymentException("Pri generovan� podpisu spr�vy do�lo k chybe.", e);
      }
    }
  }
}
