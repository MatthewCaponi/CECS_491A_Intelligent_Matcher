﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayerUnitTests
{
    public static class LogTargetHelper
    {
        public static string ReadTestLog(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    
                }

                return s;
            }
        }
    }
}
