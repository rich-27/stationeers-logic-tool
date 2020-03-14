using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationeersLogicTool.GameTypes.LogicChips;
using StationeersLogicTool.GameTypes.Networking;

namespace StationeersLogicTool.Models
{
    public class LogicModel
    {
        public List<Chip> Chips { get; } = new List<Chip>();

        public List<Network> PowerNetworks
        {
            get => Chips.SelectMany(c => c.PowerPorts.Select(p => p.Network)).Distinct().ToList();
        }

        public List<Network> DataNetworks
        {
            get => Chips.SelectMany(c => c.DataPorts.Select(p => p.Network)).Distinct().Where(n => n?.Ports.Count > 1).ToList();
        }
        
        public void AddChip(Chip chip, Network powerNetwork = null, DataOutPort in1Connection = null, DataOutPort in2Connection = null)
        {
            Chips.Add(chip);

            if (powerNetwork is object && chip is PoweredChip p) { p.PowerPort.SetNetwork(powerNetwork); }

            if (chip.DataInPorts.FirstOrDefault() is DataInPort dataIn1) { in1Connection?.Connect(dataIn1); }
            if (chip.DataInPorts.ElementAtOrDefault(1) is DataInPort dataIn2) { in2Connection?.Connect(dataIn2); }
        }
    }
}