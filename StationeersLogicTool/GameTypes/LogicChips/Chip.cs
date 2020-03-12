using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationeersLogicTool.GameTypes.Networking;

namespace StationeersLogicTool.GameTypes.LogicChips
{
    public static class ChipFactory
    {
        public enum PortSpec { BLANK, POWER, DATA_IN, DATA_OUT }
        public struct PortMap
        {
            private readonly PortSpec[] map;

            public PortSpec Top { get { return map[(int)ChipFace.TOP]; } set { map[(int)ChipFace.TOP] = value; } }
            public PortSpec Left { get { return map[(int)ChipFace.LEFT]; } set { map[(int)ChipFace.LEFT] = value; } }
            public PortSpec Right { get { return map[(int)ChipFace.RIGHT]; } set { map[(int)ChipFace.RIGHT] = value; } }
            public PortSpec Bottom { get { return map[(int)ChipFace.BOTTOM]; } set { map[(int)ChipFace.BOTTOM] = value; } }
        }
    }
    public enum ChipFace { TOP = 0, LEFT = 1, RIGHT = 2, BOTTOM = 3 }
    public abstract class Chip
    {

        public string Label { get; set; }

        public int PowerUsage { get; private set; }

        private readonly Port[] ports = new Port[4];

        public Port TopPort => ports[(int)ChipFace.TOP];
        public Port LeftPort =>ports[(int)ChipFace.LEFT];
        public Port RightPort => ports[(int)ChipFace.RIGHT];
        public Port BottomPort => ports[(int)ChipFace.BOTTOM];

        public IEnumerable<PowerPort> PowerPorts => ports.Where(m => m is PowerPort).Cast<PowerPort>();
        public IEnumerable<DataPort> DataPorts => ports.Where(m => m is DataPort).Cast<DataPort>();
        public IEnumerable<DataInPort> DataInPorts => ports.Where(m => m is DataInPort).Cast<DataInPort>();
        public IEnumerable<DataOutPort> DataOutPorts => ports.Where(m => m is DataOutPort).Cast<DataOutPort>();

        protected Chip(string label, int power)
        {
            Label = label;
            PowerUsage = power;
        }

        private Port InstantiatePortOfType(PortSpec s)
        {
            switch (s)
            {
                case PortSpec.POWER:
                    return Port.Create<PowerPort>(this);
                case PortSpec.DATA_IN:
                    return Port.Create<DataInPort>(this);
                case PortSpec.DATA_OUT:
                    return Port.Create<DataOutPort>(this);
                default:
                    return null;
            }
        }
    }
}
