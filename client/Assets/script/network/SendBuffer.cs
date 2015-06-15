using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class SendBuffer : Buffer
    {
        private readonly MemoryStream _ms = new MemoryStream();

        public SendBuffer(int sz)
            : base(sz)
        {

        }

        public void push<T>(int mid, T inst)
        {
            Serializer.Serialize<T>(_ms, inst);
            int length = (int) _ms.Length;
            set(BitConverter.GetBytes(length), sizeof(int));
            set(BitConverter.GetBytes(mid), sizeof(int));
            set(_ms.GetBuffer(), length);
            _ms.Flush();
        }
    }
}
