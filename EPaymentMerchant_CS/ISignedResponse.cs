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
  /// Spoloèné rozhranie payment response-ov
  /// </summary>
  public interface ISignedResponse
  {
    /// <summary>
    /// Overovanie podpisu správy (integrita & autentickos)
    /// </summary>
    /// <param name="sharedSecret">Tajnı k¾úè pouívanı na podpisovanie správ</param>
    /// <returns>True ak je prijatı podpis správny, false ak nieje</returns>
    bool VerifySignature(string sharedSecret);

    /// <summary>
    /// Zisuje stav verifikácie správy. Ak úspešne skonèí VerifySignature, potom je hodnota IsVerified true, inak je false.
    /// </summary>
    bool IsVerified
    {
      get;
    }
    
    /// <summary>
    /// Prijatı podpis prijatej správy. Porovnáva sa s podpisom správy vygenerovanım touto kninicou.
    /// </summary>
    string ReceivedSignature
    {
      get;
    }

    /// <summary>
    /// Metóda na zistenie vısledku platby. Je ju moné vola iba po úspešnom verifikovaní správy, inak vyhodí EBankingException.
    /// </summary>
    /// <returns>Vısledok platby</returns>
    PaymentResponse GetPaymentResponse();
  }
}
