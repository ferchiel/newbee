using Assets.script.network;
using proto.c2s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.handler
{
    class TestHandler
    {
        public void registe()
        {
            PackageRegister.inst.regist(20, (new HeartBeat()).GetType(), tHandler);
        }

        public void tHandler(ProtoBuf.IExtensible package, object args)
        {

        }
    }
}
