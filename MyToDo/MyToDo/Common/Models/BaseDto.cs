using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    /// <summary>
    /// 方式1：使用 using Prism.Mvvm; +  public class BaseDto:BindableBase + 	set { id = value;RaisePropertyChanged(); }
	/// 方式2：当前使用的方式，wpf自带的方式，前端： Text="{Binding Search, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
    /// </summary>
    public class BaseDto:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string propertyName="")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
        private int id;
		public int Id
		{
			get { return id; }
			set { id = value;OnPropertyChanged(); }
		}
		private DateTime createDate;
		public DateTime CreateDate
		{
			get { return createDate; }
			set { createDate = value; OnPropertyChanged(); }
        }
		private DateTime updateDate;
        public DateTime UpdateDate
		{
			get { return updateDate; }
			set { updateDate = value; OnPropertyChanged(); }
		}
	}
}
