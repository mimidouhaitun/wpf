using MyToDo.Common.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public static class EventAggregatorExtensions
    {
        #region UpdateModel消息
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="updateModel"></param>
        public static void PublishExt(this IEventAggregator eventAggregator, UpdateModel updateModel)
        {
            eventAggregator.GetEvent<UpdateLoadingEvent>().Publish(updateModel);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="action"></param>
        public static void SubscribeExt(this IEventAggregator eventAggregator,Action<UpdateModel> action)
        {
            eventAggregator.GetEvent<UpdateLoadingEvent>().Subscribe(action);
        }
        #endregion

        #region 字符串消息
        /// <summary>
        /// 订阅字符串消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="action"></param>
        public static void SubscribeStr(this IEventAggregator eventAggregator, Action<string> action)
        {
            eventAggregator.GetEvent<MessageEvent>().Subscribe(action);
        }

        /// <summary>
        /// 推送字符串消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="updateModel"></param>
        public static void PublishStr(this IEventAggregator eventAggregator, string updateModel)
        {
            eventAggregator.GetEvent<MessageEvent>().Publish(updateModel);
        }
        #endregion
    }
}
