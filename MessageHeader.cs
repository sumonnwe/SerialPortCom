namespace NETSFUNCTION
{
    internal class MessageHeader
    {         
        public MessageHeader() { }

        public byte[] ECN { get; set; }

        public byte[] FunctionCode { get; set; }

        public byte[] VersionCode { get; set; }

        public byte RFU { get; set; }

        public byte Seperator { get; set; }         
    }
}
