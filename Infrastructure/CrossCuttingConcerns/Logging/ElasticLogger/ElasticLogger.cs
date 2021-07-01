using Infrastructure.Utilities.IoC;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCuttingConcerns.Logging.ElasticLogger
{
    public class ElasticLogger : ILoggerService
    {
        private ILogger _logger;
        public ElasticLogger()
        {
            _logger = (ILogger)ServiceTool.ServiceProvider.GetService(typeof(ILogger));

        }
        public void Info(object logMessage)
        {

            _logger.Information(GetStringFromObject(logMessage));
        }
        private string GetStringFromObject(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public void Debug(object logMessage)
        {

            _logger.Debug(GetStringFromObject(logMessage));
        }

        public void Warn(object logMessage)
        {

            _logger.Warning(GetStringFromObject(logMessage));
        }

        public void Fatal(object logMessage)
        {
            _logger.Fatal(GetStringFromObject(logMessage));
        }

        public void Error(object logMessage)
        {
            _logger.Error(GetStringFromObject(logMessage));
        }
    }
}
