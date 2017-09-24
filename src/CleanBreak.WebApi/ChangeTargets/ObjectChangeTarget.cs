using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CleanBreak.WebApi.Changes;
using CleanBreak.WebApi.Core;

namespace CleanBreak.WebApi.ChangeTargets
{
	public class ObjectChangeTarget : IChangeTarget
	{
		public Type ClassType { get; set; }

		public bool IsMap(ApiRequest request, TargetContext context)
		{
			var mappings = getMappings(context);
			var requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(request.Method.Method, request.Url,
				context.HttpConfiguration);
			string parametersTypes = String.Join(",", requestHandler.ActionParatemersTypes.Select(p => p.AssemblyQualifiedName));
			return mappings.Any(m => m.ControllerType == requestHandler.ControllerType.AssemblyQualifiedName
															&& m.ActionName == requestHandler.ActionName
															&& m.Parameters == parametersTypes
															&& m.IsReturnType == false);
		}



		public bool IsMap(ApiResponse response, TargetContext context)
		{
			var mappings = getMappings(context);
			var requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(response.RequestMethod.Method, response.RequestUrl,
				context.HttpConfiguration);
			string parametersTypes = String.Join(",", requestHandler.ActionParatemersTypes.Select(p => p.AssemblyQualifiedName));
			return mappings.Any(m => m.ControllerType == requestHandler.ControllerType.AssemblyQualifiedName
															&& m.ActionName == requestHandler.ActionName
															&& m.Parameters == parametersTypes
															&& m.IsReturnType);
		}

		private IEnumerable<Mapper> getMappings(TargetContext context)
		{
			string key = $"{GetType().AssemblyQualifiedName}_{ClassType.AssemblyQualifiedName}";
			var mappings = context.Cache.Get<Mapper[]>(key);
			if (mappings != null)
			{
				return mappings;
			}
			var config = context.HttpConfiguration;
			var controllersDescriptoes = config.Services.GetHttpControllerSelector().GetControllerMapping().Values;

			HashSet<Mapper> mappers = new HashSet<Mapper>();
			foreach (var controllerDescriptor in controllersDescriptoes)
			{
				var actions = config.Services.GetActionSelector().GetActionMapping(controllerDescriptor);
				foreach (var action in actions)
				{
					var actionDescr = actions[action.Key];
					foreach (var act in actionDescr)
					{
						if (hasType(act.ReturnType))
						{
							mappers.Add(new Mapper()
							{
								ControllerType = controllerDescriptor.ControllerType.AssemblyQualifiedName,
								IsReturnType = true,
								Parameters = String.Join(",", act.GetParameters().Select(p => p.ParameterType.AssemblyQualifiedName)),
								ActionName = act.ActionName
							});
						}
						foreach (var actParatemer in act.GetParameters())
						{
							if (hasType(actParatemer.ParameterType))
							{
								mappers.Add(new Mapper()
								{
									ControllerType = controllerDescriptor.ControllerType.AssemblyQualifiedName,
									IsReturnType = false,
									Parameters = String.Join(",", act.GetParameters().Select(p => p.ParameterType.AssemblyQualifiedName)),
									ActionName = act.ActionName
								});
							}
						}
					}
				}
			}
			context.Cache.Set(key, mappers.ToArray());
			return mappers;
		}

		private bool hasType(Type type)
		{
			if (type == null)
			{
				return false;
			}
			return type == ClassType
						|| (type.IsArray && type.GetElementType() == ClassType)
						||
						(type.IsGenericType && type.GetGenericTypeDefinition() == typeof (IEnumerable<>) &&
						type.GenericTypeArguments[0] == ClassType);

		}
	}

	public struct Mapper
	{
		public string ControllerType { get; set; }
		public bool IsReturnType { get; set; }
		public string ActionName { get; set; }
		public string Parameters { get; set; }
	}
}