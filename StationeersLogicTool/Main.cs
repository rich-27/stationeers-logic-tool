using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationeersLogicTool.GameTypes.LogicChips;
using StationeersLogicTool.GameTypes.Networking;
using StationeersLogicTool.Models;

namespace StationeersLogicTool
{
    class Main
    {
        public static void Execute(LogicModel model)
        {
            List<Chip> chips = model.Chips;

            Network powerGrid = new Network();

            model.AddChip(new LogicReader(), powerGrid);
            model.AddChip(new LogicWriter(), powerGrid,
                chips.Last<LogicReader>().DataOutPort);

            powerGrid = new Network();

            model.AddChip(new LogicReader(), powerGrid);
            model.AddChip(new LogicMemoryUnit(15));
            model.AddChip(new MinMaxUnit(), powerGrid,
                chips.Last<LogicReader>().DataOutPort,
                chips.Last<LogicMemoryUnit>().DataOutPorts.First());
            model.AddChip(new MathUnit() { Mode = MathMode.SUBTRACT }, powerGrid,
                chips.Last<LogicReader>().DataOutPort,
                chips.Last<MinMaxUnit>().DataOutPort);

            model.AddChip(new LogicMemoryUnit(1.5));
            model.AddChip(new MathUnit() { Mode = MathMode.DIVIDE }, powerGrid,
                chips.Last<MathUnit>().DataOutPort,
                chips.Last<LogicMemoryUnit>().DataOutPorts.First());

            model.AddChip(new LogicMemoryUnit(100));
            model.AddChip(new MathUnit() { Mode = MathMode.MULTIPLY }, powerGrid,
                chips.First<LogicReader>().DataOutPort,
                chips.Last<LogicMemoryUnit>().DataOutPorts.First());

            model.AddChip(new MinMaxUnit(), powerGrid,
                chips.Last<MathUnit>(c => c.Mode == MathMode.DIVIDE).DataOutPort,
                chips.Last<MathUnit>().DataOutPort);
            model.AddChip(new BatchWriter(), powerGrid,
                chips.Last<MinMaxUnit>().DataOutPort);
        }
    }
}
