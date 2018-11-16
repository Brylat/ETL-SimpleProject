namespace Etl.Extract.Service.dto{
    public class CarModelDto{

        public CarModelDto() { }

        public CarModelDto(string _modelName, string _modelValue){
            modelName = _modelName;
            modelValue = _modelValue;
        }
        public string modelName { get; set; }
        public string modelValue { get; set; }
    }
}
