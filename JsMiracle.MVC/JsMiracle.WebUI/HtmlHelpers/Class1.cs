//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;

//namespace JsMiracle.WebUI.HtmlHelpers
//{
//    public static class Class1
//    {
//        public static MvcHtmlString TextBoxFor123<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
//        {
//            string format = null;
//            return htmlHelper.TextBoxFor123<TModel, TProperty>(expression, format,null);
//        }

//        public static MvcHtmlString TextBoxFor123<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
//        {
//            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
//            return htmlHelper.TextBoxHelper(metadata, metadata.Model, ExpressionHelper.GetExpressionText(expression), format, htmlAttributes);
//        }

//        private static MvcHtmlString TextBoxHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
//        {
//            bool useViewData = false;
//            bool isChecked = false;
//            bool setId = true;
//            bool isExplicitValue = true;
//            string str = format;
//            IDictionary<string, object> dictionary = htmlAttributes;
//            return InputHelper(htmlHelper, InputType.Text, metadata, expression, model, useViewData, isChecked, setId, isExplicitValue, str, dictionary);
//        }

//        private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
//        {
//            ModelState state;
//            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
//            if (string.IsNullOrEmpty(fullHtmlFieldName))
//            {
//                throw new ArgumentException("name");
//            }
//            TagBuilder tagBuilder = new TagBuilder("input");
//            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
//            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
//            tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
//            string str2 = htmlHelper.FormatValue(value, format);
//            bool flag = false;

//            //ModelState state;
//            //if (this.ViewData.ModelState.TryGetValue(key, out state) && (state.Value != null))
//            //{
//            //    return state.Value.ConvertTo(destinationType, null);
//            //}
       

//            switch (inputType)
//            {
//                case InputType.CheckBox:
//                    {
//                        bool? modelStateValue = GetModelStateValue(htmlHelper,fullHtmlFieldName, typeof(bool)) as bool?;
//                        if (modelStateValue.HasValue)
//                        {
//                            isChecked = modelStateValue.Value;
//                            flag = true;
//                        }
//                        break;
//                    }
//                case InputType.Password:
//                    if (value != null)
//                    {
//                        tagBuilder.MergeAttribute("value", str2, isExplicitValue);
//                    }
//                    goto Label_016C;

//                case InputType.Radio:
//                    break;

//                default:
//                    {
//                        string str4 = (string)GetModelStateValue(htmlHelper,fullHtmlFieldName, typeof(string));
//                        tagBuilder.MergeAttribute("value", str4 ?? (useViewData ? htmlHelper.EvalString(fullHtmlFieldName, format) : str2), isExplicitValue);
//                        goto Label_016C;
//                    }
//            }
//            if (!flag)
//            {
//                string a = GetModelStateValue(htmlHelper,fullHtmlFieldName, typeof(string)) as string;
//                if (a != null)
//                {
//                    isChecked = string.Equals(a, str2, StringComparison.Ordinal);
//                    flag = true;
//                }
//            }
//            if (!flag && useViewData)
//            {
//                isChecked = EvalBoolean(htmlHelper,fullHtmlFieldName);
//            }
//            if (isChecked)
//            {
//                tagBuilder.MergeAttribute("checked", "checked");
//            }
//            tagBuilder.MergeAttribute("value", str2, isExplicitValue);
//        Label_016C:
//            if (setId)
//            {
//                tagBuilder.GenerateId(fullHtmlFieldName);
//            }
//            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Errors.Count > 0))
//            {
//                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
//            }
//            tagBuilder.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
//            if (inputType == InputType.CheckBox)
//            {
//                StringBuilder builder2 = new StringBuilder();
//                builder2.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));
//                TagBuilder builder3 = new TagBuilder("input");
//                builder3.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
//                builder3.MergeAttribute("name", fullHtmlFieldName);
//                builder3.MergeAttribute("value", "false");
//                builder2.Append(builder3.ToString(TagRenderMode.SelfClosing));
//                return MvcHtmlString.Create(builder2.ToString());
//            }
//            return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
//            //var mvcstr = new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
//            //return mvcstr;
//        }


//        internal static object GetModelStateValue( HtmlHelper htmlHelper,string key, Type destinationType)
//        {
//            ModelState state;
//            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state) && (state.Value != null))
//            {
//                return state.Value.ConvertTo(destinationType, null);
//            }
//            return null;
//        }


//        internal static bool EvalBoolean( HtmlHelper htmlHelper,string key)
//        {
//            return Convert.ToBoolean(htmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
//        }

//        internal static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
//        {
//            return new MvcHtmlString(tagBuilder.ToString(renderMode));
//        }

//        internal static string EvalString(this HtmlHelper htmlHelper, string key, string format)
//        {
//            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
//        }

//    }
//}