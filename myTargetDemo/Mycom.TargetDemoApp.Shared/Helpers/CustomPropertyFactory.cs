using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Mycom.TargetDemoApp.Helpers
{
    internal static class CustomPropertyFactory<TSource>
    {
        public static ICustomProperty Create<TValue>(String name,
                                                     Func<TSource, TValue> getValueFunc = null,
                                                     Action<TSource, TValue> setValueAction = null)
        {
            return new CustomProperty<TValue>(name,
                                              getValueFunc,
                                              setValueAction);
        }

        public static ICustomProperty Create<TValue, TIndexedValue>(String name,
                                                                    Func<TSource, TValue> getValueFunc = null,
                                                                    Func<TSource, Object, TIndexedValue> getIndexedValueFunc = null,
                                                                    Action<TSource, TValue> setValueAction = null,
                                                                    Action<TSource, TIndexedValue, Object> setIndexedValueAction = null)
        {
            return new CustomProperty<TValue, TIndexedValue>(name,
                                                             getValueFunc,
                                                             getIndexedValueFunc,
                                                             setValueAction,
                                                             setIndexedValueAction);
        }

        public static IReadOnlyDictionary<String, ICustomProperty> CreateDictionary(params ICustomProperty[] properties)
        {
            return properties?.ToDictionary(property => property.Name);
        }

        private sealed class CustomProperty<TValue> : ICustomProperty
        {
            private readonly Func<TSource, TValue> _getValueFunc;
            private readonly Action<TSource, TValue> _setValueAction;

            public CustomProperty(String name,
                                  Func<TSource, TValue> getValueFunc = null,
                                  Action<TSource, TValue> setValueAction = null)
            {
                _getValueFunc = getValueFunc;
                _setValueAction = setValueAction;

                Name = name;
                Type = typeof (TValue);
                CanRead = _getValueFunc != null;
                CanWrite = _setValueAction != null;
            }

            public Object GetIndexedValue(Object target, Object index) => null;

            public Object GetValue(Object target) => _getValueFunc != null ? _getValueFunc((TSource) target) : default(Object);

            public void SetIndexedValue(Object target, Object value, Object index) { }

            public void SetValue(Object target, Object value) => _setValueAction?.Invoke((TSource) target, (TValue) value);

            public Boolean CanRead { get; }

            public Boolean CanWrite { get; }

            public String Name { get; }

            public Type Type { get; }
        }

        private sealed class CustomProperty<TValue, TIndexedValue> : ICustomProperty
        {
            private readonly Func<TSource, Object, TIndexedValue> _getIndexedValueFunc;
            private readonly Func<TSource, TValue> _getValueFunc;
            private readonly Action<TSource, TIndexedValue, Object> _setIndexedValueAction;
            private readonly Action<TSource, TValue> _setValueAction;

            public CustomProperty(String name,
                                  Func<TSource, TValue> getValueFunc = null,
                                  Func<TSource, Object, TIndexedValue> getIndexedValueFunc = null,
                                  Action<TSource, TValue> setValueAction = null,
                                  Action<TSource, TIndexedValue, Object> setIndexedValueAction = null)
            {
                _getValueFunc = getValueFunc;
                _getIndexedValueFunc = getIndexedValueFunc;
                _setValueAction = setValueAction;
                _setIndexedValueAction = setIndexedValueAction;

                Name = name;
                Type = typeof (TValue);
                CanRead = _getValueFunc != null || _getIndexedValueFunc != null;
                CanWrite = _setValueAction != null || _setIndexedValueAction != null;
            }

            public Object GetIndexedValue(Object target, Object index)
            {
                if (_getIndexedValueFunc != null)
                {
                    return _getIndexedValueFunc((TSource) target, index);
                }
                return null;
            }

            public Object GetValue(Object target)
            {
                if (_getValueFunc != null)
                {
                    return _getValueFunc.Invoke((TSource) target);
                }

                return null;
            }

            public void SetIndexedValue(Object target, Object value, Object index) => _setIndexedValueAction?.Invoke((TSource) target, (TIndexedValue) value, index);

            public void SetValue(Object target, Object value) => _setValueAction?.Invoke((TSource) target, (TValue) value);

            public Boolean CanRead { get; }

            public Boolean CanWrite { get; }

            public String Name { get; }

            public Type Type { get; }
        }
    }
}