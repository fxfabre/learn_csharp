﻿//using System.ServiceModel;


namespace WCF_CalculatorLib
{
//    [ServiceContract]
    public interface ICalculator
    {
//        [OperationContract]
        double Add(double n1, double n2);

        //[OperationContract]
        double Subtract(double n1, double n2);

//        [OperationContract]
        double Multiply(double n1, double n2);

//        [OperationContract]
        double Divide(double n1, double n2);
    }
}