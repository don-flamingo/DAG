using System;
using System.Collections.Generic;
using System.Text;

namespace DAG.Process
{
    public class Response
    {
        public int Code { get; set; }
        public string Stdout { get; set; }
        public string Stderr { get; set; }
    }
}
