using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DystopianWarsCalc.Model.Rules
{
    public class Orbat
    {
        internal List<Weapon> weapons = new List<Weapon>();
        public IReadOnlyList<Weapon> Weapons
        {
            get
            {
                return this.weapons;
            }
        }

        internal List<Datasheet> datasheets = new List<Datasheet>();

        public IReadOnlyList<Datasheet> Datasheets
        {
            get
            {
                return this.datasheets;
            }
        }

        internal Dictionary<string, Trait> traits = new Dictionary<string, Trait>();
        public IReadOnlyList<Trait> Traits
        {
            get
            {
                return this.traits.Values.ToList();
            }
        }

        public Trait GetTrait(string name)
        {
            if (!this.traits.ContainsKey(name))
            {
                var newTrait = new Trait(name);
                this.traits.Add(name, newTrait);
            }

            return this.traits[name];
        }

        internal Dictionary<string, SpecialRule> specialRules = new Dictionary<string, SpecialRule>();
        public IReadOnlyList<SpecialRule> SpecialRules
        {
            get
            {
                return this.specialRules.Values.ToList();
            }
        }

        public SpecialRule GetSpecialRule(string name)
        {
            if (!this.specialRules.ContainsKey(name))
            {
                var newSpecialRule = new SpecialRule(name);
                this.specialRules.Add(name, newSpecialRule);
            }

            return this.specialRules[name];
        }

        public Orbat(string fileName)
        {
            FileName = fileName;
        }

        public string FactionName
        {
            get; internal set;
        }

        public string FileName { get; private set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
