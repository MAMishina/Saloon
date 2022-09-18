using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saloon
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime[] input=new DateTime[100];
            int[] impatience = new int[100];
            DateTime[] output=new DateTime[100];
            int N;
            FillInTheOriginalArrays(out input, out impatience, out N);
            FillInTheResultingArray(input, impatience, N, out output);
            PrintResultInFile(output);

        }

        public static void FillInTheOriginalArrays(out DateTime[] input, out int[] impatience, out int N)
        {
            StreamReader sr = new StreamReader("In.txt");
            N = Convert.ToInt32(sr.ReadLine());
            input = new DateTime[N];
            impatience = new int[N];

            for (int i = 0; i < N; i++)
            {
                string[] client = sr.ReadLine().Split(' ');
                input[i] = new DateTime(1, 1, 1,
                    Convert.ToInt32(client[0]), Convert.ToInt32(client[1]), 0);
                impatience[i] = Convert.ToInt32(client[2]);
            }

            sr.Close();
            
        }

        public static void FillInTheResultingArray(DateTime[] input, int[] impatience, int N, out DateTime[] output)
        {
            output = new DateTime[N];

            for (int i = 0; i < N; i++)
            {
                int countClient = 0;
                int difference = 0;

                for (int j = i - 1; j >= 0; j--)
                    if (output[j] > input[i])
                    {
                        countClient++;

                        difference = i - j;    
                    }

                if (impatience[i] < countClient)
                    output[i] = input[i];
                else
                {
                    output[i] = input[i].AddMinutes(20);

                    for (int j = i - 1; j >= i - difference; j--)
                        if (output[j] > input[i])
                        {
                            output[i] = output[j].AddMinutes(20);

                            break;
                        }
                }
            }
        }

        public static void PrintResultInFile(DateTime[] output)
        {

            StreamWriter sw = new StreamWriter("Out.txt");

            for (int i = 0; i < output.Length; i++)
                sw.WriteLine(output[i].Hour + " " + output[i].Minute);

            sw.Close();
        }
    }
}