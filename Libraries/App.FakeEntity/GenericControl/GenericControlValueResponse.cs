using System.Collections.Generic;

namespace App.FakeEntity.GenericControl
{
    public class GenericControlValueResponse
    {
        public string __RequestVerificationToken { get; set; }
        public List<ControlValueItemResponse> controlValueItemResponse { get; set; }
     //   public List<ValueItemResponse> ValueItemResponse { get; set; }
    }

    public class ControlValueItemResponse
    {        
        public int GenericControlValueId { get; set; }

        public string Name { get; set; }
        public string ValueName { get; set; }

    }

    //public class ValueItemResponse
    //{
    //    public int Id { get; set; }
    //    public int EntityId { get; set; }
    //    public int GenericControlValueId { get; set; }
    //    public string Value { get; set; }
    //}
}