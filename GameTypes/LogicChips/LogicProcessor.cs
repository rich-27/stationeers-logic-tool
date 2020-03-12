using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersLogicTool.GameTypes.LogicChips
{
    public abstract class LogicProcessorChip : Chip
    {
        protected LogicProcessorChip(string label) : base(label, 25,
            new PortMap() { Top = PortSpec.POWER, Left = PortSpec.DATA_IN, Right = PortSpec.DATA_IN, Bottom = PortSpec.DATA_OUT })
        { }
    }

    public enum MathMode { ADD, SUBTRACT, MULTIPLY, DIVIDE };
    public class MathUnit : LogicProcessorChip
    {
        public MathMode Mode { get; set; }
        public MathUnit() : this(null) { }
        public MathUnit(string label) : base(label) { }
    }
    public class MinMaxUnit : LogicProcessorChip
    {
        public MinMaxUnit() : this(null) { }
        public MinMaxUnit(string label) : base(label) { }
    }
}