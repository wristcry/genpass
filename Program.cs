using System;
using System.Security.Cryptography;

namespace genpass {
    class Program {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();
        static readonly Random r = new Random();

        static int rand(int max) {
            return r.Next(0, max);
        }

        static int rand_s(int max) {
            byte[] data = new byte[sizeof(uint)];
            rng.GetBytes(data);
            double u = BitConverter.ToUInt32(data, 0) / (uint.MaxValue + 1.0);
            return (int)Math.Floor((0 + ((double)max - 0) * u));
        }

        static string randstr(int length) {
            char[] c = new char[length];
            for (int i = 0; i < c.Length; i++)
                c[i] = chars[rand(chars.Length)];
            return new string(c);
        }

        static string randstr_s(int length) {
            char[] c = new char[length];
            for (int i = 0; i < c.Length; i++)
                c[i] = chars[rand_s(chars.Length)];
            return new string(c);
        }

        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Password Generator \n\n" +
                    "Usage: genpass.exe [args] <length>\n\n" +
                    "Arguments \n\n     " +
                    "--secure | -s   Use cryptographically secure RNG instead of System.Random");
                return;
            }
            bool s = false;
            int l = 0;
            string p;
            for (int i = 0; i < args.Length; i++)
                if (args[i][0] == '-') {
                    if (args[i] == "--secure" || args[i] == "-s") s = true;
                }
                else l = Convert.ToInt32(args[i]);
            if (l < 4) {
                Console.WriteLine("length must be >= 4");
                return;
            }
            if (s) p = randstr_s(l);
            else p = randstr(l);
            Console.WriteLine(p);
            return;
        }
    }
}
