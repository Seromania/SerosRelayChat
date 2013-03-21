using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerosRelayChatServer
{
    class Protocol
    {
        public String vonUser;
        public String Command;
        public String[] Arg;

        public Protocol()
        {
            this.vonUser = "";
            this.Command = "";
            this.Arg = new String[512];
        }

        public Protocol(byte[] data)
        {
            try
            {
                this.vonUser = Encoding.UTF8.GetString(data[0]);
                this.Command = data[1].ToString();
                this.Arg = new String[512];
                for (int i = 0; i < 512; i++)
                {
                    if (data[i + 2].ToString() != "\r\n")
                        Arg[i] = data[i + 2].ToString();
                    else
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Protocoll!");
            }
        }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            
            result.AddRange(Encoding.UTF8.GetBytes(vonUser));
            result.AddRange(Encoding.UTF8.GetBytes(" "));
            result.AddRange(Encoding.UTF8.GetBytes(Command));
            result.AddRange(Encoding.UTF8.GetBytes(" "));
            foreach (String value in Arg)
            {
                result.AddRange(Encoding.UTF8.GetBytes(value));
                result.AddRange(Encoding.UTF8.GetBytes(" "));
            }
            result.AddRange(Encoding.UTF8.GetBytes("\r\n"));

            return result.ToArray();
            
        }
    }
}
