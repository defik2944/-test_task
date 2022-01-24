using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Test
{
    class ActivePortsFinder
    {
        public List<string> Find()
        {
            List<string> activePorts = new List<string>();

            foreach (string s in SerialPort.GetPortNames())
            {
                var port = new SerialPort(s);
                try
                {
                    port.Open();
                    port.Close();
                }
                catch (Exception ex)
                {
                    activePorts.Add(s);
                }
            }

            return activePorts;
        }
    }
}
