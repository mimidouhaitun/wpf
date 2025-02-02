﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
   public  class ToDoDto: BaseDto
    {	
		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; OnPropertyChanged(); }
        }
		private string content;

		public string Content
		{
			get { return content; }
			set { content = value; OnPropertyChanged(); }
		}
		private StatusEnum status;

		public StatusEnum Status
		{
			get { return status; }
			set { status = value; OnPropertyChanged(); }
		}
	}
}
