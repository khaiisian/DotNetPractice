namespace DotNetPractice.RestApiRedo.Model
{
    public class ParamModel
    {
        public ParamModel(string name, object value)
        {
            Name = name; 
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
