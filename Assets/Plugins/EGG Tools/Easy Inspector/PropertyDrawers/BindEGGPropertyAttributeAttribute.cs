using EGG.Attributes;
using System;

[AttributeUsage(AttributeTargets.Class)]
public class BindEGGPropertyAttributeAttribute : System.Attribute
{
    public Type attributeType;

    public BindEGGPropertyAttributeAttribute(Type attributeTypeToBind)
    {
        attributeType = attributeTypeToBind;
    }
}