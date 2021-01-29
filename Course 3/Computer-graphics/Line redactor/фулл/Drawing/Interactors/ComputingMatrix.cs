using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Drawing.Interactors
{
    class ComputingMatrix
    {
        private Matrix<double> data;
        private Matrix<double> operation;
        
        public ComputingMatrix() { }

        public void AddData(double[,] data) => this.data = Matrix<double>.Build.DenseOfArray(data);

        public void AddOperation(double[,] operation) => this.operation = Matrix<double>.Build.DenseOfArray(operation);

        public void ComputeMatrix()
        {
            data = data.Multiply(operation);
        }

        public double[,] GetNonNormalizedResult()
        {
            return data.ToArray();
        }

        public double[,] GetResult()
        {
            var arrayData = data.ToArray();

            var result = new double[data.RowCount, 4];
            for (var i = 0; i < data.RowCount; ++i)
            {
                result[i, 0] = arrayData[i, 0] / arrayData[i, 3];
                result[i, 1] = arrayData[i, 1] / arrayData[i, 3];
                result[i, 2] = arrayData[i, 2] / arrayData[i, 3];
                result[i, 3] = arrayData[i, 3] / arrayData[i, 3];
            }
            return result;
        }
    }
}
