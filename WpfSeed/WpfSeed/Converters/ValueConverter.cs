using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfSeed.Converters
{
    public abstract class ValueConverter<TIn, TOut> : IValueConverter
    {
        public abstract TOut Convert(TIn value, CultureInfo culture);
        public abstract TIn ConvertBack(TOut value, CultureInfo culture);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return Convert((TIn)value, culture);
            else if (value is TIn inValue)
                return Convert(inValue, culture);
            else
                throw new ArgumentException($"{GetType()?.Name}.Convert expects type {typeof(TIn)?.Name} as input, {value.GetType()?.Name} is not supported.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return ConvertBack((TOut)value, culture);
            else if (value is TOut outValue)
                return ConvertBack(outValue, culture);
            else
                throw new ArgumentException($"{GetType()?.Name}.ConvertBack expects type {typeof(TOut)?.Name} as input, {value.GetType()?.Name} is not supported.");
        }
    }
}
