using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace RealtickTickerConverter
{
    class ClipboardToTicker
    {

        Dictionary<string, string> code = new Dictionary<string,string>();
        Dictionary<string, string> years = new Dictionary<string, string>();
        Dictionary<string, string> expirations = new Dictionary<string, string>();
        public ClipboardToTicker()
        {
            code.Add("jan+call", "A");
            code.Add("jan+put", "M");
            code.Add("feb+call", "B");
            code.Add("feb+put", "N");
            code.Add("mar+call", "C");
            code.Add("mar+put", "O");
            code.Add("apr+call", "D");
            code.Add("apr+put", "P");
            code.Add("may+call", "E");
            code.Add("may+put", "Q");
            code.Add("jun+call", "F");
            code.Add("jun+put", "R");
            code.Add("jul+call", "G");
            code.Add("jul+put", "S");
            code.Add("aug+call", "H");
            code.Add("aug+put", "T");
            code.Add("sep+call", "I");
            code.Add("sep+put", "U");
            code.Add("oct+call", "J");
            code.Add("oct+put", "V");
            code.Add("nov+call", "K");
            code.Add("nov+put", "W");
            code.Add("dec+call", "L");
            code.Add("dec+put", "X");
        
            years.Add("11", "1");
            years.Add("12", "2");

            expirations.Add("nov+11", "19");
            expirations.Add("dec+11", "17");
            expirations.Add("jan+12", "21");
            expirations.Add("feb+12", "18");
            expirations.Add("mar+12", "17");
            expirations.Add("apr+12", "21");
            expirations.Add("may+12", "19");
            expirations.Add("jun+12", "16");
            expirations.Add("jul+12", "21");
            expirations.Add("aug+12", "18");
            expirations.Add("sep+12", "22");
            expirations.Add("oct+12", "20");
            expirations.Add("nov+12", "17");
            expirations.Add("dec+12", "22");

            expirations.Add("jan+13", "19");
            expirations.Add("feb+13", "16");
            expirations.Add("mar+13", "16");
            expirations.Add("apr+13", "20");
            expirations.Add("may+13", "18");
            expirations.Add("jun+13", "22");
            expirations.Add("jul+13", "20");
            expirations.Add("aug+13", "17");
            expirations.Add("sep+13", "21");
            expirations.Add("oct+13", "19");
            expirations.Add("nov+13", "16");
            expirations.Add("dec+13", "21");
        }
        static string REGEX = @"(\d+):(\d+):(\d+)\s+PM\s+(\w+)\s+(\w+)\s+(\d+).(\d+)\s+(\w+)";
        public bool isClipboardFinancialRelated(string txt)
        {
            Match m = Regex.Match(txt, REGEX);
            return m.Success;
        }

 

        public string generateMonthYearCallPutCode(string month, string year, string callPut)
        {   
            string key2 = month+"+"+callPut;
            key2 = key2.ToLower();
            string key1 = month + "+" + year;
            key1 = key1.ToLower();
            return expirations[key1]+ code[key2] + years[year];

        }

        public string generateRealtickTicker(string txt)
        {
            Regex r = new Regex(REGEX);
            int[] gnums = r.GetGroupNumbers();
            Match m = r.Match(txt);

            string ticker = m.Groups[4].Captures[0].ToString();
            string monthYear = m.Groups[5].Captures[0].ToString();
            string month = monthYear.Substring(0, 3);
            string year = monthYear.Substring(3, 2);
            string strike = m.Groups[6].Captures[0].ToString();
            string callPut = m.Groups[8].Captures[0].ToString();
            string code = generateMonthYearCallPutCode(month, year,callPut);
            return "+" + ticker + "\\" + code + "\\" + strike;
        }

    }
}
