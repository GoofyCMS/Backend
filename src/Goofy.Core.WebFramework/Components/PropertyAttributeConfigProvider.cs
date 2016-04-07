using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Goofy.Core.Entity.Base;

namespace Goofy.Core.WebFramework.Components
{
    public class PropertyAttributeConfigProvider : IEntityPropertyAttributeConfigProvider
    {
        public KeyValuePair<string, object> GetConfig(Attribute propertyAttribute)
        {
            if (propertyAttribute is RequiredAttribute)
                return new KeyValuePair<string, object>("validations", new List<string> { "required" });
            else if (propertyAttribute is StringLengthAttribute)
            {
                var attr = propertyAttribute as StringLengthAttribute;
                return new KeyValuePair<string, object>("validations", new List<string> { string.Format("length_{0}_{1}", attr.MinimumLength, attr.MaximumLength) });
            }
            else if (propertyAttribute is MaxLengthAttribute)
            {
                var attr = propertyAttribute as MaxLengthAttribute;
                return new KeyValuePair<string, object>("validations", new List<string> { string.Format("max_length_{0}", attr.Length) });
            }
            else if (propertyAttribute is MinLengthAttribute)
            {
                var attr = propertyAttribute as MinLengthAttribute;
                return new KeyValuePair<string, object>("validations", new List<string> { string.Format("min_length_{0}", attr.Length) });
            }
            else if (propertyAttribute is DataTypeAttribute)
            {
                return new KeyValuePair<string, object>("data_type", GetDataTypeText(((DataTypeAttribute)propertyAttribute).DataType));
            }
            return default(KeyValuePair<string, object>);
        }

        private string GetDataTypeText(DataType dataType)
        {
            if (dataType == DataType.EmailAddress)
                return "email";
            else if (dataType == DataType.Password)
                return "password";
            else if (dataType == DataType.Text)
                return "text";
            else if (dataType == DataType.Url)
                return "url";
            else if (dataType == DataType.Upload)
                return "upload";
            else if (dataType == DataType.Time)
                return "time";
            else if (dataType == DataType.Date)
                return "date";
            else if (dataType == DataType.DateTime)
                return "datetime";
            else if (dataType == DataType.Duration)
                return "duration";
            else if (dataType == DataType.Html)
                return "html";
            else if (dataType == DataType.ImageUrl)
                return "imageUrl";
            else if (dataType == DataType.MultilineText)
                return "multilineText";
            else if (dataType == DataType.PhoneNumber)
                return "phoneNumber";
            else if (dataType == DataType.PostalCode)
                return "postalCode";
            else if (dataType == DataType.Currency)
                return "currency";
            else if (dataType == DataType.Custom)
                return "custom";
            else return "creditCard";
        }
    }
}
