using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO_WEB.Common
{
    static class CompareStringHelper
    {
        private static readonly string[] VietnameseChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string Convert(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietnameseChar.Length; i++)
            {
                for (int j = 0; j < VietnameseChar[i].Length; j++)
                    str = str.Replace(VietnameseChar[i][j], VietnameseChar[0][i - 1]);
            }
            return str;
        }
    }
}