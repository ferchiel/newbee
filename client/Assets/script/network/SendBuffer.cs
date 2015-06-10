using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class SendBuffer
    {
        private readonly MemoryStream _ms = new MemoryStream();

        public void push<T>(int msgId, T inst)
        {
            byte[] id = BitConverter.GetBytes(msgId);
            _ms.Write(id, 0, id.Length);
            Serializer.SerializeWithLengthPrefix<T>(_ms, inst, PrefixStyle.Base128);
        }

        public byte[] get()
        {
            return _ms.GetBuffer();
        }
    }
}
