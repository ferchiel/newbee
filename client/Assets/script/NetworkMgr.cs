using System;
using System.Text;
using System.Timers;
using UnityEngine;
using proto.c2s;
using ProtoBuf.Meta;
using ProtoBuf;
using System.IO;

namespace Castle.Network
{
    class NetworkMgr : MonoBehaviour
    {
        private CastleNetwork _network = null;
        private int _clientid = 10;
        private MemoryStream _io = null;
        private HeartBeat _heart = null;

        // Use this for initialization
        void Start()
        {
            _network = new CastleNetwork();
            _network.init("192.168.111.128", 7878);
            //_network.send()

            //_heart = new HeartBeat();
            //_heart.id = _clientid;

            //_network.send(_heart);

            //_timer.Elapsed += Tick;
            //_timer.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //private void Tick(object sender, ElapsedEventArgs e)
        //{
        //    RuntimeTypeModel serializer = RuntimeTypeModel.Default;
        //    serializer.Serialize(_io, _heart);
        //}
    }
}
