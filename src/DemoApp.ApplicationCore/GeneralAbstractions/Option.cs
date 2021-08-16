using System;

namespace DemoApp.ApplicationCore.GeneralAbstractions
{
    /// <summary>
    /// Quick and dirty implementation of Result a la Rust
    /// Normally best to use language extention option, or OneOf library
    /// </summary>
    public sealed class Option<TValidResult, TFailure>
    {
        private readonly TValidResult _result = default;
        private readonly TFailure _status = default;
        private readonly bool _isValidResult = false;
        public Option(TValidResult result) 
            => (_result, _isValidResult) = (result, true);

        public Option(TFailure error) 
            => (_status, _isValidResult) = (error, false);

        public T Extract<T>(Func<TValidResult, T> validResult, Func<TFailure, T> errored) 
            => _isValidResult ? validResult(this._result) : errored(this._status);
    }
}
