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
                chips.Last(c => c is LogicReader).OutPort);

            powerGrid = new Network();

            model.AddChip(new LogicReader(), powerGrid);
            model.AddChip(new LogicMemoryUnit(15));
            model.AddChip(new MinMaxUnit(), powerGrid,
                chips.Last(c => c is LogicReader).OutPort,
                chips.Last(c => c is LogicMemoryUnit).OutPort);
            model.AddChip(new MathUnit() { Mode = MathMode.SUBTRACT }, powerGrid,
                chips.Last(c => c is LogicReader).OutPort,
                chips.Last(c => c is MinMaxUnit).OutPort);

            model.AddChip(new LogicMemoryUnit(1.5));
            model.AddChip(new MathUnit() { Mode = MathMode.DIVIDE }, powerGrid,
                chips.Last(c => c is MathUnit).OutPort,
                chips.Last(c => c is LogicMemoryUnit).OutPort);

            model.AddChip(new LogicMemoryUnit(100));
            model.AddChip(new MathUnit() { Mode = MathMode.MULTIPLY }, powerGrid,
                chips.First(c => c is LogicReader).OutPort,
                chips.Last(c => c is LogicMemoryUnit).OutPort);

            model.AddChip(new MinMaxUnit(), powerGrid,
                chips.Last(c => c is MathUnit m && m.Mode == MathMode.DIVIDE).OutPort,
                chips.Last(c => c is MathUnit).OutPort);
            model.AddChip(new BatchWriter(), powerGrid,
                chips.Last(c => c is MinMaxUnit).OutPort);
        }
    }
}
