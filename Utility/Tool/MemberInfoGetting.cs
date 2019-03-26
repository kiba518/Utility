using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class MemberInfoGetting
    {
        //举例
        //string TableName = "123";
        //string nameOfTestVariable = MemberInfoGetting.GetMemberName(() => TableName);
        //最后得到 nameOfTestVariable  = "TableName"
        /// <summary>
        /// 获取变量名
        /// </summary>
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        //获取实体类里面所有的名称、值、DESCRIPTION值  
        public static string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name; //名称  
                object value = item.GetValue(t, null);  //值  
                string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;// 属性值  

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1}:{2},", name, value, des);
                }
                else
                {
                    getProperties(value);
                }
            }
            return tStr;
        }

        

    }
}
