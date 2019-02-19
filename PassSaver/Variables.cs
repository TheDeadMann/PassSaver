using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassSaver
{
    class Variables
    {
        public static string Version = "1.0.0";

        public static string Logo = @"
______              _____                      
| ___ \            /  ___|                     
| |_/ /_ _ ___ ___ \ `--.  __ ___   _____ _ __ 
|  __/ _` / __/ __| `--. \/ _` \ \ / / _ \ '__|
| | | (_| \__ \__ \/\__/ / (_| |\ V /  __/ |   
\_|  \__,_|___/___/\____/ \__,_| \_/ \___|_|   ";

        public static char[] chars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string done = @"
______                 
|  _  \                
| | | |___  _ __   ___ 
| | | / _ \| '_ \ / _ \
| |/ / (_) | | | |  __/
|___/ \___/|_| |_|\___|";

        public static string error = @"
 _____                    
|  ___|                   
| |__ _ __ _ __ ___  _ __ 
|  __| '__| '__/ _ \| '__|
| |__| |  | | | (_) | |   
\____/_|  |_|  \___/|_|";

        public static string[] fileArray = Directory.GetFiles(@"..\..\PS\");
    }
}
