﻿namespace MyToDo.Api.Parameters
{
    public class QueryParameter
    {
#pragma warning disable
        public int PageIndex {  get; set; }
        public int PageSize { get; set; }
        public string? Search {  get; set; }
    }
}
