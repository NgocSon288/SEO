using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SEO_WEB.Common
{
    public static class Constants
    {
        public static readonly string SESSION = "SESSION"; 
        public static readonly char END_CHAR = '|';
        public static readonly char BETWEEN_CHAR = '~';



        public static void DeleteFile(string path)
        { 
            FileInfo myfileinf = new FileInfo(path);
            myfileinf.Delete();
        }
    }
}