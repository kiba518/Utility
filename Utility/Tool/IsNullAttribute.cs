namespace Utility
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class IsNullAttribute : System.Attribute
    {
        public string IsNull { get; set; }
        public IsNullAttribute(string isNull)
        {
            this.IsNull = isNull;
        }
    }
}
