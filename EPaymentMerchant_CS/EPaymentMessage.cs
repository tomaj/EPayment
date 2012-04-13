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
  /// Abstraktná trieda pre všetky prijímané aj odosielané správy.
  /// Definuje spoloèné vlastnosti a metódy správ - volanie validácie a podpisu správ, prístup k variabilnému symbolu a podpisu správy
  /// </summary>
  public abstract class EPaymentMessage
  {
    /// <summary>
    /// Nekryprovanı podpis posielanej alebo prijatej správy
    /// </summary>
    public abstract string SignatureBase
    {
      get;
    }
    
    /// <summary>
    /// Podpíše správu - pouíva SignatureBase
    /// </summary>
    /// <param name="sharedSecret">Tajnı k¾úè, ktorım sa podpisujú správy</param>
    public abstract void SignMessage(string sharedSecret);

    /// <summary>
    /// Podpis správy vygenerovanı v metóde SignMessage()
    /// </summary>
    protected string signature = null;
    /// <summary>
    /// Finálny podpis správy, je ho moné vola a po podpísaní správy metódou SignMessage()
    /// </summary>
    public string Signature
    {
      get
      {
        if (signature == null)
        {
          throw new ApplicationException("Message was not signed yet.");
        }
        return signature;
      }
    }

    /// <summary>
    /// Variabilnı symbol platby
    /// </summary>
    protected string vs = null;
    /// <summary>
    /// Variabilnı symbol platby - jedinı identifikátor platby pouívanı všetkımi platobnımi systémami
    /// </summary>
    public virtual string Vs
    {
      get { return vs; }
    }

    /// <summary>
    /// Zvaliduje odosielanú alebo prijímanú správu
    /// </summary>
    /// <returns>True ak je správa valídna, false ak nieje</returns>
    public abstract bool Validate();
  }
}
