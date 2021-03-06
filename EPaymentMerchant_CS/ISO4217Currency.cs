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
namespace Monogram.EPayment.Merchant
{
  /// <summary>
  /// Enum obsahujúci zoznam mien podľa ISO 4217
  /// </summary>
  public enum ISO4217Currency
  {
    AED = 784,
    AFN = 971,
    ALL = 8,
    AMD = 51,
    ANG = 532,
    AOA = 973,
    ARS = 32,
    AUD = 36,
    AWG = 533,
    AZN = 944,
    BAM = 977,
    BBD = 52,
    BDT = 50,
    BGN = 975,
    BHD = 48,
    BIF = 108,
    BMD = 60,
    BND = 96,
    BOB = 68,
    BOV = 984,
    BRL = 986,
    BSD = 44,
    BTN = 64,
    BWP = 72,
    BYR = 974,
    BZD = 84,
    CAD = 124,
    CDF = 976,
    CHE = 947,
    CHF = 756,
    CHW = 948,
    CLF = 990,
    CLP = 152,
    CNY = 156,
    COP = 170,
    COU = 970,
    CRC = 188,
    CUP = 192,
    CVE = 132,
    CZK = 203,
    DJF = 262,
    DKK = 208,
    DOP = 214,
    DZD = 12,
    EEK = 233,
    EGP = 818,
    ERN = 232,
    ETB = 230,
    EUR = 978,
    FJD = 242,
    FKP = 238,
    GBP = 826,
    GEL = 981,
    GHS = 936,
    GIP = 292,
    GMD = 270,
    GNF = 324,
    GTQ = 320,
    GYD = 328,
    HKD = 344,
    HNL = 340,
    HRK = 191,
    HTG = 332,
    HUF = 348,
    IDR = 360,
    ILS = 376,
    INR = 356,
    IQD = 368,
    IRR = 364,
    ISK = 352,
    JMD = 388,
    JOD = 400,
    JPY = 392,
    KES = 404,
    KGS = 417,
    KHR = 116,
    KMF = 174,
    KPW = 408,
    KRW = 410,
    KWD = 414,
    KYD = 136,
    KZT = 398,
    LAK = 418,
    LBP = 422,
    LKR = 144,
    LRD = 430,
    LSL = 426,
    LTL = 440,
    LVL = 428,
    LYD = 434,
    MAD = 504,
    MDL = 498,
    MGA = 969,
    MKD = 807,
    MMK = 104,
    MNT = 496,
    MOP = 446,
    MRO = 478,
    MUR = 480,
    MVR = 462,
    MWK = 454,
    MXN = 484,
    MXV = 979,
    MYR = 458,
    MZN = 943,
    NAD = 516,
    NGN = 566,
    NIO = 558,
    NOK = 578,
    NPR = 524,
    NZD = 554,
    OMR = 512,
    PAB = 590,
    PEN = 604,
    PGK = 598,
    PHP = 608,
    PKR = 586,
    PLN = 985,
    PYG = 600,
    QAR = 634,
    RON = 946,
    RSD = 941,
    RUB = 643,
    RWF = 646,
    SAR = 682,
    SBD = 90,
    SCR = 690,
    SDG = 938,
    SEK = 752,
    SGD = 702,
    SHP = 654,
    SKK = 703,
    SLL = 694,
    SOS = 706,
    SRD = 968,
    STD = 678,
    SYP = 760,
    SZL = 748,
    THB = 764,
    TJS = 972,
    TMM = 795,
    TND = 788,
    TOP = 776,
    TRY = 949,
    TTD = 780,
    TWD = 901,
    TZS = 834,
    UAH = 980,
    UGX = 800,
    USD = 840,
    USN = 997,
    USS = 998,
    UYU = 858,
    UZS = 860,
    VEF = 937,
    VND = 704,
    VUV = 548,
    WST = 882,
    XAF = 950,
    XAG = 961,
    XAU = 959,
    XBA = 955,
    XBB = 956,
    XBC = 957,
    XBD = 958,
    XCD = 951,
    XDR = 960,
//    XFU = Nil,
    XOF = 952,
    XPD = 964,
    XPF = 953,
    XPT = 962,
    XTS = 963,
//    XXX = 999,
    YER = 886,
    ZAR = 710,
    ZMK = 894,
    ZWD = 716
  }
  
  /// <summary>
  /// Trieda popisujúca menu.
  /// </summary>
  public class ISO4217CurrencyDetail
  {
    /// <summary>
    /// Identifikátor meny
    /// </summary>
    private ISO4217Currency currency;
    /// <summary>
    /// Kód meny
    /// </summary>
    private string strCode;
    /// <summary>
    /// Exponent meny
    /// </summary>
    private double? exponent;
    /// <summary>
    /// Číselný kód meny
    /// </summary>
    private int numCode;
    /// <summary>
    /// Názov meny
    /// </summary>
    private string name;

    /// <summary>
    /// Identifikátor meny
    /// </summary>
    public ISO4217Currency Currency { get { return currency; } }
    /// <summary>
    /// Textový kód meny
    /// </summary>
    public string StrCode { get { return strCode; } }
    /// <summary>
    /// Exponent meny. 10^(exponent) minor unitov (halierov, centov) je jeden major unit (koruna, euro, dolár)
    /// </summary>
    public double? Exponent { get { return exponent; } }
    /// <summary>
    /// Číselný kód meny
    /// </summary>
    public int NumCode { get { return numCode; } }
    /// <summary>
    /// Názov meny
    /// </summary>
    public string Name { get { return name; } }

    /// <summary>
    /// Konštruktor objektu detailov meny, v konštruktore sa nastavia všetky detaily.
    /// </summary>
    /// <param name="currency1">Identifikátor meny</param>
    public ISO4217CurrencyDetail(ISO4217Currency currency1)
    {
      currency = currency1;
      switch (currency)
      {
        case ISO4217Currency.AED: strCode = "AED"; exponent = 2; numCode = 784; name = "United Arab Emirates dirham"; break;
        case ISO4217Currency.AFN: strCode = "AFN"; exponent = 2; numCode = 971; name = "Afghani"; break;
        case ISO4217Currency.ALL: strCode = "ALL"; exponent = 2; numCode = 8;   name = "Lek"; break;
        case ISO4217Currency.AMD: strCode = "AMD"; exponent = 2; numCode = 51;  name = "Armenian dram"; break;
        case ISO4217Currency.ANG: strCode = "ANG"; exponent = 2; numCode = 532; name = "Netherlands Antillean guilder"; break;
        case ISO4217Currency.AOA: strCode = "AOA"; exponent = 2; numCode = 973; name = "Kwanza"; break;
        case ISO4217Currency.ARS: strCode = "ARS"; exponent = 2; numCode = 32;  name = "Argentine peso"; break;
        case ISO4217Currency.AUD: strCode = "AUD"; exponent = 2; numCode = 36;  name = "Australian dollar"; break;
        case ISO4217Currency.AWG: strCode = "AWG"; exponent = 2; numCode = 533; name = "Aruban guilder"; break;
        case ISO4217Currency.AZN: strCode = "AZN"; exponent = 2; numCode = 944; name = "Azerbaijanian manat"; break;
        case ISO4217Currency.BAM: strCode = "BAM"; exponent = 2; numCode = 977; name = "Convertible marks"; break;
        case ISO4217Currency.BBD: strCode = "BBD"; exponent = 2; numCode = 52;  name = "Barbados dollar"; break;
        case ISO4217Currency.BDT: strCode = "BDT"; exponent = 2; numCode = 50;  name = "Bangladeshi taka"; break;
        case ISO4217Currency.BGN: strCode = "BGN"; exponent = 2; numCode = 975; name = "Bulgarian lev"; break;
        case ISO4217Currency.BHD: strCode = "BHD"; exponent = 3; numCode = 48;  name = "Bahraini dinar"; break;
        case ISO4217Currency.BIF: strCode = "BIF"; exponent = 0; numCode = 108; name = "Burundian franc"; break;
        case ISO4217Currency.BMD: strCode = "BMD"; exponent = 2; numCode = 60;  name = "Bermudian dollar (customarily known as Bermuda dollar)"; break;
        case ISO4217Currency.BND: strCode = "BND"; exponent = 2; numCode = 96;  name = "Brunei dollar"; break;
        case ISO4217Currency.BOB: strCode = "BOB"; exponent = 2; numCode = 68;  name = "Boliviano"; break;
        case ISO4217Currency.BOV: strCode = "BOV"; exponent = 2; numCode = 984; name = "Bolivian Mvdol (funds code)"; break;
        case ISO4217Currency.BRL: strCode = "BRL"; exponent = 2; numCode = 986; name = "Brazilian real"; break;
        case ISO4217Currency.BSD: strCode = "BSD"; exponent = 2; numCode = 44;  name = "Bahamian dollar"; break;
        case ISO4217Currency.BTN: strCode = "BTN"; exponent = 2; numCode = 64;  name = "Ngultrum"; break;
        case ISO4217Currency.BWP: strCode = "BWP"; exponent = 2; numCode = 72;  name = "Pula"; break;
        case ISO4217Currency.BYR: strCode = "BYR"; exponent = 0; numCode = 974; name = "Belarussian ruble"; break;
        case ISO4217Currency.BZD: strCode = "BZD"; exponent = 2; numCode = 84;  name = "Belize dollar"; break;
        case ISO4217Currency.CAD: strCode = "CAD"; exponent = 2; numCode = 124; name = "Canadian dollar"; break;
        case ISO4217Currency.CDF: strCode = "CDF"; exponent = 2; numCode = 976; name = "Franc Congolais"; break;
        case ISO4217Currency.CHE: strCode = "CHE"; exponent = 2; numCode = 947; name = "WIR euro (complementary currency)"; break;
        case ISO4217Currency.CHF: strCode = "CHF"; exponent = 2; numCode = 756; name = "Swiss franc"; break;
        case ISO4217Currency.CHW: strCode = "CHW"; exponent = 2; numCode = 948; name = "WIR franc (complementary currency)"; break;
        case ISO4217Currency.CLF: strCode = "CLF"; exponent = 0; numCode = 990; name = "Unidad de Fomento (funds code)"; break;
        case ISO4217Currency.CLP: strCode = "CLP"; exponent = 0; numCode = 152; name = "Chilean peso"; break;
        case ISO4217Currency.CNY: strCode = "CNY"; exponent = 2; numCode = 156; name = "Renminbi"; break;
        case ISO4217Currency.COP: strCode = "COP"; exponent = 2; numCode = 170; name = "Colombian peso"; break;
        case ISO4217Currency.COU: strCode = "COU"; exponent = 2; numCode = 970; name = "Unidad de Valor Real"; break;
        case ISO4217Currency.CRC: strCode = "CRC"; exponent = 2; numCode = 188; name = "Costa Rican colon"; break;
        case ISO4217Currency.CUP: strCode = "CUP"; exponent = 2; numCode = 192; name = "Cuban peso"; break;
        case ISO4217Currency.CVE: strCode = "CVE"; exponent = 2; numCode = 132; name = "Cape Verde escudo"; break;
        case ISO4217Currency.CZK: strCode = "CZK"; exponent = 2; numCode = 203; name = "Czech koruna"; break;
        case ISO4217Currency.DJF: strCode = "DJF"; exponent = 0; numCode = 262; name = "Djibouti franc"; break;
        case ISO4217Currency.DKK: strCode = "DKK"; exponent = 2; numCode = 208; name = "Danish krone"; break;
        case ISO4217Currency.DOP: strCode = "DOP"; exponent = 2; numCode = 214; name = "Dominican peso"; break;
        case ISO4217Currency.DZD: strCode = "DZD"; exponent = 2; numCode = 12;  name = "Algerian dinar"; break;
        case ISO4217Currency.EEK: strCode = "EEK"; exponent = 2; numCode = 233; name = "Kroon"; break;
        case ISO4217Currency.EGP: strCode = "EGP"; exponent = 2; numCode = 818; name = "Egyptian pound"; break;
        case ISO4217Currency.ERN: strCode = "ERN"; exponent = 2; numCode = 232; name = "Nakfa"; break;
        case ISO4217Currency.ETB: strCode = "ETB"; exponent = 2; numCode = 230; name = "Ethiopian birr"; break;
        case ISO4217Currency.EUR: strCode = "EUR"; exponent = 2; numCode = 978; name = "Euro"; break;
        case ISO4217Currency.FJD: strCode = "FJD"; exponent = 2; numCode = 242; name = "Fiji dollar"; break;
        case ISO4217Currency.FKP: strCode = "FKP"; exponent = 2; numCode = 238; name = "Falkland Islands pound"; break;
        case ISO4217Currency.GBP: strCode = "GBP"; exponent = 2; numCode = 826; name = "Pound sterling"; break;
        case ISO4217Currency.GEL: strCode = "GEL"; exponent = 2; numCode = 981; name = "Lari"; break;
        case ISO4217Currency.GHS: strCode = "GHS"; exponent = 2; numCode = 936; name = "Cedi"; break;
        case ISO4217Currency.GIP: strCode = "GIP"; exponent = 2; numCode = 292; name = "Gibraltar pound"; break;
        case ISO4217Currency.GMD: strCode = "GMD"; exponent = 2; numCode = 270; name = "Dalasi"; break;
        case ISO4217Currency.GNF: strCode = "GNF"; exponent = 0; numCode = 324; name = "Guinea franc"; break;
        case ISO4217Currency.GTQ: strCode = "GTQ"; exponent = 2; numCode = 320; name = "Quetzal"; break;
        case ISO4217Currency.GYD: strCode = "GYD"; exponent = 2; numCode = 328; name = "Guyana dollar"; break;
        case ISO4217Currency.HKD: strCode = "HKD"; exponent = 2; numCode = 344; name = "Hong Kong dollar"; break;
        case ISO4217Currency.HNL: strCode = "HNL"; exponent = 2; numCode = 340; name = "Lempira"; break;
        case ISO4217Currency.HRK: strCode = "HRK"; exponent = 2; numCode = 191; name = "Croatian kuna"; break;
        case ISO4217Currency.HTG: strCode = "HTG"; exponent = 2; numCode = 332; name = "Haiti gourde"; break;
        case ISO4217Currency.HUF: strCode = "HUF"; exponent = 2; numCode = 348; name = "Forint"; break;
        case ISO4217Currency.IDR: strCode = "IDR"; exponent = 2; numCode = 360; name = "Rupiah"; break;
        case ISO4217Currency.ILS: strCode = "ILS"; exponent = 2; numCode = 376; name = "New Israeli shekel"; break;
        case ISO4217Currency.INR: strCode = "INR"; exponent = 2; numCode = 356; name = "Indian rupee"; break;
        case ISO4217Currency.IQD: strCode = "IQD"; exponent = 3; numCode = 368; name = "Iraqi dinar"; break;
        case ISO4217Currency.IRR: strCode = "IRR"; exponent = 2; numCode = 364; name = "Iranian rial"; break;
        case ISO4217Currency.ISK: strCode = "ISK"; exponent = 2; numCode = 352; name = "Iceland krona"; break;
        case ISO4217Currency.JMD: strCode = "JMD"; exponent = 2; numCode = 388; name = "Jamaican dollar"; break;
        case ISO4217Currency.JOD: strCode = "JOD"; exponent = 3; numCode = 400; name = "Jordanian dinar"; break;
        case ISO4217Currency.JPY: strCode = "JPY"; exponent = 0; numCode = 392; name = "Japanese yen"; break;
        case ISO4217Currency.KES: strCode = "KES"; exponent = 2; numCode = 404; name = "Kenyan shilling"; break;
        case ISO4217Currency.KGS: strCode = "KGS"; exponent = 2; numCode = 417; name = "Som"; break;
        case ISO4217Currency.KHR: strCode = "KHR"; exponent = 2; numCode = 116; name = "Riel"; break;
        case ISO4217Currency.KMF: strCode = "KMF"; exponent = 0; numCode = 174; name = "Comoro franc"; break;
        case ISO4217Currency.KPW: strCode = "KPW"; exponent = 2; numCode = 408; name = "North Korean won"; break;
        case ISO4217Currency.KRW: strCode = "KRW"; exponent = 0; numCode = 410; name = "South Korean won"; break;
        case ISO4217Currency.KWD: strCode = "KWD"; exponent = 3; numCode = 414; name = "Kuwaiti dinar"; break;
        case ISO4217Currency.KYD: strCode = "KYD"; exponent = 2; numCode = 136; name = "Cayman Islands dollar"; break;
        case ISO4217Currency.KZT: strCode = "KZT"; exponent = 2; numCode = 398; name = "Tenge"; break;
        case ISO4217Currency.LAK: strCode = "LAK"; exponent = 2; numCode = 418; name = "Kip"; break;
        case ISO4217Currency.LBP: strCode = "LBP"; exponent = 2; numCode = 422; name = "Lebanese pound"; break;
        case ISO4217Currency.LKR: strCode = "LKR"; exponent = 2; numCode = 144; name = "Sri Lanka rupee"; break;
        case ISO4217Currency.LRD: strCode = "LRD"; exponent = 2; numCode = 430; name = "Liberian dollar"; break;
        case ISO4217Currency.LSL: strCode = "LSL"; exponent = 2; numCode = 426; name = "Loti"; break;
        case ISO4217Currency.LTL: strCode = "LTL"; exponent = 2; numCode = 440; name = "Lithuanian litas"; break;
        case ISO4217Currency.LVL: strCode = "LVL"; exponent = 2; numCode = 428; name = "Latvian lats"; break;
        case ISO4217Currency.LYD: strCode = "LYD"; exponent = 3; numCode = 434; name = "Libyan dinar"; break;
        case ISO4217Currency.MAD: strCode = "MAD"; exponent = 2; numCode = 504; name = "Moroccan dirham"; break;
        case ISO4217Currency.MDL: strCode = "MDL"; exponent = 2; numCode = 498; name = "Moldovan leu"; break;
        case ISO4217Currency.MGA: strCode = "MGA"; exponent = 0.7; numCode = 969; name = "Malagasy ariary"; break;
        case ISO4217Currency.MKD: strCode = "MKD"; exponent = 2; numCode = 807; name = "Denar"; break;
        case ISO4217Currency.MMK: strCode = "MMK"; exponent = 2; numCode = 104; name = "Kyat"; break;
        case ISO4217Currency.MNT: strCode = "MNT"; exponent = 2; numCode = 496; name = "Tugrik"; break;
        case ISO4217Currency.MOP: strCode = "MOP"; exponent = 2; numCode = 446; name = "Pataca"; break;
        case ISO4217Currency.MRO: strCode = "MRO"; exponent = 0.7; numCode = 478; name = "Ouguiya"; break;
        case ISO4217Currency.MUR: strCode = "MUR"; exponent = 2; numCode = 480; name = "Mauritius rupee"; break;
        case ISO4217Currency.MVR: strCode = "MVR"; exponent = 2; numCode = 462; name = "Rufiyaa"; break;
        case ISO4217Currency.MWK: strCode = "MWK"; exponent = 2; numCode = 454; name = "Kwacha"; break;
        case ISO4217Currency.MXN: strCode = "MXN"; exponent = 2; numCode = 484; name = "Mexican peso"; break;
        case ISO4217Currency.MXV: strCode = "MXV"; exponent = 2; numCode = 979; name = "Mexican Unidad de Inversion (UDI) (funds code)"; break;
        case ISO4217Currency.MYR: strCode = "MYR"; exponent = 2; numCode = 458; name = "Malaysian ringgit"; break;
        case ISO4217Currency.MZN: strCode = "MZN"; exponent = 2; numCode = 943; name = "Metical"; break;
        case ISO4217Currency.NAD: strCode = "NAD"; exponent = 2; numCode = 516; name = "Namibian dollar"; break;
        case ISO4217Currency.NGN: strCode = "NGN"; exponent = 2; numCode = 566; name = "Naira"; break;
        case ISO4217Currency.NIO: strCode = "NIO"; exponent = 2; numCode = 558; name = "Cordoba oro"; break;
        case ISO4217Currency.NOK: strCode = "NOK"; exponent = 2; numCode = 578; name = "Norwegian krone"; break;
        case ISO4217Currency.NPR: strCode = "NPR"; exponent = 2; numCode = 524; name = "Nepalese rupee"; break;
        case ISO4217Currency.NZD: strCode = "NZD"; exponent = 2; numCode = 554; name = "New Zealand dollar"; break;
        case ISO4217Currency.OMR: strCode = "OMR"; exponent = 3; numCode = 512; name = "Rial Omani"; break;
        case ISO4217Currency.PAB: strCode = "PAB"; exponent = 2; numCode = 590; name = "Balboa"; break;
        case ISO4217Currency.PEN: strCode = "PEN"; exponent = 2; numCode = 604; name = "Nuevo sol"; break;
        case ISO4217Currency.PGK: strCode = "PGK"; exponent = 2; numCode = 598; name = "Kina"; break;
        case ISO4217Currency.PHP: strCode = "PHP"; exponent = 2; numCode = 608; name = "Philippine peso"; break;
        case ISO4217Currency.PKR: strCode = "PKR"; exponent = 2; numCode = 586; name = "Pakistan rupee"; break;
        case ISO4217Currency.PLN: strCode = "PLN"; exponent = 2; numCode = 985; name = "Zloty"; break;
        case ISO4217Currency.PYG: strCode = "PYG"; exponent = 0; numCode = 600; name = "Guarani"; break;
        case ISO4217Currency.QAR: strCode = "QAR"; exponent = 2; numCode = 634; name = "Qatari rial"; break;
        case ISO4217Currency.RON: strCode = "RON"; exponent = 2; numCode = 946; name = "Romanian new leu"; break;
        case ISO4217Currency.RSD: strCode = "RSD"; exponent = 2; numCode = 941; name = "Serbian dinar"; break;
        case ISO4217Currency.RUB: strCode = "RUB"; exponent = 2; numCode = 643; name = "Russian ruble"; break;
        case ISO4217Currency.RWF: strCode = "RWF"; exponent = 0; numCode = 646; name = "Rwanda franc"; break;
        case ISO4217Currency.SAR: strCode = "SAR"; exponent = 2; numCode = 682; name = "Saudi riyal"; break;
        case ISO4217Currency.SBD: strCode = "SBD"; exponent = 2; numCode = 90;  name = "Solomon Islands dollar"; break;
        case ISO4217Currency.SCR: strCode = "SCR"; exponent = 2; numCode = 690; name = "Seychelles rupee"; break;
        case ISO4217Currency.SDG: strCode = "SDG"; exponent = 2; numCode = 938; name = "Sudanese pound"; break;
        case ISO4217Currency.SEK: strCode = "SEK"; exponent = 2; numCode = 752; name = "Swedish krona"; break;
        case ISO4217Currency.SGD: strCode = "SGD"; exponent = 2; numCode = 702; name = "Singapore dollar"; break;
        case ISO4217Currency.SHP: strCode = "SHP"; exponent = 2; numCode = 654; name = "Saint Helena pound"; break;
        case ISO4217Currency.SKK: strCode = "SKK"; exponent = 2; numCode = 703; name = "Slovak koruna"; break;
        case ISO4217Currency.SLL: strCode = "SLL"; exponent = 2; numCode = 694; name = "Leone"; break;
        case ISO4217Currency.SOS: strCode = "SOS"; exponent = 2; numCode = 706; name = "Somali shilling"; break;
        case ISO4217Currency.SRD: strCode = "SRD"; exponent = 2; numCode = 968; name = "Surinam dollar"; break;
        case ISO4217Currency.STD: strCode = "STD"; exponent = 2; numCode = 678; name = "Dobra"; break;
        case ISO4217Currency.SYP: strCode = "SYP"; exponent = 2; numCode = 760; name = "Syrian pound"; break;
        case ISO4217Currency.SZL: strCode = "SZL"; exponent = 2; numCode = 748; name = "Lilangeni"; break;
        case ISO4217Currency.THB: strCode = "THB"; exponent = 2; numCode = 764; name = "Baht"; break;
        case ISO4217Currency.TJS: strCode = "TJS"; exponent = 2; numCode = 972; name = "Somoni"; break;
        case ISO4217Currency.TMM: strCode = "TMM"; exponent = 2; numCode = 795; name = "Manat"; break;
        case ISO4217Currency.TND: strCode = "TND"; exponent = 3; numCode = 788; name = "Tunisian dinar"; break;
        case ISO4217Currency.TOP: strCode = "TOP"; exponent = 2; numCode = 776; name = "Pa'anga"; break;
        case ISO4217Currency.TRY: strCode = "TRY"; exponent = 2; numCode = 949; name = "New Turkish lira"; break;
        case ISO4217Currency.TTD: strCode = "TTD"; exponent = 2; numCode = 780; name = "Trinidad and Tobago dollar"; break;
        case ISO4217Currency.TWD: strCode = "TWD"; exponent = 2; numCode = 901; name = "New Taiwan dollar"; break;
        case ISO4217Currency.TZS: strCode = "TZS"; exponent = 2; numCode = 834; name = "Tanzanian shilling"; break;
        case ISO4217Currency.UAH: strCode = "UAH"; exponent = 2; numCode = 980; name = "Hryvnia"; break;
        case ISO4217Currency.UGX: strCode = "UGX"; exponent = 2; numCode = 800; name = "Uganda shilling"; break;
        case ISO4217Currency.USD: strCode = "USD"; exponent = 2; numCode = 840; name = "US dollar"; break;
        case ISO4217Currency.USN: strCode = "USN"; exponent = 2; numCode = 997; name = "United States dollar (next day) (funds code)"; break;
        case ISO4217Currency.USS: strCode = "USS"; exponent = 2; numCode = 998; name = "United States dollar (same day) (funds code) (one source claims it is no longer used, but it is still on the ISO 4217-MA list)"; break;
        case ISO4217Currency.UYU: strCode = "UYU"; exponent = 2; numCode = 858; name = "Peso Uruguayo"; break;
        case ISO4217Currency.UZS: strCode = "UZS"; exponent = 2; numCode = 860; name = "Uzbekistan som"; break;
        case ISO4217Currency.VEF: strCode = "VEF"; exponent = 2; numCode = 937; name = "Venezuelan bolívar fuerte"; break;
        case ISO4217Currency.VND: strCode = "VND"; exponent = 2; numCode = 704; name = "Vietnamese đồng"; break;
        case ISO4217Currency.VUV: strCode = "VUV"; exponent = 0; numCode = 548; name = "Vatu"; break;
        case ISO4217Currency.WST: strCode = "WST"; exponent = 2; numCode = 882; name = "Samoan tala"; break;
        case ISO4217Currency.XAF: strCode = "XAF"; exponent = 0; numCode = 950; name = "CFA franc BEAC"; break;
        case ISO4217Currency.XAG: strCode = "XAG"; exponent = null; numCode = 961; name = "Silver (one troy ounce)"; break;
        case ISO4217Currency.XAU: strCode = "XAU"; exponent = null; numCode = 959; name = "Gold (one troy ounce)"; break;
        case ISO4217Currency.XBA: strCode = "XBA"; exponent = null; numCode = 955; name = "European Composite Unit (EURCO) (bond market unit)"; break;
        case ISO4217Currency.XBB: strCode = "XBB"; exponent = null; numCode = 956; name = "European Monetary Unit (E.M.U.-6) (bond market unit)"; break;
        case ISO4217Currency.XBC: strCode = "XBC"; exponent = null; numCode = 957; name = "European Unit of Account 9 (E.U.A.-9) (bond market unit)"; break;
        case ISO4217Currency.XBD: strCode = "XBD"; exponent = null; numCode = 958; name = "European Unit of Account 17 (E.U.A.-17) (bond market unit)"; break;
        case ISO4217Currency.XCD: strCode = "XCD"; exponent = 2; numCode = 951; name = "East Caribbean dollar"; break;
        case ISO4217Currency.XDR: strCode = "XDR"; exponent = null; numCode = 960; name = "Special Drawing Rights"; break;
        //case ISO4217Currency.XFU: strCode = "XFU"; exponent = null; numCode = Nil; name = "UIC franc (special settlement currency)"; break;
        case ISO4217Currency.XOF: strCode = "XOF"; exponent = 0; numCode = 952; name = "CFA Franc BCEAO"; break;
        case ISO4217Currency.XPD: strCode = "XPD"; exponent = null; numCode = 964; name = "Palladium (one troy ounce)"; break;
        case ISO4217Currency.XPF: strCode = "XPF"; exponent = 0; numCode = 953; name = "CFP franc"; break;
        case ISO4217Currency.XPT: strCode = "XPT"; exponent = null; numCode = 962; name = "Platinum (one troy ounce)"; break;
        case ISO4217Currency.XTS: strCode = "XTS"; exponent = null; numCode = 963; name = "Code reserved for testing purposes"; break;
        //case ISO4217Currency.XXX: strCode = "XXX"; exponent = null; numCode = 999; name = "No currency"; break;
        case ISO4217Currency.YER: strCode = "YER"; exponent = 2; numCode = 886; name = "Yemeni rial"; break;
        case ISO4217Currency.ZAR: strCode = "ZAR"; exponent = 2; numCode = 710; name = "South African rand"; break;
        case ISO4217Currency.ZMK: strCode = "ZMK"; exponent = 2; numCode = 894; name = "Kwacha"; break;
        case ISO4217Currency.ZWD: strCode = "ZWD"; exponent = 2; numCode = 716; name = "Zimbabwe dollar"; break;
      }
    }

    /// <summary>
    /// Získanie identifikátora meny zo zadaného textového kódu meny
    /// </summary>
    /// <param name="currencyStrCode">Textový kód meny</param>
    /// <returns>Identifikátor meny</returns>
    public static ISO4217Currency GetCurrencyFromStrCode(string currencyStrCode)
    {
      currencyStrCode = currencyStrCode.ToUpper();
      switch (currencyStrCode)
      {
        case "AED": return ISO4217Currency.AED;
        case "AFN": return ISO4217Currency.AFN;
        case "ALL": return ISO4217Currency.ALL;
        case "AMD": return ISO4217Currency.AMD;
        case "ANG": return ISO4217Currency.ANG;
        case "AOA": return ISO4217Currency.AOA;
        case "ARS": return ISO4217Currency.ARS;
        case "AUD": return ISO4217Currency.AUD;
        case "AWG": return ISO4217Currency.AWG;
        case "AZN": return ISO4217Currency.AZN;
        case "BAM": return ISO4217Currency.BAM;
        case "BBD": return ISO4217Currency.BBD;
        case "BDT": return ISO4217Currency.BDT;
        case "BGN": return ISO4217Currency.BGN;
        case "BHD": return ISO4217Currency.BHD;
        case "BIF": return ISO4217Currency.BIF;
        case "BMD": return ISO4217Currency.BMD;
        case "BND": return ISO4217Currency.BND;
        case "BOB": return ISO4217Currency.BOB;
        case "BOV": return ISO4217Currency.BOV;
        case "BRL": return ISO4217Currency.BRL;
        case "BSD": return ISO4217Currency.BSD;
        case "BTN": return ISO4217Currency.BTN;
        case "BWP": return ISO4217Currency.BWP;
        case "BYR": return ISO4217Currency.BYR;
        case "BZD": return ISO4217Currency.BZD;
        case "CAD": return ISO4217Currency.CAD;
        case "CDF": return ISO4217Currency.CDF;
        case "CHE": return ISO4217Currency.CHE;
        case "CHF": return ISO4217Currency.CHF;
        case "CHW": return ISO4217Currency.CHW;
        case "CLF": return ISO4217Currency.CLF;
        case "CLP": return ISO4217Currency.CLP;
        case "CNY": return ISO4217Currency.CNY;
        case "COP": return ISO4217Currency.COP;
        case "COU": return ISO4217Currency.COU;
        case "CRC": return ISO4217Currency.CRC;
        case "CUP": return ISO4217Currency.CUP;
        case "CVE": return ISO4217Currency.CVE;
        case "CZK": return ISO4217Currency.CZK;
        case "DJF": return ISO4217Currency.DJF;
        case "DKK": return ISO4217Currency.DKK;
        case "DOP": return ISO4217Currency.DOP;
        case "DZD": return ISO4217Currency.DZD;
        case "EEK": return ISO4217Currency.EEK;
        case "EGP": return ISO4217Currency.EGP;
        case "ERN": return ISO4217Currency.ERN;
        case "ETB": return ISO4217Currency.ETB;
        case "EUR": return ISO4217Currency.EUR;
        case "FJD": return ISO4217Currency.FJD;
        case "FKP": return ISO4217Currency.FKP;
        case "GBP": return ISO4217Currency.GBP;
        case "GEL": return ISO4217Currency.GEL;
        case "GHS": return ISO4217Currency.GHS;
        case "GIP": return ISO4217Currency.GIP;
        case "GMD": return ISO4217Currency.GMD;
        case "GNF": return ISO4217Currency.GNF;
        case "GTQ": return ISO4217Currency.GTQ;
        case "GYD": return ISO4217Currency.GYD;
        case "HKD": return ISO4217Currency.HKD;
        case "HNL": return ISO4217Currency.HNL;
        case "HRK": return ISO4217Currency.HRK;
        case "HTG": return ISO4217Currency.HTG;
        case "HUF": return ISO4217Currency.HUF;
        case "IDR": return ISO4217Currency.IDR;
        case "ILS": return ISO4217Currency.ILS;
        case "INR": return ISO4217Currency.INR;
        case "IQD": return ISO4217Currency.IQD;
        case "IRR": return ISO4217Currency.IRR;
        case "ISK": return ISO4217Currency.ISK;
        case "JMD": return ISO4217Currency.JMD;
        case "JOD": return ISO4217Currency.JOD;
        case "JPY": return ISO4217Currency.JPY;
        case "KES": return ISO4217Currency.KES;
        case "KGS": return ISO4217Currency.KGS;
        case "KHR": return ISO4217Currency.KHR;
        case "KMF": return ISO4217Currency.KMF;
        case "KPW": return ISO4217Currency.KPW;
        case "KRW": return ISO4217Currency.KRW;
        case "KWD": return ISO4217Currency.KWD;
        case "KYD": return ISO4217Currency.KYD;
        case "KZT": return ISO4217Currency.KZT;
        case "LAK": return ISO4217Currency.LAK;
        case "LBP": return ISO4217Currency.LBP;
        case "LKR": return ISO4217Currency.LKR;
        case "LRD": return ISO4217Currency.LRD;
        case "LSL": return ISO4217Currency.LSL;
        case "LTL": return ISO4217Currency.LTL;
        case "LVL": return ISO4217Currency.LVL;
        case "LYD": return ISO4217Currency.LYD;
        case "MAD": return ISO4217Currency.MAD;
        case "MDL": return ISO4217Currency.MDL;
        case "MGA": return ISO4217Currency.MGA;
        case "MKD": return ISO4217Currency.MKD;
        case "MMK": return ISO4217Currency.MMK;
        case "MNT": return ISO4217Currency.MNT;
        case "MOP": return ISO4217Currency.MOP;
        case "MRO": return ISO4217Currency.MRO;
        case "MUR": return ISO4217Currency.MUR;
        case "MVR": return ISO4217Currency.MVR;
        case "MWK": return ISO4217Currency.MWK;
        case "MXN": return ISO4217Currency.MXN;
        case "MXV": return ISO4217Currency.MXV;
        case "MYR": return ISO4217Currency.MYR;
        case "MZN": return ISO4217Currency.MZN;
        case "NAD": return ISO4217Currency.NAD;
        case "NGN": return ISO4217Currency.NGN;
        case "NIO": return ISO4217Currency.NIO;
        case "NOK": return ISO4217Currency.NOK;
        case "NPR": return ISO4217Currency.NPR;
        case "NZD": return ISO4217Currency.NZD;
        case "OMR": return ISO4217Currency.OMR;
        case "PAB": return ISO4217Currency.PAB;
        case "PEN": return ISO4217Currency.PEN;
        case "PGK": return ISO4217Currency.PGK;
        case "PHP": return ISO4217Currency.PHP;
        case "PKR": return ISO4217Currency.PKR;
        case "PLN": return ISO4217Currency.PLN;
        case "PYG": return ISO4217Currency.PYG;
        case "QAR": return ISO4217Currency.QAR;
        case "RON": return ISO4217Currency.RON;
        case "RSD": return ISO4217Currency.RSD;
        case "RUB": return ISO4217Currency.RUB;
        case "RWF": return ISO4217Currency.RWF;
        case "SAR": return ISO4217Currency.SAR;
        case "SBD": return ISO4217Currency.SBD;
        case "SCR": return ISO4217Currency.SCR;
        case "SDG": return ISO4217Currency.SDG;
        case "SEK": return ISO4217Currency.SEK;
        case "SGD": return ISO4217Currency.SGD;
        case "SHP": return ISO4217Currency.SHP;
        case "SKK": return ISO4217Currency.SKK;
        case "SLL": return ISO4217Currency.SLL;
        case "SOS": return ISO4217Currency.SOS;
        case "SRD": return ISO4217Currency.SRD;
        case "STD": return ISO4217Currency.STD;
        case "SYP": return ISO4217Currency.SYP;
        case "SZL": return ISO4217Currency.SZL;
        case "THB": return ISO4217Currency.THB;
        case "TJS": return ISO4217Currency.TJS;
        case "TMM": return ISO4217Currency.TMM;
        case "TND": return ISO4217Currency.TND;
        case "TOP": return ISO4217Currency.TOP;
        case "TRY": return ISO4217Currency.TRY;
        case "TTD": return ISO4217Currency.TTD;
        case "TWD": return ISO4217Currency.TWD;
        case "TZS": return ISO4217Currency.TZS;
        case "UAH": return ISO4217Currency.UAH;
        case "UGX": return ISO4217Currency.UGX;
        case "USD": return ISO4217Currency.USD;
        case "USN": return ISO4217Currency.USN;
        case "USS": return ISO4217Currency.USS;
        case "UYU": return ISO4217Currency.UYU;
        case "UZS": return ISO4217Currency.UZS;
        case "VEF": return ISO4217Currency.VEF;
        case "VND": return ISO4217Currency.VND;
        case "VUV": return ISO4217Currency.VUV;
        case "WST": return ISO4217Currency.WST;
        case "XAF": return ISO4217Currency.XAF;
        case "XAG": return ISO4217Currency.XAG;
        case "XAU": return ISO4217Currency.XAU;
        case "XBA": return ISO4217Currency.XBA;
        case "XBB": return ISO4217Currency.XBB;
        case "XBC": return ISO4217Currency.XBC;
        case "XBD": return ISO4217Currency.XBD;
        case "XCD": return ISO4217Currency.XCD;
        case "XDR": return ISO4217Currency.XDR;
        //case "XFU": return ISO4217Currency.XFU;
        case "XOF": return ISO4217Currency.XOF;
        case "XPD": return ISO4217Currency.XPD;
        case "XPF": return ISO4217Currency.XPF;
        case "XPT": return ISO4217Currency.XPT;
        case "XTS": return ISO4217Currency.XTS;
        //case "XXX": return ISO4217Currency.XXX;
        case "YER": return ISO4217Currency.YER;
        case "ZAR": return ISO4217Currency.ZAR;
        case "ZMK": return ISO4217Currency.ZMK;
        case "ZWD": return ISO4217Currency.ZWD;
        default: throw new ArgumentException("Unknown currency code: " + currencyStrCode);
      }
    }
  }
}