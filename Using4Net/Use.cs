using System;
using System.Threading;

namespace Using4Net
{
    public static class While
    {
        public static void Using(IDisposable[] disposable, Action action, Action<Exception> onErrorOccurred = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                onErrorOccurred?.Invoke(ex);
            }
            finally
            {
                foreach (var _disposable in disposable)
                    if (null != _disposable)
                        _disposable.Dispose();
            }
        }

        public static void Using(IDisposable[] disposable, Action action, uint retries, Action<Exception> onErrorOccurred = null)
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
                        onErrorOccurred?.Invoke(ex);
                }
                finally
                {
                    if (_try == retries - 1)
                        foreach (var _disposable in disposable)
                            if (null != _disposable)
                                _disposable.Dispose();

                    _try++;
                }
            }
        }

        public static void Using(IDisposable[] disposable, Action action, uint retries, TimeSpan waitTimeSpanBetweenTries, Action<Exception> onErrorOccurred = null)
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
                        onErrorOccurred?.Invoke(ex);

                    Thread.Sleep(waitTimeSpanBetweenTries);
                }
                finally
                {
                    if (_try == retries - 1 || false == errorOccurred)
                        foreach (var _disposable in disposable)
                            if (null != _disposable)
                                _disposable.Dispose();

                    _try++;
                }
            }
        }
    }
}