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
                this.Arg = new String[512];
                String completeStr = Encoding.UTF8.GetString(data);
                String[] splitStr = completeStr.Split(' ');
                int n = 0;
                foreach (String s in splitStr)
                {
                    if (n == 0)
                    {
                        this.vonUser = s;
                        n++;
                        continue;
                    }
                    if (n == 1)
                    {
                        this.Command = s;
                        n++;
                        continue;
                    }
                    this.Arg[n - 2] = s;
                    n++;
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
                result.AddRange(Encoding.UTF8.GetBytes(" "));
                result.AddRange(Encoding.UTF8.GetBytes(value));
            }
            result.AddRange(Encoding.UTF8.GetBytes(" "));
            result.AddRange(Encoding.UTF8.GetBytes("\r\n"));

            return result.ToArray();
            
        }
    }
}
