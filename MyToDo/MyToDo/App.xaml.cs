﻿using DryIoc;
using MyToDo.Service;
using MyToDo.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView>();
            containerRegistry.RegisterForNavigation<ToDoView>();
            containerRegistry.RegisterForNavigation<MemoView>();
            containerRegistry.RegisterForNavigation<SettingsView>();
            containerRegistry.RegisterForNavigation<SkinView>();
            containerRegistry.RegisterForNavigation<AboutView>();

            //这种注入方式是在MyHttpRestClient中有带常数参数Url的构造函数，需要将常数传入。
            //这种注入方式比较麻烦，采用后面一种仅注入类
            //containerRegistry.GetContainer().Register<MyHttpRestClient>(
            //    made:DryIoc.Parameters.Of.Type<string>(serviceKey: "webUrl"));
            //containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5057/", serviceKey: "webUrl");

            containerRegistry.GetContainer().Register<MyHttpRestClient>();

            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();

        }
    }
}
