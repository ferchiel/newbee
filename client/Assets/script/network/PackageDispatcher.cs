using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class PackageDispatcher
    {
        private PackageDispatcher() { }
        public static readonly PackageDispatcher inst = new PackageDispatcher();

        private proto.MsgId.MSG _id = proto.MsgId.MSG.Msg_id_error;
        private int _size = 0;
        private byte[] _packageData = null;

        public void dispatch(RecvBuffer buf)
        {

        }

        int encode_varint(byte[] buf, UInt64 x)
        {
            int n;
            n = 0;
            while (x > 127)
            {
                buf[n++] = (byte)(0x80 | (x & 0x7F));
                x >>= 7;
            }
            buf[n++] = (byte)x;
            return n;
        }
        uint64_t decode_varint(char* buf)
        {
            int shift, n;
            uint64_t x, c;
            n = 0;
            x = 0;
            for (shift = 0; shift < 64; shift += 7)
            {
                c = (uint64_t)buf[n++];
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
