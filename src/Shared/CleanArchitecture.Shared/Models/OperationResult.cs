﻿using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.Models
{

    public class OperationResult(bool isSuccess, string message, OperationStatusCode status)
    {
        public bool IsSuccess { get; set; } = isSuccess;
        public string Message { get; set; } = message;
        public OperationStatusCode Status { get; set; } = status;


        public static OperationResult NotFound(string message)
          => new(true, message, OperationStatusCode.NotFound);

        public static OperationResult NotFound()
            => new(true, OperationStatusCode.NotFound.ToDisplay(), OperationStatusCode.NotFound);

        public static OperationResult Fail()
            => new(false, OperationStatusCode.ServerError.ToDisplay(), OperationStatusCode.ServerError);

        public static OperationResult Fail(string message)
            => new(false, message, OperationStatusCode.ServerError);

        public static OperationResult Fail(string message, OperationStatusCode statusCode)
            => new(false, message, statusCode);

        public static OperationResult Success()
            => new(true, OperationStatusCode.OK.ToDisplay(), OperationStatusCode.OK);

        public static OperationResult Success(string message)
            => new(true, message, OperationStatusCode.OK);
    }

    public class OperationResult<TData>
        (bool isSuccess, string message, OperationStatusCode status, TData? data)
        : OperationResult(isSuccess, message, status)
    {
        public TData? Result { get; set; } = data;

        public static OperationResult<TData> Success(TData data)
            => new(true, OperationStatusCode.OK.ToDisplay(), OperationStatusCode.OK, data);

        public static OperationResult<TData> Fail(TData data = default)
            => new(false, OperationStatusCode.ServerError.ToDisplay(), OperationStatusCode.ServerError, data);

        public static OperationResult<TData> Fail(string message, TData data = default)
            => new(false, message, OperationStatusCode.ServerError, data);

        public static new OperationResult<TData> Fail(string message)
            => new(false, message, OperationStatusCode.ServerError, default);
    }
}
