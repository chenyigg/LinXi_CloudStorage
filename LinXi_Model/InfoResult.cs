using System;
using System.Collections.Generic;
using System.Text;

namespace LinXi_Model
{
    public class InfoResult<T> where T : class
    {
        public InfoResult()
        {
        }

        public InfoResult(T t)
        {
            Entity = t;
        }

        public bool Success { get; set; } = true;
        public int Code { get; set; } = 200;
        public string Msg { get; set; } = "成功";
        public T Entity { get; set; }
    }
}