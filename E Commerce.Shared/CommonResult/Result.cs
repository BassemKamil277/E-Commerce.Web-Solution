using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Result
    {
        private readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count == 0;

        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors;

        #region usig CTOR
        // is success 
        protected Result()
        {

        }

        // have error 
        protected Result(Error error)
        {
            _errors.Add(error);
        }

        // have Errors 
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        #endregion

        #region Using Functions

        public static Result Ok() => new Result();

        public static Result Fail(Error error) => new Result(error);

        public static Result Fail(List<Error> errors) => new Result(errors);
        #endregion
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Can't Access The Value");

        // Ok 
        private Result(TValue value) : base() 
        {
            _value = value;
        }

        // have Error 
        private Result(Error error) : base(error)
        {
            _value = default!;
        }

        // have errors 
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static new Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static new Result<TValue> Fail(List<Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);

    }
}
