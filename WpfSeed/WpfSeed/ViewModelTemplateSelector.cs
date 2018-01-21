using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfSeed
{
    public class ViewModelTemplateSelector : DataTemplateSelector
    {
        private Regex _viewModelNameRegex;
        private Type _viewModelType;
        private Type _viewType;

        public Type RootViewModelType
        {
            get { return _viewModelType; }
            set
            {
                _viewModelType = value;
                _viewModelNameRegex = new Regex("^" + _viewModelType.Namespace.Replace(".", "\\.") + @"\.(.*View)Model$");
            }
        }
        public Type RootViewType
        {
            get { return _viewType; }
            set { _viewType = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            if (_viewModelType == null || _viewType == null)
                throw new InvalidOperationException(nameof(ViewModelTemplateSelector) + " cannot select a template when " + nameof(RootViewModelType) + " or " + nameof(RootViewType) + " is not set.");

            var itemType = item.GetType();
            var match = _viewModelNameRegex.Match(itemType.FullName);
            if (!match.Success)
                return null;

            var viewTypeName = match.Groups[1].Value;
            var viewType = _viewType.Assembly.GetType(viewTypeName);

            if (viewType == null)
                return null;
            else
                return new DataTemplate { VisualTree = new FrameworkElementFactory(viewType) };
        }
    }
}
