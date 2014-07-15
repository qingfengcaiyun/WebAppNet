using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Glibs.Util
{
    public static class RegexDo
    {
        //<-----------------------正则表达式判断系列--------------------------->

        /**/
        /// <summary> 
        /// 通用判断 
        /// </summary> 
        /// <param name="reg">string，正则表达式！</param> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsMatchReg(string reg, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否Byte类型（8 位的无符号整数）： 0 和 255 之间的无符号整数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsByte(string str)
        {
            try
            {
                Byte.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否SByte类型（8 位的有符号整数）： -128 到 +127 之间的整数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsSByte(string str)
        {
            try
            {
                SByte.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Int16类型（16 位的有符号整数）： -32768 到 +32767 之间的有符号整数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsInt16(string str)
        {
            try
            {
                Int16.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Int32类型（32 位的有符号整数）：-2,147,483,648 到 +2,147,483,647 之间的有符号整数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsInt32(string str)
        {
            try
            {
                Int32.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Int64类型（64 位的有符号整数）： -9,223,372,036,854,775,808 到 +9,223,372,036,854,775,807 之间的整数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsInt64(string str)
        {
            try
            {
                Int64.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Single类型（单精度（32 位）浮点数字）： -3.402823e38 和 +3.402823e38 之间的单精度 32 位数字 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsSingle(string str)
        {
            try
            {
                Single.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Double类型（双精度（64 位）浮点数字）： -1.79769313486232e308 和 +1.79769313486232e308 之间的双精度 64 位数字 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsDouble(string str)
        {
            try
            {
                Double.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否为布尔型 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsBoolean(string str)
        {
            try
            {
                Boolean.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Char类型（Unicode（16 位）字符）：该 16 位数字的值范围为从十六进制值 0x0000 到 0xFFFF 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsChar(string str)
        {
            try
            {
                Char.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Char类型（96 位十进制值）：从正 79,228,162,514,264,337,593,543,950,335 到负 79,228,162,514,264,337,593,543,950,335 之间的十进制数 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsDecimal(string str)
        {
            try
            {
                Decimal.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否DateTime类型（表示时间上的一刻）： 范围在公元（基督纪元）0001 年 1 月 1 日午夜 12:00:00 到公元 (C.E.) 9999 年 12 月 31 日晚上 11:59:59 之间的日期和时间 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsDateTime(string str)
        {
            try
            {
                DateTime.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否Date类型（表示时间的日期部分）： 范围在公元（基督纪元）0001 年 1 月 1 日 到公元 (C.E.) 9999 年 12 月 31 日之间的日期 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsDate(string str)
        {
            DateTime Value;
            try
            {
                Value = DateTime.Parse(str);
            }
            catch
            {
                return false;
            }

            if (Value.Date.ToString() == str)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**/
        /// <summary> 
        /// 是否Time类型（表示时间部分HHMMSS）： 范围在夜 12:00:00 到晚上 11:59:59 之间的时间 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsTime(string str)
        {
            DateTime Value;
            try
            {
                Value = DateTime.Parse(str);
            }
            catch
            {
                return false;
            }

            if (Value.Year == 1 && Value.Month == 1 && Value.Day == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**/
        /// <summary> 
        /// 是否IPAddress类型（IPv4 的情况下使用以点分隔的四部分表示法格式表示，IPv6 的情况下使用冒号与十六进制格式表示） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsIPAddress(string str)
        {
            try
            {
                System.Net.IPAddress.Parse(str);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**/
        /// <summary> 
        /// 是否中国电话号码类型（XXX/XXXX-XXXXXXX/XXXXXXXX (\d{3,4})-?\d{7,8}）：判断是否是（区号：3或4位）-（电话号码：7或8位） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsChinaPhone(string str)
        {
            string reg = @"(\d{3,4})-?\d{7,8}";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否中国邮政编码（6位数字 \d{6}） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsChinesePostalCode(string str)
        {
            string reg = @"\d{6}";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否中国移动电话号码（13开头的总11位数字 13\d{9}） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsChineseMobile(string str)
        {
            string reg = @"1\d{10}";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否EMail类型（XXX@XXX.XXX \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsEmail(string str)
        {
            string reg = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否Internet URL地址类型（http://） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsURL(string str)
        {
            string reg = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否中文字符（[\u4e00-\u9fa5]） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsChineseWord(string str)
        {
            string reg = @"[\u4e00-\u9fa5]";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否是数字（0到9的数字[\d]+）：不包括符号"."和"-" 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param>
        /// <returns>Boolean</returns> 
        public static bool IsNumber(string str)
        {
            string reg = @"[\d]+";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否只包含数字，英文和下划线（[\w]+） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsStringModel_01(string str)
        {
            string reg = @"[\w]+";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否大写首字母的英文字母（[A-Z][a-z]+） 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsStringModel_02(string str)
        {
            string reg = @"[A-Z][a-z]+";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否全角字符（[^\x00-\xff]）：包括汉字在内 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsWideWord(string str)
        {
            string reg = @"[^\x00-\xff]";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否半角字符（[^\x00-\xff]）：包括汉字在内 
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsNarrowWord(string str)
        {
            string reg = @"[\x00-\xff]";
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(str, reg);
            }
        }

        /**/
        /// <summary> 
        /// 是否合法的中国身份证号码
        /// </summary> 
        /// <param name="str">string，需要验证的字符串！</param> 
        /// <returns>Boolean</returns> 
        public static bool IsChineseID(string str)
        {
            if (str.Length == 15)
            {
                str = CidUpdate(str);
            }
            if (str.Length == 18)
            {
                string strResult = CheckCidInfo(str);
                if (strResult == "非法地区" || strResult == "非法生日" || strResult == "非法证号")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //中国身份证号码验证
        private static string CheckCidInfo(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            string info = string.Empty;
            Regex rg = new Regex(@"^\d{17}(\d|x)$");
            Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return string.Empty;
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "非法地区";
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + " - " + cid.Substring(10, 2) + " - " + cid.Substring(12, 2));
            }
            catch
            {
                return "非法生日";
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return ("非法证号");
            }
            else
            {
                return (aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "男" : "女"));
            }
        }


        //身份证号码15升级为18位
        private static string CidUpdate(string ShortCid)
        {
            char[] strJiaoYan = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            int[] intQuan = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            string strTemp;
            int intTemp = 0;

            strTemp = ShortCid.Substring(0, 6) + "19" + ShortCid.Substring(6);
            for (int i = 0; i <= strTemp.Length - 1; i++)
            {
                intTemp += int.Parse(strTemp.Substring(i, 1)) * intQuan[i];
            }
            intTemp = intTemp % 11;
            return strTemp + strJiaoYan[intTemp];
        }
    }
}