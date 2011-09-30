using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LearnMVC.WebServices
{
    [ServiceContract(Namespace = "http://tempuri.org", Name = "FileConverter")]
    public interface IWCFService
    {
        [OperationContract]
        int GetInt(int i);

        [OperationContract]
        ComplexResponse ComplexMethod(ComplexRequest request);

        [OperationContract]
        bool UploadFile();
    }
}
