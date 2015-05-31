using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.UI.Fluent;

namespace WorkshopExpert.Web.Common.Telerik.Extensions
{
	public static class TelerikGridExtensions
	{
        /// <summary>
        /// http://www.codeproject.com/Articles/228870/Telerik-MVC-Grid-ActionLink-column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="linkText"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
		public static GridTemplateColumnBuilder<T> ActionLink<T>(this GridColumnFactory<T> factory, 
            string linkText, string action, Expression<Func<T, object>> routeValues)
			where T : class
		{
			return ActionLink(factory, linkText, action, string.Empty, routeValues);
		}

		/// <span class="code-SummaryComment"><summary>
        /// Renders action links templates for both, server side and client side
		/// <span class="code-SummaryComment"></summary>	
        public static GridTemplateColumnBuilder<T> ActionLink<T>(this GridColumnFactory<T> factory, string linkText, string action, string controller, Expression<Func<T, object>> routeValues)
			where T: class
		{
			if (string.IsNullOrEmpty(controller))
				controller = factory.Container.ViewContext.Controller.GetType().Name.Replace("Controller", "");

			var urlHelper = new UrlHelper(factory.Container.ViewContext.RequestContext);

			var builder = factory.Template(x =>
			{
				var actionUrl = urlHelper.Action(action, controller, routeValues.Compile().Invoke(x));
				return string.Format(@"<a href=""{0}"">{1}</a>", actionUrl, linkText);
			});

			if (!(routeValues.Body is NewExpression))
				throw new ArgumentException("routeValues.Body must be a NewExpression");

			RouteValueDictionary routeValueDictionary = ExtractClientTemplateRouteValues(((NewExpression)routeValues.Body));

			var link = urlHelper.Action(action, controller, routeValueDictionary);
            var clientTemplate = string.Format(@"<a class= ""k-button k-button-icontext"" href=""{0}"">{2}{1}</a>", link, linkText, @"<span class=""k-icon k-edit""></span>");

			return builder.ClientTemplate(clientTemplate);
		}

		private static RouteValueDictionary ExtractClientTemplateRouteValues(NewExpression newExpression)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();

			for (int index = 0; index < newExpression.Arguments.Count; index++)
			{
				var argument = newExpression.Arguments[index];
				var member = newExpression.Members[index];

				object value;

				switch (argument.NodeType)
				{
					case ExpressionType.Constant:
						value = ((ConstantExpression) argument).Value;
						break;

					case ExpressionType.MemberAccess:
						MemberExpression memberExpression = (MemberExpression) argument;

						if (memberExpression.Expression is ParameterExpression)
							value = string.Format("#= {0} #", memberExpression.Member.Name);
						else
							value = GetValue(memberExpression);

						break;

					default:
						throw new InvalidOperationException("Unknown expression type!");
				}

				routeValueDictionary.Add(member.Name, value );
			}
			return routeValueDictionary;
		}

		private static object GetValue(MemberExpression member)
		{
			var objectMember = Expression.Convert(member, typeof(object));
			var getterLambda = Expression.Lambda<Func<object>>(objectMember);
			return getterLambda.Compile().Invoke();
		}

	}
}