using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersLogicTool.GameTypes.LogicChips
{
    public abstract class LogicMemoryChip : Chip
    {
        protected LogicMemoryChip(string label) : base(label, 0,
            new PortMap() { Left = PortSpec.DATA_OUT, Right = PortSpec.DATA_OUT })
        { }
    }

    public class LogicMemoryUnit : LogicMemoryChip
    {
        private int valueX10;
        public double Value { get => (double)valueX10 / 10; set { valueX10 = (int)Math.Floor(value * 10); } }

        public LogicMemoryUnit() : this(null, 0) { }
        public LogicMemoryUnit(string label) : this(label, 0) { }
        public LogicMemoryUnit(double value) : this(null, value) { }
        public LogicMemoryUnit(string label, double value) : base(label) => Value = value;
    }
}
