using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.DataAccessLayer {
    public static class SqlInjectedValueFormatter {
        public static string ObjectToString(object value){
            if (value == null){
                throw new NullReferenceException("value");
            }

            if (value == DBNull.Value){
                // NULL value, WHERE [NickName] is NULL
                return "NULL";
            }

             if (value is string){
                // Any string to Unicode string
                return StringToString((string) value);
            }

            if (value is char){
                // Any symbol to string
                return CharToString((char) value);
            }

            if (value is Guid){
                // Guid --> string, '{33D69AC8-A114-44A4-A3CB-8E8CCB3E05E4}'
                return GuidToString((Guid) value);
            }

            if (value is bool){
                return BoolToString((bool) value);
            }

            if (value is DateTime){
                return DateTimeToString((DateTime) value);
            }

            if (value is ValueType){
                // uint, ulong, long 
                return value.ToString();
            }

            return null;
        }

        private static string DateTimeToString(DateTime dt) {
            return String.Format("'{0}/{1}/{2} {3}:{4}:{5}'", 
                dt.Month, dt.Day, dt.Year, dt.Hour, dt.Minute, dt.Second);
        }
                                                 
        private static string BoolToString(bool p){
            return (p ? 1 : 0).ToString();
        }

        public static string IntToString(int intValue){
            return intValue.ToString();
        }


        public static string StringToString(string strValue){
            return string.Format("'{0}'", strValue);
        }

        public static string CharToString(char ch){
            return string.Format("'{0}'", ch);
        }

        public static string GuidToString(Guid guid){
            return string.Format("'{0}'", guid);
        }
    }
}
