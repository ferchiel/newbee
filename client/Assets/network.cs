using network.net;
using System;
using System.Net.Sockets;

namespace network
{
    class network
    {
        private TcpClient _client = null;
        private NetworkStream _ns = null;
        private buffer _buf = new buffer(512);

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
            Console.WriteLine("connect ok!");
            Console.WriteLine("ip: {0}, port: {1}", ip, port);
            _ns = _client.GetStream();
            _ns.BeginRead(_buf.data(), _buf.write, _buf.free(), _onReceive, this);
        }

        private void _onReceive(IAsyncResult ar)
        {
            if (!_client.Connected)
            {
                Console.WriteLine("disconnect...");
                return;
            }
            int readCt = _ns.EndRead(ar);
            _buf.write += readCt;
            if (_buf.free() < 10)
                _buf.resize(0);
            _ns.BeginRead(_buf.data(), _buf.write, _buf.free(), _onReceive, this);
        }

        public void send(byte[] d)
        {
            //_ns.Write(d, 0, d.Length);
            //Console.WriteLine(Encoding.ASCII.GetString(d));
            _ns.BeginWrite(d, 0, d.Length, _onSend, this);
        }

        public void send(buffer b)
        {
            _ns.BeginWrite(b.data(), b.read, b.avail(), _onSend, this);
            b.reset();
        }

        private void _onSend(IAsyncResult ar)
        {
            Console.WriteLine("on send...");
            _ns.EndWrite(ar);
            //_ns.
        }
    }
}
