using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIForm.Models.Result
{
    public interface IResult
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}
