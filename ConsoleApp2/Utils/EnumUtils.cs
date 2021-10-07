using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Utils
{
    static class EnumUtils
    {

        public static TEnum GetEnumTypeFromConsole<TEnum>(String promptString, String invalidFormatString)
            where TEnum : struct
        {
            TEnum rp;
            String inputString;
            while (true)
            {
                try
                {
                    Console.WriteLine(promptString);
                    inputString = Console.ReadLine().Trim();

                    rp = Enum.Parse<TEnum>(inputString, true);

                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(invalidFormatString);
                }
            }

            return rp;
            
        } 

        public static TEnum[] GetValues<TEnum>() where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}
