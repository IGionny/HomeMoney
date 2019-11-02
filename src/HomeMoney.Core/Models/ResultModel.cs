using System.Collections.Generic;
using System.Linq;

namespace HomeMoney.Core.Models
{
    public class ResultModel<T>
    {
        public ResultModel()
        {
            this.Messages = new List<ResultMessage>();
        }

        public ResultModel(T value) : this()
        {
            Value = value;
        }


        public T Value { get; set; }

        public IList<ResultMessage> Messages { get; set; }

        public bool IsValid
        {
            get
            {
                if (!Messages.Any()) return true;
                return Messages.All(x => x.Level == ResultMessageLevel.Info);      
            }
        } 

        public ResultModel<T> AddError(string message, string property = null, int code = 0)
        {
            Messages.Add(new ResultMessage(message, property, code));
            return this;
        }

        public ResultModel<T> AddWarning(string message, string property = null, int code = 0)
        {
            Messages.Add(new ResultMessage(message, property, code, ResultMessageLevel.Warning));
            return this;
        }

        public ResultModel<T> AddInfo(string message, string property = null, int code = 0)
        {
            Messages.Add(new ResultMessage(message, property, code, ResultMessageLevel.Info));
            return this;
        }

        public override string ToString()
        {
            return string.Join(", ", Messages.Select(x => x.Message));
        }
    }

    public class ResultMessage
    {
        public ResultMessage()
        {
        }

        public ResultMessage(string message, string property = null, int code = 0,
            ResultMessageLevel level = ResultMessageLevel.Error)
        {
            Message = message;
            Property = property;
            Code = code;
        }

        public ResultMessageLevel Level { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }
        public int Code { get; set; }
    }

    public enum ResultMessageLevel
    {
        /// <summary>
        /// By default is an Error message
        /// </summary>
        Error = 0,
        Warning = 1,
        Info = 2
    }
}