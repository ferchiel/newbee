using Assets.script.network;
using Castle.Network;
using proto.c2s;
using ProtoBuf;
using System;
using System.Net.Sockets;

namespace Castle.Network
{
    class Network
    {
        private const int kRecvBufferSize = 1024;
        private TcpClient _client = null;
        private NetworkStream _ns = null;
        private RecvBuffer _buffer = new RecvBuffer(kRecvBufferSize);

        public void init(String ip, int port)
        {
            try
            {
                _client = new TcpClient(ip, port);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("connect okey!");
            Console.WriteLine("ip: {0}, port: {1}", ip, port);
            _ns = _client.GetStream();
            _ns.BeginRead(_buffer.data(), _buffer.write, _buffer.free(), _onReceive, this);
        }

        private void _onReceive(IAsyncResult ar)
        {
            if (!_client.Connected)
            {
                Console.WriteLine("disconnect...");
                return;
            }
            int readCt = _ns.EndRead(ar);
            _buffer.write += readCt;
            if (readCt > 0)
                _buffer.dispatch();
            if (_buffer.free() < 10)
                _buffer.resize(0);
            _ns.BeginRead(_buffer.data(), _buffer.write, _buffer.free(), _onReceive, this);
        }

        public void send(SendBuffer buffer)
        {
            this.send(buffer.data());
        }

        public void send(byte[] d)
        {
            _ns.BeginWrite(d, 0, d.Length, _onSend, this);
        }

        private void _onSend(IAsyncResult ar)
        {
            _ns.EndWrite(ar);
        }
    }
}
