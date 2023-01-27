using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Scripts.SimpleFrame.Exceptions
{
    /// <summary>
    /// Json解析异常类,专门负责对于Json路径错误或者json格式错误进行捕获。
    /// </summary>
    internal class JsonAnalysisException:Exception
    {
        public JsonAnalysisException():base() { }
        public JsonAnalysisException(string exceptionMessage):base(exceptionMessage) { }
    }
}
