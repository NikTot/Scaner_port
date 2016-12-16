namespace Scaner_port
{
    class PortInfo
    {
        public int PortNumber { get; set; }
        public string Local { get; set; }
        public string Remote { get; set; }
        public string State { get; set; }

        public PortInfo (int i, string local, string remote, string state)
        {
            PortNumber = i;
            Local = local;
            Remote = remote;
            State = state;
        }
    }
}
