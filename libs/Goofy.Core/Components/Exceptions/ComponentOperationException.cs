using System;

namespace Goofy.Core.Components.Exceptions
{
    public class ComponentOperationException : Exception
    {
        private readonly Component _component;
        private readonly bool _installing;

        public ComponentOperationException(Component component, bool installing)
        {
            _component = component;
            _installing = installing;
        }

        public override string Message
        {
            get
            {
                string message;
                if (_installing)
                    message = string.Format("Trying to install component \"{0}\" already installed", _component.FullName);
                else
                    message = string.Format("Trying to uninstall the non-installed component \"{0}\"", _component.FullName);
                return message;
            }
        }
    }
}
