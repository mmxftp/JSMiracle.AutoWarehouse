using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JsMiracle.Framework
{
    public class FormatJsonResult : JsonResult
    {
        // 为了兼容IE ,否则IE会认为Json为需下载内容
        public const string TextContextType = "text/html";
        //public const string ContextType = "application/json";

        public override void ExecuteResult(ControllerContext context)
        {
            //this.ContentType = TextContextType;
            ////JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            ////jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            ////return JsonConvert.SerializeObject(obj, jsSettings);
            //base.ExecuteResult(context);

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;
            response.ContentType = TextContextType;

            StringWriter sw = new StringWriter();
            var serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    //Converters = new JsonConverter[] { new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter() },
                    Converters = new JsonConverter[] { new Newtonsoft.Json.Converters.IsoDateTimeConverter() },
                    //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,       // 外键递归死循环跳过, 数据量一多会非常慢
                    ReferenceLoopHandling = ReferenceLoopHandling.Error,
                    NullValueHandling = NullValueHandling.Ignore,
                    //ContractResolver = new NHibernateContractResolver(ExceptMemberName)                    
                }
                );

#if DEBUG
            try
            {
#endif
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, Data);
                }
#if DEBUG
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
#endif

            response.Write(sw.ToString());

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class FormatJsonExtension
    {

        public static FormatJsonResult JsonFormat(this Controller c
            , object data
            , JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            FormatJsonResult result = new FormatJsonResult();
            result.Data = data;
            result.JsonRequestBehavior = behavior;
            return result;
        }

    }
}
