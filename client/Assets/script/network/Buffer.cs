using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script.network
{
    class Buffer
    {
        private byte[] _data = null;
        private int _read = 0;
        private int _write = 0;
        public int read
        {
            get
            {
                return _read;
            }
            set
            {
                _read = value;
            }
        }
        public int write
        {
            get
            {
                return _write;
            }
            set
            {
                _write = value;
            }
        }

        public Buffer(int sz)
        {
            _data = new byte[sz];
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
            System.Buffer.BlockCopy(_data, read, ret, 0, sz);
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
            System.Buffer.BlockCopy(source, 0, _data, write, sz);
            write += sz;
            return true;
        }
    }
}
