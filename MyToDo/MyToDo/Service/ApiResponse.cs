using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class ApiResponse
    {
        public string Message {  get; set; }
        public bool Status {  get; set; }
        public object Result {  get; set; }
    }
    public class ApiResponse<T>
    {
#pragma warning disable
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
        private T result;

        public T Result
        {
            get { return result; }
            set { result = value; }
        }
 
    }
}
