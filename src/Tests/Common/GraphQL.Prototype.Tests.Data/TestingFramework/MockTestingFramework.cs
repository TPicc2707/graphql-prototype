using NSubstitute;
using NSubstitute.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace GraphQL.Prototype.Tests.Data.TestingFramework
{
    public class MockTestingFramework : IMockNsubstituteMethods, IMockMethods
    {
        public TValue GetObject<TValue>()
        {
            return Arg.Any<TValue>();
        }

        public T InitializeMockedClass<T>(object[] parameters) where T : class
        {
            return Substitute.For<T>(parameters);
        }

        public T RetrieveMockedObject<T>(T obj) where T : class
        {
            return obj;
        }

        public void SetupReturnsNoneResult<T>(T obj, Expression<Func<T, Task>> expression, object[] parameters) where T : class
        {
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, parameters);
        }

        public void SetupReturnsNoneResult<T>(T obj, Expression<Action<T>> expression, object[] parameters) where T : class
        {
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, parameters);
        }

        public void SetupReturnsNoneResult<T>(T obj, Expression<Func<T, Task>> expression) where T : class
        {
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, null);
        }

        public T SetupReturnsResult<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, object[] parameters, TResult result) where T : class
        {
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, parameters).Returns(Task.FromResult(result));
            return obj;
        }

        public T SetupReturnsResult<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, TResult result) where T : class
        {
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, null).Returns(Task.FromResult(result));
            return obj;
        }

        public T SetupThrowsException<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, Exception exception) where T : class
        {
            obj.When<T>((Func<T, Task>)expression.Compile()).Throw(exception);
            return obj;

        }

        public T SetupThrowsException<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, object[] parameters, Exception exception) where T : class
        {
            Exception exception2 = exception;
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, parameters).Returns<object>(delegate
            {
                throw exception2;
            }, Array.Empty<Func<CallInfo, object>>());
            return obj;
        }

        public T SetupThrowsException<T>(T obj, Expression<Func<T, Task>> expression, Exception exception) where T : class
        {
            obj.When<T>(expression.Compile()).Throw(exception);
            return obj;
        }

        public T SetupThrowsException<T>(T obj, Expression<Func<T, Task>> expression, object[] parameters, Exception exception) where T : class
        {
            Exception exception2 = exception;
            MethodInfo method = ((MethodCallExpression)expression.Body).Method;
            method.Invoke(obj, parameters).Returns<object>(delegate
            {
                throw exception2;
            }, Array.Empty<Func<CallInfo, object>>());
            return obj;

        }

        public T SetupThrowsException<T>(T obj, Expression<Action<T>> expression, Exception exception) where T : class
        {
            obj.When<T>(expression.Compile()).Throw(exception);
            return obj;
        }

        public void VerifyMethodRun<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, int times) where T : class
        {
            obj.Received(times).When<T>((Func<T, Task>)expression.Compile());
        }

        public void VerifyMethodRun<T>(T obj, Expression<Func<T, Task>> expression, int times) where T : class
        {
            obj.Received(times).When<T>(expression.Compile());
        }

        public void VerifyMethodRun<T>(T obj, Expression<Action<T>> expression, int times) where T : class
        {
            obj.Received(times).When<T>(expression.Compile());
        }
    }
}
