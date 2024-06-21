using System.Linq.Expressions;

namespace GraphQL.Prototype.Tests.Data.TestingFramework
{
    public interface IMockNsubstituteMethods : IMockMethods
    {
        T InitializeMockedClass<T>(object[] parameters) where T : class;
        T RetrieveMockedObject<T>(T obj) where T : class;
        T SetupReturnsResult<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, object[] parameters, TResult result) where T : class;
        T SetupReturnsResult<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, TResult result) where T : class;
        void SetupReturnsNoneResult<T>(T obj, Expression<Func<T, Task>> expression, object[] parameters) where T : class;
        void SetupReturnsNoneResult<T>(T obj, Expression<Action<T>> expression, object[] parameters) where T : class;
        void SetupReturnsNoneResult<T>(T obj, Expression<Func<T, Task>> expression) where T : class;
        T SetupThrowsException<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, Exception exception) where T : class;
        T SetupThrowsException<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, object[] parameters, Exception exception) where T : class;
        T SetupThrowsException<T>(T obj, Expression<Func<T, Task>> expression, Exception exception) where T : class;
        T SetupThrowsException<T>(T obj, Expression<Func<T, Task>> expression, object[] parameters, Exception exception) where T : class;
        T SetupThrowsException<T>(T obj, Expression<Action<T>> expression, Exception exception) where T : class;
        void VerifyMethodRun<T, TResult>(T obj, Expression<Func<T, Task<TResult>>> expression, int times) where T : class;
        void VerifyMethodRun<T>(T obj, Expression<Func<T, Task>> expression, int times) where T : class;
        void VerifyMethodRun<T>(T obj, Expression<Action<T>> expression, int times) where T : class;

    }
}
