using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyToDo.Extensions
{
    public class PassWordProp
    {
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PassWordProp),
                new FrameworkPropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        private static void OnPassWordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            var newPassword = e.NewValue as string;
            if (newPassword != passwordBox.Password)
            {
                passwordBox.Password = newPassword;
            }
        }
        /// <summary>
        /// 对应前端的myPwd:PassWordProp.PassWord，
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPassWord(DependencyObject obj)
        {
            return obj.GetValue(PassWordProperty) as string;
        }
        public static void SetPassWord(DependencyObject obj, string val)
        {
            obj.SetValue(PassWordProperty, val);
        }
    }
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox; //这里的PassWordBox不是初始的PassWordBox，相当于临时变量
            string oldPwd = PassWordProp.GetPassWord(passwordBox);
            if (passwordBox != null)
            {
                string newPwd = passwordBox.Password;
                if (oldPwd != newPwd)
                {
                    //passwordBox.Password = newPwd; 会造成死循环，不断的执行该方法，
                    PassWordProp.SetPassWord(passwordBox, newPwd);
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }
}
