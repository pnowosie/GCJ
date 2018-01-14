using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace qualA
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var inputReader = new StreamReader(args[0]))
            using (var outputWriter = new StreamWriter(args[0]+".out"))
            {
                var cases = int.Parse(inputReader.ReadLine());

                for (var currentCase = 1; currentCase <= cases; currentCase++)
                {
                    var number = BigInteger.Parse(inputReader.ReadLine());

                    while (true)
                    {
                        var untidyPosition = -1;
                        var numberString = number.ToString();
                        var currentDigit = '0';
                        for (var i = 0; i < numberString.Length; i++)
                        {
                            if (numberString[i] < currentDigit)
                            {
                                untidyPosition = i;
                                break;
                            }
                            currentDigit = numberString[i];
                        }
                        if (untidyPosition == -1)
                        {
                            break;
                        }

                        var power = numberString.Length - untidyPosition;
                        var higher = BigInteger.Parse(numberString.Substring(0, numberString.Length - power)) - 1;
                        var lower = new string('9', power);
                        number = BigInteger.Parse(higher + lower);
                    }

                    outputWriter.WriteLine("Case #{0}: {1}", currentCase, number);
                }
            }
        }
    }
}
