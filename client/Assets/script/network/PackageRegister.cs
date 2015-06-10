using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class PackageRegister
    {
        public static delegate void PackageHandler(ProtoBuf.IExtensible package, object args);
        public struct PackageData
        {
            public proto.MsgId.MSG mid;
            public Type pack;
            public PackageHandler handler;
        }


        private PackageRegister() { }
        public static readonly PackageRegister inst = new PackageRegister();

        private Dictionary<proto.MsgId.MSG, PackageData> _binder;

        public bool regist(proto.MsgId.MSG id, Type pack, PackageHandler handler)
        {
            if (_binder.ContainsKey(id))
                return false;

            PackageData data = new PackageData();
            data.mid = id;
            data.pack = pack;
            data.handler = handler;

            _binder.Add(id, data);
            return true;
        }

        public bool regist(PackageData data)
        {
            if (_binder.ContainsKey(data.mid))
                return false;

            _binder.Add(data.mid, data);
            return true;
        }

        public ProtoBuf.IExtensible getPackage(proto.MsgId.MSG id)
        {
            if (_recv.ContainsKey(id) == true)
                return _recv[id];
            return null;
        }

        public void remove(proto.MsgId.MSG id)
        {
            if (_recv.ContainsKey(id) == false)
                return;

            ProtoBuf.IExtensible package = _recv[id];
            _recv.Remove(id);

            if (_send.ContainsKey(package) == false)
                return;

            _send.Remove(package);
        }

        public void remove(ProtoBuf.IExtensible package)
        {
            if (_send.ContainsKey(package) == false)
                return;

            proto.MsgId.MSG id = _send[package];
            _send.Remove(package);

            if (_recv.ContainsKey(id) == false)
                return;

            _recv.Remove(id);
        }

        public void clear()
        {
            _recv.Clear();
            _send.Clear();
        }
    }
}
