using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class PackageRegister
    {
        public delegate void PackageHandler(ProtoBuf.IExtensible package, object args);
        public class PackageData
        {
            public int mid;
            public Type pack;
            public PackageHandler handler;
        }


        private PackageRegister() { }
        public static readonly PackageRegister inst = new PackageRegister();

        private Dictionary<int, PackageData> _binder;

        public bool regist(int id, Type pack, PackageHandler handler)
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


        public PackageData get(int mid)
        {
            if (_binder.ContainsKey(mid) == false)
                return null;
            return _binder[mid];
        }
    }
}
