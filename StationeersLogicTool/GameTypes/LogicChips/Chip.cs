using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationeersLogicTool.GameTypes.Networking;

namespace StationeersLogicTool.GameTypes.LogicChips
{
    public enum ChipFace { TOP = 0, LEFT = 1, RIGHT = 2, BOTTOM = 3 }
    public abstract class Chip
    {
        public string Label { get; set; }

        public int PowerUsage { get; private set; }

        protected enum PortSpec { BLANK, POWER, DATA_IN, DATA_OUT }
        // Helper accessors defined at end of class
        protected partial struct PortMap { }

        private readonly Port[] ports = new Port[4];

        public Port TopPort => ports[(int)ChipFace.TOP];
        public Port LeftPort =>ports[(int)ChipFace.LEFT];
        public Port RightPort => ports[(int)ChipFace.RIGHT];
        public Port BottomPort => ports[(int)ChipFace.BOTTOM];

        public IEnumerable<PowerPort> PowerPorts => ports.Filter<PowerPort>();
        public IEnumerable<DataPort> DataPorts => ports.Filter<DataPort>();
        public IEnumerable<DataInPort> DataInPorts => ports.Filter<DataInPort>();
        public IEnumerable<DataOutPort> DataOutPorts => ports.Filter<DataOutPort>();

        protected Chip(string label, int power, PortMap map)
        {
            Label = label;
            PowerUsage = power;

            ports = map.Select(s => InstantiatePortOfType(this, s)).ToArray();
        }

        private static Port InstantiatePortOfType(Chip c, PortSpec s) => s switch
        {
            PortSpec.POWER => Port.Create<PowerPort>(c),
            PortSpec.DATA_IN => Port.Create<DataInPort>(c),
            PortSpec.DATA_OUT => Port.Create<DataOutPort>(c),
            _ => null
        };

        protected partial struct PortMap : IEnumerable<PortSpec>
        {
            private (PortSpec Top, PortSpec Left, PortSpec Right, PortSpec Bottom) map;

            public PortSpec Top { get => map.Top; set => map.Top = value; }
            public PortSpec Left { get => map.Left; set => map.Left = value; }
            public PortSpec Right { get => map.Right; set => map.Right = value; }
            public PortSpec Bottom { get => map.Bottom; set => map.Bottom = value; }

            public IEnumerator<PortSpec> GetEnumerator()
            {
                yield return map.Top;
                yield return map.Left;
                yield return map.Right;
                yield return map.Bottom;
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
    public abstract class PoweredChip : Chip
    {
        public PowerPort PowerPort => PowerPorts.FirstOrDefault();

        protected PoweredChip(string label, int power, PortMap map) : base(label, power, map) { }
    }
}
