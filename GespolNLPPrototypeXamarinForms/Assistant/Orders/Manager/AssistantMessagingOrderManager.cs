using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Orders.Manager
{
    public class AssistantMessagingOrderManager
    {
        private readonly Dictionary<string,Action<string>> _orderDictionary = new Dictionary<string, Action<string>>()
        {
            {AssistantMessagingOrder.CancelSearch,s => new CancelSearchOrder().Execute() },
            {AssistantMessagingOrder.ExecuteCreateCommand,selectedModuleString => new ExecuteCreateCommand(selectedModuleString).Execute() },
        };
        public AssistantMessagingOrderManager()
        {
            MessagingCenter.Subscribe<string>(this,AssistantMessagingOrder.CancelSearch,_orderDictionary[AssistantMessagingOrder.CancelSearch].Invoke);
            MessagingCenter.Subscribe<string>(this,AssistantMessagingOrder.ExecuteCreateCommand,_orderDictionary[AssistantMessagingOrder.ExecuteCreateCommand].Invoke);
        }

        ~AssistantMessagingOrderManager()
        {
            foreach (var key in _orderDictionary.Keys)
            {
                MessagingCenter.Unsubscribe<string>(this,key);
            }
        }
        
    }
}