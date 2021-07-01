﻿using Castle.DynamicProxy;
using Infrastructure.CrossCuttingConcerns.Logging;
using Infrastructure.CrossCuttingConcerns.Logging.Log4Net;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private ILoggerService _loggerService;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.IsAssignableFrom(typeof(ILoggerService)))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerService = (ILoggerService)Activator.CreateInstance(loggerService);
        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _loggerService.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetailWithException;
        }

    }
}
