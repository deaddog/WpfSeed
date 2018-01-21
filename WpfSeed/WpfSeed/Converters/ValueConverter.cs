using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfSeed.Converters
{
    public abstract class ValueConverter<TIn, TOut> : IValueConverter
    {
        public abstract TOut Convert(TIn value, CultureInfo culture);
        public abstract TIn ConvertBack(TOut value, CultureInfo culture);

        private readonly bool _testTargetType;

        public ValueConverter(bool testTargetType = true)
        {
            _testTargetType = testTargetType;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_testTargetType && targetType != typeof(TOut) && !targetType.IsSubclassOf(typeof(TOut)))
                throw new InvalidCastException($"{GetType()?.Name}.Convert can only convert to type {typeof(TOut)?.Name}, {targetType?.Name} is expected.");
            else if (value is null)
                return Convert((TIn)value, culture);
            else if (value is TIn inValue)
                return Convert(inValue, culture);
            else
                throw new ArgumentException($"{GetType()?.Name}.Convert expects type {typeof(TIn)?.Name} as input, {value.GetType()?.Name} is not supported.", nameof(value));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_testTargetType && targetType != typeof(TIn) && !targetType.IsSubclassOf(typeof(TIn)))
                throw new InvalidCastException($"{GetType()?.Name}.ConvertBack can only convert to type {typeof(TIn)?.Name}, {targetType?.Name} is expected.");
            else if (value is null)
                return ConvertBack((TOut)value, culture);
            else if (value is TOut outValue)
                return ConvertBack(outValue, culture);
            else
                throw new ArgumentException($"{GetType()?.Name}.ConvertBack expects type {typeof(TOut)?.Name} as input, {value.GetType()?.Name} is not supported.", nameof(value));
        }
    }
}
