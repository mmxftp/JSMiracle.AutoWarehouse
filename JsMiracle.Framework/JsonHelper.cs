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
            this.ContentType = TextContextType;
            base.ExecuteResult(context);

            //if (context == null)
            //{
            //    throw new ArgumentNullException("context");
            //}

            //var response = context.HttpContext.Response;
            //response.ContentType = ContentType;

            //StringWriter sw = new StringWriter();
            //var serializer = JsonSerializer.Create(
            //    new JsonSerializerSettings
            //    {
            //        Converters = new JsonConverter[] { new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter() },
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //        NullValueHandling = NullValueHandling.Ignore,
            //        //ContractResolver = new NHibernateContractResolver(ExceptMemberName)                    
            //    }
            //    );


            //using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            //{
            //    jsonWriter.Formatting = Formatting.Indented;
            //    serializer.Serialize(jsonWriter, Data);
            //}
            //response.Write(sw.ToString());
     
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class FormatJsonExtension
    {

        public static FormatJsonResult JsonFormat(this Controller c
            , object data
            ,JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            FormatJsonResult result = new FormatJsonResult();
            result.Data = data;
            result.JsonRequestBehavior = behavior;
            return result;
        }

    }
}
