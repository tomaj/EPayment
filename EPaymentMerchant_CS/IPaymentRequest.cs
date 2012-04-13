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
  /// Spolo�n� interface pre payment requesty (zalo�en� na HTTP GET ale aj in�ch sp�soboch odosielania)
  /// </summary>
  public interface IPaymentRequest
  {
    /// <summary>
    /// Variabiln� symbol platby - jedin� identifik�tor platby spolo�n� pre v�etky platobn� syst�my.
    /// </summary>
    string Vs
    {
      get;
      set;
    }

    /// <summary>
    /// Met�da na zvalidovanie form�tu payment requestu
    /// </summary>
    /// <returns>True ak je form�t payment requestu spr�vny, false ak nieje</returns>
    bool Validate();

    /// <summary>
    /// Met�da podpisuj�ca payment request
    /// </summary>
    /// <param name="sharedSecret">Tajn� k���, ktor�m sa spr�va podp�e</param>
    void SignMessage(string sharedSecret);
  }
}
