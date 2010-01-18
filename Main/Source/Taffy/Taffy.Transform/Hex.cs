using System;
using System.Text;

namespace Taffy.Transform {
    public class Hex {
        public static string ToString(string hexData) {
            string Data1 = "";
            string sData = "";

            while (hexData.Length > 0)
            //first take two hex value using substring.
            //then  convert Hex value into ascii.
            //then convert ascii value into character.
            {
                Data1 = System.Convert.ToChar(System.Convert.ToUInt32(hexData.Substring(0, 2), 16)).ToString();
                sData = sData + Data1;
                hexData = hexData.Substring(2, hexData.Length - 2);
            }
            return sData;
        }
        public static string FromString(string input) {
            //first take each charcter using substring.
            //then  convert character into ascii.
            //then convert ascii value into Hex Format

            string sValue;
            string sHex = "";
            foreach (char c in input.ToCharArray()) {
                sValue = String.Format("{0:X}", Convert.ToUInt32(c));
                sHex = sHex + sValue;
            }
            return sHex;
        }
    }
}