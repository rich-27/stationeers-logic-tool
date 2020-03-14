using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StationeersLogicTool.GameTypes.Networking;

namespace StationeersLogicTool.GameTypes.LogicChips
{
    public abstract class LogicIOChip : PoweredChip
    {
        public DataOutPort DataOutPort => DataOutPorts.FirstOrDefault();

        protected LogicIOChip(string label) : base(label, 10,
            new PortMap() { Top = PortSpec.POWER, Left = PortSpec.DATA_IN, Right = PortSpec.DATA_OUT })
        { }
    }

    public class LogicReader : LogicIOChip
    {
        public LogicReader() : this(null) { }
        public LogicReader(string label) : base(label) { }
    }
    public class LogicWriter : LogicIOChip
    {
        public LogicWriter() : this(null) { }
        public LogicWriter(string label) : base(label) { }
    }
    public class BatchWriter : LogicIOChip
    {
        public BatchWriter() : this(null) { }
        public BatchWriter(string label) : base(label) { }
    }
}