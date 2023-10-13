using DystopianWarsCalc.Model.Rules;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DWListBuilder.Utilities
{
    public static class SerializationHelper
    {
        // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0

        public static IList<Orbat> ReadOrbats()
        {
            var files = Directory.GetFiles(Defines.ORBATStorePath);
            IList<Orbat> orbats = new List<Orbat>();
            foreach (var file in files)
            {
                try
                {
                    string jsonString = File.ReadAllText(file);
                    Orbat newOrbat = JsonSerializer.Deserialize<Orbat>(jsonString);
                    orbats.Add(newOrbat);
                }
                catch (Exception e)
                {
                    int x = 0;
                }
            }

            return orbats;
        }

        public static void WriteOrbat(Orbat orbat)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonString = JsonSerializer.Serialize(orbat, options);
            File.WriteAllText(Defines.ORBATStorePath, jsonString);
        }
    }
}
