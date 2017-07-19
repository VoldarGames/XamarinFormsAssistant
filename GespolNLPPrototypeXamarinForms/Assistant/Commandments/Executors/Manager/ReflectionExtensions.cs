using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XamarinFormsAssistant.Assistant.Attributes;
using XamarinFormsAssistant.Assistant.SpeechNames.Manager;

namespace XamarinFormsAssistant.Assistant.Commandments.Executors.Manager
{
    public static class ReflectionExtensions
    {

        public static object GetChildrenValue(this PropertyInfo propertyInfo)
        {
            var currentInstanceResult = Assistant.GetInstance().CurrentInstanceResult;

            if (currentInstanceResult
                .GetType()
                .GetProperties()
                .FirstOrDefault(info => info.Equals(propertyInfo)) != null)
            {
                return propertyInfo.GetValue(currentInstanceResult);
            }
            return GetChildrenValueInternal(propertyInfo, currentInstanceResult);
           
        }

        private static object GetChildrenValueInternal(PropertyInfo propertyInfo, object currentInstanceResult)
        {
            foreach (var navigationProperty in currentInstanceResult.GetType().GetProperties().Where(info => info.GetCustomAttribute<AssistantRequiredNavigationField>() != null))
            {
                var targetProperty =
                    navigationProperty.PropertyType.GetProperties()
                        .FirstOrDefault(info => info.Equals(propertyInfo));
                if (targetProperty == null) continue;

                var targetInstance = currentInstanceResult.GetType()
                    .GetProperty(navigationProperty.Name)
                    .GetValue(currentInstanceResult);

                if (targetProperty.GetCustomAttribute<AssistantRequiredNavigationField>() != null)
                {
                    if (propertyInfo.GetCustomAttribute<AssistantRequiredNavigationField>() == null)
                    {
                        return GetChildrenValueInternal(propertyInfo,targetInstance);
                    }
                }

                return propertyInfo.GetValue(targetInstance);

            }
            return null;
        }

        public static void SetChildrenValue(this PropertyInfo propertyInfo, object value)
        {
            var currentInstanceResult = Assistant.GetInstance().CurrentInstanceResult;

            if (currentInstanceResult
                .GetType()
                .GetProperties()
                .FirstOrDefault(info => info.Equals(propertyInfo)) != null)
            {
                propertyInfo.SetValue(currentInstanceResult, value);
            }
            else
            {
                SetChildrenValueInternal(propertyInfo, value, currentInstanceResult);
            }
        }

        private static void SetChildrenValueInternal(PropertyInfo propertyInfo, object value, object currentInstanceResult)
        {
            foreach (var navigationProperty in currentInstanceResult.GetType().GetProperties().Where(info => info.GetCustomAttribute<AssistantRequiredNavigationField>() != null))
            {
                var targetProperty =
                    navigationProperty.PropertyType.GetProperties()
                        .FirstOrDefault(info => info.Equals(propertyInfo));
                if (targetProperty == null) continue;

                var targetInstance = currentInstanceResult.GetType()
                    .GetProperty(navigationProperty.Name)
                    .GetValue(currentInstanceResult);

                if (targetProperty.GetCustomAttribute<AssistantRequiredNavigationField>() != null)
                {
                    if (propertyInfo.GetCustomAttribute<AssistantRequiredNavigationField>() == null)
                    {
                        SetChildrenValueInternal(propertyInfo, value, targetInstance);
                    }
                }

                targetProperty.SetValue(targetInstance, value);

            }
        }


        public static PropertyInfo GetPropertyInChildrenWithAttribute<TAttribute>(this Type type, string propertyName)
            where TAttribute : Attribute
        {
            var propertyInfoList = new List<PropertyInfo>();
            GetAllRequiredFields(type, ref propertyInfoList);
            return propertyInfoList
                .FirstOrDefault(info => info.GetCustomAttribute<TAttribute>() != null
                                        &&
                                        AssistantSpeechNamesManager.ExistsSpeechName(
                                            Assistant.GetInstance().CurrentContextType, propertyName.ToUpper())
                                        &&
                                        AssistantSpeechNamesManager.GetKey(propertyName).ToUpper()
                                            .Equals(info.Name.ToUpper()));
        }

        private static void GetAllRequiredFields(Type currentContextType, ref List<PropertyInfo> propertyInfoList )
        {
            propertyInfoList.AddRange(currentContextType.GetProperties().Where(info => info.GetCustomAttribute<AssistantRequiredFieldAttribute>() != null));
            foreach (var navigationProperty in currentContextType.GetProperties().Where(info => info.GetCustomAttribute<AssistantRequiredNavigationField>() != null))
            {
                GetAllRequiredFields(navigationProperty.PropertyType,ref propertyInfoList);
            }
        }
    }
}