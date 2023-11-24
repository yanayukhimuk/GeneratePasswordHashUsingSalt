using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordHashUsingSalt
{
    [MemoryDiagnoser()]
    public class Benchmark
    {
        readonly string password = "Password";
        readonly byte[] salt = new byte[32];

        [Benchmark]
        public void OldImplementation()
        {
            Program.GeneratePasswordHashUsingSalt(password, salt);
        }

        [Benchmark(Baseline = true)]
        public void NewImplementation()
        {
            Program.GeneratePasswordHashUsingSaltUpdated(password, salt);
        }
    }
}
