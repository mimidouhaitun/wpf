namespace MyToDo.Api.Service
{
	public class ApiResponse<T>
	{
#pragma warning disable
        private string message;
        private bool status;
        private T result;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public T Result
        {
            get { return result; }
            set { result = value; }
        }

        public ApiResponse(bool status, T result)
        {
            this.status = status;
            this.result = result;
        }

        public ApiResponse(bool status, string message)
        {
            this.message = message;
            this.status = status;
        }

    }
    public class ApiResponse
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
		private object result;

		public object Result
		{
			get { return result; }
			set { result = value; }
		}

        public ApiResponse(bool status, object result)
        {
            this.status = status;
            this.result = result;
        }

		public ApiResponse(bool status, string message)
		{
			this.message = message;
			this.status = status;
		}

    }
}
