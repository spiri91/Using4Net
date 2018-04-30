using System;
using System.Threading;

namespace Using4Net
{
    public static class While
    {
        public static void Using(IDisposable[] disposable, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                foreach (var _disposable in disposable)
                    _disposable.Dispose();
            }
        }

        public static void Using(IDisposable[] disposable, Action action, uint retries)
        {
            int _try = 0;

            while (_try < retries)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (_try == retries - 1)
                        throw ex;

                    _try++;
                }
                finally
                {
                    if (_try == retries - 1)
                        foreach (var _disposable in disposable)
                            _disposable.Dispose();
                }
            }
        }

        public static void Using(IDisposable[] disposable, Action action, Action<Exception> onErrorOccurred)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                onErrorOccurred(ex);
            }
            finally
            {
                foreach (var _disposable in disposable)
                    _disposable.Dispose();
            }
        }

        public static void Using(IDisposable[] disposable, Action action, uint retries, TimeSpan waitTimeSpanBetweenTries)
        {
            int _try = 0;
            bool errorOccurred = true;

            while (_try < retries)
            {
                try
                {
                    errorOccurred = true;

                    action();

                    errorOccurred = false;
                }
                catch (Exception ex)
                {
                    if (_try == retries - 1)
                        throw ex;

                    Thread.Sleep(waitTimeSpanBetweenTries);

                    _try++;
                }
                finally
                {
                    if (_try == retries - 1 || false == errorOccurred)
                        foreach (var _disposable in disposable)
                            _disposable.Dispose();
                }
            }
        }
    }
}