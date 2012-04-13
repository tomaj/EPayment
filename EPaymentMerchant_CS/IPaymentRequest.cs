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
  /// Spoloèný interface pre payment requesty (založené na HTTP GET ale aj iných spôsoboch odosielania)
  /// </summary>
  public interface IPaymentRequest
  {
    /// <summary>
    /// Variabilný symbol platby - jediný identifikátor platby spoloèný pre všetky platobné systémy.
    /// </summary>
    string Vs
    {
      get;
      set;
    }

    /// <summary>
    /// Metóda na zvalidovanie formátu payment requestu
    /// </summary>
    /// <returns>True ak je formát payment requestu správny, false ak nieje</returns>
    bool Validate();

    /// <summary>
    /// Metóda podpisujúca payment request
    /// </summary>
    /// <param name="sharedSecret">Tajný k¾úè, ktorým sa správa podpíše</param>
    void SignMessage(string sharedSecret);
  }
}
