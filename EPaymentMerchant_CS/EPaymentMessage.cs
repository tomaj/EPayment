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
  /// Abstraktn� trieda pre v�etky prij�man� aj odosielan� spr�vy.
  /// Definuje spolo�n� vlastnosti a met�dy spr�v - volanie valid�cie a podpisu spr�v, pr�stup k variabiln�mu symbolu a podpisu spr�vy
  /// </summary>
  public abstract class EPaymentMessage
  {
    /// <summary>
    /// Nekryprovan� podpis posielanej alebo prijatej spr�vy
    /// </summary>
    public abstract string SignatureBase
    {
      get;
    }
    
    /// <summary>
    /// Podp�e spr�vu - pou��va SignatureBase
    /// </summary>
    /// <param name="sharedSecret">Tajn� k���, ktor�m sa podpisuj� spr�vy</param>
    public abstract void SignMessage(string sharedSecret);

    /// <summary>
    /// Podpis spr�vy vygenerovan� v met�de SignMessage()
    /// </summary>
    protected string signature = null;
    /// <summary>
    /// Fin�lny podpis spr�vy, je ho mo�n� vola� a� po podp�san� spr�vy met�dou SignMessage()
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
    /// Variabiln� symbol platby
    /// </summary>
    protected string vs = null;
    /// <summary>
    /// Variabiln� symbol platby - jedin� identifik�tor platby pou��van� v�etk�mi platobn�mi syst�mami
    /// </summary>
    public virtual string Vs
    {
      get { return vs; }
    }

    /// <summary>
    /// Zvaliduje odosielan� alebo prij�man� spr�vu
    /// </summary>
    /// <returns>True ak je spr�va val�dna, false ak nieje</returns>
    public abstract bool Validate();
  }
}
