namespace NETSFUNCTION
{
    internal class MessageData
    {
        public MessageData() { }
        public byte[] FieldCode { get; set; }
        public int Len { get; set; }
        public byte[] Data { get; set; }

        public byte Separator { get; set; }  
    }
}
