using System;

namespace network.net
{
    class buffer
    {
        private byte[] _data = null;
        public int read { get; set; }
        public int write { get; set; }

        public buffer(int sz)
        {
            _data = new byte[sz];
        }

        public buffer(byte[] copy, int sz)
        {
            _data = new byte[sz];
            Buffer.BlockCopy(copy, 0, _data, 0, copy.Length);
        }

        public byte[] data()
        {
            return _data;
        }

        public int free()
        {
            return _data.Length - write;
        }

        public int avail()
        {
            return write - read;
        }

        public void reset()
        {
            write = read = 0;
        }

        public int size()
        {
            return _data.Length;
        }

        public void resize(int min)
        {
            int tar = _data.Length * 2;
            while (tar < min)
                tar *= 2;

            try
            {
                Array.Resize<byte>(ref _data, tar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public byte[] get(int sz)
        {
            if (avail() < sz)
                return null;

            byte[] ret = new byte[sz];
            Buffer.BlockCopy(_data, read, ret, 0, sz);
            read += sz;
            if (avail() == 0)
                reset();
            return ret;
        }

        public bool set(byte[] source, int sz)
        {
            if (free() < sz)
            {
                resize(_data.Length + sz);
            }
            Buffer.BlockCopy(source, 0, _data, write, sz);
            write += sz;
            return true;
        }
    }
}
