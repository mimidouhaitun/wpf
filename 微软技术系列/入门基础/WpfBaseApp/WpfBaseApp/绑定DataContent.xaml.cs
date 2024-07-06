using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBaseApp
{
    /// <summary>
    /// 绑定DataContent.xaml 的交互逻辑
    /// </summary>
    public partial class 绑定DataContent : Window
    {
        public 绑定DataContent()
        {
            InitializeComponent();
            Test test = new Test();
            test.className = "高二三班";
            test.myName = "黄海荣";
            List<Student> students= new List<Student>();
            students.Add(new Student() { Name = "张三", Age = 20, Sex = "男" });
            students.Add(new Student() { Name = "李四", Age = 30, Sex = "女" });
            students.Add(new Student() { Name = "王五", Age = 40, Sex = "男" });
            test.students=students;
            this.DataContext = test;

        }
    }
    public class Test
    {
#pragma warning disable
        public Test()
        {

        }
        public string myName { get; set; }
        public string className { get; set; }
        public List<Student> students { get; set; }
    }
    public class Student
    {
        public string Name { get;set; }
        public int Age { get;set; }
        public string Sex { get;set; }
    }
}
