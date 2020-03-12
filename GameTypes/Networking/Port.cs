using StationeersLogicTool.GameTypes.LogicChips;

namespace StationeersLogicTool.GameTypes.Networking
{
    public abstract class Port
    {
        public Chip Chip { get; private set; }
        public Network Network { get; private set; }

        public static T Create<T>(Chip chip) where T : Port, new() => new T() { Chip = chip };
        
        public void SetNetwork(Network network) { ClearNetwork(); Network = network; network.Ports.Add(this); }
        public void ClearNetwork() { Network?.Ports.Remove(this); Network = null; }
    }

    public class PowerPort : Port { }

    public abstract class DataPort : Port { }
    public class DataInPort : DataPort { }
    public class DataOutPort : DataPort
    {
        public void Connect(DataInPort port)
        {
            if (Network is null) { SetNetwork(new Network()); }
            port.SetNetwork(Network);
        }
    }
}
