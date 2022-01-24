using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Test
{
    class PortChecker
    {
        public bool Do(string targetName, string nameWantToFind)
        {
            int startSplitIndex;
            foreach (SerialPortInfo info in GetInfo(targetName))
            {
                startSplitIndex = info.Name.IndexOf(" (" + targetName + ")");
                string name = info.Name.Remove(startSplitIndex);
                if (name == nameWantToFind)
                {
                    return true;
                }
            }
            return false;
        }
        private List<SerialPortInfo> GetInfo(string targetName)
        {
            List<SerialPortInfo> ports = null;
            string qry = "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(" + targetName + ")%'";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\CIMV2", qry))
            {
                ports = searcher.Get().Cast<ManagementBaseObject>()
                    .Select(sp => new SerialPortInfo((string)sp["Caption"]))
                    .ToList();
            }

            return ports;
        }
    }
}
