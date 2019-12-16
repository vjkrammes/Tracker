using System;
using System.ComponentModel.DataAnnotations;

namespace TrackerCommon
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class DescriptionAttribute : Attribute
    {
        private readonly string _description;
        public DescriptionAttribute(string description) => _description = description;
        public string Description { get => _description; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class HasNullableMembersAttribute : Attribute
    {
        private readonly bool _hasNullableMembers;
        public HasNullableMembersAttribute() => _hasNullableMembers = true;
        public HasNullableMembersAttribute(bool value) => _hasNullableMembers = value;
        public bool HasNullableMembers { get => _hasNullableMembers; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class NullOnUpdateAttribute : Attribute
    {
        private readonly bool _nullify;
        public NullOnUpdateAttribute() => _nullify = true;
        public NullOnUpdateAttribute(bool value) => _nullify = value;
        public bool NullOnUpdate { get => _nullify; }
    }
    
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class NullOnInsertAttribute : Attribute
    {
        private readonly bool _nullify;
        public NullOnInsertAttribute() => _nullify = true;
        public NullOnInsertAttribute(bool value) => _nullify = value;
        public bool NullOnInsert { get => _nullify; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class NonNegativeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            switch (value)
            {
                case int ival:
                    valid = ival >= 0;
                    break;
                case long lval:
                    valid = lval >= 0;
                    break;
                case float fval:
                    valid = fval >= 0.0;
                    break;
                case double dval:
                    valid = dval >= 0.0;
                    break;
                case decimal mval:
                    valid = mval >= 0M;
                    break;
            }
            if (!valid)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    var prop = validationContext.DisplayName;
                    return new ValidationResult($"{prop} must be greater than or equal to zero");
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class PositiveAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool valid = false;
            switch (value)
            {
                case int ival:
                    valid = ival > 0;
                    break;
                case long lval:
                    valid = lval > 0;
                    break;
                case float fval:
                    valid = fval > 0.0;
                    break;
                case double dval:
                    valid = dval > 0.0;
                    break;
                case decimal mval:
                    valid = mval > 0M;
                    break;
            }
            if (!valid)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    var prop = validationContext.DisplayName;
                    return new ValidationResult($"{prop} must be greater than zero");
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return null;
        }
    }
}
