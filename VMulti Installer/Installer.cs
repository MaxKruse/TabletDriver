using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace VMulti_Installer
{
    public partial class Installer
    {
        public void Install(Version ver)
        {
            //var installed = Detect(ver);
            switch (ver)
            {
                case Version.x86:
                    
                    break;
                case Version.x64:

                    break;
                default:
                    throw new ArgumentException("Invalid version");
            }
        }
        
        public void Uninstall(Version ver)
        {
            //var installed = Detect(ver);
            switch (ver)
            {
                case Version.x86:
                    break;
                case Version.x64:
                    break;
                default:
                    throw new ArgumentException("Invalid version");
            }
        }

        public bool Detect(Version ver)
        {
            throw new NotImplementedException();
        }
    }
}
