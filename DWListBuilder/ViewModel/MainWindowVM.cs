using DWListBuilder.Utilities;
using DystopianWarsCalc.Data;
using DystopianWarsCalc.Model.Rules;
using DystopianWarsCalc.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWListBuilder.ViewModel
{
    internal class MainWindowVM
    {
        internal MainWindowVM()
        {
            List<Orbat> orbats;

            Directory.CreateDirectory(Defines.ORBATStorePath);
            var processedFiles = Directory.GetFiles(Defines.ORBATStorePath);
            if (processedFiles.Count() == 0)
            {
                var files = Directory.GetFiles(Defines.ORBATPath);
                orbats = new List<Orbat>();

                foreach (var file in files)
                {
                    var result = OrbatPDFExtractor.LoadORBAT(file);
                    orbats.Add(result);
                    SerializationHelper.WriteOrbat(result);
                }
            }
            else
            {
                orbats = SerializationHelper.ReadOrbats() as List<Orbat>;
            }
           

            this.OrbatEditorVM = new OrbatEditorVM(orbats);
        }

        public OrbatEditorVM OrbatEditorVM { get; private set; }
    }
}
