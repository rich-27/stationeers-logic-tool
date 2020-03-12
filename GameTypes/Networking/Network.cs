using System.Collections.Generic;

namespace StationeersLogicTool.GameTypes.Networking
{
    public class Network
    {
        protected List<Port> ports = new List<Port>();
        public List<Port> Ports => ports;
    }
}
