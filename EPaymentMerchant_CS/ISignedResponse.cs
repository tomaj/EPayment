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

namespace Monogram.EPayment.Merchant
{
  /// <summary>
  /// Spolo�n� rozhranie payment response-ov
  /// </summary>
  public interface ISignedResponse
  {
    /// <summary>
    /// Overovanie podpisu spr�vy (integrita & autentickos�)
    /// </summary>
    /// <param name="sharedSecret">Tajn� k��� pou��van� na podpisovanie spr�v</param>
    /// <returns>True ak je prijat� podpis spr�vny, false ak nieje</returns>
    bool VerifySignature(string sharedSecret);

    /// <summary>
    /// Zis�uje stav verifik�cie spr�vy. Ak �spe�ne skon�� VerifySignature, potom je hodnota IsVerified true, inak je false.
    /// </summary>
    bool IsVerified
    {
      get;
    }
    
    /// <summary>
    /// Prijat� podpis prijatej spr�vy. Porovn�va sa s podpisom spr�vy vygenerovan�m touto kni�nicou.
    /// </summary>
    string ReceivedSignature
    {
      get;
    }

    /// <summary>
    /// Met�da na zistenie v�sledku platby. Je ju mo�n� vola� iba po �spe�nom verifikovan� spr�vy, inak vyhod� EBankingException.
    /// </summary>
    /// <returns>V�sledok platby</returns>
    PaymentResponse GetPaymentResponse();
  }
}
