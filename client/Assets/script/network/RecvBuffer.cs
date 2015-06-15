using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.script.network
{
    class RecvBuffer : Buffer
    {
        private const int kMsgIdError = -1;
        private const int kMsgLengthError = -1;
        private int _mid = kMsgIdError;
        private int _length = kMsgLengthError;

        public RecvBuffer(int sz)
            : base(sz)
        {

        }

        public void dispatch()
        {
            if (kMsgIdError == _mid)
            {
                // get mid
                if (avail() < sizeof(int)) // 
                    return;

                if (kMsgIdError == (_mid = BitConverter.ToInt32(get(sizeof(int)), 0)))
                    return;
            }

            if (kMsgLengthError == _length)
            {
                // get package length
                if (avail() < sizeof(int))
                    return;

                if (kMsgLengthError == (_length = BitConverter.ToInt32(get(sizeof(int)), 0)))
                    return;
            }

            // wait net stream
            int readsz = avail();
            if (readsz < _length)
                return;

            byte[] packagedata = get(readsz);
            PackageRegister.PackageData packageAssist = PackageRegister.inst.get(_mid);
            global::ProtoBuf.IExtensible protoPackage = (global::ProtoBuf.IExtensible)
                packageAssist.pack.Assembly.CreateInstance(packageAssist.pack.FullName);

            packageAssist.handler(protoPackage, this);
        }


        // base-128
        private int encode_base128(byte[] buf, UInt64 x)
        {
            int n = 0;
            while (x > 127)
            {
                buf[n++] = (byte)(0x80 | (x & 0x7F));
                x >>= 7;
            }
            buf[n++] = (byte)x;
            return n;
        }

        private UInt64 decode_base128(byte[] buf)
        {
            int shift, n = 0;
            UInt64 x = 0, c;
            for (shift = 0; shift < 64; shift += 7)
            {
                c = (UInt64)buf[n++];
                x |= (c & 0x7F) << shift;
                if ((c & 0x80) == 0)
                {
                    break;
                }
            }
            return x;
        }
    }
}
